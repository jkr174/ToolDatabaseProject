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
        bool selectedFile = true;

        SqlConnection toolRentalConnection;
        SqlCommand rentalCommand;
        SqlDataAdapter rentalAdapter;
        DataTable rentalTable;
        public frmRented()
        {
            InitializeComponent();
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            if (dlgOpen.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    // connect to sales database
                    toolRentalConnection = new SqlConnection("Data Source =.\\SQLEXPRESS;" +
                        "AttachDbFilename=" + dlgOpen.FileName + "; " +
                        "Integrated Security=True;" +
                        "Connect Timeout=30;" +
                        "User Instance=True");
                    toolRentalConnection.Open();
                    
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
            else
            {
                selectedFile = false;
                MessageBox.Show(
                    "No file selected. Program will now exit.",
                    "Program stopping",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                this.Close();
            }
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
                toolRentalConnection.Close();
                rentalCommand.Dispose();
                rentalAdapter.Dispose();
                rentalTable.Dispose();
            }
        }
    }
}
