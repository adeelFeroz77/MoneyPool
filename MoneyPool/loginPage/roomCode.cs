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
    public partial class roomCode : Form
    {
        string email;
        public static string RoomCode;
        SqlConnection connection;
        SqlCommand command;
        string address = "Data Source = DESKTOP-FVDPVI1\\SAMEER; Initial Catalog = MONEYPOOL; Integrated Security = True";
        public roomCode()
        {
            InitializeComponent();
            connection = new SqlConnection(address);
        }

        private void guna2HtmlLabel1_Click(object sender, EventArgs e)
        {

        }

        private void guna2HtmlLabel2_Click(object sender, EventArgs e)
        {

        }

        private void BtnJoinRoom_Click(object sender, EventArgs e)
        {
            string roomCapacity = "";
            string userCount = "";
            string check = null;

            RoomCode = tbRoomCode.Text;
            email = Form1.email;
            string adminID;
            if (RoomCode == null)
            {
                MessageBox.Show("Room Code can't be empty");
             
                return;
            }
            try
            {
                connection.Open();
                command = new SqlCommand("Select UserID from ROOM where RoomID = '"+RoomCode+"'", connection);
                adminID = (string)command.ExecuteScalar();
                if(adminID == null)
                {
                    MessageBox.Show("Room Doesn't Exist");
                    connection.Close();
                    return;
                }
                if(adminID == email)
                {
                    MessageBox.Show("You're Already Joined in this room");
                    connection.Close();
                    return;
                }
                command = new SqlCommand("Select RoomName from ROOM where RoomID = '" + RoomCode + "'", connection);
                command = new SqlCommand("Select RoomCapacity from Room where RoomID = '" + RoomCode + "'", connection);
                roomCapacity = (string)command.ExecuteScalar().ToString();
                command = new SqlCommand("Select Count(UserID) from ROOMDETAILS where RoomID = '" + RoomCode + "'", connection);
                userCount = (string)command.ExecuteScalar().ToString();

                if (userCount == roomCapacity)
                {
                    MessageBox.Show("Room Is Full");
                    connection.Close();
                    return;
                }

                

                command = new SqlCommand("Select USERID from ROOMDETAILS where RoomID = '"+RoomCode+"' and USERID = '"+email+"'", connection);
                string var = (string)command.ExecuteScalar();
                if (var != null) {
                    MessageBox.Show("You're Already Joined in this room");
                    connection.Close();
                    return;
                }
                command = new SqlCommand("INSERT INTO ROOMDETAILS VALUES('" + email + "','" + RoomCode + "','" + adminID + "')", connection);
                command.ExecuteNonQuery();
                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                connection.Close();
            }
            Room Room = new Room();
            this.Hide();    //Hide the Old Form
            Room.ShowDialog();    //Show the New Form
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
