using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Reflection;
using System.Net;
using THOK.PDA.Util;
using THOK.WES.Interface.Model;
using THOK.PDA.Service;

namespace THOK.PDA.View
{
    public partial class BaseTaskForm : Form
    {
        HttpDataService httpDataService = new HttpDataService();
        string billType = "";
        string billId = "";

        public int index;

        public BaseTaskForm(string billType, string billId)
        {
            InitializeComponent();
            this.billType = billType;
            this.billId = billId;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void BaseTaskForm_Load(object sender, EventArgs e)
        {
            
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            
        }

        private void dgInfo_CurrentCellChanged(object sender, EventArgs e)
        {
            
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            
        }

        private void BaseTaskForm_KeyDown(object sender, KeyEventArgs e)
        {
           
        }
    }
}