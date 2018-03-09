using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;
using Lottery.DAL.Flex;
using System.Web;
using Lottery.Entity;
using System.Net;
using System.IO;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.Crypto.Parameters;
using System.Security.Cryptography;

namespace Lottery.IPhone.WFP
{
    /// <summary>
    /// 微付支付信息
    /// </summary>
    public static class WfpHelper
    {
        /// <summary>
        /// 微付商户Id
        /// </summary>
        public static String WFP_USER = "";

        /// <summary>
        /// 微付API版本
        /// </summary>
        public static String WFP_API_VERSION = "";

        /// <summary>
        /// 微付签名方式
        /// </summary>
        public static String WFP_SIGN = "";

        /// <summary>
        /// 微付支付API
        /// </summary>
        public static String WFP_PAY_API = "";

        /// <summary>
        /// 微付订单查询API
        /// </summary>
        public static String WFP_QUERY_API = "";

        /// <summary>
        /// 微付支付私有密钥
        /// </summary>
        public static String WFP_PRIVATE_KEY = "";

        /// <summary>
        /// 是否检查客户端IP
        /// </summary>
        public static string WFP_CHECK_IP = "";

        static WfpHelper()
        {
            if (ConfigurationManager.AppSettings["WfpUser"] != null)
            {
                WFP_USER = ConfigurationManager.AppSettings["WfpUser"].ToString();
            }

            if (ConfigurationManager.AppSettings["WfpApiVersion"] != null)
            {
                WFP_API_VERSION = ConfigurationManager.AppSettings["WfpApiVersion"].ToString();
            }

            if (ConfigurationManager.AppSettings["WfpSign"] != null)
            {
                WFP_SIGN = ConfigurationManager.AppSettings["WfpSign"].ToString();
            }

            if (ConfigurationManager.AppSettings["WfpPayApi"] != null)
            {
                WFP_PAY_API = ConfigurationManager.AppSettings["WfpPayApi"].ToString();
            }

            if (ConfigurationManager.AppSettings["WfpQueryApi"] != null)
            {
                WFP_QUERY_API = ConfigurationManager.AppSettings["WfpQueryApi"].ToString();
            }

            if (ConfigurationManager.AppSettings["WfpPrivateKey"] != null)
            {
                WFP_PRIVATE_KEY = ConfigurationManager.AppSettings["WfpPrivateKey"].ToString();
            }

            if (ConfigurationManager.AppSettings["WfpCheckIp"] != null)
            {
                WFP_CHECK_IP = ConfigurationManager.AppSettings["WfpCheckIp"].ToString();
            }
        }

        /// <summary>
        /// 格式化字符串空格
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string Trim(string str)
        {
            return str == null ? "" : str.Trim();
        }

        /// <summary>
        /// 获取随笔付支付方式
        /// </summary>
        /// <param name="sysCode">平台支付方式编码</param>
        /// <returns></returns>
        public static ChannelMapModel GetChannelMap(string sysCode)
        {
            SbfDAL dal = new SbfDAL();
            IList<ChannelMapModel> channels = dal.GetSbfChannels();

            foreach (ChannelMapModel map in channels)
            {
                if (map != null && map.SysCode.Equals(sysCode, StringComparison.OrdinalIgnoreCase))
                {
                    return map;
                }
            }

            return null;
        }

        /// <summary>
        /// 获取页面地址
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        public static string GetUrl(string page)
        {
            string scheme = HttpContext.Current.Request.Url.Scheme;
            string host = HttpContext.Current.Request.Url.Host;
            int port = HttpContext.Current.Request.Url.Port;
            string url = string.Empty;
            string[] segments = HttpContext.Current.Request.Url.Segments;
            for (int i = 0; i < segments.Length - 1; i++)
            {
                url = url + segments[i];
            }

            return string.Format("{0}://{1}:{2}{3}{4}", scheme, host, port, url, page);
        }

        /// <summary>
        /// MD5加密
        /// </summary>
        /// <param name="dataStr"></param>
        /// <param name="codeType"></param>
        /// <returns></returns>
        public static string GetMD5(string dataStr, string codeType)
        {
            System.Security.Cryptography.MD5 md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
            byte[] t = md5.ComputeHash(System.Text.Encoding.GetEncoding(codeType).GetBytes(dataStr));
            System.Text.StringBuilder sb = new System.Text.StringBuilder(32);
            for (int i = 0; i < t.Length; i++)
            {
                sb.Append(t[i].ToString("x").PadLeft(2, '0'));
            }
            return sb.ToString();
        }

        /// <summary>
        /// 搜集请求参数
        /// </summary>
        /// <returns></returns>
        public static string GetRequestData()
        {
            StringBuilder parStr = new StringBuilder();

            //QueryString
            var pars = HttpContext.Current.Request.QueryString;

            foreach (var key in pars.AllKeys)
            {
                parStr.AppendFormat("&{0}={1}", key, pars[key]);
            }

            //Form
            pars = HttpContext.Current.Request.Form;

            foreach (var key in pars.AllKeys)
            {
                parStr.AppendFormat("&{0}={1}", key, pars[key]);
            }

            if (parStr.Length <= 0)
            {
                return string.Empty;
            }

            return parStr.ToString().Substring(1);
        }


