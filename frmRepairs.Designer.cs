
namespace KWSalesOrderFormProject
{
    partial class frmRepairs
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
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.lblSearch = new System.Windows.Forms.Label();
            this.txtEmployeeAssigned = new System.Windows.Forms.TextBox();
            this.txtItemID = new System.Windows.Forms.TextBox();
            this.lblEmpAssigned = new System.Windows.Forms.Label();
            this.lblItemID = new System.Windows.Forms.Label();
            this.picTools = new System.Windows.Forms.PictureBox();
            this.button1 = new System.Windows.Forms.Button();
            this.btnInventory = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnAddNew = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.lblTitle = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.grdRentals = new System.Windows.Forms.DataGridView();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnPrint = new System.Windows.Forms.Button();
            this.cboStatus = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.picTools)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdRentals)).BeginInit();
            this.SuspendLayout();
            // 
            // txtSearch
            // 
            this.txtSearch.Enabled = false;
            this.txtSearch.Location = new System.Drawing.Point(157, 83);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(190, 20);
            this.txtSearch.TabIndex = 105;
            // 
            // lblSearch
            // 
            this.lblSearch.AutoSize = true;
            this.lblSearch.Location = new System.Drawing.Point(12, 86);
            this.lblSearch.Name = "lblSearch";
            this.lblSearch.Size = new System.Drawing.Size(139, 13);
            this.lblSearch.TabIndex = 104;
            this.lblSearch.Text = "Search by Repair Ticket ID:";
            // 
            // txtEmployeeAssigned
            // 
            this.txtEmployeeAssigned.Location = new System.Drawing.Point(120, 143);
            this.txtEmployeeAssigned.Name = "txtEmployeeAssigned";
            this.txtEmployeeAssigned.Size = new System.Drawing.Size(389, 20);
            this.txtEmployeeAssigned.TabIndex = 92;
            this.txtEmployeeAssigned.Visible = false;
            // 
            // txtItemID
            // 
            this.txtItemID.Location = new System.Drawing.Point(120, 117);
            this.txtItemID.Name = "txtItemID";
            this.txtItemID.Size = new System.Drawing.Size(389, 20);
            this.txtItemID.TabIndex = 91;
            this.txtItemID.Visible = false;
            // 
            // lblEmpAssigned
            // 
            this.lblEmpAssigned.AutoSize = true;
            this.lblEmpAssigned.Location = new System.Drawing.Point(12, 146);
            this.lblEmpAssigned.Name = "lblEmpAssigned";
            this.lblEmpAssigned.Size = new System.Drawing.Size(102, 13);
            this.lblEmpAssigned.TabIndex = 88;
            this.lblEmpAssigned.Text = "Employee Assigned:";
            this.lblEmpAssigned.Visible = false;
            // 
            // lblItemID
            // 
            this.lblItemID.AutoSize = true;
            this.lblItemID.Location = new System.Drawing.Point(12, 120);
            this.lblItemID.Name = "lblItemID";
            this.lblItemID.Size = new System.Drawing.Size(44, 13);
            this.lblItemID.TabIndex = 87;
            this.lblItemID.Text = "Item ID:";
            this.lblItemID.Visible = false;
            // 
            // picTools
            // 
            this.picTools.Image = global::KWSalesOrderFormProject.Properties.Resources.toolsIcon;
            this.picTools.InitialImage = null;
            this.picTools.Location = new System.Drawing.Point(320, 524);
            this.picTools.Name = "picTools";
            this.picTools.Size = new System.Drawing.Size(27, 23);
            this.picTools.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picTools.TabIndex = 86;
            this.picTools.TabStop = false;
            this.picTools.Visible = false;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(93, 524);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 85;
            this.button1.TabStop = false;
            this.button1.Text = "Repairs";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // btnInventory
            // 
            this.btnInventory.Location = new System.Drawing.Point(221, 524);
            this.btnInventory.Name = "btnInventory";
            this.btnInventory.Size = new System.Drawing.Size(75, 23);
            this.btnInventory.TabIndex = 84;
            this.btnInventory.TabStop = false;
            this.btnInventory.Text = "Inventory";
            this.btnInventory.UseVisualStyleBackColor = true;
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(221, 495);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(75, 23);
            this.btnDelete.TabIndex = 81;
            this.btnDelete.TabStop = false;
            this.btnDelete.Text = "&Delete";
            this.btnDelete.UseVisualStyleBackColor = true;
            // 
            // btnAddNew
            // 
            this.btnAddNew.Location = new System.Drawing.Point(93, 408);
            this.btnAddNew.Name = "btnAddNew";
            this.btnAddNew.Size = new System.Drawing.Size(156, 23);
            this.btnAddNew.TabIndex = 80;
            this.btnAddNew.TabStop = false;
            this.btnAddNew.Text = "&Add New Ticket";
            this.btnAddNew.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            this.btnCancel.Enabled = false;
            this.btnCancel.Location = new System.Drawing.Point(353, 466);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 79;
            this.btnCancel.TabStop = false;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnSave
            // 
            this.btnSave.Enabled = false;
            this.btnSave.Location = new System.Drawing.Point(221, 466);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 78;
            this.btnSave.TabStop = false;
            this.btnSave.Text = "&Save";
            this.btnSave.UseVisualStyleBackColor = true;
            // 
            // btnEdit
            // 
            this.btnEdit.Location = new System.Drawing.Point(93, 466);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(75, 23);
            this.btnEdit.TabIndex = 77;
            this.btnEdit.TabStop = false;
            this.btnEdit.Text = "&Edit";
            this.btnEdit.UseVisualStyleBackColor = true;
            // 
            // lblTitle
            // 
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.Location = new System.Drawing.Point(118, 12);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(275, 65);
            this.lblTitle.TabIndex = 74;
            this.lblTitle.Text = "Repair Tickets";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::KWSalesOrderFormProject.Properties.Resources.toolsIcon;
            this.pictureBox1.Location = new System.Drawing.Point(12, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(100, 65);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 73;
            this.pictureBox1.TabStop = false;
            // 
            // grdRentals
            // 
            this.grdRentals.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdRentals.Location = new System.Drawing.Point(12, 111);
            this.grdRentals.Name = "grdRentals";
            this.grdRentals.Size = new System.Drawing.Size(497, 239);
            this.grdRentals.TabIndex = 72;
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(353, 495);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(75, 23);
            this.btnExit.TabIndex = 71;
            this.btnExit.Text = "E&xit";
            this.btnExit.UseVisualStyleBackColor = true;
            // 
            // btnPrint
            // 
            this.btnPrint.Location = new System.Drawing.Point(353, 524);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(75, 23);
            this.btnPrint.TabIndex = 70;
            this.btnPrint.Text = "&Print";
            this.btnPrint.UseVisualStyleBackColor = true;
            // 
            // cboStatus
            // 
            this.cboStatus.FormattingEnabled = true;
            this.cboStatus.Location = new System.Drawing.Point(388, 83);
            this.cboStatus.Name = "cboStatus";
            this.cboStatus.Size = new System.Drawing.Size(121, 21);
            this.cboStatus.TabIndex = 106;
            this.cboStatus.SelectedIndexChanged += new System.EventHandler(this.cboStatus_SelectedIndexChanged);
            // 
            // frmRepairs
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(521, 559);
            this.Controls.Add(this.cboStatus);
            this.Controls.Add(this.txtSearch);
            this.Controls.Add(this.lblSearch);
            this.Controls.Add(this.txtEmployeeAssigned);
            this.Controls.Add(this.txtItemID);
            this.Controls.Add(this.lblEmpAssigned);
            this.Controls.Add(this.lblItemID);
            this.Controls.Add(this.picTools);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnInventory);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnAddNew);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnEdit);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.grdRentals);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnPrint);
            this.Name = "frmRepairs";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.btnRepairs_Load);
            ((System.ComponentModel.ISupportInitialize)(this.picTools)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdRentals)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Label lblSearch;
        private System.Windows.Forms.TextBox txtEmployeeAssigned;
        private System.Windows.Forms.TextBox txtItemID;
        private System.Windows.Forms.Label lblEmpAssigned;
        private System.Windows.Forms.Label lblItemID;
        private System.Windows.Forms.PictureBox picTools;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btnInventory;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnAddNew;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.DataGridView grdRentals;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.ComboBox cboStatus;
    }
}