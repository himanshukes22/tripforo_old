<%@ Control Language="VB" AutoEventWireup="true" CodeFile="FltSearchFixDep.ascx.vb" Inherits="UserControl_FltSearchFixDep" %>

<%@ Register Src="~/UserControl/HotelSearch.ascx" TagPrefix="uc1" TagName="HotelSearch" %>
<%@ Register Src="~/UserControl/HotelDashboard.ascx" TagPrefix="uc1" TagName="HotelDashboard" %>
<%--<script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>--%>

<link type="text/css" href="<%=ResolveUrl("~/Styles/jquery-ui-1.8.8.custom.css") %>"
    rel="stylesheet" />
<%-- <link href="//netdna.bootstrapcdn.com/bootstrap/3.2.0/css/bootstrap.min.css" rel="stylesheet">--%>
    <link rel="stylesheet" href="//cdnjs.cloudflare.com/ajax/libs/bootstrap-select/1.6.3/css/bootstrap-select.min.css" />

<link href="../Advance_CSS/css/textbox.css" rel="stylesheet" />
<script src='../Scripts/ADDvalidationPax.js?v=1.2' type='text/javascript'></script>
<script src="//code.jquery.com/ui/1.11.4/jquery-ui.js"></script>
<link href="https://cdn.jsdelivr.net/npm/select2@4.0.13/dist/css/select2.min.css" rel="stylesheet" />
<script src="https://cdn.jsdelivr.net/npm/select2@4.0.13/dist/js/select2.min.js"></script>

<style type="text/css">
    option {
        margin-bottom: 5px;
        border-bottom: 1px solid #000;
    }

    .btn .dropdown-toggle .selectpicker .btn-default {
        border-bottom: 1px solid #000;
    border-top: none;
    border-left: none;
    border-right: none;
    }

</style>


<%--<style type="text/css">
    
select {
    display: none !important;
}

.dropdown-select {
    background-image: linear-gradient(to bottom, rgba(255, 255, 255, 0.25) 0%, rgba(255, 255, 255, 0) 100%);
    background-repeat: repeat-x;
    filter: progid:DXImageTransform.Microsoft.gradient(startColorstr='#40FFFFFF', endColorstr='#00FFFFFF', GradientType=0);
    background-color: #fff;
    border-radius: 6px;
    border: solid 1px #eee;
    box-shadow: 0px 2px 5px 0px rgba(155, 155, 155, 0.5);
    box-sizing: border-box;
    cursor: pointer;
    display: block;
    float: left;
    font-size: 14px;
    font-weight: normal;
    height: 42px;
    line-height: 40px;
    outline: none;
    padding-left: 18px;
    padding-right: 30px;
    position: relative;
    text-align: left !important;
    transition: all 0.2s ease-in-out;
    -webkit-user-select: none;
    -moz-user-select: none;
    -ms-user-select: none;
    user-select: none;
    white-space: nowrap;
    width: auto;

}

.dropdown-select:focus {
    background-color: #fff;
}

.dropdown-select:hover {
    background-color: #fff;
}

.dropdown-select:active,
.dropdown-select.open {
    background-color: #fff !important;
    border-color: #bbb;
    box-shadow: 0 1px 4px rgba(0, 0, 0, 0.05) inset;
}

.dropdown-select:after {
    height: 0;
    width: 0;
    border-left: 4px solid transparent;
    border-right: 4px solid transparent;
    border-top: 4px solid #777;
    -webkit-transform: origin(50% 20%);
    transform: origin(50% 20%);
    transition: all 0.125s ease-in-out;
    content: '';
    display: block;
    margin-top: -2px;
    pointer-events: none;
    position: absolute;
    right: 10px;
    top: 50%;
}

.dropdown-select.open:after {
    -webkit-transform: rotate(-180deg);
    transform: rotate(-180deg);
}

.dropdown-select.open .list {
    -webkit-transform: scale(1);
    transform: scale(1);
    opacity: 1;
    pointer-events: auto;
}

.dropdown-select.open .option {
    cursor: pointer;
}

.dropdown-select.wide {
    width: 100%;
}

.dropdown-select.wide .list {
    left: 0 !important;
    right: 0 !important;
}

.dropdown-select .list {
    box-sizing: border-box;
    transition: all 0.15s cubic-bezier(0.25, 0, 0.25, 1.75), opacity 0.1s linear;
    -webkit-transform: scale(0.75);
    transform: scale(0.75);
    -webkit-transform-origin: 50% 0;
    transform-origin: 50% 0;
    box-shadow: 0 0 0 1px rgba(0, 0, 0, 0.09);
    background-color: #fff;
    border-radius: 6px;
    margin-top: 4px;
    padding: 3px 0;
    opacity: 0;
    overflow: hidden;
    pointer-events: none;
    position: absolute;
    top: 100%;
    left: 0;
    z-index: 999;
    max-height: 250px;
    overflow: auto;
    border: 1px solid #ddd;
}

.dropdown-select .list:hover .option:not(:hover) {
    background-color: transparent !important;
}
.dropdown-select .dd-search{
  overflow:hidden;
  display:flex;
  align-items:center;
  justify-content:center;
  margin:0.5rem;
}

.dropdown-select .dd-searchbox{
  width:90%;
  padding:0.5rem;
  border:1px solid #999;
  border-color:#999;
  border-radius:4px;
  outline:none;
}
.dropdown-select .dd-searchbox:focus{
  border-color:#12CBC4;
}

.dropdown-select .list ul {
    padding: 0;
}

.dropdown-select .option {
    cursor: default;
    font-weight: 400;
    line-height: 40px;
    outline: none;
    padding-left: 18px;
    padding-right: 29px;
    text-align: left;
    transition: all 0.2s;
    list-style: none;
}

.dropdown-select .option:hover,
.dropdown-select .option:focus {
    background-color: #f6f6f6 !important;
}

.dropdown-select .option.selected {
    font-weight: 600;
    color: #12cbc4;
}

.dropdown-select .option.selected:focus {
    background: #f6f6f6;
}

.dropdown-select a {
    color: #aaa;
    text-decoration: none;
    transition: all 0.2s ease-in-out;
}

.dropdown-select a:hover {
    color: #666;
}

</style>--%>




