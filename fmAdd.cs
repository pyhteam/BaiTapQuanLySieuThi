using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using QuanLySieuThi.Data;
using System.Windows.Forms;

namespace QuanLySieuThi
{
    public partial class fmAdd : Form
    {
        SqlConnection sqlConnection = new SqlConnection(Connect.connectString);
        public fmAdd()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                sqlConnection.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.Text;
                int status = rdbStatus1.Checked ? 1 : 0;
                cmd.CommandText = $"INSERT INTO Product VALUES ('{txtCode.Text}', '{txtName.Text}','{cbbProducer.SelectedItem.ToString()}','{cbbType.SelectedItem.ToString()}','{status}','{DateTime.Now}')";
                cmd.Connection = sqlConnection;
                var result = cmd.ExecuteNonQuery();
                if (result > 0)
                {
                    MessageBox.Show("Thêm thành công ");
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
