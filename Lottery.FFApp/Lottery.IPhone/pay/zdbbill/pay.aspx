<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="pay.aspx.cs" Inherits="Lottery.IPhone.ZDB.pay" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>智得宝支付</title>
    <script src="../../statics/js/jquery.min.js"></script>
</head>
<body>
    <% 
        if (this.IsSuccess)
        {
    %>
    
    <iframe id="payForm" style="display:none;"></iframe>
    <div id="resultmsg"></div>
    <h1>支付成功。<span id="sec">10</span>秒后自动关闭页面...</h1>
    <script>
        var tm1;

        $(function () {

            $("#payForm").attr("src", "<%=this.PayUrl %>");
            tm1 = window.setInterval(checkState, 1000);
        });

        function checkState() {
            var adminid = "<%=this.UserId%>";
            var orderid = "<%=this.OrderId%>";
            $.ajax({
                type: "get",
                dataType: "json",
                url: "/ajax/ajaxMoney.aspx?oper=ajaxChargeState&userId=" + adminId + "&orderId=" + orderId + "&clienttime=" + Math.random(),
                error: function (XmlHttpRequest, textStatus, errorThrown) { alert(XmlHttpRequest.responseText); },
                success: function (d) {
                    if (d.result == "1") {                        
                        $("#resultmsg").html("支付成功，5秒后关闭页面。");
                        closePage();

                        if (tm1) {
                            window.clearInterval(tm1);
                        }
                    }
                    else if (d.result == "-1") {
                        $("#resultmsg").html("d.returnval");

                        if (tm1) {
                            window.clearInterval(tm1);
                        }
                    }
                }
            });
        }

        function closePage() {
            var seconds = 5;
            var timer = window.setInterval(function () {
                seconds--;

                if (seconds <= 0) {
                    window.clearInterval(timer);
                    window.opener = null;
                    window.open('', '_self');
                    window.close();
                }
                else {
                    $("#sec").html(seconds);
                }
            }, 1000);
        }
    </script>
    <% 
        }
        else
        {
    %>
    <h1><%=this.Message %></h1>
    <% 
        }
    %>
</body>
</html>
