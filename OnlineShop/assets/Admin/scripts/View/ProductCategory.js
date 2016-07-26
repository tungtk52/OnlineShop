var product_category = {
    init: function () {
        $(document).ready(function () {
            $("#btnAddNew", $("#listProductCategory")).off("click");
            $("#btnAddNew", $("#listProductCategory")).on("click", function () {
                product_category.reset();
                product_category.load_parent($("#ddlParent"));
                $("#wapListProductCategory").addClass("hide");
                $("#divAddNewProductCategory").removeClass("hide");
                $("#divAddNewProductCategory div div div[class='caption']").html("<i class=\"icon-edit\"></i> Thêm nhóm sản phẩm");
                $("#hidId", $("#divAddNewProductCategory")).val(0);
            });

            $("#btnSaveProductCategory", $("#frmCreateProductCategory")).off("click");
            $("#btnSaveProductCategory", $("#frmCreateProductCategory")).on("click", function () {
                if (product_category.validate()) {
                    product_category.save();
                }
            });

            $("#btnResetProductCategoryInput", $("#frmCreateProductCategory")).off("click");
            $("#btnResetProductCategoryInput", $("#frmCreateProductCategory")).on("click", function () {
                product_category.reset();
            });

            $("[name='btnBackToListProductCategory']", $("#frmCreateProductCategory")).off("click");
            $("[name='btnBackToListProductCategory']", $("#frmCreateProductCategory")).on("click", function () {
                $("#divAddNewProductCategory").addClass("hide");
                $("#wapListProductCategory").removeClass("hide");
            });

            $("[name='btnEditProductCategory']", $("#listProductCategory")).off("click");
            $("[name='btnEditProductCategory']", $("#listProductCategory")).on("click", function () {
                var current = $(this);
                var option = {
                    controller: "Admin/ProductCategory",
                    action: "GetById",
                    blockUI: $("div[class='page-content']"),
                    data: {
                        id: current.attr("pcid")
                    },
                    callback: function (response) {
                        if (response.Code === ResponseCode.Success) {
                            var form = $("#frmCreateProductCategory");
                            $("#hidId", form).val(response.Data.Id);
                            $("#txtName", form).val(response.Data.Name);
                            $("#ddlParent", form).val(response.Data.ParentId);
                            $("#txtDisplayOrder", form).val(response.Data.DisplayOrder);
                            $("#txtSeoTitle", form).val(response.Data.SeoTitle);
                            $("#txtMetaKeyword", form).val(response.Data.MetaKeyword);
                            $("#MetaDescription", form).val(response.Data.MetaDescription);

                            if (response.Data.Status === true) {
                                $("#divStatus div div span:eq(0)").addClass("checked");
                                $("#divStatus div div span:eq(1)").removeClass("checked");
                            }
                            else {
                                $("#divStatus div div span:eq(0)").removeClass("checked");
                                $("#divStatus div div span:eq(1)").addClass("checked");
                            }

                            if (response.Data.ShowOnHome === true) {
                                $("#divShowOnHome div div span:eq(0)").addClass("checked");
                                $("#divShowOnHome div div span:eq(1)").removeClass("checked");
                            }
                            else {
                                $("#divShowOnHome div div span:eq(0)").removeClass("checked");
                                $("#divShowOnHome div div span:eq(1)").addClass("checked");
                            }

                            $("#txtMetaDescription", form).val(response.Data.ShowOnHome),

                            $("#divAddNewProductCategory").removeClass("hide");
                            $("#wapListProductCategory").addClass("hide");
                            $("#divAddNewProductCategory div div div[class='caption']").html("<i class=\"icon-edit\"></i> Sửa nhóm sản phẩm");
                        }
                        else {
                            sysmess.error(response.Message);
                        }
                    }
                };
                sysrequest.send(option);
            });

            $("[name='btnDeleteUser']", $("#listUser")).off("click");
            $("[name='btnDeleteUser']", $("#listUser")).on("click", function () {
                var current = $(this);
                sysmess.confirm("Bạn có chắc chắn muốn xóa tài khoản này?", function () {
                    user_list.delete(current.attr("uid"), current);
                });
            });
        });
    },
    save:function()
    {
        var form = $("#frmCreateProductCategory");
        var id = $("#hidId", form).val();
        var status=false;
        if( $("#divStatus div div span[class='checked'] input").val() === 1)
        {
            status=true;
        }

        var showOnHome=false;
        if( $("#divShowOnHome div div span[class='checked'] input").val() === 1)
        {
            showOnHome=true;
        }
        var option = {
            controller: "Admin/ProductCategory",
            action: "Save",
            blockUI: $("div[class='page-content']"),
            data: {
                Id: id,
                Name: $("#txtName", form).val(),
                ParentId: $("#ddlParent", form).val(),
                DisplayOrder: $("#txtDisplayOrder", form).val(),
                SeoTitle: $("#txtSeoTitle", form).val(),
                MetaKeyword: $("#txtMetaKeyword", form).val(),
                MetaDescription: $("#txtMetaDescription", form).val(),
                Status:status,
                ShowOnHome: showOnHome
            },
            callback: function (response) {
                if (response.Code === ResponseCode.Success) {
                    if (id > 0) {
                        sysmess.info("Sửa thành công!");
                        var row = $("a[name='btnEditProductCategory'][pcid='" + id + "']").parent().parent().parent().parent().parent();
                        $("td:eq(1)", row).html($("#txtName", form).val());
                        $("td:eq(2)", row).html($("#ddlParent option:selected", form).text());
                        $("td:eq(3)", row).html($("#txtDisplayOrder", form).val());
                        $("td:eq(4)", row).html($("#txtMetaKeyword", form).val());
                        if (status === true)
                        {
                            $("td:eq(5)", row).html("<span class=\"label label-success\">Hoạt động</span>");
                        }
                        else
                        {
                            $("td:eq(5)", row).html("<span class=\"label label-important\">Khóa</span>");
                        }
                        if (showOnHome === true)
                        {
                            $("td:eq(6)", row).html("<span class=\"label label-success\">Hiển thị</span>");
                        }
                        else {
                            $("td:eq(6)", row).html("<span class=\"label label-important\">Không</span>");
                        }
                    }
                    else {
                        $("a[controller='Admin/ProductCategory'][action='Index']", $("#divLeftMenu")).trigger("click");
                    }
                    $("#divAddNewProductCategory").addClass("hide");
                    $("#wapListProductCategory").removeClass("hide");
                    product_category.reset();
                }
                else {
                    sysmess.error(response.Message);
                }
            }
        }
        if (product_category.validate()) {
            sysrequest.send(option);
        }
    },
    delete: function (id, current) {
        var option = {
            controller: "Admin/ProductCategory",
            action: "Delete",
            data: {
                id: id
            },
            callback: function (response) {
                if (response.Code === ResponseCode.Success) {
                    current.parent().parent().parent().parent().parent().remove();
                }
                else {
                    sysmess.error(response.Message);
                }
            }
        };
        sysmess.confirm("Bạn có chắc chắn muốn xóa nhóm sản phẩm này?", function() {
            sysrequest.send(option);
        });
    },
    validate: function () {
        var result = true;
        var focus = null;
        var form = $("#frmCreateProductCategory");
        if ($("#txtName", form).val() == null || $("#txtName", form).val() === "") {
            $("#txtName", form).parent("div").parent("div").addClass("error");
            $("#txtName", form).next().remove();
            $("#txtName", form).parent("div")
                .append(" <span class=\"help-inline no-left-padding\">Bạn phải nhập tên nhóm sản phẩm.</span>");
            result = false;
            focus = $("#txtName", form);
        }

        if ($("#txtDisplayOrder", form).val() == null || $("#txtDisplayOrder", form).val() === "") {
            $("#txtDisplayOrder", form).parent("div").parent("div").addClass("error");
            $("#txtDisplayOrder", form).next().remove();
            $("#txtDisplayOrder", form).parent("div")
                .append(" <span class=\"help-inline no-left-padding\">Bạn phải nhập thứ tự.</span>");
            result = false;
            if (focus == null) {
                focus = $("#txtDisplayOrder", form);
            }
        }

        if ($("#txtSeoTitle", form).val() == null || $("#txtSeoTitle", form).val() === "") {
            $("#txtSeoTitle", form).parent("div").parent("div").addClass("error");
            $("#txtSeoTitle", form).next().remove();
            $("#txtSeoTitle", form).parent("div")
                .append(" <span class=\"help-inline no-left-padding\">Bạn phải nhập tiêu đề SEO.</span>");
            result = false;
            if (focus == null) {
                focus = $("#txtSeoTitle", form);
            }
        }

        if ($("#txtMetaKeyword", form).val() == null || $("#txtMetaKeyword", form).val() === "") {
            $("#txtMetaKeyword", form).parent("div").parent("div").addClass("error");
            $("#txtMetaKeyword", form).next().remove();
            $("#txtMetaKeyword", form).parent("div")
                .append(" <span class=\"help-inline no-left-padding\">Bạn phải nhập từ khóa.</span>");
            result = false;
            if (focus == null) {
                focus = $("#txtMetaKeyword", form);
            }
        }

        if ($("#txtMetaDescription", form).val() == null || $("#txtMetaDescription", form).val() === "") {
            $("#txtMetaDescription", form).parent("div").parent("div").addClass("error");
            $("#txtMetaDescription", form).next().remove();
            $("#txtMetaDescription", form).parent("div")
                .append(" <span class=\"help-inline no-left-padding\">Bạn phải nhập mô tả.</span>");
            result = false;
            if (focus == null) {
                focus = $("#txtMetaDescription", form);
            }
        }

        if (focus != null) {
            focus.focus();
            syscommon.scroll(focus);
        }

        return result;
    },
    reset: function () {
        var form = $("#frmCreateProductCategory");
        $("[type='text'],textarea,[type='password']", form).each(function () {
            $(this).val("");
        });
        $("span", form).each(function () {
            $(this).removeClass("checked");
        });
        
        $("#divStatus div div span:eq(0)").addClass("checked");
        $("#divShowOnHome div div span:eq(0)").addClass("checked");

        //Xoa cac thong bao khi data nhap vao khong chinh xac
        $("span.help-inline", form).each(function () {
            $(this).parent("div").parent("div").removeClass("error");
            $(this).remove();

        });
    },
    load_parent:function(element)
    {
        var option = {
            controller: "Admin/ProductCategory",
            action: "GetAll",
            blockUI:element,
            callback:function(response)
            {
                if(response.Code === ResponseCode.Success)
                {
                    if(response.Data.length>0)
                    {
                        var html = "";
                        html += "<option value=\"0\">--Chọn nhóm cha--</option>";
                        for(var i=0;i<response.Data.length;i++)
                        {
                            var item = response.Data[i];
                            html += "<option value='"+item.Id+"'>"+item.Name+"</option>";
                        }
                        element.html(html);
                    }
                }
                else
                {
                    sysmess.error(response.Message);
                }
            }
        }
        sysrequest.send(option);
    }
};