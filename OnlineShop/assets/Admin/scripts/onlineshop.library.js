
var ResponseCode = {
    Success: 1,
    Error: 0,
    ErrorExist: 2,
    DataNull: 3,
    ErrorParam: 4,
    NotPermitted: 5,
    NotValid: 6,
    Locked: 7,
    Overflow: 8,
    ErrorConnect: 9
};

/***********************************************************************

Desc    : Class định nghĩa các method dùng để hiển thị message
************************************************************************/
var sysmess = {
    log: function (msg) {
      
    },
    info: function (msg, func) {
        dialogs.show({ element: $('#dialog-info'), message: msg, process: func });
    },
    error: function (msg, func) {
        dialogs.show({ element: $('#dialog-error'), message: msg, process: func });
    },
    confirm: function (msg, func) {
        dialogs.show({ element: $('#dialog-confirm'), message: msg, process: func });
    },
    warning: function (msg, func) {
        dialogs.show({ element: $('#dialog-warning'), message: msg, process: func });
    }
};
/***********************************************************************
Desc    : Class định nghĩa các method dùng để xử lý thời gian
************************************************************************/
var systimer = {
    today: function (isFull) {
        var current = new Date();
        if (isFull) {
            return sysformat.date(current, 'dd/mm/yyyy hh:mm:ss');
        }
        else {
            return sysformat.date(current, 'dd/mm/yyyy');
        }
    },
    date: function (data) {
        return new Date(parseInt(data.substr(6)));
    },
    datediff: function (date1, date2) {
        if (typeof (date1) == 'string') {
            var from = date1.split("/");
            date1 = new Date(parseInt(from[2]), parseInt(from[0]) - 1, parseInt(from[1]));
        }
        if (typeof (date2) == 'string') {
            var from = date2.split("/");
            date2 = new Date(parseInt(from[2]), parseInt(from[0]) - 1, parseInt(from[1]));
        }
        var data = parseInt(date1.getTime() - date2.getTime(), 10);
        var d, h, m, s;
        s = Math.floor(data / 1000);
        m = Math.floor(s / 60);
        s = s % 60;
        h = Math.floor(m / 60);
        m = m % 60;
        d = Math.floor(h / 24);
        h = h % 24;
        var date = {
            days: d,
            hours: h,
            minutes: m,
            seconds: s
        };
        return date;
    }
};
/***********************************************************************
Desc    : Class định nghĩa các method dùng để format dữ liệu
************************************************************************/
var sysformat = {
    money: function (value, options) {
        var defaults = {
            symbol: '₫',
            separator: ',',
            alignSymbol: 'right'
        };
        var settings = options == null ? defaults : $.extend({}, defaults, options);
        var buf = "";
        var sBuf = "";
        var j = 0;
        value = String(value);

        if (value.indexOf(".") > 0) {
            buf = value.substring(0, value.indexOf("."));
        } else {
            buf = value;
        }
        if (buf.length % 3 != 0 && (buf.length / 3 - 1) > 0) {
            sBuf = buf.substring(0, buf.length % 3) + settings.separator;
            buf = buf.substring(buf.length % 3);
        }
        j = buf.length;
        for (var i = 0; i < (j / 3 - 1); i++) {
            sBuf = sBuf + buf.substring(0, 3) + settings.separator;
            buf = buf.substring(3);
        }
        sBuf = sBuf + buf;
        if (value.indexOf(".") > 0) {
            value = sBuf + value.substring(value.indexOf("."));
        }
        else {
            value = sBuf;
        }
        switch (settings.alignSymbol) {
            case "right":
                value = value + ' ' + settings.symbol;
                break;
            case "left":
                value = settings.symbol + ' ' + value;
                break;
            default:
                value = value + settings.symbol;
                break;
        }
        return value;
    },
    number: function (value) {
        var number = Number(value.replace(/[^0-9\.-]+/g, ""));
        return number;
    },
    date: function (data, fmt) {
        if ($.trim(data) == '') {
            return '';
        }
        var date = null;
        switch (typeof (data)) {
            case 'string':
                var temp = data.split(' ');
                if (temp.length > 1) {
                    date = temp[0].split('/').concat(temp[1].split(':'));
                }
                else {
                    date = temp[0].split('/');
                }
                break;
            case 'object':
                if (data instanceof Date) {
                    date = new Array();
                    if (data.getDate() < 10) {
                        date.push('0' + data.getDate());
                    }
                    else {
                        date.push(data.getDate());
                    }
                    if (data.getMonth() + 1 < 10) {
                        date.push('0' + (data.getMonth() + 1));
                    }
                    else {
                        date.push(data.getMonth() + 1);
                    }
                    date.push(data.getFullYear());
                    if (data.getHours() < 10) {
                        date.push('0' + data.getHours());
                    }
                    else {
                        date.push(data.getHours());
                    }
                    if (data.getMinutes() < 10) {
                        date.push('0' + data.getMinutes());
                    }
                    else {
                        date.push(data.getMinutes());
                    }
                    if (data.getSeconds() < 10) {
                        date.push('0' + data.getSeconds());
                    }
                    else {
                        date.push(data.getSeconds());
                    }
                }
                else {
                    date = String(data);
                }
                break;
            default:
                date = String(data);
        }
        if (fmt == null) fmt = 'mm/dd/yyyy';
        switch (fmt) {
            case 'yyyy/mm/dd':
                return date[2] + '/' + date[1] + '/' + date[0];
            case 'dd/mm/yyyy':
                return date[0] + '/' + date[1] + '/' + date[2];
            case 'mm/dd/yyyy':
                return date[1] + '/' + date[0] + '/' + date[2];
            case 'dd/mm/yyyy hh:mm:ss':
                return date[0] + '/' + date[1] + '/' + date[2] + ' ' + date[3] + ':' + date[4] + ':' + date[5];
            case 'mm/dd/yyyy hh:mm:ss':
                return date[1] + '/' + date[0] + '/' + date[2] + ' ' + date[3] + ':' + date[4] + ':' + date[5];
            case 'hh:mm dd/mm/yyyy':
                return date[3] + ':' + date[4] + ' ' + date[0] + '/' + date[1] + '/' + date[2];
            case 'mm/yyyy':
                return date[1] + '/' + date[2];
            default:
                return data;
        }
    }
};

