using System;
using System.Collections;
using System.IO;
using System.Net.Sockets;
using System.Text;

namespace Lottery.Utils.Mail
{
	public class SmtpServerHelper
	{
		public string ErrMsg
		{
			get
			{
				return this.errmsg;
			}
			set
			{
				this.errmsg = value;
			}
		}

		public SmtpServerHelper()
		{
			this.SMTPCodeAdd();
		}

		~SmtpServerHelper()
		{
			this.networkStream.Close();
			this.tcpClient.Close();
		}

		private string Base64Encode(string str)
		{
			byte[] bytes = Encoding.Default.GetBytes(str);
			return Convert.ToBase64String(bytes);
		}

		private string Base64Decode(string str)
		{
			byte[] bytes = Convert.FromBase64String(str);
			return Encoding.Default.GetString(bytes);
		}

		private string GetStream(string FilePath)
		{
			FileStream fileStream = new FileStream(FilePath, FileMode.Open);
			byte[] array = new byte[Convert.ToInt32(fileStream.Length)];
			fileStream.Read(array, 0, array.Length);
			fileStream.Close();
			return Convert.ToBase64String(array);
		}

		private void SMTPCodeAdd()
		{
			this.ErrCodeHT.Add("421", "服务未就绪，关闭传输信道");
			this.ErrCodeHT.Add("432", "需要一个密码转换");
			this.ErrCodeHT.Add("450", "要求的邮件操作未完成，邮箱不可用（例如，邮箱忙）");
			this.ErrCodeHT.Add("451", "放弃要求的操作；处理过程中出错");
			this.ErrCodeHT.Add("452", "系统存储不足，要求的操作未执行");
			this.ErrCodeHT.Add("454", "临时认证失败");
			this.ErrCodeHT.Add("500", "邮箱地址错误");
			this.ErrCodeHT.Add("501", "参数格式错误");
			this.ErrCodeHT.Add("502", "命令不可实现");
			this.ErrCodeHT.Add("503", "服务器需要SMTP验证");
			this.ErrCodeHT.Add("504", "命令参数不可实现");
			this.ErrCodeHT.Add("530", "需要认证");
			this.ErrCodeHT.Add("534", "认证机制过于简单");
			this.ErrCodeHT.Add("538", "当前请求的认证机制需要加密");
			this.ErrCodeHT.Add("550", "要求的邮件操作未完成，邮箱不可用（例如，邮箱未找到，或不可访问）");
			this.ErrCodeHT.Add("551", "用户非本地，请尝试<forward-path>");
			this.ErrCodeHT.Add("552", "过量的存储分配，要求的操作未执行");
			this.ErrCodeHT.Add("553", "邮箱名不可用，要求的操作未执行（例如邮箱格式错误）");
			this.ErrCodeHT.Add("554", "传输失败");
			this.RightCodeHT.Add("220", "服务就绪");
			this.RightCodeHT.Add("221", "服务关闭传输信道");
			this.RightCodeHT.Add("235", "验证成功");
			this.RightCodeHT.Add("250", "要求的邮件操作完成");
			this.RightCodeHT.Add("251", "非本地用户，将转发向<forward-path>");
			this.RightCodeHT.Add("334", "服务器响应验证Base64字符串");
			this.RightCodeHT.Add("354", "开始邮件输入，以<CRLF>.<CRLF>结束");
		}

		private bool SendCommand(string str)
		{
			if (str == null || str.Trim() == string.Empty)
			{
				return true;
			}
			this.logs += str;
			byte[] bytes = Encoding.Default.GetBytes(str);
			try
			{
				this.networkStream.Write(bytes, 0, bytes.Length);
			}
			catch
			{
				this.errmsg = "网络连接错误";
				return false;
			}
			return true;
		}

		private string RecvResponse()
		{
			string text = string.Empty;
			byte[] array = new byte[1024];
			int num;
			try
			{
				num = this.networkStream.Read(array, 0, array.Length);
			}
			catch
			{
				this.errmsg = "网络连接错误";
				string result = "false";
				return result;
			}
			if (num == 0)
			{
				return text;
			}
			text = Encoding.Default.GetString(array).Substring(0, num);
			this.logs = this.logs + text + this.CRLF;
			return text;
		}

		private bool Dialog(string str, string errstr)
		{
			if (str == null || str.Trim() == string.Empty)
			{
				return true;
			}
			if (!this.SendCommand(str))
			{
				return false;
			}
			string text = this.RecvResponse();
			if (text == "false")
			{
				return false;
			}
			string text2 = text.Substring(0, 3);
			if (this.RightCodeHT[text2] != null)
			{
				return true;
			}
			if (this.ErrCodeHT[text2] != null)
			{
				this.errmsg = this.errmsg + text2 + this.ErrCodeHT[text2].ToString();
				this.errmsg += this.CRLF;
			}
			else
			{
				this.errmsg += text;
			}
			this.errmsg += errstr;
			return false;
		}

