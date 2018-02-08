using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Lottery.DAL;
using Lottery.DAL.Flex;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;

namespace Lottery.Admin
{
	public class usercashedit : AdminCenter
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			base.Admin_Load("", "html");
			if (!base.IsPostBack)
			{
				string str = this.txtId.Text = base.Str2Str(base.q("id"));
				this.doh.Reset();
				this.doh.SqlCmd = "select top 1 * from Flex_UserGetCash where Id=" + str;
				DataTable dataTable = this.doh.GetDataTable();
				this.orderNo.Value = dataTable.Rows[0]["ssid"].ToString();
				this.tradeDate.Value = DateTime.Now.ToString("yyyyMMdd");
				this.Amt.Value = Convert.ToDouble(dataTable.Rows[0]["CashMoney"].ToString()).ToString("0.00");
				this.bankAccName.Value = dataTable.Rows[0]["PayName"].ToString();
				this.bankName.Value = dataTable.Rows[0]["PayBankAddress"].ToString();
				this.bankCode.Value = dataTable.Rows[0]["bankCode"].ToString();
				this.bankAccNo.Value = dataTable.Rows[0]["PayAccount"].ToString();
				this.lblUserName.Text = dataTable.Rows[0]["UserName"].ToString();
				this.lblBank.Text = dataTable.Rows[0]["PayBank"].ToString();
				this.txtPayName.Text = dataTable.Rows[0]["PayName"].ToString();
				this.txtPayAccount.Text = dataTable.Rows[0]["PayAccount"].ToString();
				this.txtMoney.Text = Convert.ToDouble(dataTable.Rows[0]["CashMoney"].ToString()).ToString("0.00");
				if (string.IsNullOrEmpty(this.bankName.Value))
				{
					this.bankName.Value = dataTable.Rows[0]["PayBank"].ToString();
				}
				this.doh.Reset();
				this.doh.SqlCmd = "select top 1 * from N_UserBank where replace(PayAccount,' ','')='" + dataTable.Rows[0]["PayAccount"].ToString().Replace(" ", "").Trim() + "' and UserId=" + dataTable.Rows[0]["UserId"].ToString();
				DataTable dataTable2 = this.doh.GetDataTable();
				if (dataTable2.Rows.Count < 1)
				{
					this.lblBankCode.Text = "-1";
					this.lblBank.Text = "取款的卡号与会员绑定的卡号不符，请仔细查看！";
				}
				else if (string.IsNullOrEmpty(string.Concat(dataTable.Rows[0]["PayMethod"])))
				{
					this.lblBankCode.Text = "-1";
					this.lblBank.Text = "未绑定银行信息或者提现后删除了银行信息,请从新绑定！";
				}
				else
				{
					this.doh.Reset();
					this.doh.ConditionExpress = "id=" + dataTable.Rows[0]["PayMethod"].ToString();
					object[] fields = this.doh.GetFields("Sys_Bank", "Url,Code");
					this.url = string.Concat(fields[0]);
					this.lblBankCode.Text = string.Concat(fields[1]);
				}
			}
		}

		protected void btnSave_Click(object sender, EventArgs e)
		{
			if (string.IsNullOrEmpty(this.bankAccName.Value) || string.IsNullOrEmpty(this.bankName.Value) || string.IsNullOrEmpty(this.bankCode.Value) || string.IsNullOrEmpty(this.bankAccNo.Value))
			{
				base.FinalMessage("银行信息不完整，不能处理！", "/admin/close.htm", 0);
			}
			else
			{
				if (this.rbo2.Checked)
				{
					if (this.lblBankCode.Text.Equals("-1"))
					{
						base.FinalMessage(this.lblBank.Text, "/admin/close.htm", 0);
					}
					else if (new UserGetCashDAL().Exists(" (state=0 or state=99) and Id=" + this.txtId.Text))
					{
						new UserGetCashDAL().Check(this.txtId.Text, this.txtSeason.Text, 1);
						new LogAdminOperDAL().SaveLog(this.AdminId, this.txtId.Text, "会员取款", "同意会员" + this.lblUserName.Text + "的提现申请（手工）");
						base.FinalMessage("操作成功", "/admin/close.htm", 0);
					}
					else
					{
						base.FinalMessage("该提现请求已处理，不能重复处理！", "/admin/close.htm", 0);
					}
				}
				if (this.rbo3.Checked)
				{
					if (new UserGetCashDAL().Exists(" (state=0 or state=99) and Id=" + this.txtId.Text))
					{
						new UserGetCashDAL().Check(this.txtId.Text, this.txtSeason.Text, 2);
						new LogAdminOperDAL().SaveLog(this.AdminId, this.txtId.Text, "会员取款", "拒绝会员" + this.lblUserName.Text + "的提现申请");
						base.FinalMessage("操作成功", "/admin/close.htm", 0);
					}
					else
					{
						base.FinalMessage("该提现请求已处理，不能重复处理！", "/admin/close.htm", 0);
					}
				}
				if (this.rbo1.Checked)
				{
					if (new UserGetCashDAL().Exists(" (state=0 or state=99) and Id=" + this.txtId.Text))
					{
						string code = ConfigurationManager.AppSettings["codeMiao"].ToString();
						string key = ConfigurationManager.AppSettings["keyMiao"].ToString();
						string value = this.remitMiao(code, key);
						if (string.IsNullOrEmpty(value))
						{
							base.FinalMessage("连接支付平台错误！", "/admin/close.htm", 0);
						}
						else
						{
							JObject jObject = (JObject)JsonConvert.DeserializeObject(value);
							string a = jObject["is_success"].ToString();
							if (a == "true")
							{
								new UserGetCashDAL().Check(this.txtId.Text, this.txtSeason.Text, 1);
								new LogAdminOperDAL().SaveLog(this.AdminId, this.txtId.Text, "会员取款", "同意会员" + this.lblUserName.Text + "的提现申请（国盛通微信）");
								base.FinalMessage("取款成功", "/admin/close.htm", 0);
							}
							else
							{
								string pageMsg = jObject["errror_msg"].ToString();
								base.FinalMessage(pageMsg, "/admin/close.htm", 0);
							}
						}
					}
					else
					{
						base.FinalMessage("该提现请求已处理，不能重复处理！", "/admin/close.htm", 0);
					}
				}
			}
		}

		private string remitMiao(string code, string key)
		{
			string text = "";
			RestRequest restRequest = new RestRequest("", Method.POST);
			restRequest.AddParameter("account_name", this.bankAccName.Value);
			restRequest.AddParameter("account_number", this.bankAccNo.Value);
			restRequest.AddParameter("amount", this.Amt.Value);
			restRequest.AddParameter("bank_name", this.bankCode.Value);
			restRequest.AddParameter("bitch_no", this.orderNo.Value);
			restRequest.AddParameter("currentDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
			restRequest.AddParameter("input_charset", "UTF-8");
			restRequest.AddParameter("merchant_code", code);
			restRequest.AddParameter("transid", this.orderNo.Value);
			List<RestSharp.Parameter> list = (from p in restRequest.Parameters
			orderby p.Name
			select p).ToList<RestSharp.Parameter>();
			list.Add(new RestSharp.Parameter
			{
				Name = "key",
				Value = key
			});
			foreach (RestSharp.Parameter current in list)
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
			string text2 = usercashedit.md5(text.Substring(0, text.Length - 1)).ToUpper();
			text2 = usercashedit.md5(text2);
			text2 = usercashedit.md5(text2);
			restRequest.AddParameter("sign", text2);
			RestResponse restResponse = usercashedit.SendRequestMiao(restRequest);
			return restResponse.Content;
		}

		public static RestResponse SendRequestMiao(RestRequest request)
		{
			request.AddHeader("Accept", "*/*");
			RestClient restClient = new RestClient("http://df.likan.top/gateway/dfm.html");
			return (RestResponse)restClient.Execute(request);
		}

		private string remit(string code, string key)
		{
			string text = "";
			RestRequest restRequest = new RestRequest("", Method.POST);
			restRequest.AddParameter("account_name", this.bankAccName.Value);
			restRequest.AddParameter("account_number", this.bankAccNo.Value);
			restRequest.AddParameter("amount", this.Amt.Value);
			restRequest.AddParameter("bank_name", this.bankCode.Value);
			restRequest.AddParameter("bitch_no", this.orderNo.Value);
			restRequest.AddParameter("currentDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
			restRequest.AddParameter("input_charset", "UTF-8");
			restRequest.AddParameter("merchant_code", code);
			restRequest.AddParameter("transid", this.orderNo.Value);
			List<RestSharp.Parameter> list = (from p in restRequest.Parameters
			orderby p.Name
			select p).ToList<RestSharp.Parameter>();
			list.Add(new RestSharp.Parameter
			{
				Name = "key",
				Value = key
			});
			foreach (RestSharp.Parameter current in list)
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
			string text2 = usercashedit.md5(text.Substring(0, text.Length - 1)).ToUpper();
			text2 = usercashedit.md5(text2);
			text2 = usercashedit.md5(text2);
			restRequest.AddParameter("sign", text2);
			RestResponse restResponse = usercashedit.SendRequest(restRequest);
			return restResponse.Content;
		}

		public static RestResponse SendRequest(RestRequest request)
		{
			request.AddHeader("Accept", "*/*");
			RestClient restClient = new RestClient("http://df.9stpay.com/gstpay/gateway/dfm.html");
			return (RestResponse)restClient.Execute(request);
		}

		public static string md5(string str)
		{
			byte[] array = Encoding.UTF8.GetBytes(str);
			array = new MD5CryptoServiceProvider().ComputeHash(array);
			string text = "";
			for (int i = 0; i < array.Length; i++)
			{
				text += array[i].ToString("x").PadLeft(2, '0');
			}
			return text;
		}

		public static string GetMd5Hash(string input)
		{
			string result;
			using (MD5 mD = MD5.Create())
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

		protected HtmlForm form1;

		protected Label lblBank;

		protected Label lblBankCode;

		protected Label lblUserName;

		protected Label txtPayName;

		protected Label txtPayAccount;

		protected Label txtMoney;

		protected RadioButton rbo2;

		protected RadioButton rbo3;

		protected RadioButton rbo1;

		protected TextBox txtSeason;

		protected TextBox txtId;

		protected Label lblMsg;

		protected Button btnSave;

		protected HtmlInputHidden orderNo;

		protected HtmlInputHidden tradeDate;

		protected HtmlInputHidden Amt;

		protected HtmlInputHidden bankAccName;

		protected HtmlInputHidden bankName;

		protected HtmlInputHidden bankCode;

		protected HtmlInputHidden bankAccNo;

		public string url = "";
	}
}
