using System;
using System.Collections.Generic;
using System.Web;
using Lucene.Net.Analysis.Standard;
using Lucene.Net.Index;
using Lucene.Net.QueryParsers;
using Lucene.Net.Search;

namespace Lottery.Utils.LuceneHelp
{
	public class SearchIndex
	{
		public static int GetCount(string type, string channelid, string classid, string year, string keywords, string groupname, out Dictionary<string, int> groupAggregate)
		{
			if (keywords.Length == 0)
			{
				keywords = "jUmBoT";
			}
			DateTime arg_15_0 = DateTime.Now;
			string[] array = type.Split(new char[]
			{
				','
			});
			int num = array.Length;
			IndexSearcher[] array2 = new IndexSearcher[num];
			for (int i = 0; i < num; i++)
			{
				array2[i] = new IndexSearcher(HttpContext.Current.Server.MapPath("~/data/index/" + array[i] + "/"));
			}
			MultiSearcher multiSearcher = new MultiSearcher(array2);
			BooleanQuery booleanQuery = new BooleanQuery();
			if (channelid != "0")
			{
				Term t = new Term("channelid", channelid);
				TermQuery query = new TermQuery(t);
				booleanQuery.Add(query, BooleanClause.Occur.MUST);
			}
			if (Validator.StrToInt(year, 0) > 1900)
			{
				Term t2 = new Term("year", year);
				TermQuery query2 = new TermQuery(t2);
				booleanQuery.Add(query2, BooleanClause.Occur.MUST);
			}
			string[] fields = new string[]
			{
				"title",
				"tags",
				"summary",
				"content",
				"fornull"
			};
			MultiFieldQueryParser multiFieldQueryParser = new MultiFieldQueryParser(fields, new StandardAnalyzer());
			Query query3 = multiFieldQueryParser.Parse(keywords);
			booleanQuery.Add(query3, BooleanClause.Occur.MUST);
			Hits hits = multiSearcher.Search(booleanQuery);
			if (num == 1)
			{
				groupAggregate = SimpleFacets.Facet(booleanQuery, array2[0], groupname);
			}
			else
			{
				groupAggregate = null;
			}
			return hits.Length();
		}

		public static List<SearchItem> Search(string type, string channelid, string classid, string year, string keywords, int pageLen, int pageNo, out int recCount, out double eventTime)
		{
			if (keywords.Length == 0)
			{
				keywords = "jUmBoT";
			}
			DateTime now = DateTime.Now;
			string[] array = type.Split(new char[]
			{
				','
			});
			int num = array.Length;
			IndexSearcher[] array2 = new IndexSearcher[num];
			for (int i = 0; i < num; i++)
			{
				array2[i] = new IndexSearcher(HttpContext.Current.Server.MapPath("~/data/index/" + array[i] + "/"));
			}
			MultiSearcher multiSearcher = new MultiSearcher(array2);
			BooleanQuery booleanQuery = new BooleanQuery();
			if (channelid != "0")
			{
				Term t = new Term("channelid", channelid);
				TermQuery query = new TermQuery(t);
				booleanQuery.Add(query, BooleanClause.Occur.MUST);
			}
			if (Validator.StrToInt(year, 0) > 1900)
			{
				Term t2 = new Term("year", year);
				TermQuery query2 = new TermQuery(t2);
				booleanQuery.Add(query2, BooleanClause.Occur.MUST);
			}
			string[] fields = new string[]
			{
				"title",
				"tags",
				"summary",
				"content",
				"fornull"
			};
			MultiFieldQueryParser multiFieldQueryParser = new MultiFieldQueryParser(fields, new StandardAnalyzer());
			Query query3 = multiFieldQueryParser.Parse(keywords);
			booleanQuery.Add(query3, BooleanClause.Occur.MUST);
			Sort sort = new Sort(new SortField(null, 1, true));
			Hits hits = multiSearcher.Search(booleanQuery, sort);
			List<SearchItem> list = new List<SearchItem>();
			recCount = hits.Length();
			if (recCount > 0)
			{
				int num2 = (pageNo - 1) * pageLen;
				while (num2 < recCount && list.Count < pageLen)
				{
					SearchItem searchItem = null;
					try
					{
						searchItem = new SearchItem();
						searchItem.Id = hits.Doc(num2).Get("id");
						searchItem.ChannelId = hits.Doc(num2).Get("channelid");
						searchItem.ClassId = hits.Doc(num2).Get("classid");
						searchItem.TableName = hits.Doc(num2).Get("tablename");
						searchItem.Img = hits.Doc(num2).Get("img");
						searchItem.Title = hits.Doc(num2).Get("title");
						searchItem.Summary = hits.Doc(num2).Get("summary");
						searchItem.Tags = hits.Doc(num2).Get("tags");
						searchItem.Url = hits.Doc(num2).Get("url");
						searchItem.AddDate = hits.Doc(num2).Get("adddate");
						searchItem.Year = hits.Doc(num2).Get("year");
					}
					catch (Exception ex)
					{
						Console.WriteLine(ex.Message);
					}
					finally
					{
						list.Add(searchItem);
						num2++;
					}
				}
				multiSearcher.Close();
				eventTime = (double)((float)Convert.ToInt16((DateTime.Now - now).TotalMilliseconds));
				return list;
			}
			eventTime = (double)((float)Convert.ToInt16((DateTime.Now - now).TotalMilliseconds));
			return null;
		}
	}
}
