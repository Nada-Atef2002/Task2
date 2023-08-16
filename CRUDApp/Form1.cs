using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace CRUDApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        SqlConnection cn;
        SqlCommand cmd;
        SqlDataAdapter da;
        SqlDataReader dr;
        private void Form1_Load_1(object sender, EventArgs e)
        {
            cn = new SqlConnection("Data Source=SQL5106.site4now.net;" +
                                   "Initial Catalog=db_a9cf6f_cruddb;" +
                                   "User Id=db_a9cf6f_cruddb_admin;Password=Mo123456");
        }
        private void Clear()
        {
            txtempid.Text = "";
            txtempISBN.Text = "";
            txtempname.Text = "";
        }
        private void label1_Click(object sender, EventArgs e)
        {

        }
        private void btnsave_Click_1(object sender, EventArgs e)
        {
            cn.Open();
            cmd = new SqlCommand("insert into Books values (@Book_ID , @ISBN , @Name)", cn);
            cmd.Parameters.AddWithValue("@Book_ID", int.Parse(this.txtempid.Text));
            cmd.Parameters.AddWithValue("@ISBN", int.Parse(this.txtempISBN.Text));
            cmd.Parameters.AddWithValue("@Name", this.txtempname.Text);
            cmd.ExecuteNonQuery();
            displaydata();
            Clear();
            cn.Close();
        }
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            cn.Open();
            cmd = new SqlCommand($"update Books set Name='{txtempname.Text}', ISBN='{txtempISBN.Text}' where Book_ID='{txtempid.Text}'", cn);
            cmd.ExecuteNonQuery();
            displaydata();
            Clear();
            cn.Close();
        }
        private void btnDelete_Click(object sender, EventArgs e)
        {
            cn.Open();
            cmd = new SqlCommand($"delete from Books where Book_ID='{txtempid.Text}'", cn);
            cmd.ExecuteNonQuery();
            displaydata();
            Clear();
            cn.Close();
        }
        private void displaydata()
        {
            cmd = cn.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from Books";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }
        private void btnshowall_Click(object sender, EventArgs e)
        {
            cn.Open();
            displaydata();
            Clear();
            cn.Close();
        }
        private void btnexit_Click(object sender, EventArgs e)
        {
            cn.Open();
            Application.Exit();
            cn.Close();
        }
        private void btnfind_Click(object sender, EventArgs e)
        {
            cn.Open();
            cmd = cn.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = $"select * from Books where Book_ID='{txtempid.Text}'";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            Clear();
            cn.Close();
        }
    }
}
