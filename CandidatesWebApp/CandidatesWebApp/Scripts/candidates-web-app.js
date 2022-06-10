$(document).ready(function () {
    $('#inputPhoneNumber').usPhoneFormat({
        format: 'xxx-xxx-xxxx',
    });

    $("#panelAdvancedSearch").hide();

    $('#tableCandidates').DataTable({
        "processing": true,
        "ajax": {
            'url': '/api/candidates/',
            'type': "GET",
            'dataSrc': ''
        },
        "columnDefs": [
            { "targets": 6, "orderable": false }
        ],
        "columns": [
            { "data": "Id" },
            { "data": "FirstName" },
            { "data": "LastName" },
            { "data": "EmailAddress" },
            { "data": "PhoneNumber" },
            { "data": "ResidentialZipCode" },
            {
                "data": null,
                "render": function (data, type, row) {
                    return '<button class="btn btn-edit" data-toggle="modal" data-target="#modalCandidate" onclick="onclickBtnEdit(' + data.Id + ')"><i class="fa fa-pencil"></i></button>' +
                        '<button class="btn btn-delete" onclick="onclickBtnDelete(' + data.Id + ')"><i class="fa fa-trash"></i></button>';
                }
            }
        ],
        "dom": "ltipr",
        "language": {
            "loadingRecords": '&nbsp;'
        }
    });

    //Type on the "Search" input from the top bar
    $('#inputSearch').on('keyup', function () {
        $('#tableCandidates').DataTable().search(this.value).draw();
    });

    //Click on the "Add Candidate" button
    $("#buttonAddCandidate").on("click", function () {
        $("#modalTitle").text("Create Candidate");
        $("#divInputId").hide();
        $('#formCandidate').trigger("reset");
    });

    //Click on the "Save" button from the modal
    $("#buttonSave").on("click", function () {
        if ($("#formCandidate")[0].checkValidity()) {
            var candidate = {
                "Id": $("#inputId").val(),
                "FirstName": $("#inputFirstName").val(),
                "LastName": $("#inputLastName").val(),
                "EmailAddress": $("#inputEmailAddress").val(),
                "PhoneNumber": $("#inputPhoneNumber").val(),
                "ResidentialZipCode": $("#inputResidentialZipCode").val()
            };

            saveCandidate(candidate);
        }
        else {
            $("#formCandidate")[0].reportValidity()
        }
    });

    //Click on the "Advanved Search" button from the top bar
    $("#btnShowAdvancedSearch").on("click", function () {
        $("#panelAdvancedSearch").toggle();
    });

    //Click on the "Search" button from the Advanved Search panel
    $("#btnAdvancedSearch").on("click", function () {
        var candidate = {
            "FirstName": $("#inputFirstNameSearch").val(),
            "LastName": $("#inputLastNameSearch").val(),
            "EmailAddress": $("#inputEmailAddressSearch").val(),
            "PhoneNumber": $("#inputPhoneNumberSearch").val(),
            "ResidentialZipCode": $("#inputResidentialZipcodeSearch").val()
        };

        advancedSearch(candidate);
    });

    //Click on the "Search" button from the Advanved Search panel
    $("#btnReset").on("click", function () {
        $('#tableCandidates').DataTable().ajax.reload();
        $('#formAdvancedSearch').trigger("reset");
    });

    function saveCandidate(candidate) {
        if (candidate.Id) {
            $.ajax({
                url: '/api/candidates/',
                type: 'PUT',
                data: candidate,
                success: function () {
                    toastr.success('Candidate updated with success!');
                    $('#modalCandidate').modal('hide');
                    $('#tableCandidates').DataTable().ajax.reload();
                },
                error: function (request, message, error) {
                    toastr.error("Candidate could not be updated. Please try again.");
                }
            });
        }
        else {
            $.ajax({
                url: '/api/candidates/',
                type: 'POST',
                data: candidate,
                success: function (response) {
                    toastr.success('Candidate created with success!');
                    $('#modalCandidate').modal('hide');
                    $('#tableCandidates').DataTable().ajax.reload();
                },
                error: function (request, message, error) {
                    toastr.error("Candidate could not be created. Please try again.");
                }
            });
        }
    }

    function advancedSearch(candidate) {
        $.ajax({
            url: '/api/candidates/search',
            type: 'GET',
            data: {
                firstName: candidate.FirstName,
                lastName: candidate.LastName,
                emailAddress: candidate.EmailAddress,
                phoneNumber: candidate.PhoneNumber,
                residentialZipCode: candidate.ResidentialZipCode
            },
            success: function (candidates) {
                $('#tableCandidates').DataTable().clear();
                $('#tableCandidates').DataTable().rows.add(candidates).draw(false);
            },
            error: function (request, message, error) {
                toastr.error("Search could not be done. Please try again.");
            }
        });
    }
});

function onclickBtnEdit(id) {
    $("#modalTitle").text("Edit Candidate");
    getCandidate(id);
}

function onclickBtnDelete(id) {
    if (confirm("Are you sure you want to delete this Candidate?")) {
        deleteCandidate(id);
    }
}

function getCandidate(id) {
    $.ajax({
        url: '/api/candidates/' + id,
        type: 'GET',
        success: function (candidate) {
            fillModalToEdit(candidate);
        },
        error: function (request, message, error) {
            toastr.error("Candidate could not be retrieved. Please try again.");
        }
    });
}

function deleteCandidate(id) {
    $.ajax({
        url: '/api/candidates?id=' + id,
        type: 'DELETE',
        success: function () {
            toastr.success('Candidate deleted with success!');
            $('#modalCandidate').modal('hide');
            $('#tableCandidates').DataTable().ajax.reload();
        },
        error: function (request, message, error) {
            toastr.error("Candidate could not be deleted. Please try again.");
        }
    });
}

function fillModalToEdit(candidate) {
    $("#divInputId").show();
    $("#inputId").val(candidate.Id);
    $("#inputFirstName").val(candidate.FirstName);
    $("#inputLastName").val(candidate.LastName);
    $("#inputEmailAddress").val(candidate.EmailAddress);
    $("#inputPhoneNumber").val(candidate.PhoneNumber);
    $("#inputResidentialZipCode").val(candidate.ResidentialZipCode);
}