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
	public class ajaxPayRecord : AdminCenter
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
			case "ajaxGetYBList":
				this.ajaxGetYBList();
				goto IL_116;
			case "ajaxGetZFList":
				this.ajaxGetZFList();
				goto IL_116;
			case "ajaxGet18List":
				this.ajaxGet18List();
				goto IL_116;
			case "ajaxGetIpsList":
				this.ajaxGetIpsList();
				goto IL_116;
			case "ajaxGetSftList":
				this.ajaxGetSftList();
				goto IL_116;
			case "ajaxGetSftCheckList":
				this.ajaxGetSftCheckList();
				goto IL_116;
			}
			this.DefaultResponse();
			IL_116:
			base.Response.Write(this._response);
		}

		private void DefaultResponse()
		{
			this._response = base.JsonResult(0, "未知操作");
		}

		private void ajaxGetYBList()
		{
			string text = base.q("keys");
			string text2 = base.q("d1");
			string text3 = base.q("d2");
			int num = base.Int_ThisPage();
			int pageSize = base.Str2Int(base.q("pagesize"), 20);
			int num2 = base.Str2Int(base.q("flag"), 0);
			string text4 = "";
			if (text2.Trim().Length == 0)
			{
				text2 = this.StartTime;
			}
			if (text3.Trim().Length == 0)
			{
				text3 = this.EndTime;
			}
			if (Convert.ToDateTime(text2) > Convert.ToDateTime(text3))
			{
				text2 = text3;
			}
			if (text2.Trim().Length > 0 && text3.Trim().Length > 0)
			{
				string text5 = text4;
				text4 = string.Concat(new string[]
				{
					text5,
					" Convert(varchar(10),order_time,120) >='",
					text2,
					"' and Convert(varchar(10),order_time,120) <='",
					text3,
					"'"
				});
			}
			this.doh.Reset();
			this.doh.ConditionExpress = text4;
			int totalCount = this.doh.Count("Pay_YiBao_temp");
			string sql = SqlHelp.GetSql0("ID,UserId,dbo.f_GetUserName(UserId) as UserName,order_no,order_amount,substring([order_time],1,4)+'-'+substring([order_time],5,2)+'-'+substring([order_time],7,2)+' '+substring([order_time],9,2)+':'+substring([order_time],11,2)+':'+substring([order_time],13,2) as order_time,trade_no,trade_status", "Pay_YiBao_temp", "Id", pageSize, num, "desc", text4);
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

		private void ajaxGetZFList()
		{
			string text = base.q("keys");
			string text2 = base.q("d1");
			string text3 = base.q("d2");
			int num = base.Int_ThisPage();
			int pageSize = base.Str2Int(base.q("pagesize"), 20);
			int num2 = base.Str2Int(base.q("flag"), 0);
			string text4 = "";
			if (text2.Trim().Length == 0)
			{
				text2 = this.StartTime;
			}
			if (text3.Trim().Length == 0)
			{
				text3 = this.EndTime;
			}
			if (Convert.ToDateTime(text2) > Convert.ToDateTime(text3))
			{
				text2 = text3;
			}
			if (text2.Trim().Length > 0 && text3.Trim().Length > 0)
			{
				string text5 = text4;
				text4 = string.Concat(new string[]
				{
					text5,
					" Convert(varchar(10),order_time,120) >='",
					text2,
					"' and Convert(varchar(10),order_time,120) <='",
					text3,
					"'"
				});
			}
			this.doh.Reset();
			this.doh.ConditionExpress = text4;
			int totalCount = this.doh.Count("Pay_Trade_temp");
			string sql = SqlHelp.GetSql0("*,dbo.f_GetUserName(UserId) as UserName", "Pay_Trade_temp", "Id", pageSize, num, "desc", text4);
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

		private void ajaxGet18List()
		{
			string text = base.q("keys");
			string text2 = base.q("d1");
			string text3 = base.q("d2");
			int num = base.Int_ThisPage();
			int pageSize = base.Str2Int(base.q("pagesize"), 20);
			int num2 = base.Str2Int(base.q("flag"), 0);
			string text4 = "";
			if (text2.Trim().Length == 0)
			{
				text2 = this.StartTime;
			}
			if (text3.Trim().Length == 0)
			{
				text3 = this.EndTime;
			}
			if (Convert.ToDateTime(text2) > Convert.ToDateTime(text3))
			{
				text2 = text3;
			}
			if (text2.Trim().Length > 0 && text3.Trim().Length > 0)
			{
				string text5 = text4;
				text4 = string.Concat(new string[]
				{
					text5,
					" Convert(varchar(10),o_time,120) >='",
					text2,
					"' and Convert(varchar(10),o_time,120) <='",
					text3,
					"'"
				});
			}
			this.doh.Reset();
			this.doh.ConditionExpress = text4;
			int totalCount = this.doh.Count("Pay_My18_temp");
			string sql = SqlHelp.GetSql0("*,dbo.f_GetUserName(U_id) as UserName", "Pay_My18_temp", "o_time", pageSize, num, "desc", text4);
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

		private void ajaxGetIpsList()
		{
			string text = base.q("keys");
			string text2 = base.q("d1");
			string text3 = base.q("d2");
			string text4 = base.q("code");
			string text5 = base.q("payrequestid");
			string text6 = base.q("ipsrequestid");
			int num = base.Int_ThisPage();
			int pageSize = base.Str2Int(base.q("pagesize"), 20);
			int num2 = base.Str2Int(base.q("flag"), 0);
			string text7 = "";
			if (text2.Trim().Length == 0)
			{
				text2 = this.StartTime;
			}
			if (text3.Trim().Length == 0)
			{
				text3 = this.EndTime;
			}
			if (Convert.ToDateTime(text2) > Convert.ToDateTime(text3))
			{
				text2 = text3;
			}
			if (text2.Trim().Length > 0 && text3.Trim().Length > 0)
			{
				string text8 = text7;
				text7 = string.Concat(new string[]
				{
					text8,
					" PaySTime >='",
					text2,
					"' and PaySTime <'",
					text3,
					"'"
				});
			}
			if (!string.IsNullOrEmpty(text4))
			{
				text7 = text7 + "  and PayCode='" + text4 + "'";
			}
			if (!string.IsNullOrEmpty(text5))
			{
				text7 = text7 + "  and payrequestid like '%" + text5 + "%'";
			}
			if (!string.IsNullOrEmpty(text6))
			{
				text7 = text7 + "  and ipsrequestid like '%" + text6 + "%'";
			}
			this.doh.Reset();
			this.doh.ConditionExpress = text7;
			int totalCount = this.doh.Count("Pay_temp");
			string sql = SqlHelp.GetSql0("dbo.f_GetUserName(UserId) as UserName,(select MerName from Sys_ChargeSet where Id=a.PayCode) as PayName,*", "Pay_temp a", "Id", pageSize, num, "desc", text7);
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

		private void ajaxGetSftList()
		{
			string text = base.q("keys");
			string text2 = base.q("d1");
			string text3 = base.q("d2");
			int num = base.Int_ThisPage();
			int pageSize = base.Str2Int(base.q("pagesize"), 20);
			int num2 = base.Str2Int(base.q("flag"), 0);
			string text4 = "";
			if (text2.Trim().Length == 0)
			{
				text2 = this.StartTime;
			}
			if (text3.Trim().Length == 0)
			{
				text3 = this.EndTime;
			}
			if (Convert.ToDateTime(text2) > Convert.ToDateTime(text3))
			{
				text2 = text3;
			}
			if (text2.Trim().Length > 0 && text3.Trim().Length > 0)
			{
				string text5 = text4;
				text4 = string.Concat(new string[]
				{
					text5,
					" STime >='",
					text2,
					"' and STime <'",
					text3,
					"'"
				});
			}
			this.doh.Reset();
			this.doh.ConditionExpress = text4;
			int totalCount = this.doh.Count("Pay_SFT_temp");
			string sql = SqlHelp.GetSql0("dbo.f_GetUserName(UserId) as UserName,*", "Pay_SFT_temp", "Id", pageSize, num, "desc", text4);
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

		private void ajaxGetSftCheckList()
		{
			string text = base.q("keys");
			string text2 = base.q("d1");
			string text3 = base.q("d2");
			int num = base.Int_ThisPage();
			int pageSize = 500;
			int num2 = base.Str2Int(base.q("flag"), 0);
			string text4 = "";
			if (text2.Trim().Length == 0)
			{
				text2 = this.StartTime;
			}
			if (text3.Trim().Length == 0)
			{
				text3 = this.EndTime;
			}
			if (Convert.ToDateTime(text2) > Convert.ToDateTime(text3))
			{
				text2 = text3;
			}
			if (text2.Trim().Length > 0 && text3.Trim().Length > 0)
			{
				string text5 = text4;
				text4 = string.Concat(new string[]
				{
					text5,
					" STime >='",
					text2,
					"' and STime <'",
					text3,
					"'"
				});
			}
			this.doh.Reset();
			this.doh.ConditionExpress = text4;
			int totalCount = this.doh.Count("Pay_SFT_temp");
			string sql = SqlHelp.GetSql0("dbo.f_GetUserName(UserId) as UserName,*", "Pay_SFT_temp", "Id", pageSize, num, "desc", text4);
			this.doh.Reset();
			this.doh.SqlCmd = sql;
			DataTable dataTable = this.doh.GetDataTable();
			DataTable dataTable2 = this.CreatDataTable();
			for (int i = 0; i < dataTable.Rows.Count; i++)
			{
				DataRow dataRow = dataTable2.NewRow();
				dataRow["rowid"] = string.Concat(i + 1);
				dataRow["UserId"] = dataTable.Rows[i]["UserId"].ToString();
				dataRow["UserName"] = dataTable.Rows[i]["UserName"].ToString();
				dataRow["billno"] = dataTable.Rows[i]["billno"].ToString();
				dataRow["amount"] = dataTable.Rows[i]["amount"].ToString();
				dataRow["ordertime"] = dataTable.Rows[i]["ordertime"].ToString();
				dataRow["ipsbillno"] = dataTable.Rows[i]["ipsbillno"].ToString();
				dataRow["stime"] = dataTable.Rows[i]["stime"].ToString();
				dataRow["status"] = dataTable.Rows[i]["status"].ToString();
				string text6 = dataTable.Rows[i]["UserId"].ToString();
				XmlDocument xmlDocument = new XmlDocument();
				xmlDocument.LoadXml(ajaxPayRecord.query(dataTable.Rows[i]["billno"].ToString(), "POST"));
				XmlElement documentElement = xmlDocument.DocumentElement;
				XmlNodeList xmlNodeList = documentElement.SelectNodes("/pay/response/is_success");
				XmlNodeList xmlNodeList2 = documentElement.SelectNodes("/pay/response/order_amount");
				XmlNodeList xmlNodeList3 = documentElement.SelectNodes("/pay/response/trade_no");
				XmlNodeList xmlNodeList4 = documentElement.SelectNodes("/pay/response/order_no");
				if (xmlNodeList[0].InnerText != "TRUE")
				{
					dataRow["remark"] = "对账错误";
				}
				else if (dataTable.Rows[i]["billno"].ToString().Length == 19)
				{
					if (Convert.ToDecimal(xmlNodeList2[0].InnerText) != Convert.ToDecimal(dataTable.Rows[i]["amount"].ToString()))
					{
						dataRow["remark"] = "对账错误";
					}
					else if (!xmlNodeList3[0].InnerText.Equals(dataTable.Rows[i]["ipsbillno"].ToString()))
					{
						dataRow["remark"] = "对账错误";
					}
					else
					{
						dataRow["remark"] = "对账通过";
					}
				}
				else if (Convert.ToDecimal(xmlNodeList2[0].InnerText) != Convert.ToDecimal(dataTable.Rows[i]["amount"].ToString()))
				{
					dataRow["remark"] = "对账错误";
				}
				else if (!xmlNodeList3[0].InnerText.Equals(dataTable.Rows[i]["ipsbillno"].ToString()))
				{
					dataRow["remark"] = "对账错误";
				}
				else if (!xmlNodeList4[0].InnerText.Substring(15, text6.Length).Equals(text6))
				{
					dataRow["remark"] = "对账错误";
				}
				else
				{
					dataRow["remark"] = "对账通过";
				}
				dataTable2.Rows.Add(dataRow);
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
			restRequest.AddParameter("order_no", merchant_order);
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
			restRequest.AddParameter("sign", ajaxPayRecord.GetMd5Hash(text.Substring(0, text.Length - 1)));
			RestResponse restResponse = ajaxPayRecord.SendRequest(restRequest);
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
						"rowid",
						typeof(string)
					},
					{
						"UserId",
						typeof(string)
					},
					{
						"UserName",
						typeof(string)
					},
					{
						"billno",
						typeof(string)
					},
					{
						"amount",
						typeof(decimal)
					},
					{
						"ordertime",
						typeof(string)
					},
					{
						"ipsbillno",
						typeof(string)
					},
					{
						"stime",
						typeof(string)
					},
					{
						"status",
						typeof(string)
					},
					{
						"remark",
						typeof(string)
					}
				}
			};
		}

		private string _operType = string.Empty;

		private string _response = string.Empty;
	}
}
