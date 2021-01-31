var pageUrl = null;
$(document).ready(function() {
    var acePageSourceCodeEditor = ace.edit("acePageSourceCodeEditor");
    acePageSourceCodeEditor.setTheme("ace/theme/monokai");
    acePageSourceCodeEditor.session.setMode("ace/mode/razor");

    var acePageModelSourceCodeEditor = ace.edit("acePageModelSourceCodeEditor");
    acePageModelSourceCodeEditor.setTheme("ace/theme/monokai");
    acePageModelSourceCodeEditor.session.setMode("ace/mode/csharp");

    if (window.location.href.indexOf("pageUrl") !== -1) {
        pageUrl = window.location.href.substr(window.location.href.indexOf("pageUrl")+8);

        loadCodeWindows(pageUrl);
    } else {
        $('.treeview-animated').mdbTreeview();
    }

    var split = Split(['#one', '#two', '#three']);

    $("#btnExample, #btnContentPage, #btnPageModel").click(function () {
        $(this).toggleClass("active");

        var id = $(this).data("div-id");
        $("#" +id).toggle();

        var visiblePanelIds = [];
        $('.codeWindows:visible').each(function(index, element) {
            visiblePanelIds.push("#" +element.id);
        });

        split.destroy();
        split = Split(visiblePanelIds);
    });

    //trick to update editors
    //https://github.com/ajaxorg/ace/issues/2497
    // $('.nav-tabs a').on('shown.bs.tab', function(event){
    //     acePageSourceCodeEditor.resize();
    //     acePageModelSourceCodeEditor.resize();
    // });

    $('#displayPageIFrameUrlBtn').click(function (event) {
        event.preventDefault();

        var url = $("#displayPageIFrameUrl").val();

        $("#displayPageIFrame").attr("src", url);
    })

    $('.page').click(function () {
        $this = $(this);
        var url = $this.data("page-url");
        loadCodeWindows(url);

        $("#displayPageIFrameUrl").val(url);
        //var detailPage = $("#page-detail-info #" + $this.attr("id"));


        //detailPage.toggle();

        //  if (prevDetailPage != null) {
        //      prevDetailPage.toggle();
        // }
        //
        //  prevDetailPage = detailPage;

    });

    function loadCodeWindows(url) {
        var pageSourceExtension = ".cshtml.cs";

        var hasContentPage = true;
        if(url.startsWith("/Lesson0/")) {
            pageSourceExtension = ".cs";
            hasContentPage = false;
        }

        var pageSource = url + pageSourceExtension;
        var pageSourceUrl = "/code/Pages" + pageSource + ".txt";

        var pageModelSource = url + ".cshtml";
        var pageModelSourceUrl = "/code/Pages" + pageModelSource + ".txt";

        $.ajax({
            url: pageSourceUrl,
            cache: false
        }).done(function (result) {
            acePageModelSourceCodeEditor.setValue(result);
        }).fail(function () {
            acePageModelSourceCodeEditor.setValue("Geen code beschikbaar!");
        });

        if(hasContentPage) {
            $.ajax({
                url: pageModelSourceUrl,
                cache: false
            })
            .done(function(result) {
                    acePageSourceCodeEditor.setValue(result);
            }).fail(function () {
                acePageSourceCodeEditor.setValue("Geen code beschikbaar!");
            });
        } else {
            acePageSourceCodeEditor.setValue("Geen code beschikbaar!");
        }

        $("#displayPageIFrame").attr("src", url);
    }
});
