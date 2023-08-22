﻿/**
* jQuery jPList Plugin ##VERSION## 
* Copyright 2014 Miriam Zusin. All rights reserved.
* To use this file you must buy a licence at http://codecanyon.net/user/no81no/portfolio 
*/
(function () {//+
    'use strict';

    /**
	* Get control status
	* @param {Object} context
	* @param {boolean} isDefault - if true, get default (initial) control status; else - get current control status
	* @return {jQuery.fn.jplist.app.dto.StatusDTO}
	*/
    var getStatus = function (context, isDefault, selectedbutton) {

        var data
			, textAndPathsGroup = []
			, status = null
			, ignore = ''
			, storageStatus;

        storageStatus = context.$control.data('storage-status');

        if (isDefault && storageStatus) {
            status = storageStatus;
        }
        else {

            if (context.controlOptions && context.controlOptions.ignore) {

                //get ignore characters
                ignore = context.controlOptions.ignore;
            }

            context.params.$buttons.each(function (index, el) {

                var $button = jQuery(el)
					, selected;

                if (isDefault) {
                    selected = $button.attr('data-selected') === 'true';
                }
                else {
                    //get button data
                    selected = $button.data('selected') || false;
                }

                textAndPathsGroup.push({
                    selected: selected
					, text: $button.data('dataText')
					, path: $button.data('dataPath')
                });
            });



         


            //init status related data
            data = new jQuery.fn.jplist.ui.controls.ButtonTextFilterGroupDTO(textAndPathsGroup, ignore);

            //init status
            status = new jQuery.fn.jplist.app.dto.StatusDTO(
                context.name
                , context.action
                , context.type
                , data
                , context.inStorage
                , context.inAnimation
                , context.isAnimateToTop
            );

        }

        return status;
    };

    /**
    * Get deep link
    * @param {Object} context
    * @return {string} deep link
    */
    var getDeepLink = function (context) {

        var deepLink = ''
            , status
            , isDefault = false
            , textAndPath
            , sections = [];

        //get status
        status = getStatus(context, isDefault, '');

        if (status.data && status.data.textAndPathsGroup && status.data.textAndPathsGroup.length > 0) {

            for (var i = 0; i < status.data.textAndPathsGroup.length; i++) {

                //get text and path
                textAndPath = status.data.textAndPathsGroup[i];

                if (textAndPath.selected) {
                    sections.push(textAndPath.text + context.options.delimiter3 + textAndPath.path);
                }
            }

            if (sections.length > 0) {

                //init deep link
                deepLink = context.name + context.options.delimiter0 + 'selected=' + sections.join(context.options.delimiter2);
            }
        }

        return deepLink;
    };

    /**
    * get status by deep link
    * @param {Object} context
    * @param {string} propName - deep link property name
    * @param {string} propValue - deep link property value
    * @return {jQuery.fn.jplist.app.dto.StatusDTO}
    */
    var getStatusByDeepLink = function (context, propName, propValue) {

        var isDefault = false
            , status
            , sections
            , textAndPathArr;

        //get status
        status = getStatus(context, isDefault, '');

        if (status.data && (propName === 'selected')) {

            status.data.textAndPathsGroup = [];

            sections = propValue.split(context.options.delimiter2);

            for (var i = 0; i < sections.length; i++) {

                textAndPathArr = sections[i].split(context.options.delimiter3);

                if (textAndPathArr.length === 2) {

                    status.data.textAndPathsGroup.push({
                        selected: true
                        , text: textAndPathArr[0]
                        , path: textAndPathArr[1]
                    });
                }
            }
        }

        return status;
    };

    /**
    * Get control paths
    * @param {Object} context
    * @param {Array.<jQuery.fn.jplist.domain.dom.models.DataItemMemberPathModel>} paths
    */
    var getPaths = function (context, paths) {

        var $button
            , dataPath
            , path;

        context.params.$buttons.each(function (index, el) {

            //get checkbox
            $button = jQuery(this);

            //get data-path
            dataPath = $button.attr('data-path');

            if (dataPath) {

                //create path object
                path = new jQuery.fn.jplist.domain.dom.models.DataItemMemberPathModel(dataPath, 'text');

                //add path to the paths list
                paths.push(path);
            }

        });
    };

    /**
    * Set control status
    * @param {Object} context
    * @param {jQuery.fn.jplist.app.dto.StatusDTO} status
    * @param {boolean} restoredFromStorage - is status restored from storage
    */
    var setStatus = function (context, status, restoredFromStorage) {

        var textAndPath
            , $button;

        //reset	all buttons
        context.params.$buttons.each(function (index, el) {

            //get button
            $button = jQuery(el);

            //remove selected class
            $button.removeClass('jplist-selected');

            //reset selected value
            $button.data('selected', false);
        });

        if (status.data && status.data.textAndPathsGroup && jQuery.isArray(status.data.textAndPathsGroup) && status.data.textAndPathsGroup.length > 0) {

            for (var i = 0; i < status.data.textAndPathsGroup.length; i++) {

                //get "text and path" object
                textAndPath = status.data.textAndPathsGroup[i];

                $button = context.params.$buttons.filter('[data-path="' + textAndPath.path + '"][data-text="' + textAndPath.text + '"]');

                if ($button.length > 0 && textAndPath.selected) {

                    $button.addClass('jplist-selected');
                    $button.data('selected', true);
                }
            }
        }
    };

    /**
    * Init control events
    * @param {Object} context
    */
    var initEvents = function (context) {

        var selected;

      



        context.params.$buttons.on('click', function () {
            var $button = jQuery(this);
            var selectedText = $button.data('dataFltr').split('/');

            context.params.$buttons.each(function () {

                var $button1 = jQuery(this)
                $button1.data('selected', false);
              
            });

            //for (var i = 0; i < selectedText.length; i++) {

            //    context.params.$buttons.each(function () {

            //        var $button1 = jQuery(this)
            //        if ($button1.data('dataText') == selectedText[i]) {
            //            $button1.data('selected', true);
            //        }
                   
            //    });
            //}





            //get selected value
           // selected = $button.data('selected') || false;

            //toggle value
             $button.data('selected', true);

            //save last status in the history
         
            //context.history.addStatus(getStatus(context, false, $button));

                               //render statuses
            // context.observer.trigger(context.observer.events.forceRenderStatusesEvent, [false]);

             //context.observer.trigger(context.observer.events.forceRenderStatusesEvent, [true]);
             //var lastSt1 = new Array();
             //var lastSt = context.history.getLastList(context.history);
             //if (lastSt != null) {

             //    for (var i = 0; i < lastSt.length - 1; i++) {

             //        if (lastSt[i].type != 'button-text-filter-group') {
                       
             //            lastSt1.push(lastSt[i]);
             //        }
             //    }
             //}

             var status = getStatus(context, false, $button);
           //  lastSt1.push(status);
             context.history.addStatus(getStatus(context, false, $button));
             context.observer.trigger(context.observer.events.forceRenderStatusesEvent, [false]);        
             //context.observer.trigger(context.observer.events.setStatusesEvent, [lastSt1]);
            // context.observer.trigger(context.observer.events.renderStatusesEvent, [lastSt1]);



            // context.history.popList(context.history)
        });
    };

    /** 
    * Dropdown control: sort dropdown, filter dropdown, paging dropdown etc.
    * @constructor
    * @param {Object} context
    */
    var Init = function (context) {

        context.params = {
            $buttons: context.$control.find('[data-button]')
        };

        //in every button: save its data
        context.params.$buttons.each(function () {

            var $button = jQuery(this)
                , selected;

            selected = $button.attr('data-selected') === 'true';

            if (context.options.deepLinking) {
                selected = false;
            }

            $button.data('selected', selected);
            $button.data('dataPath', $button.attr('data-path'));
            $button.data('dataText', $button.attr('data-text'));
            $button.data('dataFltr', $button.attr('data-fltr'));

           
        });

        //init events
        initEvents(context);

        return jQuery.extend(this, context);
    };

    /**
    * Get control status
    * @param {boolean} isDefault - if true, get default (initial) control status; else - get current control status
    * @return {jQuery.fn.jplist.app.dto.StatusDTO}
    */
    Init.prototype.getStatus = function (isDefault) {
        return getStatus(this, isDefault, '');
    };

    /**
    * Get Deep Link
    * @return {string} deep link
    */
    Init.prototype.getDeepLink = function () {
        return getDeepLink(this);
    };

    /**
    * Get Paths by Deep Link
    * @param {string} propName - deep link property name
    * @param {string} propValue - deep link property value
    * @return {jQuery.fn.jplist.app.dto.StatusDTO}
    */
    Init.prototype.getStatusByDeepLink = function (propName, propValue) {
        return getStatusByDeepLink(this, propName, propValue);
    };

    /**
    * Get Paths
    * @param {Array.<jQuery.fn.jplist.domain.dom.models.DataItemMemberPathModel>} paths
    */
    Init.prototype.getPaths = function (paths) {
        getPaths(this, paths);
    };

    /**
    * Set Status
    * @param {jQuery.fn.jplist.app.dto.StatusDTO} status
    * @param {boolean} restoredFromStorage - is status restored from storage
    */
    Init.prototype.setStatus = function (status, restoredFromStorage) {
        setStatus(this, status, restoredFromStorage);
    };

    /** 
    * Button text filter group control
    * @constructor
    * @param {Object} context
    */
    jQuery.fn.jplist.ui.controls.TextFilterGroup = function (context) {
        return new Init(context);
    };

})();

