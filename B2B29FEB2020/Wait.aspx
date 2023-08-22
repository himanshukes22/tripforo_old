<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Wait.aspx.vb" Inherits="Wait" ValidateRequest="false" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Please Wait...</title>
    <link href="css/main2.css" rel="stylesheet" type="text/css" />
   <style type="text/css">
       .body {
  -webkit-font-smoothing: antialiased;
  align-items: center;
  background-color: #f4f7ff;
  display: flex;
  height: 100vh;
  justify-content: center;
}

@keyframes drawThick {
  0% {
    stroke-dashoffset: 180;
  }
  100% {
    stroke-dashoffset: 0;
  }
}
@keyframes drawDots {
  0% {
    stroke-dashoffset: 60;
  }
  100% {
    stroke-dashoffset: 0;
  }
}
@keyframes drawStatic {
  0% {
    stroke-dashoffset: 8;
  }
  30% {
    stroke-dashoffset: 0;
  }
  100% {
    stroke-dashoffset: 8;
  }
}
@keyframes rumble {
  0% {
    transform: translate(-1px, -1px);
  }
  10% {
    transform: translate(0, 0);
  }
  15% {
    transform: translate(-1px, -1px);
  }
  35% {
    transform: translate(1px, 1px);
  }
  45% {
    transform: translate(-1px, -1px);
  }
  55% {
    transform: translate(1px, 1px);
  }
  65% {
    transform: translate(0, 0);
  }
  80% {
    transform: translate(1px, 1px);
  }
  100% {
    transform: translate(-1px, -1px);
  }
}
@keyframes rotate3d {
  0% {
    transform: rotate3d(1, -1, 0, 0);
  }
  30% {
    transform: rotate3d(1, -1, 0, 0deg);
  }
  50% {
    transform: rotate3d(1, -1, 0, 60deg);
  }
  100% {
    transform: rotate3d(1, -1, 0, 0);
  }
}
@keyframes launch {
  5% {
    transform: translate(0, 0);
  }
  8% {
    transform: translate(2px, -2px);
  }
  30% {
    transform: translate(-60px, 60px);
  }
  100% {
    transform: translate(0, 0);
  }
}
@keyframes slideIn {
  0% {
    transform: translate(-60px, 60px);
  }
  35% {
    transform: translate(-60px, 60px);
  }
  100% {
    transform: translate(0, 0);
  }
}
.loadingWrap {
  text-align: center;
}

.loading {
  max-width: 70px;
  width: 100%;
}
.loading .background {
  fill: #102555;
}
.loading .rotate {
  animation: rotate3d 4s 1;
  transform-origin: center;
}
.loading .rumble {
  animation: rumble 1s infinite 1.3333333333s;
}
.loading .arrow {
  animation: launch 4s 1;
  fill: #fff;
}
.loading .exhaust {
  animation: slideIn 4s 1;
}
.loading .gradient stop {
  stop-color: #fff;
  stop-opacity: 0.2;
}
.loading .gradient stop:last-child {
  stop-opacity: 0;
}
.loading .gradientBox {
  fill: url("#gradient");
}
.loading .filteredGroup {
  filter: url("#stickyFilter");
}
.loading .line {
  animation-iteration-count: infinite;
  stroke: #dde1ee;
  stroke-linecap: round;
}
.loading .lineThick {
  animation-name: drawThick;
  animation-timing-function: linear;
}
.loading .lineDots {
  animation-name: drawDots;
  animation-timing-function: linear;
  stroke-dasharray: 0, 30;
  stroke-width: 5.5;
}
.loading .lineStatic {
  animation-name: drawStatic;
  animation-timing-function: ease;
  stroke-dasharray: 22;
  stroke-width: 8;
}
.loading .line1 {
  animation-duration: 1.6s;
  stroke-dasharray: 8, 22, 3, 27, 16, 14;
  stroke-width: 4;
}
.loading .line2 {
  animation-duration: 1.3s;
  stroke-dasharray: 18, 12, 10, 20, 3, 27;
  stroke-width: 6;
}
.loading .line3 {
  animation-duration: 1.4s;
  stroke-dasharray: 3, 27, 10, 20, 20, 10;
  stroke-width: 4;
}
.loading .line4 {
  animation-duration: 0.4s;
}
.loading .line5 {
  animation-duration: 0.7s;
}
.loading .line6 {
  animation-duration: 0.8s;
}
.loading .line7 {
  animation-duration: 0.6s;
}

.label {
  color: #555;
  font-family: Arial, Helvetica, sans-serif;
  font-size: 0.8rem;
  font-weight: 700;
  margin-top: 1rem;
}

