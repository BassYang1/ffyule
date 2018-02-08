// Decompiled with JetBrains decompiler
// Type: Web.MIL.Html.HtmlParser
// Assembly: Lottery.Collect, Version=7.0.1.203, Culture=neutral, PublicKeyToken=null
// MVID: 916E4E87-E8A0-4A21-8438-E89468303682
// Assembly location: F:\pros\tianheng\bf\WebAppOld\bin\Lottery.Collect.dll

using System.Collections.Specialized;
using System.Text;

namespace Web.MIL.Html
{
  internal class HtmlParser
  {
    private static char[] WHITESPACE_CHARS = " \t\r\n".ToCharArray();
    private bool mRemoveEmptyElementText = false;

    private static string DecodeScript(string script)
    {
      return script.Replace("[MIL-SCRIPT-LT]", "<").Replace("[MIL-SCRIPT-GT]", ">").Replace("[MIL-SCRIPT-CR]", "\r").Replace("[MIL-SCRIPT-LF]", "\n");
    }

    private static string EncodeScript(string script)
    {
      return script.Replace("<", "[MIL-SCRIPT-LT]").Replace(">", "[MIL-SCRIPT-GT]").Replace("\r", "[MIL-SCRIPT-CR]").Replace("\n", "[MIL-SCRIPT-LF]");
    }

    private int FindTagOpenNodeIndex(HtmlNodeCollection nodes, string name)
    {
      for (int index = nodes.Count - 1; index >= 0; --index)
      {
        if (nodes[index] is HtmlElement && (((HtmlElement) nodes[index]).Name.ToLower().Equals(name.ToLower()) && ((HtmlElement) nodes[index]).Nodes.Count == 0 && !((HtmlElement) nodes[index]).IsTerminated))
          return index;
      }
      return -1;
    }