		private bool Dialog(string[] str, string errstr)
		{
			for (int i = 0; i < str.Length; i++)
			{
				if (!this.Dialog(str[i], ""))
				{
					this.errmsg += this.CRLF;
					this.errmsg += errstr;
					return false;
				}
			}
			return true;
		}

		private bool Connect(string smtpServer, int port)
		{
			try
			{
				this.tcpClient = new TcpClient(smtpServer, port);
			}
			catch (Exception ex)
			{
				this.errmsg = ex.ToString();
				bool result = false;
				return result;
			}
			this.networkStream = this.tcpClient.GetStream();
			if (this.RightCodeHT[this.RecvResponse().Substring(0, 3)] == null)
			{
				this.errmsg = "网络连接失败";
				return false;
			}
			return true;
		}

		private string GetPriorityString(MailPriority mailPriority)
		{
			string result = "Normal";
			if (mailPriority == MailPriority.Low)
			{
				result = "Low";
			}
			else if (mailPriority == MailPriority.High)
			{
				result = "High";
			}
			return result;
		}

		public bool SendEmail(string smtpServer, int port, MailMessage mailMessage)
		{
			return this.SendEmail(smtpServer, port, false, "", "", mailMessage);
		}

		public bool SendEmail(string smtpServer, int port, string username, string password, MailMessage mailMessage)
		{
			return this.SendEmail(smtpServer, port, true, username, password, mailMessage);
		}

