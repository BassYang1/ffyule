<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="chargeSelf.aspx.cs" Inherits="Lottery.Web.money.chargeSbf" %>

<!doctype html>
<html>
<head>
    <meta charset="utf-8">
    <title>九州娱乐</title>
    <meta name="viewport" content="width=device-width,initial-scale=1.0,user-scalable=no">
    <meta name="format-detection" content="telephone=no,email=no">
    <meta name="apple-mobile-web-app-capable" content="yes">
    <link rel="stylesheet" type="text/css" href="/statics/sytle/css/global.css" />
    <link rel="stylesheet" type="text/css" href="/statics/sytle/css/style.css" />
    <link href="/statics/sytle/css/flipclock.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="/statics/jquery-1.11.3.min.js"></script>
    <script src="/statics/formValidator.js" type="text/javascript"></script>
    <script src="/statics/layer/layer.min.js" type="text/javascript"></script>
    <script type="text/javascript" src="/statics/sytle/js/EM.tools.js"></script>
    <script type="text/javascript" src="/statics/global.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#info").hide();
            $("#add").show();
            _jcms_GetRefreshCode('imgCode');
            $i("mincharge").innerHTML = site.MinCharge;
            $.formValidator.initConfig({
                onerror: function (msg, obj, errorlist) {
                    emAlert(msg);
                },
                onsuccess: function () { return true; }
            });
            $("#txtPayName").formValidator({ tipid: "tipPayName", onshow: "请输入支付宝姓名", onfocus: "请输入支付宝姓名", defaultvalue: "" }).InputValidator({ min: 1, max: 10, onerror: "请输入支付宝姓名" });
            $("#txtPayMoney").formValidator({ tipid: "tipPayMoney", onshow: "请输入充值金额", onfocus: "请输入充值金额", defaultvalue: "" }).InputValidator({ min: 1, max: 10, onerror: "请输入充值金额" }).RegexValidator({ regexp: "^([1-9]{1}[0-9]{0,8})$", onerror: "请输入整数" });
            $("#txtUserCode").formValidator({ tipid: "tipUserCode", onshow: "请输入验证码", onfocus: "验证码必须填写" }).InputValidator({ min: 4, max: 4, onerror: "验证码必须为4位" });
            //金额大写
            $("#txtPayMoney").delegate('', 'keyup', function (event) {
                chkPrice(this);
                $i('moneyUpper').innerHTML = atoc($('#txtPayMoney').val());
            });
            ajaxList();
        });

        function ajaxList() {
            var index = emLoading();
            $.ajax({
                type: "get",
                dataType: "json",
                data: "clienttime=" + Math.random(),
                url: "/ajax/ajaxBank.aspx?oper=ajaxGetChargeSetList&code=commonpay",
                error: function (XmlHttpRequest, textStatus, errorThrown) { emAlert("亲！页面过期,请刷新页面!"); },
                success: function (d) {
                    switch (d.result) {
                        case '-1':
                            top.window.location = '/login.html';
                            break;
                        case '1':
                            if (d.recordcount > 0) {
                                if (d.table[0].isused == "0") {
                                    $i('time1').innerHTML = d.table[0].starttime;
                                    $i('time2').innerHTML = d.table[0].endtime;
                                    $i('mincharge').innerHTML = d.table[0].mincharge;
                                    $i('maxcharge').innerHTML = d.table[0].maxcharge;
                                    //$i('lblPayName').innerHTML = d.table[0].merkey;
                                    $i('lblPayAccount').innerHTML = d.table[0].mercode;
                                    $(".userName").html(getCookie("username"));
                                }
                                else {
                                    $("#add").html("即使充值已关闭，请等待开放！");
                                    $("#form1").hide();
                                }
                            }
                            break;
                    }
                    closeload(index);
                }
            });
        }

        function chkPost() {
            var uMoney = $("#txtPayMoney").val();
            var uCode = $("#txtUserCode").val();
            if ($.formValidator.PageIsValid('1')) {
                if (eval(uMoney) < eval($i("mincharge").innerHTML)) {
                    emAlert('充值金额不能小于最小充值金额');
                    return;
                }
                else if (eval(uMoney) > eval($i("maxcharge").innerHTML)) {
                    emAlert('充值金额不能大于最大充值金额');
                    return;
                }
                else {
                    //获取订单号
                    var index = emLoading();

                    $.ajax({
                        type: "get",
                        dataType: "json",
                        async: false,
                        url: "/ajax/ajaxMoney.aspx?oper=ajaxChargeOrderId&code=" + encodeURIComponent(uCode) + "&clienttime=" + Math.random(),
                        error: function (XmlHttpRequest, textStatus, errorThrown) { emAlert("亲！页面过期,请刷新页面!"); },
                        success: function (d) {
                            if (d.result == "1") {
                                $("#orderId").val(d.returnval); //订单号
                                $("#add").hide();
                                $("#info").show();
                                $i('lblPayMoney').innerHTML = parseFloat($("#txtPayMoney").val()).toFixed(2);

                                $("#payAccount").show();
                                if ($(".bank:checked").val() == "ZFB") {
                                    $(".payType").html("支付宝支付");
                                    $('.recharge-qrcode').html("<img src='../statics/sytle/images/zfbpayQC.jpg' />");
                                    $("#payAccount").hide();
                                }
                                else if ($(".bank:checked").val() == "WX") {
                                    $(".payType").html("微信支付");
                                    $('.recharge-qrcode').html("<img src='../statics/sytle/images/wxpayQC.jpg' />");
                                    $("#payAccount").hide();
                                }
                                else {
                                    $(".payType").html("银行卡支付");
                                    $i('lblPayAccount').innerHTML = "<font color='red'>6217002540004763022</font>";
                                }
                            }
                            else {
                                $("#info").hide();
                                $("#add").show();
                                emAlert(d.returnval);
                                $("#txtPayMoney").val("");
                                $("#txtUserCode").val("");
                                ajaxList();
                                _jcms_GetRefreshCode('imgCode', 28);
                            }
                            closeload(index);
                        }
                    });
                }
            }
        }

        function submitPay() {
            var orderId = $("#orderId").val(); //订单号
            var userId = "<%= this.AdminId %>";
            var bank = $(".bank:checked").val(); //订单号
            var uMoney = $("#txtPayMoney").val();


            if (bank && userId && userId != "0") {
                $.ajax({
                    type: "post",
                    dataType: "json",
                    async: false,
                    data: "bank=" + bank + "&setCode=commonpay&amount=" + uMoney + "&userId=" + userId + "&orderId=" + orderId,
                    url: "/ajax/ajaxMoney.aspx?oper=ajaxCharge1&clienttime=" + Math.random(),
                    error: function (XmlHttpRequest, textStatus, errorThrown) { emAlert(XmlHttpRequest.responseText); },
                    success: function (d) {
                        if (d.result == "1") {
                            emAlert("请通知客服完成充值");

                            window.setTimeout(function () {
                                location.href = "/center.html";
                            }, 3000);
                        }
                        else if (d.result == "0") {
                            emAlert(d.returnval);
                        }
                        else {
                            emAlert("充值失败");
                        }
                    }
                });
            }
        }

        function getHost() {
            var url = location.href;
            var temps = url.split("//");
            if (temps.length > 0) {
                var host = temps[0];
                temps = temps[1].split("/");

                if (temps.length > 0) {
                    return host + "//" + temps[0];
                }
            }

            return "";
        }

        function goTo(url) {
            var targetWndName = "MyWindow";
            var wnd = window.open("", targetWndName);
            var link = document.getElementById("link");
            link.target = targetWndName;
            link.href = url;
            link.click();
        }

        function openwin(url) {
            var a = document.createElement("a");
            a.setAttribute("href", url);
            a.setAttribute("target", "_blank");
            a.setAttribute("id", "openwin");
            document.body.appendChild(a);
            a.click();
        }


        //检查订单状态
        function checkState(orderId) {
            var adminId = "<%=this.AdminId%>";
            var timer;
            var check = function (t) {
                $.ajax({
                    type: "get",
                    dataType: "json",
                    url: "/ajax/ajaxMoney.aspx?oper=ajaxChargeState&userId=" + adminId + "&orderId=" + orderId + "&clienttime=" + Math.random(),
                    error: function (XmlHttpRequest, textStatus, errorThrown) { emAlert("亲！页面过期,请刷新页面!"); },
                    success: function (d) {
                        if (d.result == "1") {
                            //var notice = new Audio("/statics/music/pay.mp3");
                            //notice.play();

                            if (t) {
                                window.clearInterval(t);
                            }

                            //window.setTimeout(function () {
                            //    location.href = "/center.html";
                            //}, 11000);

                            //emAlert(d.returnval);

                            location.href = "/center.html";
                        }
                        else if (d.result == "-1") {
                            if (t) {
                                window.clearInterval(t);
                            }

                            emAlert(d.returnval);
                            //emAlert("亲！页面过期,请刷新页面!");
                        }
                    }
                });
            };

            timer = window.setInterval(function () {
                check(timer);
            }, 1000); //轮询支付状态
        }
    </script>
