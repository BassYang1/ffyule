// Decompiled with JetBrains decompiler
// Type: Web.MIL.Html.HtmlAttributeCollection
// Assembly: Lottery.Collect, Version=7.0.1.203, Culture=neutral, PublicKeyToken=null
// MVID: 916E4E87-E8A0-4A21-8438-E89468303682
// Assembly location: F:\pros\tianheng\bf\WebAppOld\bin\Lottery.Collect.dll

using System.Collections;

namespace Web.MIL.Html
{
  public class HtmlAttributeCollection : CollectionBase
  {
    private HtmlElement mElement;

    public HtmlAttributeCollection()
    {
      this.mElement = (HtmlElement) null;
    }

    internal HtmlAttributeCollection(HtmlElement element)
    {
      this.mElement = element;
    }

    public int Add(HtmlAttribute attribute)
    {
      return this.List.Add((object) attribute);
    }

    public HtmlAttribute FindByName(string name)
    {
      if (this.IndexOf(name) == -1)
        return (HtmlAttribute) null;
      return this[this.IndexOf(name)];
    }

    public int IndexOf(string name)
    {
      for (int index = 0; index < this.List.Count; ++index)
      {
        if (this[index].Name.ToLower().Equals(name.ToLower()))
          return index;
      }
      return -1;
    }

    public HtmlAttribute this[string name]
    {
      get
      {
        return this.FindByName(name);
      }
    }

    public HtmlAttribute this[int index]
    {
      get
      {
        return (HtmlAttribute) this.List[index];
      }
      set
      {
        this.List[index] = (object) value;
      }
    }
  }
}
