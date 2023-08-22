<%@ Control Language="VB" AutoEventWireup="false" CodeFile="FltSearchmdf.ascx.vb" Inherits="UserControl_FltSearchmdf" %>

<%--<%@ Register Src="~/UserControl/HotelSearch.ascx" TagPrefix="uc1" TagName="HotelSearch" %>
<%@ Register Src="~/UserControl/HotelDashboard.ascx" TagPrefix="uc1" TagName="HotelDashboard" %>--%>

<style type="text/css">
    /*.dropdown-container {
  display: flex;
  flex-direction: column;
  justify-content: center;
  align-items: center;
  margin: 40px 0 0 0;
}*/

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

            .dropdown-menu span:hover {
                background: #eee;
            }

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


<style type="text/css">
    .minus, .plus {
        width: 35px;
        height: 35px;
        background: #404040;
        border-radius: 50%;
        padding: 8px 5px 8px 5px;
        /* border: 1px solid #ddd; */
        display: inline-block;
        vertical-align: middle;
        text-align: center;
        cursor: pointer;
    }

    .inp {
        width: 30px;
        text-align: center;
        color: #000;
        background: none;
        border: none;
    }

    .main_dv {
        width: 100%;
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
        background: #ff0000;
        float: right;
        text-align: center;
        padding: 4px 12px;
        display: block;
        color: #fff;
        font-size: 11px;
        border-radius: 3px;
        -webkit-border-radius: 3px;
        -moz-border-radius: 3px;
    }

    .innr_pnl {
        width: 200px;
        position: relative;
        /*padding: 10px;*/
    }

    .dropdown-content-n {
        /*display:none;*/
        position: absolute;
        background-color: #fff;
        width: 200px;
        padding: 10px;
        box-shadow: 0 0 20px 0 rgba(0,0,0,0.45);
        z-index: 1;
        top: 167px;
        box-sizing: content-box;
        -webkit-box-sizing: content-box;
        right: 56px
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

    .clear {
        clear: both;
    }
</style>

<style>
    .circle {
        border-radius: 50%;
        margin: -32px;
        font-size: 2em;
        z-index: 1011;
        position: relative;
        margin-left: -254px;
        margin-top: 26px;
        cursor: pointer;
       
    }

    .fa-exchange {
        
        color: #FFFFFF;
        padding: 6px;
        font-size: 15px;
    }


    @media only screen and (max-width: 600px) {
        .dis {
            display: none;
        }
    }
</style>







<div class="kunal">

    <div id="hide" style="color:#fff;color: #fff;position: absolute;right: 24px;top: 13px;">X</div>

    <div class="theme-search-area-options clearfix" style="margin-top: 10px !important; width: 100% !important;">
        <div class="col-md-2 col-xs-4 nopad text-search" style="width: 111px !important;">
            <label class=" active">
                <input type="radio" name="TripType" value="rdbOneWay" id="rdbOneWay" checked="checked" />
                One Way</label>
        </div>
        <div class="col-md-2 col-xs-4 nopad text-search">
            <label class="">
                <input type="radio" name="TripType" value="rdbRoundTrip" id="rdbRoundTrip" />
                Round Trips</label>
        </div>
        <%-- <div class="  col-md-1 col-xs-4 nopad text-search">
            <label class="">
                <input type="radio" name="TripType" value="rdbMultiCity" id="rdbMultiCity" />
                Multi-City
            </label>
        </div>--%>

        <script>
            $(document).ready(function () {
                var selector = '.topways div label';
                $(selector).bind('click', function () {
                    $(selector).removeClass('active');
                    $(this).addClass('active');
                });

            });
        </script>
    </div>

    <div class="theme-search-area-form" id="hero-search-form">
        <div class="row" data-gutter="10">
            <div class="col-md-5 ">
                <div class="row" data-gutter="10">
                    <div class="onewayss text-search mltcs col-md-6 ">
                        <div class="theme-search-area-section first theme-search-area-section-curved theme-search-area-section-sm theme-search-area-section-fade-white theme-search-area-section-no-border">
                            <label class="theme-search-area-section-label _op-06">From</label>
                            <div class="theme-search-area-section-inner">
                                <i class="theme-search-area-section-icon lin lin-location-pin"></i>
                                <input type="text" name="txtDepCity1" class="theme-search-area-section-input2 typeahead" placeholder="Departure" onclick="this.value = '';" id="txtDepCity1"/>
                                <input type="hidden" id="hidtxtDepCity1" name="hidtxtDepCity1" value="" />
                            </div>
                        </div>
                    </div>

                    <i class="fa fa-exchange circle interchange dis" id="change" style=""></i>
                    <div class="onewayss text-search mltcs col-md-6 ">
                        <div class="theme-search-area-section theme-search-area-section-curved theme-search-area-section-sm theme-search-area-section-fade-white theme-search-area-section-no-border">
                            <label class="theme-search-area-section-label _op-06">To</label>
                            <div class="theme-search-area-section-inner">
                                <i class="theme-search-area-section-icon lin lin-location-pin"></i>
                                <input type="text" name="txtArrCity1" onclick="this.value = '';" id="txtArrCity1" class="theme-search-area-section-input2 typeahead" placeholder="Destination"  style="font-size: 12px;"/>
                                <input type="hidden" id="hidtxtArrCity1" name="hidtxtArrCity1" value="" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <div class="col-md-6 ">
                <div class="row" data-gutter="10">
                    <div class="col-md-4 " id="one">
                        <div class="theme-search-area-section theme-search-area-section-curved theme-search-area-section-sm theme-search-area-section-fade-white theme-search-area-section-no-border">
                            <label class="theme-search-area-section-label _op-06">Depart</label>
                            <div class="theme-search-area-section-inner">
                                <i class="theme-search-area-section-icon lin lin-calendar"></i>
                                <input type="text" class="theme-search-area-section-input2 datePickerStart _mob-h" placeholder="dd/mm/yyyy" name="txtDepDate" id="txtDepDate" value=""
                                    readonly="readonly"  style="font-size: 12px;"/>
                                <input type="hidden" class="theme-search-area-section-input _desk-h mobile-picker" name="hidtxtDepDate" id="hidtxtDepDate" value="" />
                            </div>
                        </div>
                    </div>

                    <div class="col-md-4 " id="Return">
                        <div class="theme-search-area-section theme-search-area-section-curved theme-search-area-section-sm theme-search-area-section-fade-white theme-search-area-section-no-border" id="trRetDateRow">
                            <label class="theme-search-area-section-label _op-06">Arrival</label>
                            <div class="theme-search-area-section-inner">
                                <i class="theme-search-area-section-icon lin lin-calendar"></i>
                                <input type="text" placeholder="dd/mm/yyyy" name="txtRetDate" id="txtRetDate" class="theme-search-area-section-input2 datePickerEnd _mob-h" value="" readonly="readonly" style="font-size: 12px;" />
                                <input type="hidden" name="hidtxtRetDate" id="hidtxtRetDate" value="" />
                            </div>
                        </div>
                    </div>


                    <div class="col-md-4 " id="Traveller">
                        <div class="theme-search-area-section theme-search-area-section-curved theme-search-area-section-sm theme-search-area-section-fade-white theme-search-area-section-no-border quantity-selector" data-increment="Passengers">
                            <label class="theme-search-area-section-label _op-06">Passengers</label>
                            <div class="theme-search-area-section-inner">
                                <i class="theme-search-area-section-icon lin lin-people"></i>

                                <input type="text" class="theme-search-area-section-input2 div" id="sapnTotPax" placeholder=" Traveller">
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <div class="col-md-1 ">
                <button type="button" id="btnSearch" name="btnSearch" value="Search" class="theme-search-area-submit _tt-uc theme-search-area-submit-curved theme-search-area-submit-sm theme-search-area-submit-no-border theme-search-area-submit-primary" style="width: 70px;">Search</button>
            </div>




            <div style="display: none;" id="two">
                <div class="onewayss col-md-4 nopad text-search mltcs" id="DivDepCity2">
                    <div class="form-group">
                        <div class="input-group">

                            <input type="text" name="txtDepCity2" class="form-control" placeholder="Enter Your Departure City" id="txtDepCity2" />
                            <input type="hidden" id="hidtxtDepCity2" name="hidtxtDepCity2" value="" />
                        </div>
                    </div>
                </div>

                <div class="onewayss col-md-4 nopad text-search mltcs">
                    <div class="form-group">
                        <div class="input-group">

                            <input type="text" name="txtArrCity2" class="form-control" placeholder="Enter Your Destination City" id="txtArrCity2" />
                            <input type="hidden" id="hidtxtArrCity2" name="hidtxtArrCity2" value="" />
                        </div>
                    </div>
                </div>
                <div class="col-md-2 nopad text-search mrgs" id="DivArrCity2">
                    <div class="form-group">
                        <div class="input-group">

                            <input type="text" name="txtDepDate2" id="txtDepDate2" class=" form-control" placeholder="dd/mm/yyyy" readonly="readonly" value="" />
                            <input type="hidden" name="hidtxtDepDate2" id="hidtxtDepDate2" value="" />
                        </div>
                    </div>
                </div>
            </div>

            <div style="display: none;" id="three">

                <div class="onewayss col-md-4 nopad text-search mltcs" id="DivDepCity3">
                    <div class="form-group">

                        <div class="input-group">

                            <input type="text" name="txtDepCity3" class="form-control" placeholder="Enter Your Departure City" id="txtDepCity3" />
                            <input type="hidden" id="hidtxtDepCity3" name="hidtxtDepCity3" value="" />
                        </div>
                    </div>
                </div>
                <div class="onewayss col-md-4 nopad text-search mltcs">
                    <div class="form-group">

                        <div class="input-group">

                            <input type="text" name="txtArrCity3" class="form-control" placeholder="Enter Your Destination City" id="txtArrCity3" />
                            <input type="hidden" id="hidtxtArrCity3" name="hidtxtArrCity3" value="" />
                        </div>
                    </div>
                </div>
                <div class="col-md-2 nopad text-search mrgs" id="DivArrCity3">
                    <div class="form-group">

                        <div class="input-group">


                            <input type="text" name="txtDepDate3" id="txtDepDate3" class="form-control" placeholder="dd/mm/yyyy" readonly="readonly" />
                            <input type="hidden" name="hidtxtDepDate3" id="hidtxtDepDate3" value="" />
                        </div>
                    </div>
                </div>
            </div>

            <div style="display: none;" id="four">
                <div class="onewayss col-md-4 nopad text-search mltcs" id="DivDepCity4">
                    <div class="form-group">

                        <div class="input-group">

                            <input type="text" name="txtDepCity4" class="form-control" placeholder="Enter Your Departure City" id="txtDepCity4" />
                            <input type="hidden" id="hidtxtDepCity4" name="hidtxtDepCity4" value="" />
                        </div>
                    </div>
                </div>
                <div class="onewayss col-md-4 nopad text-search mltcs">
                    <div class="form-group">

                        <div class="input-group">
                            <div class="input-group-addon">
                                <i class="fa fa-map-marker fa-lg"></i>
                            </div>
                            <input type="text" name="txtArrCity4" class="form-control" placeholder="Enter Your Destination City" id="txtArrCity4" />
                            <input type="hidden" id="hidtxtArrCity4" name="hidtxtArrCity4" value="" />
                        </div>
                    </div>
                </div>
                <div class="col-md-2 nopad text-search mrgs" id="DivArrCity4">
                    <div class="form-group">
                        <div class="input-group">
                            <div class="input-group-addon">
                                <i class="fa fa-calendar"></i>
                            </div>
                            <input type="text" name="txtDepDate4" id="txtDepDate4" class="form-control" placeholder="dd/mm/yyyy" readonly="readonly" />
                            <input type="hidden" name="hidtxtDepDate4" id="hidtxtDepDate4" value="" />
                        </div>
                    </div>
                </div>
            </div>

            <div style="display: none;" id="five">
                <div class="onewayss col-md-4 nopad text-search mltcs" id="DivDepCity5">
                    <div class="form-group">
                        <div class="input-group">
                            <div class="input-group-addon">
                                <i class="fa fa-map-marker fa-lg"></i>
                            </div>
                            <input type="text" name="txtDepCity5" class="form-control" placeholder="Enter Your Departure City" id="txtDepCity5" />
                            <input type="hidden" id="hidtxtDepCity5" name="hidtxtDepCity5" value="" />
                        </div>
                    </div>
                </div>
                <div class="onewayss col-md-4 nopad text-search mltcs">
                    <div class="form-group">
                        <div class="input-group">
                            <div class="input-group-addon">
                                <i class="fa fa-map-marker fa-lg"></i>
                            </div>
                            <input type="text" name="txtArrCity5" class="form-control" placeholder="Enter Your Destination City" onclick="this.value = '';" id="txtArrCity5" />
                            <input type="hidden" id="hidtxtArrCity5" name="hidtxtArrCity5" value="" />
                        </div>
                    </div>
                </div>
                <div class="col-md-2 nopad text-search mrgs" id="DivArrCity5">
                    <div class="form-group">
                        <div class="input-group">
                            <div class="input-group-addon">
                                <i class="fa fa-calendar"></i>
                            </div>
                            <input type="text" name="txtDepDate5" id="txtDepDate5" class="form-control" placeholder="dd/mm/yyyy" readonly="readonly" />
                            <input type="hidden" name="hidtxtDepDate5" id="hidtxtDepDate5" value="" />
                        </div>
                    </div>
                </div>
            </div>

            <div style="display: none;" id="six">
                <div class="onewayss col-md-4 nopad text-search mltcs" id="DivDepCity6">
                    <div class="form-group">
                        <div class="input-group">
                            <div class="input-group-addon">
                                <i class="fa fa-map-marker fa-lg"></i>
                            </div>
                            <input type="text" name="txtDepCity6" class="form-control" placeholder="Enter Your Departure City" id="txtDepCity6" />
                            <input type="hidden" id="hidtxtDepCity6" name="hidtxtDepCity6" value="" />
                        </div>
                    </div>
                </div>

                <div class="onewayss col-md-4 nopad text-search mltcs">
                    <div class="form-group">
                        <div class="input-group">
                            <div class="input-group-addon">
                                <i class="fa fa-map-marker fa-lg"></i>
                            </div>
                            <input type="text" name="txtArrCity6" class="form-control" placeholder="Enter Your Destination City" onclick="this.value = '';" id="txtArrCity6" />
                            <input type="hidden" id="hidtxtArrCity6" name="hidtxtArrCity6" value="" />
                        </div>
                    </div>
                </div>
                <div class="col-md-2 nopad text-search mrgs" id="ArrCity6">
                    <div class="form-group">
                        <div class="input-group">
                            <div class="input-group-addon">
                                <i class="fa fa-calendar"></i>
                            </div>
                            <input type="text" name="txtDepDate6" id="txtDepDate6" class="form-control" placeholder="dd/mm/yyyy" readonly="readonly" />
                            <input type="hidden" name="hidtxtDepDate6" id="hidtxtDepDate6" value="" />
                        </div>
                    </div>
                </div>
            </div>



            <div class="row col-md-5 col-xs-12 pull-right" id="add">
                <div class="col-md-4 col-xs-4 text-search text-right">
                    <a id="plus" class="pulse text-search">Add City</a>
                </div>
                <div class="col-md-4 col-xs-4 text-search  text-right">
                    <a id="minus" class="pulse text-search">Remove City</a>
                </div>
            </div>

        </div>
    </div>

</div>





<div class="row" style="display: none;">
    <div class="text-search col-md-5 col-xs-12" style="padding-bottom: 10px; cursor: pointer; margin-top: -30px;" id="advtravel">Advanced options <i class="fa fa-chevron-right" aria-hidden="true"></i></div>
    <div class="col-md-12 advopt" id="advtravelss" style="display: none;">
        <div class="col-md-3 nopad text-search">
            <div class="form-group">
                <label for="exampleInputEmail1">
                    Airlines</label>
                <input type="text" placeholder="Search By Airlines" class="form-control" name="txtAirline" value="" id="txtAirline" />
                <input type="hidden" id="hidtxtAirline" name="hidtxtAirline" value="" />

            </div>
        </div>
        <div class="col-md-3 col-xs-12 text-search">
            <div class="form-group">
                <label for="exampleInputEmail1">
                    Class Type</label>
                <select name="Cabin" class="form-control" id="Cabin">
                    <option value="" selected="selected">--All--</option>
                    <option value="C">Business</option>
                    <option value="Y">Economy</option>
                    <option value="F">First</option>
                    <option value="W">Premium Economy</option>
                </select>

            </div>
        </div>
    </div>
</div>

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
    $(document).ready(function () {
        //$("#hide").click(function () {
        //    $(".kunal").hide();
        //});
        //$("#show-hide-mob").click(function () {
        //    $(".kunal").show();
        //});
    });
</script>


<script type="text/javascript">
    $(document).ready(function () {
        $('.minus').click(function () {
            var $input = $(this).parent().find('input');
            var count = parseInt($input.val()) - 1;
            count = count <= 0 ? 0 : count;
            $input.val(count);
            $input.change();
            return false;
        });
        $('.plus').click(function () {
            var $input = $(this).parent().find('input');
            $input.val(parseInt($input.val()) + 1);
            $input.change();
            return false;
        });
    });
</script>

<script>
    $(document).ready(function () {
        $("#advtravel").click(function () {
            $("#advtravelss").slideToggle();
        });


        $("#Traveller").click(function () {
            $("#box").show();
        });
        $("#serachbtn").click(function () {
            $("#box").hide();
        });

    });
</script>

<div id="box" style="display: none;">
    <div id="div_Adult_Child_Infant" class="dropdown-content-n myText">
        <div class="innr_pnl">
            <div class="main_dv">
                <div class="ttl_col">
                    <p>Adult</p>
                    <span>(12+ yrs)</span>
                </div>
                <div class="number">
                    <span class="minus">-</span>
                    <input type="text" class="inp" value="1" name="Adult" id="Adult" />
                    <span class="plus">+</span>
                </div>

            </div>
            <div class="main_dv">
                <div class="ttl_col">
                    <p>Children</p>
                    <span>(2+ 12 yrs)</span>
                </div>

                <div class="number">
                    <span class="minus">-</span>
                    <input type="text" class="inp" value="0" name="Child" id="Child" />
                    <span class="plus">+</span>
                </div>

            </div>
            <div class="main_div">

                <div class="ttl_col">
                    <p>Infant(s)</p>
                    <span>(below 2 yrs)</span>
                </div>

                <div class="number">
                    <span class="minus">-</span>
                    <input type="text" class="inp" value="0" name="Infant" id="Infant" />
                    <span class="plus">+</span>
                </div>

            </div>

            <div class="clear"></div>

            <a href="#" onclick="plus()" class="dn_btn" id="serachbtn">Done</a>


        </div>
    </div>
</div>


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



<script type="text/javascript">
    function plus() {
        document.getElementById("sapnTotPax").value = (parseInt(document.getElementById("Adult").value.split(' ')[0]) + parseInt(document.getElementById("Child").value.split(' ')[0]) + parseInt(document.getElementById("Infant").value.split(' ')[0])).toString() + ' Traveller';
    }
    plus();
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


<script type="text/javascript" src="<%=ResolveUrl("~/Scripts/jquery-ui-1.8.8.custom.min.js")%>"></script>
<script type="text/javascript" src="<%=ResolveUrl("~/Scripts/change.min.js") %>"></script>
<script type="text/javascript" src="<%=ResolveUrl("~/Scripts/search3.js") %>"></script>

<script type="text/javascript">

    $(function () {
        $("#CB_GroupSearch").click(function () {

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



