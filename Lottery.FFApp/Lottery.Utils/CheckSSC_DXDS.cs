// Decompiled with JetBrains decompiler
// Type: Lottery.Utils.CheckSSC_DXDS
// Assembly: Lottery.Utils, Version=1.0.1.1, Culture=neutral, PublicKeyToken=null
// MVID: E7A9C185-AF0A-4444-AE46-9A73782D0A74
// Assembly location: F:\pros\tianheng\bf\WebAppOld\bin\Lottery.Utils.dll

using System;
namespace Lottery.Utils
{
    public static class CheckSSC_DXDS
    {
        public static int P_2DXDS(string LotteryNumber, string CheckNumber, string Pos)
        {
            int num = 0;
            string[] strArray1 = LotteryNumber.Split(',');
            if (Pos == "L")
                LotteryNumber = strArray1[0] + "," + strArray1[1];
            if (Pos == "R")
                LotteryNumber = strArray1[3] + "," + strArray1[4];
            string str = LotteryNumber.Replace("0", "小_双").Replace("1", "小_单").Replace("2", "小_双").Replace("3", "小_单").Replace("4", "小_双").Replace("5", "大_单").Replace("6", "大_双").Replace("7", "大_单").Replace("8", "大_双").Replace("9", "大_单");
            LotteryNumber.Replace("0", "双").Replace("1", "单").Replace("2", "双").Replace("3", "单").Replace("4", "双").Replace("5", "单").Replace("6", "双").Replace("7", "单").Replace("8", "双").Replace("9", "单");
            string[] strArray2 = str.Split(',');
            string[] strArray3 = CheckNumber.Split(',');
            string[] strArray4 = strArray2[0].Split('_');
            string[] strArray5 = strArray2[1].Split('_');
            for (int index1 = 0; index1 < strArray4.Length; ++index1)
            {
                for (int index2 = 0; index2 < strArray4.Length; ++index2)
                {
                    if (strArray3[0].IndexOf(strArray4[index1]) != -1 && strArray3[1].IndexOf(strArray5[index2]) != -1)
                        ++num;
                }
            }
            return num;
        }

        public static int P_2DX(string LotteryNumber, string CheckNumber, string Pos)
        {
            if (string.IsNullOrEmpty(CheckNumber) || string.IsNullOrEmpty(LotteryNumber) || string.IsNullOrEmpty(Pos))
            {
                return 0;
            }

            string[] userNums = CheckNumber.Split('_');
            string[] lotNums = LotteryNumber.Split(',');
            int lotNum = -1;

            if (lotNums.Length != 5)
            {
                return 0;
            }

            if (Pos == "W")
            {
                lotNum = Convert.ToInt32(lotNums[0]);
            }
            else if (Pos == "Q")
            {
                lotNum = Convert.ToInt32(lotNums[1]);
            }
            else if (Pos == "B")
            {
                lotNum = Convert.ToInt32(lotNums[2]);
            }
            else if (Pos == "S")
            {
                lotNum = Convert.ToInt32(lotNums[3]);
            }
            else if (Pos == "G")
            {
                lotNum = Convert.ToInt32(lotNums[4]);
            }

            if (lotNum < 0 || lotNum > 9)
            {
                return 0;
            }


            int num = 0;
            foreach (string n in userNums)
            {
                if (n == "大" && lotNum > 4)
                {
                    num++;
                }

                if (n == "小" && lotNum < 5)
                {
                    num++;
                }

            }

            return num;
        }

        public static int P_2DS(string LotteryNumber, string CheckNumber, string Pos)
        {
            if (string.IsNullOrEmpty(CheckNumber) || string.IsNullOrEmpty(LotteryNumber) || string.IsNullOrEmpty(Pos))
            {
                return 0;
            }

            string[] userNums = CheckNumber.Split('_');
            string[] lotNums = LotteryNumber.Split(',');
            int lotNum = -1;

            if (lotNums.Length != 5)
            {
                return 0;
            }

            if (Pos == "W")
            {
                lotNum = Convert.ToInt32(lotNums[0]);
            }
            else if (Pos == "Q")
            {
                lotNum = Convert.ToInt32(lotNums[1]);
            }
            else if (Pos == "B")
            {
                lotNum = Convert.ToInt32(lotNums[2]);
            }
            else if (Pos == "S")
            {
                lotNum = Convert.ToInt32(lotNums[3]);
            }
            else if (Pos == "G")
            {
                lotNum = Convert.ToInt32(lotNums[4]);
            }

            if (lotNum < 0 || lotNum > 9)
            {
                return 0;
            }


            int num = 0;
            foreach (string n in userNums)
            {
                if (n == "单" && (lotNum % 2 != 0))
                {
                    num++;
                }

                if (n == "双" && (lotNum % 2 == 0))
                {
                    num++;
                }

            }

            return num;
        }
    }
}
