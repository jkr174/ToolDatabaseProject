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

        private void btnPrevious_Click(object sender, EventArgs e)
        {
            if (repairManager.Position == 0)
            {
                Console.Beep();
            }
            repairManager.Position--;
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (repairManager.Position == repairManager.Count - 1)
            {
                Console.Beep();
            }
            repairManager.Position++;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DialogResult response;
            response = MessageBox.Show(
                "Are you sure you want to delete this record?", 
                "Delete", 
                MessageBoxButtons.YesNo,
            MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
            if (response == DialogResult.No)
            {
                return;
            }
        }

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

        public void RentalConnection()
        {
            repairConnection = new SqlConnection(
                    "Data Source = (LocalDB)\\MSSQLLocalDB;" +
                    "AttachDbFilename=|DataDirectory|\\ToolRentalsDB.mdf;" +
                    "Integrated Security = True;" +
                    "Connect Timeout=30;");
            repairConnection.Open();
        }
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
                    btnDelete.Enabled = true;
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
                    btnDelete.Enabled = false;
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
                //Add or Edit State
                default:
                    cboStatus.Enabled = false;
                    btnAddNew.Enabled = false;
                    btnSave.Enabled = true;
                    btnCancel.Enabled = true;
                    btnEdit.Enabled = false;
                    btnDelete.Enabled = false;
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
