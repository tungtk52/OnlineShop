var dialogs = {
    init: function () {
        $('#dialog-error').dialog({
            autoOpen: false,
            resizable: false,
            dialogClass: 'ui-dialog-red',
            show: {
                effect: "blind",
                duration: 500
            },
            hide: {
                effect: "explode",
                duration: 500
            },
            modal: true,
            buttons: [
	      	{
	      	    "text": "Đóng",
	      	    'class': 'btn',
	      	    click: function () {
	      	        $(this).dialog("close");
	      	    }
	      	}
	      ]
        });
        $('#dialog-info').dialog({
            autoOpen: false,
            resizable: false,
            dialogClass: 'ui-dialog-blue',
            show: {
                effect: "blind",
                duration: 500
            },
            hide: {
                effect: "explode",
                duration: 500
            },
            modal: true,
            buttons: [
	      	{
	      	    "text": "Đóng",
	      	    'class': 'btn',
	      	    click: function () {
	      	        $(this).dialog("close");
	      	    }
	      	}
	      ]
        });
        $('#dialog-warning').dialog({
            autoOpen: false,
            resizable: false,
            dialogClass: 'ui-dialog-yellow',
            show: {
                effect: "blind",
                duration: 500
            },
            hide: {
                effect: "explode",
                duration: 500
            },
            modal: true,
            buttons: [
	      	{
	      	    "text": "Đóng",
	      	    'class': 'btn',
	      	    click: function () {
	      	        $(this).dialog("close");
	      	    }
	      	}]
        });
        $('#dialog-confirm').dialog({
            autoOpen: false,
            resizable: false,
            dialogClass: 'ui-dialog-green',
            show: {
                effect: "blind",
                duration: 500
            },
            hide: {
                effect: "explode",
                duration: 500
            },
            modal: true,
            buttons: [
            {
                'class': 'btn red',
                'text': 'Đồng ý',
                click: function () {
                    $(this).dialog("close");
                    dialogs.process();
                }
            },
	      	{
	      	    'class': 'btn',
	      	    'text': 'Hủy',
	      	    click: function () {
	      	        $(this).dialog("close");
	      	    }
	      	}]
        });
    },
    show: function (options) {
        $('span.dialog-msg', options.element).html(options.message);
        if (typeof options.process === 'function') {
            dialogs.process = options.process;
        }
        else {
            dialogs.process = function () { };
        }
        options.element.dialog("open");
        $('.ui-dialog button').blur();
    },
    process: function () { }
};