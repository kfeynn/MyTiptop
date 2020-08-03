var oldPageSize = 15;
var oldPageNumber = 1;
//var delMessage = "您确定要删除吗？";
//var batchDelMessage = "您确定要删除全部吗？";
//var changeMessage = "您确定要修改吗？";

//表单提交操作
function doPostBack(action) {

    //alert("doPostBack");

    var from = $("form:first");
    if (action != "") {
        from.prop("action", action);
    }
    from.get(0).submit();
};

$(function () {
    /*页面变量初始化*/
    oldPageSize = $("#pageSize").val();
    oldPageNumber = $("#pageNumber").val();

    //页数按钮
    $(".pagination li").click(function () {

        if ($(this).prop("className") == "disabled") {
            //禁用按钮不提交
            return;
        }
        else {

            $("#pageNumber").val($(this).children("a").attr("page"));
            // alert($(this).children("a").attr("page"));

            doPostBack("");
            return false;
        }
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