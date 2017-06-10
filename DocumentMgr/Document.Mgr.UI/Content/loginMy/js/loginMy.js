
//获取所有input
var email = document.getElementById("email");
var pwd = document.getElementById("pwd");
var vcode = document.getElementById("vcode");


//获取所有span
var email_info = document.getElementById("email_info");
var pwd_info = document.getElementById("pwd_info");
var vcode_info = document.getElementById("vcode_info");

function checkEmail(){
    if(!email.value){
        email_info.style.fontSize = "13px";
        email_info.style.color = "#CC3333";
        email_info.style.width="31%";
        email_info.style.height="2em";
        email_info.style.textAlign="center";
        email_info.style.lineHeight="2em";
        email_info.style.marginTop='0.05%'
        email_info.innerHTML = '请输入邮箱!';
        email_info.style.display="block";
    }
    else if(email.value){
        email_info.style.display="none";
    }
}

//密码
function checkPwd(){
    if(!pwd.value){
        pwd_info.style.fontSize = "13px";
        pwd_info.style.color = "#CC3333";
        pwd_info.style.width="31%";
        pwd_info.style.height="2em";
        pwd_info.style.textAlign="center";
        pwd_info.style.lineHeight="2em";
        pwd_info.innerHTML = '请输入密码！';
        pwd_info.style.display="block";
    }
    else{
        pwd_info.innerHTML ='';
        pwd_info.style.backgroundColor= "#fff";
        pwd_info.style.border="none";
        pwd_info.style.display="none";

    }

}

//验证码
function checkVCode() {
    if(!vcode.value){
        vcode_info.style.fontSize = "13px";
        vcode_info.style.color = "#CC3333";
        vcode_info.style.width="31%";
        vcode_info.style.height="2em";
        vcode_info.style.textAlign="center";
        vcode_info.style.lineHeight="2em";
        vcode_info.innerHTML = '请输入验证码！';
        vcode_info.style.display="block";
    }

    else{
        vcode_info.innerHTML ='';
        vcode_info.style.backgroundColor= "#fff";
        vcode_info.style.border="none";
        vcode_info.style.display="none";
    }
}

function mySubmit() {

    if(!email.value){
        email_info.style.fontSize = "13px";
        email_info.style.color = "#CC3333";
        email_info.style.width="31%";
        email_info.style.height="2em";
        email_info.style.textAlign="center";
        email_info.style.lineHeight="2em";
        email_info.innerHTML = '请填写邮箱！';
        email.focus();
        return false;
    }
    if(!pwd.value){
        pwd_info.style.fontSize = "13px";
        pwd_info.style.color = "#CC3333";
        pwd_info.style.width="31%";
        pwd_info.style.height="2em";
        pwd_info.style.textAlign="center";
        pwd_info.style.lineHeight="2em";
        pwd_info.innerHTML = '请填写用户密码！';
        pwd.focus();
        return false;
    }

    if(!vcode.value){
        vcode_info.style.fontSize = "13px";
        vcode_info.style.color = "#CC3333";
        vcode_info.style.width="31%";
        vcode_info.style.height="2em";
        vcode_info.style.textAlign="center";
        vcode_info.style.lineHeight="2em";
        vcode_info.innerHTML = '请填写验证码！';
        vcode_info.focus();
        return false;
    } 
     return true;
}


