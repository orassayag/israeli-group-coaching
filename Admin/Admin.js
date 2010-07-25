function enableMail() {
    document.getElementById('ctl00_getMailDiv').className = 'mailYes';
}

function validateMail() {
    var email = document.getElementById('ctl00_ContentPlaceHolder1_mailSearchText').value;

    if (email == null || email == undefined || email == "") {
        document.getElementById('ctl00_ContentPlaceHolder1_mailError').innerText = "Please Enter Mail Address";
        return false;
    }
    return true;
}

function validate(email) {
    if (email == null || email == undefined || email == "") {
        document.getElementById('ctl00_errorLabel').innerText = "Please Enter Mail Address";
        return false;
    }

    if (/^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/.test(email)) {
        return true;
    }
    return false;
}

function validateForgot() {

    var mail = document.getElementById('ctl00_recoverMail').value;

    if (!validate(mail)) {
        document.getElementById('ctl00_errorLabel').innerText = "Illegal Mail Address";
        return false;
    }
    return true;
}

function validateAddMail() {

    var mail = document.getElementById('ctl00_ContentPlaceHolder1_mailAddress').value;
    if (!validate(mail)) {
        document.getElementById('ctl00_ContentPlaceHolder1_mailAddLabel').innerText = "Illegal Mail Address";
        return false;
    }
    return true;
}

function switchLanguage(pageType, selector) {
    switch (pageType) {
        case 'content':
            switch (selector.selectedIndex) {
                case 1:
                    document.getElementById('ctl00_ContentPlaceHolder1_contentTitle').className = "textHeb";
                    document.getElementById('ctl00_ContentPlaceHolder1_contentKeyWords').className = "textHeb";
                    document.getElementById('ctl00_ContentPlaceHolder1_contentDescription').className = "textHeb";
                    document.getElementById('ctl00_ContentPlaceHolder1_contentButtonTitle').className = "textHeb";
                    document.getElementById('ctl00_ContentPlaceHolder1_contentEditor').setAttribute("ContentLangDirection", "RightToLeft");
                    break;
                case 2:
                    document.getElementById('ctl00_ContentPlaceHolder1_contentButtonTitle').className = "textEn";
                    document.getElementById('ctl00_ContentPlaceHolder1_contentTitle').className = "textEn";
                    document.getElementById('ctl00_ContentPlaceHolder1_contentKeyWords').className = "textEn";
                    document.getElementById('ctl00_ContentPlaceHolder1_contentDescription').className = "textEn";
                    document.getElementById('ctl00_ContentPlaceHolder1_contentEditor').setAttribute("ContentLangDirection", "LeftToRight");
                    break;
                default:
                    break;
            }
            break;
        case 'article':
            switch (selector.selectedIndex) {
                case 1:
                    document.getElementById('ctl00_ContentPlaceHolder1_articleTitle').className = "textHeb";
                    document.getElementById('ctl00_ContentPlaceHolder1_articleKeyWords').className = "textHeb";
                    document.getElementById('ctl00_ContentPlaceHolder1_articleDescription').className = "textHeb";
                    document.getElementById('ctl00_ContentPlaceHolder1_articleEditor').setAttribute("ContentLangDirection", "RightToLeft");
                    break;
                case 2:
                    document.getElementById('ctl00_ContentPlaceHolder1_articleTitle').className = "textEn";
                    document.getElementById('ctl00_ContentPlaceHolder1_articleKeyWords').className = "textEn";
                    document.getElementById('ctl00_ContentPlaceHolder1_articleDescription').className = "textEn";
                    document.getElementById('ctl00_ContentPlaceHolder1_articleEditor').setAttribute("ContentLangDirection", "LeftToRight");
                    break;
                default:
                    break;
            }
            break;
        case "mail":
            switch (selector.selectedIndex) {
                case 1:
                    document.getElementById('ctl00_ContentPlaceHolder1_mailTitle').className = "textHeb";
                    document.getElementById('ctl00_ContentPlaceHolder1_sendMailEditor').setAttribute("ContentLangDirection", "RightToLeft");
                    break;
                case 2:
                    document.getElementById('ctl00_ContentPlaceHolder1_mailTitle').className = "textEn";
                    document.getElementById('ctl00_ContentPlaceHolder1_sendMailEditor').setAttribute("ContentLangDirection", "LeftToRight");
                    break;
                default:
                    break;
            }
            break;
        default:
            break;
    }
}

