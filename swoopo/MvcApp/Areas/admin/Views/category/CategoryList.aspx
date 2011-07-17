<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage" %>
<!DOCTYPE html>
<html>
<head runat="server">
    <title>CategoryList</title>
    <link type="text/css" rel="Stylesheet" href="<%= Url.Content("~/Content/reset.css") %>" />
    <link type="text/css" rel="Stylesheet" href="<%= Url.Content("~/Content/site.css") %>" />
    <link type="text/css" rel="Stylesheet" href="<%= Url.Content("~/Content/style.css") %>" />
    <style type="text/css">
        .r_ID { width:40px; }
        .r_CategoryName { width:200px; }
        .r_OrderID { width:40px; }
    </style>
    <script type="text/javascript" src="<%= Url.Content("~/scripts/jquery-1.4.2.min.js") %>"></script>
    <script type="text/javascript">
        var path = "<%= HttpRuntime.AppDomainAppVirtualPath %>";
        var redirect = function () {
            window.location.href = path + "/Admin/Category/CategoryEdit";
        };
    </script>
</head>
<body>
    <div style="margin-bottom:5px;">
        <button class="button" onclick="javascript:redirect();">
            添加</button>
    </div>
    <div>
        
    </div>
    <div id="grid">
    </div>
    <script type="text/javascript" src="<%= Url.Content("~/scripts/TableView.js") %>"></script>
    <script type="text/javascript" src="<%= Url.Content("~/Content/js/admin-category-list.js") %>"></script>
</body>
</html>
