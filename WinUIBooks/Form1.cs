using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Library.Books.Models;

namespace WinUIBooks
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            var book = new Book
            {
                Title = txtTitle.Text,
                Author = txtAuthor.Text,
                Price = decimal.Parse(txtPrice.Text),
                Description = txtDescription.Text,
                CountryId = (int)cmbCountry.SelectedValue
            };

            BookRepo repo = new BookRepo();
            repo.AddBook(book);
        }

        private void LoadBooks()
        {
            BookRepo repo = new BookRepo();
            this.grdBooks.DataSource = null;
            this.grdBooks.DataSource = repo.GetAllBooks();
        }

        //private void LoadBooksHideGridColumns()
        //{
        //    BookRepo repo = new BookRepo();
        //    this.grdBooks.DataSource = null;
        //    this.grdBooks.DataSource = repo.GetAllBooks();
        //    this.grdBooks.Columns["id"].Visible = false;
        //}

        private void PopulateCountries()
        {
            BookRepo repo = new BookRepo();
            this.cmbCountry.DisplayMember = "Name";
            this.cmbCountry.ValueMember = "Id";
            this.cmbCountry.DataSource = repo.GetCountries();
            LoadBooks();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadBooks();
            PopulateCountries();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            var book = new Book
            {
                Id = int.Parse(lblId.Text),
                Title = txtTitle.Text,
                Author = txtAuthor.Text,
                Price = decimal.Parse(txtPrice.Text),
                Description = txtDescription.Text,
                CountryId = (int)cmbCountry.SelectedValue
            };

            BookRepo repo = new BookRepo();
            repo.UpdateBook(book);
            LoadBooks();
        }
        private void grdBooks_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            lblId.Text = grdBooks.Rows[e.RowIndex].Cells[0].Value.ToString();
            txtTitle.Text = grdBooks.Rows[e.RowIndex].Cells[1].Value.ToString();
            txtAuthor.Text = grdBooks.Rows[e.RowIndex].Cells[2].Value.ToString();
            txtPrice.Text = grdBooks.Rows[e.RowIndex].Cells[3].Value.ToString();
            txtDescription.Text = grdBooks.Rows[e.RowIndex].Cells[4].Value.ToString();
        }

        private void btnInsertGetId_Click(object sender, EventArgs e)
        {
            var book = new Book
            {
                Title = txtTitle.Text,
                Author = txtAuthor.Text,
                Price = decimal.Parse(txtPrice.Text),
                Description = txtDescription.Text,
                CountryId = 2
            };

            BookRepo repo = new BookRepo();
            int id = repo.AddBookReturnId(book);
            MessageBox.Show("Generated ID is: " + id.ToString());
            LoadBooks();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            var book = new Book
            {
                Id = int.Parse(lblId.Text),
                Title = txtTitle.Text,
                Author = txtAuthor.Text,
                Price = decimal.Parse(txtPrice.Text),
                Description = txtDescription.Text,
                CountryId = (int)cmbCountry.SelectedValue
            };

            BookRepo repo = new BookRepo();
            repo.DeleteBookId(book.Id);
            LoadBooks();
        }
        private void cmbCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            BookRepo repo = new BookRepo();
            string value = cmbCountry.SelectedValue.ToString();
            var list = repo.GetAuthorsFromSelectedCountry(Convert.ToInt32(value));
            lstAuthor.ValueMember = "Author";
            lstAuthor.DataSource = list;
        }

        private void lstAuthor_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnChangeColor_Click(object sender, EventArgs e)
        {
            if (rbGreen.Checked)
            {
                this.BackColor = Color.Green;
            }
            if (rbRed.Checked)
            {
                this.BackColor = Color.Red;
            }
            if (rbAqua.Checked)
            {
                this.BackColor = Color.Aqua;
            }
        }
    }
}
