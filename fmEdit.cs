using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Windows.Forms;
using QuanLySieuThi.Data;

namespace QuanLySieuThi
{
    public partial class fmEdit : Form
    {
        SqlConnection sqlConnection = new SqlConnection(Connect.connectString);
        DataTable dataTable = null;
        int id;
        public fmEdit(int id)
        {
            this.id = id;
            InitializeComponent();
        }

        private void fmEdit_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            sqlConnection.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = sqlConnection;
            cmd.CommandText = $"SELECT * FROM Product WHERE id ={id}";
            SqlDataReader sdr;
            sdr = cmd.ExecuteReader();
            dataTable = new DataTable();
            dataTable.Load(sdr);

            txtCode.Text = dataTable.Rows[0][1].ToString();
            txtName.Text = dataTable.Rows[0][2].ToString();
            cbbProducer.Text = dataTable.Rows[0][3].ToString();
            cbbType.Text = dataTable.Rows[0][4].ToString();
         

            // check status
            string status = dataTable.Rows[0][5].ToString();
            if (status == "1")
            {
                rdbStatus1.Checked = true;
            }
            else
            {
                rdbStatus2.Checked = true;
            }
            txtDate.Text = dataTable.Rows[0][6].ToString();

            sqlConnection.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                sqlConnection.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.Text;
                int status = rdbStatus1.Checked ? 1 : 0;
                cmd.CommandText = $"UPDATE Product SET Name='{txtName.Text}', Producer='{cbbProducer.SelectedItem.ToString()}',Type='{cbbType.SelectedItem.ToString()}',Status='{status}' WHERE id ='{id}'";
                cmd.Connection = sqlConnection;
                var result = cmd.ExecuteNonQuery();
                if (result > 0)
                {
                    MessageBox.Show("Sửa thành công ");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi" + ex.Message, "Thông báo ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                sqlConnection.Close();
            }
        }
    }
}
