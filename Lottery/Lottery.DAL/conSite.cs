using System;
using System.Collections;
using System.Data;
using System.Web;
using Lottery.DBUtility;
using Lottery.Entity;
using Lottery.Utils;

namespace Lottery.DAL
{
	public class conSite : ComData
	{
		public void GetListJSON(ref string _jsonstr)
		{
			using (DbOperHandler dbOperHandler = new ComData().Doh())
			{
				dbOperHandler.Reset();
				dbOperHandler.SqlCmd = "select top 1 * from Sys_Info";
				DataTable dataTable = dbOperHandler.GetDataTable();
				_jsonstr = base.ConverTableToJSON(dataTable);
				dataTable.Clear();
				dataTable.Dispose();
			}
		}

		public SiteModel CreateEntity()
		{
			SiteModel siteModel = new SiteModel();
			using (DbOperHandler dbOperHandler = new ComData().Doh())
			{
				dbOperHandler.Reset();
				dbOperHandler.SqlCmd = "SELECT * FROM [Sys_Info] where Id=1";
				DataTable dataTable = dbOperHandler.GetDataTable();
				DataRow dataRow = dataTable.Rows[0];
				siteModel.Name = "时时彩";
				siteModel.Dir = App.Path;
				siteModel.WebIsOpen = Convert.ToInt32(dataRow["WebIsOpen"].ToString());
				siteModel.WebCloseSeason = dataRow["WebCloseSeason"].ToString();
				siteModel.ZHIsOpen = Convert.ToInt32(dataRow["ZHIsOpen"].ToString());
				siteModel.RegIsOpen = Convert.ToInt32(dataRow["RegIsOpen"].ToString());
				siteModel.BetIsOpen = Convert.ToInt32(dataRow["BetIsOpen"].ToString());
				siteModel.CSUrl = dataRow["CSUrl"].ToString();
				siteModel.SignMinTotal = Convert.ToInt32(dataRow["SignMinTotal"].ToString());
				siteModel.SignMaxTotal = Convert.ToInt32(dataRow["SignMaxTotal"].ToString());
				siteModel.SignNum = Convert.ToInt32(dataRow["SignNum"].ToString());
				siteModel.WarnTotal = Convert.ToDecimal(dataRow["WarnTotal"].ToString());
				siteModel.MaxBet = Convert.ToDecimal(dataRow["MaxBet"].ToString());
				siteModel.MaxWin = Convert.ToDecimal(dataRow["MaxWin"].ToString());
				siteModel.MaxWinFK = Convert.ToDecimal(dataRow["MaxWinFK"].ToString());
				siteModel.MaxLevel = Convert.ToDecimal(dataRow["MaxLevel"].ToString());
				siteModel.MinCharge = Convert.ToDecimal(dataRow["MinCharge"].ToString());
				siteModel.Points = Convert.ToInt32(dataRow["Points"].ToString());
				siteModel.PriceOutCheck = Convert.ToDecimal(dataRow["PriceOutCheck"].ToString());
				siteModel.PriceOut = Convert.ToDecimal(dataRow["PriceOut"].ToString());
				siteModel.PriceOut2 = Convert.ToDecimal(dataRow["PriceOut2"].ToString());
				siteModel.PriceNum = Convert.ToInt32(dataRow["PriceNum"].ToString());
				siteModel.PriceOutPerson = Convert.ToInt32(dataRow["PriceOutPerson"].ToString());
				siteModel.AutoLottery = Convert.ToInt32(dataRow["AutoLottery"].ToString());
				siteModel.ProfitModel = Convert.ToInt32(dataRow["ProfitModel"].ToString());
				siteModel.ProfitMargin = Convert.ToInt32(dataRow["ProfitMargin"].ToString());
				siteModel.AutoRanking = Convert.ToInt32(dataRow["AutoRanking"].ToString());
				siteModel.PriceTime1 = dataRow["PriceTime1"].ToString();
				siteModel.PriceTime2 = dataRow["PriceTime2"].ToString();
				siteModel.BankTime = Convert.ToDecimal(dataRow["BankTime"].ToString());
				siteModel.ClientVersion = dataRow["ClientVersion"].ToString();
				siteModel.UpdateTime = Convert.ToDateTime(dataRow["UpdateTime"].ToString());
				siteModel.NewsUpdateTime = Convert.ToDateTime(dataRow["NewsUpdateTime"].ToString());
				siteModel.Version = dataRow["ClientVersion"].ToString();
			}
			return siteModel;
		}

