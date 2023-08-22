/**
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
    var getStatus = function (context, isDefault) {

        var data
			, status;

        data = new jQuery.fn.jplist.ui.controls.DropdownSortDTO(context.$control.attr('data-path')
																			, context.$control.attr('data-type')
																			, context.$control.attr('data-order')
																			, context.$control.attr('data-datetime-format')
																			, context.$control.attr('data-ignore'));

        status = new jQuery.fn.jplist.app.dto.StatusDTO(
			context.name
			, context.action
			, context.type
			, data
			, false
			, false
			, false
		);

        return status;
    };



    var initEvents = function (context) {

        context.$control.on('click', function () {

            // 
            var dataOrder = $(this).attr('data-order');
            var dd;
            if($.trim(dataOrder)=='asc')
            {
                $(this).attr('data-order', 'desc')
                dd = 'desc';
            }
            else
            {
                $(this).attr('data-order', 'asc')
                dd = 'asc';
            }
            //$(this).removeAttr();
            var status;
            //$.trim(context.$root).replace($(this)[0].outerHTML, "<DIV class=hidden data-control-action=\"sort\" data-control-name=\"sortCITZ\" data-control-type=\"sortCITZ\" data-path=\".price\" data-type=\"number\" data-order=\"" + dd + "\" >sort </DIV>");

            //$(this)[0].outerHTML="<DIV class=hidden data-control-action=\"sort\" data-control-name=\"sortCITZ\" data-control-type=\"sortCITZ\" data-path=\".price\" data-type=\"number\" data-order=\"" + dd + "\" >sort </DIV>";
            status = getStatus(context, false);
            //context.history.popStatus();



            //context.history.addStatus(status);
            var lastSt = new Array();
            var lastSt1 = new Array();
            var removeItem;
            var lastSt = context.history.getLastList(context.history);
            if (lastSt != null) {

                for (var i = 0; i < lastSt.length - 1; i++) {

                    if (lastSt[i].action !='sort') {

                        lastSt1.push(lastSt[i]);// = status;

                        //removeItem= lastSt[i];
                        
                    }
                }
            }


           

            //lastSt = jQuery.grep(lastSt, function (value) {
            //    return value != removeItem;
            //});


            lastSt1.push(status);
            // context.history.popList(context.history.listStatusesQueue);
          //  context.history.addStatus(lastSt);
           // context.history.addList(lastSt);
           
           // context.observer.trigger(context.observer.events.restoreEvent, [status]);
            context.observer.trigger(context.observer.events.setStatusesEvent, [lastSt1]);
          //  context.observer.trigger(context.observer.events.sortEvent, [status, context]);
            //context.observer.trigger(context.observer.events.renderList, [context, status]);
            //render html by statuses
            context.observer.trigger(context.observer.events.renderStatusesEvent, [lastSt1]);

           // var panel = jQuery.fn.jplist.ui.panel.controllers.PanelController(context.$root, context.options, context.history, context.observer);
           // var context1 = jQuery.fn.jplist.ui.list.controllers.DOMController(context.$root, context.options, context.observer, panel, context.history);
            //context1.collection.applyStatuses(status);

            //context.observer.trigger(context.observer.events.forceRenderStatusesEvent, [true]);
        });
    };




    /**
	* Get control paths
	* @param {Object} context
	* @param {Array.<jQuery.fn.jplist.domain.dom.models.DataItemMemberPathModel>} paths
	*/
    var getPaths = function (context, paths) {

        var jqPath
			, dataType
			, path;

        //init vars
        jqPath = context.$control.attr('data-path');
        dataType = context.$control.attr('data-type');

        //init path
        if (jqPath) {

            //init path
            path = new jQuery.fn.jplist.domain.dom.models.DataItemMemberPathModel(jqPath, dataType);
            paths.push(path);
        }
    };

    /** 
	* 'Default sort' control - used instead of 'sort dropdown' control, to define the inital sort order.
	* @constructor
	* @param {Object} context
	*/
    var Init = function (context) {
        initEvents(context);
        return jQuery.extend(this, context);
    };

    /**
	* Get control status
	* @param {boolean} isDefault - if true, get default (initial) control status; else - get current control status
	* @return {jQuery.fn.jplist.app.dto.StatusDTO}
	*/
    Init.prototype.getStatus = function (isDefault) {
        return getStatus(this, isDefault);
    };

    /**
	* Get Paths
	* @param {Array.<jQuery.fn.jplist.domain.dom.models.DataItemMemberPathModel>} paths
	*/
    Init.prototype.getPaths = function (paths) {
        getPaths(this, paths);
    };

    /** 
	* 'Default sort' control - used instead of 'sort dropdown' control, to define the inital sort order.
	* @constructor
	* @param {Object} context	
	*/
    jQuery.fn.jplist.ui.controls.DefaultSort1 = function (context) {
        return new Init(context);
    };

})();

