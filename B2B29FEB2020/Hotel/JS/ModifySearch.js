var cellStyle = '';
var childHelp = '<strong><u>Please provide the ages of children in each room.</u></strong>';
var adultHelp = '';
var textRooms = '<strong>Rooms:&nbsp; </strong>';
var textAdults = '<strong>Adults</strong>';
var textChildren = '<strong>Children</strong>';
var textChildError = 'Please specify the ages of all children.';
var pad = "";
var textRoomX = 'Room ?:';
var textChildX = 'Child ?:';

var childrenPerRoom = new Array();
var adultsPerRoom = new Array();
var childAgesPerRoom = new Array();
var numRooms = 0;
var maxChildren = 0;
var pad = "";

adultsPerRoom[0] = 1;
childrenPerRoom[0] = 0;

numRooms = 1;
if (numRooms < 1) {
    numRooms = 1;
}
refresh();

function setChildAge(room, child, age) {
    if (childAgesPerRoom[room] == null) {
        childAgesPerRoom[room] = new Array();
    }
    childAgesPerRoom[room][child] = age;
}

function setNumAdults(room, numAdults) {
    adultsPerRoom[room] = numAdults;
}

function setNumChildren(room, numChildren) {
    childrenPerRoom[room] = numChildren;
    refresh();
}

function setNumRooms(x) {
    numRooms = x;
    for (i = 0; i < x; i++) {
        if (adultsPerRoom[i] == null) {
            adultsPerRoom[i] = 1;
        }
        if (childrenPerRoom[i] == null) {
            childrenPerRoom[i] = 0;
        }
    }
    refresh();
}

function renderRoomSelect() {
    var x = "";
    x += "<select class='combobox' name='numberOfRooms' onchange='setNumRooms(this.options[this.selectedIndex].value);'>";
    for (var i = 1; i < 5; i++) {
        x += "<option value='" + i + "'" + (numRooms == i ? " selected" : "") + ">" + i;
    }
    x += "</select>";
    return x;
}

function refresh() {
    maxChildren = 0;
    document.getElementById('rooms').value = numRooms;
    for (var i = 0; i < numRooms; i++) {
        if (childrenPerRoom[i] > maxChildren) {
            maxChildren = childrenPerRoom[i];
        }
        if (document.getElementById('contrycode').value != 'IN') {
            if (document.getElementById('contrycode').value != "") {
                if (parseInt(adultsPerRoom[i]) > 3 && parseInt(childrenPerRoom[i]) > 0) {
                    alert(adultsPerRoom[i] + '4 Adult and Child can not alowed in One Room. Please select less then 4 adult for child Occupancy room.');
                    childrenPerRoom[i] = 0;
                }
            }
        }
    }

    var x = "";
    if (adultHelp.length > 0) {
        x = adultHelp + '<p>\n';
    }

    if (numRooms > 8) {
        x += "<div class='mainselector' >";
        x += "<div class='elementholder'>" + textRooms + "</div><div class='elementholder'>";
        x += renderRoomSelect();
        x += "</div></div>";

    } else {
        x += "<div style='clear: both;'><div style='float:left;'>" + textRooms + "</div><div style='float:left;'>" + renderRoomSelect() + "</div>"
        x += "<div style='float:left;margin-left:40px;'><div style='float:left;'>" + textAdults + "</div><div style='float:left; margin-left:40px;'>" + textChildren + "</div></div><div style='clear:both;'></div>";
        x += "<div class='mainselector'>";
        x += "<div class='elementholder'>"

        //           if (numRooms != 1) {
        //                x += "";
        //            }
        for (var i = 0; i < numRooms; i++) {
            x += "</div>";
            x += "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";

            if (numRooms > 1) {
                x += "" + getValue(textRoomX, i + 1) + "";
            }
            else {
                x += '&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;';
            }
            x += "";
            x += buildSelect("room-" + i + "-adult-total", "setNumAdults(" + i + ", this.options[this.selectedIndex].value)", 1, 4, adultsPerRoom[i]);
            x += "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
            x += buildSelect("room-" + i + "-child-total", "setNumChildren(" + i + ", this.options[this.selectedIndex].value)", 0, 3, childrenPerRoom[i]);
            x += "";
            if (numRooms > 1)
                x += "<div style='clear:both;'></div>";
        }
        x += "</div>";

        var didHeader = false;
        for (var i = 0; i < numRooms; i++) {
            if (childrenPerRoom[i] > 0) {
                if (!didHeader) {
                    x += "<div class='mainselector'>";
                    x += "<div class='elementholder'>";
                    x += "</br>";
                    x += childHelp;
                    x += "</br>";
                    x += "</div><div class='elementholder'><div class='element3'>&nbsp;</div>";
                    for (var j = 0; j < maxChildren; j++) {
                        x += "<div class='element3'>" + getValue(textChildX, j + 1) + "</div>";
                    }
                    didHeader = true;
                }
                x += "</div><div class='elementholder'><div class='element3' style='width:55px;'>" + getValue(textRoomX, i + 1) + "</div>";
                for (var j = 0; j < childrenPerRoom[i]; j++) {
                    x += "<div class='element3'>";
                    var def = 0;
                    if (childAgesPerRoom[i] != null) {
                        if (childAgesPerRoom[i][j] != null) {
                            def = childAgesPerRoom[i][j];
                        }
                    }
                    x += "<select  class='combobox'  name='room-" + i + "-child-" + j + "-age' onchange='setChildAge(" + i + ", " + j + ", this.options[this.selectedIndex].value);'>";
                    /// x += "<option value='-1'"+(def == -1 ? " selected" : "")+">-?-";
                    x += "<option value='0'" + (def == 0 ? " selected" : "") + ">0";
                    for (var k = 1; k <= 12; k++) {
                        x += "<option value='" + k + "'" + (def == k ? " selected" : "") + ">" + k;
                    }
                    x += "</select></div>";
                }
                if (childrenPerRoom[i] < maxChildren) {
                    for (var j = childrenPerRoom[i]; j < maxChildren; j++) {
                        x += "<div class='element'>&nbsp;</div>";
                    }
                }
                x += "</div>";
            }
        }
        if (didHeader) {
            x += "</div>";
        }
    }
    /*------------------------------*/
    document.getElementById('hot-search-params').innerHTML = x;
    /*------------------------------*/
}