		public SiteModel GetEntity()
		{
			return new SiteModel
			{
				Name = XmlCOM.ReadXml("~/WEB-INF/site", "Name"),
				Dir = App.Path,
				WebIsOpen = Convert.ToInt32(XmlCOM.ReadXml("~/WEB-INF/site", "WebIsOpen")),
				WebCloseSeason = XmlCOM.ReadXml("~/WEB-INF/site", "WebCloseSeason"),
				ZHIsOpen = Convert.ToInt32(XmlCOM.ReadXml("~/WEB-INF/site", "ZHIsOpen")),
				RegIsOpen = Convert.ToInt32(XmlCOM.ReadXml("~/WEB-INF/site", "RegIsOpen")),
				BetIsOpen = Convert.ToInt32(XmlCOM.ReadXml("~/WEB-INF/site", "BetIsOpen")),
				CSUrl = XmlCOM.ReadXml("~/WEB-INF/site", "CSUrl"),
				SignMinTotal = Convert.ToInt32(XmlCOM.ReadXml("~/WEB-INF/site", "SignMinTotal")),
				SignMaxTotal = Convert.ToInt32(XmlCOM.ReadXml("~/WEB-INF/site", "SignMaxTotal")),
				SignNum = Convert.ToInt32(XmlCOM.ReadXml("~/WEB-INF/site", "SignNum")),
				WarnTotal = Convert.ToDecimal(XmlCOM.ReadXml("~/WEB-INF/site", "WarnTotal")),
				MaxBet = Convert.ToDecimal(XmlCOM.ReadXml("~/WEB-INF/site", "MaxBet")),
				MaxWin = Convert.ToDecimal(XmlCOM.ReadXml("~/WEB-INF/site", "MaxWin")),
				MaxWinFK = Convert.ToDecimal(XmlCOM.ReadXml("~/WEB-INF/site", "MaxWinFK")),
				MaxLevel = Convert.ToDecimal(XmlCOM.ReadXml("~/WEB-INF/site", "MaxLevel")),
				MinCharge = Convert.ToDecimal(XmlCOM.ReadXml("~/WEB-INF/site", "MinCharge")),
				Points = Convert.ToInt32(XmlCOM.ReadXml("~/WEB-INF/site", "Points")),
				PriceOutCheck = Convert.ToDecimal(XmlCOM.ReadXml("~/WEB-INF/site", "PriceOutCheck")),
				PriceOut = Convert.ToDecimal(XmlCOM.ReadXml("~/WEB-INF/site", "PriceOut")),
				PriceOut2 = Convert.ToDecimal(XmlCOM.ReadXml("~/WEB-INF/site", "PriceOut2")),
				PriceNum = Convert.ToInt32(XmlCOM.ReadXml("~/WEB-INF/site", "PriceNum")),
				PriceOutPerson = Convert.ToInt32(XmlCOM.ReadXml("~/WEB-INF/site", "PriceOutPerson")),
				AutoLottery = Convert.ToInt32(XmlCOM.ReadXml("~/WEB-INF/site", "AutoLottery")),
				ProfitModel = Convert.ToInt32(XmlCOM.ReadXml("~/WEB-INF/site", "ProfitModel")),
				ProfitMargin = Convert.ToInt32(XmlCOM.ReadXml("~/WEB-INF/site", "ProfitMargin")),
				AutoRanking = Convert.ToInt32(XmlCOM.ReadXml("~/WEB-INF/site", "AutoRanking")),
				PriceTime1 = XmlCOM.ReadXml("~/WEB-INF/site", "PriceTime1"),
				PriceTime2 = XmlCOM.ReadXml("~/WEB-INF/site", "PriceTime2"),
				BankTime = Convert.ToDecimal(XmlCOM.ReadXml("~/WEB-INF/site", "BankTime")),
				ClientVersion = XmlCOM.ReadXml("~/WEB-INF/site", "ClientVersion"),
				UpdateTime = Convert.ToDateTime(XmlCOM.ReadXml("~/WEB-INF/site", "UpdateTime")),
				NewsUpdateTime = Convert.ToDateTime(XmlCOM.ReadXml("~/WEB-INF/site", "NewsUpdateTime")),
				CookieDomain = XmlCOM.ReadXml("~/WEB-INF/site", "CookieDomain"),
				CookiePath = XmlCOM.ReadXml("~/WEB-INF/site", "CookiePath"),
				CookiePrev = XmlCOM.ReadXml("~/WEB-INF/site", "CookiePrev"),
				CookieKeyCode = XmlCOM.ReadXml("~/WEB-INF/site", "CookieKeyCode"),
				Version = XmlCOM.ReadXml("~/WEB-INF/site", "Version"),
				DebugKey = XmlCOM.ReadXml("~/WEB-INF/site", "DebugKey")
			};
		}

