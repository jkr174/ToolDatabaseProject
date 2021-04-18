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
        int myBookmark,
            pageNumber;
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
            try
            {

                inventoryConnection = new SqlConnection(
                    "Data Source=(LocalDB)\\MSSQLLocalDB;" +
                    "AttachDbFilename=|DataDirectory|\\ToolRentalsDB.mdf;" +
                    "Integrated Security=True;" +
                    "Connect Timeout=30");
                inventoryConnection.Open();

                inventoryCommand = new SqlCommand(
                    "SELECT	*" +
                    "FROM inventoryTable ", inventoryConnection);

                inventoryAdapter = new SqlDataAdapter();
                inventoryAdapter.SelectCommand = inventoryCommand;

                inventoryCommandBuilder = new SqlCommandBuilder(inventoryAdapter);
                inventoryTable = new DataTable();
                inventoryAdapter.Fill(inventoryTable);
                grdRentals.ReadOnly = true;
                grdRentals.DataSource = inventoryTable;

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
            }
        }
    }
}
