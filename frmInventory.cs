/*
 * Created by: Jovany Romo
 * Updated: 5-9-2021
 * 
 * Summary: Allows for the user to view, add, edit, and delete the inventory table in the database. 
 */
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
    public partial class frmInventory : Form
    {
        bool selectedFile = false;
        string myState;

        SqlConnection inventoryConnection;
        SqlCommand inventoryCommand;
        SqlCommandBuilder inventoryCommandBuilder;
        SqlDataAdapter inventoryAdapter;
        DataTable inventoryTable;
        CurrencyManager inventoryManager;
        public frmInventory()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Load Event that automatically attempts to connect to the database. If it fails, then the program displays an error exception message to the user.
        /// </summary>
        /// Input: Loading
        /// Output: Display
        private void frmInventory_Load(object sender, EventArgs e)
        {
            try
            {
                InventoryConnection();

                inventoryCommand = new SqlCommand(
                    "SELECT	ItemID, ProductCategory, SKUNumber, Name, TotalQty, OnHandQty, OutQty " +
                    "FROM intentoryTable ", inventoryConnection);

                inventoryAdapter = new SqlDataAdapter();
                inventoryAdapter.SelectCommand = inventoryCommand;

                inventoryCommandBuilder = new SqlCommandBuilder(inventoryAdapter);
                inventoryTable = new DataTable();
                inventoryAdapter.Fill(inventoryTable);
                grdInventory.ReadOnly = true;
                grdInventory.DataSource = inventoryTable;

                inventoryManager = (CurrencyManager)
                    this.BindingContext[inventoryTable];
                inventoryConnection.Close();
                selectedFile = true;
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

        }
        /// <summary>
        /// Event for when the application closes and attempts to save changes made to the database.
        /// </summary>
        /// Input: Closing
        /// Output: Application saves and closes.
        private void frmInventory_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (selectedFile != false)
            {
                inventoryConnection.Dispose();
                inventoryCommand.Dispose();
                inventoryAdapter.Dispose();
                inventoryTable.Dispose();
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnAddNew_Click(object sender, EventArgs e)
        {
            SetState("Add");

        }
        /// <summary>
        /// Method that handles how the application looks during different sections of it, allowing for more usability throughout the application. 
        /// </summary>
        /// Input: Updated as the user uses the application
        /// Output: Application changes how it looks and works.
        private void SetState(string appState)
        {
            myState = appState;
            switch (appState)
            {
                case "View":
                    grdInventory.Visible = true;
                    grdInventory.ReadOnly = true;
                    btnAddNew.Enabled = true;
                    btnSave.Enabled = false;
                    btnCancel.Enabled = false;
                    btnEdit.Enabled = true;
                    btnDelete.Enabled = true;
                    btnPrint.Enabled = true;
                    btnExit.Enabled = true;
                    txtSearch.Enabled = true;
                    txtItemID.Visible = false;
                    txtCat.Visible = false;
                    txtSKUNumber.Visible = false;
                    txtProductName.Visible = false;
                    txtPrice.Visible = false;
                    lblItemID.Visible = false;
                    lblCat.Visible = false;
                    lblSKUNumber.Visible = false;
                    lblProductName.Visible = false;
                    lblPrice.Visible = false;
                    break;
                case "Edit":
                    grdInventory.Visible = true;
                    grdInventory.ReadOnly = false;
                    btnAddNew.Enabled = false;
                    btnSave.Enabled = true;
                    btnCancel.Enabled = true;
                    btnEdit.Enabled = false;
                    btnDelete.Enabled = false;
                    btnPrint.Enabled = false;
                    btnExit.Enabled = false;
                    break;
                //Add State
                default:
                    grdInventory.Visible = false;
                    btnAddNew.Enabled = false;
                    btnSave.Enabled = true;
                    btnCancel.Enabled = true;
                    btnEdit.Enabled = false;
                    btnDelete.Enabled = false;
                    btnPrint.Enabled = false;
                    btnExit.Enabled = false;
                    txtItemID.Visible = true;
                    txtCat.Visible = true;
                    txtSKUNumber.Visible = true;
                    txtProductName.Visible = true;
                    txtPrice.Visible = true;
                    txtSearch.Enabled = false;
                    lblItemID.Visible = true;
                    lblCat.Visible = true;
                    lblSKUNumber.Visible = true;
                    lblProductName.Visible = true;
                    lblPrice.Visible = true;
                    break;
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            grdInventory.EditMode = DataGridViewEditMode.EditOnKeystroke;
            SetState("Edit");
        }
        /// <summary>
        /// How the application handles saving information from the user in different states.
        /// </summary>
        /// Input: Click save button event
        /// Output: Confirmation Message Box and saved information to the database. 
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (myState == "Edit")
                {
                    try
                    {
                        InventoryConnection();

                        inventoryAdapter.Update(inventoryTable);

                        MessageBox.Show("Record(s) Updated.",
                            "Saved",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
                        inventoryConnection.Close();
                    }
                    catch(Exception ex)
                    {
                        MessageBox.Show(ex.Message,
                            "Error!",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                    }
                }
                else if (myState == "Add")
                {
                    try
                    {
                        InventoryConnection();
                        inventoryCommand = new SqlCommand(
                            "INSERT INTO intentoryTable (ItemID, ProductCategory, SKUNumber, Name, Price, TotalQty, OnHandQty, OutQty) " +
                            "VALUES (" +
                                "'" + txtItemID.Text + "'" +
                                ",'" + txtCat.Text + "'" +
                                ",'" + txtSKUNumber.Text + "'" +
                                ",'" + txtProductName.Text + "'" +
                                ",'" + txtPrice.Text + "'" +
                                ",'1'" +
                                ",'1'" +
                                ",'0')", inventoryConnection);
                        inventoryCommand.ExecuteNonQuery();
                        inventoryConnection.Close();

                        MessageBox.Show("Item saved.",
                            "Save",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
                    }
                    catch(Exception ex)
                    {
                        MessageBox.Show(ex.Message,
                            "Error saving to database.",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,
                    "Error saving information.",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            finally
            {
                frmInventory_Load(sender, e);
            }
            SetState("View");
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            inventoryManager.CancelCurrentEdit();
            SetState("View");
        }
        /// <summary>
        /// Connection string that uses localDB to connect to the database file and automatically opens the connection.
        /// </summary>
        public void InventoryConnection()
        {
            inventoryConnection = new SqlConnection(
                    "Data Source=(LocalDB)\\MSSQLLocalDB;" +
                    "AttachDbFilename=|DataDirectory|\\ToolRentalsDB.mdf;" +
                    "Integrated Security=True;" +
                    "Connect Timeout=30");
            inventoryConnection.Open();
        }
        /// <summary>
        /// Dynamically able to search the database
        /// </summary>
        /// Input: user text input
        /// Output: Datagrid is updated as you type
        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            InventoryConnection();

            inventoryCommand = new SqlCommand(
                "SELECT	*" +
                "FROM intentoryTable " +
                "WHERE Name LIKE '"+txtSearch.Text+"%'", inventoryConnection);

            inventoryAdapter = new SqlDataAdapter();
            inventoryAdapter.SelectCommand = inventoryCommand;

            inventoryCommandBuilder = new SqlCommandBuilder(inventoryAdapter);
            inventoryTable = new DataTable();
            inventoryAdapter.Fill(inventoryTable);
            grdInventory.ReadOnly = true;
            grdInventory.DataSource = inventoryTable;

            inventoryConnection.Close();
        }
        /// <summary>
        /// Allows for the user to delete rows on the table. Displays a warning message to confirm if the user wants to delete the information.
        /// </summary>
        /// Input: Delete button click event
        /// Output: Updates the datagrid and the database with the changed information.
        private void btnDelete_Click(object sender, EventArgs e)
        {
            DialogResult dialog = MessageBox.Show("Are you sure you want to delete this row?", "Warning", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
            if (dialog == DialogResult.Yes)
            {
                try
                {
                    grdInventory.Rows.RemoveAt(grdInventory.CurrentRow.Index);
                    inventoryAdapter.Update(inventoryTable);
                    frmInventory_Load(sender, e);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            CaptureScreen();
            PrintDocument recordDocument;
            recordDocument = new PrintDocument();
            recordDocument.DocumentName = "Titles Record";
            recordDocument.PrintPage += new PrintPageEventHandler(this.PrintRecordPage);
            printPreview.Document = recordDocument;
            printPreview.ShowDialog();
        }
        Bitmap memoryImage;
        /// <summary>
        /// Captures the current screen, enabling the user to be able to print out a ticket.
        /// </summary>
        private void CaptureScreen()
        {
            Graphics myGraphics = this.CreateGraphics();
            Size s = this.Size;
            memoryImage = new Bitmap(s.Width, s.Height, myGraphics);
            Graphics memoryGraphics = Graphics.FromImage(memoryImage);
            memoryGraphics.CopyFromScreen(this.Location.X, this.Location.Y, 0, 0, s);
        }
        private void PrintRecordPage(object sender, PrintPageEventArgs e)
        {
            Pen myPen = new Pen(Color.Black, 3);
            switch (myState)
            {
                case "View":
                    e.Graphics.DrawImage(picTools.Image,
                        e.MarginBounds.Left + 10,
                        e.MarginBounds.Top + 10, 80, 80);
                    Bitmap bm = new Bitmap(this.grdInventory.Width, this.grdInventory.Height);
                    grdInventory.DrawToBitmap(bm, new Rectangle(10, 175, this.grdInventory.Width, this.grdInventory.Height));
                    e.Graphics.DrawImage(bm, 0, 0);
                    break;
                //Single Record
                default:
                    e.Graphics.DrawImage(memoryImage, 20, 0);
                    break;
            }

        }
    }
}
