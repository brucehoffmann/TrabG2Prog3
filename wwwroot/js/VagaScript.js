VagaScript = (function () {
    var datatable = {};

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
                        return `<div class='text-center'><button class="btn btn-primary" onclick="VagaScript.reservar('${row.id}')">Reservar</button></div>`;
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

    var reservar = function (vagaId) {
        vagaId;
        debugger
    }

    document.addEventListener('DOMContentLoaded', function () {
        start();
    });

    return {
        reservar: reservar
    };

})();