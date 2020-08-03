var oldPageSize = 15;
var oldPageNumber = 1;
var delMessage = "您确定要删除吗？";
var batchDelMessage = "您确定要删除全部吗？";
var changeMessage = "您确定要修改吗？";

//表单提交操作
function doPostBack(action) {

    //禁用按钮防止重复提交
    //$(".submit").attr({ "disabled": "disabled" });

    //$('a').removeAttr('href');//去掉a标签中的href属性
    //$('a').removeAttr('onclick');//去掉a标签中的onclick事件
    //$(".dataListEdit .page .bt").prop("onclick", null);
    //$(".dataListEdit .page .bt").removeAttr('onclick');


    //alert($("a").prop());

    var from = $("form:first");
    if (action != "") {
        from.prop("action", action);
    }
    from.get(0).submit();
};


//分类树展开/关闭
function categoryTree(obj, layer) {
    //显示/隐藏 区域
    var id = $(obj).closest('tr').attr('id')
    //选择NAME 的tr 元素
    $("tr[name='" + id + "']").fadeToggle("slow");
    $("tr[name='" + id + "']").css('visibility', 'visible');

    var state = $(obj).prop("class");
    if (state == "open") {
        //更加 + - 图标
        $(obj).removeClass("open").addClass("close");
    }
    else if (state == "close") {
        //更加 + - 图标
        $(obj).removeClass("close").addClass("open");
    }
    //重新计算父框架高度
    initIFrameHeight();
}



$(function () {
    /*页面变量初始化*/
    oldPageSize = $("#pageSize").val();
    oldPageNumber = $("#pageNumber").val();



    //搜索按钮
    $(".submit").click(function () {
        doPostBack("");
        return false;
    });

    //删除按钮
    $(".deleteOperate").click(function () {
        if (!confirm(delMessage)) {
            return false;
        }
    });

    //批量修改
    $(".change").click(function () {
        //if ($("input[type='checkbox'][selectItem='true']:checked").length > 0) {
            if (confirm(changeMessage)) {
                doPostBack($(this).attr("delUrl"));
            }
        //}
        //else {
        //    alert("没有选中任何一项!");
        //}
    })

    //批量删除
    $(".batchDel").click(function () {
        if ($("input[type='checkbox'][selectItem='true']:checked").length > 0) {      
            if (confirm(batchDelMessage)) {
                doPostBack($(this).attr("delUrl"));
            }
        }
        else {
            alert("没有选中任何一项!");
        }
    })



    //ajax删除
    $(".ajaxdeleteOperate").click(function () {
        var ajaxDeleteObj = $(this);
        $.jBox.confirm(delMessage, "提示", function (v, h, f) {
            if (v == 'ok') {
                $.jBox.tip("正在删除...", 'loading');
                $.get(ajaxDeleteObj.attr("url"), function (data, textStatus) {
                    if (data != "0") {
                        ajaxDeleteObj.parents("tr").remove();
                        $.jBox.tip('删除成功！', 'success');
                    } else {
                        $.jBox.error('删除失败，请联系管理员！', '删除失败');
                    }
                });
            }
            else if (v == 'cancel') {
                // 取消
            }

            return true; //close
        });
        return false;
    });

    //表格全选
    $("#allSelect").click(function () {
        $("input[type='checkbox'][selectItem='true']").prop("checked", $(this).prop("checked"));
    });

    /************  显示/隐藏 部分区域  ***/



    //$('.show-details-btn').on('click', function (e) {
    //    e.preventDefault();
    //    var id = $(this).closest('tr').attr('id')
    //    //选择NAME 的tr 元素
    //    $("tr[name='" + id + "']").fadeToggle("slow");
    //    $("tr[name='" + id + "']").css('visibility', 'visible');

    //    //$(this).find(ace.vars['.icon']).toggleClass('fa-angle-double-down').toggleClass('fa-angle-double-up');

    //});

    /************ 分项全选  ***/
    $('.fcheckbox').on('click', function (e) {
        var id = $(this).closest('tr').attr('id')

        //jquery 选择器
        $("tr[name='" + id + "'] input[type='checkbox']").prop("checked", $(this).prop("checked"));
    });


    var oldDisplayOrder = 0;
    //修改排序值
    $(".sortinput").focus(function () {
        var sortInputObj = $(this);
        oldDisplayOrder = sortInputObj.val();
        sortInputObj.val("").prop("class", "selectedsortinput");
    });
    $(".sortinput").blur(function () {
        var sortInputObj = $(this);
        if (sortInputObj.val() == "") {
            sortInputObj.val(oldDisplayOrder)
        }
        else {
            var reg = /^-?\d+$/;
            if (!reg.test(sortInputObj.val())) {
                sortInputObj.val(oldDisplayOrder).prop("class", "selectedsortinput");
                alert("只能输入数字！")
                return;
            }
            else {
                if (oldDisplayOrder != sortInputObj.val()) {
                    $.jBox.tip("正在更新...", 'loading');
                    $.get(sortInputObj.attr("url") + "&displayOrder=" + sortInputObj.val(), function (data, textStatus) {
                        if (data != "0") {
                            $.jBox.tip('更新成功！', 'success');
                        } else {
                            sortInputObj.val(oldDisplayOrder);
                            $.jBox.error('更新失败，请联系管理员！', '更新失败');
                        }
                    });
                }
            }
        }
        sortInputObj.prop("class", "unselectedsortinput");
    });

    //页数按钮
    $(".dataListEdit .page .bt").click(function () {
        $("#pageNumber").val($(this).attr("page"));

        doPostBack("");
        return false;
    });

    //每页显示条数输入框
    $("#pageSize").focus(function () {
        $(this).val("");
    });
    $("#pageSize").blur(function () {
        var value = $(this).val();
        if (value == "") {
            $(this).val(oldPageSize);
        }
        else {
            var regex = /^\d+$/;
            if (!regex.test(value)) {
                alert("只能输入数字!");
                $(this).val(oldPageSize);
            }
            else if (value > 20000) {
                alert("每页不能超过20000条！");
                $(this).val(oldPageSize);
            }
            else if (parseInt(value) != oldPageSize) {
                doPostBack("");
            }
        }
    });

    //跳转到指定页输入框
    $("#pageNumber").focus(function () {
        $(this).val("");
    });
    $("#pageNumber").blur(function () {
        var value = $(this).val();
        if (value == "") {
            $(this).val(oldPageNumber);
        }
        else {
            var regex = /^\d+$/;
            if (!regex.test(value)) {
                alert("只能输入数字!");
                $(this).val(oldPageNumber);
            }
            else {
                var totalPages = $(this).attr("totalPages");
                if (parseInt(value) > parseInt(totalPages)) {
                    alert("跳转页数不能大于" + totalPages);
                    $(this).val(oldPageNumber);
                }
                else if (parseInt(value) != oldPageNumber) {
                    doPostBack("");
                }
            }
        }
    });


});