// Decompiled with JetBrains decompiler
// Type: Lottery.Collect.Public
// Assembly: Lottery.Collect, Version=7.0.1.203, Culture=neutral, PublicKeyToken=null
// MVID: 916E4E87-E8A0-4A21-8438-E89468303682
// Assembly location: F:\pros\tianheng\bf\WebAppOld\bin\Lottery.Collect.dll

using Lottery.DAL;
using Lottery.Utils;
using System;
using System.Configuration;
using System.IO;
using System.Net;
using System.Text;
using System.Xml;

namespace Lottery.Collect
{
    public class Public
    {
        public static string GetJson(string loid)
        {
            string shtml = HtmlOperate.GetHtml(ConfigurationManager.AppSettings["CollectUrl"].ToString() + "/Data/hisStory.xml?" + (object)new Random().Next(1, 1000));
            if (!string.IsNullOrEmpty(shtml))
            {
                foreach (XmlNode xmlNode in Public.GetXmlNode(shtml, "row"))
                {
                    string innerText1 = xmlNode.Attributes["code"].InnerText;
                    string innerText2 = xmlNode.Attributes["expect"].InnerText;
                    string str1 = xmlNode.Attributes["opencode"].InnerText.Replace("+", ",");
                    switch (loid)
                    {
                        case "1001":
                            if ("cqssc".Equals(xmlNode.Attributes["code"].InnerText))
                            {
                                shtml = "[{\"title\": \"" + (innerText2.Substring(0, 8) + "-" + innerText2.Substring(8)) + "\",\"number\": \"" + str1 + "\"}]";
                                break;
                            }
                            break;
                        case "1003":
                            if ("xjssc".Equals(xmlNode.Attributes["code"].InnerText))
                            {
                                shtml = "[{\"title\": \"" + (innerText2.Substring(0, 8) + "-" + innerText2.Substring(9)) + "\",\"number\": \"" + str1 + "\"}]";
                                break;
                            }
                            break;
                        case "1007":
                            if ("tjssc".Equals(xmlNode.Attributes["code"].InnerText))
                            {
                                shtml = "[{\"title\": \"" + (innerText2.Substring(0, 8) + "-" + innerText2.Substring(8)) + "\",\"number\": \"" + str1 + "\"}]";
                                break;
                            }
                            break;
                        case "1008":
                            if ("ynssc".Equals(xmlNode.Attributes["code"].InnerText))
                            {
                                shtml = "[{\"title\": \"" + (innerText2.Substring(0, 8) + "-" + innerText2.Substring(8)) + "\",\"number\": \"" + str1 + "\"}]";
                                break;
                            }
                            break;
                        case "2001":
                            if ("sd11x5".Equals(xmlNode.Attributes["code"].InnerText))
                            {
                                shtml = "[{\"title\": \"" + (innerText2.Substring(0, 8) + "-" + innerText2.Substring(8)) + "\",\"number\": \"" + str1 + "\"}]";
                                break;
                            }
                            break;
                        case "2002":
                            if ("gd11x5".Equals(xmlNode.Attributes["code"].InnerText))
                            {
                                shtml = "[{\"title\": \"" + (innerText2.Substring(0, 8) + "-" + innerText2.Substring(8)) + "\",\"number\": \"" + str1 + "\"}]";
                                break;
                            }
                            break;
                        case "2003":
                            if ("sh11x5".Equals(xmlNode.Attributes["code"].InnerText))
                            {
                                shtml = "[{\"title\": \"" + (innerText2.Substring(0, 8) + "-" + innerText2.Substring(8)) + "\",\"number\": \"" + str1 + "\"}]";
                                break;
                            }
                            break;
                        case "2004":
                            if ("jx11x5".Equals(xmlNode.Attributes["code"].InnerText))
                            {
                                shtml = "[{\"title\": \"" + (innerText2.Substring(0, 8) + "-" + innerText2.Substring(8)) + "\",\"number\": \"" + str1 + "\"}]";
                                break;
                            }
                            break;
                        case "1010":
                            if ("krkeno".Equals(xmlNode.Attributes["code"].InnerText))
                            {
                                string[] strArray = str1.Split(',');
                                string str2 = ((Convert.ToInt32(strArray[0]) + Convert.ToInt32(strArray[1]) + Convert.ToInt32(strArray[2]) + Convert.ToInt32(strArray[3])) % 10).ToString() + "," + (object)((Convert.ToInt32(strArray[4]) + Convert.ToInt32(strArray[5]) + Convert.ToInt32(strArray[6]) + Convert.ToInt32(strArray[7])) % 10) + "," + (object)((Convert.ToInt32(strArray[8]) + Convert.ToInt32(strArray[9]) + Convert.ToInt32(strArray[10]) + Convert.ToInt32(strArray[11])) % 10) + "," + (object)((Convert.ToInt32(strArray[12]) + Convert.ToInt32(strArray[13]) + Convert.ToInt32(strArray[14]) + Convert.ToInt32(strArray[15])) % 10) + "," + (object)((Convert.ToInt32(strArray[16]) + Convert.ToInt32(strArray[17]) + Convert.ToInt32(strArray[18]) + Convert.ToInt32(strArray[19])) % 10);
                                shtml = "[{\"title\": \"" + innerText2 + "\",\"number\": \"" + str2 + "\"}]";
                                break;
                            }
                            break;
                        case "1012":
                            if ("sgkeno".Equals(xmlNode.Attributes["code"].InnerText))
                            {
                                string[] strArray = str1.Split(',');
                                string str2 = ((Convert.ToInt32(strArray[0]) + Convert.ToInt32(strArray[1]) + Convert.ToInt32(strArray[2]) + Convert.ToInt32(strArray[3])) % 10).ToString() + "," + (object)((Convert.ToInt32(strArray[4]) + Convert.ToInt32(strArray[5]) + Convert.ToInt32(strArray[6]) + Convert.ToInt32(strArray[7])) % 10) + "," + (object)((Convert.ToInt32(strArray[8]) + Convert.ToInt32(strArray[9]) + Convert.ToInt32(strArray[10]) + Convert.ToInt32(strArray[11])) % 10) + "," + (object)((Convert.ToInt32(strArray[12]) + Convert.ToInt32(strArray[13]) + Convert.ToInt32(strArray[14]) + Convert.ToInt32(strArray[15])) % 10) + "," + (object)((Convert.ToInt32(strArray[16]) + Convert.ToInt32(strArray[17]) + Convert.ToInt32(strArray[18]) + Convert.ToInt32(strArray[19])) % 10);
                                shtml = "[{\"title\": \"" + innerText2 + "\",\"number\": \"" + str2 + "\"}]";
                                break;
                            }
                            break;
                        case "1013":
                            if ("twbingo".Equals(xmlNode.Attributes["code"].InnerText))
                            {
                                string[] strArray = str1.Split(',');
                                string str2 = ((Convert.ToInt32(strArray[0]) + Convert.ToInt32(strArray[1]) + Convert.ToInt32(strArray[2]) + Convert.ToInt32(strArray[3])) % 10).ToString() + "," + (object)((Convert.ToInt32(strArray[4]) + Convert.ToInt32(strArray[5]) + Convert.ToInt32(strArray[6]) + Convert.ToInt32(strArray[7])) % 10) + "," + (object)((Convert.ToInt32(strArray[8]) + Convert.ToInt32(strArray[9]) + Convert.ToInt32(strArray[10]) + Convert.ToInt32(strArray[11])) % 10) + "," + (object)((Convert.ToInt32(strArray[12]) + Convert.ToInt32(strArray[13]) + Convert.ToInt32(strArray[14]) + Convert.ToInt32(strArray[15])) % 10) + "," + (object)((Convert.ToInt32(strArray[16]) + Convert.ToInt32(strArray[17]) + Convert.ToInt32(strArray[18]) + Convert.ToInt32(strArray[19])) % 10);
                                shtml = "[{\"title\": \"" + innerText2 + "\",\"number\": \"" + str2 + "\"}]";
                                break;
                            }
                            break;
                        case "3004":
                            if ("krkeno".Equals(xmlNode.Attributes["code"].InnerText))
                            {
                                string[] strArray = str1.Split(',');
                                string str2 = ((Convert.ToInt32(strArray[0]) + Convert.ToInt32(strArray[1]) + Convert.ToInt32(strArray[2]) + Convert.ToInt32(strArray[3]) + Convert.ToInt32(strArray[4]) + Convert.ToInt32(strArray[5]) + Convert.ToInt32(strArray[6])) % 10).ToString() + "," + (object)((Convert.ToInt32(strArray[7]) + Convert.ToInt32(strArray[8]) + Convert.ToInt32(strArray[9]) + Convert.ToInt32(strArray[10]) + Convert.ToInt32(strArray[11]) + Convert.ToInt32(strArray[12]) + Convert.ToInt32(strArray[13])) % 10) + "," + (object)((Convert.ToInt32(strArray[14]) + Convert.ToInt32(strArray[15]) + Convert.ToInt32(strArray[16]) + Convert.ToInt32(strArray[17]) + Convert.ToInt32(strArray[18]) + Convert.ToInt32(strArray[19])) % 10);
                                shtml = "[{\"title\": \"" + innerText2 + "\",\"number\": \"" + str2 + "\"}]";
                                break;
                            }
                            break;
                        case "3001":
                            if ("shssl".Equals(xmlNode.Attributes["code"].InnerText))
                            {
                                shtml = "[{\"title\": \"" + (innerText2.Substring(0, 8) + "-" + innerText2.Substring(8)) + "\",\"number\": \"" + str1 + "\"}]";
                                break;
                            }
                            break;
                    }
                }
            }
            return shtml;
        }

