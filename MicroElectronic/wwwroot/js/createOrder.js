function createOrder(params) {
    const url = params.url;
    const modal = $('#mainModal');
    let title = "Создание заявки";

    $.ajax({
        type: 'GET',
        url: url,
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