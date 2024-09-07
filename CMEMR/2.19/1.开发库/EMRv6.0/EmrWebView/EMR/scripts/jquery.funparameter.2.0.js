;(function($){
	$.fn.funparapack = function(options){
		var undefined = "undefined";
		
		var opt = $.extend({}, $.fn.funparameter.defaults, options);
		/*
		for(var o in $.fn.funparameter.defaults){
			alert($.fn.funparameter.defaults[o]);
		}
		*/
		
		this.each(function(){
			opt.before();
			opt.process();
			opt.after();
			opt.callback();
		});
	}
	
	$.fn.funparameter.defaults = {
				before:    befEvent,
				process:   proEvent,
				after:     aftEvent,
				callback:  callback
			};
	
	function befEvent(){
		alert("befEvent");
	}
	function proEvent(){
		alert("proEvent");
	}
	function aftEvent(){
		alert("aftEvent");
	}
	function callback(){
		alert("call back !!");
	}
})(jQuery);