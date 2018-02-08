// Decompiled with JetBrains decompiler
// Type: Lottery.WebApp.contract.ContractgzPop
// Assembly: Lottery.FFApp, Version=1.0.1.1, Culture=neutral, PublicKeyToken=null
// MVID: CD5F1C7F-2EB9-4806-9452-C9F3634A8986
// Assembly location: F:\pros\tianheng\bf\WebAppOld\bin\Lottery.FFApp.dll

using Lottery.DAL;
using System;
using System.Data;

namespace Lottery.WebApp.contract
{
  public class ContractgzPop : UserCenterSession
  {
    public string userId = "0";
    public string maxAdminPer = "0";

    protected void Page_Load(object sender, EventArgs e)
    {
      this.Admin_Load("", "html");
      if (this.IsPostBack)
        return;
      if (this.Request.QueryString["id"] != null)
        this.userId = this.Request.QueryString["id"].ToString();
      this.doh.Reset();
      this.doh.SqlCmd = string.Format("SELECT top 1 UserGroup from N_User where Id=" + this.AdminId);
      DataTable dataTable = this.doh.GetDataTable();
      if (dataTable.Rows.Count > 0)
        this.maxAdminPer = Convert.ToInt32(dataTable.Rows[0]["UserGroup"]) != 4 ? (Convert.ToInt32(dataTable.Rows[0]["UserGroup"]) != 3 ? (Convert.ToInt32(dataTable.Rows[0]["UserGroup"]) != 2 ? "1" : "11") : "2") : "1";
    }
  }
}
