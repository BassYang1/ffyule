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
			this.doh.SqlCmd = "SELECT * FROM [Sys_Lottery] where IsOpen=0 order by sort asc";
			DataTable dataTable = this.doh.GetDataTable();
			for (int i = 0; i < dataTable.Rows.Count; i++)
			{
				this.doh.Reset();
				this.doh.SqlCmd = "SELECT * FROM [Sys_Lottery] where Id=" + dataTable.Rows[i]["Id"];
				DataTable dataTable2 = this.doh.GetDataTable();
				this.loStr = "var lotJson={\"result\" :\"1\",\"returnval\" :\"加载完成\"," + dtHelp.DT2JSON(dataTable2) + "}";
				base.SaveJsFile(this.loStr, HttpContext.Current.Server.MapPath("~/statics/json/" + dataTable.Rows[i]["Id"] + ".js"));
			}
			string txtStr = "var lotteryJsonData={\"result\" :\"1\",\"returnval\" :\"加载完成\"," + dtHelp.DT2JSON(dataTable) + "}";
			base.SaveJsFile(txtStr, HttpContext.Current.Server.MapPath("~/statics/json/Lottery.js"));
			dataTable.Clear();
			dataTable.Dispose();
			this.doh.Reset();
			this.doh.SqlCmd = "SELECT Id,Title,Ltype FROM Sys_Lottery where IsOpen=0 ORDER BY Sort asc";
			DataTable dataTable3 = this.doh.GetDataTable();
			this.doh.Reset();
			this.doh.SqlCmd = "SELECT Id,LotteryId,Title FROM Sys_PlaySmallType where IsOpen=0 and flag=0 ORDER BY Sort asc";
			DataTable dataTable4 = this.doh.GetDataTable();
			string txtStr2 = "var PlayData={\"result\" :\"1\",\"returnval\" :\"加载完成\"," + dtHelp.DT2JSON2(dataTable3, dataTable4) + "}";
			dataTable3.Clear();
			dataTable3.Dispose();
			dataTable4.Clear();
			dataTable4.Dispose();
			base.SaveJsFile(txtStr2, HttpContext.Current.Server.MapPath("~/statics/json/LotAndSmalldata.js"));
			base.Response.Write("更新完成：" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
		}

		protected HtmlForm form1;
	}
}
