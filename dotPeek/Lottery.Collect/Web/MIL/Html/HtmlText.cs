// Decompiled with JetBrains decompiler
// Type: Web.MIL.Html.HtmlText
// Assembly: Lottery.Collect, Version=7.0.1.203, Culture=neutral, PublicKeyToken=null
// MVID: 916E4E87-E8A0-4A21-8438-E89468303682
// Assembly location: F:\pros\tianheng\bf\WebAppOld\bin\Lottery.Collect.dll

using System.ComponentModel;

namespace Web.MIL.Html
{
  public class HtmlText : HtmlNode
  {
    protected string mText;

    public HtmlText(string text)
    {
      this.mText = text;
    }

    public override string ToString()
    {
      return this.Text;
    }

    public override string HTML
    {
      get
      {
        if (this.NoEscaping)
          return this.Text;
        return HtmlEncoder.EncodeValue(this.Text);
      }
    }

    internal bool NoEscaping
    {
      get
      {
        if (this.mParent == null)
          return false;
        return this.mParent.NoEscaping;
      }
    }

    [Description("The text located in this text node")]
    [Category("General")]
    public string Text
    {
      get
      {
        return this.mText;
      }
      set
      {
        this.mText = value;
      }
    }

    public override string XHTML
    {
      get
      {
        return HtmlEncoder.EncodeValue(this.Text);
      }
    }
  }
}
