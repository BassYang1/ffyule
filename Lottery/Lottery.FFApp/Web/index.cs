using System;
using System.Data;
using System.Web.UI;
using Lottery.DAL;

namespace Web
{
	public class index : Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if (base.Request.QueryString["id"] != null)
			{
				this.lotteryId = base.Request.QueryString["id"].ToString();
			}
			this.LName = LotteryUtils.LotteryTitle(Convert.ToInt32(this.lotteryId));
			this.LotteryLines = "";
			if (!base.IsPostBack)
			{
				int top = 50;
				if (base.Request["n"] != null)
				{
					top = Convert.ToInt32(base.Request["n"]);
				}
				DataTable listDataTable = new LotteryDataDAL().GetListDataTable(Convert.ToInt32(this.lotteryId), top);
				if (this.lotteryId.Substring(0, 1) == "1")
				{
					this.count = 10;
					int[,] array = new int[5, 10];
					int[,] array2 = new int[5, 10];
					int[,] array3 = new int[5, 10];
					int[,] array4 = new int[5, 10];
					int[,] array5 = new int[5, 10];
					string[,] array6 = new string[5, 10];
					for (int i = 0; i < listDataTable.Rows.Count; i++)
					{
						DataRow dataRow = listDataTable.Rows[i];
						string str = dataRow["Title"].ToString();
						string text = dataRow["Number"].ToString();
						string[] array7 = text.Split(new char[]
						{
							','
						});
						string text2 = "<tr>";
						text2 += "<td rowspan=\"2\" style=\"width:100px;\">期号</td>";
						text2 += "<td rowspan=\"2\" style=\"width:100px;\">开奖号码</td>";
						text2 += "<td colspan=\"10\">万位</td>";
						text2 += "<td colspan=\"10\">千位</td>";
						text2 += "<td colspan=\"10\">百位</td>";
						text2 += "<td colspan=\"10\">十位</td>";
						text2 += "<td colspan=\"10\">个位</td>";
						text2 += "</tr>";
						text2 += "<tr>";
						for (int j = 0; j < array7.Length; j++)
						{
							for (int k = 0; k < 10; k++)
							{
								object obj = text2;
								text2 = string.Concat(new object[]
								{
									obj,
									"<td>",
									k,
									"</td>"
								});
							}
						}
						text2 += "</tr>";
						this.LotteryHeadLines = text2;
						string text3 = "<tr>";
						text3 += "<td class=\"issue\">";
						text3 += str;
						text3 += "</td>";
						text3 += "<td align=\"center\" class=\"tdwth\">";
						text3 += text;
						text3 += "</td>";
						for (int j = 0; j < array7.Length; j++)
						{
							for (int l = 0; l <= 9; l++)
							{
								if (l == Convert.ToInt32(array7[j]))
								{
									array[j, l]++;
									array2[j, l] = -1;
									array4[j, l]++;
									if (array3[j, l] < array4[j, l])
									{
										array3[j, l] = array4[j, l];
									}
								}
								else
								{
									array4[j, l] = 0;
									array2[j, l]++;
									if (array5[j, l] < array2[j, l])
									{
										array5[j, l] = array2[j, l];
									}
								}
								if (l == Convert.ToInt32(array7[j]))
								{
									if (j % 2 == 0)
									{
										text3 = text3 + "<td class=\"charball td0\"><div class=\"ball01\">" + array7[j] + "</div></td>";
									}
									else
									{
										text3 = text3 + "<td class=\"charball td1\"><div class=\"ball01\">" + array7[j] + "</div></td>";
									}
									array2[j, l]++;
								}
								else if (j % 2 == 0)
								{
									object obj = text3;
									text3 = string.Concat(new object[]
									{
										obj,
										"<td class=\"wdh td0\"><div class=\"ball14\">",
										array2[j, l],
										"</div></td>"
									});
								}
								else
								{
									object obj = text3;
									text3 = string.Concat(new object[]
									{
										obj,
										"<td class=\"wdh td1\"><div class=\"ball14\">",
										array2[j, l],
										"</div></td>"
									});
								}
							}
						}
						text3 += "</tr>";
						this.LotteryLines += text3;
					}
					string text4 = "<tr>";
					text4 += "<td colspan=\"2\">";
					text4 += "当前最大连开";
					text4 += "</td>";
					for (int j = 0; j < 5; j++)
					{
						for (int l = 0; l <= 9; l++)
						{
							if (j % 2 == 0)
							{
								object obj = text4;
								text4 = string.Concat(new object[]
								{
									obj,
									"<td class=\"wdh td0\"><div class=\"ball14\">",
									array3[j, l],
									"</div></td>"
								});
							}
							else
							{
								object obj = text4;
								text4 = string.Concat(new object[]
								{
									obj,
									"<td class=\"wdh td1\"><div class=\"ball14\">",
									array3[j, l],
									"</div></td>"
								});
							}
						}
					}
					text4 += "</tr>";
					string text5 = "<tr>";
					text5 += "<td colspan=\"2\">";
					text5 += "当前最大遗漏";
					text5 += "</td>";
					for (int j = 0; j < 5; j++)
					{
						for (int l = 0; l <= 9; l++)
						{
							if (j % 2 == 0)
							{
								object obj = text5;
								text5 = string.Concat(new object[]
								{
									obj,
									"<td class=\"wdh td0\"><div class=\"ball14\">",
									array5[j, l],
									"</div></td>"
								});
							}
							else
							{
								object obj = text5;
								text5 = string.Concat(new object[]
								{
									obj,
									"<td class=\"wdh td1\"><div class=\"ball14\">",
									array5[j, l],
									"</div></td>"
								});
							}
						}
					}
					text5 += "</tr>";
					string text6 = "<tr>";
					text6 += "<td colspan=\"2\">";
					text6 += "当前出现次数";
					text6 += "</td>";
					for (int j = 0; j < 5; j++)
					{
						for (int l = 0; l <= 9; l++)
						{
							if (j % 2 == 0)
							{
								object obj = text6;
								text6 = string.Concat(new object[]
								{
									obj,
									"<td class=\"wdh td0\"><div class=\"ball14\">",
									array[j, l],
									"</div></td>"
								});
							}
							else
							{
								object obj = text6;
								text6 = string.Concat(new object[]
								{
									obj,
									"<td class=\"wdh td1\"><div class=\"ball14\">",
									array[j, l],
									"</div></td>"
								});
							}
						}
					}
					text6 += "</tr>";
					this.LotteryLines = this.LotteryLines + text6 + text4 + text5;
				}
				if (this.lotteryId.Substring(0, 1) == "2")
				{
					this.count = 11;
					int[,] array = new int[5, 11];
					int[,] array2 = new int[5, 11];
					int[,] array3 = new int[5, 11];
					int[,] array4 = new int[5, 11];
					int[,] array5 = new int[5, 11];
					string[,] array6 = new string[5, 11];
					for (int i = 0; i < listDataTable.Rows.Count; i++)
					{
						DataRow dataRow = listDataTable.Rows[i];
						string str = dataRow["Title"].ToString();
						string text = dataRow["Number"].ToString();
						string[] array7 = text.Split(new char[]
						{
							','
						});
						string text2 = "<tr>";
						text2 += "<td rowspan=\"2\" style=\"width:100px;\">期号</td>";
						text2 += "<td rowspan=\"2\" style=\"width:100px;\">开奖号码</td>";
						text2 += "<td colspan=\"11\">万位</td>";
						text2 += "<td colspan=\"11\">千位</td>";
						text2 += "<td colspan=\"11\">百位</td>";
						text2 += "<td colspan=\"11\">十位</td>";
						text2 += "<td colspan=\"11\">个位</td>";
						text2 += "</tr>";
						text2 += "<tr>";
						for (int j = 0; j < array7.Length; j++)
						{
							for (int k = 1; k <= 11; k++)
							{
								object obj = text2;
								text2 = string.Concat(new object[]
								{
									obj,
									"<td>",
									k,
									"</td>"
								});
							}
						}
						text2 += "</tr>";
						this.LotteryHeadLines = text2;
						string text3 = "<tr>";
						text3 += "<td class=\"issue\">";
						text3 += str;
						text3 += "</td>";
						text3 += "<td align=\"center\" class=\"tdwth\">";
						text3 += text;
						text3 += "</td>";
						for (int j = 0; j < array7.Length; j++)
						{
							for (int l = 0; l <= 10; l++)
							{
								if (l + 1 == Convert.ToInt32(array7[j]))
								{
									array[j, l]++;
									array2[j, l] = -1;
									array4[j, l]++;
									if (array3[j, l] < array4[j, l])
									{
										array3[j, l] = array4[j, l];
									}
								}
								else
								{
									array4[j, l] = 1;
									array2[j, l]++;
									if (array5[j, l] < array2[j, l])
									{
										array5[j, l] = array2[j, l];
									}
								}
								if (l + 1 == Convert.ToInt32(array7[j]))
								{
									if (j % 2 == 0)
									{
										text3 = text3 + "<td class=\"charball td0\"><div class=\"ball01\">" + array7[j] + "</div></td>";
									}
									else
									{
										text3 = text3 + "<td class=\"charball td1\"><div class=\"ball01\">" + array7[j] + "</div></td>";
									}
									array2[j, l]++;
								}
								else if (j % 2 == 0)
								{
									object obj = text3;
									text3 = string.Concat(new object[]
									{
										obj,
										"<td class=\"wdh td0\"><div class=\"ball14\">",
										array2[j, l],
										"</div></td>"
									});
								}
								else
								{
									object obj = text3;
									text3 = string.Concat(new object[]
									{
										obj,
										"<td class=\"wdh td1\"><div class=\"ball14\">",
										array2[j, l],
										"</div></td>"
									});
								}
							}
						}
						text3 += "</tr>";
						this.LotteryLines += text3;
					}
					string text4 = "<tr>";
					text4 += "<td colspan=\"2\">";
					text4 += "当前最大连开";
					text4 += "</td>";
					for (int j = 0; j < 5; j++)
					{
						for (int l = 0; l <= 10; l++)
						{
							if (j % 2 == 0)
							{
								object obj = text4;
								text4 = string.Concat(new object[]
								{
									obj,
									"<td class=\"wdh td0\"><div class=\"ball14\">",
									array3[j, l],
									"</div></td>"
								});
							}
							else
							{
								object obj = text4;
								text4 = string.Concat(new object[]
								{
									obj,
									"<td class=\"wdh td1\"><div class=\"ball14\">",
									array3[j, l],
									"</div></td>"
								});
							}
						}
					}
					text4 += "</tr>";
					string text5 = "<tr>";
					text5 += "<td colspan=\"2\">";
					text5 += "当前最大遗漏";
					text5 += "</td>";
					for (int j = 0; j < 5; j++)
					{
						for (int l = 0; l <= 10; l++)
						{
							if (j % 2 == 0)
							{
								object obj = text5;
								text5 = string.Concat(new object[]
								{
									obj,
									"<td class=\"wdh td0\"><div class=\"ball14\">",
									array5[j, l],
									"</div></td>"
								});
							}
							else
							{
								object obj = text5;
								text5 = string.Concat(new object[]
								{
									obj,
									"<td class=\"wdh td1\"><div class=\"ball14\">",
									array5[j, l],
									"</div></td>"
								});
							}
						}
					}
					text5 += "</tr>";
					string text6 = "<tr>";
					text6 += "<td colspan=\"2\">";
					text6 += "当前出现次数";
					text6 += "</td>";
					for (int j = 0; j < 5; j++)
					{
						for (int l = 0; l <= 10; l++)
						{
							if (j % 2 == 0)
							{
								object obj = text6;
								text6 = string.Concat(new object[]
								{
									obj,
									"<td class=\"wdh td0\"><div class=\"ball14\">",
									array[j, l],
									"</div></td>"
								});
							}
							else
							{
								object obj = text6;
								text6 = string.Concat(new object[]
								{
									obj,
									"<td class=\"wdh td1\"><div class=\"ball14\">",
									array[j, l],
									"</div></td>"
								});
							}
						}
					}
					text6 += "</tr>";
					this.LotteryLines = this.LotteryLines + text6 + text4 + text5;
				}
				if (this.lotteryId.Substring(0, 1) == "3")
				{
					this.count = 10;
					int[,] array = new int[3, 10];
					int[,] array2 = new int[3, 10];
					int[,] array3 = new int[3, 10];
					int[,] array4 = new int[3, 10];
					int[,] array5 = new int[3, 10];
					string[,] array6 = new string[3, 10];
					for (int i = 0; i < listDataTable.Rows.Count; i++)
					{
						DataRow dataRow = listDataTable.Rows[i];
						string str = dataRow["Title"].ToString();
						string text = dataRow["Number"].ToString();
						string[] array7 = text.Split(new char[]
						{
							','
						});
						string text2 = "<tr>";
						text2 += "<td rowspan=\"2\" style=\"width:100px;\">期号</td>";
						text2 += "<td rowspan=\"2\" style=\"width:100px;\">开奖号码</td>";
						text2 += "<td colspan=\"10\">百位</td>";
						text2 += "<td colspan=\"10\">十位</td>";
						text2 += "<td colspan=\"10\">个位</td>";
						text2 += "</tr>";
						text2 += "<tr>";
						for (int j = 0; j < array7.Length; j++)
						{
							for (int k = 0; k < 10; k++)
							{
								object obj = text2;
								text2 = string.Concat(new object[]
								{
									obj,
									"<td>",
									k,
									"</td>"
								});
							}
						}
						text2 += "</tr>";
						this.LotteryHeadLines = text2;
						string text3 = "<tr>";
						text3 += "<td class=\"issue\">";
						text3 += str;
						text3 += "</td>";
						text3 += "<td align=\"center\" class=\"tdwth\">";
						text3 += text;
						text3 += "</td>";
						for (int j = 0; j < array7.Length; j++)
						{
							for (int l = 0; l <= 9; l++)
							{
								if (l == Convert.ToInt32(array7[j]))
								{
									array[j, l]++;
									array2[j, l] = -1;
									array4[j, l]++;
									if (array3[j, l] < array4[j, l])
									{
										array3[j, l] = array4[j, l];
									}
								}
								else
								{
									array4[j, l] = 0;
									array2[j, l]++;
									if (array5[j, l] < array2[j, l])
									{
										array5[j, l] = array2[j, l];
									}
								}
								if (l == Convert.ToInt32(array7[j]))
								{
									if (j % 2 == 0)
									{
										text3 = text3 + "<td class=\"charball td0\"><div class=\"ball01\">" + array7[j] + "</div></td>";
									}
									else
									{
										text3 = text3 + "<td class=\"charball td1\"><div class=\"ball01\">" + array7[j] + "</div></td>";
									}
									array2[j, l]++;
								}
								else if (j % 2 == 0)
								{
									object obj = text3;
									text3 = string.Concat(new object[]
									{
										obj,
										"<td class=\"wdh td0\"><div class=\"ball14\">",
										array2[j, l],
										"</div></td>"
									});
								}
								else
								{
									object obj = text3;
									text3 = string.Concat(new object[]
									{
										obj,
										"<td class=\"wdh td1\"><div class=\"ball14\">",
										array2[j, l],
										"</div></td>"
									});
								}
							}
						}
						text3 += "</tr>";
						this.LotteryLines += text3;
					}
					string text4 = "<tr>";
					text4 += "<td colspan=\"2\">";
					text4 += "当前最大连开";
					text4 += "</td>";
					for (int j = 0; j < 3; j++)
					{
						for (int l = 0; l <= 9; l++)
						{
							if (j % 2 == 0)
							{
								object obj = text4;
								text4 = string.Concat(new object[]
								{
									obj,
									"<td class=\"wdh td0\"><div class=\"ball14\">",
									array3[j, l],
									"</div></td>"
								});
							}
							else
							{
								object obj = text4;
								text4 = string.Concat(new object[]
								{
									obj,
									"<td class=\"wdh td1\"><div class=\"ball14\">",
									array3[j, l],
									"</div></td>"
								});
							}
						}
					}
					text4 += "</tr>";
					string text5 = "<tr>";
					text5 += "<td colspan=\"2\">";
					text5 += "当前最大遗漏";
					text5 += "</td>";
					for (int j = 0; j < 3; j++)
					{
						for (int l = 0; l <= 9; l++)
						{
							if (j % 2 == 0)
							{
								object obj = text5;
								text5 = string.Concat(new object[]
								{
									obj,
									"<td class=\"wdh td0\"><div class=\"ball14\">",
									array5[j, l],
									"</div></td>"
								});
							}
							else
							{
								object obj = text5;
								text5 = string.Concat(new object[]
								{
									obj,
									"<td class=\"wdh td1\"><div class=\"ball14\">",
									array5[j, l],
									"</div></td>"
								});
							}
						}
					}
					text5 += "</tr>";
					string text6 = "<tr>";
					text6 += "<td colspan=\"2\">";
					text6 += "当前出现次数";
					text6 += "</td>";
					for (int j = 0; j < 3; j++)
					{
						for (int l = 0; l <= 9; l++)
						{
							if (j % 2 == 0)
							{
								object obj = text6;
								text6 = string.Concat(new object[]
								{
									obj,
									"<td class=\"wdh td0\"><div class=\"ball14\">",
									array[j, l],
									"</div></td>"
								});
							}
							else
							{
								object obj = text6;
								text6 = string.Concat(new object[]
								{
									obj,
									"<td class=\"wdh td1\"><div class=\"ball14\">",
									array[j, l],
									"</div></td>"
								});
							}
						}
					}
					text6 += "</tr>";
					this.LotteryLines = this.LotteryLines + text6 + text4 + text5;
				}
				if (this.lotteryId.Substring(0, 1) == "4")
				{
					this.count = 10;
					int[,] array = new int[10, 11];
					int[,] array2 = new int[10, 11];
					int[,] array3 = new int[10, 11];
					int[,] array4 = new int[10, 11];
					int[,] array5 = new int[10, 11];
					string[,] array6 = new string[10, 11];
					for (int i = 0; i < listDataTable.Rows.Count; i++)
					{
						DataRow dataRow = listDataTable.Rows[i];
						string str = dataRow["Title"].ToString();
						string text = dataRow["Number"].ToString();
						string[] array7 = text.Split(new char[]
						{
							','
						});
						string text2 = "<tr>";
						text2 += "<td rowspan=\"2\" style=\"width:100px;\">期号</td>";
						text2 += "<td rowspan=\"2\" style=\"width:100px;\">开奖号码</td>";
						text2 += "<td colspan=\"10\">一</td>";
						text2 += "<td colspan=\"10\">二</td>";
						text2 += "<td colspan=\"10\">三</td>";
						text2 += "<td colspan=\"10\">四</td>";
						text2 += "<td colspan=\"10\">五</td>";
						text2 += "<td colspan=\"10\">六</td>";
						text2 += "<td colspan=\"10\">七</td>";
						text2 += "<td colspan=\"10\">八</td>";
						text2 += "<td colspan=\"10\">九</td>";
						text2 += "<td colspan=\"10\">十</td>";
						text2 += "</tr>";
						text2 += "<tr>";
						for (int j = 0; j < array7.Length; j++)
						{
							for (int k = 1; k <= 10; k++)
							{
								object obj = text2;
								text2 = string.Concat(new object[]
								{
									obj,
									"<td>",
									k,
									"</td>"
								});
							}
						}
						text2 += "</tr>";
						this.LotteryHeadLines = text2;
						string text3 = "<tr>";
						text3 += "<td class=\"issue\">";
						text3 += str;
						text3 += "</td>";
						text3 += "<td align=\"center\" class=\"tdwth\">";
						text3 += text;
						text3 += "</td>";
						for (int j = 0; j < array7.Length; j++)
						{
							for (int l = 0; l <= 9; l++)
							{
								if (l + 1 == Convert.ToInt32(array7[j]))
								{
									array[j, l]++;
									array2[j, l] = -1;
									array4[j, l]++;
									if (array3[j, l] < array4[j, l])
									{
										array3[j, l] = array4[j, l];
									}
								}
								else
								{
									array4[j, l] = 1;
									array2[j, l]++;
									if (array5[j, l] < array2[j, l])
									{
										array5[j, l] = array2[j, l];
									}
								}
								if (l + 1 == Convert.ToInt32(array7[j]))
								{
									if (j % 2 == 0)
									{
										text3 = text3 + "<td class=\"charball td0\"><div class=\"ball01\">" + array7[j] + "</div></td>";
									}
									else
									{
										text3 = text3 + "<td class=\"charball td1\"><div class=\"ball01\">" + array7[j] + "</div></td>";
									}
									array2[j, l]++;
								}
								else if (j % 2 == 0)
								{
									object obj = text3;
									text3 = string.Concat(new object[]
									{
										obj,
										"<td class=\"wdh td0\"><div class=\"ball14\">",
										array2[j, l],
										"</div></td>"
									});
								}
								else
								{
									object obj = text3;
									text3 = string.Concat(new object[]
									{
										obj,
										"<td class=\"wdh td1\"><div class=\"ball14\">",
										array2[j, l],
										"</div></td>"
									});
								}
							}
						}
						text3 += "</tr>";
						this.LotteryLines += text3;
					}
					string text4 = "<tr>";
					text4 += "<td colspan=\"2\">";
					text4 += "当前最大连开";
					text4 += "</td>";
					for (int j = 0; j < 10; j++)
					{
						for (int l = 0; l <= 9; l++)
						{
							if (j % 2 == 0)
							{
								object obj = text4;
								text4 = string.Concat(new object[]
								{
									obj,
									"<td class=\"wdh td0\"><div class=\"ball14\">",
									array3[j, l],
									"</div></td>"
								});
							}
							else
							{
								object obj = text4;
								text4 = string.Concat(new object[]
								{
									obj,
									"<td class=\"wdh td1\"><div class=\"ball14\">",
									array3[j, l],
									"</div></td>"
								});
							}
						}
					}
					text4 += "</tr>";
					string text5 = "<tr>";
					text5 += "<td colspan=\"2\">";
					text5 += "当前最大遗漏";
					text5 += "</td>";
					for (int j = 0; j < 10; j++)
					{
						for (int l = 0; l <= 9; l++)
						{
							if (j % 2 == 0)
							{
								object obj = text5;
								text5 = string.Concat(new object[]
								{
									obj,
									"<td class=\"wdh td0\"><div class=\"ball14\">",
									array5[j, l],
									"</div></td>"
								});
							}
							else
							{
								object obj = text5;
								text5 = string.Concat(new object[]
								{
									obj,
									"<td class=\"wdh td1\"><div class=\"ball14\">",
									array5[j, l],
									"</div></td>"
								});
							}
						}
					}
					text5 += "</tr>";
					string text6 = "<tr>";
					text6 += "<td colspan=\"2\">";
					text6 += "当前出现次数";
					text6 += "</td>";
					for (int j = 0; j < 10; j++)
					{
						for (int l = 0; l <= 9; l++)
						{
							if (j % 2 == 0)
							{
								object obj = text6;
								text6 = string.Concat(new object[]
								{
									obj,
									"<td class=\"wdh td0\"><div class=\"ball14\">",
									array[j, l],
									"</div></td>"
								});
							}
							else
							{
								object obj = text6;
								text6 = string.Concat(new object[]
								{
									obj,
									"<td class=\"wdh td1\"><div class=\"ball14\">",
									array[j, l],
									"</div></td>"
								});
							}
						}
					}
					text6 += "</tr>";
					this.LotteryLines = this.LotteryLines + text6 + text4 + text5;
				}
			}
		}

		public string LotteryHeadLines;

		public string LotteryLines;

		public string LName;

		public string lotteryId = "1001";

		public int count = 10;
	}
}
