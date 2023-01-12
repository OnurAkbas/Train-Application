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
    public partial class LineList : MetroFramework.Forms.MetroForm
    {
        public LineList()
        {
            InitializeComponent();
            loadData();
        }

        MySqlDataAdapter adapter;

        public AdminMain lineListForm;
        public addLine Linelistform;
        public editLine linelist;

        public DataTable dataTable = new DataTable();

        public void loadData()
        {
            try
            {
                dataTable.Clear();
                string query = "select * from lineDB";

                if (Util.Connection.State == ConnectionState.Closed) Util.Connection.Open();
                adapter = new MySqlDataAdapter(query, Util.Connection);

                adapter.FillSchema(dataTable, SchemaType.Source);
                dataTable.Columns[0].DataType = typeof(string);
                adapter.Fill(dataTable);

                dgvList.DataSource = dataTable;

                // Station ID
                dgvList.Columns[0].Width = 100;
                dgvList.Columns[0].HeaderCell.Value = "Unique Line ID";

                // Station Name Section
                dgvList.Columns[1].Width = 120;
                dgvList.Columns[1].HeaderCell.Value = "Line Name";

                // Station Description
                dgvList.Columns[2].Width = 100;
                dgvList.Columns[2].HeaderCell.Value = "Line Color";

                // Station Description
                dgvList.Columns[3].Width = 150;
                dgvList.Columns[3].HeaderCell.Value = "Line Description";
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

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
            AdminMain main = new AdminMain();
            main.linelist = this;
            main.Show();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            this.Visible = true;
            addLine Train = new addLine();
            Train.AddLines = this;
            Train.Show();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvList.SelectedRows.Count == 0)
            {
                MetroFramework.MetroMessageBox.Show(this, "Select an Station to delete!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return;
            }
            else
            {
                try
                {
                    MySqlCommand cmd = new MySqlCommand();
                    cmd.Connection = Util.Connection;

                    cmd.CommandText = "DELETE FROM linedb WHERE LineID = ?id";
                    cmd.Parameters.Add("?id", MySqlDbType.VarChar).Value = dgvList.SelectedRows[0].Cells["LineID"].Value.ToString();

                    if (Util.Connection.State == ConnectionState.Closed) Util.Connection.Open();
                    int success = cmd.ExecuteNonQuery();
                    if (success == 0) // Error
                    {
                        MetroFramework.MetroMessageBox.Show(this, "Failed!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    }
                    else
                    {
                        MetroFramework.MetroMessageBox.Show(this, "You have deleted Line!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                MetroFramework.MetroMessageBox.Show(this, "Please Select an Line from the list (Highlight)!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return;
            }
            else
            {
                editLine editstation = new editLine(dgvList.SelectedRows[0].Cells[0].Value.ToString());
                editstation.editmode = this;
                editstation.Show();
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            dataTable.Clear();
            loadData();
        }
    }
}

