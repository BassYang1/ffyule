// Decompiled with JetBrains decompiler
// Type: Lottery.Collect.Config
// Assembly: Lottery.Collect, Version=7.0.1.203, Culture=neutral, PublicKeyToken=null
// MVID: 916E4E87-E8A0-4A21-8438-E89468303682
// Assembly location: F:\pros\tianheng\bf\WebAppOld\bin\Lottery.Collect.dll

namespace Lottery.Collect
{
  public class Config
  {
    private static string _DefaultUrl = "";
    private static string _DefaultUrlYoule = "";

    static Config()
    {
      Config._DefaultUrl = "http://cloud.bmob.cn/5c86c74041efdeb5/hisStory-xml";
      Config._DefaultUrlYoule = "http://cloud.bmob.cn/5c86c74041efdeb5/lottery{0}-xml";
    }

    public static string DefaultUrl
    {
      get
      {
        return Config._DefaultUrl;
      }
      set
      {
        Config._DefaultUrl = value;
      }
    }

    public static string DefaultUrlYoule
    {
      get
      {
        return Config._DefaultUrlYoule;
      }
      set
      {
        Config._DefaultUrlYoule = value;
      }
    }
  }
}
