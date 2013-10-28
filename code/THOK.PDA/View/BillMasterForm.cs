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
    public partial class BillMasterForm : Form
    {
        string billType = "";

        public BillMasterForm(string billType)
        {
            InitializeComponent();
            this.billType = billType;
        }

        private void BillMasterForm_Load(object sender, EventArgs e)
        {
            switch (billType)
            {
                case "2": this.label2.Text = "出库主单据号"; break;
            }
            DataTable tempTable = null;

            HttpDataService httpDataService = new HttpDataService();
            tempTable = httpDataService.SearchBillMaster(billType);

            this.lbInfo.ValueMember = "BillNo";
            this.lbInfo.DisplayMember = "BillNo";

            this.lbInfo.DataSource = tempTable;
            if (tempTable.Rows.Count == 0)
            {
                this.btnNext.Enabled = false;
            }
            WaitCursor.Restore();
        }
    }
}