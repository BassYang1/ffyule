using Lottery.Collect.Sys;
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
            //QqSscData.QqSsc();
            //YouleToOther.DataToOther(1011);
            //while (true)
            //{
            //    SysXdl90MData.UpdateData();
            //    Thread.Sleep(5000);
            //}

			base.Response.Write("程序已经启动，开奖进行中。" + DateTime.Now.ToString());
			base.Response.End();
		}
    }
}