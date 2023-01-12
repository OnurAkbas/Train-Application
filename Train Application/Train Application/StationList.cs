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
    public partial class StationList : MetroFramework.Forms.MetroForm
    {
        public StationList()
        {
            InitializeComponent();
            loadData();
        }

        MySqlDataAdapter adapter;
        public DataTable dataTable = new DataTable();

        public AdminMain TrainListForm;
        public addStation Trainlistform;
        public editStation trainlist;



        public void loadData()
        {
            try
            {
                dataTable.Clear();
                string query = "select * from stationDB";

                if (Util.Connection.State == ConnectionState.Closed) Util.Connection.Open();
                adapter = new MySqlDataAdapter(query, Util.Connection);

                adapter.FillSchema(dataTable, SchemaType.Source);
                dataTable.Columns[0].DataType = typeof(string);
                adapter.Fill(dataTable);
               
                dgvList.DataSource = dataTable;

                // Station ID
                dgvList.Columns[0].Width = 130;
                dgvList.Columns[0].HeaderCell.Value = "Unique Station ID";

                // Station Name Section
                dgvList.Columns[1].Width = 150;
                dgvList.Columns[1].HeaderCell.Value = "Station Name";

                // Station Description
                dgvList.Columns[2].Width = 150;
                dgvList.Columns[2].HeaderCell.Value = "Station Description";
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

        private void btnAdd_Click(object sender, EventArgs e)
        {
            this.Visible = true;
            addStation Train= new addStation();
            Train.AddTrains = this;
            Train.Show();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
            AdminMain main = new AdminMain();
            main.stationlist = this;
            main.Show();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            dataTable.Clear();
            loadData();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvList.SelectedRows.Count == 0)
            {
                MetroFramework.MetroMessageBox.Show(this, "Select an Station to delete!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return;
            }else
            {

                try
                {
                    MySqlCommand cmd = new MySqlCommand();
                    cmd.Connection = Util.Connection;

                    cmd.CommandText = "DELETE FROM connectDB WHERE stationID = ?id";
                    cmd.Parameters.Add("?id", MySqlDbType.VarChar).Value = dgvList.SelectedRows[0].Cells["stationID"].Value.ToString();

                    if (Util.Connection.State == ConnectionState.Closed) Util.Connection.Open();
                    int success = cmd.ExecuteNonQuery();
                    if (success == 0) // Error
                    {
                    }
                    else
                    {
                        MetroFramework.MetroMessageBox.Show(this, "You have disconnected the station from its line!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

                try
                    {
                    MySqlCommand cmd = new MySqlCommand();
                    cmd.Connection = Util.Connection;

                    cmd.CommandText = "DELETE FROM stationdb WHERE StationID = ?id";
                    cmd.Parameters.Add("?id", MySqlDbType.VarChar).Value = dgvList.SelectedRows[0].Cells["StationID"].Value.ToString();

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
                        loadData();
                    }
                }
                catch
                {
                    MetroFramework.MetroMessageBox.Show(this, "Database Error!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    dataTable.Clear();
                    loadData();
                }
                finally
                {
                    Util.Connection.Close();
                }
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dgvList.SelectedRows.Count == 0)
            {
                MetroFramework.MetroMessageBox.Show(this, "Please Select an Station from the list (Highlight)!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return;
            }
            else
            {
                editStation editstation = new editStation(dgvList.SelectedRows[0].Cells[0].Value.ToString());
                editstation.editmode = this;
                editstation.Show();
            }
        }
    }
    }


