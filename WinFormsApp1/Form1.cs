using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        
        String connectionString = "Data Source=.;Initial Catalog=Traduction;Integrated Security=True";
        SqlDataAdapter adapter;
        SqlConnection connection;


        public Form1()
        {
          
          

            InitializeComponent();
            adapter = new SqlDataAdapter();
            connection = new SqlConnection(connectionString);

        }

        private void SearchData()
        {
            connection.Open();
            if ((textBox1.Text.Trim() != "") && (comboBox1.SelectedIndex==1))
            {
                label3.Text = "English --> French";
                SqlCommand cmd = new SqlCommand("select french,type,exp_fr,exp_ang from dict where lower(english) ='" + textBox1.Text.Trim().ToLower() + "'", connection);
                SqlDataReader myreader = cmd.ExecuteReader();

                if (myreader.Read())
                {
                    
                    listBox1.Items.Add(" Translation --> " + myreader.GetString(0));
                    listBox1.Items.Add( " Type --> " + myreader.GetString(1));
                    listBox1.Items.Add(" French example --> " + myreader.GetString(2));
                    listBox1.Items.Add(" English example --> " + myreader.GetString(3));

                }

                else
                {
                    MessageBox.Show("The word doesn't exists yet in our dictionnary");
                }


               
                
            }
            else if ((textBox1.Text.Trim() != "") && (comboBox1.SelectedIndex == 0))
            {
                label3.Text = "French --> English";
                SqlCommand cmd = new SqlCommand("select english,type,exp_ang,exp_fr from dict where lower(french) ='" + textBox1.Text.Trim().ToLower() + "'", connection);
                SqlDataReader myreader = cmd.ExecuteReader();

                if (myreader.Read())
                {
                    listBox1.Items.Add(" Translation --> " + myreader.GetString(0));
                    listBox1.Items.Add(" Type -->" + myreader.GetString(1));
                    listBox1.Items.Add(" English example -->" + myreader.GetString(2));
                    listBox1.Items.Add(" Frensh example --> " + myreader.GetString(3));
                }
                else
                {
                    MessageBox.Show("The word doesn't exists yet in our dictionnary");
                }



               
            } 
        else if (comboBox1.SelectedIndex == -1)
           { MessageBox.Show("Please choose the language"); }
        else
            { MessageBox.Show("Please make sure your wrote a word"); }

            connection.Close();
        }
  

        private void Form1_Load(object sender, EventArgs e)
        {
            label3.Text = "";
            try
            {
                connection = new SqlConnection(connectionString);
                AutoCompleteStringCollection c = new AutoCompleteStringCollection();
                connection.Open();
                SqlCommand cmd = new SqlCommand("select english ,french from dict", connection);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {

                    c.Add(reader.GetString(0));
                    c.Add(reader.GetString(1));



                }

                textBox1.AutoCompleteCustomSource = c;
                textBox1.AutoCompleteMode = AutoCompleteMode.Suggest;
                textBox1.AutoCompleteSource = AutoCompleteSource.CustomSource;
                connection.Close();
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }


        }

        private void button1_Click(object sender, EventArgs e)
        {       
            listBox1.Items.Clear();
                SearchData();
        }

       

        private void button1_Click_1(object sender, EventArgs e)
        {
            Form5 form = new Form5();
            form.Show();
            this.Hide();
        }
    }
}