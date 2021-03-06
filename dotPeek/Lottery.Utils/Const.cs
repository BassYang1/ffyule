﻿// Decompiled with JetBrains decompiler
// Type: Lottery.Utils.Const
// Assembly: Lottery.Utils, Version=1.0.1.1, Culture=neutral, PublicKeyToken=null
// MVID: E7A9C185-AF0A-4444-AE46-9A73782D0A74
// Assembly location: F:\pros\tianheng\bf\WebAppOld\bin\Lottery.Utils.dll

using System;
using System.Web;

namespace Lottery.Utils
{
  public class Const
  {
    public static string RootUserId
    {
      get
      {
        return "1026";
      }
    }

    public static string ConnectionString
    {
      get
      {
        return "Data Source=.;Initial Catalog=Ticket;Persist Security Info=True;User ID=sa;Password=aA321321";
      }
    }

    public static string DatabaseType
    {
      get
      {
        return "1";
      }
    }

    public static string GetUserIp
    {
      get
      {
        bool flag = false;
        string str = HttpContext.Current.Request.ServerVariables["HTTP_X_ForWARDED_For"] != null ? HttpContext.Current.Request.ServerVariables["HTTP_X_ForWARDED_For"].ToString() : HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"].ToString();
        if (str.Length > 15)
        {
          flag = true;
        }
        else
        {
          string[] strArray = str.Split('.');
          if (strArray.Length == 4)
          {
            for (int index = 0; index < strArray.Length; ++index)
            {
              if (strArray[index].Length > 3)
                flag = true;
            }
          }
          else
            flag = true;
        }
        if (flag)
          return "1.1.1.1";
        return str;
      }
    }

    public static string FormatIp(string ipStr)
    {
      string[] strArray = ipStr.Split('.');
      string str = "";
      for (int index = 0; index < strArray.Length; ++index)
      {
        if (strArray[index].Length < 3)
          strArray[index] = Convert.ToString("000" + strArray[index]).Substring(Convert.ToString("000" + strArray[index]).Length - 3, 3);
        str += strArray[index].ToString();
      }
      return str;
    }

    public static string GetRefererUrl
    {
      get
      {
        if (HttpContext.Current.Request.ServerVariables["Http_Referer"] == null)
          return "";
        return HttpContext.Current.Request.ServerVariables["Http_Referer"].ToString();
      }
    }

    public static string GetCurrentUrl
    {
      get
      {
        string serverVariable = HttpContext.Current.Request.ServerVariables["Url"];
        if (HttpContext.Current.Request.QueryString.Count == 0)
          return serverVariable;
        return serverVariable + "?" + HttpContext.Current.Request.ServerVariables["Query_String"];
      }
    }
  }
}
