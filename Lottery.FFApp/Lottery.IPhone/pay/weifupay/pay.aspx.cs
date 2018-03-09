using log4net;
using Lottery.DAL.Flex;
using Lottery.Entity;
using Lottery.Utils;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;

namespace Lottery.IPhone.WFP
{
    /// <summary>
    /// 支付页面
    /// </summary>
    public partial class pay : Page
    {
        /// <summary>
        /// Log instance.
        /// </summary>
        protected static readonly ILog Log = LogManager.GetLogger(typeof(pay));

        /// <summary>
        /// 支付结果
        /// </summary>
        public bool IsSuccess { get; set; }

        /// <summary>
        /// 支付消息
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 调起APP支付地址
        /// </summary>
        public string PayUrl { get; set; }

        /// <summary>
        /// 用户Id
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// 订单Id
        /// </summary>
        public string OrderId { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                //用户Id
                string adminId = WfpHelper.Trim(Request["userId"]);
                this.UserId = adminId;
                //支付方式Id
                string chargeSetId = WfpHelper.Trim(Request["setId"]);

                #region 组装微付参数
                var parameters = new SortedDictionary<string, string>();

                //微付API版本
                string interface_version = WfpHelper.WFP_API_VERSION;
                parameters.Add("interface_version", interface_version);

                //支付方式
                string service_type = WfpHelper.Trim(Request["bank"]);
                parameters.Add("service_type", service_type);

                //签名方式
                string sign_type = WfpHelper.WFP_SIGN;
                //parameters.Add("sign_type", sign_type);

                //商户Id
                string merchant_code = WfpHelper.WFP_USER;
                parameters.Add("merchant_code", merchant_code);

                //订单号
                string orderId = WfpHelper.Trim(Request["orderId"]);
                string order_no = string.IsNullOrEmpty(orderId) ? SsId.Charge : orderId;
                this.OrderId = order_no;
                order_no = order_no.Substring(0, 1) + order_no.Substring(2);
                parameters.Add("order_no", order_no);

                //订单时间
                string order_time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                parameters.Add("order_time", order_time);

                //支付金额
                decimal amount = 0m;
                decimal.TryParse(WfpHelper.Trim(Request["amount"]), out amount);
                //amount = 0.01m;
                string order_amount = amount.ToString("F2");
                parameters.Add("order_amount", order_amount);

                //订单信息
                string product_name = "微付支付";
                string product_code = service_type;
                string product_num = "1";
                string product_desc = "微付支付";
                parameters.Add("product_name", product_name);
                parameters.Add("product_code", product_code);
                parameters.Add("product_num", product_num);
                parameters.Add("product_desc", product_desc);

                //附加参数
                string extra_return_param = "";
                string extend_param = "";
                parameters.Add("extra_return_param", extra_return_param);
                parameters.Add("extend_param", extend_param);

                //支付通知页面
                string notify_url = WfpHelper.GetUrl("notify.aspx");
                parameters.Add("notify_url", notify_url);

                //接入支付系统服务器IP
                string client_ip = WfpHelper.Trim(Request.ServerVariables.Get("REMOTE_ADDR"));
                //ip不能是localhost(参数校验异常)，不能是内网IP(不是商户真实IP)
                client_ip = "114.115.146.222";
                parameters.Add("client_ip", client_ip);

                //签名
                string signStr = "";
                string requestStr = "";

                foreach (var p in parameters)
                {
                    requestStr = string.Format("{0}{1}={2}&", requestStr, p.Key, p.Value);

                    if (!string.IsNullOrEmpty(p.Value))
                    {
                        signStr = string.Format("{0}{1}={2}&", signStr, p.Key, p.Value);
                    }
                }

                signStr = signStr.Substring(0, signStr.Length - 1);

                Log.Debug("*签名参数********************************");
                Log.Debug(signStr);

                #endregion

                //merchant private key 支付密钥
                string merchant_private_key = @"MIICdQIBADANBgkqhkiG9w0BAQEFAASCAl8wggJbAgEAAoGBALgva882+VP3twe7
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
                merchant_private_key = WfpHelper.RSAPrivateKeyJava2DotNet(merchant_private_key);

                //签名
                string signData = HttpUtility.UrlEncode(WfpHelper.RSASign(signStr, merchant_private_key));

                //支付请求参数
                string para = signStr + "&sign_type=" + sign_type + "&sign=" + signData;

                //支付
                int num = (new UserChargeDAL()).Save(adminId, orderId, chargeSetId, service_type, Convert.ToDecimal(order_amount));

                //失败
                if (num == -1)
                {
                    Log.Error("充值金额不能小于最小充值金额!");
                    this.IsSuccess = false;
                    this.Message = "充值金额不能小于最小充值金额";
                }
                //成功
                else if (num > 0)
                {
                    Log.Info(String.Format("开始请求第三方支付: {0}?{1}", WfpHelper.WFP_PAY_API, para));
                    string responseXml = WfpHelper.HttpPost(WfpHelper.WFP_PAY_API, para);

                    //支付
                    Log.Info(String.Format("开始支付，订单号: {0}", orderId));
                    Log.Debug("*系统支付参数********************************");
                    Log.Debug(WfpHelper.GetRequestData());

                    //转换响应XML
                    var xmlDoc = XElement.Load(new StringReader(responseXml));
                    //获取支付地址
                    var xmlData = xmlDoc.XPathSelectElement("/response/payURL");

                    //请求响应
                    if (xmlData != null)
                    {
                        string payUrl = Regex.Match(xmlData.ToString(), "(?<=>).*?(?=<)").Value;   //读起App支付地址
                        this.PayUrl = System.Web.HttpUtility.UrlDecode(payUrl);
                        this.IsSuccess = true;
                        this.Message = "支付成功";

                        Log.Info(String.Format("移动支付{0}连接: {1}", service_type, this.PayUrl));

                        //Response.Redirect(this.PayUrl, false);
                        //Response.Write("<script language='javascript'>window.open('" + this.PayUrl + "');window.opener = null; window.open('', '_self'); window.close();</script>");
                    }
                    else
                    {
                        Log.ErrorFormat("支付失败: {0}", responseXml);
                        this.IsSuccess = false;
                        this.Message = "支付失败";
                    }
                }
            }
            catch (Exception ex)
            {
                Log.ErrorFormat("支付失败: {0}", ex);
                this.IsSuccess = false;
                this.Message = ex.Message;
            }
        }
    }
}