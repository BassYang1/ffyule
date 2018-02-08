using System;
using Lottery.DAL;

namespace Lottery.Admin
{
	public class ajaxsysDelData : AdminCenter
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!base.CheckFormUrl())
			{
				base.Response.End();
			}
			base.Admin_Load("master", "json");
			this._operType = base.q("oper");
			string operType = this._operType;
			if (operType != null)
			{
				if (operType == "ajaxDel")
				{
					this.ajaxDel();
					goto IL_66;
				}
			}
			this.DefaultResponse();
			IL_66:
			base.Response.Write(this._response);
		}

		private void DefaultResponse()
		{
			this._response = base.JsonResult(0, "未知操作");
		}

		private void ajaxDel()
		{
			string text = base.f("d1");
			string d = base.f("d2");
			string a = base.f("flag");
			string str = Convert.ToDateTime(text).ToString("yyyy-MM-dd HH:mm:ss");
			if (a == "1")
			{
				new SysDelDataDAL().DeleteUserBet(text, d);
				new LogAdminOperDAL().SaveLog(this.AdminId, "0", "系统设置", "清理了" + str + "及之前的会员投注记录");
			}
			if (a == "2")
			{
				new SysDelDataDAL().DeleteUserGetCash(text, d);
				new LogAdminOperDAL().SaveLog(this.AdminId, "0", "系统设置", "清理了" + str + "及之前的会员取款记录");
			}
			if (a == "3")
			{
				new SysDelDataDAL().DeleteUserMoneyLog(text, d);
				new LogAdminOperDAL().SaveLog(this.AdminId, "0", "系统设置", "清理了" + str + "及之前的会员账变记录");
			}
			if (a == "4")
			{
				new SysDelDataDAL().DeleteUserMoneyStat(text, d);
				new LogAdminOperDAL().SaveLog(this.AdminId, "0", "系统设置", "清理了" + str + "及之前的会员统计记录");
			}
			if (a == "5")
			{
				new SysDelDataDAL().DeleteLotteryData(text, d);
				new LogAdminOperDAL().SaveLog(this.AdminId, "0", "系统设置", "清理了" + str + "及之前的开奖记录");
			}
			if (a == "6")
			{
				new SysDelDataDAL().DeleteUserLogs(text, d);
				new LogAdminOperDAL().SaveLog(this.AdminId, "0", "系统设置", "清理了" + str + "及之前的会员登录记录");
			}
			if (a == "7")
			{
				new SysDelDataDAL().DeleteLogs(text, d);
				new LogAdminOperDAL().SaveLog(this.AdminId, "0", "系统设置", "清理了" + str + "及之前的系统日志记录");
			}
			if (a == "8")
			{
				new SysDelDataDAL().DeleteUserBetZh(text, d);
				new LogAdminOperDAL().SaveLog(this.AdminId, "0", "系统设置", "清理了" + str + "及之前的会员追号记录");
			}
			this._response = base.JsonResult(1, "删除成功");
		}

		private string _operType = string.Empty;

		private string _response = string.Empty;
	}
}
