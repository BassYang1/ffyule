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

namespace Lottery.FFApp.WFP
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
        /// 二维码数据
        /// </summary>
        public string QrCode { get; set; }

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
                string input_charset_value = "UTF-8";
                parameters.Add("input_charset", input_charset_value);

                //微付API版本
                string interface_version_value = WfpHelper.WFP_API_VERSION;
                parameters.Add("interface_version", interface_version_value);

                //商户Id
                string merchant_code_value = WfpHelper.WFP_USER;
                parameters.Add("merchant_code", merchant_code_value);

                //支付金额
                decimal amount = 0m;
                decimal.TryParse(WfpHelper.Trim(Request["amount"]), out amount);
                //amount = 0.01m;
                string order_amount_value = amount.ToString("F2");
                parameters.Add("order_amount", order_amount_value);

                //订单号
                string orderId = WfpHelper.Trim(Request["orderId"]);
                string order_no_value = string.IsNullOrEmpty(orderId) ? SsId.Charge : orderId;
                //只能由字母、数字组成(订单号不合法)
                this.OrderId = order_no_value;
                order_no_value = order_no_value.Substring(0, 1) + order_no_value.Substring(2);
                parameters.Add("order_no", order_no_value);

                //订单时间
                string order_time_value = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                parameters.Add("order_time", order_time_value);

                //签名方式
                string sign_type_value = WfpHelper.WFP_SIGN;
                //parameters.Add("sign_type", sign_type_value);

                //订单信息
                string product_name_value = "网银微付支付";
                parameters.Add("product_name", product_name_value);

                string product_code_value = "网银微付支付";
                parameters.Add("product_code", product_code_value);

                string product_num_value = "1";
                parameters.Add("product_num", product_num_value);

                string product_desc_value = "网银微付支付";
                parameters.Add("product_desc", product_desc_value);

                //支付方式
                string service_type_value = "direct_pay";
                parameters.Add("service_type", service_type_value);

                //附加参数
                string extra_return_param_value = "";
                string extend_param_value = "";
                parameters.Add("extra_return_param", extra_return_param_value);
                parameters.Add("extend_param", extend_param_value);

                //是否检查客户端IP
                string client_ip_check_value = WfpHelper.WFP_CHECK_IP;
                parameters.Add("client_ip_check", client_ip_check_value);

                //是否检查客户端IP
                string redo_flag_value = "0";
                parameters.Add("redo_flag", redo_flag_value);

                //银行编号
                string bank_code_value = WfpHelper.Trim(Request["bank"]);

                //支付类型
                string pay_type_value = "b2c";
                if (bank_code_value.ToUpper().Equals("QQ"))
                {
                    bank_code_value = "";
                    pay_type_value = "tenpay_scan";
                }

                parameters.Add("bank_code", bank_code_value);
                parameters.Add("pay_type", pay_type_value);

                //商品展示页
                string show_url_value = "";
                parameters.Add("show_url", show_url_value);

                //支付异步通知页面
                string notify_url_value = WfpHelper.GetUrl("notify.aspx");
                parameters.Add("notify_url", notify_url_value);

                //支付返回页面
                string return_url_value = WfpHelper.GetUrl("result.aspx");
                //return_url_value = "";
                parameters.Add("return_url", return_url_value);

                //接入支付系统服务器IP
                string client_ip_value = WfpHelper.Trim(Request.ServerVariables.Get("REMOTE_ADDR"));
                //ip不能是localhost(参数校验异常)，不能是内网IP(不是商户真实IP)
                client_ip_value = "192.168.0.164";
                parameters.Add("client_ip", client_ip_value);

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

                //merchant private key 支付密钥
                string key = "MIICdQIBADANBgkqhkiG9w0BAQEFAASCAl8wggJbAgEAAoGBALgva882+VP3twe7cd+uKTXnVtDesHga+Kn5Z3fbmNWCPZjCE4UOxE3DcqNkY1Nd7Umh3SdalYDk//+FXaTP+ImeUO1c2HHDop/vBbBKTUGJnfqU+BLOMERl/YVB/aO0KtvcUTvmrds4mAKFrqKpF5WPlSXtaML3UZ38SbnQDUwvAgMBAAECgYBGWk662svHyAIQoQexIew528CsjbMoXV0IR+y+upGZVGNE2zTriSVwcqxyPuE1sdX2Xy6DXrVmg5JJPt7zGkFbYN07hKwC4JZyDD0A8cjaFuXdnd7C+fp8kp9K2ZBOsdTCVOEjVOdPRmjtP0nelpJaXy01m2jnek+jVHWBNGiaUQJBAPFoazUPkLrIYtbtWF8VymdyefQLQZx2v5/KLUXcPtHkYKftqre+AiKLlXAxGYTXOad1kHK19wo4ZKs9i3QEGecCQQDDUYfaQI3d1ibazqECgSYKNa7LMpLdyoJrx3rec5eQpIr/gL29YrCU8nbrbXX+Otj2EP4unkXtFk2DUAG2JMJ5AkBySgAvb74SX9pDbLygz50ymoTYIBgg7itMiBgk8d+f2SJVfnSLZt514mWOZwBw3sBB4qvPUwyw/v/R/mIuO97TAkBfX/aYqqEbzDDY88FHuczbe29JJf71cqfQ/W2QJp3CMbb2IOWGDyTu9p7/Q0o0xIOhVJbqKLs4lIVxM6ZCTRzxAkA6S4nus23oeJHqeAUWj1q7Vb8Xixy5tMqxVHHpzCF1yjWiykG+CVkL7eB65/diuj1z94TmerAYrWXOQpf1ys6e";
                string merchant_private_key = WfpHelper.RSAPrivateKeyJava2DotNet(key);
                
                //signStr = "client_ip=120.237.123.242&extra_return_param=pay&interface_version=V3.1&merchant_code=9000804143&notify_url=http://192.168.0.164:8018/ZHFToMer_notify.aspx&order_amount=0.01&order_no=486&order_time=2018-03-03 05:00:19&product_code=A1&product_desc=test&product_name=book&product_num=12&service_type=weixin_scan";
                //签名
                string signData = WfpHelper.RSASign(signStr, merchant_private_key);
                //signData = HttpUtility.UrlEncode(signData);

                Log.Debug("*签名********************************");
                Log.Debug(signData);

                //支付请求参数
                Log.Info(String.Format("开始支付，订单号: {0}", orderId));
                Log.Debug("*系统支付参数********************************");
                requestStr = requestStr + "sign_type=" + sign_type + "&sign=" + signData;
                Log.Debug(requestStr);

                #endregion

                //支付
                int num = (new UserChargeDAL()).Save(adminId, orderId, chargeSetId, bank_code_value, Convert.ToDecimal(order_amount_value));

                //成功
                if (num == -1)
                {
                    Log.Error("充值金额不能小于最小充值金额!");
                    Response.Write("充值金额不能小于最小充值金额!");
                }
                //失败
                else if (num > 0)
                {
                    Log.Info(String.Format("开始请求第三方支付: {0}?{1}", WfpHelper.WFP_PAY_API, requestStr));

                    payForm.Action = WfpHelper.WFP_PAY_API;
                    sign.Value = signData;
                    merchant_code.Value = merchant_code_value;
                    bank_code.Value = bank_code_value;
                    order_no.Value = order_no_value;
                    order_amount.Value = order_amount_value;
                    service_type.Value = service_type_value;
                    input_charset.Value = input_charset_value;
                    notify_url.Value = notify_url_value;
                    interface_version.Value = interface_version_value;
                    sign_type.Value = sign_type_value;
                    order_time.Value = order_time_value;
                    product_name.Value = product_name_value;
                    client_ip.Value = client_ip_value;
                    client_ip_check.Value = client_ip_check_value;
                    extend_param.Value = extend_param_value;
                    extra_return_param.Value = extra_return_param_value;
                    product_code.Value = product_code_value;
                    product_desc.Value = product_desc_value;
                    product_num.Value = product_num_value;
                    return_url.Value = return_url_value;
                    show_url.Value = show_url_value;
                    redo_flag.Value = redo_flag_value;
                    pay_type.Value = pay_type_value;

                    Page.ClientScript.RegisterStartupScript(this.GetType(), "payform", "document.getElementById(\"payForm\").submit()", true); 
                }

                //payForm.Action = WfpHelper.WFP_PAY_API;
                //sign.Value = "DgYZnJymXunKqSsW86UhrKJ3ui7dsvQE9vXpJvQZVQks11bCHA5NmgLcLz+glPvnJr4PW2EeImrHgX85DYudZZ62B3X/pTTT4t6zBZvdBL7u0VNjkGChuol864fCyfPr0vVeeqoMeuA/hwQwTGzPkTTdDg30sypYrpi3STF2y4o=";
                //merchant_code.Value = "108016002010";
                //bank_code.Value = "";
                //order_no.Value = "C5644456534678162925";
                //order_amount.Value = "0.01";
                //service_type.Value = "direct_pay";
                //input_charset.Value = "UTF-8";
                //notify_url.Value = "http://192.168.0.164:8016/pay/weifupay/notify.aspx";
                //interface_version.Value = "V3.0";
                //sign_type.Value = "RSA-S";
                //order_time.Value = "2015-01-01 14:20:55";
                //product_name.Value = "网银微付支付";
                //client_ip.Value = "114.115.146.222";
                //client_ip_check.Value = "0";
                //extend_param.Value = "";
                //extra_return_param.Value = "";
                //product_code.Value = "网银微付支付";
                //product_desc.Value = "网银微付支付";
                //product_num.Value = "1";
                //return_url.Value = "";
                //show_url.Value = "";
                //redo_flag.Value = "0";
                //pay_type.Value = "b2c";
            }
            catch (Exception ex)
            {
                Log.ErrorFormat("支付失败: {0}", ex);
                Response.Write(string.Format("支付失败: {0}", ex));
                this.IsSuccess = false;
                this.Message = ex.Message;
            }
        }
    }
}