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
    public partial class StationConnected : MetroFramework.Forms.MetroForm
    {
        public StationConnected()
        {
            InitializeComponent();

            DataTable linkcombo = new DataTable("linkcombo");

            string query = "select stationName from stationdb";

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

        MySqlDataAdapter adapter;
        public DataTable dataTable = new DataTable();

        public AdminMain form12345;

        private void StationConnected_Load(object sender, EventArgs e)
        {

        }

        private void btnTrainList_Click(object sender, EventArgs e)
        {
            Close();
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

                string query = "SELECT connectID, linedb.LineName, stationdb.stationName FROM((connectdb INNER JOIN linedb ON connectdb.lineID = linedb.LineID) INNER JOIN  stationdb ON connectdb.stationID = stationdb.StationID) WHERE stationdb.stationName = @info";
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

                // Station Name Section                  
                dgvList.Columns[1].Width = 90;
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

        private void dgvList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
