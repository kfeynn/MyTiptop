

//更换皮肤样式
$(function () {
    //alert('ea');
    $('#btn-no-skin').on('click', function (e) {
        //$("#btn-skin-dark").click(function () {
        $('body').removeAttr('class').attr('class', 'no-skin');
        e.preventDefault();
    });

    $('#btn-skin-1').on('click', function (e) {
        $('body').removeAttr('class').attr('class', 'skin-1');
        e.preventDefault();
    });

    $('#btn-skin-2').on('click', function (e) {
        $('body').removeAttr('class').attr('class', 'skin-2');
        e.preventDefault();
    });

    $('#btn-skin-3').on('click', function (e) {
        $('body').removeAttr('class').attr('class', 'skin-3');
        e.preventDefault();
    });
});


$(function () {

    //$('.submenu a').on('click', function (e) {
    $('.nav.nav-list a').on('click', function (e) {

        //更新面包屑路径条
        $('.breadcrumb .active').text($(this).text());
        //导航条选中状态
        $("ul.submenu li.active").removeClass('active');  //删除其他兄弟元素的样式

        $("ul.nav.nav-list li.active").removeClass('active');
        $(this).parent().parent().parent("li").addClass("active");

        $(this).parent().addClass("active");              //添加当前元素的样式   

    });

});





