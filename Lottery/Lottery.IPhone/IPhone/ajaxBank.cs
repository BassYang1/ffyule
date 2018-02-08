using System;
using Lottery.DAL;
using Lottery.DAL.Flex;

namespace Lottery.IPhone
{
	public class ajaxBank : UserCenterSession
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
			if (operType != null)
			{
				if (operType == "ajaxGetList")
				{
					this.ajaxGetList();
					goto IL_92;
				}
				if (operType == "ajaxGetChargeSetList")
				{
					this.ajaxGetChargeSetList();
					goto IL_92;
				}
				if (operType == "ajaxAddBank")
				{
					this.ajaxAddBank();
					goto IL_92;
				}
			}
			this.DefaultResponse();
			IL_92:
			base.Response.Write(this._response);
		}

		private void DefaultResponse()
		{
			this._response = base.JsonResult(0, "未知操作");
		}

		private void ajaxGetList()
		{
			string response = "";
			new Lottery.DAL.Flex.UserBankDAL().GetIphoneBankInfoJSON(this.AdminId, ref response);
			this._response = response;
		}

		private void ajaxGetChargeSetList()
		{
			string id = base.q("id");
			string response = "";
			new Lottery.DAL.Flex.UserBankDAL().GetIphoneChargeSetJSON(id, ref response);
			this._response = response;
		}

		private void ajaxAddBank()
		{
			string payMethod = base.f("Bank");
			string payBank = base.f("BankName");
			string payBankAddress = base.f("Address");
			string payAccount = base.f("PayAccount");
			string payName = base.f("PayName");
			string text = base.f("PayPwd");
			string response = new Lottery.DAL.Flex.UserBankDAL().Save(this.AdminId, payMethod, payBank, payBankAddress, payAccount, payName, "", "");
			this._response = response;
		}

		private string _operType = string.Empty;

		private string _response = string.Empty;
	}
}
