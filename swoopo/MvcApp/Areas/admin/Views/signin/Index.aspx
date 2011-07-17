<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage" %>
<!DOCTYPE html>
<html>
<head id="Head1" runat="server">
    <!--后台登陆页面-->
    <title>Sign in</title>
    <link type="text/css" rel="Stylesheet" href="<%= Url.Content("~/Content/reset.css") %>" />
    <link type="text/css" rel="Stylesheet" href="<%= Url.Content("~/Content/site.css") %>" />
    <style type="text/css">
        body {
            font-size: 13px;
        }
        input {
            width: 200px;
        }
        input:focus { background-color:#FFFFE1; }
        input:blur { background-color:#FFF; }
        .signin {
            position: absolute;
            left: 40%;
            top: 35%;
        }
        .signin h3 { line-height:2; font-size:15px; font-weight:bold; }
        .signin div {
            padding-bottom: 10px;
        }
        .btn-warp {
            padding-left: 80px;
        }
        table td { padding:5px; }
        #btnSignin { height:25px; line-height:25px; border:1px solid; border-color:#CCC #999 #999 #CCC; background:url(../content/images/bg.png) repeat-x -792px -150px; }
        #btnSignin:hover { border:1px solid #666; }
    </style>

    <script type="text/javascript" src="<%= Url.Content("~/Scripts/jquery-1.4.2.min.js") %>"></script>
</head>
<body>
    <div class="signin">
        <div>
            <h3>Sign In</h3>
        </div>
        <table cellpadding="5" cellspacing="5">
            <tr>
                <td><label for="UserName">User name:</label></td>
                <td>
                    <input type="text" value="" name="UserName" id="UserName" />
                </td>
            </tr>
            <tr>
                <td><label for="UserPwd">Password:</label></td>
                <td>
                    <input type="password" value="" name="UserPwd" id="UserPwd" />
                </td>
            </tr>
            <tr>
                <td></td>
                <td>
                    <button id="btnSignin" type="submit">Sign In</button>
                </td>
            </tr>
        </table>
        <div class="validation-summary-errors btn-warp"></div>
    </div>
    
    <script type="text/javascript">
        var path = "<%= HttpRuntime.AppDomainAppVirtualPath %>";
        jQuery(document).ready(function () {
            jQuery("#UserName").val("admin");
            jQuery("#UserPwd").val("admin");

            jQuery("#UserName").focus();
            //登录事件
            jQuery("#btnSignin").click(function () {
                if (jQuery.trim(jQuery("#UserName").val()) == "") {
                    jQuery(".validation-summary-errors").html("请输入用户名！");
                    jQuery("#UserName").focus();
                    return;
                }
                if (jQuery.trim(jQuery("#UserPwd").val()) == "") {
                    jQuery(".validation-summary-errors").html("请输入密码！");
                    jQuery("#UserPwd").focus();
                    return;
                }

                jQuery.ajax({
                    type: "POST",
                    timeout: 10000,
                    url: path + "/Admin/Signin/Signin",
                    data: "UserName=" + jQuery("#UserName").val() + "&UserPwd=" + jQuery("#UserPwd").val(),
                    success: function (data) {
                        if (data.error) {
                            if (data.error.join) {
                                var error = "<ul><li>" + data.error.join("</li><li>") + "</li></ul>";
                                jQuery(".validation-summary-errors").html(error);
                            }
                            else {
                                jQuery(".validation-summary-errors").html(data.error);
                            }
                            return;
                        }
                        if (data.value) {
                            window.location.href = path + "/Admin/Index";
                        }
                    },
                    error: function (xmlHttpRequest, error, errorThrown) {
                        if (error == "timeout") {
                            error = "连接超时，请检查网络";
                            jQuery(".validation-summary-errors").html(error);
                        }
                    }
                });
            });

            //绑定回车事件
            document.body.onkeydown = function (event) {
                event = window.event || event;
                var keycode = event.keyCode || event.which;
                if (keycode != 13) {
                    return;
                }
                //jQuery("#btnSignin").focus();
                jQuery("#btnSignin").click();
            };
        });
    </script>
</body>
</html>