$(function(){
	//屏幕分辨率
	var screenRelW = window.screen.availWidth;
	var screenRelH = window.screen.availHeight;
	//页面大小随浏览器变化
	var screenWidth = $(document.body).width();
	var screenHeight = $(document.body).height();
	//如果屏幕太小
	if(screenWidth < 1280){
		screenWidth = 1280;
		$(".page-content").css({width: screenWidth});
		$(".head").css({width: screenWidth});
	}
	if(screenHeight < 630){
		screenHeight = 630;
	}
	
	var rightWidth = parseInt(parseInt(screenWidth) - 226);
	var rightHeight = parseInt(parseInt(screenHeight) - 123);

	$("#main-menu").css({height: rightHeight-8});
	if($.browser.msie) {
		rightWidth = rightWidth-20;
		rightHeight = rightHeight-8;
	}
	$("#right-content").css({width: rightWidth, height: rightHeight});
	var mainWidth = $(".page-content").css("width");
	var headWidth = $(".head").css("width");
	//浏览器窗口大小改变时
	$(window).resize(function(){
		var currWidth = document.body.clientWidth-15;
		var currHeight = document.body.clientHeight;
		//如果屏幕太小
		if(currWidth < 1280){
			currWidth = 1280;
			$(".page-content").css({width: currWidth});
			$(".head").css({width: currWidth});
		}
		if(currHeight < 630){
			currHeight = 630;
		}
		
		var rightWidthC = parseInt(parseInt(currWidth) - 226);
		var rightHeightC = parseInt(parseInt(currHeight) - 123);

		$("#main-menu").css({height: rightHeightC-8});
		if($.browser.msie) {
			rightHeightC = rightHeightC-8;
		}
		$("#right-content").css({width: rightWidthC, height: rightHeightC});
	});
	//菜单展开闭合效果
	$("#main-menu .menu-switch").click(function(){
		if($(this).css("background-image").indexOf("close") != -1){
			$(this).css("background-image" , "url(images/icon_open.png)");
		}else{
			$(this).css("background-image" , "url(images/icon_close.png)");
		}

		$(this).parent().siblings().toggle().end();
	});
	//菜单的鼠标悬停效果
	$(".subItem").mouseover(function(){
		$(this).attr("title", $(this).text());
	});
});