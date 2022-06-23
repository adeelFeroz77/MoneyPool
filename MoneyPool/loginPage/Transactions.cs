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
    public partial class Transactions : Form
    {
        SqlConnection connection;
        SqlCommand command;
        string address = "Data Source = DESKTOP-FVDPVI1\\SAMEER; Initial Catalog = MONEYPOOL; Integrated Security = True";
        SqlDataAdapter dataAdapter;
        DataTable dt;

        public Transactions()
        {
            InitializeComponent();
            connection = new SqlConnection(address);
            display();
        }
        public void display()
        {
            connection.Open();
            dt = new DataTable();
            dataAdapter = new SqlDataAdapter("Select cd.CommMonth AS 'NOT PAID MONTHS' , cd.CommYear AS 'NOT PAID YEAR' from RoomDetails rd CROSS JOIN COMMITTEEDETAILS cd LEFT JOIN TRANSACTIONS t on (t.UserID = rd.UserID and t.RoomID = rd.RoomID  and t.CurrentMonth = cd.CommMonth and t.CurrentYear = cd.CommYear) WHERE t.CurrentMonth is null and t.currentYear is null and rd.UserID = '"+Form1.email+"' and rd.RoomID = '"+Room.RoomCode+ "' and cd.roomID = rd.roomID", connection);
            dataAdapter.Fill(dt);
            guna2DataGridView1.DataSource = dt;
            connection.Close();

        }
        private void guna2CustomRadioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void guna2DataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            month.Text = guna2DataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            year.Text = guna2DataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            if(guna2CustomRadioButton1.Checked != true && guna2CustomRadioButton2.Checked != true)
            {
                MessageBox.Show("Select Payment Method");
                return;
            }
            try
            {
                connection.Open();
                command = new SqlCommand("INSERT INTO TRANSACTIONS VALUES('"+Form1.email+"','"+Room.RoomCode+"','"+month.Text+"','"+year.Text+"','PAID')", connection);
                command.ExecuteNonQuery();
                connection.Close(); 
                MessageBox.Show("Your amount is Paid!");
            }
            catch (Exception ex)
            {
                connection.Close();
                MessageBox.Show(ex.Message);
                return;
            }
            display();
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
