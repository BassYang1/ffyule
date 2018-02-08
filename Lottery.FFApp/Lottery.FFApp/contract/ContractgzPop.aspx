<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ContractgzPop.aspx.cs"
    Inherits="Lottery.WebApp.contract.ContractgzPop" %>

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
        var count = 0;
        $(document).ready(function () {
            ajaxGetList();
        });
        
        var DataFH="";
        function ajaxGetList()
        {
            $("#btnAdd").hide();
            $("#btnEdit").hide();
            $("#btnCannel").hide();
            $("#btnSave").hide();
             $.ajax({
                    type: "get",
                    dataType: "json",
                    data: "id=<%=userId %>&clienttime=" + Math.random(),
                    url: "/ajax/ajaxContractGZ.aspx?oper=GetContractInfo",
                    error: function (XmlHttpRequest, textStatus, errorThrown) { alert(XmlHttpRequest.responseText); },
                    success: function (d) {
                    
                    var html="";
                     if (d.table.length > 0) {
                        DataFH=d;
                        for (var i = 0; i < d.table.length; i++) {
                            var t = d.table[i];
                            html+='<div class="input-group"><label class="lab">0'+(i + 1)+'.每日销量</label><label class="lab">'+t.minmoney+'万</label><label class="lab">，日结工资'+t.money+'%</label></div>';
                        }
                        if (d.table[0].groupid == 4) {
                            $i("info").innerHTML = "该会员是招商级别，直接用平台工资标准";
							$("#add").html(html);
                        }
                        else if (d.table[0].groupid == 3) {
                            $i("info").innerHTML = "该会员是特权直属级别，直接用平台工资标准";
                            $("#add").html(html);
                        }
                         else if (d.table[0].groupid == 2) {
                            $i("info").innerHTML = "该会员是直属级别，直接用平台工资标准";
                            $("#add").html(html);
 
                        }
                        else {
                            if (d.table[0].groupid >= 5) {
                                $i("info").innerHTML="您的级别不参与契约";
                            }
                            else
                            {
                                maxAdminPer=1;
                                    if(d.table[0].isused==0)
                                    {
                                         $i("info").innerHTML="契约待接受";
                                         $("#add").html(html);
                                         $("#btnEdit").show();
                                    }
                                    if(d.table[0].isused==1)
                                    {
                                         $i("info").innerHTML="契约已签订";
                                         $("#add").html(html);
                                         $("#btnCannel").show();
                                    }
                                    if(d.table[0].isused==2)
                                    {
                                        $i("info").innerHTML="契约已拒绝，可重新分配";
                                        $("#add").html(html);
                                        $("#btnEdit").show();
                                    }
                                    if(d.table[0].isused==3)
                                    {
                                        $i("info").innerHTML="契约撤销，等待会员同意！";
                                        $("#add").html(html);
                                    }
                                    if(d.table[0].isused==4)
                                    {
                                        $i("info").innerHTML="会员同意撤销，请您修改契约！";
                                        $("#add").html(html);
                                        $("#btnEdit").show();
                                    }
                              }
                        }
                       }
                       else{
                            $i("info").innerHTML = "契约未分配，请您分配！";
							$("#btnAdd").show();
                       }
                    }
                });
        }

        function UpdateFrom() {
            $("#btnEdit").hide();
            $("#btnAdd").show();
            $("#btnSave").show();
            $("#add").html("");
            count=DataFH.table.length;
            for (var i = 0; i < DataFH.table.length; i++) {
                var t = DataFH.table[i];
                var div = document.createElement('div');
                div.className = 'input-group';

                var label = document.createElement('label');
                label.className = 'lab';
                label.innerHTML = '0' + (i+1) + '.每日销量';

                var input = document.createElement('input');
                input.className = 'ipt';
                input.id = "money_" + i;
                input.value=t.minmoney;

                var label2 = document.createElement('label');
                label2.className = 'lab';
                label2.innerHTML = '万，日工资';

                var select = document.createElement('select');
                select.id = "per_" + i;
                select.className = 'select';

                var img = document.createElement('img');
                img.id = "img_" + i;
                img.src = '/statics/img/icon_lot_del.png';
                img.className = 'img';
                img.onclick = function () {
                    document.getElementById("add").removeChild(div);
                    $("#img_" + (i)).show();
                }

                div.appendChild(label);
                div.appendChild(input);
                div.appendChild(label2);
                div.appendChild(select);
                div.appendChild(img);
                document.getElementById('add').appendChild(div);
                PerBindUpdate("per_" + i, parseFloat(t.money).toFixed(2));
                $("#per_" + i).val(parseFloat(t.money).toFixed(2));
                //$("#per_" + i).attr("disabled",true);
             }
        }

        function AddFrom() {
            $("#btnSave").show();
             if(maxPer="undefined")
				maxPer="0";
            if (count < 10&&parseFloat(maxPer)<10) {
                //使用createElement创建元素
                var div = document.createElement('div');
                div.className = 'input-group';

                var label = document.createElement('label');
                label.className = 'lab';
                label.innerHTML = '0' + count + '.每日销量';

                var input = document.createElement('input');
                input.className = 'ipt';
                input.id = "money_" + count;

                var label2 = document.createElement('label');
                label2.className = 'lab';
                label2.innerHTML = '万，日工资';

                var select = document.createElement('select');
                select.id = "per_" + count;
                select.className = 'select';
                var img = document.createElement('img');
                img.id = "img_" + count;
                img.src = '/statics/img/icon_lot_del.png';
                img.className = 'img';
                img.onclick = function () {
                    count--;
                    document.getElementById("add").removeChild(div);
                    $("#img_" + (count-1)).show();
                    $("#per_" + (count-1)).attr("disabled",false);
                    maxPer = parseFloat($("#per_" + (count-1)).val())*10;
                }

                div.appendChild(label);
                div.appendChild(input);
                div.appendChild(label2);
                div.appendChild(select);
                div.appendChild(img);
                document.getElementById('add').appendChild(div);
                PerBind("per_" + count);
                for (var i = 0; i < count; i++) {
                    $("#img_" + i).hide();
                    $("#per_" + i).attr("disabled",true);
                }
            }
            count++;
        }

        var SelectedData = [];
        function ajaxView() {
            SelectedData.splice(0, SelectedData.length);
            for (var i = 0; i <= count; i++) {
                var money = $("#money_" + i).val();
                var per = $("#per_" + i).val();
                if (money != "undefined" && parseFloat(money) >=0 && per!=null) {
                    var json1 = {
                        "userid": <%=userId %>,
                        "money": money,
                        "per": per
                    };
                    SelectedData.push(json1);
                }
            }
            var arrzh = JSON.stringify(SelectedData);
            $.ajax({
                    type: "post",
                    dataType: "json",
                    data: arrzh,
                    async: false,
                    url: "/ajax/ajaxContractGZ.aspx?oper=ajaxSaveContract&clienttime=" + Math.random(),
                    error: function (XmlHttpRequest, textStatus, errorThrown) { emAlert("亲！页面过期,请刷新页面!"); },
                    success: function (d) {
                        switch (d.result) {
                            case '0':
                                emAlert(d.returnval);
                                break;
                            case '1':
                                ajaxGetList();
//                                var index = layer.getFrameIndex(window.name);
//                                parent.ajaxSearch();
//                                parent.layer.close(parent.layer.getFrameIndex(window.name));
                                break;
                        }
                    }
                });
		}

        var maxPer=0;
        function PerBind(obj) {
            for (var k = 0; k <= count; k++) {
                var per = $("#per_" + k).val();
                if (per != "undefined" && per!=null)
                {
                    if(parseFloat(per)*10>parseFloat(maxPer)){
					    maxPer=parseFloat(per*10).toFixed(2);
					}
                }
            }
            var str = '';
			for(var i=parseInt(maxPer)+1;i<=parseInt(<%=maxAdminPer %>);i++)
            {
                if(i>parseFloat(maxPer))
                {
                    str += '<option value="'+parseFloat(i*0.1).toFixed(2)+'">'+parseFloat(i*0.1).toFixed(2)+'%</option>';
                }
            }
            $('#' + obj).html(str);
        }

        function PerBindUpdate(obj,per) {
            var str = '';
            for(var i=1;i<=parseInt(<%=maxAdminPer %>);i++)
            {
                if(i>parseInt(maxPer))
                {
                    str += '<option value="'+parseFloat(i*0.1).toFixed(2)+'">'+parseFloat(i*0.1).toFixed(2)+'%</option>';
                }
            }
            $('#' + obj).html(str);
        }

        function ajaxUpdate(state) {
            var index = emLoading();
            $.ajax({
                type: "post",
                dataType: "json",
                url: "/ajax/ajaxContractGZ.aspx?oper=UpdateContractStateUserId&clienttime=" + Math.random(),
                data: "state=" + state+"&userid="+<%=userId %>,
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
    <div class="tto-popup">
        <div class="popup-body">
        <form id="form1" class="tto-form2">
        <div class="input-group">
           <span id="info" class="info"></span>
        </div>
        <div id="add" style="height:350px;">
        </div>
         <div class="btn-group">
            <input id="btnAdd" type="button" value="添加规则" onclick="AddFrom()" class="btn btn-bg btn-primary" />
            <input id="btnEdit" type="button" value="编辑契约" onclick="UpdateFrom()" class="btn btn-bg btn-primary" />
            <input id="btnCannel" type="button" value="撤销契约" onclick="ajaxUpdate(3)" class="btn btn-bg btn-primary" />
            <input id="btnSave" type="button" value="确定提交" onclick="ajaxView()" class="btn btn-bg btn-primary" />
        </div>
        </form>
        </div>
    </div>
</body>
</html>
