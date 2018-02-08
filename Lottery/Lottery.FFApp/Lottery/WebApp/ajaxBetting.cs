using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Web;
using System.Web.Script.Serialization;
using Lottery.DAL;
using Lottery.DAL.Flex;
using Lottery.Entity;
using Lottery.Utils;

namespace Lottery.WebApp
{
	public class ajaxBetting : UserCenterSession
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
			case "ajaxBetting":
				this.ajaxBetting2();
				goto IL_116;
			case "ajaxBettingCancel":
				this.ajaxBettingCancel();
				goto IL_116;
			case "ajaxZHBetting":
				this.ajaxZHBetting();
				goto IL_116;
			case "ajaxLottery":
				this.ajaxLottery();
				goto IL_116;
			case "ajaxBigType":
				this.ajaxBigType();
				goto IL_116;
			case "ajaxLotteryTime23":
				this.ajaxLotteryTime23();
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

		private void ajaxLottery()
		{
			this.doh.Reset();
			this.doh.SqlCmd = "SELECT * FROM [Sys_Lottery] where IsOpen=0 order by sort asc";
			DataTable dataTable = this.doh.GetDataTable();
			this._response = "{\"result\" :\"1\",\"returnval\" :\"加载完成\"," + dtHelp.DT2JSON(dataTable) + "}";
			dataTable.Clear();
			dataTable.Dispose();
		}

		private void ajaxBigType()
		{
			string str = base.q("lid");
			this.doh.Reset();
			this.doh.SqlCmd = "SELECT Id,TypeId,Title FROM Sys_PlayBigType where TypeId=(SELECT [Ltype] FROM [Sys_Lottery] where Id =" + str + ") and IsOpen=1 ORDER BY Sort asc";
			DataTable dataTable = this.doh.GetDataTable();
			this.doh.Reset();
			this.doh.SqlCmd = "SELECT * FROM Sys_PlaySmallType where IsOpen=1 ORDER BY Sort asc";
			DataTable dataTable2 = this.doh.GetDataTable();
			this._response = "{\"result\" :\"1\",\"returnval\" :\"加载完成\"," + dtHelp.DT2JSON(dataTable, dataTable2) + "}";
			dataTable.Clear();
			dataTable.Dispose();
		}

		private void ajaxBettingCancel()
		{
			string betId = base.f("Id");
			string response = new Lottery.DAL.Flex.UserBetDAL().BetCancel(betId);
			this._response = response;
		}

