 
//AJAX删除 全检数据中的不良数据
function deletePqAtion(sid)
{
    var r = confirm("您确认删除吗?");
    if (r != true)
        return false;
    $.ajax({
        dataType: "json",
        type: 'post',
        url: '/malladmin/checkdata/SecondDeletePq',
        data: {
            sid: sid //文本输入框的内容
        },
        success: function (data, status) {

            //alert(data.content);
            //alert(status);
            //处理返回结果
            if (status == "success") {
                //查询新的记录并显示
                QueryPqData(data.content);
                //lblMessage.innerText = "删除完毕";
                //alert('删除成功');
            }
            else {
                alert('error!');
            }
        },
        error: function (data) {
            alert('error!');
        }
    });
}
//查询不良数据
function QueryPqData(RecDataId)
{
    $.ajax({
        type: 'post',
        url: '/malladmin/checkdata/SecondGetPqList',
        dataType: "json",
        data: {
            sid: RecDataId //文本输入框的内容
        },
        success: function (data) {
            //处理返回结果
            //eg:
            //删除元素
            $('#fixTr').nextAll("tr").remove();
            //循环添加元素
            for (var j = 0; j < data.content.length; j++) {
                //alert(data.content[j].PqShrtName);
                //创造元素 并 更新值  <td>" + data.content[j].SizeName + "</td>
                var obj = "<tr><td><a class='editOperate' href='#' onclick='deletePqAtion(" + data.content[j].PqDataId + ")'>[删除]</a></td><td>" + data.content[j].PqShrtName + "</td><td>" + data.content[j].PqName + "</td><td>" + data.content[j].RecNum + "</td><td>" + data.content[j].Memo + "</td></tr>";
                //alert(obj);
                //添加！
                $('#fixTable').append(obj);
            }
        },
        error: function (data) {
            alert('error!');
        }
    });
}
//验证表单
function validateForAdd()
{
    $("#msg").html("");
    //JS手写前台验证 (品种、检查员1，2 验证)
    var flag = false;
    var prdtId = 0;
    $.ajax({
        dataType: "json",
        type: 'post',
        url: '/malladmin/basedata/IsExistsPrdtAjax',
        data: {
            prdtCode: $("#prdtCode").val()  //文本输入框的内容
        },
        async: false, //取消异步
        success: function (data, status) {
            prdtId = data.content;
            if (data.content == "0") {
                $("#msg").html("产品不存在");
            }
            else {
                //处理返回结果
                flag = true;
            }
        },
        error: function (data, status) {
            alert('ajax Prdt Error222');
        }
    });

    if ($('#CheckDate').val() == "") {
        flag = false;
        $("#msg").html("日期为空！");
    }
    if ($('#CheckerName1').html() == "") {
        flag = false;
        $("#msg").html("检查员1为空！");
    }
    if ($('#CheckerName2').html() == "")
    {
        flag = false;
        $("#msg").html("检查员2为空！");
    }

    //检验唯一性

    $.ajax({
        dataType: "json",
        type: 'post',
        url: '/malladmin/checkdata/SecondIsExistsAjax',
        data: {
            PrdtId: prdtId,
            CheckDate: $("#CheckDate").val(),
            PrdtLotNo: $("#PrdtLotNo").val(),
            PrdtInfo: $("#PrdtInfo").val(),
            CheckOrderId: $("#CheckOrderId").val(),
        },
        async: false, //取消异步
        success: function (data, status) {
            if (data.content == "1") {
                //处理返回结果
                flag = false;
                $("#msg").html("输入数据相同！");
            }
        },
        error: function (data, status) {
           
            alert('ajax Prdt Error333');
        }
    });

    return Boolean(flag);
}

