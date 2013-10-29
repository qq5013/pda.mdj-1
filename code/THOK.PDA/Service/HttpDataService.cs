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
