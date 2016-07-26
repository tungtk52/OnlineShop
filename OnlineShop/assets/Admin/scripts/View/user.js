var user_login = {
    init: function () {
            //$("#btnLogin").off("click");
            //$("#btnLogin").on("click", function () {
            //    var attr=$("[name='remember']").parent().attr("class");
            //    var remember=0;
            //    if(attr=="checked")
            //    {
            //        remember=1;
            //    }
            //    var option = {
            //        controller: "Admin/User",
            //        action: "LogOn",
            //        data: {
            //            UserName: $("[name='username']").val(),
            //            Password: $("[name='password']").val(),
            //            Remember: remember ,
            //        },
            //        callback: function (response) {
            //            if (response.Code == ResponseCode.Success) {

            //            }
            //            else {
            //                $("#frmLogOn div:eq(0)").removeClass("hide");
            //                $("#frmLogOn div:eq(0) span").html(response.Message);
            //            }
            //        }
            //    };
            //    sysrequest.send(option);
        //});
        $(document).ready(function () {
            $(".alert-error").addClass("hide");
            $(".alert-error span").html("Nhập tài khoản và mật khẩu.");
            if (status == 'False' && $("#txtUserName").val() != "" && $("#txtPassword").val() != "") {
                $(".alert-error").removeClass("hide");
                $(".alert-error span").html("Tài khoản và mật khẩu không đúng.");
            }
            $("#btnLogin").off("click");
            $("#btnLogin").on("click", function () {
                $(".alert-error").addClass("hide");
                $(".alert-error span").html("Nhập tài khoản và mật khẩu.");
                $("#frmLogOn").submit();
            });
        });
    }
}

