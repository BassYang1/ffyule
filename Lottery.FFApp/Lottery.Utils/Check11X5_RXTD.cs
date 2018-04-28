// Decompiled with JetBrains decompiler
// Type: Lottery.Utils.Check11X5_RXDS
// Assembly: Lottery.Utils, Version=1.0.1.1, Culture=neutral, PublicKeyToken=null
// MVID: E7A9C185-AF0A-4444-AE46-9A73782D0A74
// Assembly location: F:\pros\tianheng\bf\WebAppOld\bin\Lottery.Utils.dll

using System;
using System.Collections.Generic;
using System.Linq;

namespace Lottery.Utils
{
    public static class Check11X5_RXTD
    {
        /// <summary>
        /// 任选拖胆
        /// </summary>
        /// <param name="lotNumber">开奖号码</param>
        /// <param name="CheckNumber">下注号码</param>
        /// <param name="type">开奖类型</param>
        /// <returns></returns>
        public static int RXTD(string lotNumber, string CheckNumber, string type)
        {
            if (string.IsNullOrEmpty(CheckNumber) || string.IsNullOrEmpty(lotNumber))
            {
                return 0;
            }

            string danmaNum = "";
            string[] tuomaNums = null;
            string[] userNums = CheckNumber.Split('_');
            string[] lotNums = lotNumber.Split(',');

            if (userNums.Length != 2)
            {
                return 0;
            }

            danmaNum = userNums[0];

            if (lotNums.Count(n => n == danmaNum) < 1) //是否中胆码
            {
                return 0;
            }

            tuomaNums = userNums[0].Split(',');


            int count = 0;

            foreach (string num in tuomaNums)
            {
                if (lotNums.Count(n => n == num) > 0)
                {
                    count++;
                }
            }

            if (type == "2")
            {
                return count;
            }
            else if (type == "3" && count >= 2)
            {
                return (count * (count - 1)) / (2 * 1);
            }
            else if (type == "4" && count >= 3)
            {
                return (count * (count - 1) * (count - 2)) / (3 * 2 * 1);
            }
            else if (count >= 4) //拖胆5,6,7,8
            {
                return (count * (count - 1) * (count - 2) * (count - 3)) / (4 * 3 * 2 * 1);
            }

            return 0;
        }
    }
}
