using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Net;

namespace Lottery.Utils
{
	public static class ImageHelp
	{
		public static ImageFormat ImgFormat(string _Photo)
		{
			string text = _Photo.Substring(_Photo.LastIndexOf(".") + 1, _Photo.Length - _Photo.LastIndexOf(".") - 1).ToLower();
			ImageFormat result = ImageFormat.Jpeg;
			string a;
			if ((a = text) != null)
			{
				if (a == "png")
				{
					result = ImageFormat.Png;
					return result;
				}
				if (a == "gif")
				{
					result = ImageFormat.Gif;
					return result;
				}
				if (a == "bmp")
				{
					result = ImageFormat.Bmp;
					return result;
				}
			}
			result = ImageFormat.Jpeg;
			return result;
		}

		public static bool LocalImage2Thumbs(string originalImagePath, string thumbnailPath, int width, int height, string mode)
		{
			Image image = Image.FromFile(originalImagePath);
			ImageHelp.Image2Thumbs(image, thumbnailPath, width, height, mode);
			image.Dispose();
			return true;
		}

		public static bool RemoteImage2Thumbs(string remoteImageUrl, string thumbnailPath, int width, int height, string mode)
		{
			bool result;
			try
			{
				WebRequest webRequest = WebRequest.Create(remoteImageUrl);
				webRequest.Timeout = 20000;
				Stream responseStream = webRequest.GetResponse().GetResponseStream();
				Image image = Image.FromStream(responseStream);
				ImageHelp.Image2Thumbs(image, thumbnailPath, width, height, mode);
				image.Dispose();
				result = true;
			}
			catch
			{
				result = false;
			}
			return result;
		}

		public static void Image2Thumbs(Image originalImage, string thumbnailPath, int photoWidth, int photoHeight, string mode)
		{
			int num = photoWidth;
			int num2 = photoHeight;
			int num3 = photoWidth;
			int num4 = photoHeight;
			int x = 0;
			int y = 0;
			int num5 = originalImage.Width;
			int num6 = originalImage.Height;
			int x2 = 0;
			int y2 = 0;
			string a;
			if ((a = mode.ToUpper()) != null)
			{
				if (!(a == "FILL"))
				{
					if (!(a == "HW"))
					{
						if (!(a == "W"))
						{
							if (!(a == "H"))
							{
								if (a == "CUT")
								{
									if ((double)originalImage.Width / (double)originalImage.Height > (double)num / (double)num2)
									{
										num6 = originalImage.Height;
										num5 = originalImage.Height * num / num2;
										y = 0;
										x = (originalImage.Width - num5) / 2;
									}
									else
									{
										num5 = originalImage.Width;
										num6 = originalImage.Width * photoHeight / num;
										x = 0;
										y = (originalImage.Height - num6) / 2;
									}
								}
							}
							else
							{
								num = (num3 = originalImage.Width * photoHeight / originalImage.Height);
							}
						}
						else
						{
							num2 = (num4 = originalImage.Height * photoWidth / originalImage.Width);
						}
					}
				}
				else
				{
					num4 = photoHeight;
					num3 = num4 * num5 / num6;
					if (num3 > photoWidth)
					{
						num4 = num4 * photoWidth / num3;
						num3 = photoWidth;
					}
					x2 = (photoWidth - num3) / 2;
					y2 = (photoHeight - num4) / 2;
				}
			}
			Image image = new Bitmap(num, num2);
			Graphics graphics = Graphics.FromImage(image);
			graphics.InterpolationMode = InterpolationMode.High;
			graphics.SmoothingMode = SmoothingMode.HighQuality;
			graphics.CompositingQuality = CompositingQuality.HighQuality;
			graphics.InterpolationMode = InterpolationMode.High;
			graphics.Clear(Color.White);
			graphics.DrawImage(originalImage, new Rectangle(x2, y2, num3, num4), new Rectangle(x, y, num5, num6), GraphicsUnit.Pixel);
			try
			{
				image.Save(thumbnailPath, ImageHelp.ImgFormat(thumbnailPath));
			}
			catch (Exception ex)
			{
				throw ex;
			}
			finally
			{
				image.Dispose();
				graphics.Dispose();
			}
		}

		public static void MakeMyThumbs(string originalImagePath, string thumbnailPath, int toW, int toH, int X, int Y, int W, int H)
		{
			Image image = Image.FromFile(originalImagePath);
			Image image2 = new Bitmap(toW, toH);
			Graphics graphics = Graphics.FromImage(image2);
			graphics.InterpolationMode = InterpolationMode.High;
			graphics.SmoothingMode = SmoothingMode.HighQuality;
			graphics.CompositingQuality = CompositingQuality.HighQuality;
			graphics.InterpolationMode = InterpolationMode.High;
			graphics.Clear(Color.Transparent);
			graphics.DrawImage(image, new Rectangle(0, 0, toW, toH), new Rectangle(X, Y, W, H), GraphicsUnit.Pixel);
			try
			{
				image2.Save(thumbnailPath, ImageHelp.ImgFormat(thumbnailPath));
			}
			catch (Exception ex)
			{
				throw ex;
			}
			finally
			{
				image.Dispose();
				image2.Dispose();
				graphics.Dispose();
			}
		}

		public static void AddWater(string Path, string Path_sy, string addText)
		{
			Image image = Image.FromFile(Path);
			Graphics graphics = Graphics.FromImage(image);
			graphics.DrawImage(image, 0, 0, image.Width, image.Height);
			Font font = new Font("Verdana", 60f);
			Brush brush = new SolidBrush(Color.Green);
			graphics.DrawString(addText, font, brush, 35f, 35f);
			graphics.Dispose();
			image.Save(Path_sy);
			image.Dispose();
		}

