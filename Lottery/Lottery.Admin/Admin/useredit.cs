using System;
using System.Data;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Lottery.DAL;
using Lottery.Utils;

namespace Lottery.Admin
{
	public class useredit : AdminCenter
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			base.Admin_Load("master", "html");
			string str = this.txtId.Text = base.Str2Str(base.q("id"));
			base.getEditDropDownList(ref this.ddlPoint, 0);
			base.getGroupDropDownList(ref this.ddlGroup, 0);
			if (!base.IsPostBack)
			{
				this.doh.Reset();
				this.doh.SqlCmd = "select top 1 * from V_User where Id=" + str;
				DataTable dataTable = this.doh.GetDataTable();
				this.txtId.Text = dataTable.Rows[0]["Id"].ToString();
				this.txtName.Text = dataTable.Rows[0]["UserName"].ToString();
				this.txtParent.Text = dataTable.Rows[0]["ParentName"].ToString();
				this.ddlGroup.SelectedValue = Convert.ToInt32(dataTable.Rows[0]["UserGroup"]).ToString();
				this.txtGroup.Text = dataTable.Rows[0]["UserGroup"].ToString();
				this.txtMoney.Text = dataTable.Rows[0]["Money"].ToString();
				this.txtScore.Text = dataTable.Rows[0]["Score"].ToString();
				this.ddlPoint.SelectedValue = dataTable.Rows[0]["UPoint"].ToString();
				this.txtPoint.Text = dataTable.Rows[0]["UPoint"].ToString();
				this.txtQuestion.Text = dataTable.Rows[0]["Question"].ToString();
				this.txtAnswer.Text = (string.IsNullOrEmpty(dataTable.Rows[0]["Answer"].ToString()) ? "" : (dataTable.Rows[0]["Answer"].ToString().Substring(0, 1) + "*"));
				this.tipAnswer.Text = dataTable.Rows[0]["Answer"].ToString();
				this.txtRegtime.Text = dataTable.Rows[0]["RegTime"].ToString();
				this.txtOntime.Text = dataTable.Rows[0]["OnTime"].ToString();
				this.txtIp.Text = dataTable.Rows[0]["IP"].ToString();
				this.txtOnline.Text = ((Convert.ToInt32(dataTable.Rows[0]["IsOnline"].ToString()) == 0) ? "离线" : "在线");
				this.lblCookie.Text = dataTable.Rows[0]["SessionId"].ToString();
				this.ddlIsEnable.SelectedValue = dataTable.Rows[0]["IsEnable"].ToString();
				this.ddlIsBet.SelectedValue = dataTable.Rows[0]["IsBet"].ToString();
				this.ddlIsGetcash.SelectedValue = dataTable.Rows[0]["IsGetcash"].ToString();
				this.ddlIsTranAcc.SelectedValue = dataTable.Rows[0]["IsTranAcc"].ToString();
				this.txtEnableSeason.Text = dataTable.Rows[0]["EnableSeason"].ToString();
				this.trueName = dataTable.Rows[0]["TrueName"].ToString();
				this.txtPayName.Text = this.trueName;
				this.tipPayName.Text = this.trueName;
				this.txtMobile.Text = (string.IsNullOrEmpty(dataTable.Rows[0]["Mobile"].ToString()) ? "" : (dataTable.Rows[0]["Mobile"].ToString().Substring(0, dataTable.Rows[0]["Mobile"].ToString().Length - 3) + "***"));
				this.txtEmail.Text = (string.IsNullOrEmpty(dataTable.Rows[0]["Email"].ToString()) ? "" : (dataTable.Rows[0]["Email"].ToString().Substring(0, dataTable.Rows[0]["Email"].ToString().Length - 3) + "***"));
				this.doh.Reset();
				this.doh.SqlCmd = "select top 1 * from N_UserBank where IsLock=1 and UserId=" + str + "order by Id desc";
				DataTable dataTable2 = this.doh.GetDataTable();
				if (dataTable2.Rows.Count < 1)
				{
					this.doh.Reset();
					this.doh.SqlCmd = "select top 1 * from N_UserBank where IsLock=1 and UserId=" + str + "order by Id desc";
					dataTable2 = this.doh.GetDataTable();
				}
				if (dataTable2.Rows.Count > 0)
				{
					string text = dataTable2.Rows[0]["PayBank"].ToString();
					this.txtPayBank.Text = (string.IsNullOrEmpty(text) ? "" : (text.Substring(0, 1) + "***" + text.Substring(text.Length - 1, 1)));
					string text2 = dataTable2.Rows[0]["PayAccount"].ToString();
					this.txtPayAccount.Text = (string.IsNullOrEmpty(text2) ? "" : (text2.Substring(0, 4) + "***" + text2.Substring(text2.Length - 4, 3)));
					string text3 = dataTable2.Rows[0]["PayBankAddress"].ToString();
					this.txtPayBankAddress.Text = (string.IsNullOrEmpty(text3) ? "" : (text3.Substring(0, 1) + "***" + text3.Substring(text3.Length - 1, 1)));
				}
				else
				{
					this.txtPayBank.Text = "---";
					this.txtPayAccount.Text = "---";
					this.txtPayBankAddress.Text = "---";
				}
				string[] array = dataTable.Rows[0]["UserCode"].ToString().Replace(",,", "_").Replace(",", "").Split(new char[]
				{
					'_'
				});
				if (array.Length > 1)
				{
					for (int i = 0; i < array.Length - 1; i++)
					{
						if (!string.IsNullOrEmpty(array[i]))
						{
							this.doh.Reset();
							this.doh.ConditionExpress = "Id=" + array[i];
							TextBox expr_813 = this.txtCode;
							expr_813.Text = expr_813.Text + this.doh.GetField("N_User", "UserName") + "||";
						}
					}
					this.txtCode.Text = this.txtCode.Text.Substring(0, this.txtCode.Text.Length - 2);
				}
				else
				{
					this.txtCode.Text = "---";
				}
			}
		}

		protected void btnSave_Click(object sender, EventArgs e)
		{
			ListItem selectedItem = this.ddlGroup.SelectedItem;
			ListItem selectedItem2 = this.ddlPoint.SelectedItem;
			if (!this.CheckUserGroup(this.txtId.Text, selectedItem.Value))
			{
				this.lblmsg.Text = "会员类型不能高于上级或和上级持平！";
			}
			else if (!this.CheckUserPoint(this.txtId.Text, selectedItem2.Value))
			{
				this.lblmsg.Text = "会员返点不能高于上级返点！";
			}
			else
			{
				this.doh.Reset();
				this.doh.ConditionExpress = "Id=" + this.txtId.Text;
				if (!string.IsNullOrEmpty(this.txtLoginPwd.Text))
				{
					this.doh.AddFieldItem("Password", MD5.Last64(MD5.Lower32(this.txtLoginPwd.Text)));
					new LogAdminOperDAL().SaveLog(this.AdminId, this.txtId.Text, "登录密码", "修改登录密码");
				}
				if (!string.IsNullOrEmpty(this.txtBankPwd.Text))
				{
					this.doh.AddFieldItem("PayPass", MD5.Last64(MD5.Lower32(this.txtBankPwd.Text)));
					new LogAdminOperDAL().SaveLog(this.AdminId, this.txtId.Text, "资金密码", "修改资金密码");
				}
				this.doh.AddFieldItem("Score", this.txtScore.Text);
				this.doh.AddFieldItem("Point", Convert.ToDecimal(selectedItem2.Value));
				this.doh.AddFieldItem("UserGroup", selectedItem.Value);
				if (!this.tipAnswer.Text.Trim().Equals(this.txtAnswer.Text.Trim()))
				{
					this.doh.AddFieldItem("Answer", this.txtAnswer.Text);
					new LogAdminOperDAL().SaveLog(this.AdminId, this.txtId.Text, "密保答案", "密保答案从 " + this.tipAnswer.Text + " 修改为 " + this.txtAnswer.Text);
				}
				if (!this.tipPayName.Text.Trim().Equals(this.txtPayName.Text.Trim()))
				{
					this.doh.AddFieldItem("TrueName", this.txtPayName.Text);
					new LogAdminOperDAL().SaveLog(this.AdminId, this.txtId.Text, "会员银行", "真实姓名从 " + this.tipPayName.Text + " 修改为 " + this.txtPayName.Text);
				}
				this.doh.AddFieldItem("IsEnable", this.ddlIsEnable.SelectedValue);
				this.doh.AddFieldItem("IsBet", this.ddlIsBet.SelectedValue);
				this.doh.AddFieldItem("IsGetcash", this.ddlIsGetcash.SelectedValue);
				this.doh.AddFieldItem("IsTranAcc", this.ddlIsTranAcc.SelectedValue);
				this.doh.AddFieldItem("EnableSeason", this.txtEnableSeason.Text);
				int num = this.doh.Update("N_User");
				if (num > 0)
				{
					decimal num2 = Convert.ToDecimal(this.txtGroup.Text.Trim()) - Convert.ToDecimal(selectedItem.Value);
					decimal num3 = Convert.ToDecimal(this.txtPoint.Text.Trim()) - Convert.ToDecimal(selectedItem2.Value);
					if (num3 != 0m)
					{
						new LogAdminOperDAL().SaveLog(this.AdminId, this.txtId.Text, "会员返点", "修改会员返点");
					}
					if (num2 != 0m)
					{
						new LogAdminOperDAL().SaveLog(this.AdminId, this.txtId.Text, "会员类型", "修改会员类型");
					}
					if (num3 > 0m)
					{
						this.doh.Reset();
						this.doh.SqlCmd = "SELECT Id,Point FROM [N_User] where usercode like '%" + Strings.PadLeft(this.txtId.Text.Trim()) + "%' and Id<>" + this.txtId.Text.Trim();
						DataTable dataTable = this.doh.GetDataTable();
						for (int i = 0; i < dataTable.Rows.Count; i++)
						{
							if (Convert.ToDecimal(dataTable.Rows[i]["Point"]) > Convert.ToDecimal(num3))
							{
								this.doh.Reset();
								this.doh.ConditionExpress = "Id=" + dataTable.Rows[i]["Id"];
								this.doh.AddFieldItem("Point", Convert.ToDecimal(dataTable.Rows[i]["Point"]) - num3);
								this.doh.Update("N_User");
							}
							else
							{
								this.doh.Reset();
								this.doh.ConditionExpress = "Id=" + dataTable.Rows[i]["Id"];
								this.doh.AddFieldItem("Point", 0);
								this.doh.Update("N_User");
							}
						}
					}
					if (num2 > 0m)
					{
						this.doh.Reset();
						this.doh.SqlCmd = "SELECT Id,UserGroup FROM [N_User] where usercode like '%" + Strings.PadLeft(this.txtId.Text.Trim()) + "%' and Id<>" + this.txtId.Text.Trim();
						DataTable dataTable = this.doh.GetDataTable();
						for (int i = 0; i < dataTable.Rows.Count; i++)
						{
							if (Convert.ToDecimal(dataTable.Rows[i]["UserGroup"]) - Convert.ToDecimal(num2) >= 0m)
							{
								this.doh.Reset();
								this.doh.ConditionExpress = "Id=" + dataTable.Rows[i]["Id"];
								this.doh.AddFieldItem("UserGroup", Convert.ToDecimal(dataTable.Rows[i]["UserGroup"]) - num2);
								this.doh.Update("N_User");
							}
							else
							{
								this.doh.Reset();
								this.doh.ConditionExpress = "Id=" + dataTable.Rows[i]["Id"];
								this.doh.AddFieldItem("UserGroup", 0);
								this.doh.Update("N_User");
							}
						}
					}
				}
				base.FinalMessage("成功保存", "close.htm", 0);
			}
		}

		private bool CheckUserGroup(string userId, string group)
		{
			bool result;
			if (Convert.ToInt32(group) > 0)
			{
				this.doh.Reset();
				this.doh.SqlCmd = "SELECT top 1 UserGroup FROM N_User where Id=(select ParentId from N_User where Id=" + userId + ")";
				DataTable dataTable = this.doh.GetDataTable();
				if (dataTable.Rows.Count > 0)
				{
					if (Convert.ToInt32(group) >= Convert.ToInt32(dataTable.Rows[0]["UserGroup"]))
					{
						result = false;
						return result;
					}
				}
			}
			result = true;
			return result;
		}

		private bool CheckUserPoint(string userId, string point)
		{
			this.doh.Reset();
			this.doh.SqlCmd = "SELECT top 1 Point FROM N_User where Id=(select ParentId from N_User where Id=" + userId + ")";
			DataTable dataTable = this.doh.GetDataTable();
			bool result;
			if (dataTable.Rows.Count > 0)
			{
				if (Convert.ToDecimal(point) > Convert.ToDecimal(dataTable.Rows[0]["Point"]))
				{
					result = false;
					return result;
				}
			}
			result = true;
			return result;
		}

		protected HtmlForm form1;

		protected TextBox txtId;

		protected DropDownList ddlGroup;

		protected TextBox txtGroup;

		protected TextBox txtName;

		protected TextBox txtLoginPwd;

		protected TextBox txtParent;

		protected TextBox txtBankPwd;

		protected TextBox txtCode;

		protected DropDownList ddlPoint;

		protected TextBox txtPoint;

		protected TextBox txtOnline;

		protected TextBox txtMoney;

		protected TextBox txtOntime;

		protected TextBox txtScore;

		protected TextBox txtIp;

		protected DropDownList ddlIsEnable;

		protected TextBox txtRegtime;

		protected DropDownList ddlIsBet;

		protected TextBox txtPayBank;

		protected DropDownList ddlIsGetcash;

		protected TextBox txtPayBankAddress;

		protected DropDownList ddlIsTranAcc;

		protected TextBox txtPayAccount;

		protected TextBox txtQuestion;

		protected TextBox txtPayName;

		protected TextBox tipPayName;

		protected TextBox txtAnswer;

		protected TextBox tipAnswer;

		protected TextBox txtEmail;

		protected TextBox txtMobile;

		protected TextBox txtEnableSeason;

		protected Label lblmsg;

		protected TextBox lblCookie;

		protected Button btnSave;

		private string trueName = "";
	}
}
