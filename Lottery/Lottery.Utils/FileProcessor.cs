using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading;

namespace Lottery.Utils
{
	public class FileProcessor : IDisposable
	{
		public FileProcessor(string uploadLocation)
		{
			this._currentFilePath = uploadLocation;
		}

		public List<string> FinishedFiles
		{
			get
			{
				return this._finishedFiles;
			}
		}

		public void ProcessBuffer(ref byte[] bufferData, bool addToBufferHistory)
		{
			int num = -1;
			if (!this._startFound)
			{
				num = this.GetStartBytePosition(ref bufferData);
				if (num != -1)
				{
					this._startIndexBufferID = this._currentBufferIndex + 1L;
					this._startLocationInBufferID = num;
					this._startFound = true;
				}
			}
			if (this._startFound)
			{
				int num2 = 0;
				if (num != -1)
				{
					num2 = num;
				}
				int num3 = bufferData.Length - num2;
				int endBytePosition = this.GetEndBytePosition(ref bufferData);
				if (endBytePosition != -1)
				{
					num3 = endBytePosition - num2;
					this._endFound = true;
					this._endIndexBufferID = this._currentBufferIndex + 1L;
					this._endLocationInBufferID = endBytePosition;
				}
				if (num3 > 0)
				{
					if (this._fileStream == null)
					{
						this._fileStream = new FileStream(this._currentFilePath + this._currentFileName, FileMode.OpenOrCreate);
						int num4 = 3600;
						new Timer(new TimerCallback(FileProcessor.DeleteFile), this._currentFilePath + this._currentFileName, num4 * 1000, 0);
					}
					this._fileStream.Write(bufferData, num2, num3);
					this._fileStream.Flush();
				}
			}
			if (this._endFound)
			{
				this.CloseStreams();
				this._startFound = false;
				this._endFound = false;
				this.ProcessBuffer(ref bufferData, false);
			}
			if (addToBufferHistory)
			{
				this._bufferHistory.Add(this._currentBufferIndex, bufferData);
				this._currentBufferIndex += 1L;
				this.RemoveOldBufferData();
			}
		}

		private void RemoveOldBufferData()
		{
			for (long num = this._currentBufferIndex; num >= 0L; num -= 1L)
			{
				if (num <= this._currentBufferIndex - 3L)
				{
					if (this._bufferHistory.ContainsKey(num))
					{
						this._bufferHistory.Remove(num);
					}
					else
					{
						num = 0L;
					}
				}
			}
			GC.Collect();
		}

		public void CloseStreams()
		{
			if (this._fileStream != null)
			{
				this._fileStream.Dispose();
				this._fileStream.Close();
				this._fileStream = null;
				this._finishedFiles.Add(this._currentFileName);
				this._currentFileName = Guid.NewGuid().ToString() + ".bin";
			}
		}

		public void GetFieldSeperators(ref byte[] bufferData)
		{
			try
			{
				this._formPostID = Encoding.UTF8.GetString(bufferData).Substring(29, 13);
				this._fieldSeperator = "-----------------------------" + this._formPostID;
			}
			catch (Exception ex)
			{
				Debug.WriteLine("Error in GetFieldSeperators(): " + ex.Message);
			}
		}

		private int GetStartBytePosition(ref byte[] bufferData)
		{
			int num = 0;
			if (this._startIndexBufferID == this._currentBufferIndex + 1L)
			{
				num = this._startLocationInBufferID;
			}
			if (this._endIndexBufferID == this._currentBufferIndex + 1L)
			{
				num = this._endLocationInBufferID;
			}
			byte[] bytes = Encoding.UTF8.GetBytes("Content-Type: ");
			int num2 = FileProcessor.FindBytePattern(ref bufferData, ref bytes, num);
			if (num2 != -1)
			{
				bytes = Encoding.UTF8.GetBytes("\r\n\r\n");
				int num3 = FileProcessor.FindBytePattern(ref bufferData, ref bytes, num2);
				if (num3 != -1)
				{
					return num3 + 4;
				}
			}
			else
			{
				if (num - bytes.Length > 0)
				{
					return -1;
				}
				if (this._currentBufferIndex > 0L)
				{
					byte[] array = this._bufferHistory[this._currentBufferIndex - 1L];
					byte[] array2 = FileProcessor.MergeArrays(ref array, ref bufferData);
					byte[] bytes2 = Encoding.UTF8.GetBytes("Content-Type: ");
					num2 = FileProcessor.FindBytePattern(ref array2, ref bytes2, array.Length - bytes2.Length);
					if (num2 != -1)
					{
						bytes2 = Encoding.UTF8.GetBytes("Content-Type: ");
						int num4 = FileProcessor.FindBytePattern(ref array2, ref bytes2, array.Length - bytes2.Length);
						if (num4 != -1)
						{
							if (num4 > array.Length)
							{
								return num4 - array.Length;
							}
							return 0;
						}
					}
				}
			}
			return -1;
		}