/***********************************************************************
Author  : VanDH
Desc    : Class định nghĩa các method dùng để valid data
************************************************************************/
var sysvalid = {
    email: function (value) {
        var pattern = new RegExp(/^((([a-z]|\d|[!#\$%&'\*\+\-\/=\?\^_`{\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+(\.([a-z]|\d|[!#\$%&'\*\+\-\/=\?\^_`{\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+)*)|((\x22)((((\x20|\x09)*(\x0d\x0a))?(\x20|\x09)+)?(([\x01-\x08\x0b\x0c\x0e-\x1f\x7f]|\x21|[\x23-\x5b]|[\x5d-\x7e]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(\\([\x01-\x09\x0b\x0c\x0d-\x7f]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]))))*(((\x20|\x09)*(\x0d\x0a))?(\x20|\x09)+)?(\x22)))@((([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.)+(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.?$/i);
        return pattern.test(value);
    },
    json: function (value) {
        if (/^[\],:{}\s]*$/.test(value.replace(/\\["\\\/bfnrtu]/g, '@').replace(/"[^"\\\n\r]*"|true|false|null|-?\d+(?:\.\d*)?(?:[eE][+\-]?\d+)?/g, ']').replace(/(?:^|:|,)(?:\s*\[)+/g, ''))) {
            return true;
        }
        else {
            return false;
        }
    }
};
/***********************************************************************
Desc    : Class định nghĩa các method tiện tích thường dùng
************************************************************************/
var syscommon = {
    scroll: function (obj) {
        try {
            $('html, body').animate({
                scrollTop: obj.offset().top - 50
            }, 200);
        }
        catch (e) {
            sysmess.error('Lỗi javascript! <br />' + e);
        }
    },
    replace: function (value, oldChar, newChar) {
        var regex = new RegExp(oldChar, "g");
        return value.replace(regex, newChar);
    },
    cutstring: function (str, max) {
        if (max >= str.length) {
            return str;
        }
        else {
            var s = str.substring(0, max);
            s = s.substring(0, s.lastIndexOf(" "));
            return s + '...';
        }
    }
};

/***********************************************************************
Desc    : Class định nghĩa các method dùng để gửi và nhận data
************************************************************************/
var sysrequest = {
    defaults: {
        element: null,
        blockUI: null,
        scrollUI: null,
        action: '',
        controller: '',
        type: "POST",
        data: {},
        dataType: "json",
        async: true,
        callback: function (data) { }
    },
    send: function (options) {
        var settings = $.extend({}, sysrequest.defaults, options);
        var url = "/" + settings.controller + (settings.action != '' ? "/" + settings.action : settings.action);
        if (settings.element == null) settings.element = $("div[class='page-content'] div div");
        if (settings.blockUI == null) settings.blockUI = settings.element;
        if (settings.scrollUI == null) settings.scrollUI = settings.blockUI;
        $.ajax({
            url: url,
            type: settings.type,
            data: (settings.data),
            dataType: settings.dataType,
            async: settings.async,
            beforeSend: function () {
                App.blockUI(settings.blockUI);
            },
            error: function (jqXHR, textStatus, errorThrown) {
                sysmess.error('Có lỗi xảy ra khi gửi yêu cầu xử lý!');
                sysmess.log(textStatus);
                sysmess.log(errorThrown);
            },
            success: function (response) {
                switch (settings.dataType) {
                    case "html":
                        settings.element.html(response);
                        settings.callback();
                        syscommon.scroll(settings.scrollUI);
                        break;
                    case "json":
                        settings.callback(response);
                        break;
                }
            },
            complete: function () {
                App.unblockUI(settings.blockUI);
            }
        });
    }
};
(function (win) {
    $(document).ready(function () {
        App.init(); 
        dialogs.init();
        UIJQueryUI.init();
        menu.init();
    });
})(window);