<style type="text/css">


    .dropdown {
        
        padding: 20px;
       
        display: flex;
        justify-content: space-around;
        font-size: 1.1rem;
        cursor: pointer;
  
    }

    .fa-angle-down {
        position: relative;
        top: 2px;
        font-size: 1.3rem;
        transition: transform 0.3s ease;
    }

    .rotate-dropdown-arrow {
        transform: rotate(-180deg);
    }

    /*.dropdown-menu {
        display: none;
        flex-direction: column;
        border-radius: 4px;
        margin-top: 8px;
        width: 160px;
        padding: 10px;
        box-shadow: 0 0 5px -1px rgba(0, 0, 0, 0.3);
        background: #fafafa;
        transform-origin: top left;
    }*/

    .dropdown-menu {
        position: relative !important;
    }

        .dropdown-menu span {
            padding: 10px;
            flex-grow: 1;
            width: 100%;
            box-sizing: border-box;
            text-align: center;
            cursor: pointer;
            transition: background 0.3s ease;
        }

            .dropdown-menu span:last-child {
                border: none;
            }

      /*      .dropdown-menu span:hover {
                background: #eee;
            }*/

    #openDropdown:checked + .dropdown-menu {
        display: flex;
        animation: openDropDown 0.4s ease;
    }

    @keyframes openDropDown {
        from {
            transform: rotateX(50deg);
        }

        to {
            transform: rotateX(0deg);
        }
    }
</style>



<style type="text/css">
    .inp {
        width: 30px;
        text-align: center;
        color: #000;
        background: none;
        border: none;
    }

    .main_dv {
        width: 100%;
        float: left;
        margin-bottom: 13px;
    }

    .ttl_col {
        width: 35%;
        float: left;
    }

        .ttl_col span {
            font-size: 10px;
            color: #a3a2a2;
            display: block;
        }

        .ttl_col p {
            font-size: 13px;
            color: #000;
            display: block;
        }

    .dn_btn {
        cursor: pointer;
        background: #ff0000;
        float: right;
        text-align: center;
        padding: 4px 12px;
        display: block;
        color: #fff;
        font-size: 11px;
        border-radius: 3px;
        -webkit-border-radius: 3px;
        -moz-border-radius: 3px;
    }

    .innr_pnl {
        width: 200px;
        position: relative;
        padding: 0px;
    }

    .dropdown-content-n2 {
        /*display:none;*/
        position: absolute;
        background-color: #fff;
        width: 200px;
        padding: 10px;
        box-shadow: 0 0 20px 0 rgba(0,0,0,0.45);
        z-index: 1;
        /*top: 114px;*/
        box-sizing: content-box;
        -webkit-box-sizing: content-box;
        right: 349px
    }

    .innr_pnl::before {
        content: '';
        position: absolute;
        left: 2%;
        top: -15px;
        width: 0;
        height: 0;
        border-left: 5px solid transparent;
        border-right: 5px solid transparent;
        border-bottom: 5px solid #fff;
        clear: both;
    }

    .clear {
        clear: both;
    }
</style>


<style>
    /*the container must be positioned relative:*/
    .custom-select {
        position: relative;
        font-family: Arial;
    }

        .custom-select select {
            display: none; /*hide original SELECT element:*/
        }

  /*  .select-selected {
        background-color: DodgerBlue;
    }
*/
        /*style the arrow inside the select element:*/
        .select-selected:after {
            position: absolute;
            content: "";
            top: 14px;
            right: 10px;
            width: 0;
            height: 0;
            border: 6px solid transparent;
            border-color: #000 transparent transparent transparent;
        }

        /*point the arrow upwards when the select box is open (active):*/
        .select-selected.select-arrow-active:after {
            border-color: transparent transparent #000 transparent;
            top: 7px;
        }

    /*style the items (options), including the selected item:*/
    .select-items div, .select-selected {
        color: #000;
        padding: 8px 16px;
        border: 1px solid transparent;
        border-color: transparent transparent rgba(0, 0, 0, 0.1) transparent;
        cursor: pointer;
        user-select: none;
        font-size:18px;
    }

    /*style items (options):*/
    .select-items {
        position: absolute;
        background-color: #fff;
        top: 100%;
        left: 0;
        right: 0;
        z-index: 99;
        height: 145px;
        overflow: auto;
        box-shadow: 0 0 25px rgb(0 0 0 / 30%);
    }

    /*hide the items when the select box is closed:*/
    .select-hide {
        display: none;
    }

    .select-items div:hover, .same-as-selected {
        background-color: rgba(0, 0, 0, 0.1);
    }
</style>




