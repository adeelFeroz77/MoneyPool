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
    public partial class Room : Form
    {
        string email;
        string date, month, year;
        public static string RoomCode;
        SqlConnection connection;
        SqlCommand command;
        string address = "Data Source = DESKTOP-FVDPVI1\\SAMEER; Initial Catalog = MONEYPOOL; Integrated Security = True";
        SqlDataAdapter dataAdapter;
        DataTable dt;


        public Room()
        {
            InitializeComponent();
            connection = new SqlConnection(address);

            DateTime di = DateTime.Now;
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

            date = di.ToString().Substring(0, 2);
            month = dic[di.ToString().Substring(3, 2)];
            year = di.ToString().Substring(6, 4);

            email = Form1.email;
            if (CreateRoom.roomCode != null)
            {
                RoomCode = CreateRoom.roomCode;
            }
            else if (roomCode.RoomCode != null) {
                RoomCode = roomCode.RoomCode;
            }
            else
            {
                RoomCode = ViewRoom.roomCode;
            }
            string adminID;
            try
            {

                connection.Open();
                command = new SqlCommand("Select UserID from ROOM where RoomID = '" + RoomCode + "'", connection);
                adminID = (string)command.ExecuteScalar();

                if (adminID == email)
                {
                    manageBTN.Enabled = true;
                }else
                {
                    manageBTN.Hide();
                }
                // command.ExecuteNonQuery();

                string currentMonth, currentYear;
                command = new SqlCommand("SELECT CommMonth FROM COMMITTEEDETAILS WHERE UserID = '" + email + "' and RoomID = '"+RoomCode+"'", connection);
                currentMonth = (string)command.ExecuteScalar();
                //MessageBox.Show(currentMonth);
                if (currentMonth == null)
                {
                    label1.Text = "Committee Not Started Yet";
                    label2.Text = "";
                    label3.Text = "Committee Not Started Yet";
                }
                else
                {
                    string userMail;
                    command = new SqlCommand("Select UserID from COMMITTEEDETAILS Where CommMonth = '" + month + "' and commYear = '" + year + "' and  RoomID = '" + RoomCode + "'", connection);
                    userMail = (string)command.ExecuteScalar();
                    command = new SqlCommand("SELECT CommYear FROM COMMITTEEDETAILS WHERE UserID = '" + email + "' and RoomID = '" + RoomCode + "'", connection);
                    currentYear = (string)command.ExecuteScalar();
                    label2.Text = currentMonth + ", " + currentYear;
                    label3.Text = userMail;
                    string count;
                    command = new SqlCommand("Select COUNT(*) from RoomDetails rd CROSS JOIN COMMITTEEDETAILS cd LEFT JOIN TRANSACTIONS t on(t.UserID = rd.UserID and t.RoomID = rd.RoomID  and t.CurrentMonth = cd.CommMonth and t.CurrentYear = cd.CommYear) WHERE t.CurrentMonth is null and t.currentYear is null and cd.CommMonth = FORMAT(GETDATE(), 'MMM')", connection);
                    count = (string)command.ExecuteScalar().ToString();
                    if(count == "0")
                    {
                        string check;
                        command = new SqlCommand("Select UserID from TRANSACTIONS where userID = '"+userMail+"' and TransactionType = '"+"RECEIVED"+ "' and  RoomID = '" + RoomCode + "'", connection);
                        check = (string)command.ExecuteScalar();
                        if (check == null)
                        {
                            command = new SqlCommand("INSERT INTO TRANSACTIONS VALUES('" + userMail + "','" + RoomCode + "','" + month + "','" + year + "','RECEIVED')", connection);
                            command.ExecuteNonQuery();
                        }
                    }
                }
                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                connection.Close();

            }
            display();
            
        }
        public void display()
        {
            connection.Open();
            dt = new DataTable();
            dataAdapter = new SqlDataAdapter("Select rd.UserID,cd.CommMonth AS 'NOT PAID MONTHS' , cd.CommYear AS 'NOT PAID YEAR' from RoomDetails rd CROSS JOIN COMMITTEEDETAILS cd LEFT JOIN TRANSACTIONS t on (t.UserID = rd.UserID and t.RoomID = rd.RoomID  and t.CurrentMonth = cd.CommMonth and t.CurrentYear = cd.CommYear) WHERE t.CurrentMonth is null and t.currentYear is null and rd.UserID = '"+email+"' and rd.RoomID = '"+RoomCode+ "' and cd.roomID = rd.roomID", connection);
            dataAdapter.Fill(dt);
            guna2DataGridView1.DataSource = dt;
            dt = new DataTable();
            dataAdapter = new SqlDataAdapter("SELECT UserID,CurrentMonth,CurrentYear from TRANSACTIONS where CurrentMonth = '"+month+"' and CurrentYear = '"+year+"' and TransactionType = '"+"PAID"+"' and roomID = '"+RoomCode+"'", connection);
            dataAdapter.Fill(dt);
            guna2DataGridView2.DataSource = dt;
            connection.Close();

        }

        private void BtnHome_Click(object sender, EventArgs e)
        {
            People people = new People();
            this.Hide();    //Hide the Old Form
            people.ShowDialog();    //Show the New Form
            this.Close();    //Close the Old Form
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {

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

        private void backImgBTn_Click(object sender, EventArgs e)
        {
            ViewRoom view = new ViewRoom();
            this.Hide();    //Hide the Old Form
            view.ShowDialog();    //Show the New Form
            this.Close();    //Close the Old Form
        }

        private void manageBTN_Click(object sender, EventArgs e)
        {
            Manage mng = new Manage();
            this.Hide();    //Hide the Old Form
            mng.ShowDialog();    //Show the New Form
            this.Close();    //Close the Old Form
        }

        private void guna2Button1_Click_1(object sender, EventArgs e)
        {
            Transactions tr = new Transactions();
            this.Hide();    //Hide the Old Form
            tr.ShowDialog();    //Show the New Form
            this.Close();    //Close the Old Form
        }


    }
}
