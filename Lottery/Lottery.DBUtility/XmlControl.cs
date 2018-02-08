using System;
using System.Data;
using System.IO;
using System.Xml;

namespace Lottery.DBUtility
{
	public class XmlControl
	{
		public XmlControl(string _XmlFile)
		{
			try
			{
				this._objXmlDoc.Load(_XmlFile);
			}
			catch (Exception ex)
			{
				throw ex;
			}
			this._strXmlFile = _XmlFile;
		}

		public XmlNodeList GetList(string _XmlPathNode)
		{
			return this._objXmlDoc.SelectNodes(_XmlPathNode);
		}

		public bool ExistNode(string _XmlPathNode)
		{
			return this._objXmlDoc.SelectSingleNode(_XmlPathNode) != null;
		}

		public DataView GetData(string _XmlPathNode)
		{
			DataSet dataSet = new DataSet();
			StringReader reader = new StringReader(this._objXmlDoc.SelectSingleNode(_XmlPathNode).OuterXml);
			dataSet.ReadXml(reader);
			return dataSet.Tables[0].DefaultView;
		}

		public DataTable GetTable(string _XmlPathNode)
		{
			DataTable result;
			try
			{
				DataSet dataSet = new DataSet();
				StringReader reader = new StringReader(this._objXmlDoc.SelectSingleNode(_XmlPathNode).OuterXml);
				dataSet.ReadXml(reader);
				result = dataSet.Tables[0];
			}
			catch
			{
				result = null;
			}
			return result;
		}

		public string GetText(string _XmlPathNode)
		{
			return this.GetText(_XmlPathNode, false);
		}

		public string GetText(string _XmlPathNode, bool isCDATA)
		{
			XmlNode xmlNode = this._objXmlDoc.SelectSingleNode(_XmlPathNode);
			if (xmlNode == null)
			{
				return "";
			}
			if (isCDATA)
			{
				return xmlNode.FirstChild.InnerText;
			}
			return xmlNode.InnerText;
		}

		public void Update(string _XmlPathNode, string _Content)
		{
			this.Update(_XmlPathNode, _Content, false);
		}

		public void Update(string _XmlPathNode, string _Content, bool isCDATA)
		{
			if (isCDATA)
			{
				this._objXmlDoc.SelectSingleNode(_XmlPathNode).FirstChild.InnerText = _Content;
				return;
			}
			this._objXmlDoc.SelectSingleNode(_XmlPathNode).InnerText = _Content;
		}

		public void Delete(string _XmlPathNode)
		{
			XmlNode xmlNode = this._objXmlDoc.SelectSingleNode(_XmlPathNode);
			if (xmlNode != null)
			{
				xmlNode.ParentNode.RemoveChild(xmlNode);
			}
		}

		public void RemoveAll(string _MainNode)
		{
			XmlNode xmlNode = this._objXmlDoc.SelectSingleNode(_MainNode);
			if (xmlNode != null)
			{
				xmlNode.RemoveAll();
			}
		}

		public void InsertNode(string _MainNode, string _ChildNode, string _Element, string _Content)
		{
			XmlNode xmlNode = this._objXmlDoc.SelectSingleNode(_MainNode);
			XmlElement xmlElement = this._objXmlDoc.CreateElement(_ChildNode);
			if (xmlNode != null)
			{
				xmlNode.AppendChild(xmlElement);
				XmlElement xmlElement2 = this._objXmlDoc.CreateElement(_Element);
				xmlElement2.InnerText = _Content;
				xmlElement.AppendChild(xmlElement2);
			}
		}

		public void InsertElement(string _MainNode, string _Element, string _Attrib, string _AttribContent, string _Content)
		{
			XmlNode xmlNode = this._objXmlDoc.SelectSingleNode(_MainNode);
			XmlElement xmlElement = this._objXmlDoc.CreateElement(_Element);
			xmlElement.SetAttribute(_Attrib, _AttribContent);
			xmlElement.InnerText = _Content;
			if (xmlNode != null)
			{
				xmlNode.AppendChild(xmlElement);
			}
		}

		public void InsertElement(string _MainNode, string _Element, string _Content, bool isCDATA)
		{
			XmlNode xmlNode = this._objXmlDoc.SelectSingleNode(_MainNode);
			XmlElement xmlElement = this._objXmlDoc.CreateElement(_Element);
			if (isCDATA)
			{
				xmlElement.InnerXml = "<![CDATA[" + _Content + "]]>";
			}
			else
			{
				xmlElement.InnerText = _Content;
			}
			if (xmlNode != null)
			{
				xmlNode.AppendChild(xmlElement);
			}
		}

		public void Save()
		{
			try
			{
				this._objXmlDoc.Save(this._strXmlFile);
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public void Dispose()
		{
			this._objXmlDoc = null;
		}

		private string _strXmlFile;

		private XmlDocument _objXmlDoc = new XmlDocument();
	}
}
