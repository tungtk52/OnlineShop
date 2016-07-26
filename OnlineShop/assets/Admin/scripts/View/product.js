var product_index = {
    init: function () {
        $(document).ready(function () {
            $("#btnAddNew", $("#listProduct")).off("click");
            $("#btnAddNew", $("#listProduct")).on("click", function () {
                product_index.reset();
                product_index.load_parent($("#ddlCategory"));
                $("#divAddNewProduct").removeClass("hide");
                $("#wapListProduct").addClass("hide");
            });

            $("[name='btnBackToListProduct']", $("#frmCreateProduct")).off("click");
            $("[name='btnBackToListProduct']", $("#frmCreateProduct")).on("click", function () {
                $("#divAddNewProduct").addClass("hide");
                $("#wapListProduct").removeClass("hide");
                syscommon.scroll($("#wapListProduct"));
            });

            $("#btnSelectImage", $("#frmCreateProduct")).off("click");
            $("#btnSelectImage", $("#frmCreateProduct")).on("click", function (e) {
                e.preventDefault();
                var finder = new CKFinder();
                finder.selectActionFunction=function (url) {
                    $("#txtImage", $("#frmCreateProduct")).val(url);
                };
                finder.popup();
            });

            var editor = CKEDITOR.replace("txtDescription", {
                customConfig: '/assets/Admin/scripts/plugin/ckeditor/config.js'
            });

            $("#btnSaveProduct", $("#frmCreateProduct")).off("click");
            $("#btnSaveProduct", $("#frmCreateProduct")).on("click", function() {
                product_index.save();
            });
            $("#btnResetProductInput", $("#frmCreateProduct")).off("click");
            $("#btnResetProductInput", $("#frmCreateProduct")).on("click", function () {
                product_index.reset();
            });
            $("#btnBackToListProduct", $("#frmCreateProduct")).off("click");
            $("#btnBackToListProduct", $("#frmCreateProduct")).on("click", function () {
                product_index.reset();
                $("#divAddNewProduct").removeClass("hide");
                $("#wapListProduct").addClass("hide");
            });
        });
    },
    save: function () {
        var form = $("#frmCreateProduct");
        var item = $("[name='optionStatus']").parent().attr("class", "checked");
        
        var option = {
            controller: "Product",
            action: "Save",
            blockUI: $("div[class='page-content']"),
            data: {
                Id: $("#hidId", form).val(),
                Code: $("#txtCode", form).val(),
                Name: $("#txtName", form).val(),
                CategoryId: $("#ddlCategory", form).val(),
                Image: $("#txtImage", form).val(),
                Price: $("#txtPrice", form).val(),
                Promotion: $("#txtPromotion", form).val(),
                MetaKeyword: $("#txtMetaKeyword", form).val(),
                Description: CKEDITOR.instances.txtDescription.getData(),
                Status: $("[name='optionStatus']",item).val()
            },
            callback: function(response) {
                if (response.Code === ResponseCode.Success) {
                    $("#divAddNewProduct").addClass("hide");
                    sysmess.info("Thêm mới thành công!");
                } else {
                    sysmess.error(response.Message);
                }
            }
        };
        //if (product_index.validate()) {
            sysrequest.send(option);
        //}
    },
    validate: function () {
        var result = true;
        var focus = null;
        var form = $("#frmCreateProduct");
        if ($("#txtCode", form).val() == null || $("#txtCode", form).val() === "") {
            $("#txtCode", form).parent("div").parent("div").addClass("error");
            $("#txtCode", form).next().remove();
            $("#txtCode", form).parent("div")
                .append(" <span class=\"help-inline no-left-padding\">Bạn phải nhập mã sản phẩm.</span>");
            result = false;
            focus = $("#txtCode", form);
        }

        if ($("#txtName", form).val() == null || $("#txtName", form).val() === "") {
            $("#txtName", form).parent("div").parent("div").addClass("error");
            $("#txtName", form).next().remove();
            $("#txtName", form).parent("div")
                .append(" <span class=\"help-inline no-left-padding\">Bạn phải nhập tên sản phẩm.</span>");
            result = false;
            if (focus == null) {
                focus = $("#txtName", form);
            }
        }

        if ($("#ddlCategory", form).val() == null || $("#ddlCategory", form).val() === "" || $("#ddlCategory", form).val()==="0") {
            $("#ddlCategory", form).parent("div").parent("div").addClass("error");
            $("#ddlCategory", form).next().remove();
            $("#ddlCategory", form).parent("div")
                .append(" <span class=\"help-inline no-left-padding\">Bạn phải chọn nhóm sản phẩm.</span>");
            result = false;
            if (focus == null) {
                focus = $("#ddlCategory", form);
            }
        }

        if ($("#txtImage", form).val() == null || $("#txtImage", form).val() === "") {
            $("#txtImage", form).parent("div").parent("div").addClass("error");
            $("#txtImage", form).next().next().remove();
            $("#txtImage", form).parent("div")
                .append(" <span class=\"help-inline no-left-padding\">Bạn phải chọn ảnh sản phẩm.</span>");
            result = false;
            if (focus == null) {
                focus = $("#txtImage", form);
            }
        }

        if ($("#txtPrice", form).val() == null || $("#txtPrice", form).val() === "") {
            $("#txtPrice", form).parent("div").parent("div").addClass("error");
            $("#txtPrice", form).next().remove();
            $("#txtPrice", form).parent("div")
                .append(" <span class=\"help-inline no-left-padding\">Bạn phải nhập mức giá.</span>");
            result = false;
            if (focus == null) {
                focus = $("#txtPrice", form);
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
        var form = $("#frmCreateProduct");
        $("[type='text'],textarea,[type='password']", form).each(function () {
            $(this).val("");
        });
        $("span", form).each(function () {
            $(this).removeClass("checked");
        });

        $("#divStatus div div span:eq(0)").addClass("checked");

        //Xoa cac thong bao khi data nhap vao khong chinh xac
        $("span.help-inline", form).each(function () {
            $(this).parent("div").parent("div").removeClass("error");
            $(this).remove();

        });
    },
    load_parent: function(element) {
        var option = {
            controller: "Admin/ProductCategory",
            action: "GetAll",
            blockUI: element,
            callback: function(response) {
                if (response.Code === ResponseCode.Success) {
                    if (response.Data.length > 0) {
                        var html = "";
                        html += "<option value=\"0\">--Chọn nhóm sản phẩm--</option>";
                        for (var i = 0; i < response.Data.length; i++) {
                            var item = response.Data[i];
                            html += "<option value='" + item.Id + "'>" + item.Name + "</option>";
                        }
                        element.html(html);
                    }
                } else {
                    sysmess.error(response.Message);
                }
            }
        }
        sysrequest.send(option);
    }
};