		private bool SendEmail(string smtpServer, int port, bool ESmtp, string username, string password, MailMessage mailMessage)
		{
			if (!this.Connect(smtpServer, port))
			{
				return false;
			}
			string priorityString = this.GetPriorityString(mailMessage.Priority);
			bool flag = mailMessage.BodyFormat == MailFormat.HTML;
			string text;
			if (ESmtp)
			{
				if (!this.Dialog(new string[]
				{
					"EHLO " + smtpServer + this.CRLF,
					"AUTH LOGIN" + this.CRLF,
					this.Base64Encode(username) + this.CRLF,
					this.Base64Encode(password) + this.CRLF
				}, "SMTP服务器验证失败，请核对用户名和密码。"))
				{
					return false;
				}
			}
			else
			{
				text = "HELO " + smtpServer + this.CRLF;
				if (!this.Dialog(text, ""))
				{
					return false;
				}
			}
			text = "MAIL FROM:<" + username + ">" + this.CRLF;
			if (!this.Dialog(text, "发件人地址错误，或不能为空"))
			{
				return false;
			}
			string[] array = new string[mailMessage.Recipients.Count];
			for (int i = 0; i < mailMessage.Recipients.Count; i++)
			{
				array[i] = "RCPT TO:<" + (string)mailMessage.Recipients[i] + ">" + this.CRLF;
			}
			if (!this.Dialog(array, "收件人地址有误"))
			{
				return false;
			}
			text = "DATA" + this.CRLF;
			if (!this.Dialog(text, ""))
			{
				return false;
			}
			text = string.Concat(new string[]
			{
				"From:",
				mailMessage.FromName,
				"<",
				mailMessage.From,
				">",
				this.CRLF
			});
			if (mailMessage.Recipients.Count == 0)
			{
				return false;
			}
			string text2 = text;
			text = string.Concat(new string[]
			{
				text2,
				"To:=?",
				mailMessage.Charset.ToUpper(),
				"?B?",
				this.Base64Encode((string)mailMessage.Recipients[0]),
				"?=<",
				(string)mailMessage.Recipients[0],
				">",
				this.CRLF
			});
			text = text + ((mailMessage.Subject == string.Empty || mailMessage.Subject == null) ? "Subject:" : ((mailMessage.Charset == "") ? ("Subject:" + mailMessage.Subject) : string.Concat(new string[]
			{
				"Subject:=?",
				mailMessage.Charset.ToUpper(),
				"?B?",
				this.Base64Encode(mailMessage.Subject),
				"?="
			}))) + this.CRLF;
			text = text + "X-Priority:" + priorityString + this.CRLF;
			text = text + "X-MSMail-Priority:" + priorityString + this.CRLF;
			text = text + "Importance:" + priorityString + this.CRLF;
			text = text + "X-Mailer: Lottery.Utils.Mail.SmtpMail Pubclass [cn]" + this.CRLF;
			text = text + "MIME-Version: 1.0" + this.CRLF;
			if (mailMessage.Attachments.Count != 0)
			{
				text = text + "Content-Type: multipart/mixed;" + this.CRLF;
				string text3 = text;
				text = string.Concat(new string[]
				{
					text3,
					" boundary=\"=====",
					flag ? "001_Dragon520636771063_" : "001_Dragon303406132050_",
					"=====\"",
					this.CRLF,
					this.CRLF
				});
			}
			if (flag)
			{
				if (mailMessage.Attachments.Count == 0)
				{
					text = text + "Content-Type: multipart/alternative;" + this.CRLF;
					text = text + " boundary=\"=====003_Dragon520636771063_=====\"" + this.CRLF + this.CRLF;
					text = text + "This is a multi-part message in MIME format." + this.CRLF + this.CRLF;
				}
				else
				{
					text = text + "This is a multi-part message in MIME format." + this.CRLF + this.CRLF;
					text = text + "--=====001_Dragon520636771063_=====" + this.CRLF;
					text = text + "Content-Type: multipart/alternative;" + this.CRLF;
					text = text + " boundary=\"=====003_Dragon520636771063_=====\"" + this.CRLF + this.CRLF;
				}
				text = text + "--=====003_Dragon520636771063_=====" + this.CRLF;
				text = text + "Content-Type: text/plain;" + this.CRLF;
				text = text + ((mailMessage.Charset == "") ? " charset=\"iso-8859-1\"" : (" charset=\"" + mailMessage.Charset.ToLower() + "\"")) + this.CRLF;
				text = text + "Content-Transfer-Encoding: base64" + this.CRLF + this.CRLF;
				text = text + this.Base64Encode("邮件内容为HTML格式，请选择HTML方式查看") + this.CRLF + this.CRLF;
				text = text + "--=====003_Dragon520636771063_=====" + this.CRLF;
				text = text + "Content-Type: text/html;" + this.CRLF;
				text = text + ((mailMessage.Charset == "") ? " charset=\"iso-8859-1\"" : (" charset=\"" + mailMessage.Charset.ToLower() + "\"")) + this.CRLF;
				text = text + "Content-Transfer-Encoding: base64" + this.CRLF + this.CRLF;
				text = text + this.Base64Encode(mailMessage.Body) + this.CRLF + this.CRLF;
				text = text + "--=====003_Dragon520636771063_=====--" + this.CRLF;
			}
			else
			{
				if (mailMessage.Attachments.Count != 0)
				{
					text = text + "--=====001_Dragon303406132050_=====" + this.CRLF;
				}
				text = text + "Content-Type: text/plain;" + this.CRLF;
				text = text + ((mailMessage.Charset == "") ? " charset=\"iso-8859-1\"" : (" charset=\"" + mailMessage.Charset.ToLower() + "\"")) + this.CRLF;
				text = text + "Content-Transfer-Encoding: base64" + this.CRLF + this.CRLF;
				text = text + this.Base64Encode(mailMessage.Body) + this.CRLF;
			}
			if (mailMessage.Attachments.Count != 0)
			{
				for (int j = 0; j < mailMessage.Attachments.Count; j++)
				{
					string text4 = mailMessage.Attachments[j];
					string text5 = text;
					text = string.Concat(new string[]
					{
						text5,
						"--=====",
						flag ? "001_Dragon520636771063_" : "001_Dragon303406132050_",
						"=====",
						this.CRLF
					});
					text = text + "Content-Type: text/plain;" + this.CRLF;
					string text6 = text;
					text = string.Concat(new string[]
					{
						text6,
						" name=\"=?",
						mailMessage.Charset.ToUpper(),
						"?B?",
						this.Base64Encode(text4.Substring(text4.LastIndexOf("\\") + 1)),
						"?=\"",
						this.CRLF
					});
					text = text + "Content-Transfer-Encoding: base64" + this.CRLF;
					text = text + "Content-Disposition: attachment;" + this.CRLF;
					string text7 = text;
					text = string.Concat(new string[]
					{
						text7,
						" filename=\"=?",
						mailMessage.Charset.ToUpper(),
						"?B?",
						this.Base64Encode(text4.Substring(text4.LastIndexOf("\\") + 1)),
						"?=\"",
						this.CRLF,
						this.CRLF
					});
					text = text + this.GetStream(text4) + this.CRLF + this.CRLF;
				}
				string text8 = text;
				text = string.Concat(new string[]
				{
					text8,
					"--=====",
					flag ? "001_Dragon520636771063_" : "001_Dragon303406132050_",
					"=====--",
					this.CRLF,
					this.CRLF
				});
			}
			text = text + this.CRLF + "." + this.CRLF;
			if (!this.Dialog(text, "错误信件信息"))
			{
				return false;
			}
			text = "QUIT" + this.CRLF;
			if (!this.Dialog(text, "断开连接时错误"))
			{
				return false;
			}
			this.networkStream.Close();
			this.tcpClient.Close();
			return true;
		}

		private string CRLF = "\r\n";

		private string errmsg;

		private TcpClient tcpClient;

		private NetworkStream networkStream;

		private string logs = "";

		private Hashtable ErrCodeHT = new Hashtable();

		private Hashtable RightCodeHT = new Hashtable();
	}
}
