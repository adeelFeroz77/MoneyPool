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
    public partial class Home : Form
    {
        SqlConnection connection;
        SqlCommand command;
        string address = "Data Source = DESKTOP-FVDPVI1\\SAMEER; Initial Catalog = MONEYPOOL; Integrated Security = True";
     

        public Home()
        {
            InitializeComponent();
            connection = new SqlConnection(address);
            DateTime dt = DateTime.Now;
            //Console.WriteLine(dt);
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("01", "Jan");
            dic.Add("02", "Feb");
            dic.Add("03", "Mar");
            dic.Add("04", "Apr");
            dic.Add("05", "May");
            dic.Add("06", "Jun");
            dic.Add("07", "Jul");   
            dic.Add("08", "Aug");
            dic.Add("09", "Sep");
            dic.Add("10", "Oct");
            dic.Add("11", "Nov");
            dic.Add("12", "Dec");

            string date = dt.ToString().Substring(0, 2);
            string month = dic[dt.ToString().Substring(3, 2)];
            string year = dt.ToString().Substring(6, 4);

            label1.Text = date;
            label2.Text = month;
            label3.Text = year;

            CreateRoom.roomCode = null;
            ViewRoom.roomCode = null;
            roomCode.RoomCode = null;
            Room.RoomCode = null;

            string name;
            try
            {
                connection.Open();
                command = new SqlCommand("Select UserFirstname from UserInfo where UserID = '" + Form1.email.ToString() + "'",connection);
                name = (string)command.ExecuteScalar();
                label4.Text = name;
                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                connection.Close();
            }
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            CreateRoom createRoom = new CreateRoom();
            this.Hide();    //Hide the Old Form
            createRoom.ShowDialog();    //Show the New Form
            this.Close();    //Close the Old Form
        }

        private void btnRoom_Click(object sender, EventArgs e)
        {
            ViewRoom viewRoom = new ViewRoom();
            this.Hide();    //Hide the Old Form
            viewRoom.ShowDialog();    //Show the New Form
            this.Close();    //Close the Old Form
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            roomCode joinroom = new roomCode();
            this.Hide();    //Hide the Old Form
            joinroom.ShowDialog();    //Show the New Form
            this.Close();    //Close the Old Form
        }

        private void panel1_DoubleClick(object sender, EventArgs e)
        {

        }

        private void panel1_Click(object sender, EventArgs e)
        {
            Home home = new Home();
            this.Hide();    //Hide the Old Form
            home.ShowDialog();    //Show the New Form
            this.Close();    //Close the Old Form
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            Application.Restart();
        }
    }
}
