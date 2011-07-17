<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage" %>
<% Swoopo.Model.CategoryEntity category = ViewData["categoryItem"] as Swoopo.Model.CategoryEntity; %>
<!DOCTYPE html>
<html>
<head id="Head1" runat="server">
    <title>CategoryManager</title>
    <link type="text/css" rel="Stylesheet" href="<%= Url.Content("~/Content/reset.css") %>" />
    <link type="text/css" rel="Stylesheet" href="<%= Url.Content("~/Content/site.css") %>" />
    <script type="text/javascript" src="<%= Url.Content("~/scripts/jquery-1.4.2.min.js") %>"></script>
</head>
<body>
    <div>
        <h2>
            CategoryManager</h2>
        <table cellpadding="5" cellspacing="5">
            <tr>
                <td class="form-title">
                    父级分类：
                </td>
                <td>
                    <%= Html.DropDownList("Cateories")%>
                </td>
            </tr>
            <tr>
                <td class="form-title">
                    分类名称<span class="required">*</span>：
                </td>
                <td>
                    <%= Html.TextBox("CategoryName", category.CategoryName, new { @class = "form-input" })%>
                </td>
            </tr>
            <tr>
                <td class="form-title">
                    排序：
                </td>
                <td>
                    <%= Html.TextBox("OrderId", category.OrderID, new { @class = "form-input" })%>
                </td>
            </tr>
            <tr>
                <td class="form-textarea-title ">
                    备注：
                </td>
                <td>
                    <%= Html.TextArea("Remark", category.Remark, new { @class = "form-textarea" })%>
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td>
                    <button class="button" onclick="javascript:CategoryManager.submit();">
                        保存</button>
                    <button class="button" onclick="javascript:CategoryManager.cancel();">
                        取消</button>
                </td>
            </tr>
        </table>
    </div>
    <div class="validation-summary-errors"></div>
    <script type="text/javascript" src="<%= Url.Content("~/Content/js/admin-category.js") %>"></script>
</body>
</html>