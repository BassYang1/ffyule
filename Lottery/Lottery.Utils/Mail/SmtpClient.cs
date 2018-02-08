using System;

namespace Lottery.Utils.Mail
{
	public class SmtpClient
	{
		public SmtpClient()
		{
		}

		public SmtpClient(string _smtpServer, int _smtpPort)
		{
			this._SmtpServer = _smtpServer;
			this._SmtpPort = _smtpPort;
		}

		public string ErrMsg
		{
			get
			{
				return this.errmsg;
			}
		}

		public string SmtpServer
		{
			get
			{
				return this._SmtpServer;
			}
			set
			{
				this._SmtpServer = value;
			}
		}

		public int SmtpPort
		{
			get
			{
				return this._SmtpPort;
			}
			set
			{
				this._SmtpPort = value;
			}
		}

		public bool Send(MailMessage mailMessage, string username, string password)
		{
			SmtpServerHelper smtpServerHelper = new SmtpServerHelper();
			if (smtpServerHelper.SendEmail(this._SmtpServer, this._SmtpPort, username, password, mailMessage))
			{
				return true;
			}
			this.errmsg = smtpServerHelper.ErrMsg;
			return false;
		}

		private string _SmtpServer;

		private int _SmtpPort;

		private string errmsg;
	}
}
