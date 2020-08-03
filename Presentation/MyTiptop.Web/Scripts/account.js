var returnUrl = "/"; //返回地址
var shadowName = ""; //影子账号名

//展示验证错误
function showVerifyError(verifyErrorList) {
    //alert(verifyErrorList);
    if (verifyErrorList != undefined && verifyErrorList != null && verifyErrorList.length > 0) {
        var msg = "";
        for (var i = 0; i < verifyErrorList.length; i++) {
            msg += verifyErrorList[i].msg + "\n";
        }
        alert(msg)
    }
}

//用户登录
function login() {
    var loginForm = document.forms["loginForm"];
    //alert(loginForm);
    //alert(loginForm.action);
    var accountName = loginForm.elements["shadowName"].value;
    var password = loginForm.elements["password"].value;
    var verifyCode = loginForm.elements["verifyCode"] ? loginForm.elements["verifyCode"].value : undefined;
    var isRemember = loginForm.elements["isRemember"] ? loginForm.elements["isRemember"].checked ? 1 : 0 : 0;
    if (!verifyLogin(accountName, password, verifyCode)) {
        return;
    }
    var parms = new Object();
    parms["shadowName"] = accountName;
    parms["password"] = password;
    parms["verifyCode"] = verifyCode;
    parms["isRemember"] = isRemember;
    //$.post("/account/login", parms, loginResponse) 
    //$.post(loginForm.action, parms, loginResponse)
    //alert(loginForm.action);
    // 兼容ie 
    $.ajax({
        //几个参数需要注意一下
        type: "POST",//方法类型
        //dataType: "json",//预期服务器返回的数据类型
        url: loginForm.action , //"/users/login",//url
        data: parms ,  
        success: function (result) {
            loginResponse(result);
        },
        error: function () {
            //alert("异常！");
            showVerifyError(result.content);
        }
    });
}

//验证登录
function verifyLogin(accountName, password, verifyCode) {
    if (accountName.length == 0) {
        alert("请输入帐号名");
        return false;
    }
    if (password.length == 0) {
        alert("请输入密码");
        return false;
    }
    if (verifyCode != undefined && verifyCode.length == 0) {
        alert("请输入验证码");
        return false;
    }
    return true;
}

//处理登录的反馈信息
function loginResponse(data) {
    var result = eval("(" + data + ")");
    if (result.state == "success") {
        //alert(returnUrl);
        var url = result.content;
        //alert(url);
        window.location.href = url;
    }
    else {
        showVerifyError(result.content);
        //alert("error");
    }
}



////重置用户密码
//function resetPwd(v) {
//    var resetPwdForm = document.forms["resetPwdForm"];

//    var password = resetPwdForm.elements["password"].value;
//    var confirmPwd = resetPwdForm.elements["confirmPwd"].value;

//    if (!verifyResetPwd(password, confirmPwd)) {
//        return;
//    }

//    var parms = new Object();
//    parms["password"] = password;
//    parms["confirmPwd"] = confirmPwd;
//    $.post("account/resetpwd?v=" + v, parms, resetPwdResponse)
//}



