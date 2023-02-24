namespace Phoneshop.WinForms
{
    partial class PhoneOverview
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.searchBox = new System.Windows.Forms.TextBox();
            this.listBox = new System.Windows.Forms.ListBox();
            this.brandLbl = new System.Windows.Forms.Label();
            this.typeLbl = new System.Windows.Forms.Label();
            this.priceLbl = new System.Windows.Forms.Label();
            this.descrLbl = new System.Windows.Forms.Label();
            this.descriptBox = new System.Windows.Forms.TextBox();
            this.brandBox = new System.Windows.Forms.TextBox();
            this.typeBox = new System.Windows.Forms.TextBox();
            this.priceBox = new System.Windows.Forms.TextBox();
            this.exitButton = new System.Windows.Forms.Button();
            this.createButton = new System.Windows.Forms.Button();
            this.deleteButton = new System.Windows.Forms.Button();
            this.idBox = new System.Windows.Forms.TextBox();
            this.stockLbl = new System.Windows.Forms.Label();
            this.stockBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // searchBox
            // 
            this.searchBox.Location = new System.Drawing.Point(12, 12);
            this.searchBox.Name = "searchBox";
            this.searchBox.PlaceholderText = "Search";
            this.searchBox.Size = new System.Drawing.Size(284, 23);
            this.searchBox.TabIndex = 0;
            this.searchBox.TextChanged += new System.EventHandler(this.OnKeyStrokeSearch);
            // 
            // listBox
            // 
            this.listBox.FormattingEnabled = true;
            this.listBox.ItemHeight = 15;
            this.listBox.Location = new System.Drawing.Point(12, 54);
            this.listBox.Name = "listBox";
            this.listBox.Size = new System.Drawing.Size(284, 349);
            this.listBox.TabIndex = 1;
            this.listBox.Click += new System.EventHandler(this.OnClickItem);
            // 
            // brandLbl
            // 
            this.brandLbl.AutoSize = true;
            this.brandLbl.Location = new System.Drawing.Point(333, 15);
            this.brandLbl.Name = "brandLbl";
            this.brandLbl.Size = new System.Drawing.Size(41, 15);
            this.brandLbl.TabIndex = 2;
            this.brandLbl.Text = "Brand:";
            // 
            // typeLbl
            // 
            this.typeLbl.AutoSize = true;
            this.typeLbl.Location = new System.Drawing.Point(333, 64);
            this.typeLbl.Name = "typeLbl";
            this.typeLbl.Size = new System.Drawing.Size(34, 15);
            this.typeLbl.TabIndex = 3;
            this.typeLbl.Text = "Type:";
            // 
            // priceLbl
            // 
            this.priceLbl.AutoSize = true;
            this.priceLbl.Location = new System.Drawing.Point(492, 15);
            this.priceLbl.Name = "priceLbl";
            this.priceLbl.Size = new System.Drawing.Size(36, 15);
            this.priceLbl.TabIndex = 4;
            this.priceLbl.Text = "Price:";
            // 
            // descrLbl
            // 
            this.descrLbl.AutoSize = true;
            this.descrLbl.Location = new System.Drawing.Point(333, 112);
            this.descrLbl.Name = "descrLbl";
            this.descrLbl.Size = new System.Drawing.Size(70, 15);
            this.descrLbl.TabIndex = 5;
            this.descrLbl.Text = "Description:";
            // 
            // descriptBox
            // 
            this.descriptBox.Location = new System.Drawing.Point(333, 139);
            this.descriptBox.Multiline = true;
            this.descriptBox.Name = "descriptBox";
            this.descriptBox.ReadOnly = true;
            this.descriptBox.Size = new System.Drawing.Size(662, 264);
            this.descriptBox.TabIndex = 6;
            // 
            // brandBox
            // 
            this.brandBox.Location = new System.Drawing.Point(380, 12);
            this.brandBox.Name = "brandBox";
            this.brandBox.ReadOnly = true;
            this.brandBox.Size = new System.Drawing.Size(100, 23);
            this.brandBox.TabIndex = 7;
            // 
            // typeBox
            // 
            this.typeBox.Location = new System.Drawing.Point(380, 61);
            this.typeBox.Name = "typeBox";
            this.typeBox.ReadOnly = true;
            this.typeBox.Size = new System.Drawing.Size(100, 23);
            this.typeBox.TabIndex = 8;
            // 
            // priceBox
            // 
            this.priceBox.Location = new System.Drawing.Point(534, 12);
            this.priceBox.Name = "priceBox";
            this.priceBox.ReadOnly = true;
            this.priceBox.Size = new System.Drawing.Size(100, 23);
            this.priceBox.TabIndex = 9;
            // 
            // exitButton
            // 
            this.exitButton.Location = new System.Drawing.Point(911, 409);
            this.exitButton.Name = "exitButton";
            this.exitButton.Size = new System.Drawing.Size(84, 33);
            this.exitButton.TabIndex = 10;
            this.exitButton.Text = "Exit";
            this.exitButton.UseVisualStyleBackColor = true;
            this.exitButton.Click += new System.EventHandler(this.OnClickExit);
            // 
            // createButton
            // 
            this.createButton.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.createButton.Location = new System.Drawing.Point(22, 419);
            this.createButton.Name = "createButton";
            this.createButton.Size = new System.Drawing.Size(25, 25);
            this.createButton.TabIndex = 11;
            this.createButton.Text = "+";
            this.createButton.UseVisualStyleBackColor = true;
            this.createButton.Click += new System.EventHandler(this.OnClickCreate);
            // 
            // deleteButton
            // 
            this.deleteButton.Enabled = false;
            this.deleteButton.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.deleteButton.Location = new System.Drawing.Point(72, 419);
            this.deleteButton.Name = "deleteButton";
            this.deleteButton.Size = new System.Drawing.Size(25, 25);
            this.deleteButton.TabIndex = 12;
            this.deleteButton.Text = "-";
            this.deleteButton.UseVisualStyleBackColor = true;
            this.deleteButton.Click += new System.EventHandler(this.OnClickDelete);
            // 
            // idBox
            // 
            this.idBox.Location = new System.Drawing.Point(696, 12);
            this.idBox.Name = "idBox";
            this.idBox.Size = new System.Drawing.Size(100, 23);
            this.idBox.TabIndex = 13;
            this.idBox.Visible = false;
            // 
            // stockLbl
            // 
            this.stockLbl.AutoSize = true;
            this.stockLbl.Location = new System.Drawing.Point(492, 64);
            this.stockLbl.Name = "stockLbl";
            this.stockLbl.Size = new System.Drawing.Size(39, 15);
            this.stockLbl.TabIndex = 14;
            this.stockLbl.Text = "Stock:";
            // 
            // stockBox
            // 
            this.stockBox.Location = new System.Drawing.Point(534, 61);
            this.stockBox.Name = "stockBox";
            this.stockBox.ReadOnly = true;
            this.stockBox.Size = new System.Drawing.Size(100, 23);
            this.stockBox.TabIndex = 15;
            // 
            // PhoneOverview
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1042, 454);
            this.Controls.Add(this.stockBox);
            this.Controls.Add(this.stockLbl);
            this.Controls.Add(this.idBox);
            this.Controls.Add(this.deleteButton);
            this.Controls.Add(this.createButton);
            this.Controls.Add(this.exitButton);
            this.Controls.Add(this.priceBox);
            this.Controls.Add(this.typeBox);
            this.Controls.Add(this.brandBox);
            this.Controls.Add(this.descriptBox);
            this.Controls.Add(this.descrLbl);
            this.Controls.Add(this.priceLbl);
            this.Controls.Add(this.typeLbl);
            this.Controls.Add(this.brandLbl);
            this.Controls.Add(this.listBox);
            this.Controls.Add(this.searchBox);
            this.Name = "PhoneOverview";
            this.Text = "Phoneshop";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox searchBox;
        private System.Windows.Forms.ListBox listBox;
        private System.Windows.Forms.Label brandLbl;
        private System.Windows.Forms.Label typeLbl;
        private System.Windows.Forms.Label priceLbl;
        private System.Windows.Forms.Label descrLbl;
        private System.Windows.Forms.TextBox descriptBox;
        private System.Windows.Forms.TextBox brandBox;
        private System.Windows.Forms.TextBox typeBox;
        private System.Windows.Forms.TextBox priceBox;
        private System.Windows.Forms.Button exitButton;
        private System.Windows.Forms.Button createButton;
        private System.Windows.Forms.Button deleteButton;
        private System.Windows.Forms.TextBox idBox;
        private System.Windows.Forms.Label stockLbl;
        private System.Windows.Forms.TextBox stockBox;
    }
}
