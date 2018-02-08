using System;
using System.Data;
using System.Text;
using System.Web;
using System.Web.UI.HtmlControls;
using Lottery.DAL;

namespace Lottery.Web.temp
{
	public class WebPoint : UserCenterSession
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			this.doh.Reset();
			this.doh.SqlCmd = "select * from N_UserLevel";
			DataTable dataTable = this.doh.GetDataTable();
			for (int i = 0; i < dataTable.Rows.Count; i++)
			{
				StringBuilder stringBuilder = new StringBuilder();
				string text = dataTable.Rows[i]["Point"].ToString();
				this.doh.Reset();
				this.doh.SqlCmd = "select * from Sys_PlaySmallType where IsOpen=0";
				DataTable dataTable2 = this.doh.GetDataTable();
				for (int j = 0; j < dataTable2.Rows.Count; j++)
				{
					string text2 = string.Concat(new string[]
					{
						"{\"SmallTypeId\":\"",
						dataTable2.Rows[j]["Id"].ToString(),
						"\",\"SmallTypeName\":\"",
						dataTable2.Rows[j]["Title"].ToString(),
						"\",\"points\": ["
					});
					if (Convert.ToInt32(dataTable2.Rows[j]["LotteryId"].ToString()) == 1)
					{
						decimal d = Convert.ToDecimal(dataTable2.Rows[j]["MaxBonus"].ToString());
						decimal num = Convert.ToDecimal(dataTable2.Rows[j]["MinBonus"].ToString());
						decimal d2 = Convert.ToDecimal(dataTable2.Rows[j]["MinBonus2"].ToString());
						decimal num2 = (d - num) / 260m;
						if (d2 == 0m)
						{
							if (num2 == 0m)
							{
								this.doh.Reset();
								this.doh.SqlCmd = "select * from N_UserLevel where (Point=0.00) order by Point desc";
								DataTable dataTable3 = this.doh.GetDataTable();
								for (int k = 0; k < dataTable3.Rows.Count; k++)
								{
									decimal num3 = Convert.ToDecimal(dataTable3.Rows[k]["Point"].ToString());
									decimal value = num + Convert.ToDecimal(num3) * 2m * num2;
									object obj = text2;
									text2 = string.Concat(new object[]
									{
										obj,
										"{\"no\":",
										k + 1,
										",\"bonus\": \"",
										Convert.ToDouble(value).ToString("0.00"),
										"\",\"point\": \"0.00\"},"
									});
								}
							}
							else
							{
								this.doh.Reset();
								this.doh.SqlCmd = "select * from N_UserLevel where (Point=" + text + " or Point=0.00) order by Point desc";
								DataTable dataTable3 = this.doh.GetDataTable();
								for (int k = 0; k < dataTable3.Rows.Count; k++)
								{
									decimal num3 = Convert.ToDecimal(dataTable3.Rows[k]["Point"].ToString());
									decimal value = num + Convert.ToDecimal(num3) * 2m * num2;
									object obj = text2;
									text2 = string.Concat(new object[]
									{
										obj,
										"{\"no\":",
										k + 1,
										",\"bonus\": \"",
										Convert.ToDouble(value).ToString("0.00"),
										"\",\"point\": \"",
										Convert.ToDecimal((Convert.ToDecimal(text) - num3) / 10m).ToString("0.00"),
										"\"},"
									});
								}
							}
						}
						else
						{
							this.doh.Reset();
							this.doh.ConditionExpress = "Title2=@Title2 and LotteryId=1";
							this.doh.AddConditionParameter("@Title2", "P_3Z3_L");
							object[] fields = this.doh.GetFields("Sys_PlaySmallType", "MaxBonus,MinBonus");
							decimal d3 = (Convert.ToDecimal(fields[0]) - Convert.ToDecimal(fields[1])) / 260m;
							this.doh.Reset();
							this.doh.ConditionExpress = "Title2=@Title2 and LotteryId=1";
							this.doh.AddConditionParameter("@Title2", "P_3Z6_L");
							object[] fields2 = this.doh.GetFields("Sys_PlaySmallType", "MaxBonus,MinBonus");
							decimal d4 = (Convert.ToDecimal(fields2[0]) - Convert.ToDecimal(fields2[1])) / 260m;
							if (num2 == 0m)
							{
								this.doh.Reset();
								this.doh.SqlCmd = "select * from N_UserLevel where (Point=0.00) order by Point desc";
								DataTable dataTable3 = this.doh.GetDataTable();
								for (int k = 0; k < dataTable3.Rows.Count; k++)
								{
									decimal num3 = Convert.ToDecimal(dataTable3.Rows[k]["Point"].ToString());
									decimal value = num + Convert.ToDecimal(num3) * 2m * d3;
									decimal value2 = d2 + Convert.ToDecimal(num3) * 2m * d4;
									object obj = text2;
									text2 = string.Concat(new object[]
									{
										obj,
										"{\"no\":",
										k + 1,
										",\"bonus\": \"",
										Convert.ToDouble(value).ToString("0.00"),
										"/",
										Convert.ToDouble(value2).ToString("0.00"),
										"\",\"point\": \"0.00\"},"
									});
								}
							}
							else
							{
								this.doh.Reset();
								this.doh.SqlCmd = "select * from N_UserLevel where (Point=" + text + " or Point=0.00) order by Point desc";
								DataTable dataTable3 = this.doh.GetDataTable();
								for (int k = 0; k < dataTable3.Rows.Count; k++)
								{
									decimal num3 = Convert.ToDecimal(dataTable3.Rows[k]["Point"].ToString());
									decimal value = num + Convert.ToDecimal(num3) * 2m * d3;
									decimal value2 = d2 + Convert.ToDecimal(num3) * 2m * d4;
									object obj = text2;
									text2 = string.Concat(new object[]
									{
										obj,
										"{\"no\":",
										k + 1,
										",\"bonus\": \"",
										Convert.ToDouble(value).ToString("0.00"),
										"/",
										Convert.ToDouble(value2).ToString("0.00"),
										"\",\"point\": \"",
										Convert.ToDecimal((Convert.ToDecimal(text) - num3) / 10m).ToString("0.00"),
										"\"},"
									});
								}
							}
						}
						text2 = text2.Substring(0, text2.Length - 1);
						text2 += "]}";
						if (j == dataTable2.Rows.Count - 1)
						{
							stringBuilder.Append(text2);
						}
						else
						{
							stringBuilder.Append(text2 + ",");
						}
					}
					if (Convert.ToInt32(dataTable2.Rows[j]["LotteryId"].ToString()) == 2)
					{
						decimal d = Convert.ToDecimal(dataTable2.Rows[j]["MaxBonus"].ToString());
						decimal num = Convert.ToDecimal(dataTable2.Rows[j]["MinBonus"].ToString());
						decimal d2 = Convert.ToDecimal(dataTable2.Rows[j]["MinBonus2"].ToString());
						decimal num2 = (d - num) / 260m;
						if (d2 == 0m)
						{
							if (num2 == 0m)
							{
								this.doh.Reset();
								this.doh.SqlCmd = "select * from N_UserLevel where (Point=0.00) order by Point desc";
								DataTable dataTable3 = this.doh.GetDataTable();
								for (int k = 0; k < dataTable3.Rows.Count; k++)
								{
									decimal num3 = Convert.ToDecimal(dataTable3.Rows[k]["Point"].ToString());
									decimal value = num + Convert.ToDecimal(num3) * 2m * num2;
									object obj = text2;
									text2 = string.Concat(new object[]
									{
										obj,
										"{\"no\":",
										k + 1,
										",\"bonus\": \"",
										Convert.ToDouble(value).ToString("0.00"),
										"\",\"point\": \"0.00\"},"
									});
								}
							}
							else
							{
								decimal num4;
								if (Convert.ToDecimal(text) > 130m)
								{
									num4 = 130m;
								}
								else
								{
									num4 = Convert.ToDecimal(text);
								}
								this.doh.Reset();
								this.doh.SqlCmd = "select * from N_UserLevel where (Point=" + num4 + " or Point=0.00) order by Point desc";
								DataTable dataTable3 = this.doh.GetDataTable();
								for (int k = 0; k < dataTable3.Rows.Count; k++)
								{
									decimal num3 = Convert.ToDecimal(dataTable3.Rows[k]["Point"].ToString());
									decimal value = num + Convert.ToDecimal(num3) * 2m * num2;
									object obj = text2;
									text2 = string.Concat(new object[]
									{
										obj,
										"{\"no\":",
										k + 1,
										",\"bonus\": \"",
										Convert.ToDouble(value).ToString("0.00"),
										"\",\"point\": \"",
										Convert.ToDecimal((Convert.ToDecimal(num4) - num3) / 10m).ToString("0.00"),
										"\"},"
									});
								}
							}
						}
						text2 = text2.Substring(0, text2.Length - 1);
						text2 += "]}";
						if (j == dataTable2.Rows.Count - 1)
						{
							stringBuilder.Append(text2);
						}
						else
						{
							stringBuilder.Append(text2 + ",");
						}
					}
					if (Convert.ToInt32(dataTable2.Rows[j]["LotteryId"].ToString()) == 3)
					{
						decimal d = Convert.ToDecimal(dataTable2.Rows[j]["MaxBonus"].ToString());
						decimal num = Convert.ToDecimal(dataTable2.Rows[j]["MinBonus"].ToString());
						decimal d2 = Convert.ToDecimal(dataTable2.Rows[j]["MinBonus2"].ToString());
						decimal num2 = (d - num) / 220m;
						if (d2 == 0m)
						{
							if (num2 == 0m)
							{
								this.doh.Reset();
								this.doh.SqlCmd = "select * from N_UserLevel where (Point=0.00) order by Point desc";
								DataTable dataTable3 = this.doh.GetDataTable();
								for (int k = 0; k < dataTable3.Rows.Count; k++)
								{
									decimal num3 = Convert.ToDecimal(dataTable3.Rows[k]["Point"].ToString());
									decimal value = num + Convert.ToDecimal(num3) * 2m * num2;
									object obj = text2;
									text2 = string.Concat(new object[]
									{
										obj,
										"{\"no\":",
										k + 1,
										",\"bonus\": \"",
										Convert.ToDouble(value).ToString("0.00"),
										"\",\"point\": \"0.00\"},"
									});
								}
							}
							else
							{
								decimal num4;
								if (Convert.ToDecimal(text) > 110m)
								{
									num4 = 110m;
								}
								else
								{
									num4 = Convert.ToDecimal(text);
								}
								this.doh.Reset();
								this.doh.SqlCmd = "select * from N_UserLevel where (Point=" + num4 + " or Point=0.00) order by Point desc";
								DataTable dataTable3 = this.doh.GetDataTable();
								for (int k = 0; k < dataTable3.Rows.Count; k++)
								{
									decimal num3 = Convert.ToDecimal(dataTable3.Rows[k]["Point"].ToString());
									decimal value = num + Convert.ToDecimal(num3) * 2m * num2;
									object obj = text2;
									text2 = string.Concat(new object[]
									{
										obj,
										"{\"no\":",
										k + 1,
										",\"bonus\": \"",
										Convert.ToDouble(value).ToString("0.00"),
										"\",\"point\": \"",
										Convert.ToDecimal((Convert.ToDecimal(num4) - num3) / 10m).ToString("0.00"),
										"\"},"
									});
								}
							}
						}
						else
						{
							this.doh.Reset();
							this.doh.ConditionExpress = "Title2=@Title2 and LotteryId=1";
							this.doh.AddConditionParameter("@Title2", "P_3Z3_L");
							object[] fields = this.doh.GetFields("Sys_PlaySmallType", "MaxBonus,MinBonus");
							decimal d3 = (Convert.ToDecimal(fields[0]) - Convert.ToDecimal(fields[1])) / 220m;
							this.doh.Reset();
							this.doh.ConditionExpress = "Title2=@Title2 and LotteryId=1";
							this.doh.AddConditionParameter("@Title2", "P_3Z6_L");
							object[] fields2 = this.doh.GetFields("Sys_PlaySmallType", "MaxBonus,MinBonus");
							decimal d4 = (Convert.ToDecimal(fields2[0]) - Convert.ToDecimal(fields2[1])) / 220m;
							if (num2 == 0m)
							{
								this.doh.Reset();
								this.doh.SqlCmd = "select * from N_UserLevel where (Point=0.00) order by Point desc";
								DataTable dataTable3 = this.doh.GetDataTable();
								for (int k = 0; k < dataTable3.Rows.Count; k++)
								{
									decimal num3 = Convert.ToDecimal(dataTable3.Rows[k]["Point"].ToString());
									decimal value = num + Convert.ToDecimal(num3) * 2m * d3;
									decimal value2 = d2 + Convert.ToDecimal(num3) * 2m * d4;
									object obj = text2;
									text2 = string.Concat(new object[]
									{
										obj,
										"{\"no\":",
										k + 1,
										",\"bonus\": \"",
										Convert.ToDouble(value).ToString("0.00"),
										"/",
										Convert.ToDouble(value2).ToString("0.00"),
										"\",\"point\": \"0.00\"},"
									});
								}
							}
							else
							{
								decimal num4;
								if (Convert.ToDecimal(text) > 110m)
								{
									num4 = 110m;
								}
								else
								{
									num4 = Convert.ToDecimal(text);
								}
								this.doh.Reset();
								this.doh.SqlCmd = "select * from N_UserLevel where (Point=" + num4 + " or Point=0.00) order by Point desc";
								DataTable dataTable3 = this.doh.GetDataTable();
								for (int k = 0; k < dataTable3.Rows.Count; k++)
								{
									decimal num3 = Convert.ToDecimal(dataTable3.Rows[k]["Point"].ToString());
									decimal value = num + Convert.ToDecimal(num3) * 2m * d3;
									decimal value2 = d2 + Convert.ToDecimal(num3) * 2m * d4;
									object obj = text2;
									text2 = string.Concat(new object[]
									{
										obj,
										"{\"no\":",
										k + 1,
										",\"bonus\": \"",
										Convert.ToDouble(value).ToString("0.00"),
										"/",
										Convert.ToDouble(value2).ToString("0.00"),
										"\",\"point\": \"",
										Convert.ToDecimal((Convert.ToDecimal(num4) - num3) / 10m).ToString("0.00"),
										"\"},"
									});
								}
							}
						}
						text2 = text2.Substring(0, text2.Length - 1);
						text2 += "]}";
						if (j == dataTable2.Rows.Count - 1)
						{
							stringBuilder.Append(text2);
						}
						else
						{
							stringBuilder.Append(text2 + ",");
						}
					}
				}
				string txtStr = "var PointJsonData={\"result\" :\"1\",\"returnval\" :\"加载完成\",\"recordcount\":1,\"table\": [" + stringBuilder.ToString() + "]}";
				base.SaveJsFile(txtStr, HttpContext.Current.Server.MapPath("~/statics/json/json_" + text + ".js"));
			}
			base.Response.Write("更新完成：" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
		}

		protected HtmlForm form1;
	}
}
