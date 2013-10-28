using System;
using System.Collections.Generic;
using System.Text;

namespace THOK.WES.Interface.Model
{
    public class BillDetail
    {
        private string billNo = string.Empty;
        public string BillNo
        {
            get { return billNo; }
            set { billNo = value; }
        }

        private string billType = string.Empty;
        public string BillType
        {
            get { return billType; }
            set { billType = value; }
        }

        public string BillTypeName
        {
            get 
            {
                switch (billType)
                {
                    case "1":
                        return "入库";
                    case "2":
                        return "出库";
                    case "3":
                        return "移库";
                    case "4":
                        return "盘点";
                    default:
                        return "";
                }
            }

            set
            {
                billType = value;
            }
        }

        private int detailID = 0;
        public int DetailID
        {
            get { return detailID; }
            set { detailID = value; }
        }

        private string storageName = string.Empty;
        public string StorageName
        {
            get { return storageName; }
            set { storageName = value; }
        }

        private string storageRfid = string.Empty;
        public string StorageRfid
        {
            get { return storageRfid; }
            set { storageRfid = value; }
        }

        private string cellRfid = string.Empty;
        public string CellRfid
        {
            get { return cellRfid; }
            set { cellRfid = value; }
        }

        private string targetStorageName = string.Empty;
        public string TargetStorageName
        {
            get { return targetStorageName; }
            set { targetStorageName = value; }
        }

        private string targetStorageRfid = string.Empty;
        public string TargetStorageRfid
        {
            get { return targetStorageRfid; }
            set { targetStorageRfid = value; }
        }

        private string productCode = string.Empty;
        public string ProductCode
        {
            get { return productCode; }
            set { productCode = value; }
        }

        private string productName = string.Empty;
        public string ProductName
        {
            get { return productName; }
            set { productName = value; }
        }

        private decimal pieceQuantity = 0;
        public decimal PieceQuantity
        {
            get { return pieceQuantity; }
            set { pieceQuantity = value; }
        }

        private decimal barQuantity = 0;
        public decimal BarQuantity
        {
            get { return barQuantity; }
            set { barQuantity = value; }
        }

        private decimal operatePieceQuantity = 0;
        public decimal OperatePieceQuantity
        {
            get { return operatePieceQuantity; }
            set { operatePieceQuantity = value; }
        }

        private decimal operateBarQuantity = 0;
        public decimal OperateBarQuantity
        {
            get { return operateBarQuantity; }
            set { operateBarQuantity = value; }
        }

        private decimal total = 0;
        public decimal Total
        {
            get { return total; }
            set { total = value; }
        }

        private string isRounding = string.Empty;
        public string IsRounding
        {
            get { return isRounding; }
            set { isRounding = value; }
        }

        private bool ableMerge = false;
        public bool AbleMerge
        {
            get { return ableMerge; }
            set { ableMerge = value; }
        }

        private string operatorCode = string.Empty;
        public string OperatorCode
        {
            //get { return operatorCode; }
            set { operatorCode = value; }
        }

        private string @operator = string.Empty;
        public string Operator
        {
            get { return @operator; }
            set { @operator = value; }
        }

        private string status = string.Empty;
        public string Status
        {
            get { return status; }
            set { status = value; }
        }

        public string StatusName
        {
            get
            {
                switch (status)
                {
                    case "0":
                        return "未开始";
                    case "1":
                        return "已申请";
                    case "2":
                        return "已完成";
                    default:
                        return "";
                }
            }
            set
            {
                status = value;
            }
        }

        private int palletTag = 0;
        public int PalletTag
        {
            get { return palletTag; }
            set { palletTag = value; }
        }
    }
}
