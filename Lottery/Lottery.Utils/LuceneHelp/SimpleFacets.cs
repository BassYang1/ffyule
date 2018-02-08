using System;
using System.Collections.Generic;
using Lucene.Net.Search;
using Lucene.Net.Util;

namespace Lottery.Utils.LuceneHelp
{
	public class SimpleFacets
	{
		public static void Facet(BooleanQuery bq, IndexSearcher s, string field, Dictionary<string, int> dics)
		{
			StringIndex stringIndex = FieldCache_Fields.DEFAULT.GetStringIndex(s.GetIndexReader(), field);
			int[] array = new int[stringIndex.lookup.Length];
			SimpleFacets.FacetCollector results = new SimpleFacets.FacetCollector(array, stringIndex);
			s.Search(bq, results);
			SimpleFacets.DictionaryEntryQueue dictionaryEntryQueue = new SimpleFacets.DictionaryEntryQueue(stringIndex.lookup.Length);
			for (int i = 1; i < stringIndex.lookup.Length; i++)
			{
				if (array[i] > 0 && stringIndex.lookup[i] != null && stringIndex.lookup[i] != "0")
				{
					dictionaryEntryQueue.Insert(new SimpleFacets.FacetEntry(stringIndex.lookup[i], -array[i]));
				}
			}
			int num = dictionaryEntryQueue.Size();
			for (int j = num - 1; j >= 0; j--)
			{
				SimpleFacets.FacetEntry facetEntry = dictionaryEntryQueue.Pop() as SimpleFacets.FacetEntry;
				dics.Add(facetEntry.Value, -facetEntry.Count);
			}
		}

		public static Dictionary<string, int> Facet(Query query, IndexSearcher s, string field)
		{
			StringIndex stringIndex = FieldCache_Fields.DEFAULT.GetStringIndex(s.GetIndexReader(), field);
			int[] array = new int[stringIndex.lookup.Length];
			SimpleFacets.FacetCollector results = new SimpleFacets.FacetCollector(array, stringIndex);
			s.Search(query, results);
			SimpleFacets.DictionaryEntryQueue dictionaryEntryQueue = new SimpleFacets.DictionaryEntryQueue(stringIndex.lookup.Length);
			for (int i = 1; i < stringIndex.lookup.Length; i++)
			{
				if (array[i] > 0 && stringIndex.lookup[i] != null && stringIndex.lookup[i] != "0")
				{
					dictionaryEntryQueue.Insert(new SimpleFacets.FacetEntry(stringIndex.lookup[i], -array[i]));
				}
			}
			int num = dictionaryEntryQueue.Size();
			Dictionary<string, int> dictionary = new Dictionary<string, int>();
			for (int j = num - 1; j >= 0; j--)
			{
				SimpleFacets.FacetEntry facetEntry = dictionaryEntryQueue.Pop() as SimpleFacets.FacetEntry;
				dictionary.Add(facetEntry.Value, -facetEntry.Count);
			}
			return dictionary;
		}

		private sealed class DictionaryEntryQueue : PriorityQueue
		{
			internal DictionaryEntryQueue(int size)
			{
				base.Initialize(size);
			}

			public override bool LessThan(object a, object b)
			{
				SimpleFacets.FacetEntry facetEntry = (SimpleFacets.FacetEntry)a;
				SimpleFacets.FacetEntry facetEntry2 = (SimpleFacets.FacetEntry)b;
				return facetEntry.Count < facetEntry2.Count;
			}
		}

		private class FacetEntry
		{
			public FacetEntry(string v, int c)
			{
				this.value = v;
				this.count = c;
			}

			public int Count
			{
				get
				{
					return this.count;
				}
				set
				{
					this.count = value;
				}
			}

			public string Value
			{
				get
				{
					return this.value;
				}
				set
				{
					this.value = value;
				}
			}

			private int count;

			private string value;
		}

		private class FacetCollector : HitCollector
		{
			public FacetCollector(int[] c, StringIndex s)
			{
				this.counter = c;
				this.si = s;
			}

			public override void Collect(int doc, float score)
			{
				this.counter[this.si.order[doc]]++;
			}

			private int[] counter;

			private StringIndex si;
		}
	}
}