    private StringCollection GetTokens(string input)
    {
      StringCollection stringCollection = new StringCollection();
      int startIndex1 = 0;
      HtmlParser.ParseStatus parseStatus = HtmlParser.ParseStatus.ReadText;
      while (startIndex1 < input.Length)
      {
        switch (parseStatus)
        {
          case HtmlParser.ParseStatus.ReadText:
            if (startIndex1 + 2 < input.Length && input.Substring(startIndex1, 2).Equals("</"))
            {
              startIndex1 += 2;
              stringCollection.Add("</");
              parseStatus = HtmlParser.ParseStatus.ReadEndTag;
              break;
            }
            if (input.Substring(startIndex1, 1).Equals("<"))
            {
              ++startIndex1;
              stringCollection.Add("<");
              parseStatus = HtmlParser.ParseStatus.ReadStartTag;
              break;
            }
            int num1 = input.IndexOf("<", startIndex1);
            if (num1 == -1)
            {
              stringCollection.Add(input.Substring(startIndex1));
              return stringCollection;
            }
            stringCollection.Add(input.Substring(startIndex1, num1 - startIndex1));
            startIndex1 = num1;
            break;
          case HtmlParser.ParseStatus.ReadEndTag:
            while (startIndex1 < input.Length && input.Substring(startIndex1, 1).IndexOfAny(HtmlParser.WHITESPACE_CHARS) != -1)
              ++startIndex1;
            int startIndex2 = startIndex1;
            while (startIndex1 < input.Length && input.Substring(startIndex1, 1).IndexOfAny(" \r\n\t>".ToCharArray()) == -1)
              ++startIndex1;
            stringCollection.Add(input.Substring(startIndex2, startIndex1 - startIndex2));
            while (startIndex1 < input.Length && input.Substring(startIndex1, 1).IndexOfAny(HtmlParser.WHITESPACE_CHARS) != -1)
              ++startIndex1;
            if (startIndex1 < input.Length && input.Substring(startIndex1, 1).Equals(">"))
            {
              stringCollection.Add(">");
              parseStatus = HtmlParser.ParseStatus.ReadText;
              ++startIndex1;
              break;
            }
            break;
          case HtmlParser.ParseStatus.ReadStartTag:
            while (startIndex1 < input.Length && input.Substring(startIndex1, 1).IndexOfAny(HtmlParser.WHITESPACE_CHARS) != -1)
              ++startIndex1;
            int startIndex3 = startIndex1;
            while (startIndex1 < input.Length && input.Substring(startIndex1, 1).IndexOfAny(" \r\n\t/>".ToCharArray()) == -1)
              ++startIndex1;
            stringCollection.Add(input.Substring(startIndex3, startIndex1 - startIndex3));
            while (startIndex1 < input.Length && input.Substring(startIndex1, 1).IndexOfAny(HtmlParser.WHITESPACE_CHARS) != -1)
              ++startIndex1;
            if (startIndex1 + 1 < input.Length && input.Substring(startIndex1, 1).Equals("/>"))
            {
              stringCollection.Add("/>");
              parseStatus = HtmlParser.ParseStatus.ReadText;
              startIndex1 += 2;
              break;
            }
            if (startIndex1 < input.Length && input.Substring(startIndex1, 1).Equals(">"))
            {
              stringCollection.Add(">");
              parseStatus = HtmlParser.ParseStatus.ReadText;
              ++startIndex1;
              break;
            }
            parseStatus = HtmlParser.ParseStatus.ReadAttributeName;
            break;
          case HtmlParser.ParseStatus.ReadAttributeName:
            while (startIndex1 < input.Length && input.Substring(startIndex1, 1).IndexOfAny(HtmlParser.WHITESPACE_CHARS) != -1)
              ++startIndex1;
            int startIndex4 = startIndex1;
            while (startIndex1 < input.Length && input.Substring(startIndex1, 1).IndexOfAny(" \r\n\t/>=".ToCharArray()) == -1)
              ++startIndex1;
            stringCollection.Add(input.Substring(startIndex4, startIndex1 - startIndex4));
            while (startIndex1 < input.Length && input.Substring(startIndex1, 1).IndexOfAny(HtmlParser.WHITESPACE_CHARS) != -1)
              ++startIndex1;
            if (startIndex1 + 1 < input.Length && input.Substring(startIndex1, 2).Equals("/>"))
            {
              stringCollection.Add("/>");
              parseStatus = HtmlParser.ParseStatus.ReadText;
              startIndex1 += 2;
              break;
            }
            if (startIndex1 < input.Length && input.Substring(startIndex1, 1).Equals(">"))
            {
              stringCollection.Add(">");
              parseStatus = HtmlParser.ParseStatus.ReadText;
              ++startIndex1;
              break;
            }
            if (startIndex1 < input.Length && input.Substring(startIndex1, 1).Equals("="))
            {
              stringCollection.Add("=");
              ++startIndex1;
              parseStatus = HtmlParser.ParseStatus.ReadAttributeValue;
              break;
            }
            if (startIndex1 < input.Length && input.Substring(startIndex1, 1).Equals("/"))
            {
              ++startIndex1;
              break;
            }
            break;
          case HtmlParser.ParseStatus.ReadAttributeValue:
            while (startIndex1 < input.Length && input.Substring(startIndex1, 1).IndexOfAny(HtmlParser.WHITESPACE_CHARS) != -1)
              ++startIndex1;
            if (startIndex1 < input.Length && input.Substring(startIndex1, 1).Equals("\""))
            {
              int num2 = startIndex1;
              ++startIndex1;
              while (startIndex1 < input.Length && !input.Substring(startIndex1, 1).Equals("\""))
                ++startIndex1;
              if (startIndex1 < input.Length && input.Substring(startIndex1, 1).Equals("\""))
                ++startIndex1;
              stringCollection.Add(input.Substring(num2 + 1, startIndex1 - num2 - 2));
              parseStatus = HtmlParser.ParseStatus.ReadAttributeName;
            }
            else if (startIndex1 < input.Length && input.Substring(startIndex1, 1).Equals("'"))
            {
              int num2 = startIndex1;
              ++startIndex1;
              while (startIndex1 < input.Length && !input.Substring(startIndex1, 1).Equals("'"))
                ++startIndex1;
              if (startIndex1 < input.Length && input.Substring(startIndex1, 1).Equals("'"))
                ++startIndex1;
              stringCollection.Add(input.Substring(num2 + 1, startIndex1 - num2 - 2));
              parseStatus = HtmlParser.ParseStatus.ReadAttributeName;
            }
            else
            {
              int startIndex5 = startIndex1;
              while (startIndex1 < input.Length && input.Substring(startIndex1, 1).IndexOfAny(" \r\n\t/>".ToCharArray()) == -1)
                ++startIndex1;
              stringCollection.Add(input.Substring(startIndex5, startIndex1 - startIndex5));
              while (startIndex1 < input.Length && input.Substring(startIndex1, 1).IndexOfAny(HtmlParser.WHITESPACE_CHARS) != -1)
                ++startIndex1;
              parseStatus = HtmlParser.ParseStatus.ReadAttributeName;
            }
            if (startIndex1 + 1 < input.Length && input.Substring(startIndex1, 2).Equals("/>"))
            {
              stringCollection.Add("/>");
              parseStatus = HtmlParser.ParseStatus.ReadText;
              startIndex1 += 2;
            }
            else if (startIndex1 < input.Length && input.Substring(startIndex1, 1).Equals(">"))
            {
              stringCollection.Add(">");
              ++startIndex1;
              parseStatus = HtmlParser.ParseStatus.ReadText;
            }
            break;
        }
      }
      return stringCollection;
    }

