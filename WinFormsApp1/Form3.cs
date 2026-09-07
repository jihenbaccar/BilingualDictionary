using System;
using System.Collections;
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
    public partial class Form3 : Form
    {
        String connectionString = "Data Source=.;Initial Catalog=Traduction;Integrated Security=True";
        SqlDataAdapter adapter;
        SqlConnection connection;

        public Form3()
        {
            InitializeComponent();
            adapter = new SqlDataAdapter();
            connection = new SqlConnection(connectionString);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            importdata();

        }


        private void importdata()
        {

            connection.Open();
            using (StreamReader reader = new StreamReader(@"C:\Users\Jihen\OneDrive\Documents\dictfile.txt"))
            {
                
               while(!reader.EndOfStream)
                {
                    string line= reader.ReadLine();
                    string[] str = line.Split("#");
                    string query = "insert into dict(english,french,type,exp_ang,exp_fr) values (@english,@french,@type,@exp_ang,@exp_fr)";
                 
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = connection;
                    cmd.CommandText = query;
                    cmd.CommandType= System.Data.CommandType.Text;
                    cmd.Parameters.AddWithValue("@english", str[0]);
                    cmd.Parameters.AddWithValue("@french", str[1]);
                    cmd.Parameters.AddWithValue("@type", str[2]);
                    cmd.Parameters.AddWithValue("@exp_ang", str[3]);
                    cmd.Parameters.AddWithValue("@exp_fr", str[4]);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("imported successfully");
                }
            
            
            }
            connection.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            exportdata();
        }

        private void exportdata()
        {
            connection.Open();
            SqlCommand cmd = new SqlCommand("select english,french,type,exp_ang,exp_fr from dict",connection);
            SqlDataReader rd = cmd.ExecuteReader();
            using (StreamWriter writer = new StreamWriter(@"C:\Users\Jihen\OneDrive\Documents\dictexport.txt"))
            {
                while (rd.Read())
                {
                    string line = string.Format("{0}#{1}#{2}#{3}#{4}",rd.GetString(0), rd.GetString(1),rd.GetString(2), rd.GetString(3),rd.GetString(4));
                    writer.WriteLine(line);
                }

                MessageBox.Show("Successfully exported");
            }
            rd.Close();
            connection.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form6 form = new Form6();
            form.Show();
            this.Hide();
        }
    }
          


    }
    

