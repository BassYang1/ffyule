using log4net;
using Lottery.DAL.Flex;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Lottery.FFApp.ZDB
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
                string merchant_code = Request.Form["merchant_code"].ToString().Trim();

                string notify_id = Request.Form["notify_id"].ToString().Trim();

                string notify_type = Request.Form["notify_type"].ToString().Trim();

                string interface_version = Request.Form["interface_version"].ToString().Trim();

                string sign_type = Request.Form["sign_type"].ToString().Trim();

                string zhihpaysign = Request.Form["sign"].ToString().Trim();

                string order_no = Request.Form["order_no"].ToString().Trim();

                string order_time = Request.Form["order_time"].ToString().Trim();

                string order_amount = Request.Form["order_amount"].ToString().Trim();

                string extra_return_param = Request.Form["extra_return_param"];

                string trade_no = Request.Form["trade_no"].ToString().Trim();

                string trade_time = Request.Form["trade_time"].ToString().Trim();

                string bank_seq_no = Request.Form["bank_seq_no"];

                string trade_status = Request.Form["trade_status"].ToString().Trim();

                //Array data
                string signStr = "";

                if (bank_seq_no != "")
                {
                    signStr = signStr + "bank_seq_no=" + bank_seq_no + "&";
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
                if (notify_id != "")
                {
                    signStr = signStr + "notify_id=" + notify_id + "&";
                }
                if (notify_type != "")
                {
                    signStr = signStr + "notify_type=" + notify_type + "&";
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
                if (trade_no != "")
                {
                    signStr = signStr + "trade_no=" + trade_no + "&";
                }
                if (trade_status != "")
                {
                    signStr = signStr + "trade_status=" + trade_status + "&";
                }
                if (trade_time != "")
                {
                    signStr = signStr + "trade_time=" + trade_time;
                }

                //merchant private key 支付密钥
                string zhihf_public_key = @"MIGfMA0GCSqGSIb3DQEBAQUAA4GNADCBiQKBgQC4L2vPNvlT97cHu3Hfrik151bQ3rB4Gvip+Wd325jVgj2YwhOFDsRNw3KjZGNTXe1Jod0nWpWA5P//hV2kz/iJnlDtXNhxw6Kf7wWwSk1BiZ36lPgSzjBEZf2FQf2jtCrb3FE75q3bOJgCha6iqReVj5Ul7WjC91Gd/Em50A1MLwIDAQAB";
                zhihf_public_key = ZdbHelper.RSAPrivateKeyJava2DotNet(zhihf_public_key);

                //check sign
                bool result = ZdbHelper.ValidateRsaSign(signStr, zhihf_public_key, zhihpaysign);

                if (result == true)
                {
                    if (new UserChargeDAL().Update(order_no) == false)
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