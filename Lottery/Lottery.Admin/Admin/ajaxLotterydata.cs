using System;
using System.Data;
using System.Xml;
using Lottery.Collect;
using Lottery.DAL;
using Lottery.Utils;

namespace Lottery.Admin
{
	public class ajaxLotterydata : AdminCenter
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
			switch (operType)
			{
			case "ajaxGetList":
				this.ajaxGetList();
				goto IL_12F;
			case "ajaxGetNum":
				this.ajaxGetNum();
				goto IL_12F;
			case "ajaxPaiJiang":
				this.ajaxPaiJiang();
				goto IL_12F;
			case "ajaxPaiJiangTitle":
				this.ajaxPaiJiangTitle();
				goto IL_12F;
			case "ajaxDel":
				this.ajaxDel();
				goto IL_12F;
			case "ajaxGetDNList":
				this.ajaxGetDNList();
				goto IL_12F;
			case "ajaxGetListNo":
				this.ajaxGetListNo();
				goto IL_12F;
			}
			this.DefaultResponse();
			IL_12F:
			base.Response.Write(this._response);
		}

		private void DefaultResponse()
		{
			this._response = base.JsonResult(0, "未知操作");
		}

		private void ajaxGetList()
		{
			string text = base.q("d1");
			string text2 = base.q("d2");
			string text3 = base.q("sort");
			string text4 = base.q("u");
			int num = base.Str2Int(base.q("gId"), 0);
			int num2 = base.Int_ThisPage();
			int pageSize = base.Str2Int(base.q("pagesize"), 20);
			int num3 = base.Str2Int(base.q("flag"), 0);
			if (text.Trim().Length == 0)
			{
				text = this.StartTime;
			}
			if (text2.Trim().Length == 0)
			{
				text2 = this.EndTime;
			}
			if (Convert.ToDateTime(text) > Convert.ToDateTime(text2))
			{
				text = text2;
			}
			string text5 = string.Concat(new object[]
			{
				" STime >='",
				text,
				"' and STime <'",
				text2,
				"' and [Type]=",
				num3
			});
			string text6 = base.q("id");
			if (!string.IsNullOrEmpty(text4))
			{
				text5 = text5 + "and Title like '" + text4 + "%'";
			}
			if (!string.IsNullOrEmpty(text3))
			{
				if (text3.Equals("0"))
				{
					text5 += "and Flag=0";
				}
				if (text3.Equals("1"))
				{
					text5 += "and Flag>0";
				}
			}
			this.doh.Reset();
			this.doh.ConditionExpress = text5;
			int totalCount = this.doh.Count("V_LotteryData");
			string sql = SqlHelp.GetSql0("Id,TypeName,Title,Number,NumberAll,Total,DX,DS,OpenTime,STime,Flag,Flag2,IsFill", "V_LotteryData", "STime", pageSize, num2, "desc", text5);
			this.doh.Reset();
			this.doh.SqlCmd = sql;
			DataTable dataTable = this.doh.GetDataTable();
			this._response = string.Concat(new string[]
			{
				"{\"result\" :\"1\",\"returnval\" :\"操作成功\",\"pagebar\" :\"",
				PageBar.GetPageBar(3, "js", 2, totalCount, pageSize, num2, "javascript:ajaxList(<#page#>);"),
				"\",",
				dtHelp.DT2JSON(dataTable),
				"}"
			});
			dataTable.Clear();
			dataTable.Dispose();
		}

		private void ajaxGetListNo()
		{
			string text = base.q("d1");
			string text2 = base.q("d2");
			string text3 = base.q("sort");
			string text4 = base.q("u");
			int num = base.Str2Int(base.q("gId"), 0);
			int num2 = base.Int_ThisPage();
			int pageSize = base.Str2Int(base.q("pagesize"), 20);
			int num3 = base.Str2Int(base.q("flag"), 0);
			if (text.Trim().Length == 0)
			{
				text = this.StartTime;
			}
			if (text2.Trim().Length == 0)
			{
				text2 = this.EndTime;
			}
			if (Convert.ToDateTime(text) > Convert.ToDateTime(text2))
			{
				text = text2;
			}
			string text5 = string.Concat(new string[]
			{
				" STime >='",
				text,
				"' and STime <'",
				text2,
				"' and Flag>0"
			});
			string text6 = base.q("id");
			if (!string.IsNullOrEmpty(text4))
			{
				text5 = text5 + "and Title like '" + text4 + "%'";
			}
			this.doh.Reset();
			this.doh.ConditionExpress = text5;
			int totalCount = this.doh.Count("V_LotteryData");
			string sql = SqlHelp.GetSql0("Id,TypeName,Title,Number,NumberAll,Total,DX,DS,OpenTime,STime,Flag,Flag2,IsFill", "V_LotteryData", "STime", pageSize, num2, "desc", text5);
			this.doh.Reset();
			this.doh.SqlCmd = sql;
			DataTable dataTable = this.doh.GetDataTable();
			this._response = string.Concat(new string[]
			{
				"{\"result\" :\"1\",\"returnval\" :\"操作成功\",\"pagebar\" :\"",
				PageBar.GetPageBar(3, "js", 2, totalCount, pageSize, num2, "javascript:ajaxList(<#page#>);"),
				"\",",
				dtHelp.DT2JSON(dataTable),
				"}"
			});
			dataTable.Clear();
			dataTable.Dispose();
		}

		private void ajaxGetNum()
		{
			int num = base.Str2Int(base.q("flag"), 0);
			int num2 = num;
			if (num2 <= 2005)
			{
				switch (num2)
				{
				case 1001:
					this.GetLotteryNumber("cqssc");
					break;
				case 1002:
					break;
				case 1003:
					this.GetLotteryNumber("xjssc");
					break;
				case 1004:
					this.GetLotteryNumberYoule(1004);
					break;
				case 1005:
					break;
				case 1006:
					break;
				case 1007:
					this.GetLotteryNumber("tjssc");
					break;
				case 1008:
					this.GetLotteryNumber("ynssc");
					break;
				case 1009:
					this.GetLotteryNumberYoule(1009);
					break;
				case 1010:
					this.GetLotteryNumberYoule(1010);
					break;
				case 1011:
					this.GetLotteryNumberYoule(1011);
					break;
				case 1012:
					this.GetLotteryNumberYoule(1012);
					break;
				case 1013:
					this.GetLotteryNumber("twbingo");
					break;
				case 1014:
					this.GetLotteryNumber("jpkeno");
					break;
				case 1015:
					this.GetLotteryNumber("phkeno");
					break;
				case 1016:
					this.GetLotteryNumberYoule(1016);
					break;
				case 1017:
					this.GetLotteryNumber("krkeno");
					break;
				default:
					switch (num2)
					{
					case 2001:
						this.GetLotteryNumber("sd11x5");
						break;
					case 2002:
						this.GetLotteryNumber("gd11x5");
						break;
					case 2003:
						this.GetLotteryNumber("sh11x5");
						break;
					case 2004:
						this.GetLotteryNumber("jx11x5");
						break;
					}
					break;
				}
			}
			else
			{
				switch (num2)
				{
				case 3001:
					this.GetLotteryNumber("shssl");
					break;
				case 3002:
					Fc3dData.Fc3d();
					break;
				case 3003:
					Tcp3Data.Tcp3();
					break;
				case 3004:
					this.GetLotteryNumber("krkeno");
					break;
				case 3005:
					break;
				case 3006:
					break;
				default:
					if (num2 == 4001)
					{
						this.GetLotteryNumber("bjpk10");
					}
					break;
				}
			}
			this._response = base.JsonResult(1, "获取号码成功");
		}

		private void ajaxPaiJiang()
		{
			int num = base.Str2Int(base.q("flag"), 0);
			new LotteryDataDAL().UpdateAllBetNumber(num);
			new LogAdminOperDAL().SaveLog(this.AdminId, "0", "游戏管理", "手工派奖Id为" + num + "的彩种");
			this._response = base.JsonResult(1, "操作成功，请在日志中查看派奖情况");
		}

		private void ajaxPaiJiangTitle()
		{
			int type = base.Str2Int(base.q("flag"), 0);
			string text = base.f("ids");
			string[] array = text.Split(new char[]
			{
				','
			});
			for (int i = 0; i < array.Length; i++)
			{
				new LotteryDataDAL().UpdateBetNumber(type, array[i]);
			}
			new LogAdminOperDAL().SaveLog(this.AdminId, "0", "游戏管理", "手工派奖Id为" + text + "的彩种");
			this._response = base.JsonResult(1, "操作成功，请在日志中查看派奖情况");
		}

		private void ajaxDel()
		{
			string text = base.f("id");
			this.doh.Reset();
			this.doh.ConditionExpress = "id=@id";
			this.doh.AddConditionParameter("@id", text);
			int num = this.doh.Delete("Sys_LotteryData");
			new LogAdminOperDAL().SaveLog(this.AdminId, "0", "游戏管理", "删除Id为" + text + "的彩种开奖数据");
			if (num > 0)
			{
				this._response = base.JsonResult(1, "删除成功");
			}
			else
			{
				this._response = base.JsonResult(0, "删除失败");
			}
		}

		private void ajaxGetDNList()
		{
			string text = base.q("u");
			int num = base.Str2Int(base.q("gId"), 0);
			int num2 = base.Int_ThisPage();
			int pageSize = base.Str2Int(base.q("pagesize"), 20);
			int num3 = base.Str2Int(base.q("flag"), 0);
			string text2 = "[Type]=" + num3;
			if (!string.IsNullOrEmpty(text))
			{
				text2 = text2 + "and Title like '" + text + "%'";
			}
			this.doh.Reset();
			this.doh.ConditionExpress = text2;
			int totalCount = this.doh.Count("V_LotteryData");
			string sql = SqlHelp.GetSql0("Id,TypeName,Title,Number,OpenTime", "V_LotteryData", "OpenTime", pageSize, num2, "desc", text2);
			this.doh.Reset();
			this.doh.SqlCmd = sql;
			DataTable dataTable = this.doh.GetDataTable();
			DataTable dataTable2 = new DataTable();
			dataTable2.Columns.Add("TypeName");
			dataTable2.Columns.Add("Title");
			dataTable2.Columns.Add("Number");
			dataTable2.Columns.Add("Number1");
			dataTable2.Columns.Add("Number2");
			dataTable2.Columns.Add("Number3");
			dataTable2.Columns.Add("Number4");
			dataTable2.Columns.Add("Number5");
			dataTable2.Columns.Add("Win1");
			dataTable2.Columns.Add("Win2");
			dataTable2.Columns.Add("Win3");
			dataTable2.Columns.Add("Win4");
			dataTable2.Columns.Add("Win5");
			dataTable2.Columns.Add("Total");
			dataTable2.Columns.Add("OpenTime");
			for (int i = 0; i < dataTable.Rows.Count; i++)
			{
				DataRow dataRow = dataTable2.NewRow();
				dataRow["TypeName"] = dataTable.Rows[i]["TypeName"].ToString();
				dataRow["Title"] = dataTable.Rows[i]["Title"].ToString();
				dataRow["Number"] = dataTable.Rows[i]["Number"].ToString() + "(" + CheckSSC_DN.CheckNNum(dataTable.Rows[i]["Number"].ToString()) + ")";
				dataRow["Number1"] = CheckSSC_DN.AddDnNum(dataTable.Rows[i]["Number"].ToString(), 1) + "(" + CheckSSC_DN.CheckNNum(CheckSSC_DN.AddDnNum(dataTable.Rows[i]["Number"].ToString(), 1)) + ")";
				dataRow["Number2"] = CheckSSC_DN.AddDnNum(dataTable.Rows[i]["Number"].ToString(), 2) + "(" + CheckSSC_DN.CheckNNum(CheckSSC_DN.AddDnNum(dataTable.Rows[i]["Number"].ToString(), 2)) + ")";
				dataRow["Number3"] = CheckSSC_DN.AddDnNum(dataTable.Rows[i]["Number"].ToString(), 3) + "(" + CheckSSC_DN.CheckNNum(CheckSSC_DN.AddDnNum(dataTable.Rows[i]["Number"].ToString(), 3)) + ")";
				dataRow["Number4"] = CheckSSC_DN.AddDnNum(dataTable.Rows[i]["Number"].ToString(), 4) + "(" + CheckSSC_DN.CheckNNum(CheckSSC_DN.AddDnNum(dataTable.Rows[i]["Number"].ToString(), 4)) + ")";
				dataRow["Number5"] = CheckSSC_DN.AddDnNum(dataTable.Rows[i]["Number"].ToString(), 5) + "(" + CheckSSC_DN.CheckNNum(CheckSSC_DN.AddDnNum(dataTable.Rows[i]["Number"].ToString(), 5)) + ")";
				dataRow["Win1"] = CheckSSC_DN.P_Wj(dataTable.Rows[i]["Number"].ToString(), 1) + " 倍";
				dataRow["Win2"] = CheckSSC_DN.P_Wj(dataTable.Rows[i]["Number"].ToString(), 2) + " 倍";
				dataRow["Win3"] = CheckSSC_DN.P_Wj(dataTable.Rows[i]["Number"].ToString(), 3) + " 倍";
				dataRow["Win4"] = CheckSSC_DN.P_Wj(dataTable.Rows[i]["Number"].ToString(), 4) + " 倍";
				dataRow["Win5"] = CheckSSC_DN.P_Wj(dataTable.Rows[i]["Number"].ToString(), 5) + " 倍";
				dataRow["Total"] = (CheckSSC_DN.P_Wj(dataTable.Rows[i]["Number"].ToString(), 1) + CheckSSC_DN.P_Wj(dataTable.Rows[i]["Number"].ToString(), 2) + CheckSSC_DN.P_Wj(dataTable.Rows[i]["Number"].ToString(), 3) + CheckSSC_DN.P_Wj(dataTable.Rows[i]["Number"].ToString(), 4) + CheckSSC_DN.P_Wj(dataTable.Rows[i]["Number"].ToString(), 5) - 5) * 100;
				dataRow["OpenTime"] = dataTable.Rows[i]["OpenTime"].ToString();
				dataTable2.Rows.Add(dataRow);
			}
			this._response = string.Concat(new string[]
			{
				"{\"result\" :\"1\",\"returnval\" :\"操作成功\",\"pagebar\" :\"",
				PageBar.GetPageBar(3, "js", 2, totalCount, pageSize, num2, "javascript:ajaxList(<#page#>);"),
				"\",",
				dtHelp.DT2JSON(dataTable2),
				"}"
			});
			dataTable2.Clear();
			dataTable2.Dispose();
		}

		public void GetLotteryNumberYoule(int loid)
		{
			this.doh.Reset();
			this.doh.SqlCmd = "SELECT IssueNum FROM [N_UserBet] where lotteryId=" + loid + " and state=0 and DATEDIFF(minute,STime,getdate())>=5 group by IssueNum ";
			DataTable dataTable = this.doh.GetDataTable();
			for (int i = 0; i < dataTable.Rows.Count; i++)
			{
				string number = "";
				string[] array = NumberCode.CreateCode20();
				if (array.Length >= 20)
				{
					int num = (Convert.ToInt32(array[0]) + Convert.ToInt32(array[1]) + Convert.ToInt32(array[2]) + Convert.ToInt32(array[3])) % 10;
					int num2 = (Convert.ToInt32(array[4]) + Convert.ToInt32(array[5]) + Convert.ToInt32(array[6]) + Convert.ToInt32(array[7])) % 10;
					int num3 = (Convert.ToInt32(array[8]) + Convert.ToInt32(array[9]) + Convert.ToInt32(array[10]) + Convert.ToInt32(array[11])) % 10;
					int num4 = (Convert.ToInt32(array[12]) + Convert.ToInt32(array[13]) + Convert.ToInt32(array[14]) + Convert.ToInt32(array[15])) % 10;
					int num5 = (Convert.ToInt32(array[16]) + Convert.ToInt32(array[17]) + Convert.ToInt32(array[18]) + Convert.ToInt32(array[19])) % 10;
					number = string.Concat(new object[]
					{
						num,
						",",
						num2,
						",",
						num3,
						",",
						num4,
						",",
						num5
					});
				}
				if (!new LotteryDataDAL().Exists(loid, dataTable.Rows[i]["IssueNum"].ToString()))
				{
					new LotteryDataDAL().Add(loid, dataTable.Rows[i]["IssueNum"].ToString(), number, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), string.Join(",", array));
				}
			}
		}

		public void GetLotteryNumber(string code)
		{
			try
			{
				string text = DateTime.Now.ToString("yyyy-MM-dd");
				string html = Lottery.DAL.HtmlOperate.GetHtml(string.Concat(new string[]
				{
					"http://192.168.0.31:50000/Data/",
					text,
					"/",
					code,
					".xml"
				}));
				if (string.IsNullOrEmpty(html))
				{
					new LogExceptionDAL().Save("采集异常", "采集找不到开奖数据的关键字符");
				}
				else
				{
					XmlNodeList xmlNode = this.GetXmlNode(html, "row");
					if (xmlNode == null)
					{
						new LogExceptionDAL().Save("采集异常", "采集找不到开奖数据的关键字符");
					}
					else if (xmlNode.Count == 0)
					{
						new LogExceptionDAL().Save("采集异常", "采集找不到开奖数据的关键字符");
					}
					else
					{
						foreach (XmlNode xmlNode2 in xmlNode)
						{
							string innerText = xmlNode2.Attributes["expect"].InnerText;
							string text2 = xmlNode2.Attributes["opencode"].InnerText.Replace("+", ",");
							string innerText2 = xmlNode2.Attributes["opentime"].InnerText;
							if (string.IsNullOrEmpty(innerText2) || string.IsNullOrEmpty(innerText) || string.IsNullOrEmpty(text2))
							{
								new LogExceptionDAL().Save("采集异常", "采集找不到开奖数据的关键字符");
								break;
							}
							if (!text2.Contains("255"))
							{
								switch (code)
								{
								case "cqssc":
								{
									string title = innerText.Substring(0, 8) + "-" + innerText.Substring(8);
									if (text2.Length == 9)
									{
										if (!new LotteryDataDAL().Exists(1001, title))
										{
											new LotteryDataDAL().Add(1001, title, text2, innerText2, "");
										}
									}
									break;
								}
								case "xjssc":
								{
									string title = innerText.Substring(0, 8) + "-" + innerText.Substring(9);
									if (text2.Length == 9)
									{
										if (!new LotteryDataDAL().Exists(1003, title))
										{
											new LotteryDataDAL().Add(1003, title, text2, innerText2, "");
										}
									}
									break;
								}
								case "tjssc":
								{
									string title = innerText.Substring(0, 8) + "-" + innerText.Substring(8);
									if (text2.Length == 9)
									{
										if (!new LotteryDataDAL().Exists(1007, title))
										{
											new LotteryDataDAL().Add(1007, title, text2, innerText2, "");
										}
									}
									break;
								}
								case "ynssc":
								{
									string title = innerText.Substring(0, 8) + "-" + innerText.Substring(8);
									if (text2.Length == 9)
									{
										if (!new LotteryDataDAL().Exists(1008, title))
										{
											new LotteryDataDAL().Add(1008, title, text2, innerText2, "");
										}
									}
									break;
								}
								case "sgkeno":
									if (!new LotteryDataDAL().Exists(1012, innerText))
									{
										string[] array = text2.Split(new char[]
										{
											','
										});
										int num2 = (Convert.ToInt32(array[0]) + Convert.ToInt32(array[1]) + Convert.ToInt32(array[2]) + Convert.ToInt32(array[3])) % 10;
										int num3 = (Convert.ToInt32(array[4]) + Convert.ToInt32(array[5]) + Convert.ToInt32(array[6]) + Convert.ToInt32(array[7])) % 10;
										int num4 = (Convert.ToInt32(array[8]) + Convert.ToInt32(array[9]) + Convert.ToInt32(array[10]) + Convert.ToInt32(array[11])) % 10;
										int num5 = (Convert.ToInt32(array[12]) + Convert.ToInt32(array[13]) + Convert.ToInt32(array[14]) + Convert.ToInt32(array[15])) % 10;
										int num6 = (Convert.ToInt32(array[16]) + Convert.ToInt32(array[17]) + Convert.ToInt32(array[18]) + Convert.ToInt32(array[19])) % 10;
										string number = string.Concat(new object[]
										{
											num2,
											",",
											num3,
											",",
											num4,
											",",
											num5,
											",",
											num6
										});
										new LotteryDataDAL().Add(1012, innerText, number, innerText2, string.Join(",", array));
									}
									break;
								case "twbingo":
									if (!new LotteryDataDAL().Exists(1013, innerText))
									{
										string[] array = text2.Split(new char[]
										{
											','
										});
										int num2 = (Convert.ToInt32(array[0]) + Convert.ToInt32(array[1]) + Convert.ToInt32(array[2]) + Convert.ToInt32(array[3])) % 10;
										int num3 = (Convert.ToInt32(array[4]) + Convert.ToInt32(array[5]) + Convert.ToInt32(array[6]) + Convert.ToInt32(array[7])) % 10;
										int num4 = (Convert.ToInt32(array[8]) + Convert.ToInt32(array[9]) + Convert.ToInt32(array[10]) + Convert.ToInt32(array[11])) % 10;
										int num5 = (Convert.ToInt32(array[12]) + Convert.ToInt32(array[13]) + Convert.ToInt32(array[14]) + Convert.ToInt32(array[15])) % 10;
										int num6 = (Convert.ToInt32(array[16]) + Convert.ToInt32(array[17]) + Convert.ToInt32(array[18]) + Convert.ToInt32(array[19])) % 10;
										string number = string.Concat(new object[]
										{
											num2,
											",",
											num3,
											",",
											num4,
											",",
											num5,
											",",
											num6
										});
										new LotteryDataDAL().Add(1013, innerText, number, innerText2, string.Join(",", array));
									}
									break;
								case "sd11x5":
								{
									string title = innerText.Substring(0, 8) + "-" + innerText.Substring(8);
									if (text2.Length == 14)
									{
										if (!new LotteryDataDAL().Exists(2001, title))
										{
											new LotteryDataDAL().Add(2001, title, text2, innerText2, "");
										}
									}
									break;
								}
								case "gd11x5":
								{
									string title = innerText.Substring(0, 8) + "-" + innerText.Substring(8);
									if (text2.Length == 14)
									{
										if (!new LotteryDataDAL().Exists(2002, title))
										{
											new LotteryDataDAL().Add(2002, title, text2, innerText2, "");
										}
									}
									break;
								}
								case "sh11x5":
								{
									string title = innerText.Substring(0, 8) + "-" + innerText.Substring(8);
									if (text2.Length == 14)
									{
										if (!new LotteryDataDAL().Exists(2003, title))
										{
											new LotteryDataDAL().Add(2003, title, text2, innerText2, "");
										}
									}
									break;
								}
								case "jx11x5":
								{
									string title = innerText.Substring(0, 8) + "-" + innerText.Substring(8);
									if (text2.Length == 14)
									{
										if (!new LotteryDataDAL().Exists(2004, title))
										{
											new LotteryDataDAL().Add(2004, title, text2, innerText2, "");
										}
									}
									break;
								}
								case "krkeno":
									if (!new LotteryDataDAL().Exists(1017, innerText))
									{
										string[] array = text2.Split(new char[]
										{
											','
										});
										int num2 = (Convert.ToInt32(array[0]) + Convert.ToInt32(array[1]) + Convert.ToInt32(array[2]) + Convert.ToInt32(array[3])) % 10;
										int num3 = (Convert.ToInt32(array[4]) + Convert.ToInt32(array[5]) + Convert.ToInt32(array[6]) + Convert.ToInt32(array[7])) % 10;
										int num4 = (Convert.ToInt32(array[8]) + Convert.ToInt32(array[9]) + Convert.ToInt32(array[10]) + Convert.ToInt32(array[11])) % 10;
										int num5 = (Convert.ToInt32(array[12]) + Convert.ToInt32(array[13]) + Convert.ToInt32(array[14]) + Convert.ToInt32(array[15])) % 10;
										int num6 = (Convert.ToInt32(array[16]) + Convert.ToInt32(array[17]) + Convert.ToInt32(array[18]) + Convert.ToInt32(array[19])) % 10;
										string number = string.Concat(new object[]
										{
											num2,
											",",
											num3,
											",",
											num4,
											",",
											num5,
											",",
											num6
										});
										new LotteryDataDAL().Add(1017, innerText, number, innerText2, "");
									}
									if (!new LotteryDataDAL().Exists(3004, innerText))
									{
										string[] array = text2.Split(new char[]
										{
											','
										});
										int num2 = (Convert.ToInt32(array[0]) + Convert.ToInt32(array[1]) + Convert.ToInt32(array[2]) + Convert.ToInt32(array[3]) + Convert.ToInt32(array[4]) + Convert.ToInt32(array[5]) + Convert.ToInt32(array[6])) % 10;
										int num3 = (Convert.ToInt32(array[7]) + Convert.ToInt32(array[8]) + Convert.ToInt32(array[9]) + Convert.ToInt32(array[10]) + Convert.ToInt32(array[11]) + Convert.ToInt32(array[12]) + Convert.ToInt32(array[13])) % 10;
										int num4 = (Convert.ToInt32(array[14]) + Convert.ToInt32(array[15]) + Convert.ToInt32(array[16]) + Convert.ToInt32(array[17]) + Convert.ToInt32(array[18]) + Convert.ToInt32(array[19])) % 10;
										string number = string.Concat(new object[]
										{
											num2,
											",",
											num3,
											",",
											num4
										});
										new LotteryDataDAL().Add(3004, innerText, number, innerText2, "");
									}
									break;
								case "jpkeno":
									if (!new LotteryDataDAL().Exists(1014, innerText))
									{
										string[] array = text2.Split(new char[]
										{
											','
										});
										int num2 = (Convert.ToInt32(array[0]) + Convert.ToInt32(array[1]) + Convert.ToInt32(array[2]) + Convert.ToInt32(array[3])) % 10;
										int num3 = (Convert.ToInt32(array[4]) + Convert.ToInt32(array[5]) + Convert.ToInt32(array[6]) + Convert.ToInt32(array[7])) % 10;
										int num4 = (Convert.ToInt32(array[8]) + Convert.ToInt32(array[9]) + Convert.ToInt32(array[10]) + Convert.ToInt32(array[11])) % 10;
										int num5 = (Convert.ToInt32(array[12]) + Convert.ToInt32(array[13]) + Convert.ToInt32(array[14]) + Convert.ToInt32(array[15])) % 10;
										int num6 = (Convert.ToInt32(array[16]) + Convert.ToInt32(array[17]) + Convert.ToInt32(array[18]) + Convert.ToInt32(array[19])) % 10;
										string number = string.Concat(new object[]
										{
											num2,
											",",
											num3,
											",",
											num4,
											",",
											num5,
											",",
											num6
										});
										new LotteryDataDAL().Add(1014, innerText, number, innerText2, string.Join(",", array));
									}
									break;
								case "phkeno":
									if (!new LotteryDataDAL().Exists(1015, innerText))
									{
										string[] array = text2.Split(new char[]
										{
											','
										});
										int num2 = (Convert.ToInt32(array[0]) + Convert.ToInt32(array[1]) + Convert.ToInt32(array[2]) + Convert.ToInt32(array[3])) % 10;
										int num3 = (Convert.ToInt32(array[4]) + Convert.ToInt32(array[5]) + Convert.ToInt32(array[6]) + Convert.ToInt32(array[7])) % 10;
										int num4 = (Convert.ToInt32(array[8]) + Convert.ToInt32(array[9]) + Convert.ToInt32(array[10]) + Convert.ToInt32(array[11])) % 10;
										int num5 = (Convert.ToInt32(array[12]) + Convert.ToInt32(array[13]) + Convert.ToInt32(array[14]) + Convert.ToInt32(array[15])) % 10;
										int num6 = (Convert.ToInt32(array[16]) + Convert.ToInt32(array[17]) + Convert.ToInt32(array[18]) + Convert.ToInt32(array[19])) % 10;
										string number = string.Concat(new object[]
										{
											num2,
											",",
											num3,
											",",
											num4,
											",",
											num5,
											",",
											num6
										});
										new LotteryDataDAL().Add(1015, innerText, number, innerText2, string.Join(",", array));
									}
									break;
								case "bjpk10":
									if (!new LotteryDataDAL().Exists(4001, innerText))
									{
										string[] array = text2.Split(new char[]
										{
											','
										});
										if (array.Length == 10)
										{
											new LotteryDataDAL().Add(4001, innerText, text2, innerText2, "");
										}
									}
									break;
								case "shssl":
								{
									string title = innerText.Substring(0, 8) + "-" + innerText.Substring(8);
									if (text2.Length == 5)
									{
										if (!new LotteryDataDAL().Exists(3001, title))
										{
											new LotteryDataDAL().Add(3001, title, text2, innerText2, "");
										}
									}
									break;
								}
								}
							}
						}
					}
				}
			}
			catch (Exception ex)
			{
				new LogExceptionDAL().Save("采集异常", "采集获取开奖数据出错，错误代码：" + ex.Message);
			}
		}

		public XmlNodeList GetXmlNode(string shtml, string rootElm)
		{
			XmlNodeList result = null;
			try
			{
				XmlDocument xmlDocument = new XmlDocument();
				xmlDocument.LoadXml(shtml);
				result = xmlDocument.ChildNodes.Item(1).SelectNodes(rootElm);
			}
			catch
			{
			}
			return result;
		}

		private string _operType = string.Empty;

		private string _response = string.Empty;
	}
}
