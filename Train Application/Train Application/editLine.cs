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
    public partial class editLine : MetroFramework.Forms.MetroForm
    {
        public editLine(String id)
        {
            InitializeComponent();

            txtID.Text = "" + id;

            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = Util.Connection;
            cmd.CommandText = "select * from linedb where LineID= ?id";
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
                    if(myReader.GetString(2) == "")
                    {

                    }
                    else {
                        comboBox1.Text = myReader.GetString(2);
                    }
                    txtDescription.Text = myReader.GetString(3);
                }
                myReader.Close();
                Util.Connection.Close();
            }
            catch
            {
                MetroFramework.MetroMessageBox.Show(this, "Opss.. PROBLEM", "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
            finally
            {
                Util.Connection.Close();
            }
        }

        public LineList editmode;

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = Util.Connection;
                cmd.CommandText = "UPDATE linedb SET LineName = ?name,Color = ?color, LineDescription = ?description where LineID= ?id";
                cmd.Parameters.Add("?id", MySqlDbType.Int32).Value = txtID.Text;
                cmd.Parameters.Add("?name", MySqlDbType.String).Value = txtName.Text;
                cmd.Parameters.Add("?color", MySqlDbType.String).Value = comboBox1.SelectedItem;
                cmd.Parameters.Add("?description", MySqlDbType.String).Value = txtDescription.Text;
                Util.Connection.Open();
                cmd.ExecuteNonQuery();
                MetroFramework.MetroMessageBox.Show(this, "You have Edited the Line", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Close();
                LineList linelist = new LineList();
                linelist.linelist = this;
                linelist.Show();
            }
            catch
            {
                MetroFramework.MetroMessageBox.Show(this, "Opss.. PROBLEM", "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
            finally
            {
                Util.Connection.Close();
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
