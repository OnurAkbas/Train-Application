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
    public partial class addLine : MetroFramework.Forms.MetroForm
    {
        public addLine()
        {
            InitializeComponent();
        }

        public LineList AddLines;

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if(txtName.Text == "")
            {
                MetroFramework.MetroMessageBox.Show(this, "Please Enter a Name for the Line", "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return;
            }
            if (comboBox1.Text == "")
            {
                MetroFramework.MetroMessageBox.Show(this, "Please Select a color for the LINE!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return;
            }
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = Util.Connection;

                cmd.CommandText = "INSERT INTO linedb VALUES(null,?LineName,?Color,?LineDescription)";
                cmd.Parameters.Add("?LineName", MySqlDbType.VarChar).Value = txtName.Text;
                cmd.Parameters.Add("?Color", MySqlDbType.VarChar).Value = comboBox1.SelectedItem;
                cmd.Parameters.Add("?LineDescription", MySqlDbType.VarChar).Value = txtDescription.Text;

                if (Util.Connection.State == ConnectionState.Closed) Util.Connection.Open();
                int success = cmd.ExecuteNonQuery();
                if (success == 0) // Error
                {
                    MetroFramework.MetroMessageBox.Show(this, "Their was an problem adding the Station!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                }
                else
                {
                    MetroFramework.MetroMessageBox.Show(this, "You have Succesfully added a new Station!", "Succcess ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Util.Connection.Close();
                    Close();
                    LineList linelist = new LineList();
                    linelist.Linelistform = this;
                    linelist.Show();
                }
            }
            catch
            {
                MetroFramework.MetroMessageBox.Show(this, "Database ERROR!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
            finally
            {
                Util.Connection.Close();
            }
        }
    }
}
