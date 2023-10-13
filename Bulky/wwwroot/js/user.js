var dataTable;
$(document).ready(function () {
    loadDataTable();
});
function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "ajax": { url: '/admin/user/getall' },
        "columns": [
            { data: "Name" ,"width":"15%"},
            { data: "Email", "width": "15%" },
            { data: "PhoneNumber", "width": "15%" },
            { data: "Company.Name", "width": "15%" },
            { data: "Role", "width": "15%" },
            {
                data: 'Id',
                "render": function (data) {
                    return `<div class="w-75 btn-group" role="group">
                                <a href="/admin/company/upsert?id=${data}" class="btn btn-primary mx-2">
                                    <i class="bi bi-pencil-square"></i>  Edit
                                </a>    
                            </div>`
                },
                "width": "25%"
            }
        ]
    });
}