//验证表单
function validateForEdit() {
    $("#msg").html("");
    //JS手写前台验证 (品种、检查员1，2 验证)
    var flag = false;
    var prdtId = 0;
    $.ajax({
        dataType: "json",
        type: 'post',
        url: '/malladmin/basedata/IsExistsPrdtAjax',
        data: {
            prdtCode: $("#prdtCode").val()  //文本输入框的内容
        },
        async: false, //取消异步
        success: function (data, status) {
            prdtId = data.content;
            if (data.content == "0") {
                $("#msg").html("产品不存在");
            }
            else {
                //处理返回结果
                flag = true;
            }
        },
        error: function (data, status) {
            alert('ajax Prdt Error222');
        }
    });
     
    if ($('#CheckDate').val() == "") {
        flag = false;
        $("#msg").html("日期为空！");
    }
    if ($('#CheckerName1').html() == "") {
        flag = false;
        $("#msg").html("检查员1为空！");
    }
    if ($('#CheckerName2').html() == "") {
        flag = false;
        $("#msg").html("检查员2为空！");
    }

     



    return Boolean(flag);
}



//添加不良信息
function addPqInfo(sid) {
    if (sid == 0) {

        //alert('aa');
        $('#lblMsg').html("请先添加基本信息！");

        return false;
    }
    else {
        //清空提示
        $('#lblMsg').html("");
        //提交添加
        $.ajax({
            type: 'post',
            url: '/malladmin/checkdata/SecondAddPqModel',
            dataType: "json",
            data: {
                sid: sid,
                PqShortName: $('#PqShortName').val(),
                //SizeId: $('#SizeId').val(),
                RecNum: $('#RecNum').val(),
                Memo: $('#Memo').val(),
            },
            success: function (data) {
                if (data.content == "1") {
                    //处理返回结果
                    $('#lblMsg').html("添加完成");
                }
                else {
                    $('#lblMsg').html("请检查输入数据！");
                }
                //查询新的记录并显示
                QueryPqData(sid);
            },
            error: function (data) {
                alert('error!');
            }
        });
        //阻止提交
        //return false;
    }

     
}
//检验不良数量
function checkPqAtion(sid)
{
    if (sid == 0) {
        $('#lblMsg').html("请先添加基本信息！");
        return false;
    }
    else {
        $.ajax({
            type: 'post',
            url: '/malladmin/checkdata/SecondCheckPqNumberAjax',
            dataType: "json",
            data: {
                sid: sid
            },
            success: function (data) {
                if (data.content == "1") {
                    //处理返回结果
                    $('#lblMsg').html("小于主表不良数量！还需要添加");
                }
                else if (data.content == "2") {
                    $('#lblMsg').html("OK！");
                }
                else if (data.content == "3") {
                    $('#lblMsg').html("大于主表不良数量！请查看数据");
                }

            },
            error: function (data) {
                alert('error!');
            }
        });
    }
}

$(function () {
    //不良简称查询不良名称
    $("#PqShortName").blur(function () {

        $.ajax({
            dataType: "json",
            type: 'post',
            url: '/malladmin/basedata/GetPqNameFromShortNameAjax',
            data: {
                sid: $(this).val() //文本输入框的内容
            },
            success: function (data, status) {
                if (status == "success") {
                    //处理返回结果
                    $('#pqName').html(data.content);
                }
            },
            error: function (data, status) {
                $('#pqName').html("");
            }
        });
    });
    
    //检查人员
    $("#CheckerCode1").blur(function () {
        $.ajax({
            dataType: "json",
            type: 'post',
            url: '/malladmin/basedata/GetCheckerNameFromCheckerCodeAjax',
            data: {
                CheckerCode: $(this).val() //文本输入框的内容
            },
            success: function (data, status) {
                if (status == "success") {
                    //处理返回结果
                    $('#CheckerName1').html(data.content);
                }
            },
            error: function (data, status) {
                $('#CheckerName1').html("");
            }
        });
    });
    
    //检查人员
    $("#CheckerCode2").blur(function () {
        $.ajax({
            dataType: "json",
            type: 'post',
            url: '/malladmin/basedata/GetCheckerNameFromCheckerCodeAjax',
            data: {
                CheckerCode: $(this).val() //文本输入框的内容
            },
            success: function (data, status) {
                if (status == "success") {
                    //处理返回结果
                    $('#CheckerName2').html(data.content);
                }
            },
            error: function (data, status) {
                $('#CheckerName2').html("");
            }
        });
    });





    
});