		private void ajaxZHBetting()
		{
			if (this.AdminId == "")
			{
				this._response = base.JsonResult(0, "投注失败,请重新登录后再进行投注!");
				return;
			}
			HttpContext.Current.Response.ContentType = "application/json";
			string str = new StreamReader(HttpContext.Current.Request.InputStream).ReadToEnd();
			List<ajaxBetting.RequestDataJSONZH> list = ajaxBetting.JSONToObject<List<ajaxBetting.RequestDataJSONZH>>("[" + str + "]");
			ajaxBetting.RequestDataJSONZH requestDataJSONZH = new ajaxBetting.RequestDataJSONZH();
			int userId = Convert.ToInt32(this.AdminId);
			int lotteryId = Convert.ToInt32(list[0].lotteryId);
			int isStop = Convert.ToInt32(list[0].IsStop);
			int totalNums = Convert.ToInt32(list[0].ZHNums);
			decimal num = Convert.ToDecimal(list[0].ZHSums);
			try
			{
				string[] expr_D3 = new Lottery.DAL.Flex.UserBetDAL().GetIssueTimeAndSN(lotteryId);
				string text = expr_D3[0];
				DateTime sTime = Convert.ToDateTime(expr_D3[1]);
				DateTime serverTime = PublicDAL.GetServerTime();
				string text2 = new Lottery.DAL.Flex.UserBetDAL().CheckBet(userId, lotteryId, Convert.ToDecimal(num), sTime);
				if (!string.IsNullOrEmpty(text2))
				{
					this._response = base.JsonResult(0, text2);
				}
				else
				{
					num = 0m;
					UserZhBetModel userZhBetModel = new UserZhBetModel();
					userZhBetModel.UserId = userId;
					userZhBetModel.LotteryId = lotteryId;
					userZhBetModel.PlayId = 0;
					userZhBetModel.StartIssueNum = text;
					userZhBetModel.TotalNums = totalNums;
					userZhBetModel.IsStop = isStop;
					userZhBetModel.STime = DateTime.Now;
					new List<UserBetModel>();
					List<UserZhDetailModel> list2 = new List<UserZhDetailModel>();
					for (int i = 0; i < list.Count; i++)
					{
						requestDataJSONZH = list[i];
						this.doh.Reset();
						this.doh.ConditionExpress = "Id=@Id";
						this.doh.AddConditionParameter("@Id", requestDataJSONZH.playId.ToString());
						string playCode = string.Concat(this.doh.GetField("Sys_PlaySmallType", "Title2"));
						if (Convert.ToDecimal(requestDataJSONZH.price) < decimal.Zero || Convert.ToDecimal(requestDataJSONZH.Num) < decimal.One || Convert.ToDecimal(requestDataJSONZH.times) < decimal.One)
						{
							this._response = base.JsonResult(0, "投注错误！请重新投注！");
							return;
						}
						decimal num2 = 0m;
						string text3 = Calculate.BetNumerice(userId, lotteryId, requestDataJSONZH.balls, requestDataJSONZH.playId.ToString(), requestDataJSONZH.strPos, Convert.ToInt32(requestDataJSONZH.Num), Convert.ToDecimal(requestDataJSONZH.Point), ref num2);
						if (!string.IsNullOrEmpty(text3))
						{
							this._response = text3.Replace("[", "").Replace("]", "");
							return;
						}
						if (num2 <= decimal.Zero)
						{
							this._response = base.JsonResult(0, "投注失败,返点错误，请重新投注！");
							return;
						}
						UserBetModel userBetModel = new UserBetModel();
						userBetModel.UserId = userId;
						userBetModel.UserMoney = decimal.Zero;
						userBetModel.LotteryId = lotteryId;
						userBetModel.PlayId = Convert.ToInt32(requestDataJSONZH.playId);
						userBetModel.PlayCode = playCode;
						userBetModel.SingleMoney = Convert.ToDecimal(requestDataJSONZH.price);
						userBetModel.Num = Convert.ToInt32(requestDataJSONZH.Num);
						userBetModel.Detail = requestDataJSONZH.balls;
						userBetModel.Point = Convert.ToDecimal(requestDataJSONZH.Point);
						userBetModel.Bonus = num2;
						userBetModel.Pos = requestDataJSONZH.strPos;
						userBetModel.STime2 = serverTime;
						userBetModel.IsDelay = 0;
						userBetModel.ZHID = 0;
						for (int j = 0; j < requestDataJSONZH.table2.Count; j++)
						{
							ajaxBetting.RequestDataJSONZH2 requestDataJSONZH2 = requestDataJSONZH.table2[j];
							if (Convert.ToInt32(requestDataJSONZH2.ZHTimes) > 0 && Convert.ToDecimal(requestDataJSONZH2.ZHIssueNum.Replace("-", "")) >= Convert.ToDecimal(text.Replace("-", "")))
							{
								UserZhDetailModel userZhDetailModel = new UserZhDetailModel();
								userZhDetailModel.IssueNum = requestDataJSONZH2.ZHIssueNum;
								userZhDetailModel.Times = Convert.ToInt32(requestDataJSONZH2.ZHTimes);
								userZhDetailModel.STime = Convert.ToDateTime(requestDataJSONZH2.ZHSTime);
								userZhDetailModel.Lists.Add(userBetModel);
								num += userBetModel.SingleMoney * userBetModel.Num * userZhDetailModel.Times;
								list2.Add(userZhDetailModel);
							}
						}
					}
					userZhBetModel.TotalSums = num;
					if (list2.Count > 0)
					{
						if (new Lottery.DAL.Flex.UserBetDAL().InsertZhBet(userZhBetModel, list2, num, "Web端追号") > 0)
						{
							this._response = base.JsonResult(1, "追号成功！请等待开奖！");
						}
						else
						{
							this._response = base.JsonResult(0, "对不起,投注失败！");
						}
					}
					else
					{
						this._response = base.JsonResult(0, "对不起,投注失败！0");
					}
				}
			}
			catch (Exception)
			{
				this._response = base.JsonResult(0, "对不起,投注失败！");
			}
		}

