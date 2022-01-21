class MyCodeEditor extends HTMLElement {
    _ace;
    _shadowRoot;

    constructor() {
        super();

        this._shadowRoot = this.attachShadow({ mode: "open" });

        const container = document.createElement("div");
        container.innerHTML = `
          <style>
              .aceCodeEditor {
                  position: relative;
                  width: 100%;
                  height: 400px;
              }
          </style>
          <h1 id="title">Code Editor</h1>
          <div class="editor aceCodeEditor">Er is geen page geselecteerd</div>
        `;

        this._shadowRoot.appendChild(container);

        this._ace = ace.edit(this._shadowRoot.querySelector(".aceCodeEditor"), {
            theme: "ace/theme/monokai",
            autoScrollEditorIntoView: true
        });

        this._ace.renderer.attachToShadowRoot();
    }

    connectedCallback() {
    }

    // static observedAttributes = 

    static get observedAttributes() {
        return ['title', 'code', 'theme', 'mode'];
    }

    attributeChangedCallback(name, oldValue, newValue) {
        switch (name) {
            case "title":
                this._shadowRoot.getElementById("title").innerText = newValue;
                break;
            case "code":
                this._ace.setValue(newValue);
                break;
            case "theme":
                this._ace.setTheme(newValue);
                break;
            case "mode":
                this._ace.session.setMode(newValue);
                break;
            default:
                break;
        }
    }
}

window.customElements.define('my-code-editor', MyCodeEditor);

const codeWindowTemplate = ({}) => `

         <div class="row" id="codeWindowContainer">
             <div class="split codeWindows" id="livePreviewContainer">
                 <iframe id="displayPageIFrame" class="w-100" style="height: 500px">
                 </iframe>
             </div>
             <div class="split codeWindows" id="pageContentContainer">
             </div>
             <div class="split codeWindows" id="pageModelContainer">
             </div>
         </div>
      `;

let outerContainer = null;

$(document).ready(function () {
    let url = new URL(window.location);
    let pageUrl = url.searchParams.get("pageUrl");

    outerContainer = $("#codeWindowMainContainer");
    if (pageUrl !== null) {
        loadCodeWindows(url);
    } else {
        $('.treeview-animated').mdbTreeview();

        //node in the thee is clicked (selected)
        $('.page').click(function () {
            const newUrl = new URL(url.href+"?pageUrl=" + $(this).data("page-url"));
            loadCodeWindows(newUrl);
            $("#mainContentContainer").show();
        });
    }

    $('#displayPageIFrameUrl').keypress(function (event) {
        if(event.which === 13) {
            handleAddressBar();
        }
    });

    //Go Button clicked
    $("#displayPageIFrameUrlBtn").on("click", function () {
        handleAddressBar();
    });
    
    $("#btnDisplayPagePopup").on("click", function () {
        handlePopUp();    
    });

    registerButtons();
});

function handlePopUp() {
    const urlAddressBar = $("#displayPageIFrameUrl").val();
    const newUrl = new URL(window.location.origin + "/DisplayExamples?pageUrl=" + urlAddressBar);
    window.open(newUrl,'popup',"width="+screen.availWidth+",height="+screen.availHeight);
}

function handleAddressBar() {
    const urlAddressBar = $("#displayPageIFrameUrl").val();
    const newUrl = new URL(window.location.origin + urlAddressBar);

    outerContainer.empty();
    loadCodeWindows(newUrl);
}

let livePreview = null;
let pageContent = null;
let pageModel = null;

function getUrlPaths(url)
{
    let livePreviewUrl = null;
    let codeFileUrl = null;

    if(url.searchParams.get("pageUrl") !== null) {
        // if(url.searchParams.get("pageUrl").startsWith("/Lesson0"))
        // {
        //     debugger;
        // }
        // else
        // {
            codeFileUrl = url.searchParams.get("pageUrl");
            url.searchParams.delete("pageUrl");
            livePreviewUrl = codeFileUrl + url.search;
        // }
    } else {
        codeFileUrl = url.pathname;
        livePreviewUrl = url.pathname + url.search;
    }

    return {livePreviewUrl: livePreviewUrl, codeFileUrl: codeFileUrl};
}

