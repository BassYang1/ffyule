var lotTimes = 0;
//显示开奖信息，时间等
function ajaxLotteryTime() {
    IsKaijiang = false;
    intDiff = 0;
    $.ajax({
        type: "get",
        dataType: "json",
        data: "clienttime=" + Math.random(),
        url:"/ajax/ajax.aspx?oper=ajaxLotteryTime" + lid,
        success: function (data) {
            currsn = data.cursn;
            nestsn = data.nestsn;
            $('#lotteryname').html(data.name);
            $('#nestsn').html(nestsn);
            $('#nestsn2').html(nestsn);
            $('#cursn').html(currsn);
            intDiffClose = parseInt(data.closetime);
            intDiff = parseInt(data.ordertime);
            lotTimes = parseInt(intDiff) - parseInt(intDiffClose); //倒计时 = 开奖时间1分钟 - 封单时间10秒
            diff = parseInt(intDiff) - parseInt(intDiffClose);
//            if (lotTimes < 0) {
//                lotTimes = parseInt(intDiffClose);
//                ajaxLotteryTime();
//            }
            $("#lotOpenNum").html(data.opennum);
            $("#lotNoOpenNum").html(eval($("#lotIssNum").html()) - eval(data.opennum));
            bettingCountdown();
            timer();
        }
    });
}

//开奖倒计时
var bettingCountdown = function () {
    bettingConfirmCountdown();
    var $countdown = $("#betting-countdown");
    var ms = parseInt(lotTimes); //倒计时 = 开奖时间1分钟 - 封单时间10秒
    Util.countdown(ms, true, true, true, function (time) {
        time = time.split(":");
        var str = '';
        for (var i = 0; i < time.length; i++) {
            str += '<span>' + time[i] + '</span>'
        }
        $countdown.html(str);
    });
}

//下注提示开奖倒时间
var bettingConfirmCountdown = function () {
    var $countdown = $("#confirm-countdown");
    var ms = parseInt(lotTimes);
    Util.countdown(ms, true, true, true, function (time) {
        $countdown.text(time);
    });
}

var lotteryResult = function () {
    var $lotteryNumbers = $("#lottery-numbers");
    if ($lotteryNumbers.size() == 0) return;
    var numberArr = $("#lotNumber").val();
    numberArr = (numberArr + "").split(",");
    $lotteryNumbers.children("li").each(function (index) {
        var $item = $(this);
        var numberRun = new NumberRun($item.find(".number-run"), numberArr[index]);
        numberRun.run();
    });
}

var numbers = new Array()
var IsKaijiang = false;
var currsn;
var nestsn;
var intDiff = parseInt(0); //开奖倒计时
var intDiffClose = parseInt(0); //封单倒计时
var diff = 0;
var timers;
function timer() {
    clearInterval(timers);
    timers = window.setInterval(function () {
        //上期结束开始下期
        if (intDiff == 0) {
            ajaxLotteryTime();
        }

        //开始封单计时
        if (intDiffClose != 0) {
            if (intDiff == intDiffClose) {
                lotTimes = parseInt(intDiffClose); //开始封单记时
                bettingCountdown();
                emAlert('第' + nestsn + '期' + "投注已截止");
                audioPlay('fengdan');
            }
        }

        if (intDiff <= intDiffClose) {
            $('#state').html("封单");
            diff = intDiffClose;
        }
        else {
            $('#state').html("投注");
        }

        intDiff--;
        if (--diff == 0) {
            //准备开奖
            window.setTimeout(ajaxKaiJiang1, (intDiffClose + 1) * 1000);
        }

    }, 1000);
}

