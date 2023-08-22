<%@ Page Title="" Language="VB" MasterPageFile="~/MasterAfterLogin.master" AutoEventWireup="false" CodeFile="Result.aspx.vb" Inherits="FlightInt_FltResult" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link rel="stylesheet" href="../css/flightIntlRes.css" type="text/css" />
    <link rel="stylesheet" href="../css/main2.css" type="text/css" />

         <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.5.1/jquery.min.js"></script>
  <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.4.1/js/bootstrap.min.js"></script>
    <script type="text/javascript" src="../js/fareinfo.js"></script>
    <script type="text/javascript" src="../js/chrome.js"></script>
    <link href="CSS/newcss/main.css" rel="stylesheet" />

 <style type="text/css">
        .backdrop {
            background-color: white;
            position: fixed;
            top: 0;
            right: 0;
            bottom: 0;
            left: 0;
        }

        .spinner {
            position: absolute;
            top: calc(50% - 12.5px);
            left: calc(50% - 25px);
            width: 50px;
            height: 50px;
            border-top: 8px solid aliceblue;
            border-right: 8px solid aliceblue;
            border-bottom: 8px solid aliceblue;
            border-left: 8px solid #8c618d;
            border-radius: 50%;
            animation-name: spin;
            animation-duration: 3s;
            animation-iteration-count: infinite;
            animation-timing-function: linear;
        }

        @keyframes spin {
            0% {
                transform: rotate(0deg);
                border-left: 8px solid deeppink;
            }

            25% {
                transform: rotate(360deg);
                border-left: 8px solid gold;
            }

            50% {
                transform: rotate(720deg);
                border-left: 8px solid palegreen;
            }

            75% {
                transform: rotate(1080deg);
                border-left: 8px solid aqua;
            }

            100% {
                transform: rotate(1440deg);
                border-left: 8px solid deeppink;
            }
        }

        .logo {
            position: absolute;
            top: calc(50% + 35px);
            left: calc(50% - 25px);
            font-family: sans-serif;
            color: gray;
            letter-spacing: 0.1em;
        }
    </style>


    <style type="text/css">
        .pad {
            padding-right: 2px !important;
            padding-left: 2px !important;
        }

        .f16 {
            color: #000;
    font-family: Lato-Bold,Arial,Helvetica,sans-serif;
    font-size: 11px;
    position: relative;
    /*margin-bottom: 5px;*/
    font-weight:bold;
        }


        .spn {
            color: red;
    background: #eee;
    padding: 4px;
    border-radius: 4px;
        }

        .spn1 {
            color: red;
    background: #525252;
    padding: 4px;
    border-radius: 4px;
        }

        .spnBtnShow {
            background: #ffcfcf;
    padding: 2px;
    border-radius: 2px;
        }

        .spnBtnHide {
            background: #cfffd7;
    padding: 2px;
    border-radius: 2px;
        }


    </style>


<style type="text/css">
     .modal-window {
  position: fixed;
  background-color: rgb(0 0 0 / 25%);
  top: 0;
  right: 0;
  bottom: 0;
  left: 0;
  z-index: 999;
  visibility: hidden;
  opacity: 0;
  pointer-events: none;
  -webkit-transition: all 0.3s;
  transition: all 0.3s;
}
.modal-window:target {
  visibility: visible;
  opacity: 1;
  pointer-events: auto;
}
.modal-window > div {
  width: 100%;
  position: absolute;
  top: 50%;
  left: 50%;
  -webkit-transform: translate(-50%, -50%);
          transform: translate(-50%, -50%);
  padding: 2em;
  background: #ffffff;
}
.modal-window header {
  font-weight: bold;
}
.modal-window h1 {
  font-size: 150%;
  margin: 0 0 15px;
}

.modal-close {
  color: #aaa;
  line-height: 50px;
  font-size: 80%;
  position: absolute;
  right: 0;
  text-align: center;
  top: 0;
  width: 70px;
  text-decoration: none;
}
.modal-close:hover {
  color: black;
}

/* Demo Styles */



a {
  color: inherit;
}



.modal-window div:not(:last-of-type) {
  margin-bottom: 15px;
}

small {
  color: #aaa;
}

.btn {
  background-color: #fff;
  padding: 1em 1.5em;
  border-radius: 3px;
  text-decoration: none;
}
.btn i {
  padding-right: 0.3em;
}

 </style>


<style type="text/css">
        
.tabs {
  max-width: 100% !important;
}
.tabs-nav li {
  float: left !important;
  width: 25% !important;
  
}
.tabs-nav li:first-child a {
  border-right: 0 !important;
  border-top-left-radius: 6px !important;
}
.tabs-nav li:last-child a {
  border-top-right-radius: 6px !important;
}
.tb-btn {
  background: #eaeaed;
  border: 1px solid #cecfd5;
  color: #0087cc;
  display: block;
  font-weight: 600;
  padding: 10px 0;
  text-align: center;
  text-decoration: none;
}
.tb-btn:hover {
  color: #ff7b29;
}
.tab-active ,.tb-btn {
  background: #fff !important;
  border-bottom-color: transparent !important;
  color: #2db34a !important;
  cursor: default !important;
}
.tabs-stage {
  /*border: 1px solid #cecfd5 !important;*/
  border-radius: 0 0 6px 6px !important;
  border-top: 0 !important;
  clear: both !important;
  /*padding: 24px 30px !important;*/
  position: relative !important;
  top: -1px !important;
}

    </style>

