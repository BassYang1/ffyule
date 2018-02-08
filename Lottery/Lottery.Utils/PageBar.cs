using System;
using System.Collections.Generic;
using System.Text;

namespace Lottery.Utils
{
	public static class PageBar
	{
		private static string getbar1(string stype, int stepNum, int pageRoot, int pageFoot, int pageCount, int totalCount, int pageSize, int currentPage, string Http1, string HttpM, string HttpN, int limitPage)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("<div class='p_btns'>");
			if (totalCount > pageSize)
			{
				if (currentPage != 1)
				{
					stringBuilder.Append("<a target='_self' href='" + PageBar.GetPageUrl(1, Http1, HttpM, HttpN, limitPage) + "'>首页</a>");
					stringBuilder.Append("<a target='_self' href='" + PageBar.GetPageUrl(currentPage - 1, Http1, HttpM, HttpN, limitPage) + "'>上一页</a>");
				}
				else
				{
					stringBuilder.Append("<a class='disabled'>首页</a>");
					stringBuilder.Append("<a class='disabled'>上一页</a>");
				}
				if (stepNum > 0)
				{
					for (int i = pageRoot; i <= pageFoot; i++)
					{
						if (i == currentPage)
						{
							stringBuilder.Append("<span class='active'>" + i.ToString() + "</span>");
						}
						else
						{
							stringBuilder.Append(string.Concat(new string[]
							{
								"<a target='_self' href='",
								PageBar.GetPageUrl(i, Http1, HttpM, HttpN, limitPage),
								"'>",
								i.ToString(),
								"</a>"
							}));
						}
						if (i == pageCount)
						{
							break;
						}
					}
				}
				if (currentPage != pageCount)
				{
					stringBuilder.Append("<a target='_self' href='" + PageBar.GetPageUrl(currentPage + 1, Http1, HttpM, HttpN, limitPage) + "'>下一页</a>");
					stringBuilder.Append("<a target='_self' href='" + PageBar.GetPageUrl(pageCount, Http1, HttpM, HttpN, limitPage) + "'>末页</a>");
				}
				else
				{
					stringBuilder.Append("<a class='disabled'>下一页</a>");
					stringBuilder.Append("<a class='disabled'>末页</a>");
				}
			}
			stringBuilder.Append("</div>");
			return stringBuilder.ToString();
		}