		public void CreateSiteConfig()
		{
			SiteModel siteModel = this.CreateEntity();
			string xmlFile = HttpContext.Current.Server.MapPath("~/WEB-INF/site.xml");
			XmlControl xmlControl = new XmlControl(xmlFile);
			xmlControl.Update("Root/Name", siteModel.Name);
			xmlControl.Update("Root/Dir", siteModel.Dir);
			xmlControl.Update("Root/WebIsOpen", string.Concat(siteModel.WebIsOpen));
			xmlControl.Update("Root/WebCloseSeason", siteModel.WebCloseSeason);
			xmlControl.Update("Root/ZHIsOpen", string.Concat(siteModel.ZHIsOpen));
			xmlControl.Update("Root/RegIsOpen", string.Concat(siteModel.RegIsOpen));
			xmlControl.Update("Root/BetIsOpen", string.Concat(siteModel.BetIsOpen));
			xmlControl.Update("Root/CSUrl", siteModel.CSUrl);
			xmlControl.Update("Root/SignMinTotal", string.Concat(siteModel.SignMinTotal));
			xmlControl.Update("Root/SignMaxTotal", string.Concat(siteModel.SignMaxTotal));
			xmlControl.Update("Root/SignNum", string.Concat(siteModel.SignNum));
			xmlControl.Update("Root/WarnTotal", string.Concat(siteModel.WarnTotal));
			xmlControl.Update("Root/MaxBet", string.Concat(siteModel.MaxBet));
			xmlControl.Update("Root/MaxWin", string.Concat(siteModel.MaxWin));
			xmlControl.Update("Root/MaxWinFK", string.Concat(siteModel.MaxWinFK));
			xmlControl.Update("Root/MaxLevel", string.Concat(siteModel.MaxLevel));
			xmlControl.Update("Root/MinCharge", string.Concat(siteModel.MinCharge));
			xmlControl.Update("Root/Points", string.Concat(siteModel.Points));
			xmlControl.Update("Root/PriceOutCheck", string.Concat(siteModel.PriceOutCheck));
			xmlControl.Update("Root/PriceOut", string.Concat(siteModel.PriceOut));
			xmlControl.Update("Root/PriceOut2", string.Concat(siteModel.PriceOut2));
			xmlControl.Update("Root/PriceNum", string.Concat(siteModel.PriceNum));
			xmlControl.Update("Root/PriceTime1", siteModel.PriceTime1);
			xmlControl.Update("Root/PriceTime2", siteModel.PriceTime2);
			xmlControl.Update("Root/BankTime", string.Concat(siteModel.BankTime));
			xmlControl.Update("Root/PriceOutPerson", string.Concat(siteModel.PriceOutPerson));
			xmlControl.Update("Root/ClientVersion", siteModel.ClientVersion);
			xmlControl.Update("Root/UpdateTime", string.Concat(siteModel.UpdateTime));
			xmlControl.Update("Root/NewsUpdateTime", string.Concat(siteModel.NewsUpdateTime));
			xmlControl.Update("Root/AutoLottery", string.Concat(siteModel.AutoLottery));
			xmlControl.Update("Root/ProfitModel", string.Concat(siteModel.ProfitModel));
			xmlControl.Update("Root/ProfitMargin", string.Concat(siteModel.ProfitMargin));
			xmlControl.Update("Root/AutoRanking", string.Concat(siteModel.AutoRanking));
			xmlControl.Update("Root/Version", siteModel.Version);
			xmlControl.Save();
			xmlControl.Dispose();
		}

