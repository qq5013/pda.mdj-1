using System;
using THOK.WES.Interface.Model;
using System.Net;
using LitJson;
using System.Data;

namespace THOK.WES.Interface
{
    public class Task
    {
        private Uri url = null;

        private string taskType = string.Empty;

        public delegate void GetBillMasterCompletedEventHandler(bool isSuccess,string msg,BillMaster[] billMasters);
        public event GetBillMasterCompletedEventHandler GetBillMasterCompleted;

        public delegate void GetBillDetailCompletedEventHandler(bool isSuccess,string msg,BillDetail[] billDetails);
        public event GetBillDetailCompletedEventHandler GetBillDetailCompleted;

        public delegate void ApplyCompletedEventHandler(bool isSuccess,string msg);
        public event ApplyCompletedEventHandler ApplyCompleted;

        public delegate void CancelCompletedEventHandler(bool isSuccess,string msg);
        public event CancelCompletedEventHandler CancelCompleted;

        public delegate void ExecuteCompletedEventHandler(bool isSuccess,string msg);
        public event ExecuteCompletedEventHandler ExecuteCompleted;

        public delegate void GetRfidInfoCompletedEventHandler(bool isSuccess, string msg, BillDetail[] billDetails);
        public event GetRfidInfoCompletedEventHandler GetRfidInfoCompleted;

        public delegate void BcComposeEventHandler(bool isSuccess, string msg);
        public event BcComposeEventHandler BcComposeCompleted;

        public delegate void GetShelfEventHandler(bool isSuccess, string msg, ShelfInfo[] shelfInfo);
        public event GetShelfEventHandler GetShelf;

        public delegate void GetOutAbnormityTask(OutAbnormityBill[] outAbnormityBill);
        public event GetOutAbnormityTask GetOutAbnormity;

        public Task(string url)
        {
            this.url = new Uri(url);
        }

        //查询所有可以执行的主单；
        public void SearchBillMaster(string parameter)
        {
            taskType = "GetBillMaster";
            WebClient client = new WebClient();
            client.Headers["Content-Type"] = @"application/x-www-form-urlencoded; charset=UTF-8";
            parameter = JsonMapper.ToJson(parameter.Split(','));
            client.UploadStringAsync(url, "post", @"Parameter={'Method':'GetMaster','BillTypes':" + parameter + "}");
            client.UploadStringCompleted += new UploadStringCompletedEventHandler(client_UploadStringCompleted);
        }

        //查询选择的所有主单里所有未执行细单；
        public void SearchBillDetail(BillMaster[] billMasters, string productCode, string operateType,string OperateArea, string @operator)
        {
            taskType = "GetBillDetail";
            WebClient client = new WebClient();
            client.Headers["Content-Type"] = @"application/x-www-form-urlencoded; charset=UTF-8";
            string parameter = JsonMapper.ToJson(billMasters);
            client.UploadStringAsync(url, "post", @"Parameter={'Method':'GetDetail','ProductCode': '" + productCode + "','OperateType':'" + operateType + "','OperateArea':'" + OperateArea + "','Operator':'" + @operator + "','BillMasters':" + parameter + "}");
            client.UploadStringCompleted += new UploadStringCompletedEventHandler(client_UploadStringCompleted);

        }

        //请求所有选择的细单；
        public void Apply(BillDetail[] billDetails, string useTag)
        {
            taskType = "Apply";
            WebClient client = new WebClient();
            client.Headers["Content-Type"] = @"application/x-www-form-urlencoded; charset=UTF-8";
            string parameter = JsonMapper.ToJson(billDetails);
            client.UploadStringAsync(url, "post", @"Parameter={'Method':'Apply','UseTag':'" + useTag + "','BillDetails':" + parameter + "}");
            client.UploadStringCompleted += new UploadStringCompletedEventHandler(client_UploadStringCompleted);
        }

