using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Reflection;
using THOK.PDA.Util;
using THOK.PDA.Service;


namespace THOK.PDA.View
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            SystemCache.MainFrom = this;
        }

        private void btnOut_Click(object sender, EventArgs e)
        {
            string billType = "2";
            FormShow(billType);
        }
        private void btnSmall_Click(object sender, EventArgs e)
        {
            string billType = "3";
            FormShow(billType);
        }
        private void btnAbnormal_Click(object sender, EventArgs e)
        {
            string billType = "4";
            FormShow(billType);
        }
        private void FormShow(string billType)
        {
            WaitCursor.Set();
            BillMasterForm bmFrom = new BillMasterForm(billType);
            bmFrom.Show();
            this.Visible = false;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnParamenter_Click(object sender, EventArgs e)
        {
            WaitCursor.Set();
            try
            {
                ParameterForm parameterFrom = new ParameterForm();
                parameterFrom.Show();
                this.Visible = false;
            }
            catch (Exception)
            {
                WaitCursor.Restore();
            }
        }
    }
}