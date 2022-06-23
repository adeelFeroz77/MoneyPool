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

    public partial class CreateRoom : Form
    {
        string roomName;
        public static string roomCode;
        int users;
        int amount;
        string email = Form1.email;
        SqlConnection connection;
        SqlCommand command;
        Random random = new Random();
        string address = "Data Source = DESKTOP-FVDPVI1\\SAMEER; Initial Catalog = MONEYPOOL; Integrated Security = True";
        public CreateRoom()
        {
            InitializeComponent();
            connection = new SqlConnection(address);
       

        }

        private void CreateRoom_Load(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void guna2TextBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void BtnHome_Click(object sender, EventArgs e)
        {
            Home home = new Home();
            this.Hide();    //Hide the Old Form
            home.ShowDialog();    //Show the New Form
            this.Close();    //Close the Old Form
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            if (guna2TextBox1.Text==null && guna2TextBox3.Text==null && guna2TextBox2.Text == null)
            {
                MessageBox.Show("Fields Can't Be Empty");
                return;
            }

            roomName = guna2TextBox3.Text;
            users = int.Parse(guna2TextBox1.Text);
            amount = int.Parse(guna2TextBox2.Text);

            
            try
            {
                for (int i = 0; i < 5; i++)
                    roomCode += (char)random.Next(65, 90);
                connection.Open();
                command = new SqlCommand("INSERT INTO ROOM VALUES('" + roomName + "','" + roomCode + "','" + email + "'," + users + "," + amount + ")", connection);
                command.ExecuteNonQuery();
                command = new SqlCommand("INSERT INTO ROOMDETAILS VALUES('"+email+"','"+roomCode+ "','" + email + "')", connection);
                command.ExecuteNonQuery();
                //int currentCount;
                //command = new SqlCommand("INSERT INTO ROOMDETAILS VALUES('" + email + "','" + roomCode + "','" + email + "')", connection);
                //command.ExecuteNonQuery();
                connection.Close();

                Room Room = new Room();
                this.Hide();    //Hide the Old Form
                Room.ShowDialog();    //Show the New Form
                this.Close();    //Close the Old Form
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                connection.Close();
                guna2Button1_Click(sender, e);
            }

            
        }

        private void panel1_Click(object sender, EventArgs e)
        {
            Home home = new Home();
            this.Hide();    //Hide the Old Form
            home.ShowDialog();    //Show the New Form
            this.Close();    //Close the Old Form
        }

        private void backImgBTn_Click(object sender, EventArgs e)
        {
            Home home = new Home();
            this.Hide();    //Hide the Old Form
            home.ShowDialog();    //Show the New Form
            this.Close();    //Close the Old Form
        }
    }
}
