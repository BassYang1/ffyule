using System;
using System.Collections;
using System.Text;
using LitJson;
using Lottery.DAL;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using log4net;
using System.Configuration;

namespace Lottery.Collect
{
    public class JsonDataModel
    {
        public string Number { get; set; }
        public string DateLine { get; set; }
        public string DataQs { get; set; }
    }

    public class QqSscData
    {
        /// <summary>
        /// Log instance.
        /// </summary>
        protected static readonly ILog Log = LogManager.GetLogger(typeof(QqSscData));

        private static LotteryDataDAL _dal = new LotteryDataDAL();

        private static string TxffcAPI = "";

        static QqSscData()
        {
            if (ConfigurationManager.AppSettings["TxffcAPI"] != null)
            {
                TxffcAPI = ConfigurationManager.AppSettings["TxffcAPI"].ToString();
            }
        }


        public static void QqSsc()
        {
            try
            {
                if (string.IsNullOrEmpty(TxffcAPI))
                {
                    Log.Debug("未配腾迅分分彩API");
                    return;
                }

                //http://api.b1cp.com/t?p=json&t=qqffc&token=FF446B723EB25993
                //http://www.b1cp.com/api?p=json&t=txffc&limit=1&token=00fb782bad8e5241
                //Log.Debug("开始QqSsc...");
                string text = HtmlOperate2.HttpGet(TxffcAPI, Encoding.UTF8);

                if (text.IndexOf("opentime") < 0 && text.IndexOf("expect") < 0 && text.IndexOf("opencode") < 0)
                {
                    Log.DebugFormat("开奖数据无效: {0}", text);
                    return;
                }

                text = text.Substring(8, text.Length - 9);
                text = "{\"rows\":5,\"data\":" + text + "}";
                JsonData jsonData = JsonMapper.ToObject(text);
                foreach (JsonData jsonData2 in ((IEnumerable)jsonData["data"]))
                {
                    string openTime = jsonData2["opentime"].ToString();
                    string openCode = jsonData2["opencode"].ToString();
                    string expect = jsonData2["expect"].ToString(); //期号

                    if (string.IsNullOrEmpty(openTime) || string.IsNullOrEmpty(openCode) || string.IsNullOrEmpty(expect))
                    {
                        Log.ErrorFormat("腾讯分分彩找不到开奖数据的关键字符: {0}", text);
                        //Dal.Save("采集异常", "腾讯分分彩找不到开奖数据的关键字符");
                        break;
                    }

                    expect = expect.Substring(0, 8) + "-" + expect.Substring(8);

                    if (_dal.Update(1005, expect, openCode, openTime, openCode))
                    {
                        Log.DebugFormat("腾迅分分彩新一期开奖信息: {0}", text);
                        Public.SaveLotteryData2File(1005);
                        LotteryCheck.RunOfIssueNum(1005, expect);
                    }
                }
            }
            catch (Exception ex)
            {
                Log.ErrorFormat("腾讯分分彩异常: {0}", ex);
                //new LogExceptionDAL().Save("采集异常", "腾讯分分彩获取开奖数据出错，错误代码：" + ex.Message);
            }
        }
    }
}
