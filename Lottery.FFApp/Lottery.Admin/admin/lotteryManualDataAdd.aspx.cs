// Decompiled with JetBrains decompiler
// Type: Lottery.AdminFile.Admin.lotteryDataAdd
// Assembly: Lottery.Admin, Version=7.0.1.203, Culture=neutral, PublicKeyToken=null
// MVID: 838B9BD2-8091-4C2A-B624-E2A206486676
// Assembly location: F:\pros\tianheng\bf\admin\bin\Lottery.Admin.dll

using Lottery.DAL;
using System;
using System.Data;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace Lottery.AdminFile.Admin
{
    public partial class lotteryManualDataAdd : AdminCenter
    {
        protected HtmlForm form1;
        protected DropDownList ddlType;
        protected DropDownList ddlIssueNum;
        protected HiddenField txtNumberAll;
        protected HiddenField txtTotal;
        protected TextBox txtTitle;
        protected TextBox txtNumber;
        protected TextBox txtOpenTime;
        protected Button btnSave;

        protected void Page_Load(object sender, EventArgs e)
        {
            this.Admin_Load("", "html");
            this.getSysLotteryDropDownList(ref this.ddlType);
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            int int32 = Convert.ToInt32(this.ddlType.SelectedValue);
            string Number = this.txtNumber.Text;
            string NumberAll = this.txtNumberAll.Value;
            string total = this.txtTotal.Value;
            string openTime = this.txtOpenTime.Text;
            string title = this.txtTitle.Text;

            this.doh.Reset();
            this.doh.SqlCmd = "SELECT * FROM Sys_LotteryData WHERE Type=" + int32 + " and Title='" + title + "'";
            DataTable dataTable = this.doh.GetDataTable();

            if (dataTable != null && dataTable.Rows.Count > 0)
            {
                this.FinalMessage("已开奖，操作失败", "/admin/close.htm", 0);
                return;
            }

            if (new LotteryDataDAL().ManualAdd(int32, title, Number, openTime, NumberAll))
            {
                new LogAdminOperDAL().SaveLog(this.AdminId, "0", "游戏管理", "添加了" + this.txtTitle.Text + "预设开奖号码");
                this.FinalMessage("操作成功", "/admin/close.htm", 0);
            }
            else
            {
                this.FinalMessage("操作失败", "/admin/close.htm", 0);
            }
        }
    }
}
