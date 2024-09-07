;(function($){
	$.fn.parachange = function(options, optionName, optionvalue){
		var undefined;
		
		var opt = $.extend({}, $.fn.parachange.defaults , options);
		
		if(arguments.length == 3){
			if(arguments[0] === "option"){
				var optName = arguments[1];	
				alert(optName+"---"+arguments[2]);
				opt[optName] = arguments[2];
			}
		}
		
		this.each(function(){
			$(document.body).css("background" , opt.color);
		});
		
		/*
		 * 获取对象属性
		 * for(var p in opt){
		 * 		alert(opt[p]);
		 * }
		 */

		if(arguments.length == 2){
			if(arguments[0] === "option"){
				var optName = arguments[1];
				return opt[optName];
			}
			return opt.optName;
		}
		
		//debug(this);
	}
	$.fn.parachange.defaults = {
							foreGround: "red",
							backGround: "blue",
							color:      "green"
						};
	
	function debug(){
		alert("debug");
	}
})(jQuery);