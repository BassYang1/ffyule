// Decompiled with JetBrains decompiler
// Type: Web.MIL.Html.HtmlNodeCollection
// Assembly: Lottery.Collect, Version=7.0.1.203, Culture=neutral, PublicKeyToken=null
// MVID: 916E4E87-E8A0-4A21-8438-E89468303682
// Assembly location: F:\pros\tianheng\bf\WebAppOld\bin\Lottery.Collect.dll

using System.Collections;

namespace Web.MIL.Html
{
  public class HtmlNodeCollection : CollectionBase
  {
    private HtmlElement mParent;

    public HtmlNodeCollection()
    {
      this.mParent = (HtmlElement) null;
    }

    internal HtmlNodeCollection(HtmlElement parent)
    {
      this.mParent = parent;
    }

    public int Add(HtmlNode node)
    {
      if (this.mParent != null)
        node.SetParent(this.mParent);
      return this.List.Add((object) node);
    }

    public HtmlNodeCollection FindByAttributeName(string attributeName)
    {
      return this.FindByAttributeName(attributeName, true);
    }

    public HtmlNodeCollection FindByAttributeName(string attributeName, bool searchChildren)
    {
      HtmlNodeCollection htmlNodeCollection = new HtmlNodeCollection((HtmlElement) null);
      foreach (HtmlNode node1 in (IEnumerable) this.List)
      {
        if (node1 is HtmlElement)
        {
          foreach (HtmlAttribute attribute in (CollectionBase) ((HtmlElement) node1).Attributes)
          {
            if (attribute.Name.ToLower().Equals(attributeName.ToLower()))
            {
              htmlNodeCollection.Add(node1);
              break;
            }
          }
          if (searchChildren)
          {
            foreach (HtmlNode node2 in (CollectionBase) ((HtmlElement) node1).Nodes.FindByAttributeName(attributeName, searchChildren))
              htmlNodeCollection.Add(node2);
          }
        }
      }
      return htmlNodeCollection;
    }

    public HtmlNodeCollection FindByAttributeNameValue(string attributeName, string attributeValue, bool isLike)
    {
      return this.FindByAttributeNameValue(attributeName, attributeValue, isLike, true);
    }

    public HtmlNodeCollection FindByAttributeNameValue(string attributeName, string attributeValue, bool isLike, bool searchChildren)
    {
      HtmlNodeCollection htmlNodeCollection = new HtmlNodeCollection((HtmlElement) null);
      foreach (HtmlNode node1 in (IEnumerable) this.List)
      {
        if (node1 is HtmlElement)
        {
          foreach (HtmlAttribute attribute in (CollectionBase) ((HtmlElement) node1).Attributes)
          {
            if (attribute.Name.ToLower().Equals(attributeName.ToLower()))
            {
              if (isLike)
              {
                if (attribute.Value.ToLower().StartsWith(attributeValue.ToLower()))
                {
                  htmlNodeCollection.Add(node1);
                  break;
                }
                break;
              }
              if (attribute.Value.ToLower().Equals(attributeValue.ToLower()))
              {
                htmlNodeCollection.Add(node1);
                break;
              }
              break;
            }
          }
          if (searchChildren)
          {
            foreach (HtmlNode node2 in (CollectionBase) ((HtmlElement) node1).Nodes.FindByAttributeNameValue(attributeName, attributeValue, isLike, searchChildren))
              htmlNodeCollection.Add(node2);
          }
        }
      }
      return htmlNodeCollection;
    }

    public HtmlNodeCollection FindByName(string name)
    {
      return this.FindByName(name, true);
    }

    public HtmlNodeCollection FindByName(string name, bool searchChildren)
    {
      HtmlNodeCollection htmlNodeCollection = new HtmlNodeCollection((HtmlElement) null);
      foreach (HtmlNode node1 in (IEnumerable) this.List)
      {
        if (node1 is HtmlElement)
        {
          if (((HtmlElement) node1).Name.ToLower().Equals(name.ToLower()))
            htmlNodeCollection.Add(node1);
          if (searchChildren)
          {
            foreach (HtmlNode node2 in (CollectionBase) ((HtmlElement) node1).Nodes.FindByName(name, searchChildren))
              htmlNodeCollection.Add(node2);
          }
        }
      }
      return htmlNodeCollection;
    }

    public int IndexOf(HtmlNode node)
    {
      return this.List.IndexOf((object) node);
    }

    public void Insert(int index, HtmlNode node)
    {
      if (this.mParent != null)
        node.SetParent(this.mParent);
      this.InnerList.Insert(index, (object) node);
    }

    public HtmlNode this[string name]
    {
      get
      {
        HtmlNodeCollection byName = this.FindByName(name, false);
        if (byName.Count > 0)
          return byName[0];
        return (HtmlNode) null;
      }
    }

    public HtmlNode this[int index]
    {
      get
      {
        return (HtmlNode) this.InnerList[index];
      }
      set
      {
        if (this.mParent != null)
          value.SetParent(this.mParent);
        this.InnerList[index] = (object) value;
      }
    }
  }
}
