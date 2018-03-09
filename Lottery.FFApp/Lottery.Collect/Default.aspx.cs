using Lottery.Collect.Sys;
using Lottery.DAL;
using Lottery.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Lottery.Collect
{
    public partial class Default : System.Web.UI.Page
    {
		protected void Page_Load(object sender, EventArgs e)
		{
            //Win32.SetSystemTime();
            //SysFlb90mData.UpdateData();   
            //SysXdl90mData.UpdateData();
            YouleCheck.RunOfIssueNum(1004, "20180308-2431");
			base.Response.Write("程序已经启动，开奖进行中。" + DateTime.Now.ToString());
			base.Response.End();
		}
    }
}