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

namespace Train_Application
{
    public partial class LineConnected : MetroFramework.Forms.MetroForm
    {
        public LineConnected()
        {
            InitializeComponent();


            DataTable linkcombo = new DataTable("linkcombo");

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

        public AdminMain linesconnect;

        MySqlDataAdapter adapter;
        public DataTable dataTable = new DataTable();

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
            AdminMain linesconnect = new AdminMain();
            linesconnect.lineconencted = this;
            linesconnect.Show();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {

            if (comboBox1.Text == "Pick a Line To Preview")
            {
                MetroFramework.MetroMessageBox.Show(this, "Please Select A Line to preview.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return;
            }
            try
            { 
                 dataTable.Clear();
                if (Util.Connection.State == ConnectionState.Closed) Util.Connection.Open();

                    string query = "SELECT connectID, linedb.LineName, stationdb.stationName FROM((connectdb INNER JOIN linedb ON connectdb.lineID = linedb.LineID) INNER JOIN  stationdb ON connectdb.stationID = stationdb.StationID) WHERE linedb.LineName = @info";
                    MySqlCommand cmdline = new MySqlCommand(query, Util.Connection);
                    cmdline.Parameters.AddWithValue("@info", comboBox1.Text);
                    
                    adapter = new MySqlDataAdapter(cmdline);
                    adapter.FillSchema(dataTable, SchemaType.Source);
                    dataTable.Columns[0].DataType = typeof(string);
                    adapter.Fill(dataTable);

                    dgvList.DataSource = dataTable;

                    // Station ID
                    dgvList.Columns[0].Width = 70;
                    dgvList.Columns[0].HeaderCell.Value = "Connect ID";

                    // Station Name Section                  dgvList.Columns[1].Width = 90;
                    dgvList.Columns[1].HeaderCell.Value = "Line Name";

                    // Station Description
                    dgvList.Columns[2].Width = 100;
                    dgvList.Columns[2].HeaderCell.Value = "Station Name";

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

        private void button2_Click(object sender, EventArgs e)
        {
            if (dgvList.SelectedRows.Count == 0)
            {
                MetroFramework.MetroMessageBox.Show(this, "Select an Station to Disconnect!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return;
            }
            else
            {
                try
                {
                    MySqlCommand cmd = new MySqlCommand();
                    cmd.Connection = Util.Connection;

                    cmd.CommandText = "DELETE FROM connectDB WHERE connectID = ?id";
                    cmd.Parameters.Add("?id", MySqlDbType.VarChar).Value = dgvList.SelectedRows[0].Cells["connectID"].Value.ToString();

                    if (Util.Connection.State == ConnectionState.Closed) Util.Connection.Open();
                    int success = cmd.ExecuteNonQuery();
                    if (success == 0) // Error
                    {
                        MetroFramework.MetroMessageBox.Show(this, "Failed!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    }
                    else
                    {
                        MetroFramework.MetroMessageBox.Show(this, "You have deleted Station!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        dataTable.Clear();
                    }
                }
                catch
                {
                    MetroFramework.MetroMessageBox.Show(this, "Database Error!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    dataTable.Clear();
                }
                finally
                {
                    Util.Connection.Close();
                }
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void dgvList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