        public static string GetBetRankJson()
        {
            return HtmlOperate.GetHtml(ConfigurationManager.AppSettings["CollectUrl"].ToString() + "/Data/BetRank.xml?" + (object)new Random().Next(1, 1000));
        }

        public static void SetOpenListJson(int lotteryId)
        {
            string _xml = "";
            string _jsonstr = "";
            new LotteryDataDAL().GetListJSON(lotteryId, ref _jsonstr, ref _xml);
            string str1 = ConfigurationManager.AppSettings["DataUrl"].ToString();
            string str2 = str1 + "OpenList" + (object)lotteryId + ".xml";
            DirFile.CreateFolder(DirFile.GetFolderPath(false, str2));
            StreamWriter streamWriter1 = new StreamWriter(str2, false, Encoding.UTF8);
            streamWriter1.Write(_jsonstr);
            streamWriter1.Close();
            string str3 = str1 + "lottery" + (object)lotteryId + ".xml";
            DirFile.CreateFolder(DirFile.GetFolderPath(false, str3));
            StreamWriter streamWriter2 = new StreamWriter(str3, false, Encoding.UTF8);
            streamWriter2.Write(_xml);
            streamWriter2.Close();
        }

        public static string GetOpenListJson(int lotteryId)
        {
            string str = ConfigurationManager.AppSettings["CollectUrl"].ToString() + "/Data/OpenList" + (object)lotteryId + ".xml";
            if (Public.RemoteFileExists(str))
                return HtmlOperate.GetHtml(str);
            return "";
        }

