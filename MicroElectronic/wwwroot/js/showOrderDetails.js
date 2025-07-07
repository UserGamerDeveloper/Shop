function showOrderDetails(params) {
    const url = params.url;
    const orderId = params.orderId;
    const modal = $('#mainModal');
    let title = "Детали заявки";
    console.log(orderId);
    $.ajax({
        type: 'GET',
        url: url,
        data: {"orderId": orderId},
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