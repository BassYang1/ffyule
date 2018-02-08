using System;
using System.Data;
using System.Web;
using Lottery.DAL;
using Lottery.DBUtility;
using Lottery.Utils;

namespace Lottery.Admin
{
	public class ajaxSave : AdminCenter
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			base.Admin_Load("master", "json");
			this._operType = base.q("oper");
			string operType = this._operType;
			if (operType != null)
			{
				if (operType == "Info")
				{
					this.Info();
					goto IL_90;
				}
				if (operType == "Save")
				{
					this.Save();
					goto IL_90;
				}
				if (operType == "Update")
				{
					this.Update();
					goto IL_90;
				}
				if (operType == "OptionsInfo")
				{
					this.OptionsInfo();
					goto IL_90;
				}
			}
			this.DefaultResponse();
			IL_90:
			base.Response.Write(this._response);
		}

		private void DefaultResponse()
		{
			this._response = base.JsonResult(0, "未知操作");
		}

		private void Info()
		{
			string str = base.q("id");
			string str2 = base.q("t");
			this.doh.Reset();
			this.doh.SqlCmd = "select top 1 * from " + str2 + " where Id=" + str;
			DataTable dataTable = this.doh.GetDataTable();
			this._response = "{\"result\" :\"1\",\"returnval\" :\"操作成功\"," + dtHelp.DT2JSON(dataTable) + "}";
			dataTable.Clear();
			dataTable.Dispose();
		}

		private void Save()
		{
			string text = base.q("t");
			string[] allKeys = HttpContext.Current.Request.Form.AllKeys;
			this.doh.Reset();
			int num = 0;
			for (int i = 0; i < allKeys.Length; i++)
			{
				string text2 = base.f(allKeys[i]);
				if (!string.IsNullOrEmpty(text2))
				{
					if (allKeys[i].ToLower().Equals("password"))
					{
						this.doh.AddFieldItem("Password", MD5.Last64(MD5.Lower32(text2)));
					}
					else
					{
						this.doh.AddFieldItem(allKeys[i], text2);
					}
					num++;
				}
			}
			if (num > 0)
			{
				num = this.doh.Insert(text);
				new LogAdminOperDAL().SaveLog(this.AdminId, "0", "添加操作", string.Concat(new object[]
				{
					"添加了表",
					text,
					"的数据，Id为",
					num
				}));
			}
			if (num > 0)
			{
				this._response = base.JsonResult(1, "保存成功");
			}
			else
			{
				this._response = base.JsonResult(0, "保存失败");
			}
		}

		private void Update()
		{
			string text = base.q("id");
			string text2 = base.q("t");
			string[] allKeys = HttpContext.Current.Request.Form.AllKeys;
			this.doh.Reset();
			this.doh.ConditionExpress = "id=" + text;
			int num = 0;
			for (int i = 0; i < allKeys.Length; i++)
			{
				string text3 = base.f(allKeys[i]);
				if (!string.IsNullOrEmpty(text3))
				{
					if (allKeys[i].ToLower().Equals("password"))
					{
						this.doh.AddFieldItem("Password", MD5.Last64(MD5.Lower32(text3)));
					}
					else
					{
						this.doh.AddFieldItem(allKeys[i], text3);
					}
					num++;
				}
			}
			if (num > 0)
			{
				num = this.doh.Update(text2);
				new LogAdminOperDAL().SaveLog(this.AdminId, "0", "编辑操作", "修改了表" + text2 + "的数据，Id为" + text);
			}
			if (num > 0)
			{
				this._response = base.JsonResult(1, "修改成功");
			}
			else
			{
				this._response = base.JsonResult(0, "修改失败");
			}
		}

		private void OptionsInfo()
		{
			string str = base.q("t");
			string text = base.q("w");
			string text2 = base.q("n");
			if (string.IsNullOrEmpty(text2))
			{
				text2 = "name";
			}
			this.doh.Reset();
			this.doh.SqlCmd = "select id," + text2 + " as name from " + str;
			if (!string.IsNullOrEmpty(text))
			{
				DbOperHandler expr_75 = this.doh;
				expr_75.SqlCmd = expr_75.SqlCmd + " where " + text;
			}
			DataTable dataTable = this.doh.GetDataTable();
			this._response = "{\"result\" :\"1\",\"returnval\" :\"操作成功\"," + dtHelp.DT2JSON(dataTable) + "}";
			dataTable.Clear();
			dataTable.Dispose();
		}

		private string _operType = string.Empty;

		private string _response = string.Empty;
	}
}