/* Website Link */
.website-link {
  background: #f8faff;
  border-radius: 50px 0 0 50px;
  bottom: 30px;
  color: #324b77;
  cursor: pointer;
  font-family: "Montserrat", sans-serif;
  font-weight: 600;
  height: 34px;
  filter: drop-shadow(2px 3px 4px rgba(0, 0, 0, 0.1));
  padding: 0 20px 0 40px;
  position: fixed;
  right: 0;
  text-align: left;
  text-decoration: none;
}
.website-link__icon {
  left: -10px;
  position: absolute;
  top: -12px;
  width: 44px;
}
.website-link__name {
  display: block;
  font-size: 14px;
  line-height: 14px;
  margin: 5px 0 3px;
}
.website-link__last-name {
  color: #55bada;
}
.website-link__message {
  color: #8aa8c5;
  display: block;
  font-size: 7px;
  line-height: 7px;
}
   </style>
</head>
<body>
    <form id="form1" runat="server">

         <div    >
        <div  class="wait" >
            <h1 class="text-center" style="font-size: 20px;"></h1>
            <span></span><br />
            <%-- <img src='<%=ResolveUrl("~/images/loadingAnim.gif")%>' alt="" /><br />--%>
            <br />
            <div id="searchquery" style="color: #004b91; padding-top: 15px">
            </div>
        </div>
    </div>



<div class="body">
  <div class="loadingWrap">
    <svg class="loading" viewBox="0 0 84.6 84.6">
      <defs>
        <filter id="stickyFilter">
          <feGaussianBlur in="SourceGraphic" stdDeviation="2" result="blur" />
          <feColorMatrix in="blur" mode="matrix" values="1 0 0 0 0  0 1 0 0 0  0 0 1 0 0  0 0 0 19 -9" result="filter" />
          <feComposite in="SourceGraphic" in2="filter" operator="atop"/>
        </filter>
        <linearGradient id="gradient" class="gradient" x1="0%" y1="0%" x2="0%" y2="40%" gradientTransform="rotate(45)">
          <stop offset="0%"/>
          <stop offset="100%"/>
        </linearGradient>
      </defs>
      <clipPath id="clip">
        <circle cx="42.3" cy="42.3" r="41.6"/>
      </clipPath>
      <g clip-path="url(#clip)">
        <path class="background" d="M0 0h84.6v84.6H0z"/>
        <g class="exhaust">
          <path class="gradientBox" d="M2.37244 65.5981l28.84968-28.84968 16.54614 16.54614-28.84968 28.84968z"/>
          <path class="gradientBox" d="M6.35189 69.55189l28.84968-28.84968 8.34378 8.34378-28.84968 28.84968z"/>
          <g class="filteredGroup">
            <path class="line1 line lineThick" d="M47.7 49.6L13.4 83.9"/>
            <path class="line2 line lineThick" d="M42.3 42.3L8 76.6"/>
            <path class="line3 line lineThick" d="M33.5 38.5L-.8 72.8"/>

            <path class="line4 line lineDots"  d="M45 45.82L10.7 80.12"/>
            <path class="line5 line lineDots" d="M38.16 40.06L3.86 74.36"/>

            <path class="line6 line lineStatic" d="M39.46 41.15L18.91 61.7"/>
            <path class="line7 line lineStatic" d="M43.57 43.77L23.02 64.32"/>
          </g>
        </g>
        <g class="rotate">
          <g class="rumble">
            <path class="arrow" d="M60.8 27.2c.6-2.1-1.3-4-3.4-3.4L19.5 35.3c-2.2.7-2.6 3.6-.8 4.9l15.8 7.6c1.1.5 2 1.4 2.5 2.6L44.4 66c1.3 1.9 4.2 1.4 4.9-.8l11.5-38z"/>
          </g>
        </g>
      </g>
    </svg>
    <div class="label">Wait..while we are confirming your flight</div>
  </div>
 </div>



     
    <%If Session("search_type").ToString = "Adv" Then%>

    <script type="text/javascript">
        window.location.href = 'Adv_Search/Book_con.aspx';
    </script>

    <%  ElseIf Session("search_type").ToString = "Flt" Then%>

    <script type="text/javascript">
        window.location.href = 'Domestic/BookingConfimation.aspx?OBTID=<%=Request("OBTID")%>&IBTID=<%=Request("IBTID") %>&FT=<%=Request("FT") %>';
    </script>
    <%  ElseIf Session("search_type").ToString = "RTF" Then%>

    <script type="text/javascript">
        window.location.href = 'LccRF/LccRTFBooking.aspx?OBTID=<%=Request("OBTID")%>&IBTID=<%=Request("IBTID") %>&FT=<%=Request("FT") %>';
    </script>
  <%  ElseIf Session("search_type").ToString = "FltInt" Then%>

    <script type="text/javascript">
        window.location.href = 'International/BookingConfimation.aspx?TID=<%=Request("tid")%>';
    </script>
    <%  Else%>
    <%  End If%>
    </form>
</body>
</html>

