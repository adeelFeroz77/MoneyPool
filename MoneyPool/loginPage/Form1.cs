using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Data.SqlClient;
namespace loginPage
{
    public partial class Form1 : Form
    {
     
        SqlConnection connection;
        SqlCommand command;
        string address = "Data Source = DESKTOP-FVDPVI1\\SAMEER; Initial Catalog = MONEYPOOL; Integrated Security = True";
        public static string email;
        string password;

        public Form1()
        {
            InitializeComponent();
            connection = new SqlConnection(address);
            




        }

        private const int CS_DropShadow = 0x000020000;  
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ClassStyle |= CS_DropShadow;
                return cp;
            }

        }

        private void Form1_Load(object sender, EventArgs e)
        {
           
        }

        private void guna2Panel2_Paint(object sender, PaintEventArgs e)
        {

        }


        private void guna2Button1_Click(object sender, EventArgs e)
        {

        }

        private void guna2Button1_Click_1(object sender, EventArgs e)
        {
            SignUp signup = new SignUp();
            this.Hide();    //Hide the Old Form
            signup.ShowDialog();    //Show the New Form
            this.Close();    //Close the Old Form

        }

        private void BtnLogin_Click(object sender, EventArgs e)
        {
            email = emailID.Text;
            password = Pass.Text;
            string check = "";
            try
            {
                connection.Open();
                command = new SqlCommand("SELECT UserPassword FROM USERINFO WHERE UserID = '"+email+"'", connection);
                check = (string)command.ExecuteScalar();

                connection.Close();
            }
            catch (Exception ex)
            {
                connection.Close();
                MessageBox.Show(ex.Message);
            }
            if (check == "")
            {
                MessageBox.Show("Enter Email or Password");
                emptyFields();
                return;
            }
            if (check != password)
            {
                MessageBox.Show("Enter Correct Email or Password");
                emptyFields();
                return;
            }
            Home home = new Home();
            this.Hide();    //Hide the Old Form
            home.ShowDialog();    //Show the New Form
            this.Close();    //Close the Old Form
        }
        public void emptyFields()
        {
            emailID.Text = "";
            Pass.Text = "";

        }

    }
 }


