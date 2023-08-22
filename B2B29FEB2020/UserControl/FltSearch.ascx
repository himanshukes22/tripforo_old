<%@ Control Language="VB" AutoEventWireup="false" CodeFile="FltSearch.ascx.vb" Inherits="UserControl_FltSearch" %>

<%@ Register Src="~/UserControl/HotelSearch.ascx" TagPrefix="uc1" TagName="HotelSearch" %>
<%@ Register Src="~/UserControl/HotelDashboard.ascx" TagPrefix="uc1" TagName="HotelDashboard" %>

<link type="text/css" href="<%=ResolveUrl("~/Styles/jquery-ui-1.8.8.custom.css") %>"
    rel="stylesheet" />

<link href="../Advance_CSS/css/textbox.css" rel="stylesheet" />
<link href="../Hotel/css/B2Bhotelengine.css" rel="stylesheet" />

<style type="text/css">
    /*.dropdown-container {
  display: flex;
  flex-direction: column;
  justify-content: center;
  align-items: center;
  margin: 40px 0 0 0;
}*/

    div#ui-datepicker-div.ui-datepicker {
        width: auto !important;
    }

    .dropdown {
        /*background: #f8f8f8;*/
        padding: 20px;
        /*border-radius: 3px;
  width: 140px;*/
        display: flex;
        justify-content: space-around;
        font-size: 1.1rem;
        cursor: pointer;
        /*box-shadow: 0 0 1px rgba(0, 0, 0, 0.3);*/
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

    .dropdown-menu {
        display: none;
        flex-direction: column;
        border-radius: 4px;
        margin-top: 8px;
        width: 160px;
        padding: 10px;
        box-shadow: 0 0 5px -1px rgba(0, 0, 0, 0.3);
        background: #fafafa;
        transform-origin: top left;
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

         /*   .dropdown-menu span:hover {
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


<style>
    .alert-box {
        padding: 15px;
        margin-bottom: 20px;
        border: 1px solid transparent;
        border-radius: 4px;
    }

    .success {
        color: #3c763d;
        background-color: #dff0d8;
        border-color: #d6e9c6;
        display: none;
    }

    .failure {
        color: #a94442;
        background-color: #f2dede;
        border-color: #ebccd1;
        display: none;
    }

    .warning {
        color: #8a6d3b;
        background-color: #fcf8e3;
        border-color: #faebcc;
        display: none;
    }
</style>

<style>
    .circle {
        border-radius: 50%;
        */
        /* margin: -32px; */
        font-size: 2em;
        z-index: 1011;
         position: relative; 
        margin-left: -242px;
        margin-top: -11px;
        cursor: pointer;
        border: 1px solid red;
    z-index: 99999;
    font-weight: 400 !important;
    color: red !important;
    }

    .fa-exchange {
        color: #909090;
        padding: 6px;
        font-size: 18px;
    }
</style>


<style type="text/css">
    .minus, .plus {
        width: 35px;
        height: 35px;
        background: #ffffff;
        border-radius: 10%;
        padding: 8px 5px 8px 5px;
        border: 1px solid #ddd;
        display: inline-block;
        vertical-align: middle;
        text-align: center;
        cursor: pointer;
        color: #9d9d9d;
        line-height: 17px;
        font-weight: 600;
    font-size: 20px;
    }

      .minus1, .plus1 {
        width: 35px;
        height: 35px;
        background: #ffffff;
        border-radius: 10%;
        padding: 8px 5px 8px 5px;
        border: 1px solid #ddd;
        display: inline-block;
        vertical-align: middle;
        text-align: center;
        cursor: pointer;
        color: #9d9d9d;
        line-height: 17px;
        font-weight: 600;
    font-size: 20px;
    }

       .minusF, .plusF {
        width: 35px;
        height: 35px;
        background: #ffffff;
        border-radius: 10%;
        padding: 8px 5px 8px 5px;
        border: 1px solid #ddd;
        display: inline-block;
        vertical-align: middle;
        text-align: center;
        cursor: pointer;
        color: #9d9d9d;
        line-height: 17px;
        font-weight: 600;
    font-size: 20px;
    }

    .inp {
        width: 30px;
        text-align: center;
        color: #000;
        background: none;
        border: none;
    }

    .main_dv {
        width: 35% !important;
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
        background: #0567a0;
        float: right;
        text-align: center;
        padding: 4px 12px;
        display: block;
        color: #fff;
        font-size: 20px;
        border-radius: 3px;
        -webkit-border-radius: 3px;
        -moz-border-radius: 3px;
    }

    .innr_pnl {
        width: 100% !important;
        position: relative;
        padding: 19px;
        left:20px;
    }

    .dropdown-content-n {
        position: absolute;
        z-index: 99;
        width: 420px;
        border: solid 1px #dbdbdb;
        border-radius: 2px;
        background-color: #fff;
        -webkit-box-shadow: 2px 0 5px 0 rgb(194 194 194 / 50%);
        box-shadow: 2px 0 5px 0 rgb(194 194 194 / 50%);
        margin-top: -63px;
    }

        .dropdown-content-n ::before {
            position: absolute;
            top: -10px;
            left: 35px;
            display: block;
            width: 0;
            height: 0;
            content: '';
            border-right: 10px solid transparent;
            border-bottom: 10px solid #dbdbdb;
            border-left: 10px solid transparent;
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

    .number {
        background: #d9d9d9;
    width: 72%;
    border-radius: 5px;
    }


    .clear {
        clear: both;
    }
</style>

<style type="text/css">
    /*  .topways > label.active {
        background: #5f5f5f;
        color: #ffffff !important;
        text-align: center;
        font-size: 14px;
        width: 23%;
        margin-right: -12px;
        padding: 5px 9px;
        white-space: normal;
        color: #555;
        border: 1px solid #dedede;
        border-radius: 5px;
    }*/

    /*    .mail {
        cursor: pointer;
        font-size: 14px;
        width: 23%;
        margin-right: -12px;
        padding: 5px 9px;
        white-space: normal;
        color: #555;
        border: 1px solid #dedede;
        text-align: center;
        border-radius: 5px;
        font-weight: 100;
    }*/

    label.mail input[type=radio]:checked + label::before {
        border-color: #0769a0;
        background-color: #0769a0;
        -webkit-box-shadow: inset 0 0 0 3px #fff;
        box-shadow: inset 0 0 0 3px #fff;
    }

    label.mail input[type=radio] + label::before {
        position: relative;
        top: 1px;
        display: inline-block;
        width: 18px;
        height: 18px;
        margin-right: 7px;
        content: '';
        cursor: pointer;
        vertical-align: top;
        border: 1px solid #b4b2b0;
        border-radius: 100%;
        background: #fff;
    }

    label.mail:hover {
        background: #f4f4f8;
    }

    label.mail {
        position: relative;
        top: -5px;
        left: -5px;
        margin: 0;
        padding: 5px 7px 5px 5px;
        cursor: pointer;
        border-radius: 5px;
    }

    label {
        font-weight: 100 !important;
    }
</style>

<style type="text/css">
    .dflex {
        display: flex;
    }

    .pax-limit {
        display: flex;
        flex-direction: column;
        position: relative;
    }

    .light-grey {
        color: #8a8a8a;
        font-size: .786em;
    }
</style>

<%--<script type="text/javascript">


    console.clear();

    function cfix(el) {
        let $target;

        if (el.target) {
            $target = $(event.target);
        } else {
            $target = $(el);
        }

        if ($target.val()) {
            $target.addClass('c-fix');
        } else {
            $target.removeClass('c-fix');
        }
    };

    try {

        $('[placeholder]:not(:placeholder-shown), [placeholder]:placeholder-shown').length;

    } catch (e) {

        $('[placeholder]').each(function (i, el) {
            cfix(el);
        });

        $('.demo-container').on('input', '[placeholder]', cfix);
    };
</script>


<script type="text/javascript">
    $(".btn").click(function () {

        var lable = $(".div").text().trim();

        if (lable == "Hide") {
            $(".div").text("Show");
            $(".myText").hide();
        }
        else {
            $(".div").text("Hide");
            $(".myText").show();
        }

    });

</script>

<script>
    $(document).ready(function () {
        var selector = '.topways label';
        $(selector).bind('click', function () {
            $(selector).removeClass('active');
            $(this).addClass('active');
        });

    });
</script>--%>

<script type="text/javascript">

    $(document).ready(function () {
        $('.minus').click(function () {
            var $input = $(this).parent().find('input');
            var $inputid = $input.attr('id');
            var count = parseInt($input.val()) - 1;
            if ($inputid != "Adult") {
                count = count <= 0 ? 0 : count;
            }
            else {
                count = count < 1 ? 1 : count;
            }
            $input.val(count);
            $input.change();
            AddAllPax();
            return false;
        });
        $('.plus').click(function () {
            var $input = $(this).parent().find('input');
            let inpcount = parseInt($input.val()) + 1;
            $input.val(inpcount);
            $input.change();
            AddAllPax();
            return false;
        });

        AddAllPax();

        function AddAllPax() {
            let adultinp = $("#Adult").val();
            let childinp = $("#Child").val();
            let infantinp = $("#Infant").val();

            $("#sapnTotPax").val(parseInt(adultinp) + parseInt(childinp) + parseInt(infantinp) + " Traveler(s)");


        }
    });


</script>



<div class="tab-content _pt-20">

    <div class="topways theme-search-area-options-white theme-search-area-options-dot-primary-inverse clearfix" style="padding: 17px;">


          <label class="mail active">
            <input type="radio" name="TripType" value="fixed" id="fixed" style="display: none;" disabled/>
            <label for="fixed">Fixed Departure</label>
        </label>

        <label class="mail" style="color:#ccc;">
            <input type="radio" name="TripType" value="rdbOneWay" id="rdbOneWay" style="display: none;" disabled/>

            <label for="rdbOneWay">One Way</label></label>

        <label class="mail" style="color:#ccc;">
            <input type="radio" name="TripType" value="rdbRoundTrip" id="rdbRoundTrip" style="display: none;" disabled/>
            <label for="rdbRoundTrip">Round Trip</label>

        </label>

        <label class="btn btn-primary mail " style="display: none;">
            <input type="radio" name="TripType" value="rdbMultiCity" id="rdbMultiCity" style="display: none;" />
            Multi-City
        </label>

      

    </div>
    <script>
        //$(document).ready(function () {
        //    var selector = '.topways div label';
        //    $(selector).bind('click', function () {
        //        $(selector).removeClass('active');
        //        $(this).addClass('active');
        //    });

        //});


    </script>

    <div class="non-fxd" style="display:none;">

        <div class="tab-pane active" id="SearchAreaTabs-3" role="tab-panel">
            <div class="theme-search-area theme-search-area-stacked">
                <div class="theme-search-area-form">


                    <div class="row" data-gutter="none">
                        <div class="col-md-6 col-xs-6 from-des">
                            <div class="tb-container">
                                <input type="text" name="txtDepCity1" class="aumd-tb typeahead" placeholder=" " data-provide="typeahead" onclick="this.value = '';" id="txtDepCity1" />
                                <input type="hidden" id="hidtxtDepCity1" class="aumd-tb" name="hidtxtDepCity1" value="" />
                                <label class="aumd-tb-label" for="inputName">Leaving From</label>
                                <span class="validation"></span>
                            </div>
                        </div>

                        <i class="fa fa-exchange circle interchange dis" id="change" style=""></i>

                        <div class="col-md-6 col-xs-6 to-arrv">
                            <div class="tb-container">
                                <input type="text" name="txtArrCity1" onclick="this.value = '';" id="txtArrCity1" class="aumd-tb typeahead" data-provide="typeahead" placeholder="Arrival" />
                                <input type="hidden" id="hidtxtArrCity1" name="hidtxtArrCity1" value="" />
                                <label class="aumd-tb-label" for="inputName">Leaving To</label>
                                <span class="validation"></span>
                            </div>
                        </div>
                    </div>



                    <div class="row" data-gutter="none">
                        <div class="col-md-6 col-xs-6 from-date" id="one">
                            <div class="tb-container">
                                <input type="text" class="aumd-tb datePickerStart _mob-h" placeholder="dd/mm/yyyy" name="txtDepDate" id="txtDepDate" value="" readonly="readonly" />
                                <input type="hidden" class="aumd-tb _desk-h mobile-picker" name="hidtxtDepDate" id="hidtxtDepDate" value="" />
                                <label class="aumd-tb-label" for="inputName">Depart Date</label>
                                <span class="validation"></span>
                            </div>
                        </div>

                        <div class="col-md-6 col-xs-6 to-date" id="Return">
                            <div class="tb-container">
                                <input type="text" placeholder="" name="txtRetDate" id="txtRetDate" class="aumd-tb datePickerEnd _mob-h second" value="" readonly="readonly" />
                                <input type="hidden" class="aumd-tb _desk-h mobile-picker" name="hidtxtRetDate" id="hidtxtRetDate" value="" />
                                <label class="aumd-tb-label" for="inputName">Return Date</label>
                                <span class="validation"></span>
                            </div>
                        </div>
                    </div>



                    <div class="row" data-gutter="none">
                        <div class="col-md-12 col-xs-12 pax-des" id="Traveller">
                            <div class="tb-container" href="#advtravelss" onclick="moreoptions()">
                                <input class="aumd-tb div" id="sapnTotPax" placeholder=" " type="text" />
                                <label class="aumd-tb-label" for="inputName">Traveller(s) and Other Options</label>
                                <span class="validation"></span>
                            </div>

                            <div id="advtravelss" style="display: none;">
                                <div id="div_Adult_Child_Infant">
                                    <div class="innr_pnl dflex">
                                        <div class="main_dv pax-limit">
                                            <label>
                                                <span>Adult</span>

                                            </label>
                                            <div class="number">
                                                <span class="minus">-</span>
                                                <input type="text" class="inp" value="1" min="1" name="Adult" id="Adult" />
                                                <span class="plus">+</span>
                                            </div>

                                        </div>
                                        <div class="main_dv pax-limit">
                                            <label>
                                                <span>Child<span class="light-grey">(2+ 12 yrs)</span></span>

                                            </label>

                                            <div class="number">
                                                <span class="minus">-</span>
                                                <input type="text" class="inp" value="0" min="0" name="Child" id="Child" />
                                                <span class="plus">+</span>
                                            </div>

                                        </div>
                                        <div class="main_dv pax-limit">

                                            <label>
                                                <span>Infant <span class="light-grey">(below 2 yrs)</span></span>

                                            </label>

                                            <div class="number">
                                                <span class="minus">-</span>
                                                <input type="text" class="inp" value="0" min="0" name="Infant" id="Infant" />
                                                <span class="plus Infant">+</span>
                                            </div>

                                        </div>

                                    </div>
                                   
                                </div>

                                <div class="advopt row">
                        <div class="row-col-gap" data-gutter="10">
                            <div class="col-md-6">


                                <div class="tb-container">
                                    <label>Airline</label>
                                    <input type="text" placeholder="" class="form-control" name="txtAirline" value="" id="txtAirline" />
                                    <input type="hidden" id="hidtxtAirline" name="hidtxtAirline" value="" />
                                    
                                    <span class="validation"></span>
                                </div>

                            </div>
                            <div class="col-md-6">

                                <div class="tb-container">
                                    <label>Class Type</label>
                                    <select name="Cabin" class="aumd-tb form-control" id="Cabin">
                                        <option value="" selected="selected">--Cabin Class--</option>
                                        <option value="C">Business</option>
                                        <option value="Y">Economy</option>
                                        <option value="F">First</option>
                                        <option value="W">Premium Economy</option>
                                    </select>
                                    
                                    <span class="validation"></span>
                                </div>



                            </div>

                        </div>



                    </div>

                            </div>

                        </div>



                    </div>


                    <div class="row" data-gutter="none" style="padding: 17px; float: right;">
                        <div class="col-md-6">
                            <button type="button" id="btnSearch" value="Search Flight" class="btn btn-danger">Search Flight</button>
                        </div>
                    </div>

                </div>
            </div>
        </div>
    </div>








    <div class="row">
        <div style="display: none;" id="two">
            <div class="onewayss col-md-4" id="DivDepCity2">
                <label for="exampleInputEmail1">
                    Leaving From:</label>

                <div class="input-container">
                    <i class="icon" aria-hidden="true">
                        <img src="../Images/icons/flight_dep.png" style="width: 23px; margin-top: -6px;" /></i>

                    <input type="text" name="txtDepCity2" class="input-field" placeholder="Departure City" id="txtDepCity2" />
                    <input type="hidden" id="hidtxtDepCity2" name="hidtxtDepCity2" value="" />
                </div>

            </div>

            <div class="onewayss col-md-4">
                <label for="exampleInputEmail1">
                    Going To:</label>

                <div class="input-container">
                    <i class="icon" aria-hidden="true">
                        <img src="../Images/icons/Flight_Arri.png" style="width: 23px; margin-top: -6px;" /></i>

                    <input type="text" name="txtArrCity2" class="input-field" placeholder="Destination City" id="txtArrCity2" />
                    <input type="hidden" id="hidtxtArrCity2" name="hidtxtArrCity2" value="" />
                </div>

            </div>
            <div class="col-md-4" id="DivArrCity2">

                <label for="exampleInputEmail1">
                    Depart Date:</label>
                <div class="input-container">
                    <i class="fa fa-calendar icon" aria-hidden="true"></i>

                    <input type="text" name="txtDepDate2" id="txtDepDate2" class="input-field" placeholder="dd/mm/yyyy" readonly="readonly" value="" />
                    <input type="hidden" name="hidtxtDepDate2" id="hidtxtDepDate2" value="" />
                </div>

            </div>
        </div>
    </div>
    <div class="row">
        <div style="display: none;" id="three">

            <div class="onewayss col-md-4" id="DivDepCity3">



                <div class="input-container">
                    <i class="icon" aria-hidden="true">
                        <img src="../Images/icons/flight_dep.png" style="width: 23px; margin-top: -6px;" /></i>

                    <input type="text" name="txtDepCity3" class="input-field" placeholder="Departure City" id="txtDepCity3" />
                    <input type="hidden" id="hidtxtDepCity3" name="hidtxtDepCity3" value="" />
                </div>

            </div>
            <div class="onewayss col-md-4">



                <div class="input-container">
                    <i class="icon" aria-hidden="true">
                        <img src="../Images/icons/Flight_Arri.png" style="width: 23px; margin-top: -6px;" /></i>
                    <input type="text" name="txtArrCity3" class="input-field" placeholder="Destination City" id="txtArrCity3" />
                    <input type="hidden" id="hidtxtArrCity3" name="hidtxtArrCity3" value="" />
                </div>

            </div>
            <div class="col-md-4" id="DivArrCity3">




                <div class="input-container">
                    <i class="fa fa-calendar icon" aria-hidden="true"></i>

                    <input type="text" name="txtDepDate3" id="txtDepDate3" class="input-field" placeholder="dd/mm/yyyy" readonly="readonly" />
                    <input type="hidden" name="hidtxtDepDate3" id="hidtxtDepDate3" value="" />

                </div>
            </div>
        </div>
    </div>
    <div class="row">
        <div style="display: none;" id="four">
            <div class="onewayss col-md-4" id="DivDepCity4">



                <div class="input-container">
                    <i class="icon" aria-hidden="true">
                        <img src="../Images/icons/flight_dep.png" style="width: 23px; margin-top: -6px;" /></i>

                    <input type="text" name="txtDepCity4" class="input-field" placeholder="Departure City" id="txtDepCity4" />
                    <input type="hidden" id="hidtxtDepCity4" name="hidtxtDepCity4" value="" />
                </div>

            </div>
            <div class="onewayss col-md-4">


                <div class="input-container">

                    <i class="icon" aria-hidden="true">
                        <img src="../Images/icons/Flight_Arri.png" style="width: 23px; margin-top: -6px;" /></i>

                    <input type="text" name="txtArrCity4" class="input-field" placeholder="Destination City" id="txtArrCity4" />
                    <input type="hidden" id="hidtxtArrCity4" name="hidtxtArrCity4" value="" />
                </div>

            </div>
            <div class="col-md-4" id="DivArrCity4">

                <div class="input-container">

                    <i class="fa fa-calendar icon" aria-hidden="true"></i>

                    <input type="text" name="txtDepDate4" id="txtDepDate4" class="input-field" placeholder="dd/mm/yyyy" readonly="readonly" />
                    <input type="hidden" name="hidtxtDepDate4" id="hidtxtDepDate4" value="" />
                </div>

            </div>
        </div>
    </div>
    <div class="row">
        <div style="display: none;" id="five">
            <div class="onewayss col-md-4" id="DivDepCity5">

                <div class="input-container">
                    <i class="icon" aria-hidden="true">
                        <img src="../Images/icons/flight_dep.png" style="width: 23px; margin-top: -6px;" /></i>
                    <input type="text" name="txtDepCity5" class="input-field" placeholder="Departure City" id="txtDepCity5" />
                    <input type="hidden" id="hidtxtDepCity5" name="hidtxtDepCity5" value="" />
                </div>

            </div>
            <div class="onewayss col-md-4">
                <div class="form-group">
                    <div class="input-container">
                        <i class="icon" aria-hidden="true">
                            <img src="../Images/icons/Flight_Arri.png" style="width: 23px; margin-top: -6px;" /></i>
                        <input type="text" name="txtArrCity5" class="input-field" placeholder="Destination City" onclick="this.value = '';" id="txtArrCity5" />
                        <input type="hidden" id="hidtxtArrCity5" name="hidtxtArrCity5" value="" />
                    </div>
                </div>
            </div>
            <div class="col-md-4" id="DivArrCity5">

                <div class="input-container">
                    <i class="fa fa-calendar icon" aria-hidden="true"></i>
                    <input type="text" name="txtDepDate5" id="txtDepDate5" class="input-field" placeholder="dd/mm/yyyy" readonly="readonly" />
                    <input type="hidden" name="hidtxtDepDate5" id="hidtxtDepDate5" value="" />
                </div>

            </div>
        </div>
    </div>
    <div class="row">
        <div style="display: none;" id="six">
            <div class="onewayss col-md-4" id="DivDepCity6">

                <div class="input-container">
                    <i class="icon" aria-hidden="true">
                        <img src="../Images/icons/flight_dep.png" style="width: 23px; margin-top: -6px;" /></i>
                    <input type="text" name="txtDepCity6" class="input-field" placeholder="Departure City" id="txtDepCity6" />
                    <input type="hidden" id="hidtxtDepCity6" name="hidtxtDepCity6" value="" />
                </div>

            </div>

            <div class="onewayss col-md-4">

                <div class="input-container">
                    <i class="icon" aria-hidden="true">
                        <img src="../Images/icons/Flight_Arri.png" style="width: 23px; margin-top: -6px;" /></i>
                    <input type="text" name="txtArrCity6" class="input-field" placeholder="Destination City" onclick="this.value = '';" id="txtArrCity6" />
                    <input type="hidden" id="hidtxtArrCity6" name="hidtxtArrCity6" value="" />
                </div>

            </div>
            <div class="col-md-4" id="ArrCity6">

                <div class="input-container">
                    <i class="fa fa-calendar icon" aria-hidden="true"></i>
                    <input type="text" name="txtDepDate6" id="txtDepDate6" class="input-field" placeholder="dd/mm/yyyy" readonly="readonly" />
                    <input type="hidden" name="hidtxtDepDate6" id="hidtxtDepDate6" value="" />
                </div>

            </div>
        </div>

        <div class="row" style="display: none;">
            <div class="col-md-6" id="add">
                <div class="col-md-4">
                    <a id="plus" class="pulse btn btn-danger">Add City</a>
                </div>
                <div class="col-md-2">
                    <a id="minus" class="pulse btn btn-danger">Remove City</a>
                </div>
            </div>
        </div>
    </div>
















</div>











<div class="clear1"></div>


<div class="col-md-3 col-xs-12 text-search" id="trAdvSearchRow" style="display: none">
    <div class="lft ptop10">
        All Fare Classes
    </div>
    <div class="lft mright10">
        <input type="checkbox" name="chkAdvSearch" id="chkAdvSearch" value="True" />
    </div>
    <div class="large-4 medium-4 small-12 columns">
        Gds Round Trip Fares
                                
                                <span class="lft mright10">
                                    <input type="checkbox" name="GDS_RTF" id="GDS_RTF" value="True" />
                                </span>
    </div>

    <div class="large-4 medium-4 small-12 columns">
        Lcc Round Trip Fares
                                
                                <span class="lft mright10">
                                    <input type="checkbox" name="LCC_RTF" id="LCC_RTF" value="True" />
                                </span>
    </div>

</div>




<script type="text/javascript">
    jQuery(function () {

        jQuery('#rdbOneWay').click(function () {
            jQuery('.fxd').hide();
            jQuery('.non-fxd').show();
            jQuery('#calendar').hide();
        });

        jQuery('#rdbRoundTrip').click(function () {
            jQuery('.fxd').hide();
            jQuery('.non-fxd').show();
            jQuery('#calendar').hide();
        });


        jQuery('#fixed').click(function () {
            jQuery('.non-fxd').hide();
            jQuery('.fxd').show();

        });

    });
</script>

<script>
    $(document).ready(function () {
        $("#advtravel").click(function () {
            $("#advtravelss").Toggle();
        });


        //$("#Traveller").click(function () {
        //    $("#box").slideToggle();
        //});
        //$("#serachbtn").click(function () {
        //    $("#box").slideToggle();
        //});

    });
</script>



<script>
    $(document).ready(function () {
        $("#advtravel").click(function () {
            $("#advtravelss").slideToggle();
        });


        //$("#Traveller").click(function () {
        //    $("#box").slideToggle();
        //});
        //$("#serachbtn").click(function () {
        //    $("#box").slideToggle();
        //});

    });
</script>


<script>
    $(document).ready(function () {

        // $(".interchange").on('click', function () {
        $(".interchange").unbind().click(function () {

            var pickup = $('#hidtxtDepCity1').val();
            $('#hidtxtDepCity1').val($('#hidtxtArrCity1').val());
            $('#hidtxtArrCity1').val(pickup);

            var pickup = $('#txtDepCity1').val();
            $('#txtDepCity1').val($('#txtArrCity1').val());
            $('#txtArrCity1').val(pickup);


        });



    });
</script>



<%--<script>
    $('#basic').flagStrap();

    $('.select-country').flagStrap({
        countries: {
            "US": "USD",
            "AU": "AUD",
            "CA": "CAD",
            "SG": "SGD",
            "GB": "GBP",
        },
        buttonSize: "btn-sm",
        buttonType: "btn-info",
        labelMargin: "10px",
        scrollable: false,
        scrollableHeight: "350px"
    });

    $('#advanced').flagStrap({
        buttonSize: "btn-lg",
        buttonType: "btn-primary",
        labelMargin: "20px",
        scrollable: false,
        scrollableHeight: "350px"
    });
</script>--%>

<script>
    $(document).ready(function () {
        //$("#advtravel").click(function () {
        //    $("#advtravelss").slideToggle();
        //});


        $("#Traveller").click(function () {
            $("#box").show();
        });
        $("#serachbtn").click(function () {
            $("#box").hide();
        });

    });
</script>


<script>
    function moreoptions() {
        var x = document.getElementById("advtravelss");
        if (x.style.display === "none") {
            x.style.display = "block";
        } else {
            x.style.display = "none";
        }

        $('.flight-box').animate({
            scrollTop: $(".second").offset().top
        },
            'slow');
    }
</script>








<%--<script>
    $(document).ready(function () {
        $("#countries").msDropdown();
    })
</script>--%>



<script>
    $(".selectall").click(function () {
        $(".individual").prop("checked", $(this).prop("checked"));
    });
</script>


<script type="text/javascript">
    var myDate = new Date();
    var currDate = (myDate.getDate()) + '/' + (myDate.getMonth() + 1) + '/' + myDate.getFullYear();
    document.getElementById("txtDepDate").value = currDate;
    document.getElementById("hidtxtDepDate").value = currDate;
    document.getElementById("txtRetDate").value = currDate;
    document.getElementById("hidtxtRetDate").value = currDate;
    var UrlBase = '<%=ResolveUrl("~/") %>';
</script>

<%--<script type="text/javascript" src="<%=ResolveUrl("~/Scripts/jquery-1.4.4.min.js") %>"></script>--%>
<script type="text/javascript" src="<%=ResolveUrl("~/Scripts/jquery-ui-1.8.8.custom.min.js")%>"></script>
<script type="text/javascript" src="<%=ResolveUrl("~/Scripts/change.min.js") %>"></script>
<script type="text/javascript" src="<%=ResolveUrl("~/Scripts/search3.js?v=1.2")%>"></script>

<script type="text/javascript">

    $(function () {
        $("#CB_GroupSearch").click(function () {
            debugger;
            if ($(this).is(":checked")) {
                // $("#box").hide();
                $("#Traveller").hide();
                $("#rdbRoundTrip").attr("checked", true);
                $("#rdbOneWay").attr("checked", false);

            } else {
                // $("#box").show();
                $("#Traveller").show();
            }
        });
    });
</script>







<!--Rotate-->

<!--Rotate-->