<style type="text/css">
        .new-fare-details {
    width: 100%;
    font-size: 12px;
    padding: 12px 0;
    float: left;
    margin: 0px;
}

.new-fare-d-1 {
    width: 35%;
    padding: 0px;
    float: left;
    margin: 0;
    border: 1px solid #d9d9d9;
    display: table;
    font-size: 13px;
    font-weight: 600;
}

.nw-b2b-fared {
    margin: 0px;
    padding: 0px 0px 5px;
    float: left;
    border-bottom: 1px solid #d9d9d9;
    width: 100%;
}

.nw-pad-b2 {
    padding: 10px;
    float: left;
    width: 92%;
}
.nw-far-1 {
    margin: 0px;
    padding: 0px;
    width: 58%;
    font-weight: 600;
    float: left;
}

.nw-far-2 {
    margin: 0px;
    padding: 0px;
    width: 40%;
    float: right;
    text-align: right;
}

.nw-b2b-fared {
    margin: 0px;
    padding: 0px 0px 5px;
    float: left;
    border-bottom: 1px solid #d9d9d9;
    width: 100%;
}

.can-bnew-rg {
    width: 60%;
    float: right;
    margin: 0px;
    padding: 0px;
}

.can-b2b-tr {
    width: 100%;
    margin: 0 0;
    padding: 0px;
    float: left;
}

.lef-b2b-cane {
    width: 48%;
    padding: 0px;
    margin: 0px;
    float: left;
}

.b2b-ca-char {
    width: 100%;
    padding: 0 0 6px;
    font-size: 12px;
    font-weight: 600;
}

.b2b-can-tabe {
    margin: 0px;
    padding: 0px;
    font-size: 12px;
    width: 100%;
    float: left;
    background: #fff;
}

.rig-b2b-cane {
    width: 48%;
    padding: 0px;
    margin: 0px;
    float: right;
}

.tr-b2b-cancgr {
    width: 100%;
    float: left;
    margin: 0px;
    padding: 0 0 0;
}

.trm-had {
    margin: 0px;
    padding: 10px 0;
    font-size: 18px;
    font-weight: 600;
}

.terms-b2b2-cancahe {
    font-size: 10px;
    color: #000;
    padding: 0;
    height: 67px;
    overflow-x: hidden;
}

    </style>

   
<style type="text/css">

    .falsebookbutton {
    display: inline-block;
    background: #e60000;
    border: #ff3131;
    color: #fff;
    text-align: center;
    padding: 10px;
    border-radius: 5px;
    }
    .falsebookbutton:hover {
        background:#797979;
        border:#797979;
    }

    .buttonfltbk {
            display: inline-block;
    background: #e60000;
    border: #ff3131;
    color: #fff;
    text-align: center;
    padding: 10px;
    border-radius: 5px;
    }

        .buttonfltbk:hover {
            background:#797979;
        border:#797979;
        }
  #loading-msg {
            width: 100%;
            position: absolute;
            left: 0;
            bottom: 220px;
            text-align: center;
            
            color: #333;
            
        }


</style>


    <table style="background: #fff; width: 950px; margin: auto;" align="center">
        <tr>
            <td colspan="2" style="width: 100%; border: 1px solid #eee;">
                <asp:PlaceHolder ID="mtrxPL" runat="server"></asp:PlaceHolder>
            </td>
        </tr>
        <tr>
            <td align="left" style="width: 700px;" valign="top">
                <div style="width: 720px; margin: auto; background: #fff;">
                    <asp:Xml ID="xmlRes" runat="server" TransformSource="Multicity.xsl"></asp:Xml>
                </div>
            </td>
            <td valign="top" style="padding: 8px; width: 200px; border: 1px solid #eee;" rowspan="2">
                <div style="clear: both; color: #2a0d37; text-align: center; font-weight: bold; height: 25px;">
                    <asp:Label ID="lblTotal" runat="server"></asp:Label>
                </div>
                <div style="clear: both; background: #f2f4f4; padding-left: 10px; font-weight: bold; height: 25px; line-height: 25px;">
                    Filter By Airline
                </div>
                <div id="div2" runat="server" style="float: left; padding-left: 10px">
                    <asp:CheckBoxList ID="chkair" runat="server" CellPadding="10" CellSpacing="10" RepeatDirection="Vertical"
                        RepeatLayout="Table" TextAlign="Right" AutoPostBack="true">
                    </asp:CheckBoxList>
                </div>
            </td>
        </tr>
    </table>
    
    <script type="text/javascript" language="javascript">
        function __doPostBack(eventTarget, eventArgument) {
            var formObj = document.getElementById("aspnetForm");

            if (!formObj.onsubmit || (formObj.onsubmit() != false)) {

                formObj.__EVENTTARGET.value = eventTarget;

                formObj.__EVENTARGUMENT.value = eventArgument;

                formObj.submit();

            }

    </script>

    <script type="text/javascript">
        function SetHiddenVariable(st) {
            var jsVar;
            if (st == 0)
            { jsVar = '0-Stop'; }
            else if (st == 1)
            { jsVar = '1-Stop'; }
            else if (st == 2)
            { jsVar = '2-Stop'; }
            else
            { jsVar = st; }
            __doPostBack('callPostBack', jsVar);
        }
        function SetHiddenVariable1(st) {
            var jsVar = st + "";
            __doPostBack('callPostBack', jsVar);
        }
    </script>

    <div id="divfareDetails" class="frdiv" style="display: none">
    </div>
</asp:Content>