        //商户私钥签名
        public static string RSASign(string signStr, string privateKey)
        {
            try
            {
                RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
                RSAParameters para = new RSAParameters();
                rsa.FromXmlString(privateKey);
                byte[] signBytes = rsa.SignData(UTF8Encoding.UTF8.GetBytes(signStr), "md5");
                return Convert.ToBase64String(signBytes);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        //RSA私钥格式转换
        public static string RSAPrivateKeyJava2DotNet(string privateKey)
        {
            RsaPrivateCrtKeyParameters privateKeyParam = (RsaPrivateCrtKeyParameters)PrivateKeyFactory.CreateKey(Convert.FromBase64String(privateKey));
            return string.Format(
                "<RSAKeyValue><Modulus>{0}</Modulus><Exponent>{1}</Exponent><P>{2}</P><Q>{3}</Q><DP>{4}</DP><DQ>{5}</DQ><InverseQ>{6}</InverseQ><D>{7}</D></RSAKeyValue>",
                Convert.ToBase64String(privateKeyParam.Modulus.ToByteArrayUnsigned()),
                Convert.ToBase64String(privateKeyParam.PublicExponent.ToByteArrayUnsigned()),
                Convert.ToBase64String(privateKeyParam.P.ToByteArrayUnsigned()),
                Convert.ToBase64String(privateKeyParam.Q.ToByteArrayUnsigned()),
                Convert.ToBase64String(privateKeyParam.DP.ToByteArrayUnsigned()),
                Convert.ToBase64String(privateKeyParam.DQ.ToByteArrayUnsigned()),
                Convert.ToBase64String(privateKeyParam.QInv.ToByteArrayUnsigned()),
                Convert.ToBase64String(privateKeyParam.Exponent.ToByteArrayUnsigned())
            );
        }

        //使用微付公钥验签
        public static bool ValidateRsaSign(string plainText, string publicKey, string signedData)
        {
            try
            {
                RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
                RSAParameters para = new RSAParameters();
                rsa.FromXmlString(publicKey);
                return rsa.VerifyData(UTF8Encoding.UTF8.GetBytes(plainText), "md5", Convert.FromBase64String(signedData));
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        //微付公钥格式转换
        public static string RSAPublicKeyJava2DotNet(string publicKey)
        {
            //publicKey = publicKey.Trim().Replace("%", "").Replace(",", "").Replace(" ", "+");
            //if (publicKey.Length % 4 > 0)
            //{
            //    publicKey = publicKey.PadRight(publicKey.Length + 4 - publicKey.Length % 4, '=');
            //}

            RsaKeyParameters publicKeyParam = (RsaKeyParameters)PublicKeyFactory.CreateKey(Convert.FromBase64String(publicKey));
            return string.Format(
                "<RSAKeyValue><Modulus>{0}</Modulus><Exponent>{1}</Exponent></RSAKeyValue>",
                Convert.ToBase64String(publicKeyParam.Modulus.ToByteArrayUnsigned()),
                Convert.ToBase64String(publicKeyParam.Exponent.ToByteArrayUnsigned())
            );
        }

        /// <summary>
        /// post请求到指定地址并获取返回的信息内容
        /// </summary>
        /// <param name="url">请求地址</param>
        /// <param name="postData">请求参数</param>
        /// <param name="encodeType">编码类型如：UTF-8</param>
        /// <returns>返回响应内容</returns>
        public static string HttpPost(string POSTURL, string PostData)
        {
            //发送请求的数据
            WebRequest myHttpWebRequest = WebRequest.Create(POSTURL);
            myHttpWebRequest.Method = "POST";
            UTF8Encoding encoding = new UTF8Encoding();
            byte[] byte1 = encoding.GetBytes(PostData);
            myHttpWebRequest.ContentType = "application/x-www-form-urlencoded";
            myHttpWebRequest.ContentLength = byte1.Length;
            Stream newStream = myHttpWebRequest.GetRequestStream();
            newStream.Write(byte1, 0, byte1.Length);
            newStream.Close();

            //发送成功后接收返回的XML信息
            HttpWebResponse response = (HttpWebResponse)myHttpWebRequest.GetResponse();
            string lcHtml = string.Empty;
            Encoding enc = Encoding.GetEncoding("UTF-8");
            Stream stream = response.GetResponseStream();
            StreamReader streamReader = new StreamReader(stream, enc);
            lcHtml = streamReader.ReadToEnd();
            return lcHtml;
        }

        /// <summary>
        /// 以GET方式抓取远程页面内容
        /// </summary>
        /// <param name="url">请求地址</param>
        /// <returns></returns>
        public static string HttpGet(string url)
        {
            string strResult;
            try
            {
                HttpWebRequest hwr = (HttpWebRequest)HttpWebRequest.Create(url);
                hwr.Timeout = 19600;
                HttpWebResponse hwrs = (HttpWebResponse)hwr.GetResponse();
                Stream myStream = hwrs.GetResponseStream();
                StreamReader sr = new StreamReader(myStream, Encoding.UTF8);
                StringBuilder sb = new StringBuilder();
                while (-1 != sr.Peek())
                {
                    sb.Append(sr.ReadLine() + "\r\n");
                }
                strResult = sb.ToString();
                hwrs.Close();
            }
            catch (Exception ee)
            {
                strResult = ee.Message;
            }
            return strResult;
        }
    }
}
