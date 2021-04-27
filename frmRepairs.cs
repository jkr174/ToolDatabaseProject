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
                    btnFirst.Enabled = true;
                    btnPrevious.Enabled = true;
                    btnNext.Enabled = true;
                    btnLast.Enabled = true;
                    btnAddNew.Enabled = true;
                    btnSave.Enabled = false;
                    btnCancel.Enabled = false;
                    btnEdit.Enabled = true;
                    btnDelete.Enabled = true;
                    btnPrint.Enabled = true;
                    txtSearch.Enabled = true;
                    break;
                //Add or Edit State
                default:
                    btnFirst.Enabled = false;
                    btnPrevious.Enabled = false;
                    btnNext.Enabled = false;
                    btnLast.Enabled = false;
                    btnAddNew.Enabled = false;
                    btnSave.Enabled = true;
                    btnCancel.Enabled = true;
                    btnEdit.Enabled = false;
                    btnDelete.Enabled = false;
                    btnPrint.Enabled = false;
                    break;
            }
        }
    }
}
