// Decompiled with JetBrains decompiler
// Type: Lottery.DAL.LotteryDataDAL
// Assembly: Lottery.DAL, Version=1.0.1.1, Culture=neutral, PublicKeyToken=null
// MVID: 7C79BA5B-21B3-40F1-B96A-84E656E22E29
// Assembly location: F:\pros\tianheng\bf\WebAppOld\bin\Lottery.DAL.dll

using Lottery.DBUtility;
using Lottery.Utils;
using System;
using System.Data;

namespace Lottery.DAL
{
  public class LotteryDataDAL : ComData
  {
    public void GetListJSON(int lotteryId, ref string _jsonstr)
    {
      using (DbOperHandler dbOperHandler = new ComData().Doh())
      {
        dbOperHandler.Reset();
        dbOperHandler.SqlCmd = "select top 10 * from Sys_LotteryData where Type=" + (object) lotteryId + " order by Title desc";
        DataTable dataTable = dbOperHandler.GetDataTable();
        _jsonstr = this.ConverTableToJSON(dataTable);
        dataTable.Clear();
        dataTable.Dispose();
      }
    }

    public void GetListJSON(int lotteryId, ref string _jsonstr, ref string _xml)
    {
      using (DbOperHandler dbOperHandler = new ComData().Doh())
      {
        dbOperHandler.Reset();
        dbOperHandler.SqlCmd = "select top 20 * from Sys_LotteryData where Type=" + (object) lotteryId + " order by Title desc";
        DataTable dataTable = dbOperHandler.GetDataTable();
        _jsonstr = this.ConverTableToJSON(dataTable);
        _xml = this.ConverTableToLotteryXML(dataTable);
        dataTable.Clear();
        dataTable.Dispose();
      }
    }

    public void GetDnListJSON(int lotteryId, ref string _jsonstr)
    {
      DataTable table = new DataTable();
      table.Columns.Add("Title");
      table.Columns.Add("Number");
      table.Columns.Add("Number1");
      table.Columns.Add("Number2");
      table.Columns.Add("Number3");
      table.Columns.Add("Number4");
      table.Columns.Add("Number5");
      using (DbOperHandler dbOperHandler = new ComData().Doh())
      {
        dbOperHandler.Reset();
        dbOperHandler.SqlCmd = "select top 18 * from Sys_LotteryData where Type=" + (object) lotteryId + " order by Title desc";
        DataTable dataTable = dbOperHandler.GetDataTable();
        for (int index = 0; index < dataTable.Rows.Count; ++index)
        {
          DataRow row = table.NewRow();
          row["Title"] = (object) dataTable.Rows[index]["Title"].ToString();
          row["Number"] = (object) (dataTable.Rows[index]["Number"].ToString() + "(" + CheckSSC_DN.CheckNNum(dataTable.Rows[index]["Number"].ToString()) + ")");
          row["Number1"] = (object) (CheckSSC_DN.AddDnNum(dataTable.Rows[index]["Number"].ToString(), 1) + "(" + CheckSSC_DN.CheckNNum(CheckSSC_DN.AddDnNum(dataTable.Rows[index]["Number"].ToString(), 1)) + ")");
          row["Number2"] = (object) (CheckSSC_DN.AddDnNum(dataTable.Rows[index]["Number"].ToString(), 2) + "(" + CheckSSC_DN.CheckNNum(CheckSSC_DN.AddDnNum(dataTable.Rows[index]["Number"].ToString(), 2)) + ")");
          row["Number3"] = (object) (CheckSSC_DN.AddDnNum(dataTable.Rows[index]["Number"].ToString(), 3) + "(" + CheckSSC_DN.CheckNNum(CheckSSC_DN.AddDnNum(dataTable.Rows[index]["Number"].ToString(), 3)) + ")");
          row["Number4"] = (object) (CheckSSC_DN.AddDnNum(dataTable.Rows[index]["Number"].ToString(), 4) + "(" + CheckSSC_DN.CheckNNum(CheckSSC_DN.AddDnNum(dataTable.Rows[index]["Number"].ToString(), 4)) + ")");
          row["Number5"] = (object) (CheckSSC_DN.AddDnNum(dataTable.Rows[index]["Number"].ToString(), 5) + "(" + CheckSSC_DN.CheckNNum(CheckSSC_DN.AddDnNum(dataTable.Rows[index]["Number"].ToString(), 5)) + ")");
          table.Rows.Add(row);
        }
        _jsonstr = this.ConverTableToJSON(table);
        table.Clear();
        table.Dispose();
      }
    }

    public bool Exists(int _type, string _title)
    {
      int num = 0;
      using (DbOperHandler dbOperHandler = new ComData().Doh())
      {
        dbOperHandler.Reset();
        dbOperHandler.ConditionExpress = "Title=@Title and Type=@Type";
        dbOperHandler.AddConditionParameter("@Title", (object) _title);
        dbOperHandler.AddConditionParameter("@Type", (object) _type);
        if (dbOperHandler.Exist("Sys_LotteryData"))
          num = 1;
      }
      return num == 1;
    }

    public bool Exists(int _type, string _title, string _number)
    {
      int num = 0;
      using (DbOperHandler dbOperHandler = new ComData().Doh())
      {
        dbOperHandler.Reset();
        dbOperHandler.ConditionExpress = "Type=@Type and NumberAll=@NumberAll";
        dbOperHandler.AddConditionParameter("@Type", (object) _type);
        dbOperHandler.AddConditionParameter("@NumberAll", (object) _number);
        if (dbOperHandler.Exist("Sys_LotteryData"))
          num = 1;
      }
      return num == 1;
    }

