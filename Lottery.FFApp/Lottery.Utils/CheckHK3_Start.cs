// Decompiled with JetBrains decompiler
// Type: Lottery.Utils.CheckSSC_2Start
// Assembly: Lottery.Utils, Version=1.0.1.1, Culture=neutral, PublicKeyToken=null
// MVID: E7A9C185-AF0A-4444-AE46-9A73782D0A74
// Assembly location: F:\pros\tianheng\bf\WebAppOld\bin\Lottery.Utils.dll

using System;
using System.Collections.Generic;
using System.Linq;

namespace Lottery.Utils
{
    /// <summary>
    /// 六合彩
    /// </summary>
    public static class CheckHK3_Start
    {
        private static int[] Hong = { 01, 02, 07, 08, 12, 13, 18, 19, 23, 24, 29, 30, 34, 35, 40, 45, 46 };
        private static int[] Lan = { 03, 04, 09, 10, 14, 15, 20, 25, 26, 31, 36, 37, 41, 42, 47, 48 };
        private static int[] Lv = { 05, 06, 11, 16, 17, 21, 22, 27, 28, 32, 33, 38, 39, 43, 44, 49 };

        /// <summary>
        /// 是否和局
        /// </summary>
        /// <param name="lotNumber"></param>
        /// <returns></returns>
        public static bool isDraw(string lotNumber)
        {
            if (string.IsNullOrEmpty(lotNumber))
            {
                return false;
            }

            string[] lotNums = lotNumber.Split(',');

            if (lotNums.Length != 7)
            {
                return false;
            }

            string tm = lotNums[6];

            if (tm == "49")
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// 特码
        /// </summary>
        /// <param name="CheckNumber"></param>
        /// <param name="lotNumber"></param>
        /// <returns></returns>
        public static int TM(string lotNumber, string CheckNumber)
        {
            if (string.IsNullOrEmpty(CheckNumber) || string.IsNullOrEmpty(lotNumber))
            {
                return 0;
            }

            string[] userNums = CheckNumber.Split('_');
            string[] lotNums = lotNumber.Split(',');

            if (lotNums.Length != 7 || lotNums[6] == "49")
            {
                return 0;
            }

            return userNums.Count(n => n == lotNums[6]);
        }

        /// <summary>
        /// 特码大小
        /// </summary>
        /// <param name="CheckNumber"></param>
        /// <param name="lotNumber"></param>
        /// <returns></returns>
        public static int TMDX(string lotNumber, string CheckNumber)
        {
            if (string.IsNullOrEmpty(CheckNumber) || string.IsNullOrEmpty(lotNumber))
            {
                return 0;
            }

            string[] userNums = CheckNumber.Split('_');
            string[] lotNums = lotNumber.Split(',');

            if (lotNums.Length != 7 || lotNums[6] == "49")
            {
                return 0;
            }

            int tm = Convert.ToInt32(lotNums[6]);
            if (tm >= 1 && tm <= 24 && userNums.Count(n => n == "小") > 0)
            {
                return 1;
            }
            else if (tm >= 25 && tm <= 48 && userNums.Count(n => n == "大") > 0)
            {
                return 1;
            }

            return 0;
        }

        /// <summary>
        /// 特码单双
        /// </summary>
        /// <param name="CheckNumber"></param>
        /// <param name="lotNumber"></param>
        /// <returns></returns>
        public static int TMDS(string lotNumber, string CheckNumber)
        {
            if (string.IsNullOrEmpty(CheckNumber) || string.IsNullOrEmpty(lotNumber))
            {
                return 0;
            }

            string[] userNums = CheckNumber.Split('_');
            string[] lotNums = lotNumber.Split(',');

            if (lotNums.Length != 7 || lotNums[6] == "49")
            {
                return 0;
            }

            int tm = Convert.ToInt32(lotNums[6]);
            if ((tm % 2) == 0 && userNums.Count(n => n == "双") > 0)
            {
                return 1;
            }
            else if ((tm % 2) != 1 && userNums.Count(n => n == "单") > 0)
            {
                return 1;
            }

            return 0;
        }

        /// <summary>
        /// 合数大小
        /// </summary>
        /// <param name="CheckNumber"></param>
        /// <param name="lotNumber"></param>
        /// <returns></returns>
        public static int TMHDX(string lotNumber, string CheckNumber)
        {
            if (string.IsNullOrEmpty(CheckNumber) || string.IsNullOrEmpty(lotNumber))
            {
                return 0;
            }

            string[] userNums = CheckNumber.Split('_');
            string[] lotNums = lotNumber.Split(',');

            if (lotNums.Length != 7 || lotNums[6] == "49")
            {
                return 0;
            }

            string tm = lotNums[6];
            int sum = tm.Length > 1
                ? Convert.ToInt32(tm.Substring(0, 1)) + Convert.ToInt32(tm.Substring(1, 1))
                : Convert.ToInt32(tm.Substring(0, 1));

            if (sum <= 6 && userNums.Count(n => n == "小") > 0)
            {
                return 1;
            }
            else if (sum >= 7 && userNums.Count(n => n == "大") > 0)
            {
                return 1;
            }

            return 0;
        }

        /// <summary>
        /// 特码单双
        /// </summary>
        /// <param name="CheckNumber"></param>
        /// <param name="lotNumber"></param>
        /// <returns></returns>
        public static int TMHDS(string lotNumber, string CheckNumber)
        {
            if (string.IsNullOrEmpty(CheckNumber) || string.IsNullOrEmpty(lotNumber))
            {
                return 0;
            }

            string[] userNums = CheckNumber.Split('_');
            string[] lotNums = lotNumber.Split(',');

            if (lotNums.Length != 7 || lotNums[6] == "49")
            {
                return 0;
            }

            string tm = lotNums[6];
            int sum = tm.Length > 1
                ? Convert.ToInt32(tm.Substring(0, 1)) + Convert.ToInt32(tm.Substring(1, 1))
                : Convert.ToInt32(tm.Substring(0, 1));


            if ((sum % 2) == 0 && userNums.Count(n => n == "双") > 0)
            {
                return 1;
            }
            else if ((sum % 2) != 1 && userNums.Count(n => n == "单") > 0)
            {
                return 1;
            }

            return 0;
        }

        /// <summary>
        /// 合数大小
        /// </summary>
        /// <param name="CheckNumber"></param>
        /// <param name="lotNumber"></param>
        /// <returns></returns>
        public static int TMWDX(string lotNumber, string CheckNumber)
        {
            if (string.IsNullOrEmpty(CheckNumber) || string.IsNullOrEmpty(lotNumber))
            {
                return 0;
            }

            string[] userNums = CheckNumber.Split('_');
            string[] lotNums = lotNumber.Split(',');

            if (lotNums.Length != 7 || lotNums[6] == "49")
            {
                return 0;
            }

            string tm = lotNums[6];
            int ws = tm.Length > 1
                ? Convert.ToInt32(tm.Substring(1, 1))
                : Convert.ToInt32(tm.Substring(0, 1));

            if (ws <= 4 && userNums.Count(n => n == "小") > 0)
            {
                return 1;
            }
            else if (ws >= 5 && userNums.Count(n => n == "大") > 0)
            {
                return 1;
            }

            return 0;
        }

        /// <summary>
        /// 特码单双
        /// </summary>
        /// <param name="CheckNumber"></param>
        /// <param name="lotNumber"></param>
        /// <returns></returns>
        public static int TMWDS(string lotNumber, string CheckNumber)
        {
            if (string.IsNullOrEmpty(CheckNumber) || string.IsNullOrEmpty(lotNumber))
            {
                return 0;
            }

            string[] userNums = CheckNumber.Split('_');
            string[] lotNums = lotNumber.Split(',');

            if (lotNums.Length != 7 || lotNums[6] == "49")
            {
                return 0;
            }

            string tm = lotNums[6];
            int ws = tm.Length > 1
                ? Convert.ToInt32(tm.Substring(1, 1))
                : Convert.ToInt32(tm.Substring(0, 1));


            if ((ws % 2) == 0 && userNums.Count(n => n == "双") > 0)
            {
                return 1;
            }
            else if ((ws % 2) != 1 && userNums.Count(n => n == "单") > 0)
            {
                return 1;
            }

            return 0;
        }

        /// <summary>
        /// 特码头数
        /// </summary>
        /// <param name="CheckNumber"></param>
        /// <param name="lotNumber"></param>
        /// <returns></returns>
        public static int TMTS(string lotNumber, string CheckNumber)
        {
            if (string.IsNullOrEmpty(CheckNumber) || string.IsNullOrEmpty(lotNumber))
            {
                return 0;
            }

            string[] userNums = CheckNumber.Split('_');
            string[] lotNums = lotNumber.Split(',');

            if (lotNums.Length != 7 || lotNums[6] == "49")
            {
                return 0;
            }

            string tm = lotNums[6];
            string ts = tm.Length > 1
                ? tm.Substring(0, 1)
                : "0";

            return userNums.Count(n => n == ts);
        }

        /// <summary>
        /// 特码属数
        /// </summary>
        /// <param name="CheckNumber"></param>
        /// <param name="lotNumber"></param>
        /// <returns></returns>
        public static int TMWS(string lotNumber, string CheckNumber)
        {
            if (string.IsNullOrEmpty(CheckNumber) || string.IsNullOrEmpty(lotNumber))
            {
                return 0;
            }

            string[] userNums = CheckNumber.Split('_');
            string[] lotNums = lotNumber.Split(',');

            if (lotNums.Length != 7 || lotNums[6] == "49")
            {
                return 0;
            }

            string tm = lotNums[6];
            string ws = tm.Length > 1
                ? tm.Substring(1, 1)
                : tm.Substring(0, 1);

            return userNums.Count(n => n == ws);
        }

        /// <summary>
        /// 特码半特
        /// </summary>
        /// <param name="CheckNumber"></param>
        /// <param name="lotNumber"></param>
        /// <returns></returns>
        public static int TMBT(string lotNumber, string CheckNumber)
        {
            if (string.IsNullOrEmpty(CheckNumber) || string.IsNullOrEmpty(lotNumber))
            {
                return 0;
            }

            string[] lotNums = lotNumber.Split(',');

            if (lotNums.Length != 7 || lotNums[6] == "49")
            {
                return 0;
            }

            int tm = Convert.ToInt32(lotNums[6]);
            string dx = tm <= 24 ? "小" : "大";
            string ds = tm % 2 == 0 ? "双" : "单";

            if (CheckNumber.Contains(dx) && CheckNumber.Contains(ds))
            {
                return 1;
            }

            return 0;
        }

        /// <summary>
        /// 色波
        /// </summary>
        /// <param name="CheckNumber"></param>
        /// <param name="lotNumber"></param>
        /// <returns></returns>
        public static int TMSB(string lotNumber, string CheckNumber)
        {
            if (string.IsNullOrEmpty(CheckNumber) || string.IsNullOrEmpty(lotNumber))
            {
                return 0;
            }

            string[] lotNums = lotNumber.Split(',');

            if (lotNums.Length != 7 || lotNums[6] == "49")
            {
                return 0;
            }

            int tm = Convert.ToInt32(lotNums[6]);

            if (Hong.Contains(tm) && CheckNumber.Contains("红"))
            {
                return 1;
            }
            else if (Lan.Contains(tm) && CheckNumber.Contains("蓝"))
            {
                return 1;
            }
            else if (Lv.Contains(tm) && CheckNumber.Contains("绿"))
            {
                return 1;
            }

            return 0;
        }

        /// <summary>
        /// 特码半波
        /// </summary>
        /// <param name="CheckNumber"></param>
        /// <param name="lotNumber"></param>
        /// <returns></returns>
        public static int TMBB(string lotNumber, string CheckNumber)
        {
            if (string.IsNullOrEmpty(CheckNumber) || string.IsNullOrEmpty(lotNumber))
            {
                return 0;
            }

            string[] lotNums = lotNumber.Split(',');

            if (lotNums.Length != 7 || lotNums[6] == "49")
            {
                return 0;
            }

            int num = 0;
            int tm = Convert.ToInt32(lotNums[6]);
            string dx = tm <= 24 ? "小" : "大";
            string ds = tm % 2 == 0 ? "双" : "单";
            string sb = Hong.Contains(tm) ? "红" : (Lan.Contains(tm) ? "蓝" : (Lv.Contains(tm) ? "绿" : ""));

            if (CheckNumber.Contains(sb))
            {
                if (CheckNumber.Contains(dx))
                {
                    num++;
                }

                if (CheckNumber.Contains(ds))
                {
                    num++;
                }
            }

            return num;
        }

        /// <summary>
        /// 特码半半波
        /// </summary>
        /// <param name="CheckNumber"></param>
        /// <param name="lotNumber"></param>
        /// <returns></returns>
        public static int TMBBB(string lotNumber, string CheckNumber)
        {
            if (string.IsNullOrEmpty(CheckNumber) || string.IsNullOrEmpty(lotNumber))
            {
                return 0;
            }

            string[] lotNums = lotNumber.Split(',');

            if (lotNums.Length != 7 || lotNums[6] == "49")
            {
                return 0;
            }

            int num = 0;
            int tm = Convert.ToInt32(lotNums[6]);
            string dx = tm <= 24 ? "小" : "大";
            string ds = tm % 2 == 0 ? "双" : "单";
            string sb = Hong.Contains(tm) ? "红" : (Lan.Contains(tm) ? "蓝" : (Lv.Contains(tm) ? "绿" : ""));

            if (CheckNumber.Contains(sb) && CheckNumber.Contains(dx) && CheckNumber.Contains(ds))
            {
                return 1;
            }

            return num;
        }
    }
}