		public static void AddImageSignPic(string Path, string filename, string watermarkFilename, int watermarkStatus, int quality, int watermarkTransparency)
		{
			Image image = Image.FromFile(Path);
			Graphics graphics = Graphics.FromImage(image);
			Image image2 = new Bitmap(watermarkFilename);
			if (image2.Height >= image.Height || image2.Width >= image.Width)
			{
				return;
			}
			ImageAttributes imageAttributes = new ImageAttributes();
			ColorMap[] map = new ColorMap[]
			{
				new ColorMap
				{
					OldColor = Color.FromArgb(255, 0, 255, 0),
					NewColor = Color.FromArgb(0, 0, 0, 0)
				}
			};
			imageAttributes.SetRemapTable(map, ColorAdjustType.Bitmap);
			float num = 0.5f;
			if (watermarkTransparency >= 1 && watermarkTransparency <= 10)
			{
				num = (float)watermarkTransparency / 10f;
			}
			float[][] array = new float[5][];
			float[][] arg_BD_0 = array;
			int arg_BD_1 = 0;
			float[] array2 = new float[5];
			array2[0] = 1f;
			arg_BD_0[arg_BD_1] = array2;
			float[][] arg_D4_0 = array;
			int arg_D4_1 = 1;
			float[] array3 = new float[5];
			array3[1] = 1f;
			arg_D4_0[arg_D4_1] = array3;
			float[][] arg_EB_0 = array;
			int arg_EB_1 = 2;
			float[] array4 = new float[5];
			array4[2] = 1f;
			arg_EB_0[arg_EB_1] = array4;
			float[][] arg_FF_0 = array;
			int arg_FF_1 = 3;
			float[] array5 = new float[5];
			array5[3] = num;
			arg_FF_0[arg_FF_1] = array5;
			array[4] = new float[]
			{
				0f,
				0f,
				0f,
				0f,
				1f
			};
			float[][] newColorMatrix = array;
			ColorMatrix newColorMatrix2 = new ColorMatrix(newColorMatrix);
			imageAttributes.SetColorMatrix(newColorMatrix2, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);
			int x = 0;
			int y = 0;
			switch (watermarkStatus)
			{
			case 1:
				x = (int)((float)image.Width * 0.01f);
				y = (int)((float)image.Height * 0.01f);
				break;
			case 2:
				x = (int)((float)image.Width * 0.5f - (float)(image2.Width / 2));
				y = (int)((float)image.Height * 0.01f);
				break;
			case 3:
				x = (int)((float)image.Width * 0.99f - (float)image2.Width);
				y = (int)((float)image.Height * 0.01f);
				break;
			case 4:
				x = (int)((float)image.Width * 0.01f);
				y = (int)((float)image.Height * 0.5f - (float)(image2.Height / 2));
				break;
			case 5:
				x = (int)((float)image.Width * 0.5f - (float)(image2.Width / 2));
				y = (int)((float)image.Height * 0.5f - (float)(image2.Height / 2));
				break;
			case 6:
				x = (int)((float)image.Width * 0.99f - (float)image2.Width);
				y = (int)((float)image.Height * 0.5f - (float)(image2.Height / 2));
				break;
			case 7:
				x = (int)((float)image.Width * 0.01f);
				y = (int)((float)image.Height * 0.99f - (float)image2.Height);
				break;
			case 8:
				x = (int)((float)image.Width * 0.5f - (float)(image2.Width / 2));
				y = (int)((float)image.Height * 0.99f - (float)image2.Height);
				break;
			case 9:
				x = (int)((float)image.Width * 0.99f - (float)image2.Width);
				y = (int)((float)image.Height * 0.99f - (float)image2.Height);
				break;
			}
			graphics.DrawImage(image2, new Rectangle(x, y, image2.Width, image2.Height), 0, 0, image2.Width, image2.Height, GraphicsUnit.Pixel, imageAttributes);
			ImageCodecInfo[] imageEncoders = ImageCodecInfo.GetImageEncoders();
			ImageCodecInfo imageCodecInfo = null;
			ImageCodecInfo[] array6 = imageEncoders;
			for (int i = 0; i < array6.Length; i++)
			{
				ImageCodecInfo imageCodecInfo2 = array6[i];
				if (imageCodecInfo2.MimeType.Contains("jpeg"))
				{
					imageCodecInfo = imageCodecInfo2;
				}
			}
			EncoderParameters encoderParameters = new EncoderParameters();
			long[] array7 = new long[1];
			if (quality < 0 || quality > 100)
			{
				quality = 80;
			}
			array7[0] = (long)quality;
			EncoderParameter encoderParameter = new EncoderParameter(Encoder.Quality, array7);
			encoderParameters.Param[0] = encoderParameter;
			if (imageCodecInfo != null)
			{
				image.Save(filename, imageCodecInfo, encoderParameters);
			}
			else
			{
				image.Save(filename);
			}
			graphics.Dispose();
			image.Dispose();
			image2.Dispose();
			imageAttributes.Dispose();
		}

		public static void AddWaterPic(string Path, string Path_syp, string Path_sypf)
		{
			Image image = Image.FromFile(Path);
			Image image2 = Image.FromFile(Path_sypf);
			Graphics graphics = Graphics.FromImage(image);
			graphics.DrawImage(image2, new Rectangle(image.Width - image2.Width, image.Height - image2.Height, image2.Width, image2.Height), 0, 0, image2.Width, image2.Height, GraphicsUnit.Pixel);
			graphics.Dispose();
			image.Save(Path_syp);
			image.Dispose();
		}
	}
}
