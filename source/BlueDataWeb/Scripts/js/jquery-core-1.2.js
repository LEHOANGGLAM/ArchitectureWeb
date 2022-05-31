﻿(function (f) { function p(a, b, c) { var h = c.relative ? a.position().top : a.offset().top, d = c.relative ? a.position().left : a.offset().left, i = c.position[0]; h -= b.outerHeight() - c.offset[0]; d += a.outerWidth() + c.offset[1]; if (/iPad/i.test(navigator.userAgent)) h -= f(window).scrollTop(); var j = b.outerHeight() + a.outerHeight(); if (i == "center") h += j / 2; if (i == "bottom") h += j; i = c.position[1]; a = b.outerWidth() + a.outerWidth(); if (i == "center") d -= a / 2; if (i == "left") d -= a; return { top: h, left: d } } function u(a, b) { var c = this, h = a.add(c), d, i = 0, j = 0, m = a.attr("title"), q = a.attr("data-tooltip"), r = o[b.effect], l, s = a.is(":input"), v = s && a.is(":checkbox, :radio, select, :button, :submit"), t = a.attr("type"), k = b.events[t] || b.events[s ? v ? "widget" : "input" : "def"]; if (!r) throw 'Nonexistent effect "' + b.effect + '"'; k = k.split(/,\s*/); if (k.length != 2) throw "Tooltip: bad events configuration for " + t; a.bind(k[0], function (e) { clearTimeout(i); if (b.predelay) j = setTimeout(function () { c.show(e) }, b.predelay); else c.show(e) }).bind(k[1], function (e) { clearTimeout(j); if (b.delay) i = setTimeout(function () { c.hide(e) }, b.delay); else c.hide(e) }); if (m && b.cancelDefault) { a.removeAttr("title"); a.data("title", m) } f.extend(c, { show: function (e) { if (!d) { if (q) d = f(q); else if (b.tip) d = f(b.tip).eq(0); else if (m) d = f(b.layout).addClass(b.tipClass).appendTo(document.body).hide().append(m); else { d = a.next(); d.length || (d = a.parent().next()) } if (!d.length) throw "Cannot find tooltip for " + a; } if (c.isShown()) return c; d.stop(true, true); var g = p(a, d, b); b.tip && d.html(a.data("title")); e = e || f.Event(); e.type = "onBeforeShow"; h.trigger(e, [g]); if (e.isDefaultPrevented()) return c; g = p(a, d, b); d.css({ position: "absolute", top: g.top, left: g.left }); l = true; r[0].call(c, function () { e.type = "onShow"; l = "full"; h.trigger(e) }); g = b.events.tooltip.split(/,\s*/); if (!d.data("__set")) { d.bind(g[0], function () { clearTimeout(i); clearTimeout(j) }); g[1] && !a.is("input:not(:checkbox, :radio), textarea") && d.bind(g[1], function (n) { n.relatedTarget != a[0] && a.trigger(k[1].split(" ")[0]) }); d.data("__set", true) } return c }, hide: function (e) { if (!d || !c.isShown()) return c; e = e || f.Event(); e.type = "onBeforeHide"; h.trigger(e); if (!e.isDefaultPrevented()) { l = false; o[b.effect][1].call(c, function () { e.type = "onHide"; h.trigger(e) }); return c } }, isShown: function (e) { return e ? l == "full" : l }, getConf: function () { return b }, getTip: function () { return d }, getTrigger: function () { return a } }); f.each("onHide,onBeforeShow,onShow,onBeforeHide".split(","), function (e, g) { f.isFunction(b[g]) && f(c).bind(g, b[g]); c[g] = function (n) { n && f(c).bind(g, n); return c } }) } f.tools = f.tools || { version: "1.2.5" }; f.tools.tooltip = { conf: { effect: "toggle", fadeOutSpeed: "fast", predelay: 0, delay: 30, opacity: 1, tip: 0, position: ["top", "center"], offset: [0, 0], relative: false, cancelDefault: true, events: { def: "mouseenter,mouseleave", input: "focus,blur", widget: "focus mouseenter,blur mouseleave", tooltip: "mouseenter,mouseleave" }, layout: "<div/>", tipClass: "tooltip" }, addEffect: function (a, b, c) { o[a] = [b, c] } }; var o = { toggle: [function (a) { var b = this.getConf(), c = this.getTip(); b = b.opacity; b < 1 && c.css({ opacity: b }); c.show(); a.call() }, function (a) { this.getTip().hide(); a.call() }], fade: [function (a) { var b = this.getConf(); this.getTip().fadeTo(b.fadeInSpeed, b.opacity, a) }, function (a) { this.getTip().fadeOut(this.getConf().fadeOutSpeed, a) }] }; f.fn.tooltip = function (a) { var b = this.data("tooltip"); if (b) return b; a = f.extend(true, {}, f.tools.tooltip.conf, a); if (typeof a.position == "string") a.position = a.position.split(/,?\s/); this.each(function () { b = new u(f(this), a); f(this).data("tooltip", b) }); return a.api ? b : this } })(jQuery); (function ($) {
    $.fn.appear = function (fn, options) {
        var settings = $.extend({ data: undefined, one: true }, options); return this.each(function () {
            var t = $(this); t.appeared = false; if (!fn) { t.trigger('appear', settings.data); return; }
            var w = settings.container ? $(settings.container) : $(window); if (settings.container) { w.data('customContainer', true); }
            var check = function () {
                if (!t.is(':visible')) { t.appeared = false; return; }
                var a = w.scrollLeft(); var b = w.scrollTop(); var wh = w.height(); var ww = w.width(); var o = t.offset(); if (w.data('customContainer')) { var cOffset = w.offset(); var x = o.left - cOffset.left + a; var y = o.top - cOffset.top + b; } else { var x = o.left; var y = o.top; }
                if (y + t.height() >= b && y <= b + wh && x + t.width() >= a && x <= a + ww) { if (!t.appeared) t.trigger('appear', settings.data); } else { t.appeared = false; }
            }; var modifiedFn = function () {
                t.appeared = true; if (settings.one) { w.unbind('scroll', check); var i = $.inArray(check, $.fn.appear.checks); if (i >= 0) $.fn.appear.checks.splice(i, 1); }
                fn.apply(this, arguments);
            }; if (settings.one) t.one('appear', settings.data, modifiedFn); else t.bind('appear', settings.data, modifiedFn); w.scroll(check); $.fn.appear.checks.push(check); (check)();
        });
    }; $.extend($.fn.appear, { checks: [], timeout: null, checkAll: function () { var length = $.fn.appear.checks.length; if (length > 0) while (length--) ($.fn.appear.checks[length])(); }, run: function () { if ($.fn.appear.timeout) clearTimeout($.fn.appear.timeout); $.fn.appear.timeout = setTimeout($.fn.appear.checkAll, 20); } }); $.each(['append', 'prepend', 'after', 'before', 'attr', 'removeAttr', 'addClass', 'removeClass', 'toggleClass', 'remove', 'css', 'show', 'hide'], function (i, n) { var old = $.fn[n]; if (old) { $.fn[n] = function () { var r = old.apply(this, arguments); $.fn.appear.run(); return r; } } });
})(jQuery); $.data.ceEditorMethods = {
    entity_encoding: "raw", runEditor: function (elm) {
        if (typeof ($.fn.tinymce) == 'undefined') { $.ceEditor('state', 'loading'); return $.getScript(current_path + '/lib/js/tinymce/jquery.tinymce.js', function () { $.ceEditor('state', 'loaded'); elm.ceEditor('run'); }); }
        var available_langs = { ar: true, az: true, be: true, bg: true, bn: true, br: true, bs: true, ca: true, ch: true, cs: true, cy: true, da: true, de: true, dv: true, el: true, en: true, es: true, et: true, eu: true, fa: true, fi: true, fr: true, gl: true, gu: true, he: true, hi: true, hr: true, hu: true, hy: true, ia: true, id: true, ii: true, is: true, it: true, ja: true, ka: true, kl: true, ko: true, lb: true, lt: true, lv: true, mk: true, ml: true, mn: true, ms: true, nb: true, nl: true, nn: true, no: true, pl: true, ps: true, pt: true, ro: true, ru: true, sc: true, se: true, si: true, sk: true, sl: true, sq: true, sr: true, sv: true, ta: true, te: true, th: true, tr: true, tt: true, tw: true, uk: true, ur: true, vi: true, zh: true, zu: true }; var lang = (typeof (available_langs[cart_language.toLowerCase()]) != 'undefined') ? cart_language.toLowerCase() : 'en'; elm.tinymce({ script_url: current_path + '/lib/js/tinymce/tiny_mce.js', plugins: "pagebreak,style,layer,table,save,advhr,advimage,advlink,emotions,iespell,inlinepopups,insertdatetime,preview,media,searchreplace,print,contextmenu,paste,directionality,fullscreen,noneditable,visualchars,nonbreaking,xhtmlxtras,template,wordcount,advlist,autosave", theme_advanced_buttons1: "save,newdocument,|,bold,italic,underline,strikethrough,|,justifyleft,justifycenter,justifyright,justifyfull,|,styleselect,formatselect,fontselect,fontsizeselect", theme_advanced_buttons2: "removeformat,cut,copy,paste,pastetext,pasteword,|,search,replace,|,bullist,numlist,|,outdent,indent,blockquote,|,undo,redo,|,link,unlink,anchor,image,cleanup,help,code,|,insertdate,inserttime,preview,|,forecolor,backcolor", theme_advanced_buttons3: '', theme_advanced_toolbar_location: 'top', theme_advanced_toolbar_align: 'left', theme_advanced_statusbar_location: 'bottom', theme_advanced_resizing: true, theme_advanced_resize_horizontal: false, theme: 'advanced', language: lang, strict_loading_mode: true, convert_urls: false, remove_script_host: false, body_class: 'wysiwyg-content', content_css: current_path + '/skins/' + skin_name_customer + '/customer/styles.css,' + current_path + '/skins/' + skin_name + '/admin/wysiwyg_reset.css', extended_valid_elements: 'div[*]', file_browser_callback: function (field_name, url, type, win) { tinyMCE.activeEditor.windowManager.open({ file: current_path + '/lib/js/elfinder/elfinder.tinymce.html', width: 600, height: 450, resizable: 'yes', inline: 'yes', close_previous: 'no', popup_css: false }, { 'window': win, 'input': field_name, 'current_location': current_location + '/', 'connector_url': fn_url('elf_connector.images?ajax_custom=1') }); } });
    }, destroyEditor: function (elm) { tinyMCE.execCommand('mceRemoveControl', false, elm.attr('id')); }, recoverEditor: function (elm) { tinyMCE.execCommand('mceAddControl', false, elm.attr('id')); }
}
jQuery.extend({
    ajaxRequest: function (url, params) {
        params = params || {}; params.method = params.method || 'get'; params.callback = params.callback || {}; params.pre_processing = params.pre_processing || {}; params.data = params.data || {}; params.message = params.message || lang.loading; params.caching = params.caching || false; params.hidden = params.hidden || false; params.repeat_on_error = params.repeat_on_error || false; params.low_priority = params.low_priority || false; params.force_exec = params.force_exec || false; params.obj = params.obj || null; params.append = params.append || null; params.result_ids = params.result_ids || null; var QUERIES_LIMIT = 1; if (typeof (params.data.security_hash) == 'undefined' && typeof (security_hash) != 'undefined' && params.method.toLowerCase() == 'post') { params.data.security_hash = security_hash; }
        if (jQuery.active_queries >= QUERIES_LIMIT) {
            if (params.low_priority == true) { jQuery.queries_stack.push(function () { jQuery.ajaxRequest(url, params); }); } else { jQuery.queries_stack.unshift(function () { jQuery.ajaxRequest(url, params); }); }
            return true;
        }
        if (params.hidden == false) { jQuery.toggleStatusBox('show', params.message); }
        var hash = ''; if (params.caching == true) { hash = jQuery.crc32(url + params.result_ids + jQuery.param(params.data)); }
        if (!hash || !jQuery.ajax_cache[hash]) {
            url = fn_query_remove(url, 'result_ids'); if (params.result_ids) { params.data.result_ids = params.result_ids; }
            if (params.skip_result_ids_check) { params.data.skip_result_ids_check = params.skip_result_ids_check; }
            var saved_data = []; var result_ids = ''; for (i in params.data) { if (i == 'result_ids') { result_ids = params.data[i].split(','); break; } else if (params.data[i] && typeof (params.data[i]['name']) != 'undefined' && params.data[i]['name'] == 'result_ids') { result_ids = params.data[i]['value'].split(','); break; } }
            if (result_ids.length > 0) {
                for (j in result_ids) { var container = $('#' + result_ids[j]); if (container.hasClass('cm-save-fields')) { saved_data[result_ids[j]] = $(':input:visible', container).serializeArray(); } }
                params.saved_data = saved_data;
            }
            if (url) {
                if (params.obj && params.obj.hasClass('cm-comet')) { jQuery.ajaxSubmit(params.obj, null, { url: url, result_ids: result_ids }); } else {
                    jQuery.active_queries++; return jQuery.ajax({
                        type: params.method, url: url, dataType: 'json', cache: true, data: params.data, success: function (data, textStatus) {
                            if (hash) { jQuery.ajax_cache[hash] = data; }
                            jQuery.ajaxResponse(data, params);
                        }, error: function (XMLHttpRequest, textStatus, errorThrown) {
                            if (params.repeat_on_error) { params.repeat_on_error = false; jQuery.ajaxRequest(url, params); return false; }
                            jQuery.toggleStatusBox('hide'); if (params.hidden == false) { var err_msg = lang.error_ajax.str_replace('[error]', (textStatus ? textStatus : lang.error)); jQuery.showNotifications({ 'data': { 'type': 'E', 'title': lang.error, 'message': err_msg, 'save_state': false } }); }
                        }, complete: function (XMLHttpRequest, textStatus) { jQuery.active_queries--; if (jQuery.queries_stack.length) { var f = jQuery.queries_stack.shift(); f(); } }
                    });
                }
            }
        } else if (hash && jQuery.ajax_cache[hash]) { jQuery.ajaxResponse(jQuery.ajax_cache[hash], params); }
    }, ajaxSubmit: function (obj, elm, params) {
        var callback = 'fn_form_post_' + obj.attr('name'); var f_callback = window[callback] || null; var REQUEST_XML = 1; var REQUEST_IFRAME = 2; var REQUEST_COMET = 3; var is_comet = obj.hasClass('cm-comet') || (elm && elm.hasClass('cm-comet')); if (obj.is('form')) {
            f_callback = function (data, params) {
                if (obj.hasClass('cm-disable-empty')) { $('input:text[value=""]', obj).removeAttr('disabled'); }
                if (obj.hasClass('cm-disable-empty-files')) { $('input:file[value=""]', obj).removeAttr('disabled'); }
                if (window[callback]) { window[callback](data, params); }
            }
        }
        jQuery.processNotifications(); if (is_comet || obj.attr('enctype') == 'multipart/form-data') {
            if (!$('iframe[name=upload_iframe]').length) {
                $('<iframe name="upload_iframe" src="javascript: false;" class="hidden"></iframe>').appendTo('body'); $('#comet_container').ceProgress('init'); $('iframe[name=upload_iframe]').load(function () {
                    $('#comet_container').ceProgress('finish'); var response = {}; if ($(this).contents().text() != null) { eval('var response = ' + $(this).contents().find('textarea').val()); }
                    response = response || {}; jQuery.ajaxResponse(response, { callback: f_callback }); if ($(this).contents().find('textarea').val() != undefined) { jQuery.comet_active = false; }
                });
            }
            if (obj.is('form')) { obj.append('<input type="hidden" name="is_ajax" value="' + (is_comet ? REQUEST_COMET : REQUEST_IFRAME) + '" />'); obj.attr('target', 'upload_iframe'); jQuery.comet_active = true; jQuery.ajaxRequest('', null); }
            if (is_comet && !obj.is('form')) { $('iframe[name=upload_iframe]').attr('src', params.url + '&result_ids=' + params.result_ids + '&is_ajax=' + REQUEST_COMET); }
            return true;
        } else { var hash = $(':input', obj).serializeArray(); hash.push({ name: elm.attr('name'), value: elm.val() }); var aj = jQuery.ajaxRequest(obj.attr('action'), { method: obj.attr('method'), data: hash, callback: f_callback, force_exec: obj.hasClass('cm-ajax-force') ? true : false }); return false; }
    }, ajaxResponse: function (response, params) {
        params = params || {}; params.force_exec = params.force_exec || false; params.callback = params.callback || {}; params.pre_processing = params.pre_processing || {}; var regex_all = new RegExp('<script[^>]*>([\u0001-\uFFFF]*?)</script>', 'img'); var matches = []; var match = ''; var elm; var data = response.data || {}; if (params.pre_processing) { if (typeof (params.pre_processing) == 'function') { params.pre_processing(data, params); } else if (params.pre_processing[1]) { params.pre_processing[0][params.pre_processing[1]](data, response.text, params); } }
        if (data.force_redirection) { jQuery.toggleStatusBox('hide'); jQuery.redirect(data.force_redirection); return true; }
        if (data.html) {
            for (var k in data.html) {
                elm = $('#' + k); if (elm.length != 1) { continue; }
                if (data.html[k].indexOf('<form') != -1 && elm.parents('form').length) { $('body').append(elm); }
                matches = data.html[k].match(regex_all); if (params.append) { elm.append(matches ? data.html[k].replace(regex_all, '') : data.html[k]); } else { elm.html(matches ? data.html[k].replace(regex_all, '') : data.html[k]); }
                if (typeof (params.saved_data) != 'undefined' && typeof (params.saved_data[k]) != 'undefined') {
                    var elements = []; for (var i in params.saved_data[k]) { elements[params.saved_data[k][i]['name']] = params.saved_data[k][i]['value']; }
                    $('input:visible, select:visible', elm).each(function (id, local_elm) {
                        jelm = $(local_elm); if (typeof (elements[jelm.attr('name')]) != 'undefined' && !jelm.parents().hasClass('cm-skip-save-fields')) {
                            if (jelm.attr('type') == 'radio') { if (jelm.attr('value') == elements[jelm.attr('name')]) { jelm.attr('checked', 'checked'); } } else { jelm.val(elements[jelm.attr('name')]); }
                            jelm.trigger('change');
                        }
                    });
                }
                if (jQuery.trim(elm.html())) { elm.parents('.hidden.cm-hidden-wrapper').removeClass('hidden'); } else { elm.parents('.cm-hidden-wrapper').addClass('hidden'); }
                if (matches) {
                    var s_sources = []; jQuery.loaded_scripts = jQuery.loaded_scripts || []; for (var i = 0; i < matches.length; i++) {
                        var m = $(matches[i]); if (m.attr('src')) {
                            var _src = jQuery.getBaseName(m.attr('src')); var script_idx = jQuery.inArray(_src, jQuery.loaded_scripts); if (m.hasClass('cm-ajax-force') && script_idx != -1) { jQuery.loaded_scripts[script_idx] = null; script_idx = -1; }
                            if (script_idx == -1) { s_sources.push(m.attr('src')); }
                        } else {
                            var _hash = jQuery.crc32(m.html()); var _execute = false; if (!this.eval_cache[_hash] || params.force_exec || m.hasClass('cm-ajax-force')) { this.eval_cache[_hash] = true; if (!s_sources.length) { jQuery.globalEval(m.html()); } else { _execute = true; } }
                            if (s_sources.length) {
                                var list = []; for (var _i = 0; _i < s_sources.length; _i++) { list.push($.getScript(s_sources[_i])); }
                                if (_execute == true) { (function (s) { $.when.apply(null, list).then(function () { jQuery.globalEval(s); }); })(m.html()); }
                                s_sources = [];
                            }
                        }
                    }
                    if (s_sources.length) { for (var _i = 0; _i < s_sources.length; _i++) { $.getScript(s_sources[_i]); } }
                }
                $(".cm-j-tabs", elm).each(function () { $(this).idTabs(); }); if (data.html[k].indexOf('<form') != -1 || data.html[k].indexOf('<!--processForm') != -1) { jQuery.processForms(elm); }
                if (elm.parents('form').length) { elm.parents('form:first').highlightFields(); }
                $('.cm-hide-inputs').disableFields(); if (elm.data('callback')) { elm.data('callback')(); elm.removeData('callback'); }
                if (data.html[k].indexOf('cm-ajax-content-more') != -1) { $('.cm-ajax-content-more', elm).each(function () { var self = $(this); self.appear(function () { jQuery.loadAjaxContent(self); }, { one: false, container: '#scroller_' + self.attr('rev') }); }); }
                if (data.html[k].indexOf('cm-tooltip') != -1) { $('.cm-tooltip', elm).each(function () { var self = $(this); if (!self.data('tooltip')) { self = jQuery.initTooltip(self, {}); } }); }
            }
        }
        if (data.notifications) { jQuery.showNotifications(data.notifications); }
        if (params.callback) { if (typeof (params.callback) == 'function') { params.callback(data, params); } else if (params.callback[1]) { params.callback[0][params.callback[1]](data, response.text, params); } }
        $.ceFloatingBar(); jQuery.toggleStatusBox('hide');
    }, getBaseName: function (path)
    { return path.split('/').pop(); }, ajax_cache: {}, queries_stack: [], active_queries: 0, comet_active: false, eval_cache: {}
}); function fn_query_remove(query, vars) {
    if (typeof (vars) == 'undefined') { return query; }
    if (typeof vars == 'string') { vars = [vars]; }
    var start = query; if (query.indexOf('?') >= 0) {
        start = query.substr(0, query.indexOf('?') + 1); var search = query.substr(query.indexOf('?') + 1); var srch_array = search.split("&"); var temp_array = new Array(); var concat = true; var amp = ''; for (var i = 0; i < srch_array.length; i++) {
            temp_array = srch_array[i].split("="); concat = true; for (var j = 0; j < vars.length; j++) { if (vars[j] == temp_array[0] || temp_array[0].indexOf(vars[j] + '[') != -1) { concat = false; break; } }
            if (concat == true) { start += amp + temp_array[0] + '=' + temp_array[1]; }
            amp = '&';
        }
    }
    return start;
}; (function ($) {
    var ajax = $.ajax; $.ajax = function (origSettings) {
        if (!$.loaded_scripts) { $.loaded_scripts = []; $('script').each(function () { var _src = $(this).attr('src'); if (_src) { $.loaded_scripts.push($.getBaseName(_src)); } }); }
        if (origSettings.dataType && origSettings.dataType == 'script') {
            var _src = $.getBaseName(origSettings.url); if ($.inArray(_src, $.loaded_scripts) != -1) { return false; }
            $.loaded_scripts.push($.getBaseName(origSettings.url));
        }
        return ajax(origSettings);
    }; if (typeof (ajax_callback_data) != 'undefined' && ajax_callback_data) { jQuery.globalEval(ajax_callback_data); ajax_callback_data = false; }; $.getScript = function (url, callback) { return $.ajax({ type: "GET", url: url, success: callback, dataType: "script", cache: true }); };
})(jQuery); var skin_version = 2; var index_script = 'index.php'; var current_path = ''; var http_location = 'http://www.hotdeal.vn'; var domain_location = 'www.hotdeal.vn'; var changes_warning = 'Y'; var lang = { select_district: 'Quận/Huyện', select_state: 'Chọn Tỉnh/Thành phố', select_ward: 'Chọn Phường/Xã', cannot_buy: 'You cannot buy the product with these option variants ', no_products_selected: 'No products selected', error_no_items_selected: 'No items selected! At least one check box must be selected to perform this action.', delete_confirmation: 'Are you sure you want to delete the selected items?', text_out_of_stock: 'Out-of-stock', in_stock: 'Tồn kho', items: 'item(s)', text_required_group_product: 'Please select a product for the required group [group_name]', notice: 'Thông báo', warning: 'Cảnh báo', loading: 'Đang tải...', none: 'None', text_are_you_sure_to_proceed: 'Bạn có chắc là bạn muốn xử lý?', text_invalid_url: 'You have entered an invalid URL', text_cart_changed: 'Items in the shopping cart have been changed. Please click on \"OK\" to save changes, or on \"Cancel\" to leave the items unchanged.', error_validator_email: 'Địa chỉ <b>[field]<\/b> không chính xác.\r\n', error_validator_confirm_email: 'The email addresses in the <b>[field]<\/b> field and confirmation fields do not match.', error_validator_phone: '<b>Số điện thoại<\/b> không chính xác. Định dạng đúng của số điện thoại là (555) 555-55-55 hoặc 55 55 555 5555.', error_validator_integer: 'The value of the <b>[field]<\/b> field is invalid. It should be integer.', error_validator_multiple: 'The <b>[field]<\/b> field does not contain the selected options.', error_validator_password: '<b>[field2]<\/b> và <b>[field1]<\/b> không khớp.', error_validator_required: 'Vui lòng nhập \" <b>[field]<\/b> \" .', error_validator_zipcode: 'The ZIP / Postal code in the <b>[field]<\/b> field is incorrect. The correct format is [extra].', error_validator_message: 'The value of the <b>[field]<\/b> field is invalid.', text_page_loading: 'Loading... Your request is being processed, please wait.', view_cart: 'View cart', checkout: 'Thanh toán', product_added_to_cart: 'Bạn đã thêm deal vào giỏ hàng', products_added_to_cart: 'Products were added to your cart', product_added_to_wl: 'Product was added to your Wish list', product_added_to_cl: 'Product was added to your Compare list', close: 'Close', error: 'Lỗi', error_ajax: 'Oops, something went wrong ([error]). Please try again.', text_changes_not_saved: 'Your changes have not been saved.', text_data_changed: 'Your changes have not been saved.\r\n\r\nPress OK to continue, or Cancel to stay on the current page.' }
var currencies = { 'primary': { 'decimals_separator': ',', 'thousands_separator': '.', 'decimals': '0', 'coefficient': '1' }, 'secondary': { 'decimals_separator': ',', 'thousands_separator': '.', 'decimals': '0', 'coefficient': '1' } }; var default_editor = 'tinymce'; var default_previewer = 'prettyphoto'; var cart_language = 'VN'; var images_dir = '/skins/hotdealv2/customer/images'; var skin_name = 'hotdealv2'; var notice_displaying_time = 5; var cart_prices_w_taxes = false; var translate_mode = false; var regexp = new Array(); $(document).ready(function () { jQuery.runCart('C'); }); var points = {}; function fn_reward_points_check_exceptions(data) {
    id = data.id; if (exclude_from_calculate[id] || !points[id]) { return false; }
    var delta = 0; var modifiers = []; var qty = document.getElementById('amount_' + id) ? parseInt(document.getElementById('amount_' + id).value) : 1; for (i in pr_o[id]) {
        if (!document.getElementById(pr_o[id][i]['id']) || !pr_o[id][i]['pm']) { continue; }
        modifiers[i] = pr_o[id][i]['pm'][pr_o[id][i]['selected_value']]; if (typeof (modifiers[i]) == 'undefined') { continue; }
        if (modifiers[i].substring(0, 1) == 'A') { delta += parseFloat(jQuery.formatPrice(modifiers[i].substring(1, modifiers[i].length - 1), decplaces)); } else if (modifiers[i].substring(0, 1) == 'P') { delta += parseFloat(jQuery.formatPrice(points[id]['reward'] * parseFloat(modifiers[i].substring(1, modifiers[i].length - 1)) / 100, decplaces)); }
    }
    if (document.getElementById('reward_points_' + id) && points[id]['reward']) {
        if (points_with_discounts == 'Y' && pr_d[id] && (pr_d[id]['A'] || pr_d[id]['P'])) { var amount_for_points = (parseFloat(update_ids[id]['original_price']['P'])) ? (update_ids[id]['original_price']['P'] - update_ids[id]['discount_value']['P']) : 0; } else { var amount_for_points = parseFloat(update_ids[id]['original_price']['P']); }
        var pamount = (points[id]['amount_type'] && points[id]['amount_type'] == 'P') ? amount_for_points * points[id]['pure_amount'] / 100 : points[id]['pure_amount'] * amount_for_points / update_ids[id]['original_price']['P']; var reward = Math.round(parseFloat(pamount) + parseFloat(delta)); reward = Math.round(parseInt(qty) * reward); var span_reward_points = document.getElementById('reward_points_' + id); span_reward_points.innerHTML = reward; var div_reward_points = $(span_reward_points).parent(); if (reward) { $(div_reward_points).show(); } else { $(div_reward_points).hide(); }
    }
    if (document.getElementById('price_in_points_' + id) && points[id]['per']) { var subtotal = (price_in_points_with_discounts == 'Y' && pr_d[id] && (pr_d[id]['A'] || pr_d[id]['P'])) ? update_ids[id]['product_subtotal']['P'] : qty * update_ids[id]['original_price']['P']; document.getElementById('price_in_points_' + id).innerHTML = Math.round(points[id]['per'] * subtotal); }
    return true;
}
var price_in_points_with_discounts = 'Y'; var points_with_discounts = 'Y'; fn_register_hooks('reward_points', ['check_exceptions']); jQuery.fn.extend({ selectbox: function (options) { return this.each(function () { new jQuery.SelectBox(this, options); }); } }); if (!window.console) { var console = { log: function (msg) { } } }
jQuery.SelectBox = function (selectobj, options) {
    var opt = options || {}; opt.inputClass = opt.inputClass || "selectbox"; opt.containerClass = opt.containerClass || "selectbox-wrapper"; opt.hoverClass = opt.hoverClass || "current"; opt.currentClass = opt.selectedClass || "selected"
    opt.debug = opt.debug || false; var elm_id = selectobj.id; var active = -1; var inFocus = false; var hasfocus = 0; var $select = $(selectobj); var $container = setupContainer(opt); var $input = setupInput(opt); $select.hide().before($input).before($container); init(); $input.click(function () { if (!inFocus) { $container.toggle(); } }).focus(function () { if ($container.not(':visible')) { inFocus = true; $container.show(); } }).keydown(function (event) { switch (event.keyCode) { case 38: event.preventDefault(); moveSelect(-1); break; case 40: event.preventDefault(); moveSelect(1); break; case 13: event.preventDefault(); $('li.' + opt.hoverClass).trigger('click'); break; case 27: hideMe(); break; } }).blur(function () { if ($container.is(':visible') && hasfocus > 0) { if (opt.debug) console.log('container visible and has focus') } else { hideMe(); } }); function hideMe() { hasfocus = 0; $container.hide(); }
    function init() { $container.append(getSelectOptions($input.attr('id'))).hide(); var width = $input.css('width'); $container.width(width); }
    function setupContainer(options) { var container = document.createElement("div"); $container = $(container); $container.attr('id', elm_id + '_container'); $container.addClass(options.containerClass); return $container; }
    function setupInput(options) { var input = document.createElement("input"); var $input = $(input); $input.attr("id", elm_id + "_input"); $input.attr("type", "text"); $input.addClass(options.inputClass); $input.attr("autocomplete", "off"); $input.attr("readonly", "readonly"); $input.attr("tabIndex", $select.attr("tabindex")); return $input; }
    function moveSelect(step) {
        var lis = $("li", $container); if (!lis) return; active += step; if (active < 0) { active = 0; } else if (active >= lis.size()) { active = lis.size() - 1; }
        lis.removeClass(opt.hoverClass); $(lis[active]).addClass(opt.hoverClass);
    }
    function setCurrent() { var li = $("li." + opt.currentClass, $container).get(0); var ar = ('' + li.id).split('_'); var el = ar[ar.length - 1]; $select.val(el); $select.trigger('change'); $input.val($(li).html()); return true; }
    function getCurrentSelected() { return $select.val(); }
    function getCurrentValue() { return $input.val(); }
    function getSelectOptions(parentid) {
        var select_options = new Array(); var ul = document.createElement('ul'); $select.children('option').each(function () {
            var li = document.createElement('li'); li.setAttribute('id', parentid + '_' + $(this).val()); li.innerHTML = $(this).html(); if ($(this).is(':selected')) { $input.val($(this).html()); $(li).addClass(opt.currentClass); }
            ul.appendChild(li); $(li).mouseover(function (event) { hasfocus = 1; if (opt.debug) console.log('over on : ' + this.id); jQuery(event.target, $container).addClass(opt.hoverClass); }).mouseout(function (event) { hasfocus = -1; if (opt.debug) console.log('out on : ' + this.id); jQuery(event.target, $container).removeClass(opt.hoverClass); }).click(function (event) { var fl = $('li.' + opt.hoverClass, $container).get(0); if (opt.debug) console.log('click on :' + this.id); $('li.' + opt.currentClass).removeClass(opt.currentClass); $(this).addClass(opt.currentClass); setCurrent(); hideMe(); });
        }); return ul;
    }
}; (function ($) {
    var NivoSlider = function (element, options) {
        var settings = $.extend({}, $.fn.nivoSlider.defaults, options); var vars = { currentSlide: 0, currentImage: '', totalSlides: 0, running: false, paused: false, stop: false }; var slider = $(element); slider.data('nivo:vars', vars); slider.css('position', 'relative'); slider.addClass('nivoSlider'); var kids = slider.children(); kids.each(function () {
            var child = $(this); var link = ''; if (!child.is('img')) {
                if (child.is('a')) { child.addClass('nivo-imageLink'); link = child; }
                child = child.find('img:first');
            }
            var childWidth = child.width(); if (childWidth == 0) childWidth = child.attr('width'); var childHeight = child.height(); if (childHeight == 0) childHeight = child.attr('height'); if (childWidth > slider.width()) { slider.width(childWidth); }
            if (childHeight > slider.height()) { slider.height(childHeight); }
            if (link != '') { link.css('display', 'none'); }
            child.css('display', 'none'); vars.totalSlides++;
        }); if (settings.randomStart) { settings.startSlide = Math.floor(Math.random() * vars.totalSlides); }
        if (settings.startSlide > 0) { if (settings.startSlide >= vars.totalSlides) settings.startSlide = vars.totalSlides - 1; vars.currentSlide = settings.startSlide; }
        if ($(kids[vars.currentSlide]).is('img')) { vars.currentImage = $(kids[vars.currentSlide]); } else { vars.currentImage = $(kids[vars.currentSlide]).find('img:first'); }
        if ($(kids[vars.currentSlide]).is('a')) { $(kids[vars.currentSlide]).css('display', 'block'); }
        slider.css('background', 'url("' + vars.currentImage.attr('src') + '") no-repeat'); slider.append($('<div class="nivo-caption"><p></p></div>').css({ display: 'none', opacity: settings.captionOpacity })); $('.nivo-caption', slider).css('opacity', 0); var processCaption = function (settings) {
            var nivoCaption = $('.nivo-caption', slider); if (vars.currentImage.attr('title') != '' && vars.currentImage.attr('title') != undefined) {
                var title = vars.currentImage.attr('title'); if (title.substr(0, 1) == '#') title = $(title).html(); if (nivoCaption.css('opacity') != 0) { nivoCaption.find('p').stop().fadeTo(settings.animSpeed, 0, function () { $(this).html(title); $(this).stop().fadeTo(settings.animSpeed, 1); }); } else { nivoCaption.find('p').html(title); }
                nivoCaption.stop().fadeTo(settings.animSpeed, settings.captionOpacity);
            } else { nivoCaption.stop().fadeTo(settings.animSpeed, 0); }
        }
        processCaption(settings); var timer = 0; if (!settings.manualAdvance && kids.length > 1) { timer = setInterval(function () { nivoRun(slider, kids, settings, false); }, settings.pauseTime); }
        if (settings.directionNav) {
            slider.append('<div class="nivo-directionNav"><a class="nivo-prevNav">' + settings.prevText + '</a><a class="nivo-nextNav">' + settings.nextText + '</a></div>'); if (settings.directionNavHide) { $('.nivo-directionNav', slider).hide(); slider.hover(function () { $('.nivo-directionNav', slider).show(); }, function () { $('.nivo-directionNav', slider).hide(); }); }
            $('a.nivo-prevNav', slider).live('click', function () { if (vars.running) return false; clearInterval(timer); timer = ''; vars.currentSlide -= 2; nivoRun(slider, kids, settings, 'prev'); }); $('a.nivo-nextNav', slider).live('click', function () { if (vars.running) return false; clearInterval(timer); timer = ''; nivoRun(slider, kids, settings, 'next'); });
        }
        if (settings.controlNav) {
            var nivoControl = $('<div class="nivo-controlNav"></div>'); slider.append(nivoControl); for (var i = 0; i < kids.length; i++) {
                if (settings.controlNavThumbs) {
                    var child = kids.eq(i); if (!child.is('img')) { child = child.find('img:first'); }
                    if (settings.controlNavThumbsFromRel) { nivoControl.append('<a class="nivo-control" rel="' + i + '"><img src="' + child.attr('rel') + '" alt="" /></a>'); } else { nivoControl.append('<a class="nivo-control" rel="' + i + '"><img src="' + child.attr('src').replace(settings.controlNavThumbsSearch, settings.controlNavThumbsReplace) + '" alt="" /></a>'); }
                } else { nivoControl.append('<a class="nivo-control" rel="' + i + '">' + (i + 1) + '</a>'); }
            }
            $('.nivo-controlNav a:eq(' + vars.currentSlide + ')', slider).addClass('active'); $('.nivo-controlNav a', slider).live('click', function () { if (vars.running) return false; if ($(this).hasClass('active')) return false; clearInterval(timer); timer = ''; slider.css('background', 'url("' + vars.currentImage.attr('src') + '") no-repeat'); vars.currentSlide = $(this).attr('rel') - 1; nivoRun(slider, kids, settings, 'control'); });
        }
        if (settings.keyboardNav) {
            $(window).keypress(function (event) {
                if (event.keyCode == '37') { if (vars.running) return false; clearInterval(timer); timer = ''; vars.currentSlide -= 2; nivoRun(slider, kids, settings, 'prev'); }
                if (event.keyCode == '39') { if (vars.running) return false; clearInterval(timer); timer = ''; nivoRun(slider, kids, settings, 'next'); }
            });
        }
        if (settings.pauseOnHover) { slider.hover(function () { vars.paused = true; clearInterval(timer); timer = ''; }, function () { vars.paused = false; if (timer == '' && !settings.manualAdvance) { timer = setInterval(function () { nivoRun(slider, kids, settings, false); }, settings.pauseTime); } }); }
        slider.bind('nivo:animFinished', function () {
            vars.running = false; $(kids).each(function () { if ($(this).is('a')) { $(this).css('display', 'none'); } }); if ($(kids[vars.currentSlide]).is('a')) { $(kids[vars.currentSlide]).css('display', 'block'); }
            if (timer == '' && !vars.paused && !settings.manualAdvance) { timer = setInterval(function () { nivoRun(slider, kids, settings, false); }, settings.pauseTime); }
            settings.afterChange.call(this);
        }); var createSlices = function (slider, settings, vars) { for (var i = 0; i < settings.slices; i++) { var sliceWidth = Math.round(slider.width() / settings.slices); if (i == settings.slices - 1) { slider.append($('<div class="nivo-slice"></div>').css({ left: (sliceWidth * i) + 'px', width: (slider.width() - (sliceWidth * i)) + 'px', height: '0px', opacity: '0', background: 'url("' + vars.currentImage.attr('src') + '") no-repeat -' + ((sliceWidth + (i * sliceWidth)) - sliceWidth) + 'px 0%' })); } else { slider.append($('<div class="nivo-slice"></div>').css({ left: (sliceWidth * i) + 'px', width: sliceWidth + 'px', height: '0px', opacity: '0', background: 'url("' + vars.currentImage.attr('src') + '") no-repeat -' + ((sliceWidth + (i * sliceWidth)) - sliceWidth) + 'px 0%' })); } } }
        var createBoxes = function (slider, settings, vars) { var boxWidth = Math.round(slider.width() / settings.boxCols); var boxHeight = Math.round(slider.height() / settings.boxRows); for (var rows = 0; rows < settings.boxRows; rows++) { for (var cols = 0; cols < settings.boxCols; cols++) { if (cols == settings.boxCols - 1) { slider.append($('<div class="nivo-box"></div>').css({ opacity: 0, left: (boxWidth * cols) + 'px', top: (boxHeight * rows) + 'px', width: (slider.width() - (boxWidth * cols)) + 'px', height: boxHeight + 'px', background: 'url("' + vars.currentImage.attr('src') + '") no-repeat -' + ((boxWidth + (cols * boxWidth)) - boxWidth) + 'px -' + ((boxHeight + (rows * boxHeight)) - boxHeight) + 'px' })); } else { slider.append($('<div class="nivo-box"></div>').css({ opacity: 0, left: (boxWidth * cols) + 'px', top: (boxHeight * rows) + 'px', width: boxWidth + 'px', height: boxHeight + 'px', background: 'url("' + vars.currentImage.attr('src') + '") no-repeat -' + ((boxWidth + (cols * boxWidth)) - boxWidth) + 'px -' + ((boxHeight + (rows * boxHeight)) - boxHeight) + 'px' })); } } } }
        var nivoRun = function (slider, kids, settings, nudge) {
            var vars = slider.data('nivo:vars'); if (vars && (vars.currentSlide == vars.totalSlides - 1)) { settings.lastSlide.call(this); }
            if ((!vars || vars.stop) && !nudge) return false; settings.beforeChange.call(this); if (!nudge) { slider.css('background', 'url("' + vars.currentImage.attr('src') + '") no-repeat'); } else {
                if (nudge == 'prev') { slider.css('background', 'url("' + vars.currentImage.attr('src') + '") no-repeat'); }
                if (nudge == 'next') { slider.css('background', 'url("' + vars.currentImage.attr('src') + '") no-repeat'); }
            }
            vars.currentSlide++; if (vars.currentSlide == vars.totalSlides) { vars.currentSlide = 0; settings.slideshowEnd.call(this); }
            if (vars.currentSlide < 0) vars.currentSlide = (vars.totalSlides - 1); if ($(kids[vars.currentSlide]).is('img')) { vars.currentImage = $(kids[vars.currentSlide]); } else { vars.currentImage = $(kids[vars.currentSlide]).find('img:first'); }
            if (settings.controlNav) { $('.nivo-controlNav a', slider).removeClass('active'); $('.nivo-controlNav a:eq(' + vars.currentSlide + ')', slider).addClass('active'); }
            processCaption(settings); $('.nivo-slice', slider).remove(); $('.nivo-box', slider).remove(); var currentEffect = settings.effect; if (settings.effect == 'random') { var anims = new Array('sliceDownRight', 'sliceDownLeft', 'sliceUpRight', 'sliceUpLeft', 'sliceUpDown', 'sliceUpDownLeft', 'fold', 'fade', 'boxRandom', 'boxRain', 'boxRainReverse', 'boxRainGrow', 'boxRainGrowReverse'); currentEffect = anims[Math.floor(Math.random() * (anims.length + 1))]; if (currentEffect == undefined) currentEffect = 'fade'; }
            if (settings.effect.indexOf(',') != -1) { var anims = settings.effect.split(','); currentEffect = anims[Math.floor(Math.random() * (anims.length))]; if (currentEffect == undefined) currentEffect = 'fade'; }
            if (vars.currentImage.attr('data-transition')) { currentEffect = vars.currentImage.attr('data-transition'); }
            vars.running = true; if (currentEffect == 'sliceDown' || currentEffect == 'sliceDownRight' || currentEffect == 'sliceDownLeft') {
                createSlices(slider, settings, vars); var timeBuff = 0; var i = 0; var slices = $('.nivo-slice', slider); if (currentEffect == 'sliceDownLeft') slices = $('.nivo-slice', slider)._reverse(); slices.each(function () {
                    var slice = $(this); slice.css({ 'top': '0px' }); if (i == settings.slices - 1) { setTimeout(function () { slice.animate({ height: '100%', opacity: '1.0' }, settings.animSpeed, '', function () { slider.trigger('nivo:animFinished'); }); }, (100 + timeBuff)); } else { setTimeout(function () { slice.animate({ height: '100%', opacity: '1.0' }, settings.animSpeed); }, (100 + timeBuff)); }
                    timeBuff += 50; i++;
                });
            }
            else if (currentEffect == 'sliceUp' || currentEffect == 'sliceUpRight' || currentEffect == 'sliceUpLeft') {
                createSlices(slider, settings, vars); var timeBuff = 0; var i = 0; var slices = $('.nivo-slice', slider); if (currentEffect == 'sliceUpLeft') slices = $('.nivo-slice', slider)._reverse(); slices.each(function () {
                    var slice = $(this); slice.css({ 'bottom': '0px' }); if (i == settings.slices - 1) { setTimeout(function () { slice.animate({ height: '100%', opacity: '1.0' }, settings.animSpeed, '', function () { slider.trigger('nivo:animFinished'); }); }, (100 + timeBuff)); } else { setTimeout(function () { slice.animate({ height: '100%', opacity: '1.0' }, settings.animSpeed); }, (100 + timeBuff)); }
                    timeBuff += 50; i++;
                });
            }
            else if (currentEffect == 'sliceUpDown' || currentEffect == 'sliceUpDownRight' || currentEffect == 'sliceUpDownLeft') {
                createSlices(slider, settings, vars); var timeBuff = 0; var i = 0; var v = 0; var slices = $('.nivo-slice', slider); if (currentEffect == 'sliceUpDownLeft') slices = $('.nivo-slice', slider)._reverse(); slices.each(function () {
                    var slice = $(this); if (i == 0) { slice.css('top', '0px'); i++; } else { slice.css('bottom', '0px'); i = 0; }
                    if (v == settings.slices - 1) { setTimeout(function () { slice.animate({ height: '100%', opacity: '1.0' }, settings.animSpeed, '', function () { slider.trigger('nivo:animFinished'); }); }, (100 + timeBuff)); } else { setTimeout(function () { slice.animate({ height: '100%', opacity: '1.0' }, settings.animSpeed); }, (100 + timeBuff)); }
                    timeBuff += 50; v++;
                });
            }
            else if (currentEffect == 'fold') {
                createSlices(slider, settings, vars); var timeBuff = 0; var i = 0; $('.nivo-slice', slider).each(function () {
                    var slice = $(this); var origWidth = slice.width(); slice.css({ top: '0px', height: '100%', width: '0px' }); if (i == settings.slices - 1) { setTimeout(function () { slice.animate({ width: origWidth, opacity: '1.0' }, settings.animSpeed, '', function () { slider.trigger('nivo:animFinished'); }); }, (100 + timeBuff)); } else { setTimeout(function () { slice.animate({ width: origWidth, opacity: '1.0' }, settings.animSpeed); }, (100 + timeBuff)); }
                    timeBuff += 50; i++;
                });
            }
            else if (currentEffect == 'fade') { createSlices(slider, settings, vars); var firstSlice = $('.nivo-slice:first', slider); firstSlice.css({ 'height': '100%', 'width': slider.width() + 'px' }); firstSlice.animate({ opacity: '1.0' }, (settings.animSpeed * 2), '', function () { slider.trigger('nivo:animFinished'); }); }
            else if (currentEffect == 'slideInRight') { createSlices(slider, settings, vars); var firstSlice = $('.nivo-slice:first', slider); firstSlice.css({ 'height': '100%', 'width': '0px', 'opacity': '1' }); firstSlice.animate({ width: slider.width() + 'px' }, (settings.animSpeed * 2), '', function () { slider.trigger('nivo:animFinished'); }); }
            else if (currentEffect == 'slideInLeft') { createSlices(slider, settings, vars); var firstSlice = $('.nivo-slice:first', slider); firstSlice.css({ 'height': '100%', 'width': '0px', 'opacity': '1', 'left': '', 'right': '0px' }); firstSlice.animate({ width: slider.width() + 'px' }, (settings.animSpeed * 2), '', function () { firstSlice.css({ 'left': '0px', 'right': '' }); slider.trigger('nivo:animFinished'); }); }
            else if (currentEffect == 'boxRandom') {
                createBoxes(slider, settings, vars); var totalBoxes = settings.boxCols * settings.boxRows; var i = 0; var timeBuff = 0; var boxes = shuffle($('.nivo-box', slider)); boxes.each(function () {
                    var box = $(this); if (i == totalBoxes - 1) { setTimeout(function () { box.animate({ opacity: '1' }, settings.animSpeed, '', function () { slider.trigger('nivo:animFinished'); }); }, (100 + timeBuff)); } else { setTimeout(function () { box.animate({ opacity: '1' }, settings.animSpeed); }, (100 + timeBuff)); }
                    timeBuff += 20; i++;
                });
            }
            else if (currentEffect == 'boxRain' || currentEffect == 'boxRainReverse' || currentEffect == 'boxRainGrow' || currentEffect == 'boxRainGrowReverse') {
                createBoxes(slider, settings, vars); var totalBoxes = settings.boxCols * settings.boxRows; var i = 0; var timeBuff = 0; var rowIndex = 0; var colIndex = 0; var box2Darr = new Array(); box2Darr[rowIndex] = new Array(); var boxes = $('.nivo-box', slider); if (currentEffect == 'boxRainReverse' || currentEffect == 'boxRainGrowReverse') { boxes = $('.nivo-box', slider)._reverse(); }
                boxes.each(function () { box2Darr[rowIndex][colIndex] = $(this); colIndex++; if (colIndex == settings.boxCols) { rowIndex++; colIndex = 0; box2Darr[rowIndex] = new Array(); } }); for (var cols = 0; cols < (settings.boxCols * 2) ; cols++) {
                    var prevCol = cols; for (var rows = 0; rows < settings.boxRows; rows++) {
                        if (prevCol >= 0 && prevCol < settings.boxCols) {
                            (function (row, col, time, i, totalBoxes) {
                                var box = $(box2Darr[row][col]); var w = box.width(); var h = box.height(); if (currentEffect == 'boxRainGrow' || currentEffect == 'boxRainGrowReverse') { box.width(0).height(0); }
                                if (i == totalBoxes - 1) { setTimeout(function () { box.animate({ opacity: '1', width: w, height: h }, settings.animSpeed / 1.3, '', function () { slider.trigger('nivo:animFinished'); }); }, (100 + time)); } else { setTimeout(function () { box.animate({ opacity: '1', width: w, height: h }, settings.animSpeed / 1.3); }, (100 + time)); }
                            })(rows, prevCol, timeBuff, i, totalBoxes); i++;
                        }
                        prevCol--;
                    }
                    timeBuff += 100;
                }
            }
        }
        var shuffle = function (arr) { for (var j, x, i = arr.length; i; j = parseInt(Math.random() * i), x = arr[--i], arr[i] = arr[j], arr[j] = x); return arr; }
        var trace = function (msg) {
            if (this.console && typeof console.log != "undefined")
                console.log(msg);
        }
        this.stop = function () { if (!$(element).data('nivo:vars').stop) { $(element).data('nivo:vars').stop = true; trace('Stop Slider'); } }
        this.start = function () { if ($(element).data('nivo:vars').stop) { $(element).data('nivo:vars').stop = false; trace('Start Slider'); } }
        settings.afterLoad.call(this); return this;
    }; $.fn.nivoSlider = function (options) { return this.each(function (key, value) { var element = $(this); if (element.data('nivoslider')) return element.data('nivoslider'); var nivoslider = new NivoSlider(this, options); element.data('nivoslider', nivoslider); }); }; $.fn.nivoSlider.defaults = { effect: 'random', slices: 15, boxCols: 8, boxRows: 4, animSpeed: 500, pauseTime: 3000, startSlide: 0, directionNav: true, directionNavHide: true, controlNav: true, controlNavThumbs: false, controlNavThumbsFromRel: false, controlNavThumbsSearch: '.jpg', controlNavThumbsReplace: '_thumb.jpg', keyboardNav: true, pauseOnHover: true, manualAdvance: false, captionOpacity: 0.8, prevText: 'Prev', nextText: 'Next', randomStart: false, beforeChange: function () { }, afterChange: function () { }, slideshowEnd: function () { }, lastSlide: function () { }, afterLoad: function () { } }; $.fn._reverse = [].reverse;
})(jQuery); (function (a) { a.tiny = a.tiny || {}; a.tiny.scrollbar = { options: { axis: "y", wheel: 40, scroll: true, lockscroll: true, size: "auto", sizethumb: "auto", invertscroll: false } }; a.fn.tinyscrollbar = function (d) { var c = a.extend({}, a.tiny.scrollbar.options, d); this.each(function () { a(this).data("tsb", new b(a(this), c)) }); return this }; a.fn.tinyscrollbar_update = function (c) { return a(this).data("tsb").update(c) }; function b(q, g) { var k = this, t = q, j = { obj: a(".viewport", q) }, h = { obj: a(".overview", q) }, d = { obj: a(".scrollbar", q) }, m = { obj: a(".track", d.obj) }, p = { obj: a(".thumb", d.obj) }, l = g.axis === "x", n = l ? "left" : "top", v = l ? "Width" : "Height", r = 0, y = { start: 0, now: 0 }, o = {}, e = ("ontouchstart" in document.documentElement) ? true : false; function c() { k.update(); s(); return k } this.update = function (z) { j[g.axis] = j.obj[0]["offset" + v]; h[g.axis] = h.obj[0]["scroll" + v]; h.ratio = j[g.axis] / h[g.axis]; d.obj.toggleClass("disable", h.ratio >= 1); m[g.axis] = g.size === "auto" ? j[g.axis] : g.size; p[g.axis] = Math.min(m[g.axis], Math.max(0, (g.sizethumb === "auto" ? (m[g.axis] * h.ratio) : g.sizethumb))); d.ratio = g.sizethumb === "auto" ? (h[g.axis] / m[g.axis]) : (h[g.axis] - j[g.axis]) / (m[g.axis] - p[g.axis]); r = (z === "relative" && h.ratio <= 1) ? Math.min((h[g.axis] - j[g.axis]), Math.max(0, r)) : 0; r = (z === "bottom" && h.ratio <= 1) ? (h[g.axis] - j[g.axis]) : isNaN(parseInt(z, 10)) ? r : parseInt(z, 10); w() }; function w() { var z = v.toLowerCase(); p.obj.css(n, r / d.ratio); h.obj.css(n, -r); o.start = p.obj.offset()[n]; d.obj.css(z, m[g.axis]); m.obj.css(z, m[g.axis]); p.obj.css(z, p[g.axis]) } function s() { if (!e) { p.obj.bind("mousedown", i); m.obj.bind("mouseup", u) } else { j.obj[0].ontouchstart = function (z) { if (1 === z.touches.length) { i(z.touches[0]); z.stopPropagation() } } } if (g.scroll && window.addEventListener) { t[0].addEventListener("DOMMouseScroll", x, false); t[0].addEventListener("mousewheel", x, false) } else { if (g.scroll) { t[0].onmousewheel = x } } } function i(A) { a("body").addClass("noSelect"); var z = parseInt(p.obj.css(n), 10); o.start = l ? A.pageX : A.pageY; y.start = z == "auto" ? 0 : z; if (!e) { a(document).bind("mousemove", u); a(document).bind("mouseup", f); p.obj.bind("mouseup", f) } else { document.ontouchmove = function (B) { B.preventDefault(); u(B.touches[0]) }; document.ontouchend = f } } function x(B) { if (h.ratio < 1) { var A = B || window.event, z = A.wheelDelta ? A.wheelDelta / 120 : -A.detail / 3; r -= z * g.wheel; r = Math.min((h[g.axis] - j[g.axis]), Math.max(0, r)); p.obj.css(n, r / d.ratio); h.obj.css(n, -r); if (g.lockscroll || (r !== (h[g.axis] - j[g.axis]) && r !== 0)) { A = a.event.fix(A); A.preventDefault() } } } function u(z) { if (h.ratio < 1) { if (g.invertscroll && e) { y.now = Math.min((m[g.axis] - p[g.axis]), Math.max(0, (y.start + (o.start - (l ? z.pageX : z.pageY))))) } else { y.now = Math.min((m[g.axis] - p[g.axis]), Math.max(0, (y.start + ((l ? z.pageX : z.pageY) - o.start)))) } r = y.now * d.ratio; h.obj.css(n, -r); p.obj.css(n, y.now) } } function f() { a("body").removeClass("noSelect"); a(document).unbind("mousemove", u); a(document).unbind("mouseup", f); p.obj.unbind("mouseup", f); document.ontouchmove = document.ontouchend = null } return c() } }(jQuery)); (function ($) {
    function Countdown() { this.regional = []; this.regional[''] = { labels: ['Years', 'Months', 'Weeks', 'Days', 'Hours', 'Minutes', 'Seconds'], labels1: ['Year', 'Month', 'Week', 'Day', 'Hour', 'Minute', 'Second'], compactLabels: ['y', 'm', 'w', 'd'], whichLabels: null, timeSeparator: ':', isRTL: false }; this._defaults = { until: null, since: null, timezone: null, serverSync: null, format: 'dHMS', layout: '', compact: false, significant: 0, description: '', expiryUrl: '', expiryText: '', alwaysExpire: false, onExpiry: null, onTick: null, tickInterval: 1 }; $.extend(this._defaults, this.regional['']); this._serverSyncs = []; }
    var PROP_NAME = 'countdown'; var Y = 0; var O = 1; var W = 2; var D = 3; var H = 4; var M = 5; var S = 6; $.extend(Countdown.prototype, {
        markerClassName: 'hasCountdown', _timer: setInterval(function () { $.countdown._updateTargets(); }, 980), _timerTargets: [], setDefaults: function (options) { this._resetExtraLabels(this._defaults, options); extendRemove(this._defaults, options || {}); }, UTCDate: function (tz, year, month, day, hours, mins, secs, ms) {
            if (typeof year == 'object' && year.constructor == Date) { ms = year.getMilliseconds(); secs = year.getSeconds(); mins = year.getMinutes(); hours = year.getHours(); day = year.getDate(); month = year.getMonth(); year = year.getFullYear(); }
            var d = new Date(); d.setUTCFullYear(year); d.setUTCDate(1); d.setUTCMonth(month || 0); d.setUTCDate(day || 1); d.setUTCHours(hours || 0); d.setUTCMinutes((mins || 0) - (Math.abs(tz) < 30 ? tz * 60 : tz)); d.setUTCSeconds(secs || 0); d.setUTCMilliseconds(ms || 0); return d;
        }, periodsToSeconds: function (periods) {
            return periods[0] * 31557600 + periods[1] * 2629800 + periods[2] * 604800 +
            periods[3] * 86400 + periods[4] * 3600 + periods[5] * 60 + periods[6];
        }, _settingsCountdown: function (target, name) {
            if (!name) { return $.countdown._defaults; }
            var inst = $.data(target, PROP_NAME); return (name == 'all' ? inst.options : inst.options[name]);
        }, _attachCountdown: function (target, options) {
            var $target = $(target); if ($target.hasClass(this.markerClassName)) { return; }
            $target.addClass(this.markerClassName); var inst = { options: $.extend({}, options), _periods: [0, 0, 0, 0, 0, 0, 0] }; $.data(target, PROP_NAME, inst); this._changeCountdown(target);
        }, _addTarget: function (target) { if (!this._hasTarget(target)) { this._timerTargets.push(target); } }, _hasTarget: function (target) { return ($.inArray(target, this._timerTargets) > -1); }, _removeTarget: function (target) { this._timerTargets = $.map(this._timerTargets, function (value) { return (value == target ? null : value); }); }, _updateTargets: function () { for (var i = this._timerTargets.length - 1; i >= 0; i--) { this._updateCountdown(this._timerTargets[i]); } }, _updateCountdown: function (target, inst) {
            var $target = $(target); inst = inst || $.data(target, PROP_NAME); if (!inst) { return; }
            $target.html(this._generateHTML(inst)); $target[(this._get(inst, 'isRTL') ? 'add' : 'remove') + 'Class']('countdown_rtl'); var onTick = this._get(inst, 'onTick'); if (onTick) { var periods = inst._hold != 'lap' ? inst._periods : this._calculatePeriods(inst, inst._show, this._get(inst, 'significant'), new Date()); var tickInterval = this._get(inst, 'tickInterval'); if (tickInterval == 1 || this.periodsToSeconds(periods) % tickInterval == 0) { onTick.apply(target, [periods]); } }
            var expired = inst._hold != 'pause' && (inst._since ? inst._now.getTime() < inst._since.getTime() : inst._now.getTime() >= inst._until.getTime()); if (expired && !inst._expiring) {
                inst._expiring = true; if (this._hasTarget(target) || this._get(inst, 'alwaysExpire')) {
                    this._removeTarget(target); var onExpiry = this._get(inst, 'onExpiry'); if (onExpiry) { onExpiry.apply(target, []); }
                    var expiryText = this._get(inst, 'expiryText'); if (expiryText) { var layout = this._get(inst, 'layout'); inst.options.layout = expiryText; this._updateCountdown(target, inst); inst.options.layout = layout; }
                    var expiryUrl = this._get(inst, 'expiryUrl'); if (expiryUrl) { window.location = expiryUrl; }
                }
                inst._expiring = false;
            }
            else if (inst._hold == 'pause') { this._removeTarget(target); }
            $.data(target, PROP_NAME, inst);
        }, _changeCountdown: function (target, options, value) {
            options = options || {}; if (typeof options == 'string') { var name = options; options = {}; options[name] = value; }
            var inst = $.data(target, PROP_NAME); if (inst) {
                this._resetExtraLabels(inst.options, options); extendRemove(inst.options, options); this._adjustSettings(target, inst); $.data(target, PROP_NAME, inst); var now = new Date(); if ((inst._since && inst._since < now) || (inst._until && inst._until > now)) { this._addTarget(target); }
                this._updateCountdown(target, inst);
            }
        }, _resetExtraLabels: function (base, options) {
            var changingLabels = false; for (var n in options) { if (n != 'whichLabels' && n.match(/[Ll]abels/)) { changingLabels = true; break; } }
            if (changingLabels) { for (var n in base) { if (n.match(/[Ll]abels[0-9]/)) { base[n] = null; } } }
        }, _adjustSettings: function (target, inst) {
            var now; var serverSync = this._get(inst, 'serverSync'); var serverOffset = 0; var serverEntry = null; for (var i = 0; i < this._serverSyncs.length; i++) { if (this._serverSyncs[i][0] == serverSync) { serverEntry = this._serverSyncs[i][1]; break; } }
            if (serverEntry != null) { serverOffset = (serverSync ? serverEntry : 0); now = new Date(); }
            else { var serverResult = (serverSync ? serverSync.apply(target, []) : null); now = new Date(); serverOffset = (serverResult ? now.getTime() - serverResult.getTime() : 0); this._serverSyncs.push([serverSync, serverOffset]); }
            var timezone = this._get(inst, 'timezone'); timezone = (timezone == null ? -now.getTimezoneOffset() : timezone); inst._since = this._get(inst, 'since'); if (inst._since != null) { inst._since = this.UTCDate(timezone, this._determineTime(inst._since, null)); if (inst._since && serverOffset) { inst._since.setMilliseconds(inst._since.getMilliseconds() + serverOffset); } }
            inst._until = this.UTCDate(timezone, this._determineTime(this._get(inst, 'until'), now)); if (serverOffset) { inst._until.setMilliseconds(inst._until.getMilliseconds() + serverOffset); }
            inst._show = this._determineShow(inst);
        }, _destroyCountdown: function (target) {
            var $target = $(target); if (!$target.hasClass(this.markerClassName)) { return; }
            this._removeTarget(target); $target.removeClass(this.markerClassName).empty(); $.removeData(target, PROP_NAME);
        }, _pauseCountdown: function (target) { this._hold(target, 'pause'); }, _lapCountdown: function (target) { this._hold(target, 'lap'); }, _resumeCountdown: function (target) { this._hold(target, null); }, _hold: function (target, hold) {
            var inst = $.data(target, PROP_NAME); if (inst) {
                if (inst._hold == 'pause' && !hold) {
                    inst._periods = inst._savePeriods; var sign = (inst._since ? '-' : '+'); inst[inst._since ? '_since' : '_until'] = this._determineTime(sign + inst._periods[0] + 'y' +
                    sign + inst._periods[1] + 'o' + sign + inst._periods[2] + 'w' +
                    sign + inst._periods[3] + 'd' + sign + inst._periods[4] + 'h' +
                    sign + inst._periods[5] + 'm' + sign + inst._periods[6] + 's'); this._addTarget(target);
                }
                inst._hold = hold; inst._savePeriods = (hold == 'pause' ? inst._periods : null); $.data(target, PROP_NAME, inst); this._updateCountdown(target, inst);
            }
        }, _getTimesCountdown: function (target) { var inst = $.data(target, PROP_NAME); return (!inst ? null : (!inst._hold ? inst._periods : this._calculatePeriods(inst, inst._show, this._get(inst, 'significant'), new Date()))); }, _get: function (inst, name) { return (inst.options[name] != null ? inst.options[name] : $.countdown._defaults[name]); }, _determineTime: function (setting, defaultTime) {
            var offsetNumeric = function (offset) { var time = new Date(); time.setTime(time.getTime() + offset * 1000); return time; }; var offsetString = function (offset) {
                offset = offset.toLowerCase(); var time = new Date(); var year = time.getFullYear(); var month = time.getMonth(); var day = time.getDate(); var hour = time.getHours(); var minute = time.getMinutes(); var second = time.getSeconds(); var pattern = /([+-]?[0-9]+)\s*(s|m|h|d|w|o|y)?/g; var matches = pattern.exec(offset); while (matches) {
                    switch (matches[2] || 's') { case 's': second += parseInt(matches[1], 10); break; case 'm': minute += parseInt(matches[1], 10); break; case 'h': hour += parseInt(matches[1], 10); break; case 'd': day += parseInt(matches[1], 10); break; case 'w': day += parseInt(matches[1], 10) * 7; break; case 'o': month += parseInt(matches[1], 10); day = Math.min(day, $.countdown._getDaysInMonth(year, month)); break; case 'y': year += parseInt(matches[1], 10); day = Math.min(day, $.countdown._getDaysInMonth(year, month)); break; }
                    matches = pattern.exec(offset);
                }
                return new Date(year, month, day, hour, minute, second, 0);
            }; var time = (setting == null ? defaultTime : (typeof setting == 'string' ? offsetString(setting) : (typeof setting == 'number' ? offsetNumeric(setting) : setting))); if (time) time.setMilliseconds(0); return time;
        }, _getDaysInMonth: function (year, month) { return 32 - new Date(year, month, 32).getDate(); }, _normalLabels: function (num) { return num; }, _generateHTML: function (inst) {
            var significant = this._get(inst, 'significant'); inst._periods = (inst._hold ? inst._periods : this._calculatePeriods(inst, inst._show, significant, new Date())); var shownNonZero = false; var showCount = 0; var sigCount = significant; var show = $.extend({}, inst._show); for (var period = Y; period <= S; period++) { shownNonZero |= (inst._show[period] == '?' && inst._periods[period] > 0); show[period] = (inst._show[period] == '?' && !shownNonZero ? null : inst._show[period]); showCount += (show[period] ? 1 : 0); sigCount -= (inst._periods[period] > 0 ? 1 : 0); }
            var showSignificant = [false, false, false, false, false, false, false]; for (var period = S; period >= Y; period--) {
                if (inst._show[period]) {
                    if (inst._periods[period]) { showSignificant[period] = true; }
                    else { showSignificant[period] = sigCount > 0; sigCount--; }
                }
            }
            var compact = this._get(inst, 'compact'); var layout = this._get(inst, 'layout'); var labels = (compact ? this._get(inst, 'compactLabels') : this._get(inst, 'labels')); var whichLabels = this._get(inst, 'whichLabels') || this._normalLabels; var timeSeparator = this._get(inst, 'timeSeparator'); var description = this._get(inst, 'description') || ''; var showCompact = function (period) {
                var labelsNum = $.countdown._get(inst, 'compactLabels' + whichLabels(inst._periods[period])); return (show[period] ? inst._periods[period] +
                (labelsNum ? labelsNum[period] : labels[period]) + ' ' : '');
            }; var showFull = function (period) {
                var labelsNum = $.countdown._get(inst, 'labels' + whichLabels(inst._periods[period])); return ((!significant && show[period]) || (significant && showSignificant[period]) ? '<span class="countdown_section"><span class="countdown_amount">' +
                inst._periods[period] + '</span><br/>' +
                (labelsNum ? labelsNum[period] : labels[period]) + '</span>' : '');
            }; return (layout ? this._buildLayout(inst, show, layout, compact, significant, showSignificant) : ((compact ? '<span class="countdown_row countdown_amount' +
            (inst._hold ? ' countdown_holding' : '') + '">' +
            showCompact(Y) + showCompact(O) + showCompact(W) + showCompact(D) +
            (show[H] ? this._minDigits(inst._periods[H], 2) : '') +
            (show[M] ? (show[H] ? timeSeparator : '') +
            this._minDigits(inst._periods[M], 2) : '') +
            (show[S] ? (show[H] || show[M] ? timeSeparator : '') +
            this._minDigits(inst._periods[S], 2) : '') : '<span class="countdown_row countdown_show' + (significant || showCount) +
            (inst._hold ? ' countdown_holding' : '') + '">' +
            showFull(Y) + showFull(O) + showFull(W) + showFull(D) +
            showFull(H) + showFull(M) + showFull(S)) + '</span>' +
            (description ? '<span class="countdown_row countdown_descr">' + description + '</span>' : '')));
        }, _buildLayout: function (inst, show, layout, compact, significant, showSignificant) {
            var labels = this._get(inst, (compact ? 'compactLabels' : 'labels')); var whichLabels = this._get(inst, 'whichLabels') || this._normalLabels; var labelFor = function (index) { return ($.countdown._get(inst, (compact ? 'compactLabels' : 'labels') + whichLabels(inst._periods[index])) || labels)[index]; }; var digit = function (value, position) { return Math.floor(value / position) % 10; }; var subs = { desc: this._get(inst, 'description'), sep: this._get(inst, 'timeSeparator'), yl: labelFor(Y), yn: inst._periods[Y], ynn: this._minDigits(inst._periods[Y], 2), ynnn: this._minDigits(inst._periods[Y], 3), y1: digit(inst._periods[Y], 1), y10: digit(inst._periods[Y], 10), y100: digit(inst._periods[Y], 100), y1000: digit(inst._periods[Y], 1000), ol: labelFor(O), on: inst._periods[O], onn: this._minDigits(inst._periods[O], 2), onnn: this._minDigits(inst._periods[O], 3), o1: digit(inst._periods[O], 1), o10: digit(inst._periods[O], 10), o100: digit(inst._periods[O], 100), o1000: digit(inst._periods[O], 1000), wl: labelFor(W), wn: inst._periods[W], wnn: this._minDigits(inst._periods[W], 2), wnnn: this._minDigits(inst._periods[W], 3), w1: digit(inst._periods[W], 1), w10: digit(inst._periods[W], 10), w100: digit(inst._periods[W], 100), w1000: digit(inst._periods[W], 1000), dl: labelFor(D), dn: inst._periods[D], dnn: this._minDigits(inst._periods[D], 2), dnnn: this._minDigits(inst._periods[D], 3), d1: digit(inst._periods[D], 1), d10: digit(inst._periods[D], 10), d100: digit(inst._periods[D], 100), d1000: digit(inst._periods[D], 1000), hl: labelFor(H), hn: inst._periods[H], hnn: this._minDigits(inst._periods[H], 2), hnnn: this._minDigits(inst._periods[H], 3), h1: digit(inst._periods[H], 1), h10: digit(inst._periods[H], 10), h100: digit(inst._periods[H], 100), h1000: digit(inst._periods[H], 1000), ml: labelFor(M), mn: inst._periods[M], mnn: this._minDigits(inst._periods[M], 2), mnnn: this._minDigits(inst._periods[M], 3), m1: digit(inst._periods[M], 1), m10: digit(inst._periods[M], 10), m100: digit(inst._periods[M], 100), m1000: digit(inst._periods[M], 1000), sl: labelFor(S), sn: inst._periods[S], snn: this._minDigits(inst._periods[S], 2), snnn: this._minDigits(inst._periods[S], 3), s1: digit(inst._periods[S], 1), s10: digit(inst._periods[S], 10), s100: digit(inst._periods[S], 100), s1000: digit(inst._periods[S], 1000) }; var html = layout; for (var i = Y; i <= S; i++) { var period = 'yowdhms'.charAt(i); var re = new RegExp('\\{' + period + '<\\}(.*)\\{' + period + '>\\}', 'g'); html = html.replace(re, ((!significant && show[i]) || (significant && showSignificant[i]) ? '$1' : '')); }
            $.each(subs, function (n, v) { var re = new RegExp('\\{' + n + '\\}', 'g'); html = html.replace(re, v); }); return html;
        }, _minDigits: function (value, len) {
            value = '' + value; if (value.length >= len) { return value; }
            value = '0000000000' + value; return value.substr(value.length - len);
        }, _determineShow: function (inst) { var format = this._get(inst, 'format'); var show = []; show[Y] = (format.match('y') ? '?' : (format.match('Y') ? '!' : null)); show[O] = (format.match('o') ? '?' : (format.match('O') ? '!' : null)); show[W] = (format.match('w') ? '?' : (format.match('W') ? '!' : null)); show[D] = (format.match('d') ? '?' : (format.match('D') ? '!' : null)); show[H] = (format.match('h') ? '?' : (format.match('H') ? '!' : null)); show[M] = (format.match('m') ? '?' : (format.match('M') ? '!' : null)); show[S] = (format.match('s') ? '?' : (format.match('S') ? '!' : null)); return show; }, _calculatePeriods: function (inst, show, significant, now) {
            inst._now = now; inst._now.setMilliseconds(0); var until = new Date(inst._now.getTime()); if (inst._since) {
                if (now.getTime() < inst._since.getTime()) { inst._now = now = until; }
                else { now = inst._since; }
            }
            else { until.setTime(inst._until.getTime()); if (now.getTime() > inst._until.getTime()) { inst._now = now = until; } }
            var periods = [0, 0, 0, 0, 0, 0, 0]; if (show[Y] || show[O]) {
                var lastNow = $.countdown._getDaysInMonth(now.getFullYear(), now.getMonth()); var lastUntil = $.countdown._getDaysInMonth(until.getFullYear(), until.getMonth()); var sameDay = (until.getDate() == now.getDate() || (until.getDate() >= Math.min(lastNow, lastUntil) && now.getDate() >= Math.min(lastNow, lastUntil))); var getSecs = function (date) { return (date.getHours() * 60 + date.getMinutes()) * 60 + date.getSeconds(); }; var months = Math.max(0, (until.getFullYear() - now.getFullYear()) * 12 + until.getMonth() - now.getMonth() +
                ((until.getDate() < now.getDate() && !sameDay) || (sameDay && getSecs(until) < getSecs(now)) ? -1 : 0)); periods[Y] = (show[Y] ? Math.floor(months / 12) : 0); periods[O] = (show[O] ? months - periods[Y] * 12 : 0); now = new Date(now.getTime()); var wasLastDay = (now.getDate() == lastNow); var lastDay = $.countdown._getDaysInMonth(now.getFullYear() + periods[Y], now.getMonth() + periods[O]); if (now.getDate() > lastDay) { now.setDate(lastDay); }
                now.setFullYear(now.getFullYear() + periods[Y]); now.setMonth(now.getMonth() + periods[O]); if (wasLastDay) { now.setDate(lastDay); }
            }
            var diff = Math.floor((until.getTime() - now.getTime()) / 1000); var extractPeriod = function (period, numSecs) { periods[period] = (show[period] ? Math.floor(diff / numSecs) : 0); diff -= periods[period] * numSecs; }; extractPeriod(W, 604800); extractPeriod(D, 86400); extractPeriod(H, 3600); extractPeriod(M, 60); extractPeriod(S, 1); if (diff > 0 && !inst._since) {
                var multiplier = [1, 12, 4.3482, 7, 24, 60, 60]; var lastShown = S; var max = 1; for (var period = S; period >= Y; period--) {
                    if (show[period]) {
                        if (periods[lastShown] >= max) { periods[lastShown] = 0; diff = 1; }
                        if (diff > 0) { periods[period]++; diff = 0; lastShown = period; max = 1; }
                    }
                    max *= multiplier[period];
                }
            }
            if (significant) {
                for (var period = Y; period <= S; period++) {
                    if (significant && periods[period]) { significant--; }
                    else if (!significant) { periods[period] = 0; }
                }
            }
            return periods;
        }
    }); function extendRemove(target, props) {
        $.extend(target, props); for (var name in props) { if (props[name] == null) { target[name] = null; } }
        return target;
    }
    $.fn.countdown = function (options) {
        var otherArgs = Array.prototype.slice.call(arguments, 1); if (options == 'getTimes' || options == 'settings') { return $.countdown['_' + options + 'Countdown'].apply($.countdown, [this[0]].concat(otherArgs)); }
        return this.each(function () {
            if (typeof options == 'string') { $.countdown['_' + options + 'Countdown'].apply($.countdown, [this].concat(otherArgs)); }
            else { $.countdown._attachCountdown(this, options); }
        });
    }; $.countdown = new Countdown();
})(jQuery); (function ($, window) {
    var $window = $(window); $.fn.lazyload = function (options) {
        var elements = this; var $container; var settings = { threshold: 0, failure_limit: 0, event: "scroll", effect: "show", container: window, data_attribute: "original", skip_invisible: true, appear: null, load: null }; function update() {
            var counter = 0; elements.each(function () {
                var $this = $(this); if (settings.skip_invisible && !$this.is(":visible")) { return; }
                if ($.abovethetop(this, settings) || $.leftofbegin(this, settings)) { } else if (!$.belowthefold(this, settings) && !$.rightoffold(this, settings)) { $this.trigger("appear"); } else { if (++counter > settings.failure_limit) { return false; } }
            });
        }
        if (options) {
            if (undefined !== options.failurelimit) { options.failure_limit = options.failurelimit; delete options.failurelimit; }
            if (undefined !== options.effectspeed) { options.effect_speed = options.effectspeed; delete options.effectspeed; }
            $.extend(settings, options);
        }
        $container = (settings.container === undefined || settings.container === window) ? $window : $(settings.container); if (0 === settings.event.indexOf("scroll")) { $container.bind(settings.event, function (event) { return update(); }); }
        this.each(function () {
            var self = this; var $self = $(self); self.loaded = false; $self.one("appear", function () {
                if (!this.loaded) {
                    if (settings.npapear) { var elements_left = elements.length; settings.appear.call(self, elements_left, settings); }
                    $("<img />").bind("load", function () {
                        $self.hide().attr("src", $self.data(settings.data_attribute))
                        [settings.effect](settings.effect_speed); self.loaded = true; var temp = $.grep(elements, function (element) { return !element.loaded; }); elements = $(temp); $self.css("visibility", "visible"); if (settings.load) { var elements_left = elements.length; settings.load.call(self, elements_left, settings); }
                    }).attr("src", $self.data(settings.data_attribute));
                }
            }); if (0 !== settings.event.indexOf("scroll")) { $self.bind(settings.event, function (event) { if (!self.loaded) { $self.trigger("appear"); } }); }
        }); $window.bind("resize", function (event) { update(); }); update(); return this;
    }; $.belowthefold = function (element, settings) {
        var fold; if (settings.container === undefined || settings.container === window) { fold = $window.height() + $window.scrollTop(); } else { fold = $(settings.container).offset().top + $(settings.container).height(); }
        return fold <= $(element).offset().top - settings.threshold;
    }; $.rightoffold = function (element, settings) {
        var fold; if (settings.container === undefined || settings.container === window) { fold = $window.width() + $window.scrollLeft(); } else { fold = $(settings.container).offset().left + $(settings.container).width(); }
        return fold <= $(element).offset().left - settings.threshold;
    }; $.abovethetop = function (element, settings) {
        var fold; if (settings.container === undefined || settings.container === window) { fold = $window.scrollTop(); } else { fold = $(settings.container).offset().top; }
        return fold >= $(element).offset().top + settings.threshold + $(element).height();
    }; $.leftofbegin = function (element, settings) {
        var fold; if (settings.container === undefined || settings.container === window) { fold = $window.scrollLeft(); } else { fold = $(settings.container).offset().left; }
        return fold >= $(element).offset().left + settings.threshold + $(element).width();
    }; $.inviewport = function (element, settings) { return !$.rightofscreen(element, settings) && !$.leftofscreen(element, settings) && !$.belowthefold(element, settings) && !$.abovethetop(element, settings); }; $.extend($.expr[':'], { "below-the-fold": function (a) { return $.belowthefold(a, { threshold: 0 }); }, "above-the-top": function (a) { return !$.belowthefold(a, { threshold: 0 }); }, "right-of-screen": function (a) { return $.rightoffold(a, { threshold: 0 }); }, "left-of-screen": function (a) { return !$.rightoffold(a, { threshold: 0 }); }, "in-viewport": function (a) { return !$.inviewport(a, { threshold: 0 }); }, "above-the-fold": function (a) { return !$.belowthefold(a, { threshold: 0 }); }, "right-of-fold": function (a) { return $.rightoffold(a, { threshold: 0 }); }, "left-of-fold": function (a) { return !$.rightoffold(a, { threshold: 0 }); } });
})(jQuery, window); var search_remove_to = null; $(document).ready(function () {
    if (skin_version == 2) {
        $(".product-item").click(function () { }); $(".select_city").hover(function () { $(this).find(".selectbox-wrapper", this).css("display", "block"); }, function () { $(this).find(".selectbox-wrapper", this).css("display", "none"); }); $(".view-more-term").click(function () {
            var last_height = $(".product-terms-inner-in").attr("last-height"); $(".product-terms-inner-in").attr("last-height", $(".product-terms-inner-in").height()); if (last_height) { $(".product-terms-inner-in .row").css("height", last_height); $(".product-terms-inner-in").css("height", last_height); } else { $(".product-terms-inner-in .row").css("height", "auto"); $(".product-terms-inner-in").css("height", "auto"); }
            return false;
        }); $(".remain-time").each(function () { $(this).countdown({ until: $(this).attr('rel') - server_time, format: 'dHMS', layout: '<div>{dn}<br /><span>ngày</span></div><div class="sep">:</div><div>{hn}<br /><span>giờ</span></div><div class="sep">:</div><div>{mn}<br /><span>phút</span></div><div class="sep">:</div><div>{sn}<br /><span>giây</span></div>', expiryText: 'hết hạn' }); }); $(".view-more-large").live('click', function () { $.ajax({ url: $(this).attr("href") + '&paging=1', type: 'get', dataType: 'json', data: { result_ids: 'list_product_item,list_product_pagination', is_ajax: 1 }, beforeSend: function () { $("#list_product_pagination").before("<div id=\"ajax-new-load\"></div>"); }, success: function (response) { $("#ajax-new-load").remove(); var item_product_list = response.data.html.list_product_item; $("#list_product_item").append(item_product_list); var list_product_pagination = response.data.html.list_product_pagination; $("#list_product_pagination").html(list_product_pagination); } }); return false; }); $(".sort a:not(.star-filter)").live('click', function () {
            var anchor = $(this); $(".sort a").removeClass('active'); $(this).addClass('active'); $.ajax({
                url: $(this).attr("href"), type: 'get', dataType: 'json', data: { result_ids: 'list_product_item,list_product_pagination', is_ajax: 1 }, beforeSend: function () { $("#list_product_item").html("<div id=\"ajax-new-load\"></div>"); }, success: function (response) {
                    $("#ajax-new-load").remove(); if (anchor.hasClass('star-select')) { anchor.parents('.star-filters').find('.star-filter').html(anchor.html()); anchor.parents('.star-filters').find('ul').css("display", "none"); }
                    if (response.data.html) { var item_product_list = response.data.html.list_product_item; $("#list_product_item").html(item_product_list); var list_product_pagination = response.data.html.list_product_pagination; $("#list_product_pagination").html(list_product_pagination); }
                }
            }); return false;
        }); $("a.star-filter").click(function () { $(this).parents('.star-filters').find("ul").toggle(); return false; }); $(".product-item").live({
            mouseenter: function ()
            { $(this).find('.type,.meta').stop(); $(this).find('.type-voucher-star.hidden').stop(); $(this).find('.type,.meta').animate({ opacity: 1 }, 500).css("display", "block"); $(this).find('.type-voucher-star.hidden').animate({ opacity: 1 }, 500).css("display", "block"); $(this).find('.meta-recents').hide(); $(this).find('.meta-dealtoday').hide(); }, mouseleave: function () {
                $(this).find('.meta-recents').show(); $(this).find('.meta-dealtoday').show(); if ($('.meta-recents').hasClass('meta-recents') || $('.meta-recents').hasClass('meta-dealtoday')) { $(this).find('.type,.meta').css("display", "none"); $(this).find('.type-voucher-star.hidden').css("display", "none"); return; }
                $(this).find('.type,.meta').stop(); $(this).find('.type-voucher-star.hidden').stop(); $(this).find('.type,.meta').animate({ opacity: 0 }, 500).css("display", "none"); $(this).find('.type-voucher-star.hidden').animate({ opacity: 0 }, 500).css("display", "none");
            }
        }); reload_count_down_time_product(); reload_design_order_page(); fn_align_notificate_message(); if ($("body").hasClass('c-index') || $("body").hasClass('c-products')) {
            var menu = $('.hd-header-bottom'), pos = $(menu).offset(); var hasCategoryClass = false; if ($("body").hasClass('cm-category') || $("body").hasClass('c-products')) { hasCategoryClass = true; }
            $(window).scroll(function () {
                if ($(this).scrollTop() > pos.top) { $("body").removeClass('cm-category'); menu.css({ position: 'fixed', top: 0, zIndex: 5, width: '100%' }); } else if ($(this).scrollTop() <= pos.top) {
                    if (hasCategoryClass) { $("body").addClass('cm-category'); }
                    menu.css({ position: 'relative', left: 0, zIndex: -1 });
                }
            });
        }
        $(".nav-tabs a").click(function () {
            var parent = $(this).parents(".tabbable"); var content_id = $(this).attr("href"); parent.find('.nav-tabs li').removeClass('active'); $(this).parent('li').addClass('active'); $(".tab-pane", parent).css("display", "none"); $(content_id, parent).css("display", "block"); var iframe = $(content_id, parent).find('iframe'); if (!iframe.attr("src")) { iframe.attr("src", iframe.attr("org-src")); }
            return false;
        }); $(".nav-tabs a:first").trigger('click'); $(".product-description img").each(function () { $(this).parents("p").css("text-align", "center"); }); $('.change_quantity').live("change", function () { if ($("#txt_qty_tmp").val() != $(this).val()) { $('#button_cart_update').trigger('click'); } }); $('.notice-icon-nd').tipsy({ gravity: 'w', live: true }); $(".product-map .hidden").show(); var imapwidth = $(".location-address-box").width(); if (imapwidth > 0) { $(".location-address-box").css("margin-left", (640 - imapwidth) / 2 + 33); }
        imapwidth = $(".map-title-use-at .location-address-box").width(); if (imapwidth > 0) { $(".map-title-use-at .location-address-box").css("margin-left", (640 - imapwidth) / 2 + 33); }
        imapwidth = $(".map-title-booking-at .location-address-box").width(); if (imapwidth > 0) { $(".map-title-booking-at .location-address-box").css("margin-left", (640 - imapwidth) / 2 + 33); }
        $(".product-map .hidden").hide(); $(".fb-close").live("click", function () { var html_pool = $(".poll").html(); if (html_pool == "" || html_pool == null) { $(".feedback").remove(); } })
        var src_iframe = $(".product-description iframe").attr("src"); if (src_iframe != undefined) { if (src_iframe.indexOf('youtube.com') != -1 || src_iframe.indexOf('youtu.be') != -1) { $(".product-description iframe").show(); } }
        $(".hp-more-deal").click(function () { var rel = $(this).attr("rel"); var cat_id = $(this).attr("id"); if (rel == 1) { $("." + cat_id).show(); $(this).attr("rel", 2); $("#" + cat_id + " .btn-hp-more-deal span").hide(); $("#" + cat_id + " .btn-hp-more-deal span").eq(1).show(); $("#pagination_" + cat_id + " .result-count span").hide(); $("#pagination_" + cat_id + " .result-count span").eq(1).show(); return false; } }); $(".search-menu").hover(function () { $(".search-menu .sub-menu-wrapper").addClass('hover'); clearTimeout(search_remove_to); }, function () { }); $(".ui-autocomplete").live('hover', function () { clearTimeout(search_remove_to); }, function () { }); $(".catalog-menu").hover(function () { $(".sub-menu-search").removeClass('hover'); }); $(document).click(function (event) { if ($(event.target).closest('.sub-menu-search').get(0) == null && $(event.target).closest('.ui-autocomplete').get(0) == null) { $(".sub-menu-search").removeClass('hover'); } }); if ($("#scroll-active").attr('class')) { if ($("#scroll-active").attr('class').length > 0) { $(document).ready(function () { var select_id = ''; $(window).scroll(function () { var scroll = $(this).scrollTop() + 200; $('.page-section').each(function (index, element) { var _min = $(element).offset().top; if (scroll > _min) { select_id = $(element).attr('id'); } }); $('.first-level').removeClass('cm-active'); $('.' + select_id).addClass('cm-active'); }); }); } }
        $(".search-menu .form-search-box .search-button").click(function () { var txt_search = $(".search-menu .form-search-box .search-textbox").val(); txt_search = $.trim(txt_search); if (txt_search == "") { $(".search-menu .form-search-box .search-textbox").css("border-color", "#ED1C24"); $(".search-menu .form-search-box .search-textbox").focus(); return false; } }); $(".lazy").lazyload({ effect: "fadeIn", effectspeed: 180, threshold: 440 }).removeClass("lazy"); $(document).ajaxStop(function () { $(".lazy").lazyload({ effect: "fadeIn" }).removeClass("lazy"); reload_count_down_time_product(); reload_design_order_page(); fn_align_notificate_message(); });
    } else {
        $('.product_timeout .key').each(function () { $(this).countdown({ until: $(this).attr('rel') - server_time, format: 'HMS', layout: '{hn}:{mn}:{sn}', expiryText: 'hết hạn' }); }); $(".box_times .remain_times").countdown({ until: $(".box_times .remain_times").attr('rel'), format: 'HMS', layout: '<span>{hn}</span>:<span>{mn}</span>:<span>{sn}</span> ', expiryText: 'Hết Hạn', onExpiry: reload_coutdown_travel }); if ($(".remain_times_travel").attr('rel') > 0) { $(".box_times .remain_times_travel").countdown({ until: $(".box_times .remain_times_travel").attr('rel'), format: 'HMS', layout: '<span>{hn}</span>:<span>{mn}</span>:<span>{sn}</span> ', expiryText: 'Hết Hạn', onExpiry: reload_coutdown_travel }); }
        $('.limitdate .key').countdown({ until: $('.limitdate .key').attr('rel'), format: 'HMS', layout: '{hn} giờ<br>{mn} phút<br>{sn} giây', expiryText: 'hết hạn', onExpiry: reload_coutdown_travel }); $('#slider').nivoSlider({ effect: 'fade' }); var nav_margin = -(($('#slider img').length * 22) / 2); $('.nivo-controlNav').css('margin-left', nav_margin + 'px'); $(".product_image img.lazy").lazyload({ effect: "fadeIn", effectspeed: 180, threshold: 440 }); if ($("body").hasClass('cm-index-index')) { var menu = $('.nav_2'), pos = menu.offset(); $(window).scroll(function () { if ($(this).scrollTop() > pos.top) { menu.css({ position: 'fixed', top: 0, left: '50%', marginLeft: -menu.width() / 2 + 'px', zIndex: 5 }); } else if ($(this).scrollTop() <= pos.top) { menu.css({ position: 'relative', left: 0, marginLeft: 0, zIndex: 1 }); } }); }
    }
    $("#summary_form #place_order").live('click', function () { if ($(".place_order_hidden").length <= 0) { $(this).before("<input type=\"hidden\" name=\"dispatch[checkout.place_order]\" class=\"place_order_hidden\" value=\"Y\" />"); $(this).attr("disabled", "disabled"); $.toggleStatusBox('show', 'Đang đặt hàng. Xin bạn vui lòng chờ trong giây lát...'); $("#summary_form").submit(); } }); $(document).click(function (event) { if ($(event.target).closest('.buy-button-options').get(0) == null && $(event.target).closest('.dealoptions').get(0) == null) { $(".dealoptions").hide(); } }); $().UItoTop({ easingType: 'easeOutQuart' });
}); function reload_count_down_time_product() { $('.product-item .key').each(function () { $(this).countdown({ until: $(this).attr('rel') - server_time, format: 'HMS', layout: '{hn} : {mn} : {sn}', expiryText: 'hết hạn' }); }); }
function reduce_item_product_same_category(product_id, reject_ids) {
    var height_main = $(".cm-products-view .span9").height(); var height_product = $(".cm-products-view .span3 .product-sidebar").height(); var height_mid_product = $(".cm-products-view .span3 .product-sidebar .product-row-item-same-txt").height() - 20; var number_product = $(".product-row-item-same-txt").length; if (height_main < height_product) { var number_element_reject = parseInt((height_product - height_main) / height_mid_product); for (var i = number_product; i >= number_product - number_element_reject; i--) { $(".cm-products-view .span3 .product-sidebar .product-row-item-same-txt").eq(i).remove(); } }
    else { var number_element_add = parseInt((height_main - height_product) / height_mid_product); $.ajax({ url: fn_url(index_script + "?dispatch=products.get_more_product_same_category"), type: 'get', data: { product_id: product_id, reject_ids: reject_ids, number_element_add: number_element_add }, beforeSend: function () { }, success: function (response) { $(".cm-products-view .span3 .product-sidebar").append(response); } }); }
}
function fn_align_notificate_message() { var notificates = $(".notification-content"); if ($(notificates).length > 1) { var margin_top = 0; $(notificates).each(function (index, element) { if (index >= 1) { margin_top = margin_top + $(notificates).eq(index - 1).height(); $(this).css("margin-top", margin_top + "px"); } }); } }
function fn_form_post_step_three_block_cart_form() { reload_design_order_page(); }
function slider_nav_margin() { var slider_navigator = $(".product-main-info-short .navigator-content"); slider_navigator.css({ left: "50%", right: "auto", marginLeft: "-" + (slider_navigator.width() / 2) + 'px' }); $(".lof-slidecontent").css("overflow", "visible"); }
function reload_design_order_page() { $int_height_total_info = $(".order_total_info").height() + 2; $(".pay_comment_info").css("height", $int_height_total_info); $(".pay_comment_info .checkout-textarea").css("min-height", $int_height_total_info - 55); $int_height_shipping_method = $(".order_shipping_method").height(); $("#step_two_body .step-complete-wrapper .overflow-hidden").css("min-height", $int_height_shipping_method - 60); }
(function ($) {
    $.fn.UItoTop = function (options) {
        var defaults = { text: 'To Top', min: 200, inDelay: 600, outDelay: 400, containerID: 'toTop', containerHoverID: 'toTopHover', scrollSpeed: 1200, easingType: 'linear' }; var settings = $.extend(defaults, options); var containerIDhash = '#' + settings.containerID; var containerHoverIDHash = '#' + settings.containerHoverID; $('body').append('<a href="#" id="' + settings.containerID + '">' + settings.text + '</a>'); $(containerIDhash).hide().click(function () { $('html, body').animate({ scrollTop: 0 }, settings.scrollSpeed, settings.easingType); $('#' + settings.containerHoverID, this).stop().animate({ 'opacity': 0 }, settings.inDelay, settings.easingType); return false; }).prepend('<span id="' + settings.containerHoverID + '"></span>').hover(function () { $(containerHoverIDHash, this).stop().animate({ 'opacity': 1 }, 600, 'linear'); }, function () { $(containerHoverIDHash, this).stop().animate({ 'opacity': 0 }, 700, 'linear'); }); $(window).scroll(function () {
            var sd = $(window).scrollTop(); if (typeof document.body.style.maxHeight === "undefined") { $(containerIDhash).css({ 'position': 'absolute', 'top': $(window).scrollTop() + $(window).height() - 50 }); }
            if (sd > settings.min)
                $(containerIDhash).fadeIn(settings.inDelay); else
                $(containerIDhash).fadeOut(settings.Outdelay);
        });
    };
})(jQuery); function clear_space(id) { var field = document.getElementById(id); var field_value = field.value; field_value = $.trim(field_value); field.value = field_value; }
function reload_coutdown_travel() { window.location.reload(); }
$(function () { var auto_deals = [{ value: 'Buffet', count: '10' }, { value: 'Buffet 1', count: '12' }]; $(".search-textbox").autocomplete({ source: function (request, response) { $.ajax({ url: fn_url(index_script + "?dispatch=search.autocomplete"), dataType: "json", data: { s: $(".search-textbox").val() }, success: function (data) { response(json_decode(data.text)); } }); }, focus: function (event, ui) { $(".search-textbox").val($(".ui-state-hover .search-label").html()); return false; }, select: function (event, ui) { $(".search-textbox").val($(".ui-state-hover .search-label").html()); return false; } }); $.each($(".search-textbox"), function (index, item) { $(item).data("autocomplete")._renderItem = function (ul, item) { return $("<li>").append("<a><span class='search-label'>" + item.value + "</span><span class=\"search-count\">" + item.count + " kết quả</span></a>").appendTo(ul); }; }); }); function json_decode(str_json) {
    var json = this.window.JSON; if (typeof json === 'object' && typeof json.parse === 'function') {
        try { return json.parse(str_json); } catch (err) {
            if (!(err instanceof SyntaxError)) { throw new Error('Unexpected error type in json_decode()'); }
            this.php_js = this.php_js || {}; this.php_js.last_error_json = 4; return null;
        }
    }
    var cx = /[\u0000\u00ad\u0600-\u0604\u070f\u17b4\u17b5\u200c-\u200f\u2028-\u202f\u2060-\u206f\ufeff\ufff0-\uffff]/g; var j; var text = str_json; cx.lastIndex = 0; if (cx.test(text)) { text = text.replace(cx, function (a) { return '\\u' + ('0000' + a.charCodeAt(0).toString(16)).slice(-4); }); }
    if ((/^[\],:{}\s]*$/).test(text.replace(/\\(?:["\\\/bfnrt]|u[0-9a-fA-F]{4})/g, '@').replace(/"[^"\\\n\r]*"|true|false|null|-?\d+(?:\.\d*)?(?:[eE][+\-]?\d+)?/g, ']').replace(/(?:^|:|,)(?:\s*\[)+/g, ''))) { j = eval('(' + text + ')'); return j; }
    this.php_js = this.php_js || {}; this.php_js.last_error_json = 4; return null;
}
$(document).ready(function () { $(".login-social").click(function () { login_social_new_window($(this).attr('href')); return false; }); }); function login_social_new_window(social)
{ window.location.href = '/?dispatch=auth.socialconnect&social=' + social; return; var height = 500; var width = 800; social_new_window = window.open('/?dispatch=auth.socialconnect&social=' + social, 'hotdeal.vn-connect-social', 'location=yes,status=yes,resizable=true,width=' + width + ',height=' + height); social_new_window.window.focus(); popupMonitor = window.setTimeout(checkPopup, 500); }
function checkPopup() {
    if (false == social_new_window.closed)
    { social_new_window.window.focus(); popupMonitor = window.setTimeout(checkPopup, 500); }
    else
    { window.clearInterval(); }
}
function check_popup_close() { this.window.location.reload(); }
$(document).ready(function () { $(".cm-dialog-opener").click(function () { var height = 500; var width = 800; view_maps = window.open($(this).attr('href'), 'view-maps', 'location=yes,status=yes,resizable=true,width=' + width + ',height=' + height); view_maps.window.focus(); return false; }); }); jQuery.extend(jQuery.easing, {
    easeInQuad: function (x, t, b, c, d) { return c * (t /= d) * t + b; }, easeOutQuad: function (x, t, b, c, d) { return -c * (t /= d) * (t - 2) + b; }, easeInOutQuad: function (x, t, b, c, d) { if ((t /= d / 2) < 1) return c / 2 * t * t + b; return -c / 2 * ((--t) * (t - 2) - 1) + b; }, easeInCubic: function (x, t, b, c, d) { return c * (t /= d) * t * t + b; }, easeOutCubic: function (x, t, b, c, d) { return c * ((t = t / d - 1) * t * t + 1) + b; }, easeInOutCubic: function (x, t, b, c, d) { if ((t /= d / 2) < 1) return c / 2 * t * t * t + b; return c / 2 * ((t -= 2) * t * t + 2) + b; }, easeInQuart: function (x, t, b, c, d) { return c * (t /= d) * t * t * t + b; }, easeOutQuart: function (x, t, b, c, d) { return -c * ((t = t / d - 1) * t * t * t - 1) + b; }, easeInOutQuart: function (x, t, b, c, d) { if ((t /= d / 2) < 1) return c / 2 * t * t * t * t + b; return -c / 2 * ((t -= 2) * t * t * t - 2) + b; }, easeInQuint: function (x, t, b, c, d) { return c * (t /= d) * t * t * t * t + b; }, easeOutQuint: function (x, t, b, c, d) { return c * ((t = t / d - 1) * t * t * t * t + 1) + b; }, easeInOutQuint: function (x, t, b, c, d) { if ((t /= d / 2) < 1) return c / 2 * t * t * t * t * t + b; return c / 2 * ((t -= 2) * t * t * t * t + 2) + b; }, easeInSine: function (x, t, b, c, d) { return -c * Math.cos(t / d * (Math.PI / 2)) + c + b; }, easeOutSine: function (x, t, b, c, d) { return c * Math.sin(t / d * (Math.PI / 2)) + b; }, easeInOutSine: function (x, t, b, c, d) { return -c / 2 * (Math.cos(Math.PI * t / d) - 1) + b; }, easeInExpo: function (x, t, b, c, d) { return (t == 0) ? b : c * Math.pow(2, 10 * (t / d - 1)) + b; }, easeOutExpo: function (x, t, b, c, d) { return (t == d) ? b + c : c * (-Math.pow(2, -10 * t / d) + 1) + b; }, easeInOutExpo: function (x, t, b, c, d) { if (t == 0) return b; if (t == d) return b + c; if ((t /= d / 2) < 1) return c / 2 * Math.pow(2, 10 * (t - 1)) + b; return c / 2 * (-Math.pow(2, -10 * --t) + 2) + b; }, easeInCirc: function (x, t, b, c, d) { return -c * (Math.sqrt(1 - (t /= d) * t) - 1) + b; }, easeOutCirc: function (x, t, b, c, d) { return c * Math.sqrt(1 - (t = t / d - 1) * t) + b; }, easeInOutCirc: function (x, t, b, c, d) { if ((t /= d / 2) < 1) return -c / 2 * (Math.sqrt(1 - t * t) - 1) + b; return c / 2 * (Math.sqrt(1 - (t -= 2) * t) + 1) + b; }, easeInElastic: function (x, t, b, c, d) {
        var s = 1.70158; var p = 0; var a = c; if (t == 0) return b; if ((t /= d) == 1) return b + c; if (!p) p = d * .3; if (a < Math.abs(c)) { a = c; var s = p / 4; }
        else var s = p / (2 * Math.PI) * Math.asin(c / a); return -(a * Math.pow(2, 10 * (t -= 1)) * Math.sin((t * d - s) * (2 * Math.PI) / p)) + b;
    }, easeOutElastic: function (x, t, b, c, d) {
        var s = 1.70158; var p = 0; var a = c; if (t == 0) return b; if ((t /= d) == 1) return b + c; if (!p) p = d * .3; if (a < Math.abs(c)) { a = c; var s = p / 4; }
        else var s = p / (2 * Math.PI) * Math.asin(c / a); return a * Math.pow(2, -10 * t) * Math.sin((t * d - s) * (2 * Math.PI) / p) + c + b;
    }, easeInOutElastic: function (x, t, b, c, d) {
        var s = 1.70158; var p = 0; var a = c; if (t == 0) return b; if ((t /= d / 2) == 2) return b + c; if (!p) p = d * (.3 * 1.5); if (a < Math.abs(c)) { a = c; var s = p / 4; }
        else var s = p / (2 * Math.PI) * Math.asin(c / a); if (t < 1) return -.5 * (a * Math.pow(2, 10 * (t -= 1)) * Math.sin((t * d - s) * (2 * Math.PI) / p)) + b; return a * Math.pow(2, -10 * (t -= 1)) * Math.sin((t * d - s) * (2 * Math.PI) / p) * .5 + c + b;
    }, easeInBack: function (x, t, b, c, d, s) { if (s == undefined) s = 1.70158; return c * (t /= d) * t * ((s + 1) * t - s) + b; }, easeOutBack: function (x, t, b, c, d, s) { if (s == undefined) s = 1.70158; return c * ((t = t / d - 1) * t * ((s + 1) * t + s) + 1) + b; }, easeInOutBack: function (x, t, b, c, d, s) { if (s == undefined) s = 1.70158; if ((t /= d / 2) < 1) return c / 2 * (t * t * (((s *= (1.525)) + 1) * t - s)) + b; return c / 2 * ((t -= 2) * t * (((s *= (1.525)) + 1) * t + s) + 2) + b; }, easeInBounce: function (x, t, b, c, d) { return c - jQuery.easing.easeOutBounce(x, d - t, 0, c, d) + b; }, easeOutBounce: function (x, t, b, c, d) { if ((t /= d) < (1 / 2.75)) { return c * (7.5625 * t * t) + b; } else if (t < (2 / 2.75)) { return c * (7.5625 * (t -= (1.5 / 2.75)) * t + .75) + b; } else if (t < (2.5 / 2.75)) { return c * (7.5625 * (t -= (2.25 / 2.75)) * t + .9375) + b; } else { return c * (7.5625 * (t -= (2.625 / 2.75)) * t + .984375) + b; } }, easeInOutBounce: function (x, t, b, c, d) { if (t < d / 2) return jQuery.easing.easeInBounce(x, t * 2, 0, c, d) * .5 + b; return jQuery.easing.easeOutBounce(x, t * 2 - d, 0, c, d) * .5 + c * .5 + b; }
}); (function ($) {
    function fixTitle($ele) { if ($ele.attr('title') || typeof ($ele.attr('original-title')) != 'string') { $ele.attr('original-title', $ele.attr('title') || '').removeAttr('title'); } }
    function Tipsy(element, options) { this.$element = $(element); this.options = options; this.enabled = true; fixTitle(this.$element); }
    Tipsy.prototype = {
        show: function () {
            var title = this.getTitle(); if (title && this.enabled) {
                var $tip = this.tip(); $tip.find('.tipsy-inner')[this.options.html ? 'html' : 'text'](title); $tip[0].className = 'tipsy'; $tip.remove().css({ top: 0, left: 0, visibility: 'hidden', display: 'block' }).appendTo(document.body); var pos = $.extend({}, this.$element.offset(), { width: this.$element[0].offsetWidth, height: this.$element[0].offsetHeight }); var actualWidth = $tip[0].offsetWidth, actualHeight = $tip[0].offsetHeight; var gravity = (typeof this.options.gravity == 'function') ? this.options.gravity.call(this.$element[0]) : this.options.gravity; var tp; switch (gravity.charAt(0)) { case 'n': tp = { top: pos.top + pos.height + this.options.offset, left: pos.left + pos.width / 2 - actualWidth / 2 }; break; case 's': tp = { top: pos.top - actualHeight - this.options.offset, left: pos.left + pos.width / 2 - actualWidth / 2 }; break; case 'e': tp = { top: pos.top + pos.height / 2 - actualHeight / 2, left: pos.left - actualWidth - this.options.offset }; break; case 'w': tp = { top: pos.top + pos.height / 2 - actualHeight / 2, left: pos.left + pos.width + this.options.offset }; break; }
                if (gravity.length == 2) { if (gravity.charAt(1) == 'w') { tp.left = pos.left + pos.width / 2 - 15; } else { tp.left = pos.left + pos.width / 2 - actualWidth + 15; } }
                $tip.css(tp).addClass('tipsy-' + gravity); if (this.options.fade) { $tip.stop().css({ opacity: 0, display: 'block', visibility: 'visible' }).animate({ opacity: this.options.opacity }); } else { $tip.css({ visibility: 'visible', opacity: this.options.opacity }); }
            }
        }, hide: function () { if (this.options.fade) { this.tip().stop().fadeOut(function () { $(this).remove(); }); } else { this.tip().remove(); } }, getTitle: function () {
            var title, $e = this.$element, o = this.options; fixTitle($e); var title, o = this.options; if (typeof o.title == 'string') { title = $e.attr(o.title == 'title' ? 'original-title' : o.title); } else if (typeof o.title == 'function') { title = o.title.call($e[0]); }
            title = ('' + title).replace(/(^\s*|\s*$)/, ""); return title || o.fallback;
        }, tip: function () {
            if (!this.$tip) { this.$tip = $('<div class="tipsy"></div>').html('<div class="tipsy-arrow"></div><div class="tipsy-inner"/></div>'); }
            return this.$tip;
        }, validate: function () { if (!this.$element[0].parentNode) { this.hide(); this.$element = null; this.options = null; } }, enable: function () { this.enabled = true; }, disable: function () { this.enabled = false; }, toggleEnabled: function () { this.enabled = !this.enabled; }
    }; $.fn.tipsy = function (options) {
        if (options === true) { return this.data('tipsy'); } else if (typeof options == 'string') { return this.data('tipsy')[options](); }
        options = $.extend({}, $.fn.tipsy.defaults, options); function get(ele) {
            var tipsy = $.data(ele, 'tipsy'); if (!tipsy) { tipsy = new Tipsy(ele, $.fn.tipsy.elementOptions(ele, options)); $.data(ele, 'tipsy', tipsy); }
            return tipsy;
        }
        function enter() { var tipsy = get(this); tipsy.hoverState = 'in'; if (options.delayIn == 0) { tipsy.show(); } else { setTimeout(function () { if (tipsy.hoverState == 'in') tipsy.show(); }, options.delayIn); } }; function leave() { var tipsy = get(this); tipsy.hoverState = 'out'; if (options.delayOut == 0) { tipsy.hide(); } else { setTimeout(function () { if (tipsy.hoverState == 'out') tipsy.hide(); }, options.delayOut); } }; if (!options.live) this.each(function () { get(this); }); if (options.trigger != 'manual') { var binder = options.live ? 'live' : 'bind', eventIn = options.trigger == 'hover' ? 'mouseenter' : 'focus', eventOut = options.trigger == 'hover' ? 'mouseleave' : 'blur'; this[binder](eventIn, enter)[binder](eventOut, leave); }
        return this;
    }; $.fn.tipsy.defaults = { delayIn: 0, delayOut: 0, fade: false, fallback: '', gravity: 'n', html: false, live: false, offset: 0, opacity: 0.8, title: 'title', trigger: 'hover' }; $.fn.tipsy.elementOptions = function (ele, options) { return $.metadata ? $.extend({}, options, $(ele).metadata()) : options; }; $.fn.tipsy.autoNS = function () { return $(this).offset().top > ($(document).scrollTop() + $(window).height() / 2) ? 's' : 'n'; }; $.fn.tipsy.autoWE = function () { return $(this).offset().left > ($(document).scrollLeft() + $(window).width() / 2) ? 'e' : 'w'; };
})(jQuery); document.write('<style>.cm-noscript { display:none }</style>');