    private void MoveNodesDown(ref HtmlNodeCollection nodes, int node_index, HtmlElement new_parent)
    {
      for (int index = node_index; index < nodes.Count; ++index)
      {
        new_parent.Nodes.Add(nodes[index]);
        nodes[index].SetParent(new_parent);
      }
      int count = nodes.Count;
      for (int index = node_index; index < count; ++index)
        nodes.RemoveAt(node_index);
      new_parent.IsExplicitlyTerminated = true;
    }

    public HtmlNodeCollection Parse(string html)
    {
      HtmlNodeCollection nodes = new HtmlNodeCollection((HtmlElement) null);
      html = this.PreprocessScript(html, "script");
      html = this.PreprocessScript(html, "style");
      html = this.RemoveComments(html);
      html = this.RemoveSGMLComments(html);
      StringCollection tokens = this.GetTokens(html);
      int index1 = 0;
      HtmlElement htmlElement = (HtmlElement) null;
      while (index1 < tokens.Count)
      {
        if ("<".Equals(tokens[index1]))
        {
          int index2 = index1 + 1;
          if (index2 >= tokens.Count)
            return nodes;
          string name1 = tokens[index2];
          index1 = index2 + 1;
          htmlElement = new HtmlElement(name1);
          while (index1 < tokens.Count && !">".Equals(tokens[index1]) && !"/>".Equals(tokens[index1]))
          {
            string name2 = tokens[index1];
            ++index1;
            if (index1 < tokens.Count && "=".Equals(tokens[index1]))
            {
              int index3 = index1 + 1;
              string str = index3 >= tokens.Count ? (string) null : tokens[index3];
              index1 = index3 + 1;
              HtmlAttribute attribute = new HtmlAttribute(name2, HtmlEncoder.DecodeValue(str));
              htmlElement.Attributes.Add(attribute);
            }
            else if (index1 < tokens.Count)
            {
              HtmlAttribute attribute = new HtmlAttribute(name2, (string) null);
              htmlElement.Attributes.Add(attribute);
            }
          }
          nodes.Add((HtmlNode) htmlElement);
          if (index1 < tokens.Count && "/>".Equals(tokens[index1]))
          {
            htmlElement.IsTerminated = true;
            ++index1;
            htmlElement = (HtmlElement) null;
          }
          else if (index1 < tokens.Count && ">".Equals(tokens[index1]))
            ++index1;
        }
        else if (">".Equals(tokens[index1]))
          ++index1;
        else if ("</".Equals(tokens[index1]))
        {
          int index2 = index1 + 1;
          if (index2 >= tokens.Count)
            return nodes;
          string name = tokens[index2];
          index1 = index2 + 1;
          int tagOpenNodeIndex = this.FindTagOpenNodeIndex(nodes, name);
          if (tagOpenNodeIndex != -1)
            this.MoveNodesDown(ref nodes, tagOpenNodeIndex + 1, (HtmlElement) nodes[tagOpenNodeIndex]);
          while (index1 < tokens.Count && !">".Equals(tokens[index1]))
            ++index1;
          if (index1 < tokens.Count && ">".Equals(tokens[index1]))
            ++index1;
          htmlElement = (HtmlElement) null;
        }
        else
        {
          string str = tokens[index1];
          if (this.mRemoveEmptyElementText)
            str = this.RemoveWhitespace(str);
          string text = HtmlParser.DecodeScript(str);
          if (!this.mRemoveEmptyElementText || text.Length != 0)
          {
            if (htmlElement == null || !htmlElement.NoEscaping)
              text = HtmlEncoder.DecodeValue(text);
            HtmlText htmlText = new HtmlText(text);
            nodes.Add((HtmlNode) htmlText);
          }
          ++index1;
        }
      }
      return nodes;
    }

