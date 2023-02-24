namespace Phoneshop.WinForms
{
    partial class AddForm
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
            this.brandLbl = new System.Windows.Forms.Label();
            this.brandInput = new System.Windows.Forms.TextBox();
            this.typeInput = new System.Windows.Forms.TextBox();
            this.descrInput = new System.Windows.Forms.TextBox();
            this.priceInput = new System.Windows.Forms.TextBox();
            this.typeLbl = new System.Windows.Forms.Label();
            this.descrLbl = new System.Windows.Forms.Label();
            this.priceLbl = new System.Windows.Forms.Label();
            this.applyButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.currLbl = new System.Windows.Forms.Label();
            this.stockLbl = new System.Windows.Forms.Label();
            this.stockInput = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // brandLbl
            // 
            this.brandLbl.AutoSize = true;
            this.brandLbl.Location = new System.Drawing.Point(16, 18);
            this.brandLbl.Name = "brandLbl";
            this.brandLbl.Size = new System.Drawing.Size(38, 15);
            this.brandLbl.TabIndex = 0;
            this.brandLbl.Text = "Brand";
            // 
            // brandInput
            // 
            this.brandInput.Location = new System.Drawing.Point(12, 36);
            this.brandInput.Name = "brandInput";
            this.brandInput.Size = new System.Drawing.Size(311, 23);
            this.brandInput.TabIndex = 1;
            // 
            // typeInput
            // 
            this.typeInput.Location = new System.Drawing.Point(12, 80);
            this.typeInput.Name = "typeInput";
            this.typeInput.Size = new System.Drawing.Size(311, 23);
            this.typeInput.TabIndex = 2;
            // 
            // descrInput
            // 
            this.descrInput.Location = new System.Drawing.Point(12, 124);
            this.descrInput.Name = "descrInput";
            this.descrInput.Size = new System.Drawing.Size(311, 23);
            this.descrInput.TabIndex = 3;
            // 
            // priceInput
            // 
            this.priceInput.Location = new System.Drawing.Point(35, 168);
            this.priceInput.Name = "priceInput";
            this.priceInput.PlaceholderText = "0";
            this.priceInput.Size = new System.Drawing.Size(125, 23);
            this.priceInput.TabIndex = 4;
            this.priceInput.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.priceInput_KeyPress);
            // 
            // typeLbl
            // 
            this.typeLbl.AutoSize = true;
            this.typeLbl.Location = new System.Drawing.Point(16, 62);
            this.typeLbl.Name = "typeLbl";
            this.typeLbl.Size = new System.Drawing.Size(31, 15);
            this.typeLbl.TabIndex = 5;
            this.typeLbl.Text = "Type";
            // 
            // descrLbl
            // 
            this.descrLbl.AutoSize = true;
            this.descrLbl.Location = new System.Drawing.Point(16, 106);
            this.descrLbl.Name = "descrLbl";
            this.descrLbl.Size = new System.Drawing.Size(67, 15);
            this.descrLbl.TabIndex = 6;
            this.descrLbl.Text = "Description";
            // 
            // priceLbl
            // 
            this.priceLbl.AutoSize = true;
            this.priceLbl.Location = new System.Drawing.Point(16, 150);
            this.priceLbl.Name = "priceLbl";
            this.priceLbl.Size = new System.Drawing.Size(33, 15);
            this.priceLbl.TabIndex = 7;
            this.priceLbl.Text = "Price";
            // 
            // applyButton
            // 
            this.applyButton.Location = new System.Drawing.Point(25, 258);
            this.applyButton.Name = "applyButton";
            this.applyButton.Size = new System.Drawing.Size(75, 23);
            this.applyButton.TabIndex = 11;
            this.applyButton.Text = "Apply";
            this.applyButton.UseVisualStyleBackColor = true;
            this.applyButton.Click += new System.EventHandler(this.OnClickApply);
            // 
            // cancelButton
            // 
            this.cancelButton.Location = new System.Drawing.Point(121, 258);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 12;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.OnClickCancel);
            // 
            // currLbl
            // 
            this.currLbl.AutoSize = true;
            this.currLbl.Location = new System.Drawing.Point(16, 171);
            this.currLbl.Name = "currLbl";
            this.currLbl.Size = new System.Drawing.Size(13, 15);
            this.currLbl.TabIndex = 8;
            this.currLbl.Text = "€";
            // 
            // stockLbl
            // 
            this.stockLbl.AutoSize = true;
            this.stockLbl.Location = new System.Drawing.Point(16, 213);
            this.stockLbl.Name = "stockLbl";
            this.stockLbl.Size = new System.Drawing.Size(36, 15);
            this.stockLbl.TabIndex = 9;
            this.stockLbl.Text = "Stock";
            // 
            // stockInput
            // 
            this.stockInput.Location = new System.Drawing.Point(60, 210);
            this.stockInput.Name = "stockInput";
            this.stockInput.PlaceholderText = "0";
            this.stockInput.Size = new System.Drawing.Size(100, 23);
            this.stockInput.TabIndex = 10;
            this.stockInput.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.stockInput_KeyPress);
            // 
            // AddForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(345, 304);
            this.Controls.Add(this.stockInput);
            this.Controls.Add(this.stockLbl);
            this.Controls.Add(this.currLbl);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.applyButton);
            this.Controls.Add(this.priceLbl);
            this.Controls.Add(this.descrLbl);
            this.Controls.Add(this.typeLbl);
            this.Controls.Add(this.priceInput);
            this.Controls.Add(this.descrInput);
            this.Controls.Add(this.typeInput);
            this.Controls.Add(this.brandInput);
            this.Controls.Add(this.brandLbl);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "AddForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Add Phone";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label brandLbl;
        private System.Windows.Forms.TextBox brandInput;
        private System.Windows.Forms.TextBox typeInput;
        private System.Windows.Forms.TextBox descrInput;
        private System.Windows.Forms.TextBox priceInput;
        private System.Windows.Forms.TextBox stockInput;
        private System.Windows.Forms.Label typeLbl;
        private System.Windows.Forms.Label descrLbl;
        private System.Windows.Forms.Label priceLbl;
        private System.Windows.Forms.Label stockLbl;
        private System.Windows.Forms.Button applyButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Label currLbl;
    }
}