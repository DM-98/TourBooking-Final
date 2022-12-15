function initializeDataTable(tableId) {
    $(document).ready(function () {
        var table = $(tableId).DataTable({
            'lengthChange': false,
            'buttons': {
                'dom': {
                    'button': {
                        'className': 'btn btn-company'
                    }
                },
                'buttons': [
                    {
                        'extend': 'csv',
                        'text': 'Download CSV',
                        'exportOptions': {
                            'columns': ':not(:last-child)'
                        }
                    },
                    {
                        'extend': 'excel',
                        'text': 'Download Excel',
                        'exportOptions': {
                            'columns': ':not(:last-child)'
                        }
                    },
                    {
                        'extend': 'pdf',
                        'text': 'Download PDF',
                        'exportOptions': {
                            'columns': ':not(:last-child)'
                        }
                    },
                    {
                        'extend': 'print',
                        'text': 'Print',
                        'exportOptions': {
                            'columns': ':not(:last-child)'
                        }
                    },
                    {
                        'extend': 'copy',
                        'text': 'Copy to clipboard',
                        'exportOptions': {
                            'columns': ':not(:last-child)'
                        }
                    }
                ]
            },
            'responsive': true,
            'language': {
                'buttons': {
                    'copyTitle': 'Copy to clipboard',
                    'copySuccess': {
                        '_': '%d elements copied to the clipboard',
                        '1': '1 element copied to the clipboard'
                    }
                },
                'emptyTable': 'No data found',
                'info': 'Showing rows _START_-_END_ (page _PAGE_ of _PAGES_)',
                'infoEmpty': '',
                'infoFiltered': '(filtered)',
                'zeroRecords': 'No data found',
                'thousands': '.',
                'search': 'Search:',
                'lengthMenu': 'Show _MENU_ rows',
                'paginate': {
                    'first': 'First page',
                    'last': 'Last page',
                    'next': 'Next',
                    'previous': 'Previous'
                }
            }
        });

        table.buttons().container().appendTo(tableId + '_wrapper .col-md-6:eq(0)');
        $('.dt-buttons').removeClass('btn-group');
        $(tableId + '_filter').children('label').css({ 'margin-top': '5px' });
        $(tableId + '_info').css({ 'margin-top': '-10px' });
        $('.btn').css({ 'margin-bottom': '3px' });
    });
}

function destroyDataTable(tableId) {
    $(document).ready(function () {
        $(tableId).DataTable().destroy();
    });
}

function submitClick() {
    var btn = document.getElementById('submitId');
    var span = document.getElementById('spanId')

    if ($('form').valid()) {
        btn.classList.add('disabled');
        span.classList.remove('d-none');
    }
}