</head>
<body>
    <div class="container">
        <header id="header">
        <h1 class="title">充&nbsp;&nbsp;&nbsp;值</h1>
        <a href="javascript:history.go(-1);" class="back"></a>
    </header>
        <main id="main">
        <div id="add" class="user-recharge">
        	<div class="account-balance">
            	<i class="icon-money"></i>
                当前余额
                <strong id="money" class="balance">0.00</strong>
                <a href="#" class="refresh"></a>
            </div>
            <form action="" method="post" class="lt-form recharge-form">
                <div class="form-item">
                    <div class="item-title">
                        <label class="lab">充值方式</label>
                    </div>
                    <div class="item-content">
                        <div class="item-content-text">
                            <label><input type="radio" value="ZFB" title="支付宝移动支付" class="radio bank" name="bank" checked/>支付宝</label>
                            <label><input type="radio" value="WX" title="微信移动支付" class="radio bank" name="bank" checked />微信</label>
                            <label><input type="radio" value="CCB" title="银行卡支付" class="radio bank" name="bank" checked/>建设银行</label>
                            <input type="hidden" id="orderId" />
                        </div>
                    </div>
                </div>
                <div class="form-item">
                    <div class="item-title">
                        <label class="lab">充值金额</label>
                    </div>
                    <div class="item-content">
                       <input id="txtPayMoney" type="text" value="" class="ipt" onkeypress="chkPrice(this)" placeholder="请输入充值金额" />
                    </div>
                    <div class="item-extra">
                        元
                    </div>
                </div>
                 <div class="form-item">
                    <div class="item-title">
                        <label class="lab">金额大写</label>
                    </div>
                    <div class="item-content">
                       <span id="moneyUpper"></span>
                    </div>
                </div>
                 <div class="form-item">
                    <div class="item-title">
                        <label class="lab">验证码</label>
                    </div>
                    <div class="item-content">
                        <input id="txtUserCode" type="text" class="ipt" maxlength="4" size="4"  placeholder="请输入验证码" />
                    
                    </div>
                    <img id="imgCode" onclick="_jcms_GetRefreshCode('imgCode');" src="" alt="点击更换" class="code" />
                </div>
                <div class="form-btns">
                     <input type="button" value="下一步" class="btn primary-btn" onclick="chkPost();" />
                    <a id="link" href="javascript:void(0)" style="visibility: hidden; position: absolute;">
                    </a>
                </div>
                 <div class="form-btns">
                开放时间： 每天<font id="time1" style="color: Red;"></font>至<font id="time2" style="color: Red;"></font><br />
                充值限额： 最低<font id="mincharge" style="color: Red;"></font>元，最高<font id="maxcharge" style="color: Red;"></font>元！ 充值不到账请联系客服进行处理！
                </div>
            </form>
        </div>

        <div id="info" class="recharge-apply" style="display:none;">
        	<div class="account-info">
            	<p><span class="k">充值金额：</span><span class="v" id="lblPayMoney"></span></p>
                <p><span class="k">支付方式：</span><span class="v payType"></span></p>
                <p><span class="k">备注账号：</span><span class="v userName"></span></p>
                <p id="payAccount"><span class="k">收款帐号：</span><span class="v" id="lblPayAccount"></span></p>
            </div>
            <div class="recharge-qrcode">
            </div>
            <div class="recharge-countdown">
				<div class="J_RechargeCountdown"></div>
            </div>
            <%--<div class="recharge-tips">
                <p class="p1">请点击确认支付，通知管理员处理</p>
            </div>--%>
            <div class="recharge-bottom">
               <input type="button" value="确认付款" class="btn primary-btn" onclick="submitPay();" />
            </div>
        </div>
    </main>
    </div>
    <script src="/statics/sytle/js/flipclock.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(function () {
            $(".J_RechargeCountdown").FlipClock(1800, {
                clockFace: 'MinuteCounter', countdown: true, callbacks: {
                    stop: function () {
                        //倒计时完成后调用
                    }
                }
            });
        });
    </script>

</body>
</html>
