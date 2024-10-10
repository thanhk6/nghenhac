function vsw_exec_cmd(cmdName) {
    cmdName = cmdName.replace('-', '');

    if (cmdName) {
        var cmdParam = '';
        var listCid;
        var i;
        if (cmdName === "copy") {
            listCid = document.getElementsByName('cid');
            for (i = 0; i < listCid.length; i++) {
                if (listCid[i].checked) {
                    cmdParam = listCid[i].value;
                    break;
                }
            }
        }
        else if (cmdName === "publish" || cmdName === "unpublish" || cmdName === 'delete') {
            listCid = document.getElementsByName('cid');
            for (i = 0; i < listCid.length; i++) {
                if (listCid[i].checked) {
                    cmdParam += (cmdParam === '' ? '' : ',') + listCid[i].value;
                }
            }
        }
        else if (cmdName === "edit") {
            listCid = document.getElementsByName('cid');
            for (i = 0; i < listCid.length; i++) {
                if (listCid[i].checked) {
                    cmdParam = listCid[i].value;
                    break;
                }
            }
            VSWRedirect('Add', cmdParam, 'RecordID');
            return;
        }
        else if (cmdName === "saveorder") {
            listCid = document.getElementsByName('cid');
            for (i = 0; i < listCid.length; i++) {
                cmdParam += (cmdParam === '' ? '' : ',') + listCid[i].value;
                var order = document.getElementById('order[' + listCid[i].value + ']');
                cmdParam += (cmdParam === '' ? '' : ',') + order.value;
            }
        }

        if (cmdParam !== '')
            cmdName = '[' + cmdName + '][' + cmdParam + ']';

        document.getElementById('_vsw_action').value = cmdName;
    }

    if (typeof document.vswForm.onsubmit == "function") {
        document.vswForm.onsubmit();
    }

    document.vswForm.submit();
}

function isChecked(isitchecked) {
    if (isitchecked === true) {
        document.vswForm.boxchecked.value++;
    }
    else {
        document.vswForm.boxchecked.value--;
    }
}

function checkAll(n, fldName) {
    if (!fldName) {
        fldName = 'cb';
    }

    var f = document.vswForm;
    var c = f.toggle.checked;
    var n2 = 0;

    for (var i = 0; i < n; i++) {
        var cb = eval('f.' + fldName + '' + i);
        if (cb) {
            cb.checked = c;
            n2++;
        }
    }

    if (c) {
        document.vswForm.boxchecked.value = n2;
    } else {
        document.vswForm.boxchecked.value = 0;
    }
}

function gmobj(o) {
    if (document.getElementById) { m = document.getElementById(o); }
    else if (document.all) { m = document.all[o]; }
    else if (document.layers) { m = document[o]; }
    return m;
}

function getNodeValue(o) {
    try {
        return o.item(0).firstChild.nodeValue;
    }
    catch (err) {
        return '';
    }
}

function VSWCheckDefaultValue(value, name) {
    if (typeof (window.VSWArrDefault) != 'undefined') {
        for (var i = 0; i < window.VSWArrDefault.length; i++) {
            if (i === window.VSWArrDefault.length - 1) break;

            if (window.VSWArrDefault[i] == value && window.VSWArrDefault[i + 1] === name)
                return true;

            i++;
        }
    }

    return false;
}

