<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage" %>
<!DOCTYPE html>
<html>
<head runat="server">
    <title>Product</title>
    <link type="text/css" rel="Stylesheet" href="<%= Url.Content("~/Content/reset.css") %>" />
    <link type="text/css" rel="Stylesheet" href="<%= Url.Content("~/Content/site.css") %>" />
    <script type="text/javascript" src="<%= Url.Content("~/scripts/jquery-1.4.2.min.js") %>"></script>
</head>
<body>
    <div>
        <h2>
            
            Product</h2>
    </div>
    <div>
        <button id="btn" class="button">新增</button>
    </div>
    <div id="grid"></div>
    <script type="text/javascript">
        document.getElementById("btn").onclick = function () {
            var url = parent.SW.path + "/Admin/Product/Edit";
            window.location.href = url;
        };
        jQuery.post("List", null, function (data) {
            if (data.value) {
                var grid = new TableView('grid');
                grid.title = "分类列表";
                grid.header = {
                    //ID: "",
                    CategoryName: "名称",
                    OrderID: "排序",
                    Remark: "备注"
                };
                grid.dataKey = "ID";
                grid.display.multiple = false;
                grid.display.title = false;
                grid.display.count = false;
                grid.display.marker = false;
                grid.clear();
                grid.addRange(data.value);
                // 因为查询条件可能变化, 结果数量有变化, 所以要重新生成分页控件.
                //grid.itemCount = response.itemCount;
                grid.render();
            }
        });
    </script>
</body>
</html>
