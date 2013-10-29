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

        private void btnAbnormalOut_Click(object sender, EventArgs e)
        {
            string billType = "abnormal";
            FormShow(billType);
        }
        private void btnSmallOut_Click(object sender, EventArgs e)
        {
            string billType = "small";
            FormShow(billType);
        }
        private void FormShow(string billType)
        {
            WaitCursor.Set();
            TaskForm btFrom = new TaskForm(billType);
            btFrom.Show();
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