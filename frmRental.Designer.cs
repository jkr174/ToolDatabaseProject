
namespace KWSalesOrderFormProject
{
    partial class frmRented
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmRented));
            this.dlgOpen = new System.Windows.Forms.OpenFileDialog();
            this.btnPrint = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.rentedItemsBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.grdRentals = new System.Windows.Forms.DataGridView();
            this.lblTitle = new System.Windows.Forms.Label();
            this.btnAddNew = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnRepairs = new System.Windows.Forms.Button();
            this.btnInventory = new System.Windows.Forms.Button();
            this.printPreview = new System.Windows.Forms.PrintPreviewDialog();
            this.lblCustID = new System.Windows.Forms.Label();
            this.lblItemID = new System.Windows.Forms.Label();
            this.lblSKUNumber = new System.Windows.Forms.Label();
            this.lblProductName = new System.Windows.Forms.Label();
            this.txtCustID = new System.Windows.Forms.TextBox();
            this.txtItemID = new System.Windows.Forms.TextBox();
            this.txtSKUNumber = new System.Windows.Forms.TextBox();
            this.txtProductName = new System.Windows.Forms.TextBox();
            this.btnAddCust = new System.Windows.Forms.Button();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.lblEmail = new System.Windows.Forms.Label();
            this.txtAddress = new System.Windows.Forms.TextBox();
            this.lblAddress = new System.Windows.Forms.Label();
            this.txtPhone = new System.Windows.Forms.TextBox();
            this.lblPhone = new System.Windows.Forms.Label();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.lblSearch = new System.Windows.Forms.Label();
            this.picTools = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.cboStatus = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnSearch = new System.Windows.Forms.Button();
            this.btnEditCust = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.rentedItemsBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdRentals)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picTools)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // dlgOpen
            // 
            this.dlgOpen.Filter = "SQL Server Databases (*.mdf)|*.mdf";
            // 
            // btnPrint
            // 
            this.btnPrint.Location = new System.Drawing.Point(353, 466);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(75, 23);
            this.btnPrint.TabIndex = 2;
            this.btnPrint.Text = "&Print";
            this.btnPrint.UseVisualStyleBackColor = true;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(353, 524);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(75, 23);
            this.btnExit.TabIndex = 3;
            this.btnExit.Text = "E&xit";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // grdRentals
            // 
            this.grdRentals.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.grdRentals.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdRentals.Location = new System.Drawing.Point(12, 111);
            this.grdRentals.Name = "grdRentals";
            this.grdRentals.Size = new System.Drawing.Size(641, 239);
            this.grdRentals.TabIndex = 4;
            // 
            // lblTitle
            // 
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.Location = new System.Drawing.Point(118, 12);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(275, 65);
            this.lblTitle.TabIndex = 37;
            this.lblTitle.Text = "Rental Tickets";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // btnAddNew
            // 
            this.btnAddNew.Location = new System.Drawing.Point(93, 495);
            this.btnAddNew.Name = "btnAddNew";
            this.btnAddNew.Size = new System.Drawing.Size(75, 23);
            this.btnAddNew.TabIndex = 43;
            this.btnAddNew.TabStop = false;
            this.btnAddNew.Text = "&Add";
            this.btnAddNew.UseVisualStyleBackColor = true;
            this.btnAddNew.Click += new System.EventHandler(this.btnAddNew_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Enabled = false;
            this.btnCancel.Location = new System.Drawing.Point(353, 495);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 42;
            this.btnCancel.TabStop = false;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSave
            // 
            this.btnSave.Enabled = false;
            this.btnSave.Location = new System.Drawing.Point(221, 466);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 41;
            this.btnSave.TabStop = false;
            this.btnSave.Text = "&Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnEdit
            // 
            this.btnEdit.Location = new System.Drawing.Point(93, 466);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(75, 23);
            this.btnEdit.TabIndex = 40;
            this.btnEdit.TabStop = false;
            this.btnEdit.Text = "&Edit";
            this.btnEdit.UseVisualStyleBackColor = true;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // btnRepairs
            // 
            this.btnRepairs.Location = new System.Drawing.Point(93, 524);
            this.btnRepairs.Name = "btnRepairs";
            this.btnRepairs.Size = new System.Drawing.Size(75, 23);
            this.btnRepairs.TabIndex = 49;
            this.btnRepairs.TabStop = false;
            this.btnRepairs.Text = "Repairs";
            this.btnRepairs.UseVisualStyleBackColor = true;
            this.btnRepairs.Click += new System.EventHandler(this.btnRepairs_Click);
            // 
            // btnInventory
            // 
            this.btnInventory.Location = new System.Drawing.Point(221, 524);
            this.btnInventory.Name = "btnInventory";
            this.btnInventory.Size = new System.Drawing.Size(75, 23);
            this.btnInventory.TabIndex = 48;
            this.btnInventory.TabStop = false;
            this.btnInventory.Text = "Inventory";
            this.btnInventory.UseVisualStyleBackColor = true;
            this.btnInventory.Click += new System.EventHandler(this.btnInventory_Click);
            // 
            // printPreview
            // 
            this.printPreview.AutoScrollMargin = new System.Drawing.Size(0, 0);
            this.printPreview.AutoScrollMinSize = new System.Drawing.Size(0, 0);
            this.printPreview.ClientSize = new System.Drawing.Size(400, 300);
            this.printPreview.Enabled = true;
            this.printPreview.Icon = ((System.Drawing.Icon)(resources.GetObject("printPreview.Icon")));
            this.printPreview.Name = "printPreview";
            this.printPreview.Visible = false;
            // 
            // lblCustID
            // 
            this.lblCustID.AutoSize = true;
            this.lblCustID.Location = new System.Drawing.Point(12, 120);
            this.lblCustID.Name = "lblCustID";
            this.lblCustID.Size = new System.Drawing.Size(68, 13);
            this.lblCustID.TabIndex = 51;
            this.lblCustID.Text = "Customer ID:";
            this.lblCustID.Visible = false;
            // 
            // lblItemID
            // 
            this.lblItemID.AutoSize = true;
            this.lblItemID.Location = new System.Drawing.Point(12, 146);
            this.lblItemID.Name = "lblItemID";
            this.lblItemID.Size = new System.Drawing.Size(41, 13);
            this.lblItemID.TabIndex = 52;
            this.lblItemID.Text = "ItemID:";
            this.lblItemID.Visible = false;
            // 
            // lblSKUNumber
            // 
            this.lblSKUNumber.AutoSize = true;
            this.lblSKUNumber.Location = new System.Drawing.Point(12, 172);
            this.lblSKUNumber.Name = "lblSKUNumber";
            this.lblSKUNumber.Size = new System.Drawing.Size(72, 13);
            this.lblSKUNumber.TabIndex = 53;
            this.lblSKUNumber.Text = "SKU Number:";
            this.lblSKUNumber.Visible = false;
            // 
            // lblProductName
            // 
            this.lblProductName.AutoSize = true;
            this.lblProductName.Location = new System.Drawing.Point(12, 198);
            this.lblProductName.Name = "lblProductName";
            this.lblProductName.Size = new System.Drawing.Size(78, 13);
            this.lblProductName.TabIndex = 54;
            this.lblProductName.Text = "Product Name:";
            this.lblProductName.Visible = false;
            // 
            // txtCustID
            // 
            this.txtCustID.Location = new System.Drawing.Point(96, 117);
            this.txtCustID.Name = "txtCustID";
            this.txtCustID.Size = new System.Drawing.Size(413, 20);
            this.txtCustID.TabIndex = 55;
            this.txtCustID.Visible = false;
            // 
            // txtItemID
            // 
            this.txtItemID.Location = new System.Drawing.Point(96, 143);
            this.txtItemID.Name = "txtItemID";
            this.txtItemID.Size = new System.Drawing.Size(413, 20);
            this.txtItemID.TabIndex = 56;
            this.txtItemID.Visible = false;
            // 
            // txtSKUNumber
            // 
            this.txtSKUNumber.Location = new System.Drawing.Point(96, 169);
            this.txtSKUNumber.Name = "txtSKUNumber";
            this.txtSKUNumber.Size = new System.Drawing.Size(413, 20);
            this.txtSKUNumber.TabIndex = 57;
            this.txtSKUNumber.Visible = false;
            // 
            // txtProductName
            // 
            this.txtProductName.Location = new System.Drawing.Point(96, 195);
            this.txtProductName.Name = "txtProductName";
            this.txtProductName.Size = new System.Drawing.Size(413, 20);
            this.txtProductName.TabIndex = 58;
            this.txtProductName.Visible = false;
            // 
            // btnAddCust
            // 
            this.btnAddCust.Enabled = false;
            this.btnAddCust.Location = new System.Drawing.Point(532, 524);
            this.btnAddCust.Name = "btnAddCust";
            this.btnAddCust.Size = new System.Drawing.Size(121, 23);
            this.btnAddCust.TabIndex = 59;
            this.btnAddCust.TabStop = false;
            this.btnAddCust.Text = "Add New Customer";
            this.btnAddCust.UseVisualStyleBackColor = true;
            this.btnAddCust.Click += new System.EventHandler(this.btnAddCust_Click);
            // 
            // txtEmail
            // 
            this.txtEmail.Location = new System.Drawing.Point(96, 221);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(413, 20);
            this.txtEmail.TabIndex = 61;
            this.txtEmail.Text = "example@email.com";
            this.txtEmail.Visible = false;
            // 
            // lblEmail
            // 
            this.lblEmail.AutoSize = true;
            this.lblEmail.Location = new System.Drawing.Point(12, 224);
            this.lblEmail.Name = "lblEmail";
            this.lblEmail.Size = new System.Drawing.Size(35, 13);
            this.lblEmail.TabIndex = 60;
            this.lblEmail.Text = "Email:";
            this.lblEmail.Visible = false;
            // 
            // txtAddress
            // 
            this.txtAddress.Location = new System.Drawing.Point(96, 247);
            this.txtAddress.Name = "txtAddress";
            this.txtAddress.Size = new System.Drawing.Size(413, 20);
            this.txtAddress.TabIndex = 63;
            this.txtAddress.Visible = false;
            // 
            // lblAddress
            // 
            this.lblAddress.AutoSize = true;
            this.lblAddress.Location = new System.Drawing.Point(12, 250);
            this.lblAddress.Name = "lblAddress";
            this.lblAddress.Size = new System.Drawing.Size(48, 13);
            this.lblAddress.TabIndex = 62;
            this.lblAddress.Text = "Address:";
            this.lblAddress.Visible = false;
            // 
            // txtPhone
            // 
            this.txtPhone.Location = new System.Drawing.Point(96, 273);
            this.txtPhone.Name = "txtPhone";
            this.txtPhone.Size = new System.Drawing.Size(413, 20);
            this.txtPhone.TabIndex = 65;
            this.txtPhone.Text = "___-___-____";
            this.txtPhone.Visible = false;
            // 
            // lblPhone
            // 
            this.lblPhone.AutoSize = true;
            this.lblPhone.Location = new System.Drawing.Point(12, 276);
            this.lblPhone.Name = "lblPhone";
            this.lblPhone.Size = new System.Drawing.Size(41, 13);
            this.lblPhone.TabIndex = 64;
            this.lblPhone.Text = "Phone:";
            this.lblPhone.Visible = false;
            // 
            // txtSearch
            // 
            this.txtSearch.Enabled = false;
            this.txtSearch.Location = new System.Drawing.Point(157, 83);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(150, 20);
            this.txtSearch.TabIndex = 69;
            this.txtSearch.TextChanged += new System.EventHandler(this.textBox1_TextChanged_2);
            // 
            // lblSearch
            // 
            this.lblSearch.AutoSize = true;
            this.lblSearch.Location = new System.Drawing.Point(12, 86);
            this.lblSearch.Name = "lblSearch";
            this.lblSearch.Size = new System.Drawing.Size(139, 13);
            this.lblSearch.TabIndex = 68;
            this.lblSearch.Text = "Search by Rental Ticket ID:";
            // 
            // picTools
            // 
            this.picTools.Image = global::ToolRentalFormProject.Properties.Resources.toolsIcon;
            this.picTools.InitialImage = null;
            this.picTools.Location = new System.Drawing.Point(320, 524);
            this.picTools.Name = "picTools";
            this.picTools.Size = new System.Drawing.Size(27, 23);
            this.picTools.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picTools.TabIndex = 50;
            this.picTools.TabStop = false;
            this.picTools.Visible = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::ToolRentalFormProject.Properties.Resources.toolsIcon;
            this.pictureBox1.Location = new System.Drawing.Point(12, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(100, 65);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 36;
            this.pictureBox1.TabStop = false;
            // 
            // cboStatus
            // 
            this.cboStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboStatus.FormattingEnabled = true;
            this.cboStatus.Location = new System.Drawing.Point(532, 83);
            this.cboStatus.Name = "cboStatus";
            this.cboStatus.Size = new System.Drawing.Size(121, 21);
            this.cboStatus.TabIndex = 70;
            this.cboStatus.SelectedIndexChanged += new System.EventHandler(this.cboStatus_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(486, 86);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 13);
            this.label1.TabIndex = 71;
            this.label1.Text = "Status:";
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(313, 81);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(75, 23);
            this.btnSearch.TabIndex = 72;
            this.btnSearch.TabStop = false;
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // btnEditCust
            // 
            this.btnEditCust.Enabled = false;
            this.btnEditCust.Location = new System.Drawing.Point(532, 495);
            this.btnEditCust.Name = "btnEditCust";
            this.btnEditCust.Size = new System.Drawing.Size(121, 23);
            this.btnEditCust.TabIndex = 73;
            this.btnEditCust.TabStop = false;
            this.btnEditCust.Text = "Edit Customer";
            this.btnEditCust.UseVisualStyleBackColor = true;
            this.btnEditCust.Click += new System.EventHandler(this.btnEditCust_Click);
            // 
            // frmRented
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(665, 559);
            this.ControlBox = false;
            this.Controls.Add(this.btnEditCust);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cboStatus);
            this.Controls.Add(this.txtSearch);
            this.Controls.Add(this.lblSearch);
            this.Controls.Add(this.txtPhone);
            this.Controls.Add(this.lblPhone);
            this.Controls.Add(this.txtAddress);
            this.Controls.Add(this.lblAddress);
            this.Controls.Add(this.txtEmail);
            this.Controls.Add(this.lblEmail);
            this.Controls.Add(this.btnAddCust);
            this.Controls.Add(this.txtProductName);
            this.Controls.Add(this.txtSKUNumber);
            this.Controls.Add(this.txtItemID);
            this.Controls.Add(this.txtCustID);
            this.Controls.Add(this.lblProductName);
            this.Controls.Add(this.lblSKUNumber);
            this.Controls.Add(this.lblItemID);
            this.Controls.Add(this.lblCustID);
            this.Controls.Add(this.picTools);
            this.Controls.Add(this.btnRepairs);
            this.Controls.Add(this.btnInventory);
            this.Controls.Add(this.btnAddNew);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnEdit);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.grdRentals);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnPrint);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "frmRented";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Rent Form";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmKWSales_FormClosing);
            this.Load += new System.EventHandler(this.frmKWSales_Load);
            ((System.ComponentModel.ISupportInitialize)(this.rentedItemsBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdRentals)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picTools)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.OpenFileDialog dlgOpen;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.BindingSource rentedItemsBindingSource;
        private System.Windows.Forms.DataGridView grdRentals;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button btnAddNew;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Button btnRepairs;
        private System.Windows.Forms.Button btnInventory;
        private System.Windows.Forms.PrintPreviewDialog printPreview;
        private System.Windows.Forms.PictureBox picTools;
        private System.Windows.Forms.Label lblCustID;
        private System.Windows.Forms.Label lblItemID;
        private System.Windows.Forms.Label lblSKUNumber;
        private System.Windows.Forms.Label lblProductName;
        private System.Windows.Forms.TextBox txtCustID;
        private System.Windows.Forms.TextBox txtItemID;
        private System.Windows.Forms.TextBox txtSKUNumber;
        private System.Windows.Forms.TextBox txtProductName;
        private System.Windows.Forms.Button btnAddCust;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.Label lblEmail;
        private System.Windows.Forms.TextBox txtAddress;
        private System.Windows.Forms.Label lblAddress;
        private System.Windows.Forms.TextBox txtPhone;
        private System.Windows.Forms.Label lblPhone;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Label lblSearch;
        private System.Windows.Forms.ComboBox cboStatus;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Button btnEditCust;
    }
}

