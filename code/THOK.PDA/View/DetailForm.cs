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
using THOK.WES.Interface.Model;
using System.Net;
using THOK.PDA.Service;

namespace THOK.PDA.View
{
    public partial class DetailForm : Form
    {
        Detail detail = null;
        DataTable detailTable = null;
        HttpDataService httpDataService = new HttpDataService();
        DataRow detailRow = null;

        string billType = "";
        string detailID = "";
        string billID = "";

        public int Index;

        public DetailForm(Detail detail)
        {
            InitializeComponent();
            this.detail = detail;
        }

        private void BillDetailForm_Load(object sender, EventArgs e)
        {
            if (SystemCache.ConnetionType == "NetWork")
            {
                this.lbID.Text = detail.TaskID.ToString();
                this.lbOrderID.Text=detail.OrderID;
                this.lbCellCode.Text = detail.CellCode;
                this.lbProductName.Text = detail.ProductName;
                this.lbPieceQuantity.Text = detail.PieceQuantity.ToString();
                this.lbBarQuantity.Text = detail.BarQuantity.ToString();
                this.lbStatus.Text = detail.Status;
                this.lbOrderType.Text = detail.OrderType;

                WaitCursor.Restore();
            }
        }

        private void btnComplete_Click(object sender, EventArgs e)
        {
            WaitCursor.Set();
            try
            {
                if (SystemCache.ConnetionType == "NetWork")
                {
                    BillDetail billDetail = new BillDetail();

                    billDetail.BillNo = billID;
                    billDetail.BillType = billType;
                    billDetail.DetailID = Convert.ToInt32(lbID.Text);
                    billDetail.Operator = Dns.GetHostName();
                    billDetail.OperatePieceQuantity = Convert.ToDecimal(lbPieceQuantity.Text);
                    billDetail.OperateBarQuantity = Convert.ToDecimal(lbBarQuantity.Text);
                    httpDataService.FinishTask(billDetail);
                }
                MessageBox.Show("确认成功!");
                TaskForm baseTaskForm = new TaskForm(this.billType);
                if (this.Index > 0)
                {
                    baseTaskForm.index = this.Index;
                }
                baseTaskForm.Show();
                this.Close();
            }
            catch (Exception ex)
            {
                WaitCursor.Restore();
                MessageBox.Show(ex.Message);
                this.Close();
                SystemCache.MainFrom.Visible = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            lbPieceQuantity.Text = Convert.ToString(Convert.ToInt32(lbPieceQuantity.Text) + 1);
        }
        private void button2_Click(object sender, EventArgs e)
        {
            lbPieceQuantity.Text = Convert.ToString(Convert.ToInt32(lbPieceQuantity.Text) - 1);
            if (Convert.ToInt32(lbPieceQuantity.Text) < 0)
            {
                lbPieceQuantity.Text = "0";
            }
        }
        private void button3_Click(object sender, EventArgs e)
        {
            lbBarQuantity.Text = Convert.ToString(Convert.ToInt32(lbBarQuantity.Text) + 1);
        }
        private void button4_Click(object sender, EventArgs e)
        {
            lbBarQuantity.Text = Convert.ToString(Convert.ToInt32(lbBarQuantity.Text) - 1);
            if (Convert.ToInt32(lbBarQuantity.Text) < 0)
            {
                lbBarQuantity.Text = "0";
            }
        }
    }
}