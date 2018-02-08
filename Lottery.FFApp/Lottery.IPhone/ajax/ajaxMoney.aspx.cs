// Decompiled with JetBrains decompiler
// Type: Lottery.IPhone.ajaxMoney
// Assembly: Lottery.IPhone, Version=1.0.1.1, Culture=neutral, PublicKeyToken=null
// MVID: 6A500EAD-7331-41B6-AA69-9D37BCA394DC
// Assembly location: F:\pros\tianheng\bf\Iphone\bin\Lottery.IPhone.dll

using Lottery.DAL;
using Lottery.DAL.Flex;
using Lottery.Utils;
using System;

namespace Lottery.IPhone
{
  public partial class ajaxMoney : UserCenterSession
  {
    private string _operType = string.Empty;
    private string _response = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
      this.Admin_Load("", "json");
      this._operType = this.q("oper");
      switch (this._operType)
      {
        case "ajaxCharge":
          this.ajaxCharge();
          break;
        case "ajaxGetChargeList":
          this.ajaxGetChargeList();
          break;
        case "ajaxCash":
          this.ajaxCash();
          break;
        case "ajaxGetCashList":
          this.ajaxGetCashList();
          break;
        default:
          this.DefaultResponse();
          break;
      }
      this.Response.Write(this._response);
    }

    private void DefaultResponse()
    {
      this._response = this.JsonResult(0, "未知操作");
    }

    private void ajaxCharge()
    {
      string checkCode = this.f("name");
      string str = this.f("money");
      string _code = this.f("code");
      string _realcode = "";
      if (!ValidateCode.CheckValidateCode(_code, ref _realcode))
      {
        this._response = this.JsonResult(0, "验证码错误");
      }
      else
      {
        int num = new Lottery.DAL.Flex.UserChargeDAL().Save(this.AdminId, "1007", checkCode, Convert.ToDecimal(str));
        if (num == -1)
          this._response = this.JsonResult(0, "充值金额不能小于最小充值金额!");
        else if (num > 0)
          this._response = this.JsonResult(1, this.AdminId.ToString());
        else
          this._response = this.JsonResult(0, "充值失败");
      }
    }

    private void ajaxGetChargeList()
    {
      string str1 = this.q("state");
      string str2 = this.q("d1");
      string str3 = this.q("d2");
      int _thispage = this.Int_ThisPage();
      int _pagesize = this.Str2Int(this.q("pagesize"), 20);
      this.Str2Int(this.q("flag"), 0);
      string _wherestr1 = "UserId =" + this.AdminId;
      if (str2.Trim().Length == 0)
        str2 = this.StartTime;
      if (str3.Trim().Length == 0)
        str3 = this.EndTime;
      if (Convert.ToDateTime(str2) > Convert.ToDateTime(str3))
        str2 = str3;
      if (str2.Trim().Length > 0 && str3.Trim().Length > 0)
        _wherestr1 = _wherestr1 + " and STime >='" + str2 + "' and STime <='" + str3 + "'";
      if (!string.IsNullOrEmpty(str1))
        _wherestr1 = _wherestr1 + " and state=" + str1;
      string _jsonstr = "";
      new Lottery.DAL.Flex.UserChargeDAL().GetIphoneListJSON(_thispage, _pagesize, _wherestr1, ref _jsonstr);
      this._response = _jsonstr;
    }

    private void ajaxCash()
    {
      string BankId = this.f("bankId");
      string UserBankId = this.f("userBankId");
      string Money = this.f("money");
      string PassWord = this.f("pass");
      string _code = this.f("code");
      string _realcode = "";
      if (!ValidateCode.CheckValidateCode(_code, ref _realcode))
        this._response = this.JsonResult(0, "验证码错误");
      else
        this._response = this.JsonResult(0, new UserGetCashDAL().UserGetCash(this.AdminId, UserBankId, BankId, Money, PassWord));
    }

    private void ajaxGetCashList()
    {
      string str1 = this.q("d1");
      string str2 = this.q("d2");
      string str3 = this.q("state");
      int _thispage = this.Int_ThisPage();
      int _pagesize = this.Str2Int(this.q("pagesize"), 20);
      this.Str2Int(this.q("flag"), 0);
      string _wherestr1 = "UserId =" + this.AdminId;
      if (str1.Trim().Length == 0)
        str1 = this.StartTime;
      if (str2.Trim().Length == 0)
        str2 = this.EndTime;
      if (Convert.ToDateTime(str1) > Convert.ToDateTime(str2))
        str1 = str2;
      if (str1.Trim().Length > 0 && str2.Trim().Length > 0)
        _wherestr1 = _wherestr1 + " and STime >='" + str1 + "' and STime <='" + str2 + "'";
      if (!string.IsNullOrEmpty(str3))
        _wherestr1 = _wherestr1 + " and State =" + str3;
      string _jsonstr = "";
      new UserGetCashDAL().GetIphoneListJSON(_thispage, _pagesize, _wherestr1, ref _jsonstr);
      this._response = _jsonstr;
    }
  }
}