var user_list = {
    init: function () {
        $(document).ready(function () {
            $("#btnAddNew", $("#listUser")).off("click");
            $("#btnAddNew", $("#listUser")).on("click", function () {
                user_list.reset();
                $("#divAddNewUser").removeClass("hide");
                $("#wapListUser").addClass("hide");
                $("hidId", $("#frmCreateUser")).val(0);
                $("#txtPassword", $("#frmCreateUser")).parent().parent().parent().removeClass("hide");
                $("#divAddNewUser div div div[class='caption']").html("<i class=\"icon-plus\"></i> Thêm mới tài khoản");
            });
            
            $("#btnSaveUser", $("#frmCreateUser")).off("click");
            $("#btnSaveUser", $("#frmCreateUser")).on("click", function () {
                if (user_list.validate())
                {
                    user_list.save();
                }
            });

            $("#btnResetUserInput", $("#frmCreateUser")).off("click");
            $("#btnResetUserInput", $("#frmCreateUser")).on("click", function () {
                user_list.reset();
            });

            $("[name='btnBackToListUser']", $("#frmCreateUser")).off("click");
            $("[name='btnBackToListUser']", $("#frmCreateUser")).on("click", function () {
                $("#divAddNewUser").addClass("hide");
                $("#wapListUser").removeClass("hide");
            });

            $("[name='btnEditUser']", $("#listUser")).off("click");
            $("[name='btnEditUser']", $("#listUser")).on("click", function () {
                var current = $(this);
                var option = {
                    controller: "Admin/User",
                    action: "GetById",
                    blockUI:$("div[class='page-content']"),
                    data: {
                        id:current.attr("uid")
                    },
                    callback:function(response)
                    {
                        if(response.Code==ResponseCode.Success)
                        {
                            var form=$("#frmCreateUser");
                            $("#hidId", form).val(response.Data.Id);
                            $("#txtUserName", form).val(response.Data.UserName);
                            $("#txtFullName", form).val(response.Data.FullName);
                            $("#txtAddress", form).val(response.Data.Address);
                            $("#txtEmail", form).val(response.Data.Email);
                            $("#txtPhone", form).val(response.Data.Phone);
                            $("#txtPassword", form).parent().parent().parent().addClass("hide");
                            $("#divAddNewUser").removeClass("hide");
                            $("#wapListUser").addClass("hide");
                            $("#divAddNewUser div div div[class='caption']").html("<i class=\"icon-edit\"></i> Sửa tài khoản");
                        }
                        else
                        {
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
        var form = $("#frmCreateUser");
        var id = $("#hidId", form).val();
        var option = {
            controller: "Admin/User",
            action: "Save",
            blockUI: $("div[class='page-content']"),
            data: {
                Id:id,
                UserName: $("#txtUserName", form).val(),
                Password: $("#txtPassword", form).val(),
                FullName: $("#txtFullName", form).val(),
                Email: $("#txtEmail", form).val(),
                Phone: $("#txtPhone", form).val(),
                Address: $("#txtAddress", form).val()
            },
            callback:function(response)
            {
                if(response.Code==ResponseCode.Success)
                {
                    if (id > 0)
                    {
                        sysmess.info("Sửa thành công!");
                        var row = $("a[name='btnEditUser'][uid='"+id+"']").parent().parent().parent().parent().parent();
                        $("td:eq(2)", row).html($("#txtFullName", form).val());
                        $("td:eq(3)", row).html($("#txtPhone", form).val());
                        $("td:eq(4)", row).html($("#txtEmail", form).val());
                        $("td:eq(5)", row).html($("#txtAddress", form).val());
                    }
                    else {
                        sysmess.info("Thêm mới thành công!");
                    }
                    $("#divAddNewUser").addClass("hide");
                    $("#wapListUser").removeClass("hide");
                    user_list.reset();
                }
                else {
                    sysmess.error(response.Message);
                }
            }
        }
        sysrequest.send(option);
    },
    
    delete:function(id,current){
        var option = {
            controller: "Admin/User",
            action: "Delete",
            data: {
                id:id
            },
            callback:function(response)
            {
                if(response.Code==ResponseCode.Success)
                {
                    current.parent().parent().parent().parent().parent().remove();
                }
                else {
                    sysmess.error(response.Message);
                }
            }
        };
        sysrequest.send(option);
    },
    validate: function () {
        var result = true;
        var focus = null;
        var form = $("#frmCreateUser");
        if ($("#txtUserName", form).val() == null || $("#txtUserName", form).val() == "") {
            $("#txtUserName", form).parent("div").parent("div").addClass("error");
            $("#txtUserName", form).next().remove();
            $("#txtUserName", form).parent("div")
                .append(" <span class=\"help-inline no-left-padding\">Bạn phải nhập tên đăng nhập.</span>");
            result = false;
            focus = $("#txtUserName", form);
        }
        
        if ($("#txtPassword", form).parent().parent().parent().attr("class") != "span6 hide")
        {
            if ($("#txtPassword", form).val() == null || $("#txtPassword", form).val() == "") {
                $("#txtPassword", form).parent("div").parent("div").addClass("error");
                $("#txtPassword", form).next().remove();
                $("#txtPassword", form).parent("div")
                    .append(" <span class=\"help-inline no-left-padding\">Bạn phải nhập mật khẩu.</span>");
                result = false;
                if (focus == null) {
                    focus = $("#txtPassword", form);
                }
            }
        }
        
        if ($("#txtEmail", form).val() == null || $("#txtEmail", form).val() == "") {
            $("#txtEmail", form).parent("div").parent("div").addClass("error");
            $("#txtEmail", form).next().remove();
            $("#txtEmail", form).parent("div")
                .append(" <span class=\"help-inline no-left-padding\">Bạn phải nhập email.</span>");
            result = false;
            if (focus == null) {
                focus = $("#txtEmail", form);
            }
        }
        
        if (focus != null) {
            focus.focus();
            syscommon.scroll(focus);
        }

        return result;
    },
    reset: function () {
        var form = $("#frmCreateUser");
        $("[type='text'],[type='password']", form).each(function () {
            $(this).val("");
        });
        $("span", form).each(function () {
            $(this).removeClass("checked");
        });

        //Xoa cac thong bao khi data nhap vao khong chinh xac
        $("span.help-inline", form).each(function () {
            $(this).parent("div").parent("div").removeClass("error");
            $(this).remove();

        });
    }
};