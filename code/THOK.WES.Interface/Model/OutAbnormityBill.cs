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

        private string orderID;
        public string OrderID
        {
            get { return orderID; }
            set { orderID = value; }
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

        private decimal taskPieceQuantity = 0;
        public decimal TaskPieceQuantity
        {
            get { return taskPieceQuantity; }
            set { taskPieceQuantity = value; }
        }

        private decimal taskBarQuantity = 0;
        public decimal TaskBarQuantity
        {
            get { return taskBarQuantity; }
            set { taskBarQuantity = value; }
        }

        private string status = string.Empty;
        public string Status
        {
            get { return status; }
            set { status = value; }
        }
    }
}
