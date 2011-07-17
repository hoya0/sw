jQuery(document).ready(function () {
    var iptSt = document.getElementById("iptSt"),
        iptEt = document.getElementById("iptEt");
    var date = new Date();
    var year = date.getFullYear();
    var month = date.getMonth() + 1;
    month = month < 10 ? ("0" + month) : month;
    var day = date.getDate();
    day = day < 10 ? ("0" + day) : day;
    var h = date.getHours();
    h = h < 10 ? ("0" + h) : h;
    var m = date.getMinutes();
    m = m < 10 ? ("0" + m) : m;
    var s = date.getSeconds();
    s = s < 10 ? ("0" + s) : s;
    var dateStr = year + "-" + month + "-" + day + " " + h + ":" + m + ":" + s;
    iptSt.value = dateStr;

    iptSt.onfocus = function () {
        WdatePicker({
            maxDate: "#F{$dp.$D(\'iptEt\');}",
            dateFmt: "yyyy-MM-dd HH:mm:ss"
        });
    };
    iptEt.onfocus = function () {
        WdatePicker({
            minDate: "#F{$dp.$D('iptSt',{d: 0});}",
            dateFmt: "yyyy-MM-dd HH:mm:ss"
        });
    };
    iptEt.onblur = function () {
        if (jQuery.trim(iptSt.value) == "" || jQuery.trim(iptEt.value) == "") {
            return;
        }
        var st = new Date(iptSt.value),
            et = new Date(iptEt.value);
        if (et - st <= 0) {
            alert("结束时间应大于开始时间。");
            return;
        }
        //秒
        var i = (et - st) / 1000,
            unit = "秒";
        var value = Common.formatDate(i);
        jQuery("#remindTime").val(value);
        console.info(et - st);
    };
    CKEDITOR.replace('txtEditor', {
        skin: 'v2',
        width: "800px"
    });
});
var ProEdit = {
    save: function () {
        var proName = jQuery("#proName").val();
        var category = jQuery("#Cateories").val();
        var st = jQuery("#iptSt").val();
        var et = jQuery("#iptEt").val();
        var remind = new Date(et) - new Date(st);
        var content = CKEDITOR.instances.txtEditor.getData();
        var params = {
            ProName: proName,
            StarTime: st,
            EndTime: et,
            ProState: 0,
            Remark: content,
            CategoryID: category
        };
        jQuery.post(parent.SW.path + "/Admin/Product/Save", params, function (res) {
            if (res.error != null) {
                document.getElementById("error").innerHTML = res.error;
            }
            else {
                window.location.href = parent.SW.path + "/Admin/Product/";
            }
        });
    }
};