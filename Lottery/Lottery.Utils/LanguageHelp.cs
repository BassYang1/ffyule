using System;
using System.Collections.Generic;
using Lottery.Utils.fastJSON;

namespace Lottery.Utils
{
	public class LanguageHelp
	{
		public Dictionary<string, object> GetEntity(string _lng)
		{
			string text = DirFile.ReadFile("~/data/languages/" + _lng + ".js");
			text = Strings.GetHtml(text, "//<!--语言包begin", "//-->语言包end");
			return (Dictionary<string, object>)JSON.Instance.ToObject(text);
		}
	}
}
