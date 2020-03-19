var tdl = null;

$(function () {
    tdl = $("#tdlTable").DataTable({
        'paging': true,
        'serverSide': true,
        'ajax': "/ToDoList/Data/" + $('#status').val(),
        "columns": [
            { data: null },
            { data: "name" },
            {
                data: "createDate",
                render: function (data) {
                    var createdate = moment(data).format('DD/MMMM/YYYY HH:mm');
                    return createdate;
                }
            },
            {
                data: "finishedDate",
                render: function (data) {
                    if (data != null) {
                        return moment(data).format('DD/MMMM/YYYY HH:mm');
                    }
                    return "Still Ongoing";
                }
            },
            {
                data: "status",
                render: function (data) {
                    if (data) {
                        return "Done";
                    }
                    return "Active";
                }
            },
            { data: "status" },
            {
                data: "status",
                render: function (data, type, row) {
                    if (data) {
                        return "No Action";
                    } else {
                        return '<a style="color:#ffc107;" onclick="tdlGetById(' + row.id + ')"> <i class="fa fa-edit fa-lg"></i></a > | ' +
                            '<a style = "color:#dc3545;" onclick="tdlDelete(' + row.id + ')"> <i class="fa fa-trash fa-lg"></i></a > | ' +
                            '<a style = "color:#28a745;" onclick="tdlUpdateStatus(' + row.id + ')"> <i class="fa fa-check-circle fa-lg"></i></a >'
                    }
                }
            }
        ],
        //'columns': [
        //    {
        //        data: 'status',
        //        "render": function (data, type, row) {
        //            if (data) {
        //                return '<input type="checkbox" class="flat" disabled="disabled" checked="checked"></i></input>';
        //            }
        //            return '<a href="#" onclick="UpdateStatus(' + row.id + ')"><i class="fa fa-check-circle"></i></a>';
        //        }
        //    },
        //    {
        //        data: 'todoName'
        //    },
        //    {
        //        data: 'status',
        //        "render": function (data) {
        //            if (data) {
        //                return 'Completed';
        //            }
        //            return 'Active';
        //        }
        //    },
        //    {
        //        data: 'createDate',
        //        "render": function (data) {
        //            var createdate = moment(data).format('DD/MMMM/YYYY HH:mm');
        //            return createdate;
        //        }
        //    },
        //    {
        //        data: 'status',
        //        "render": function (data, type, row) {
        //            if (data) {
        //                return '<a href="#" class="disabled"><i class="fa fa-edit" ></i></a> | <a href="#" class="disabled"><i class="fa fa-trash"></i></a>';
        //            }
        //            return '<a href="#" onclick="return GetbyID(' + row.id + ')"><i class="fa fa-edit"></i></a> | <a href="#" onclick="Delete(' + row.id + ')"><i class="fa fa-trash"></i></a>';
        //        }
        //    }
        //],
        "columnDefs": [
            {
                "searchable": false,
                "targets": [0, 6]
            },
            {
                "orderable": false,
                "targets": [0, 1, 2, 3, 4, 6]
            },
            {
                "visible": false,
                "targets": [5]
            }
        ],
        "order": [[5, 'asc']]//,
        //"initComplete": function () {
        //    this.api().columns().every(function () {
        //        var column = this;
        //        var text = ["No.", "To Do List Name", "Date", "Completed Date", "Status", "Status", "Action"];
        //        if (column.index() !== 0 && column.index() !== 6) {
        //            var select = $('<select style="width: 100%"><option value="">' + text[column.index()] + '</option></select>')
        //                .appendTo($(column.header()).empty())
        //                .on('change', function () {
        //                    var val = $.fn.dataTable.util.escapeRegex(
        //                        $(this).val()
        //                    );

        //                    column
        //                        .search(val ? '^' + val + '$' : '', true, false)
        //                        .draw();
        //                });
        //            column.data().unique().sort().each(function (d, j) {
        //                select.append('<option value="' + d + '">' + d + '</option>')
        //            });
        //            $('#filter' + column.index()).select2({
        //                theme: 'bootstrap4'
        //            })
        //        }
        //    });
        //}
    });
    tdl.on('order.dt search.dt', function () {
        tdl.column(0, { search: 'applied', order: 'applied' }).nodes().each(function (cell, i) {
            cell.innerHTML = i + 1 + ".";
        });
    }).draw();
});

$('#status').change(function () {
    debugger;
    tdl.ajax.url('/ToDoList/Data/' + $('#status').val()).load();
});

