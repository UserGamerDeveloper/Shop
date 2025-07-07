function removeFromApp(params) {
    var itemId = parseInt(params.itemId);

        $.ajax({
            type: 'GET',
            url: '/ApplicationItems/RemoveItem',
            data: { "itemId": itemId },
            success: function () {
                console.log("Success remove item application!");
                $('#appItemsLoad').load("/ApplicationItems/MyApplicationItems #appItems");
            },
            failure: function () {
                console.log("Failure remove item application!");
            },
            error: function (response) {
                console.log("Error remove item application!" + response);
            }
        });
}; 

function clearAppItems(params) {
    var userId = parseInt(params.userId);

    $.ajax({
        type: 'POST',
        url: '/ApplicationItems/ClearItems',
        data: { "userId": userId },
        success: function () {
            console.log("Success clear items");
            $('#appItemsLoad').load("/ApplicationItems/MyApplicationItems #appItems");
        },
        failure: function () {
            console.log("Failure clear items application");
        },
        error: function (response) {
            console.log("Error clear items application: " + response);
        }
    });
};