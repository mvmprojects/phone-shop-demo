using Phoneshop.Domain.Interfaces;
using Phoneshop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Phoneshop.WinForms
{
    public partial class PhoneOverview : Form
    {
        //static readonly IPhoneService phoneService = new PhoneService();
        private static IPhoneService _phoneService;

        public PhoneOverview(IPhoneService phoneService)
        {
            _phoneService = phoneService;

            InitializeComponent();

            GetAll();
        }

        private void GetAll()
        {
            List<Phone> phones = _phoneService.GetPhones();

            PopulateListBox(phones);
        }

        private void ClearText()
        {
            brandBox.Clear();
            typeBox.Clear();
            descriptBox.Clear();
            priceBox.Clear();
            stockBox.Clear();
        }

        private void GetList(string query)
        {
            List<Phone> phones = _phoneService.Search(query);

            PopulateListBox(phones);
        }

        private void PopulateListBox(List<Phone> phones)
        {
            listBox.DataSource = phones;
            //listBox.DisplayMember = "FullName";
        }

        private void OnClickExit(object sender, EventArgs e)
        {
            if (MessageBox.Show(
                "Are you sure you wish to close the application?",
                "Exit",
                MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void OnClickItem(object sender, EventArgs e)
        {
            Phone item = (Phone)listBox.SelectedItem;
            if (item != null)
            {
                brandBox.Text = item.Brand.Name;
                priceBox.Text = item.Price.ToString("C");
                typeBox.Text = item.Type;
                descriptBox.Text = item.Description;
                stockBox.Text = item.Stock.ToString();
                idBox.Text = item.Id.ToString();

                deleteButton.Enabled = true;
            }
        }

        private void OnKeyStrokeSearch(object sender, EventArgs e)
        {
            if (searchBox.Text.Length < 1)
            {
                GetAll();
            }
            if (searchBox.Text.Length > 3)
            {
                GetList(searchBox.Text);
            }
        }

        private void OnClickDelete(object sender, EventArgs e)
        {
            if (MessageBox.Show(
                $"Are you sure you wish to delete this item?" +
                $"\n\n{brandBox.Text} - {typeBox.Text}",
                "Delete",
                MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                _phoneService.DeletePhone(Int32.Parse(idBox.Text));
                deleteButton.Enabled = false;
                ClearText();
                GetAll();
            }
        }

        private void OnClickCreate(object sender, EventArgs e)
        {
            AddForm addForm = new();
            addForm.DataValidated += new EventHandler(ChildDataValidated);
            addForm.ShowDialog();
        }

        // act when child form has valid data available
        private void ChildDataValidated(object sender, EventArgs e)
        {

            if (sender is AddForm child)
            {
                Phone phone = new()
                {
                    Brand = new Brand() { Name = child.BrandName },
                    Type = child.Type,
                    Description = child.Description,
                    Price = child.Price,
                    Stock = child.Stock
                };

                var result = _phoneService.CreatePhone(phone);

                if (result.Id <= 0)
                {
                    MessageBox
                        .Show("Problem detected: Phone was not added.");
                }

                GetAll();
            }
        }
    }
}
