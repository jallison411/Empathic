$(document).on("ready",function(){
	if($(window).width()<=767){
		if(location.hash=="#select"){
			$("body").addClass("select-project");
		}else if(location.hash=="#note"){
	  		$(".document-container").hide();
	  		$(".note-container").show();
	  		$(".active").removeClass("active");
	  		$(".note-nav").addClass("active");
	  		$(".select-project").removeClass("select-project");
	  	}else{
	  		$(".document-container").show();
	  		$(".note-container").hide();
	  		$(".active").removeClass("active");
	  		$(".home-nav").addClass("active");
	  		$(".select-project").removeClass("select-project");
	  	}
	}
	$(window).on("resize",function(){
		if($(window).width()<=767){
			if(location.hash=="#select"){
				$("body").addClass("select-project");
			}else if(location.hash=="#note"){
		  		$(".document-container").hide();
		  		$(".note-container").show();
		  		$(".active").removeClass("active");
		  		$(".note-nav").addClass("active");
		  		$(".select-project").removeClass("select-project");
		  	}else{
		  		$(".document-container").show();
		  		$(".note-container").hide();
		  		$(".active").removeClass("active");
		  		$(".home-nav").addClass("active");
		  		$(".select-project").removeClass("select-project");
		  	}
		}else{
			$(".document-container").show();
		  	$(".note-container").show();
		}
	});
	// Click login to go to main page
	$(".btn.btn-login").on("click",function(e){
		$(location).attr('href', './index.html#select');
	});
	$(".btn.btn-view").on("click",function(e){
		$(location).attr('href', './index.html#home');
	});
	$(".mobile-back").on("click",function(){
		$(location).attr('href', './index.html#select');
	});
	$(window).on('hashchange', function() {
		if($(window).width()<=767){
		  	if(location.hash=="#select"){
				$("body").addClass("select-project");
				$(".document-container").hide();
		  		$(".note-container").hide();
			}else if(location.hash=="#note"){
		  		$(".document-container").hide();
		  		$(".note-container").show();
		  		$(".active").removeClass("active");
		  		$(".note-nav").addClass("active");
		  		$(".select-project").removeClass("select-project");
		  	}else if(location.hash=="#home"){
		  		$(".document-container").show();
		  		$(".note-container").hide();
		  		$(".active").removeClass("active");
		  		$(".home-nav").addClass("active");
		  		$(".select-project").removeClass("select-project");
		  	}
		}
	});
	// $(".document-item").on("click",function(e){
	// 	var url = $(this).data("url");
	// 	$(location).attr('href', url);
	// });
	$(".side-project .dropdown-menu li").on("click",function(e){
		var text = e.target.text;
		$(".side-project .btn-text").text(text);
	});
	$(".note-header .glyphicon").on("click",function(e){
		$(this).toggleClass("rotate");
		$(".note-container").toggleClass("note-open");
		$(".document-container").toggleClass("open-note");
	});
	$(".dropdown-btn").on("click",function(){
		$(this).find(".dropdown-menu").toggle("show");
		$(this).toggleClass("dropdown-selected");
	});
	$(".boxlink").click(function() {
  window.location = $(this).find("a").attr("href"); 
  return false;
		
		
});
	// $(".note-item").on("click",function(e){
	// 	$(".note-container").toggleClass("note-open");
	// 	$(".note-header .glyphicon").toggle("show");
	// });
	// $(".note-header .glyphicon").on("click",function(e){
	// 	$(".note-container").toggleClass("note-open");
	// 	$(".note-header .glyphicon").toggle("show");
	// });
})
