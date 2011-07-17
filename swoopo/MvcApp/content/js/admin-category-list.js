var CategoryList = {
    get: function () {
        jQuery.post("CategoryLists", null, function (data) {
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
                var deep = 1,
                    path;
                for (var i = 0, len = data.value.length; i < len; i++) {
                    path = data.value[i].Path || "";
                    deep = path.split(",").length - 1;
                    deep < 0 ? deep = 0 : deep = deep;
                    data.value[i].CategoryName =
                        "<span class='t" + deep + "'>" +
                        data.value[i].CategoryName + "</span>";
                }

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
    }
};
CategoryList.get();