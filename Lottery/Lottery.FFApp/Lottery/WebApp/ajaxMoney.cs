using System;
using Lottery.DAL;
using Lottery.DAL.Flex;

namespace Lottery.WebApp
{
	public class ajaxMoney : UserCenterSession
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			base.Admin_Load("", "json");
			this._operType = base.q("oper");
			string operType = this._operType;
			if (operType != null)
			{
				if (operType == "ajaxCharge")
				{
					this.ajaxCharge();
					goto IL_A6;
				}
				if (operType == "ajaxGetChargeList")
				{
					this.ajaxGetChargeList();
					goto IL_A6;
				}
				if (operType == "ajaxCash")
				{
					this.ajaxCash();
					goto IL_A6;
				}
				if (operType == "ajaxGetCashList")
				{
					this.ajaxGetCashList();
					goto IL_A6;
				}
				if (operType == "ajaxGetTranAccList")
				{
					this.ajaxGetTranAccList();
					goto IL_A6;
				}
			}
			this.DefaultResponse();
			IL_A6:
			base.Response.Write(this._response);
		}

		private void DefaultResponse()
		{
			this._response = base.JsonResult(0, "未知操作");
		}

		private void ajaxCharge()
		{
			string bankId = base.f("setid");
			string text = base.f("name");
			string value = base.f("money");
			string text2 = base.f("code");
			int num = new Lottery.DAL.Flex.UserChargeDAL().Save(this.AdminId, bankId, text.Trim(), Convert.ToDecimal(value));
			if (num == -1)
			{
				this._response = base.JsonResult(0, "充值金额不能小于最小充值金额!");
			}
			else if (num > 0)
			{
				this._response = base.JsonResult(1, this.AdminId.ToString());
			}
			else
			{
				this._response = base.JsonResult(0, "充值失败");
			}
		}

		private void ajaxGetChargeList()
		{
			string text = base.q("state");
			string text2 = base.q("d1") + " 00:00:00";
			string text3 = base.q("d2") + " 23:59:59";
			int thispage = base.Int_ThisPage();
			int pagesize = base.Str2Int(base.q("pagesize"), 20);
			int num = base.Str2Int(base.q("flag"), 0);
			string text4 = "UserId =" + this.AdminId;
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
					" and STime >='",
					text2,
					"' and STime <='",
					text3,
					"'"
				});
			}
			if (!string.IsNullOrEmpty(text))
			{
				text4 = text4 + " and state=" + text;
			}
			string response = "";
			new WebAppListOper().GetChargeListJSON(thispage, pagesize, text4, ref response);
			this._response = response;
		}

		private void ajaxCash()
		{
			string bankId = base.f("bankId");
			string userBankId = base.f("userBankId");
			string money = base.f("money");
			string passWord = base.f("pass");
			string text = base.f("code");
			string str = new UserGetCashDAL().UserGetCash(this.AdminId, userBankId, bankId, money, passWord);
			this._response = base.JsonResult(0, str);
		}

		private void ajaxGetCashList()
		{
			string text = base.q("d1") + " 00:00:00";
			string text2 = base.q("d2") + " 23:59:59";
			string text3 = base.q("state");
			int thispage = base.Int_ThisPage();
			int pagesize = base.Str2Int(base.q("pagesize"), 20);
			int num = base.Str2Int(base.q("flag"), 0);
			string text4 = "UserId =" + this.AdminId;
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
				string text5 = text4;
				text4 = string.Concat(new string[]
				{
					text5,
					" and STime >='",
					text,
					"' and STime <='",
					text2,
					"'"
				});
			}
			if (!string.IsNullOrEmpty(text3))
			{
				text4 = text4 + " and State =" + text3;
			}
			string response = "";
			new WebAppListOper().GetCashListJSON(thispage, pagesize, text4, ref response);
			this._response = response;
		}

		private void ajaxGetTranAccList()
		{
			string text = base.q("d1") + " 00:00:00";
			string text2 = base.q("d2") + " 23:59:59";
			string text3 = base.q("state");
			int thispage = base.Int_ThisPage();
			int pagesize = base.Str2Int(base.q("pagesize"), 20);
			int num = base.Str2Int(base.q("flag"), 0);
			string text4 = string.Format("(UserId ={0} or ToUserId={0})", this.AdminId);
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
				string text5 = text4;
				text4 = string.Concat(new string[]
				{
					text5,
					" and STime >='",
					text,
					"' and STime <='",
					text2,
					"'"
				});
			}
			if (!string.IsNullOrEmpty(text3))
			{
				text4 = text4 + " and Type =" + text3;
			}
			string response = "";
			new WebAppListOper().GetTranAccListJSON(thispage, pagesize, text4, ref response);
			this._response = response;
		}

		private string _operType = string.Empty;

		private string _response = string.Empty;
	}
}
