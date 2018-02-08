using System;
using System.Configuration;
using Lottery.DBUtility;

namespace Lottery.DAL
{
	public class conNumber : ComData
	{
		public void Create(int loid, string title, string number)
		{
			string arg = ConfigurationManager.AppSettings["DataUrl"].ToString();
			string xmlFile = arg + loid + ".xml";
			XmlControl xmlControl = new XmlControl(xmlFile);
			xmlControl.Update("Root/Title", title);
			xmlControl.Update("Root/Number", number);
			xmlControl.Save();
			xmlControl.Dispose();
		}
	}
}
