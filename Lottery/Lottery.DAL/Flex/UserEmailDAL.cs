using System;
using System.Collections;
using System.Data;
using Lottery.DBUtility;
using Lottery.Utils;

namespace Lottery.DAL.Flex
{
	public class UserEmailDAL : ComData
	{
		public void GetListJSON(int _thispage, int _pagesize, string _wherestr1, ref string _jsonstr)
		{
			using (DbOperHandler dbOperHandler = new ComData().Doh())
			{
				dbOperHandler.Reset();
				dbOperHandler.ConditionExpress = _wherestr1;
				int num = dbOperHandler.Count("N_UserEmail");
				string sql = SqlHelp.GetSql0(num + " as totalcount,row_number() over (order by Id desc) as rowid,dbo.f_GetUserName(SendId) as SendName,dbo.f_GetUserName(ReceiveId) as ReceiveName,*", "N_UserEmail", "Id", _pagesize, _thispage, "desc", _wherestr1);
				dbOperHandler.Reset();
				dbOperHandler.SqlCmd = sql;
				DataTable dataTable = dbOperHandler.GetDataTable();
				_jsonstr = base.ConverTableToJSON2(dataTable);
				dataTable.Clear();
				dataTable.Dispose();
			}
		}

		public string Save(string code, string SendId, string ReceiveId, string Title, string Contents)
		{
			string text = "";
			ArrayList arrayList = new ArrayList();
			string result;
			using (DbOperHandler dbOperHandler = new ComData().Doh())
			{
				if (!string.IsNullOrEmpty(ReceiveId))
				{
					dbOperHandler.Reset();
					dbOperHandler.ConditionExpress = "UserName=@UserName";
					dbOperHandler.AddConditionParameter("@UserName", ReceiveId);
					ReceiveId = string.Concat(dbOperHandler.GetField("N_User", "Id"));
					arrayList.Add(ReceiveId);
				}
				else if (code != null)
				{
					if (!(code == "0"))
					{
						if (!(code == "1"))
						{
							if (code == "2")
							{
								dbOperHandler.Reset();
								dbOperHandler.SqlCmd = string.Format("select Id from N_User where UserCode like '%{0}%' and Id<>{0}", Strings.PadLeft(SendId));
								DataTable dataTable = dbOperHandler.GetDataTable();
								if (dataTable.Rows.Count < 1)
								{
									text = "您没有下级不能发送!";
								}
								else
								{
									for (int i = 0; i < dataTable.Rows.Count; i++)
									{
										arrayList.Add(dataTable.Rows[i]["Id"].ToString());
									}
								}
							}
						}
						else
						{
							dbOperHandler.Reset();
							dbOperHandler.SqlCmd = string.Format("select Id from N_User where ParentId={0}", SendId);
							DataTable dataTable2 = dbOperHandler.GetDataTable();
							if (dataTable2.Rows.Count < 1)
							{
								text = "您没有直属下级不能发送!";
							}
							else
							{
								for (int j = 0; j < dataTable2.Rows.Count; j++)
								{
									arrayList.Add(dataTable2.Rows[j]["Id"].ToString());
								}
							}
						}
					}
					else
					{
						dbOperHandler.Reset();
						dbOperHandler.ConditionExpress = "Id=@Id";
						dbOperHandler.AddConditionParameter("@Id", SendId);
						string text2 = string.Concat(dbOperHandler.GetField("N_User", "ParentId"));
						if (text2.Equals("0"))
						{
							text = "您没有上级不能发送!";
						}
						else
						{
							arrayList.Add(text2);
						}
					}
				}
				foreach (string fieldValue in arrayList)
				{
					dbOperHandler.Reset();
					dbOperHandler.AddFieldItem("SendId", SendId);
					dbOperHandler.AddFieldItem("ReceiveId", fieldValue);
					dbOperHandler.AddFieldItem("Title", Title);
					dbOperHandler.AddFieldItem("Contents", Contents);
					dbOperHandler.AddFieldItem("STime", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
					dbOperHandler.AddFieldItem("IsRead", "0");
					if (dbOperHandler.Insert("N_UserEmail") > 0)
					{
						text = "发送成功！";
					}
					else
					{
						text = "发送失败！";
					}
				}
				result = text;
			}
			return result;
		}

		public void UpdateState(string _id)
		{
			using (DbOperHandler dbOperHandler = new ComData().Doh())
			{
				dbOperHandler.Reset();
				dbOperHandler.ConditionExpress = "id=@id";
				dbOperHandler.AddConditionParameter("@id", _id);
				dbOperHandler.AddFieldItem("IsRead", "1");
				dbOperHandler.Update("N_UserEmail");
			}
		}

		public int DeletesSend(string _id)
		{
			int result;
			using (DbOperHandler dbOperHandler = new ComData().Doh())
			{
				dbOperHandler.Reset();
				dbOperHandler.ConditionExpress = "id=@id";
				dbOperHandler.AddConditionParameter("@id", _id);
				dbOperHandler.AddFieldItem("IsDelSend", "1");
				result = dbOperHandler.Update("N_UserEmail");
			}
			return result;
		}

		public int DeletesReceive(string _id)
		{
			int result;
			using (DbOperHandler dbOperHandler = new ComData().Doh())
			{
				dbOperHandler.Reset();
				dbOperHandler.ConditionExpress = "id=@id";
				dbOperHandler.AddConditionParameter("@id", _id);
				dbOperHandler.AddFieldItem("IsDelReceive", "1");
				result = dbOperHandler.Update("N_UserEmail");
			}
			return result;
		}

		public int DeletesByUserSend(string UserId)
		{
			int result;
			using (DbOperHandler dbOperHandler = new ComData().Doh())
			{
				dbOperHandler.Reset();
				dbOperHandler.ConditionExpress = "UserId=@UserId";
				dbOperHandler.AddConditionParameter("@SendId", UserId);
				dbOperHandler.AddFieldItem("IsDelSend", "1");
				result = dbOperHandler.Update("N_UserEmail");
			}
			return result;
		}

		public int DeletesByUserReceive(string UserId)
		{
			int result;
			using (DbOperHandler dbOperHandler = new ComData().Doh())
			{
				dbOperHandler.Reset();
				dbOperHandler.ConditionExpress = "ReceiveId=@UserId";
				dbOperHandler.AddConditionParameter("@UserId", UserId);
				dbOperHandler.AddFieldItem("IsRead", "1");
				dbOperHandler.AddFieldItem("IsDelReceive", "1");
				result = dbOperHandler.Update("N_UserEmail");
			}
			return result;
		}

		public int ReadedUserReceive(string UserId)
		{
			int result;
			using (DbOperHandler dbOperHandler = new ComData().Doh())
			{
				dbOperHandler.Reset();
				dbOperHandler.ConditionExpress = "ReceiveId=@UserId";
				dbOperHandler.AddConditionParameter("@UserId", UserId);
				dbOperHandler.AddFieldItem("IsRead", "1");
				result = dbOperHandler.Update("N_UserEmail");
			}
			return result;
		}

		public int UpdateIsRead(string Id)
		{
			int result;
			using (DbOperHandler dbOperHandler = new ComData().Doh())
			{
				dbOperHandler.Reset();
				dbOperHandler.ConditionExpress = "Id=@Id";
				dbOperHandler.AddConditionParameter("@Id", Id);
				dbOperHandler.AddFieldItem("IsRead", "1");
				result = dbOperHandler.Update("N_UserEmail");
			}
			return result;
		}

		public void GetMessageListJSON(int _thispage, int _pagesize, string _wherestr1, ref string _jsonstr)
		{
			using (DbOperHandler dbOperHandler = new ComData().Doh())
			{
				dbOperHandler.Reset();
				dbOperHandler.ConditionExpress = _wherestr1;
				int num = dbOperHandler.Count("N_UserMessage");
				string sql = SqlHelp.GetSql0(num + " as totalcount,row_number() over (order by Id desc) as rowid,dbo.f_GetUserName(UserId) as UserName,*", "N_UserMessage", "Id", _pagesize, _thispage, "desc", _wherestr1);
				dbOperHandler.Reset();
				dbOperHandler.SqlCmd = sql;
				DataTable dataTable = dbOperHandler.GetDataTable();
				_jsonstr = base.ConverTableToJSON2(dataTable);
				dataTable.Clear();
				dataTable.Dispose();
			}
		}

		public int DeletesMessage(string UserId)
		{
			int result;
			using (DbOperHandler dbOperHandler = new ComData().Doh())
			{
				dbOperHandler.Reset();
				dbOperHandler.ConditionExpress = "UserId=@UserId";
				dbOperHandler.AddConditionParameter("@UserId", UserId);
				result = dbOperHandler.Delete("N_UserMessage");
			}
			return result;
		}

		public int DeletesMessageById(string Id)
		{
			int result;
			using (DbOperHandler dbOperHandler = new ComData().Doh())
			{
				dbOperHandler.Reset();
				dbOperHandler.ConditionExpress = "Id=@Id";
				dbOperHandler.AddConditionParameter("@Id", Id);
				result = dbOperHandler.Delete("N_UserMessage");
			}
			return result;
		}

		public int ReadedUserMessage(string UserId)
		{
			int result;
			using (DbOperHandler dbOperHandler = new ComData().Doh())
			{
				dbOperHandler.Reset();
				dbOperHandler.ConditionExpress = "UserId=@UserId";
				dbOperHandler.AddConditionParameter("@UserId", UserId);
				dbOperHandler.AddFieldItem("IsRead", "1");
				result = dbOperHandler.Update("N_UserMessage");
			}
			return result;
		}

		public int UpdateMessageIsRead(string Id)
		{
			int result;
			using (DbOperHandler dbOperHandler = new ComData().Doh())
			{
				dbOperHandler.Reset();
				dbOperHandler.ConditionExpress = "Id=@Id";
				dbOperHandler.AddConditionParameter("@Id", Id);
				dbOperHandler.AddFieldItem("IsRead", "1");
				result = dbOperHandler.Update("N_UserMessage");
			}
			return result;
		}
	}
}
