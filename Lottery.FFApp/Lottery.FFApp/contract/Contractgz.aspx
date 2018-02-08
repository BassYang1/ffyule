<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Contractgz.aspx.cs"
    Inherits="Lottery.WebApp.contract.Contractgz" %>

<!doctype html>
<html>
<head>
    <meta charset="utf-8">
    <meta http-equiv="Content-Type" content="text/html;charset=utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />
    <meta name="renderer" content="webkit" />
    <title>非凡娱乐</title>
    <link rel="stylesheet" type="text/css" href="/statics/css/common.css" />
    <link rel="stylesheet" type="text/css" href="/statics/css/member.css" />
    <script src="/statics/jquery-1.11.3.min.js" type="text/javascript"></script>
    <script src="/statics/global.js" type="text/javascript"></script>
    <script src="/statics/layer/layer.js" type="text/javascript"></script>
    <script src="/statics/js/EM.tools.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            ajaxGetList();
        });

        function ajaxGetList() {
            $("#btnAgree").hide();
            $("#btnRefuse").hide();
            $("#btnRefuseCannel").hide();
            $("#btnAgreeCannel").hide();
            $.ajax({
                type: "get",
                dataType: "json",
                data: "id=<%=AdminId %>&clienttime=" + Math.random(),
                url: "/ajax/ajaxContractGZ.aspx?oper=GetContractInfo",
                error: function (XmlHttpRequest, textStatus, errorThrown) { alert(XmlHttpRequest.responseText); },
                success: function (d) {
                    var html = "";
                    if (d.table.length > 0) {
                        html += '<table class="query-table">';
                       html += ' <colgroup>';
                       html += '<col class="w50"/>';
                       html += '<col class="w150"/>';
                       html += '<col class="w150"/>';
                       html += '<col class="w500"/>';
                       html += '</colgroup>';

                        html += '<thead><tr>';
                        html += '<th>编号</th>';
                        html += '<th>契约条件</th>';
                        html += '<th>契约比例</th>';
                        html += '<th>&nbsp;</th>';
                        html += '</tr></thead>';
						html += '<tbody>';
                        for (var i = 0; i < d.table.length; i++) {
                            var t = d.table[i];
                                    html += '<tr>';
                                    html += '<td>' + (i + 1) + '</td><td><label class="lab">每日销量</label><label class="lab">' + t.minmoney + '万</label></td><td><label class="lab">' + t.money + '%</label></td><td>&nbsp;</td>';
									html += '</tr>';
                                }
                                html += '</tbody>';
                                html += '</table>';
                        if (d.table[0].groupid == 4) {
                            $i("info").innerHTML = "您是招商级别，直接用平台工资标准";
                            $("#add").html(html);
                        }
                        else if (d.table[0].groupid == 3) {
                            $i("info").innerHTML = "您是特权直属级别，直接用平台工资标准";
                            $("#add").html(html);
                        }
                        else if (d.table[0].groupid == 2) {
                            $i("info").innerHTML = "您是直属级别，直接用平台工资标准";
                            $("#add").html(html);
                        }
                        else {
                            if (d.table[0].groupid >= 5) {
                                $i("info").innerHTML = "您的级别不参与契约";
                            }
                            else {
                                if (d.table[0].isused == 0) {
                                    $i("info").innerHTML = "契约待接受，请您确认";
                                    $("#btnAgree").show();
                                    $("#btnRefuse").show();
                                }
                                if (d.table[0].isused == 1) {
                                    $i("info").innerHTML = "契约已签订";
                                }
                                if (d.table[0].isused == 2) {
                                    $i("info").innerHTML = "契约已拒绝，请联系上级重新分配";
                                }
                                if (d.table[0].isused == 3) {
                                    $i("info").innerHTML = "上级要求撤销契约，请您确认";
                                    $("#btnRefuseCannel").show();
                                    $("#btnAgreeCannel").show();
                                }
                                if (d.table[0].isused == 4) {
                                    $i("info").innerHTML = "您已同意撤销契约，请等待上级重新分配";
                                }
                                $("#add").html(html); 
                            }
                        }
                    }
                    else {
						$i("info").innerHTML = "契约未分配，请联系上级！";
                    }
                }
            });
        }

        function ajaxUpdate(state) {
            var index = emLoading();
            $.ajax({
                type: "post",
                dataType: "json",
                url: "/ajax/ajaxContractGZ.aspx?oper=UpdateContractState&clienttime=" + Math.random(),
                data: "state=" + state,
                error: function (XmlHttpRequest, textStatus, errorThrown) { alert(XmlHttpRequest.responseText); },
                success: function (d) {
                    switch (d.result) {
                        case '0':
                            emAlert(d.message);
                            break;
                        case '1':
                            ajaxGetList();
                            break;
                    }
                    closeload(index);
                }
            });
        }
    </script>
</head>
<body>
 <div class="query-area">
        <div class="query-form">
            <form id="ajaxInput" action="" method="post">
            <div class="query-date"> <span id="info" class="info"></span></div>
			<div class="btn-group" style="float:right; margin-right:-10px; ">
                <input id="btnAgree" type="button" value="同意契约" onclick="ajaxUpdate(1)" class="btn btn-bg btn-primary" />
			    <input id="btnRefuse" type="button" value="拒绝契约" onclick="ajaxUpdate(2)" class="btn btn-bg btn-primary" />
                <input id="btnRefuseCannel" type="button" value="拒绝撤销" onclick="ajaxUpdate(1)" class="btn btn-bg btn-primary" />
			    <input id="btnAgreeCannel" type="button" value="同意撤销" onclick="ajaxUpdate(4)" class="btn btn-bg btn-primary" />
            </div>
            </form>
        </div>
        <div id="add" class="query-result" style="width: 100%"></div>
    </div>
</body>
</html>