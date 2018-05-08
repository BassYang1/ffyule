using Lottery.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Lottery.Collect
{
    public partial class Test : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var result = CheckSSC_5Start.P_5TS("0,8,3,3,0", "0_8", 2);
            Response.Write(result);
        }
    }
}