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

namespace Lottery.IPhone.ZDB
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
                string adminId = ZdbHelper.Trim(Request["userId"]);
                this.UserId = adminId;
                //支付方式Id
                string chargeSetId = ZdbHelper.Trim(Request["setId"]);

                #region 组装智得宝参数
                //智得宝API版本
                string interface_version = ZdbHelper.ZDB_API_VERSION;
                //支付方式
                string service_type = ZdbHelper.Trim(Request["bank"]);
                //签名方式
                string sign_type = ZdbHelper.ZDB_SIGN;
                //商户Id
                string merchant_code = ZdbHelper.ZDB_USER;
                //订单号
                string orderId = ZdbHelper.Trim(Request["orderId"]);
                string order_no = string.IsNullOrEmpty(orderId) ? SsId.Charge : orderId;
                this.OrderId = order_no;

                //订单时间
                string order_time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                //支付金额
                decimal amount = 0m;
                decimal.TryParse(ZdbHelper.Trim(Request["amount"]), out amount);
                amount = 0.01m;
                string order_amount = amount.ToString("F2");
                //订单信息
                string product_name = "智得宝支付";
                string product_code = service_type;
                string product_num = "1";
                string product_desc = "智得宝支付";
                //附加参数
                string extra_return_param = "";
                string extend_param = "";
                //支付通知页面
                string notify_url = ZdbHelper.GetUrl("notify.aspx");
                //接入支付系统服务器IP
                string ip = Request.ServerVariables.Get("Local_Addr");
                string client_ip = !string.IsNullOrEmpty(ip) ? ip : "103.215.11.13";

                ////////////////组装签名参数//////////////////
                string signStr = "";
                if (client_ip != "")
                {
                    signStr = signStr + "client_ip=" + client_ip + "&";
                }
                if (extend_param != "")
                {
                    signStr = signStr + "extend_param=" + extend_param + "&";
                }
                if (extra_return_param != "")
                {
                    signStr = signStr + "extra_return_param=" + extra_return_param + "&";
                }
                if (interface_version != "")
                {
                    signStr = signStr + "interface_version=" + interface_version + "&";
                }
                if (merchant_code != "")
                {
                    signStr = signStr + "merchant_code=" + merchant_code + "&";
                }
                if (notify_url != "")
                {
                    signStr = signStr + "notify_url=" + notify_url + "&";
                }
                if (order_amount != "")
                {
                    signStr = signStr + "order_amount=" + order_amount + "&";
                }
                if (order_no != "")
                {
                    signStr = signStr + "order_no=" + order_no + "&";
                }
                if (order_time != "")
                {
                    signStr = signStr + "order_time=" + order_time + "&";
                }
                if (product_code != "")
                {
                    signStr = signStr + "product_code=" + product_code + "&";
                }
                if (product_desc != "")
                {
                    signStr = signStr + "product_desc=" + product_desc + "&";
                }
                if (product_name != "")
                {
                    signStr = signStr + "product_name=" + product_name + "&";
                }
                if (product_num != "")
                {
                    signStr = signStr + "product_num=" + product_num + "&";
                }
                if (service_type != "")
                {
                    signStr = signStr + "service_type=" + service_type;
                }

                //merchant private key 支付密钥
                string merchant_private_key = @"MIICdQIBADANBgkqhkiG9w0BAQEFAASCAl8wggJbAgEAAoGBALgva882+VP3twe7cd+uKTXnVtDesHga+Kn5Z3fbmNWCPZjCE4UOxE3DcqNkY1Nd7Umh3SdalYDk//+FXaTP+ImeUO1c2HHDop/vBbBKTUGJnfqU+BLOMERl/YVB/aO0KtvcUTvmrds4mAKFrqKpF5WPlSXtaML3UZ38SbnQDUwvAgMBAAECgYBGWk662svHyAIQoQexIew528CsjbMoXV0IR+y+upGZVGNE2zTriSVwcqxyPuE1sdX2Xy6DXrVmg5JJPt7zGkFbYN07hKwC4JZyDD0A8cjaFuXdnd7C+fp8kp9K2ZBOsdTCVOEjVOdPRmjtP0nelpJaXy01m2jnek+jVHWBNGiaUQJBAPFoazUPkLrIYtbtWF8VymdyefQLQZx2v5/KLUXcPtHkYKftqre+AiKLlXAxGYTXOad1kHK19wo4ZKs9i3QEGecCQQDDUYfaQI3d1ibazqECgSYKNa7LMpLdyoJrx3rec5eQpIr/gL29YrCU8nbrbXX+Otj2EP4unkXtFk2DUAG2JMJ5AkBySgAvb74SX9pDbLygz50ymoTYIBgg7itMiBgk8d+f2SJVfnSLZt514mWOZwBw3sBB4qvPUwyw/v/R/mIuO97TAkBfX/aYqqEbzDDY88FHuczbe29JJf71cqfQ/W2QJp3CMbb2IOWGDyTu9p7/Q0o0xIOhVJbqKLs4lIVxM6ZCTRzxAkA6S4nus23oeJHqeAUWj1q7Vb8Xixy5tMqxVHHpzCF1yjWiykG+CVkL7eB65/diuj1z94TmerAYrWXOQpf1ys6e";
                merchant_private_key = ZdbHelper.RSAPrivateKeyJava2DotNet(merchant_private_key);

                //签名
                string signData = HttpUtility.UrlEncode(ZdbHelper.RSASign(signStr, merchant_private_key));

                //支付请求参数
                string para = signStr + "&sign_type=" + sign_type + "&sign=" + signData;
                //请求响应
                string responseXml = ZdbHelper.HttpPost(ZdbHelper.ZDB_PAY_API, para);

                #endregion

                //支付
                Log.Info(String.Format("开始支付，订单号: {0}", orderId));
                Log.Debug("*系统支付参数********************************");
                Log.Debug(ZdbHelper.GetRequestData());

                //转换响应XML
                var xmlDoc = XElement.Load(new StringReader(responseXml));
                //获取支付地址
                var xmlData = xmlDoc.XPathSelectElement("/response/payURL");
                
                if (xmlData != null)
                {
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
                        Log.Info(String.Format("开始请求第三方支付: {0}?{1}", ZdbHelper.ZDB_PAY_API, para));
                        //Response.Redirect(String.Format("{0}?{1}", ZdbHelper.ZDB_PAY_API, para), false); //页面调转

                        string payUrl = Regex.Match(xmlData.ToString(), "(?<=>).*?(?=<)").Value;   //读起App支付地址
                        this.PayUrl = payUrl;
                        this.IsSuccess = true;
                        this.Message = "支付成功";

                    }
                }
                else
                {
                    Log.ErrorFormat("支付失败: {0}", responseXml);
                    this.IsSuccess = false;
                    this.Message = "支付失败";
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