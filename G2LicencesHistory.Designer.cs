namespace StelthXmlFilling
{
    partial class G2LicencesHistory
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
            this.licencehistoryDatagrid = new System.Windows.Forms.DataGridView();
            this.purchaseDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.licenses = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.expirationDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.supportExpirationDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.guaranteeExpirationDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.licencehistoryDatagrid)).BeginInit();
            this.SuspendLayout();
            // 
            // licencehistoryDatagrid
            // 
            this.licencehistoryDatagrid.AllowUserToAddRows = false;
            this.licencehistoryDatagrid.AllowUserToDeleteRows = false;
            this.licencehistoryDatagrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.licencehistoryDatagrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.licencehistoryDatagrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.purchaseDate,
            this.licenses,
            this.expirationDate,
            this.supportExpirationDate,
            this.guaranteeExpirationDate});
            this.licencehistoryDatagrid.Location = new System.Drawing.Point(12, 12);
            this.licencehistoryDatagrid.Name = "licencehistoryDatagrid";
            this.licencehistoryDatagrid.Size = new System.Drawing.Size(543, 110);
            this.licencehistoryDatagrid.TabIndex = 0;
            this.licencehistoryDatagrid.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.licencehistoryDatagrid_CellEndEdit);
            // 
            // purchaseDate
            // 
            this.purchaseDate.HeaderText = "Дата покупки";
            this.purchaseDate.Name = "purchaseDate";
            this.purchaseDate.ReadOnly = true;
            // 
            // licenses
            // 
            this.licenses.HeaderText = "Кол-во лицензий";
            this.licenses.MaxInputLength = 3;
            this.licenses.Name = "licenses";
            // 
            // expirationDate
            // 
            this.expirationDate.HeaderText = "Срок лицензий";
            this.expirationDate.Name = "expirationDate";
            // 
            // supportExpirationDate
            // 
            this.supportExpirationDate.HeaderText = "Срок тех.поддержки";
            this.supportExpirationDate.Name = "supportExpirationDate";
            // 
            // guaranteeExpirationDate
            // 
            this.guaranteeExpirationDate.HeaderText = "Срок гарантийной поддержки";
            this.guaranteeExpirationDate.Name = "guaranteeExpirationDate";
            // 
            // G2LicencesHistory
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(567, 139);
            this.Controls.Add(this.licencehistoryDatagrid);
            this.Name = "G2LicencesHistory";
            this.Text = "G2LicencesHistory";
            ((System.ComponentModel.ISupportInitialize)(this.licencehistoryDatagrid)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView licencehistoryDatagrid;
        private System.Windows.Forms.DataGridViewTextBoxColumn purchaseDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn licenses;
        private System.Windows.Forms.DataGridViewTextBoxColumn expirationDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn supportExpirationDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn guaranteeExpirationDate;
    }
}