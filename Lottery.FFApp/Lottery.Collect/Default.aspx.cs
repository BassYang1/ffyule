using Lottery.Collect.Sys;
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
            //SysNy30mData.UpdateData();
            //SysTw5fcData.Lottery.LastExpect = "106090131";
            //SysTw5fcData.Lottery.OpenTime = "2018-03-20 23:49:15";
            //SysTw5fcData.UpdateData();
            QqSscData.QqSsc();
			base.Response.Write("程序已经启动，开奖进行中。" + DateTime.Now.ToString());
			base.Response.End();
		}
    }
}