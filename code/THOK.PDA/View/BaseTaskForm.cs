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
        BillService billService = new BillService();
        DataTable detailTable = null;        
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
            this.label2.Text = billId;
            switch (billType)
            {
                case "2": this.label2.Text += "(出库)"; break;
            }
            DataTable tempTable = null;

            BillMaster billMaster = new BillMaster();
            billMaster.BillNo = billId;
            billMaster.BillType = billType;

            tempTable = httpDataService.SearchBillDetail(billMaster);
            detailTable = tempTable;

            this.dgInfo.DataSource = tempTable;
            if (tempTable.Rows.Count == 0)
            {
                this.btnNext.Enabled = false;
            }
            DataGridTableStyle gridStyle = new DataGridTableStyle();
            gridStyle.MappingName = tempTable.TableName;
            dgInfo.TableStyles.Add(gridStyle);
            GridColumnStylesCollection columnStyles = this.dgInfo.TableStyles[0].GridColumnStyles;

            columnStyles["OperateStorageName"].HeaderText = "货位";
            columnStyles["OperateProductName"].HeaderText = "烟名";
            columnStyles["OperateName"].HeaderText = "类型";
            columnStyles["StatusName"].HeaderText = "状态";
            columnStyles["DetailID"].HeaderText = "单据号";

            columnStyles["OperateStorageName"].Width = 100;
            columnStyles["OperateProductName"].Width = 120;
            columnStyles["OperateName"].Width = 50;
            columnStyles["StatusName"].Width = 50;
            //不显示，宽度设为0
            columnStyles["OperateName"].Width = 0;
            columnStyles["DetailID"].Width = 0;
            columnStyles["OperatePieceQuantity"].Width = 0;
            columnStyles["OperateBarQuantity"].Width = 0;
            columnStyles["TargetStorageName"].Width = 0;

            if (tempTable.Rows.Count != 0)
            {
                if (tempTable.Rows.Count <= index)
                {
                    index = tempTable.Rows.Count - 1;
                }
                dgInfo.Select(index);
                dgInfo.CurrentRowIndex = index;
                dgInfo.Focus();
            }
            WaitCursor.Restore();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            WaitCursor.Set();
            try
            {
                BillMasterForm billMasterForm = new BillMasterForm(billType);
                billMasterForm.Show();
                this.Close();
            }
            catch (Exception ex)
            {
                WaitCursor.Restore();
                MessageBox.Show(ex.Message);
            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            WaitCursor.Set();
            try
            {
                if (SystemCache.ConnetionType == "NetWork")
                {
                    BillDetail billDetail = new BillDetail();
                    billDetail.BillNo = billId;
                    billDetail.BillType = billType;
                    billDetail.DetailID = int.Parse(this.dgInfo[this.dgInfo.CurrentCell.RowNumber, 0].ToString());
                    billDetail.PieceQuantity = decimal.Parse(this.dgInfo[this.dgInfo.CurrentCell.RowNumber, 5].ToString());
                    billDetail.BarQuantity = decimal.Parse(this.dgInfo[this.dgInfo.CurrentCell.RowNumber, 6].ToString());
                    billDetail.Operator = Dns.GetHostName();
                    httpDataService.ApplyTask(billDetail);
                    //修改内存中对应作业的状态为：已申请
                    DataRow[] rows = detailTable.Select(string.Format("DetailID = {0}", this.dgInfo[this.dgInfo.CurrentCell.RowNumber, 0].ToString()));
                    rows[0]["StatusName"] = "已申请";
                }
                else
                {
                    MessageBox.Show("0x001:btnNext_Click");
                }
                BillDetailForm billDetailForm = new BillDetailForm(billType, this.dgInfo[this.dgInfo.CurrentCell.RowNumber, 0].ToString(), billId, detailTable);
                billDetailForm.Index = this.index;
                billDetailForm.Show();
                this.Close();
            }
            catch (Exception ex)
            {
                WaitCursor.Restore();
                MessageBox.Show(ex.Message);
            }
        }

        private void dgInfo_CurrentCellChanged(object sender, EventArgs e)
        {
            this.dgInfo.Select(this.dgInfo.CurrentCell.RowNumber);
            this.index = this.dgInfo.CurrentCell.RowNumber;
        }

        private void BaseTaskForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.ToString() == "Escape")
            {
                this.btnBack_Click(null, null);
            }
            if (e.KeyCode.ToString() == "Return")
            {
                this.btnNext_Click(null, null);
            }
        }
    }
}