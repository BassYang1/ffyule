using log4net;
using Lottery.DAL.Flex;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Lottery.IPhone.WFP
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

                string zhihpaysign = WfpHelper.Trim(Request["sign"]);
                //parameters.Add("zhihpaysign", zhihpaysign);

                string order_no = WfpHelper.Trim(Request["order_no"]);
                parameters.Add("order_no", order_no);
                order_no = order_no.Substring(0, 1) + "_" + order_no.Substring(1);

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
                string zhihf_public_key = @"MIICdQIBADANBgkqhkiG9w0BAQEFAASCAl8wggJbAgEAAoGBALgva882+VP3twe7
cd+uKTXnVtDesHga+Kn5Z3fbmNWCPZjCE4UOxE3DcqNkY1Nd7Umh3SdalYDk//+F
XaTP+ImeUO1c2HHDop/vBbBKTUGJnfqU+BLOMERl/YVB/aO0KtvcUTvmrds4mAKF
rqKpF5WPlSXtaML3UZ38SbnQDUwvAgMBAAECgYBGWk662svHyAIQoQexIew528Cs
jbMoXV0IR+y+upGZVGNE2zTriSVwcqxyPuE1sdX2Xy6DXrVmg5JJPt7zGkFbYN07
hKwC4JZyDD0A8cjaFuXdnd7C+fp8kp9K2ZBOsdTCVOEjVOdPRmjtP0nelpJaXy01
m2jnek+jVHWBNGiaUQJBAPFoazUPkLrIYtbtWF8VymdyefQLQZx2v5/KLUXcPtHk
YKftqre+AiKLlXAxGYTXOad1kHK19wo4ZKs9i3QEGecCQQDDUYfaQI3d1ibazqEC
gSYKNa7LMpLdyoJrx3rec5eQpIr/gL29YrCU8nbrbXX+Otj2EP4unkXtFk2DUAG2
JMJ5AkBySgAvb74SX9pDbLygz50ymoTYIBgg7itMiBgk8d+f2SJVfnSLZt514mWO
ZwBw3sBB4qvPUwyw/v/R/mIuO97TAkBfX/aYqqEbzDDY88FHuczbe29JJf71cqfQ
/W2QJp3CMbb2IOWGDyTu9p7/Q0o0xIOhVJbqKLs4lIVxM6ZCTRzxAkA6S4nus23o
eJHqeAUWj1q7Vb8Xixy5tMqxVHHpzCF1yjWiykG+CVkL7eB65/diuj1z94TmerAY
rWXOQpf1ys6e";

                //zhihf_public_key = WfpHelper.RSAPublicKeyJava2DotNet(zhihf_public_key);

                //check sign
                bool result = false;
                //result = WfpHelper.ValidateRsaSign(signStr, zhihf_public_key, zhihpaysign);

                if (result || trade_status.Equals("SUCCESS", StringComparison.OrdinalIgnoreCase))
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