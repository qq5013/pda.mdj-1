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
    public partial class BillDetailForm : Form
    {
        DataTable detailTable = null;
        HttpDataService httpDataService = new HttpDataService();
        BillService billService = new BillService();
        DataRow detailRow = null;

        string billType = "";
        string detailID = "";
        string billID = "";

        public int Index;

        public BillDetailForm(string billType, string detailID, string billID, DataTable table)
        {
            InitializeComponent();
            this.billType = billType;
            this.detailID = detailID;
            this.billID = billID;
            detailTable = table;
            if (this.billType == "3")
            {
                this.button1.Visible = true;
                this.button2.Visible = true;
                this.button3.Visible = true;
                this.button4.Visible = true;
            }
            else
            {
                this.button1.Visible = false;
                this.button2.Visible = false;
                this.button3.Visible = false;
                this.button4.Visible = false;
            }
        }

        private void BillDetailForm_Load(object sender, EventArgs e)
        {
            switch (billType)
            {
                case "2": this.label2.Text = "出库单据明细"; break;
            }
            if (SystemCache.ConnetionType == "NetWork")
            {
                detailRow = detailTable.Select(string.Format("DetailID = {0}", detailID))[0];
            }

            this.lbID.Text = detailRow["DetailID"].ToString();
            this.lbStorageID.Text = detailRow["OperateStorageName"].ToString();
            this.lbTobacconame.Text = detailRow["OperateProductName"].ToString();
            this.lbPiece.Text = detailRow["OperatePieceQuantity"].ToString();
            this.lbItem.Text = detailRow["OperateBarQuantity"].ToString();
            this.lbState.Text = detailRow["StatusName"].ToString();
            this.lbType.Text = detailRow["OperateName"].ToString();
            this.lbBillid.Text = billID;

            if (detailRow["TargetStorageName"].ToString() != "")
            {
                this.lbType.Text = this.lbType.Text + "->" + detailRow["TargetStorageName"].ToString();
            }
            WaitCursor.Restore();
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
                    billDetail.OperatePieceQuantity = Convert.ToDecimal(lbPiece.Text);
                    billDetail.OperateBarQuantity = Convert.ToDecimal(lbItem.Text);
                    httpDataService.FinishTask(billDetail);
                }
                MessageBox.Show("确认成功!");
                BaseTaskForm baseTaskForm = new BaseTaskForm(this.billType, billID);
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
            lbPiece.Text = Convert.ToString(Convert.ToInt32(lbPiece.Text) + 1);
        }
        private void button2_Click(object sender, EventArgs e)
        {
            lbPiece.Text = Convert.ToString(Convert.ToInt32(lbPiece.Text) - 1);
            if (Convert.ToInt32(lbPiece.Text) < 0)
            {
                lbPiece.Text = "0";
            }
        }
        private void button3_Click(object sender, EventArgs e)
        {
            lbItem.Text = Convert.ToString(Convert.ToInt32(lbItem.Text) + 1);
        }
        private void button4_Click(object sender, EventArgs e)
        {
            lbItem.Text = Convert.ToString(Convert.ToInt32(lbItem.Text) - 1);
            if (Convert.ToInt32(lbItem.Text) < 0)
            {
                lbItem.Text = "0";
            }
        }
    }
}