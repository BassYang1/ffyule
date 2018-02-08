using System;
using System.Data;
using System.Web;
using System.Web.UI.HtmlControls;
using Lottery.DAL;
using Lottery.Utils;

namespace Lottery.Web.temp
{
	public class WebLot : UserCenterSession
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			this.doh.Reset();
			this.doh.SqlCmd = "SELECT * FROM [Sys_Lottery] where IsOpen=0 order by Id asc";
			DataTable dataTable = this.doh.GetDataTable();
			this.loStr = "var lotteryJsonData={\"result\" :\"1\",\"returnval\" :\"加载完成\"," + dtHelp.DT2JSON(dataTable) + "}";
			dataTable.Clear();
			dataTable.Dispose();
			base.SaveJsFile(this.loStr, HttpContext.Current.Server.MapPath("~/statics/json/Lottery_Json.js"));
			this.doh.Reset();
			this.doh.SqlCmd = "SELECT Id,Title,Ltype FROM Sys_Lottery where IsOpen=0 ORDER BY Id asc";
			DataTable dataTable2 = this.doh.GetDataTable();
			this.doh.Reset();
			this.doh.SqlCmd = "SELECT Id,LotteryId,Title FROM Sys_PlaySmallType where IsOpen=0 and flag=0 ORDER BY Sort asc";
			DataTable dataTable3 = this.doh.GetDataTable();
			string txtStr = "var PlayData={\"result\" :\"1\",\"returnval\" :\"加载完成\"," + dtHelp.DT2JSON2(dataTable2, dataTable3) + "}";
			dataTable2.Clear();
			dataTable2.Dispose();
			dataTable3.Clear();
			dataTable3.Dispose();
			base.SaveJsFile(txtStr, HttpContext.Current.Server.MapPath("~/statics/json/LotAndSmalldata.js"));
			base.Response.Write("更新完成：" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
		}

		protected HtmlForm form1;
	}
}
