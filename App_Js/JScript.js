$(function () {
    $('.news-container').vTicker();
});

(function () {
    $.fn.vTicker = function (options) {
        var defaults = {
            speed: 4000,
            pause: 6000,
            showItems: 4,
            animation: '',
            mousePause: false
        };

        var options = $.extend(defaults, options);

        moveUp = function (obj, height) {
            obj = obj.children('ul');
            first = obj.children('li:first').clone(true);

            obj.animate({ top: '-=' + height + 'px' }, options.speed, function () {
                $(this).children('li:first').remove();
                $(this).css('top', '0px');
            });

            if (options.animation == 'fade') {
                obj.children('li:first').fadeOut(options.speed);
                obj.children('li:last').hide().fadeIn(options.speed);
            }

            first.appendTo(obj);
        };

        return this.each(function () {
            obj = $(this);
            maxHeight = 95;

            obj.css({ overflow: 'hidden', position: 'relative' })
                .children('ul').css({ position: 'absolute', margin: 0, padding: 0 })
                .children('li').css({ margin: 0, padding: 0 });

            obj.children('ul').children('li').each(function () {
                if ($(this).height() > maxHeight) {
                    maxHeight = 95;
                }
            });

            obj.children('ul').children('li').each(function () {
                $(this).height(maxHeight);
            });

            obj.height(maxHeight * 2);

            interval = setInterval('moveUp(obj, maxHeight)', options.pause);

            if (options.mousePause) {
                obj.bind("mouseenter", function () {
                    clearInterval(interval);
                }).bind("mouseleave", function () {
                    interval = setInterval('moveUp(obj, maxHeight)', options.pause);
                });
            }
        });
    };
})(jQuery);


function checkLead() {
    var g = null;
    g = document.getElementById('ctl00_ContentPlaceHolder1_getLeadName').value;
    if (g == "") {
        document.getElementById('ctl00_ContentPlaceHolder1_leadLabel').innerText = "אנא הכנס/י את שמך";
        return false;
    }
    g = document.getElementById('ctl00_ContentPlaceHolder1_getLeadMail').value;
    if (g == "") {
        document.getElementById('ctl00_ContentPlaceHolder1_leadLabel').innerText = "אנא הכנס/י דואר אלקטרוני";
        return false;
    }

    var reg = /^([A-Za-z0-9_\-\.])+\@([A-Za-z0-9_\-\.])+\.([A-Za-z]{2,4})$/;
    if (!reg.test(g)) {
        document.getElementById('ctl00_ContentPlaceHolder1_leadLabel').innerText = "דואר אלקטרוני לא חוקי";
        return false;
    }

    g = document.getElementById('ctl00_ContentPlaceHolder1_getLeadTitle').value;
    if (g == "") {
        document.getElementById('ctl00_ContentPlaceHolder1_leadLabel').innerText = "אנא הכנס/י נושא";
        return false;
    }
    g = document.getElementById('ctl00_ContentPlaceHolder1_getLeadBody').value;
    if (g == "") {
        document.getElementById('ctl00_ContentPlaceHolder1_leadLabel').innerText = "אנא הכנס/י הודעה";
        return false;
    }

    return true;
}

function validate() {
    var g = null;
    g = document.getElementById('ctl00_getName').value;
    if (g == "") {
        document.getElementById('ctl00_messageLabel').innerText = "אנא הכנס/י את שמך";
        return false;
    }
    g = document.getElementById('ctl00_getMail').value;
    if (g == "") {
        document.getElementById('ctl00_messageLabel').innerText = "אנא הכנס/י דואר אלקטרוני";
        return false;
    }
}

function load(sessionID) {
    var load = window.open('FullPage.aspx?pageID=null&type=graduates&sessionID=' + sessionID, '', 'scrollbars=no,menubar=no,height=400,width=400,resizable=yes,toolbar=no,location=no,status=no');
}

