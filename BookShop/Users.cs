using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BookShop
{
    public partial class Users : Form
    {
        public Users()
        {
            InitializeComponent();
            populate();
        }
        SqlConnection Con = new SqlConnection("Data Source=localhost;" + "Initial Catalog=BookShopDb;" + "Integrated Security=SSPI");
        private void populate()
        {
            Con.Open();
            string query = "select * from UserTbl";
            SqlDataAdapter sda = new SqlDataAdapter(query, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            BookDGV.DataSource = ds.Tables[0];
            Con.Close();
        }
        private void SaveBtn_Click(object sender, EventArgs e)
        {
            if (UNameTb.Text == "" || AddresTb.Text == "" || PassTb.Text == "" || UPhoneTb.Text == "")

            {
                MessageBox.Show("Missing Information");
            }
            else if(UPhoneTb.Text.Length != 11)
            {
                MessageBox.Show("Wrong Phone Format");
            }
            else
            {
                try
                {
                    Con.Open();
                    string query = "insert into UserTbl values('" + UNameTb.Text + "','"+UPhoneTb.Text+"','"+AddresTb.Text + "','"  + PassTb.Text + "');";
                    SqlCommand cmd = new SqlCommand(query, Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("User saved succesfully");
                    Con.Close();
                    populate();
                    Reset();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }
        private void Reset()
        {
            UNameTb.Text = "";
            PassTb.Text = "";
            UPhoneTb.Text = "";
            AddresTb.Text = "";
        }
        private void ResetBtn_Click(object sender, EventArgs e)
        {
            Reset();
        }

        private void DeleteBtn_Click(object sender, EventArgs e)
        {
            if (key == 0)
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                try
                {
                    Con.Open();
                    string query = "delete from UserTbl where UId=" + key + ";";
                    SqlCommand cmd = new SqlCommand(query, Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("User deleted succesfully");
                    Con.Close();
                    populate();
                    Reset();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }
        int key = 0;
        private void BookDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            UNameTb.Text = BookDGV.SelectedRows[0].Cells[1].Value.ToString();
            UPhoneTb.Text = BookDGV.SelectedRows[0].Cells[2].Value.ToString();
            AddresTb.Text = BookDGV.SelectedRows[0].Cells[3].Value.ToString();
            PassTb.Text = BookDGV.SelectedRows[0].Cells[4].Value.ToString();
            if (UNameTb.Text == "")
            {
                key = 0;
            }
            else
            {
                key = Convert.ToInt32(BookDGV.SelectedRows[0].Cells[0].Value.ToString());
            }
        }
        
private void EditBtn_Click(object sender, EventArgs e)
        {
            if (UNameTb.Text == "" || AddresTb.Text == "" || PassTb.Text == "" || UPhoneTb.Text == "")

            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                try
                {
                    Con.Open();
                    string query = "update UserTbl set UName ='" + UNameTb.Text + "',UPhone ='" + UPhoneTb.Text + "',Uadd='" + AddresTb.Text + "',UPass='" + PassTb.Text +"' where UId =" + key + ";";
                    SqlCommand cmd = new SqlCommand(query, Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("User Updated succesfully");
                    Con.Close();
                    populate();
                    Reset();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }

        private void label9_Click(object sender, EventArgs e)
        {
            Login Obj = new Login();
            Obj.Show();
            this.Hide();
        }

        private void label5_Click(object sender, EventArgs e)
        {
            Books Obj = new Books();
            Obj.Show();
            this.Hide();
        }

        private void label7_Click(object sender, EventArgs e)
        {
            DashBoard Obj = new DashBoard();
            Obj.Show();
            this.Hide();
        }
    }
}
