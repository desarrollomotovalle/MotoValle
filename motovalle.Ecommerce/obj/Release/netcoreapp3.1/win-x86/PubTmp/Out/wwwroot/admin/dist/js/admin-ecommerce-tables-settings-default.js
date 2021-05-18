/*
    Date: 2020/04/01
    Company: Innova Marketing Systems S.A.S
    Programmer: Diego Arenas Tangarife
    Comments: Default config to admin section tables
 */

// tables Config
$(document).ready(function () {
    $('#dtTableDefault').DataTable({
        "autoWidth": true,
        "paging": true,
        "ordering": true,
        "scrollY": true,
        "scrollX": "70vh",
        "searching": true,
    });

    $('.dataTables_length').addClass('bs-select');
});