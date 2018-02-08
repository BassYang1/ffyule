using System;
using System.Data;
using System.Web;
using Lottery.DBUtility;

namespace Lottery.DAL
{
	public class conPlay : ComData
	{
		public void Create()
		{
			using (DbOperHandler dbOperHandler = new ComData().Doh())
			{
				dbOperHandler.Reset();
				dbOperHandler.SqlCmd = "SELECT * FROM Sys_PlaySmallType where IsOpen=0";
				DataTable dataTable = dbOperHandler.GetDataTable();
				for (int i = 0; i < dataTable.Rows.Count; i++)
				{
					DataRow dataRow = dataTable.Rows[i];
					string text = "ssc";
					if (Convert.ToInt32(dataRow["LotteryId"]) == 1)
					{
						text = "ssc";
					}
					if (Convert.ToInt32(dataRow["LotteryId"]) == 2)
					{
						text = "11x5";
					}
					if (Convert.ToInt32(dataRow["LotteryId"]) == 3)
					{
						text = "dpc";
					}
					if (Convert.ToInt32(dataRow["LotteryId"]) == 4)
					{
						text = "pk10";
					}
					string xmlFile = HttpContext.Current.Server.MapPath(string.Concat(new object[]
					{
						"~/WEB-INF/",
						text,
						"/",
						dataRow["Title2"],
						".xml"
					}));
					XmlControl xmlControl = new XmlControl(xmlFile);
					xmlControl.Update("play/id", dataRow["Id"].ToString());
					xmlControl.Update("play/type", dataRow["type"].ToString());
					xmlControl.Update("play/typename", dataRow["title0"].ToString());
					xmlControl.Update("play/name", dataRow["Title"].ToString());
					xmlControl.Update("play/minbonus", dataRow["Minbonus"].ToString());
					xmlControl.Update("play/posbonus", dataRow["Posbonus"].ToString());
					xmlControl.Update("play/maxbonus", dataRow["Maxbonus"].ToString());
					xmlControl.Update("play/remark", dataRow["remark"].ToString());
					xmlControl.Update("play/example", dataRow["example"].ToString());
					xmlControl.Update("play/help", dataRow["help"].ToString());
					xmlControl.Save();
					xmlControl.Dispose();
				}
			}
		}
	}
}
