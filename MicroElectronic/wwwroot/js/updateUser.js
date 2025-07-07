function updateUser(params) {
    const url = params.url;
    const id = params.id;
    const modal = $('#mainModal');
    let title = "Редактирование пользователя";

    $.ajax({
        type: 'GET',
        url: url,
        data: {"id": id},
        success: function (response) {
            modal.find(".modal-title").html(title);
            modal.find(".modal-body").html(response);
            modal.modal('show')
        },
        failure: function () {
            modal.modal('hide')
        },
        error: function (response) {
            alert(response.Description);
        }
    });
};

function deleteUser(params) {
    const url = params.url;
    const id = params.id;

    const modal = $('#mainModal');
    let title = "Удаление пользователя";

    $.ajax({
        type: 'GET',
        url: url,
        data: { "id": id },
        success: function (response) {
            modal.find(".modal-title").html(title);
            modal.find(".modal-body").html(response);
            modal.modal('show')
        },
        failure: function () {
            modal.modal('hide')
        },
        error: function (response) {
            alert(response.Description);
        }
    });
};