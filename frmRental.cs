/*
 * Created by: Jovany Romo
 * Date Updated: 5/9/2021
 * 
 * Summary: User is able to view, add, edit, and delete to the Rental Table in the database.
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
    public partial class frmRented : Form
    {
        bool selectedFile = false,
            addTicket = false,
            addCust = false;
        string myState,
            ticketStatus;

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
            Close();
        }
        /// <summary>
        /// Load Event that automatically attempts to connect to the database. If it fails, then the program displays an error exception message to the user.
        /// </summary>
        /// Input: Loading
        /// Output: Display
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
        /// <summary>
        /// Event for when the application closes and attempts to save changes made to the database.
        /// </summary>
        /// Input: Closing
        /// Output: Application saves and closes.
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
        /// <summary>
        /// How the application handles saving information from the user in different states.
        /// </summary>
        /// Input: Click save button event
        /// Output: Confirmation Message Box and saved information to the database. 
        private void btnSave_Click(object sender, EventArgs e)
        {
            RentalConnection();
            DateTime now = DateTime.Now;
            try
            {
                if (addTicket == true)
                {
                    rentalCommand = new SqlCommand("INSERT INTO rentedItems (CustomerID, ItemID, SKUNumber, ProductName, RentDate) " +
                                                    "VALUES (" +
                                                        "'" + txtCustID.Text + "'" +
                                                        ",'" + txtItemID.Text + "'" +
                                                        ",'" + txtSKUNumber.Text + "'" +
                                                        ",'" + txtProductName.Text + "'" +
                                                        ",'" + now + "')", rentalConnection);
                    rentalCommand.ExecuteNonQuery();
                    MessageBox.Show("Ticket saved.",
                        "Save",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                    
                }
                else if (addCust == true)
                {
                    rentalCommand = new SqlCommand("INSERT INTO customersTable (CustomerID, FirstName, MiddleName, LastName, Email, Address, Phone) " +
                                                    "VALUES (" +
                                                        "'" + txtCustID.Text + "'" +
                                                        ",'" + txtItemID.Text + "'" +
                                                        ",'" + txtSKUNumber.Text + "'" +
                                                        ",'" + txtProductName.Text + "'" +
                                                        ",'" + txtEmail.Text + "'" +
                                                        ",'" + txtAddress.Text + "'" +
                                                        ",'" + txtPhone.Text + "')", rentalConnection);
                    rentalCommand.ExecuteNonQuery();
                    MessageBox.Show("Customer saved.",
                        "Save",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }
                else if (myState == "Edit")
                {
                    rentalCommand = new SqlCommand(
                        "UPDATE rentedItems " +
                        "SET " +
                            "[CustomerID] = '" + txtCustID.Text.ToString() + "'" +
                            ",[ItemID] = '" + txtItemID.Text.ToString() + "'" +
                            ",[SKUNumber] = '" + txtSKUNumber.Text.ToString() + "'" +
                            ",[ProductName] = '" + txtProductName.Text.ToString() + "'" +
                            ",[Status] = '" + cboStatus.Text.ToString() + "' " +
                        "WHERE RentID = '" + txtSearch.Text.ToString() + "'", rentalConnection);
                    rentalCommand.ExecuteNonQuery();
                    MessageBox.Show("Customer saved.",
                        "Save",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }
                else if (myState=="Edit Customer")
                {
                    txtItemID.DataBindings.Add("Text", rentalTable, "FirstName");
                    txtSKUNumber.DataBindings.Add("Text", rentalTable, "MiddleName");
                    txtProductName .DataBindings.Add("Text", rentalTable, "LastName");
                    txtEmail.DataBindings.Add("Text", rentalTable, "Email");
                    txtAddress.DataBindings.Add("Text", rentalTable, "Address");
                    txtPhone.DataBindings.Add("Text", rentalTable, "Phone");
                    rentalCommand = new SqlCommand(
                        "UPDATE customersTable " +
                        "SET " +
                            "[FirstName] = '" + txtItemID.Text.ToString() + "'" +
                            ",[MiddleName] = '" + txtSKUNumber.Text.ToString() + "'" +
                            ",[LastName] = '" + txtProductName.Text.ToString() + "'" +
                            ",[Email] = '" + txtEmail.Text.ToString() + "' " +
                            ",[Address] = '" + txtAddress.Text.ToString() + "'" +
                            ",[Phone] = '" + txtPhone.Text.ToString() + "'" +
                        "WHERE CustomerID = '" + txtSearch.Text.ToString() + "'", rentalConnection);
                    rentalCommand.ExecuteNonQuery();
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
            rentalConnection.Close();
            SetState("View");

        }

        private void btnAddNew_Click(object sender, EventArgs e)
        {
                rentalManager.AddNew();
                SetState("Add");
                addTicket = true;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            rentalManager.CancelCurrentEdit();
            SetState("View");
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
        /// <summary>
        /// Either prints the datagrid for the user, or captures the current screen
        /// </summary>
        /// Input: The Print Button click event
        /// Output: The print preview dialog.
        private void PrintRecordPage(object sender, PrintPageEventArgs e)
        {
            Pen myPen = new Pen(Color.Black, 3);
            switch (myState)
            {
                case "View":
                    e.Graphics.DrawImage(picTools.Image,
                        e.MarginBounds.Left + 10,
                        e.MarginBounds.Top + 10, 80, 80);
                    Bitmap bm = new Bitmap(this.grdRentals.Width, this.grdRentals.Height);
                    grdRentals.DrawToBitmap(bm, new Rectangle(10, 175, this.grdRentals.Width, this.grdRentals.Height));
                    e.Graphics.DrawImage(bm, 0, 0);
                    break;
                    //Single Record
                default:
                    e.Graphics.DrawImage(memoryImage, 20, 0);
                    break;
            }
            
        }
        /// <summary>
        /// Connection string that uses localDB to connect to the database file and automatically opens the connection.
        /// </summary>
        public void RentalConnection()
        {
            rentalConnection = new SqlConnection(
                    "Data Source = (LocalDB)\\MSSQLLocalDB;" +
                    "AttachDbFilename=|DataDirectory|\\ToolRentalsDB.mdf; " +
                    "Integrated Security = True;" +
                    "Connect Timeout=30;");
            rentalConnection.Open();
        }
        /// <summary>
        /// Opens the Inventory Form
        /// </summary>
        /// Input: Inventory Button click event
        /// Output: 
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
        /// <summary>
        /// Dynamically able to search the database
        /// </summary>
        ///Input: user text input
        ///Output: Datagrid is updated as you type
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
        /// <summary>
        /// Allows the user to search situational events during different parts of the form, allowing to search for tickets and information that they need.
        /// </summary>
        /// Input: What the user types in the search bar, and when they click on the Search Button.
        /// Output: Displays either the customer or ticket that the user is looking for.
        private void btnSearch_Click(object sender, EventArgs e)
        {
            switch (myState)
            {
                case "Edit Customer":
                    txtCustID.DataBindings.Clear();
                    txtItemID.DataBindings.Clear();
                    txtSKUNumber.DataBindings.Clear();
                    txtProductName.DataBindings.Clear();
                    cboStatus.DataBindings.Clear();
                    RentalConnection();
                    rentalCommand = new SqlCommand(
                                "SELECT * " +
                                "FROM customersTable " +
                                "WHERE CustomerID " +
                                "LIKE '" + txtSearch.Text + "%'", rentalConnection);
                    rentalAdapter = new SqlDataAdapter();
                    rentalAdapter.SelectCommand = rentalCommand;
                    rentalTable = new DataTable();
                    rentalAdapter.Fill(rentalTable);
                    txtCustID.DataBindings.Add("Text", rentalTable, "FirstName");
                    txtItemID.DataBindings.Add("Text", rentalTable, "MiddleName");
                    txtSKUNumber.DataBindings.Add("Text", rentalTable, "LastName");
                    txtProductName.DataBindings.Add("Text", rentalTable, "Email");
                    txtAddress.DataBindings.Add("Text", rentalTable, "Address");
                    txtPhone.DataBindings.Add("Text", rentalTable, "Phone");

                    rentalManager = (CurrencyManager)
                        this.BindingContext[rentalTable];
                    rentalConnection.Close();
                    rentalAdapter.Dispose();
                    rentalTable.Dispose();
                    break;
                default:
                    txtCustID.DataBindings.Clear();
                    txtItemID.DataBindings.Clear();
                    txtSKUNumber.DataBindings.Clear();
                    txtProductName.DataBindings.Clear();
                    cboStatus.DataBindings.Clear();
                    SetState("Edit");
                    RentalConnection();
                    rentalCommand = new SqlCommand(
                                "SELECT * " +
                                "FROM rentedItems " +
                                "WHERE RentID " +
                                "LIKE '" + txtSearch.Text + "%'", rentalConnection);

                    rentalAdapter = new SqlDataAdapter();
                    rentalAdapter.SelectCommand = rentalCommand;
                    rentalTable = new DataTable();
                    rentalAdapter.Fill(rentalTable);
                    txtCustID.DataBindings.Add("Text", rentalTable, "CustomerID");
                    txtItemID.DataBindings.Add("Text", rentalTable, "ItemID");
                    txtSKUNumber.DataBindings.Add("Text", rentalTable, "SKUNumber");
                    txtProductName.DataBindings.Add("Text", rentalTable, "ProductName");
                    cboStatus.DataBindings.Add("Text", rentalTable, "Status");
                    rentalManager = (CurrencyManager)
                        this.BindingContext[rentalTable];
                    rentalConnection.Close();
                    rentalAdapter.Dispose();
                    rentalTable.Dispose();
                    break;
            }
        }

        private void btnEditCust_Click(object sender, EventArgs e)
        {
            SetState("Edit Customer");
        }
        /// <summary>
        /// Allows the user to access the Repair Section of the form
        /// </summary>
        /// Input: Repair button click event
        /// Output: Repair form loads
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
        /// <summary>
        /// Whenever the user wants to search for a ticket that's opened or closed, it calls the text change event so that the datagrid automatically updates.
        /// </summary>
        /// Input:  User clicks on a different item in the combobox
        /// Output: Datagrid is updated automatically 
        private void cboStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            ticketStatus = cboStatus.SelectedItem.ToString();
            textBox1_TextChanged_2(txtSearch, EventArgs.Empty);
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
                case "Connect":
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
                    btnAddNew.Enabled = true;
                    btnSave.Enabled = false;
                    btnCancel.Enabled = false;
                    btnEdit.Enabled = true;
                    btnDelete.Enabled = true;
                    btnPrint.Enabled = true;
                    btnInventory.Enabled = true;
                    btnRepairs.Enabled = true;
                    btnEditCust.Enabled = true;
                    grdRentals.Visible = true;
                    lblCustID.Visible = false;
                    lblItemID.Visible = false;
                    lblProductName.Visible = false;
                    lblSKUNumber.Visible = false;
                    lblEmail.Visible = false;
                    lblAddress.Visible = false;
                    lblPhone.Visible = false;
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
                    btnAddNew.Enabled = false;
                    btnSave.Enabled = true;
                    btnCancel.Enabled = true;
                    btnEdit.Enabled = false;
                    btnDelete.Enabled = false;
                    btnPrint.Enabled = false;
                    btnAddCust.Enabled = false;
                    btnSearch.Enabled = false;
                    grdRentals.Visible = false;
                    lblCustID.Text = "Customer ID:";
                    lblItemID.Text = "First Name:";
                    lblProductName.Text = "Last Name:";
                    lblSKUNumber.Text = "Middle Name:";
                    lblCustID.Visible = true;
                    lblItemID.Visible = true;
                    lblProductName.Visible = true;
                    lblSKUNumber.Visible = true;
                    lblEmail.Visible = true;
                    lblAddress.Visible = true;
                    lblPhone.Visible = true;
                    txtCustID.Visible = true;
                    txtItemID.Visible = true;
                    txtProductName.Visible = true;
                    txtSKUNumber.Visible = true;
                    txtEmail.Visible = true;
                    txtAddress.Visible = true;
                    txtPhone.Visible = true;
                    txtSearch.Enabled = false;  
                    break;
                case "Edit Customer":
                    btnAddNew.Enabled = false;
                    btnSave.Enabled = true;
                    btnCancel.Enabled = true;
                    btnEdit.Enabled = false;
                    btnDelete.Enabled = false;
                    btnPrint.Enabled = false;
                    btnAddCust.Enabled = false;
                    btnSearch.Enabled = true;
                    btnEditCust.Enabled = false;
                    cboStatus.Enabled = false;
                    grdRentals.Visible = false;
                    lblSearch.Text = "Search by Customer ID:";
                    lblCustID.Text = "Customer ID:";
                    lblItemID.Text = "First Name:";
                    lblProductName.Text = "Last Name:";
                    lblSKUNumber.Text = "Middle Name:";
                    lblCustID.Visible = true;
                    lblItemID.Visible = true;
                    lblProductName.Visible = true;
                    lblSKUNumber.Visible = true;
                    lblEmail.Visible = true;
                    lblAddress.Visible = true;
                    lblPhone.Visible = true;
                    txtCustID.Visible = false;
                    txtItemID.Visible = true;
                    txtProductName.Visible = true;
                    txtSKUNumber.Visible = true;
                    txtEmail.Visible = true;
                    txtAddress.Visible = true;
                    txtPhone.Visible = true;
                    txtSearch.Enabled = true;
                    break;
                case "Edit":
                    btnAddNew.Enabled = false;
                    btnSave.Enabled = true;
                    btnCancel.Enabled = true;
                    btnEdit.Enabled = false;
                    btnDelete.Enabled = false;
                    btnPrint.Enabled = true;
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
                    txtSearch.Enabled = true;
                    break;
                //Add or Edit State
                default:
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
