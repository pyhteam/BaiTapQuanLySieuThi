using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using QuanLySieuThi.Data;
using System.Windows.Forms;

namespace QuanLySieuThi
{
    public partial class fmMain : Form
    {
        SqlConnection sqlConnection = new SqlConnection(Connect.connectString);
        DataTable dataTable = null;
        public fmMain()
        {
            InitializeComponent();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            var fmAdd =  new fmAdd();
            fmAdd.ShowDialog();
        }

        private void fmMain_Load(object sender, EventArgs e)
        {
            LoadData();
        }
        private void LoadData()
        {


            sqlConnection.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = sqlConnection;
            cmd.CommandText = "SELECT * FROM Product";
            SqlDataReader sdr;
            sdr = cmd.ExecuteReader();
            dataTable = new DataTable();
            dataTable.Load(sdr);


            gvProduct.DataSource = dataTable;
            gvProduct.Columns[0].HeaderText = "Id";
            gvProduct.Columns[1].HeaderText = "Mã";
            gvProduct.Columns[2].HeaderText = "Tên ";
            gvProduct.Columns[3].HeaderText = "Nhà sản xuất";
            gvProduct.Columns[4].HeaderText = "Loại Mặc hàng ";
            gvProduct.Columns[5].HeaderText = "Trạng thái";
            gvProduct.Columns[6].HeaderText = "Ngày Tạo";

            gvProduct.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            sqlConnection.Close();
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(gvProduct.CurrentRow.Cells[0].Value.ToString());
            var fmEditProduct = new fmEdit(id);
            fmEditProduct.ShowDialog();
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("bạn có chắc chắn muốn xóa", "confirmation", MessageBoxButtons.YesNo);
            if (dr == DialogResult.Yes)
            {
                int id = Convert.ToInt32(gvProduct.CurrentRow.Cells[0].Value.ToString());
                SqlCommand cmd = new SqlCommand($"DELETE FROM Product WHERE id='{id}'", sqlConnection);
                sqlConnection.Open();
                int result = cmd.ExecuteNonQuery();
                if (result == 1)
                {
                    sqlConnection.Close();
                    MessageBox.Show("Record Deleted Successfully");
                    dataTable.Clear();
                    LoadData();
                }
                else
                {
                    MessageBox.Show("Record not Deleted....Please try again.");
                }

            }
        }
        private void gvProduct_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                this.gvProduct.CurrentCell = this.gvProduct.Rows[e.RowIndex].Cells[e.ColumnIndex];
                this.contextMenuStrip1.Show(this.gvProduct, new Point(e.RowIndex, e.ColumnIndex));
            }
        }

        private void gvProduct_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                this.gvProduct.CurrentCell = this.gvProduct.Rows[e.RowIndex].Cells[e.ColumnIndex];
                this.contextMenuStrip1.Show(this.gvProduct, new Point(e.RowIndex, e.ColumnIndex));
            }
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            dataTable.Clear();
            LoadData();
        }

        private void btnSeach_Click(object sender, EventArgs e)
        {
            dataTable.Clear();
            sqlConnection.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = sqlConnection;
            cmd.CommandText = $"SELECT * FROM Product WHERE Name LIKE '%{txtSearch.Text}%'";
            SqlDataReader sdr;
            sdr = cmd.ExecuteReader();
            dataTable = new DataTable();
            dataTable.Load(sdr);


            gvProduct.DataSource = dataTable;
            gvProduct.Columns[0].HeaderText = "Id";
            gvProduct.Columns[1].HeaderText = "Mã";
            gvProduct.Columns[2].HeaderText = "Tên ";
            gvProduct.Columns[3].HeaderText = "Nhà sản xuất";
            gvProduct.Columns[4].HeaderText = "Loại Mặc hàng ";
            gvProduct.Columns[5].HeaderText = "Trạng thái";
            gvProduct.Columns[6].HeaderText = "Ngày Tạo";

            gvProduct.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            sqlConnection.Close();
        }

        private void btnSort_Click(object sender, EventArgs e)
        {
            string value = cbbSort.SelectedText.ToString();
            switch (value)
            {
                case "Code":
                    SeachByCode();
                    break;
                case "Name":
                    SeachByName();
                    break;
                case "Date":
                    SeachByDate();
                    break;
                default:
                    SeachByCode();
                    break;
            }
        }

        private void SeachByDate()
        {
            dataTable.Clear();
            sqlConnection.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = sqlConnection;
            cmd.CommandText = $"SELECT * FROM Product ORDER BY Date DESC";
            SqlDataReader sdr;
            sdr = cmd.ExecuteReader();
            dataTable = new DataTable();
            dataTable.Load(sdr);


            gvProduct.DataSource = dataTable;
            gvProduct.Columns[0].HeaderText = "Id";
            gvProduct.Columns[1].HeaderText = "Mã";
            gvProduct.Columns[2].HeaderText = "Tên ";
            gvProduct.Columns[3].HeaderText = "Nhà sản xuất";
            gvProduct.Columns[4].HeaderText = "Loại Mặc hàng ";
            gvProduct.Columns[5].HeaderText = "Trạng thái";
            gvProduct.Columns[6].HeaderText = "Ngày Tạo";

            gvProduct.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            sqlConnection.Close();
        }

        private void SeachByName()
        {
            dataTable.Clear();
            sqlConnection.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = sqlConnection;
            cmd.CommandText = $"SELECT * FROM Product ORDER BY Name DESC";
            SqlDataReader sdr;
            sdr = cmd.ExecuteReader();
            dataTable = new DataTable();
            dataTable.Load(sdr);


            gvProduct.DataSource = dataTable;
            gvProduct.Columns[0].HeaderText = "Id";
            gvProduct.Columns[1].HeaderText = "Mã";
            gvProduct.Columns[2].HeaderText = "Tên ";
            gvProduct.Columns[3].HeaderText = "Nhà sản xuất";
            gvProduct.Columns[4].HeaderText = "Loại Mặc hàng ";
            gvProduct.Columns[5].HeaderText = "Trạng thái";
            gvProduct.Columns[6].HeaderText = "Ngày Tạo";

            gvProduct.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            sqlConnection.Close();
        }

        private void SeachByCode()
        {
            dataTable.Clear();
            sqlConnection.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = sqlConnection;
            cmd.CommandText = $"SELECT * FROM Product ORDER BY Code DESC";
            SqlDataReader sdr;
            sdr = cmd.ExecuteReader();
            dataTable = new DataTable();
            dataTable.Load(sdr);


            gvProduct.DataSource = dataTable;
            gvProduct.Columns[0].HeaderText = "Id";
            gvProduct.Columns[1].HeaderText = "Mã";
            gvProduct.Columns[2].HeaderText = "Tên ";
            gvProduct.Columns[3].HeaderText = "Nhà sản xuất";
            gvProduct.Columns[4].HeaderText = "Loại Mặc hàng ";
            gvProduct.Columns[5].HeaderText = "Trạng thái";
            gvProduct.Columns[6].HeaderText = "Ngày Tạo";

            gvProduct.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            sqlConnection.Close();
        }
    }
}