    public bool Add(int type, string title, string Number, string opentime, string NumberAll = "")
    {
      int num = LotterySum.SumNumber(Number);
      using (DbOperHandler dbOperHandler = new ComData().Doh())
      {
        NumberAll = string.IsNullOrEmpty(NumberAll) ? Number : NumberAll;
        dbOperHandler.Reset();
        dbOperHandler.AddFieldItem("Type", (object) type);
        dbOperHandler.AddFieldItem("Title", (object) title);
        dbOperHandler.AddFieldItem(nameof (Number), (object) Number);
        dbOperHandler.AddFieldItem(nameof (NumberAll), (object) NumberAll);
        dbOperHandler.AddFieldItem("Total", (object) num);
        dbOperHandler.AddFieldItem("Opentime", (object) opentime);
        dbOperHandler.AddFieldItem("IsFill", (object) "1");
        if (dbOperHandler.Insert("Sys_LotteryData") > 0)
          return true;
      }
      return false;
    }

    public bool AddYoule(int type, string title, string Number, string opentime, string NumberAll = "")
    {
      int num = LotterySum.SumNumber(Number);
      using (DbOperHandler dbOperHandler = new ComData().Doh())
      {
        NumberAll = string.IsNullOrEmpty(NumberAll) ? Number : NumberAll;
        dbOperHandler.Reset();
        dbOperHandler.AddFieldItem("Type", (object) type);
        dbOperHandler.AddFieldItem("Title", (object) title);
        dbOperHandler.AddFieldItem(nameof (Number), (object) Number);
        dbOperHandler.AddFieldItem(nameof (NumberAll), (object) NumberAll);
        dbOperHandler.AddFieldItem("Total", (object) num);
        dbOperHandler.AddFieldItem("Opentime", (object) opentime);
        dbOperHandler.AddFieldItem("IsFill", (object) "1");
        if (dbOperHandler.Insert("Sys_LotteryData") > 0)
          return true;
      }
      return false;
    }

    public bool UpdateBetNumber(int type, string title)
    {
      using (DbOperHandler dbOperHandler = new ComData().Doh())
      {
        dbOperHandler.Reset();
        dbOperHandler.SqlCmd = "select top 1 Type,Title,Number from Sys_LotteryData where Id=" + title;
        DataTable dataTable = dbOperHandler.GetDataTable();
        if (dataTable.Rows.Count > 0)
        {
          LotteryCheck.AdminRunOper(Convert.ToInt32(dataTable.Rows[0]["Type"].ToString()), dataTable.Rows[0]["Title"].ToString(), dataTable.Rows[0]["Number"].ToString());
          return true;
        }
      }
      return false;
    }

    public bool UpdateAllBetNumber(int type)
    {
      using (DbOperHandler dbOperHandler = new ComData().Doh())
      {
        dbOperHandler.Reset();
        dbOperHandler.SqlCmd = "select top 1 Type,Title,Number from Sys_LotteryData where Type=" + (object) type + " order by Title desc";
        DataTable dataTable = dbOperHandler.GetDataTable();
        if (dataTable.Rows.Count > 0)
        {
          for (int index = 0; index < dataTable.Rows.Count; ++index)
            LotteryCheck.AdminRunOper(Convert.ToInt32(dataTable.Rows[index]["Type"].ToString()), dataTable.Rows[index]["Title"].ToString(), dataTable.Rows[index]["Number"].ToString());
        }
      }
      return true;
    }

    public DataTable GetListDataTable(int lotteryId, int top)
    {
      using (DbOperHandler dbOperHandler = new ComData().Doh())
      {
        dbOperHandler.Reset();
        dbOperHandler.SqlCmd = "select * from (select top " + (object) top + " * from Sys_LotteryData where Type=" + (object) lotteryId + " order by Title Desc) A order by Title asc";
        return dbOperHandler.GetDataTable();
      }
    }

    public string GetHisNumber(string ltype)
    {
      string str1 = "";
      using (DbOperHandler dbOperHandler = new ComData().Doh())
      {
        dbOperHandler.Reset();
        dbOperHandler.SqlCmd = "SELECT TOP 1000 [Number]\r\n                                FROM [Sys_LotteryData] where '" + ltype + "'=substring(Convert(varchar(10),type),1,1) order by STime asc";
        DataTable dataTable = dbOperHandler.GetDataTable();
        if (dataTable.Rows.Count > 1)
        {
          Random random = new Random();
          string str2 = dataTable.Rows[random.Next(0, dataTable.Rows.Count - 1)]["Number"].ToString();
          int num = random.Next(0, 4);
          string[] strArray = str2.Split(',');
          for (int index = 0; index < strArray.Length; ++index)
            str1 = index != num ? str1 + strArray[index] + "," : str1 + (object) random.Next(0, 9) + ",";
          str1 = str1.Substring(0, str1.Length - 1);
        }
        else
          str1 = NumberCode.CreateCode(5);
      }
      return str1;
    }

    public DataTable GetHisNumber2(string ltype)
    {
      using (DbOperHandler dbOperHandler = new ComData().Doh())
      {
        dbOperHandler.Reset();
        dbOperHandler.SqlCmd = "SELECT TOP 1000 [Number]\r\n                                FROM [Sys_LotteryData] where '" + ltype + "'=substring(Convert(varchar(10),type),1,1) order by STime asc";
        return dbOperHandler.GetDataTable();
      }
    }
  }
}