		public void CreateSiteFiles()
		{
			SiteModel entity = this.GetEntity();
			string text = string.Empty;
			text = string.Concat(new object[]
			{
				"var site = new Object();\r\nsite.Dir = '",
				entity.Dir,
				"';\r\nsite.WebIsOpen = '",
				entity.WebIsOpen,
				"';\r\nsite.WebCloseSeason = '",
				entity.WebCloseSeason,
				"';\r\nsite.ZHIsOpen = '",
				entity.ZHIsOpen,
				"';\r\nsite.RegIsOpen = '",
				entity.RegIsOpen,
				"';\r\nsite.BetIsOpen = '",
				entity.BetIsOpen,
				"';\r\nsite.CSUrl = '",
				entity.CSUrl,
				"';\r\nsite.SignMinTotal = '",
				entity.SignMinTotal,
				"';\r\nsite.SignMaxTotal = '",
				entity.SignMaxTotal,
				"';\r\nsite.SignNum = '",
				entity.SignNum,
				"';\r\nsite.WarnTotal = '",
				entity.WarnTotal,
				"';\r\nsite.MaxBet = '",
				entity.MaxBet,
				"';\r\nsite.MaxWin = '",
				entity.MaxWin,
				"';\r\nsite.MaxLevel = '",
				entity.MaxLevel,
				"';\r\nsite.MinCharge = '",
				entity.MinCharge,
				"';\r\nsite.Points = '",
				entity.Points,
				"';\r\nsite.PriceOutCheck = '",
				entity.PriceOutCheck,
				"';\r\nsite.PriceOut = '",
				entity.PriceOut,
				"';\r\nsite.PriceOut2 = '",
				entity.PriceOut2,
				"';\r\nsite.PriceNum = '",
				entity.PriceNum,
				"';\r\nsite.PriceTime1 = '",
				entity.PriceTime1,
				"';\r\nsite.PriceTime2 = '",
				entity.PriceTime2,
				"';\r\nsite.BankTime = '",
				entity.BankTime,
				"';\r\nsite.PriceOutPerson = '",
				entity.PriceOutPerson,
				"';\r\nsite.ClientVersion = '",
				entity.ClientVersion,
				"';\r\nsite.UpdateTime = '",
				entity.UpdateTime,
				"';\r\nsite.NewsUpdateTime = '",
				entity.NewsUpdateTime,
				"';\r\nsite.AutoLottery = '",
				entity.AutoLottery,
				"';\r\nsite.ProfitModel = '",
				entity.ProfitModel,
				"';\r\nsite.ProfitMargin = '",
				entity.ProfitMargin,
				"';\r\nsite.AutoRanking = '",
				entity.AutoRanking,
				"';\r\nsite.CookieDomain = '",
				entity.CookieDomain,
				"';\r\nsite.CookiePath = '",
				entity.CookiePath,
				"';\r\nsite.CookiePrev = '",
				entity.CookiePrev,
				"';\r\nsite.CookieKeyCode = '",
				entity.CookieKeyCode,
				"';\r\nsite.Version = '",
				entity.Version,
				"';\r\n"
			});
			string text2 = DirFile.ReadFile("~/statics/global.js");
			string text3 = "//<!--网站参数begin";
			string text4 = "//-->网站参数end";
			ArrayList htmls = Strings.GetHtmls(text2, text3, text4, true, true);
			if (htmls.Count > 0)
			{
				text2 = text2.Replace(htmls[0].ToString(), string.Concat(new string[]
				{
					text3,
					"\r\n\r\n",
					text,
					"\r\n\r\n",
					text4
				}));
			}
			DirFile.SaveFile(text2, "~/statics/global.js");
		}

		public SiteModel GetSite()
		{
			return new conSite().GetEntity();
		}
	}
}
