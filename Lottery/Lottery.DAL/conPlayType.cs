using System;
using System.Data;
using System.IO;
using System.Text;
using System.Web;
using Lottery.DBUtility;
using Lottery.Utils;

namespace Lottery.DAL
{
	public class conPlayType : ComData
	{
		public void Create(int typeId, string url)
		{
			using (DbOperHandler dbOperHandler = new ComData().Doh())
			{
				dbOperHandler.Reset();
				dbOperHandler.SqlCmd = "SELECT [Id],[TypeId],[Title] FROM [Sys_PlayBigType] where TypeId=" + typeId + " and IsOpen=0 order by sort ";
				DataTable dataTable = dbOperHandler.GetDataTable();
				dbOperHandler.Reset();
				dbOperHandler.SqlCmd = "SELECT [Id],[Type],[Title0],[Title],[Title2],[Radio] FROM [Sys_PlaySmallType] where IsOpen=0 and flag=0 order by Id ";
				DataTable dataTable2 = dbOperHandler.GetDataTable();
				string txtStr = base.ConverTableToXML(dataTable, dataTable2);
				this.SaveJsFile(txtStr, HttpContext.Current.Server.MapPath(url), "2");
			}
		}

		protected void SaveJsFile(string TxtStr, string TxtFile, string Edcode)
		{
			Encoding encoding = Encoding.Default;
			if (Edcode != null)
			{
				if (!(Edcode == "1"))
				{
					if (!(Edcode == "2"))
					{
						if (Edcode == "3")
						{
							encoding = Encoding.Unicode;
						}
					}
					else
					{
						encoding = Encoding.UTF8;
					}
				}
				else
				{
					encoding = Encoding.GetEncoding("GB2312");
				}
			}
			DirFile.CreateFolder(DirFile.GetFolderPath(false, TxtFile));
			StreamWriter streamWriter = new StreamWriter(TxtFile, false, encoding);
			streamWriter.Write(TxtStr);
			streamWriter.Close();
		}
	}
}
