/*
 * Created by: Jovany Romo
 * Date Updated: 5/9/2021
 * 
 * Summary: User is able to view, add, and edit to the Rental Table in the database.
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
    public partial class frmRepairs : Form
    {
        bool selectedFile = false;
        string myState,
            ticketStatus;

        SqlConnection repairConnection;
        SqlCommand repairCommand;
        SqlCommandBuilder repairCommandBuilder;
        SqlDataAdapter repairAdapter;
        DataTable repairTable;
        CurrencyManager repairManager;
        public frmRepairs()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Dynamically able to search the database
        /// </summary>
        ///Input: user text input
        ///Output: Datagrid is updated as you type
        private void cboStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            ticketStatus = cboStatus.SelectedItem.ToString();
            txtSearch_TextChanged(txtSearch, EventArgs.Empty);
        }

        private void frmRepairs_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (selectedFile != false)
            {
                repairCommand.Dispose();
                repairCommandBuilder.Dispose();
                repairAdapter.Dispose();
                repairTable.Dispose();
            }
        }
        /// <summary>
        /// When the user clicks on the edit button, the form updates, allowing for user input and for the user to search for tickets.
        /// </summary>
        /// Input: When the user clicks on the edit button button
        /// Output: Form updates so the user has fields to type into.
        private void btnEdit_Click(object sender, EventArgs e)
        {
            txtItemID.DataBindings.Clear();
            txtEmployeeAssigned.DataBindings.Clear();
            cboStatus.DataBindings.Clear();
            SetState("Edit");
            RentalConnection();
            repairCommand = new SqlCommand(
                "SELECT * " +
                "FROM repairTable " +
                "ORDER BY dateAdded", repairConnection);

            repairAdapter = new SqlDataAdapter();
            repairAdapter.SelectCommand = repairCommand;
            repairTable = new DataTable();
            repairAdapter.Fill(repairTable);
            txtItemID.DataBindings.Add("Text", repairTable, "ItemID");
            txtEmployeeAssigned.DataBindings.Add("Text", repairTable, "EmployeeAssigned");
            repairManager = (CurrencyManager)
                this.BindingContext[repairTable];
            repairConnection.Close();
            repairAdapter.Dispose();
            repairTable.Dispose();
        }
        /// <summary>
        /// How the application handles saving information from the user in different states.
        /// </summary>
        /// Input: Click save button event
        /// Output: Confirmation Message Box and saved information to the database. 
        private void btnSave_Click(object sender, EventArgs e)
        {
            DateTime now = DateTime.Now;
            try
            {
                if (myState == "Edit")
                {
                    RentalConnection();
                    repairCommand = new SqlCommand(
                        "UPDATE repairTable " +
                        "SET " +
                            "[ItemID] = '" + txtItemID.Text.ToString() + "'" +
                            ",[Status] = '" + cboStatus.Text.ToString() + "'" +
                            ",[EmployeeAssigned] = '" + txtEmployeeAssigned.Text.ToString() + "' " +
                        "WHERE [RepairTicketID] = '" + txtSearch.Text.ToString() + "'", repairConnection);
                    repairCommand.ExecuteNonQuery();
                    MessageBox.Show("Ticket saved.",
                        "Save",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                    repairConnection.Close();
                }
                else if(myState == "Add")
                {
                    RentalConnection();
                    repairCommand = new SqlCommand( "INSERT INTO repairTable (ItemID, Status, EmployeeAssigned, dateAdded) " +
                                                    "VALUES (" +
                                                        "'" + txtItemID.Text +
                                                        ",'Open'" +
                                                        ",'" + txtEmployeeAssigned.Text + "'" +
                                                        ",'" + now + "')", repairConnection);
                    repairCommand.ExecuteNonQuery();
                    MessageBox.Show("Ticket saved.",
                        "Save",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                    repairConnection.Close();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,
                    "Error saving information.",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            btnRepairs_Load(sender, e);
            SetState("View");
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnAddNew_Click(object sender, EventArgs e)
        {
            SetState("Add");
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            repairManager.CancelCurrentEdit();
            SetState("View");
        }
        /// <summary>
        /// Dynamically able to search the database
        /// </summary>
        ///Input: user text input
        ///Output: Datagrid is updated as you type
        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
                RentalConnection();
                if (myState == "View")
                {
                    if (cboStatus.Text == "All")
                    {
                        repairAdapter = new SqlDataAdapter(
                        "SELECT * " +
                        "FROM repairTable " +
                        "WHERE RepairTicketID " +
                        "LIKE '" + txtSearch.Text + "%'", repairConnection);
                    }
                    else if (cboStatus.Text != null)
                    {
                        repairAdapter = new SqlDataAdapter(
                        "SELECT	* " +
                        "FROM repairTable " +
                        "WHERE RepairTicketID " +
                        "LIKE '" + txtSearch.Text + "%'" +
                        "AND Status " +
                        "LIKE '" + ticketStatus + "%' ", repairConnection);
                    }
                    else
                    {
                        repairAdapter = new SqlDataAdapter(
                        "SELECT	* " +
                        "FROM repairTable " +
                        "WHERE RepairTicketID " +
                        "LIKE '" + txtSearch.Text + "%'", repairConnection);
                    }
                    repairTable = new DataTable();
                    repairAdapter.Fill(repairTable);
                    grdRepairs.DataSource = repairTable;
                    repairConnection.Close();
                    repairAdapter.Dispose();
                    repairTable.Dispose();
                }
        }
        /// <summary>
        /// Load Event that automatically attempts to connect to the database. If it fails, then the program displays an error exception message to the user.
        /// </summary>
        /// Input: Loading
        /// Output: Display
        private void btnRepairs_Load(object sender, EventArgs e)
        {
            myState = "Connecting";
            cboStatus.Items.Add("All");
            cboStatus.Items.Add("Open");
            cboStatus.Items.Add("Closed");
            cboStatus.SelectedItem = "All";
            try
            {
                RentalConnection();

                repairCommand = new SqlCommand("SELECT * FROM repairTable", repairConnection);

                repairAdapter = new SqlDataAdapter();
                repairAdapter.SelectCommand = repairCommand;

                repairCommandBuilder = new SqlCommandBuilder(repairAdapter);
                repairTable = new DataTable();
                repairAdapter.Fill(repairTable);
                grdRepairs.ReadOnly = true;
                grdRepairs.DataSource = repairTable;

                repairManager = (CurrencyManager)
                    this.BindingContext[repairTable];

                selectedFile = true;
                txtEmployeeAssigned.DataBindings.Clear();
            txtItemID.DataBindings.Clear();
            cboStatus.DataBindings.Clear();
                repairConnection.Close();
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
        /// Allows the user to search situational events during different parts of the form, allowing to search for tickets and information that they need.
        /// </summary>
        /// Input: What the user types in the search bar, and when they click on the Search Button.
        /// Output: Displays either the customer or ticket that the user is looking for.
        private void btnSearch_Click(object sender, EventArgs e)
        {
            txtItemID.Enabled = true;
            txtEmployeeAssigned.Enabled = true;
            cboStatus.Enabled = true;
            repairCommand = new SqlCommand(
                        "SELECT * " +
                        "FROM repairTable " +
                        "WHERE RepairTicketID " +
                        "LIKE '" + txtSearch.Text + "%'", repairConnection);
            repairAdapter = new SqlDataAdapter();
            repairAdapter.SelectCommand = repairCommand;
            repairTable = new DataTable();
            repairAdapter.Fill(repairTable);
            //bind txt boxes
            txtEmployeeAssigned.DataBindings.Clear();
            txtItemID.DataBindings.Clear();
            cboStatus.DataBindings.Clear();
            txtItemID.DataBindings.Add("Text", repairTable, "ItemID");
            txtEmployeeAssigned.DataBindings.Add("Text", repairTable, "EmployeeAssigned");
            cboStatus.DataBindings.Add("Text", repairTable, "Status");
            repairManager = (CurrencyManager)
                this.BindingContext[repairTable];
        }
        /// <summary>
        /// Connection string that uses localDB to connect to the database file and automatically opens the connection.
        /// </summary>
        public void RentalConnection()
        {
            repairConnection = new SqlConnection(
                    "Data Source = (LocalDB)\\MSSQLLocalDB;" +
                    "AttachDbFilename=|DataDirectory|\\ToolRentalsDB.mdf;" +
                    "Integrated Security = True;" +
                    "Connect Timeout=30;");
            repairConnection.Open();
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
                    btnAddNew.Enabled = true;
                    btnSave.Enabled = false;
                    btnCancel.Enabled = false;
                    btnEdit.Enabled = true;
                    btnPrint.Enabled = true;
                    btnSearch.Visible = false;
                    grdRepairs.Visible = true;
                    lblItemID.Visible = false;
                    lblEmpAssigned.Visible = false;
                    txtItemID.Visible = false;
                    txtEmployeeAssigned.Visible = false;
                    txtSearch.Enabled = true;
                    break;
                case "Edit":
                    btnAddNew.Enabled = false;
                    btnSave.Enabled = true;
                    btnCancel.Enabled = true;
                    btnEdit.Enabled = false;
                    btnPrint.Enabled = false;
                    txtEmployeeAssigned.Enabled = false;
                    txtItemID.Enabled = false;
                    cboStatus.Enabled = false;
                    cboStatus.Items.Remove("All");
                    grdRepairs.Visible = false;
                    btnSearch.Visible = true;
                    btnSearch.Enabled = true;
                    lblItemID.Visible = true;
                    lblEmpAssigned.Visible = true;
                    txtItemID.Visible = true;
                    txtEmployeeAssigned.Visible = true;
                    txtSearch.Enabled = true;
                    txtItemID.Text = "";
                    txtEmployeeAssigned.Text = "";
                    break;
                //Add State
                default:
                    cboStatus.Enabled = false;
                    btnAddNew.Enabled = false;
                    btnSave.Enabled = true;
                    btnCancel.Enabled = true;
                    btnEdit.Enabled = false;
                    btnPrint.Enabled = false;
                    grdRepairs.Visible = false;
                    lblItemID.Visible = true;
                    lblEmpAssigned.Visible = true;
                    txtItemID.Visible = true;
                    txtEmployeeAssigned.Visible = true;
                    txtSearch.Enabled = false;
                    cboStatus.Enabled = false;
                    break;
            }
        }
    }
}
