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
        const int itemsPerPage = 45;

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

        private void btnConnect_Click(object sender, EventArgs e)
        {
            /*try
            {

                inventoryConnection = new SqlConnection(
                    "Data Source=(LocalDB)\\MSSQLLocalDB;" +
                    "AttachDbFilename=|DataDirectory|\\ToolRentalsDB.mdf;" +
                    "Integrated Security=True;" +
                    "Connect Timeout=30");
                inventoryConnection.Open();

                inventoryCommand = new SqlCommand(
                    "SELECT	*" +
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

                //SetState("View");

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
            }*/
        }

        private void frmInventory_Load(object sender, EventArgs e)
        {
            try
            {
                InventoryConnection();

                inventoryCommand = new SqlCommand(
                    "SELECT	*" +
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

        private void btnFirst_Click(object sender, EventArgs e)
        {

        }

        private void btnPrevious_Click(object sender, EventArgs e)
        {

        }

        private void btnNext_Click(object sender, EventArgs e)
        {

        }

        private void btnLast_Click(object sender, EventArgs e)
        {

        }

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
            this.Close();
        }

        private void btnAddNew_Click(object sender, EventArgs e)
        {

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
                    break;
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

        private void btnCancel_Click(object sender, EventArgs e)
        {
            inventoryManager.CancelCurrentEdit();
            SetState("View");
        }
        private DataGridViewCell GetCellWhereTextExistsInGridView(string searchText, DataGridView dataGridView, int columnIndex)
        {
            DataGridViewCell cellWhereTextIsMet = null;

            // For every row in the grid
            foreach (DataGridViewRow row in dataGridView.Rows)
            {
                // I did not test this case, but cell.Value is an object, and objects can be null
                // So check if the cell is null before using .ToString()
                if (row.Cells[columnIndex].Value != null && searchText == row.Cells[columnIndex].Value.ToString())
                {
                    // the searchText is equals to the text in this cell.
                    cellWhereTextIsMet = row.Cells[columnIndex];
                    break;
                }
            }

            return cellWhereTextIsMet;
        }
        public void InventoryConnection()
        {
            inventoryConnection = new SqlConnection(
                    "Data Source=(LocalDB)\\MSSQLLocalDB;" +
                    "AttachDbFilename=|DataDirectory|\\ToolRentalsDB.mdf;" +
                    "Integrated Security=True;" +
                    "Connect Timeout=30");
            inventoryConnection.Open();
        }

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

        private void cboStatus_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
