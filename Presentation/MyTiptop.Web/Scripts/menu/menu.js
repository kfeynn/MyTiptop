//var menuList = [
//    { "title": "一览表", "subMenuList": [{ "title": "一览表", "url": "/Equipment/EquipmentList" }] },
//]


var g_barcode = null;
var g_length = 0;
var g_type = null;
var g_success = "A";
var p_sfv09 = 0;
var p_fchar = null;
var l_sfv04 = null;
var l_fnum = 1;
var g_putin = 0;
var l_barcode = null;
var g_rvbs04 = null;
var data1 = [
             {}
];

function p620_getdata() {
    //	g_barcode = document.getElementById('barcode').value;
    g_barcode = dwr.util.getValue('barcode');
    g_length = g_barcode.length;
    g_type = null;


    p_fchar = g_barcode.charAt(0);
    l_fnum = 1;
    if (p_fchar == "0")
    {
        p_fchar = g_barcode.charAt(1);
        l_fnum = 2;
    }
    l_barcode = g_barcode.substring(l_fnum, g_length);
    var replay0 = function (data) 
    {
        g_type = data;
        switch (g_type)
        {
            case "L":
                //document.getElementById("requisition").value = l_barcode;
                dwr.util.setValue("requisition", l_barcode);
                var p620_show = function (bsfp620) {
                    data1 = bsfp620;
                    if (data1.length == 0)
                    {
                        alert("未找到此入库单信息或已过账，请检查！");
                        dwr.util.setValue("requisition", "");
                        dwr.util.setValue("barcode", '');
                        return;
                    }
                    $(function () { $('#dg').datagrid('loadData', data1); })
                }
                bsfp620_manager.b620_getdata(l_barcode, p620_show);
                //document.getElementById('barcode').value = '';
                dwr.util.setValue("barcode", '');
                break;

            case "A":
                window.alert(g_type + "未能识别之条码，请确认！");
                break;

            case "C":
                //判断单号值，如果为空，则提醒先扫描单号
                var l_requisition = dwr.util.getValue("requisition");
                var l_employee = dwr.util.getValue("employee");
                if (l_requisition == "")
                {
                    window.alert("请先扫描单号条码！");
                    dwr.util.setValue("barcode", "");
                    document.getElementById('barcode').focus();
                    return;
                }
                if (l_employee == "") 
                {
                    window.alert("请先扫描工号条码！");
                    dwr.util.setValue("barcode", "");
                    document.getElementById('barcode').focus();
                    return;
                }
                //判断储位的值，如果为空，则提醒先扫描储位
                var l_position = dwr.util.getValue('position');
                if (l_position == "") 
                {
                    window.alert("请先扫描储位条码！");
                    dwr.util.setValue("barcode", "");
                    document.getElementById('barcode').focus();
                    return;
                }
                //料号
                l_fnum = 2;
                l_enum = g_length - 10;
                l_sfv04 = g_barcode.substring(l_fnum, l_enum);


                ///////////////zyq 20190611 ST/////////////////
                //获取判断
                var p620_get_tc_brb_rvbs = function (l_number) {
                    if (l_number == "A") {
                        window.alert("该包装票不存在，请检查！");
                        return;
                    }
                    else if (l_number == "W") {
                        window.alert("该包装票已经上架，请检查！");
                        return;
                    }
                    else {
                        //取得该包装票数量
                        var p620_get_tc_brb11 = function (p_number) {
                            if (p_number[0] == "A") {
                                //window.alert("该包装票不存在，请检查！");
                                P620_lock('该包装票不存在');
                                return;
                            }
                            if (p_number[0] == "B") {
                                //window.alert("该包装票未装箱，请检查！");
                                P620_lock('该包装票未装箱');
                                return;
                            }
                            if (p_number[0] == "C") {
                                //window.alert("该包装票未装满箱，请检查！");
                                P620_lock('该包装票未装满箱');
                                return;
                            }
                            if (p_number[0] == "D") {
                                //window.alert("该料号当班次未QC检验，请检查！");
                                P620_lock('该料号当班次未QC检验');
                                return;
                            }
                            if (p_number[0] == "E") {
                                //window.alert("该料号当班次未QC检验，请检查！");
                                P620_lock('该包装票下的结构件条码前缀重复，请检查！');
                                return;
                            }
                            p_sfv09 = parseInt(p_number[1]);
                            if (p_sfv09 > 0) {
                                //如果为客户料号则需要取回厂内料号
                                var p620_get_tc_brb22 = function (p_tc_brb22) {
                                    l_sfv04 = p_tc_brb22;
                                    //更新储位及数量
                                    var ins_boolean = function (p_boolean) {
                                        g_success = p_boolean;
                                        if (g_success == "A") {
                                            //取得 index
                                            var l_rows = $('#dg').datagrid('getRows');
                                            for (i = 0; i < l_rows.length; i++) {
                                                if (l_rows[i]['sfv04'] == l_sfv04) {
                                                    var l_sfv09 = l_rows[i]['sfv09'];
                                                    l_rows[i]['sfv09'] = l_sfv09 - p_sfv09;
                                                    if (l_sfv09 <= 0) {
                                                        $('#dg').datagrid('deleteRow', i);
                                                    }
                                                    //$('#dg').datagrid('acceptChanges');
                                                    $('#dg').datagrid('refreshRow', i);
                                                }
                                            }
                                            dwr.util.setValue("packno", g_barcode);
                                            dwr.util.setValue("barcode", '');
                                        } else {
                                            if (g_success == "W") {
                                                window.alert("该包装票已经上架，请检查！");
                                            } else if (g_success == "X") {
                                                //window.alert("该包装票的数量超出本单数量，请检查！");
                                                P620_lock('该包装票的数量超出本单数量');
                                            } else if (g_success == "U") {
                                                var p620_get_rvbs_sum = function (r_sum) {
                                                    g_putin = r_sum;
                                                    if (g_putin != 0) {
                                                        P620_lock('该包装票还有  ' + g_putin.toString() + '(数量) 未入库，请检查！');
                                                    }
                                                }
                                                Azz_Get.get_rvbs_sum_1(l_requisition, g_barcode, p_sfv09, p620_get_rvbs_sum);
                                            } else if (g_success == "E") {
                                                window.alert("未维护对应存储位置信息，请检查！");
                                            } else {
                                                window.alert("更新不成功！");
                                            }
                                        }
                                    }
                                    Azz_Get.ins_rvbs('asft620', l_requisition, l_sfv04, g_barcode, p_sfv09, l_position, l_employee, ins_boolean);
                                }
                                Azz_Get.get_tc_brb22(g_barcode, p620_get_tc_brb22);
                            }
                            else {
                                P620_lock('包装票不存在或者未装满');
                            }
                        }
                        Azz_Get.get_tc_brb11(g_barcode, 'A', p620_get_tc_brb11);//get_tc_brb11_test
                    }
                }

                Azz_Get.get_tc_brb_rvbs(g_barcode, 'asft620', p620_get_tc_brb_rvbs); 

                ///////////////zyq 20190611 ED/////////////////

                dwr.util.setValue("barcode", '');
                break;
            case "H":
                dwr.util.setValue("employee", l_barcode);
                dwr.util.setValue("barcode", '');
                break;
            case "M":
                //document.getElementById('position').value = l_barcode;
                dwr.util.setValue("position", l_barcode);
                dwr.util.setValue("barcode", '');
                break;
            default:
                dwr.util.setValue("barcode", '');
                window.alert("未能识别之条码，请确认！");
                break;
        }
    }
    Azz_Get.getdata(p_fchar, replay0);
}

function P620_lock(p_msg) {
    p_msg += '，请输入解锁密码！';
    var win = $.messager.prompt('锁屏提示', p_msg, function (r) {
        if (r) {
            var p_lock = function (p_success) {
                if (p_success) {
                    win.window("close");
                } else {
                    alert('您输入的密码有误，请重新输入！');
                }
            }
            Azz_Get.check_pwd('b', r, p_lock);
        }
    });
}

$(function () {
    $('#dg').datagrid({
        data: data1
    });

    document.getElementById("barcode").focus();

});






