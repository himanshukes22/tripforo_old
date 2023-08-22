 
        function focusObj(obj) {
            if (obj.value == "First Name") obj.value = "";

        }

        function blurObj(obj) {
            if (obj.value == "") obj.value = "First Name";


        }
        function focusObj1(obj) {

            if (obj.value == "Last Name") obj.value = "";
        }

        function blurObj1(obj) {


            if (obj.value == "") obj.value = "Last Name";
        }
        function focusObjM(obj) {

            if (obj.value == "Middle Name") obj.value = "";
        }

        function blurObjM(obj) {


            if (obj.value == "") obj.value = "Middle Name";
        }


        function focusObjAir(obj) {

            if (obj.value == "Airline") obj.value = "";
        }

        function blurObjAir(obj) {


            if (obj.value == "") obj.value = "Airline";
        }
        function focusObjNumber(obj) {

            if (obj.value == "Number") obj.value = "";
        }

        function blurObjNumber(obj) {


            if (obj.value == "") obj.value = "Number";
        }

        function focusObjCFName(obj) {

            if (obj.value == "First Name") obj.value = "";
        }

        function blurObjCFName(obj) {


            if (obj.value == "") obj.value = "First Name";
        }
        function focusObjCMName(obj) {

            if (obj.value == "Middle Name") obj.value = "";
        }

        function blurObjCMName(obj) {


            if (obj.value == "") obj.value = "Middle Name";
        }
        function focusObjCLName(obj) {

            if (obj.value == "Last Name") obj.value = "";
        }

        function blurObjCLName(obj) {


            if (obj.value == "") obj.value = "Last Name";
        }





        function focusObjIFName(obj) {

            if (obj.value == "First Name") obj.value = "";
        }

        function blurObjIFName(obj) {


            if (obj.value == "") obj.value = "First Name";
        }
        function focusObjIMName(obj) {

            if (obj.value == "Middle Name") obj.value = "";
        }

        function blurObjIMName(obj) {


            if (obj.value == "") obj.value = "Middle Name";
        }
        function focusObjILName(obj) {

            if (obj.value == "Last Name") obj.value = "";
        }

        function blurObjILName(obj) {


            if (obj.value == "") obj.value = "Last Name";
        }
   
        function paxValidate() {

            var elem = document.getElementById('ctl00_ContentPlaceHolder1_tbl_Pax').getElementsByTagName("input");
            var elem1 = document.getElementById('ctl00_ContentPlaceHolder1_tbl_Pax').getElementsByTagName("select");

            for (var i = 0; i < elem.length; i++) {
                if (elem[i].type == "text" && elem[i].value == "" || elem[i].value == "First Name" && elem[i].id.indexOf("txtAFirstName") > 0) {
                    alert('First Name can not be blank for Adult');
                    elem[i].focus();
                    return false;

                }
                if (elem[i].type == "text" && elem[i].value == "" || elem[i].value == "Last Name" && elem[i].id.indexOf("txtALastName") > 0) {
                    alert('Last Name can not be blank for Adult');
                    elem[i].focus();
                    return false;

                }

            }


           
            //             var elem1 = document.getElementById('td_Child').getElementsByTagName("input");
            if (document.getElementById('ctl00_ContentPlaceHolder1_Repeater_Child_ctl00_txtCFirstName')) 
            {
                for (var i = 0; i < elem.length; i++) 
                {
                    if (elem[i].type == "text" && elem[i].value == "" || elem[i].value == "First Name" && elem[i].id.indexOf("txtCFirstName") > 0) 
                    {
                        alert('First Name can not be blank for Child');
                        elem[i].focus();
                        return false;

                    }
                    if (elem[i].type == "text" && elem[i].value == "" || elem[i].value == "Last Name" && elem[i].id.indexOf("txtCLastName") > 0) 
                    {
                        alert('Last Name can not be blank for Child');
                        elem[i].focus();
                        return false;

                    }

                 

                }
                for (var i = 0; i < elem1.length; i++)
                
                 {
                     if (elem1[i].value == "Year" && elem1[i].id.indexOf("ddl_AgeChild") > 0) {
                        alert('Please Select Age');
                        elem1[i].focus();
                        return false;

                    }
                }
            }




           

            //var elem = document.getElementById('td_Infant').getElementsByTagName("input");
            if (document.getElementById('ctl00_ContentPlaceHolder1_Repeater_Infant_ctl00_txtIFirstName'))
             {
                 for (var i = 0; i < elem.length; i++)
                 {
                    if (elem[i].type == "text" && elem[i].value == "" || elem[i].value == "First Name" && elem[i].id.indexOf("txtIFirstName") > 0)
                     {
                        alert('First Name can not be blank for Infant');
                        elem[i].focus();
                        return false;

                    }
                    if (elem[i].type == "text" && elem[i].value == "" || elem[i].value == "Last Name" && elem[i].id.indexOf("txtILastName") > 0)
                     {
                        alert('Last Name can not be blank for Infant');
                        elem[i].focus();
                        return false;

                    }

                    
                }
                for (var i = 0; i < elem1.length; i++) {
if (elem1[i].value == "Year" && elem1[i].id.indexOf("ddl_AgeInfant") > 0) {
                        alert('Please Select Age');
                        elem1[i].focus();
                        return false;

                    }
                }
            }





           



           
            

            if (confirm("Are you sure!"))
                return true;
            return false;

        }
        function isCharKey(evt) {
            var charCode = (evt.which) ? evt.which : event.keyCode;
            if (charCode >= 65 && charCode <= 122 || charCode == 32 || charCode == 08) {
                return true;
            }
            else {

                return false;
            }
        }