		private void ajaxBetting2()
		{
			if (this.AdminId == "")
			{
				this._response = base.JsonResult(0, "投注失败,请重新登录后再进行投注!");
				return;
			}
			if (this.site.BetIsOpen == 1)
			{
				this._response = base.JsonResult(0, "系统正在维护，不能投注！");
				return;
			}
			HttpContext.Current.Response.ContentType = "application/json";
			List<ajaxBetting.RequestDataJSON> list = ajaxBetting.JSONToObject<List<ajaxBetting.RequestDataJSON>>(HttpUtility.UrlDecode(new StreamReader(HttpContext.Current.Request.InputStream).ReadToEnd()));
			ajaxBetting.RequestDataJSON requestDataJSON = new ajaxBetting.RequestDataJSON();
			int lotteryId = list[0].lotteryId;
			int userId = Convert.ToInt32(this.AdminId);
			decimal num = 0m;
			try
			{
				string[] expr_B0 = new Lottery.DAL.Flex.UserBetDAL().GetIssueTimeAndSN(lotteryId);
				string text = expr_B0[0];
				DateTime sTime = Convert.ToDateTime(expr_B0[1]);
				DateTime serverTime = PublicDAL.GetServerTime();
				for (int i = 0; i < list.Count; i++)
				{
					requestDataJSON = list[i];
					num += requestDataJSON.price * requestDataJSON.Num * requestDataJSON.times;
				}
				string text2 = new Lottery.DAL.Flex.UserBetDAL().CheckBet(userId, lotteryId, Convert.ToDecimal(num), sTime);
				if (!string.IsNullOrEmpty(text2))
				{
					this._response = base.JsonResult(0, text2);
				}
				else
				{
					int num2 = 0;
					for (int j = 0; j < list.Count; j++)
					{
						requestDataJSON = list[j];
						this.doh.Reset();
						this.doh.ConditionExpress = "Id=@Id";
						this.doh.AddConditionParameter("@Id", requestDataJSON.playId.ToString());
						string playCode = string.Concat(this.doh.GetField("Sys_PlaySmallType", "Title2"));
						decimal num3 = 0m;
						if (lotteryId != 5001)
						{
							if (Convert.ToDecimal(requestDataJSON.price) < decimal.Zero || Convert.ToDecimal(requestDataJSON.Num) < decimal.One || Convert.ToDecimal(requestDataJSON.times) < decimal.One)
							{
								this._response = base.JsonResult(0, "投注错误！请重新投注！");
								return;
							}
							string text3 = Calculate.BetNumerice(userId, lotteryId, requestDataJSON.balls, requestDataJSON.playId.ToString(), requestDataJSON.strPos, Convert.ToInt32(requestDataJSON.Num), Convert.ToDecimal(requestDataJSON.Point), ref num3);
							if (!string.IsNullOrEmpty(text3))
							{
								this._response = text3.Replace("[", "").Replace("]", "");
								return;
							}
							if (num3 <= decimal.Zero)
							{
								this._response = base.JsonResult(0, "投注失败,返点错误，请重新投注！");
								return;
							}
							if (Convert.ToDecimal(requestDataJSON.price) * Convert.ToInt32(requestDataJSON.Num) * Convert.ToInt32(requestDataJSON.times) >= 1000000m)
							{
								this._response = base.JsonResult(0, "投注失败,单个玩法投注额不能超过100万！");
								return;
							}
						}
						UserBetModel userBetModel = new UserBetModel();
						userBetModel.UserId = userId;
						userBetModel.UserMoney = decimal.Zero;
						userBetModel.LotteryId = lotteryId;
						userBetModel.PlayId = Convert.ToInt32(requestDataJSON.playId.ToString());
						userBetModel.PlayCode = playCode;
						userBetModel.IssueNum = text;
						userBetModel.SingleMoney = Convert.ToDecimal(requestDataJSON.price);
						userBetModel.Num = Convert.ToInt32(requestDataJSON.Num);
						userBetModel.Detail = requestDataJSON.balls;
						userBetModel.Point = Convert.ToDecimal(requestDataJSON.Point);
						userBetModel.Bonus = num3;
						userBetModel.Pos = requestDataJSON.strPos;
						userBetModel.STime = sTime;
						userBetModel.STime2 = serverTime;
						userBetModel.IsDelay = 0;
						userBetModel.Times = Convert.ToDecimal(requestDataJSON.times);
						userBetModel.ZHID = 0;
						if (userBetModel.Pos.Equals(""))
						{
							if (userBetModel.PlayCode.Equals("P_5ZH") || userBetModel.PlayCode.Equals("P_4ZH_L") || userBetModel.PlayCode.Equals("P_4ZH_R") || userBetModel.PlayCode.Equals("P_3ZH_L") || userBetModel.PlayCode.Equals("P_3ZH_C") || userBetModel.PlayCode.Equals("P_3ZH_R"))
							{
								num2 = new Lottery.DAL.Flex.UserBetDAL().InsertBetZH(userBetModel, "Web端");
							}
							else
							{
								num2 = new Lottery.DAL.Flex.UserBetDAL().InsertBet(userBetModel, "Web端");
							}
						}
						else
						{
							num2 = new Lottery.DAL.Flex.UserBetDAL().InsertBetPos(userBetModel, "Web端");
						}
					}
					if (num2 > 0)
					{
						this._response = base.JsonResult(1, "第" + text + "期投注成功，请期待开奖！");
					}
					else
					{
						this._response = base.JsonResult(0, "对不起,投注失败！");
					}
				}
			}
			catch (Exception arg)
			{
				this._response = base.JsonResult(0, "对不起,投注失败！" + arg);
			}
		}

