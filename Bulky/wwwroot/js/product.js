$(document).ready(function () {
    loadDataTable();
});
function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "ajax": { url: '/admin/product/getall' },
        "columns": [
            { data: 'Title' ,"width":"25%"},
            { data: 'ISBN', "width": "15%" },
            { data: 'ListPrice', "width": "10%" },
            { data: 'Author', "width": "15%" },
            { data: 'Category.Name', "width": "10%" },
            {
                data: 'Id',
                "render": function (data) {
                    return `<div class="w-75 btn-group" role="group">
                                <a href="/admin/product/upsert?id=${data}" class="btn btn-primary mx-2">
                                    <i class="bi bi-pencil-square"></i>  Edit
                                </a>
                                <a href="" class="btn btn-danger mx-2">
                                    <i class="bi bi-trash-fill"></i>  Delete
                                </a>
                            </div>`
                },
                "width": "25%"
            }
        ]
    });
}