using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using service1;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;
using System.Collections.Generic;

namespace Train_Application
{
    public partial class ShortDistance : MetroFramework.Forms.MetroForm
    {
        public ShortDistance()
        {
            InitializeComponent();

            DataTable linkcombo = new DataTable("linkcombo");

            try
            {
                string query = "select LineName from linedb";

                using (SqlConnection sqlConn = new SqlConnection(""))
                {
                    using (adapter = new MySqlDataAdapter(query, Util.Connection))
                    {
                        adapter.Fill(linkcombo);
                    }
                }
                foreach (DataRow adapter in linkcombo.Rows)
                {
                    comboBox1.Items.Add(adapter[0].ToString());
                }
            }
            catch { }
            finally { Util.Connection.Close(); }

        }
        DataTable listconnection = new DataTable("listconnection");


        public DataTable dataTable = new DataTable();

        MySqlDataAdapter adapter;

        public AdminMain shortdistance;

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            listconnection.Clear();
            comboBox2.Items.Clear();
            comboBox3.Items.Clear();
            try
            {
                string query = "SELECT connectID, linedb.LineName, stationdb.stationName FROM((connectdb INNER JOIN linedb ON connectdb.lineID = linedb.LineID) INNER JOIN  stationdb ON connectdb.stationID = stationdb.StationID) WHERE linedb.LineName = @info";
                MySqlCommand cmdline = new MySqlCommand(query, Util.Connection);
                cmdline.Parameters.AddWithValue("@info", comboBox1.Text);

                using (adapter = new MySqlDataAdapter(cmdline))
                {
                    adapter.Fill(listconnection);
                }
                
                foreach (DataRow adapter in listconnection.Rows)
                {
                    comboBox2.Items.Add(adapter[2].ToString());
                }

            }
            catch { }
            finally { Util.Connection.Close(); }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = new DataTable();

                if (Util.Connection.State == ConnectionState.Closed) Util.Connection.Open();

                string query = "Select stationOne, stationTwo, distance FROM distancedb where lineName = @info";
                MySqlCommand cmdline = new MySqlCommand(query, Util.Connection);
                cmdline.Parameters.AddWithValue("@info", comboBox1.Text);


                MySqlDataAdapter sqlDa = new MySqlDataAdapter(cmdline);

                sqlDa.Fill(dt);

                if (dt.Rows.Count < 2)
                {
                MetroFramework.MetroMessageBox.Show(this, "Not Enough Stations To Apply Distance (Need 2 or More)", "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                }
                else
                {

                int row = dt.Rows.Count;

                List<string> station1 = new List<string>();
                List<string> station2 = new List<string>();
                List<string> distance = new List<string>();

                for (int count = 0; count < row; count++)
                {
                station1.Add(dt.Rows[count]["stationOne"].ToString());
                station2.Add(dt.Rows[count]["stationTwo"].ToString());
                distance.Add(dt.Rows[count]["distance"].ToString());
                }

                string from = comboBox2.Text;
                string to = comboBox3.Text;


                Class1 test = new Class1();
                
                int distancecalculator = test.ShortDistance(station1, station2, distance, from, to, row);

                    if (distancecalculator == 10000000)
                    {
                    textBox1.Text = "No Station Connected";
                    }
                    else
                    {
                    textBox1.Text = "" + distancecalculator;
                    }
                }
            }
            catch
            {
                
            }
            finally
            {
                Util.Connection.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
            AdminMain returntoadmin = new AdminMain();
            returntoadmin.shortdistanceform = this;
            returntoadmin.Show();
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox3.Items.Clear();
            foreach (DataRow adapter in listconnection.Select().Skip(comboBox2.SelectedIndex).Take(100))
                {
                    comboBox3.Items.Add(adapter[2].ToString());
                }
        }
    }
}
