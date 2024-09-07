;(function($){
	$.fn.funparameter = function(options){
		var undefined = "undefined";
		
		var defaults = {
				color:     "green",
				before:    befEvent,
				process:   proEvent,
				after:     aftEvent
			};
		
		var opt = $.extend({}, defaults, options);
		
		this.each(function(){
			opt.before();
			opt.process();
			opt.after();
		});
		
		function befEvent(){
			alert("before event");
		}
		function proEvent(){
			alert("process event");
		}
		function aftEvent(){
			alert("after event");
		}
	}
	
	
	/*
	 * 
	 $.fn.funparameter.defaults = {
				before:    $.fn.funparameter.befEvent,
				process:   $.fn.funparameter.proEvent,
				after:     $.fn.funparameter.aftEvent
			};
	$.fn.funparameter.befEvent = function(){
		alert("befEvent");
	}
	$.fn.funparameter.proEvent = function(){
		alert("proEvent");
	}
	$.fn.funparameter.aftEvent = function(){
		alert("aftEvent");
	}
	*/
})(jQuery);