<%--<script type="text/javascript">
    $(function () { $('.search-select').comboSelect() });


    (function (factory) {
        'use strict';
        if (typeof define === 'function' && define.amd) {
            // AMD. Register as an anonymous module.
            define(['jquery'], factory);
        } else if (typeof exports === 'object' && typeof require === 'function') {
            // Browserify
            factory(require('jquery'));
        } else {
            // Browser globals
            factory(jQuery);
        }
    }(function ($, undefined) {

        var pluginName = "comboSelect",
            dataKey = 'comboselect';
        var defaults = {
            comboClass: 'combo-select',
            comboArrowClass: 'combo-arrow',
            comboDropDownClass: 'combo-dropdown',
            inputClass: 'combo-input text-input',
            disabledClass: 'option-disabled',
            hoverClass: 'option-hover',
            selectedClass: 'option-selected',
            markerClass: 'combo-marker',
            themeClass: '',
            maxHeight: 200,
            extendStyle: true,
            focusInput: true
        };

        /**
         * Utility functions
         */

        var keys = {
            ESC: 27,
            TAB: 9,
            RETURN: 13,
            LEFT: 37,
            UP: 38,
            RIGHT: 39,
            DOWN: 40,
            ENTER: 13,
            SHIFT: 16
        },
            isMobile = (/android|webos|iphone|ipad|ipod|blackberry|iemobile|opera mini/i.test(navigator.userAgent.toLowerCase()));

        /**
         * Constructor
         * @param {[Node]} element [Select element]
         * @param {[Object]} options [Option object]
         */
        function Plugin(element, options) {

            /* Name of the plugin */

            this._name = pluginName;

            /* Reverse lookup */

            this.el = element

            /* Element */

            this.$el = $(element)

            /* If multiple select: stop */

            if (this.$el.prop('multiple')) return;

            /* Settings */

            this.settings = $.extend({}, defaults, options, this.$el.data());

            /* Defaults */

            this._defaults = defaults;

            /* Options */

            this.$options = this.$el.find('option, optgroup')

            /* Initialize */

            this.init();

            /* Instances */

            $.fn[pluginName].instances.push(this);

        }

        $.extend(Plugin.prototype, {
            init: function () {

                /* Construct the comboselect */

                this._construct();


                /* Add event bindings */

                this._events();


            },
            _construct: function () {

                var self = this

                /**
                 * Add negative TabIndex to `select`
                 * Preserves previous tabindex
                 */

                this.$el.data('plugin_' + dataKey + '_tabindex', this.$el.prop('tabindex'))

                /* Add a tab index for desktop browsers */

                !isMobile && this.$el.prop("tabIndex", -1)

                /**
                 * Wrap the Select
                 */

                this.$container = this.$el.wrapAll('<div class="' + this.settings.comboClass + ' ' + this.settings.themeClass + '" />').parent();

                /**
                 * Check if select has a width attribute
                 */
                if (this.settings.extendStyle && this.$el.attr('style')) {

                    this.$container.attr('style', this.$el.attr("style"))

                }


                /**
                 * Append dropdown arrow
                 */

                this.$arrow = $('<div class="' + this.settings.comboArrowClass + '" />').appendTo(this.$container)


                /**
                 * Append dropdown
                 */

                this.$dropdown = $('<ul class="' + this.settings.comboDropDownClass + '" />').appendTo(this.$container)


                /**
                 * Create dropdown options
                 */

                this._build();

                /**
                 * Append Input
                 */

                this.$input = $('<input type="text"' + (isMobile ? 'tabindex="-1"' : '') + ' placeholder="' + this.getPlaceholder() + '" class="' + this.settings.inputClass + '">').appendTo(this.$container)

                /* Update input text */

                this._updateInput()

            },
            getPlaceholder: function () {

                var p = '';

                this.$options.filter(function (idx, opt) {

                    return opt.nodeName == 'OPTION'
                }).each(function (idx, e) {

                    if (e.value == '') p = e.innerHTML
                });

                return p
            },
            _build: function () {

                var self = this;

                var o = '', k = 0;

                this.$options.each(function (i, e) {

                    if (e.nodeName.toLowerCase() == 'optgroup') {

                        return o += '<li class="option-group">' + this.label + '</li>'
                    }

                    o += '<li class="' + (this.disabled ? self.settings.disabledClass : "option-item") + ' ' + (k == self.$el.prop('selectedIndex') ? self.settings.selectedClass : '') + '" data-index="' + (k) + '" data-value="' + this.value + '">' + (this.innerHTML) + '</li>'

                    k++;
                })

                this.$dropdown.html(o)

                /**
                 * Items
                 */

                this.$items = this.$dropdown.children();
            },

            _events: function () {

                /* Input: focus */

                this.$container.on('focus.input', 'input', $.proxy(this._focus, this))

                /**
                 * Input: mouseup
                 * For input select() event to function correctly
                 */
                this.$container.on('mouseup.input', 'input', function (e) {
                    e.preventDefault()
                })

                /* Input: blur */

                this.$container.on('blur.input', 'input', $.proxy(this._blur, this))

                /* Select: change */

                this.$el.on('change.select', $.proxy(this._change, this))

                /* Select: focus */

                this.$el.on('focus.select', $.proxy(this._focus, this))

                /* Select: blur */

                this.$el.on('blur.select', $.proxy(this._blurSelect, this))

                /* Dropdown Arrow: click */

                this.$container.on('click.arrow', '.' + this.settings.comboArrowClass, $.proxy(this._toggle, this))

                /* Dropdown: close */

                this.$container.on('comboselect:close', $.proxy(this._close, this))

                /* Dropdown: open */

                this.$container.on('comboselect:open', $.proxy(this._open, this))

                /* Dropdown: update */

                this.$container.on('comboselect:update', $.proxy(this._update, this));


                /* HTML Click */

                $('html').off('click.comboselect').on('click.comboselect', function () {

                    $.each($.fn[pluginName].instances, function (i, plugin) {

                        plugin.$container.trigger('comboselect:close')

                    })
                });

                /* Stop `event:click` bubbling */

                this.$container.on('click.comboselect', function (e) {
                    e.stopPropagation();
                })


                /* Input: keydown */

                this.$container.on('keydown', 'input', $.proxy(this._keydown, this))

                /* Input: keyup */

                this.$container.on('keyup', 'input', $.proxy(this._keyup, this))

                /* Dropdown item: click */

                this.$container.on('click.item', '.option-item', $.proxy(this._select, this))

            },

            _keydown: function (event) {



                switch (event.which) {

                    case keys.UP:
                        this._move('up', event)
                        break;

                    case keys.DOWN:
                        this._move('down', event)
                        break;

                    case keys.TAB:
                        this._enter(event)
                        break;

                    case keys.RIGHT:
                        this._autofill(event);
                        break;

                    case keys.ENTER:
                        this._enter(event);
                        break;

                    default:
                        break;


                }

            },


            _keyup: function (event) {

                switch (event.which) {
                    case keys.ESC:
                        this.$container.trigger('comboselect:close')
                        break;

                    case keys.ENTER:
                    case keys.UP:
                    case keys.DOWN:
                    case keys.LEFT:
                    case keys.RIGHT:
                    case keys.TAB:
                    case keys.SHIFT:
                        break;

                    default:
                        this._filter(event.target.value)
                        break;
                }
            },

            _enter: function (event) {

                var item = this._getHovered()

                item.length && this._select(item);

                /* Check if it enter key */
                if (event && event.which == keys.ENTER) {

                    if (!item.length) {

                        /* Check if its illegal value */

                        this._blur();

                        return true;
                    }

                    event.preventDefault();
                }


            },
            _move: function (dir) {

                var items = this._getVisible(),
                    current = this._getHovered(),
                    index = current.prevAll('.option-item').filter(':visible').length,
                    total = items.length


                switch (dir) {
                    case 'up':
                        index--;
                        (index < 0) && (index = (total - 1));
                        break;

                    case 'down':
                        index++;
                        (index >= total) && (index = 0);
                        break;
                }


                items
                    .removeClass(this.settings.hoverClass)
                    .eq(index)
                    .addClass(this.settings.hoverClass)


                if (!this.opened) this.$container.trigger('comboselect:open');

                this._fixScroll()
            },

            _select: function (event) {

                var item = event.currentTarget ? $(event.currentTarget) : $(event);

                if (!item.length) return;

                /**
                 * 1. get Index
                 */

                var index = item.data('index');

                this._selectByIndex(index);

                //this.$container.trigger('comboselect:close')

                this.$input.focus();

                this.$container.trigger('comboselect:close');

            },

            _selectByIndex: function (index) {

                /**
                 * Set selected index and trigger change
                 * @type {[type]}
                 */
                if (typeof index == 'undefined') {

                    index = 0

                }

                if (this.$el.prop('selectedIndex') != index) {

                    this.$el.prop('selectedIndex', index).trigger('change');
                }

            },

            _autofill: function () {

                var item = this._getHovered();

                if (item.length) {

                    var index = item.data('index')

                    this._selectByIndex(index)

                }

            },


            _filter: function (search) {

                var self = this,
                    items = this._getAll();
                needle = $.trim(search).toLowerCase(),
                    reEscape = new RegExp('(\\' + ['/', '.', '*', '+', '?', '|', '(', ')', '[', ']', '{', '}', '\\'].join('|\\') + ')', 'g'),
                    pattern = '(' + search.replace(reEscape, '\\$1') + ')';


                /**
                 * Unwrap all markers
                 */

                $('.' + self.settings.markerClass, items).contents().unwrap();

                /* Search */

                if (needle) {

                    /* Hide Disabled and optgroups */

                    this.$items.filter('.option-group, .option-disabled').hide();


                    items
                        .hide()
                        .filter(function () {

                            var $this = $(this),
                                text = $.trim($this.text()).toLowerCase();

                            /* Found */
                            if (text.toString().indexOf(needle) != -1) {

                                /**
                                 * Wrap the selection
                                 */

                                $this
                                    .html(function (index, oldhtml) {
                                        return oldhtml.replace(new RegExp(pattern, 'gi'), '<span class="' + self.settings.markerClass + '">$1</span>')
                                    })

                                return true
                            }

                        })
                        .show()
                } else {


                    this.$items.show();
                }

                /* Open the comboselect */

                this.$container.trigger('comboselect:open')


            },

            _highlight: function () {

                /*
                1. Check if there is a selected item
                2. Add hover class to it
                3. If not add hover class to first item
                */

                var visible = this._getVisible().removeClass(this.settings.hoverClass),
                    $selected = visible.filter('.' + this.settings.selectedClass)

                if ($selected.length) {

                    $selected.addClass(this.settings.hoverClass);

                } else {

                    visible
                        .removeClass(this.settings.hoverClass)
                        .first()
                        .addClass(this.settings.hoverClass)
                }

            },

            _updateInput: function () {

                var selected = this.$el.prop('selectedIndex')

                if (this.$el.val()) {

                    text = this.$el.find('option').eq(selected).text()

                    this.$input.val(text)

                } else {

                    this.$input.val('')

                }

                return this._getAll()
                    .removeClass(this.settings.selectedClass)
                    .filter(function () {

                        return $(this).data('index') == selected
                    })
                    .addClass(this.settings.selectedClass)

            },
            _blurSelect: function () {

                this.$container.removeClass('combo-focus');

            },
            _focus: function (event) {

                /* Toggle focus class */

                this.$container.toggleClass('combo-focus', !this.opened);

                /* If mobile: stop */

                if (isMobile) return;

                /* Open combo */

                if (!this.opened) this.$container.trigger('comboselect:open');

                /* Select the input */

                this.settings.focusInput && event && event.currentTarget && event.currentTarget.nodeName == 'INPUT' && event.currentTarget.select()
            },

            _blur: function () {

                /**
                 * 1. Get hovered item
                 * 2. If not check if input value == select option
                 * 3. If none
                 */

                var val = $.trim(this.$input.val().toLowerCase()),
                    isNumber = !isNaN(val);

                var index = this.$options.filter(function () {
                    return this.nodeName == 'OPTION'
                }).filter(function () {
                    var _text = this.innerText || this.textContent
                    if (isNumber) {
                        return parseInt($.trim(_text).toLowerCase()) == val
                    }

                    return $.trim(_text).toLowerCase() == val

                }).prop('index')

                /* Select by Index */

                this._selectByIndex(index)

            },

            _change: function () {


                this._updateInput();

            },

            _getAll: function () {

                return this.$items.filter('.option-item')

            },
            _getVisible: function () {

                return this.$items.filter('.option-item').filter(':visible')

            },

            _getHovered: function () {

                return this._getVisible().filter('.' + this.settings.hoverClass);

            },

            _open: function () {

                var self = this

                this.$container.addClass('combo-open')

                this.opened = true

                /* Focus input field */

                this.settings.focusInput && setTimeout(function () { !self.$input.is(':focus') && self.$input.focus(); });

                /* Highligh the items */

                this._highlight()

                /* Fix scroll */

                this._fixScroll()

                /* Close all others */


                $.each($.fn[pluginName].instances, function (i, plugin) {

                    if (plugin != self && plugin.opened) plugin.$container.trigger('comboselect:close')
                })

            },

            _toggle: function () {

                this.opened ? this._close.call(this) : this._open.call(this)
            },

            _close: function () {

                this.$container.removeClass('combo-open combo-focus')

                this.$container.trigger('comboselect:closed')

                this.opened = false

                /* Show all items */

                this.$items.show();

            },
            _fixScroll: function () {

                /**
                 * If dropdown is hidden
                 */
                if (this.$dropdown.is(':hidden')) return;


                /**
                 * Else
                 */
                var item = this._getHovered();

                if (!item.length) return;

                /**
                 * Scroll
                 */

                var offsetTop,
                    upperBound,
                    lowerBound,
                    heightDelta = item.outerHeight()

                offsetTop = item[0].offsetTop;

                upperBound = this.$dropdown.scrollTop();

                lowerBound = upperBound + this.settings.maxHeight - heightDelta;

                if (offsetTop < upperBound) {

                    this.$dropdown.scrollTop(offsetTop);

                } else if (offsetTop > lowerBound) {

                    this.$dropdown.scrollTop(offsetTop - this.settings.maxHeight + heightDelta);
                }

            },
            /**
             * Update API
             */

            _update: function () {

                this.$options = this.$el.find('option, optgroup')

                this.$dropdown.empty();

                this._build();
            },

            /**
             * Destroy API
             */

            dispose: function () {

                /* Remove combo arrow, input, dropdown */

                this.$arrow.remove()

                this.$input.remove()

                this.$dropdown.remove()

                /* Remove tabindex property */
                this.$el
                    .removeAttr("tabindex")

                /* Check if there is a tabindex set before */

                if (!!this.$el.data('plugin_' + dataKey + '_tabindex')) {
                    this.$el.prop('tabindex', this.$el.data('plugin_' + dataKey + '_tabindex'))
                }

                /* Unwrap */

                this.$el.unwrap()

                /* Remove data */

                this.$el.removeData('plugin_' + dataKey)

                /* Remove tabindex data */

                this.$el.removeData('plugin_' + dataKey + '_tabindex')

                /* Remove change event on select */

                this.$el.off('change.select focus.select blur.select');

            }
        });



        // A really lightweight plugin wrapper around the constructor,
        // preventing against multiple instantiations
        $.fn[pluginName] = function (options, args) {

            this.each(function () {

                var $e = $(this),
                    instance = $e.data('plugin_' + dataKey)

                if (typeof options === 'string') {

                    if (instance && typeof instance[options] === 'function') {
                        instance[options](args);
                    }

                } else {

                    if (instance && instance.dispose) {
                        instance.dispose();
                    }

                    $.data(this, "plugin_" + dataKey, new Plugin(this, options));

                }

            });

            // chain jQuery functions
            return this;
        };

        $.fn[pluginName].instances = [];

    }));



