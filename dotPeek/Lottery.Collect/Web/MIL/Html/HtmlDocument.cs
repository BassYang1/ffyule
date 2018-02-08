// Decompiled with JetBrains decompiler
// Type: Web.MIL.Html.HtmlDocument
// Assembly: Lottery.Collect, Version=7.0.1.203, Culture=neutral, PublicKeyToken=null
// MVID: 916E4E87-E8A0-4A21-8438-E89468303682
// Assembly location: F:\pros\tianheng\bf\WebAppOld\bin\Lottery.Collect.dll

using System.Collections;
using System.ComponentModel;
using System.Text;

namespace Web.MIL.Html
{
  public class HtmlDocument
  {
    private HtmlNodeCollection mNodes = new HtmlNodeCollection((HtmlElement) null);
    private string mXhtmlHeader = "<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Strict//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd\">";

    internal HtmlDocument(string html, bool wantSpaces)
    {
      this.mNodes = new HtmlParser()
      {
        RemoveEmptyElementText = (!wantSpaces)
      }.Parse(html);
    }

    public static HtmlDocument Create(string html)
    {
      return new HtmlDocument(html, false);
    }

    public static HtmlDocument Create(string html, bool wantSpaces)
    {
      return new HtmlDocument(html, wantSpaces);
    }

    [Category("General")]
    [Description("This is the DOCTYPE for XHTML production")]
    public string DocTypeXHTML
    {
      get
      {
        return this.mXhtmlHeader;
      }
      set
      {
        this.mXhtmlHeader = value;
      }
    }

    [Description("The HTML version of this document")]
    [Category("Output")]
    public string HTML
    {
      get
      {
        StringBuilder stringBuilder = new StringBuilder();
        foreach (HtmlNode node in (CollectionBase) this.Nodes)
          stringBuilder.Append(node.HTML);
        return stringBuilder.ToString();
      }
    }

    public HtmlNodeCollection Nodes
    {
      get
      {
        return this.mNodes;
      }
    }

    [Description("The XHTML version of this document")]
    [Category("Output")]
    public string XHTML
    {
      get
      {
        StringBuilder stringBuilder = new StringBuilder();
        if (this.mXhtmlHeader != null)
        {
          stringBuilder.Append(this.mXhtmlHeader);
          stringBuilder.Append("\r\n");
        }
        foreach (HtmlNode node in (CollectionBase) this.Nodes)
          stringBuilder.Append(node.XHTML);
        return stringBuilder.ToString();
      }
    }
  }
}
