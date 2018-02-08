using System;
using System.Data;
using System.IO;
using System.Text;
using System.Web;
using System.Xml;

namespace Lottery.Utils
{
	public static class XmlCOM
	{
		public static DataSet ReadXml(string path)
		{
			DataSet dataSet = new DataSet();
			FileStream fileStream = null;
			StreamReader streamReader = null;
			DataSet result;
			try
			{
				fileStream = new FileStream(path, FileMode.Open, FileAccess.Read);
				streamReader = new StreamReader(fileStream, Encoding.UTF8);
				dataSet.ReadXml(streamReader);
				result = dataSet;
			}
			finally
			{
				fileStream.Close();
				streamReader.Close();
			}
			return result;
		}

		public static string ReadConfig(string name, string key)
		{
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.Load(HttpContext.Current.Server.MapPath(name + ".config"));
			XmlNodeList elementsByTagName = xmlDocument.GetElementsByTagName(key);
			if (elementsByTagName.Count == 0)
			{
				return "";
			}
			XmlNode xmlNode = elementsByTagName[0];
			return xmlNode.InnerText;
		}

		public static void UpdateConfig(string name, string nKey, string nValue)
		{
			if (XmlCOM.ReadConfig(name, nKey) != "")
			{
				XmlDocument xmlDocument = new XmlDocument();
				xmlDocument.Load(HttpContext.Current.Server.MapPath(name + ".config"));
				XmlNodeList elementsByTagName = xmlDocument.GetElementsByTagName(nKey);
				XmlNode xmlNode = elementsByTagName[0];
				xmlNode.InnerText = nValue;
				XmlTextWriter xmlTextWriter = new XmlTextWriter(new StreamWriter(HttpContext.Current.Server.MapPath(name + ".config")));
				xmlTextWriter.Formatting = Formatting.Indented;
				xmlDocument.WriteTo(xmlTextWriter);
				xmlTextWriter.Close();
			}
		}

		public static string ReadXml(string name, string key)
		{
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.Load(HttpContext.Current.Server.MapPath(name + ".xml"));
			XmlNodeList elementsByTagName = xmlDocument.GetElementsByTagName(key);
			if (elementsByTagName.Count == 0)
			{
				return "";
			}
			XmlNode xmlNode = elementsByTagName[0];
			return xmlNode.InnerText;
		}
	}
}
