using System;
using Lottery.DAL;

namespace Lottery.IPhone
{
	public class ajaxEmail : UserCenterSession
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			base.Admin_Load("", "json");
			this._operType = base.q("oper");
			string operType = this._operType;
			switch (operType)
			{
			case "ajaxGetSendList":
				this.ajaxGetSendList();
				goto IL_14A;
			case "ajaxGetReceiveList":
				this.ajaxGetReceiveList();
				goto IL_14A;
			case "ajaxGetNewsContent":
				this.ajaxGetNewsContent();
				goto IL_14A;
			case "ajaxGetListCount":
				this.ajaxGetListCount();
				goto IL_14A;
			case "ajaxAllState":
				this.ajaxAllState();
				goto IL_14A;
			case "ajaxAllDel":
				this.ajaxAllDel();
				goto IL_14A;
			case "ajaxDel":
				this.ajaxDel();
				goto IL_14A;
			case "ajaxGetUserList":
				this.ajaxGetUserList();
				goto IL_14A;
			case "Send":
				this.Send();
				goto IL_14A;
			}
			this.DefaultResponse();
			IL_14A:
			base.Response.Write(this._response);
		}

		private void DefaultResponse()
		{
			this._response = base.JsonResult(0, "未知操作");
		}

		private void ajaxGetSendList()
		{
			string text = base.q("d1");
			string text2 = base.q("d2");
			int thispage = base.Int_ThisPage();
			int pagesize = base.Str2Int(base.q("pagesize"), 20);
			if (text.Trim().Length == 0)
			{
				text = DateTime.Now.AddDays(-10.0).ToString("yyyy-MM-dd HH:mm:ss");
			}
			if (text2.Trim().Length == 0)
			{
				text2 = this.EndTime;
			}
			if (Convert.ToDateTime(text) > Convert.ToDateTime(text2))
			{
				text = text2;
			}
			string text3 = "IsDel=0 and SendId =" + this.AdminId;
			if (text.Trim().Length > 0 && text2.Trim().Length > 0)
			{
				string text4 = text3;
				text3 = string.Concat(new string[]
				{
					text4,
					" and STime >='",
					text,
					"' and STime <='",
					text2,
					"'"
				});
			}
			string response = "";
			new UserEmailDAL().GetListJSON(thispage, pagesize, text3, this.AdminId, ref response);
			this._response = response;
		}

		private void ajaxGetReceiveList()
		{
			string text = base.q("d1");
			string text2 = base.q("d2");
			string a = base.q("type");
			int thispage = base.Int_ThisPage();
			int pagesize = base.Str2Int(base.q("pagesize"), 20);
			if (text.Trim().Length == 0)
			{
				text = DateTime.Now.AddDays(-10.0).ToString("yyyy-MM-dd HH:mm:ss");
			}
			if (text2.Trim().Length == 0)
			{
				text2 = this.EndTime;
			}
			if (Convert.ToDateTime(text) > Convert.ToDateTime(text2))
			{
				text = text2;
			}
			string text3 = "IsDel=0 and ReceiveId =" + this.AdminId;
			if (a == "0")
			{
				text3 += " and IsRead =0";
			}
			if (a == "1")
			{
				text3 += " and IsRead =0";
			}
			if (text.Trim().Length > 0 && text2.Trim().Length > 0)
			{
				string text4 = text3;
				text3 = string.Concat(new string[]
				{
					text4,
					" and STime >='",
					text,
					"' and STime <='",
					text2,
					"'"
				});
			}
			string response = "";
			new UserEmailDAL().GetListJSON(thispage, pagesize, text3, this.AdminId, ref response);
			this._response = response;
		}

		private void ajaxGetNewsContent()
		{
			string str = base.q("id");
			string text = "id =" + str;
			string text2 = "";
			this._response = text2.Replace("<br/>", "");
		}

		private void ajaxGetListCount()
		{
			string wherestr = "ReceiveId =" + this.AdminId + " and IsRead =0";
			string text = "";
			new UserEmailDAL().GetListCount(wherestr, ref text);
			this._response = text.Replace("<br/>", "");
		}

		private void ajaxAllDel()
		{
			string text = base.f("ids");
			string[] array = text.Split(new char[]
			{
				','
			});
			for (int i = 0; i < array.Length; i++)
			{
				new UserEmailDAL().Deletes(array[i]);
			}
		}

		private void ajaxAllState()
		{
			string text = base.f("ids");
			string[] array = text.Split(new char[]
			{
				','
			});
			for (int i = 0; i < array.Length; i++)
			{
				new UserEmailDAL().UpdateState(array[i]);
			}
		}

		private void ajaxDel()
		{
			string id = base.f("id");
			new UserEmailDAL().Deletes(id);
		}

		private void ajaxGetUserList()
		{
			string wherestr = "parentId=" + this.AdminId;
			string response = "";
			new UserDAL().GetListJSON(1, 99999, wherestr, "desc", "Id", ref response);
			this._response = response;
		}

		private void Send()
		{
			string title = base.f("title");
			string contents = base.f("content");
			string text = base.f("type");
			string conditionValue = base.f("name");
			if (text.Equals("1"))
			{
				this.doh.Reset();
				this.doh.ConditionExpress = "Id=@Id";
				this.doh.AddConditionParameter("@Id", this.AdminId);
				string text2 = string.Concat(this.doh.GetField("N_User", "ParentId"));
				if (text2.Equals("0"))
				{
					this._response = base.JsonResult(1, "您没有上级不能发送!");
				}
				else
				{
					int num = new UserEmailDAL().Save(this.AdminId, text2, title, contents);
					if (num > 0)
					{
						new LogSysDAL().Save("会员管理", "Id为" + this.AdminId + "的会员发送邮件！");
						this._response = base.JsonResult(1, "邮件发送成功!");
					}
					else
					{
						this._response = base.JsonResult(1, "邮件发送失败!");
					}
				}
			}
			else
			{
				this.doh.Reset();
				this.doh.ConditionExpress = "UserName=@UserName";
				this.doh.AddConditionParameter("@UserName", conditionValue);
				string text3 = string.Concat(this.doh.GetField("N_User", "Id"));
				if (string.IsNullOrEmpty(text3))
				{
					this._response = base.JsonResult(1, "您输入的账号不正确!");
				}
				else
				{
					new UserEmailDAL().Save(this.AdminId, text3, title, contents);
					new LogSysDAL().Save("会员管理", "Id为" + this.AdminId + "的会员发送邮件！");
					this._response = base.JsonResult(1, "邮件发送成功!");
				}
			}
		}

		private string _operType = string.Empty;

		private string _response = string.Empty;
	}
}
