using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Xml;
using Lottery.DAL;
using Lottery.Utils;
using RestSharp;

namespace Lottery.Admin
{
	public class ajaxLogs : AdminCenter
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
				goto IL_148;
			case "clear":
				this.ajaxClear();
				goto IL_148;
			case "ajaxGetExceptionList":
				this.ajaxGetExceptionList();
				goto IL_148;
			case "ajaxExceptionClear":
				this.ajaxExceptionClear();
				goto IL_148;
			case "ajaxGetUserLoginList":
				this.ajaxGetUserLoginList();
				goto IL_148;
			case "ajaxUserLoginClear":
				this.ajaxUserLoginClear();
				goto IL_148;
			case "ajaxGetAdminList":
				this.ajaxGetAdminList();
				goto IL_148;
			case "ajaxGetErrorList":
				this.ajaxGetErrorList();
				goto IL_148;
			}
			this.DefaultResponse();
			IL_148:
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
			string text3 = base.q("sel2");
			string text4 = base.q("u");
			int num = base.Int_ThisPage();
			int pageSize = base.Str2Int(base.q("pagesize"), 20);
			int num2 = base.Str2Int(base.q("flag"), 0);
			string text5 = "1=1";
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
			if (text.Trim().Length > 0 && text2.Trim().Length > 0)
			{
				string text6 = text5;
				text5 = string.Concat(new string[]
				{
					text6,
					" and STime >='",
					text,
					"' and STime <'",
					text2,
					"'"
				});
			}
			if (!string.IsNullOrEmpty(text3))
			{
				text5 = text5 + " and Title LIKE '%" + text3 + "%'";
			}
			if (!string.IsNullOrEmpty(text4))
			{
				text5 = text5 + " and Content LIKE '%" + text4 + "%'";
			}
			this.doh.Reset();
			this.doh.ConditionExpress = text5;
			int totalCount = this.doh.Count("Log_Sys");
			string sql = SqlHelp.GetSql0("*", "Log_Sys", "id", pageSize, num, "desc", text5);
			this.doh.Reset();
			this.doh.SqlCmd = sql;
			DataTable dataTable = this.doh.GetDataTable();
			this._response = string.Concat(new string[]
			{
				"{\"result\" :\"1\",\"returnval\" :\"操作成功\",\"pagebar\" :\"",
				PageBar.GetPageBar(3, "js", 2, totalCount, pageSize, num, "javascript:ajaxList(<#page#>);"),
				"\",",
				dtHelp.DT2JSON(dataTable),
				"}"
			});
			dataTable.Clear();
			dataTable.Dispose();
		}

		private void ajaxClear()
		{
			new LogSysDAL().DeleteLogs();
			new LogAdminOperDAL().SaveLog(this.AdminId, "0", "系统设置", "管理员清空了系统日志！");
			this._response = base.JsonResult(1, "成功清空");
		}

		private void ajaxGetExceptionList()
		{
			string text = base.q("d1");
			string text2 = base.q("d2");
			string text3 = base.q("sel2");
			string text4 = base.q("u");
			int num = base.Int_ThisPage();
			int pageSize = base.Str2Int(base.q("pagesize"), 20);
			int num2 = base.Str2Int(base.q("flag"), 0);
			string text5 = "1=1";
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
			if (text.Trim().Length > 0 && text2.Trim().Length > 0)
			{
				string text6 = text5;
				text5 = string.Concat(new string[]
				{
					text6,
					" and STime >='",
					text,
					"' and STime <'",
					text2,
					"'"
				});
			}
			if (!string.IsNullOrEmpty(text3))
			{
				text5 = text5 + " and Title LIKE '%" + text3 + "%'";
			}
			if (!string.IsNullOrEmpty(text4))
			{
				text5 = text5 + " and Content LIKE '%" + text4 + "%'";
			}
			this.doh.Reset();
			this.doh.ConditionExpress = text5;
			int totalCount = this.doh.Count("Log_Exception");
			string sql = SqlHelp.GetSql0("*", "Log_Exception", "id", pageSize, num, "desc", text5);
			this.doh.Reset();
			this.doh.SqlCmd = sql;
			DataTable dataTable = this.doh.GetDataTable();
			this._response = string.Concat(new string[]
			{
				"{\"result\" :\"1\",\"returnval\" :\"操作成功\",\"pagebar\" :\"",
				PageBar.GetPageBar(3, "js", 2, totalCount, pageSize, num, "javascript:ajaxList(<#page#>);"),
				"\",",
				dtHelp.DT2JSON(dataTable),
				"}"
			});
			dataTable.Clear();
			dataTable.Dispose();
		}

		private void ajaxExceptionClear()
		{
			new LogExceptionDAL().DeleteLogs();
			new LogAdminOperDAL().SaveLog(this.AdminId, "0", "系统设置", "管理员清空了异常日志！");
			this._response = base.JsonResult(1, "成功清空");
		}

		private void ajaxGetUserLoginList()
		{
			string text = base.q("d1");
			string text2 = base.q("d2");
			string value = base.q("sel2");
			string text3 = base.q("u");
			int num = base.Int_ThisPage();
			int pageSize = base.Str2Int(base.q("pagesize"), 20);
			int num2 = base.Str2Int(base.q("flag"), 0);
			string text4 = "";
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
			string text5 = text4;
			text4 = string.Concat(new string[]
			{
				text5,
				" LoginTime >='",
				text,
				"' and LoginTime <='",
				text2,
				"'"
			});
			if (!string.IsNullOrEmpty(text3))
			{
				if (string.IsNullOrEmpty(value))
				{
					text5 = text4;
					text4 = string.Concat(new string[]
					{
						text5,
						" and (UserName = '",
						text3,
						"' or IP = '",
						text3,
						"' or Browser = '",
						text3,
						"')"
					});
				}
				if ("1".Equals(value))
				{
					text4 = text4 + " and UserName = '" + text3 + "'";
				}
				if ("2".Equals(value))
				{
					text4 = text4 + " and IP = '" + text3 + "'";
				}
				if ("3".Equals(value))
				{
					text4 = text4 + " and Browser = '" + text3 + "'";
				}
			}
			this.doh.Reset();
			this.doh.ConditionExpress = text4;
			int totalCount = this.doh.Count("V_LogUserLogin");
			string sql = SqlHelp.GetSql0("*", "V_LogUserLogin", "LoginTime", pageSize, num, "desc", text4);
			this.doh.Reset();
			this.doh.SqlCmd = sql;
			DataTable dataTable = this.doh.GetDataTable();
			this._response = string.Concat(new string[]
			{
				"{\"result\" :\"1\",\"returnval\" :\"操作成功\",\"pagebar\" :\"",
				PageBar.GetPageBar(3, "js", 2, totalCount, pageSize, num, "javascript:ajaxList(<#page#>);"),
				"\",",
				dtHelp.DT2JSON(dataTable),
				"}"
			});
			dataTable.Clear();
			dataTable.Dispose();
		}

		private void ajaxUserLoginClear()
		{
			new LogSysDAL().DeleteUserLogs();
			new LogAdminOperDAL().SaveLog(this.AdminId, "0", "系统设置", "管理员清空了登录日志！");
			this._response = base.JsonResult(1, "成功清空");
		}

		private void ajaxGetAdminList()
		{
			string text = base.q("d1");
			string text2 = base.q("d2");
			string text3 = base.q("sel2");
			string text4 = base.q("admin");
			string text5 = base.q("u");
			int num = base.Int_ThisPage();
			int pageSize = base.Str2Int(base.q("pagesize"), 20);
			int num2 = base.Str2Int(base.q("flag"), 0);
			string text6 = "";
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
			string text7 = text6;
			text6 = string.Concat(new string[]
			{
				text7,
				" OperTime >='",
				text,
				"' and OperTime <='",
				text2,
				"'"
			});
			if (!string.IsNullOrEmpty(text3))
			{
				text6 = text6 + " and OperTitle LIKE '" + text3 + "%'";
			}
			if (!string.IsNullOrEmpty(text4))
			{
				text6 = text6 + " and AdminName='" + text4 + "'";
			}
			if (!string.IsNullOrEmpty(text5))
			{
				text6 = text6 + " and UserName= '" + text5 + "'";
			}
			this.doh.Reset();
			this.doh.ConditionExpress = text6;
			int totalCount = this.doh.Count("V_LogAdminOper");
			string sql = SqlHelp.GetSql0("*", "V_LogAdminOper", "OperTime", pageSize, num, "desc", text6);
			this.doh.Reset();
			this.doh.SqlCmd = sql;
			DataTable dataTable = this.doh.GetDataTable();
			this._response = string.Concat(new string[]
			{
				"{\"result\" :\"1\",\"returnval\" :\"操作成功\",\"pagebar\" :\"",
				PageBar.GetPageBar(3, "js", 2, totalCount, pageSize, num, "javascript:ajaxList(<#page#>);"),
				"\",",
				dtHelp.DT2JSON(dataTable),
				"}"
			});
			dataTable.Clear();
			dataTable.Dispose();
		}

		private void ajaxGetErrorList()
		{
			string text = base.q("d1");
			if (text.Trim().Length == 0)
			{
				text = Convert.ToDateTime(this.StartTime).ToString("yyyy-MM-dd");
			}
			if (text.Trim().Length > 0)
			{
				text = Convert.ToDateTime(text).ToString("yyyy-MM-dd");
			}
			string sqlCmd = string.Format("select STime,Userid,dbo.f_GetUserName(Userid) as UserName,charge,remark\r\n                        FROM N_UserMoneyStatAll b where Convert(varchar(10),STime,120)='{0}'\r\n                        and charge>0\r\n                        and (userid not in (\r\n                          SELECT Userid FROM [Pay_SFT_temp] where Convert(varchar(10),STime,120)='{0}'\r\n                        ) and userid not in(\r\n                          SELECT [payUser] FROM [payRecord] where Convert(varchar(10),[payTime],120)='{0}'\r\n                        ) and userid not in(\r\n                          SELECT [UserId] FROM [N_UserCharge] where BankId=888 and  Convert(varchar(10),STime,120)='{0}'\r\n                        )\r\n                        )\r\n                        order by Userid", text);
			this.doh.Reset();
			this.doh.SqlCmd = sqlCmd;
			DataTable dataTable = this.doh.GetDataTable();
			this._response = "{\"result\" :\"1\",\"returnval\" :\"操作成功\"," + dtHelp.DT2JSON(dataTable) + "}";
			dataTable.Clear();
			dataTable.Dispose();
		}

		private void ajaxGetError2List()
		{
			string text = base.q("d1");
			int num = base.Int_ThisPage();
			int pageSize = 100;
			int num2 = base.Str2Int(base.q("flag"), 0);
			string text2 = "";
			if (text.Trim().Length == 0)
			{
				text = this.StartTime;
			}
			this.doh.Reset();
			this.doh.ConditionExpress = text2;
			int totalCount = this.doh.Count("V_CheckCharge");
			string sql = SqlHelp.GetSql0("*", "V_CheckCharge", "Id", pageSize, num, "asc", text2);
			this.doh.Reset();
			this.doh.SqlCmd = sql;
			DataTable dataTable = this.doh.GetDataTable();
			DataTable dataTable2 = this.CreatDataTable();
			for (int i = 0; i < dataTable.Rows.Count; i++)
			{
				bool flag = false;
				DataRow dataRow = dataTable2.NewRow();
				dataRow["userName"] = dataTable.Rows[i]["userName"].ToString();
				dataRow["stime"] = dataTable.Rows[i]["stime"].ToString();
				dataRow["charge"] = dataTable.Rows[i]["charge"].ToString();
				string[] array = dataTable.Rows[i]["remark"].ToString().Split(new char[]
				{
					','
				});
				for (int j = 0; j < array.Length; j++)
				{
					XmlDocument xmlDocument = new XmlDocument();
					xmlDocument.LoadXml(ajaxLogs.query("", "POST"));
					XmlElement documentElement = xmlDocument.DocumentElement;
					XmlNodeList xmlNodeList = documentElement.SelectNodes("/pay/response/is_success");
					dataRow["remark" + (j + 1)] = xmlNodeList[0].InnerText;
					if (xmlNodeList[0].InnerText != "TRUE")
					{
						flag = true;
					}
				}
				if (flag)
				{
					dataTable2.Rows.Add(dataRow);
				}
			}
			this._response = string.Concat(new string[]
			{
				"{\"result\" :\"1\",\"returnval\" :\"加载完成\",\"pagebar\" :\"",
				PageBar.GetPageBar(3, "js", 2, totalCount, pageSize, num, "javascript:ajaxList(<#page#>);"),
				"\",",
				dtHelp.DT2JSON(dataTable2),
				"}"
			});
			dataTable.Clear();
			dataTable.Dispose();
		}

		private static string query(string merchant_order, string method)
		{
			string text = "";
			RestRequest restRequest = new RestRequest("/query/", (method == "POST") ? Method.POST : Method.GET);
			restRequest.AddParameter("order_no", "2015122723564252780");
			restRequest.AddParameter("trade_no", "30636184796920821");
			restRequest.AddParameter("input_charset", "UTF-8");
			restRequest.AddParameter("merchant_code", "19931090");
			List<Parameter> list = (from p in restRequest.Parameters
			orderby p.Name
			select p).ToList<Parameter>();
			list.Add(new Parameter
			{
				Name = "key",
				Value = "bad3bdfc0e984b999bfe0a6ecd16ce7d"
			});
			foreach (Parameter current in list)
			{
				object obj = text;
				text = string.Concat(new object[]
				{
					obj,
					current.Name,
					"=",
					current.Value,
					"&"
				});
			}
			restRequest.AddParameter("sign", ajaxLogs.GetMd5Hash(text.Substring(0, text.Length - 1)));
			RestResponse restResponse = ajaxLogs.SendRequest(restRequest);
			return restResponse.Content;
		}

		public static RestResponse SendRequest(RestRequest request)
		{
			request.AddHeader("Accept", "*/*");
			RestClient restClient = new RestClient("http://pay.41.cn/");
			return (RestResponse)restClient.Execute(request);
		}

		public static string GetMd5Hash(string input)
		{
			string result;
			using (System.Security.Cryptography.MD5 mD = System.Security.Cryptography.MD5.Create())
			{
				byte[] array = mD.ComputeHash(Encoding.UTF8.GetBytes(input));
				StringBuilder stringBuilder = new StringBuilder();
				for (int i = 0; i < array.Length; i++)
				{
					stringBuilder.Append(array[i].ToString("x2"));
				}
				result = stringBuilder.ToString();
			}
			return result;
		}

		private DataTable CreatDataTable()
		{
			return new DataTable
			{
				Columns = 
				{
					{
						"userName",
						typeof(string)
					},
					{
						"stime",
						typeof(string)
					},
					{
						"charge",
						typeof(decimal)
					},
					{
						"remark1",
						typeof(string)
					},
					{
						"remark2",
						typeof(string)
					},
					{
						"remark3",
						typeof(string)
					},
					{
						"remark4",
						typeof(string)
					},
					{
						"remark5",
						typeof(string)
					},
					{
						"remark6",
						typeof(string)
					},
					{
						"remark7",
						typeof(string)
					},
					{
						"remark8",
						typeof(string)
					}
				}
			};
		}

		private string _operType = string.Empty;

		private string _response = string.Empty;
	}
}