function loadCodeWindows(url, pageUrlFromTree = null) {
    let {livePreviewUrl, codeFileUrl} = getUrlPaths(url, pageUrlFromTree);

    $(outerContainer).html(codeWindowTemplate({}));

    //live preview scherm instellen
    $('#displayPageIFrame').attr("src", livePreviewUrl);
    $('#displayPageIFrameUrl').val(livePreviewUrl);

    //to handle get request in form(s) in the iframe!
    $('#displayPageIFrame').on("load",function(){
        $("form" ,$(this)[0].contentWindow.document).submit(function() {
            let method = $(this).attr("method");
            if (method !== undefined && method.length > 0 && method.toLowerCase() !== "post") {
                let queryString = $(this).serialize();
                if(queryString !== "") {
                    $("#displayPageIFrameUrl").val(codeFileUrl + "?" +queryString);
                }
            }
        })
    });

    let ajax1 = null;
    let ajax2 = null;

    if(codeFileUrl.startsWith("/Lesson0"))
    {
        ajax1 = loadCode("code", "#pageContentContainer", "/code/Pages" + codeFileUrl + ".cs.txt", 'ace/mode/csharp', "Content Page");
        ajax2 = loadCode("not available", "#pageModelContainer", "/code/Pages" + codeFileUrl + ".cshtml.cs" + ".txt", 'ace/mode/csharp', "Page Model");
    } else {
        ajax1 = loadCode("contentPage", "#pageContentContainer", "/code/Pages" + codeFileUrl + ".cshtml" + ".txt", 'ace/mode/razor', "Content Page");
        ajax2 = loadCode("pageModel", "#pageModelContainer", "/code/Pages" + codeFileUrl + ".cshtml.cs" + ".txt", 'ace/mode/csharp', "Page Model");
    }

    Promise.all([ajax1, ajax2]).then(() => {
        updateSplitJs();
    }).catch((response) => {
        updateSplitJs();
    });
}

function registerButtons() {
    $(document).on("click", "#livePreviewContainerBtn", function () {
        $(this).toggleClass("active");
        let containerDiv = $("#livePreviewContainer");
        if (containerDiv.length === 0) {
            $("#codeWindowContainer").prepend(livePreview);
        } else {
            livePreview = containerDiv.remove();
        }
        updateSplitJs();
    });

    //add or remove page content window
    $(document).on("click", "#pageContentContainerBtn", function () {
        $(this).toggleClass("active");
        let containerDiv = $("#pageContentContainer");
        if (containerDiv.length === 0) {
            let livePreviewDiv = $("#livePreviewContainer");
            if (livePreviewDiv.length === 0) {
                $("#codeWindowContainer").prepend(pageContent);
            } else {
                livePreviewDiv.after(pageContent);
            }
        } else {
            pageContent = containerDiv.remove();
        }
        updateSplitJs();
    });

    //add or remove page model window
    $(document).on("click", "#pageModelContainerBtn", function () {
        $(this).toggleClass("active");
        let containerDiv = $("#pageModelContainer");
        if (containerDiv.length === 0) {
            $("#codeWindowContainer").append(pageModel);
        } else {
            pageModel = containerDiv.remove();
        }
        updateSplitJs();
    });
}

function loadCode(id, appendToSelector, url, mode, title) {
    return $.ajax({
        url: url,
        cache: false
    }).done(function (result) {
        var editor = $(`<my-code-editor id="${id}" title="${title}" code="loading" mode=""></my-code-editor>`);
        $(editor).attr("code", result);
        $(editor).attr("mode", mode);
        editor.appendTo(appendToSelector);
        $(appendToSelector+"Btn").show()
    }).fail(function () {
        $(appendToSelector).remove();
        $(appendToSelector+"Btn").hide();
    });
}

var split = null;
function updateSplitJs() {
    if (split !== null) {
        split.destroy();
    }

    var visiblePanelIds = [];
    $('.split').each(function(index, element) {
        visiblePanelIds.push("#" +element.id);
    });

    split = Split(visiblePanelIds);
}
