$(document).ready(function () {
	$(".carousel-testimonials").owlCarousel({
		loop: true,
		margin: 10,
		nav:true,
		autoPlay: true,
		responsive: {
			0: {
				items: 1
			},
			600: {
				items: 3
			},
			1000: {
				items: 5
			}
		}
	});
});