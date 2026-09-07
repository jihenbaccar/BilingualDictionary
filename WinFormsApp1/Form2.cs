using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace WinFormsApp1
{
    public partial class Form2 : Form
    {
      
        String connectionString = "Data Source=.;Initial Catalog=Traduction;Integrated Security=True";
        SqlDataAdapter adapter;
        SqlConnection connection;
        DataTable dt;

        public Form2()
        {
          
            InitializeComponent();
            adapter = new SqlDataAdapter();
            connection = new SqlConnection(connectionString);
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        // to show the data in a datagridview

        private void showData()
        {
            adapter = new SqlDataAdapter("select english,french,type,exp_ang,exp_fr from dict ", connection);
            dt = new DataTable();
            adapter.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        private void Add_translation()
        {
            connection = new SqlConnection(connectionString);
            connection.Open();

            SqlCommand cmd = new SqlCommand("insert into dict(english,french,type,exp_ang,exp_fr) values (@ang, @fr, @type, @exp_ang, @exp_fr)",connection);

            cmd.Parameters.AddWithValue("@ang",textBox1.Text);
            cmd.Parameters.AddWithValue("@fr", textBox2.Text);
            cmd.Parameters.AddWithValue("@type", textBox3.Text);
            cmd.Parameters.AddWithValue("@exp_ang", textBox4.Text);
            cmd.Parameters.AddWithValue("@exp_fr", textBox5.Text);
            cmd.ExecuteNonQuery();
            connection.Close();

            MessageBox.Show("successfully added!!");
           
        }

        private void Delete_translation()
        {
            // we check if one of the textboxes (english or french word) are filled so we can search for it and if it exists we delete it from the database
            connection.Open();
            if (textBox1.Text.Trim() != "")
            {
                SqlCommand cmd = new SqlCommand("delete from dict where english = @english", connection);
                cmd.Parameters.AddWithValue("@english", textBox1.Text.Trim());
                if (cmd.ExecuteNonQuery() > 0)
                {
                    MessageBox.Show("Deleted successfully");
                }
                else
                {
                    MessageBox.Show("Failed to delete,this word doesn't exist");
                }


            }
            else if (textBox2.Text.Trim() != "")
            {
                SqlCommand cmd = new SqlCommand("delete from dict where french = @french", connection);
                cmd.Parameters.AddWithValue("@french", textBox2.Text.Trim());
                if (cmd.ExecuteNonQuery() > 0)
                {
                    MessageBox.Show("Deleted successfully");
                }
                else
                {
                    MessageBox.Show("Failed to delete,this word doesn't exist");
                }

            }
            connection.Close();

        }

       // this function searches a word from the database if it exists we fill the rest of the boxes

        private void Search()
        {
            Boolean exists = false; 
            connection.Open();
            if (textBox1.Text.Trim() != "")
            {
                SqlCommand cmd = new SqlCommand("select * from dict where english='" + textBox1.Text.Trim() + "'", connection);
                SqlDataReader myreader = cmd.ExecuteReader();

                if (myreader.Read())
                {
                    exists= true;
                    textBox2.Text = myreader.GetString(2);
                    textBox3.Text = myreader.GetString(3);
                    textBox4.Text = myreader.GetString(4);
                    textBox5.Text = myreader.GetString(5);
                }


            }
            
            else if (textBox2.Text.Trim() != "")
            {
                exists= true;
                SqlCommand cmd = new SqlCommand("select * from dict where french='" + textBox2.Text.Trim() + "'", connection);
                SqlDataReader myreader = cmd.ExecuteReader();
                if (myreader.Read())
                {
                    textBox1.Text = myreader.GetString(1);
                    textBox3.Text = myreader.GetString(3);
                    textBox4.Text = myreader.GetString(4);
                    textBox5.Text = myreader.GetString(5);
                }
            }

            if(exists==false) 
            {
                MessageBox.Show("Word doesn't exist");
            }

            connection.Close();

        }

        private void Update_translation()
        {
 
            connection.Open();           
            if (textBox1.Text.Trim() != "")  
            {

                SqlCommand cmd = new SqlCommand("update dict set english=@english,french= @french, type = @type, exp_ang = @ang, exp_fr = @fr where english = @word", connection);
                cmd.Parameters.AddWithValue("@word", textBox1.Text);
                cmd.Parameters.AddWithValue("@english", textBox1.Text);
                cmd.Parameters.AddWithValue("@french", textBox2.Text);
                cmd.Parameters.AddWithValue("@type", textBox3.Text);
                cmd.Parameters.AddWithValue("@ang", textBox4.Text);
                cmd.Parameters.AddWithValue("@fr", textBox5.Text);

                cmd.ExecuteNonQuery();
                MessageBox.Show("successfully Updated !!");
            }
            

                connection.Close();
            

        }

        // this button makes sure all the fields are filled so we can add a word

        private void button1_Click(object sender, EventArgs e)
        {
            connection.Open();

   
            if((textBox1.Text.Trim()=="") || (textBox2.Text.Trim() == "") || (textBox3.Text.Trim() == "") || (textBox4.Text.Trim() == "") || (textBox5.Text.Trim() == ""))
            {
                MessageBox.Show("Please fill all the fields ");
            }
            else
            {
                SqlCommand cmd = new SqlCommand("select * from dict where english='" + textBox1.Text.Trim() + "'", connection);
                SqlDataReader myreader = cmd.ExecuteReader();

                if (myreader.Read())
                {
                    MessageBox.Show("This word exists already");
                } else
                {
                    Add_translation();
                }
               
            }
            connection.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            showData();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Delete_translation();
           
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Search();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Update_translation();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Form6 form = new Form6();
            form.Show();
            this.Hide();
        }
    }
    
}