</script>--%>



<div>
    <div class="topwaysF" style="display: none">

        <div class="col-md-1 col-xs-4 nopad text-search">
            <label class="mail active">
                <input type="radio" style="display: none;" name="TripTypeF" value="rdbOneWayF" id="rdbOneWayF" checked="checked" />
                One Way</label>
        </div>
        <div class="col-md-2 col-xs-4 nopad text-search">
            <label class="mail">
                <input type="radio" name="TripTypeF" style="display: none;" value="rdbRoundTripF" id="rdbRoundTripF" />
                Round Trips</label>
        </div>
    </div>
    <script>
        $(document).ready(function () {
            var selector = '.topwaysF div label'; //mk
            $(selector).bind('click', function () {
                $(selector).removeClass('active');
                $(this).addClass('active');
            });

        });
    </script>

</div>


<div class="tab-content _pt-20 fxd">

    <div class="tab-pane active" id="SearchAreaTabs-3">
        <div class="theme-search-area theme-search-area-stacked">
            <div class="theme-search-area-form">

                <div class="row" data-gutter="none">
                    <div class="col-md-12 ">
                        <div class="theme-search-area-section first theme-search-area-section-curved theme-search-area-section-bg-white theme-search-area-section-no-border theme-search-area-section-mr">
                            <div class="theme-search-area-section-inner">
                                <%-- <i class="theme-search-area-section-icon lin lin-location-pin"></i>--%>

                                <div class="tb-container">
                                    <div class="">
                                        <asp:DropDownList ID="Sector" type="text" runat="server" class="selectpicker aumd-tb div" data-show-subtext="true" data-live-search="true">
                                            <asp:ListItem Value="">--Select FDD Sector--</asp:ListItem>
                                        </asp:DropDownList>
                                        <%--<label class="aumd-tb-label" for="Sector">Sector</label>--%>
                                        <span class="validation"></span>
                                    </div>

                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="form-group" style="display: none">
                        <label for="exampleInputEmail1">
                            Search Sector:</label>
                        <div class="input-group">
                            <div class="input-group-addon">
                                <i class="fa fa-map-marker fa-lg yellow"></i>
                            </div>
                            <input type="text" name="txtDepCityFD" class="form-control" placeholder="Enter Your Departure City" value="" onclick="this.value = '';" id="txtDepCityFD" />
                            <input type="hidden" id="hidtxtDepCityFD" name="hidtxtDepCityFD" value="TT" />
                        </div>
                    </div>

                    <div class="form-group" style="display: none">
                        <label for="exampleInputEmail1">
                            Leaving From:</label>
                        <div class="input-group">
                            <div class="input-group-addon">
                                <i class="fa fa-map-marker fa-lg yellow"></i>
                            </div>
                            <input type="text" name="txtDepCity1F" class="form-control" placeholder="Enter Your Departure City" value="New Delhi, India-Indira Gandhi Intl(DEL)" onclick="this.value = '';" id="txtDepCity1F" />
                            <input type="hidden" id="hidtxtDepCity1F" name="hidtxtDepCity1F" value="ZZZ,IN" />
                        </div>
                    </div>
                </div>
                <br />
                <div class="row" data-gutter="none">

                    <div class="col-md-12 " id="TravellerF">
                        <div class="theme-search-area-section theme-search-area-section-curved theme-search-area-section-bg-white theme-search-area-section-no-border theme-search-area-section-mr quantity-selector" data-increment="Passengers">
                            <div class="theme-search-area-section-inner">
                                <div class="tb-container">
                                    <%--  <i class="theme-search-area-section-icon lin lin-people"></i>--%>
                                    <input class="aumd-tb div" id="sapnTotPaxF" placeholder=" Traveller" type="text" />
                                    <label class="aumd-tb-label" for="sapnTotPaxF">Traveller(s)</label>
                                    <span class="validation"></span>
                                </div>
                            </div>
                        </div>
                        <div id="boxF" style="display: none;">
                            <div id="div_Adult_Child_Infant" class="myText">
                                <div class="innr_pnl dflex">
                                    <div class="main_dv pax-limit">
                                        <label>
                                            <span>Adult</span>

                                        </label>
                                        <div class="number">
                                            <span class="minusF">-</span>
                                            <input type="text" class="inp" value="1" min="1" name="Adult" id="AdultF" />
                                            <span class="plusF">+</span>
                                        </div>

                                    </div>
                                    <div class="main_dv pax-limit">
                                        <label>
                                            <span>Child<span class="light-grey">(2+ 12 yrs)</span></span>

                                        </label>

                                        <div class="number">
                                            <span class="minusF">-</span>
                                            <input type="text" class="inp" value="0" min="0" name="Child" id="ChildF" />
                                            <span class="plusF">+</span>
                                        </div>

                                    </div>
                                    <div class="main_dv pax-limit">

                                        <label>
                                            <span>Infant <span class="light-grey">(below 2 yrs)</span></span>

                                        </label>

                                        <div class="number">
                                            <span class="minusF">-</span>
                                            <input type="text" class="inp" value="0" min="0" name="Infant" id="InfantF" />
                                            <span class="plusF Infant">+</span>
                                        </div>

                                    </div>

                                </div>
                            </div>
                        </div>

                    </div>



                </div>
                <br />

                <div class="row" data-gutter="none" style="padding: 17px; float: right;">
                    <div class="col-md-6">
                        <button type="button" id="btnOpenCal" value="Search Flight" class="btn btn-danger">Open Calander</button>
                    </div>
                    <div class="col-md-6 hidden">
                        <button type="button" id="btnSearchF" value="Search Flight" class="btn btn-danger">Search Flight</button>
                    </div>
                </div>

                <%--   <div class="row" data-gutter="none">
                    <div class="col-md-12">
                        <input type="checkbox" class="chkNonstop" name="chkNonstop" id="chkNonstop" value="Y" style="display: none" />
                        <button type="button" id="btnSearchF" value="Search" class="theme-search-area-submit _mt-0 theme-search-area-submit-no-border theme-search-area-submit-curved ">Search</button>

                    </div>
                </div>--%>
            </div>
        </div>
    </div>








    <div class="onewayss col-md-5 nopad text-search mltcs" style="display: none">
        <div class="form-group">
            <label for="exampleInputEmail1">
                Going To:</label>
            <div class="input-group">
                <div class="input-group-addon">
                    <i class="fa fa-map-marker fa-lg yellow"></i>
                </div>
                <input type="text" name="txtArrCity1F" onclick="this.value = '';" id="txtArrCity1F" value="New Delhi, India-Indira Gandhi Intl(DEL)" class="form-control" placeholder="Enter Your Destination City" />
                <input type="hidden" id="hidtxtArrCity1F" name="hidtxtArrCity1F" value="ZZZ,IN" />
            </div>
        </div>
    </div>
    <div class="col-md-2 nopad text-search mrgs" id="oneF" style="display: none">
        <div class="form-group">
            <label for="exampleInputEmail1">
                Depart Date:</label>
            <div class="input-group">

                <div class="input-group-addon">
                    <i class="fa fa-calendar yellow"></i>
                </div>
                <input type="text" class="form-control" placeholder="dd/mm/yyyy" name="txtDepDateF" id="txtDepDateF" value=""
                    readonly="readonly" />
                <input type="hidden" name="hidtxtDepDate" id="hidtxtDepDateF" value="" />

            </div>
        </div>
    </div>
    <div class="col-md-2 nopad text-search mrgs" id="ReturnF" style="display: none">
        <div class="form-group" id="trRetDateRowF" style="display: none;">
            <label for="exampleInputEmail1">
                Return Date:</label>
            <div class="input-group">
                <div class="input-group-addon">
                    <i class="fa fa-calendar yellow"></i>
                </div>
                <input type="text" placeholder="dd/mm/yyyy" name="txtRetDateF" id="txtRetDateF" class=" form-control" value=""
                    readonly="readonly" />
                <input type="hidden" name="hidtxtRetDateF" id="hidtxtRetDateF" value="" />


            </div>
        </div>
    </div>
    <div style="display: none;" id="twoF">
        <div class="onewayss col-md-4 nopad text-search mltcs" id="DivDepCity2F">
            <div class="form-group">
                <div class="input-group">
                    <div class="input-group-addon">
                        <i class="fa fa-map-marker fa-lg yellow"></i>
                    </div>
                    <input type="text" name="txtDepCity2F" class="form-control" placeholder="Enter Your Departure City" id="txtDepCity2F" />
                    <input type="hidden" id="hidtxtDepCity2F" name="hidtxtDepCity2F" value="" />
                </div>
            </div>
        </div>

        <div class="onewayss col-md-4 nopad text-search mltcs">
            <div class="form-group">
                <div class="input-group">
                    <div class="input-group-addon">
                        <i class="fa fa-map-marker fa-lg yellow"></i>
                    </div>
                    <input type="text" name="txtArrCity2F" class="form-control" placeholder="Enter Your Destination City" id="txtArrCity2F" />
                    <input type="hidden" id="hidtxtArrCity2F" name="hidtxtArrCity2F" value="" />
                </div>
            </div>
        </div>
        <div class="col-md-2 nopad text-search mrgs" id="DivArrCity2F">
            <div class="form-group">
                <div class="input-group">
                    <div class="input-group-addon">
                        <i class="fa fa-calendar yellow"></i>
                    </div>
                    <input type="text" name="txtDepDate2" id="txtDepDate2F" class=" form-control" placeholder="dd/mm/yyyy" readonly="readonly" value="" />
                    <input type="hidden" name="hidtxtDepDate2F" id="hidtxtDepDate2F" value="" />
                </div>
            </div>
        </div>
    </div>

    <div style="display: none;" id="threeF">

        <div class="onewayss col-md-4 nopad text-search mltcs" id="DivDepCity3F">
            <div class="form-group">

                <div class="input-group">
                    <div class="input-group-addon">
                        <i class="fa fa-map-marker fa-lg yellow"></i>
                    </div>
                    <input type="text" name="txtDepCity3F" class="form-control" placeholder="Enter Your Departure City" id="txtDepCity3F" />
                    <input type="hidden" id="hidtxtDepCity3F" name="hidtxtDepCity3F" value="" />
                </div>
            </div>
        </div>
        <div class="onewayss col-md-4 nopad text-search mltcs">
            <div class="form-group">

                <div class="input-group">
                    <div class="input-group-addon">
                        <i class="fa fa-map-marker fa-lg yellow"></i>
                    </div>
                    <input type="text" name="txtArrCity3F" class="form-control" placeholder="Enter Your Destination City" id="txtArrCity3F" />
                    <input type="hidden" id="hidtxtArrCity3F" name="hidtxtArrCity3F" value="" />
                </div>
            </div>
        </div>
        <div class="col-md-2 nopad text-search mrgs" id="DivArrCity3F">
            <div class="form-group">

                <div class="input-group">

                    <div class="input-group-addon">
                        <i class="fa fa-calendar yellow"></i>
                    </div>
                    <input type="text" name="txtDepDate3F" id="txtDepDate3F" class="form-control" placeholder="dd/mm/yyyy" readonly="readonly" />
                    <input type="hidden" name="hidtxtDepDate3F" id="hidtxtDepDate3F" value="" />
                </div>
            </div>
        </div>
    </div>

    <div style="display: none;" id="four">
        <div class="onewayss col-md-4 nopad text-search mltcs" id="DivDepCity4F">
            <div class="form-group">

                <div class="input-group">
                    <div class="input-group-addon">
                        <i class="fa fa-map-marker fa-lg yellow"></i>
                    </div>
                    <input type="text" name="txtDepCity4F" class="form-control" placeholder="Enter Your Departure City" id="txtDepCity4F" />
                    <input type="hidden" id="hidtxtDepCity4F" name="hidtxtDepCity4F" value="" />
                </div>
            </div>
        </div>
        <div class="onewayss col-md-4 nopad text-search mltcs">
            <div class="form-group">

                <div class="input-group">
                    <div class="input-group-addon">
                        <i class="fa fa-map-marker fa-lg yellow"></i>
                    </div>
                    <input type="text" name="txtArrCity4F" class="form-control" placeholder="Enter Your Destination City" id="txtArrCity4F" />
                    <input type="hidden" id="hidtxtArrCity4F" name="hidtxtArrCity4F" value="" />
                </div>
            </div>
        </div>
        <div class="col-md-2 nopad text-search mrgs" id="DivArrCity4F">
            <div class="form-group">
                <div class="input-group">
                    <div class="input-group-addon">
                        <i class="fa fa-calendar yellow"></i>
                    </div>
                    <input type="text" name="txtDepDate4F" id="txtDepDate4F" class="form-control" placeholder="dd/mm/yyyy" readonly="readonly" />
                    <input type="hidden" name="hidtxtDepDate4F" id="hidtxtDepDate4F" value="" />
                </div>
            </div>
        </div>
    </div>

    <div style="display: none;" id="five">
        <div class="onewayss col-md-4 nopad text-search mltcs" id="DivDepCity5">
            <div class="form-group">
                <div class="input-group">
                    <div class="input-group-addon">
                        <i class="fa fa-map-marker fa-lg yellow"></i>
                    </div>
                    <input type="text" name="txtDepCity5F" class="form-control" placeholder="Enter Your Departure City" id="txtDepCity5F" />
                    <input type="hidden" id="hidtxtDepCity5F" name="hidtxtDepCity5F" value="" />
                </div>
            </div>
        </div>
        <div class="onewayss col-md-4 nopad text-search mltcs">
            <div class="form-group">
                <div class="input-group">
                    <div class="input-group-addon">
                        <i class="fa fa-map-marker fa-lg yellow"></i>
                    </div>
                    <input type="text" name="txtArrCity5F" class="form-control" placeholder="Enter Your Destination City" onclick="this.value = '';" id="txtArrCity5F" />
                    <input type="hidden" id="hidtxtArrCity5F" name="hidtxtArrCity5F" value="" />
                </div>
            </div>
        </div>
        <div class="col-md-2 nopad text-search mrgs" id="DivArrCity5F">
            <div class="form-group">
                <div class="input-group">
                    <div class="input-group-addon">
                        <i class="fa fa-calendar yellow"></i>
                    </div>
                    <input type="text" name="txtDepDate5" id="txtDepDate5F" class="form-control" placeholder="dd/mm/yyyy" readonly="readonly" />
                    <input type="hidden" name="hidtxtDepDate5F" id="hidtxtDepDate5F" value="" />
                </div>
            </div>
        </div>
    </div>

    <div style="display: none;" id="six">
        <div class="onewayss col-md-4 nopad text-search mltcs" id="DivDepCity6F">
            <div class="form-group">
                <div class="input-group">
                    <div class="input-group-addon">
                        <i class="fa fa-map-marker fa-lg yellow"></i>
                    </div>
                    <input type="text" name="txtDepCity6F" class="form-control" placeholder="Enter Your Departure City" id="txtDepCity6F" />
                    <input type="hidden" id="hidtxtDepCity6F" name="hidtxtDepCity6" value="" />
                </div>
            </div>
        </div>

        <div class="onewayss col-md-4 nopad text-search mltcs">
            <div class="form-group">
                <div class="input-group">
                    <div class="input-group-addon">
                        <i class="fa fa-map-marker fa-lg yellow"></i>
                    </div>
                    <input type="text" name="txtArrCity6F" class="form-control" placeholder="Enter Your Destination City" onclick="this.value = '';" id="txtArrCity6F" />
                    <input type="hidden" id="hidtxtArrCity6F" name="hidtxtArrCity6F" value="" />
                </div>
            </div>
        </div>
        <div class="col-md-2 nopad text-search mrgs" id="ArrCity6F">
            <div class="form-group">
                <div class="input-group">
                    <div class="input-group-addon">
                        <i class="fa fa-calendar yellow"></i>
                    </div>
                    <input type="text" name="txtDepDate6F" id="txtDepDate6F" class="form-control" placeholder="dd/mm/yyyy" readonly="readonly" />
                    <input type="hidden" name="hidtxtDepDate6F" id="hidtxtDepDate6F" value="" />
                </div>
            </div>
        </div>
    </div>

    <div class="clear"></div>

    <div class="row col-md-5 col-xs-12 pull-right" id="addF" style="display: none">
        <div class="col-md-4 col-xs-4 text-search text-right">
            <a id="plusF" class="pulse text-search">Add City</a>
        </div>
        <div class="col-md-4 col-xs-4 text-search  text-right">
            <a id="minusF" class="pulse text-search">Remove City</a>
        </div>
    </div>


    <div class="row" style="display: none">
        <div class="text-search col-md-5 col-xs-12" style="padding-bottom: 10px; cursor: pointer;" id="advtravelF">Advanced options <i class="fa fa-chevron-right" aria-hidden="true"></i></div>
        <div class="col-md-12 advopt" id="advtravelssF" style="display: none; overflow: auto;">
            <div class="col-md-3 nopad text-search">
                <div class="form-group">
                    <label for="exampleInputEmail1">
                        Airlines</label>
                    <input type="text" placeholder="Search By Airlines" class="form-control" name="txtAirline" value="" id="txtAirlineF" />
                    <input type="hidden" id="hidtxtAirlineF" name="hidtxtAirlineF" value="" />

                </div>
            </div>

            <div class="col-md-3 col-xs-12 text-search">
                <div class="form-group">
                    <label for="exampleInputEmail1">
                        Class Type</label>
                    <select name="CabinF" class="form-control" id="CabinF">
                        <option value="" selected="selected">--All--</option>
                        <option value="C">Business</option>
                        <option value="Y">Economy</option>
                        <option value="F">First</option>
                        <option value="W">Premium Economy</option>
                    </select>

                </div>
            </div>
        </div>
    </div>







    <div class="clear1"></div>
    <div class="col-md-3 col-xs-12 text-search" id="trAdvSearchRowF" style="display: none">
        <div class="lft ptop10">
            All Fare Classes
        </div>
        <div class="lft mright10">

            <input type="checkbox" name="chkAdvSearchF" id="chkAdvSearchF" value="True" />
        </div>
        <div class="large-4 medium-4 small-12 columns">
            Gds Round Trip Fares
                                
                                <span class="lft mright10">
                                    <input type="checkbox" name="GDS_RTFF" id="GDS_RTFF" value="True" />
                                </span>
        </div>

        <div class="large-4 medium-4 small-12 columns">
            Lcc Round Trip Fares
                                
                                <span class="lft mright10">
                                    <input type="checkbox" name="LCC_RTFF" id="LCC_RTFF" value="True" />
                                </span>
        </div>

    </div>
