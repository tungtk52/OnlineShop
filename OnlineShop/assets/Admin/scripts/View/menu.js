var menu = {
    init: function () {
        $("a[name='itemMenu']", $("#divLeftMenu")).each(function () {
            var current = $(this);
            current.off("click");
            current.on("click", function () {
                var controller = current.attr('controller');
                if (controller != null && controller.length > 0) {
                    var action = current.attr('action');
                    if (action == "" || action == null) {
                        action = "Index";
                    }
                    var options = {
                        controller: controller,
                        element: $("[class='page-content'] div div"),
                        blockUI: $("[class='page-content']"),
                        dataType: 'html',
                        action: action
                    };
                    sysrequest.send(options);
                }
            });
        });
    }
};