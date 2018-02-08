using System;
using System.Data;
using System.Web;
using System.Web.UI.HtmlControls;
using Lottery.DAL;
using Lottery.Utils;

namespace Lottery.Web.temp
{
	public class WebPlay : UserCenterSession
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			this.doh.Reset();
			this.doh.SqlCmd = "SELECT Id,TypeId,Title FROM Sys_PlayBigType where IsOpen=0 ORDER BY Sort asc";
			DataTable dataTable = this.doh.GetDataTable();
			string txtStr = "var PlayData={\"result\" :\"1\",\"returnval\" :\"加载完成\"," + dtHelp.DT2JSON(dataTable) + "}";
			dataTable.Clear();
			dataTable.Dispose();
			base.SaveJsFile(txtStr, HttpContext.Current.Server.MapPath("~/statics/json/PlayBigdate.js"));
			this.doh.Reset();
			this.doh.SqlCmd = "SELECT Id,TypeId,Title FROM Sys_PlayBigType where IsOpen=0 ORDER BY Sort asc";
			DataTable dataTable2 = this.doh.GetDataTable();
			this.doh.Reset();
			this.doh.SqlCmd = "SELECT * FROM Sys_PlaySmallType where IsOpen=0 and flag=0 ORDER BY Sort asc";
			DataTable dataTable3 = this.doh.GetDataTable();
			string txtStr2 = "var lotteryData={\"result\" :\"1\",\"returnval\" :\"加载完成\"," + dtHelp.DT2JSON(dataTable2, dataTable3) + "}";
			dataTable2.Clear();
			dataTable2.Dispose();
			dataTable3.Clear();
			dataTable3.Dispose();
			base.SaveJsFile(txtStr2, HttpContext.Current.Server.MapPath("~/statics/json/BigAndSmalldata.js"));
			base.Response.Write("更新完成：" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
		}

		protected HtmlForm form1;
	}
}
