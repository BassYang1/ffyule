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
using Lottery.Collect.Boyi;
using Lottery.Entity;

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
        private static LotteryDAL _lotteryDal = new LotteryDAL();

        public static void QqSsc()
        {
            try
            {
                //http://api.b1cp.com/t?p=json&t=qqffc&token=FF446B723EB25993
                //http://www.b1cp.com/api?p=json&t=txffc&limit=1&token=00fb782bad8e5241
                //Log.Debug("开始QqSsc...");
                
                SysLotteryModel sysLottery = _lotteryDal.GetSysLotteryByCode("qqffc");

                if (sysLottery == null || string.IsNullOrEmpty(sysLottery.ApiUrl))
                {
                    throw new Exception("无效的API配置");
                }

                IList<ByLottery> data = ByHelper.GetLotteryData(sysLottery.ApiUrl);

                foreach (ByLottery lot in data)
                {
                    string openTime = lot.opentime;
                    string openCode = lot.opencode;
                    string expect = lot.expect; //期号

                    expect = expect.Substring(0, 8) + "-" + expect.Substring(8);

                    if (_dal.Update(1005, expect, openCode, openTime, openCode))
                    {
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
