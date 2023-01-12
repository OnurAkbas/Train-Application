using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql;
using MySql.Data.MySqlClient;

namespace Train_Application
{
    public partial class Login : MetroFramework.Forms.MetroForm
    {
        public Login()
        {
            InitializeComponent();


            if (Properties.Settings.Default.UtilcbCheck)
            {
                cbUsername.Checked = true;
                txtUser.Text = Properties.Settings.Default.UtilUsernameSave;
            }
        }

    private bool TryLogin()
        {
            try
            {
                if (Util.Connection.State == ConnectionState.Closed) Util.Connection.Open();
                string command = "SELECT User,Level FROM userdb WHERE User = @user AND Pass = @pswd";
                MySqlCommand cmdline = new MySqlCommand(command, Util.Connection);
                cmdline.Parameters.AddWithValue("@user", txtUser.Text);
                cmdline.Parameters.AddWithValue("@pswd", txtPass.Text);
                MySqlDataReader dr;

                dr = cmdline.ExecuteReader();
                if (!dr.HasRows)
                {
                    MetroFramework.MetroMessageBox.Show(this, "Your Username/Password is Incorrect", "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    return false;
                }
                dr.Read();
                Properties.Settings.Default.UtilUsernameSave = txtUser.Text;
                Properties.Settings.Default.Save();
                return true;
            }
            catch (Exception er)
            {
            if (er is MySqlException)
                {
                 MetroFramework.MetroMessageBox.Show(this, "Conntection Error Check Database : " + er, "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Hand);   
                }
            else
                {
                MetroFramework.MetroMessageBox.Show(this, "An error has occured while logging in: " + System.Environment.NewLine + er.Message, "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Hand);
                } 
            }
            finally
            {
                Util.Connection.Close();
            }
            return false;
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (TryLogin())
            {
                Properties.Settings.Default.userName = txtUser.Text;
                Properties.Settings.Default.Save();

                if (Util.Level == 1)
                {
                    this.Visible = true;
                    AdminMain Admin = new AdminMain();
                    Admin.Adminform = this;
                    Admin.Show();
                }
                else
                {
                    this.Visible = true;
                    AdminMain Admin = new AdminMain();
                    Admin.Adminform = this;
                    Admin.Show();
                }
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Login_Load(object sender, EventArgs e)
        {
            this.Visible = false;
        }

        private void cbUsername_CheckedChanged(object sender, EventArgs e)
        {
        Properties.Settings.Default.UtilcbCheck = cbUsername.Checked;
        Properties.Settings.Default.Save();
    }

        private void txtPass_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                btnLogin_Click(null, null);
            }
        }
    }
}