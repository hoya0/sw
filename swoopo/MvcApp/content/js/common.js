var Common = {
    formatDate: function (seconds) {
        var d = new Date(seconds);

        var theTime = Number(seconds);
        var theTime1 = 0;
        var theTime2 = 0;
        var theTime3 = 0;
        if (theTime >= 60) {
            theTime1 = Math.floor(theTime / 60);
            theTime = Math.floor(theTime % 60);
            if (theTime1 >= 60) {
                theTime2 = Math.floor(theTime1 / 60);
                theTime1 = Math.floor((theTime / 60) % 60);
                if (theTime2 >= 24) {
                    theTime3 = Math.floor(theTime2 / 24);
                    theTime2 = Math.floor((theTime / 24) % 60);
                }
            }
        }
        var result = "" + theTime + "秒";
        if (theTime1 > 0) {
            result = "" + parseInt(theTime1) + "分" + result;
        }
        if (theTime2 > 0) {
            result = "" + parseInt(theTime2) + "小时" + result;
        }
        if (theTime3 > 0) {
            result = "" + parseInt(theTime3) + "天" + result;
        }
        return result;
    }
};