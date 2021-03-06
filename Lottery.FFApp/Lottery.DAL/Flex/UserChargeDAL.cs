﻿// Decompiled with JetBrains decompiler
// Type: Lottery.DAL.Flex.UserChargeDAL
// Assembly: Lottery.DAL, Version=1.0.1.1, Culture=neutral, PublicKeyToken=null
// MVID: 7C79BA5B-21B3-40F1-B96A-84E656E22E29
// Assembly location: F:\pros\tianheng\bf\WebAppOld\bin\Lottery.DAL.dll

using Lottery.DBUtility;
using Lottery.Utils;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Lottery.DAL.Flex
{
  public class UserChargeDAL : ComData
  {
    public void GetListJSON(int _thispage, int _pagesize, string _wherestr1, ref string _jsonstr)
    {
      using (DbOperHandler dbOperHandler = new ComData().Doh())
      {
        dbOperHandler.Reset();
        dbOperHandler.ConditionExpress = _wherestr1;
        string sql0 = SqlHelp.GetSql0(dbOperHandler.Count("Flex_ChargeRecord").ToString() + " as totalcount,row_number() over (order by Id desc) as rowid,UserName,*", "Flex_ChargeRecord", "Id", _pagesize, _thispage, "desc", _wherestr1);
        dbOperHandler.Reset();
        dbOperHandler.SqlCmd = sql0;
        DataTable dataTable = dbOperHandler.GetDataTable();
        _jsonstr = this.ConverTableToJSON(dataTable);
        dataTable.Clear();
        dataTable.Dispose();
      }
    }

    public void GetIphoneListJSON(int _thispage, int _pagesize, string _wherestr1, ref string _jsonstr)
    {
      using (DbOperHandler dbOperHandler = new ComData().Doh())
      {
        dbOperHandler.Reset();
        dbOperHandler.ConditionExpress = _wherestr1;
        int totalCount = dbOperHandler.Count("Flex_ChargeRecord");
        string sql0 = SqlHelp.GetSql0("row_number() over (order by Id desc) as rowid,UserName,*", "Flex_ChargeRecord", "Id", _pagesize, _thispage, "desc", _wherestr1);
        dbOperHandler.Reset();
        dbOperHandler.SqlCmd = sql0;
        DataTable dataTable = dbOperHandler.GetDataTable();
        _jsonstr = "{\"result\" :\"1\",\"returnval\" :\"操作成功\",\"pagebar\" :\"" + PageBar.GetPageBar(6, "js", 2, totalCount, _pagesize, _thispage, "javascript:ajaxList(<#page#>);") + "\"," + dtHelp.DT2JSON(dataTable, _pagesize * (_thispage - 1)) + "}";
        dataTable.Clear();
        dataTable.Dispose();
      }
    }

    public int Save(string userId, string bankId, string checkCode, Decimal money)
    {
      using (DbOperHandler dbOperHandler = new ComData().Doh())
      {
        int num = 0;
        if (bankId == "888")
          num = 1;
        dbOperHandler.Reset();
        dbOperHandler.SqlCmd = "select top 1 * from Sys_Info";
        DataTable dataTable = dbOperHandler.GetDataTable();
        if (Convert.ToDecimal(money) < Convert.ToDecimal(dataTable.Rows[0]["MinCharge"].ToString()))
          return -1;
        dbOperHandler.Reset();
        dbOperHandler.AddFieldItem("SsId", (object) SsId.Charge);
        dbOperHandler.AddFieldItem("UserId", (object) userId);
        dbOperHandler.AddFieldItem("BankId", (object) bankId);
        dbOperHandler.AddFieldItem("CheckCode", (object) checkCode);
        dbOperHandler.AddFieldItem("InMoney", (object) money);
        dbOperHandler.AddFieldItem("STime", (object) DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
        dbOperHandler.AddFieldItem("State", (object) num);
        return dbOperHandler.Insert("N_UserCharge");
      }
    }

    public int SaveChargeInfo(string SsId, string UserId, string BankId, string CheckCode, Decimal Money)
    {
      using (DbOperHandler dbOperHandler = new ComData().Doh())
      {
        int num = 0;
        if (BankId == "888")
          num = 1;
        dbOperHandler.Reset();
        dbOperHandler.AddFieldItem("SsId", (object) SsId);
        dbOperHandler.AddFieldItem("UserId", (object) UserId);
        dbOperHandler.AddFieldItem("BankId", (object) BankId);
        dbOperHandler.AddFieldItem("CheckCode", (object) CheckCode);
        dbOperHandler.AddFieldItem("InMoney", (object) Money);
        dbOperHandler.AddFieldItem("STime", (object) DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
        dbOperHandler.AddFieldItem("State", (object) num);
        return dbOperHandler.Insert("N_UserCharge");
      }
    }

    public void DeleteLogs()
    {
      using (DbOperHandler dbOperHandler = new ComData().Doh())
      {
        dbOperHandler.Reset();
        dbOperHandler.ConditionExpress = "1=1";
        dbOperHandler.Delete("N_UserCharge");
      }
    }

    public int SaveUpCharge(string Type, string userId, string toUserId, Decimal money)
    {
      int num = 0;
      using (SqlConnection sqlConnection = new SqlConnection(ComData.connectionString))
      {
        sqlConnection.Open();
        SqlCommand sqlCommand = new SqlCommand();
        sqlCommand.Connection = sqlConnection;
        try
        {
          string chargeLog = SsId.ChargeLog;
          sqlCommand.CommandText = "select UserName from N_User where Id=" + userId;
          string str1 = string.Concat(sqlCommand.ExecuteScalar());
          sqlCommand.CommandText = "select UserName from N_User where Id=" + toUserId;
          string str2 = string.Concat(sqlCommand.ExecuteScalar());
          if (Type == "0")
          {
            if (new UserTotalTran().MoneyOpers(chargeLog, userId, -money, 0, 0, 0, 9, 99, "", "", "转账给" + str2 + " " + (object) money + "元", "") <= 0)
              return 0;
            if (new UserTotalTran().MoneyOpers(chargeLog, toUserId, money, 0, 0, 0, 9, 99, "", "", str1 + "转账给你" + (object) money + "元", "") <= 0)
              return 0;
            SqlParameter[] values = new SqlParameter[7]
            {
              new SqlParameter("@SsId", (object) chargeLog),
              new SqlParameter("@Type", (object) Type),
              new SqlParameter("@UserId", (object) userId),
              new SqlParameter("@ToUserId", (object) toUserId),
              new SqlParameter("@MoneyChange", (object) money),
              new SqlParameter("@STime", (object) DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")),
              new SqlParameter("@Remark", (object) (userId + "为" + toUserId + "充值"))
            };
            sqlCommand.CommandText = "INSERT INTO N_UserChargeLog(SsId,Type,UserId,ToUserId,MoneyChange,STime,Remark) VALUES (@SsId,@Type,@UserId,@ToUserId,@MoneyChange,@STime,@Remark)";
            sqlCommand.CommandText += " SELECT SCOPE_IDENTITY()";
            sqlCommand.Parameters.AddRange(values);
            num = Convert.ToInt32(sqlCommand.ExecuteScalar());
            sqlCommand.Parameters.Clear();
          }
          if (Type == "1")
          {
            if (new UserTotalTran().MoneyOpers(chargeLog, userId, -money, 0, 0, 0, 10, 99, "", "", "转账给" + str2 + " " + (object) money + "元", "") <= 0)
              return 0;
            if (new UserTotalTran().MoneyOpers(chargeLog, toUserId, money, 0, 0, 0, 10, 99, "", "", str1 + "转账给你" + (object) money + "元", "") <= 0)
              return 0;
            SqlParameter[] values = new SqlParameter[7]
            {
              new SqlParameter("@SsId", (object) chargeLog),
              new SqlParameter("@Type", (object) Type),
              new SqlParameter("@UserId", (object) userId),
              new SqlParameter("@ToUserId", (object) toUserId),
              new SqlParameter("@MoneyChange", (object) money),
              new SqlParameter("@STime", (object) DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")),
              new SqlParameter("@Remark", (object) (userId + "为" + toUserId + "充值"))
            };
            sqlCommand.CommandText = "INSERT INTO N_UserChargeLog(SsId,Type,UserId,ToUserId,MoneyChange,STime,Remark) VALUES (@SsId,@Type,@UserId,@ToUserId,@MoneyChange,@STime,@Remark)";
            sqlCommand.CommandText += " SELECT SCOPE_IDENTITY()";
            sqlCommand.Parameters.AddRange(values);
            num = Convert.ToInt32(sqlCommand.ExecuteScalar());
            sqlCommand.Parameters.Clear();
          }
        }
        catch (Exception ex)
        {
          new LogExceptionDAL().Save("系统异常", ex.Message);
          return 0;
        }
      }
      return num;
    }

    public int SaveAgentFH(string UserId, string toUserId, string StartTime, string EndTime, Decimal Bet, Decimal Total, Decimal Per, Decimal InMoney)
    {
      int num = 0;
      using (SqlConnection sqlConnection = new SqlConnection(ComData.connectionString))
      {
        sqlConnection.Open();
        SqlCommand sqlCommand = new SqlCommand();
        sqlCommand.Connection = sqlConnection;
        try
        {
          string chargeLog = SsId.ChargeLog;
          sqlCommand.CommandText = "select UserName from N_User where Id=" + UserId;
          string str1 = string.Concat(sqlCommand.ExecuteScalar());
          sqlCommand.CommandText = "select UserName from N_User where Id=" + toUserId;
          string str2 = string.Concat(sqlCommand.ExecuteScalar());
          if (new UserTotalTran().MoneyOpers(chargeLog, UserId, -InMoney, 0, 0, 0, 12, 99, "", "", "分红给" + str2 + " " + (object) InMoney + "元", "") <= 0)
            return 0;
          if (new UserTotalTran().MoneyOpers(chargeLog, toUserId, InMoney, 0, 0, 0, 12, 99, "", "", str1 + "分红给你" + (object) InMoney + "元", "") <= 0)
            return 0;
          SqlParameter[] values = new SqlParameter[9]
          {
            new SqlParameter("@UserId", (object) UserId),
            new SqlParameter("@StartTime", (object) StartTime),
            new SqlParameter("@EndTime", (object) EndTime),
            new SqlParameter("@Bet", (object) Bet),
            new SqlParameter("@Total", (object) Total),
            new SqlParameter("@Per", (object) Per),
            new SqlParameter("@InMoney", (object) InMoney),
            new SqlParameter("@STime", (object) DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")),
            new SqlParameter("@Remark", (object) (UserId + "为" + toUserId + "分红"))
          };
          sqlCommand.CommandText = "INSERT INTO [Act_AgentFHRecord]([UserId],[AgentId],[StartTime],[EndTime],[Bet],[Total],[Per],[InMoney],[STime],[Remark])";
          sqlCommand.CommandText += "VALUES(@UserId,99,@StartTime,@EndTime,@Bet,@Total,@Per,@InMoney,@STime,@Remark)";
          sqlCommand.CommandText += " SELECT SCOPE_IDENTITY()";
          sqlCommand.Parameters.AddRange(values);
          num = Convert.ToInt32(sqlCommand.ExecuteScalar());
          sqlCommand.Parameters.Clear();
        }
        catch (Exception ex)
        {
          new LogExceptionDAL().Save("系统异常", ex.Message);
          return 0;
        }
      }
      return num;
    }
  }
}
