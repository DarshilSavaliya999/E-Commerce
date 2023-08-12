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
            { data: 'Author', "width": "20%" },
            { data: 'Category.Name', "width": "15%" }
        ]
    });
}