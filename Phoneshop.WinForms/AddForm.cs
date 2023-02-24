using System;
using System.Globalization;
using System.Windows.Forms;

namespace Phoneshop.WinForms
{
    public partial class AddForm : Form
    {
        public string BrandName { get { return brandInput.Text; } }
        public string Type { get { return typeInput.Text; } }
        public string Description { get { return descrInput.Text; } }
        public decimal Price { get; private set; }

        public int Stock { get; private set; }

        // event to indicate that validated data is ready
        public event EventHandler DataValidated;

        public AddForm()
        {
            InitializeComponent();
        }

        // method to signal to subscribers that data is available
        protected void OnDataValidated(EventArgs e)
        {
            DataValidated?.Invoke(this, e);
        }

        private void OnClickApply(object sender, EventArgs e)
        {
            decimal price = 0;
            int stock = 0;
            if (Decimal.TryParse(priceInput.Text,
                NumberStyles.AllowDecimalPoint,
                CultureInfo.InvariantCulture,
                out decimal parsedPrice))
            {
                price = parsedPrice;
            }

            if (Int32.TryParse(stockInput.Text, out int parsedStock))
            {
                stock = parsedStock;
            }

            if (brandInput.Text.Length > 0 &&
                typeInput.Text.Length > 0 &&
                descrInput.Text.Length > 0 &&
                price > 0 &&
                parsedStock > 0)
            {
                Price = price;
                Stock = stock;

                // tell parent that data is ready
                OnDataValidated(e);

                this.Close();
            }
            else
            {
                MessageBox.Show("Please enter valid data in all fields.");
            }
        }

        private void OnClickCancel(object sender, EventArgs e)
        {
            this.Close();
        }

        private void priceInput_KeyPress(object sender, KeyPressEventArgs e)
        {
            //if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))

            // TODO check culture to see whether to block '.' or ','

            if (char.IsLetter(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void stockInput_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    }
}
