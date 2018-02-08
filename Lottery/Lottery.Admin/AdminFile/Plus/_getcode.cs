using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Security.Cryptography;
using Lottery.DAL;
using Lottery.Utils;

namespace Lottery.AdminFile.Plus
{
	public class _getcode : AdminBasicPage
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			base.Response.ClearContent();
			base.Response.ContentType = "image/jpeg";
			this.letterHeight = base.Str2Int(base.q("h"), 30);
			this.letterWidth = this.letterHeight;
			string validateCode = ValidateCode.GetValidateCode(this.letterCount, true);
			this.CreateImage(validateCode);
		}

		private static int Next(int max)
		{
			_getcode.rand.GetBytes(_getcode.randb);
			int num = BitConverter.ToInt32(_getcode.randb, 0);
			num %= max + 1;
			if (num < 0)
			{
				num = -num;
			}
			return num;
		}

		private static int Next(int min, int max)
		{
			return _getcode.Next(max - min) + min;
		}

		public void CreateImage(string checkCode)
		{
			int num = this.letterHeight * 3 / 4 - 3;
			if (num < 12)
			{
				num = 12;
			}
			if (num > 30)
			{
				num = 30;
			}
			int max = 3;
			Font[] array = new Font[]
			{
				new Font(new FontFamily("Times New Roman"), (float)(num + _getcode.Next(max)), FontStyle.Italic),
				new Font(new FontFamily("Times New Roman"), (float)(num + _getcode.Next(max)), FontStyle.Regular),
				new Font(new FontFamily("Times New Roman"), (float)(num + _getcode.Next(max)), FontStyle.Regular),
				new Font(new FontFamily("Times New Roman"), (float)(num + _getcode.Next(max)), FontStyle.Italic)
			};
			int width = checkCode.Length * this.letterWidth;
			Bitmap bitmap = new Bitmap(width, this.letterHeight);
			Graphics graphics = Graphics.FromImage(bitmap);
			graphics.Clear(Color.White);
			for (int i = 0; i < 2; i++)
			{
				int x = _getcode.Next(bitmap.Width - 1);
				int x2 = _getcode.Next(bitmap.Width - 1);
				int y = _getcode.Next(bitmap.Height - 1);
				int y2 = _getcode.Next(bitmap.Height - 1);
				graphics.DrawLine(new Pen(Color.Silver), x, y, x2, y2);
			}
			int num2 = -num + 6;
			for (int j = 0; j < checkCode.Length; j++)
			{
				int num3 = num2 + _getcode.Next(num - 2, num + 10);
				num2 = num3;
				int y3 = -3 + _getcode.Next(0, 3);
				string s = checkCode.Substring(j, 1);
				Brush brush = new SolidBrush(this.GetRandomColor());
				Point p = new Point(num3, y3);
				graphics.DrawString(s, array[_getcode.Next(array.Length - 1)], brush, p);
			}
			for (int i = 0; i < 20; i++)
			{
				int x3 = _getcode.Next(bitmap.Width - 1);
				int y4 = _getcode.Next(bitmap.Height - 1);
				bitmap.SetPixel(x3, y4, Color.FromArgb(_getcode.Next(0, 255), _getcode.Next(0, 255), _getcode.Next(0, 255)));
			}
			MemoryStream memoryStream = new MemoryStream();
			bitmap.Save(memoryStream, ImageFormat.Png);
			base.Response.BinaryWrite(memoryStream.ToArray());
			graphics.Dispose();
			bitmap.Dispose();
		}

		public Bitmap TwistImage(Bitmap srcBmp, bool bXDir, double dMultValue, double dPhase)
		{
			double num = 6.2831853071795862;
			Bitmap bitmap = new Bitmap(srcBmp.Width, srcBmp.Height);
			Graphics graphics = Graphics.FromImage(bitmap);
			graphics.FillRectangle(new SolidBrush(Color.White), 0, 0, bitmap.Width, bitmap.Height);
			graphics.Dispose();
			double num2 = bXDir ? ((double)bitmap.Height) : ((double)bitmap.Width);
			for (int i = 0; i < bitmap.Width; i++)
			{
				for (int j = 0; j < bitmap.Height; j++)
				{
					double num3 = bXDir ? (num * (double)j / num2) : (num * (double)i / num2);
					num3 += dPhase;
					double num4 = Math.Sin(num3);
					int num5 = bXDir ? (i + (int)(num4 * dMultValue)) : i;
					int num6 = bXDir ? j : (j + (int)(num4 * dMultValue));
					Color pixel = srcBmp.GetPixel(i, j);
					if (num5 >= 0 && num5 < bitmap.Width && num6 >= 0 && num6 < bitmap.Height)
					{
						bitmap.SetPixel(num5, num6, pixel);
					}
				}
			}
			srcBmp.Dispose();
			return bitmap;
		}

		public Color GetRandomColor()
		{
			return Color.FromArgb(0, 0, 0);
		}

		private int letterWidth = 30;

		private int letterHeight = 30;

		private int letterCount = 4;

		private static byte[] randb = new byte[4];

		private static RNGCryptoServiceProvider rand = new RNGCryptoServiceProvider();
	}
}
