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
    public partial class AdminMain : MetroFramework.Forms.MetroForm
    {
        public AdminMain()
        {
            InitializeComponent();
            lblName.Text = Properties.Settings.Default.userName;

            lblTime.Text = DateTime.Now.ToShortDateString();
        }

        public Login Adminform;
        public StationList stationlist;
        public LineList linelist;
        public LineConnected lineconencted;
        public connectForm connectform;
        public ShortDistance shortdistanceform;


        private void btnTrainList_Click(object sender, EventArgs e)
        {
            Close();
            StationList Trainlist = new StationList();
            Trainlist.TrainListForm = this;
            Trainlist.Show();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
            LineList Trainlist = new LineList();
            Trainlist.lineListForm = this;
            Trainlist.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Close();
            connectForm connectform = new connectForm();
            connectform.ConnectForm = this;
            connectform.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
            LineConnected linesconnect = new LineConnected();
            linesconnect.linesconnect = this;
            linesconnect.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Close();
            DistanceFormcs distance = new DistanceFormcs();
            distance.connection = this;
            distance.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Close();
            ShortDistance distance = new ShortDistance();
            distance.shortdistance = this;
            distance.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Close();
            StationConnected distance = new StationConnected();
            distance.form12345 = this;
            distance.Show();
        }
    }
}
