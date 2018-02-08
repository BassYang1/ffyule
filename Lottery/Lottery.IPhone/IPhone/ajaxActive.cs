using System;
using System.Data;
using Lottery.DAL;
using Lottery.DAL.Flex;
using Lottery.Utils;

namespace Lottery.IPhone
{
	public class ajaxActive : UserCenterSession
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
				goto IL_162;
			case "GetHBInfo":
				this.GetHBInfo();
				goto IL_162;
			case "PaiFaHB":
				this.PaiFaHB();
				goto IL_162;
			case "GetBetActiveInfo":
				this.GetBetActiveInfo();
				goto IL_162;
			case "PaiFaBetActive":
				this.PaiFaBetActive();
				goto IL_162;
			case "GetGroup2Gz":
				this.GetGroup2Gz();
				goto IL_162;
			case "SaveGroup2Gz":
				this.SaveGroup2Gz();
				goto IL_162;
			case "GetGroup3Gz":
				this.GetGroup3Gz();
				goto IL_162;
			case "SaveGroup3Gz":
				this.SaveGroup3Gz();
				goto IL_162;
			}
			this.DefaultResponse();
			IL_162:
			base.Response.Write(this._response);
		}

		private void DefaultResponse()
		{
			this._response = base.JsonResult(0, "未知操作");
		}

		private void ajaxGetList()
		{
			this.doh.Reset();
			this.doh.SqlCmd = "SELECT [UserGroup] FROM [N_User] where Id=" + this.AdminId;
			DataTable dataTable = this.doh.GetDataTable();
			this._response = "{\"result\" :\"1\",\"returnval\" :\"操作成功\"," + dtHelp.DT2JSON(dataTable) + "}";
			dataTable.Clear();
			dataTable.Dispose();
		}

		public void GetHBInfo()
		{
			string str = "";
			new Lottery.DAL.Flex.ActiveDAL().GetHBInfoJSON(this.AdminId, ref str);
			this._response = "{\"result\" :\"1\",\"returnval\" :\"操作成功\",\"table\":" + str + "}";
		}

		public void PaiFaHB()
		{
			this._response = "{\"result\":\"0\",\"message\":\"红包大派送活动已停止，请等待下次开放！\"}";
		}

		public void GetBetActiveInfo()
		{
			string str = "";
			new Lottery.DAL.Flex.ActiveDAL().GetBetActiveInfoJSON(this.AdminId, ref str);
			this._response = "{\"result\" :\"1\",\"returnval\" :\"操作成功\",\"table\":" + str + "}";
		}

		public void PaiFaBetActive()
		{
			this._response = new Lottery.DAL.Flex.ActiveDAL().SaveBetActive(this.AdminId, "ActBet", "消费大闯关", "消费大闯关");
			this._response = this._response.Replace("[", "").Replace("]", "");
		}

		private void GetGroup2Gz()
		{
			string str = "";
			new Lottery.DAL.Flex.ActiveDAL().GetGroup2GzJSON(this.AdminId, ref str);
			this._response = "{\"result\" :\"1\",\"returnval\" :\"操作成功\",\"table\":" + str + "}";
		}

		private void SaveGroup2Gz()
		{
			this._response = new Lottery.DAL.Flex.ActiveDAL().SaveGroup2Active(this.AdminId);
			this._response = this._response.Replace("[", "").Replace("]", "");
		}

		private void GetGroup3Gz()
		{
			string str = "";
			new Lottery.DAL.Flex.ActiveDAL().GetGroup3GzJSON(this.AdminId, ref str);
			this._response = "{\"result\" :\"1\",\"returnval\" :\"操作成功\",\"table\":" + str + "}";
		}

		private void SaveGroup3Gz()
		{
			this._response = new Lottery.DAL.Flex.ActiveDAL().SaveGroup3Active(this.AdminId);
			this._response = this._response.Replace("[", "").Replace("]", "");
		}

		private string _operType = string.Empty;

		private string _response = string.Empty;
	}
}
