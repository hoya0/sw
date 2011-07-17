/// <reference path="../../Scripts/jquery-1.4.2.min.js" />

jQuery(document).ready(function () {
    var clientHeight = jQuery(window).height();
    var headHeight = jQuery(".header").outerHeight();
    var bottomHeight = jQuery(".footer").outerHeight();
    jQuery(".main").height(clientHeight - headHeight - bottomHeight);
});