function VSWRedirect(control, value, name) {
    var sUrl = '';

    if (value && value !== '' && value !== '0')
        sUrl += '/' + (name ? name : 'RecordID') + '/' + value;

    var i;
    var obj;
    var objValue;

    if (typeof (window.VSWArrVar) != 'undefined') {
        for (i = 0; i < window.VSWArrVar.length; i++) {
            if (i === window.VSWArrVar.length - 1) break;
            obj = document.getElementById(window.VSWArrVar[i]);
            if (obj != null) {
                objValue = obj.value;
                if (objValue !== '' && objValue !== '0') {
                    if (!VSWCheckDefaultValue(objValue, window.VSWArrVar[i + 1]))
                        sUrl += '/' + window.VSWArrVar[i + 1] + '/' + objValue;
                }
            }

            i++;
        }
    }

    if (typeof (window.VSWArrQT) != 'undefined') {
        for (i = 0; i < window.VSWArrQT.length; i++) {
            if (i === window.VSWArrQT.length - 1) break;

            if (name && name === window.VSWArrQT[i + 1]) {
                i++;
                continue;
            }

            if ((control ? control : 'Index') === 'Index' && 'PageIndex' === window.VSWArrQT[i + 1]) {
                i++;
                continue;
            }

            if (window.VSWArrQT[i] !== '' && window.VSWArrQT[i] !== '0')
                if (!VSWCheckDefaultValue(window.VSWArrQT[i], window.VSWArrQT[i + 1]))
                    sUrl += '/' + window.VSWArrQT[i + 1] + '/' + window.VSWArrQT[i];

            i++;
        }
    }

    var url;
    if (typeof (window.VSWArrVar_QS) != 'undefined') {
        url = '';
        for (i = 0; i < window.VSWArrVar_QS.length; i++) {
            if (i === window.VSWArrVar_QS.length - 1) break;
            obj = document.getElementById(window.VSWArrVar_QS[i]);
            if (obj != null) {
                objValue = obj.value;
                if (objValue !== '' && objValue !== '0') {
                    if (!VSWCheckDefaultValue(objValue, window.VSWArrVar_QS[i + 1]))
                        url += (url === '' ? '' : '&') + window.VSWArrVar_QS[i + 1] + '=' + objValue;
                }
            }

            i++;
        }
        if (url !== '')
            sUrl = sUrl + '?' + url;
    }

    if (typeof (window.VSWArrQT_QS) != 'undefined') {
        url = '';
        for (i = 0; i < window.VSWArrQT_QS.length; i++) {
            if (i === window.VSWArrQT_QS.length - 1) break;

            if (window.VSWArrQT_QS[i] !== '' && window.VSWArrQT_QS[i] !== '0')
                if (!VSWCheckDefaultValue(window.VSWArrQT_QS[i], window.VSWArrQT_QS[i + 1]))
                    url += (url === '' ? '' : '&') + window.VSWArrQT_QS[i + 1] + '=' + window.VSWArrQT_QS[i];

            i++;
        }
        if (url !== '')
            sUrl = sUrl + '?' + url;
    }

    if (control)
        sUrl = control + '.aspx' + sUrl;
    else
        sUrl = 'Index.aspx' + sUrl;

    window.location.href = '/' + window.CPPath + '/' + window.VSWController + '/' + sUrl;
}

function trim(str, chars) {
    return ltrim(rtrim(str, chars), chars);
}

function ltrim(str, chars) {
    chars = chars || "\\s";
    return str.replace(new RegExp("^[" + chars + "]+", "g"), "");
}

function rtrim(str, chars) {
    chars = chars || "\\s";
    return str.replace(new RegExp("[" + chars + "]+$", "g"), "");
}

function GetIndex(custom, key, index) {
    var i = custom.indexOf(key + '=', index);
    if (i > -1) {
        var k = custom.indexOf('\n', i - 1);
        if (k === -1 || k === i - 1 || k === custom.length - 1) {
            return i;
        }
        else {
            if (k < i) {
                var s = custom.substr(k, i - k);
                s = trim(s, '');

                if (s === '')
                    return i;
                else
                    return GetIndex(custom, key, i + key.length + 1);
            }

            return i;
        }
    }

    return -1;
}

function getvalue(custom, key, value) {
    return getvalue(custom, key, value, 0);
}

function getvalue(custom, key, value, index) {
    var i = GetIndex(custom, key, 0);
    if (i > -1) {
        var j = custom.indexOf('\n', i);
        if (j === -1) j = custom.length;

        var oldvalue = custom.substr(i, j - i);

        custom = custom.replace(oldvalue, key + '=' + value);
    }
    else {
        if (custom === '') custom = key + '=' + value;
        else custom += '\n' + key + '=' + value;
    }

    return custom;
}

function GetCustom(key) {
    var txtCustom = document.getElementById("Custom");
    var txtSetCustom = document.getElementById("set_custom");

    var custom = txtCustom.value;
    txtSetCustom.value = '';

    var i = GetIndex(custom, key, 0);
    if (i > -1) {
        var j = custom.indexOf('\n', i);

        if (j === -1)
            j = custom.length;

        var value = custom.substr(i + key.length + 1, j - i - key.length - 1);

        txtSetCustom.value = value;
    }
}

function SetCustom() {
    var key = '';
    for (var i = 0; i < document.getElementsByName("rSetCustom").length; i++) {
        if (document.getElementsByName("rSetCustom").item(i).checked) {
            key = document.getElementsByName("rSetCustom").item(i).value;
            break;
        }
    }

    var txtCustom = document.getElementById("Custom");
    var txtSetCustom = document.getElementById("set_custom");
    var sCode = '';

    if (txtCustom.value !== '')
        sCode = txtCustom.value;

    sCode = getvalue(sCode, key, txtSetCustom.value);

    txtCustom.value = sCode;
}

