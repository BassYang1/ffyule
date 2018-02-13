

//检查充值订单
var checkAddFunds = function () {
    var lastDate = getCookie("LastChargeDateAdm");
    lastDate = (lastDate == undefined || lastDate == null) ? "" : lastDate;

    $.ajax({
        type: "get",
        dataType: "json",
        url: "/admin/ajaxMoneyStatAll.aspx?oper=ajaxChargeState&date=" + lastDate + "&clienttime=" + Math.random(),
        error: function (XmlHttpRequest, textStatus, errorThrown) {
            console.log(errorThrown);
        },
        success: function (d) {
            delCookie("LastChargeDateAdm");
            setCookie("LastChargeDateAdm", d.returnval);

            if (d.result != "0") {
                var notice = new Audio("/statics/music/pay.mp3");
                notice.play();
            }
        }
    });
};

var timer = window.setInterval(function () {
    checkAddFunds();
}, 5000); //轮询支付状态