function buildSelect(name, onchange, min, max, selected) {
    var x = "<select  class='combobox w15'  name='" + name + "'";
    if (onchange != null) {
        x += " onchange='" + onchange + "'";
    }
    x += ">";
    for (var i = min; i <= max; i++) {
        x += "<option value='" + i + "'";
        if (i == selected) {
            x += " selected";
        }

        x += ">" + i + "";
    }
    x += "</select>";
    return x;
}

function validateGuests(form) {
    if (numRooms < 9) {
        var missingAge = false;
        for (var i = 0; i < numRooms; i++) {
            var numChildren = childrenPerRoom[i];
            if (numChildren != null && numChildren > 0) {
                for (var j = 0; j < numChildren; j++) {
                    if (childAgesPerRoom[i] == null || childAgesPerRoom[i][j] == null || childAgesPerRoom[i][j] == -1) {
                        missingAge = true;
                    }
                }
            }
        }

        if (missingAge) {
            alert(textChildError);
            return false;


        } else {
            document.getElementById('rooms').value = numRooms;
            document.getElementById('adts').value = adultsPerRoom;
            document.getElementById('chds').value = numChildren;
            return true;
        }
    } else {
        document.getElementById('rooms').value = numRooms;
        document.getElementById('adts').value = adultsPerRoom;
        document.getElementById('chds').value = numChildren;
        return true;
    }
}

function submitGuestInfoForm(form) {
    if (!validateGuests(form)) {
        return false;
    }

    return true;
}

function getValue(str, val) {
    return str.replace(/\?/g, val);
}
function setgustDetail(x, Guest) {
    var gust = Guest.split('_');
    var adt = gust[0].split(',');
    var chd = gust[1].split(',');
    for (var d = 0; d < x; d++) {
        var nofchild = parseInt(chd[d]);
        adultsPerRoom[d] = adt[d];
        childrenPerRoom[d] = chd[d];
        for (var e = 0; e < nofchild; e++) {
            var ages = gust[2].split("R" + d + "C" + e + ",");
            setChildAge(d, e, ages[1].split(',')[0])
        }
    }
    // refresh();
}
