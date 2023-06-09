const Baseurl = 'https://localhost:44307/'
const formToJson = ($form) => {
    const data = {};
    $form.each(function () {
        $(this).find("input, select, textarea").each(function () {
            data[$(this).attr("name")] = $(this).val();
        });
    });
    return data
}
const ajaxJSON = (url, dt) => {
    let lt
    $.ajax({
        url: Baseurl + url,
        type: "GET",
        data: dt,
        contentType: "application/json",
        crossDomain: true,
        timeout: 5000,
        success: function (response) {
            console.log(response);
            lt = response
        },
        error: function (error) {
            console.log(error);
        }
    });
    return lt
}
const ajaxJSONPost = (url, dt) => {
    let lt
    $.ajax({
        url: Baseurl + url,
        type: "POST",
        data: dt,
        contentType: "application/json",
        timeout: 5000,
        success: function (response) {
            console.log(response);
            lt = response
        },
        error: function (error) {
            console.log(error);
        }
    });
    return lt
}
const addFormulaireAjax = (formData) => {
    let lt
    $.ajax({
        url: Baseurl + 'api/Formulaire/insertFormulaire',
        type: 'POST',
        data: JSON.stringify(formData),
        processData: false,
        contentType: false,
        timeout: 5000,
        success: function (response) {
            lt = response;
        },
        error: function (xhr, status, error) {
            lt = error
        }
    });
    return lt
}
const asyncAjax = async (url, dt) => {
    try {
        var response = await $.ajax({
            url: Baseurl + url,
            type: 'POST',
            data: JSON.stringify(dt),
            processData: false,
            contentType: false,
            timeout: 5000
        });
        console.log(response);
    } catch (error) {
        console.log(error);
    }
}
function ekUpload() {
    function Init() {

        console.log("Upload Initialised");

        var fileSelect = document.getElementById('file-upload'),
            fileDrag = document.getElementById('file-drag'),
            submitButton = document.getElementById('submit-button');
        if (fileSelect != null)
            fileSelect.addEventListener('change', fileSelectHandler, false);

        // Is XHR2 available?
        var xhr = new XMLHttpRequest();
        if (xhr.upload) {
            // File Drop
            if (!fileDrag) return
            fileDrag.addEventListener('dragover', fileDragHover, false);
            fileDrag.addEventListener('dragleave', fileDragHover, false);
            fileDrag.addEventListener('drop', fileSelectHandler, false);
        }
    }

    function fileDragHover(e) {
        var fileDrag = document.getElementById('file-drag');

        e.stopPropagation();
        e.preventDefault();

        fileDrag.className = (e.type === 'dragover' ? 'hover' : 'modal-body file-upload');
    }

    function fileSelectHandler(e) {
        // Fetch FileList object
        var files = e.target.files || e.dataTransfer.files;

        // Cancel event and hover styling
        fileDragHover(e);

        // Process all File objects
        for (var i = 0, f; f = files[i]; i++) {
            parseFile(f);
            uploadFile(f);
        }
    }

    // Output
    function output(msg) {
        // Response
        var m = document.getElementById('messages');
        m.innerHTML = msg;
    }

    function parseFile(file) {

        console.log(file.name);
        output(
            '<strong>' + encodeURI(file.name) + '</strong>'
        );

        // var fileType = file.type;
        // console.log(fileType);
        var imageName = file.name;

        var isGood = (/\.(?=gif|jpg|png|jpeg|pdf)/gi).test(imageName);
        if (isGood) {
            document.getElementById('start').classList.add("hidden");
            document.getElementById('response').classList.remove("hidden");
            document.getElementById('notimage').classList.add("hidden");
            // Thumbnail Preview
            document.getElementById('file-image').classList.remove("hidden");
            document.getElementById('file-image').src = URL.createObjectURL(file);
        }
        else {
            document.getElementById('file-image').classList.add("hidden");
            document.getElementById('start').classList.remove("hidden");
            document.getElementById('response').classList.add("hidden");
            document.getElementById("file-upload-form").reset();
        }
    }

    function setProgressMaxValue(e) {
        var pBar = document.getElementById('file-progress');

        if (e.lengthComputable) {
            pBar.max = e.total;
        }
    }

    function updateFileProgress(e) {
        var pBar = document.getElementById('file-progress');

        if (e.lengthComputable) {
            pBar.value = e.loaded;
        }
    }

    function uploadFile(file) {

        var xhr = new XMLHttpRequest(),
            fileInput = document.getElementById('class-roster-file'),
            pBar = document.getElementById('file-progress'),
            fileSizeLimit = 1024; // In MB
        if (xhr.upload) {
            // Check if file is less than x MB
            if (file.size <= fileSizeLimit * 1024 * 1024) {
                // Progress bar
                pBar.style.display = 'inline';
                xhr.upload.addEventListener('loadstart', setProgressMaxValue, false);
                xhr.upload.addEventListener('progress', updateFileProgress, false);

                // File received / failed
                xhr.onreadystatechange = function (e) {
                    if (xhr.readyState == 4) {
                        // Everything is good!

                        // progress.className = (xhr.status == 200 ? "success" : "failure");
                        // document.location.reload(true);
                    }
                };

                // Start upload
                xhr.open('POST', document.getElementById('file-upload-form').action, true);
                xhr.setRequestHeader('X-File-Name', file.name);
                xhr.setRequestHeader('X-File-Size', file.size);
                xhr.setRequestHeader('Content-Type', 'multipart/form-data');
                xhr.send(file);
            } else {
                output('Please upload a smaller file (< ' + fileSizeLimit + ' MB).');
            }
        }
    }

    // Check for the various File API support.
    if (window.File && window.FileList && window.FileReader) {
        Init();
    } else {
        document.getElementById('file-drag').style.display = 'none';
    }
}
ekUpload();