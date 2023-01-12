using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using service1;

namespace Train_Application
{
    public partial class DistanceFormcs : MetroFramework.Forms.MetroForm
    {
        public DistanceFormcs()
        {
            InitializeComponent();

            DataTable linkcombo = new DataTable("linkcombo");

           

            this.textBox1.KeyPress += new KeyPressEventHandler(textBox1_KeyPress_1);

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
            catch {}
            finally { Util.Connection.Close(); }
        }

        public int row = 0;

        MySqlDataAdapter adapter;
        public DataTable dataTable = new DataTable();
        public AdminMain connection;

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {
            int value;
            Class1 test = new Class1();

            

            if (Int32.TryParse(textBox1.Text, out value))
            {
                string over50 = test.checkvalue(value);

                if (over50 == "red")
                {
                    textBox1.BackColor = Color.Red;
                    button1.Enabled = false;
                }
                else
                {
                    textBox1.BackColor = Color.White;
                    button1.Enabled = true;
                }
            }
        }

        private void textBox1_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            e.Handled = (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
            (e.KeyChar != '.'));
        }

        public void updatetable()
        {
            try
            {
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = Util.Connection;
            cmd.CommandText = "UPDATE distancedb SET distance = ?distance where stationOne = ?s1 AND stationTwo = ?s2";
            cmd.Parameters.Add("?s1", MySqlDbType.VarChar).Value = label3.Text;
            cmd.Parameters.Add("?s2", MySqlDbType.VarChar).Value = label4.Text;
            cmd.Parameters.Add("?distance", MySqlDbType.Int32).Value = textBox1.Text;
            Util.Connection.Open();
            cmd.ExecuteNonQuery();
            MetroFramework.MetroMessageBox.Show(this, "You have Edited the Line", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            dataTable.Clear();
            loadtable();
            }
            catch
            {
                MetroFramework.MetroMessageBox.Show(this, "Opss.. PROBLEM (UPDATE ERROR)", "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
            finally
            {
                Util.Connection.Close();
            }
        }

        public void loadtable()
        {
            try
            {
                dataTable.Clear();
                if (Util.Connection.State == ConnectionState.Closed) Util.Connection.Open();

                string query = "Select stationOne, stationTwo, distance FROM distancedb where lineName = @info";
                MySqlCommand cmdline = new MySqlCommand(query, Util.Connection);
                cmdline.Parameters.AddWithValue("@info", comboBox1.Text);

                adapter = new MySqlDataAdapter(cmdline);
                adapter.FillSchema(dataTable, SchemaType.Source);
                dataTable.Columns[0].DataType = typeof(string);
                adapter.Fill(dataTable);

                dgvList.DataSource = dataTable;

                // Station ID
                dgvList.Columns[0].Width = 80;
                dgvList.Columns[0].HeaderCell.Value = "Station From";

                // Station Name Section                  
                dgvList.Columns[1].Width = 80;
                dgvList.Columns[1].HeaderCell.Value = "Station To";

                // Station Description
                dgvList.Columns[2].Width = 60;
                dgvList.Columns[2].HeaderCell.Value = "Distance";

            }
            catch
            {
                //show database error
            }
            finally
            {
                Util.Connection.Close();
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.Text == "Pick a Line To Preview")
            {
                button1.Enabled = false;
                button2.Enabled = false;
                button3.Enabled = false;
                MetroFramework.MetroMessageBox.Show(this, "Please Select A Line to preview.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return;
            }
            row = 0;
            loadtable();
            nexttable(row);
        }

        public void nexttable(int row)
        {
            try
            {
                DataTable dt = new DataTable();

                if (Util.Connection.State == ConnectionState.Closed) Util.Connection.Open();

                string query = "SELECT connectID, linedb.LineName, stationdb.stationName FROM((connectdb INNER JOIN linedb ON connectdb.lineID = linedb.LineID) INNER JOIN  stationdb ON connectdb.stationID = stationdb.StationID) WHERE linedb.LineName = @info;";
                MySqlCommand cmdline = new MySqlCommand(query, Util.Connection);
                cmdline.Parameters.AddWithValue("@info", comboBox1.Text);
                cmdline.Parameters.AddWithValue("@count", row);

                MySqlDataAdapter sqlDa = new MySqlDataAdapter(cmdline);
                sqlDa.Fill(dt);
                label2.Text = "" + dt.Rows.Count;

                if (dt.Rows.Count < 2)
                {
                button1.Enabled = false;
                button2.Enabled = false;
                button3.Enabled = false;
                MetroFramework.MetroMessageBox.Show(this, "Not Enough Stations To Apply Distance (Need 2 or More)", "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                }
                else
                {
                label3.Text = dt.Rows[row]["stationName"].ToString();
                label4.Text = dt.Rows[row+1]["stationName"].ToString();
                button1.Enabled = true;
                button2.Enabled = true;
                button3.Enabled = true;
                }
            }
            catch
            {
                refresh(5);
            }
            finally
            {
                Util.Connection.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int value;
            if (Int32.TryParse(textBox1.Text, out value))

            if (value > 50 || value == 0)
            {
                MetroFramework.MetroMessageBox.Show(this, "The distance must be between 1 - 50", "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return;
            }

            try
            {
                if (Util.Connection.State == ConnectionState.Closed) Util.Connection.Open();
                string command = "SELECT * FROM distancedb WHERE stationOne = @s1 AND stationTwo = @s2 AND lineName = @line";
                MySqlCommand cmdline = new MySqlCommand(command, Util.Connection);
                cmdline.Parameters.AddWithValue("@s1", label3.Text);
                cmdline.Parameters.AddWithValue("@s2", label4.Text);
                cmdline.Parameters.AddWithValue("@line", comboBox1.Text);
                MySqlDataReader drr;

                drr = cmdline.ExecuteReader();
                if (!drr.HasRows)
                {

                }
                else
                {
                if (MetroFramework.MetroMessageBox.Show(this, "The Distance Already Exsists, would you like to Change?", "Error", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                Util.Connection.Close();
                updatetable();
                }
                else
                {
                return;
                }
                return;
                }
                drr.Read();
            }
            catch
            {
                MetroFramework.MetroMessageBox.Show(this, "Error", "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return;
            }
            finally
            {
                Util.Connection.Close();
            }

            try
            {
            MySqlCommand cmdd = new MySqlCommand();
            cmdd.Connection = Util.Connection;

            cmdd.CommandText = "INSERT INTO distancedb VALUES(null,?s1,?s2,?distance,?lineName)";
            cmdd.Parameters.Add("?s1", MySqlDbType.VarChar).Value = label3.Text;
            cmdd.Parameters.Add("?s2", MySqlDbType.VarChar).Value = label4.Text;
            cmdd.Parameters.Add("?distance", MySqlDbType.VarChar).Value = textBox1.Text;
            cmdd.Parameters.Add("?lineName", MySqlDbType.VarChar).Value = comboBox1.Text;

            if (Util.Connection.State == ConnectionState.Closed) Util.Connection.Open();
            int success = cmdd.ExecuteNonQuery();
            if (success == 0) // Error
            {
            MetroFramework.MetroMessageBox.Show(this, "Their was an problem!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            return;
            }
            else
            {
            dataTable.Clear();
            MetroFramework.MetroMessageBox.Show(this, "You have Succesfully applied the distance!!", "Succcess ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            loadtable();
            }
            }
            catch
            {
            MetroFramework.MetroMessageBox.Show(this, "Connection Error", "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
            finally
            {
            Util.Connection.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            refresh(0);
        }

        public void refresh(int istrue)
        {
            if (istrue == 5)
            {
            row = row - 1;
            }
            else
            {
            row++;
            }
            nexttable(row);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (row <= 0){
            
            }
            else
            {
            row--;
            }
            nexttable(row);
        }
    }
}
