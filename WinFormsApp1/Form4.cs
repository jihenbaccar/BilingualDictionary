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

namespace WinFormsApp1
{
    public partial class Form4 : Form
    {
        String connectionString = "Data Source=.;Initial Catalog=authentification;Integrated Security=True";
        SqlDataAdapter adapter;
        SqlConnection connection;
        DataTable dt;

        public Form4()
        {
            InitializeComponent();
            adapter = new SqlDataAdapter();
            connection = new SqlConnection(connectionString);

        }

        private void Form4_Load(object sender, EventArgs e)
        {

        }// we make sure that this user is an admin or not, else they can't access to the admin privileges

        private void button1_Click(object sender, EventArgs e)
        {
            if ((textBox1.Text.Trim() != "") && (textBox2.Text.Trim() != ""))
            {
                bool exists = false;
                DataTable dt = new DataTable();
                connection.Open();
                adapter = new SqlDataAdapter("select * from admin where username ='" + textBox1.Text + "' and password='" + textBox2.Text + "'", connection);
                adapter.Fill(dt);
                connection.Close();

                if (dt.Rows.Count > 0)
                {
                    exists = true;
                }

                if (exists == true)
                {
                    MessageBox.Show("Welcome");

                    Form6 form = new Form6();
                    form.Show();
                    this.Hide();
                }
                else
                    MessageBox.Show("Make sure to write your informations correctly");

            }
            else
                MessageBox.Show("Please make sure you fill all the fields");
        }
    }
}
