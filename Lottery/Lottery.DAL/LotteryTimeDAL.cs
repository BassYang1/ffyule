using System;
using System.Data;
using Lottery.DBUtility;

namespace Lottery.DAL
{
	public class LotteryTimeDAL : ComData
	{
		public DataTable GetTable()
		{
			DataTable result = null;
			using (DbOperHandler dbOperHandler = new ComData().Doh())
			{
				dbOperHandler.Reset();
				dbOperHandler.SqlCmd = "select [Id],[LotteryId],[Sn],[Time],[Sort] from Sys_LotteryTime order by Id desc";
				result = dbOperHandler.GetDataTable();
			}
			return result;
		}

		public int GetTsIssueNum(string lotteryId)
		{
			DateTime now = DateTime.Now;
			int result = 0;
			using (DbOperHandler dbOperHandler = new ComData().Doh())
			{
				if (lotteryId != null)
				{
					if (!(lotteryId == "1010") && !(lotteryId == "3004"))
					{
						if (!(lotteryId == "1017"))
						{
							if (!(lotteryId == "1012"))
							{
								if (!(lotteryId == "1013"))
								{
									if (lotteryId == "4001")
									{
										dbOperHandler.Reset();
										dbOperHandler.SqlCmd = "select datediff(d,'2017-02-10',Convert(varchar(10),getdate(),120)) as d";
										DataTable dataTable = dbOperHandler.GetDataTable();
										if (DateTime.Now > Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd") + " 00:00:00") && DateTime.Now < Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd") + " 09:07:01"))
										{
											result = 601048 + (Convert.ToInt32(dataTable.Rows[0]["d"]) - 1) * 179;
										}
										else
										{
											result = 601048 + Convert.ToInt32(dataTable.Rows[0]["d"]) * 179;
										}
									}
								}
								else
								{
									dbOperHandler.Reset();
									dbOperHandler.SqlCmd = "select datediff(d,'2017-01-01',Convert(varchar(10),getdate(),120)) as d";
									DataTable dataTable = dbOperHandler.GetDataTable();
									if (now > Convert.ToDateTime(now.ToString("yyyy-MM-dd") + " 00:00:00") && now < Convert.ToDateTime(now.ToString("yyyy-MM-dd") + " 06:50:00"))
									{
										result = 106000000 + (Convert.ToInt32(dataTable.Rows[0]["d"]) - 1) * 203;
									}
									else
									{
										result = 106000000 + Convert.ToInt32(dataTable.Rows[0]["d"]) * 203;
									}
								}
							}
							else
							{
								dbOperHandler.Reset();
								dbOperHandler.SqlCmd = "select datediff(d,'2016-10-23',Convert(varchar(10),getdate(),120)) as d";
								DataTable dataTable = dbOperHandler.GetDataTable();
								if (now > Convert.ToDateTime(now.ToString("yyyy-MM-dd") + " 00:00:00") && now < Convert.ToDateTime(now.ToString("yyyy-MM-dd") + " 00:01:45"))
								{
									result = 2588087 + (Convert.ToInt32(dataTable.Rows[0]["d"]) - 1) * 660;
								}
								else
								{
									result = 2588087 + Convert.ToInt32(dataTable.Rows[0]["d"]) * 660;
								}
							}
						}
						else
						{
							dbOperHandler.Reset();
							dbOperHandler.SqlCmd = "select datediff(d,'2016-10-08',Convert(varchar(10),getdate(),120)) as d";
							DataTable dataTable = dbOperHandler.GetDataTable();
							result = 1658117 + Convert.ToInt32(dataTable.Rows[0]["d"]) * 880;
						}
					}
					else
					{
						dbOperHandler.Reset();
						dbOperHandler.SqlCmd = "select datediff(d,'2016-10-08',Convert(varchar(10),getdate(),120)) as d";
						DataTable dataTable = dbOperHandler.GetDataTable();
						result = 1658122 + Convert.ToInt32(dataTable.Rows[0]["d"]) * 880;
					}
				}
			}
			return result;
		}

		public int GetTsIssueNumToPet(int lotteryId, int curIssueNum)
		{
			int result = 0;
			using (DbOperHandler dbOperHandler = new ComData().Doh())
			{
				DataTable dataTable;
				if (lotteryId <= 1017)
				{
					switch (lotteryId)
					{
					case 1010:
						break;
					case 1011:
						goto IL_2DE;
					case 1012:
						dbOperHandler.Reset();
						dbOperHandler.SqlCmd = "select datediff(d,'2016-10-23',Convert(varchar(10),getdate(),120)) as d";
						dataTable = dbOperHandler.GetDataTable();
						result = curIssueNum - 2588087 - Convert.ToInt32(dataTable.Rows[0]["d"]) * 660;
						goto IL_2DE;
					case 1013:
						dbOperHandler.Reset();
						dbOperHandler.SqlCmd = "select datediff(d,'2017-01-01',Convert(varchar(10),getdate(),120)) as d";
						dataTable = dbOperHandler.GetDataTable();
						result = curIssueNum - 106000000 - Convert.ToInt32(dataTable.Rows[0]["d"]) * 203;
						goto IL_2DE;
					default:
						if (lotteryId != 1017)
						{
							goto IL_2DE;
						}
						dbOperHandler.Reset();
						dbOperHandler.SqlCmd = "select datediff(d,'2016-10-08',Convert(varchar(10),getdate(),120)) as d";
						dataTable = dbOperHandler.GetDataTable();
						result = curIssueNum - 1658117 - Convert.ToInt32(dataTable.Rows[0]["d"]) * 880;
						goto IL_2DE;
					}
				}
				else if (lotteryId != 3004)
				{
					if (lotteryId != 4001)
					{
						goto IL_2DE;
					}
					dbOperHandler.Reset();
					dbOperHandler.SqlCmd = "select datediff(d,'2017-01-01',Convert(varchar(10),getdate(),120)) as d";
					dataTable = dbOperHandler.GetDataTable();
					result = curIssueNum - 106000000 - Convert.ToInt32(dataTable.Rows[0]["d"]) * 203;
					dbOperHandler.Reset();
					dbOperHandler.SqlCmd = "select datediff(d,'2017-02-10',Convert(varchar(10),getdate(),120)) as d";
					dataTable = dbOperHandler.GetDataTable();
					if ((DateTime.Now > Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd") + " 00:00:00") && DateTime.Now < Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd") + " 09:07:01")) || (DateTime.Now > Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd") + " 23:57:01") && DateTime.Now < Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd") + " 23:59:59")))
					{
						result = curIssueNum - 601048 - (Convert.ToInt32(dataTable.Rows[0]["d"]) - 1) * 179;
						goto IL_2DE;
					}
					result = curIssueNum - 601048 - Convert.ToInt32(dataTable.Rows[0]["d"]) * 179;
					goto IL_2DE;
				}
				dbOperHandler.Reset();
				dbOperHandler.SqlCmd = "select datediff(d,'2016-10-08',Convert(varchar(10),getdate(),120)) as d";
				dataTable = dbOperHandler.GetDataTable();
				result = curIssueNum - 1658122 - Convert.ToInt32(dataTable.Rows[0]["d"]) * 880;
				IL_2DE:;
			}
			return result;
		}
	}
}
