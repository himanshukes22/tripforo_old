
var cellStyle='';
var childHelp='Please provide the ages of children in each room.';
var adultHelp='';
var textRooms='Rooms:';
var textAdults = '<div><strong>Adults</strong></div>';
var textChildren = '<div><strong>Children</strong></div>';
var textChildError='Please specify the ages of all children.';
var textRoomX='Room ?:';
var textChildX='Child ?:';

var childrenPerRoom=new Array();
var adultsPerRoom=new Array();
var childAgesPerRoom=new Array();
var numRooms=0;
var maxChildren=0;

adultsPerRoom[0]=1;
childrenPerRoom[0]=0;

    numRooms=1;
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
        x += "<select  class='form-control'  name='numberOfRooms' onchange='setNumRooms(this.options[this.selectedIndex].value);'>";
        for (var i = 1; i < 5; i++) {
            x += "<option value='"+i+"'"+(numRooms == i ? " selected" : "")+">" + i;
        }
        x += "</select>";
        return x;
    }

    function refresh() {
        maxChildren = 0;
        document.getElementById('rooms').value=numRooms;
        for (var i = 0; i < numRooms; i++) {
            if (childrenPerRoom[i] > maxChildren) {
                maxChildren = childrenPerRoom[i];
            }
            if (document.getElementById('contrycode').value != 'IN') {
                if (document.getElementById('contrycode').value != "") {
                    if (parseInt(adultsPerRoom[i]) > 3 && parseInt(childrenPerRoom[i]) > 0) {
                        alert(adultsPerRoom[i] + ' Adult and Child can not allowed in One Room. Please select less then 4 adult for child occupancy room.');
                        childrenPerRoom[i] = 0;
                    }
                    else if (parseInt(adultsPerRoom[i]) == 1 && parseInt(childrenPerRoom[i]) > 2) {
                    alert(adultsPerRoom[i] + ' Adult and 3 Child can not allow in One Room. Please select 2 child occupancy room.');
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
             x += "<div class='mainselector'>";
            x += "<div class='elementholder'><div class='element'>" + textRooms + "</div></div><div class='elementholder'><div class='element'>";
            x += renderRoomSelect();
            x += "</div></div></div>";
        } else {
            x += "<div class='mainselector'>";
            x += "<div class='elementholder'><div class='element'>" + textRooms + "</div>";
            if (numRooms > 1) {
                x += "<div class='element'>&nbsp;</div>";
            }
            x += "<div class='element'>" + textAdults + "</div><div class='element'>" + textChildren + "</div></div>";
            for (var i = 0; i < numRooms; i++) {
                x += "<div class='elementholder'><div class='element'>";
                if (i == 0) {
                    x += renderRoomSelect();
                } else {
                    x += "&nbsp;";
                }
                x += "</div>";
                if (numRooms > 1) {
                    x += "<div class='element'>"+getValue(textRoomX, i+1) + "</div>";
                }
                x += "<div class='element'>";
                x += buildSelect("room-" + i + "-adult-total", "setNumAdults(" + i + ", this.options[this.selectedIndex].value)", 1, 4, adultsPerRoom[i]);
                x += "</div><div class='element'>";
                x += buildSelect("room-" + i + "-child-total", "setNumChildren(" + i + ", this.options[this.selectedIndex].value)", 0, 3, childrenPerRoom[i]);
                x += "</div></div>";
            }
            x += "</div><br/>";
            
            var didHeader = false;
            for (var i = 0; i < numRooms; i++) {
                if (childrenPerRoom[i] > 0) {
                    if (!didHeader) {
                        x += "<div class='mainselector'>";
                        x += "<div class='elementholder'><div class='element2'>";
                        x += "</br>";
                        x += childHelp;
                        x += "";
                        x += "</div></div><div class='elementholder'><div class='element3'>&nbsp;</div>";
                        for (var j = 0; j < maxChildren; j++) {
                            x += "<div class='element3'>" + getValue(textChildX, j+1) + "</div>";
                        }
                        didHeader = true;
                    }
                    x += "</div><div class='elementholder'><div class='element3'> " + getValue(textRoomX, i+1) + "</div>";
                    for (var j = 0; j < childrenPerRoom[i]; j++) {
                        x += "<div class='element3'>";
                        var def = 0;
                        if (childAgesPerRoom[i] != null) {
                            if (childAgesPerRoom[i][j] != null) {
                                def = childAgesPerRoom[i][j];
                            }
                        }
                        x += "<select  class='form-control'  name='room-" + i + "-child-" + j + "-age' onchange='setChildAge(" + i + ", " + j + ", this.options[this.selectedIndex].value);'>";
                       // x += "<option value='-1'"+(def == -1 ? " selected" : "")+">-?-";
                        x += "<option value='0'"+(def == 0 ? " selected" : "")+">0";
                        for (var k = 1; k <= 12; k++) {
                            x += "<option value='"+k+"'"+(def == k ? " selected" : "")+">"+k;
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
         

        var cls = "adt";

        if ($.trim(name).search('child') > 0)
        {
            cls = 'chd';
        }
        var x = "<select  class='form-control "+cls+" '  name='" + name + "'";
        if (onchange != null) {
            x += " onchange='" + onchange + "'";
        }
        x +=">";
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
            if (missingAge) 
            {
                alert(textChildError);
                return false;
            }
            else
            {
              document.getElementById('rooms').value=numRooms;
              document.getElementById('adts').value=adultsPerRoom ;
              document.getElementById('chds').value=numChildren  ;
                return true;
            }
        } else {
         document.getElementById('rooms').value=numRooms;
         document.getElementById('adts').value=adultsPerRoom ;
              document.getElementById('chds').value=numChildren  ;
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
////-->
//function Setdate1()
//{
//submit1();
//document.getElementById('pickUpMonth1').value =document.getElementById('pickUpMonth').value;
//  document.getElementById('pickUpMonth_dep1').value =document.getElementById('pickUpMonth_dep').value;
//  
//}