        //取消所有选择的细单的请求；
        public void Cancel(BillDetail[] billDetails, string useTag)
        {
            taskType = "Cancel";
            WebClient client = new WebClient();
            client.Headers["Content-Type"] = @"application/x-www-form-urlencoded; charset=UTF-8";
            string parameter = JsonMapper.ToJson(billDetails);
            client.UploadStringAsync(url, "post", @"Parameter={'Method':'Cancel','UseTag':'" + useTag + "','BillDetails':" + parameter + "}");
            client.UploadStringCompleted += new UploadStringCompletedEventHandler(client_UploadStringCompleted);
        }

        //执行所有选择的细单；
        public void Execute(BillDetail[] billDetails, string useTag)
        {
            taskType = "Execute";
            WebClient client = new WebClient();
            client.Headers["Content-Type"] = @"application/x-www-form-urlencoded; charset=UTF-8";  
            string parameter = JsonMapper.ToJson(billDetails);
            client.UploadStringAsync(url, "post", @"Parameter={'Method':'Execute','UseTag':'" + useTag + "','BillDetails':" + parameter + "}");
            client.UploadStringCompleted += new UploadStringCompletedEventHandler(client_UploadStringCompleted);
        }

        //根据rfid查询卷烟和数量；
        public void SearchRfidInfo(string rfid)
        {
            taskType = "GetRfidInfo";
            WebClient client = new WebClient();
            client.Headers["Content-Type"] = @"application/x-www-form-urlencoded; charset=UTF-8";
            //string parameter = JsonMapper.ToJson(rfid.Split(','));
            client.UploadStringAsync(url, "post", @"Parameter={'Method':'GetRfidInfo','RfidId':'" + rfid + "'}");
            client.UploadStringCompleted += new UploadStringCompletedEventHandler(client_UploadStringCompleted);
        }

        public void BcCompose(string billNo)
        {
            taskType = "Compose";
            WebClient client = new WebClient();
            client.Headers["Content-Type"] = @"application/x-www-form-urlencoded; charset=UTF-8";
            client.UploadStringAsync(url, "post", @"billNo=" + billNo);
            client.UploadStringCompleted += new UploadStringCompletedEventHandler(client_UploadStringCompleted);
        }
        //查询shelf的信息
        public void Getshelf()
        {
            taskType = "GetShelf";
            WebClient client = new WebClient();
            client.Headers["Content-Type"] = @"application/x-www-form-urlencoded; charset=UTF-8";
            client.UploadStringAsync(url, "post", @"Parameter={'Method':'GetShelf'}");
            client.UploadStringCompleted += new UploadStringCompletedEventHandler(client_UploadStringCompleted);
        }

