using System;
using System.Collections;
using System.IO;

namespace Lottery.Utils.Mail
{
	public class MailAttachments
	{
		public MailAttachments()
		{
			this._Attachments = new ArrayList();
		}

		public string this[int index]
		{
			get
			{
				return (string)this._Attachments[index];
			}
		}

		public void Add(params string[] filePath)
		{
			if (filePath == null)
			{
				throw new ArgumentNullException("非法的附件");
			}
			for (int i = 0; i < filePath.Length; i++)
			{
				this.Add(filePath[i]);
			}
		}

		public void Add(string filePath)
		{
			if (File.Exists(filePath) && this._Attachments.Count < 10)
			{
				this._Attachments.Add(filePath);
			}
		}

		public void Clear()
		{
			this._Attachments.Clear();
		}

		public int Count
		{
			get
			{
				return this._Attachments.Count;
			}
		}

		private const int MaxAttachmentNum = 10;

		private IList _Attachments;
	}
}
