using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace Lottery.Utils.IPSearchHelp
{
	public class SearchIndex
	{
		static SearchIndex()
		{
			SearchIndex.pcz.SetDbFilePath(SearchIndex.filePath);
		}

		public static string GetIPLocation(string IPValue)
		{
			string result;
			lock (SearchIndex.lockHelper)
			{
				string addressWithIP = SearchIndex.pcz.GetAddressWithIP(IPValue.Trim());
				if (SearchIndex.fileIsExsit)
				{
					if (addressWithIP.IndexOf("IANA") >= 0)
					{
						result = "";
					}
					else
					{
						result = addressWithIP;
					}
				}
				else
				{
					result = null;
				}
			}
			return result;
		}

		private static object lockHelper = new object();

		private static SearchIndex.PHCZIP pcz = new SearchIndex.PHCZIP();

		private static string filePath = HttpContext.Current.Server.MapPath("/statics/data/ipdata.config");

		private static bool fileIsExsit = true;

		public class CZ_INDEX_INFO
		{
			public CZ_INDEX_INFO()
			{
				this.IpSet = 0u;
				this.IpEnd = 0u;
				this.Offset = 0u;
			}

			public uint IpSet;

			public uint IpEnd;

			public uint Offset;
		}

		public class PHCZIP
		{
			public PHCZIP()
			{
				this.bFilePathInitialized = false;
			}

			public PHCZIP(string dbFilePath)
			{
				this.bFilePathInitialized = false;
				this.SetDbFilePath(dbFilePath);
			}

			public void Initialize()
			{
				this.Search_Index_Set = 0u;
				this.Search_Index_End = this.Index_Count - 1u;
			}

			public void Dispose()
			{
				if (this.bFilePathInitialized)
				{
					this.bFilePathInitialized = false;
					this.FileStrm.Close();
					this.FileStrm.Dispose();
				}
			}

			public bool SetDbFilePath(string dbFilePath)
			{
				if (dbFilePath == "")
				{
					return false;
				}
				try
				{
					this.FileStrm = new FileStream(dbFilePath, FileMode.Open, FileAccess.Read, FileShare.Read);
				}
				catch
				{
					bool result = false;
					return result;
				}
				if (this.FileStrm.Length < 8L)
				{
					this.FileStrm.Close();
					this.FileStrm.Dispose();
					return false;
				}
				this.FileStrm.Seek(0L, SeekOrigin.Begin);
				this.Index_Set = this.GetUInt32();
				this.Index_End = this.GetUInt32();
				this.Index_Count = (this.Index_End - this.Index_Set) / 7u + 1u;
				this.bFilePathInitialized = true;
				return true;
			}

			public string GetAddressWithIP(string IPValue)
			{
				if (!this.bFilePathInitialized)
				{
					return "";
				}
				this.Initialize();
				uint num = this.IPToUInt32(IPValue);
				while (true)
				{
					this.Search_Set = this.IndexInfoAtPos(this.Search_Index_Set);
					this.Search_End = this.IndexInfoAtPos(this.Search_Index_End);
					if (num >= this.Search_Set.IpSet && num <= this.Search_Set.IpEnd)
					{
						break;
					}
					if (num >= this.Search_End.IpSet && num <= this.Search_End.IpEnd)
					{
						goto Block_5;
					}
					this.Search_Mid = this.IndexInfoAtPos((this.Search_Index_End + this.Search_Index_Set) / 2u);
					if (num >= this.Search_Mid.IpSet && num <= this.Search_Mid.IpEnd)
					{
						goto Block_7;
					}
					if (num < this.Search_Mid.IpSet)
					{
						this.Search_Index_End = (this.Search_Index_End + this.Search_Index_Set) / 2u;
					}
					else
					{
						this.Search_Index_Set = (this.Search_Index_End + this.Search_Index_Set) / 2u;
					}
				}
				return this.ReadAddressInfoAtOffset(this.Search_Set.Offset);
				Block_5:
				return this.ReadAddressInfoAtOffset(this.Search_End.Offset);
				Block_7:
				return this.ReadAddressInfoAtOffset(this.Search_Mid.Offset);
			}

			private string ReadAddressInfoAtOffset(uint Offset)
			{
				this.FileStrm.Seek((long)((ulong)(Offset + 4u)), SeekOrigin.Begin);
				byte tag = this.GetTag();
				string str;
				string str2;
				if (tag == 1)
				{
					this.FileStrm.Seek((long)((ulong)this.GetOffset()), SeekOrigin.Begin);
					tag = this.GetTag();
					if (tag == 2)
					{
						uint offset = this.GetOffset();
						str = this.ReadArea();
						this.FileStrm.Seek((long)((ulong)offset), SeekOrigin.Begin);
						str2 = this.ReadString();
					}
					else
					{
						this.FileStrm.Seek(-1L, SeekOrigin.Current);
						str2 = this.ReadString();
						str = this.ReadArea();
					}
				}
				else if (tag == 2)
				{
					uint offset = this.GetOffset();
					str = this.ReadArea();
					this.FileStrm.Seek((long)((ulong)offset), SeekOrigin.Begin);
					str2 = this.ReadString();
				}
				else
				{
					this.FileStrm.Seek(-1L, SeekOrigin.Current);
					str2 = this.ReadString();
					str = this.ReadArea();
				}
				return str2 + " " + str;
			}

			private uint GetOffset()
			{
				return BitConverter.ToUInt32(new byte[]
				{
					(byte)this.FileStrm.ReadByte(),
					(byte)this.FileStrm.ReadByte(),
					(byte)this.FileStrm.ReadByte(),
					0
				}, 0);
			}

			protected string ReadArea()
			{
				byte tag = this.GetTag();
				if (tag == 1 || tag == 2)
				{
					this.FileStrm.Seek((long)((ulong)this.GetOffset()), SeekOrigin.Begin);
					return this.ReadString();
				}
				this.FileStrm.Seek(-1L, SeekOrigin.Current);
				return this.ReadString();
			}

			protected string ReadString()
			{
				uint num = 0u;
				byte[] array = new byte[256];
				array[(int)((UIntPtr)num)] = (byte)this.FileStrm.ReadByte();
				while (array[(int)((UIntPtr)num)] != 0)
				{
					num += 1u;
					array[(int)((UIntPtr)num)] = (byte)this.FileStrm.ReadByte();
				}
				string arg_4C_0 = Encoding.Default.GetString(array);
				char[] trimChars = new char[1];
				return arg_4C_0.TrimEnd(trimChars);
			}

			protected byte GetTag()
			{
				return (byte)this.FileStrm.ReadByte();
			}

			protected SearchIndex.CZ_INDEX_INFO IndexInfoAtPos(uint Index_Pos)
			{
				SearchIndex.CZ_INDEX_INFO cZ_INDEX_INFO = new SearchIndex.CZ_INDEX_INFO();
				this.FileStrm.Seek((long)((ulong)(this.Index_Set + 7u * Index_Pos)), SeekOrigin.Begin);
				cZ_INDEX_INFO.IpSet = this.GetUInt32();
				cZ_INDEX_INFO.Offset = this.GetOffset();
				this.FileStrm.Seek((long)((ulong)cZ_INDEX_INFO.Offset), SeekOrigin.Begin);
				cZ_INDEX_INFO.IpEnd = this.GetUInt32();
				return cZ_INDEX_INFO;
			}

			public uint IPToUInt32(string IpValue)
			{
				string[] array = IpValue.Split(new char[]
				{
					'.'
				});
				int upperBound = array.GetUpperBound(0);
				if (upperBound != 3)
				{
					array = new string[4];
					for (int i = 1; i <= 3 - upperBound; i++)
					{
						array[upperBound + i] = "0";
					}
				}
				byte[] array2 = new byte[4];
				for (int j = 0; j <= 3; j++)
				{
					if (this.IsNumeric(array[j]))
					{
						array2[3 - j] = (byte)(Convert.ToInt32(array[j]) & 255);
					}
				}
				return BitConverter.ToUInt32(array2, 0);
			}

			protected bool IsNumeric(string str)
			{
				return str != null && Regex.IsMatch(str, "^-?\\d+$");
			}

			protected uint GetUInt32()
			{
				byte[] array = new byte[4];
				this.FileStrm.Read(array, 0, 4);
				return BitConverter.ToUInt32(array, 0);
			}

			protected bool bFilePathInitialized;

			protected string FilePath;

			protected FileStream FileStrm;

			protected uint Index_Set;

			protected uint Index_End;

			protected uint Index_Count;

			protected uint Search_Index_Set;

			protected uint Search_Index_End;

			protected SearchIndex.CZ_INDEX_INFO Search_Set;

			protected SearchIndex.CZ_INDEX_INFO Search_Mid;

			protected SearchIndex.CZ_INDEX_INFO Search_End;
		}
	}
}