function selectMethod(checkbox) {
    if (checkbox.id == 'mailSearchList') {

        var g = document.getElementById('ctl00_ContentPlaceHolder1_mailSearchByManually');
        g.className = "visi";
        var u = document.getElementById('ctl00_ContentPlaceHolder1_mailSearchByList');
        u.className = "unVisi";

        document.getElementById('searchManually').className = "visi";
        document.getElementById('mailSearchListSpan').className = "unVisi";
        document.getElementById('searchManuallySpan').className = "visi";
    }
    else {
        var v = document.getElementById('ctl00_ContentPlaceHolder1_mailSearchByList');
        v.className = "visi";
        var t = document.getElementById('ctl00_ContentPlaceHolder1_mailSearchByManually');
        t.className = "unVisi";

        document.getElementById('mailSearchList').className = "visi";
        document.getElementById('mailSearchListSpan').className = "visi";
        document.getElementById('searchManuallySpan').className = "unVisi";
    }
    checkbox.className = "unVisi";
    checkbox.checked = "";
}

function mailSelectMethod(checkbox) {

    checkbox.className = "unVisi";
    checkbox.checked = "";

    if (checkbox.id == 'getAllMailsCheck') {

        var u = document.getElementById('ctl00_ContentPlaceHolder1_getFromDateToDateMailsDiv');
        u.className = "unVisiPro";
        document.getElementById('ctl00_ContentPlaceHolder1_geMailsHidden').value = "no";

        document.getElementById('ctl00_ContentPlaceHolder1_getFromDateToDateMailsCheck').className = "visi";
        document.getElementById('getFromDateToDateMailsSpan').className = "visi";
        document.getElementById('getAllSpan').className = "unVisi";
    }
    else {
        var v = document.getElementById('ctl00_ContentPlaceHolder1_getFromDateToDateMailsDiv');
        v.className = "visi";
        document.getElementById('ctl00_ContentPlaceHolder1_geMailsHidden').value = "yes";

        document.getElementById('getAllMailsCheck').className = "visi";
        document.getElementById('getAllSpan').className = "visi";
        document.getElementById('getFromDateToDateMailsSpan').className = "unVisi";
    }
}

function validateSelectMail() {

    var selector = document.getElementById('ctl00_ContentPlaceHolder1_mailListMailSelector');
    for (var i = 0; i < selector.length; i++) {
        if (selector.item(i).selected) {
            return true;
        }
    }
    document.getElementById('ctl00_ContentPlaceHolder1_mailSelectLabel').innerText = "Please Select Mail From The List";
    return false;
}

function selectAll() {
    var checkboxCollection = document.getElementById('ctl00_ContentPlaceHolder1_mailsSelector').getElementsByTagName('input');

    for (var i = 0; i < checkboxCollection.length; i++) {
        if (checkboxCollection[i].type.toString().toLowerCase() == "checkbox") {
            checkboxCollection[i].checked = "checked";
        }
    }
}

function selectNone() {
    var checkboxCollection = document.getElementById('ctl00_ContentPlaceHolder1_mailsSelector').getElementsByTagName('input');

    for (var i = 0; i < checkboxCollection.length; i++) {
        if (checkboxCollection[i].type.toString().toLowerCase() == "checkbox") {
            checkboxCollection[i].checked = "";
        }
    }
}

function checkOne() {
    var checkboxCollection = document.getElementById('ctl00_ContentPlaceHolder1_mailsSelector').getElementsByTagName('input');

    for (var i = 0; i < checkboxCollection.length; i++) {
        if (checkboxCollection[i].type.toString().toLowerCase() == "checkbox") {
            var f = checkboxCollection[i].checked;
            if (f == true) {
                return true;
            }
        }
    }

    document.getElementById('ctl00_ContentPlaceHolder1_mailListSenderLabel').innerText = "Please Select At Least One Name";
    return false;
}

