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
    public partial class TaskForm : Form
    {
        HttpDataService httpDataService = new HttpDataService();
        DataTable detailTable = null;
        string billType = "";
        public int index;

        public TaskForm(string billType)
        {
            InitializeComponent();
            this.billType = billType;
        }

        private void BaseTaskForm_Load(object sender, EventArgs e)
        {
            DataTable tempTable = null;
            tempTable = httpDataService.SearchOutAbnormalTask();
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

            columnStyles["ID"].HeaderText = "任务号";
            columnStyles["StorageName"].HeaderText = "货位";
            columnStyles["ProductName"].HeaderText = "烟名";
            columnStyles["OrderType"].HeaderText = "类型";
            columnStyles["Status"].HeaderText = "状态";

            //如不显示，宽度设为0
            columnStyles["DetailID"].Width = 50;

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
                MainForm mf = new MainForm();
                mf.Show();
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
                    billDetail.BillType = billType;
                    billDetail.DetailID = int.Parse(this.dgInfo[this.dgInfo.CurrentCell.RowNumber, 0].ToString());
                    billDetail.PieceQuantity = decimal.Parse(this.dgInfo[this.dgInfo.CurrentCell.RowNumber, 5].ToString());
                    billDetail.BarQuantity = decimal.Parse(this.dgInfo[this.dgInfo.CurrentCell.RowNumber, 6].ToString());
                    billDetail.Operator = Dns.GetHostName();
                    httpDataService.ApplyTask(billDetail);
                    //修改内存中对应作业的状态为：已申请
                    DataRow[] rows = detailTable.Select(string.Format("TaskID = {0}", this.dgInfo[this.dgInfo.CurrentCell.RowNumber, 0].ToString()));
                    rows[0]["Status"] = "已申请";
                }
                else
                {
                    MessageBox.Show("0x001:btnNext_Click");
                }
                DetailForm billDetailForm = new DetailForm();
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

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}