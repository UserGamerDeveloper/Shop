function quantityChange(params) {
    var itemId = parseInt(params.itemId);
    var operation = params.operation;

    $.ajax({
        type: 'POST',
        url: '/ApplicationItems/ChangeQuantity',
        data: { "itemId": itemId, 'operation': operation },
        success: function () {
            console.log("Success change quantity");
            $('#appItemsLoad').load("/ApplicationItems/MyApplicationItems #appItems");
        },
        failure: function () {
            console.log("Failure change quantity");
        },
        error: function (response) {
            console.log("Error change quantity" + response);
        }
    });
}; 