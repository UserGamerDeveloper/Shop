function openDeleteModal(params) {
    const id = params.id;
    const url = params.url;
    const modal = $('#mainModal');
    let title = "Удаление категории";

    if (id == 0 || id === undefined) {
        alert("Что-то пошло не так :(");
    }

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
}