//开奖
function ajaxKaiJiang1() {
    $.ajax({
        type: "get",
        dataType: "json",
        data: "clienttime=" + Math.random(),
        url: "/ajax/ajax.aspx?oper=GetLotteryNumber1" + lid + lcode,
        success: function (d) {
            if (d.result == "0") {
                console.log(d.returnval);
                return;
            }

            var $lotteryNumbers = $("#lottery-numbers");
            var sn = $('#cursn').html().replace("-", "");

            if (d.returnval.data.length > 0) {
                var lt = d.returnval.data[0];

                if (lt.expect != sn) {
                    $('#numberstate').html("开奖结果");
                    $('#kjLoading').hide();
                    $('#lottery-numbers').show();

                    var code = lt.opencode;
                    var numberAll = code.split(",");
                    var numberArr = code;
                    setCookie("lotNumbers" + LotteryId, numberArr);
                    numberArr = (code + "").split(",");
                    if (numberAll.length >= 20) {
                        $lotteryNumbers.children("li").each(function (index) {
                            var nemberSum = (parseInt(numberAll[4 * index])
                            + parseInt(numberAll[4 * index + 1])
                            + parseInt(numberAll[4 * index + 2])
                            + parseInt(numberAll[4 * index + 3])) + "";
                            numbers[index] = numberAll[4 * index] + "+"
                            + numberAll[4 * index + 1] + "+"
                            + numberAll[4 * index + 2] + "+"
                            + numberAll[4 * index + 3] + "="
                            + nemberSum.substr(0, nemberSum.length - 1) + "<span style='color:Red'>" + nemberSum.substr(nemberSum.length - 1, 1) + "</span>";
                            var $item = $(this);
                            var numberRun = new NumberRun($item.find(".number-run"), numberArr[index]);
                            numberRun.run();
                        });
                    }
                    else {
                        $lotteryNumbers.children("li").each(function (index) {
                            var $item = $(this);
                            var numberRun = new NumberRun($item.find(".number-run"), numberArr[index]);
                            numberRun.run();
                        });
                    }
                    IsKaijiang = true;
                    audioPlay('kaijiang');
                    ajaxLotteryDataList(data);
                    ajaxList();
                    ajaxUserTotalList();
                }
                else {
                    $('#numberstate').html("等待开奖");
                    $('#kjLoading').html(GetNoOpenInfo("正", "在", "开", "奖", "中"));
                    $('#kjLoading').show();
                    $('#lottery-numbers').hide();
                }
            }
            else {
                $('#numberstate').html("等待开奖");
                $('#kjLoading').html(GetNoOpenInfo("正", "在", "开", "奖", "中"));
                $('#kjLoading').show();
                $('#lottery-numbers').hide();
            }
        }

    });
}

//开奖
function ajaxKaiJiang() {
    $.ajax({
        type: "get",
        dataType: "json",
        data: "clienttime=" + Math.random(),
        url: "/ajax/ajax.aspx?oper=GetLotteryNumber" + lid,
        success: function (data) {
            var $lotteryNumbers = $("#lottery-numbers");
            if (data.table.length > 0) {
                if (data.table[0].title == $('#cursn').html()) {
                    $('#numberstate').html("开奖结果");
                    $('#kjLoading').hide();
                    $('#lottery-numbers').show();
                    var numberAll = data.table[0].numberall.split(",");
                    var numberArr = data.table[0].number;
                    setCookie("lotNumbers" + LotteryId, numberArr);
                    numberArr = (numberArr + "").split(",");
                    if (numberAll.length >= 20) {
                        $lotteryNumbers.children("li").each(function (index) {
                            var nemberSum = (parseInt(numberAll[4 * index])
                            + parseInt(numberAll[4 * index + 1])
                            + parseInt(numberAll[4 * index + 2])
                            + parseInt(numberAll[4 * index + 3])) + "";
                            numbers[index] = numberAll[4 * index] + "+"
                            + numberAll[4 * index + 1] + "+"
                            + numberAll[4 * index + 2] + "+"
                            + numberAll[4 * index + 3] + "="
                            + nemberSum.substr(0, nemberSum.length - 1) + "<span style='color:Red'>" + nemberSum.substr(nemberSum.length - 1, 1) + "</span>";
                            var $item = $(this);
                            var numberRun = new NumberRun($item.find(".number-run"), numberArr[index]);
                            numberRun.run();
                        });
                    }
                    else {
                        $lotteryNumbers.children("li").each(function (index) {
                            var $item = $(this);
                            var numberRun = new NumberRun($item.find(".number-run"), numberArr[index]);
                            numberRun.run();
                        });
                    }
                    IsKaijiang = true;
                    audioPlay('kaijiang');
                    ajaxLotteryDataList(data);
                    ajaxList();
                    ajaxUserTotalList();
                }
                else {
                    $('#numberstate').html("等待开奖");
                    $('#kjLoading').html(GetNoOpenInfo("正", "在", "开", "奖", "中"));
                    $('#kjLoading').show();
                    $('#lottery-numbers').hide();
                }
            }
            else {
                $('#numberstate').html("等待开奖");
                $('#kjLoading').html(GetNoOpenInfo("正", "在", "开", "奖", "中"));
                $('#kjLoading').show();
                $('#lottery-numbers').hide();
            }
        }

    });
}

function ajaxLotteryDataList(d) {
    var html = "";
    if (d.table.length > 0) {
        for (var i = 0; i < d.table.length; i++) {
            var t = d.table[i];
            html += "<li><span class='issue'>" + t.title + "</span> ";
            if (Nmbtype != 4) {
                html += "<span class='number'>";
                var strArray = t.number.split(',');
                for (var j = 0; j < strArray.length; j++) {
                    html += " <span class='n'>" + strArray[j] + "</span>";
                }
                html += "</span>";
            }
            else {
                html += "<span class='number'>";
                html += t.number;
                html += "</span>";
            }
            html += "</li>";
        }
    }
    $("#today-betting").html(html);
}