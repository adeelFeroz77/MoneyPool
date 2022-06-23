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

namespace loginPage
{
    public partial class SignUp : Form
    {
        string firstname;
        string lastname;
        string Email;
        string pass;
        string cPassword;
        string phone;
        SqlCommand cmd;
        SqlConnection con;
        string address = "Data Source = DESKTOP-FVDPVI1\\SAMEER; Initial Catalog = MONEYPOOL; Integrated Security = True";
        public SignUp()
        {
            InitializeComponent();
            con = new SqlConnection(address);
        }

        private void loginLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form1 form1  = new Form1();
            this.Hide();    //Hide the Old Form
            form1.ShowDialog();    //Show the New Form
            this.Close();    //Close the Old Form
        }

        private void btnsignUp_Click(object sender, EventArgs e)
        {
            firstname = fname.Text;
            lastname = lname.Text;
            Email = email.Text;
            pass = password.Text;
            cPassword = confirmPassword.Text;
            phone = phoneNumber.Text;
            if (firstname == "" || lastname == "")
            {
                MessageBox.Show("Please enter your first/last name");
                return;
            }
            if (Email == "")
            {
                MessageBox.Show("Please enter email");
                return;
            }
            if (phone == "")
            {
                MessageBox.Show("Please enter your phone number");
                return;
            }
            if (pass == "" || cPassword == "")
            {
                MessageBox.Show("Please enter password");
                return;
            }
            if (pass != cPassword)
            {
                MessageBox.Show("Password fields must be same");
                return;
            }
            try
            {
                con.Open();
                cmd = new SqlCommand("INSERT INTO USERINFO VALUES('"+Email+"','"+firstname+"','"+lastname+"','"+phone+"','"+pass+"')", con);
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Account Registered!");
            }
            catch (Exception ex)
            {
                con.Close();
                MessageBox.Show(ex.Message);
                return;
            }



            Form1 form1 = new Form1();
            this.Hide();    //Hide the Old Form
            form1.ShowDialog();    //Show the New Form
            this.Close();    //Close the Old Form
        }

        private void phoneNumber_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
