var CategoryManager = {
    //提交表单
    submit: function () {
        var categoryName = jQuery("#CategoryName").val();
        if (jQuery.trim(categoryName) == "") {
            jQuery("#CategoryName").focus();
            return;
        }

        var category = {
            ParentID: jQuery("#Cateories").val(),
            CategoryName: jQuery("#CategoryName").val(),
            OrderID: jQuery("#OrderId").val(),
            Remark: jQuery("#Remark").val()
        };

        jQuery.post("CategoryAction", jQuery.param(category), function (data) {
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
                window.location.href = "/Admin/Category/CategoryList";
            }
        });
    },
    //取消操作
    cancel: function () {
        window.location.href = "/Admin/Category/CategoryList";
    }
};