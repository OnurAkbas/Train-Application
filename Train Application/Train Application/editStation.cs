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
    public partial class editStation : MetroFramework.Forms.MetroForm
    {
        public editStation(string id) 
        {
            InitializeComponent();
   
            txtID.Text = "" + id;

            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = Util.Connection;
            cmd.CommandText = "select * from stationdb where StationID= ?id";
            cmd.Parameters.Add("?id", MySqlDbType.Int32).Value = txtID.Text;
            Util.Connection.Open();
            cmd.ExecuteNonQuery();

            try
            {
                MySqlDataReader myReader;
                myReader = cmd.ExecuteReader();
                while (myReader.Read())
                {
                    txtName.Text = myReader.GetString(1);
                    txtDescription.Text = myReader.GetString(2);
                }
                myReader.Close();
                Util.Connection.Close();
            }
            catch
            {
                MetroFramework.MetroMessageBox.Show(this, "Opss.. PROBLEM", "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
        }

        public StationList editmode;

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = Util.Connection;
            cmd.CommandText = "UPDATE stationdb SET stationName = ?name, stationDescription = ?description where StationID= ?id";
            cmd.Parameters.Add("?id", MySqlDbType.Int32).Value = txtID.Text;
            cmd.Parameters.Add("?name", MySqlDbType.String).Value = txtName.Text;
            cmd.Parameters.Add("?description", MySqlDbType.String).Value = txtDescription.Text;
            Util.Connection.Open();
            cmd.ExecuteNonQuery();
                MetroFramework.MetroMessageBox.Show(this, "You have Edited the Station", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Close();
                StationList Trainlist = new StationList();
                Trainlist.trainlist = this;
                Trainlist.Show();
            }
            catch
            {
            MetroFramework.MetroMessageBox.Show(this, "Opss.. PROBLEM", "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
        }
    }
}