//update custom - page
function UpdateCustom(cID, sType) {
    var key = cID.toString().replace("_", ".") + '';
    var value = document.getElementById(cID).value + '';

    var txtCustom = document.getElementById("Custom");
    var sCode = '';

    if (txtCustom.value !== '')
        sCode = txtCustom.value;

    sCode = getvalue(sCode, key, value);

    txtCustom.value = sCode;
}

function vsw_checkAll(form, field, value) {
    for (var i = 0; i < form.elements.length; i++) {
        if (form.elements[i].name === field) {
            form.elements[i].checked = value;
            if (form.elements[i].disabled)
                form.elements[i].checked = false;
        }
    }
}

function ShowNewsForm(cID, sValue) {
    name_control = cID;
    window.open("/" + window.CPPath + "/FormNews/Index.aspx?Value=" + sValue, "", "width=1024, height=800, top=80, left=200,scrollbars=yes");
    return false;
}

function ShowTextForm(cID, sValue) {
    name_control = cID;
    window.open("/" + window.CPPath + "/FormText/Index.aspx?TextID=" + cID, "", "width=1024, height=800, top=80, left=200,scrollbars=yes");
    return false;
}

function ShowFileForm(cID, sValue) {
    name_control = cID;

    var finder = new CKFinder();
    finder.basePath = '../';
    finder.selectActionFunction = refreshPage;
    finder.popup();

    return false;
}


function ShowLandingForm(cID, sValue) {
    name_control = cID;

    var finder = new CKFinder();
    finder.basePath = '../';
    finder.selectActionFunction = refreshLanding;
    finder.popup();

    return false;
}
var name_control = '';
function refreshPage(arg) {
    var obj = document.getElementById(name_control);
    if (name_control.indexOf('File') > -1 || name_control.indexOf('Img') > -1 || name_control.indexOf('Logo') > -1)
        obj.value = '~' + arg;
    else
        obj.value = arg;

    //arg = '~' + arg;
    $('#' + name_control).val(arg);

    var info = $('#' + name_control).parent().parent();

    if (info.length) {
        info.find('img').attr('src', arg);
    }
}

function layout_change(pid, listParam, layout) {
    if (listParam === '') return;
    var listLayout = listParam.split(',')
    for (var i = 0; i < listLayout.length; i++) {
        var ib = listLayout[i].indexOf('[');
        //var ie = listLayout[i].indexOf(']');
        var layoutValue = listLayout[i].substring(0, ib);
        var listControlParam = listLayout[i].substring(ib + 1, listLayout[i].length - 1);

        if (layoutValue === 'Default' || layoutValue === layout)
            control_change(pid, listControlParam);
    }
}
function control_change(pid, listParam) {
    var listControl = listParam.split('|');
    for (var i = 0; i < listControl.length; i++) {
        var control = listControl[i].split('-')[0];
        var visible = listControl[i].split('-')[1];
        //document.getElementById(pid + '_' + control).disabled = (visible == 'false');
        document.getElementById('tr_' + pid + '_' + control).style.display = (visible === 'false' ? 'none' : '');
    }
}

function control_set_value(id, value) {
    var obj = document.getElementById(id);
    if (obj) {
        obj.value = value;
    } else {
        if (value === 'True') value = 1;
        if (value === 'False') value = 0;
        var arr = document.getElementsByName(id);
        if (arr != null) {
            for (var j = 0; j < arr.length; j++) {
                if (arr[j].value === value) {
                    arr[j].checked = true;
                    break;
                }
            }
        }
    }
}

function Close(arg) {
    if (window.opener)
        window.opener.refreshPage(arg);
    else
        window.parent.refreshPage(arg);

    window.close();
}

function Cancel() {
    window.close();
}

Array.prototype.swap = function (a, b) {
    var temp = this[a];
    this[a] = this[b];
    this[b] = temp;
};

this.imagePreview = function () {
    /* CONFIG */

    xOffset = 10;
    yOffset = 30;

    // these 2 variable determine popup's distance from the cursor
    // you might want to adjust to get the right result

    /* END CONFIG */
    $('a.preview').hover(function (e) {
        this.t = this.title;
        this.title = '';
        var c = (this.t != '') ? '<br/>' + this.t : '';
        $('body').append('<p id="preview"><img src="' + $(this).data('src') + '" width="350" alt="' + this.title + '" />' + c + '</p>');
        $('#preview')
            .css('top', (e.pageY - xOffset) + 'px')
            .css('left', (e.pageX + yOffset) + 'px')
            .fadeIn('fast');
    },
        function () {
            this.title = this.t;
            $('#preview').remove();
        });
    $('a.preview').mousemove(function (e) {
        $('#preview')
            .css('top', (e.pageY - xOffset) + 'px')
            .css('left', (e.pageX + yOffset) + 'px');
    });
};

