using System;
using System.Collections.Generic;
using System.Text;

namespace THOK.WES.Interface.Model
{
    public class OutAbnormityBill
    {
        private int taskID = 0;
        public int TaskID
        {
            get { return taskID; }
            set { taskID = value; }
        }

        private string positionName = string.Empty;
        public string PositionName
        {
            get { return positionName; }
            set { positionName = value; }
        }

        private string productName = string.Empty;
        public string ProductName
        {
            get { return productName; }
            set { productName = value; }
        }

        private decimal taskJianQuantity = 0;
        public decimal TaskJianQuantity
        {
            get { return taskJianQuantity; }
            set { taskJianQuantity = value; }
        }

        private decimal taskTiaoQuantity = 0;
        public decimal TaskTiaoQuantity
        {
            get { return taskTiaoQuantity; }
            set { taskTiaoQuantity = value; }
        }

        private string status = string.Empty;
        public string Status
        {
            get { return status; }
            set { status = value; }
        }
    }
}
