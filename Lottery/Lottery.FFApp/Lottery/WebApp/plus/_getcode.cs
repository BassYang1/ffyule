using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Threading;
using Lottery.DAL;
using Lottery.Utils;

namespace Lottery.WebApp.Plus
{
	public class _getcode : BasicPage
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			base.Response.Expires = 0;
			base.Response.Buffer = true;
			base.Response.ExpiresAbsolute = DateTime.Now.AddSeconds(-1.0);
			base.Response.AddHeader("pragma", "no-cache");
			base.Response.CacheControl = "no-cache";
			string validateCode = ValidateCode.GetValidateCode(this.letterCount, true);
			this.CreateImage(validateCode);
		}

		public void CreateImage(string checkCode)
		{
			int num = checkCode.Length * this.letterWidth;
			Random random = new Random();
			Bitmap bitmap = new Bitmap(num, this.letterHeight);
			Graphics graphics = Graphics.FromImage(bitmap);
			Random random2 = new Random();
			graphics.Clear(Color.White);
			for (int i = 0; i < 10; i++)
			{
				int x = random2.Next(bitmap.Width);
				int x2 = random2.Next(bitmap.Width);
				int y = random2.Next(bitmap.Height);
				int y2 = random2.Next(bitmap.Height);
				graphics.DrawLine(new Pen(Color.Silver), x, y, x2, y2);
			}
			for (int i = 0; i < 10; i++)
			{
				int x3 = random2.Next(bitmap.Width);
				int y3 = random2.Next(bitmap.Height);
				bitmap.SetPixel(x3, y3, Color.FromArgb(random2.Next()));
			}
			for (int j = 0; j < checkCode.Length; j++)
			{
				int num2 = random.Next(this.fonts.Length - 1);
				string s = checkCode.Substring(j, 1);
				Brush brush = new SolidBrush(this.GetRandomColor());
				Point p = new Point(j * this.letterWidth + 1 + random.Next(3), 1 + random.Next(3));
				graphics.DrawString(s, new Font(this.fonts[num2], 14f, FontStyle.Bold), brush, p);
			}
			graphics.DrawRectangle(new Pen(Color.LightGray, 1f), 0, 0, num - 1, this.letterHeight - 1);
			MemoryStream memoryStream = new MemoryStream();
			bitmap.Save(memoryStream, ImageFormat.Png);
			base.Response.ClearContent();
			base.Response.ContentType = "image/Png";
			base.Response.BinaryWrite(memoryStream.ToArray());
			graphics.Dispose();
			bitmap.Dispose();
		}

		public Bitmap TwistImage(Bitmap srcBmp, bool bXDir, double dMultValue, double dPhase)
		{
			Bitmap bitmap = new Bitmap(srcBmp.Width, srcBmp.Height);
			Graphics graphics = Graphics.FromImage(bitmap);
			graphics.FillRectangle(new SolidBrush(Color.White), 0, 0, bitmap.Width, bitmap.Height);
			graphics.Dispose();
			double num = bXDir ? ((double)bitmap.Height) : ((double)bitmap.Width);
			for (int i = 0; i < bitmap.Width; i++)
			{
				for (int j = 0; j < bitmap.Height; j++)
				{
					double num2 = bXDir ? (6.2831853071795862 * (double)j / num) : (6.2831853071795862 * (double)i / num);
					num2 += dPhase;
					double num3 = Math.Sin(num2);
					int num4 = bXDir ? (i + (int)(num3 * dMultValue)) : i;
					int num5 = bXDir ? j : (j + (int)(num3 * dMultValue));
					Color pixel = srcBmp.GetPixel(i, j);
					if (num4 >= 0 && num4 < bitmap.Width && num5 >= 0 && num5 < bitmap.Height)
					{
						bitmap.SetPixel(num4, num5, pixel);
					}
				}
			}
			return bitmap;
		}

		public Color GetRandomColor()
		{
			Random random = new Random((int)DateTime.Now.Ticks);
			Thread.Sleep(random.Next(50));
			Random random2 = new Random((int)DateTime.Now.Ticks);
			int num = random.Next(210);
			int num2 = random2.Next(180);
			int num3 = (num + num2 > 300) ? 0 : (400 - num - num2);
			num3 = ((num3 > 255) ? 255 : num3);
			return Color.FromArgb(num, num2, num3);
		}

		private const double PI = 3.1415926535897931;

		private const double PI2 = 6.2831853071795862;

		private int letterWidth = 20;

		private int letterHeight = 32;

		private int letterCount = 4;

		private char[] chars = "0123456789QWERTYUIOPASDFGHJKLZXCVBNM".ToCharArray();

		private string[] fonts = new string[]
		{
			"Arial",
			"Georgia"
		};
	}
}