		private int GetEndBytePosition(ref byte[] bufferData)
		{
			int num = 0;
			if (this._startIndexBufferID == this._currentBufferIndex + 1L)
			{
				num = this._startLocationInBufferID;
			}
			byte[] bytes = Encoding.UTF8.GetBytes(this._fieldSeperator);
			int num2 = FileProcessor.FindBytePattern(ref bufferData, ref bytes, num);
			if (num2 != -1)
			{
				if (num2 - 2 >= 0)
				{
					return num2 - 2;
				}
			}
			else
			{
				if (num - bytes.Length > 0)
				{
					return -1;
				}
				if (this._currentBufferIndex > 0L)
				{
					byte[] array = this._bufferHistory[this._currentBufferIndex - 1L];
					byte[] array2 = FileProcessor.MergeArrays(ref array, ref bufferData);
					byte[] bytes2 = Encoding.UTF8.GetBytes(this._fieldSeperator);
					num2 = FileProcessor.FindBytePattern(ref array2, ref bytes2, array.Length - bytes2.Length + num);
					if (num2 != -1)
					{
						bytes2 = Encoding.UTF8.GetBytes("\r\n\r\n");
						int num3 = FileProcessor.FindBytePattern(ref array2, ref bytes2, num2);
						if (num3 != -1)
						{
							if (num3 > array.Length)
							{
								return num3 - array.Length;
							}
							return -1;
						}
					}
				}
			}
			return -1;
		}

		private static int FindBytePattern(ref byte[] containerBytes, ref byte[] searchBytes, int startAtIndex)
		{
			int result = -1;
			for (int i = startAtIndex; i < containerBytes.Length; i++)
			{
				if (i + searchBytes.Length > containerBytes.Length)
				{
					return -1;
				}
				if (containerBytes[i] == searchBytes[0])
				{
					bool flag = true;
					int num = i;
					for (int j = 1; j < searchBytes.Length; j++)
					{
						num++;
						if (searchBytes[j] != containerBytes[num])
						{
							flag = false;
							break;
						}
					}
					if (flag)
					{
						return i;
					}
				}
			}
			return result;
		}

		private static byte[] MergeArrays(ref byte[] arrayOne, ref byte[] arrayTwo)
		{
			arrayOne.GetType().GetElementType();
			byte[] array = new byte[arrayOne.Length + arrayTwo.Length];
			arrayOne.CopyTo(array, 0);
			arrayTwo.CopyTo(array, arrayOne.Length);
			return array;
		}

		private static void DeleteFile(object filePath)
		{
			try
			{
				if (File.Exists((string)filePath))
				{
					File.Delete((string)filePath);
				}
			}
			catch
			{
			}
		}

		public void Dispose()
		{
			this._bufferHistory.Clear();
			GC.Collect();
		}

		private string _currentFilePath = "";

		private string _formPostID = "";

		private string _fieldSeperator = "";

		private long _currentBufferIndex;

		private bool _startFound;

		private bool _endFound;

		public string _currentFileName = Guid.NewGuid().ToString() + ".bin";

		private FileStream _fileStream;

		private long _startIndexBufferID = -1L;

		private int _startLocationInBufferID = -1;

		private long _endIndexBufferID = -1L;

		private int _endLocationInBufferID = -1;

		private Dictionary<long, byte[]> _bufferHistory = new Dictionary<long, byte[]>();

		private List<string> _finishedFiles = new List<string>();
	}
}
