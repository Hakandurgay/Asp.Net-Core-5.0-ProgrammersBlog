$(document).ready(function () {
    //@* DataTables start here. *@
    $('#usersTable').DataTable({
        dom:
            "<'row'<'col-sm-3'l><'col-sm-6 text-center'B><'col-sm-3'f>>" +
            "<'row'<'col-sm-12'tr>>" +
            "<'row'<'col-sm-5'i><'col-sm-7'p>>",
        buttons: [
            {
                text: 'Ekle',
                attr: {
                    id: "btnAdd",
                },
                className: 'btn btn-success',
                action: function (e, dt, node, config) {
                }
            },
            {
                text: 'Yenile',
                className: 'btn btn-warning',
                action: function (e, dt, node, config) {
                    $.ajax({
                        type: 'GET',
                        url: 'Admin/Category/GetAllCategories/',  /*'@Url.Action("GetAllCategories", "Category")',*/
                        contentType: "application/json",
                        beforeSend: function () {
                            $('#categoriesTable').hide();
                            $('.spinner-border').show();
                        },
                        success: function (data) {
                            const categoryListDto = jQuery.parseJSON(data);
                            console.log(categoryListDto);
                            if (categoryListDto.ResultStatus === 0) {
                                let tableBody = "";
                                $.each(categoryListDto.Categories.$values,
                                    function (index, category) {
                                        tableBody += `
                                                <tr>
                                    <td>${category.Id}</td>
                                    <td>${category.Name}</td>
                                    <td>${category.Description}</td>
                                    <td>${convertFirstLetterToUpperCase(category.IsActive.toString())}</td>
                                    <td>${convertFirstLetterToUpperCase(category.IsDeleted.toString())}</td>
                                    <td>${category.Note}</td>
                                    <td>${convertToShortDate(category.CreatedDate)}</td>
                                    <td>${category.CreatedByName}</td>
                                    <td>${convertToShortDate(category.ModifiedDate)}</td>
                                    <td>${category.ModifiedByName}</td>

     <td>
                                <button class="btn btn-primary btn-sm  data-id="${category.Id}"><span class="fas fa-edit"></span> Düzenle</button>
                                <button class="btn btn-danger btn-sm btn-delete" data-id="${category.Id}"><span class="fas fa-minus-circle"></span> Sil</button>
                            </td>
</tr>`;
                                    });
                                $('#categoriesTable > tbody').replaceWith(tableBody);
                                $('.spinner-border').hide();
                                $('#categoriesTable').fadeIn(1400); /*@* yavaşca eklenmesini sağlıyor *@*/
                            } else {
                                toastr.error(`${categoryListDto.Message}`, 'İşlem Başarısız!');
                            }
                        },
                        error: function (err) {
                            console.log(err);
                            $('.spinner-border').hide();
                            $('#categoriesTable').fadeIn(1000);
                            toastr.error(`${err.responseText}`, 'Hata!');
                        }
                    });
                }
            }
        ],
        language: {
            "sDecimal": ",",
            "sEmptyTable": "Tabloda herhangi bir veri mevcut değil",
            "sInfo": "_TOTAL_ kayıttan _START_ - _END_ arasındaki kayıtlar gösteriliyor",
            "sInfoEmpty": "Kayıt yok",
            "sInfoFiltered": "(_MAX_ kayıt içerisinden bulunan)",
            "sInfoPostFix": "",
            "sInfoThousands": ".",
            "sLengthMenu": "Sayfada _MENU_ kayıt göster",
            "sLoadingRecords": "Yükleniyor...",
            "sProcessing": "İşleniyor...",
            "sSearch": "Ara:",
            "sZeroRecords": "Eşleşen kayıt bulunamadı",
            "oPaginate": {
                "sFirst": "İlk",
                "sLast": "Son",
                "sNext": "Sonraki",
                "sPrevious": "Önceki"
            },
            "oAria": {
                "sSortAscending": ": artan sütun sıralamasını aktifleştir",
                "sSortDescending": ": azalan sütun sıralamasını aktifleştir"
            },
            "select": {
                "rows": {
                    "_": "%d kayıt seçildi",
                    "0": "",
                    "1": "1 kayıt seçildi"
                }
            }
        }
    });

    $(function () {
        const url = '/Admin/Category/Add/'; /*'@Url.Action("Add", "Category")';*/
        const placeHolderDiv = $('#modalPlaceHolder');
        $('#btnAdd').click(function () {
            $.get(url).done(function (data) {
                placeHolderDiv.html(data);
                placeHolderDiv.find(".modal").modal('show');
            });
        });
        //@* submit butonları jquery kullanılsa bile sayfanın yenilenmesini sağlar.engellemek için aşağıdaki event prevent default eklenir ve kendi click işlemi önlenir *@
        placeHolderDiv.on('click',
            '#btnSave',
            function (event) {
                event.preventDefault();
                const form = $('#form-category-add');
                const actionUrl = form.attr('action'); /*@* formdaki asp - actiona eklenen add i baz alır *@*/
                const dataToSend = form.serialize(); /*@* formdaki veriler alır *@*/

                $.post(actionUrl, dataToSend).done(function (data) {
                    console.log(data);
                    const categoryAddAjaxModel = jQuery.parseJSON(data);
                    console.log(categoryAddAjaxModel);
                    const newFormBody = $('.modal-body', categoryAddAjaxModel.CategoryAddPartial);
                    placeHolderDiv.find('.modal-body').replaceWith(newFormBody);
                    const isValid = newFormBody.find('[name="IsValid"]').val() === 'True';
                    if (isValid) {
                        placeHolderDiv.find('.modal').modal('hide');
                        const newTableRow = `
                                <tr name="${categoryAddAjaxModel.CategoryDto.Category.Id}">
                                                    <td>${categoryAddAjaxModel.CategoryDto.Category.Id}</td>
                                                    <td>${categoryAddAjaxModel.CategoryDto.Category.Name}</td>
                                                    <td>${categoryAddAjaxModel.CategoryDto.Category.Description}</td>
                                                    <td>${convertFirstLetterToUpperCase(categoryAddAjaxModel.CategoryDto.Category.IsActive.toString())}</td>
                                                    <td>${convertFirstLetterToUpperCase(categoryAddAjaxModel.CategoryDto.Category.IsDeleted.toString())}</td>
                                                    <td>${categoryAddAjaxModel.CategoryDto.Category.Note}</td>
                                                    <td>${convertToShortDate(categoryAddAjaxModel.CategoryDto.Category.CreatedDate)}</td>
                                                    <td>${categoryAddAjaxModel.CategoryDto.Category.CreatedByName}</td>
                                                    <td>${convertToShortDate(categoryAddAjaxModel.CategoryDto.Category.ModifiedDate)}</td>
                                                    <td>${categoryAddAjaxModel.CategoryDto.Category.ModifiedByName}</td>
     <td>
                                <button class="btn btn-primary btn-sm btn-update data-id="${categoryAddAjaxModel.CategoryDto.Category.Id} "><span class="fas fa-edit"></span> Düzenle</button>
                                <button class="btn btn-danger btn-sm btn-delete" data-id="${categoryAddAjaxModel.CategoryDto.Category.Id}"><span class="fas fa-minus-circle"></span> Sil</button>
                            </td>
</tr>`;
                        const newTableRowObject = $(newTableRow);
                        newTableRowObject.hide();
                        $('#categoriesTable').append(newTableRowObject);
                        newTableRowObject.fadeIn(3500);
                        toastr.success(`${categoryAddAjaxModel.CategoryDto.Message}`, 'Başarılı İşlem!');
                    } else {
                        let summaryText = "";
                        $('#validation-summary > ul > li').each(function () {
                            let text = $(this).text();
                            summaryText = `*${text}\n`;
                        });
                        toastr.warning(summaryText);
                    }
                });
            });
    });

    $(document).on('click',
        '.btn-delete',
        function (event) {
            event.preventDefault();
            const id = $(this).attr('data-id');
            Swal.fire({
                title: 'Silmek istediğinize emin misiniz?',
                text: "Seçili kategori silinicektir!",
                icon: 'warning',
                showCancelButton: true,
                confirmButtonColor: '#3085d6',
                cancelButtonColor: '#d33',
                confirmButtonText: 'Evet, silmek istiyorum.',
                cancelButtonText: 'Hayır, silmek istemiyorum.'
            }).then((result) => {
                if (result.isConfirmed) {
                    $.ajax({
                        type: 'POST',
                        dataType: 'json',
                        data: { categoryId: id },
                        url: '/Admin/Category/Delete/', /*@Url.Action("Delete", "Category")*/
                        success: function (data) {
                            const result = jQuery.parseJSON(data);
                            if (result.ResultStatus === 0) {
                                Swal.fire(
                                    'Silindi!',
                                    'Seçili kategori başarıyla silinmiştir.',
                                    'success'
                                );
                                const tableRow = $(`[name="${id}"]`);
                                tableRow.fadeOut(3500);
                            } else {
                                Swal.fire({
                                    icon: 'error',
                                    title: 'Bir hata oluştu!',
                                    text: `${result.Message}`,
                                });
                            }
                        },
                        error: function (err) { console.log(err); }
                    })
                }
            })
        });


    /*UPDATE GET İŞLEMİ*/
    $(function () {
        const url = '/Admin/Category/Update';
        const placeHolderDiv = $('#modalPlaceHolder');
        $(document).on('click', '.btn-update', function (event) {

            event.preventDefault();
            const id = $(this).attr('data-id');
            $.get(url, { categoryId: id }).done(function (data) {
                placeHolderDiv.html(data);
                placeHolderDiv.find('.modal').modal('show');
            }).fail(function () {
                toastr.error("Bir hata oluştu.");


            });

        });

        /*UPDATE POST İŞLEMİ*/
        placeHolderDiv.on('click','#btnUpdate',function (event) {
                event.preventDefault();

                const form = $('#form-category-update');
                const actionUrl = form.attr('action');
                const dataToSend = form.serialize();
            $.post(actionUrl, dataToSend).done(function (data) {

                const categoryUpdateAjaxModel = jQuery.parseJSON(data);

                console.log(categoryUpdateAjaxModel);

                    const newFormBody = $('.modal-body', categoryUpdateAjaxModel.CategoryUpdatePartial);
                    placeHolderDiv.find('.modal-body').replaceWith(newFormBody);
                    const isValid = newFormBody.find('[name="IsValid"]').val() === 'True';
                    if (isValid) {
                        placeHolderDiv.find('.modal').modal('hide');
                        const newTableRow = `
                                <tr name="${categoryUpdateAjaxModel.CategoryDto.Category.Id}">
                                                    <td>${categoryUpdateAjaxModel.CategoryDto.Category.Id}</td>
                                                    <td>${categoryUpdateAjaxModel.CategoryDto.Category.Name}</td>
                                                    <td>${categoryUpdateAjaxModel.CategoryDto.Category
                                .Description}</td>
                                                    <td>${convertFirstLetterToUpperCase(categoryUpdateAjaxModel
                                    .CategoryDto.Category.IsActive.toString())}</td>
                                                    <td>${convertFirstLetterToUpperCase(categoryUpdateAjaxModel
                                        .CategoryDto.Category.IsDeleted.toString())}</td>
                                                    <td>${categoryUpdateAjaxModel.CategoryDto.Category.Note}</td>
                                                    <td>${convertToShortDate(categoryUpdateAjaxModel.CategoryDto
                                            .Category.CreatedDate)}</td>
                                                    <td>${categoryUpdateAjaxModel.CategoryDto.Category
                                .CreatedByName}</td>
                                                    <td>${convertToShortDate(categoryUpdateAjaxModel.CategoryDto
                                    .Category.ModifiedDate)}</td>
                                                    <td>${categoryUpdateAjaxModel.CategoryDto.Category
                                .ModifiedByName}</td>
                                                    <td>
                                                        <button class="btn btn-primary btn-sm btn-update" data-id="${categoryUpdateAjaxModel.CategoryDto.Category.Id}"><span class="fas fa-edit"></span></button>
                                                        <button class="btn btn-danger btn-sm btn-delete" data-id="${categoryUpdateAjaxModel.CategoryDto.Category.Id
                            }"><span class="fas fa-minus-circle"></span></button>
                                                    </td>
                                                </tr>`;
                        const newTableRowObject = $(newTableRow);
                        const categoryTableRow = $(`[name="${categoryUpdateAjaxModel.CategoryDto.Category.Id}"]`);
                        newTableRowObject.hide();
                        categoryTableRow.replaceWith(newTableRowObject);
                        newTableRowObject.fadeIn(3500);
                        toastr.success(`${categoryUpdateAjaxModel.CategoryDto.Message}`, "Başarılı İşlem!");
                    } else {
                        let summaryText = "";
                        $('#validation-summary > ul > li').each(function () {
                            let text = $(this).text();
                            summaryText = `*${text}\n`;
                        });
                        toastr.warning(summaryText);
                    }
                }).fail(function (response) {
                    console.log(response);
                });
            });



    });



});