        void client_UploadStringCompleted(object sender, UploadStringCompletedEventArgs ex)
        {           
            switch (taskType)
            {
                #region 主单
                case "GetBillMaster":
                    try
                    {
                        string result = ex.Result;
                        Result r = JsonMapper.ToObject<Result>(result);
                        if (r.IsSuccess)
                        {
                            if (GetBillMasterCompleted != null)
                            {
                                GetBillMasterCompleted(true, r.Message, r.BillMasters);
                            }
                        }
                        else
                        {
                            if (GetBillMasterCompleted != null)
                            {
                                GetBillMasterCompleted(false, r.Message, null);
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        if (GetBillMasterCompleted != null)
                        {
                            GetBillMasterCompleted(false, e.Message, null);
                        }
                    }

                    break;
                #endregion
                #region 细单
                case "GetBillDetail":
                    try
                    {
                        string result = ex.Result;
                        Result r = JsonMapper.ToObject<Result>(result);
                        if (r.IsSuccess)
                        {
                            if (GetBillDetailCompleted != null)
                            {
                                GetBillDetailCompleted(true, r.Message, r.BillDetails);
                            }
                        }
                        else
                        {
                            if (GetBillDetailCompleted != null)
                            {
                                GetBillDetailCompleted(false, r.Message, null);
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        if (GetBillDetailCompleted != null)
                        {
                            GetBillDetailCompleted(false, e.Message, null);
                        }
                    }
                    break;
                #endregion
                #region 申请
                case "Apply":
                    try
                    {
                        string result = ex.Result;
                        Result r = JsonMapper.ToObject<Result>(result);
                        if (r.IsSuccess)
                        {
                            if (ApplyCompleted != null)
                            {
                                ApplyCompleted(true, r.Message);
                            }
                        }
                        else
                        {
                            if (ApplyCompleted != null)
                            {
                                ApplyCompleted(false, r.Message);
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        if (ApplyCompleted != null)
                        {
                            ApplyCompleted(false, e.Message);
                        }
                    }
                    break;
                #endregion
                #region 取消
                case "Cancel":
                    try
                    {
                        string result = ex.Result;
                        Result r = JsonMapper.ToObject<Result>(result);
                        if (r.IsSuccess)
                        {
                            if (CancelCompleted != null)
                            {
                                CancelCompleted(true, r.Message);
                            }
                        }
                        else
                        {
                            if (CancelCompleted != null)
                            {
                                CancelCompleted(false, r.Message);
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        if (CancelCompleted != null)
                        {
                            CancelCompleted(false, e.Message);
                        }
                    }
                    break;
                #endregion
                #region 确认
                case "Execute":
                    try
                    {
                        string result = ex.Result;
                        Result r = JsonMapper.ToObject<Result>(result);
                        if (r.IsSuccess)
                        {
                            if (ExecuteCompleted != null)
                            {
                                ExecuteCompleted(true, r.Message);
                            }
                        }
                        else
                        {
                            if (ExecuteCompleted != null)
                            {
                                ExecuteCompleted(false, r.Message);
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        if (ExecuteCompleted != null)
                        {
                            ExecuteCompleted(false, e.Message);
                        }
                    }
                    break;
                #endregion
                #region RFID
                case "GetRfidInfo":
                    try
                    {
                        string result = ex.Result;
                        Result r = JsonMapper.ToObject<Result>(result);
                        if (r.IsSuccess)
                        {
                            if (GetRfidInfoCompleted != null)
                            {
                                GetRfidInfoCompleted(true, r.Message, r.BillDetails);
                            }
                        }
                        else
                        {
                            if (GetRfidInfoCompleted != null)
                            {
                                GetRfidInfoCompleted(false, r.Message, r.BillDetails);
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        if (GetRfidInfoCompleted != null)
                        {
                            GetRfidInfoCompleted(false, e.Message, null);
                        }
                    }
                    break;
                #endregion
                #region 货架
                case "GetShelf":
                    try
                    {
                        string result = ex.Result;
                        Result r = JsonMapper.ToObject<Result>(result);
                        if (r.IsSuccess)
                        {
                            if (GetShelf != null)
                            {
                                GetShelf(true, r.Message,r.ShelfInfo);
                            }
                        }
                        else
                        {
                            if (GetShelf != null)
                            {
                                GetShelf(false, r.Message,null);
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        if (GetShelf != null)
                        {
                            GetShelf(false, e.Message,null);
                        }
                    }
                    break;
                #endregion
                #region 其他
                case "Compose":
                    try
                    {
                        string result = ex.Result;
                        Result r = JsonMapper.ToObject<Result>(result);
                        if (r.IsSuccess)
                        {
                            if (BcComposeCompleted != null)
                            {
                                BcComposeCompleted(true, r.Message);
                            }
                        }
                        else
                        {
                            if (BcComposeCompleted != null)
                            {
                                BcComposeCompleted(false, r.Message);
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        if (BcComposeCompleted != null)
                        {
                            BcComposeCompleted(false, e.Message);
                        }
                    }
                    break;
                #endregion
                #region 异型烟出库
                case "GetOutAbnormity":
                    try
                    {
                        string result = ex.Result;
                        Result r = JsonMapper.ToObject<Result>(result);
                        if (r.IsSuccess)
                        {
                            if (GetOutAbnormity != null)
                            {
                                GetOutAbnormity(r.OutAbnormityBill);
                            }
                        }
                        else
                        {
                            if (GetOutAbnormity != null)
                            {
                                GetOutAbnormity(r.OutAbnormityBill);
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        if (GetRfidInfoCompleted != null)
                        {
                            GetRfidInfoCompleted(false, e.Message, null);
                        }
                    }
                    break;
                    #endregion
                default:
                    break;
            }
        }
    }
}
