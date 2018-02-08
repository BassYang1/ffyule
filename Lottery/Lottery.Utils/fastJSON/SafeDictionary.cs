using System;
using System.Collections.Generic;

namespace Lottery.Utils.fastJSON
{
	public class SafeDictionary<TKey, TValue>
	{
		public bool ContainsKey(TKey key)
		{
			return this._Dictionary.ContainsKey(key);
		}

		public TValue this[TKey key]
		{
			get
			{
				return this._Dictionary[key];
			}
		}

		public void Add(TKey key, TValue value)
		{
			lock (this._Padlock)
			{
				this._Dictionary.Add(key, value);
			}
		}

		private readonly object _Padlock = new object();

		private readonly Dictionary<TKey, TValue> _Dictionary = new Dictionary<TKey, TValue>();
	}
}
