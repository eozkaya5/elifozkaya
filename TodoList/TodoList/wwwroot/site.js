// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

//// Write your JavaScript code.
//$(document).ready(function () {

//    // Add butonuna tıklanıldığında addItem fonksiyonu çalışacak.
//    $('#add-item-button').on('click', addItem);

//});

//function addItem() {
//    $('#add-item-error').hide();
//    var newTitle = $('#add-item-title').val();

//    $.post('/Todo/AddItem', { title: newTitle }, function () {
//        window.location = '/Todo';
//    })
//        .fail(function (data) {
//            if (data && data.responseJSON) {
//                var firstError = data.responseJSON[Object.keys(data.responseJSON)[0]];
//                $('#add-item-error').text(firstError);
//                $('#add-item-error').show();
//            }
//        });
//}




//$(document).ready(function () {

//    // ...

//    // .done-checkbox sınıfına ait tüm onay kutuları aşağıdaki kodu çalıştırır.
//    $('.done-checkbox').on('click', function (e) {
//        markCompleted(e.target);
//    });

//});

//function markCompleted(checkbox) {
//    checkbox.disabled = true;

//    $.post('/TodoItem/Index', { id: checkbox.name }, function () {
//        var row = checkbox.parentElement.parentElement;
//        $(row).addClass('done');
//    });
//}