    private string PreprocessScript(string input, string tag_name)
    {
      StringBuilder stringBuilder1 = new StringBuilder();
      int startIndex = 0;
      int length = tag_name.Length;
      while (startIndex < input.Length)
      {
        bool flag = false;
        if (startIndex + length + 1 < input.Length && input.Substring(startIndex, length + 1).ToLower().Equals("<" + tag_name))
        {
          while (true)
          {
            if (startIndex < input.Length)
            {
              if (!input.Substring(startIndex, 1).Equals(">"))
              {
                if (startIndex + 1 >= input.Length || !input.Substring(startIndex, 2).Equals("/>"))
                {
                  if (input.Substring(startIndex, 1).Equals("\""))
                  {
                    stringBuilder1.Append("\"");
                    for (++startIndex; startIndex < input.Length && !input.Substring(startIndex, 1).Equals("\""); ++startIndex)
                      stringBuilder1.Append(input.Substring(startIndex, 1));
                    if (startIndex < input.Length)
                    {
                      ++startIndex;
                      stringBuilder1.Append("\"");
                    }
                  }
                  else if (input.Substring(startIndex, 1).Equals("'"))
                  {
                    stringBuilder1.Append("'");
                    for (++startIndex; startIndex < input.Length && !input.Substring(startIndex, 1).Equals("'"); ++startIndex)
                      stringBuilder1.Append(input.Substring(startIndex, 1));
                    if (startIndex < input.Length)
                    {
                      ++startIndex;
                      stringBuilder1.Append("'");
                    }
                  }
                  else
                  {
                    stringBuilder1.Append(input.Substring(startIndex, 1));
                    ++startIndex;
                  }
                }
                else
                  goto label_5;
              }
              else
                break;
            }
            else
              goto label_21;
          }
          stringBuilder1.Append(">");
          ++startIndex;
          goto label_21;
label_5:
          stringBuilder1.Append("/>");
          startIndex += 2;
          flag = true;
label_21:
          if (startIndex < input.Length)
          {
            if (!flag)
            {
              StringBuilder stringBuilder2 = new StringBuilder();
              for (; startIndex + length + 3 < input.Length && !input.Substring(startIndex, length + 3).ToLower().Equals("</" + tag_name + ">"); ++startIndex)
                stringBuilder2.Append(input.Substring(startIndex, 1));
              stringBuilder1.Append(HtmlParser.EncodeScript(stringBuilder2.ToString()));
              stringBuilder1.Append("</" + tag_name + ">");
              if (startIndex + length + 3 < input.Length)
                startIndex += length + 3;
            }
          }
          else
            break;
        }
        else
        {
          stringBuilder1.Append(input.Substring(startIndex, 1));
          ++startIndex;
        }
      }
      return stringBuilder1.ToString();
    }

