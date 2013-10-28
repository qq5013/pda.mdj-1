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
            if (SystemCache.ConnetionType == "NetWork")
            {
                HttpDataService httpDataService = new HttpDataService();
                tempTable = httpDataService.SearchBillMaster(billType);

                this.lbInfo.ValueMember = "BillNo";
                this.lbInfo.DisplayMember = "BillNo";
            }
            this.lbInfo.DataSource = tempTable;
            if (tempTable.Rows.Count == 0)
            {
                this.btnNext.Enabled = false;
            }
            WaitCursor.Restore();
        }

        private void btnHome_Click(object sender, EventArgs e)
        {
            SystemCache.MainFrom.Visible = true;
            this.Close();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            WaitCursor.Set();
            try
            {
                BaseTaskForm baseTaskForm = new BaseTaskForm(this.billType, this.lbInfo.SelectedValue.ToString());
                baseTaskForm.Show();
                this.Close();
            }
            catch (Exception ex)
            {
                WaitCursor.Restore();
                MessageBox.Show("读取数据失败!" + ex.Message);
            }
        }

        private void BillMasterForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.ToString() == "Escape")
            {
                this.btnHome_Click(null, null);
            }
            if (e.KeyCode.ToString() == "Return")
            {
                this.btnNext_Click(null, null);
            }
        }
    }
}