		private void ajaxLotteryTime23()
		{
			string text = base.q("lid");
			string text2 = "{\"name\": \"名称\",\"lotteryid\": \"彩种类别\",\"ordertime\": \"倒计时\",\"closetime\": \"封单时间\",\"nestsn\": \"下期期号\",\"cursn\": \"当前期号\",\"curnumber\": \"开奖号码\"}";
			text2 = text2.Replace("名称", LotteryUtils.LotteryTitle(Convert.ToInt32(text))).Replace("彩种类别", text);
			DateTime now = DateTime.Now;
			DateTime dateTime = base.GetDateTime();
			string text3 = dateTime.ToString("yyyyMMdd");
			string text4 = dateTime.ToString("HH:mm:ss");
			string text5 = dateTime.ToString("yyyy-MM-dd");
			this.doh.Reset();
			this.doh.SqlCmd = "select dbo.f_GetCloseTime(" + text + ") as closetime";
			DataTable dataTable = this.doh.GetDataTable();
			text2 = text2.Replace("封单时间", dataTable.Rows[0]["closetime"].ToString());
			TimeSpan timeSpan = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd") + " 23:59:59") - DateTime.Now;
			string newValue = string.Concat(timeSpan.Days * 24 * 60 * 60 + timeSpan.Hours * 60 * 60 + timeSpan.Minutes * 60 + timeSpan.Seconds);
			text2 = text2.Replace("倒计时", newValue);
			this.doh.Reset();
			this.doh.SqlCmd = "SELECT TOP 1 [Title],[Number] FROM [Sys_LotteryData] with(nolock) where UserId=" + this.AdminId + " order by Id desc";
			DataTable dataTable2 = this.doh.GetDataTable();
			if (dataTable2.Rows.Count > 0)
			{
				string newValue2 = (dataTable2.Rows.Count > 0) ? dataTable2.Rows[0]["title"].ToString() : "您还未投注";
                decimal tdec = Convert.ToDecimal(dataTable2.Rows[0]["title"].ToString());

                string newValue3 = (dataTable2.Rows.Count > 0) ? string.Concat(++tdec) : (DateTime.Now.ToString("yyyyMMdd") + "00001");
				text2 = text2.Replace("下期期号", newValue3).Replace("当前期号", newValue2);
				string[] array = dataTable2.Rows[0]["Number"].ToString().Split(new char[]
				{
					','
				});
				string text6 = "<p class='hm'>";
				for (int i = 0; i < array.Length; i++)
				{
					text6 = text6 + "<span>" + array[i] + "</span>";
				}
				text6 += "</p>";
				text2 = text2.Replace("开奖号码", text6);
			}
			else
			{
				string newValue2 = "您还未投注";
				string newValue3 = DateTime.Now.ToString("yyyyMMdd") + "00001";
				text2 = text2.Replace("下期期号", newValue3).Replace("当前期号", newValue2);
				string text6 = "<p class='hm'>";
				text6 += "请您先投注";
				text6 += "</p>";
				text2 = text2.Replace("开奖号码", text6);
			}
			this._response = text2;
		}

