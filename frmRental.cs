﻿using System;
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
        bool selectedFile = false,
            addTicket = false,
            addCust = false;
        string myState,
            ticketStatus;
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

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmKWSales_Load(object sender, EventArgs e)
        {
            cboStatus.Items.Add("All");
            cboStatus.Items.Add("Open");
            cboStatus.Items.Add("Closed");
            cboStatus.SelectedItem = "All";
            SetState("Connect");
            try
            {
                RentalConnection();

                rentalCommand = new SqlCommand(
                    "SELECT	RentID, Status, COALESCE(FirstName, ' ') + COALESCE(' ', ' ') + COALESCE(LastName, ' ') AS CustomerName, ItemID, SKUNumber, ProductName, RentDate " +
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
                
                rentalConnection.Close();
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
        }

        private void frmKWSales_FormClosing(object sender, FormClosingEventArgs e)
        {

            if (selectedFile != false)
            {
                try
                {
                    SqlCommandBuilder rentalCommandBuilder = new SqlCommandBuilder(rentalAdapter);
                    rentalAdapter.Update(rentalTable);
                    rentalCommand.Dispose();
                    rentalCommandBuilder.Dispose();
                    rentalAdapter.Dispose();
                    rentalTable.Dispose();
                }
                catch(Exception ex)
                {
                    MessageBox.Show(
                        "Error saving database to file:\r\n" + ex.Message, 
                        "Save Error", 
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);

                }
                rentalAdapter.Update(rentalTable);
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
            DateTime now = DateTime.Now;
            try
            {
                if (addTicket == true)
                {
                    RentalConnection();
                    rentalCommand = new SqlCommand( "INSERT INTO rentedItems (CustomerID, ItemID, SKUNumber, ProductName, RentDate) " +
                                                    "VALUES (" +
                                                        "'" + txtCustID.Text + "'" +
                                                        ",'" + txtItemID.Text + "'" +
                                                        ",'" + txtSKUNumber.Text + "'" +
                                                        ",'" +txtProductName.Text + "'" +
                                                        ",'" + now + "');", rentalConnection);
                    rentalAdapter = new SqlDataAdapter();
                    rentalAdapter.InsertCommand = rentalCommand;
                    MessageBox.Show("Ticket saved.",
                        "Save",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                    rentalConnection.Close();
                }
                else if (addCust == true)
                {
                    rentalCommand = new SqlCommand( "INSERT INTO customersTable (CustomerID, FirstName, MiddleName, LastName, Email, Address, Phone) " +
                                                    "VALUES (" +
                                                        "'" + txtCustID.Text + "'" +
                                                        ",'" + txtItemID.Text + "'" +
                                                        ",'" + txtSKUNumber.Text + "'" +
                                                        ",'" + txtProductName.Text + "'" +
                                                        ",'" + txtLastName.Text + "'" +
                                                        ",'" + txtEmail.Text + "'" +
                                                        ",'" + txtAddress.Text + "'" +
                                                        ",'" + txtPhone.Text + "'); ", rentalConnection);
                    rentalAdapter = new SqlDataAdapter();
                    rentalAdapter.InsertCommand = rentalCommand;
                    MessageBox.Show("Customer saved.",
                        "Save",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,
                    "Error saving information.",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            SetState("View");
        }

        private void btnAddNew_Click(object sender, EventArgs e)
        {
            try
            {
                myBookmark = rentalManager.Position;
                rentalManager.AddNew();
                SetState("Add");
                addTicket = true;
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
            grdRentals.DrawToBitmap(bm, new Rectangle(10, 175, this.grdRentals.Width, this.grdRentals.Height));
            e.Graphics.DrawImage(bm, 0, 0);
        }
        public void RentalConnection()
        {
            rentalConnection = new SqlConnection(
                    "Data Source = (LocalDB)\\MSSQLLocalDB;" +
                    "AttachDbFilename=|DataDirectory|\\ToolRentalsDB.mdf; " +
                    "Integrated Security = True;" +
                    "Connect Timeout=30;");
            rentalConnection.Open();
        }
        private void btnInventory_Click(object sender, EventArgs e)
        {
            try
            {
                frmInventory inventoryForm = new frmInventory();
                inventoryForm.ShowDialog();
                inventoryForm.Dispose();
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,
                    "Error!",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void btnAddCust_Click(object sender, EventArgs e)
        {
            SetState("Add Customer");
            addCust = true;
        }
        private void textBox1_TextChanged_2(object sender, EventArgs e)
        {
            RentalConnection();
            if(cboStatus.Text == "All")
            {
                rentalAdapter = new SqlDataAdapter(
                "SELECT	* " +
                "FROM rentedItems " +
                "WHERE RentID " +
                "LIKE '" + txtSearch.Text + "%'", rentalConnection);
            }
            else if (cboStatus.Text != null)
            {
                rentalAdapter = new SqlDataAdapter(
                "SELECT	* " +
                "FROM rentedItems " +
                "WHERE RentID " +
                "LIKE '" + txtSearch.Text + "%'" +
                "AND Status " +
                "LIKE '" + ticketStatus + "%' ", rentalConnection);
            }
            else
            {
                rentalAdapter = new SqlDataAdapter(
                "SELECT	* " +
                "FROM rentedItems " +
                "WHERE RentID " +
                "LIKE '" + txtSearch.Text + "%'", rentalConnection);
            }
            
            rentalTable = new DataTable();
            rentalAdapter.Fill(rentalTable);
            grdRentals.DataSource = rentalTable;
            rentalConnection.Close();
        }

        private void btnRepairs_Click(object sender, EventArgs e)
        {
            try
            {
                frmRepairs repairsForm = new frmRepairs();
                repairsForm.ShowDialog();
                repairsForm.Dispose();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,
                    "Error!",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void cboStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            ticketStatus = cboStatus.SelectedItem.ToString();
            textBox1_TextChanged_2(txtSearch, EventArgs.Empty);
        }
        private void SetState(string appState)
        {
            myState = appState;
            switch (appState)
            {
                case "Connect":
                    btnFirst.Enabled = false;
                    btnPrevious.Enabled = false;
                    btnLast.Enabled = false;
                    btnAddNew.Enabled = false;
                    btnSave.Enabled = false;
                    btnCancel.Enabled = false;
                    btnEdit.Enabled = false;
                    btnDelete.Enabled = false;
                    btnPrint.Enabled = false;
                    btnAddCust.Enabled = false;
                    btnInventory.Enabled = false;
                    btnRepairs.Enabled = false;
                    txtSearch.Enabled = false;
                    break;
                case "View":
                    btnAddCust.Enabled = true;
                    btnFirst.Enabled = true;
                    btnPrevious.Enabled = true;
                    btnLast.Enabled = true;
                    btnAddNew.Enabled = true;
                    btnSave.Enabled = false;
                    btnCancel.Enabled = false;
                    btnEdit.Enabled = true;
                    btnDelete.Enabled = true;
                    btnPrint.Enabled = true;
                    btnInventory.Enabled = true;
                    btnRepairs.Enabled = true;
                    grdRentals.Visible = true;
                    lblCustID.Visible = false;
                    lblItemID.Visible = false;
                    lblProductName.Visible = false;
                    lblLastName.Visible = false;
                    lblSKUNumber.Visible = false;
                    lblEmail.Visible = false;
                    lblAddress.Visible = false;
                    lblPhone.Visible = false;
                    txtLastName.Visible = false;
                    txtCustID.Visible = false;
                    txtItemID.Visible = false;
                    txtProductName.Visible = false;
                    txtSKUNumber.Visible = false;
                    txtEmail.Visible = false;
                    txtAddress.Visible = false;
                    txtPhone.Visible = false;
                    txtSearch.Enabled = true;
                    addCust = false;
                    addTicket = false;
                    break;
                case "Add Customer":
                    btnFirst.Enabled = false;
                    btnPrevious.Enabled = false;
                    btnLast.Enabled = false;
                    btnAddNew.Enabled = false;
                    btnSave.Enabled = true;
                    btnCancel.Enabled = true;
                    btnEdit.Enabled = false;
                    btnDelete.Enabled = false;
                    btnPrint.Enabled = false;
                    btnAddCust.Enabled = false;
                    grdRentals.Visible = false;
                    lblCustID.Text = "Customer ID:";
                    lblItemID.Text = "First Name:";
                    lblProductName.Text = "Middle Name:";
                    lblSKUNumber.Text = "Last Name:";
                    lblCustID.Visible = true;
                    lblItemID.Visible = true;
                    lblProductName.Visible = true;
                    lblSKUNumber.Visible = true;
                    lblLastName.Visible = true;
                    lblEmail.Visible = true;
                    lblAddress.Visible = true;
                    lblPhone.Visible = true;
                    txtCustID.Visible = true;
                    txtItemID.Visible = true;
                    txtProductName.Visible = true;
                    txtSKUNumber.Visible = true;
                    txtLastName.Visible = true;
                    txtEmail.Visible = true;
                    txtAddress.Visible = true;
                    txtPhone.Visible = true;
                    txtSearch.Enabled = false;  
                    break;
                //Add or Edit State
                default:
                    btnFirst.Enabled = false;
                    btnPrevious.Enabled = false;
                    btnLast.Enabled = false;
                    btnAddNew.Enabled = false;
                    btnSave.Enabled = true;
                    btnCancel.Enabled = true;
                    btnEdit.Enabled = false;
                    btnDelete.Enabled = false;
                    btnPrint.Enabled = false;
                    grdRentals.Visible = false;
                    lblCustID.Visible = true;
                    lblItemID.Visible = true;
                    lblProductName.Visible = true;
                    lblSKUNumber.Visible = true;
                    lblCustID.Text = "Customer ID:";
                    lblItemID.Text = "Item ID:";
                    lblProductName.Text = "Product Name:";
                    lblSKUNumber.Text = "SKU Number:";
                    txtCustID.Visible = true;
                    txtItemID.Visible = true;
                    txtProductName.Visible = true;
                    txtSKUNumber.Visible = true;
                    txtSearch.Enabled = false;
                    break;
            }
        }
    }
}
