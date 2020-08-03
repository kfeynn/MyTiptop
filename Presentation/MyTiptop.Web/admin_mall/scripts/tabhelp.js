$(function () {
    /*以下代码用于将回车转为Tab键   */
    $(":input").keydown(function (e) {
        //确定键光标向下
        if ((e.keyCode == 13) && ((e.target.type == "text") || (e.target.type == "password") || (e.target.type == "checkbox") || (e.target.type == "file") || (e.target.type == "radio"))) {
            var event = $.event.fix(e); //修正event事件
            var element = event.target; //jQuery统一修正为target
            var buttons = "button,reset,submit,a"; //button格式 
            if (element.nodeName == "INPUT" || element.nodeName == "SELECT" || element.nodeName == "A") {
                event.stopPropagation(); //取消冒泡
                event.preventDefault(); //取消浏览器默认行为
                var inputs = $("input[type!='hidden'][type!='checkbox'][type!='radio'],select,a"); //获取缓存的页面input集合

                var index = inputs.index(element); //当前input位置     
                if (buttons.indexOf(inputs[index + 1].type) == 0) {
                    inputs[index + 1].focus();
                    //inputs[index + 1].click();
                }
                if (buttons.indexOf(inputs[index + 1].type) > 0) {
                    //inputs[index + 1].focus();
                    inputs[index + 1].click();
                }
                else {
                    inputs[index + 1].focus();
                }
            }
        }
    });




});