		public static T JSONToObject<T>(string jsonText)
		{
			JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
			T result;
			try
			{
				result = javaScriptSerializer.Deserialize<T>(jsonText);
			}
			catch (Exception ex)
			{
				throw new Exception("JSONHelper.JSONToObject(): " + ex.Message);
			}
			return result;
		}

		private string _operType = string.Empty;

		private string _response = string.Empty;

		[Serializable]
		public class RequestDataJSON
		{
			public int lotteryId
			{
				get;
				set;
			}

			public int playId
			{
				get;
				set;
			}

			public decimal price
			{
				get;
				set;
			}

			public decimal times
			{
				get;
				set;
			}

			public int Num
			{
				get;
				set;
			}

			public string price_win
			{
				get;
				set;
			}

			public decimal singelBouns
			{
				get;
				set;
			}

			public decimal Point
			{
				get;
				set;
			}

			public string balls
			{
				get;
				set;
			}

			public string strPos
			{
				get;
				set;
			}
		}

		[Serializable]
		public class RequestDataJSONZH
		{
			public int lotteryId
			{
				get;
				set;
			}

			public int playId
			{
				get;
				set;
			}

			public string IssueNum
			{
				get;
				set;
			}

			public decimal price
			{
				get;
				set;
			}

			public decimal times
			{
				get;
				set;
			}

			public int Num
			{
				get;
				set;
			}

			public string price_win
			{
				get;
				set;
			}

			public decimal singelBouns
			{
				get;
				set;
			}

			public decimal Point
			{
				get;
				set;
			}

			public string balls
			{
				get;
				set;
			}

			public string strPos
			{
				get;
				set;
			}

			public decimal ZHNums
			{
				get;
				set;
			}

			public decimal ZHSums
			{
				get;
				set;
			}

			public int IsStop
			{
				get;
				set;
			}

			public List<ajaxBetting.RequestDataJSONZH2> table2
			{
				get;
				set;
			}
		}

		[Serializable]
		public class RequestDataJSONZH2
		{
			public string ZHIssueNum
			{
				get;
				set;
			}

			public int ZHTimes
			{
				get;
				set;
			}

			public string ZHSTime
			{
				get;
				set;
			}
		}
	}
}
