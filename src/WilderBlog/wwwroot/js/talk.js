// talk.js
(function ($) {
	$(".talk-toggle").on("click", function () {
		var $btn = $(this);
		var $talk = $("#talk-" + $btn.attr("data-id"));
		if ($btn.text() == "Show Talk") {
			$btn.text("Hide Talk");
			//loadTalk($talk);
			$talk.show(0);
		} else {
			$btn.text("Show Talk");
			$talk.hide(0);
		}
	});

	function loadTalk($talk) {
		var $iframe = $talk.find("iframe");
		if (!$iframe.is('[src]')) {
			$iframe.attr("src", $iframe.attr("data-src"));
		}
	}

	$(document).ready(function () {
		var $talk = $(".talk-container");
		if ($talk.hasClass("auto-load")) loadtalk($talk);
	});
})(jQuery);