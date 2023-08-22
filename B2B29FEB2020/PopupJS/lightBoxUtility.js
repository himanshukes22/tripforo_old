function openLightBoxPopUp( sourceUrl, className) {
 
    var SK__LightBox = new Class({

        win: {}, // obj containing all lightbox DOM elements

        initialize: function() {
            this.initWindow();
            this.bindLinks();
        },

        initWindow: function() {
            this.win = {
                wrapper: $('sk-lightbox-wrapper'),
                close: $('sk-lightbox-close'),
                iframe: $('sk-lightbox-iframe')
            };

            this.win.wrapper.setStyle('opacity', '0');
            this.win.wrapper.style.display = 'block';
            this.win.wrapper.inject(document.body);

            this.win.close.addEvent('click', function(e) {
                e.preventDefault();
                this.hideWindow();
            } .bind(this));
        },

        bindLinks: function() {
        var links = $('.' + className);
            links.each(function(link) {
                    link.addEvent('click', function(e) {
                        e.preventDefault();
                        var url = sourceUrl;
                        this.mgr.showWindow(url);
                    } .bind({ mgr: this, link: link }));
            } .bind(this));
        },

        showWindow: function(url) {
            this.loadContent(url);
            this.win.wrapper.fade('in');
        },

        hideWindow: function() {
            this.win.wrapper.fade('out');
            this.win.iframe.src = 'about:blank';
        },

        loadContent: function(url) {
            if (this.win.iframe.src != url) {
                this.win.iframe.src = url;
            }
        }

    })
}