    private string RemoveComments(string input)
    {
      StringBuilder stringBuilder = new StringBuilder();
      int startIndex1 = 0;
      bool flag = false;
      while (startIndex1 < input.Length)
      {
        if (startIndex1 + 4 < input.Length && input.Substring(startIndex1, 4).Equals("<!--"))
        {
          int startIndex2 = startIndex1 + 4;
          int num = input.IndexOf("-->", startIndex2);
          if (num != -1)
            startIndex1 = num + 3;
          else
            break;
        }
        else if (input.Substring(startIndex1, 1).Equals("<"))
        {
          flag = true;
          stringBuilder.Append("<");
          ++startIndex1;
        }
        else if (input.Substring(startIndex1, 1).Equals(">"))
        {
          flag = false;
          stringBuilder.Append(">");
          ++startIndex1;
        }
        else if (input.Substring(startIndex1, 1).Equals("\"") && flag)
        {
          int startIndex2 = startIndex1;
          int startIndex3 = startIndex1 + 1;
          int num = input.IndexOf("\"", startIndex3);
          if (num != -1)
          {
            startIndex1 = num + 1;
            stringBuilder.Append(input.Substring(startIndex2, startIndex1 - startIndex2));
          }
          else
            break;
        }
        else if (input.Substring(startIndex1, 1).Equals("'") && flag)
        {
          int startIndex2 = startIndex1;
          int startIndex3 = startIndex1 + 1;
          int num = input.IndexOf("'", startIndex3);
          if (num != -1)
          {
            startIndex1 = num + 1;
            stringBuilder.Append(input.Substring(startIndex2, startIndex1 - startIndex2));
          }
          else
            break;
        }
        else
        {
          stringBuilder.Append(input.Substring(startIndex1, 1));
          ++startIndex1;
        }
      }
      return stringBuilder.ToString();
    }

    private string RemoveSGMLComments(string input)
    {
      StringBuilder stringBuilder = new StringBuilder();
      int startIndex1 = 0;
      bool flag = false;
      while (startIndex1 < input.Length)
      {
        if (startIndex1 + 2 < input.Length && input.Substring(startIndex1, 2).Equals("<!"))
        {
          int startIndex2 = startIndex1 + 2;
          int num = input.IndexOf(">", startIndex2);
          if (num != -1)
            startIndex1 = num + 3;
          else
            break;
        }
        else if (input.Substring(startIndex1, 1).Equals("<"))
        {
          flag = true;
          stringBuilder.Append("<");
          ++startIndex1;
        }
        else if (input.Substring(startIndex1, 1).Equals(">"))
        {
          flag = false;
          stringBuilder.Append(">");
          ++startIndex1;
        }
        else if (input.Substring(startIndex1, 1).Equals("\"") && flag)
        {
          int startIndex2 = startIndex1;
          int startIndex3 = startIndex1 + 1;
          int num = input.IndexOf("\"", startIndex3);
          if (num != -1)
          {
            startIndex1 = num + 1;
            stringBuilder.Append(input.Substring(startIndex2, startIndex1 - startIndex2));
          }
          else
            break;
        }
        else if (input.Substring(startIndex1, 1).Equals("'") && flag)
        {
          int startIndex2 = startIndex1;
          int startIndex3 = startIndex1 + 1;
          int num = input.IndexOf("'", startIndex3);
          if (num != -1)
          {
            startIndex1 = num + 1;
            stringBuilder.Append(input.Substring(startIndex2, startIndex1 - startIndex2));
          }
          else
            break;
        }
        else
        {
          stringBuilder.Append(input.Substring(startIndex1, 1));
          ++startIndex1;
        }
      }
      return stringBuilder.ToString();
    }

    private string RemoveWhitespace(string input)
    {
      return input.Replace("\r", "").Replace("\n", "").Replace("\t", " ").Trim();
    }

    public bool RemoveEmptyElementText
    {
      get
      {
        return this.mRemoveEmptyElementText;
      }
      set
      {
        this.mRemoveEmptyElementText = value;
      }
    }

    private enum ParseStatus
    {
      ReadText,
      ReadEndTag,
      ReadStartTag,
      ReadAttributeName,
      ReadAttributeValue,
    }
  }
}