        /// <summary>
        /// 获取开奖信息
        /// </summary>
        /// <param name="lotteryCode">彩种编号</param>
        /// <returns></returns>
        public static string GetOpenListJson(string lotteryCode)
        {
            var url = ConfigurationManager.AppSettings["LotteryUrl"].ToString();
            string token = ConfigurationManager.AppSettings["LotteryToken"].ToString();

            if (string.IsNullOrEmpty(url))
            {
                return "无效的开奖地址";
            }

            if (string.IsNullOrEmpty(token))
            {
                return "无效的用户Token";
            }

            try
            {
                url = string.Format("{0}/token/{1}/code/{2}/format/json", url, token, lotteryCode);
                string detail = HtmlOperate.GetHtml(url);

                if (string.IsNullOrEmpty(detail))
                {
                    return "无效的开效信息";
                }

                return detail;
            }
            catch (Exception ex)
            {
                return "开奖信息异常";
            }
        }

        public static string GetUserJson(int UserId)
        {
            string str = ConfigurationManager.AppSettings["CollectUrl"].ToString() + "/Data/User/User" + (object)UserId + ".xml";
            if (Public.RemoteFileExists(str))
                return HtmlOperate.GetHtml(str);
            return "";
        }

        public static XmlNodeList GetXmlNode(string shtml, string rootElm)
        {
            XmlNodeList xmlNodeList = (XmlNodeList)null;
            try
            {
                XmlDocument xmlDocument = new XmlDocument();
                xmlDocument.LoadXml(shtml);
                xmlNodeList = xmlDocument.ChildNodes.Item(1).SelectNodes(rootElm);
            }
            catch
            {
            }
            return xmlNodeList;
        }

        public static bool RemoteFileExists(string fileUrl)
        {
            HttpWebRequest httpWebRequest = (HttpWebRequest)null;
            HttpWebResponse httpWebResponse = (HttpWebResponse)null;
            try
            {
                httpWebRequest = (HttpWebRequest)WebRequest.Create(fileUrl);
                httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                if (httpWebResponse.ContentLength != 0L)
                    return true;
            }
            catch (Exception ex)
            {
                return false;
            }
            finally
            {
                if (httpWebRequest != null)
                    httpWebRequest.Abort();
                if (httpWebResponse != null)
                    httpWebResponse.Close();
            }
            return false;
        }
    }
}
