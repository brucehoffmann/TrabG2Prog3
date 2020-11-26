VagaScript = (function () {
    var datatable = {};
    var vagaIdParaRemover = 0;

    var start = function () {
        datatable = $('#table').DataTable({
            "ajax": {
                "url": `/Vaga/GetVagas`,
                "error": function (xhr, textStatus, errorThrown) {
                    alert(xhr.responseText);
                }
            },
            "initComplete": function (settings, json) {
                appFormData = json.data;
            },
            "columns": [
                {
                    name: 'andar',
                    data: "andar",
                    title: "Andar",
                    class: "text-center",
                    render: function (data, type, row, meta) {
                        return `<div class='text-center'>${data}</div>`;
                    }
                },
                {
                    name: 'corredor',
                    data: "corredor",
                    title: "Corredor",
                    class: "text-center",
                    render: function (data, type, row, meta) {
                        return `<div class='text-center'>${data}</div>`;
                    }
                },
                {
                    name: 'numero',
                    data: "numero",
                    title: "Número vaga",
                    class: "text-center",
                    render: function (data, type, row, meta) {
                        return `<div class='text-center'>${data}</div>`;
                    }
                },
                {
                    name: 'porteVeiculo',
                    data: "porteVeiculo",
                    title: "Porte do veículo",
                    class: "text-center",
                    render: function (data, type, row, meta) {
                        return `<div class='text-center'>${data}</div>`;
                    }
                },
                {
                    name: 'ocupada',
                    data: "ocupada",
                    title: "Status Vaga",
                    class: "text-center",
                    render: function (data, type, row, meta) {
                        if (data === true)
                            return `<div class='text-center'>Ocupada</div>`;
                        else
                            return `<div class='text-center'>Livre</div>`;
                    }
                },
                {
                    name: 'acoes',
                    searchable: false,
                    orderable: false,
                    title: "",
                    class: "text-center",
                    render: function (data, type, row, meta) {
                        if (row.ocupada)
                            return `<div class='text-center'>
                                        <button class="btn btn-secondary" onclick="VagaScript.liberar('${row.id}')">Liberar</button>
                                        <button class="btn btn-danger" onclick="VagaScript.deletar('${row.id}')"><i class="fa fa-trash" aria-hidden="true"></i></button>
                                    </div>`;
                        else
                            return `<div class='text-center'>
                                        <button class="btn btn-primary" onclick="VagaScript.ocupar('${row.id}')">Ocupar</button>
                                        <button class="btn btn-danger" onclick="VagaScript.deletar('${row.id}')"><i class="fa fa-trash" aria-hidden="true"></i></button>
                                    </div>`;
                    }
                }
            ],
            "processing": false,
            "paging": true,
            "language": {
                processing: '<i class="fa fa-spinner fa-spin fa-3x fa-fw"></i><span class="sr-only">Carregando...</span> '
            },
            "oLanguage": {
                "sEmptyTable": "Sem vagas para exibir",
                "sProcessing": "Processando...",
                "sLengthMenu": "Exibir _MENU_ itens",
                "sZeroRecords": "Nenhuma vaga encontrada",
                "sInfo": "Exibindo do _START_ ao _END_ de _TOTAL_ registros",
                "infoEmpty": "",
                "sInfoFiltered": "",
                "sInfoPostFix": "",
                "sSearch": "Buscar:",
                "sUrl": "",
                "oPaginate": {
                    "sFirst": " << ",
                    "sPrevious": " < ",
                    "sNext": " > ",
                    "sLast": " >> "
                }
            }
        });
    };

    var ocupar = function (vagaId) {
        $.ajax({
            url: "/Vaga/Ocupar/" + vagaId,
            type: "post",
            error: function (error) {
                var mensagemDeErro = JSON.parse(error.responseText);
                alert(mensagemDeErro.message);
            },
            success: function (data, textStatus, XMLHttpRequest) {
                datatable.ajax.reload();
            }
        });
    }

    var liberar = function (vagaId) {
        $.ajax({
            url: "/Vaga/Liberar/" + vagaId,
            type: "post",
            error: function (error) {
                var mensagemDeErro = JSON.parse(error.responseText);
                alert(mensagemDeErro.message);
            },
            success: function (data, textStatus, XMLHttpRequest) {
                datatable.ajax.reload();
            }
        });
    }

    var deletar = function (vagaId) {
        $("#modal").modal('show');
        vagaIdParaRemover = vagaId;
    }

    var confirmarDelete = function () {
        $.ajax({
            url: "/Vaga/DeletaVaga/" + vagaIdParaRemover,
            type: "delete",
            error: function (error) {
                var mensagemDeErro = JSON.parse(error.responseText);
                alert(mensagemDeErro.message);
            },
            success: function (data, textStatus, XMLHttpRequest) {
                datatable.ajax.reload();
            }
        });
    }

    document.addEventListener('DOMContentLoaded', function () {
        start();
    });

    return {
        ocupar: ocupar,
        liberar: liberar,
        deletar: deletar,
        confirmarDelete: confirmarDelete
    };

})();