</div>


<%--<script type="text/javascript">
    $(document).ready(function () {
        $('.mdb-select').materialSelect();
    });
</script>--%>

<script src="https://ajax.googleapis.com/ajax/libs/jquery/1.11.2/jquery.min.js"></script>
  <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.2/js/bootstrap.min.js"></script>
  <script src="//cdnjs.cloudflare.com/ajax/libs/bootstrap-select/1.6.3/js/bootstrap-select.min.js"></script>



<script>
    var x, i, j, l, ll, selElmnt, a, b, c;
    /*look for any elements with the class "custom-select":*/
    x = document.getElementsByClassName("custom-select");
    l = x.length;
    for (i = 0; i < l; i++) {
        selElmnt = x[i].getElementsByTagName("select")[0];
        ll = selElmnt.length;
        /*for each element, create a new DIV that will act as the selected item:*/
        a = document.createElement("DIV");
        a.setAttribute("class", "select-selected");
        a.innerHTML = selElmnt.options[selElmnt.selectedIndex].innerHTML;
        x[i].appendChild(a);
        /*for each element, create a new DIV that will contain the option list:*/
        b = document.createElement("DIV");
        b.setAttribute("class", "select-items select-hide");
        for (j = 1; j < ll; j++) {
            /*for each option in the original select element,
            create a new DIV that will act as an option item:*/
            c = document.createElement("DIV");
            c.innerHTML = selElmnt.options[j].innerHTML;
            c.addEventListener("click", function (e) {
                /*when an item is clicked, update the original select box,
                and the selected item:*/
                var y, i, k, s, h, sl, yl;
                s = this.parentNode.parentNode.getElementsByTagName("select")[0];
                sl = s.length;
                h = this.parentNode.previousSibling;
                for (i = 0; i < sl; i++) {
                    if (s.options[i].innerHTML == this.innerHTML) {
                        s.selectedIndex = i;
                        h.innerHTML = this.innerHTML;
                        y = this.parentNode.getElementsByClassName("same-as-selected");
                        yl = y.length;
                        for (k = 0; k < yl; k++) {
                            y[k].removeAttribute("class");
                        }
                        this.setAttribute("class", "same-as-selected");
                        break;
                    }
                }
                h.click();
            });
            b.appendChild(c);
        }
        x[i].appendChild(b);
        a.addEventListener("click", function (e) {
            /*when the select box is clicked, close any other select boxes,
            and open/close the current select box:*/
            e.stopPropagation();
            closeAllSelect(this);
            this.nextSibling.classList.toggle("select-hide");
            this.classList.toggle("select-arrow-active");
        });
    }
    function closeAllSelect(elmnt) {
        /*a function that will close all select boxes in the document,
        except the current select box:*/
        var x, y, i, xl, yl, arrNo = [];
        x = document.getElementsByClassName("select-items");
        y = document.getElementsByClassName("select-selected");
        xl = x.length;
        yl = y.length;
        for (i = 0; i < yl; i++) {
            if (elmnt == y[i]) {
                arrNo.push(i)
            } else {
                y[i].classList.remove("select-arrow-active");
            }
        }
        for (i = 0; i < xl; i++) {
            if (arrNo.indexOf(i)) {
                x[i].classList.add("select-hide");
            }
        }
    }
    /*if the user clicks anywhere outside the select box,
    then close all select boxes:*/
    document.addEventListener("click", closeAllSelect);
