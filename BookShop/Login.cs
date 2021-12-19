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
    public partial class Login : Form
    {
        public static int UId = 1500;
        public Login()
        {
            InitializeComponent();
        }
        SqlConnection Con = new SqlConnection("Data Source=localhost;" + "Initial Catalog=BookShopDb;" + "Integrated Security=SSPI");
        public static string UserName = "";

        private void label4_Click(object sender, EventArgs e)
        {
            AdminLogin Obj = new AdminLogin();
            Obj.Show();
            this.Hide();
        }

        private void bunifuThinButton21_Click_1(object sender, EventArgs e)
        {
            Con.Open();
            SqlDataAdapter sda = new SqlDataAdapter("select count(*) from UserTbl where UName ='" + UnameTb.Text + "' and UPass='" + UPassTb.Text + "'", Con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            if(dt.Rows[0][0].ToString() == "1")
            {
                SqlDataAdapter sda1 = new SqlDataAdapter("select UId from UserTbl where UName ='" + UnameTb.Text + "' and UPass='" + UPassTb.Text + "'", Con);
                DataTable dt1 = new DataTable();
                sda1.Fill(dt1);
                UserName = UnameTb.Text;
                UId = Convert.ToInt32(dt1.Rows[0][0].ToString());
                Billing obj = new Billing();
                obj.Show();
                this.Hide();
                Con.Close();
            }
            else
            {
                MessageBox.Show("Wrong username or password");
            }
            Con.Close();
        }
    }
}
