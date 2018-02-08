// Decompiled with JetBrains decompiler
// Type: Web.MIL.Html.HtmlNode
// Assembly: Lottery.Collect, Version=7.0.1.203, Culture=neutral, PublicKeyToken=null
// MVID: 916E4E87-E8A0-4A21-8438-E89468303682
// Assembly location: F:\pros\tianheng\bf\WebAppOld\bin\Lottery.Collect.dll

using System.ComponentModel;

namespace Web.MIL.Html
{
  public abstract class HtmlNode
  {
    protected HtmlElement mParent = (HtmlElement) null;

    [Category("Relationships")]
    public HtmlNode GetCommonAncestor(HtmlNode node)
    {
      for (HtmlNode htmlNode1 = this; htmlNode1 != null; htmlNode1 = (HtmlNode) htmlNode1.Parent)
      {
        for (HtmlNode htmlNode2 = node; htmlNode2 != null; htmlNode2 = (HtmlNode) htmlNode2.Parent)
        {
          if (htmlNode1 == htmlNode2)
            return htmlNode1;
        }
      }
      return (HtmlNode) null;
    }

    [Category("Relationships")]
    public bool IsAncestorOf(HtmlNode node)
    {
      return node.IsDescendentOf(this);
    }

    [Category("Relationships")]
    public bool IsDescendentOf(HtmlNode node)
    {
      for (HtmlNode htmlNode = (HtmlNode) this.mParent; htmlNode != null; htmlNode = (HtmlNode) htmlNode.Parent)
      {
        if (htmlNode == node)
          return true;
      }
      return false;
    }

    [Description("This is true if this is an element node")]
    [Category("General")]
    public bool IsElement()
    {
      return this is HtmlElement;
    }

    [Description("This is true if this is a text node")]
    [Category("General")]
    public bool IsText()
    {
      return this is HtmlText;
    }

    [Category("General")]
    public void Remove()
    {
      if (this.mParent == null)
        return;
      this.mParent.Nodes.RemoveAt(this.Index);
    }

    internal void SetParent(HtmlElement parentNode)
    {
      this.mParent = parentNode;
    }

    public abstract override string ToString();

    [Description("The first child of this node")]
    [Category("Navigation")]
    public HtmlNode FirstChild
    {
      get
      {
        if (this is HtmlElement && ((HtmlElement) this).Nodes.Count != 0)
          return ((HtmlElement) this).Nodes[0];
        return (HtmlNode) null;
      }
    }

    [Category("Output")]
    [Description("The HTML that represents this node and all the children")]
    public abstract string HTML { get; }

    [Description("The zero-based index of this node in the parent's nodes collection")]
    [Category("Navigation")]
    public int Index
    {
      get
      {
        if (this.mParent == null)
          return -1;
        return this.mParent.Nodes.IndexOf(this);
      }
    }

    [Category("Navigation")]
    [Description("Is this node a child of another?")]
    public bool IsChild
    {
      get
      {
        return this.mParent != null;
      }
    }

    [Category("Navigation")]
    [Description("Does this node have any children?")]
    public bool IsParent
    {
      get
      {
        return this is HtmlElement && ((HtmlElement) this).Nodes.Count > 0;
      }
    }

    [Description("Is this node a root node?")]
    [Category("Navigation")]
    public bool IsRoot
    {
      get
      {
        return this.mParent == null;
      }
    }

    [Category("Navigation")]
    [Description("The last child of this node")]
    public HtmlNode LastChild
    {
      get
      {
        if (this is HtmlElement && ((HtmlElement) this).Nodes.Count != 0)
          return ((HtmlElement) this).Nodes[((HtmlElement) this).Nodes.Count - 1];
        return (HtmlNode) null;
      }
    }

    [Description("The next sibling node")]
    [Category("Navigation")]
    public HtmlNode Next
    {
      get
      {
        if (this.Index != -1 && this.Parent.Nodes.Count > this.Index + 1)
          return this.Parent.Nodes[this.Index + 1];
        return (HtmlNode) null;
      }
    }

    [Description("The parent node of this one")]
    [Category("Navigation")]
    public HtmlElement Parent
    {
      get
      {
        return this.mParent;
      }
    }

    [Description("The previous sibling node")]
    [Category("Navigation")]
    public HtmlNode Previous
    {
      get
      {
        if (this.Index != -1 && this.Index > 0)
          return this.Parent.Nodes[this.Index - 1];
        return (HtmlNode) null;
      }
    }

    [Description("The XHTML that represents this node and all the children")]
    [Category("Output")]
    public abstract string XHTML { get; }
  }
}
