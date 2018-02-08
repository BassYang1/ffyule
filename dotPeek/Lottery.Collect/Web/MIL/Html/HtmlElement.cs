// Decompiled with JetBrains decompiler
// Type: Web.MIL.Html.HtmlElement
// Assembly: Lottery.Collect, Version=7.0.1.203, Culture=neutral, PublicKeyToken=null
// MVID: 916E4E87-E8A0-4A21-8438-E89468303682
// Assembly location: F:\pros\tianheng\bf\WebAppOld\bin\Lottery.Collect.dll

using System;
using System.Collections;
using System.ComponentModel;
using System.Text;

namespace Web.MIL.Html
{
  public class HtmlElement : HtmlNode
  {
    protected HtmlAttributeCollection mAttributes;
    protected bool mIsExplicitlyTerminated;
    protected bool mIsTerminated;
    protected string mName;
    protected HtmlNodeCollection mNodes;

    public HtmlElement(string name)
    {
      this.mNodes = new HtmlNodeCollection(this);
      this.mAttributes = new HtmlAttributeCollection(this);
      this.mName = name;
      this.mIsTerminated = false;
    }

    public override string ToString()
    {
      string str = "<" + this.mName;
      foreach (HtmlAttribute attribute in (CollectionBase) this.Attributes)
        str = str + " " + attribute.ToString();
      return str + ">";
    }

    [Description("The set of attributes associated with this element")]
    [Category("General")]
    public HtmlAttributeCollection Attributes
    {
      get
      {
        return this.mAttributes;
      }
    }

    [Category("Output")]
    public override string HTML
    {
      get
      {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append("<" + this.mName);
        foreach (HtmlAttribute attribute in (CollectionBase) this.Attributes)
          stringBuilder.Append(" " + attribute.HTML);
        if (this.Nodes.Count > 0)
        {
          stringBuilder.Append(">");
          foreach (HtmlNode node in (CollectionBase) this.Nodes)
            stringBuilder.Append(node.HTML);
          stringBuilder.Append("</" + this.mName + ">");
        }
        else if (this.IsExplicitlyTerminated)
          stringBuilder.Append("></" + this.mName + ">");
        else if (this.IsTerminated)
          stringBuilder.Append("/>");
        else
          stringBuilder.Append(">");
        return stringBuilder.ToString();
      }
    }

    internal bool IsExplicitlyTerminated
    {
      get
      {
        return this.mIsExplicitlyTerminated;
      }
      set
      {
        this.mIsExplicitlyTerminated = value;
      }
    }

    internal bool IsTerminated
    {
      get
      {
        if (this.Nodes.Count > 0)
          return false;
        return this.mIsTerminated | this.mIsExplicitlyTerminated;
      }
      set
      {
        this.mIsTerminated = value;
      }
    }

    [Description("The name of the tag/element")]
    [Category("General")]
    public string Name
    {
      get
      {
        return this.mName;
      }
      set
      {
        this.mName = value;
      }
    }

    [Category("General")]
    [Description("The set of child nodes")]
    public HtmlNodeCollection Nodes
    {
      get
      {
        if (this.IsText())
          throw new InvalidOperationException("An HtmlText node does not have child nodes");
        return this.mNodes;
      }
    }

    internal bool NoEscaping
    {
      get
      {
        return "script".Equals(this.Name.ToLower()) || "style".Equals(this.Name.ToLower());
      }
    }

    [Category("General")]
    [Description("A concatination of all the text associated with this element")]
    public string Text
    {
      get
      {
        StringBuilder stringBuilder = new StringBuilder();
        foreach (HtmlNode node in (CollectionBase) this.Nodes)
        {
          if (node is HtmlText)
            stringBuilder.Append(((HtmlText) node).Text);
        }
        return stringBuilder.ToString();
      }
    }

    [Category("Output")]
    public override string XHTML
    {
      get
      {
        if ("html".Equals(this.mName) && this.Attributes["xmlns"] == null)
          this.Attributes.Add(new HtmlAttribute("xmlns", "http://www.w3.org/1999/xhtml"));
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append("<" + this.mName.ToLower());
        foreach (HtmlAttribute attribute in (CollectionBase) this.Attributes)
          stringBuilder.Append(" " + attribute.XHTML);
        if (this.IsTerminated)
          stringBuilder.Append("/>");
        else if (this.Nodes.Count > 0)
        {
          stringBuilder.Append(">");
          foreach (HtmlNode node in (CollectionBase) this.Nodes)
            stringBuilder.Append(node.XHTML);
          stringBuilder.Append("</" + this.mName.ToLower() + ">");
        }
        else
          stringBuilder.Append("/>");
        return stringBuilder.ToString();
      }
    }
  }
}
