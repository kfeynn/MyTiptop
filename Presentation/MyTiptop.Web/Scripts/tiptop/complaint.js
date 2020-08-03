
$(function () {
    //1.初始化input控件
    var oFileInput = new FileInput();
    oFileInput.Init("txt_file", "/Security/Update");

    ////2.注册导入按钮的事件
    //$("#btn_import").click(function () {
    //    $("#myModal").modal();
    //});
});

//额外参数
var formdata = {};

//初始化fileinput
var FileInput = function () {
    var oFile = new Object();

    //初始化fileinput控件（第一次初始化）
    oFile.Init = function (ctrlName, uploadUrl) {
        var control = $('#' + ctrlName);

        //初始化上传控件的样式
        control.fileinput({
            language: 'zh', //设置语言
            uploadUrl: uploadUrl, //上传的地址
            uploadAsync: true, //默认异步上传
            allowedFileExtensions: ['jpg', 'gif', 'png'],//接收的文件后缀
            showUpload: true, //是否显示上传按钮
            showCaption: false,//是否显示标题
            browseClass: "btn btn-primary", //按钮样式     
            //dropZoneEnabled: false,//是否显示拖拽区域
            //minImageWidth: 50, //图片的最小宽度
            //minImageHeight: 50,//图片的最小高度
            //maxImageWidth: 1000,//图片的最大宽度
            //maxImageHeight: 1000,//图片的最大高度
            //maxFileSize: 0,//单位为kb，如果为0表示不限制文件大小
            minFileCount: 0,
            maxFileCount: 100,
            enctype: 'multipart/form-data',
            validateInitialCount: true,
            //previewFileIcon: "<i class='glyphicon glyphicon-king'></i>",
            layoutTemplates: {
                // actionDelete:'', //去除上传预览的缩略图中的删除图标  
                actionUpload: '',//去除上传预览缩略图中的上传图片；  
                actionZoom: ''   //去除上传预览缩略图中的查看详情预览的缩略图标。  
            },
            msgFilesTooMany: "选择上传的文件数量({n}) 超过允许的最大数值{m}！",
            //browseOnZoneClick: true,   //单击上传 ,无效
            //dropZoneTitle: "上传图片640 * 350分辨率效果更好",
            // 这个配置就是解决办法了,初始化时限制图片大小
            previewSettings: {
                image: {width: "160px",height:"160px"},
            },
            //uploadExtraData: function (previewId, index) {
            //    //向后台传递id作为额外参数，是后台可以根据id修改对应的图片地址。
            //    var obj = {};
            //    obj.id = fishId;
            //    return obj;
            //}
            uploadExtraData: function (previewId, index) {   //额外参数 返回json数组  
                return formdata
            }
        });

        //// 上传成功回调
        //$("#input-ke-2").on("filebatchuploadcomplete", function () {
        //    layer.msg("上传附件成功");
        //    setTimeout("closeUpladLayer()", 2000)
        //});
        //// 上传失败回调
        //$('#input-ke-2').on('fileerror', function (event, data, msg) {
        //    layer.msg(data.msg);
        //    tokenTimeOut(data);
        //});



        //导入文件上传完成之后的事件
        $("#txt_file").on("fileuploaded", function (event, data, previewId, index) {
            alert("fileuploaded");

            //$("#txt_file").fileinput('clear'); //只是清理还未上传的预览，上传成功的不会清除

            //上传完刷新页面
            //window.location.reload(); //? , 
            //转向页面2，显示刚才提交表单，由后台转向比较好吧。。。。

            //上传完重新初始化
            //var oFileInput = new FileInput();
            //oFileInput.Init("txt_file", "/Security/Update");

            //$("#myModal").modal("hide");
            //var data = data.response.lstOrderImport;
            //alert(data);

            //if (data == undefined) {
            //    //toastr.error('文件格式类型不正确');
            //    return;
            //}
            ////1.初始化表格
            //var oTable = new TableInit();
            //oTable.Init(data);

           
            //$("#div_startimport").show();
        });
    }
    return oFile;
};