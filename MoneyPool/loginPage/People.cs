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
    public partial class People : Form
    {
        public static string roomCode = Room.RoomCode;
        SqlConnection connection;
        SqlCommand command;
        string address = "Data Source = DESKTOP-FVDPVI1\\SAMEER; Initial Catalog = MONEYPOOL; Integrated Security = True";
        SqlDataAdapter dataAdapter;
        public DataTable dt;
        public People()
        {
            InitializeComponent();
            connection = new SqlConnection(address);
            display();
            
        }
        public void display()
        {
            connection.Open();
            dt = new DataTable();
            dataAdapter = new SqlDataAdapter("SELECT r.UserID,u.UserFirstName,u.UserLastName FROM ROOMDETAILS AS r Join UserInfo AS u ON r.UserID = u.UserID WHERE r.RoomID = '"+roomCode+"'", connection);
            dataAdapter.Fill(dt);
            guna2DataGridView1.DataSource = dt;
            connection.Close();


        }
        private void BtnHome_Click(object sender, EventArgs e)
        {
            Room Room = new Room();
            this.Hide();    //Hide the Old Form
            Room.ShowDialog();    //Show the New Form
            this.Close();    //Close the Old Form
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
            Room room = new Room();
            this.Hide();    //Hide the Old Form
            room.ShowDialog();    //Show the New Form
            this.Close();    //Close the Old Form
        }
    }
}
