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
    public partial class Manage : Form
    {
        public static string roomcode;
        string userCount;
        SqlConnection connection;
        SqlCommand command;
        string address = "Data Source = DESKTOP-FVDPVI1\\SAMEER; Initial Catalog = MONEYPOOL; Integrated Security = True";
        SqlDataAdapter dataAdapter;
        public DataTable dt;
        public Manage()
        {
            InitializeComponent();
            roomcode = Room.RoomCode;
            connection = new SqlConnection(address);
            display();
            count();
        }

        public void display()
        {
            connection.Open();
            dt = new DataTable();
            dataAdapter = new SqlDataAdapter("SELECT r.UserID,u.UserFirstName,u.UserLastName FROM ROOMDETAILS AS r Join UserInfo AS u ON r.UserID = u.UserID WHERE r.RoomID = '" + roomcode + "'", connection);
            dataAdapter.Fill(dt);
            dataGridView1.DataSource = dt;
            connection.Close();
        }

        public void count()
        {
            try
            {
                connection.Open();
                roomLabel.Text = roomcode;

                command = new SqlCommand("Select Count(UserID) from ROOMDETAILS where RoomID = '" + roomcode + "'", connection);
                userCount = (string)command.ExecuteScalar().ToString();
                connection.Close();
                label2.Text = userCount;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                connection.Close();
            }
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            People people = new People();
            this.Hide();    //Hide the Old Form
            people.ShowDialog();    //Show the New Form
            this.Close();    //Close the Old Form
        }

        private void BtnHome_Click(object sender, EventArgs e)
        {

            Room Room = new Room();
            this.Hide();    //Hide the Old Form
            Room.ShowDialog();    //Show the New Form
            this.Close();    //Close the Old Form
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            string roomCapacity="";
            string check = null;
            try
            {
                connection.Open();
                command = new SqlCommand("Select RoomCapacity from Room where RoomID = '" + Room.RoomCode + "'", connection);
                roomCapacity = (string)command.ExecuteScalar().ToString();
                command = new SqlCommand("Select UserID from CommitteeDetails where RoomID = '" + Room.RoomCode + "' and UserID = '" + Form1.email + "'", connection);
                check = (string)command.ExecuteScalar();
                MessageBox.Show("COMMITEE STARTED");
                connection.Close();
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                connection.Close();
            }
            if (check != null)
            {
                MessageBox.Show("COMMITEE ALREADY STARTED!");
                return;
            }

            if(roomCapacity != userCount || roomCapacity=="")
            {
                MessageBox.Show("Can't Proceed Until the Room is Full");
                return;
            }

            DateTime di = DateTime.Now;
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

            string date = di.ToString().Substring(0, 2);
            string monthNum = di.ToString().Substring(3, 2);
            string month = dic[monthNum];
            string year = di.ToString().Substring(6, 4);

            List<string> mons = new List<string>();
            for(int i= int.Parse(monthNum); i < int.Parse(monthNum) + int.Parse(userCount); i++)
            {
                string currentYear = year;
                int currentMonth = i;
                while (currentMonth > 12)
                {
                    currentYear = (int.Parse(currentYear)+1).ToString();
                    currentMonth = currentMonth - 12;
                }
                string finalMonth;
                if (currentMonth.ToString().Length == 1)
                {
                    finalMonth = dic["0" + (currentMonth.ToString())];
                }
                else
                {
                    finalMonth = dic[(currentMonth.ToString())];
                }
                
                mons.Add(finalMonth + "," + currentYear);
                
            }

            List<String> UserIDs = new List<string>();
            People p = new People();

            for (int i = 0; i < p.dt.Rows.Count; i++)
            {
                UserIDs.Add(p.dt.Rows[i][0].ToString());
            }
            var random = new Random();
            for (int i = 0; i < UserIDs.Count; i++)
            {
                string currentUser = UserIDs[i];
                int index = random.Next(mons.Count);
                string monsDetail = mons[index];
                mons.RemoveAt(index);
                string [] vals = monsDetail.Split(',');
                try
                {
                    connection.Open();
                    command = new SqlCommand("INSERT INTO COMMITTEEDETAILS VALUES('"+currentUser+"','"+Room.RoomCode+"','"+vals[0]+"','"+vals[1]+"')", connection);
                    command.ExecuteNonQuery();
                    connection.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    connection.Close();
                }
            }

        }

        private void backImgBTn_Click(object sender, EventArgs e)
        {
            Room room = new Room();
            this.Hide();    //Hide the Old Form
            room.ShowDialog();    //Show the New Form
            this.Close();    //Close the Old Form
        }

        private void dataGridView1_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox1.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(textBox1.Text == "")
            {
                MessageBox.Show("TextBox can't be empty!");
                return;
            }
            string outs = "";

            try
            {
                connection.Open();
                command = new SqlCommand("Select UserID from COMMITTEEDETAILS where RoomID = '" + roomcode + "' and UserID =  '" + Form1.email + "'", connection);
                outs = (string)command.ExecuteScalar();
                //MessageBox.Show(outs);
                if(outs != null)
                {
                    MessageBox.Show("Committe for this room are generated!");
                    connection.Close();
                    return;
                }

                command = new SqlCommand("Select UserID from UserINfo where UserID = '"+textBox1.Text+"'", connection);
                outs = (string)command.ExecuteScalar();
                if(outs == null)
                {
                    MessageBox.Show("User not registered!");
                    connection.Close();
                    return;
                }

                command = new SqlCommand("Select UserID from ROOMDETAILS where UserID = '"+textBox1.Text+"' and ROOMID = '"+roomcode+"'", connection);
                outs = (string)command.ExecuteScalar();
                if (outs != null)
                {
                    MessageBox.Show("User already in room!");
                    connection.Close();
                    return;
                }
                command = new SqlCommand("Select RoomCapacity from Room where RoomID = '" + roomcode + "'", connection);
                string roomCapacity = (string)command.ExecuteScalar().ToString();
                command = new SqlCommand("Select Count(UserID) from ROOMDETAILS where RoomID = '" + roomcode + "'", connection);
                userCount = (string)command.ExecuteScalar().ToString();

                if (userCount == roomCapacity)
                {
                    MessageBox.Show("Room Is Full");
                    connection.Close();
                    return;
                }

                command = new SqlCommand("INSERT INTO ROOMDETAILS VALUES('" + textBox1.Text + "','" + roomcode + "','" + Form1.email + "')", connection);
                command.ExecuteNonQuery();
                MessageBox.Show("User Added Successfully!");
                connection.Close();
                display();
                count();
                
            }catch(Exception ex)
            {
                connection.Close();
                MessageBox.Show(ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                MessageBox.Show("TextBox can't be empty!");
                return;
            }
            string outs = "";

            try
            {
                connection.Open();
                command = new SqlCommand("Select UserID from COMMITTEEDETAILS where RoomID = '" + roomcode + "' and UserID =  '" + Form1.email + "'", connection);
                outs = (string)command.ExecuteScalar();
                //MessageBox.Show(outs);
                if (outs != null)
                {
                    MessageBox.Show("Committe for this room are generated!");
                    connection.Close();
                    return;
                }

                command = new SqlCommand("Select UserID from ROOMDETAILS where UserID = '" + textBox1.Text + "' and ROOMID = '" + roomcode + "'", connection);
                outs = (string)command.ExecuteScalar();
                if (outs == null)
                {
                    MessageBox.Show("Entered User is not in this room");
                    connection.Close();
                    return;
                }
                if( textBox1.Text == Form1.email)
                {
                    MessageBox.Show("You can't remove yourself");
                    connection.Close();
                    return;
                }


                command = new SqlCommand("Delete from ROOMDETAILS where UserID = '"+textBox1.Text+"' and RoomID = '"+roomcode+"'", connection);
                command.ExecuteNonQuery();
                MessageBox.Show("User Deleted Successfully!");
                connection.Close();
                display();
                count();

            }
            catch (Exception ex)
            {
                connection.Close();
                MessageBox.Show(ex.Message);
            }
        }
    }
}
