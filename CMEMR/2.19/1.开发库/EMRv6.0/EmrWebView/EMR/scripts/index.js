$(function () {
    var IE = false;
    $.browser.msie ? IE = true : {};
    var botspace = 123;
    if (IE) {
        botspace = 130;
    }
    //屏幕分辨率
    var screenRelW = document.body.clientWidth;
    var screenRelH = document.body.clientHeight;
    if (screenRelW < 1280) {
        screenRelW = 1280;
        if (IE) {
            screenRelW = screenRelW - 22;
        }
    } else {
        if (IE) {
            screenRelW = screenRelW - 22;
        }
    }
    if (screenRelH < 632) {
        screenRelH = 630;
    }
    $(document.body).css({ width: screenRelW, height: screenRelH });
    $(".head").css({ width: screenRelW });
    //$(".page-content").css({width: screenRelW, height: screenRelH-botspace});
    //$("#right-content").css({ width: screenRelW - 226, height: screenRelH - botspace - 8 });
    $(".page-content").css({ width: screenRelW, height: screenRelH });
    $("#right-content").css({ width: screenRelW - 226, height: screenRelH });
//    if (IE) {
//        $("#main-menu").css({ height: screenRelH - botspace - 8 });
//    } else {
//        $("#main-menu").css({ height: screenRelH - botspace - 8 * 2 });
//    }



        if (IE) {
            $("#main-menu").css({ height: screenRelH });
        } else {
            $("#main-menu").css({ height: screenRelH});
        }

    $(window).resize(function () {
        var curW = document.body.clientWidth;
        var curH = document.body.clientHeight;
        if (curW > screenRelW) {
            $(document.body).css({ width: curW });
            $(".page-content").css({ width: curW });
            $("#right-content").css({ width: curW - 226 });
        }
        if (curH > screenRelH) {
            $(document.body).css({ height: curH });
            $(".page-content").css({ height: curH - botspace });
            $("#right-content").css({ height: curH - botspace - 8 });
            if (IE) {
                $("#main-menu").css({ height: curH - botspace - 8 });
            } else {
                $("#main-menu").css({ height: curH - botspace });
            }
        }
    });
    //菜单展开闭合效果
    $("#main-menu .menu-switch").click(function () {
        if ($(this).css("background-image").indexOf("close") != -1) {
            $(this).css("background-image", "url(images/icon_open.png)");
        } else {
            $(this).css("background-image", "url(images/icon_close.png)");
        }

        $(this).parent().siblings().toggle().end();
    });
    //菜单的鼠标悬停效果
    $(".subItem").mouseover(function () {
        $(this).attr("title", $(this).text());
    });
});