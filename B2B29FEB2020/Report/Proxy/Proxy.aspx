<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPageForDash.master" AutoEventWireup="false"
    CodeFile="Proxy.aspx.vb" Inherits="Report_Proxy_Proxy" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="../../JS/JScript.js" type="text/javascript"></script>
    <link type="text/css" href="<%=ResolveUrl("~/Styles/jquery-ui-1.8.8.custom.css") %>" rel="stylesheet" />
    <script type="text/javascript" src="<%=ResolveUrl("~/Scripts/AgencySearch.js") %>"></script>

    <style type="text/css">
        .container-box {
            background-color: #1fc8db;
            background-image: linear-gradient(141deg, #58a3bf 0%, #5b8fa2 51%, #b4e2f3 75%);
            width: 100%;
            height: 300px;
        }

        table {
            background: none !important;
        }

        .ui-datepicker-month, .ui-datepicker-year {
            color: #fff !important;
        }
    </style>

    <script>
       
        function Show(obj) {

            if (obj.checked) {
                //document.getElementById("txtRetDate").style.display = "block";
                document.getElementById("td_ret").style.display = "block";
                document.getElementById("td_time").style.display = "block";
                document.getElementById("ctl00_ContentPlaceHolder1_ddl_ReturnAnytime").style.display = "block";
                document.getElementById("rtn").style.display = "block";
            }
        }
        function Hide(obj) {
            if (obj.checked) {
                //document.getElementById("txtRetDate").style.display = "none";
                document.getElementById("td_ret").style.display = "none";
                document.getElementById("td_time").style.display = "none";
                document.getElementById("ctl00_ContentPlaceHolder1_ddl_ReturnAnytime").style.display = "none";
                document.getElementById("rtn").style.display = "none";
            }
        }
    </script>

     <ol class="breadcrumb-arrow">
        <li><a href="/Search.aspx"><i class="fa fa-home"></i></a></li>
        <li><a href="#">Flight</a></li>
        <li><a href="DashboardForOffline.aspx">Offline Request</a></li>
                 <li><a href="Proxy.aspx">Genrate Request</a></li>

     
       <%--<li class="dropdown">
                <a href="#lala" class="dropdown-toggle" data-toggle="dropdown" >
                   Manage Booking
                   <b class="caret"></b>
                </a>
                <ul class="dropdown-menu" role="listbox">
                   <li class="divider"></li>
                   <li><a href="Proxy.aspx" role="option">View Request</a></li>
                   <li class="divider"></li>
                   <li><a href="ProxyPaxUpdate.aspx" role="option">Update Request</a></li>
                </ul>
             </li>--%>

    </ol>
    
    <div class="card-main">

        <div class="inner-box proxy-box" style="padding: 15px;">

            <div class="row">
                <div class="col-md-4">
                    <%--Trip Type :--%>
                    <div class="row">
                        <div class="col-md-4">
                            <label class="radio-inline">
                                <asp:RadioButton ID="RB_OneWay" runat="server" Checked="true" GroupName="Trip" onclick="Hide(this)"
                                    Text="One Way" /></label>
                        </div>
                        <div class="col-md-6">
                            <label class="radio-inline">
                                <asp:RadioButton ID="RB_RoundTrip" runat="server" GroupName="Trip" onclick="Show(this)"
                                    Text="Round Trip" /></label>
                        </div>


                    </div>
                </div>
                <div class="col-md-3" style="display:none;">

                    <div class="row">
                        <div class="col-md-12 booking">
                            <%-- <label>Booking Tyle:</label>--%>
                            <div class="theme-search-area-section theme-search-area-section-line">
                                <div class="theme-search-area-section-inner">
                                    <i class="theme-search-area-section-icon lin lin-map"></i>
                                    <asp:DropDownList ID="RBL_Booking" runat="server" CssClass="theme-search-area-section-input" Style="color: #000">
                                        <asp:ListItem>Select Booking Type</asp:ListItem>
                                        <asp:ListItem>Normal</asp:ListItem>


<%--                                        <asp:ListItem>LTC</asp:ListItem>--%>

                                        <asp:ListItem>Group</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                    </div>

                </div>

            </div>

            <div class="row">
                <div class="col-md-3 col-xs-6">
                    <div class="form-group">
                        <%--<label>Leaving Form</label>--%>
                        <div class="theme-search-area-section theme-search-area-section-line">
                            <div class="theme-search-area-section-inner">
                                <i class="theme-search-area-section-icon lin lin-map"></i>
                                <input id="txtDepCity1" name="txtDepCity1" type="text" placeholder="Leaving From" class="theme-search-area-section-input" />
                                <input type="hidden" id="hidtxtDepCity1" name="hidtxtDepCity1" value="" />
                            </div>
                        </div>
                    </div>
                </div>



                <div class="col-md-3  col-xs-6">
                    <div class="form-group">
                        <%--<label>Leaving To</label>--%>
                        <div class="theme-search-area-section theme-search-area-section-line">
                            <div class="theme-search-area-section-inner">
                                <i class="theme-search-area-section-icon lin lin-map"></i>
                                <input id="txtArrCity1" name="txtArrCity1" type="text" placeholder="Going To" class="theme-search-area-section-input" />
                                <input type="hidden" id="hidtxtArrCity1" name="hidtxtArrCity1" value="" class="form-control" />
                            </div>
                        </div>
                    </div>
                </div>




                <div class="col-md-3  col-xs-6">
                    <div class="form-group">
                        <%--  <label>Departure Date</label>--%>
                        <div class="theme-search-area-section theme-search-area-section-line">
                            <div class="theme-search-area-section-inner">
                                <i class="theme-search-area-section-icon lin lin-calendar"></i>
                                <input type="text" name="txtDepDate" id="From" placeholder="Departure Date" class="theme-search-area-section-input blockbackdate" value=""
                                    readonly="readonly" />
                                <input type="hidden" name="hidtxtDepDate" id="hidtxtDepDate" value="" />
                            </div>
                        </div>
                    </div>
                </div>





                <div class="col-md-3 col-xs-6" id="td_ret" style="display: none;">

                    <div class="form-group">
                        <%--<label style="color: black">Return Date</label>--%>
                        <div class="theme-search-area-section theme-search-area-section-line">
                            <div class="theme-search-area-section-inner">
                                <i class="theme-search-area-section-icon lin lin-calendar"></i>
                                <input type="text" name="txtRetDate" placeholder="Return Date" id="To" class=" theme-search-area-section-input blockbackdate" value=""
                                    readonly="readonly" />

                                <input type="hidden" name="hidtxtRetDate" id="hidtxtRetDate" value="" />
                            </div>
                        </div>
                    </div>

                </div>
            </div>




            <div class="row">

                <div class="col-md-3  col-xs-6">
                    <div class="form-group">
                        <%--<label>Time </label>--%>
                        <div class="theme-search-area-section theme-search-area-section-line">
                            <div class="theme-search-area-section-inner">
                                <i class="theme-search-area-section-icon lin lin-clock"></i>
                                <asp:DropDownList ID="ddl_DepartAnytime" runat="server" class="theme-search-area-section-input">
                                    <asp:ListItem>Select Time</asp:ListItem>
                                    <asp:ListItem>Anytime</asp:ListItem>
                                    <asp:ListItem>Morning(0.00-12:00)</asp:ListItem>
                                    <asp:ListItem>Midday(10:00-14:00)</asp:ListItem>
                                    <asp:ListItem>Afternoon(12:00-17:00)</asp:ListItem>
                                    <asp:ListItem>Evening(17:00-20:00)</asp:ListItem>
                                    <asp:ListItem>Night(20:00-23:59)</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                </div>



                <div class="col-md-3  col-xs-6" id="rtn" style="display: none;">
                    <div class="form-group">
                        <%--  <label>Time</label>--%>
                        <div class="theme-search-area-section theme-search-area-section-line">
                            <div class="theme-search-area-section-inner">
                                <i class="theme-search-area-section-icon lin lin-clock"></i>
                                <asp:DropDownList ID="ddl_ReturnAnytime" runat="server" CssClass="theme-search-area-section-input">
                                    <asp:ListItem>Select Return Time</asp:ListItem>
                                    <asp:ListItem>Anytime</asp:ListItem>
                                    <asp:ListItem>Morning(0.00-12:00)</asp:ListItem>
                                    <asp:ListItem>Midday(10:00-14:00)</asp:ListItem>
                                    <asp:ListItem>Afternoon(12:00-17:00)</asp:ListItem>
                                    <asp:ListItem>Evening(17:00-20:00)</asp:ListItem>
                                    <asp:ListItem>Night(20:00-23:59)</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>

                </div>



                <div class="col-md-3 col-xs-6">
                    <div class="form-group">
                        <%--<label>Adult(>12 yrs)</label>--%>
                        <div class="theme-search-area-section theme-search-area-section-line">
                            <div class="theme-search-area-section-inner">
                                <i class="theme-search-area-section-icon lin lin-user"></i>
                                <%--                                <asp:DropDownList ID="ddl_Adult" runat="server" CssClass="theme-search-area-section-input">
                                    <asp:ListItem>Adult(12+ yrs)</asp:ListItem>
                                    <asp:ListItem>1</asp:ListItem>
                                    <asp:ListItem>2</asp:ListItem>
                                    <asp:ListItem>3</asp:ListItem>
                                    <asp:ListItem>4</asp:ListItem>
                                    <asp:ListItem>5</asp:ListItem>
                                    <asp:ListItem>6</asp:ListItem>
                                    <asp:ListItem>7</asp:ListItem>
                                    <asp:ListItem>8</asp:ListItem>
                                    <asp:ListItem>9</asp:ListItem>
                                    <asp:ListItem>10</asp:ListItem>
                                    <asp:ListItem>11</asp:ListItem>
                                    <asp:ListItem>12</asp:ListItem>
                                    <asp:ListItem>13</asp:ListItem>
                                    <asp:ListItem>14</asp:ListItem>
                                    <asp:ListItem>15</asp:ListItem>
                                    <asp:ListItem>16</asp:ListItem>
                                    <asp:ListItem>17</asp:ListItem>
                                    <asp:ListItem>18</asp:ListItem>
                                    <asp:ListItem>19</asp:ListItem>
                                    <asp:ListItem>20</asp:ListItem>
                                    <asp:ListItem>21</asp:ListItem>
                                    <asp:ListItem>22</asp:ListItem>
                                    <asp:ListItem>23</asp:ListItem>
                                    <asp:ListItem>24</asp:ListItem>
                                    <asp:ListItem>25</asp:ListItem>
                                    <asp:ListItem>26</asp:ListItem>
                                    <asp:ListItem>27</asp:ListItem>
                                    <asp:ListItem>28</asp:ListItem>
                                    <asp:ListItem>29</asp:ListItem>
                                    <asp:ListItem>30</asp:ListItem>
                                    <asp:ListItem>31</asp:ListItem>
                                    <asp:ListItem>32</asp:ListItem>
                                    <asp:ListItem>33</asp:ListItem>
                                    <asp:ListItem>34</asp:ListItem>
                                    <asp:ListItem>35</asp:ListItem>
                                    <asp:ListItem>36</asp:ListItem>
                                    <asp:ListItem>37</asp:ListItem>
                                    <asp:ListItem>38</asp:ListItem>
                                    <asp:ListItem>39</asp:ListItem>
                                    <asp:ListItem>40</asp:ListItem>
                                    <asp:ListItem>41</asp:ListItem>
                                    <asp:ListItem>42</asp:ListItem>
                                    <asp:ListItem>43</asp:ListItem>
                                    <asp:ListItem>44</asp:ListItem>
                                    <asp:ListItem>45</asp:ListItem>
                                    <asp:ListItem>46</asp:ListItem>
                                    <asp:ListItem>47</asp:ListItem>
                                    <asp:ListItem>48</asp:ListItem>
                                    <asp:ListItem>49</asp:ListItem>
                                    <asp:ListItem>50</asp:ListItem>
                                    
                                </asp:DropDownList>--%>
                                <asp:TextBox ID="ddl_Adult" runat="server" placeholder="Enter No of Adult(12+ yrs)" CssClass="theme-search-area-section-input" onkeypress="if(event.keyCode<48 || event.keyCode>57)event.returnValue=false;"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="col-md-3  col-xs-6">
                    <div class="form-group">
                        <div class="theme-search-area-section theme-search-area-section-line">
                            <div class="theme-search-area-section-inner">
                                <i class="theme-search-area-section-icon lin lin-user"></i>
                                <%--<label>Child(2 - 11 yrs)</label>--%>
                                <%--                                <asp:DropDownList ID="ddl_Child" runat="server" CssClass="theme-search-area-section-input">
                                    <asp:ListItem>Child(2 - 11 yrs)</asp:ListItem>
                                    <asp:ListItem>0</asp:ListItem>
                                    <asp:ListItem>1</asp:ListItem>
                                    <asp:ListItem>2</asp:ListItem>
                                    <asp:ListItem>3</asp:ListItem>
                                    <asp:ListItem>4</asp:ListItem>
                                    <asp:ListItem>5</asp:ListItem>
                                    <asp:ListItem>6</asp:ListItem>
                                    <asp:ListItem>7</asp:ListItem>
                                    <asp:ListItem>8</asp:ListItem>
                                    <asp:ListItem>9</asp:ListItem>
                                    <asp:ListItem>10</asp:ListItem>
                                    <asp:ListItem>11</asp:ListItem>
                                    <asp:ListItem>12</asp:ListItem>
                                    <asp:ListItem>13</asp:ListItem>
                                    <asp:ListItem>14</asp:ListItem>
                                    <asp:ListItem>15</asp:ListItem>
                                    <asp:ListItem>16</asp:ListItem>
                                    <asp:ListItem>17</asp:ListItem>
                                    <asp:ListItem>18</asp:ListItem>
                                    <asp:ListItem>19</asp:ListItem>
                                    <asp:ListItem>20</asp:ListItem>
                                    <asp:ListItem>21</asp:ListItem>
                                    <asp:ListItem>22</asp:ListItem>
                                    <asp:ListItem>23</asp:ListItem>
                                    <asp:ListItem>24</asp:ListItem>
                                    <asp:ListItem>25</asp:ListItem>
                                    <asp:ListItem>26</asp:ListItem>
                                    <asp:ListItem>27</asp:ListItem>
                                    <asp:ListItem>28</asp:ListItem>
                                    <asp:ListItem>29</asp:ListItem>
                                    <asp:ListItem>30</asp:ListItem>
                                    <asp:ListItem>31</asp:ListItem>
                                    <asp:ListItem>32</asp:ListItem>
                                    <asp:ListItem>33</asp:ListItem>
                                    <asp:ListItem>34</asp:ListItem>
                                    <asp:ListItem>35</asp:ListItem>
                                    <asp:ListItem>36</asp:ListItem>
                                    <asp:ListItem>37</asp:ListItem>
                                    <asp:ListItem>38</asp:ListItem>
                                    <asp:ListItem>39</asp:ListItem>
                                    <asp:ListItem>40</asp:ListItem>
                                    <asp:ListItem>41</asp:ListItem>
                                    <asp:ListItem>42</asp:ListItem>
                                    <asp:ListItem>43</asp:ListItem>
                                    <asp:ListItem>44</asp:ListItem>
                                    <asp:ListItem>45</asp:ListItem>
                                    <asp:ListItem>46</asp:ListItem>
                                    <asp:ListItem>47</asp:ListItem>
                                    <asp:ListItem>48</asp:ListItem>
                                    <asp:ListItem>49</asp:ListItem>
                                    <asp:ListItem>50</asp:ListItem>
                                </asp:DropDownList>--%>
                                <asp:TextBox ID="ddl_Child" runat="server" placeholder="Enter No of Child(2 - 11 yrs)" CssClass="theme-search-area-section-input" onkeypress="if(event.keyCode<48 || event.keyCode>57)event.returnValue=false;"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="col-md-3 col-xs-6">
                    <div class="form-group">
                        <%--<label>Infant(Under 2 yrs)</label>--%>
                        <div class="theme-search-area-section theme-search-area-section-line">
                            <div class="theme-search-area-section-inner">
                                <i class="theme-search-area-section-icon lin lin-user"></i>
<%--                                <asp:DropDownList ID="ddl_Infrant" runat="server" CssClass="theme-search-area-section-input">
                                    <asp:ListItem>Infant(Under 2 yrs)</asp:ListItem>
                                    <asp:ListItem>0</asp:ListItem>
                                    <asp:ListItem>1</asp:ListItem>
                                    <asp:ListItem>2</asp:ListItem>
                                    <asp:ListItem>3</asp:ListItem>
                                    <asp:ListItem>4</asp:ListItem>
                                    <asp:ListItem>5</asp:ListItem>
                                    <asp:ListItem>6</asp:ListItem>
                                    <asp:ListItem>7</asp:ListItem>
                                    <asp:ListItem>8</asp:ListItem>
                                    <asp:ListItem>9</asp:ListItem>
                                    <asp:ListItem>10</asp:ListItem>
                                    <asp:ListItem>11</asp:ListItem>
                                    <asp:ListItem>12</asp:ListItem>
                                    <asp:ListItem>13</asp:ListItem>
                                    <asp:ListItem>14</asp:ListItem>
                                    <asp:ListItem>15</asp:ListItem>
                                    <asp:ListItem>16</asp:ListItem>
                                    <asp:ListItem>17</asp:ListItem>
                                    <asp:ListItem>18</asp:ListItem>
                                    <asp:ListItem>19</asp:ListItem>
                                    <asp:ListItem>20</asp:ListItem>
                                    <asp:ListItem>21</asp:ListItem>
                                    <asp:ListItem>22</asp:ListItem>
                                    <asp:ListItem>23</asp:ListItem>
                                    <asp:ListItem>24</asp:ListItem>
                                    <asp:ListItem>25</asp:ListItem>
                                    <asp:ListItem>26</asp:ListItem>
                                    <asp:ListItem>27</asp:ListItem>
                                    <asp:ListItem>28</asp:ListItem>
                                    <asp:ListItem>29</asp:ListItem>
                                    <asp:ListItem>30</asp:ListItem>
                                    <asp:ListItem>31</asp:ListItem>
                                    <asp:ListItem>32</asp:ListItem>
                                    <asp:ListItem>33</asp:ListItem>
                                    <asp:ListItem>34</asp:ListItem>
                                    <asp:ListItem>35</asp:ListItem>
                                    <asp:ListItem>36</asp:ListItem>
                                    <asp:ListItem>37</asp:ListItem>
                                    <asp:ListItem>38</asp:ListItem>
                                    <asp:ListItem>39</asp:ListItem>
                                    <asp:ListItem>40</asp:ListItem>
                                    <asp:ListItem>41</asp:ListItem>
                                    <asp:ListItem>42</asp:ListItem>
                                    <asp:ListItem>43</asp:ListItem>
                                    <asp:ListItem>44</asp:ListItem>
                                    <asp:ListItem>45</asp:ListItem>
                                    <asp:ListItem>46</asp:ListItem>
                                    <asp:ListItem>47</asp:ListItem>
                                    <asp:ListItem>48</asp:ListItem>
                                    <asp:ListItem>49</asp:ListItem>
                                    <asp:ListItem>50</asp:ListItem>
                                </asp:DropDownList>--%>
                                <asp:TextBox ID="ddl_Infrant" runat="server" placeholder="Enter No of Infant(Under 2 yrs)" CssClass="theme-search-area-section-input" onkeypress="if(event.keyCode<48 || event.keyCode>57)event.returnValue=false;"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                </div>

            </div>

            <div class="row">



                <div class="col-md-3 col-xs-6">
                    <div class="form-group">
                        <%-- <label>Class</label>--%>
                        <div class="theme-search-area-section theme-search-area-section-line">
                            <div class="theme-search-area-section-inner">
                                <i class="theme-search-area-section-icon lin lin-user"></i>
                                <asp:DropDownList ID="ddl_Class" runat="server" CssClass="theme-search-area-section-input">
                                    <asp:ListItem>Select Class</asp:ListItem>
                                    <asp:ListItem>Economy</asp:ListItem>
                                    <%--<asp:ListItem>Non Refundable Economy</asp:ListItem>
                                    <asp:ListItem>Refundable Economy</asp:ListItem>
                                    <asp:ListItem>Full Fare Economy</asp:ListItem>--%>
                                    <asp:ListItem>Business</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                </div>


                <div class="col-md-3 col-xs-6" style="display: none;">
                    <div class="form-group">
                        <%--<label>Classes</label>--%>
                        <div class="theme-search-area-section theme-search-area-section-line">
                            <div class="theme-search-area-section-inner">
                                <i class="theme-search-area-section-icon lin lin-plane"></i>
                                <asp:DropDownList ID="ddl_Classes" runat="server" CssClass="theme-search-area-section-input">
                                    <asp:ListItem>Select Classes</asp:ListItem>
                                    <asp:ListItem>All</asp:ListItem>
                                    <asp:ListItem>A</asp:ListItem>
                                    <asp:ListItem>B</asp:ListItem>
                                    <asp:ListItem>C</asp:ListItem>
                                    <asp:ListItem>D</asp:ListItem>
                                    <asp:ListItem>E</asp:ListItem>
                                    <asp:ListItem>F</asp:ListItem>
                                    <asp:ListItem>G</asp:ListItem>
                                    <asp:ListItem>H</asp:ListItem>
                                    <asp:ListItem>I</asp:ListItem>
                                    <asp:ListItem>J</asp:ListItem>
                                    <asp:ListItem>K</asp:ListItem>
                                    <asp:ListItem>L</asp:ListItem>
                                    <asp:ListItem>M</asp:ListItem>
                                    <asp:ListItem>N</asp:ListItem>
                                    <asp:ListItem>O</asp:ListItem>
                                    <asp:ListItem>P</asp:ListItem>
                                    <asp:ListItem>Q</asp:ListItem>
                                    <asp:ListItem>R</asp:ListItem>
                                    <asp:ListItem>S</asp:ListItem>
                                    <asp:ListItem>T</asp:ListItem>
                                    <asp:ListItem>U</asp:ListItem>
                                    <asp:ListItem>V</asp:ListItem>
                                    <asp:ListItem>W</asp:ListItem>
                                    <asp:ListItem>X</asp:ListItem>
                                    <asp:ListItem>Y</asp:ListItem>
                                    <asp:ListItem>Z</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                </div>


                <div class="col-md-3 col-xs-6">
                    <div class="form-group">
                        <%--<label>Prefered Airlines</label>--%>
                        <div class="theme-search-area-section theme-search-area-section-line">
                            <div class="theme-search-area-section-inner">
                                <i class="theme-search-area-section-icon lin lin-plane"></i>
                                <input type="text" name="txtAirline" value="" placeholder="Prefered Airline" id="txtAirline" class="theme-search-area-section-input" />
                                <input type="hidden" id="hidtxtAirline" name="hidtxtAirline" value="" />
                            </div>
                        </div>
                    </div>
                </div>

                 <div class="col-md-3 col-xs-6">
                    <div class="form-group">
                        <%--<label>Payment Limit</label>--%>
                        <div class="theme-search-area-section theme-search-area-section-line">
                                <asp:TextBox ID="txt_Remark" runat="server" placeholder="Enter Remark" TextMode="MultiLine" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                </div>

                 <div class="col-md-3 col-xs-6">
                    <div class="form-group">
                        <%--<label>Payment Limit</label>--%>
                        <div class="theme-search-area-section theme-search-area-section-line">
                            <div class="theme-search-area-section-inner">
                                <i class="theme-search-area-section-icon lin lin-credit-card"></i>
                                <asp:TextBox ID="TxtExpectedFare" CssClass="theme-search-area-section-input" placeholder="Enter Expected Amount" onkeypress="if(event.keyCode<48 || event.keyCode>57)event.returnValue=false;" runat="server"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-md-3 col-xs-6" style="display:none;">
                    <div class="form-group">
                        <%--<label>Payment Limit</label>--%>
                        <div class="theme-search-area-section theme-search-area-section-line">
                            <div class="theme-search-area-section-inner">
                                <i class="theme-search-area-section-icon lin lin-credit-card"></i>
                                <asp:DropDownList ID="ddl_PaymentMode" runat="server" CssClass="theme-search-area-section-input">
                                    <asp:ListItem>Select Payment Limit</asp:ListItem>
                                    <asp:ListItem Value="CL">Cash Limit</asp:ListItem>
                                    <asp:ListItem Value="CL">Card Limit</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row">





                <div class="btn-save col-md-3">
                    <asp:LinkButton ID="btn_Submit" runat="server" OnClientClick="return Proxy();" CssClass="btn cmn-btn">Submit</asp:LinkButton>
                </div>
            </div>









            <div class="row">
                <div id="TBL_Projects" runat="server" class="col-md-9 col-xs-12">

                    <div>

                        <div>
                            <label>Project Id :</label>
                            <asp:DropDownList ID="DropDownListProject" runat="server" CssClass="drpBox">
                            </asp:DropDownList>
                        </div>

                        <div>
                            <label>Booked By :</label>
                            <asp:DropDownList ID="DropDownListBookedBy" runat="server" CssClass="drpBox">
                            </asp:DropDownList>
                        </div>
                    </div>

                    <div>

                        <label>Remark :</label>
<%--                        <asp:TextBox ID="txt_Remark" runat="server" class="form control input-text full-width" TextMode="MultiLine"></asp:TextBox>--%>

                    </div>
                </div>


            </div>




            <br />
            <br />
        </div>

    </div>

    <script type="text/javascript">

        var myDate = new Date();
        var currDate = (myDate.getDate()) + '/' + (myDate.getMonth() + 1) + '/' + myDate.getFullYear();
        document.getElementById("txtDepDate").value = currDate;
        document.getElementById("hidtxtDepDate").value = currDate;
        document.getElementById("txtRetDate").value = currDate;
        document.getElementById("hidtxtRetDate").value = currDate;
        var UrlBase = '<%=ResolveUrl("~/") %>';
        document.getElementById("txtRetDate").style.display = "none";
        document.getElementById("td_ret").style.display = "none";
        document.getElementById("td_time").style.display = "none";
        document.getElementById("ctl00_ContentPlaceHolder1_ddl_ReturnAnytime").style.display = "none";
        document.getElementById("rtn").style.display = "none";


    </script>

    <script type="text/javascript" src="<%=ResolveUrl("~/Scripts/jquery-1.4.4.min.js") %>"></script>

    <script type="text/javascript" src="<%=ResolveUrl("~/Scripts/jquery-ui-1.8.8.custom.min.js")%>"></script>

    <script type="text/javascript" src="<%=ResolveUrl("~/Scripts/Common.js") %>"></script>

    <script type="text/javascript" src="<%=ResolveUrl("~/Scripts/Search3_off.js")%>"></script>
    <script>
        $(".blockbackdate").datepicker({ dateFormat: "dd-mm-yy", minDate: -0 });
    </script>
</asp:Content>
