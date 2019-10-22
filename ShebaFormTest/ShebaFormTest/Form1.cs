using ShebaFormTest.BL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Windows.Forms;

namespace ShebaFormTest
{
    public partial class Form1 : Form
    {

        private string constr = ConfigurationManager.ConnectionStrings["Test"].ConnectionString;
        public Form1()
        {
            //GridboxView();
            InitializeComponent();
                   
        }

        private void GridboxView()
        {            
            string query = "SELECT * FROM Category";
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlDataAdapter sda = new SqlDataAdapter(query, con))
                {
                    using (DataTable dt = new DataTable())
                    {
                        sda.Fill(dt);
                        if (dt.Rows.Count > 0)
                            GridView.DataSource = dt;
                    }
                }
            }
            GridView.Click += new EventHandler(OnRowEditing);
        }

        //Clear Textbox
        private void ClearTextBox()
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
        }

        private void Button1_Click_1(object sender, EventArgs e)
        {
            Category category = new Category()
            {
                Name = textBox1.Text,
            };
            string query = "insert into Category (Name) values('"+category.Name+"')";
            using (SqlConnection con = new SqlConnection(constr))
            {
                SqlCommand cmd = new SqlCommand(query, con);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }

            ClearTextBox();
            GridboxView();
        }

        protected void OnRowEditing(object sender, EventArgs e)
        {
            //MessageBox.Show("SS");
            textBox1.Text = GridView.SelectedCells[1].Value.ToString();
            button1.Enabled = false;
            //MessageBox.Show(cat.ToString());
            //string query = "select * from category where Id =" + (int)cat;
            //SqlDataReader reader;
            //using (SqlConnection con = new SqlConnection(constr))
            //{
            //    using (SqlCommand cmd = new SqlCommand(query, con))
            //    {
            //        con.Open();
            //        reader = cmd.ExecuteReader();

            //        con.Close();
            //    }
            //}

            //textBox1.Text = reader;
        }

        private void Show_Click(object sender, EventArgs e)
        {
            GridboxView();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            
            int Id = (int)GridView.SelectedCells[0].Value;
            string name = textBox1.Text;

            string query = "update Category set Name='"+name+"' where Id='"+Id+"'";
            using (SqlConnection con = new SqlConnection(constr))
            {
                SqlCommand cmd = new SqlCommand(query, con);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
            ClearTextBox();
            GridboxView();
            button1.Enabled = true;
        }

        private void Button3_Click(object sender, EventArgs e)
        {

            int Id = (int)GridView.SelectedCells[0].Value;

            string query = "delete from category where Id='"+Id+"'";
            using (SqlConnection con = new SqlConnection(constr))
            {
                SqlCommand cmd = new SqlCommand(query, con);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }

            ClearTextBox();
            GridboxView();
            button1.Enabled = true;
        }
    }
}