		private static string getbar2(string stype, int stepNum, int pageRoot, int pageFoot, int pageCount, int totalCount, int pageSize, int currentPage, string Http1, string HttpM, string HttpN, int limitPage)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("<div class='p_btns'>");
			stringBuilder.Append(string.Concat(new string[]
			{
				"<span class='total_count'>共",
				totalCount.ToString(),
				"条记录/",
				pageCount.ToString(),
				"页&nbsp;</span>"
			}));
			if (totalCount > pageSize)
			{
				if (currentPage != 1)
				{
					stringBuilder.Append("<a target='_self' href='" + PageBar.GetPageUrl(1, Http1, HttpM, HttpN, limitPage) + "'>首页</a>");
					stringBuilder.Append("<a target='_self' href='" + PageBar.GetPageUrl(currentPage - 1, Http1, HttpM, HttpN, limitPage) + "'>上一页</a>");
				}
				else
				{
					stringBuilder.Append("<a class='disabled'>首页</a>");
					stringBuilder.Append("<a class='disabled'>上一页</a>");
				}
				if (stepNum > 0)
				{
					for (int i = pageRoot; i <= pageFoot; i++)
					{
						if (i == currentPage)
						{
							stringBuilder.Append("<span class='active'>" + i.ToString() + "</span>");
						}
						else
						{
							stringBuilder.Append(string.Concat(new string[]
							{
								"<a target='_self' href='",
								PageBar.GetPageUrl(i, Http1, HttpM, HttpN, limitPage),
								"'>",
								i.ToString(),
								"</a>"
							}));
						}
						if (i == pageCount)
						{
							break;
						}
					}
				}
				if (currentPage != pageCount)
				{
					stringBuilder.Append("<a target='_self' href='" + PageBar.GetPageUrl(currentPage + 1, Http1, HttpM, HttpN, limitPage) + "'>下一页</a>");
					stringBuilder.Append("<a target='_self' href='" + PageBar.GetPageUrl(pageCount, Http1, HttpM, HttpN, limitPage) + "'>末页</a>");
				}
				else
				{
					stringBuilder.Append("<a class='disabled'>下一页</a>");
					stringBuilder.Append("<a class='disabled'>末页</a>");
				}
			}
			stringBuilder.Append("</div>");
			return stringBuilder.ToString();
		}

		private static string getbar3(string stype, int stepNum, int pageRoot, int pageFoot, int pageCount, int totalCount, int pageSize, int currentPage, string Http1, string HttpM, string HttpN, int limitPage)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(string.Concat(new object[]
			{
				"<div class='page-info'>第",
				currentPage,
				"页/共",
				pageCount,
				"页 每页",
				pageSize,
				"条记录 共",
				totalCount.ToString(),
				"条记录</div>"
			}));
			stringBuilder.Append("<div class='pages'>");
			if (currentPage != 1)
			{
				stringBuilder.Append("<a target='_self' href='" + PageBar.GetPageUrl(1, Http1, HttpM, HttpN, limitPage) + "'>首页</a>");
				stringBuilder.Append("<a target='_self' href='" + PageBar.GetPageUrl(currentPage - 1, Http1, HttpM, HttpN, limitPage) + "'>上一页</a>");
			}
			else
			{
				stringBuilder.Append("<span>首页</span>");
				stringBuilder.Append("<span>上一页</span>");
			}
			if (pageRoot > 1)
			{
				stringBuilder.Append("<a target='_self' href='" + PageBar.GetPageUrl(1, Http1, HttpM, HttpN, limitPage) + "'>1..</a>");
			}
			if (stepNum > 0)
			{
				for (int i = pageRoot; i <= pageFoot; i++)
				{
					if (i == currentPage)
					{
						stringBuilder.Append("<span class='active'>" + i.ToString() + "</span>");
					}
					else
					{
						stringBuilder.Append(string.Concat(new string[]
						{
							"<a target='_self' href='",
							PageBar.GetPageUrl(i, Http1, HttpM, HttpN, limitPage),
							"'>",
							i.ToString(),
							"</a>"
						}));
					}
					if (i == pageCount)
					{
						break;
					}
				}
			}
			if (pageFoot < pageCount)
			{
				stringBuilder.Append(string.Concat(new object[]
				{
					"<a target='_self' href='",
					PageBar.GetPageUrl(pageCount, Http1, HttpM, HttpN, limitPage),
					"'>..",
					pageCount,
					"</a>"
				}));
			}
			if (currentPage != pageCount)
			{
				stringBuilder.Append("<a target='_self' href='" + PageBar.GetPageUrl(currentPage + 1, Http1, HttpM, HttpN, limitPage) + "'>下一页</a>");
				stringBuilder.Append("<a target='_self' href='" + PageBar.GetPageUrl(pageCount, Http1, HttpM, HttpN, limitPage) + "'>末页</a>");
			}
			else
			{
				stringBuilder.Append("<span>下一页</span>");
				stringBuilder.Append("<span>末页</span>");
			}
			stringBuilder.Append("</div>");
			return stringBuilder.ToString();
		}

		private static string getbar4(string stype, int stepNum, int pageRoot, int pageFoot, int pageCount, int countNum, int pageSize, int currentPage, string Http1, string HttpM, string HttpN, int limitPage, string language)
		{
			Dictionary<string, object> entity = new LanguageHelp().GetEntity(language);
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("<div class='p_btns'>");
			if (countNum > pageSize)
			{
				stringBuilder.Append("<span class='total_count'>" + ((string)entity["page_totalinfo"]).Replace("{totalcount}", countNum.ToString()).Replace("{currentpage}", currentPage.ToString()).Replace("{totalpage}", pageCount.ToString()) + "</span>");
				if (currentPage != 1)
				{
					stringBuilder.Append(string.Concat(new string[]
					{
						"<a target='_self' href='",
						PageBar.GetPageUrl(1, Http1, HttpM, HttpN, limitPage),
						"'>",
						(string)entity["page_first"],
						"</a>"
					}));
					stringBuilder.Append(string.Concat(new string[]
					{
						"<a target='_self' href='",
						PageBar.GetPageUrl(currentPage - 1, Http1, HttpM, HttpN, limitPage),
						"'>",
						(string)entity["page_prev"],
						"</a>"
					}));
				}
				else
				{
					stringBuilder.Append("<a class='disabled'>" + (string)entity["page_first"] + "</a>");
					stringBuilder.Append("<a class='disabled'>" + (string)entity["page_prev"] + "</a>");
				}
				if (pageRoot > 1)
				{
					stringBuilder.Append("<a target='_self' href='" + PageBar.GetPageUrl(1, Http1, HttpM, HttpN, limitPage) + "'>1..</a>");
				}
				if (stepNum > 0)
				{
					for (int i = pageRoot; i <= pageFoot; i++)
					{
						if (i == currentPage)
						{
							stringBuilder.Append("<span class='active'>" + i.ToString() + "</span>");
						}
						else
						{
							stringBuilder.Append(string.Concat(new string[]
							{
								"<a target='_self' href='",
								PageBar.GetPageUrl(i, Http1, HttpM, HttpN, limitPage),
								"'>",
								i.ToString(),
								"</a>"
							}));
						}
						if (i == pageCount)
						{
							break;
						}
					}
				}
				if (pageFoot < pageCount)
				{
					stringBuilder.Append(string.Concat(new object[]
					{
						"<a target='_self' href='",
						PageBar.GetPageUrl(pageCount, Http1, HttpM, HttpN, limitPage),
						"'>..",
						pageCount,
						"</a>"
					}));
				}
				if (currentPage != pageCount)
				{
					stringBuilder.Append(string.Concat(new string[]
					{
						"<a target='_self' href='",
						PageBar.GetPageUrl(currentPage + 1, Http1, HttpM, HttpN, limitPage),
						"'>",
						(string)entity["page_next"],
						"</a>"
					}));
					stringBuilder.Append(string.Concat(new string[]
					{
						"<a target='_self' href='",
						PageBar.GetPageUrl(pageCount, Http1, HttpM, HttpN, limitPage),
						"'>",
						(string)entity["page_last"],
						"</a>"
					}));
				}
				else
				{
					stringBuilder.Append("<a class='disabled'>" + (string)entity["page_next"] + "</a>");
					stringBuilder.Append("<a class='disabled'>" + (string)entity["page_last"] + "</a>");
				}
			}
			stringBuilder.Append("</div>");
			return stringBuilder.ToString();
		}

		private static string getbar6(string stype, int stepNum, int pageRoot, int pageFoot, int pageCount, int totalCount, int pageSize, int currentPage, string Http1, string HttpM, string HttpN, int limitPage)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("<div class='pages'>");
			if (currentPage != 1)
			{
				stringBuilder.Append("<a target='_self' href='" + PageBar.GetPageUrl(currentPage - 1, Http1, HttpM, HttpN, limitPage) + "'>上一页</a>");
			}
			else
			{
				stringBuilder.Append("<span>上一页</span>");
			}
			if (currentPage != pageCount)
			{
				stringBuilder.Append("<a target='_self' href='" + PageBar.GetPageUrl(currentPage + 1, Http1, HttpM, HttpN, limitPage) + "'>下一页</a>");
			}
			else
			{
				stringBuilder.Append("<span>下一页</span>");
			}
			stringBuilder.Append("</div>");
			return stringBuilder.ToString();
		}

		private static string getbarWebApp(string stype, int stepNum, int pageRoot, int pageFoot, int pageCount, int totalCount, int pageSize, int currentPage, string Http1, string HttpM, string HttpN, int limitPage)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("<div class='pages'>");
			if (currentPage != 1)
			{
				stringBuilder.Append("<a target='_self' class='first' href='" + PageBar.GetPageUrl(1, Http1, HttpM, HttpN, limitPage) + "'><i class='icon icon-first'></i></a>");
				stringBuilder.Append("<a target='_self' class='prev' href='" + PageBar.GetPageUrl(currentPage - 1, Http1, HttpM, HttpN, limitPage) + "'><i class='icon icon-prev'></i></a>");
			}
			else
			{
				stringBuilder.Append("<a href='#' class='first'><i class='icon icon-first'></i></a>");
				stringBuilder.Append("<a href='#' class='prev'><i class='icon icon-prev'></i></a>");
			}
			if (pageRoot > 1)
			{
				stringBuilder.Append("<a target='_self' href='" + PageBar.GetPageUrl(1, Http1, HttpM, HttpN, limitPage) + "'>1..</a>");
			}
			if (stepNum > 0)
			{
				for (int i = pageRoot; i <= pageFoot; i++)
				{
					if (i == currentPage)
					{
						stringBuilder.Append("<a href='#' class='page current'>" + i.ToString() + "</a>");
					}
					else
					{
						stringBuilder.Append(string.Concat(new string[]
						{
							"<a target='_self' class='page' href='",
							PageBar.GetPageUrl(i, Http1, HttpM, HttpN, limitPage),
							"'>",
							i.ToString(),
							"</a>"
						}));
					}
					if (i == pageCount)
					{
						break;
					}
				}
			}
			if (pageFoot < pageCount)
			{
				stringBuilder.Append(string.Concat(new object[]
				{
					"<a target='_self' href='",
					PageBar.GetPageUrl(pageCount, Http1, HttpM, HttpN, limitPage),
					"'>..",
					pageCount,
					"</a>"
				}));
			}
			if (currentPage != pageCount)
			{
				stringBuilder.Append("<a target='_self' class='last' href='" + PageBar.GetPageUrl(currentPage + 1, Http1, HttpM, HttpN, limitPage) + "'><i class='icon icon-next'></i></a>");
				stringBuilder.Append("<a target='_self' class='next' href='" + PageBar.GetPageUrl(pageCount, Http1, HttpM, HttpN, limitPage) + "'><i class='icon icon-last'></i></a>");
			}
			else
			{
				stringBuilder.Append("<a href='#' class='last'><i class='icon icon-last'></i></a>");
				stringBuilder.Append("<a href='#' class='next'><i class='icon icon-next'></i></a>");
			}
			stringBuilder.Append("</div>");
			stringBuilder.Append(string.Concat(new object[]
			{
				"<div class='page-info'>第",
				currentPage,
				"页/共",
				pageCount,
				"页 每页",
				pageSize,
				"条记录 共",
				totalCount.ToString(),
				"条记录</div>"
			}));
			return stringBuilder.ToString();
		}

		private static string getbarFFApp(string stype, int stepNum, int pageRoot, int pageFoot, int pageCount, int totalCount, int pageSize, int currentPage, string Http1, string HttpM, string HttpN, int limitPage)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("<div class='pages'>");
			if (currentPage != 1)
			{
				stringBuilder.Append("<a target='_self' class='first' href='" + PageBar.GetPageUrl(1, Http1, HttpM, HttpN, limitPage) + "'>首页</a>");
				stringBuilder.Append("<a target='_self' class='prev' href='" + PageBar.GetPageUrl(currentPage - 1, Http1, HttpM, HttpN, limitPage) + "'>上页</a>");
			}
			else
			{
				stringBuilder.Append("<a href='#' class='first'>首页</a>");
				stringBuilder.Append("<a href='#' class='prev'>上页</a>");
			}
			if (pageRoot > 1)
			{
				stringBuilder.Append("<a target='_self' href='" + PageBar.GetPageUrl(1, Http1, HttpM, HttpN, limitPage) + "'>1..</a>");
			}
			if (stepNum > 0)
			{
				for (int i = pageRoot; i <= pageFoot; i++)
				{
					if (i == currentPage)
					{
						stringBuilder.Append("<a href='#' class='page current'>" + i.ToString() + "</a>");
					}
					else
					{
						stringBuilder.Append(string.Concat(new string[]
						{
							"<a target='_self' class='page' href='",
							PageBar.GetPageUrl(i, Http1, HttpM, HttpN, limitPage),
							"'>",
							i.ToString(),
							"</a>"
						}));
					}
					if (i == pageCount)
					{
						break;
					}
				}
			}
			if (pageFoot < pageCount)
			{
				stringBuilder.Append(string.Concat(new object[]
				{
					"<a target='_self' href='",
					PageBar.GetPageUrl(pageCount, Http1, HttpM, HttpN, limitPage),
					"'>..",
					pageCount,
					"</a>"
				}));
			}
			if (currentPage != pageCount)
			{
				stringBuilder.Append("<a target='_self' class='last' href='" + PageBar.GetPageUrl(currentPage + 1, Http1, HttpM, HttpN, limitPage) + "'>下页</a>");
				stringBuilder.Append("<a target='_self' class='next' href='" + PageBar.GetPageUrl(pageCount, Http1, HttpM, HttpN, limitPage) + "'>末页</a>");
			}
			else
			{
				stringBuilder.Append("<a href='#' class='last'>下页</a>");
				stringBuilder.Append("<a href='#' class='next'>末页</a>");
			}
			stringBuilder.Append("</div>");
			stringBuilder.Append(string.Concat(new object[]
			{
				"<div class='page-info'>第",
				currentPage,
				"页/共",
				pageCount,
				"页 每页",
				pageSize,
				"条记录 共",
				totalCount.ToString(),
				"条记录</div>"
			}));
			return stringBuilder.ToString();
		}

		private static string getbarHanGuo(string stype, int stepNum, int pageRoot, int pageFoot, int pageCount, int totalCount, int pageSize, int currentPage, string Http1, string HttpM, string HttpN, int limitPage)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("<ul><li>");
			if (currentPage != 1)
			{
				stringBuilder.Append("<span><a target='_self' class='first' href='" + PageBar.GetPageUrl(1, Http1, HttpM, HttpN, limitPage) + "'><img src='img/arrow_01.gif' border='0'></a></span>&nbsp;");
				stringBuilder.Append("<span><a target='_self' class='prev' href='" + PageBar.GetPageUrl(currentPage - 1, Http1, HttpM, HttpN, limitPage) + "'><img src='img/arrow_02.gif' border='0'></a></span>&nbsp;");
			}
			else
			{
				stringBuilder.Append("<span><a href='#' class='first'><img src='img/arrow_01.gif' border='0'></a></span>&nbsp;");
				stringBuilder.Append("<span><a href='#' class='prev'><img src='img/arrow_02.gif' border='0'></a></span>&nbsp;");
			}
			if (pageRoot > 1)
			{
				stringBuilder.Append("<a target='_self' href='" + PageBar.GetPageUrl(1, Http1, HttpM, HttpN, limitPage) + "'>1..</a></span>&nbsp;");
			}
			if (stepNum > 0)
			{
				for (int i = pageRoot; i <= pageFoot; i++)
				{
					if (i == currentPage)
					{
						stringBuilder.Append("<span style='font-weight:bold;'><a href='#' class='now'>" + i.ToString() + "</a></span>&nbsp;");
					}
					else
					{
						stringBuilder.Append(string.Concat(new string[]
						{
							"<span style='font-weight:bold;'><a target='_self' class='page' href='",
							PageBar.GetPageUrl(i, Http1, HttpM, HttpN, limitPage),
							"'>",
							i.ToString(),
							"</a></span>&nbsp;"
						}));
					}
					if (i == pageCount)
					{
						break;
					}
				}
			}
			if (currentPage != pageCount)
			{
				stringBuilder.Append("<span><a target='_self' class='last' href='" + PageBar.GetPageUrl(currentPage + 1, Http1, HttpM, HttpN, limitPage) + "'><img src='img/arrow_03.gif' border='0'></a></span>&nbsp;");
				stringBuilder.Append("<span><a target='_self' class='next' href='" + PageBar.GetPageUrl(pageCount, Http1, HttpM, HttpN, limitPage) + "'><img src='img/arrow_04.gif' border='0'></a></span>&nbsp;");
			}
			else
			{
				stringBuilder.Append("<span><a href='#' class='last'><img src='img/arrow_03.gif' border='0'></a></span>&nbsp;");
				stringBuilder.Append("<span><a href='#' class='next'><img src='img/arrow_04.gif' border='0'></a></span>&nbsp;");
			}
			stringBuilder.Append("</li></ul>");
			return stringBuilder.ToString();
		}

		private static string getbarXinJiaPo(string stype, int stepNum, int pageRoot, int pageFoot, int pageCount, int totalCount, int pageSize, int currentPage, string Http1, string HttpM, string HttpN, int limitPage)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("<div class='view view-aktualnosci view-id-aktualnosci view-display-id-page_1 view-dom-id-1'>");
			if (currentPage != 1)
			{
				stringBuilder.Append("<div class='navigation'><div class='next'><a target='_self' class='active' href='" + PageBar.GetPageUrl(currentPage - 1, Http1, HttpM, HttpN, limitPage) + "'>next &gt;</a></div></div>");
			}
			else
			{
				stringBuilder.Append("<div class='navigation'><div class='next'><a href='#' class='active'>next &gt;</a></div></div>");
			}
			stringBuilder.Append("<div class='navigation_li'><div class='item-list'><ul class='pager'>");
			if (stepNum > 0)
			{
				for (int i = pageRoot; i <= pageFoot; i++)
				{
					if (i == currentPage)
					{
						stringBuilder.Append("<li class='pager-current'>" + i.ToString() + "</li>");
					}
					else
					{
						stringBuilder.Append(string.Concat(new string[]
						{
							"<li class='pager-item'><a target='_self' class='active' href='",
							PageBar.GetPageUrl(i, Http1, HttpM, HttpN, limitPage),
							"'>",
							i.ToString(),
							"</a></li>"
						}));
					}
					if (i == pageCount)
					{
						break;
					}
				}
			}
			stringBuilder.Append("</ul></div></div>");
			stringBuilder.Append("</div>");
			return stringBuilder.ToString();
		}

		private static string getbarDongjing(string stype, int stepNum, int pageRoot, int pageFoot, int pageCount, int totalCount, int pageSize, int currentPage, string Http1, string HttpM, string HttpN, int limitPage)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("<div class='page'>");
			if (pageRoot > 1)
			{
				stringBuilder.Append("<a target='_self' href='" + PageBar.GetPageUrl(1, Http1, HttpM, HttpN, limitPage) + "'>1..</a>");
			}
			if (stepNum > 0)
			{
				for (int i = pageRoot; i <= pageFoot; i++)
				{
					if (i == currentPage)
					{
						stringBuilder.Append("<a href='#' class='pageHover'>" + i.ToString() + "</a>");
					}
					else
					{
						stringBuilder.Append(string.Concat(new string[]
						{
							"<a target='_self' href='",
							PageBar.GetPageUrl(i, Http1, HttpM, HttpN, limitPage),
							"'>",
							i.ToString(),
							"</a>"
						}));
					}
					if (i == pageCount)
					{
						break;
					}
				}
			}
			stringBuilder.Append("</div>");
			return stringBuilder.ToString();
		}

		private static string getbarNiuYue(string stype, int stepNum, int pageRoot, int pageFoot, int pageCount, int totalCount, int pageSize, int currentPage, string Http1, string HttpM, string HttpN, int limitPage)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("<ul><li>");
			if (currentPage != 1)
			{
				stringBuilder.Append("<a href='" + PageBar.GetPageUrl(1, Http1, HttpM, HttpN, limitPage) + "' class='prev'>Prev</span>");
			}
			else
			{
				stringBuilder.Append("<span class='prev'>Prev</span>");
			}
			if (pageRoot > 1)
			{
				stringBuilder.Append("<span><a target='_self' href='" + PageBar.GetPageUrl(1, Http1, HttpM, HttpN, limitPage) + "'>1..</a></span>");
			}
			if (stepNum > 0)
			{
				for (int i = pageRoot; i <= pageFoot; i++)
				{
					if (i == currentPage)
					{
						stringBuilder.Append("<span class='current'>" + i + "</span>");
					}
					else
					{
						stringBuilder.Append(string.Concat(new string[]
						{
							"<a href='",
							PageBar.GetPageUrl(i, Http1, HttpM, HttpN, limitPage),
							"'>",
							i.ToString(),
							"</a>"
						}));
					}
					if (i == pageCount)
					{
						break;
					}
				}
			}
			if (currentPage != pageCount)
			{
				stringBuilder.Append("<a href='" + PageBar.GetPageUrl(1, Http1, HttpM, HttpN, limitPage) + "' class='next'>Next</span>");
			}
			else
			{
				stringBuilder.Append("<span class='next'>Next</span>");
			}
			stringBuilder.Append("</li></ul>");
			return stringBuilder.ToString();
		}

		public static string GetPageBar(int mode, string stype, int stepNum, int totalCount, int pageSize, int currentPage, string Http1, string HttpM, string HttpN, int limitPage)
		{
			string result = "";
			int num = (totalCount % pageSize == 0) ? (totalCount / pageSize) : (totalCount / pageSize + 1);
			currentPage = ((currentPage > num) ? num : currentPage);
			currentPage = ((currentPage < 1) ? 1 : currentPage);
			int num2 = stepNum * 2;
			num = ((num == 0) ? 1 : num);
			int num3;
			int num4;
			if (num - num2 < 1)
			{
				num3 = 1;
				num4 = num;
			}
			else
			{
				num3 = ((currentPage - stepNum > 1) ? (currentPage - stepNum) : 1);
				num4 = ((num3 + num2 > num) ? num : (num3 + num2));
				num3 = ((num4 - num2 < num3) ? (num4 - num2) : num3);
			}
			if (mode <= 81)
			{
				switch (mode)
				{
				case 1:
					result = PageBar.getbar1(stype, stepNum, num3, num4, num, totalCount, pageSize, currentPage, Http1, HttpM, HttpN, limitPage);
					break;
				case 2:
					result = PageBar.getbar2(stype, stepNum, num3, num4, num, totalCount, pageSize, currentPage, Http1, HttpM, HttpN, limitPage);
					break;
				case 3:
					result = PageBar.getbar3(stype, stepNum, num3, num4, num, totalCount, pageSize, currentPage, Http1, HttpM, HttpN, limitPage);
					break;
				case 4:
					result = PageBar.getbar4(stype, stepNum, num3, num4, num, totalCount, pageSize, currentPage, Http1, HttpM, HttpN, limitPage, "cn");
					break;
				case 5:
					result = PageBar.getbar4(stype, stepNum, num3, num4, num, totalCount, pageSize, currentPage, Http1, HttpM, HttpN, limitPage, "en");
					break;
				case 6:
					result = PageBar.getbar6(stype, stepNum, num3, num4, num, totalCount, pageSize, currentPage, Http1, HttpM, HttpN, limitPage);
					break;
				default:
					switch (mode)
					{
					case 80:
						result = PageBar.getbarFFApp(stype, stepNum, num3, num4, num, totalCount, pageSize, currentPage, Http1, HttpM, HttpN, limitPage);
						break;
					case 81:
						result = PageBar.getbarFFApp(stype, stepNum, num3, num4, num, totalCount, pageSize, currentPage, Http1, HttpM, HttpN, limitPage);
						break;
					}
					break;
				}
			}
			else if (mode != 1004)
			{
				switch (mode)
				{
				case 1010:
					result = PageBar.getbarHanGuo(stype, stepNum, num3, num4, num, totalCount, pageSize, currentPage, Http1, HttpM, HttpN, limitPage);
					break;
				case 1011:
					break;
				case 1012:
					result = PageBar.getbarXinJiaPo(stype, stepNum, num3, num4, num, totalCount, pageSize, currentPage, Http1, HttpM, HttpN, limitPage);
					break;
				default:
					if (mode == 1016)
					{
						result = PageBar.getbarDongjing(stype, stepNum, num3, num4, num, totalCount, pageSize, currentPage, Http1, HttpM, HttpN, limitPage);
					}
					break;
				}
			}
			else
			{
				result = PageBar.getbarNiuYue(stype, stepNum, num3, num4, num, totalCount, pageSize, currentPage, Http1, HttpM, HttpN, limitPage);
			}
			return result;
		}

		public static string GetPageBar(int mode, string stype, int stepNum, int totalCount, int pageSize, int currentPage, string HttpN)
		{
			return PageBar.GetPageBar(mode, stype, stepNum, totalCount, pageSize, currentPage, HttpN, HttpN, HttpN, 0);
		}

		public static string GetPageUrl(int chkPage, string Http1, string HttpM, string HttpN, int limitPage)
		{
			string text = string.Empty;
			if (chkPage == 1)
			{
				text = Http1;
			}
			else
			{
				text = ((chkPage > limitPage || limitPage == 0) ? HttpN : HttpM);
			}
			return text.Replace("<#page#>", chkPage.ToString());
		}
	}
}
