﻿using log4net;
using Lottery.DAL;
using Lottery.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Lottery.Collect.Sys
{
    /// <summary>
    /// 英国60秒赛车
    /// </summary>
    public class SysYg60scData : SysBase
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(SysYg60scData));
        private static SysBase Lottery = new SysYg60scData();

        public SysYg60scData()
            : base("yg60sc")
        {
            base.NumberCount = 10;
            base.NumberAllCount = 10;
            base.NumberAllSize = 2;
        }

        /// <summary>
        /// 生成彩票开奖信息
        /// </summary>
        /// <returns></returns>
        public override void Generate()
        {
            string[] source = { "02", "01", "03", "06", "07", "09", "04", "06", "10", "05", "08", "02", "01", "03", "10", "07", "09", "04", "06", "10", "05", "08" };
            string[] numAllArr = GetRandomNums(source, 10, false);

            base.NumberAll = string.Join(",", numAllArr);
            base.Number = base.NumberAll;
        }

        /// <summary>
        /// 更新开奖信息
        /// </summary>
        public static void UpdateData(object code = null)
        {
            try
            {
                //更新开奖期号
                Lottery.UpdateExpect();

                if (string.IsNullOrEmpty(Lottery.LastExpect) || !Lottery.LastExpect.Equals(Lottery.ExpectNo))
                {
                    Lottery.LastExpect = Lottery.ExpectNo;
                    Lottery.UpdateLottery();
                }
            }
            catch (Exception ex)
            {
                Log.ErrorFormat("英国60秒赛车: {0}", ex);
                //new LogExceptionDAL().Save("采集异常", "腾讯分分彩获取开奖数据出错，错误代码：" + ex.Message);
            }
        }
    }
}
   