// Clear Screen Input To Do List
function tdlClearScreen() {
    document.getElementById("tdlIdText").disabled = true;
    $("#tdlIdText").val('');
    $("#tdlNameText").val('');
    $("#tdlUpdate").hide();
    $("#tdlSave").show();
}

// Validation Input
function tdlValidation() {
    if ($("#tdlNameText").val().trim() == "") {
        Swal.fire({
            icon: 'error',
            title: 'Oops...',
            text: 'Please Fill To Do List'
        })
    }
    else if ($("#tdlIdText").val() == "" || $("#tdlIdText").val() == " ") {
        tdlSave();
    }
    else {
        debugger
        tdlEdit($("#tdlIdText").val());
    }
}

// Save To Do List
function tdlSave() {
    debugger;
    var todolist = new Object();
    todolist.Name = $("#tdlNameText").val();
    todolist.UserId = "16151"; //$("#tdlUserText").val();
    debugger;
    $.ajax({
        "url": "/ToDoList/Create",
        "type": "POST",
        "dataType": "json",
        "data": todolist
    }).then((result) => {
        if (result.statusCode == 200) {
            Swal.fire({
                icon: 'success',
                title: 'Your data has been saved',
                text: 'Success!'
            }).then((hasil) => {
                location.reload();
            });
            $("#tdlModal").modal("hide");
        }
        else {
            Swal.fire({
                icon: 'error',
                title: 'Your data not saved',
                text: 'Failed!'
            })
        }
    })
}

// Get To Do List Id
function tdlGetById(id) {
    debugger
    $.ajax({
        "url": "/ToDoList/Get/" + id,
        "type": "GET",
        "dataType": "json",
        "data": { Id: id }
    }).then((result) => {
        debugger
        if (result.data != null) {
            debugger
            document.getElementById("tdlIdText").disabled = true;
            debugger
            $("#tdlIdText").val(result.data.id);
            debugger
            $("#tdlNameText").val(result.data.name);
            $("#tdlUserText").val(result.data.userId);
            debugger
            $("#tdlModal").modal("show");
            $("#tdlUpdate").show();
            $("#tdlSave").hide();
        }
    })
}

// Edit To Do List
function tdlEdit(id) {
    var todolist = new Object();
    debugger
    todolist.Id = id;
    todolist.Name = $("#tdlNameText").val();
    todolist.UserId = $("#tdlUserText").val();
    $.ajax({
        "url": "/ToDoList/Edit/",
        "type": "POST",
        "dataType": "json",
        "data": { Id: todolist.Id, Name: todolist.Name, UserId: todolist.UserId }
    }).then((result) => {
        if (result.statusCode == 200) {
            $("#tdlModal").modal("hide");
            Swal.fire({
                icon: 'success',
                title: 'Your data has been updated',
                text: 'Success!'
            }).then((result) => {
                location.reload();
            });
        }
        else {
            Swal.fire({
                icon: 'error',
                title: 'Your data not updated',
                text: 'Failed!'
            })
        }
    })
}

// Delete To Do List
function tdlDelete(id) {
    Swal.fire({
        title: 'Are you sure?',
        text: "You won't be able to revert this!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, delete it!'
    }).then((result) => {
        if (result.value) {
            debugger
            $.ajax({
                "url": "/ToDoList/Delete/",
                "dataType": "json",
                "data": { Id: id }
            }).then((hasil) => {
                debugger
                if (hasil.data[0] != 0) {
                    Swal.fire({
                        icon: 'success',
                        title: 'Your data has been deleted',
                        text: 'Deleted!'
                    }).then((result) => {
                        location.reload();
                    });
                }
                else {
                    Swal.fire({
                        icon: 'error',
                        title: 'Your data not deleted',
                        text: 'Failed!'
                    })
                }
            })
        }
    })
}

// Update Status To Do List
function tdlUpdateStatus(id) {
    $.ajax({
        "url": "/ToDoList/Complete/",
        "type": "POST",
        "dataType": "json",
        "data": { Id: id }
    }).then((result) => {
        if (result.statusCode == 200) {
            $("#tdlModal").modal("hide");
            Swal.fire({
                icon: 'success',
                title: 'Your data has been updated',
                text: 'Success!'
            }).then((result) => {
                location.reload();
            });
        }
        else {
            Swal.fire({
                icon: 'error',
                title: 'Your data not updated',
                text: 'Failed!'
            })
        }
    })
}