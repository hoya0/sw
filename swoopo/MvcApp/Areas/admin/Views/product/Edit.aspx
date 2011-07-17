<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage" ValidateRequest="false" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <title>edit.aspx</title>
    <link type="text/css" rel="Stylesheet" href="<%= Url.Content("~/Content/reset.css") %>" />
    <link type="text/css" rel="Stylesheet" href="<%= Url.Content("~/Content/site.css") %>" />
    <style type="text/css">
        .form-title { width: 100px; }
        .form-input { width:200px; }
    </style>
    <script type="text/javascript" src="<%= Url.Content("~/scripts/jquery-1.4.2.min.js") %>"></script>
    <script type="text/javascript" src="<%= Url.Content("~/scripts/WdatePicker.js") %>"></script>
    <script type="text/javascript" src="<%= Url.Content("~/ckeditor/ckeditor.js") %>"></script>
    <script type="text/javascript" src="<%= Url.Content("~/content/js/common.js") %>"></script>
</head>
<body>
    <div>
        <table cellpadding="5" cellspacing="5">
            <tr>
                <td class="form-title">
                    产品名称：
                </td>
                <td class="">
                    <%= Html.TextBox("proName", "", new { @class = "form-input" })%>
                </td>
                <td class="form-title">
                    选择分类：
                </td>
                <td>
                    <%= Html.DropDownList("Cateories")%>
                </td>
            </tr>
            <tr>
                <td class="form-title">
                    开始时间：
                </td>
                <td>
                    <input type="text" class="Wdate form-input" id="iptSt" />
                </td>
                <td class="form-title">
                    结束时间：
                </td>
                <td>
                    <input type="text" class="Wdate form-input" id="iptEt" />
                </td>
            </tr>
            <tr>
                <td class="form-title">
                    时长：
                </td>
                <td>
                    <input id="remindTime" type="text" readonly="readonly" class="form-input" />
                </td>
                <td class="form-title">
                </td>
                <td>
                    <div class="chk clear">
                        <input type="checkbox" name="chkStatus" id="chkStatus" checked="checked" />
                        <label for="chkStatus">启用</label>
                    </div>
                </td>
            </tr>
        </table>
        <table>
            <tr>
                <td class="form-textarea-title form-title">
                    备注：
                </td>
                <td colspan="3">
                    <textarea id="txtEditor" rows="5" cols="10"></textarea>
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td>
                    <button class="button" onclick="javascript:ProEdit.save();">
                        保存</button>
                    <button class="button" onclick="">
                        取消</button>
                </td>
            </tr>
        </table>
    </div>
    <div id="error"></div>
    <script type="text/javascript" src="<%= Url.Content("~/content/js/admin-pro.js") %>"></script>
</body>
</html>