function formatDollar(value) {
    return value.split("").reverse().reduce(function (acc, value, i, orig) {
        return value + (i && !(i % 3) ? "." : "") + acc;
    }, "");
}

function copyToClipboard(e) {
    var $temp = $('<textarea>');
    $('body').append($temp);
    $temp.val($(e).text()).select();
    document.execCommand('copy');
    $temp.remove();

    sw_alert('Thông báo !', 'Đã copy thành công');
}

NProgress.start();
var interval = setInterval(function () { NProgress.inc(); }, 1000);

$(window).load(function () {
    clearInterval(interval);
    NProgress.done();
});

$(window).unload(function () {
    NProgress.start();
});

function CKEditorInstance() {
    if ($('#TopContent').length) {
        var ckEditor = CKEDITOR.instances["TopContent"];
        if (ckEditor) { ckEditor.destroy(true); }
        CKEDITOR.replace('TopContent', {
           
        });
    }
    if ($('#Content').length) {
        var ckEditor = CKEDITOR.instances["Content"];
        if (ckEditor) { ckEditor.destroy(true); }
        CKEDITOR.replace('Content', {
            
        });
    }


    if ($('#Specifications').length) {
        var ckEditor = CKEDITOR.instances["Specifications"];
        if (ckEditor) { ckEditor.destroy(true); }
        CKEDITOR.replace('Specifications', {
           
        });
    }
}

$('a[data-toggle="tab"]').click(function (e) {
    e.preventDefault();
    $(this).tab('show');
});

$('a[data-toggle="tab"]').on("shown.bs.tab", function (e) {
    var id = $(e.target).attr("href");
    localStorage.setItem('selectedTab', id)
});

var selectedTab = localStorage.getItem('selectedTab');
if (selectedTab != null) {
    $('a[data-toggle="tab"][href="' + selectedTab + '"]').tab('show');
}

