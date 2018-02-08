// Decompiled with JetBrains decompiler
// Type: Web.MIL.Html.HtmlAttribute
// Assembly: Lottery.Collect, Version=7.0.1.203, Culture=neutral, PublicKeyToken=null
// MVID: 916E4E87-E8A0-4A21-8438-E89468303682
// Assembly location: F:\pros\tianheng\bf\WebAppOld\bin\Lottery.Collect.dll

using System.ComponentModel;

namespace Web.MIL.Html
{
  public class HtmlAttribute
  {
    protected string mName;
    protected string mValue;

    public HtmlAttribute()
    {
      this.mName = "Unnamed";
      this.mValue = "";
    }

    public HtmlAttribute(string name, string value)
    {
      this.mName = name;
      this.mValue = value;
    }

    public override string ToString()
    {
      if (this.mValue == null)
        return this.mName;
      return this.mName + "=\"" + this.mValue + "\"";
    }

    [Category("Output")]
    [Description("The HTML to represent this attribute")]
    public string HTML
    {
      get
      {
        if (this.mValue == null)
          return this.mName;
        return this.mName + "=\"" + HtmlEncoder.EncodeValue(this.mValue) + "\"";
      }
    }

    [Description("The name of the attribute")]
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
    [Description("The value of the attribute")]
    public string Value
    {
      get
      {
        return this.mValue;
      }
      set
      {
        this.mValue = value;
      }
    }

    [Description("The XHTML to represent this attribute")]
    [Category("Output")]
    public string XHTML
    {
      get
      {
        if (this.mValue == null)
          return this.mName.ToLower();
        return this.mName + "=\"" + HtmlEncoder.EncodeValue(this.mValue.ToLower()) + "\"";
      }
    }
  }
}