function validateGetMail() {

    var o = document.getElementById('getAllMailsCheck').className;
    var g = document.getElementById('ctl00_ContentPlaceHolder1_getFromDateToDateMailsCheck').className;

    if (o == "" && g == "") {
        document.getElementById('ctl00_ContentPlaceHolder1_getMailsLabel').innerText = "Please Select An Option";
        return false;
    }

    if (o == "visi") {

        if (document.getElementById('ctl00_ContentPlaceHolder1_fromDateBox').value == "") {
            document.getElementById('ctl00_ContentPlaceHolder1_getMailsLabel').innerText = "Please Enter From Date";
            return false;
        }

        if (document.getElementById('ctl00_ContentPlaceHolder1_toDateBox').value == "") {
            document.getElementById('ctl00_ContentPlaceHolder1_getMailsLabel').innerText = "Please Enter To Date";
            return false;
        }
    }

    return true;
}

function clearC() {
    document.getElementById('ctl00_ContentPlaceHolder1_mailsResult').value = "";
}

function clearLog() {
    document.getElementById('ctl00_ContentPlaceHolder1_logsBody').value = "";
}

function graduateCheck(index) {
    var g = null;
    switch (index) {
        case 'session':
            g = document.getElementById('ctl00_ContentPlaceHolder1_graduatesYearHebrew').value;
            if (g == "") {
                document.getElementById('ctl00_ContentPlaceHolder1_addSessionLabel').innerText = "Please Enter Hebrew Session Year";
                return false;
            }
            break;
        case 'graduate':
            g = document.getElementById('ctl00_ContentPlaceHolder1_addGraduateName').value;
            if (g == "") {
                document.getElementById('ctl00_ContentPlaceHolder1_addGraduateLabel').innerText = "Please Enter Graduate's Name";
                return false;
            }
            break;
        default:
            break;
    }
    return true;
}

function validateField() {
    var g = null;
    g = document.getElementById('ctl00_ContentPlaceHolder1_generalGetUserID').value;
    if (g == "") {
        document.getElementById('ctl00_ContentPlaceHolder1_adminLabel').innerText = "Please Enter User ID";
        return false;
    }
    g = document.getElementById('ctl00_ContentPlaceHolder1_generalGetPassword').value;
    if (g == "") {
        document.getElementById('ctl00_ContentPlaceHolder1_adminLabel').innerText = "Please Enter Password";
        return false;
    }
    g = document.getElementById('ctl00_ContentPlaceHolder1_generalGetPassword2').value;
    if (g == "") {
        document.getElementById('ctl00_ContentPlaceHolder1_adminLabel').innerText = "Please Re-Enter Password";
        return false;
    }
    var g1 = document.getElementById('ctl00_ContentPlaceHolder1_generalGetPassword').value;
    var g2 = document.getElementById('ctl00_ContentPlaceHolder1_generalGetPassword2').value;
    if (g1 != g2) {
        document.getElementById('ctl00_ContentPlaceHolder1_adminLabel').innerText = "Passwords Don't Match";
        return false;
    }
    return true;
}

function validateFieldLink() {
    var g = null;
    g = document.getElementById('ctl00_ContentPlaceHolder1_contentLinkPlace').value;
    if (g == "") {
        document.getElementById('ctl00_ContentPlaceHolder1_contentLinkPageLabel').innerText = "Please Enter Page's Place";
        return false;
    }
    g = document.getElementById('ctl00_ContentPlaceHolder1_contentLinkButtonTitle').value;
    if (g == "") {
        document.getElementById('ctl00_ContentPlaceHolder1_contentLinkPageLabel').innerText = "Please Enter Page's Title";
        return false;
    }
    g = document.getElementById('ctl00_ContentPlaceHolder1_contentLinkUrl').value;
    if (g == "") {
        document.getElementById('ctl00_ContentPlaceHolder1_contentLinkPageLabel').innerText = "Please Enter Page's Url";
        return false;
    }
}

