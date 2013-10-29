using System;
using System.Collections.Generic;
using System.Text;
using THOK.PDA.Util;
using System.Data;
using THOK.WES.Interface.Model;
using Newtonsoft.Json;
using System.Net;

namespace THOK.PDA.Service
{
    public class HttpDataService
    {
        private HttpUtil util = new HttpUtil();

        public DataTable SearchBillMaster(string billTypes)
        {
            string parameter = @"Parameter={'Method':'getMaster','BillTypes':" + JsonConvert.SerializeObject(billTypes.Split(',')) + "}";

            string msg = util.GetDataFromServer(parameter);
            Result r = JsonConvert.DeserializeObject<Result>(msg);

            DataTable table = GenBill();
            if (r.IsSuccess)
            {
                for (int i = 0; i < r.BillMasters.Length; i++)
                {
                    DataRow row = table.NewRow();
                    row["BillNo"] = r.BillMasters[i].BillNo;
                    table.Rows.Add(row);
                }
                return table;
            }
            else
            {
                return table;
            }
        }
        public DataTable SearchBillDetail(BillMaster billMaster)
        {
            string parameter = @"Parameter={'Method':'getDetail','ProductCode': '" + "" + "','OperateType':'" + billMaster.BillType + "','OperateArea':'" + "1,2,3" + "','Operator':'" + Dns.GetHostName() + "','BillMasters':" + JsonConvert.SerializeObject(new BillMaster[] { billMaster }) + "}";
            string msg = util.GetDataFromServer(parameter);
            Result r = JsonConvert.DeserializeObject<Result>(msg);

            DataTable table = GenDetailTable();

            for (int i = 0; i < r.BillDetails.Length; i++)
            {
                DataRow row = table.NewRow();

                row["DetailID"] = r.BillDetails[i].DetailID;
                row["OperateStorageName"] = r.BillDetails[i].StorageName;
                row["TargetStorageName"] = r.BillDetails[i].TargetStorageName;
                row["OperateName"] = r.BillDetails[i].BillTypeName;
                row["OperateProductName"] = r.BillDetails[i].ProductName;
                row["OperatePieceQuantity"] = r.BillDetails[i].PieceQuantity;
                row["OperateBarQuantity"] = r.BillDetails[i].BarQuantity;
                row["StatusName"] = r.BillDetails[i].StatusName;

                table.Rows.Add(row);
            }
            return table;
        }

        public DataTable SearchOutAbnormalTask()
        {
            string parameter = @"Parameter={'Method':'getOutAbnormity'}";

            string msg = util.GetDataFromServer(parameter);
            Result r = JsonConvert.DeserializeObject<Result>(msg);

            DataTable table = GenBill();
            if (r.IsSuccess)
            {
                for (int i = 0; i < r.OutAbnormityBill.Length; i++)
                {
                    DataRow row = table.NewRow();
                    row["TaskID"] = r.OutAbnormityBill[i].TaskID;
                    table.Rows.Add(row);
                }
                return table;
            }
            else
            {
                return table;
            }
        }
        public DataTable SearchOutSmallTask()
        {
            string parameter = @"Parameter={'Method':'getOutSmall'}";

            string msg = util.GetDataFromServer(parameter);
            Result r = JsonConvert.DeserializeObject<Result>(msg);

            DataTable table = GenBill();
            if (r.IsSuccess)
            {
                for (int i = 0; i < r.OutAbnormityBill.Length; i++)
                {
                    DataRow row = table.NewRow();
                    row["TaskID"] = r.OutAbnormityBill[i].TaskID;
                    table.Rows.Add(row);
                }
                return table;
            }
            else
            {
                return table;
            }
        }

        public void ApplyTask(BillDetail billDetail)
        {
            string parameter = @"Parameter={'Method':'apply','UseTag':'" + "0" + "','BillDetails':" + JsonConvert.SerializeObject(new BillDetail[] { billDetail }) + "}";
            string msg = util.GetDataFromServer(parameter);
            //Result r = JsonConvert.DeserializeObject<Result>(msg);
        }
        public void CancelTask(BillDetail billDetail)
        {
            string parameter = @"Parameter={'Method':'cancel','UseTag':'" + "0" + "','BillDetails':" + JsonConvert.SerializeObject(new BillDetail[] { billDetail }) + "}";
            string msg = util.GetDataFromServer(parameter);
            //Result r = JsonConvert.DeserializeObject<Result>(msg);
        }
        public void FinishTask(BillDetail billDetail)
        {
            string parameter = @"Parameter={'Method':'execute','UseTag':'" + "0" + "','BillDetails':" + JsonConvert.SerializeObject(new BillDetail[] { billDetail }) + "}";
            string msg = util.GetDataFromServer(parameter);
        }

        private DataTable GenBill()
        {
            DataTable table = new DataTable();
            table.TableName = "AbnormalTable";
            table.Columns.Add("ID");
            return table;
        }

        private DataTable GenDetailTable()
        {
            DataTable table = new DataTable();
            table.Columns.Add("ID");
            table.Columns.Add("StorageName");
            table.Columns.Add("ProductName");
            table.Columns.Add("PieceQuantity");
            table.Columns.Add("Status");
            return table;
        }
    }
}
