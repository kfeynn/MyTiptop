//function changeFrameHeight() {
//    var ifm = document.getElementById("menuFrame");
//    //ifm.height = document.documentElement.clientHeight - 56;  //56是头顶的高度
//    ifm.height = document.documentElement.clientHeight - 210 ;
//}
//window.onresize = function () {
//    changeFrameHeight();
//}
//$(function () {
//    changeFrameHeight();
//});
////初始化iframe容器高度

//改为子页面计算父框架高度。

//重新计算父框架的高度
function initIFrameHeight() {
    //alert('initIFrameHeight');
    if (window.parent != window) {
        var ifm = parent.document.getElementById("menuFrame");

        if (ifm != null) {
             //alert('initIFrameHeight-change');

            //重新指定一个固定高度。
            parent.document.getElementById("menuFrame").style.height = "500px";
            //重新指定高度为页面高度+3
            parent.document.getElementById("menuFrame").style.height = (document.body.scrollHeight + 3) + "px";
        }
    }
}
//初始化iframe容器高度
//$(document).ready(function () {
$(function () {
    initIFrameHeight();
});

