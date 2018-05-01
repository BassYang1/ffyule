using log4net;
using Lottery.DAL;
using Lottery.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Lottery.Collect.Sys
{
    /// <summary>
    /// 六合彩测试
    /// </summary>
    public class SysHk6TestData : SysBase
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(SysHk6TestData));
        private static SysBase Lottery = new SysHk6TestData();

        public SysHk6TestData()
            : base("hk6", "六合彩测试")
        {
            base.NumberCount = 7;
            base.NumberAllCount = 7;
            base.NumberAllSize = 2;
            Log.DebugFormat("彩种: {0}, {1}", base.Name, base.Code);
        }

        /// <summary>
        /// 生成彩票开奖信息
        /// </summary>
        /// <returns></returns>
        public override void Generate()
        {
            string[] source = { "1", "13", "25", "37", "49", "12", "24", "36", "48", "11", "23", "35", "47", "10", "22", "34", "46", "9", "21", "33", "45", "8", "20", "32", "44", "7", "19", "31", "43", "6", "18", "30", "42", "5", "17", "29", "41", "4", "16", "28", "40", "3", "15", "27", "39", "2", "14", "26", "38" };
            string[] numAllArr = GetRandomNums(source, 7, false);

            base.NumberAll = string.Join(",", numAllArr);
            base.Number = base.NumberAll;
            Log.DebugFormat("生成开奖号码: {0} {1}", base.Name, base.Number);
        }

        /// <summary>
        /// 更新开奖信息
        /// </summary>
        public static void UpdateData(object code = null)
        {
            try
            {
                //更新开奖期号
                Lottery.UpdateExpectTest();

                if (string.IsNullOrEmpty(Lottery.LastExpect) || !Lottery.LastExpect.Equals(Lottery.ExpectNo))
                {
                    Lottery.LastExpect = Lottery.ExpectNo;
                    Lottery.UpdateLottery();
                    Log.DebugFormat("开奖期数: {0} {1}", Lottery.Name, Lottery.ExpectNo);
                }
            }
            catch (Exception ex)
            {
                Log.ErrorFormat("开奖发生异常: {0} {1}", Lottery.Name, ex);
                //new LogExceptionDAL().Save("采集异常", "腾讯分分彩获取开奖数据出错，错误代码：" + ex.Message);
            }
        }
    }
}
   