$(function () {
    $('input[type="checkbox"][name="ArrColor"]').click(function () {
        var color = $('input[type="checkbox"][name="ArrColor"]:checked').map(function () {
            return this.value;
        }).get();
        $('#Color').val(color.join(', '));
    })

    $('input[type="checkbox"][name="ArrSize"]').click(function () {
        var size = $('input[type="checkbox"][name="ArrSize"]:checked').map(function () {
            return this.value;
        }).get();
        $('#Size').val(size.join(', '));
    })

    var color = $('input[type="checkbox"][name="ArrColor"]:checked').map(function () {
        return this.value;
    }).get();
    $('#Color').val(color.join(', '));

    var size = $('input[type="checkbox"][name="ArrSize"]:checked').map(function () {
        return this.value;
    }).get();
    $('#Size').val(size.join(', '));

    $('ul.sortable').sortable({
        update: function () {
            var order = [];
            $('ul.sortable li').each(function (e) {
                order.push($(this).data('id') + '|' + ($(this).index() + 1));
            });
            update_file(order.join(','));
        }
    });

    $('.datepicker').datepicker({
        format: 'dd/mm/yyyy'
        //date: new Date(1960, 1, 1) // Or '02/14/2014'
    });

    $('.uniform input').on('change', function () {
        uniformUpload();
    })

    $('.product input').on('change', function () {
        productUpload();
    })

    $('.message-to input[type="checkbox"]').click(function () {
        var arrWebUser = $('.message-to input[type="checkbox"]:checked').map(function () {
            return this.value;
        }).get();
        $('#ArrWebUser').val(arrWebUser.join(','));
    })

    $('.version-save').on('click', function () {
        var recordID = $(this).data('id');
        if (recordID > 0) {
            UpVersion($('#LangID').val(), recordID, $('#RecordID').val(), $('#VersionID-' + recordID).val(), $('#File-' + recordID).val(), $('#Size-' + recordID).val());
        }
        else {
            UpVersion($('#LangID').val(), '0', $('#RecordID').val(), $('#VersionID-0').val(), $('#File-0').val(), $('#Size-0').val());
        }
    })

    $('.version-delete').on('click', function () {
        location.href = '/' + window.CPPath + '/ModProduct/DeleteVersion.aspx/DetailID/' + $(this).data('id');
    })

    $('.price').on('keyup', function (e) {
        $(this).parent().find('span').html(formatDollar($(this).val()));
    });

    $('.price').val()

    CKFinder.setupCKEditor(null, { basePath: '/' + window.CPPath + '/Content/ckfinder/', rememberLastFolder: true });
    CKEditorInstance();

    $('.box-content').height($('.box-logs').height());

    $html = $('.nav-desktop').html();

    $('.nav-mobie').html($html);

    var overlay = $('.sidebar-overlay');
    $('.sidebar-toggle-btn').on('click', function () {
        var sidebar = $('#sidebar');
        sidebar.toggleClass('open');
        overlay.addClass('active');
    });
    overlay.on('click', function () {
        $(this).removeClass('active');
        $('#sidebar').removeClass('open');
    });

    $('.nav-mobie li .a-open-down').on('click', function () {
        $(this).removeAttr('href');
        var element = $(this).parent('li');
        if (element.hasClass('open')) {
            element.removeClass('open');
            element.find('li').removeClass('open');
            element.find('ul').slideUp();
        } else {
            element.addClass('open');
            element.children('ul').slideDown();
            element.siblings('li').children('ul').slideUp();
            element.siblings('li').removeClass('open');
            element.siblings('li').find('li').removeClass('open');
            element.siblings('li').find('ul').slideUp();
        }
    });

    $('[data-toggle="tooltip"]').tooltip();

    $(".back-to-top a").click(function (n) {
        n.preventDefault();
        $("html, body").animate({
            scrollTop: 0
        }, 500)
    });
    $(window).scroll(function () {
        $(document).scrollTop() > 1e3 ? $(".back-to-top").addClass("display") : $(".back-to-top").removeClass("display")
    });

    imagePreview();
    
    $('textarea.description').keyup(function () {
        var max = 400;
        var len = $(this).val().length;
        if (len >= max) {
            $(this).parent().find('.help-block').text('Ký tự còn lại: 0');
            $(this).val($(this).val().substring(0, 399));
        } else {
            var char = max - len;
            $(this).parent().find('.help-block').text('Ký tự còn lại: ' + char);
        }
    });
    $('input.title').keyup(function () {
        var max = 200;
        var len = $(this).val().length;
        if (len >= max) {
            $(this).parent().find('.help-block').text('Ký tự còn lại: 0');
            $(this).val($(this).val().substring(0, 199));
        } else {
            var char = max - len;
            $(this).parent().find('.help-block').text('Ký tự còn lại: ' + char);
        }
    });

    $('textarea.description').each(function () {
        var max = 1000;
        var len = $(this).val().length;
        if (len >= max) {
            $(this).parent().find('.help-block').text('Ký tự còn lại: 0');
            $(this).val($(this).val().substring(0, 1000));
        } else {
            var char = max - len;
            $(this).parent().find('.help-block').text('Ký tự còn lại: ' + char);
        }
    });

    $('input.title').each(function () {
        var max = 200;
        var len = $(this).val().length;
        if (len >= max) {
            $(this).parent().find('.help-block').text('Ký tự còn lại: 0');
            $(this).val($(this).val().substring(0, 199));
        } else {
            var char = max - len;
            $(this).parent().find('.help-block').text('Ký tự còn lại: ' + char);
        }
    });

    $('.select2').select2({
        theme: 'bootstrap'
    }).on('select2:opening', function () {
        $(this).data('select2').$dropdown.find(':input.select2-search__field').attr('placeholder', 'Nhập từ khóa để tìm kiếm')
    })

    $('input.price').on('keydown', function (e) {
        // allow function keys and decimal separators
        if (
            // backspace, delete, tab, escape, enter, comma and .
            $.inArray(e.keyCode, [46, 8, 9, 27, 13, 110, 188, 190]) !== -1 ||
            // Ctrl/cmd+A, Ctrl/cmd+C, Ctrl/cmd+X
            ($.inArray(e.keyCode, [65, 67, 88]) !== -1 && (e.ctrlKey === true || e.metaKey === true)) ||
            // home, end, left, right
            (e.keyCode >= 35 && e.keyCode <= 39)) {

            /*
            // optional: replace commas with dots in real-time (for en-US locals)
            if (e.keyCode === 188) {
                e.preventDefault();
                $(this).val($(this).val() + ".");
            }
            */
    
            // optional: replace decimal points (num pad) and dots with commas in real-time (for EU locals)
            if (e.keyCode === 110 || e.keyCode === 190) {
                e.preventDefault();
                $(this).val($(this).val() + ",");
            }
            
            return;
        }
        // block any non-number
        if ((e.shiftKey || (e.keyCode < 48 || e.keyCode > 57)) && (e.keyCode < 96 || e.keyCode > 105)) {
            e.preventDefault();
        }
    });
});