</script>

<script>
    $(document).ready(function () {
        $("#advtravelF").click(function () {
            $("#advtravelssF").slideToggle();
        });


        $("#TravellerF").click(function () {
            $("#boxF").slideToggle();
        });
        $("#serachbtnF").click(function () {
            $("#boxF").slideToggle();
        });

    });
</script>

<script type="text/javascript">
    function plusF() {
        document.getElementById("sapnTotPaxF").value = (parseInt(document.getElementById("AdultF").value.split(' ')[0]) + parseInt(document.getElementById("ChildF").value.split(' ')[0]) + parseInt(document.getElementById("InfantF").value.split(' ')[0])).toString() + ' Traveller';
    }
    plusF();
</script>
<script type="text/javascript">
    var myDate = new Date();
    var currDate = (myDate.getDate()) + '/' + (myDate.getMonth() + 1) + '/' + myDate.getFullYear();
    document.getElementById("txtDepDateF").value = currDate;
    document.getElementById("hidtxtDepDateF").value = currDate;
    document.getElementById("txtRetDateF").value = currDate;
    document.getElementById("hidtxtRetDateF").value = currDate;
    var UrlBase = '<%=ResolveUrl("~/") %>';
</script>

<script type="text/javascript" src="<%=ResolveUrl("~/Scripts/jquery-1.4.4.min.js") %>"></script>
<script type="text/javascript" src="<%=ResolveUrl("~/Scripts/jquery-ui-1.8.8.custom.min.js")%>"></script>
<script type="text/javascript" src="<%=ResolveUrl("~/Scripts/change.min.js") %>"></script>
<script type="text/javascript" src="<%=ResolveUrl("~/Scripts/Search3F.js") %>"></script>

<script type="text/javascript">


    $(function () {
        $("#CB_GroupSearchF").click(function () {

            if ($(this).is(":checked")) {
                // $("#box").hide();
                $("#TravellerF").hide();
                $("#rdbRoundTripF").attr("checked", true);
                $("#rdbOneWayF").attr("checked", false);

            } else {
                // $("#box").show();
                $("#TravellerF").show();
            }
        });
    });

</script>


