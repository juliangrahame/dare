
function renderVideoList(videos) {
    var divVideoWrapper = $(".video-wrapper .row");
    divVideoWrapper.text("");
    $.each(videos, function (i, item) {
        //divVideoWrapper.append("<div><iframe width='240' height='180' src='https://www.youtube.com/embed/" + item.VideoId + "' frameborder = '0' allow = 'accelerometer; autoplay; clipboard - write; encrypted - media; gyroscope; picture -in -picture' allowfullscreen></iframe><div>");
        divVideoWrapper.append("<div class='col-sm-6 col-md-4 col-xl-3 video-card'>" +
            "<div class='card h-100'>" +
            "<img class='card-img-top' src='" + item.VideoThumbnailUrl + "' />" +
            "<div class='card-body'>" +
            "<div>" +
            "<h5 class='card-title'>" + item.VideoTitle + "</h5>" +
            "<p class='small'>" + item.VideoText + "</p>" +
            "<a class='card-link' href='https://www.youtube.com/watch?v=" + item.VideoId + "'>View</a>" +
            "</div>" +
            "</div>" +
            "</div>" +
            "</div>");  
    });
}

$('.carousel').slick({
    slidesToShow: 1,
    slidesToScroll: 1,
    autoplay: true,
    arrows: true,
    autoplaySpeed: 2000,
    fade: true
});
  
$(document).ready(function () {

    $("#loadVideosButton").click(function () {
        if ($("#searchText").val()) {
            $(".searchInput span").hide();

            $.ajax({
                url: "/umbraco/api/youtubevideoapi/getyoutubevideos/",
                data: { searchText: $("#searchText").val(), pageId: $("body").attr("data-page-Id") }
            }).done(function (data) {
                renderVideoList(data);
            });
        } else {
            $(".searchInput span").show();
        }
    });
});