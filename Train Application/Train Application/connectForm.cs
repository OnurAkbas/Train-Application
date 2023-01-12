using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Train_Application
{
    public partial class connectForm : MetroFramework.Forms.MetroForm
    {
        public connectForm()
        {
            InitializeComponent();
            loadData();
            loadData2();
        }

        MySqlDataAdapter adapter;

        public AdminMain ConnectForm;

        int RowCount = 0;

        public DataTable dataTable = new DataTable();
        public DataTable dataTable2 = new DataTable();

        public void loadData()
        {
            try
            {
                dataTable.Clear();
                string query = "SELECT stationID, stationName FROM stationdb";

                if (Util.Connection.State == ConnectionState.Closed) Util.Connection.Open();
                adapter = new MySqlDataAdapter(query, Util.Connection);

                adapter.FillSchema(dataTable, SchemaType.Source);
                dataTable.Columns[0].DataType = typeof(string);
                adapter.Fill(dataTable);

                dgvList.DataSource = dataTable;

                // Station ID
                dgvList.Columns[0].Width = 120;
                dgvList.Columns[0].HeaderCell.Value = "Unique Station ID";

                // Station Name Section
                dgvList.Columns[1].Width = 120;
                dgvList.Columns[1].HeaderCell.Value = "Station Name";

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

        public void loadData2()
        {
            try
            {
                dataTable2.Clear();
                string query = "select LineID, LineName from lineDB";

                if (Util.Connection.State == ConnectionState.Closed) Util.Connection.Open();
                adapter = new MySqlDataAdapter(query, Util.Connection);

                adapter.FillSchema(dataTable2, SchemaType.Source);
                dataTable2.Columns[0].DataType = typeof(string);
                adapter.Fill(dataTable2);

                metroGrid1.DataSource = dataTable2;

                // Station ID
                metroGrid1.Columns[0].Width = 120;
                metroGrid1.Columns[0].HeaderCell.Value = "Unique Line ID";

                // Station Name Section
                metroGrid1.Columns[1].Width = 120;
                metroGrid1.Columns[1].HeaderCell.Value = "Line Name";

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

        private void btnTrainList_Click(object sender, EventArgs e)
        {
            if (dgvList.SelectedRows.Count == 0 || metroGrid1.SelectedRows.Count == 0)
            {
                MetroFramework.MetroMessageBox.Show(this, "Select an Station and line to Connect!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return;
            }
            else
            {
                try
                {
                    if (Util.Connection.State == ConnectionState.Closed) Util.Connection.Open();
                    string command = "SELECT * FROM connectdb WHERE lineID = @line AND stationID = @station";
                    MySqlCommand cmdline = new MySqlCommand(command, Util.Connection);
                    cmdline.Parameters.AddWithValue("@line", metroGrid1.SelectedRows[0].Cells[0].Value.ToString());
                    cmdline.Parameters.AddWithValue("@station", dgvList.SelectedRows[0].Cells[0].Value.ToString());
                    MySqlDataReader drr;

                    drr = cmdline.ExecuteReader();
                    if (!drr.HasRows)
                    {
                        
                    }
                    else
                    {
                        MetroFramework.MetroMessageBox.Show(this, "The Station And the Line Already Connected", "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
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
                    if (Util.Connection.State == ConnectionState.Closed) Util.Connection.Open();
                    string command = "SELECT stationID FROM connectDB WHERE stationID = @id";
                    MySqlCommand cmdline = new MySqlCommand(command, Util.Connection);
                    cmdline.Parameters.AddWithValue("@id", dgvList.SelectedRows[0].Cells[0].Value.ToString());
                    MySqlDataReader dr;
                    dr = cmdline.ExecuteReader();

                    using (dr)
                    {
                        while (dr.Read())
                        {
                            RowCount++;
                        }
                        dr.Close();
                    }
                    dr.Close();

                    if (RowCount >= 2)
                    {
                        MetroFramework.MetroMessageBox.Show(this, "The Station Is Already Connected to more than 2 Stations", "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                        return;
                    }
                    dr.Read();
                }
                catch
                { }
                finally
                {
                    RowCount = 0;
                    Util.Connection.Close();
                }

                try
                    {
                        MySqlCommand cmdd = new MySqlCommand();
                        cmdd.Connection = Util.Connection;

                        cmdd.CommandText = "INSERT INTO connectdb VALUES(null,?lineID,?stationID)";
                        cmdd.Parameters.Add("?lineID", MySqlDbType.VarChar).Value = metroGrid1.SelectedRows[0].Cells[0].Value.ToString();
                        cmdd.Parameters.Add("?stationID", MySqlDbType.VarChar).Value = dgvList.SelectedRows[0].Cells[0].Value.ToString();

                        if (Util.Connection.State == ConnectionState.Closed) Util.Connection.Open();
                        int success = cmdd.ExecuteNonQuery();
                        if (success == 0) // Error
                        {
                            MetroFramework.MetroMessageBox.Show(this, "Their was an problem Connecting the Station to Line!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                        return;
                        }
                        else
                        {
                            MetroFramework.MetroMessageBox.Show(this, "You have Succesfully added made a new Connection!", "Succcess ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    catch
                    {
                        MetroFramework.MetroMessageBox.Show(this, "Could Not Match The Connection Between Line and Station.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    }
                    finally
                    {
                        Util.Connection.Close();
                    }
                }
            }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
            AdminMain linesconnect = new AdminMain();
            linesconnect.connectform = this;
            linesconnect.Show();
        }
    }
    }
