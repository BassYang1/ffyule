using log4net;
using Lottery.DAL.Flex;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Lottery.FFApp.WFP
{
    /// <summary>
    /// 支付返回页面
    /// </summary>
    public partial class result : System.Web.UI.Page
    {
        /// <summary>
        /// Log instance.
        /// </summary>
        protected static readonly ILog Log = LogManager.GetLogger(typeof(result));

        /// <summary>
        /// 支付结果
        /// </summary>
        public bool IsSuccess { get; set; }

        /// <summary>
        /// 支付消息
        /// </summary>
        public string Message { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.IsSuccess = false;
            this.Message = string.Empty;

            try
            {
                Log.Debug("*微付返回页面参数********************************");
                Log.Debug(WfpHelper.GetRequestData());

                //To receive the parameter form ZhiHpay
                var parameters = new SortedDictionary<string, string>();

                string merchant_code = WfpHelper.Trim(Request["merchant_code"]);
                parameters.Add("merchant_code", merchant_code);

                string notify_id = WfpHelper.Trim(Request["notify_id"]);
                parameters.Add("notify_id", notify_id);

                string notify_type = WfpHelper.Trim(Request["notify_type"]);
                parameters.Add("notify_type", notify_type);

                string interface_version = WfpHelper.Trim(Request["interface_version"]);
                parameters.Add("interface_version", interface_version);

                string sign_type = WfpHelper.Trim(Request["sign_type"]);
                //parameters.Add("sign_type", sign_type);

                string sign = WfpHelper.Trim(Request["sign"]);
                //parameters.Add("sign", sign);

                string order_no = WfpHelper.Trim(Request["order_no"]);
                parameters.Add("order_no", order_no);

                string order_time = WfpHelper.Trim(Request["order_time"]);
                parameters.Add("order_time", order_time);

                string order_amount = WfpHelper.Trim(Request["order_amount"]);
                parameters.Add("order_amount", order_amount);

                string extra_return_param = WfpHelper.Trim(Request["extra_return_param"]);
                parameters.Add("extra_return_param", extra_return_param);

                string trade_no = WfpHelper.Trim(Request["trade_no"]);
                parameters.Add("trade_no", trade_no);

                string trade_time = WfpHelper.Trim(Request["trade_time"]);
                parameters.Add("trade_time", trade_time);

                string bank_seq_no = WfpHelper.Trim(Request["bank_seq_no"]);
                parameters.Add("bank_seq_no", bank_seq_no);

                string trade_status = WfpHelper.Trim(Request["trade_status"]);
                parameters.Add("trade_status", trade_status);

                //签名
                string signStr = "";

                foreach (var p in parameters)
                {
                    if (!string.IsNullOrEmpty(p.Value))
                    {
                        signStr = string.Format("{0}{1}={2}&", signStr, p.Key, p.Value);
                    }
                }

                signStr = signStr.Substring(0, signStr.Length - 1);

                //微付公钥，每个商家对应一个固定的微付公钥
                //string key = @"MIGfMA0GCSqGSIb3DQEBAQUAA4GNADCBiQKBgQC4L2vPNvlT97cHu3Hfrik151bQ3rB4Gvip+Wd325jVgj2YwhOFDsRNw3KjZGNTXe1Jod0nWpWA5P//hV2kz/iJnlDtXNhxw6Kf7wWwSk1BiZ36lPgSzjBEZf2FQfjtCrb3FE75q3bOJgCha6iqReVj5Ul7WjC91Gd/Em50A1MLwIDAQAB";
                string key = @"MIGfMA0GCSqGSIb3DQEBAQUAA4GNADCBiQKBgQC4L2vPNvlT97cHu3Hfrik151bQ
3rB4Gvip+Wd325jVgj2YwhOFDsRNw3KjZGNTXe1Jod0nWpWA5P//hV2kz/iJnlDt
XNhxw6Kf7wWwSk1BiZ36lPgSzjBEZf2FQf2jtCrb3FE75q3bOJgCha6iqReVj5Ul
7WjC91Gd/Em50A1MLwIDAQAB";
                string merchant_public_key = WfpHelper.RSAPublicKeyJava2DotNet(key);

                //验证签名
                bool result = WfpHelper.ValidateRsaSign(signStr, merchant_public_key, sign);
                order_no = order_no.Substring(0, 1) + "_" + order_no.Substring(1);

                if (result || trade_status.Equals("SUCCESS", StringComparison.OrdinalIgnoreCase))
                {
                    if (new UserChargeDAL().Update(order_no, trade_no, bank_seq_no) == false)
                    {
                        Log.ErrorFormat("更新系统支付订单失败，订单号: {0}", order_no);
                        this.Message = "更新系统支付订单失败";
                    }
                    else
                    {
                        Log.InfoFormat("支付成功，订单号: {0}", order_no);
                        this.IsSuccess = true;
                        this.Message = "支付成功";
                    }
                }
                else
                {
                    Log.ErrorFormat("第三方支付失败，订单号: {0}", order_no);
                    this.Message = "第三方支付失败";
                }
            }
            catch (Exception ex)
            {
                Log.ErrorFormat("支付失败: {0}", ex);
                this.Message = ex.Message;
            }
        }
    }
}