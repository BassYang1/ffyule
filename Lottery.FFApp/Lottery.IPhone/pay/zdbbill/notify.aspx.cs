using log4net;
using Lottery.DAL.Flex;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Lottery.IPhone.ZDB
{
    /// <summary>
    /// 异步回调
    /// </summary>
    public partial class notify : System.Web.UI.Page
    {
        /// <summary>
        /// Log instance.
        /// </summary>
        protected static readonly ILog Log = LogManager.GetLogger(typeof(notify));

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                Log.Debug("*随笔付异步回调参数********************************");
                Log.Debug(ZdbHelper.GetRequestData());
                //To receive the parameter form ZhiHpay
                var parameters = new SortedDictionary<string, string>();
                string merchant_code = ZdbHelper.Trim(Request["merchant_code"]);
                parameters.Add("merchant_code", merchant_code);

                string notify_id = ZdbHelper.Trim(Request["notify_id"]);
                parameters.Add("notify_id", notify_id);

                string notify_type = ZdbHelper.Trim(Request["notify_type"]);
                parameters.Add("notify_type", notify_type);

                string interface_version = ZdbHelper.Trim(Request["interface_version"]);
                parameters.Add("interface_version", interface_version);

                string sign_type = ZdbHelper.Trim(Request["sign_type"]);
                //parameters.Add("sign_type", sign_type);

                string zhihpaysign = ZdbHelper.Trim(Request["sign"]);
                //parameters.Add("zhihpaysign", zhihpaysign);

                string order_no = ZdbHelper.Trim(Request["order_no"]);
                parameters.Add("order_no", order_no);
                order_no = order_no.Substring(0, 1) + "_" + order_no.Substring(1);

                string order_time = ZdbHelper.Trim(Request["order_time"]);
                parameters.Add("order_time", order_time);

                string order_amount = ZdbHelper.Trim(Request["order_amount"]);
                parameters.Add("order_amount", order_amount);

                string extra_return_param = ZdbHelper.Trim(Request["extra_return_param"]);
                parameters.Add("extra_return_param", extra_return_param);

                string trade_no = ZdbHelper.Trim(Request["trade_no"]);
                parameters.Add("trade_no", trade_no);

                string trade_time = ZdbHelper.Trim(Request["trade_time"]);
                parameters.Add("trade_time", trade_time);

                string bank_seq_no = ZdbHelper.Trim(Request["bank_seq_no"]);
                parameters.Add("bank_seq_no", bank_seq_no);

                string trade_status = ZdbHelper.Trim(Request["trade_status"]);
                parameters.Add("trade_status", trade_status);

                //Array data
                string signStr = "";
                
                foreach (var p in parameters)
                {
                    {
                        signStr = string.Format("{0}{1}={2}&", signStr, p.Key, p.Value);
                    }
                }

                signStr = signStr.Substring(0, signStr.Length - 1);

                //merchant private key 支付密钥
                string zhihf_public_key = @"MIGfMA0GCSqGSIb3DQEBAQUAA4GNADCBiQKBgQC4L2vPNvlT97cHu3Hfrik151bQ3rB4Gvip+Wd325jVgj2YwhOFDsRNw3KjZGNTXe1Jod0nWpWA5P//hV2kz/iJnlDtXNhxw6Kf7wWwSk1BiZ36lPgSzjBEZf2FQf2jtCrb3FE75q3bOJgCha6iqReVj5Ul7WjC91Gd/Em50A1MLwIDAQAB";
                zhihf_public_key = ZdbHelper.RSAPrivateKeyJava2DotNet(zhihf_public_key);

                //check sign
                bool result = ZdbHelper.ValidateRsaSign(signStr, zhihf_public_key, zhihpaysign);

                if (result == true)
                {
                    if (new UserChargeDAL().Update(order_no, trade_no, bank_seq_no) == false)
                    {
                        Log.ErrorFormat("更新系统支付订单失败，订单号: {0}", order_no);
                        Response.Write("更新系统支付订单失败");
                    }
                    else
                    {
                        Log.InfoFormat("支付成功，订单号: {0}", order_no);
                        Response.Write("支付成功");
                    }
                }
                else
                {
                    Log.ErrorFormat("第三方支付失败，订单号: {0}", order_no);
                    Response.Write("第三方支付失败");
                }
            }
            catch (Exception ex)
            {
                Log.ErrorFormat("支付失败: {0}", ex);
                Response.Write(ex.Message);
            }
        }
    }
}