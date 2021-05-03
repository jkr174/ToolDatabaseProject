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
        bool selectedFile = false,
            addTicket = false,
            addCust = false;
        string myState;
        int myBookmark,
            pageNumber;
        const int itemsPerPage = 45;

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

        }

        private void btnRepairs_Load(object sender, EventArgs e)
        {
            SetState("Connect");
            try
            {
                RentalConnection();

                repairCommand = new SqlCommand(
                    "SELECT	* " +
                    "FROM repairTable ", repairConnection);

                repairAdapter = new SqlDataAdapter();
                repairAdapter.SelectCommand = repairCommand;

                repairCommandBuilder = new SqlCommandBuilder(repairAdapter);
                repairTable = new DataTable();
                repairAdapter.Fill(repairTable);
                grdRentals.ReadOnly = true;
                grdRentals.DataSource = repairTable;

                repairManager = (CurrencyManager)
                    this.BindingContext[repairTable];

                SetState("View");
                cboStatus.Items.Add("All");
                cboStatus.Items.Add("Open");
                cboStatus.Items.Add("Closed");
                cboStatus.SelectedItem = "All";

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
        public void RentalConnection()
        {
            repairConnection = new SqlConnection(
                    "Data Source = (LocalDB)\\MSSQLLocalDB;" +
                    "AttachDbFilename=|DataDirectory|\\ToolRentalsDB.mdf; " +
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
                    btnInventory.Enabled = true;
                    grdRentals.Visible = true;
                    lblItemID.Visible = false;
                    txtItemID.Visible = false;
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
                    grdRentals.Visible = false;
                    lblItemID.Text = "First Name:";
                    lblItemID.Visible = true;
                    txtItemID.Visible = true;
                    txtSearch.Enabled = false;
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
                    lblItemID.Visible = true;
                    lblItemID.Text = "Item ID:";
                    txtItemID.Visible = true;
                    txtSearch.Enabled = false;
                    break;
            }
        }
    }
}
