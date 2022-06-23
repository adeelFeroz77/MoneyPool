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
    public partial class ViewRoom : Form
    {
       public static string roomCode;
        string email = Form1.email;
        SqlConnection connection;
        SqlCommand command;
        string address = "Data Source = DESKTOP-FVDPVI1\\SAMEER; Initial Catalog = MONEYPOOL; Integrated Security = True";
        SqlDataAdapter dataAdapter;
        DataTable dt;
    
        public ViewRoom()
        {
            InitializeComponent();

            connection = new SqlConnection(address);
            display();
            CreateRoom.roomCode = null;
            ViewRoom.roomCode = null;
            //roomCode.RoomCode = null;
            Room.RoomCode = null;

        }
        public void display()
        {
            connection.Open();
            dt = new DataTable();
            dataAdapter = new SqlDataAdapter("Select r.RoomName,rd.RoomID FROM ROOMDETAILS rd JOIN ROOM r ON r.RoomID = rd.RoomID WHERE rd.UserID = '"+email+"'", connection);
            dataAdapter.Fill(dt);

            guna2DataGridView1.DataSource = dt;
            connection.Close();

        }
        private void BtnHome_Click(object sender, EventArgs e)
        {
            Home home = new Home();
            this.Hide();    //Hide the Old Form
            home.ShowDialog();    //Show the New Form
            this.Close();    //Close the Old Form
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel1_Click(object sender, EventArgs e)
        {
            Home home = new Home();
            this.Hide();    //Hide the Old Form
            home.ShowDialog();    //Show the New Form
            this.Close();    //Close the Old Form
        }

        private void btnRoom_Click(object sender, EventArgs e)
        {

        }

        private void guna2DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            roomCode = guna2DataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            Room Room = new Room();
            this.Hide();    //Hide the Old Form
            Room.ShowDialog();    //Show the New Form
            this.Close();    //Close the Old Form
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {

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
