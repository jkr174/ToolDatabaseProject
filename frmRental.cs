using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KWSalesOrderFormProject
{
    public partial class frmRented : Form
    {
        bool selectedFile = false;
        string myState;
        int myBookmark,
            pageNumber;
        const int itemsPerPage = 45;

        SqlConnection rentalConnection;
        SqlCommand rentalCommand;
        SqlCommandBuilder rentalCommandBuilder;
        SqlDataAdapter rentalAdapter;
        DataTable rentalTable;
        CurrencyManager rentalManager;
        public frmRented()
        {
            InitializeComponent();
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            /*if (dlgOpen.ShowDialog() == DialogResult.OK)
            {*/
                try
                {
                    
                    rentalConnection = new SqlConnection("Data Source = (LocalDB)\\MSSQLLocalDB;" +
                        "AttachDbFilename=|DataDirectory|\\ToolRentalsDB.mdf; " +
                        "Integrated Security = True;" +
                        "Connect Timeout=30;");
                    rentalConnection.Open();

                    rentalCommand = new SqlCommand(
                        "SELECT	RentID, COALESCE(FirstName, ' ') + COALESCE(' ', ' ') + COALESCE(LastName, ' ') AS CustomerName, ItemID, SKUNumber, ProductName, RentDate " +
                        "FROM rentedItems " +
                        "JOIN customersTable " +
                        "ON rentedItems.CustomerID = customersTable.CustomerID ", rentalConnection);

                    rentalAdapter = new SqlDataAdapter();
                    rentalAdapter.SelectCommand = rentalCommand;

                    rentalCommandBuilder = new SqlCommandBuilder(rentalAdapter);
                    rentalTable = new DataTable();
                    rentalAdapter.Fill(rentalTable);
                    grdRentals.ReadOnly = true;
                    grdRentals.DataSource = rentalTable;

                    rentalManager = (CurrencyManager)
                        this.BindingContext[rentalTable];
                    
                    SetState("View");

                }
                catch (Exception ex)
                {
                    selectedFile = false;
                    MessageBox.Show(
                        ex.Message,
                        "Error establishing database file connection.",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    return;
                }
            /*}
            else
            {
                selectedFile = false;
                MessageBox.Show(
                    "No file selected. Program will now exit.",
                    "Program stopping",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                this.Close();
            }*/
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmKWSales_Load(object sender, EventArgs e)
        {

        }

        private void frmKWSales_FormClosing(object sender, FormClosingEventArgs e)
        {
            
            if (selectedFile != false)
            {
                rentalConnection.Close();
                rentalCommand.Dispose();
                rentalCommandBuilder.Dispose();
                rentalAdapter.Dispose();
                rentalTable.Dispose();
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            SetState("Edit");
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SetState("View");
        }

        private void btnAddNew_Click(object sender, EventArgs e)
        {
            try
            {
                myBookmark = rentalManager.Position;
                rentalManager.AddNew();
                SetState("Add");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            rentalManager.CancelCurrentEdit();
            if (myState.Equals("Add"))
                rentalManager.Position = myBookmark;
            SetState("View");
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            PrintDocument recordDocument;
            recordDocument = new PrintDocument();
            recordDocument.DocumentName = "Titles Record";
            recordDocument.PrintPage += new PrintPageEventHandler(this.PrintRecordPage);
            printPreview.Document = recordDocument;
            printPreview.ShowDialog();
        }
        private void PrintRecordPage(object sender, PrintPageEventArgs e)
        {
            // print graphic
            Pen myPen = new Pen(Color.Black, 3);
            e.Graphics.DrawRectangle(myPen,
                e.MarginBounds.Left,
                e.MarginBounds.Top,
                e.MarginBounds.Width,
                100);
            e.Graphics.DrawImage(picTools.Image,
                e.MarginBounds.Left + 10,
                e.MarginBounds.Top + 10, 80, 80);
            //Heading
            string s = "RENTAL TICKETS";
            Font myFont = new Font("Arial", 24, FontStyle.Bold);
            SizeF sSize = e.Graphics.MeasureString(s, myFont);
            Bitmap bm = new Bitmap(this.grdRentals.Width, this.grdRentals.Height);
            grdRentals.DrawToBitmap(bm, new Rectangle(0, 0, this.grdRentals.Width, this.grdRentals.Height));
            e.Graphics.DrawImage(bm, 0, 0);
        }

        /*private bool ValidateData()
{
   string message = "";
   bool allOK = true;
   if (txtTitle.Text.Equals(""))
   {
       message = "You must input a Title.\r\n";
       txtTitle.Focus();
       allOK = false;
   }
   int inputYear, currentYear;
   // Check length and range on Year Published
   if (!txtYear.Text.Trim().Equals(""))
   {
       inputYear = Convert.ToInt32(txtYear.Text);
       currentYear = DateTime.Now.Year;
       if (inputYear > currentYear || inputYear < currentYear - 150)
       {
           message += "Year published must be between " +
           (currentYear - 150).ToString() + " and " +
           currentYear.ToString() + "\r\n";
           txtYear.Focus();
           allOK = false;
       }
   }
}*/

        private void SetState(string appState)
        {
            myState = appState;
            switch (appState)
            {
                case "Connect":
                    /*txtTitle.ReadOnly = true;
                    txtYear.ReadOnly = true;
                    txtISBN.ReadOnly = true;
                    txtTitle.ReadOnly = true;
                    txtDescription.ReadOnly = true;
                    txtNotes.ReadOnly = true;
                    txtSubject.ReadOnly = true;
                    txtComments.ReadOnly = true;*/
                    btnFirst.Enabled = false;
                    btnPrevious.Enabled = false;
                    btnNext.Enabled = false;
                    btnLast.Enabled = false;
                    btnAddNew.Enabled = false;
                    btnSave.Enabled = false;
                    btnCancel.Enabled = false;
                    btnEdit.Enabled = false;
                    btnDelete.Enabled = false;
                    btnDone.Enabled = false;
                    /*grpFindTitle.Enabled = false;
                    cboPublisher.Enabled = false;
                    btnPublishers.Enabled = false;
                    txtTitle.Focus();*/
                    break;
                case "View":
                    /*txtTitle.ReadOnly = true;
                    txtYear.ReadOnly = true;
                    txtISBN.ReadOnly = true;
                    txtISBN.BackColor = Color.White;
                    txtISBN.ForeColor = Color.Black;
                    txtDescription.ReadOnly = true;
                    txtNotes.ReadOnly = true;
                    txtSubject.ReadOnly = true;
                    txtComments.ReadOnly = true;*/
                    btnFirst.Enabled = true;
                    btnPrevious.Enabled = true;
                    btnNext.Enabled = true;
                    btnLast.Enabled = true;
                    btnAddNew.Enabled = true;
                    btnSave.Enabled = false;
                    btnCancel.Enabled = false;
                    btnEdit.Enabled = true;
                    btnDelete.Enabled = true;
                    btnDone.Enabled = true;
                    /*grpFindTitle.Enabled = true;
                    btnPublishers.Enabled = true;
                    cboPublisher.Enabled = false;
                    cboAuthor1.Enabled = false;
                    cboAuthor2.Enabled = false;
                    cboAuthor3.Enabled = false;
                    cboAuthor4.Enabled = false;
                    btnXAuthor1.Enabled = false;
                    btnXAuthor2.Enabled = false;
                    btnXAuthor3.Enabled = false;
                    btnXAuthor4.Enabled = false;
                    txtTitle.Focus();*/
                    break;
                //Add or Edit State
                default:
                    /*txtTitle.ReadOnly = false;
                    txtYear.ReadOnly = false;
                    txtISBN.ReadOnly = false;
                    if (myState.Equals("Edit"))
                    {
                        txtISBN.BackColor = Color.Red;
                        txtISBN.ForeColor = Color.White;
                        txtISBN.ReadOnly = true;
                        txtISBN.TabStop = false;
                    }
                    else
                    {
                        txtISBN.TabStop = true;
                    }
                    txtDescription.ReadOnly = false;
                    txtNotes.ReadOnly = false;
                    txtSubject.ReadOnly = false;
                    txtComments.ReadOnly = false;*/
                    btnFirst.Enabled = false;
                    btnPrevious.Enabled = false;
                    btnNext.Enabled = false;
                    btnLast.Enabled = false;
                    btnAddNew.Enabled = false;
                    btnSave.Enabled = true;
                    btnCancel.Enabled = true;
                    btnEdit.Enabled = false;
                    btnDelete.Enabled = false;
                    btnDone.Enabled = false;
                    /*grpFindTitle.Enabled = false;
                    cboPublisher.Enabled = true;
                    cboAuthor1.Enabled = true;
                    cboAuthor2.Enabled = true;
                    cboAuthor3.Enabled = true;
                    cboAuthor4.Enabled = true;
                    btnXAuthor1.Enabled = true;
                    btnXAuthor2.Enabled = true;
                    btnXAuthor3.Enabled = true;
                    btnXAuthor4.Enabled = true;
                    txtTitle.Focus();*/
                    break;
            }
        }
        }
}
