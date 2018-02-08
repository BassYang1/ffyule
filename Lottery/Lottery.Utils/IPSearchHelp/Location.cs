using System;

namespace Lottery.Utils.IPSearchHelp
{
	public class Location
	{
		public Location(string _area)
		{
			if (_area.Contains("省") && _area.Contains("市"))
			{
				this._areatype = 0;
				this._captical = _area.Substring(0, _area.IndexOf('省'));
				this._city = _area.Substring(_area.IndexOf('省') + 1, _area.IndexOf('市') - _area.IndexOf('省') - 1);
			}
			if (this._areatype == 4)
			{
				for (int i = 0; i < this._capital1.Length; i++)
				{
					if (_area.StartsWith(this._capital1[i]))
					{
						this._areatype = 1;
						this._captical = this._capital1[i];
						if (_area.Length > this._capital1[i].Length + 1)
						{
							this._city = _area.Substring(this._capital1[i].Length + 1).Replace("区", "");
						}
					}
				}
			}
			if (this._areatype == 4)
			{
				for (int j = 0; j < this._capital2.Length; j++)
				{
					if (_area.StartsWith(this._capital2[j]))
					{
						this._areatype = 2;
						this._captical = this._capital2[j];
						if (_area.Length > this._capital2[j].Length)
						{
							this._city = _area.Substring(this._capital2[j].Length + 1).Replace("市", "");
						}
					}
				}
			}
			if (this._areatype == 4)
			{
				for (int k = 0; k < this._capital3.Length; k++)
				{
					if (_area.StartsWith(this._capital3[k]))
					{
						this._areatype = 3;
						this._captical = this._capital3[k];
						if (_area.Length > this._capital3[k].Length)
						{
							this._city = _area.Substring(this._capital3[k].Length);
						}
					}
				}
			}
		}

		public int AreaType
		{
			get
			{
				return this._areatype;
			}
			set
			{
				this._areatype = value;
			}
		}

		public string Captical
		{
			get
			{
				return this._captical;
			}
			set
			{
				this._captical = value;
			}
		}

		public string City
		{
			get
			{
				return this._city;
			}
			set
			{
				this._city = value;
			}
		}

		private string[] _capital0 = new string[]
		{
			"黑龙江",
			"吉林",
			"辽宁",
			"河北",
			"山西",
			"陕西",
			"山东",
			"青海",
			"甘肃",
			"宁夏",
			"河南",
			"江苏",
			"湖北",
			"浙江",
			"安徽",
			"福建",
			"江西",
			"湖南",
			"贵州",
			"四川",
			"广东",
			"云南",
			"海南"
		};

		private string[] _capital1 = new string[]
		{
			"北京",
			"上海",
			"天津",
			"重庆"
		};

		private string[] _capital2 = new string[]
		{
			"内蒙古",
			"新疆",
			"西藏",
			"宁夏",
			"广西"
		};

		private string[] _capital3 = new string[]
		{
			"香港",
			"澳门",
			"台湾"
		};

		private int _areatype = 4;

		private string _captical = "";

		private string _city = "";
	}
}
