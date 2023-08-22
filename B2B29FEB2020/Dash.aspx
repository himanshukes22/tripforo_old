<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPageForDash.master" AutoEventWireup="false" CodeFile="Dash.aspx.vb" Inherits="Dash" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
        <script type="text/javascript" src="<%=ResolveUrl("~/Scripts/jquery-1.7.1.min.js")%>"></script>
    <script type="text/javascript" src="<%=ResolveUrl("~/Scripts/jquery-ui-1.8.8.custom.min.js")%>"></script>
    <link type="text/css" href="<%=ResolveUrl("~/Styles/jquery-ui-1.8.8.custom.css") %>" rel="stylesheet" />


    <style type="text/css">
       .row .col-sm-3 .card-3 {
    background: white;
    padding: 15px;
    color: white;
    border-radius: 5px;
}

        .row .col-sm-3 .card-3 h3 {
    margin-top: 0;
   /* margin-bottom: 25px;*/
    font-weight: bold;
}

        .row .col-sm-3 .card-3 h3 small {
    font-size: small;
    line-height: 3em;
    color: rgba(255, 255, 255, 0.8);
}

        .pull-right {
    float: right!important;
}

        .row .col-sm-3 .card-3 h3 span i.fa {
    color: rgba(255, 255, 255, 0.3);
}


    </style>


    <style type="text/css">
        /*!
// CSS only Responsive Tables
// http://dbushell.com/2016/03/04/css-only-responsive-tables/
// by David Bushell


*/


        .search-box{
    position: relative;
    background-color: #000;
}

