using System;
using System.Collections;

namespace Lottery.Utils.Mail
{
	public class MailMessage
	{
		public MailMessage()
		{
			this._Recipients = new ArrayList();
			this._Attachments = new MailAttachments();
			this._BodyFormat = MailFormat.HTML;
			this._Priority = MailPriority.Normal;
			this._Charset = "GB2312";
		}

		public string Charset
		{
			get
			{
				return this._Charset;
			}
			set
			{
				this._Charset = value;
			}
		}

		public int MaxRecipientNum
		{
			get
			{
				return this._MaxRecipientNum;
			}
			set
			{
				this._MaxRecipientNum = value;
			}
		}

		public string From
		{
			get
			{
				return this._From;
			}
			set
			{
				this._From = value;
			}
		}

		public string FromName
		{
			get
			{
				return this._FromName;
			}
			set
			{
				this._FromName = value;
			}
		}

		public string Body
		{
			get
			{
				return this._Body;
			}
			set
			{
				this._Body = value;
			}
		}

		public string Subject
		{
			get
			{
				return this._Subject;
			}
			set
			{
				this._Subject = value;
			}
		}

		public MailAttachments Attachments
		{
			get
			{
				return this._Attachments;
			}
			set
			{
				this._Attachments = value;
			}
		}

		public MailPriority Priority
		{
			get
			{
				return this._Priority;
			}
			set
			{
				this._Priority = value;
			}
		}

		public IList Recipients
		{
			get
			{
				return this._Recipients;
			}
		}

		public MailFormat BodyFormat
		{
			get
			{
				return this._BodyFormat;
			}
			set
			{
				this._BodyFormat = value;
			}
		}

		public void AddRecipients(string recipient)
		{
			if (this._Recipients.Count < this.MaxRecipientNum)
			{
				this._Recipients.Add(recipient);
			}
		}

		public void AddRecipients(params string[] recipient)
		{
			if (recipient == null)
			{
				throw new ArgumentException("收件人不能为空.");
			}
			for (int i = 0; i < recipient.Length; i++)
			{
				this.AddRecipients(recipient[i]);
			}
		}

		private int _MaxRecipientNum = 30;

		private string _From;

		private string _FromName;

		private IList _Recipients;

		private MailAttachments _Attachments;

		private string _Body;

		private string _Subject;

		private MailFormat _BodyFormat;

		private string _Charset = "GB2312";

		private MailPriority _Priority;
	}
}