.search-box::before{
    content: ' ';
    display: block;
    position: absolute;
    left: 0;
    top: 0;
    width: 100%;
    height: 100%;
    z-index: 0;
    opacity: 0.5;
    background: url("Images/plane/0639_FP151755.jpg") no-repeat center center;
    background-size: cover;
}

        .rtable {
 
            /*display: inline-block;*/
            vertical-align: top;
            max-width: 100%;
            overflow-x: auto;
            white-space: nowrap;
            border-collapse: collapse;
            border-spacing: 0;            
            border-radius: 5px;
            width: 100%;
        }

        .rtable,
        .rtable--flip tbody {
            -webkit-overflow-scrolling: touch;
            background: radial-gradient(left, ellipse, rgba(0, 0, 0, 0.2) 0%, rgba(0, 0, 0, 0) 75%) 0 center, radial-gradient(right, ellipse, rgba(0, 0, 0, 0.2) 0%, rgba(0, 0, 0, 0) 75%) 100% center;
            background-size: 10px 100%, 10px 100%;
            background-attachment: scroll, scroll;
            background-repeat: no-repeat;
        }

            .rtable td:first-child,
            .rtable--flip tbody tr:first-child {
                background-image: -webkit-gradient(linear, left top, right top, color-stop(50%, white), to(rgba(255, 255, 255, 0)));
                background-image: linear-gradient(to right, white 50%, rgba(255, 255, 255, 0) 100%);
                background-repeat: no-repeat;
                background-size: 20px 100%;
            }

            .rtable td:last-child,
            .rtable--flip tbody tr:last-child {
                background-image: -webkit-gradient(linear, right top, left top, color-stop(50%, white), to(rgba(255, 255, 255, 0)));
                background-image: linear-gradient(to left, white 50%, rgba(255, 255, 255, 0) 100%);
                background-repeat: no-repeat;
                background-position: 100% 0;
                background-size: 20px 100%;
            }

            .rtable th {
                font-size: 11px;
                text-align: left;
                text-transform: capitalize !important;
                background: #ffffff !important;
                 color: #000;
            }

            .rtable th,
            .rtable td {
                padding: 10px 12px;
                border: none;
            }
             .rtable  thead tr{
                border-bottom: 1px solid #bfbfbf !important;
            }
            .rtable tr {
                 border-bottom: 1px solid #e2e2e2;
            }
                                     
        .rtable--flip {
            display: -webkit-box;
            display: flex;
            overflow: hidden;
            background: none;
        }

            .rtable--flip thead {
                display: -webkit-box;
                display: flex;
                flex-shrink: 0;
                min-width: -webkit-min-content;
                min-width: -moz-min-content;
                min-width: min-content;
            }

            .rtable--flip tbody {
                display: -webkit-box;
                display: flex;
                position: relative;
                overflow-x: auto;
                overflow-y: hidden;
            }

            .rtable--flip tr {
                display: -webkit-box;
                display: flex;
                -webkit-box-orient: vertical;
                -webkit-box-direction: normal;
                flex-direction: column;
                min-width: -webkit-min-content;
                min-width: -moz-min-content;
                min-width: min-content;
                flex-shrink: 0;
            }

            .rtable--flip td,
            .rtable--flip th {
                display: block;
            }

            .rtable--flip td {
                background-image: none !important;
                border-left: 0;
            }

                .rtable--flip th:not(:last-child),
                .rtable--flip td:not(:last-child) {
                    border-bottom: 0;
                }

        /*!
// CodePen house keeping
*/
        /*body {
            margin: 0;
            padding: 25px;
            color: #494b4d;
            font-size: 14px;
            line-height: 20px;
        }*/

        h1, h2, h3 {
            margin: 0 0 10px 0;
            color: #1d97bf;
        }

        h1 {
            font-size: 25px;
            line-height: 30px;
        }

        h2 {
            font-size: 20px;
            line-height: 25px;
        }

        h3 {
            font-size: 16px;
            line-height: 20px;
        }

        table {
            margin-bottom: 30px;
        }

        a {
            color: #ff6680;
        }

        code {
            background: #fffbcc;
            font-size: 12px;
        }
    </style>

    <style type="text/css">
        @import url("//maxcdn.bootstrapcdn.com/font-awesome/4.3.0/css/font-awesome.min.css");
        @import url(https://fonts.googleapis.com/css?family=Lato:300,400,600,700|Play:400,700|Open+Sans+Condensed:300,600|Open+Sans:400,300,600,700);

        * {
            -moz-box-sizing: border-box;
            -webkit-box-sizing: border-box;
            box-sizing: border-box;
        }

        html {
            background-color: #ddd;
        }

        body {
            font-family: "Open Sans";
            font-size: 16px;
        }


        .row:before, .row:after {
            content: "";
            display: table;
        }

        .row:after {
            clear: both;
        }

        .row .box {
            /*float: left;
            position: relative;
            width: 18%;
            height: 100px;
            margin: 1%;
            color: White;
            overflow: hidden;
            -moz-border-radius: 4px;
            -webkit-border-radius: 4px;
            border-radius: 4px;
            -moz-box-shadow: 0 0 8px rgba(0, 0, 0, 0.3);
            -webkit-box-shadow: 0 0 8px rgba(0, 0, 0, 0.3);
            box-shadow: 0 0 8px rgba(0, 0, 0, 0.3);*/
            background: #ffffff;
            padding: 10px;
            border-radius: 10px;

        }

            /*.row .box.purple {
                background-color: #A48AEB;
            }

            .row .box.blue {
                background-color: #6BB8E8;
            }

            .row .box.orange {
                background-color: #EB9561;
            }

            .row .box.green {
                background-color: #53E082;
            }*/

            .row .box .value {
                position: absolute;
                top: 8px;
                right: 8px;
                font-size: 2.2rem;
            }

            .row .box .title {
                position: absolute;
                top: 50px;
                right: 8px;
                font-size: 12px;
                font-weight: 600;
            }

            /*.row .box .viewMore {
                position: absolute;
                left: 0;
                right: 0;
                bottom: 0px;
                text-align: center;
                height: 24px;
                line-height: 24px;
                vertical-align: middle;
                background-color: rgba(0, 0, 0, 0.1);
                font-size: 0.8rem;
                cursor: pointer;
                -moz-transition: all 0.2s linear;
                -o-transition: all 0.2s linear;
                -webkit-transition: all 0.2s linear;
                transition: all 0.2s linear;
            }*/

            .row .box:hover .viewMore {
                background-color: rgba(0, 0, 0, 0.2);
                bottom: 0px;
            }

         /*   .row .box:before {
                font-family: FontAwesome;
                content: "\f007";
                position: absolute;
                left: 6px;
                top: 6px;
                font-size: 4.0rem;
                color: rgba(255, 255, 255, 0.25);
            }

            .row .box.icon-check:before {
                content: "\f145";
            }
*/
            .row .box.icon-quote:before {
                content: "\f0d6";
            }

            .row .box.icon-graph:before {
                content: "\f201";
            }

            .row .box.icon-rail:before {
                content: "\f238";
            }
            .row .box.icon-hotel:before {
                content: "\f236";
            }
            .row .box.icon-bus:before {
                content: "\f207";
            }


    </style>

    <div class="section no-padd" style="display:none;">
        <div class="row search-box">

            <div class=" col-md-6">
             <%--   <label>From</label>--%>
                <div class="inputWithIcon">

                    <input type="text" name="From" id="From" placeholder="Start Date" class="form-control" readonly="readonly" />
                    <i class="fa fa-calendar fa-lg fa-fw"  aria-hidden="true"></i>
                </div>
            </div>

            <div class="col-md-6">
              <%--  <label>To</label>--%>
                <div class="inputWithIcon">
                    <input type="text" name="To" placeholder="End Date" id="To" class="form-control" readonly="readonly" />
                    <i class="fa fa-calendar fa-lg fa-fw" aria-hidden="true"></i>
                </div>
            </div>


            <div class="col-md-12 text-center">
              <%--  <label>&nbsp;</label>--%>
                <div class="inputWithIcon">
                    <div class="btn-search dash-search">
                     <asp:Button ID="btn_result" runat="server" class="btn cmn-btn" Text="Search Result" />
                        </div>
                    <%--<i class="fa fa-search fa-lg fa-fw" aria-hidden="true"></i>--%>
                </div>
            </div>









        </div>

    </div>

    <div class="dashboard-box section">
        <h3>How are you active users trending over time?    </h3>
        <div class="row">


            <div class="col-sm-3">
              <div class="card-3" style="background: #4d5d77;">
                <h3><small>Total Tickets <asp:Label ID="lbltktcount" runat="server">0</asp:Label></small>
                <span class="pull-right"><i class="fa fa-arrow-circle-o-up fa-2x"></i></span>
                  <br><asp:Label ID="lbltktcost" runat="server">0</asp:Label>
                </h3>
                     <hr style="border-top: 1px solid #e6e6e6;margin: 10px 0;">
                <small><a href="#">View more </a></small>
              </div>
            </div>



            
            <div class="col-sm-3">
              <div class="card-3" style="background: #00bd00;">
                <h3><small>Refunded Tickets <asp:Label ID="lblrefcount" runat="server">0</asp:Label></small>
                <span class="pull-right"><i class="fa fa-arrow-circle-o-up fa-2x"></i></span>
                  <br><asp:Label ID="lblrefcost" runat="server">0</asp:Label>
                </h3>
                     <hr style="border-top: 1px solid #e6e6e6;margin: 10px 0;">
                <small><a href="#">View more </a></small>
              </div>
            </div>



             <div class="col-sm-3">
              <div class="card-3" style="background: #e69500;">
                <h3><small>Cash Inflow <asp:Label ID="lblcashinflowcount" runat="server">0</asp:Label></small>
                <span class="pull-right"><i class="fa fa-arrow-circle-o-up fa-2x"></i></span>
                  <br><asp:Label ID="lblcashinflow" runat="server">0</asp:Label>
                </h3>
                     <hr style="border-top: 1px solid #e6e6e6;margin: 10px 0;">
                <small><a href="#">View more </a></small>
              </div>
            </div>
            



             <div class="col-sm-3">
              <div class="card-3" style="background: #ff3333;">
                <h3><small>Cash Outflow <asp:Label ID="lblcashoutflowcount" runat="server">0</asp:Label></small>
                <span class="pull-right"><i class="fa fa-arrow-circle-o-up fa-2x"></i></span>
                  <br><asp:Label ID="lblcashoutflow" runat="server">0</asp:Label>
                </h3>
                     <hr style="border-top: 1px solid #e6e6e6;margin: 10px 0;">
                <small><a href="#">View more </a></small>
              </div>
            </div>
            </div>
        <br />
           <div class="row">
              <div class="col-sm-3">
              <div class="card-3" style="background: #69557a;">
                <h3><small>P.G. Upload <asp:Label ID="lblpgcount" runat="server">0</asp:Label></small>
                <span class="pull-right"><i class="fa fa-arrow-circle-o-up fa-2x"></i></span>
                  <br><asp:Label ID="lblpgcost" runat="server">0</asp:Label>
                </h3>
                     <hr style="border-top: 1px solid #e6e6e6;margin: 10px 0;">
                <small><a href="#">View more </a></small>
              </div>
            </div>





             <div class="col-sm-3">
              <div class="card-3" style="background: orchid;">
                <h3><small>Rail Booking <asp:Label ID="Label1" runat="server">0</asp:Label></small>
                <span class="pull-right"><i class="fa fa-arrow-circle-o-up fa-2x"></i></span>
                  <br><asp:Label ID="Label2" runat="server">0</asp:Label>
                </h3>
                     <hr style="border-top: 1px solid #e6e6e6;margin: 10px 0;">
                <small><a href="#">View more </a></small>
              </div>
            </div>





             <div class="col-sm-3">
              <div class="card-3" style="background: cornflowerblue;">
                <h3><small>Hotel Booking <asp:Label ID="Label3" runat="server">0</asp:Label></small>
                <span class="pull-right"><i class="fa fa-arrow-circle-o-up fa-2x"></i></span>
                  <br><asp:Label ID="Label4" runat="server">0</asp:Label>
                </h3>
                     <hr style="border-top: 1px solid #e6e6e6;margin: 10px 0;">
                <small><a href="#">View more </a></small>
              </div>
            </div>




              <div class="col-sm-3">
              <div class="card-3" style="background: darkslateblue;">
                <h3><small>Bus Booking <asp:Label ID="Label5" runat="server">0</asp:Label></small>
                <span class="pull-right"><i class="fa fa-arrow-circle-o-up fa-2x"></i></span>
                  <br><asp:Label ID="Label6" runat="server">0</asp:Label>
                </h3>
                     <hr style="border-top: 1px solid #e6e6e6;margin: 10px 0;">
                <small><a href="#">View more </a></small>
              </div>
            </div>

          <%--   <div class="row">
                    <div class="box  icon-rail" style="width: 77% !important;margin-left: 72px;">
                <div class="value" data-value="250">₹ 0</div>
                <div class="title">Rail Booking - 0</div>
                <div class="viewMore">View more »</div>
            </div>
                </div>--%>
<%--            <div class="box purple icon-check">
                <div class="value" data-value="250">₹<asp:Label ID="lbltktcost" runat="server">0</asp:Label></div>
                <div class="title">Total Tickets -<asp:Label ID="lbltktcount" runat="server">0</asp:Label></div>
                <div class="viewMore">View more »</div>
            </div>--%>
       <%--     <div class="">
                <div class="" data-value="300">₹<asp:Label ID="lblrefcost" runat="server">0</asp:Label></div>
                <div class="">Refunded Tickets - <asp:Label ID="lblrefcount" runat="server">0</asp:Label></div>
                <div class="">View more »</div>
            </div>--%>
          <%--  <div class="  icon-quote">
                <div class="" data-value="40">₹ <asp:Label ID="lblcashinflow" runat="server">0</asp:Label></div>
                <div class="">Cash Inflow - <asp:Label ID="lblcashinflowcount" runat="server">0</asp:Label></div>
                <div class="">View more »</div>
            </div>--%>
        <%--    <div class="  icon-quote">
                <div class="" data-value="25">₹ <asp:Label ID="lblcashoutflow" runat="server">0</asp:Label></div>
                <div class="">Cash Outflow - <asp:Label ID="lblcashoutflowcount" runat="server">0</asp:Label></div>
                <div class="">View more »</div>
            </div>--%>
            <%--<div class="  icon-quote">
                <div class="" data-value="25">₹ <asp:Label ID="lblpgcost" runat="server">0</asp:Label></div>
                <div class="">P.G. Upload - <asp:Label ID="lblpgcount" runat="server">0</asp:Label></div>
                <div class="">View more »</div>
            </div>--%>
        </div>


    </div>

    <div class="section table-dash" style="display:none;">
        <h3>Booking Summary</h3>
                <table class="rtable">
                    <thead>
                        <tr>
                            <th>Airlines</th>
                            <th>Airline Code</th>
                            <th>Gross Sale</th>
                            <th>Comission</td>
                            <th>Net Sale</th>
                            
                        </tr>
                    </thead>
                    <tbody id="tbbody" runat="server">                          
                        <tr>
                        <td>Vistara</td>
                        <td>#10128</td>
                        <td>₹22,000</td>
                        <td>15%</td>
                        <td>₹1,02,000</td>
                      </tr>                   
                        <tr>
                        <td>Air Asia</td>
                        <td>#10128</td>
                        <td>₹22,000</td>
                        <td>15%</td>
                        <td>₹1,02,000</td>
                      </tr>                   
                        <tr>
                        <td>Indigo</td>
                        <td>#10128</td>
                        <td>₹22,000</td>
                        <td>15%</td>
                        <td>₹1,02,000</td>
                      </tr>                   
                        <tr>
                        <td>Spice jet</td>
                        <td>#10128</td>
                        <td>₹22,000</td>
                        <td>15%</td>
                        <td>₹1,02,000</td>
                      </tr>

                    </tbody>
                </table>

            <%--<div class="col-md-3">--%>
               <%-- <div class="row">
                    <div class="box  icon-rail" style="width: 77% !important;margin-left: 72px;">
                <div class="value" data-value="250">₹ 0</div>
                <div class="title">Rail Booking - 0</div>
                <div class="viewMore">View more »</div>
            </div>
                </div>--%>

             <%--     <div class="row">
                    <div class="box  icon-hotel" style="width: 77% !important;margin-left: 72px;">
                <div class="value" data-value="250">₹ 0</div>
                <div class="title">Hotel Booking - 0</div>
                <div class="viewMore">View more »</div>
            </div>
                </div>

                  <div class="row">
                    <div class="box  icon-bus" style="width: 77% !important;margin-left: 72px;">
                <div class="value" data-value="250">₹ 0</div>
                <div class="title">Bus Booking - 0</div>
                <div class="viewMore">View more »</div>
            </div>
                </div>--%>

        </div>


     <script type="text/javascript" src="<%=ResolveUrl("~/Scripts/AgencySearch.js") %>"></script>
    <script>
        (function () {
            $(".box").velocity("transition.slideDownIn", {
                stagger: 250,
                drag: true,
                duration: 2000,
                complete: function () {
                    return $(".value").each(function (index, item) {
                        var val, value;
                        value = $(item).data("value");
                        val = parseInt(value, 10);
                        return $({
                            someValue: 0
                        }).
                        animate({
                            someValue: val
                        },
                        {
                            duration: 1000,
                            easing: 'swing',
                            step: function () {
                                return $(item).text(Math.round(this.someValue));
                            }
                        });

                    });
                }
            });


        }).call(this);


        //# sourceURL=coffeescript
    </script>

    <script>
        /*!
 * iOS doesn't respect the meta viewport tag inside a frame
 * add a link to the debug view (for demo purposes only)
 */
        if (/(iPhone|iPad|iPod)/gi.test(navigator.userAgent) && window.location.pathname.indexOf('/full') > -1) {
            var p = document.createElement('p');
            p.innerHTML = '<a target="_blank" href="https://s.codepen.io/dbushell/debug/wGaamR"><b>Click here to view this demo properly on iOS devices (remove the top frame)</b></a>';
            document.body.insertBefore(p, document.body.querySelector('h1'));
        }
    </script>


</asp:Content>

