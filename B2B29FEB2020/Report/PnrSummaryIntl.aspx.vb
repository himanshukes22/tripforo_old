Imports System.Collections.Generic
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections
Imports System.IO
Imports iTextSharp.text
Imports iTextSharp.text.pdf
Imports iTextSharp.tool.xml
Imports HtmlAgilityPack
Imports System.Linq
Imports iTextSharp.text.html.simpleparser
Imports IPTracker

Partial Class FlightInt_PnrSummaryIntl

    Inherits System.Web.UI.Page
    Private ObjIntDetails As New IntlDetails()
    Public BackClor As String
    'Dim objTktCopy As New clsTicketCopy
    Dim objTktCopy As New IntlDetails()
    Dim objTkt As New TktCopyForMail
    Dim agent_id As String
    Dim email_id As String
    Dim FltHeaderList As New DataTable()
    Dim fltTerminalDetails As New DataTable()




    Private Shared AgentId As String
    Private Shared IPAddress As String
    Public Shared OrderId As String
    Dim STDom As New SqlTransactionDom

    Public AgencyName As String = ""
    Public AgencyId As String = ""
    Public LoginId As String = ""
    Public LoginType As String = ""
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load



        Try
            If Session("UID") <> "" AndAlso Session("UID") IsNot Nothing Then
                'Check Login Type Staff or Agent
                Try
                    AgencyName = Session("AgencyName")
                    AgencyId = Session("AgencyId")
                    LoginId = Session("UID")
                    LoginType = "AGENT"
                    If (Not String.IsNullOrEmpty(Convert.ToString(Session("LoginByStaff"))) AndAlso Convert.ToString(Session("LoginByStaff")) = "true") Then
                        AgencyId = Session("StaffUserId")
                        LoginId = AgencyId
                        divAgentBalance.Visible = True
                        LoginType = "STAFF"
                    End If
                Catch ex As Exception

                End Try
                Dim State As New StateCollection()
                Dim objIP As New IPDetails()
                State.SessionID = Session.SessionID
                State.Path = Request.CurrentExecutionFilePath
                State.Username = Session("UID").ToString() 'Page.User.Identity.Name
                State.VISTING_TIME = DateTime.Now.ToString()
                Dim objST As New SessionTrack()
                objST.Add(State, Request.CurrentExecutionFilePath)
            End If

        Catch ex As Exception

        End Try

        Try
            AgentId = Convert.ToString(Session("UID"))
            IPAddress = Request.UserHostAddress


            If Not IsPostBack Then
                OrderId = Request.QueryString("Orderid")
                hdnReferenceNo.Value = OrderId
                Dim TransTD As String = Request.QueryString("TransID")
                HdnOrderId.Value = OrderId
                HdnTrnsId.Value = TransTD
                Dim charge As Integer = 0
                If txt_srvcharge.Value.Trim() <> "" Then
                    charge = Convert.ToInt32(txt_srvcharge.Value.Trim())
                End If

                'objTktCopyPDF.TicketCopyExportPDF()
                'LabelTkt.Text = TicketCopyExportPDF(OrderId, TransTD)
                ' TicketCopyExportPDF(OrderId, TransTD)
                'lbl_Summary.Text = "<html xmlns='http://www.w3.org/1999/xhtml'><head><title>Ticket Details</title>"
                ' '' lbl_Summary.Text += "<style type='text/css'>?@charset 'UTF-8';/*** Foundation for Sites by ZURB* Version 6.0.5* foundation.zurb.com* Licensed under MIT Open Source*//*! normalize.css v3.0.3 | MIT License | github.com/necolas/normalize.css *//*** 1. Set default font family to sans-serif.* 2. Prevent iOS and IE text size adjust after device orientation change,*    without disabling user zoom.*/html {font-family: sans-serif;/* 1 */-ms-text-size-adjust: 100%;/* 2 */-webkit-text-size-adjust: 100%;/* 2 */ }/*** Remove default margin.*/body {margin: 0; }/* HTML5 display definitions========================================================================== *//*** Correct `block` display not defined for any HTML5 element in IE 8/9.* Correct `block` display not defined for `details` or `summary` in IE 10/11* and Firefox.* Correct `block` display not defined for `main` in IE 11.*/article,aside,details,figcaption,figure,footer,header,hgroup,main,menu,nav,section,summary {display: block; }/*** 1. Correct `inline-block` display not defined in IE 8/9.* 2. Normalize vertical alignment of `progress` in Chrome, Firefox, and Opera.*/audio,canvas,progress,video {display: inline-block;/* 1 */vertical-align: baseline;/* 2 */ }/*** Prevent modern browsers from displaying `audio` without controls.* Remove excess height in iOS 5 devices.*/audio:not([controls]) {display: none;height: 0; }/*** Address `[hidden]` styling not present in IE 8/9/10.* Hide the `template` element in IE 8/9/10/11, Safari, and Firefox < 22.*/[hidden],template {display: none; }/* Links========================================================================== *//*** Remove the gray background color from active links in IE 10.*/a {background-color: transparent; }/*** Improve readability of focused elements when they are also in an* active/hover state.*/a:active,a:hover {outline: 0; }/* Text-level semantics========================================================================== *//*** Address styling not present in IE 8/9/10/11, Safari, and Chrome.*/abbr[title] {border-bottom: 1px dotted; }/*** Address style set to `bolder` in Firefox 4+, Safari, and Chrome.*/b,strong {font-weight: bold; }/*** Address styling not present in Safari and Chrome.*/dfn {font-style: italic; }/*** Address variable `h1` font-size and margin within `section` and `article`* contexts in Firefox 4+, Safari, and Chrome.*/h1 {font-size: 2em;margin: 0.67em 0; }/*** Address styling not present in IE 8/9.*/mark {background: #ff0;color: #000; }/*** Address inconsistent and variable font size in all browsers.*/small {font-size: 80%; }/*** Prevent `sub` and `sup` affecting `line-height` in all browsers.*/sub,sup {font-size: 75%;line-height: 0;position: relative;vertical-align: baseline; }sup {top: -0.5em; }sub {bottom: -0.25em; }/* Embedded content========================================================================== *//*** Remove border when inside `a` element in IE 8/9/10.*/img {border: 0; }/*** Correct overflow not hidden in IE 9/10/11.*/svg:not(:root) {overflow: hidden; }/* Grouping content========================================================================== *//*** Address margin not present in IE 8/9 and Safari.*/figure {margin: 1em 40px; }/*** Address differences between Firefox and other browsers.*/hr {box-sizing: content-box;height: 0; }/*** Contain overflow in all browsers.*/pre {overflow: auto; }/*** Address odd `em`-unit font size rendering in all browsers.*/code,kbd,pre,samp {font-family: monospace, monospace;font-size: 1em; }/* Forms========================================================================== *//*** Known limitation: by default, Chrome and Safari on OS X allow very limited* styling of `select`, unless a `border` property is set.*//*** 1. Correct color not being inherited.*    Known issue: affects color of disabled elements.* 2. Correct font properties not being inherited.* 3. Address margins set differently in Firefox 4+, Safari, and Chrome.*/button,input,optgroup,select,textarea {color: inherit;/* 1 */font: inherit;/* 2 */margin: 0;/* 3 */ }/*** Address `overflow` set to `hidden` in IE 8/9/10/11.*/button {overflow: visible; }/*** Address inconsistent `text-transform` inheritance for `button` and `select`.* All other form control elements do not inherit `text-transform` values.* Correct `button` style inheritance in Firefox, IE 8/9/10/11, and Opera.* Correct `select` style inheritance in Firefox.*/button,select {text-transform: none; }/*** 1. Avoid the WebKit bug in Android 4.0.* where (2) destroys native `audio`*    and `video` controls.* 2. Correct inability to style clickable `input` types in iOS.* 3. Improve usability and consistency of cursor style between image-type*    `input` and others.*/button,html input[type='button'],input[type='reset'],input[type='submit'] {-webkit-appearance: button;/* 2 */cursor: pointer;/* 3 */ }/*** Re-set default cursor for disabled elements.*/button[disabled],html input[disabled] {cursor: default; }/*** Remove inner padding and border in Firefox 4+.*/button::-moz-focus-inner,input::-moz-focus-inner {border: 0;padding: 0; }/*** Address Firefox 4+ setting `line-height` on `input` using `!important` in* the UA stylesheet.*/input {line-height: normal; }/*** It's recommended that you don't attempt to style these elements.* Firefox's implementation doesn't respect box-sizing, padding, or width.** 1. Address box sizing set to `content-box` in IE 8/9/10.* 2. Remove excess padding in IE 8/9/10.*/input[type='checkbox'],input[type='radio'] {box-sizing: border-box;/* 1 */padding: 0;/* 2 */ }/*** Fix the cursor style for Chrome's increment/decrement buttons. For certain* `font-size` values of the `input`, it causes the cursor style of the* decrement button to change from `default` to `text`.*/input[type='number']::-webkit-inner-spin-button,input[type='number']::-webkit-outer-spin-button {height: auto; }/*** 1. Address `appearance` set to `searchfield` in Safari and Chrome.* 2. Address `box-sizing` set to `border-box` in Safari and Chrome.*/input[type='search'] {-webkit-appearance: textfield;/* 1 */box-sizing: content-box;/* 2 */ }/*** Remove inner padding and search cancel button in Safari and Chrome on OS X.* Safari (but not Chrome) clips the cancel button when the search input has* padding (and `textfield` appearance).*/input[type='search']::-webkit-search-cancel-button,input[type='search']::-webkit-search-decoration {-webkit-appearance: none; }/*** Define consistent border, margin, and padding.*/fieldset {border: 1px solid #c0c0c0;margin: 0 2px;padding: 0.35em 0.625em 0.75em; }/*** 1. Correct `color` not being inherited in IE 8/9/10/11.* 2. Remove padding so people aren't caught out if they zero out fieldsets.*/legend {border: 0;/* 1 */padding: 0;/* 2 */ }/*** Remove default vertical scrollbar in IE 8/9/10/11.*/textarea {overflow: auto; }/*** Don't inherit the `font-weight` (applied by a rule above).* NOTE: the default cannot safely be changed in Chrome and Safari on OS X.*/optgroup {font-weight: bold; }/* Tables========================================================================== *//*** Remove most spacing between table cells.*/table {border-collapse: collapse;border-spacing: 0; }td,th {padding: 0; }.foundation-mq {font-family: 'small=0em&medium=40em&large=64em&xlarge=75em&xxlarge=90em'; }html,body {font-size: 100%;box-sizing: border-box; }*,*:before,*:after {box-sizing: inherit; }body {padding: 0;margin: 0;font-family: 'Helvetica Neue', Helvetica, Roboto, Arial, sans-serif;font-weight: normal;line-height: 1.5;color: #0a0a0a;background: #fefefe;-webkit-font-smoothing: antialiased;-moz-osx-font-smoothing: grayscale; }img {max-width: 100%;height: auto;-ms-interpolation-mode: bicubic;display: inline-block;vertical-align: middle; }textarea {height: auto;min-height: 50px;border-radius: 0; }select {width: 100%;border-radius: 0; }#map_canvas img,#map_canvas embed,#map_canvas object,.map_canvas img,.map_canvas embed,.map_canvas object,.mqa-display img,.mqa-display embed,.mqa-display object {max-width: none !important; }button {-webkit-appearance: none;-moz-appearance: none;background: transparent;padding: 0;border: 0;border-radius: 0;line-height: 1; }.row {max-width: 95rem;margin-left: auto;margin-right: auto; }.row::before, .row::after {content: ' ';display: table; }.row::after {clear: both; }.row.collapse > .column, .row.collapse > .columns {padding-left: 0;padding-right: 0; }.row .row {margin-left: -0.375rem;margin-right: -0.375rem; }.row .row.collapse {margin-left: 0;margin-right: 0; }.row.small-collapse > .column, .row.small-collapse > .columns {padding-left: 0;padding-right: 0; }.row.small-uncollapse > .column, .row.small-uncollapse > .columns {padding-left: 30px;padding-right: 30px; }@media screen and (min-width: 40em) {.row.medium-collapse > .column, .row.medium-collapse > .columns {padding-left: 0;padding-right: 0; }.row.medium-uncollapse > .column, .row.medium-uncollapse > .columns {padding-left: 30px;padding-right: 30px; } }@media screen and (min-width: 64em) {.row.large-collapse > .column, .row.large-collapse > .columns {padding-left: 0;padding-right: 0; }.row.large-uncollapse > .column, .row.large-uncollapse > .columns {padding-left: 30px;padding-right: 30px; } }.row.expanded {max-width: none; }.column, .columns {width: 100%;float: left;padding-left: 0.075rem;padding-right: 0.075rem; }.column:last-child:not(:first-child), .columns:last-child:not(:first-child) {float: right; }.column.end:last-child:last-child, .end.columns:last-child:last-child {float: left; }.column.row.row, .row.row.columns {float: none; }.row .column.row.row, .row .row.row.columns {padding-left: 0;padding-right: 0;margin-left: 0;margin-right: 0; }.small-1 {width: 8.33333%; }.small-push-1 {position: relative;left: 8.33333%; }.small-pull-1 {position: relative;left: -8.33333%; }.small-offset-0 {margin-left: 0%; }.small-2 {width: 16.66667%; }.small-push-2 {position: relative;left: 16.66667%; }.small-pull-2 {position: relative;left: -16.66667%; }.small-offset-1 {margin-left: 8.33333%; }.small-3 {width: 25%; }.small-push-3 {position: relative;left: 25%; }.small-pull-3 {position: relative;left: -25%; }.small-offset-2 {margin-left: 16.66667%; }.small-4 {width: 33.33333%; }.small-push-4 {position: relative;left: 33.33333%; }.small-pull-4 {position: relative;left: -33.33333%; }.small-offset-3 {margin-left: 25%; }.small-5 {width: 41.66667%; }.small-push-5 {position: relative;left: 41.66667%; }.small-pull-5 {position: relative;left: -41.66667%; }.small-offset-4 {margin-left: 33.33333%; }.small-6 {width: 50%; }.small-push-6 {position: relative;left: 50%; }.small-pull-6 {position: relative;left: -50%; }.small-offset-5 {margin-left: 41.66667%; }.small-7 {width: 58.33333%; }.small-push-7 {position: relative;left: 58.33333%; }.small-pull-7 {position: relative;left: -58.33333%; }.small-offset-6 {margin-left: 50%; }.small-8 {width: 66.66667%; }.small-push-8 {position: relative;left: 66.66667%; }.small-pull-8 {position: relative;left: -66.66667%; }.small-offset-7 {margin-left: 58.33333%; }.small-9 {width: 75%; }.small-push-9 {position: relative;left: 75%; }.small-pull-9 {position: relative;left: -75%; }.small-offset-8 {margin-left: 66.66667%; }.small-10 {width: 83.33333%; }.small-push-10 {position: relative;left: 83.33333%; }.small-pull-10 {position: relative;left: -83.33333%; }.small-offset-9 {margin-left: 75%; }.small-11 {width: 91.66667%; }.small-push-11 {position: relative;left: 91.66667%; }.small-pull-11 {position: relative;left: -91.66667%; }.small-offset-10 {margin-left: 83.33333%; }.small-12 {width: 100%; }.small-offset-11 {margin-left: 91.66667%; }.small-up-1 > .column, .small-up-1 > .columns {width: 100%;float: left; }.small-up-1 > .column:nth-of-type(1n), .small-up-1 > .columns:nth-of-type(1n) {clear: none; }.small-up-1 > .column:nth-of-type(1n+1), .small-up-1 > .columns:nth-of-type(1n+1) {clear: both; }.small-up-1 > .column:last-child, .small-up-1 > .columns:last-child {float: left; }.small-up-2 > .column, .small-up-2 > .columns {width: 50%;float: left; }.small-up-2 > .column:nth-of-type(1n), .small-up-2 > .columns:nth-of-type(1n) {clear: none; }.small-up-2 > .column:nth-of-type(2n+1), .small-up-2 > .columns:nth-of-type(2n+1) {clear: both; }.small-up-2 > .column:last-child, .small-up-2 > .columns:last-child {float: left; }.small-up-3 > .column, .small-up-3 > .columns {width: 33.33333%;float: left; }.small-up-3 > .column:nth-of-type(1n), .small-up-3 > .columns:nth-of-type(1n) {clear: none; }.small-up-3 > .column:nth-of-type(3n+1), .small-up-3 > .columns:nth-of-type(3n+1) {clear: both; }.small-up-3 > .column:last-child, .small-up-3 > .columns:last-child {float: left; }.small-up-4 > .column, .small-up-4 > .columns {width: 25%;float: left; }.small-up-4 > .column:nth-of-type(1n), .small-up-4 > .columns:nth-of-type(1n) {clear: none; }.small-up-4 > .column:nth-of-type(4n+1), .small-up-4 > .columns:nth-of-type(4n+1) {clear: both; }.small-up-4 > .column:last-child, .small-up-4 > .columns:last-child {float: left; }.small-up-5 > .column, .small-up-5 > .columns {width: 20%;float: left; }.small-up-5 > .column:nth-of-type(1n), .small-up-5 > .columns:nth-of-type(1n) {clear: none; }.small-up-5 > .column:nth-of-type(5n+1), .small-up-5 > .columns:nth-of-type(5n+1) {clear: both; }.small-up-5 > .column:last-child, .small-up-5 > .columns:last-child {float: left; }.small-up-6 > .column, .small-up-6 > .columns {width: 16.66667%;float: left; }.small-up-6 > .column:nth-of-type(1n), .small-up-6 > .columns:nth-of-type(1n) {clear: none; }.small-up-6 > .column:nth-of-type(6n+1), .small-up-6 > .columns:nth-of-type(6n+1) {clear: both; }.small-up-6 > .column:last-child, .small-up-6 > .columns:last-child {float: left; }.small-up-7 > .column, .small-up-7 > .columns {width: 14.28571%;float: left; }.small-up-7 > .column:nth-of-type(1n), .small-up-7 > .columns:nth-of-type(1n) {clear: none; }.small-up-7 > .column:nth-of-type(7n+1), .small-up-7 > .columns:nth-of-type(7n+1) {clear: both; }.small-up-7 > .column:last-child, .small-up-7 > .columns:last-child {float: left; }.small-up-8 > .column, .small-up-8 > .columns {width: 12.5%;float: left; }.small-up-8 > .column:nth-of-type(1n), .small-up-8 > .columns:nth-of-type(1n) {clear: none; }.small-up-8 > .column:nth-of-type(8n+1), .small-up-8 > .columns:nth-of-type(8n+1) {clear: both; }.small-up-8 > .column:last-child, .small-up-8 > .columns:last-child {float: left; }.column.small-centered, .small-centered.columns {float: none;margin-left: auto;margin-right: auto; }.small-uncenter,.small-push-0,.small-pull-0 {position: static;margin-left: 0;margin-right: 0; }@media screen and (min-width: 40em) {.medium-1 {width: 8.33333%; }.medium-push-1 {position: relative;left: 8.33333%; }.medium-pull-1 {position: relative;left: -8.33333%; }.medium-offset-0 {margin-left: 0%; }.medium-2 {width: 16.66667%; }.medium-push-2 {position: relative;left: 16.66667%; }.medium-pull-2 {position: relative;left: -16.66667%; }.medium-offset-1 {margin-left: 8.33333%; }.medium-3 {width: 25%; }.medium-push-3 {position: relative;left: 25%; }.medium-pull-3 {position: relative;left: -25%; }.medium-offset-2 {margin-left: 16.66667%; }.medium-4 {width: 33.33333%; }.medium-push-4 {position: relative;left: 33.33333%; }.medium-pull-4 {position: relative;left: -33.33333%; }.medium-offset-3 {margin-left: 25%; }.medium-5 {width: 41.66667%; }.medium-push-5 {position: relative;left: 41.66667%; }.medium-pull-5 {position: relative;left: -41.66667%; }.medium-offset-4 {margin-left: 33.33333%; }.medium-6 {width: 50%; }.medium-push-6 {position: relative;left: 50%; }.medium-pull-6 {position: relative;left: -50%; }.medium-offset-5 {margin-left: 41.66667%; }.medium-7 {width: 58.33333%; }.medium-push-7 {position: relative;left: 58.33333%; }.medium-pull-7 {position: relative;left: -58.33333%; }.medium-offset-6 {margin-left: 50%; }.medium-8 {width: 66.66667%; }.medium-push-8 {position: relative;left: 66.66667%; }.medium-pull-8 {position: relative;left: -66.66667%; }.medium-offset-7 {margin-left: 58.33333%; }.medium-9 {width: 75%; }.medium-push-9 {position: relative;left: 75%; }.medium-pull-9 {position: relative;left: -75%; }.medium-offset-8 {margin-left: 66.66667%; }.medium-10 {width: 83.33333%; }.medium-push-10 {position: relative;left: 83.33333%; }.medium-pull-10 {position: relative;left: -83.33333%; }.medium-offset-9 {margin-left: 75%; }.medium-11 {width: 91.66667%; }.medium-push-11 {position: relative;left: 91.66667%; }.medium-pull-11 {position: relative;left: -91.66667%; }.medium-offset-10 {margin-left: 83.33333%; }.medium-12 {width: 98%; margin:auto }.medium-offset-11 {margin-left: 91.66667%; }.medium-up-1 > .column, .medium-up-1 > .columns {width: 100%;float: left; }.medium-up-1 > .column:nth-of-type(1n), .medium-up-1 > .columns:nth-of-type(1n) {clear: none; }.medium-up-1 > .column:nth-of-type(1n+1), .medium-up-1 > .columns:nth-of-type(1n+1) {clear: both; }.medium-up-1 > .column:last-child, .medium-up-1 > .columns:last-child {float: left; }.medium-up-2 > .column, .medium-up-2 > .columns {width: 50%;float: left; }.medium-up-2 > .column:nth-of-type(1n), .medium-up-2 > .columns:nth-of-type(1n) {clear: none; }.medium-up-2 > .column:nth-of-type(2n+1), .medium-up-2 > .columns:nth-of-type(2n+1) {clear: both; }.medium-up-2 > .column:last-child, .medium-up-2 > .columns:last-child {float: left; }.medium-up-3 > .column, .medium-up-3 > .columns {width: 33.33333%;float: left; }.medium-up-3 > .column:nth-of-type(1n), .medium-up-3 > .columns:nth-of-type(1n) {clear: none; }.medium-up-3 > .column:nth-of-type(3n+1), .medium-up-3 > .columns:nth-of-type(3n+1) {clear: both; }.medium-up-3 > .column:last-child, .medium-up-3 > .columns:last-child {float: left; }.medium-up-4 > .column, .medium-up-4 > .columns {width: 25%;float: left; }.medium-up-4 > .column:nth-of-type(1n), .medium-up-4 > .columns:nth-of-type(1n) {clear: none; }.medium-up-4 > .column:nth-of-type(4n+1), .medium-up-4 > .columns:nth-of-type(4n+1) {clear: both; }.medium-up-4 > .column:last-child, .medium-up-4 > .columns:last-child {float: left; }.medium-up-5 > .column, .medium-up-5 > .columns {width: 20%;float: left; }.medium-up-5 > .column:nth-of-type(1n), .medium-up-5 > .columns:nth-of-type(1n) {clear: none; }.medium-up-5 > .column:nth-of-type(5n+1), .medium-up-5 > .columns:nth-of-type(5n+1) {clear: both; }.medium-up-5 > .column:last-child, .medium-up-5 > .columns:last-child {float: left; }.medium-up-6 > .column, .medium-up-6 > .columns {width: 16.66667%;float: left; }.medium-up-6 > .column:nth-of-type(1n), .medium-up-6 > .columns:nth-of-type(1n) {clear: none; }.medium-up-6 > .column:nth-of-type(6n+1), .medium-up-6 > .columns:nth-of-type(6n+1) {clear: both; }.medium-up-6 > .column:last-child, .medium-up-6 > .columns:last-child {float: left; }.medium-up-7 > .column, .medium-up-7 > .columns {width: 14.28571%;float: left; }.medium-up-7 > .column:nth-of-type(1n), .medium-up-7 > .columns:nth-of-type(1n) {clear: none; }.medium-up-7 > .column:nth-of-type(7n+1), .medium-up-7 > .columns:nth-of-type(7n+1) {clear: both; }.medium-up-7 > .column:last-child, .medium-up-7 > .columns:last-child {float: left; }.medium-up-8 > .column, .medium-up-8 > .columns {width: 12.5%;float: left; }.medium-up-8 > .column:nth-of-type(1n), .medium-up-8 > .columns:nth-of-type(1n) {clear: none; }.medium-up-8 > .column:nth-of-type(8n+1), .medium-up-8 > .columns:nth-of-type(8n+1) {clear: both; }.medium-up-8 > .column:last-child, .medium-up-8 > .columns:last-child {float: left; }.column.medium-centered, .medium-centered.columns {float: none;margin-left: auto;margin-right: auto; }.medium-uncenter,.medium-push-0,.medium-pull-0 {position: static;margin-left: 0;margin-right: 0; } }@media screen and (min-width: 64em) {.large-1 {width: 8.33333%; }.large-push-1 {position: relative;left: 8.33333%; }.large-pull-1 {position: relative;left: -8.33333%; }.large-offset-0 {margin-left: 0%; }.large-2 {width: 16.66667%; }.large-push-2 {position: relative;left: 16.66667%; }.large-pull-2 {position: relative;left: -16.66667%; }.large-offset-1 {margin-left: 8.33333%; }.large-3 {width: 25%; }.large-push-3 {position: relative;left: 25%; }.large-pull-3 {position: relative;left: -25%; }.large-offset-2 {margin-left: 16.66667%; }.large-4 {width: 33.33333%; }.large-push-4 {position: relative;left: 33.33333%; }.large-pull-4 {position: relative;left: -33.33333%; }.large-offset-3 {margin-left: 25%; }.large-5 {width: 41.66667%; }.large-push-5 {position: relative;left: 41.66667%; }.large-pull-5 {position: relative;left: -41.66667%; }.large-offset-4 {margin-left: 33.33333%; }.large-6 {width: 50%; }.large-push-6 {position: relative;left: 50%; }.large-pull-6 {position: relative;left: -50%; }.large-offset-5 {margin-left: 41.66667%; }.large-7 {width: 58.33333%; }.large-push-7 {position: relative;left: 58.33333%; }.large-pull-7 {position: relative;left: -58.33333%; }.large-offset-6 {margin-left: 50%; }.large-8 {width: 66.66667%; }.large-push-8 {position: relative;left: 66.66667%; }.large-pull-8 {position: relative;left: -66.66667%; }.large-offset-7 {margin-left: 58.33333%; }.large-9 {width: 75%; }.large-push-9 {position: relative;left: 75%; }.large-pull-9 {position: relative;left: -75%; }.large-offset-8 {margin-left: 66.66667%; }.large-10 {width: 83.33333%; }.large-push-10 {position: relative;left: 83.33333%; }.large-pull-10 {position: relative;left: -83.33333%; }.large-offset-9 {margin-left: 75%; }.large-11 {width: 91.66667%; }.large-push-11 {position: relative;left: 91.66667%; }.large-pull-11 {position: relative;left: -91.66667%; }.large-offset-10 {margin-left: 83.33333%; }.large-12 {width: 98%; margin:auto}.large-offset-11 {margin-left: 91.66667%; }.large-up-1 > .column, .large-up-1 > .columns {width: 100%;float: left; }.large-up-1 > .column:nth-of-type(1n), .large-up-1 > .columns:nth-of-type(1n) {clear: none; }.large-up-1 > .column:nth-of-type(1n+1), .large-up-1 > .columns:nth-of-type(1n+1) {clear: both; }.large-up-1 > .column:last-child, .large-up-1 > .columns:last-child {float: left; }.large-up-2 > .column, .large-up-2 > .columns {width: 50%;float: left; }.large-up-2 > .column:nth-of-type(1n), .large-up-2 > .columns:nth-of-type(1n) {clear: none; }.large-up-2 > .column:nth-of-type(2n+1), .large-up-2 > .columns:nth-of-type(2n+1) {clear: both; }.large-up-2 > .column:last-child, .large-up-2 > .columns:last-child {float: left; }.large-up-3 > .column, .large-up-3 > .columns {width: 33.33333%;float: left; }.large-up-3 > .column:nth-of-type(1n), .large-up-3 > .columns:nth-of-type(1n) {clear: none; }.large-up-3 > .column:nth-of-type(3n+1), .large-up-3 > .columns:nth-of-type(3n+1) {clear: both; }.large-up-3 > .column:last-child, .large-up-3 > .columns:last-child {float: left; }.large-up-4 > .column, .large-up-4 > .columns {width: 25%;float: left; }.large-up-4 > .column:nth-of-type(1n), .large-up-4 > .columns:nth-of-type(1n) {clear: none; }.large-up-4 > .column:nth-of-type(4n+1), .large-up-4 > .columns:nth-of-type(4n+1) {clear: both; }.large-up-4 > .column:last-child, .large-up-4 > .columns:last-child {float: left; }.large-up-5 > .column, .large-up-5 > .columns {width: 20%;float: left; }.large-up-5 > .column:nth-of-type(1n), .large-up-5 > .columns:nth-of-type(1n) {clear: none; }.large-up-5 > .column:nth-of-type(5n+1), .large-up-5 > .columns:nth-of-type(5n+1) {clear: both; }.large-up-5 > .column:last-child, .large-up-5 > .columns:last-child {float: left; }.large-up-6 > .column, .large-up-6 > .columns {width: 16.66667%;float: left; }.large-up-6 > .column:nth-of-type(1n), .large-up-6 > .columns:nth-of-type(1n) {clear: none; }.large-up-6 > .column:nth-of-type(6n+1), .large-up-6 > .columns:nth-of-type(6n+1) {clear: both; }.large-up-6 > .column:last-child, .large-up-6 > .columns:last-child {float: left; }.large-up-7 > .column, .large-up-7 > .columns {width: 14.28571%;float: left; }.large-up-7 > .column:nth-of-type(1n), .large-up-7 > .columns:nth-of-type(1n) {clear: none; }.large-up-7 > .column:nth-of-type(7n+1), .large-up-7 > .columns:nth-of-type(7n+1) {clear: both; }.large-up-7 > .column:last-child, .large-up-7 > .columns:last-child {float: left; }.large-up-8 > .column, .large-up-8 > .columns {width: 12.5%;float: left; }.large-up-8 > .column:nth-of-type(1n), .large-up-8 > .columns:nth-of-type(1n) {clear: none; }.large-up-8 > .column:nth-of-type(8n+1), .large-up-8 > .columns:nth-of-type(8n+1) {clear: both; }.large-up-8 > .column:last-child, .large-up-8 > .columns:last-child {float: left; }.column.large-centered, .large-centered.columns {float: none;margin-left: auto;margin-right: auto; }.large-uncenter,.large-push-0,.large-pull-0 {position: static;margin-left: 0;margin-right: 0; } }div,dl,dt,dd,ul,ol,li,h1,h2,h3,h4,h5,h6,pre,form,p,blockquote,th,td {margin: 0;padding: 0; }p {font-size: inherit;line-height: 1.6;margin-bottom: 1rem;text-rendering: optimizeLegibility; }em,i {font-style: italic;line-height: inherit; }strong,b {font-weight: bold;line-height: inherit; }small {font-size: 80%;line-height: inherit; }h1,h2,h3,h4,h5,h6 {font-family: 'Helvetica Neue', Helvetica, Roboto, Arial, sans-serif;font-weight: normal;font-style: normal;color: inherit;text-rendering: optimizeLegibility;margin-top: 0;margin-bottom: 0.5rem;line-height: 1.4; }h1 small,h2 small,h3 small,h4 small,h5 small,h6 small {color: #cacaca;line-height: 0; }h1 {font-size: 1.5rem; }h2 {font-size: 1.25rem; }h3 {font-size: 1.1875rem; }h4 {font-size: 1.125rem; }h5 {font-size: 1.0625rem; }h6 {font-size: 1rem; }@media screen and (min-width: 40em) {h1 {font-size: 3rem; }h2 {font-size: 2.5rem; }h3 {font-size: 1.9375rem; }h4 {font-size: 1.5625rem; }h5 {font-size: 1.25rem; }h6 {font-size: 1rem; } }a {color: #2199e8;text-decoration: none;line-height: inherit;cursor: pointer; }a:hover, a:focus {color: #1585cf; }a img {border: 0; }hr {max-width: 75rem;height: 0;border-right: 0;border-top: 0;border-bottom: 1px solid #cacaca;border-left: 0;clear: both; }ul,ol,dl {line-height: 1.6;margin-bottom: 1rem;list-style-position: outside; }li {font-size: inherit; }/*ul {list-style-type: disc;}*/ol {margin-left: 1.25rem; }ul ul, ol ul, ul ol, ol ol {margin-bottom: 0;list-style-type: inherit; }dl {margin-bottom: 1rem; }dl dt {margin-bottom: 0.3rem;font-weight: bold; }blockquote {margin: 0 0 1rem;padding: 0.5625rem 1.25rem 0 1.1875rem;border-left: 1px solid #cacaca; }blockquote, blockquote p {line-height: 1.6;color: #8a8a8a; }cite {display: block;font-size: 0.8125rem;color: #8a8a8a; }cite:before {content: '\2014 \0020'; }abbr {color: #0a0a0a;cursor: help;border-bottom: 1px dotted #0a0a0a; }code {font-family: Consolas, 'Liberation Mono', Courier, monospace;font-weight: normal;color: #0a0a0a;background-color: #e6e6e6;border: 1px solid #cacaca;padding: 0.125rem 0.3125rem 0.0625rem; }kbd {padding: 0.125rem 0.25rem 0;margin: 0;background-color: #e6e6e6;color: #0a0a0a;font-family: Consolas, 'Liberation Mono', Courier, monospace; }.subheader {margin-top: 0.2rem;margin-bottom: 0.5rem;font-weight: normal;line-height: 1.4;color: #8a8a8a; }.lead {font-size: 125%;line-height: 1.6; }.stat {font-size: 2.5rem;line-height: 1; }p + .stat {margin-top: -1rem; }.no-bullet {margin-left: 0;list-style: none; }.text-left {text-align: left; }.text-right {text-align: right; }.text-center {text-align: center; }.text-justify {text-align: justify; }@media screen and (min-width: 40em) {.medium-text-left {text-align: left; }.medium-text-right {text-align: right; }.medium-text-center {text-align: center; }.medium-text-justify {text-align: justify; } }@media screen and (min-width: 64em) {.large-text-left {text-align: left; }.large-text-right {text-align: right; }.large-text-center {text-align: center; }.large-text-justify {text-align: justify; } }.show-for-print {display: none !important; }@media print {* {background: transparent !important;color: black !important;box-shadow: none !important;text-shadow: none !important; }.show-for-print {display: block !important; }.hide-for-print {display: none !important; }table.show-for-print {display: table !important; }thead.show-for-print {display: table-header-group !important; }tbody.show-for-print {display: table-row-group !important; }tr.show-for-print {display: table-row !important; }td.show-for-print {display: table-cell !important; }th.show-for-print {display: table-cell !important; }a,a:visited {text-decoration: underline; }a[href]:after {content: ' (' attr(href) ')'; }.ir a:after,a[href^='javascript:']:after,a[href^='#']:after {content: ''; }abbr[title]:after {content: ' (' attr(title) ')'; }pre,blockquote {border: 1px solid #999;page-break-inside: avoid; }thead {display: table-header-group; }tr,img {page-break-inside: avoid; }img {max-width: 100% !important; }@page {margin: 0.5cm; }p,h2,h3 {orphans: 3;widows: 3; }h2,h3 {page-break-after: avoid; } }.button {display: inline-block;text-align: center;line-height: 1;cursor: pointer;-webkit-appearance: none;transition: all 0.25s ease-out;vertical-align: middle;border: 1px solid transparent;border-radius: 0;padding: 0.85em 1em;margin: 0 1rem 1rem 0;font-size: 0.9rem;background: #2199e8;color: #424242; }[data-whatinput='mouse'] .button {outline: 0; }.button:hover, .button:focus {background: #1583cc;color: #424242; }.button.tiny {font-size: 0.6rem; }.button.small {font-size: 0.75rem; }.button.large {font-size: 1.25rem; }.button.expanded {display: block;width: 100%;margin-left: 0;margin-right: 0; }.button.primary {background: #2199e8;color: #424242; }.button.primary:hover, .button.primary:focus {background: #147cc0;color: #424242; }.button.secondary {background: #777;color: #424242; }"
                ' ''lbl_Summary.Text += ".button.secondary:hover, .button.secondary:focus {background: #5f5f5f;color: #424242; }.button.success {background: #3adb76;color: #424242; }.button.success:hover, .button.success:focus {background: #22bb5b;color: #424242; }.button.alert {background: #ec5840;color: #424242; }.button.alert:hover, .button.alert:focus {background: #da3116;color: #424242; }.button.warning {background: #ffae00;color: #424242; }.button.warning:hover, .button.warning:focus {background: #cc8b00;color: #424242; }.button.hollow {border: 1px solid #2199e8;color: #2199e8; }.button.hollow, .button.hollow:hover, .button.hollow:focus {background: transparent; }.button.hollow:hover, .button.hollow:focus {border-color: #0c4d78;color: #0c4d78; }.button.hollow.primary {border: 1px solid #2199e8;color: #2199e8; }.button.hollow.primary:hover, .button.hollow.primary:focus {border-color: #0c4d78;color: #0c4d78; }.button.hollow.secondary {border: 1px solid #777;color: #777; }.button.hollow.secondary:hover, .button.hollow.secondary:focus {border-color: #3c3c3c;color: #3c3c3c; }.button.hollow.success {border: 1px solid #3adb76;color: #3adb76; }.button.hollow.success:hover, .button.hollow.success:focus {border-color: #157539;color: #157539; }.button.hollow.alert {border: 1px solid #ec5840;color: #ec5840; }.button.hollow.alert:hover, .button.hollow.alert:focus {border-color: #881f0e;color: #881f0e; }.button.hollow.warning {border: 1px solid #ffae00;color: #ffae00; }.button.hollow.warning:hover, .button.hollow.warning:focus {border-color: #805700;color: #805700; }.button.disabled {opacity: 0.25;cursor: not-allowed;pointer-events: none; }.button.dropdown::after {content: '';display: block;width: 0;height: 0;border: inset 0.4em;border-color: #fefefe transparent transparent;border-top-style: solid;position: relative;top: 0.4em;float: right;margin-left: 1em;display: inline-block; }.button.arrow-only::after {margin-left: 0;float: none;top: 0.2em; }[type='text'], [type='password'], [type='date'], [type='datetime'], [type='datetime-local'], [type='month'], [type='week'], [type='email'], [type='number'], [type='search'], [type='tel'], [type='time'], [type='url'], [type='color'],textarea {display: block;box-sizing: border-box;width: 100%;height: 2.4375rem;padding: 0.5rem;border: 1px solid #cacaca;margin: 0 0 1rem;font-family: inherit;font-size: 1rem;color: #0a0a0a;background-color: #fefefe;box-shadow: inset 0 1px 2px rgba(10, 10, 10, 0.1);border-radius: 0;transition: box-shadow 0.5s, border-color 0.25s ease-in-out;-webkit-appearance: none;-moz-appearance: none; }[type='text']:focus, [type='password']:focus, [type='date']:focus, [type='datetime']:focus, [type='datetime-local']:focus, [type='month']:focus, [type='week']:focus, [type='email']:focus, [type='number']:focus, [type='search']:focus, [type='tel']:focus, [type='time']:focus, [type='url']:focus, [type='color']:focus,textarea:focus {border: 1px solid #8a8a8a;background: #fefefe;outline: none;box-shadow: 0 0 5px #cacaca;transition: box-shadow 0.5s, border-color 0.25s ease-in-out; }textarea {max-width: 100%; }textarea[rows] {height: auto; }input:disabled, input[readonly],textarea:disabled,textarea[readonly] {/*background-color: #e6e6e6;*/cursor: default; }[type='submit'],[type='button'] {border-radius: 0;-webkit-appearance: none;-moz-appearance: none; }input[type='search'] {box-sizing: border-box; }[type='file'],[type='checkbox'],[type='radio'] {margin: 0 0 1rem; }[type='checkbox'] + label,[type='radio'] + label {display: inline-block;margin-left: 0.5rem;margin-right: 1rem;margin-bottom: 0;vertical-align: baseline; }label > [type='checkbox'],label > [type='label'] {margin-right: 0.5rem; }[type='file'] {width: 100%; }label {display: block;margin: 0;/*font-size: 0.875rem;*/font-weight: normal;line-height: 1.8;color: #0a0a0a; }label.middle {margin: 0 0 1rem;padding: 0.5625rem 0; }.help-text {margin-top: -0.5rem;font-size: 0.8125rem;font-style: italic;color: #333; }.input-group {display: table;width: 100%;margin-bottom: 1rem; }.input-group-label, .input-group-field, .input-group-button {display: table-cell;margin: 0;vertical-align: middle; }.input-group-label {text-align: center;width: 1%;height: 100%;padding: 0 1rem;background: #e6e6e6;color: #0a0a0a;border: 1px solid #cacaca; }.input-group-label:first-child {border-right: 0; }.input-group-label:last-child {border-left: 0; }.input-group-button {height: 100%;padding-top: 0;padding-bottom: 0;text-align: center;width: 1%; }.input-group-button a,.input-group-button input,.input-group-button button {margin: 0; }fieldset {border: 0;padding: 0;margin: 0; }legend {margin-bottom: 0.5rem; }.fieldset {border: 1px solid #cacaca;padding: 1.25rem;margin: 1.125rem 0; }.fieldset legend {background: #fefefe;padding: 0 0.1875rem;margin: 0;margin-left: -0.1875rem; }select {height: 2.5rem;padding: 0 0.8rem;border: 1px solid #cacaca;border-radius: 0;margin: 0 0 1rem;font-size: 1rem;font-family: inherit;line-height: normal;color: #0a0a0a;background-color: #fafafa;border-radius: 0;-webkit-appearance: none;-moz-appearance: none;background-image: url('data:image/svg+xml;utf8,<svg xmlns='http://www.w3.org/2000/svg' version='1.1' width='32' height='24' viewBox='0 0 32 24'><polygon points='0,0 32,0 16,24' style='fill: rgb(51, 51, 51)'></polygon></svg>');background-size: 9px 6px;background-position: right 0.2rem center;background-repeat: no-repeat; }@media screen and (min-width: 0\0) {select {background-image: url('data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAACAAAAAYCAYAAACbU/80AAAAGXRFWHRTb2Z0d2FyZQBBZG9iZSBJbWFnZVJlYWR5ccllPAAAAIpJREFUeNrEkckNgDAMBBfRkEt0ObRBBdsGXUDgmQfK4XhH2m8czQAAy27R3tsw4Qfe2x8uOO6oYLb6GlOor3GF+swURAOmUJ+RwtEJs9WvTGEYxBXqI1MQAZhCfUQKRzDMVj+TwrAIV6jvSUEkYAr1LSkcyTBb/V+KYfX7xAeusq3sLDtGH3kEGACPWIflNZfhRQAAAABJRU5ErkJggg=='); } }select:disabled {background-color: #e6e6e6;cursor: default; }select::-ms-expand {display: none; }select[multiple] {height: auto; }.is-invalid-input:not(:focus) {background-color: rgba(236, 88, 64, 0.1);border-color: #ec5840; }.is-invalid-label {color: #ec5840; }.form-error {display: none;margin-top: -0.5rem;margin-bottom: 1rem;font-size: 0.75rem;font-weight: bold;color: #ec5840; }.form-error.is-visible {display: block; }.hide {display: none; }.invisible {visibility: hidden; }@media screen and (min-width: 0em) and (max-width: 39.9375em) {.hide-for-small-only {display: none !important; } }@media screen and (max-width: 0em), screen and (min-width: 40em) {.show-for-small-only {display: none !important; } }@media screen and (min-width: 40em) {.hide-for-medium {display: none !important; } }@media screen and (max-width: 39.9375em) {.show-for-medium {display: none !important; } }@media screen and (min-width: 40em) and (max-width: 63.9375em) {.hide-for-medium-only {display: none !important; } }@media screen and (max-width: 39.9375em), screen and (min-width: 64em) {.show-for-medium-only {display: none !important; } }@media screen and (min-width: 64em) {.hide-for-large {display: none !important; } }@media screen and (max-width: 63.9375em) {.show-for-large {display: none !important; } }@media screen and (min-width: 64em) and (max-width: 74.9375em) {.hide-for-large-only {display: none !important; } }@media screen and (max-width: 63.9375em), screen and (min-width: 75em) {.show-for-large-only {display: none !important; } }.show-for-sr,.show-on-focus {position: absolute !important;width: 1px;height: 1px;overflow: hidden;clip: rect(0, 0, 0, 0); }.show-on-focus:active, .show-on-focus:focus {position: static !important;height: auto;width: auto;overflow: visible;clip: auto; }.show-for-landscape,.hide-for-portrait {display: block !important; }@media screen and (orientation: landscape) {.show-for-landscape,.hide-for-portrait {display: block !important; } }@media screen and (orientation: portrait) {.show-for-landscape,.hide-for-portrait {display: none !important; } }.hide-for-landscape,.show-for-portrait {display: none !important; }@media screen and (orientation: landscape) {.hide-for-landscape,.show-for-portrait {display: none !important; } }@media screen and (orientation: portrait) {.hide-for-landscape,.show-for-portrait {display: block !important; } }.float-left {float: left !important; }.float-right {float: right !important; }.float-center {display: block;margin-left: auto;margin-right: auto; }.clearfix::before, .clearfix::after {content: ' ';display: table; }.clearfix::after {clear: both; }.accordion {list-style-type: none;background: #fefefe;border: 1px solid #e6e6e6;border-radius: 0;margin-left: 0; }.accordion-title {display: block;padding: 1.25rem 1rem;line-height: 1;font-size: 0.75rem;color: #2199e8;position: relative;border-bottom: 1px solid #e6e6e6; }.accordion-title:hover, .accordion-title:focus {background-color: #e6e6e6; }:last-child > .accordion-title {border-bottom-width: 0; }.accordion-title::before {content: '+';position: absolute;right: 1rem;top: 50%;margin-top: -0.5rem; }.is-active > .accordion-title::before {content: '–'; }.accordion-content {padding: 1.25rem 1rem;display: none;border-bottom: 1px solid #e6e6e6; }.is-accordion-submenu-parent > a {position: relative; }.is-accordion-submenu-parent > a::after {content: '';display: block;width: 0;height: 0;border: inset 6px;border-color: #2199e8 transparent transparent;border-top-style: solid;position: absolute;top: 50%;margin-top: -4px;right: 1rem; }.is-accordion-submenu-parent[aria-expanded='true'] > a::after {-webkit-transform-origin: 50% 50%;-ms-transform-origin: 50% 50%;transform-origin: 50% 50%;-webkit-transform: scaleY(-1);-ms-transform: scaleY(-1);transform: scaleY(-1); }.badge {display: inline-block;padding: 0.3em;min-width: 2.1em;font-size: 0.6rem;text-align: center;border-radius: 50%;background: #2199e8;color: #fefefe; }.badge.secondary {background: #777;color: #fefefe; }.badge.success {background: #3adb76;color: #fefefe; }.badge.alert {background: #ec5840;color: #fefefe; }.badge.warning {background: #ffae00;color: #fefefe; }.breadcrumbs {list-style: none;margin: 0 0 1rem 0; }.breadcrumbs::before, .breadcrumbs::after {content: ' ';display: table; }.breadcrumbs::after {clear: both; }.breadcrumbs li {float: left;color: #0a0a0a;font-size: 0.6875rem;cursor: default;text-transform: uppercase; }.breadcrumbs li:not(:last-child)::after {color: #cacaca;content: '/';margin: 0 0.75rem;position: relative;top: 1px;opacity: 1; }.breadcrumbs a {color: #2199e8; }.breadcrumbs a:hover {text-decoration: underline; }.breadcrumbs .disabled {color: #cacaca; }.button-group {margin-bottom: 1rem;font-size: 0.9rem; }.button-group::before, .button-group::after {content: ' ';display: table; }.button-group::after {clear: both; }.button-group .button {float: left;margin: 0;font-size: inherit; }.button-group .button:not(:last-child) {border-right: 1px solid #fefefe; }.button-group.tiny {font-size: 0.6rem; }.button-group.small {font-size: 0.75rem; }.button-group.large {font-size: 1.25rem; }.button-group.expanded .button:nth-last-child(2):first-child,.button-group.expanded .button:nth-last-child(2):first-child ~ .button {width: 50%; }.button-group.expanded .button:nth-last-child(3):first-child,.button-group.expanded .button:nth-last-child(3):first-child ~ .button {width: 33.33333%; }.button-group.expanded .button:nth-last-child(4):first-child,.button-group.expanded .button:nth-last-child(4):first-child ~ .button {width: 25%; }.button-group.expanded .button:nth-last-child(5):first-child,.button-group.expanded .button:nth-last-child(5):first-child ~ .button {width: 20%; }.button-group.expanded .button:nth-last-child(6):first-child,.button-group.expanded .button:nth-last-child(6):first-child ~ .button {width: 16.66667%; }.button-group.primary .button {background: #2199e8;color: #424242; }.button-group.primary .button:hover, .button-group.primary .button:focus {background: #147cc0;color: #424242; }.button-group.secondary .button {background: #777;color: #424242; }.button-group.secondary .button:hover, .button-group.secondary .button:focus {background: #5f5f5f;color: #424242; }.button-group.success .button {background: #3adb76;color: #424242; }.button-group.success .button:hover, .button-group.success .button:focus {background: #22bb5b;color: #424242; }.button-group.alert .button {background: #ec5840;color: #424242; }.button-group.alert .button:hover, .button-group.alert .button:focus {background: #da3116;color: #424242; }.button-group.warning .button {background: #ffae00;color: #424242; }.button-group.warning .button:hover, .button-group.warning .button:focus {background: #cc8b00;color: #424242; }.button-group.stacked .button, .button-group.stacked-for-small .button {width: 100%;border-right: 0; }@media screen and (min-width: 40em) {.button-group.stacked-for-small .button {width: auto; }.button-group.stacked-for-small .button:not(:last-child) {border-right: 1px solid #fefefe; } }.callout {margin: 0 0 1rem 0;padding: 1rem;border: 1px solid rgba(10, 10, 10, 0.25);border-radius: 0;position: relative;background-color: white; }.callout > :first-child {margin-top: 0; }.callout > :last-child {margin-bottom: 0; }.callout.primary {background-color: #def0fc; }.callout.primary a {color: #116ca8; }.callout.primary a:hover {color: #0a4063; }.callout.secondary {background-color: #ebebeb; }.callout.success {background-color: #e1faea; }.callout.success a {color: #1ea450; }.callout.success a:hover {color: #126330; }.callout.alert {background-color: #fce6e2; }.callout.alert a {color: #bf2b13; }.callout.alert a:hover {color: #791b0c; }.callout.warning {background-color: #fff3d9; }.callout.warning a {color: #b37a00; }.callout.warning a:hover {color: #664600; }.callout.small {padding-top: 0.5rem;padding-right: 0.5rem;padding-bottom: 0.5rem;padding-left: 0.5rem; }.callout.large {padding-top: 3rem;padding-right: 3rem;padding-bottom: 3rem;padding-left: 3rem; }.close-button {position: absolute;color: #8a8a8a;right: 1rem;top: 0.5rem;font-size: 2em;line-height: 1;cursor: pointer; }[data-whatinput='mouse'] .close-button {outline: 0; }.close-button:hover, .close-button:focus {color: #0a0a0a; }.is-drilldown {position: relative;overflow: hidden; }.is-drilldown-submenu {position: absolute;top: 0;left: 100%;z-index: -1;height: 100%;width: 100%;background: #fefefe;transition: -webkit-transform 0.15s linear;transition: transform 0.15s linear; }.is-drilldown-submenu.is-active {z-index: 1;display: block;-webkit-transform: translateX(-100%);-ms-transform: translateX(-100%);transform: translateX(-100%); }.is-drilldown-submenu.is-closing {-webkit-transform: translateX(100%);-ms-transform: translateX(100%);transform: translateX(100%); }.is-drilldown-submenu-parent > a {position: relative; }.is-drilldown-submenu-parent > a::after {content: '';display: block;width: 0;height: 0;border: inset 6px;border-color: transparent transparent transparent #2199e8;border-left-style: solid;position: absolute;top: 50%;margin-top: -6px;right: 1rem; }.js-drilldown-back::before {content: '';display: block;width: 0;height: 0;border: inset 6px;border-color: transparent #2199e8 transparent transparent;border-right-style: solid;float: left;margin-right: 0.75rem;margin-left: 0.6rem;margin-top: 14px; }.dropdown-pane {background-color: #fefefe;border: 1px solid #cacaca;display: block;padding: 1rem;position: absolute;visibility: hidden;width: 300px;z-index: 10;border-radius: 0; }.dropdown-pane.is-open {visibility: visible; }.dropdown-pane.tiny {width: 100px; }.dropdown-pane.small {width: 200px; }.dropdown-pane.large {width: 400px; }[data-whatinput='mouse'] .dropdown.menu a {outline: 0; }.dropdown.menu .is-dropdown-submenu-parent {position: relative; }.dropdown.menu .is-dropdown-submenu-parent a::after {float: right;margin-top: 3px;margin-left: 10px; }.dropdown.menu .is-dropdown-submenu-parent.is-down-arrow a {padding-right: 1.5rem;position: relative; }.dropdown.menu .is-dropdown-submenu-parent.is-down-arrow > a::after {content: '';display: block;width: 0;height: 0;border: inset 5px;border-color: #2199e8 transparent transparent;border-top-style: solid;position: absolute;top: 12px;right: 5px; }.dropdown.menu .is-dropdown-submenu-parent.is-left-arrow > a::after {content: '';display: block;width: 0;height: 0;border: inset 5px;border-color: transparent #2199e8 transparent transparent;border-right-style: solid;float: left;margin-left: 0;margin-right: 10px; }.dropdown.menu .is-dropdown-submenu-parent.is-right-arrow > a::after {content: '';display: block;width: 0;height: 0;border: inset 5px;border-color: transparent transparent transparent #2199e8;border-left-style: solid; }.dropdown.menu .is-dropdown-submenu-parent.is-left-arrow.opens-inner .submenu {right: 0;left: auto; }.dropdown.menu .is-dropdown-submenu-parent.is-right-arrow.opens-inner .submenu {left: 0;right: auto; }.dropdown.menu .is-dropdown-submenu-parent.opens-inner .submenu {top: 100%; }.no-js .dropdown.menu ul {display: none; }.dropdown.menu .submenu {display: none;position: absolute;top: 0;left: 100%;min-width: 200px;z-index: 1;background: #fefefe;border: 1px solid #cacaca; }.dropdown.menu .submenu > li {width: 100%; }.dropdown.menu .submenu.first-sub {top: 100%;left: 0;right: auto; }.dropdown.menu .submenu:not(.js-dropdown-nohover) > .is-dropdown-submenu-parent:hover > .dropdown.menu .submenu, .dropdown.menu .submenu.js-dropdown-active {display: block; }.dropdown.menu .is-dropdown-submenu-parent.opens-left .submenu {left: auto;right: 100%; }.dropdown.menu.align-right .submenu.first-sub {top: 100%;left: auto;right: 0; }.is-dropdown-menu.vertical {width: 100px; }.is-dropdown-menu.vertical.align-right {float: right; }.is-dropdown-menu.vertical > li .submenu {top: 0;left: 100%; }.flex-video {position: relative;height: 0;padding-top: 1.5625rem;padding-bottom: 75%;margin-bottom: 1rem;overflow: hidden; }.flex-video iframe,.flex-video object,.flex-video embed,.flex-video video {position: absolute;top: 0;left: 0;width: 100%;height: 100%; }.flex-video.widescreen {padding-bottom: 56.25%; }.flex-video.vimeo {padding-top: 0; }.label {display: inline-block;padding: 0.33333rem 0.5rem;font-size: 0.8rem;line-height: 1;white-space: nowrap;cursor: default;border-radius: 0;background: #2199e8;color: #fefefe; }.label.secondary {background: #777;color: #fefefe; }.label.success {background: #3adb76;color: #fefefe; }.label.alert {background: #ec5840;color: #fefefe; }.label.warning {background: #ffae00;color: #fefefe; }.media-object {margin-bottom: 1rem;display: block; }.media-object img {max-width: none; }@media screen and (min-width: 0em) and (max-width: 39.9375em) {.media-object.stack-for-small .media-object-section {display: block;padding: 0;padding-bottom: 1rem; }.media-object.stack-for-small .media-object-section img {width: 100%; } }.media-object-section {display: table-cell;vertical-align: top; }.media-object-section:first-child {padding-right: 1rem; }.media-object-section:last-child:not( + .media-object-section:first-child) {padding-left: 1rem; }.media-object-section.middle {vertical-align: middle; }.media-object-section.bottom {vertical-align: bottom; }.menu {margin: 0;list-style-type: none; }.menu > li {display: table-cell;vertical-align: middle; }[data-whatinput='mouse'] .menu > li {outline: 0; }.menu > li:not(.menu-text) > a {display: block;padding: 0.7rem 1rem;line-height: 1; }.menu input,.menu a,.menu button {margin-bottom: 0; }.menu > li > a > img,.menu > li > a > i {vertical-align: middle; }.menu > li > a > span {vertical-align: middle; }.menu > li > a > img,.menu > li > a > i {display: inline-block;margin-right: 0.25rem; }.menu > li {display: table-cell; }.menu.vertical > li {display: block; }@media screen and (min-width: 40em) {.menu.medium-horizontal > li {display: table-cell; }.menu.medium-vertical > li {display: block; } }@media screen and (min-width: 64em) {.menu.large-horizontal > li {display: table-cell; }.menu.large-vertical > li {display: block; } }.menu.simple a {padding: 0;margin-right: 1rem; }.menu.align-right > li {float: right; }.menu.expanded {display: table;width: 100%; }.menu.expanded > li:nth-last-child(2):first-child,.menu.expanded > li:nth-last-child(2):first-child ~ li {width: 50%; }.menu.expanded > li:nth-last-child(3):first-child,.menu.expanded > li:nth-last-child(3):first-child ~ li {width: 33.33333%; }.menu.expanded > li:nth-last-child(4):first-child,.menu.expanded > li:nth-last-child(4):first-child ~ li {width: 25%; }.menu.expanded > li:nth-last-child(5):first-child,.menu.expanded > li:nth-last-child(5):first-child ~ li {width: 20%; }.menu.expanded > li:nth-last-child(6):first-child,.menu.expanded > li:nth-last-child(6):first-child ~ li {width: 16.66667%; }.menu.expanded > li:first-child:last-child {width: 100%; }.menu.icon-top > li > a {text-align: center; }.menu.icon-top > li > a > img,.menu.icon-top > li > a > i {display: block;margin: 0 auto 0.25rem; }.menu.nested {margin-left: 1rem; }.menu-text {font-weight: bold;color: inherit;line-height: 1;padding-top: 0;padding-bottom: 0;padding: 0.7rem 1rem; }html,body {height: 100%; }.off-canvas-wrapper {width: 100%;overflow-x: hidden;position: relative;-webkit-backface-visibility: hidden;backface-visibility: hidden;-webkit-overflow-scrolling: auto; }.off-canvas-wrapper-inner {position: relative;width: 100%;transition: -webkit-transform 0.5s ease;transition: transform 0.5s ease; }.off-canvas-wrapper-inner::before, .off-canvas-wrapper-inner::after {content: ' ';display: table; }.off-canvas-wrapper-inner::after {clear: both; }.off-canvas-content,.off-canvas-content {min-height: 100%;background: #fefefe;transition: -webkit-transform 0.5s ease;transition: transform 0.5s ease;-webkit-backface-visibility: hidden;backface-visibility: hidden;z-index: 1;box-shadow: 0 0 10px rgba(10, 10, 10, 0.5); }.js-off-canvas-exit {display: none;position: absolute;top: 0;left: 0;width: 100%;height: 100%;background: rgba(254, 254, 254, 0.25);cursor: pointer;transition: background 0.5s ease; }.is-off-canvas-open .js-off-canvas-exit {display: block; }.off-canvas {position: absolute;background: #e6e6e6;z-index: -1;max-height: 100%;overflow-y: auto;-webkit-transform: translateX(0px);-ms-transform: translateX(0px);transform: translateX(0px); }[data-whatinput='mouse'] .off-canvas {outline: 0; }.off-canvas.position-left {left: -250px;top: 0;width: 250px; }.is-open-left {-webkit-transform: translateX(250px);-ms-transform: translateX(250px);transform: translateX(250px); }.off-canvas.position-right {right: -250px;top: 0;width: 250px; }.is-open-right {-webkit-transform: translateX(-250px);-ms-transform: translateX(-250px);transform: translateX(-250px); }@media screen and (min-width: 40em) {.position-left.reveal-for-medium {left: 0;z-index: auto;position: fixed; }.position-left.reveal-for-medium ~ .off-canvas-content {margin-left: 250px; }.position-right.reveal-for-medium {right: 0;z-index: auto;position: fixed; }.position-right.reveal-for-medium ~ .off-canvas-content {margin-right: 250px; } }@media screen and (min-width: 64em) {.position-left.reveal-for-large {left: 0;z-index: auto;position: fixed; }.position-left.reveal-for-large ~ .off-canvas-content {margin-left: 250px; }.position-right.reveal-for-large {right: 0;z-index: auto;position: fixed; }.position-right.reveal-for-large ~ .off-canvas-content {margin-right: 250px; } }.orbit {position: relative; }.orbit-container {position: relative;margin: 0;overflow: hidden;list-style: none; }.orbit-slide {width: 100%;max-height: 100%; }.orbit-slide.no-motionui.is-active {top: 0;left: 0; }.orbit-figure {margin: 0; }.orbit-image {margin: 0;width: 100%;max-width: 100%; }.orbit-caption {position: absolute;bottom: 0;width: 100%;padding: 1rem;margin-bottom: 0;color: #fefefe;background-color: rgba(10, 10, 10, 0.5); }.orbit-previous, .orbit-next {position: absolute;top: 50%;-webkit-transform: translateY(-50%);-ms-transform: translateY(-50%);transform: translateY(-50%);z-index: 10;padding: 1rem;color: #fefefe; }[data-whatinput='mouse'] .orbit-previous, [data-whatinput='mouse'] .orbit-next {outline: 0; }.orbit-previous:hover, .orbit-next:hover, .orbit-previous:active, .orbit-next:active, .orbit-previous:focus, .orbit-next:focus {background-color: rgba(10, 10, 10, 0.5); }.orbit-previous {left: 0; }.orbit-next {left: auto;right: 0; }.orbit-bullets {position: relative;margin-top: 0.8rem;margin-bottom: 0.8rem;text-align: center; }[data-whatinput='mouse'] .orbit-bullets {outline: 0; }.orbit-bullets button {width: 1.2rem;height: 1.2rem;margin: 0.1rem;background-color: #cacaca;border-radius: 50%; }.orbit-bullets button:hover {background-color: #8a8a8a; }.orbit-bullets button.is-active {background-color: #8a8a8a; }.pagination {margin-left: 0;margin-bottom: 1rem; }.pagination::before, .pagination::after {content: ' ';display: table; }.pagination::after {clear: both; }.pagination li {font-size: 0.875rem;margin-right: 0.0625rem;display: none;border-radius: 0; }.pagination li:last-child, .pagination li:first-child {display: inline-block; }@media screen and (min-width: 40em) {.pagination li {display: inline-block; } }.pagination a,.pagination button {color: #0a0a0a;display: block;padding: 0.1875rem 0.625rem;border-radius: 0; }.pagination a:hover,.pagination button:hover {background: #e6e6e6; }.pagination .current {padding: 0.1875rem 0.625rem;background: #2199e8;color: #fefefe;cursor: default; }.pagination .disabled {padding: 0.1875rem 0.625rem;color: #cacaca;cursor: default; }.pagination .disabled:hover {background: transparent; }.pagination .ellipsis::after {content: '…';padding: 0.1875rem 0.625rem;color: #0a0a0a; }.pagination-previous a::before,.pagination-previous.disabled::before {content: '«';display: inline-block;margin-right: 0.5rem; }.pagination-next a::after,.pagination-next.disabled::after {content: '»';display: inline-block;margin-left: 0.5rem; }.progress {background-color: #cacaca;height: 1rem;margin-bottom: 1rem;border-radius: 0; }.progress.primary .progress-meter {background-color: #2199e8; }.progress.secondary .progress-meter {background-color: #777; }.progress.success .progress-meter {background-color: #3adb76; }.progress.alert .progress-meter {background-color: #ec5840; }.progress.warning .progress-meter {background-color: #ffae00; }.progress-meter {position: relative;display: block;width: 0%;height: 100%;background-color: #2199e8;border-radius: 0; }.progress-meter .progress-meter-text {position: absolute;top: 50%;left: 50%;-webkit-transform: translate(-50%, -50%);-ms-transform: translate(-50%, -50%);transform: translate(-50%, -50%);margin: 0;font-size: 0.75rem;font-weight: bold;color: #fefefe;white-space: nowrap; }.slider {position: relative;height: 0.5rem;margin-top: 1.25rem;margin-bottom: 2.25rem;background-color: #e6e6e6;cursor: pointer;-webkit-user-select: none;-moz-user-select: none;-ms-user-select: none;user-select: none;-ms-touch-action: none;touch-action: none; }.slider-fill {position: absolute;top: 0;left: 0;display: inline-block;max-width: 100%;height: 0.5rem;background-color: #cacaca;transition: all 0.2s ease-in-out; }.slider-fill.is-dragging {transition: all 0s linear; }.slider-handle {position: absolute;top: 50%;-webkit-transform: translateY(-50%);-ms-transform: translateY(-50%);transform: translateY(-50%);position: absolute;left: 0;z-index: 1;display: inline-block;width: 1.4rem;height: 1.4rem;background-color: #2199e8;transition: all 0.2s ease-in-out;-ms-touch-action: manipulation;touch-action: manipulation;border-radius: 0; }[data-whatinput='mouse'] .slider-handle {outline: 0; }.slider-handle:hover {background-color: #1583cc; }.slider-handle.is-dragging {transition: all 0s linear; }.slider.disabled,.slider[disabled] {opacity: 0.25;cursor: not-allowed; }.slider.vertical {display: inline-block;width: 0.5rem;height: 12.5rem;margin: 0 1.25rem;-webkit-transform: scale(1, -1);-ms-transform: scale(1, -1);transform: scale(1, -1); }.slider.vertical .slider-fill {top: 0;width: 0.5rem;max-height: 100%; }.slider.vertical .slider-handle {position: absolute;top: 0;left: 50%;width: 1.4rem;height: 1.4rem;-webkit-transform: translateX(-50%);-ms-transform: translateX(-50%);transform: translateX(-50%); }.sticky-container {position: relative; }.sticky {position: absolute;z-index: 0;-webkit-transform: translate3d(0, 0, 0);transform: translate3d(0, 0, 0); }.sticky.is-stuck {position: fixed;z-index: 5; }.sticky.is-stuck.is-at-top {top: 0; }.sticky.is-stuck.is-at-bottom {bottom: 0; }.sticky.is-anchored {position: absolute;left: auto;right: auto; }.sticky.is-anchored.is-at-bottom {bottom: 0; }body.is-reveal-open {overflow: hidden; }.reveal-overlay {display: none;position: fixed;top: 0;bottom: 0;left: 0;right: 0;z-index: 1005;background-color: rgba(10, 10, 10, 0.45);overflow-y: scroll; }.reveal {display: none;z-index: 1006;padding: 1rem;border: 1px solid #cacaca;margin: 100px auto 0 auto;background-color: #fefefe;border-radius: 0;position: absolute;overflow-y: auto; }[data-whatinput='mouse'] .reveal {outline: 0; }@media screen and (min-width: 40em) {.reveal {min-height: 0; } }.reveal .column, .reveal .columns,.reveal .columns {min-width: 0; }.reveal > :last-child {margin-bottom: 0; }@media screen and (min-width: 40em) {.reveal {width: 600px;max-width: 75rem; } }.reveal.collapse {padding: 0; }@media screen and (min-width: 40em) {.reveal .reveal {left: auto;right: auto;margin: 0 auto; } }@media screen and (min-width: 40em) {.reveal.tiny {width: 30%;max-width: 75rem; } }@media screen and (min-width: 40em) {.reveal.small {width: 50%;max-width: 75rem; } }@media screen and (min-width: 40em) {.reveal.large {width: 90%;max-width: 75rem; } }.reveal.full {top: 0;left: 0;width: 100%;height: 100%;height: 100vh;min-height: 100vh;max-width: none;margin-left: 0; }.switch {margin-bottom: 1rem;outline: 0;position: relative;-webkit-user-select: none;-moz-user-select: none;-ms-user-select: none;user-select: none;color: #fefefe;font-weight: bold;font-size: 0.875rem; }.switch-input {opacity: 0;position: absolute; }.switch-paddle {background: #cacaca;cursor: pointer;display: block;position: relative;width: 4rem;height: 2rem;transition: all 0.25s ease-out;border-radius: 0;color: inherit;font-weight: inherit; }input + .switch-paddle {margin: 0; }.switch-paddle::after {background: #fefefe;content: '';display: block;position: absolute;height: 1.5rem;left: 0.25rem;top: 0.25rem;width: 1.5rem;transition: all 0.25s ease-out;-webkit-transform: translate3d(0, 0, 0);transform: translate3d(0, 0, 0);border-radius: 0; }input:checked ~ .switch-paddle {background: #2199e8; }input:checked ~ .switch-paddle::after {left: 2.25rem; }[data-whatinput='mouse'] input:focus ~ .switch-paddle {outline: 0; }.switch-active, .switch-inactive {position: absolute;top: 50%;-webkit-transform: translateY(-50%);-ms-transform: translateY(-50%);transform: translateY(-50%); }.switch-active {left: 8%;display: none; }input:checked + label > .switch-active {display: block; }.switch-inactive {right: 15%; }input:checked + label > .switch-inactive {display: none; }.switch.tiny .switch-paddle {width: 3rem;height: 1.5rem;font-size: 0.625rem; }.switch.tiny .switch-paddle::after {width: 1rem;height: 1rem; }.switch.tiny input:checked ~ .switch-paddle:after {left: 1.75rem; }.switch.small .switch-paddle {width: 3.5rem;height: 1.75rem;font-size: 0.75rem; }.switch.small .switch-paddle::after {width: 1.25rem;height: 1.25rem; }.switch.small input:checked ~ .switch-paddle:after {left: 2rem; }.switch.large .switch-paddle {width: 5rem;height: 2.5rem;font-size: 1rem; }.switch.large .switch-paddle::after {width: 2rem;height: 2rem; }.switch.large input:checked ~ .switch-paddle:after {left: 2.75rem; }table {margin-bottom: 1rem;border-radius: 0; }thead,tbody,tfoot {border: 1px solid #f1f1f1;background-color: #fefefe;text-align:left !important;}caption {font-weight: bold;padding: 0.5rem 0.625rem 0.625rem; }thead,tfoot {background: #f8f8f8;color: #0a0a0a; }thead tr,tfoot tr {background: transparent; }thead th,thead td,tfoot th,tfoot td {padding: 0.5rem 0.625rem 0.625rem;font-weight: bold;text-align: left !important;}tbody tr:nth-child(even) {background-color: #f1f1f1; }tbody th,tbody td {padding: 0.4rem 0.325rem 0.125rem; }@media screen and (max-width: 63.9375em) {table.stack thead {display: none; }table.stack tfoot {display: none; }table.stack tr,table.stack th,table.stack td {display: block; }table.stack td {border-top: 0; } }table.scroll {display: block;width: 100%;overflow-y: scroll; }table.hover tr:hover {background-color: #f9f9f9; }table.hover tr:nth-of-type(even):hover {background-color: #ececec; }.tabs {margin: 0;list-style-type: none;background: #fefefe;border: 1px solid #e6e6e6; }.tabs::before, .tabs::after {content: ' ';display: table; }.tabs::after {clear: both; }.tabs.simple > li > a {padding: 0; }.tabs.simple > li > a:hover {background: transparent; }.tabs.vertical > li {width: auto;float: none;display: block; }.tabs.primary {background: #2199e8; }.tabs.primary > li > a {color: #fefefe; }.tabs.primary > li > a:hover, .tabs.primary > li > a:focus {background: #1893e4; }.tabs-title {float: left; }.tabs-title > a {display: block;padding: 1.25rem 1.5rem;line-height: 1;font-size: 12px;color: #2199e8; }.tabs-title > a:hover {background: #fefefe; }.tabs-title > a:focus, .tabs-title > a[aria-selected='true'] {background: #e6e6e6; }.tabs-content {background: #fefefe;transition: all 0.5s ease;border: 1px solid #e6e6e6;border-top: 0; }.tabs-content.vertical {border: 1px solid #e6e6e6;border-left: 0; }.tabs-panel {display: none;padding: 1rem; }.tabs-panel.is-active {display: block; }.thumbnail {border: solid 4px #fefefe;box-shadow: 0 0 0 1px rgba(10, 10, 10, 0.2);display: inline-block;line-height: 0;max-width: 100%;transition: box-shadow 200ms ease-out;border-radius: 0;margin-bottom: 1rem; }.thumbnail:hover, .thumbnail:focus {box-shadow: 0 0 6px 1px rgba(33, 153, 232, 0.5); }.title-bar {background: #0a0a0a;color: #fefefe;padding: 0.5rem; }.title-bar::before, .title-bar::after {content: ' ';display: table; }.title-bar::after {clear: both; }.title-bar .menu-icon {margin-left: 0.25rem;margin-right: 0.5rem; }.title-bar-left {float: left; }.title-bar-right {float: right;text-align: right; }.title-bar-title {font-weight: bold;vertical-align: middle;display: inline-block; }.menu-icon {position: relative;display: inline-block;vertical-align: middle;cursor: pointer;width: 20px;height: 16px; }.menu-icon::after {content: '';position: absolute;display: block;width: 100%;height: 2px;background: #fefefe;top: 0;left: 0;box-shadow: 0 7px 0 #fefefe, 0 14px 0 #fefefe; }.menu-icon:hover::after {background: #cacaca;box-shadow: 0 7px 0 #cacaca, 0 14px 0 #cacaca; }.has-tip {border-bottom: dotted 1px #8a8a8a;font-weight: bold;position: relative;display: inline-block;cursor: help; }.tooltip {background-color: #0a0a0a;color: #fefefe;font-size: 80%;padding: 0.75rem;position: absolute;z-index: 10;top: calc(100% + 0.6495rem);max-width: 10rem !important;border-radius: 0; }.tooltip::before {content: '';display: block;width: 0;height: 0;border: inset 0.75rem;border-color: transparent transparent #0a0a0a;border-bottom-style: solid;bottom: 100%;position: absolute;left: 50%;-webkit-transform: translateX(-50%);-ms-transform: translateX(-50%);transform: translateX(-50%); }.tooltip.top::before {content: '';display: block;width: 0;height: 0;border: inset 0.75rem;border-color: #0a0a0a transparent transparent;border-top-style: solid;top: 100%;bottom: auto; }.tooltip.left::before {content: '';display: block;width: 0;height: 0;border: inset 0.75rem;border-color: transparent transparent transparent #0a0a0a;border-left-style: solid;bottom: auto;left: 100%;top: 50%;-webkit-transform: translateY(-50%);-ms-transform: translateY(-50%);transform: translateY(-50%); }.tooltip.right::before {content: '';display: block;width: 0;height: 0;border: inset 0.75rem;border-color: transparent #0a0a0a transparent transparent;border-right-style: solid;bottom: auto;left: auto;right: 100%;top: 50%;-webkit-transform: translateY(-50%);-ms-transform: translateY(-50%);transform: translateY(-50%); }.top-bar {padding: 0.5rem; }.top-bar::before, .top-bar::after {content: ' ';display: table; }.top-bar::after {clear: both; }.top-bar,.top-bar ul {background-color: #e6e6e6; }.top-bar a {color: #2199e8; }.top-bar input {width: 200px;margin-right: 1rem; }@media screen and (min-width: 40em) {.top-bar-left {float: left; }.top-bar-right {float: right; } }</style>"
                ' '' lbl_Summary.Text += "<style type='text/css'>body {font-family: Arial, Helvetica, sans-serif !important;font-size: 12px !important;line-height: 140%;margin: 0px;padding: 0px;}.red{color:#f00; font-weight:bold;}.mrgt40{margin-right:40px;}.mlft40{margin-left:40px;}.bgblue{background-color:#004b91; border-bottom:6px solid #f58220; border-top:6px solid #f58220; padding:10px 0;}.w100 {width: 100%;}.w98 {width: 98%;}.w95 {width: 95%;}.w90 {width: 90%;}.w80 {width: 80%;}.w70 {width: 70%;}.w60 {width: 60%;}.w55 {width: 55%;}.w50 {width: 50%;}.w49 {width: 49%;}.w48 {width: 48%;}.w45 {width: 45%;}.w40 {width: 40%;}.w33 {width: 33%;}.w30 {width: 30%;}.w25 {width: 25%;}.w20 {width: 20%;}.w19{width:19%;}.w15 {width: 15%;}.w10 {width: 10%;}.w8 {width: 8%;}.w5 {width: 5%;}.w2 {width: 2%;}.clear {clear: both;}.clear1 {padding: 5px;clear: both;}.auto {margin: auto;}.mauto {margin: auto;}.topitz {height: 130px;background-color: #424242;width: 100%;top: 0px;clear: both;}.lft {float: left;}.rgt {float: right;}/*input[type=text] {width: 98%;border: 2px solid #666;height: 30px;}input[type='text']:focus {outline: none;}input[type=password] {width: 98%;border: 2px solid #666;height: 30px;}input[type='password']:focus {outline: none;}*//*input[type=checkbox] {margin-top: 15px;}*/.daybtn{background-color:#004b91 !important;}input[type='submit'] {background-color: #f58220;color: #ffffff !important;width: 90%;height: 37px;font-weight: bold;text-align: center;border-radius: 6px;border: 2px solid #ffffff;cursor: pointer;}input[type='submit']:focus {background-color: #004b91;}input[type='button'] {background-color: #f58220;color: #ffffff !important;width: 90%;height: 37px;font-weight: bold;text-align: center;border-radius: 6px;border: 2px solid #ffffff;cursor: pointer;}input[type='button']:focus {background-color: #004b91;}.btn{background-color: #f58220;color: #ffffff !important;width: 90%;height: 37px;font-weight: bold;text-align: center;padding-top:7px;border-radius: 6px;border: 2px solid #ffffff;cursor: pointer;}/*select {width: 98%;border: 2px solid #666;height: 30px;}select {outline: none;}*/.select1 {height: 42px !important;border: 2px solid #666;width: 98%;}.lft {float: left;}.rgt {float: right;}.loginback{width:100%; background-color:#fff;}.header {height: 100px;background-color: #424242;width: 100%;top: 0px;clear: both;}.heading{background-image: url(../Images/menubg.jpg); background-repeat:repeat-x; padding:5px; border:thin solid #ccc; border-radius:5px;}.heading1{color:#fff; font-weight:bold; padding-top:3px;}.bodypage1 {width: 100%;background-color: #f3f2fe;}.auto {margin: auto;}.mauto {margin: auto;}.clear {clear: both;}.clear1 {clear: both;padding: 8px;}#main {width: 970px;margin-left: auto;margin-right: auto;}#menu {width: 110px;position: absolute;top: 50px;left: 60%;height: 30px;display: none;}.menu {position: absolute;float: left;margin-left: 30px;}.menu ul {list-style: none;}.menu ul li {float: left;padding: 3px 5px;}.hidden {position: absolute;/*clip: rect(1px 1px 1px 1px); /* IE6 & 7 */clip: rect(1px, 1px, 1px, 1px);}#content {width: 100%;}.sec1 {float: left;width: 100%;margin-right: 10px;margin-bottom: 10px;text-align: justify;}.sec2 {float: left;width: 100%;margin-bottom: 10px;text-align: justify;}.copyright {font-size: 11px;color: #424242;text-decoration: none;background-color: #d90000;width: 970px;margin-top: 30px;margin-right: auto;margin-left: auto;padding-bottom: 10px;padding-top: 10px;}.leftcontent {float: left;width: 670px;padding-right: 10px;}.rightcontent {float: right;width: 280px;padding-right: 10px;}p {font-family: 'Times New Roman', Times, serif;font-size: 16px;font-weight: bold;color: #d90000;font-style: italic;}.titalr {font-size: 16px;font-weight: bold;clear: both;display: none;}.tital {font-size: 20px;font-weight: bold;clear: both;padding: 5px 0px;}.igtmenu {background-image: url(../Images/menu-bg1.jpg);background-repeat: repeat-x;line-height: 40px;color: #424242;padding: 0px 10px;font-size: 11px;position:relative;clear:both;}.plft5{padding-left:5px;}/*.nav {margin: 0;padding: 0;border: 0;font-size: 14px;font: inherit;color: #000;}*/a:link, a:visited {font-size: 100%;color: #1996e6;text-decoration: none;}.menur {display: none;padding-top: 90px;}.cplnew {color: #424242;font-weight: bold;vertical-align: top;}.settings {position: absolute;background: #20313f;width: 100px;line-height: 20px;z-index:1;}.settings div {padding: 5px 10px;border-bottom: 1px dotted #ccc;}.settings div:hover {background: #004b91;}.settings a {color: #424242;}.hide {display: none;}.ptop10 {padding-top: 10px;}.pointer {cursor: pointer;}.mtop10 {margin-top: 10px;}.mtop20 {margin-top: 20px;}.mtop30 {margin-top: 30px;}.mtop80 {margin-top: 80px;}.mbot10{margin-bottom:100px;}.serchbox {background-color: #004b91;padding: 2%;border-radius: 4px;color: #424242;}.starbg{background: url(../images/bg_star.png);background-repeat:no-repeat;background-position:top right;}.wht {background-color: #424242;}.black {color: #000;}.blue {color:#004b91;}.mright10 {margin-right: 10px;}.passengerr {display: none;}/*.passenger {display: block;}*/.fade {background: url(../images/fade.png);height: 90%;width: 100%;position: fixed;z-index: 9999;display: none;left: 0;top: 0;}.spn1 {float: left;background: #004b91 url(../images/selected1.png) no-repeat left;cursor: pointer;color: #424242;font-weight: bold;padding:2% 5%;}.spn {float: left;background: url(../images/selected2.png) no-repeat left;cursor: pointer;font-weight: bold;color: #272727;padding:2% 5%;}.p10{padding:10px;}.bld{font-weight:bold;}.matrix {text-align: center;margin: 0;width: 99.8%;background: #fff;padding: 5px;box-shadow: 0px 0px 5px #b9b9b9;border: 2px solid #d1d1d1;}.matrix tr td {margin: 2%;border: 1px solid #f1f1f1;font-size: 1em;line-height: 25px;text-align: center;}.matrix tr td:hover {text-decoration: underline;}.matrix tr td div {cursor: pointer;width: 100%;list-style-type: none;margin: 0px;padding: 0px;}.mtrx1 {background: #f1f1f1 url(../images/selectico.png) right bottom no-repeat;}.bggray{background-color:#ccc;}.fltnewmenu1{width:100%; min-width:180px; padding:5px; margin-bottom:10px; border-bottom:1px solid #f3f2fe;}@media screen and (min-width: 240px) and (max-width: 480px) {body {font-size:70% !important;}.modsearch{display:none;}.mtop80 {margin-top: 40px;}.w48 {width: 100%;}.w25{width:100%;}.passengerr {display: block;}.passenger {display: none;}.igtmenu {display: none;}.menur {display: block;}.nav {position: relative;min-height: 40px;}.nav ul {width: 98%;padding: 5px 0;position: absolute;top: 0;left: 0;border: solid 1px #aaa;background: #004b91 url(../images/icon-menu.png) no-repeat 10px 11px;border-radius: 5px;box-shadow: 0 1px 2px rgba(0,0,0,.3);list-style:none;}.nav li {display: none; /* hide all <li> items */margin: 0;}.nav .current {display: block; /* show only current <li> item */}.nav a {display: block;padding: 5px 5px 5px 40px;text-align: left;color: #424242;}.nav .current a {background: none;color: #424242;padding: 5px 5px 5px 40px;}/* on nav hover */.nav ul:hover {background-image: none;}.nav ul:hover li {display: block;margin: 0 0 5px;}.nav ul:hover .current {background: url(../images/icon-check.png) no-repeat 10px 7px;}/* right nav */.nav.right ul {left: auto;right: 0;}/* center nav */.nav.center ul {left: 50%;margin-left: -90px;}.w20 {width: 90%;}.crdrow {display: none;}#main {width: 100%;margin-left: auto;margin-right: auto;}#content {width: 100%;}.sec1 {float: left;width: 100%;margin-bottom: 10px;text-align: justify;}.menu {display: none;}.sec2 {margin:auto !important;width: 90% !important;margin-right: 10px;margin-bottom: 10px;text-align: justify;}.leftcontent {width: 90%;text-align: justify;}.rightcontent {width: 90%;text-align: justify;}.copyright {width: 100%;}.tital {display: none;}.titalr {display: block;}}@media screen and (min-width: 480px) and (max-width: 767px) {body {font-size:70% !important;}}@media screen and (min-width: 13600px) and (max-width: 2200px) {body {font-size:16px !important;}</style>"
                'lbl_Summary.Text += "</head><body>"
                'lbl_Summary.Text += objTktCopy.TicketDetail(OrderId, TransTD)
                'lbl_Summary.Text += "</body></html>"

                'LabelTkt.Text = "<html xmlns='http://www.w3.org/1999/xhtml'><head><title>ticket details</title>"
                'LabelTkt.Text += "</head><body>"
                LabelTkt.Text = TicketCopyExportPDF(OrderId, TransTD)
                ''LabelTkt.Text += "</body></html>"
                Dim exword As String = Request.QueryString("exword")
                If (exword <> "Nothing" And exword = "true") Then
                    btn_exporttoword_Click(sender, e)
                End If
            End If
        Catch ex As Exception
            clsErrorLog.LogInfo(ex)

        End Try

    End Sub

    Protected Sub lnklogout_Click1(ByVal sender As Object, ByVal e As EventArgs) Handles LinkButton1.Click
        Try
            FormsAuthentication.SignOut()
            Session.Abandon()
            Response.Redirect("~/Login.aspx")
        Catch ex As Exception
            clsErrorLog.LogInfo(ex)
        End Try

    End Sub

    Public Function TicketCopyExportPDFOld(OrderId As String, TransID As String) As String

        Dim strFileNmPdf As String = ""
        Dim writePDF As Boolean = False
        Dim TktCopy As String = ""
        Dim Gtotal As Integer = 0
        Dim initialAdt As Integer = 0
        Dim initalChld As Integer = 0

        Dim initialift As Integer = 0
        Dim MealBagTotalPrice As Decimal = 0
        Dim AdtTtlFare As Decimal = 0
        Dim ChdTtlFare As Decimal = 0
        Dim INFTtlFare As Decimal = 0
        Dim fare As Decimal = 0

        'Dim OrderId As String = "1c2019deXCP9cVSU"
        'Dim TransID As String = ""


        Dim objTranDom As New SqlTransactionDom()
        Dim SqlTrasaction As New SqlTransaction()
        Dim objSql As New SqlTransactionNew()
        Dim FltPaxList As New DataTable()

        Dim FltDetailsList As New DataTable()
        Dim FltProvider As New DataTable()
        Dim FltBaggage As New DataTable()
        Dim dtagentid As New DataTable()
        Dim FltagentDetail As New DataTable()
        Dim fltTerminal As New DataTable()
        Dim fltFare As New DataTable()
        Dim fltMealAndBag As New DataTable()
        Dim fltMealAndBag1 As New DataTable()
        Dim fltAirportDetails As New DataSet()
        Dim SelectedFltDS As New DataSet()
        FltPaxList = SelectPaxDetail(OrderId, TransID)
        FltHeaderList = objTktCopy.SelectHeaderDetail(OrderId)
        FltDetailsList = objTktCopy.SelectFlightDetail(OrderId)
        FltProvider = (objTranDom.GetTicketingProvider(OrderId)).Tables(0)
        dtagentid = objTktCopy.SelectAgent(OrderId)
        SelectedFltDS = SqlTrasaction.GetFltDtls(OrderId, dtagentid.Rows(0)("AgentID").ToString())
        Dim Bag As Boolean = False
        If Not String.IsNullOrEmpty(Convert.ToString(SelectedFltDS.Tables(0).Rows(0)("IsBagFare"))) Then
            Bag = Convert.ToBoolean(SelectedFltDS.Tables(0).Rows(0)("IsBagFare"))
        End If
        FltBaggage = (objTranDom.GetBaggageInformation(Convert.ToString(FltHeaderList.Rows(0)("Trip")), Convert.ToString(FltHeaderList.Rows(0)("VC")), Bag)).Tables(0)
        FltagentDetail = objTktCopy.SelectAgencyDetail(dtagentid.Rows(0)("AgentID").ToString())
        fltFare = objTktCopy.SelectFareDetail(OrderId, TransID)
        Dim dt As DateTime = Convert.ToDateTime(Convert.ToString(FltHeaderList.Rows(0)("CreateDate")))
        Dim [date] As String = dt.ToString("dd/MMM/yyyy").Replace("-", "/")

        Dim Createddate As String = [date].Split("/")(0) + " " + [date].Split("/")(1) + " " + [date].Split("/")(2)

        Dim fltmealbag As DataRow() = objSql.Get_MEAL_BAG_FareDetails(OrderId, TransID).Tables(0).Select("MealPrice>0 or BaggagePrice>0 ")
        fltMealAndBag1 = objSql.Get_MEAL_BAG_FareDetails(OrderId, TransID).Tables(0) '.Select("MealPrice>0 or BaggagePrice>0 ").CopyToDataTable()
        If fltmealbag.Length > 0 Then

            fltMealAndBag = fltMealAndBag1.Select("MealPrice>0 or BaggagePrice>0 ").CopyToDataTable()
        End If

        Try
            'Dim strAirline As String = "SG6EG8"

            Dim TicketFormate As String = ""


            'If (Convert.ToString(FltHeaderList.Rows(0)("Status")).ToLower().Trim() = "confirm" And Session("UserType") = "TA") Then
            If ((Convert.ToString(FltHeaderList.Rows(0)("Status")).ToLower().Trim() = "confirm1" OrElse Convert.ToString(FltHeaderList.Rows(0)("Status")).ToLower().Trim() = "confirmbyagent1") And Session("UserType") = "TA") Then

                TicketFormate += "<div style='clear:both;'></div> "
                TicketFormate += "<table style='width:100%;'>"
                TicketFormate += "<tr>"
                TicketFormate += "<td>"

                TicketFormate += "<table style='width:100%;'>"
                TicketFormate += "<tr>"
                TicketFormate += "<td style='font-size: 15px; width: 15%; text-align: left; padding: 5px;'>"
                TicketFormate += "<b>Booking Reference No. " & OrderId & "</b>"
                TicketFormate += "</td>"
                TicketFormate += "</tr>"
                TicketFormate += "<tr>"
                TicketFormate += "<td style='font-size: 14px; width: 15%; text-align: left; padding: 5px;'>"
                ''TicketFormate += "The PNR-<b>" & FltHeaderList.Rows(0)("GdsPnr") & " </b>is on <b>HOLD</b> and contact customer care for issuance."
                TicketFormate += "The PNR-<b>" & FltHeaderList.Rows(0)("GdsPnr") & " </b>is on <b>HOLD</b>. Our operation team is working on it and may take 20 minutes to resolve. Please contact our customer care representative at <b>+ 91-11-47 677 777</b> for any further assistance"
                TicketFormate += "</td>"
                TicketFormate += "</tr>"

                TicketFormate += "<table style='border: 1px solid #eee; font-family: Verdana, Geneva, sans-serif; font-size: 12px;padding:0px !important;width:100%;'>"
                TicketFormate += "<tr>"
                TicketFormate += "<td style='text-align: left; background-color: #eee; color: #424242; font-size: 11px; font-weight: bold; padding: 5px;' colspan='4'>"
                TicketFormate += "Passenger Information"
                TicketFormate += "</td>"
                TicketFormate += "</tr>"
                TicketFormate += "<tr>"
                TicketFormate += "<td colspan='4' style='font-size:12px; padding: 5px; width: 100%'>"
                TicketFormate += "<table>"

                TicketFormate += "<tr>"
                TicketFormate += "<td style='font-size: 11px; width: 15%; text-align: left; padding: 5px;'>GDS PNR</td>"
                TicketFormate += "<td style='font-size: 11px; width: 35%; text-align: left; padding: 5px;'>"
                TicketFormate += FltHeaderList.Rows(0)("GdsPnr")
                TicketFormate += "</td>"
                TicketFormate += "<td style='font-size: 11px; width: 20%; text-align: left; padding: 5px;'>Issued By</td>"
                TicketFormate += "<td style='font-size: 11px; width: 30%; text-align: left; padding: 5px;'>"
                TicketFormate += FltHeaderList.Rows(0)("AgencyName")
                TicketFormate += "</td>"
                TicketFormate += "</tr>"

                TicketFormate += "<tr>"
                TicketFormate += "<td style='font-size: 11px; width: 15%; text-align: left; padding: 5px;'>Airline PNR</td>"
                TicketFormate += "<td style='font-size: 11px; width: 35%; text-align: left; padding: 5px;'>"
                TicketFormate += ""
                'TicketFormate += FltHeaderList.Rows(0)("AirlinePnr")
                TicketFormate += "</td>"
                TicketFormate += "<td style='font-size: 11px; width: 20%; text-align: left; padding: 5px;'>Agency Info</td>"
                TicketFormate += "<td style='font-size: 11px; width: 30%; text-align: left; padding: 5px;'>"
                TicketFormate += FltagentDetail.Rows(0)("Mobile")
                TicketFormate += "<br/>"
                TicketFormate += FltagentDetail.Rows(0)("Email")
                TicketFormate += "</td>"
                TicketFormate += "</tr>"

                TicketFormate += "<tr>"
                TicketFormate += "<td style='font-size: 11px; width: 20%; text-align: left; padding: 5px;'>Status</td>"
                TicketFormate += "<td style='font-size: 11px; width: 30%; text-align: left; padding: 5px;'>"
                TicketFormate += IIf(Convert.ToString(FltHeaderList.Rows(0)("Status")).ToLower().Trim() = "confirm" OrElse Convert.ToString(FltHeaderList.Rows(0)("Status")).ToLower().Trim() = "confirmbyagent", "Hold", FltHeaderList.Rows(0)("Status"))
                'TicketFormate += IIf(Convert.ToString(FltHeaderList.Rows(0)("Status")).ToLower().Trim() = "confirm", "Hold", FltHeaderList.Rows(0)("Status"))
                TicketFormate += "</td>"
                TicketFormate += "<td style='font-size: 11px; width: 15%; text-align: left; padding: 5px;'>Date Of Issue</td>"
                TicketFormate += "<td style='font-size: 11px; width: 35%; text-align: left; padding: 5px;'>"
                TicketFormate += Createddate
                TicketFormate += "</td>"

                TicketFormate += "</tr>"
                'Remove Fare Type 02-04-2019
                'TicketFormate += "<tr>"
                'TicketFormate += "<td style='font-size: 11px; width: 20%; text-align: left; padding: 5px;'>Fare Type</td>"
                'TicketFormate += "<td style='font-size: 13px; width: 30%; text-align: left; padding: 5px;'>"
                'TicketFormate += Convert.ToString(SelectedFltDS.Tables(0).Rows(0)("AdtFareType"))
                'TicketFormate += "</td>"
                'TicketFormate += "<td style='font-size: 11px; width: 20%; text-align: left; padding: 5px;'>Customer Info</td>"
                'TicketFormate += "<td style='font-size: 11px; width: 30%; text-align: left; padding: 5px;'>"
                'TicketFormate += FltHeaderList.Rows(0)("PgMobile")
                'TicketFormate += "<br/>"
                'TicketFormate += FltHeaderList.Rows(0)("PgEmail")
                'TicketFormate += "</td>"
                'TicketFormate += "</tr>"


                TicketFormate += "<tr>"
                TicketFormate += "<td style='font-size: 11px; width: 20%; text-align: left; padding: 5px;'>Class</td>"
                TicketFormate += "<td style='font-size: 13px; width: 30%; text-align: left; padding: 5px;'>"
                TicketFormate += GetCabin(Convert.ToString(SelectedFltDS.Tables(0).Rows(0)("Provider")), Convert.ToString(SelectedFltDS.Tables(0).Rows(0)("AdtCabin")), Convert.ToString(SelectedFltDS.Tables(0).Rows(0)("ValiDatingCarrier")))
                TicketFormate += "</td>"
                TicketFormate += "<td style='font-size: 11px; width: 20%; text-align: left; padding: 5px;'></td>"
                TicketFormate += "<td style='font-size: 11px; width: 30%; text-align: left; padding: 5px;'></td>"
                TicketFormate += "</tr>"



                For p As Integer = 0 To FltPaxList.Rows.Count - 1
                    TicketFormate += "<tr>"
                    TicketFormate += "<td style='font-size: 11px; width: 15%; text-align: left; padding: 5px;'>Passenger Name</td>"
                    TicketFormate += "<td style='font-size: 11px; width: 35%; text-align: left; padding: 5px;'>"
                    TicketFormate += FltPaxList.Rows(p)("Name") + " " + "(" + FltPaxList.Rows(p)("PaxType") + ")"
                    TicketFormate += "</td>"
                    TicketFormate += "<td style='font-size: 11px; width: 35%; text-align: left; padding: 5px;'>"
                    TicketFormate += FltPaxList.Rows(p)("TicketNumber")
                    TicketFormate += "</td>"
                    TicketFormate += "</tr>"
                Next

                TicketFormate += "</table>"



                TicketFormate += "</td>"
                TicketFormate += "</tr>"
                TicketFormate += "<tr>"
                TicketFormate += "<td style='text-align: left; background-color: #eee; color: #424242; width: 100%; padding: 5px;' colspan='4'>"
                TicketFormate += "<table style='width:100%;'>"
                TicketFormate += "<tr>"
                TicketFormate += "<td style='text-align: left; color: #424242; font-size: 11px; width: 25%;font-weight:bold;' colspan='1'>"
                TicketFormate += "Flight Information"
                TicketFormate += "</td>"
                TicketFormate += "<td colspan='3' style='font-size: 11px; color: black; font-weight: bold; width: 75%; text-align: left; '></td>"
                TicketFormate += "</tr>"
                TicketFormate += "</table>"
                TicketFormate += "</td>"
                TicketFormate += "</tr>"

                TicketFormate += "<tr>"
                TicketFormate += "<td colspan='4' style='height:5px;'>&nbsp;</td>"
                TicketFormate += "</tr>"

                TicketFormate += "<tr>"
                TicketFormate += "<td colspan='5' style='background-color: #eee;width:100%;'>"
                TicketFormate += "<table style='width:100%;'>"
                'TicketFormate += "<tr>"
                'TicketFormate += "<td style='font-size: 10.5px; color: #424242; width: 10%; text-align: left; padding: 4px; font-weight: bold;'>FLIGHT</td>"
                'TicketFormate += "<td style='font-size: 10.5px; color: #424242; width: 20%; text-align: left; padding: 4px; font-weight: bold;'>DEPART</td>"
                'TicketFormate += "<td style='font-size: 10.5px; color: #424242; width: 20%; text-align: left; padding: 4px; font-weight: bold;'>ARRIVE</td>"
                'TicketFormate += "<td style='font-size: 10.5px; color: #424242; width: 25%; text-align: left; padding: 4px; font-weight: bold;'>DEPART AIRPORT/TERMINAL</td>"
                'TicketFormate += "<td style='font-size: 10.5px; color: #424242; width: 25%; text-align: left; padding: 4px; font-weight: bold;'>ARRIVE AIRPORT/TERMINAL</td>"
                'TicketFormate += "</tr>"

                TicketFormate += "<tr>"
                TicketFormate += "<td style='font-size: 10.5px; color: #424242; width: 10%; text-align: left; padding: 4px; font-weight: bold;'>Flight</td>"
                TicketFormate += "<td style='font-size: 10.5px; color: #424242; width: 20%; text-align: left; padding: 4px; font-weight: bold;'>Depart</td>"
                TicketFormate += "<td style='font-size: 10.5px; color: #424242; width: 20%; text-align: left; padding: 4px; font-weight: bold;'>Arrive</td>"
                TicketFormate += "<td style='font-size: 10.5px; color: #424242; width: 25%; text-align: left; padding: 4px; font-weight: bold;'>Depart Airport/Terminal</td>"
                TicketFormate += "<td style='font-size: 10.5px; color: #424242; width: 25%; text-align: left; padding: 4px; font-weight: bold;'>Arrive Airport/Terminal</td>"
                TicketFormate += "</tr>"


                For f As Integer = 0 To FltDetailsList.Rows.Count - 1

                    TicketFormate += "</table>"

                    TicketFormate += "</td>"
                    TicketFormate += "</tr>"

                    TicketFormate += "<tr>"
                    TicketFormate += "<td colspan='5' style='width:100%;'>"
                    TicketFormate += "<table style='width:100%;'>"
                    TicketFormate += "<tr>"

                    TicketFormate += "<td style='font-size: 11px; width: 10%; text-align: left; vertical-align: top;'>"
                    TicketFormate += FltDetailsList.Rows(f)("AirlineCode") + " " + FltDetailsList.Rows(f)("FltNumber")
                    TicketFormate += "<br/>"
                    TicketFormate += FltDetailsList.Rows(f)("AirlineName")
                    TicketFormate += "<br/>"
                    TicketFormate += "<img alt='Logo Not Found' src='https://RWT.co/AirLogo/sm" + FltDetailsList.Rows(f)("AirlineCode") + ".gif'/>"
                    TicketFormate += "</td>"
                    TicketFormate += "<td style='font-size: 11px; width: 20%; text-align: left; vertical-align: top;'>"
                    Dim strDepdt As String = Convert.ToString(FltDetailsList.Rows(f)("DepDate"))
                    strDepdt = IIf(strDepdt.Length = 8, STD.BAL.Utility.Left(strDepdt, 4) & "-" & STD.BAL.Utility.Mid(strDepdt, 4, 2) & "-" & STD.BAL.Utility.Right(strDepdt, 2), "20" & STD.BAL.Utility.Right(strDepdt, 2) & "-" & STD.BAL.Utility.Mid(strDepdt, 2, 2) & "-" & STD.BAL.Utility.Left(strDepdt, 2))
                    Dim deptdt As DateTime = Convert.ToDateTime(strDepdt)
                    strDepdt = deptdt.ToString("dd/MMM/yy").Replace("-", "/")

                    'Response.Write(strDepdt)

                    Dim depDay As String = Convert.ToString(deptdt.DayOfWeek)
                    strDepdt = strDepdt.Split("/")(0) + " " + strDepdt.Split("/")(1) + " " + strDepdt.Split("/")(2)
                    Dim strdeptime As String = Convert.ToString(FltDetailsList.Rows(f)("DepTime"))
                    'strdeptime = strdeptime.Substring(0, 2) + " : " + strdeptime.Substring(2, 2)
                    Try
                        If strdeptime.Length > 4 Then
                            strdeptime = strdeptime.Substring(0, 2) + " : " + strdeptime.Substring(3, 2)
                        Else
                            strdeptime = strdeptime.Substring(0, 2) + " : " + strdeptime.Substring(2, 2)
                        End If
                    Catch ex As Exception
                        clsErrorLog.LogInfo(ex)
                    End Try
                    TicketFormate += strDepdt
                    TicketFormate += "<br/>"
                    TicketFormate += "<br/>"
                    TicketFormate += strdeptime
                    TicketFormate += "</td>"

                    TicketFormate += "<td style='font-size: 11px; width: 20%; text-align: left; vertical-align: top;'>"
                    Dim strArvdt As String = Convert.ToString(FltDetailsList.Rows(f)("ArrDate"))
                    strArvdt = IIf(strArvdt.Length = 8, STD.BAL.Utility.Left(strArvdt, 4) & "-" & STD.BAL.Utility.Mid(strArvdt, 4, 2) & "-" & STD.BAL.Utility.Right(strArvdt, 2), "20" & STD.BAL.Utility.Right(strArvdt, 2) & "-" & STD.BAL.Utility.Mid(strArvdt, 2, 2) & "-" & STD.BAL.Utility.Left(strArvdt, 2))
                    Dim Arrdt As DateTime = Convert.ToDateTime(strArvdt)
                    strArvdt = Arrdt.ToString("dd/MMM/yy").Replace("-", "/")
                    Dim ArrDay As String = Convert.ToString(Arrdt.DayOfWeek)
                    strArvdt = strArvdt.Split("/")(0) + " " + strArvdt.Split("/")(1) + " " + strArvdt.Split("/")(2)
                    Dim strArrtime As String = Convert.ToString(FltDetailsList.Rows(f)("ArrTime"))
                    'strArrtime = strArrtime.Substring(0, 2) + " : " + strArrtime.Substring(2, 2)
                    Try
                        If strArrtime.Length > 4 Then
                            strArrtime = strArrtime.Substring(0, 2) + " : " + strArrtime.Substring(3, 2)
                        Else
                            strArrtime = strArrtime.Substring(0, 2) + " : " + strArrtime.Substring(2, 2)
                        End If
                    Catch ex As Exception
                        clsErrorLog.LogInfo(ex)
                    End Try
                    TicketFormate += strArvdt
                    TicketFormate += "<br/>"
                    TicketFormate += "<br/>"
                    TicketFormate += strArrtime
                    TicketFormate += "</td>"
                    TicketFormate += "<td style='font-size: 11px; width: 25%; text-align: left; padding: 2px;'>"
                    TicketFormate += FltDetailsList.Rows(f)("DepAirName") + "( " + FltDetailsList.Rows(f)("DFrom") + ")"

                    TicketFormate += "<br/>"
                    TicketFormate += "<br/>"
                    fltTerminalDetails = TerminalDetails(OrderId, FltDetailsList.Rows(f)("DFrom"), "")
                    If String.IsNullOrEmpty(Convert.ToString(fltTerminalDetails.Rows(0)("DepartureTerminal"))) Then
                        TicketFormate += fltTerminalDetails.Rows(0)("DepAirportName") + " - Trml: NA"
                    Else
                        TicketFormate += fltTerminalDetails.Rows(0)("DepAirportName") + " - Trml:" + fltTerminalDetails.Rows(0)("DepartureTerminal")
                    End If
                    TicketFormate += "</td>"
                    TicketFormate += "<td style='font-size: 11px; width: 25%; text-align: left; padding: 2px; '>"
                    TicketFormate += FltDetailsList.Rows(f)("ArrAirName") + " (" + FltDetailsList.Rows(f)("ATo") + ")"
                    TicketFormate += "<br/>"
                    TicketFormate += "<br/>"
                    fltTerminalDetails = TerminalDetails(OrderId, "", FltDetailsList.Rows(f)("ATo"))
                    If String.IsNullOrEmpty(Convert.ToString(fltTerminalDetails.Rows(0)("ArrivalTerminal"))) Then
                        TicketFormate += fltTerminalDetails.Rows(0)("ArrvlAirportName") + " - Trml: NA"
                    Else
                        TicketFormate += fltTerminalDetails.Rows(0)("ArrvlAirportName") + " - Trml:" + fltTerminalDetails.Rows(0)("ArrivalTerminal")
                    End If

                    TicketFormate += "</td>"
                    TicketFormate += "</tr>"
                    TicketFormate += "</table>"
                    TicketFormate += "</td>"
                    TicketFormate += "</tr>"

                    TicketFormate += "<tr>"
                    TicketFormate += "<td colspan='4' style='width:100%;'>"
                    TicketFormate += "<table style='width:100%;'>"
                    TicketFormate += "<tr>"
                    TicketFormate += "<td style='font-size: 11px; width: 322%; text-align: left; font-weight:bold;'>"
                    'TicketFormate += "<img alt='Logo Not Found' src='https://RWT.co/AirLogo/sm" + FltDetailsList.Rows(f)("AirlineCode") + ".gif'></img>"
                    TicketFormate += "<br/>"
                    'TicketFormate += FltDetailsList.Rows(f)("AirlineName")
                    TicketFormate += "</td>"
                    TicketFormate += "<td style='width: 32%;'></td>"
                    TicketFormate += "<td style='width: 18%; font-size:12px;text-align:left;'></td>"
                    TicketFormate += "<td style='width: 18%; font-size: 12px; text-align: left; font-weight: bold;'></td>"
                    TicketFormate += "</tr>"

                Next
                TicketFormate += "</table>"
                TicketFormate += "</td>"
                TicketFormate += "</table>"

                Div_Main.Visible = False


            ElseIf (Convert.ToString(FltHeaderList.Rows(0)("Status")).ToLower().Trim() = "rejected" And Session("UserType") = "TA") Then

                TicketFormate += "<table style='width:100%;'>"
                TicketFormate += "<tr>"
                TicketFormate += "<td style='text-align:left;font-size:15px;'>"
                TicketFormate += "<b>Booking Reference No. " & OrderId & "</b>"
                TicketFormate += "</td>"
                TicketFormate += "</tr>"
                TicketFormate += "<tr>"
                TicketFormate += "<td style='text-align:left;font-size:14px;'>"
                TicketFormate += "Please re-try the booking.Your booking has been rejected due to some technical issue at airline end."
                TicketFormate += "</td>"
                TicketFormate += "</tr></table>"
                TicketFormate += "<table style='border: 1px solid #eee; font-family: Verdana, Geneva, sans-serif; font-size: 13px;padding:0px !important;width:100%;'>"
                TicketFormate += "<tr>"
                TicketFormate += "<td style='text-align: left; background-color:#0000; color: #424242; font-size: 11px; font-weight: bold; padding: 5px;' colspan='4'>"
                TicketFormate += "Passenger & Ticket Information"
                TicketFormate += "</td>"
                TicketFormate += "</tr>"
                TicketFormate += "<tr>"
                TicketFormate += "<td colspan='4' style='font-size:12px; padding: 5px; width: 100%'>"
                TicketFormate += "<table>"


                TicketFormate += "<tr>"
                TicketFormate += "<td style='font-size: 11px; width: 15%; text-align: left; padding: 5px;'>GDS PNR</td>"
                TicketFormate += "<td style='font-size: 11px; width: 35%; text-align: left; padding: 5px;'>"
                TicketFormate += ""
                'TicketFormate += FltHeaderList.Rows(0)("GdsPnr")
                TicketFormate += "</td>"
                TicketFormate += "<td style='font-size: 11px; width: 20%; text-align: left; padding: 5px;'>Issued By</td>"
                TicketFormate += "<td style='font-size: 11px; width: 30%; text-align: left; padding: 5px;'>"
                TicketFormate += FltHeaderList.Rows(0)("AgencyName")
                TicketFormate += "</td>"
                TicketFormate += "</tr>"

                TicketFormate += "<tr>"
                TicketFormate += "<td style='font-size: 11px; width: 15%; text-align: left; padding: 5px;'>Airline PNR</td>"
                TicketFormate += "<td style='font-size: 11px; width: 35%; text-align: left; padding: 5px;'>"
                TicketFormate += ""
                'TicketFormate += FltHeaderList.Rows(0)("AirlinePnr")
                TicketFormate += "</td>"
                TicketFormate += "<td style='font-size: 11px; width: 20%; text-align: left; padding: 5px;'>Agency Info</td>"
                TicketFormate += "<td style='font-size: 11px; width: 30%; text-align: left; padding: 5px;'>"
                TicketFormate += FltagentDetail.Rows(0)("Mobile")
                TicketFormate += "<br/>"
                TicketFormate += FltagentDetail.Rows(0)("Email")
                TicketFormate += "</td>"
                TicketFormate += "</tr>"

                TicketFormate += "<tr>"
                TicketFormate += "<td style='font-size: 11px; width: 20%; text-align: left; padding: 5px;'>Status</td>"
                TicketFormate += "<td style='font-size: 11px; width: 30%; text-align: left; padding: 5px;'>"
                TicketFormate += IIf(Convert.ToString(FltHeaderList.Rows(0)("Status")).ToLower().Trim() = "confirm" OrElse Convert.ToString(FltHeaderList.Rows(0)("Status")).ToLower().Trim() = "confirmbyagent", "Hold", FltHeaderList.Rows(0)("Status"))
                'TicketFormate += IIf(Convert.ToString(FltHeaderList.Rows(0)("Status")).ToLower().Trim() = "confirm", "Hold", FltHeaderList.Rows(0)("Status"))
                TicketFormate += "</td>"
                TicketFormate += "<td style='font-size: 11px; width: 15%; text-align: left; padding: 5px;'>Date Of Issue</td>"
                TicketFormate += "<td style='font-size: 11px; width: 35%; text-align: left; padding: 5px;'>"
                TicketFormate += Createddate
                TicketFormate += "</td>"
                TicketFormate += "</tr>"

                'Remove Fare Type 02-04-2019
                'TicketFormate += "<tr>"
                'TicketFormate += "<td style='font-size: 11px; width: 20%; text-align: left; padding: 5px;'>Fare Type</td>"
                'TicketFormate += "<td style='font-size: 13px; width: 30%; text-align: left; padding: 5px; '>"
                'TicketFormate += Convert.ToString(SelectedFltDS.Tables(0).Rows(0)("AdtFareType"))
                'TicketFormate += "</td>"
                'TicketFormate += "<td style='font-size: 11px; width: 20%; text-align: left; padding: 5px;'>Customer Info</td>"
                'TicketFormate += "<td style='font-size: 11px; width: 30%; text-align: left; padding: 5px;'>"
                'TicketFormate += FltHeaderList.Rows(0)("PgMobile")
                'TicketFormate += "<br/>"
                'TicketFormate += FltHeaderList.Rows(0)("PgEmail")
                'TicketFormate += "</td>"
                'TicketFormate += "</tr>"

                TicketFormate += "<tr>"
                TicketFormate += "<td style='font-size: 11px; width: 20%; text-align: left; padding: 5px;'>Class</td>"
                TicketFormate += "<td style='font-size: 13px; width: 30%; text-align: left; padding: 5px;'>"
                TicketFormate += GetCabin(Convert.ToString(SelectedFltDS.Tables(0).Rows(0)("Provider")), Convert.ToString(SelectedFltDS.Tables(0).Rows(0)("AdtCabin")), Convert.ToString(SelectedFltDS.Tables(0).Rows(0)("ValiDatingCarrier")))
                TicketFormate += "</td>"
                TicketFormate += "<td style='font-size: 11px; width: 20%; text-align: left; padding: 5px;'></td>"
                TicketFormate += "<td style='font-size: 11px; width: 30%; text-align: left; padding: 5px;'></td>"
                TicketFormate += "</tr>"

                For p As Integer = 0 To FltPaxList.Rows.Count - 1
                    TicketFormate += "<tr>"
                    TicketFormate += "<td style='font-size: 11px; width: 15%; text-align: left; padding: 5px;'>Passenger Name</td>"
                    TicketFormate += "<td style='font-size: 11px; width: 35%; text-align: left; padding: 5px;'>"
                    TicketFormate += FltPaxList.Rows(p)("Name") + " " + "(" + FltPaxList.Rows(p)("PaxType") + ")"
                    TicketFormate += "</td>"
                    TicketFormate += "<td style='font-size: 11px; width: 35%; text-align: left; padding: 5px;'>"
                    TicketFormate += FltPaxList.Rows(p)("TicketNumber")
                    TicketFormate += "</td>"
                    TicketFormate += "</tr>"
                Next

                TicketFormate += "</table>"
                TicketFormate += "</td>"
                TicketFormate += "</tr>"
                TicketFormate += "<tr>"
                TicketFormate += "<td style='text-align: left; background-color: #000; color: #424242; width: 100%; padding: 5px;' colspan='4'>"
                TicketFormate += "<table style='width:100%;'>"
                TicketFormate += "<tr>"
                TicketFormate += "<td style='text-align: left; color: #fff; font-size: 11px; width: 25%;font-weight:bold;' colspan='1'>"
                TicketFormate += "Flight Information"
                TicketFormate += "</td>"
                TicketFormate += "<td colspan='3' style='font-size: 11px; color: black; font-weight: bold; width: 75%; text-align: left; '></td>"
                TicketFormate += "</tr>"
                TicketFormate += "</table>"
                TicketFormate += "</td>"
                TicketFormate += "</tr>"

                TicketFormate += "<tr>"
                TicketFormate += "<td colspan='4' style='height:5px;'>&nbsp;</td>"
                TicketFormate += "</tr>"

                TicketFormate += "<tr>"
                TicketFormate += "<td colspan='5' style='background-color: #eee;width:100%;'>"
                TicketFormate += "<table style='width:100%;'>"
                TicketFormate += "<tr>"
                TicketFormate += "<td style='font-size: 10.5px; color: #424242; width: 10%; text-align: left; padding: 4px; font-weight: bold;'>Fight</td>"
                TicketFormate += "<td style='font-size: 10.5px; color: #424242; width: 20%; text-align: left; padding: 4px; font-weight: bold;'>Depart</td>"
                TicketFormate += "<td style='font-size: 10.5px; color: #424242; width: 20%; text-align: left; padding: 4px; font-weight: bold;'>Arrive</td>"
                TicketFormate += "<td style='font-size: 10.5px; color: #424242; width: 25%; text-align: left; padding: 4px; font-weight: bold;'>Depart Airport/Terminal</td>"
                TicketFormate += "<td style='font-size: 10.5px; color: #424242; width: 25%; text-align: left; padding: 4px; font-weight: bold;'>Depart Airport/Terminal</td>"
                TicketFormate += "</tr>"

                For f As Integer = 0 To FltDetailsList.Rows.Count - 1

                    TicketFormate += "</table>"
                    TicketFormate += "</td>"
                    TicketFormate += "</tr>"
                    TicketFormate += "<tr>"
                    TicketFormate += "<td colspan='5' style='width:100%;'>"
                    TicketFormate += "<table style='width:100%;'>"
                    TicketFormate += "<tr>"
                    TicketFormate += "<td style='font-size: 11px; width: 10%; text-align: left;  vertical-align: top;'>"
                    TicketFormate += FltDetailsList.Rows(f)("AirlineCode") + " " + FltDetailsList.Rows(f)("FltNumber")
                    TicketFormate += "<br/>"
                    TicketFormate += FltDetailsList.Rows(f)("AirlineName")
                    TicketFormate += "<br/>"
                    TicketFormate += "<img alt='Logo Not Found' src='https://RWT.co/AirLogo/sm" + FltDetailsList.Rows(f)("AirlineCode") + ".gif'/>"
                    TicketFormate += "</td>"
                    TicketFormate += "<td style='font-size: 11px; width: 20%; text-align: left; vertical-align: top;'>"
                    Dim strDepdt As String = Convert.ToString(FltDetailsList.Rows(f)("DepDate"))
                    strDepdt = IIf(strDepdt.Length = 8, STD.BAL.Utility.Left(strDepdt, 4) & "-" & STD.BAL.Utility.Mid(strDepdt, 4, 2) & "-" & STD.BAL.Utility.Right(strDepdt, 2), "20" & STD.BAL.Utility.Right(strDepdt, 2) & "-" & STD.BAL.Utility.Mid(strDepdt, 2, 2) & "-" & STD.BAL.Utility.Left(strDepdt, 2))
                    Dim deptdt As DateTime = Convert.ToDateTime(strDepdt)
                    strDepdt = deptdt.ToString("dd/MMM/yy").Replace("-", "/")

                    ''Response.Write(strDepdt)

                    Dim depDay As String = Convert.ToString(deptdt.DayOfWeek)
                    strDepdt = strDepdt.Split("/")(0) + " " + strDepdt.Split("/")(1) + " " + strDepdt.Split("/")(2)
                    Dim strdeptime As String = Convert.ToString(FltDetailsList.Rows(f)("DepTime"))
                    'strdeptime = strdeptime.Substring(0, 2) + " : " + strdeptime.Substring(2, 2)
                    Try
                        If strdeptime.Length > 4 Then
                            strdeptime = strdeptime.Substring(0, 2) + " : " + strdeptime.Substring(3, 2)
                        Else
                            strdeptime = strdeptime.Substring(0, 2) + " : " + strdeptime.Substring(2, 2)
                        End If
                    Catch ex As Exception
                        clsErrorLog.LogInfo(ex)
                    End Try
                    TicketFormate += strDepdt
                    TicketFormate += "<br/>"
                    TicketFormate += "<br/>"
                    TicketFormate += strdeptime
                    TicketFormate += "</td>"

                    TicketFormate += "<td style='font-size: 11px; width: 20%; text-align: left; vertical-align: top;'>"
                    Dim strArvdt As String = Convert.ToString(FltDetailsList.Rows(f)("ArrDate"))
                    strArvdt = IIf(strArvdt.Length = 8, STD.BAL.Utility.Left(strArvdt, 4) & "-" & STD.BAL.Utility.Mid(strArvdt, 4, 2) & "-" & STD.BAL.Utility.Right(strArvdt, 2), "20" & STD.BAL.Utility.Right(strArvdt, 2) & "-" & STD.BAL.Utility.Mid(strArvdt, 2, 2) & "-" & STD.BAL.Utility.Left(strArvdt, 2))
                    Dim Arrdt As DateTime = Convert.ToDateTime(strArvdt)
                    strArvdt = Arrdt.ToString("dd/MMM/yy").Replace("-", "/")
                    Dim ArrDay As String = Convert.ToString(Arrdt.DayOfWeek)
                    strArvdt = strArvdt.Split("/")(0) + " " + strArvdt.Split("/")(1) + " " + strArvdt.Split("/")(2)
                    Dim strArrtime As String = Convert.ToString(FltDetailsList.Rows(f)("ArrTime"))
                    'strArrtime = strArrtime.Substring(0, 2) + " : " + strArrtime.Substring(2, 2)
                    Try
                        If strArrtime.Length > 4 Then
                            strArrtime = strArrtime.Substring(0, 2) + " : " + strArrtime.Substring(3, 2)
                        Else
                            strArrtime = strArrtime.Substring(0, 2) + " : " + strArrtime.Substring(2, 2)
                        End If
                    Catch ex As Exception
                        clsErrorLog.LogInfo(ex)
                    End Try
                    TicketFormate += strArvdt
                    TicketFormate += "<br/>"
                    TicketFormate += "<br/>"
                    TicketFormate += strArrtime
                    TicketFormate += "</td>"
                    TicketFormate += "<td style='font-size: 11px; width: 25%; text-align: left; padding: 2px; '>"
                    TicketFormate += FltDetailsList.Rows(f)("DepAirName") + "( " + FltDetailsList.Rows(f)("DFrom") + ")"
                    TicketFormate += "<br/>"
                    TicketFormate += "<br/>"
                    fltTerminalDetails = TerminalDetails(OrderId, FltDetailsList.Rows(f)("DFrom"), "")
                    If String.IsNullOrEmpty(Convert.ToString(fltTerminalDetails.Rows(0)("DepartureTerminal"))) Then
                        TicketFormate += fltTerminalDetails.Rows(0)("DepAirportName") + " - Trml: NA"
                    Else
                        TicketFormate += fltTerminalDetails.Rows(0)("DepAirportName") + " - Trml:" + fltTerminalDetails.Rows(0)("DepartureTerminal")
                    End If
                    TicketFormate += "</td>"
                    TicketFormate += "<td style='font-size: 11px; width: 25%; text-align: left; padding: 2px; '>"
                    TicketFormate += FltDetailsList.Rows(f)("ArrAirName") + " (" + FltDetailsList.Rows(f)("ATo") + ")"
                    TicketFormate += "<br/>"
                    TicketFormate += "<br/>"
                    fltTerminalDetails = TerminalDetails(OrderId, "", FltDetailsList.Rows(f)("ATo"))
                    If String.IsNullOrEmpty(Convert.ToString(fltTerminalDetails.Rows(0)("ArrivalTerminal"))) Then
                        TicketFormate += fltTerminalDetails.Rows(0)("ArrvlAirportName") + " - Trml: NA"
                    Else
                        TicketFormate += fltTerminalDetails.Rows(0)("ArrvlAirportName") + " - Trml:" + fltTerminalDetails.Rows(0)("ArrivalTerminal")
                    End If
                    TicketFormate += "</td>"
                    TicketFormate += "</tr>"
                    TicketFormate += "</table>"
                    TicketFormate += "</td>"
                    TicketFormate += "</tr>"
                    TicketFormate += "<tr>"
                    TicketFormate += "<td colspan='4' style='width:100%;'>"
                    TicketFormate += "<table style='width:100%;'>"
                    TicketFormate += "<tr>"
                    TicketFormate += "<td style='font-size: 11px; width: 322%; text-align: left; font-weight:bold;'>"
                    'TicketFormate += "<img alt='Logo Not Found' src='https://RWT.co/AirLogo/sm" + FltDetailsList.Rows(f)("AirlineCode") + ".gif'></img>"
                    TicketFormate += "<br/>"
                    'TicketFormate += FltDetailsList.Rows(f)("AirlineName")
                    TicketFormate += "</td>"
                    TicketFormate += "<td style='width: 32%;'></td>"
                    TicketFormate += "<td style='width: 18%; font-size:12px;text-align:left;'></td>"
                    TicketFormate += "<td style='width: 18%; font-size: 11px; text-align: left; font-weight: bold;'></td>"
                    TicketFormate += "</tr>"
                Next
                TicketFormate += "</table>"
                TicketFormate += "</td>"
                TicketFormate += "</table>"

            ElseIf (Convert.ToString(FltHeaderList.Rows(0)("Status")).ToLower().Trim() = "inprocess" And Session("UserType") = "TA") Then

                TicketFormate += "<table style='width:100%;'>"
                TicketFormate += "<tr>"
                TicketFormate += "<td style='text-align:center;font-size:15px;'>"
                TicketFormate += "<b>Booking Reference No. " & OrderId & "</b>"
                TicketFormate += "</td>"
                TicketFormate += "</tr>"
                TicketFormate += "<tr>"
                TicketFormate += "<td style='text-align:left;font-size:14px;'>"
                TicketFormate += "We are updating the details, Please wait for some time."
                TicketFormate += "</td>"
                TicketFormate += "</tr>"

                TicketFormate += "<table style='border: 1px solid #eee; font-size: 12px;padding:0px !important;width:100%;'>"
                TicketFormate += "<tr>"
                TicketFormate += "<td style='text-align: left; background-color: #eee; color: #424242; font-size: 12px; font-weight: bold; padding: 5px;' colspan='4'>"
                TicketFormate += "Passenger & Ticket Information"
                TicketFormate += "</td>"
                TicketFormate += "</tr>"
                TicketFormate += "<tr>"
                TicketFormate += "<td colspan='4' style='font-size:12px; padding: 5px; width: 100%'>"
                TicketFormate += "<table>"

                TicketFormate += "<tr>"
                TicketFormate += "<td style='font-size: 11px; width: 15%; text-align: left; padding: 5px;'>GDS PNR</td>"
                TicketFormate += "<td style='font-size: 11px; width: 35%; text-align: left; padding: 5px;'>"
                TicketFormate += ""
                'TicketFormate += FltHeaderList.Rows(0)("GdsPnr")
                TicketFormate += "</td>"
                TicketFormate += "<td style='font-size: 11px; width: 20%; text-align: left; padding: 5px;'>Issued By</td>"
                TicketFormate += "<td style='font-size: 11px; width: 30%; text-align: left; padding: 5px;'>"
                TicketFormate += FltHeaderList.Rows(0)("AgencyName")
                TicketFormate += "</td>"
                TicketFormate += "</tr>"

                TicketFormate += "<tr>"
                TicketFormate += "<td style='font-size: 11px; width: 15%; text-align: left; padding: 5px;'>Airline PNR</td>"
                TicketFormate += "<td style='font-size: 11px; width: 35%; text-align: left; padding: 5px;'>"
                TicketFormate += ""
                'TicketFormate += FltHeaderList.Rows(0)("AirlinePnr")
                TicketFormate += "</td>"
                TicketFormate += "<td style='font-size: 11px; width: 20%; text-align: left; padding: 5px;'>Agency Info</td>"
                TicketFormate += "<td style='font-size: 11px; width: 30%; text-align: left; padding: 5px;'>"
                TicketFormate += FltagentDetail.Rows(0)("Mobile")
                TicketFormate += "<br/>"
                TicketFormate += FltagentDetail.Rows(0)("Email")
                TicketFormate += "</td>"
                TicketFormate += "</tr>"

                TicketFormate += "<tr>"
                TicketFormate += "<td style='font-size: 11px; width: 20%; text-align: left; padding: 5px;'>Status</td>"
                TicketFormate += "<td style='font-size: 11px; width: 30%; text-align: left; padding: 5px;'>"
                TicketFormate += IIf(Convert.ToString(FltHeaderList.Rows(0)("Status")).ToLower().Trim() = "confirm" OrElse Convert.ToString(FltHeaderList.Rows(0)("Status")).ToLower().Trim() = "confirmbyagent", "Hold", FltHeaderList.Rows(0)("Status"))
                'TicketFormate += IIf(Convert.ToString(FltHeaderList.Rows(0)("Status")).ToLower().Trim() = "confirm", "Hold", FltHeaderList.Rows(0)("Status"))
                TicketFormate += "</td>"
                TicketFormate += "<td style='font-size: 11px; width: 15%; text-align: left; padding: 5px;'>Date Of Issue</td>"
                TicketFormate += "<td style='font-size: 11px; width: 35%; text-align: left; padding: 5px;'>"
                TicketFormate += Createddate
                TicketFormate += "</td>"

                TicketFormate += "</tr>"
                'Remove Fare Type 02-04-2019
                'TicketFormate += "<tr>"
                'TicketFormate += "<td style='font-size: 11px; width: 20%; text-align: left; padding: 5px;'>Fare Type</td>"
                'TicketFormate += "<td style='font-size: 13px; width: 30%; text-align: left; padding: 5px;'>"
                'TicketFormate += Convert.ToString(SelectedFltDS.Tables(0).Rows(0)("AdtFareType"))
                'TicketFormate += "</td>"
                'TicketFormate += "<td style='font-size: 11px; width: 20%; text-align: left; padding: 5px;'>Customer Info</td>"
                'TicketFormate += "<td style='font-size: 11px; width: 30%; text-align: left; padding: 5px;'>"
                'TicketFormate += FltHeaderList.Rows(0)("PgMobile")
                'TicketFormate += "<br/>"
                'TicketFormate += FltHeaderList.Rows(0)("PgEmail")
                'TicketFormate += "</td>"
                'TicketFormate += "</tr>"

                TicketFormate += "<tr>"
                TicketFormate += "<td style='font-size: 11px; width: 20%; text-align: left; padding: 5px;'>Class</td>"
                TicketFormate += "<td style='font-size: 13px; width: 30%; text-align: left; padding: 5px;'>"
                TicketFormate += GetCabin(Convert.ToString(SelectedFltDS.Tables(0).Rows(0)("Provider")), Convert.ToString(SelectedFltDS.Tables(0).Rows(0)("AdtCabin")), Convert.ToString(SelectedFltDS.Tables(0).Rows(0)("ValiDatingCarrier")))
                TicketFormate += "</td>"
                TicketFormate += "<td style='font-size: 11px; width: 20%; text-align: left; padding: 5px;'></td>"
                TicketFormate += "<td style='font-size: 11px; width: 30%; text-align: left; padding: 5px;'></td>"
                TicketFormate += "</tr>"

                For p As Integer = 0 To FltPaxList.Rows.Count - 1
                    TicketFormate += "<tr>"
                    TicketFormate += "<td style='font-size: 11px; width: 15%; text-align: left; padding: 5px;'>Passenger Name</td>"
                    TicketFormate += "<td style='font-size: 11px; width: 35%; text-align: left; padding: 5px;'>"
                    TicketFormate += FltPaxList.Rows(p)("Name") + " " + "(" + FltPaxList.Rows(p)("PaxType") + ")"
                    TicketFormate += "</td>"
                    TicketFormate += "<td style='font-size: 11px; width: 35%; text-align: left; padding: 5px;'>"
                    TicketFormate += FltPaxList.Rows(p)("TicketNumber")
                    TicketFormate += "</td>"
                    TicketFormate += "</tr>"
                Next
                TicketFormate += "</table>"
                TicketFormate += "</td>"
                TicketFormate += "</tr>"
                TicketFormate += "<tr>"
                TicketFormate += "<td style='text-align: left; background-color:#000; color: #424242; width: 100%; padding: 5px;' colspan='4'>"
                TicketFormate += "<table style='width:100%;'>"
                TicketFormate += "<tr>"
                TicketFormate += "<td style='text-align: left; color: #fff; font-size: 11px; width: 25%;font-weight:bold;' colspan='1'>"
                TicketFormate += "Flight Information"
                TicketFormate += "</td>"
                'TicketFormate += "<td colspan='3' style='font-size: 11px; color: black; font-weight: bold; width: 75%; text-align: left; '></td>"
                TicketFormate += "</tr>"
                TicketFormate += "</table>"
                TicketFormate += "</td>"
                TicketFormate += "</tr>"
                TicketFormate += "<tr>"
                TicketFormate += "<td colspan='4' style='height:5px;'>&nbsp;</td>"
                TicketFormate += "</tr>"
                TicketFormate += "<tr>"
                TicketFormate += "<td colspan='5' style='background-color: #eee;width:100%;'>"
                TicketFormate += "<table style='width:100%;'>"
                TicketFormate += "<tr>"
                TicketFormate += "<td style='font-size: 10.5px; color: #424242; width: 10%; text-align: left; padding: 4px; font-weight: bold;'>Flight</td>"
                TicketFormate += "<td style='font-size: 10.5px; color: #424242; width: 20%; text-align: left; padding: 4px; font-weight: bold;'>Depart</td>"
                TicketFormate += "<td style='font-size: 10.5px; color: #424242; width: 20%; text-align: left; padding: 4px; font-weight: bold;'>Arrive</td>"
                TicketFormate += "<td style='font-size: 10.5px; color: #424242; width: 25%; text-align: left; padding: 4px; font-weight: bold;'>Depart Airport/Terminal</td>"
                TicketFormate += "<td style='font-size: 10.5px; color: #424242; width: 25%; text-align: left; padding: 4px; font-weight: bold;'>Arrive Airport/Terminal</td>"
                TicketFormate += "</tr>"

                For f As Integer = 0 To FltDetailsList.Rows.Count - 1

                    TicketFormate += "</table>"
                    TicketFormate += "</td>"
                    TicketFormate += "</tr>"
                    TicketFormate += "<tr>"
                    TicketFormate += "<td colspan='5' style='width:100%;'>"
                    TicketFormate += "<table style='width:100%;'>"
                    TicketFormate += "<tr>"
                    TicketFormate += "<td style='font-size: 11px; width: 10%; text-align: left;  vertical-align: top;'>"
                    TicketFormate += FltDetailsList.Rows(f)("AirlineCode") + " " + FltDetailsList.Rows(f)("FltNumber")
                    TicketFormate += "<br/>"
                    TicketFormate += FltDetailsList.Rows(f)("AirlineName")
                    TicketFormate += "<br/>"
                    TicketFormate += "<img alt='Logo Not Found' src='https://RWT.co/AirLogo/sm" + FltDetailsList.Rows(f)("AirlineCode") + ".gif'/>"
                    TicketFormate += "</td>"
                    TicketFormate += "<td style='font-size: 11px; width: 20%; text-align: left; vertical-align: top;'>"
                    Dim strDepdt As String = Convert.ToString(FltDetailsList.Rows(f)("DepDate"))
                    strDepdt = IIf(strDepdt.Length = 8, STD.BAL.Utility.Left(strDepdt, 4) & "-" & STD.BAL.Utility.Mid(strDepdt, 4, 2) & "-" & STD.BAL.Utility.Right(strDepdt, 2), "20" & STD.BAL.Utility.Right(strDepdt, 2) & "-" & STD.BAL.Utility.Mid(strDepdt, 2, 2) & "-" & STD.BAL.Utility.Left(strDepdt, 2))
                    Dim deptdt As DateTime = Convert.ToDateTime(strDepdt)
                    strDepdt = deptdt.ToString("dd/MMM/yy").Replace("-", "/")

                    Dim depDay As String = Convert.ToString(deptdt.DayOfWeek)
                    strDepdt = strDepdt.Split("/")(0) + " " + strDepdt.Split("/")(1) + " " + strDepdt.Split("/")(2)
                    Dim strdeptime As String = Convert.ToString(FltDetailsList.Rows(f)("DepTime"))
                    'strdeptime = strdeptime.Substring(0, 2) + " : " + strdeptime.Substring(2, 2)
                    Try
                        If strdeptime.Length > 4 Then
                            strdeptime = strdeptime.Substring(0, 2) + " : " + strdeptime.Substring(3, 2)
                        Else
                            strdeptime = strdeptime.Substring(0, 2) + " : " + strdeptime.Substring(2, 2)
                        End If
                    Catch ex As Exception
                        clsErrorLog.LogInfo(ex)
                    End Try
                    TicketFormate += strDepdt
                    TicketFormate += "<br/>"
                    TicketFormate += "<br/>"
                    TicketFormate += strdeptime
                    TicketFormate += "</td>"

                    TicketFormate += "<td style='font-size: 11px; width: 20%; text-align: left; vertical-align: top;'>"
                    Dim strArvdt As String = Convert.ToString(FltDetailsList.Rows(f)("ArrDate"))
                    strArvdt = IIf(strArvdt.Length = 8, STD.BAL.Utility.Left(strArvdt, 4) & "-" & STD.BAL.Utility.Mid(strArvdt, 4, 2) & "-" & STD.BAL.Utility.Right(strArvdt, 2), "20" & STD.BAL.Utility.Right(strArvdt, 2) & "-" & STD.BAL.Utility.Mid(strArvdt, 2, 2) & "-" & STD.BAL.Utility.Left(strArvdt, 2))
                    Dim Arrdt As DateTime = Convert.ToDateTime(strArvdt)
                    strArvdt = Arrdt.ToString("dd/MMM/yy").Replace("-", "/")
                    Dim ArrDay As String = Convert.ToString(Arrdt.DayOfWeek)
                    strArvdt = strArvdt.Split("/")(0) + " " + strArvdt.Split("/")(1) + " " + strArvdt.Split("/")(2)
                    Dim strArrtime As String = Convert.ToString(FltDetailsList.Rows(f)("ArrTime"))
                    'strArrtime = strArrtime.Substring(0, 2) + " : " + strArrtime.Substring(2, 2)
                    Try
                        If strArrtime.Length > 4 Then
                            strArrtime = strArrtime.Substring(0, 2) + " : " + strArrtime.Substring(3, 2)
                        Else
                            strArrtime = strArrtime.Substring(0, 2) + " : " + strArrtime.Substring(2, 2)
                        End If
                    Catch ex As Exception
                        clsErrorLog.LogInfo(ex)
                    End Try

                    TicketFormate += strArvdt
                    TicketFormate += "<br/>"
                    TicketFormate += "<br/>"
                    TicketFormate += strArrtime
                    TicketFormate += "</td>"
                    TicketFormate += "<td style='font-size: 11px; width: 25%; text-align: left; padding: 2px; '>"
                    TicketFormate += FltDetailsList.Rows(f)("DepAirName") + "( " + FltDetailsList.Rows(f)("DFrom") + ")"

                    TicketFormate += "<br/>"
                    TicketFormate += "<br/>"
                    fltTerminalDetails = TerminalDetails(OrderId, FltDetailsList.Rows(f)("DFrom"), "")
                    If String.IsNullOrEmpty(Convert.ToString(fltTerminalDetails.Rows(0)("DepartureTerminal"))) Then
                        TicketFormate += fltTerminalDetails.Rows(0)("DepAirportName") + " - Trml: NA"
                    Else
                        TicketFormate += fltTerminalDetails.Rows(0)("DepAirportName") + " - Trml:" + fltTerminalDetails.Rows(0)("DepartureTerminal")
                    End If

                    TicketFormate += "</td>"
                    TicketFormate += "<td style='font-size: 11px; width: 25%; text-align: left; padding: 2px; '>"
                    TicketFormate += FltDetailsList.Rows(f)("ArrAirName") + " (" + FltDetailsList.Rows(f)("ATo") + ")"
                    TicketFormate += "<br/>"
                    TicketFormate += "<br/>"
                    fltTerminalDetails = TerminalDetails(OrderId, "", FltDetailsList.Rows(f)("ATo"))
                    If String.IsNullOrEmpty(Convert.ToString(fltTerminalDetails.Rows(0)("ArrivalTerminal"))) Then
                        TicketFormate += fltTerminalDetails.Rows(0)("ArrvlAirportName") + " - Trml: NA"
                    Else
                        TicketFormate += fltTerminalDetails.Rows(0)("ArrvlAirportName") + " - Trml:" + fltTerminalDetails.Rows(0)("ArrivalTerminal")
                    End If

                    TicketFormate += "</td>"
                    TicketFormate += "</tr>"
                    TicketFormate += "</table>"
                    TicketFormate += "</td>"
                    TicketFormate += "</tr>"

                    TicketFormate += "<tr>"
                    TicketFormate += "<td colspan='4' style='width:100%;'>"
                    TicketFormate += "<table style='width:100%;'>"
                    TicketFormate += "<tr>"
                    TicketFormate += "<td style='font-size: 11px; width: 322%; text-align: left; font-weight:bold;'>"
                    'TicketFormate += "<img alt='Logo Not Found' src='https://RWT.co/AirLogo/sm" + FltDetailsList.Rows(f)("AirlineCode") + ".gif'></img>"
                    TicketFormate += "<br/>"
                    'TicketFormate += FltDetailsList.Rows(f)("AirlineName")
                    TicketFormate += "</td>"
                    TicketFormate += "<td style='width: 32%;'></td>"
                    TicketFormate += "<td style='width: 18%; font-size:12px;text-align:left;'></td>"
                    TicketFormate += "<td style='width: 18%; font-size: 11px; text-align: left; font-weight: bold;'></td>"
                    TicketFormate += "</tr>"
                Next
                TicketFormate += "</table>"
                TicketFormate += "</td>"
                TicketFormate += "</table>"

            Else
                'starstdflsdlfsdkjksdjksdjksdfjkdfjk
                If ((Convert.ToString(FltHeaderList.Rows(0)("Status")).ToLower().Trim() = "confirm" OrElse Convert.ToString(FltHeaderList.Rows(0)("Status")).ToLower().Trim() = "confirmbyagent")) Then
                    TicketFormate += "<table style='width:100%;'>"
                    TicketFormate += "<tr>"
                    TicketFormate += "<td style='font-size: 15px; width: 15%; text-align: left; padding: 5px;'>"
                    TicketFormate += "<b>Booking Reference No. " & OrderId & "</b>"
                    TicketFormate += "</td>"
                    TicketFormate += "</tr>"
                    TicketFormate += "<tr>"
                    TicketFormate += "<td style='font-size: 14px; width: 15%; text-align: left; padding: 5px;'>"
                    ''TicketFormate += "The PNR-<b>" & FltHeaderList.Rows(0)("GdsPnr") & " </b>is on <b>HOLD</b> and contact customer care for issuance."
                    TicketFormate += "The PNR-<b>" & FltHeaderList.Rows(0)("GdsPnr") & " </b>is on <b>HOLD</b>. Our operation team is working on it and may take 20 minutes to resolve. Please contact our customer care representative at <b>+ 91-11-47 677 777</b> for any further assistance"
                    TicketFormate += "</td>"
                    TicketFormate += "</tr>"
                    TicketFormate += "</table>"
                End If

                TicketFormate += "<table style='width:100%;'>"
                TicketFormate += "<tr>"
                TicketFormate += "<td style='width:50%;text-align:left;'>"


                Dim dd As String = ""
                Dim AgentAddress As String = ""
                Try
                    Dim filepath As String = Server.MapPath("~\AgentLogo") + "\" + Convert.ToString(dtagentid.Rows(0)("AgentID")) + ".jpg" 'Server.MapPath("~/AgentLogo/" + LogoName)
                    If (System.IO.File.Exists(filepath)) Then

                        dd = "https://RWT.co/AgentLogo/" + Convert.ToString(dtagentid.Rows(0)("AgentID")) + ".jpg"
                        ''dd = "https://RWT.co/AgentLogo/" + Convert.ToString(dtagentid.Rows(0)("AgentID")) + ".jpg"
                        'dd = ResolveClientUrl("~/AgentLogo/" + Convert.ToString(dtagentid.Rows(0)("AgentID")) + ".jpg")
                        AgentAddress = Convert.ToString(FltagentDetail.Rows(0)("Agency_Name")) + "<br/>" + Convert.ToString(FltagentDetail.Rows(0)("Address")) + "<br/>" + Convert.ToString(FltagentDetail.Rows(0)("Address1")) + "<br/>Mobile:" + Convert.ToString(FltagentDetail.Rows(0)("Mobile")) + "<br/>Email:" + Convert.ToString(FltagentDetail.Rows(0)("Email"))
                    Else
                        '' dd = "https://RWT.co/images/logo.png"
                        'TicketFormate += "<img src='https://RWT.co/images/logo.png' alt='Logo' style='height:70px; width:110px'/>"
                        AgentAddress = Convert.ToString(FltagentDetail.Rows(0)("Agency_Name")) + "<br/>" + Convert.ToString(FltagentDetail.Rows(0)("Address")) + "<br/>" + Convert.ToString(FltagentDetail.Rows(0)("Address1")) + "<br/>Mobile:" + Convert.ToString(FltagentDetail.Rows(0)("Mobile")) + "<br/>Email:" + Convert.ToString(FltagentDetail.Rows(0)("Email"))
                    End If
                Catch ex As Exception
                    'clsErrorLog.LogInfo(ex)
                    'TicketFormate += "<img src='https://RWT.co/images/logo.png' alt='Logo' style='height:54px; width:110px'/>"
                    ''dd = "https://RWT.co/images/logo.png"
                End Try


                If (fltFare.Rows.Count > 0) Then

                    If (Convert.ToString(fltFare.Rows(0)("ticketcopymarkupforTax")) <> "0") Then
                        TaxNew.Text = "Tax: " & Convert.ToString(fltFare.Rows(0)("ticketcopymarkupforTax"))
                    End If
                End If

                TicketFormate += "<img src='" + dd + "' alt='Logo' style='height:70px; width:150px'/>"
                'TicketFormate += "<img src='https://RWT.co/images/logo.png' alt='Logo' style='height:54px; width:110px'/>"
                TicketFormate += "</td>"
                TicketFormate += "<td style='text-align:right;width:50%;font-size:12px;font-weight:bold;'>"
                'TicketFormate += "<span style='font-size:16px;font-weight:bold;'>Electronic Ticket</span><br/>"
                TicketFormate += "<div style='font-size:12px;font-weight:bold;'>Electronic Ticket</div><br/>"
                If String.IsNullOrEmpty(AgentAddress) Then
                    AgentAddress = ""
                Else
                    TicketFormate += "<br/>"
                    TicketFormate += "<div style='color:#424242;font-size:12px;font-weight:bold;'>" + AgentAddress + "</div><br/>"
                    ''TicketFormate += AgentAddress + "<br/>"
                End If

                TicketFormate += "</td>"
                TicketFormate += "<td style='width: 50%;text-align:right;display:none;'>"
                TicketFormate += "</td>"
                TicketFormate += "</tr>"

                TicketFormate += "<tr>"
                TicketFormate += "<td style='width:50%;text-align:left;'>"
                TicketFormate += ""
                TicketFormate += "</td>"
                TicketFormate += "<td style='width: 50%;text-align:right;'>"
                TicketFormate += ""
                TicketFormate += "</td>"
                TicketFormate += "</tr>"
                TicketFormate += "<tr>"
                TicketFormate += "<td style='width:100%;height:10px;'></td>"
                TicketFormate += "</tr>"
                'TicketFormate += "<tr>"
                'TicketFormate += "<td colspan='2' style='vertical-align:bottom;color:#f58220;text-align:right;width:100%;font-size:16px;font-weight:bold;'>"
                'TicketFormate += "Electronic Ticket"
                'TicketFormate += "</td>"
                'TicketFormate += "</tr>"
                TicketFormate += "<tr>"
                TicketFormate += "<td colspan='6' style='height: 2px; width: 100%; border: 1px solid #0b2759'></td>"
                TicketFormate += "</tr>"
                TicketFormate += "</table>"


                TicketFormate += "<table style='width: 100%;'>"
                TicketFormate += "<tr>"
                TicketFormate += "<td style='width: 100%; text-align: justify; color: #0b2759; font-size: 11px; padding: 10px;'>"

                Try
                    If (Convert.ToString(FltHeaderList.Rows(0)("Status")).ToLower().Trim() = "ticketed") Then
                        TicketFormate += "This is travel itinerary and E-ticket receipt. You may need to show this receipt to enter the airport and/or to show return or onward travel to "
                        TicketFormate += "customs and immigration officials."
                        TicketFormate += "<br/>"
                    Else
                        TicketFormate += ""
                    End If
                Catch ex As Exception
                    'clsErrorLog.LogInfo(ex)
                End Try

                TicketFormate += "</td>"
                TicketFormate += "</tr>"
                TicketFormate += "</table>"

                TicketFormate += "<table style='border: 1px solid #0b2759;font-size: 12px;padding:0px !important;width:100%;'>"
                TicketFormate += "<tr>"
                TicketFormate += "<td style='background-color: #0f4da2; color: #fff;font-size:12px;font-weight:bold; padding: 5px;' colspan='6'>"
                TicketFormate += "Passenger & Ticket Information"
                TicketFormate += "</td>"
                TicketFormate += "</tr>"
                TicketFormate += "<tr>"
                TicketFormate += "<td colspan='6' style='font-size:12px; padding: 5px; width: 100%'>"
                TicketFormate += "<table style='font-size:12px; padding: 5px; width: 100%'>"



                If ((Convert.ToString(FltHeaderList.Rows(0)("Status")).ToLower().Trim() = "confirm" OrElse Convert.ToString(FltHeaderList.Rows(0)("Status")).ToLower().Trim() = "confirmbyagent")) Then

                    TicketFormate += "<tr>"
                    TicketFormate += "<td style='font-size: 12px; width: 25%; text-align: left; padding: 5px;'>GDS PNR</td>"
                    TicketFormate += "<td style='font-size: 12px; width: 25%; text-align: left; padding: 5px;'>"
                    If FltHeaderList.Rows(0)("GdsPnr").Contains("-FQ") = True Then
                        TicketFormate += ""
                    Else
                        TicketFormate += FltHeaderList.Rows(0)("GdsPnr")
                    End If
                    TicketFormate += "</td>"
                    TicketFormate += "<td style='font-size: 12px; width: 25%; text-align: left; padding: 5px;'>Airline PNR</td>"
                    TicketFormate += "<td style='font-size: 12px; width: 25%; text-align: left; padding: 5px;'>"
                    If FltHeaderList.Rows(0)("GdsPnr").Contains("-FQ") = True Then
                        TicketFormate += ""
                    Else
                        TicketFormate += FltHeaderList.Rows(0)("AirlinePnr")
                    End If
                    TicketFormate += "</td>"
                    TicketFormate += "</tr>"

                Else
                    TicketFormate += "<tr>"
                    TicketFormate += "<td style='font-size: 12px; width: 25%; text-align: left; padding: 5px;'>GDS PNR</td>"
                    TicketFormate += "<td style='font-size: 12px; width: 25%; text-align: left; padding: 5px;'>"
                    TicketFormate += FltHeaderList.Rows(0)("GdsPnr")
                    TicketFormate += "</td>"
                    TicketFormate += "<td style='font-size: 12px; width: 25%; text-align: left; padding: 5px;'>Airline PNR</td>"
                    TicketFormate += "<td style='font-size: 12px; width: 25%; text-align: left; padding: 5px;'>"
                    TicketFormate += FltHeaderList.Rows(0)("AirlinePnr")
                    TicketFormate += "</td>"
                    TicketFormate += "</tr>"
                End If

                TicketFormate += "<tr>"
                TicketFormate += "<td style='font-size: 12px; width: 25%; text-align: left; padding: 5px;'>Class</td>"
                TicketFormate += "<td style='font-size: 12px; width: 25%; text-align: left; padding: 5px;'>"
                TicketFormate += GetCabin(Convert.ToString(SelectedFltDS.Tables(0).Rows(0)("Provider")), Convert.ToString(SelectedFltDS.Tables(0).Rows(0)("AdtCabin")), Convert.ToString(SelectedFltDS.Tables(0).Rows(0)("ValiDatingCarrier")))
                TicketFormate += "</td>"
                TicketFormate += "<td style='font-size: 12px; width: 25%; text-align: left; padding: 5px;'>Date Of Issue</td>"
                TicketFormate += "<td style='font-size: 12px; width: 25%; text-align: left; padding: 5px;'>"
                TicketFormate += Createddate
                TicketFormate += "</td>"
                TicketFormate += "</tr>"



                TicketFormate += "<tr>"
                TicketFormate += "<td style='font-size: 12px; width: 25%; text-align: left; padding: 5px;'>Status</td>"
                TicketFormate += "<td style='font-size: 12px; width: 25%; text-align: left; padding: 5px;'>"

                ''change mk
                If (Convert.ToString(FltHeaderList.Rows(0)("Status")).ToLower().Trim() = "ticketed") Then
                    TicketFormate += "confirmed"
                Else
                    TicketFormate += IIf(Convert.ToString(FltHeaderList.Rows(0)("Status")).ToLower().Trim() = "confirm" OrElse Convert.ToString(FltHeaderList.Rows(0)("Status")).ToLower().Trim() = "confirmbyagent", "Hold", FltHeaderList.Rows(0)("Status"))

                End If
                'TicketFormate += IIf(Convert.ToString(FltHeaderList.Rows(0)("Status")).ToLower().Trim() = "confirm", "Hold", FltHeaderList.Rows(0)("Status"))
                TicketFormate += "</td>"
                ''TicketFormate += "<td style='font-size: 11px; width: 10%; text-align: left; padding: 5px;'>Issued By</td>"
                ''TicketFormate += "<td style='font-size: 11px; width: 30%; text-align: left; padding: 5px;'>"
                ''TicketFormate += FltHeaderList.Rows(0)("AgencyName")
                ''TicketFormate += "</td>"
                TicketFormate += "<td style='font-size: 12px; width: 25%; text-align: left; padding: 5px;'>Agency Info</td>"
                TicketFormate += "<td style='font-size: 12px; width: 25%; text-align: left; padding: 5px;'>"
                TicketFormate += FltagentDetail.Rows(0)("Mobile")
                TicketFormate += "<br/>"
                TicketFormate += FltagentDetail.Rows(0)("Email")
                TicketFormate += "</td>"


                TicketFormate += "</tr>"
                TicketFormate += "<tr>"
                'TicketFormate += "<td style='text-align: left;color: #0f4da2; font-size: 11px; font-weight: bold; padding: 5px;' colspan='6'>"
                'TicketFormate += "Passenger  Information"
                'TicketFormate += "</td>"
                TicketFormate += "</tr>"
                TicketFormate += "<tr>"


                TicketFormate += "</tr>"

                'TicketFormate += "<tr>"

                'TicketFormate += "<td style='font-size: 12px; width: 25%; text-align: left; padding: 5px;'>Fare Type</td>"
                'TicketFormate += "<td style='font-size: 12px; width: 25%; text-align: left; padding: 5px;'>"
                'TicketFormate += Convert.ToString(SelectedFltDS.Tables(0).Rows(0)("AdtFareType"))
                'TicketFormate += "</td>"
                'TicketFormate += "</tr>"

                TicketFormate += "<tr>"

                TicketFormate += "<td style='font-size: 12px; width: 25%; text-align: left; padding: 5px;'>Customer Info</td>"
                TicketFormate += "<td style='font-size: 12px; width: 25%; text-align: left; padding: 5px;'>"
                TicketFormate += FltHeaderList.Rows(0)("PgMobile")
                TicketFormate += "<br/>"
                TicketFormate += FltHeaderList.Rows(0)("PgEmail")
                TicketFormate += "</td>"
                TicketFormate += "</tr>"



                For p As Integer = 0 To FltPaxList.Rows.Count - 1
                    TicketFormate += "<tr>"
                    TicketFormate += "<td style='font-size: 12px; width: 20%; text-align: left; padding: 5px;'>Passenger Name</td>"
                    TicketFormate += "<td style='font-size: 12px; width: 30%; text-align: left; padding: 5px;'>"
                    TicketFormate += FltPaxList.Rows(p)("Name") + " " + "(" + FltPaxList.Rows(p)("PaxType") + ")"
                    TicketFormate += "</td>"
                    TicketFormate += "<td style='font-size: 12px; width: 20%; text-align: left; padding: 5px;'>Ticket No.</td>"
                    TicketFormate += "<td style='font-size: 12px; width: 30%; text-align: left; padding: 5px;'>"
                    TicketFormate += FltPaxList.Rows(p)("TicketNumber")
                    TicketFormate += "</td>"
                    TicketFormate += "</tr>"
                Next

                TicketFormate += "</table>"
                TicketFormate += "</td>"
                TicketFormate += "</tr>"
                TicketFormate += "</table>"
                '' TicketFormate += "</td>"
                '' TicketFormate += "</tr>"
                TicketFormate += "<table><tr>"
                ''  TicketFormate += "<tr>"
                TicketFormate += "<td style='text-align: left; background-color: #0f4da2; color: #fff; width: 100%; padding: 5px;' colspan='8'>"
                TicketFormate += "<table style='width:100%;'>"
                TicketFormate += "<tr>"
                TicketFormate += "<td style='background-color: #0f4da2; color: #fff;font-size:12px;font-weight:bold; padding: 5px;' colspan='8'>"
                TicketFormate += "Flight Information"
                TicketFormate += "</td>"
                '' TicketFormate += "<td colspan='3' style='font-size: 12px; color: #424242; font-weight: bold; width: 75%; text-align: left; '></td>"
                TicketFormate += "</tr>"
                TicketFormate += "</table>"
                TicketFormate += "</td>"
                TicketFormate += "</tr>"

                'TicketFormate += "<tr>"
                'TicketFormate += "<td colspan='8' style='height:5px;'>&nbsp;</td>"
                'TicketFormate += "</tr>"

                TicketFormate += "<tr>"
                TicketFormate += "<td colspan='8' style='background-color: #eee;width:100%;'>"
                TicketFormate += "<table style='width:100%;'>"
                'TicketFormate += "<tr>"
                'TicketFormate += "<td style='font-size: 12px; color: #424242; width: 10%; text-align: left; padding: 4px; font-weight: bold;'>Flight</td>"
                'TicketFormate += "<td style='font-size: 12px; color: #424242; width: 20%; text-align: left; padding: 4px; font-weight: bold;'>Depart</td>"
                'TicketFormate += "<td style='font-size: 12px; color: #424242; width: 20%; text-align: left; padding: 4px; font-weight: bold;'>Arrive</td>"
                'TicketFormate += "<td style='font-size: 12px; color: #424242; width: 25%; text-align: left; padding: 4px; font-weight: bold;'>Depart Airport/Terminal</td>"
                'TicketFormate += "<td style='font-size: 12px; color: #424242; width: 25%; text-align: left; padding: 4px; font-weight: bold;'>Depart Airport/Terminal</td>"
                'TicketFormate += "</tr>"
                TicketFormate += "<tr>"
                TicketFormate += "<td style='font-size: 10.5px; color: #424242; width: 10%; text-align: left; padding: 4px; font-weight: bold;'>Flight</td>"
                TicketFormate += "<td style='font-size: 10.5px; color: #424242; width: 20%; text-align: left; padding: 4px; font-weight: bold;'>Depart</td>"
                TicketFormate += "<td style='font-size: 10.5px; color: #424242; width: 20%; text-align: left; padding: 4px; font-weight: bold;'>Arrive</td>"
                TicketFormate += "<td style='font-size: 10.5px; color: #424242; width: 25%; text-align: left; padding: 4px; font-weight: bold;'>Depart Airport/Terminal</td>"
                TicketFormate += "<td style='font-size: 10.5px; color: #424242; width: 25%; text-align: left; padding: 4px; font-weight: bold;'>Arrive Airport/Terminal</td>"
                TicketFormate += "</tr>"

                TicketFormate += "</table>"
                TicketFormate += "</td>"
                TicketFormate += "</tr>"

                For f As Integer = 0 To FltDetailsList.Rows.Count - 1

                    'TicketFormate += "</table>"


                    TicketFormate += "<tr>"
                    TicketFormate += "<td colspan='8' style='width:100%;'>"
                    TicketFormate += "<table style='width:100%;'>"
                    TicketFormate += "<tr>"


                    TicketFormate += "<td style='font-size: 12px; width: 10%; text-align: left;  vertical-align: top;'>"
                    TicketFormate += FltDetailsList.Rows(f)("AirlineCode") + " " + FltDetailsList.Rows(f)("FltNumber")
                    TicketFormate += "<br/>"
                    TicketFormate += FltDetailsList.Rows(f)("AirlineName")   '' chge mk
                    TicketFormate += "<br/>"
                    TicketFormate += "<img alt='Logo Not Found' src='https://RWT.co/AirLogo/sm" + FltDetailsList.Rows(f)("AirlineCode") + ".gif'/>"
                    TicketFormate += "</td>"
                    TicketFormate += "<td style='font-size: 12px; width: 20%; text-align: left; vertical-align: top;'>"
                    Dim strDepdt As String = Convert.ToString(FltDetailsList.Rows(f)("DepDate"))
                    'strDepdt = strDepdt.Substring(0, 2) + "-" + strDepdt.Substring(2, 2) + "-" + strDepdt.Substring(4, 2)
                    strDepdt = IIf(strDepdt.Length = 8, STD.BAL.Utility.Left(strDepdt, 4) & "-" & STD.BAL.Utility.Mid(strDepdt, 4, 2) & "-" & STD.BAL.Utility.Right(strDepdt, 2), "20" & STD.BAL.Utility.Right(strDepdt, 2) & "-" & STD.BAL.Utility.Mid(strDepdt, 2, 2) & "-" & STD.BAL.Utility.Left(strDepdt, 2))
                    Dim deptdt As DateTime = Convert.ToDateTime(strDepdt)
                    strDepdt = deptdt.ToString("dd/MMM/yy").Replace("-", "/")

                    ''Response.Write(strDepdt)


                    Dim depDay As String = Convert.ToString(deptdt.DayOfWeek)
                    strDepdt = strDepdt.Split("/")(0) + " " + strDepdt.Split("/")(1) + " " + strDepdt.Split("/")(2)
                    Dim strdeptime As String = Convert.ToString(FltDetailsList.Rows(f)("DepTime"))
                    'strdeptime = strdeptime.Substring(0, 2) + " : " + strdeptime.Substring(2, 2)
                    Try
                        If strdeptime.Length > 4 Then
                            strdeptime = strdeptime.Substring(0, 2) + " : " + strdeptime.Substring(3, 2)
                        Else
                            strdeptime = strdeptime.Substring(0, 2) + " : " + strdeptime.Substring(2, 2)
                        End If
                    Catch ex As Exception
                        clsErrorLog.LogInfo(ex)
                    End Try
                    TicketFormate += strDepdt
                    TicketFormate += "<br/>"
                    TicketFormate += "<br/>"
                    TicketFormate += strdeptime
                    TicketFormate += "</td>"

                    TicketFormate += "<td style='font-size: 12px; width: 20%; text-align: left; vertical-align: top;'>"
                    Dim strArvdt As String = Convert.ToString(FltDetailsList.Rows(f)("ArrDate"))
                    'strArvdt = strArvdt.Substring(0, 2) + "-" + strArvdt.Substring(2, 2) + "-" + strArvdt.Substring(4, 2)
                    strArvdt = IIf(strArvdt.Length = 8, STD.BAL.Utility.Left(strArvdt, 4) & "-" & STD.BAL.Utility.Mid(strArvdt, 4, 2) & "-" & STD.BAL.Utility.Right(strArvdt, 2), "20" & STD.BAL.Utility.Right(strArvdt, 2) & "-" & STD.BAL.Utility.Mid(strArvdt, 2, 2) & "-" & STD.BAL.Utility.Left(strArvdt, 2))
                    Dim Arrdt As DateTime = Convert.ToDateTime(strArvdt)
                    strArvdt = Arrdt.ToString("dd/MMM/yy").Replace("-", "/")
                    Dim ArrDay As String = Convert.ToString(Arrdt.DayOfWeek)
                    strArvdt = strArvdt.Split("/")(0) + " " + strArvdt.Split("/")(1) + " " + strArvdt.Split("/")(2)
                    Dim strArrtime As String = Convert.ToString(FltDetailsList.Rows(f)("ArrTime"))
                    'strArrtime = strArrtime.Substring(0, 2) + " : " + strArrtime.Substring(2, 2)
                    Try
                        If strArrtime.Length > 4 Then
                            strArrtime = strArrtime.Substring(0, 2) + " : " + strArrtime.Substring(3, 2)
                        Else
                            strArrtime = strArrtime.Substring(0, 2) + " : " + strArrtime.Substring(2, 2)
                        End If
                    Catch ex As Exception
                        clsErrorLog.LogInfo(ex)
                    End Try

                    TicketFormate += strArvdt
                    TicketFormate += "<br/>"
                    TicketFormate += "<br/>"
                    TicketFormate += strArrtime
                    TicketFormate += "</td>"
                    TicketFormate += "<td style='font-size: 12px; width: 25%; text-align: left; padding: 2px; '>"
                    TicketFormate += FltDetailsList.Rows(f)("DepAirName") + "( " + FltDetailsList.Rows(f)("DFrom") + ")"

                    TicketFormate += "<br/>"
                    TicketFormate += "<br/>"
                    fltTerminalDetails = TerminalDetails(OrderId, FltDetailsList.Rows(f)("DFrom"), "")
                    'if (!String.IsNullOrEmpty(Convert.ToString(fltTerminal.Rows[0]["DepartureTerminal"])))
                    '    TicketFormate += "Terminal:" + fltTerminal.Rows[0]["DepartureTerminal"];
                    'else
                    If String.IsNullOrEmpty(Convert.ToString(fltTerminalDetails.Rows(0)("DepartureTerminal"))) Then
                        TicketFormate += fltTerminalDetails.Rows(0)("DepAirportName") + " - Trml: NA"
                    Else
                        TicketFormate += fltTerminalDetails.Rows(0)("DepAirportName") + " - Trml:" + fltTerminalDetails.Rows(0)("DepartureTerminal")
                    End If

                    TicketFormate += "</td>"
                    TicketFormate += "<td style='font-size: 12px; width: 25%; text-align: left; padding: 2px; '>"
                    TicketFormate += FltDetailsList.Rows(f)("ArrAirName") + " (" + FltDetailsList.Rows(f)("ATo") + ")"
                    TicketFormate += "<br/>"
                    TicketFormate += "<br/>"
                    'if (!String.IsNullOrEmpty(Convert.ToString(fltTerminal.Rows[f]["ArrivalTerminal"])))
                    '    TicketFormate += "Terminal:" + fltTerminal.Rows[f]["ArrivalTerminal"];
                    'else
                    fltTerminalDetails = TerminalDetails(OrderId, "", FltDetailsList.Rows(f)("ATo"))
                    If String.IsNullOrEmpty(Convert.ToString(fltTerminalDetails.Rows(0)("ArrivalTerminal"))) Then
                        TicketFormate += fltTerminalDetails.Rows(0)("ArrvlAirportName") + " - Trml: NA"
                    Else
                        TicketFormate += fltTerminalDetails.Rows(0)("ArrvlAirportName") + " - Trml:" + fltTerminalDetails.Rows(0)("ArrivalTerminal")
                    End If

                    TicketFormate += "</td>"
                    TicketFormate += "</tr>"
                    TicketFormate += "</table>"
                    TicketFormate += "</td>"
                    TicketFormate += "</tr>"

                    TicketFormate += "<tr>"
                    TicketFormate += "<td colspan='8' style='width:100%;'>"
                    TicketFormate += "<table style='width:100%;'>"
                    TicketFormate += "<tr>"
                    TicketFormate += "<td style='font-size: 12px; width: 322%; text-align: left; font-weight:bold;'>"
                    'TicketFormate += "<img alt='Logo Not Found' src='https://RWT.co/AirLogo/sm" + FltDetailsList.Rows(f)("AirlineCode") + ".gif'></img>"
                    TicketFormate += "<br/>"
                    'TicketFormate += FltDetailsList.Rows(f)("AirlineName")
                    TicketFormate += "</td>"
                    TicketFormate += "<td style='width: 32%;'></td>"
                    TicketFormate += "<td style='width: 18%; font-size:12px;text-align:left;'></td>"
                    TicketFormate += "<td style='width: 18%; font-size: 11px; text-align: left; font-weight: bold;'></td>"
                    TicketFormate += "</tr>"

                Next
                TicketFormate += "</table>"
                TicketFormate += "</td>"

                TicketFormate += "</tr>"
                TicketFormate += "<tr id='TR_FareInformation1'>"
                TicketFormate += "<td colspan='8' style='background-color: #0f4da2; color: #fff;font-size:12px;font-weight:bold; padding: 5px;'>"
                TicketFormate += "Fare Information"
                TicketFormate += "</td>"
                TicketFormate += "</tr>"

                If TransID = "" OrElse TransID Is Nothing Then
                    TicketFormate += "<tr id='TR_FareInformation2'>"
                    TicketFormate += "<td colspan='8' style='background-color: #eee;width:100%;'>"
                    TicketFormate += "<table style='width:100%;'>"
                    TicketFormate += "<tr id='TR_FareInformation3'>"
                    TicketFormate += "<td style='font-size:12px; color: #424242; width: 10%; text-align: left; padding: 4px; font-weight: bold;'>Pax Type</td>"
                    TicketFormate += "<td style='font-size:12px; color: #424242; width: 10%; text-align: left; padding: 4px; font-weight: bold;'>Pax Count</td>"
                    TicketFormate += "<td style='font-size:12px; color: #424242; width: 15%; text-align: left; padding: 4px; font-weight: bold;'>Base fare</td>"
                    TicketFormate += "<td style='font-size:12px; color: #424242; width: 15%; text-align: left; padding: 4px; font-weight: bold;'>Fuel Surcharge</td>"
                    TicketFormate += "<td style='font-size:12px; color: #424242; width: 10%; text-align: left; padding: 4px; font-weight: bold;'>Tax</td>"
                    TicketFormate += "<td style='font-size:12px; color: #424242; width: 10%; text-align: left; padding: 4px; font-weight: bold;'>STax</td>"
                    TicketFormate += "<td style='font-size:12px; color: #424242; width: 10%; text-align: left; padding: 4px; font-weight: bold;'>Trans Fee</td>"
                    TicketFormate += "<td style='font-size:12px; color: #424242; width: 15%; text-align: left; padding: 4px; font-weight: bold;'>Trans Charge</td>"
                    TicketFormate += "<td style='font-size:12px; color: #424242; width: 15%; text-align: left; padding: 4px; font-weight: bold;'>Total</td>"
                    TicketFormate += "</tr>"
                    TicketFormate += "</table>"
                    TicketFormate += "</td>"
                    TicketFormate += "</tr>"


                    TicketFormate += "<tr id='TR_FareInformation4'>"
                    TicketFormate += "<td colspan='8' style='width:100%;'>"
                    TicketFormate += "<table style='width:100%;'>"
                    For fd As Integer = 0 To fltFare.Rows.Count - 1



                        If fltFare.Rows(fd)("PaxType").ToString() = "ADT" AndAlso initialAdt = 0 Then
                            Dim numberOfADT As Integer = FltPaxList.AsEnumerable().Where(Function(x) x("PaxType").ToString() = "ADT").ToList().Count
                            TicketFormate += "<tr id='TR_FareInformation5'>"
                            TicketFormate += "<td style='font-size: 12px; width: 10%; text-align: left; vertical-align: top;'>"
                            TicketFormate += fltFare.Rows(fd)("PaxType")
                            TicketFormate += "</td>"
                            TicketFormate += "<td style='font-size: 12px; width: 10%; text-align: left; vertical-align: top;' id='td_adtcnt'>" & numberOfADT & "</td>"
                            TicketFormate += "<td style='font-size: 12px; width: 15%; text-align: left; vertical-align: top;'>"
                            TicketFormate += (Convert.ToDecimal(fltFare.Rows(fd)("BaseFare")) * numberOfADT).ToString
                            TicketFormate += "</td>"
                            TicketFormate += "<td style='font-size: 12px; width: 15%; text-align: left; vertical-align: top;'>"
                            TicketFormate += (Convert.ToDecimal(fltFare.Rows(fd)("Fuel")) * numberOfADT).ToString
                            TicketFormate += "</td>"
                            TicketFormate += "<td style='font-size: 12px; width: 10%; text-align: left; vertical-align: top;' id='td_taxadt'>"
                            TicketFormate += ((Convert.ToDecimal(fltFare.Rows(fd)("Tax")) + Convert.ToDecimal(fltFare.Rows(fd)("TCharge"))) * numberOfADT).ToString
                            TicketFormate += "</td>"
                            TicketFormate += "<td style='font-size: 12px; width: 10%; text-align: left; vertical-align: top;'>"
                            TicketFormate += (Convert.ToDecimal(fltFare.Rows(fd)("ServiceTax")) * numberOfADT).ToString
                            TicketFormate += "</td>"
                            TicketFormate += "<td style='font-size: 12px; width: 10%; text-align: left; vertical-align: top;'>"
                            TicketFormate += (Convert.ToDecimal(fltFare.Rows(fd)("TFee")) * numberOfADT).ToString
                            TicketFormate += "</td>"
                            TicketFormate += "<td style='font-size: 12px; width: 15%; text-align: left; vertical-align: top;' id='td_tcadt'>"
                            ''TicketFormate += (Convert.ToDecimal(fltFare.Rows(fd)("TCharge")) * numberOfADT).ToString
                            TicketFormate += (Convert.ToDecimal(fltFare.Rows(fd)("ticketcopymarkupforTC")) * numberOfADT).ToString
                            TicketFormate += "</td>"
                            TicketFormate += "<td style='font-size: 12px; width: 15%; text-align: left; vertical-align: top;' id='td_adttot'>"
                            AdtTtlFare = (Convert.ToDecimal(fltFare.Rows(fd)("Total")) * numberOfADT).ToString
                            TicketFormate += AdtTtlFare.ToString
                            TicketFormate += "</td>"

                            TicketFormate += "</tr>"

                            initialAdt += 1
                        End If

                        If fltFare.Rows(fd)("PaxType").ToString() = "CHD" AndAlso initalChld = 0 Then
                            Dim numberOfCHD As Integer = FltPaxList.AsEnumerable().Where(Function(x) x("PaxType").ToString() = "CHD").ToList().Count
                            TicketFormate += "<tr id='TR_FareInformation6'>"
                            TicketFormate += "<td style='font-size: 12px; width: 10%; text-align: left; vertical-align: top;'>"
                            TicketFormate += fltFare.Rows(fd)("PaxType")
                            TicketFormate += "</td>"
                            TicketFormate += "<td style='font-size: 12px; width: 10%; text-align: left; vertical-align: top;' id='td_chdcnt'>" & numberOfCHD & "</td>"
                            TicketFormate += "<td style='font-size: 12px; width: 15%; text-align: left; vertical-align: top;'>"
                            TicketFormate += (Convert.ToDecimal(fltFare.Rows(fd)("BaseFare")) * numberOfCHD).ToString
                            TicketFormate += "</td>"
                            TicketFormate += "<td style='font-size: 12px; width: 15%; text-align: left; vertical-align: top;'>"
                            TicketFormate += (Convert.ToDecimal(fltFare.Rows(fd)("Fuel")) * numberOfCHD).ToString
                            TicketFormate += "</td>"
                            TicketFormate += "<td style='font-size: 12px; width: 10%; text-align: left; vertical-align: top;' id='td_taxchd'>"
                            TicketFormate += ((Convert.ToDecimal(fltFare.Rows(fd)("Tax")) + Convert.ToDecimal(fltFare.Rows(fd)("TCharge"))) * numberOfCHD).ToString
                            TicketFormate += "</td>"
                            TicketFormate += "<td style='font-size: 12px; width: 10%; text-align: left; vertical-align: top;'>"
                            TicketFormate += (Convert.ToDecimal(fltFare.Rows(fd)("ServiceTax")) * numberOfCHD).ToString
                            TicketFormate += "</td>"
                            TicketFormate += "<td style='font-size: 12px; width: 10%; text-align: left; vertical-align: top;'>"
                            TicketFormate += (Convert.ToDecimal(fltFare.Rows(fd)("TFee")) * numberOfCHD).ToString
                            TicketFormate += "</td>"
                            TicketFormate += "<td style='font-size: 12px; width: 15%; text-align: left; vertical-align: top;' id='td_tcchd'>"
                            ''TicketFormate += (Convert.ToDecimal(fltFare.Rows(fd)("TCharge")) * numberOfCHD).ToString
                            TicketFormate += (Convert.ToDecimal(fltFare.Rows(fd)("ticketcopymarkupforTC")) * numberOfCHD).ToString
                            TicketFormate += "</td>"
                            TicketFormate += "<td style='font-size: 12px; width: 15%; text-align: left; vertical-align: top;' id='td_chdtot'>"
                            ChdTtlFare = (Convert.ToDecimal(fltFare.Rows(fd)("Total")) * numberOfCHD).ToString
                            TicketFormate += ChdTtlFare.ToString
                            TicketFormate += "</td>"

                            TicketFormate += "</tr>"

                            initalChld += 1
                        End If
                        If fltFare.Rows(fd)("PaxType").ToString() = "INF" AndAlso initialift = 0 Then
                            Dim numberOfINF As Integer = FltPaxList.AsEnumerable().Where(Function(x) x("PaxType").ToString() = "INF").ToList().Count
                            TicketFormate += "<tr id='TR_FareInformation7'>"
                            TicketFormate += "<td style='font-size: 12px; width: 10%; text-align: left; vertical-align: top;'>"
                            TicketFormate += fltFare.Rows(fd)("PaxType")
                            TicketFormate += "</td>"
                            TicketFormate += "<td style='font-size: 12px; width: 10%; text-align: left; vertical-align: top;' id='td_infcnt'>" & numberOfINF & "</td>"
                            TicketFormate += "<td style='font-size: 12px; width: 15%; text-align: left; vertical-align: top;'>"
                            TicketFormate += (Convert.ToDecimal(fltFare.Rows(fd)("BaseFare")) * numberOfINF).ToString
                            TicketFormate += "</td>"
                            TicketFormate += "<td style='font-size: 12px; width: 15%; text-align: left; vertical-align: top;'>"
                            TicketFormate += (Convert.ToDecimal(fltFare.Rows(fd)("Fuel")) * numberOfINF).ToString
                            TicketFormate += "</td>"
                            TicketFormate += "<td style='font-size: 12px; width: 10%; text-align: left; vertical-align: top;'>"
                            TicketFormate += (Convert.ToDecimal(fltFare.Rows(fd)("Tax")) * numberOfINF).ToString
                            TicketFormate += "</td>"
                            TicketFormate += "<td style='font-size: 12px; width: 10%; text-align: left; vertical-align: top;'>"
                            TicketFormate += (Convert.ToDecimal(fltFare.Rows(fd)("ServiceTax")) * numberOfINF).ToString
                            TicketFormate += "</td>"
                            TicketFormate += "<td style='font-size: 12px; width: 10%; text-align: left; vertical-align: top;'>"
                            TicketFormate += (Convert.ToDecimal(fltFare.Rows(fd)("TFee")) * numberOfINF).ToString
                            TicketFormate += "</td>"
                            TicketFormate += "<td style='font-size: 12px; width: 15%; text-align: left; vertical-align: top;'>"
                            'TicketFormate += (Convert.ToDecimal(fltFare.Rows(fd)("TCharge")) * numberOfINF).ToString
                            TicketFormate += (Convert.ToDecimal(fltFare.Rows(fd)("ticketcopymarkupforTC")) * numberOfINF).ToString
                            TicketFormate += "</td>"
                            TicketFormate += "<td style='font-size: 12px; width: 15%; text-align: left; vertical-align: top;' id='td_Inftot'>"
                            INFTtlFare = (Convert.ToDecimal(fltFare.Rows(fd)("Total")) * numberOfINF).ToString
                            TicketFormate += INFTtlFare.ToString
                            TicketFormate += "</td>"
                            TicketFormate += "</tr>"
                            initialift += 1

                        End If
                    Next
                    fare = AdtTtlFare + ChdTtlFare + INFTtlFare
                Else
                    TicketFormate += "<tr id='TR_FareInformation8'>"
                    TicketFormate += "<td colspan='2' style='width:100%;'>"
                    TicketFormate += "<table style='width:100%;'>"


                    TicketFormate += "<tr>"
                    TicketFormate += "<td style='font-size:12px; width: 50%; text-align: left; vertical-align: top; display:none;' id='td_perpaxtype'>" + FltPaxList.Rows(0)("PaxType") + "</td>"
                    TicketFormate += "<td style='font-size:12px; width: 50%; text-align: left; vertical-align: top;'>Base Fare</td>"
                    TicketFormate += "<td style='font-size:12px; width: 50%; text-align: left; vertical-align: top;'>"
                    TicketFormate += fltFare.Rows(0)("BaseFare").ToString
                    TicketFormate += "</td>"
                    TicketFormate += "</tr>"
                    TicketFormate += "<tr>"
                    TicketFormate += "<td style='font-size:12px; width: 50%; text-align: left; vertical-align: top;'>Fuel Surcharge</td>"
                    TicketFormate += "<td style='font-size:12px; width: 50%; text-align: left; vertical-align: top;'>"
                    TicketFormate += fltFare.Rows(0)("Fuel").ToString
                    TicketFormate += "</td>"
                    TicketFormate += "</tr>"
                    TicketFormate += "<tr>"
                    TicketFormate += "<td style='font-size:12px; width: 50%; text-align: left; vertical-align: top;'>Tax</td>"
                    TicketFormate += "<td style='font-size:12px; width: 50%; text-align: left; vertical-align: top;' id='td_perpaxtax'>"
                    TicketFormate += fltFare.Rows(0)("Tax").ToString
                    TicketFormate += "</td>"
                    TicketFormate += "</tr>"
                    TicketFormate += "<tr>"
                    TicketFormate += "<td style='font-size:12px; width: 50%; text-align: left; vertical-align: top;'>STax</td>"
                    TicketFormate += "<td style='font-size:12px; width: 50%; text-align: left; vertical-align: top;'>"
                    TicketFormate += fltFare.Rows(0)("ServiceTax").ToString
                    TicketFormate += "</td>"
                    TicketFormate += "</tr>"
                    TicketFormate += "<tr>"
                    TicketFormate += "<td style='font-size:12px; width: 50%; text-align: left; vertical-align: top;'>Trans Fee</td>"
                    TicketFormate += "<td style='font-size:12px; width: 50%; text-align: left; vertical-align: top;'>"
                    TicketFormate += fltFare.Rows(0)("TFee").ToString
                    TicketFormate += "</td>"
                    TicketFormate += "</tr>"
                    TicketFormate += "<tr>"
                    TicketFormate += "<td style='font-size:12px; width: 50%; text-align: left; vertical-align: top;'>Trans Charge</td>"
                    TicketFormate += "<td style='font-size:12px; width: 50%; text-align: left; vertical-align: top;' id='td_perpaxtc'>"
                    TicketFormate += fltFare.Rows(0)("TCharge").ToString
                    TicketFormate += "</td>"
                    TicketFormate += "</tr>"
                    TicketFormate += "<tr>"

                    Dim ResuCharge As Decimal = 0
                    Dim ResuServiseCharge As Decimal = 0
                    Dim ResuFareDiff As Decimal = 0
                    If Convert.ToString(FltHeaderList.Rows(0)("ResuCharge")) IsNot Nothing AndAlso Convert.ToString(FltHeaderList.Rows(0)("ResuCharge")) <> "" Then
                        TicketFormate += "<tr>"
                        TicketFormate += "<td style='font-size:12px; width: 50%; text-align: left; vertical-align: top;'>Reissue Charge</td>"
                        TicketFormate += "<td style='font-size:12px; width: 50%; text-align: left; vertical-align: top;'>"
                        TicketFormate += FltHeaderList.Rows(0)("ResuCharge").ToString
                        ResuCharge = (Convert.ToDecimal(FltHeaderList.Rows(0)("ResuCharge"))).ToString
                        TicketFormate += "</td>"
                        TicketFormate += "</tr>"
                    End If
                    If Convert.ToString(FltHeaderList.Rows(0)("ResuServiseCharge")) IsNot Nothing AndAlso Convert.ToString(FltHeaderList.Rows(0)("ResuServiseCharge")) <> "" Then
                        TicketFormate += "<tr>"
                        TicketFormate += "<td style='font-size:12px; width: 50%; text-align: left; vertical-align: top;'>Reissue Srv. Charge</td>"
                        TicketFormate += "<td style='font-size:12px; width: 50%; text-align: left; vertical-align: top;'>"
                        TicketFormate += FltHeaderList.Rows(0)("ResuServiseCharge").ToString
                        ResuServiseCharge = (Convert.ToDecimal(FltHeaderList.Rows(0)("ResuServiseCharge"))).ToString
                        TicketFormate += "</td>"
                        TicketFormate += "</tr>"
                    End If
                    If Convert.ToString(FltHeaderList.Rows(0)("ResuFareDiff")) IsNot Nothing AndAlso Convert.ToString(FltHeaderList.Rows(0)("ResuFareDiff")) <> "" Then
                        TicketFormate += "<tr>"
                        TicketFormate += "<td style='font-size:12px; width: 50%; text-align: left; vertical-align: top;'>Reissue Fare Diff</td>"
                        TicketFormate += "<td style='font-size:12px; width: 50%; text-align: left; vertical-align: top;'>"
                        TicketFormate += FltHeaderList.Rows(0)("ResuFareDiff").ToString
                        ResuFareDiff = (Convert.ToDecimal(FltHeaderList.Rows(0)("ResuFareDiff"))).ToString
                        TicketFormate += "</td>"
                        TicketFormate += "</tr>"
                    End If
                    TicketFormate += "<td style='font-size: 12px; width: 50%; text-align: left; vertical-align: top;'>TOTAL</td>"
                    TicketFormate += "<td style='font-size: 12px; width: 50%; text-align: left; vertical-align: top;' id='td_totalfare'>"
                    fare = (Convert.ToDecimal(fltFare.Rows(0)("BaseFare")) + Convert.ToDecimal(fltFare.Rows(0)("Fuel")) + Convert.ToDecimal(fltFare.Rows(0)("Tax")) + Convert.ToDecimal(fltFare.Rows(0)("ServiceTax")) + Convert.ToDecimal(fltFare.Rows(0)("TCharge")) + Convert.ToDecimal(fltFare.Rows(0)("TFee")) + ResuCharge + ResuServiseCharge + ResuFareDiff).ToString
                    TicketFormate += fare.ToString
                    TicketFormate += "</td>"

                    'fare = Convert.ToDecimal(fltFare.Rows[0]["Total"]) + ResuCharge + ResuServiseCharge + ResuFareDiff;
                    TicketFormate += "</tr>"
                End If
                TicketFormate += "</table>"
                TicketFormate += "</td>"
                TicketFormate += "</tr>"
                If fltMealAndBag.Rows.Count > 0 Then
                    TicketFormate += "<tr id='TR_FareInformation9'>"
                    TicketFormate += "<td colspan='8' style='background-color: #eee;width:100%;'>"
                    TicketFormate += "<table style='width:100%;'>"
                    TicketFormate += "<tr>"
                    TicketFormate += "<td style='font-size:12px; color: #424242; width: 15%; text-align: left; padding: 4px; font-weight: bold;'>Pax Name</td>"
                    TicketFormate += "<td style='font-size:12px; color: #424242; width: 15%; text-align: left; padding: 4px; font-weight: bold;'>Trip Type</td>"
                    TicketFormate += "<td style='font-size:12px; color: #424242; width: 15%; text-align: left; padding: 4px; font-weight: bold;'>Meal Code</td>"
                    TicketFormate += "<td style='font-size:12px; color: #424242; width: 15%; text-align: left; padding: 4px; font-weight: bold;'>Meal Price</td>"
                    TicketFormate += "<td style='font-size:12px; color: #424242; width: 15%; text-align: left; padding: 4px; font-weight: bold;'>Baggage Code</td>"
                    TicketFormate += "<td style='font-size:12px; color: #424242; width: 15%; text-align: left; padding: 4px; font-weight: bold;'>Baggage Price</td>"
                    TicketFormate += "<td style='font-size:12px; color: #424242; width: 10%; text-align: left; padding: 4px; font-weight: bold;'>Total</td>"
                    TicketFormate += "</tr>"
                    TicketFormate += "</table>"
                    TicketFormate += "</td>"
                    TicketFormate += "</tr>"

                    TicketFormate += "<tr>"
                    TicketFormate += "<td colspan='8' style='width:100%;'>"
                    TicketFormate += "<table style='width:100%;'>"


                    For i As Integer = 0 To fltMealAndBag.Rows.Count - 1
                        'If Convert.ToString(fltMealAndBag.Rows(i)("MealPrice")) <> "0.00" AndAlso Convert.ToString(fltMealAndBag.Rows(i)("BaggagePrice")) <> "0.00" Then
                        TicketFormate += "<tr>"
                        TicketFormate += "<td style='font-size: 12px; width: 15%; text-align: left; vertical-align: top;'>"
                        TicketFormate += Convert.ToString(fltMealAndBag.Rows(i)("Name"))
                        TicketFormate += "</td>"
                        TicketFormate += "<td style='font-size: 12px; width: 15%; text-align: left; vertical-align: top;'>"
                        TicketFormate += Convert.ToString(fltMealAndBag.Rows(i)("TripType"))
                        TicketFormate += "</td>"
                        TicketFormate += "<td style='font-size: 12px; width: 15%; text-align: left; vertical-align: top;'>"
                        TicketFormate += Convert.ToString(fltMealAndBag.Rows(i)("MealCode"))
                        TicketFormate += "</td>"
                        TicketFormate += "<td style='font-size: 12px; width: 15%; text-align: left; vertical-align: top;'>"
                        TicketFormate += Convert.ToString(fltMealAndBag.Rows(i)("MealPrice"))
                        TicketFormate += "</td>"
                        TicketFormate += "<td style='font-size: 12px; width: 15%; text-align: left; vertical-align: top;'>"
                        TicketFormate += Convert.ToString(fltMealAndBag.Rows(i)("BaggageCode"))
                        TicketFormate += "</td>"
                        TicketFormate += "<td style='font-size: 12px; width: 15%; text-align: left; vertical-align: top;'>"
                        TicketFormate += Convert.ToString(fltMealAndBag.Rows(i)("BaggagePrice"))
                        TicketFormate += "</td>"
                        TicketFormate += "<td style='font-size: 12px; width: 10%; text-align: left; vertical-align: top;'>"
                        MealBagTotalPrice += Convert.ToDecimal(fltMealAndBag.Rows(i)("TotalPrice"))
                        TicketFormate += Convert.ToString(fltMealAndBag.Rows(i)("TotalPrice"))
                        TicketFormate += "</td>"

                        TicketFormate += "</tr>"
                        'End If
                    Next
                    TicketFormate += "</table>"
                    TicketFormate += "</td>"
                    TicketFormate += "</tr>"
                End If


                TicketFormate += "<tr id='TR_FareInformation10'>"
                TicketFormate += "<td colspan='8' style='background-color: #0f4da2; color:#fff;font-size:12px;font-weight:bold; padding: 5px;'>"
                TicketFormate += "<table style='width:100%;' id='TR_FareInformation11'>"
                TicketFormate += "<tr>"
                TicketFormate += "<td style='font-size:12px; width: 20%; text-align: left; vertical-align: top;'></td>"
                TicketFormate += "<td style='font-size:12px; width: 20%; text-align: left; vertical-align: top;'></td>"
                TicketFormate += "<td style='font-size:12px; width: 20%; text-align: left; vertical-align: top;'></td>"
                TicketFormate += "<td style='font-size:12px; width: 20.5%; text-align: left; vertical-align: top;'></td>"
                TicketFormate += "<td style='color: #fff; font-size:12px; width: 15%; text-align: left; vertical-align: top;'>Grand Total</td>"
                TicketFormate += "<td style='color: #fff; font-size:12px; width: 10%; text-align: left; vertical-align: top;' id='td_grandtot'>"
                TicketFormate += (fare + MealBagTotalPrice).ToString
                TicketFormate += "</td>"

                TicketFormate += "</tr>"
                TicketFormate += "</table>"
                TicketFormate += "</td>"
                TicketFormate += "</tr>"


                ''GST INFORMATION MK
                Try
                    If ((String.IsNullOrEmpty(FltHeaderList.Rows(0)("GSTNO").ToString())) And (FltHeaderList.Rows(0)("GSTNO").ToString.Length < 8)) Then

                    Else
                        ''  TicketFormate += "<table style='border: 1px solid #0b2759;font-size: 12px;padding:0px !important;width:100%;'>"
                        TicketFormate += "<tr>"
                        TicketFormate += "<td style='background-color: #eee;width:100%;' colspan='6'>"
                        TicketFormate += "GST Information"
                        TicketFormate += "</td>"
                        TicketFormate += "</tr>"
                        TicketFormate += "<tr>"
                        TicketFormate += "<td colspan='6' style='font-size:12px; padding: 5px; width: 100%'>"
                        TicketFormate += "<table style='font-size:12px; padding: 5px; width: 100%'>"


                        TicketFormate += "<tr>"
                        TicketFormate += "<td style='font-size: 12px; width: 25%; text-align: left; padding: 5px;'>GST NO</td>"
                        TicketFormate += "<td style='font-size: 12px; width: 25%; text-align: left; padding: 5px;'>"
                        TicketFormate += FltHeaderList.Rows(0)("GSTNO").ToString()
                        TicketFormate += "</td>"
                        TicketFormate += "<td style='font-size: 12px; width: 25%; text-align: left; padding: 5px;'>GST Company</td>"
                        TicketFormate += "<td style='font-size: 12px; width: 25%; text-align: left; padding: 5px;'>"
                        TicketFormate += FltHeaderList.Rows(0)("GST_Company_Name").ToString()
                        TicketFormate += "</td>"
                        TicketFormate += "</tr>"

                        TicketFormate += "<tr>"
                        TicketFormate += "<td style='font-size: 12px; width: 25%; text-align: left; padding: 5px;'>GST Address</td>"
                        TicketFormate += "<td style='font-size: 12px; width: 25%; text-align: left; padding: 5px;'>"
                        TicketFormate += FltHeaderList.Rows(0)("GST_Company_Address").ToString()
                        TicketFormate += "</td>"
                        TicketFormate += "<td style='font-size: 12px; width: 25%; text-align: left; padding: 5px;'>GST PhoneNo</td>"
                        TicketFormate += "<td style='font-size: 12px; width: 25%; text-align: left; padding: 5px;'>"
                        TicketFormate += FltHeaderList.Rows(0)("GST_PhoneNo").ToString()
                        TicketFormate += "</td>"
                        TicketFormate += "</tr>"

                        TicketFormate += "<tr>"
                        TicketFormate += "<td style='font-size: 12px; width: 25%; text-align: left; padding: 5px;'>GST Email</td>"
                        TicketFormate += "<td style='font-size: 12px; width: 25%; text-align: left; padding: 5px;'>"
                        TicketFormate += FltHeaderList.Rows(0)("GST_Email").ToString()
                        TicketFormate += "</td>"
                        TicketFormate += "</tr>"

                        TicketFormate += "</table>"
                        TicketFormate += "</td>"
                        TicketFormate += "</tr>"
                        ''  TicketFormate += "</table>"

                        TicketFormate += "<tr>"
                        TicketFormate += "<td colspan='8' style='background-color: #0f4da2; color: #fff; font-size: 11px; font-weight: bold; padding: 5px;'></td>"
                        TicketFormate += "</tr>"
                    End If
                Catch ex As Exception

                End Try
                ''GST INFORMATION END



                'TicketFormate += "<br/><br/>"
                TicketFormate += "<tr>"
                TicketFormate += "<td colspan='8'>"
                TicketFormate += "<ul style='list-style-image: url(https://RWT.co/Images/bullet.png);'>"
                TicketFormate += "<li style='font-size:10.5px;'>Kindly confirm the status of your PNR within 24 hrs of booking, as at times the same may fail on account of payment failure, internet connectivity, booking engine or due to any other reason beyond our control."
                TicketFormate += "For Customers who book their flights well in advance of the scheduled departure date it is necessary that you re-confirm the departure time of your flight between 72 and 24 hours before the Scheduled Departure Time.</li>"
                TicketFormate += "</ul>"
                TicketFormate += "</td>"
                TicketFormate += "</tr>"

                TicketFormate += "<tr>"
                TicketFormate += "<td colspan='8' style='background-color: #0f4da2; color: #fff; font-size: 11px; font-weight: bold; padding: 5px;'>Terms & Conditions :</td>"
                TicketFormate += "</tr>"

                TicketFormate += "<tr>"
                TicketFormate += "<td colspan='8'>"
                TicketFormate += "<ul style='list-style-image: url(https://RWT.co/Images/bullet.png);'>"
                TicketFormate += "<li style='font-size:10.5px;'>Guests are requested to carry their valid photo identification for all guests, including children.</li>"
                TicketFormate += "<li style='font-size:10.5px;'>We recommend check-in at least 2 hours prior to departure.</li>"
                TicketFormate += "<li style='font-size:10.5px;'>Boarding gates close 45 minutes prior to the scheduled time of departure. Please report at your departure gate at the indicated boarding time. Any passenger failing to report in time may be refused boarding privileges.</li>"
                TicketFormate += "<li style='font-size:10.5px;'>Cancellations and Changes permitted more than two (2) hours prior to departure with payment of change fee and difference in fare if applicable only in working hours (10:00 am to 06:00 pm) except Sundays and Holidays.</li>"
                TicketFormate += "<li style='font-size:10.5px;'>"
                TicketFormate += "Flight schedules are subject to change and approval by authorities."
                TicketFormate += "<br />"
                TicketFormate += "</li>"
                TicketFormate += "<li style='font-size:10.5px;'>"
                TicketFormate += "Name Changes on a confirmed booking are strictly prohibited. Please ensure that the name given at the time of booking matches as mentioned on the traveling Guests valid photo ID Proof."
                TicketFormate += "<br />"
                TicketFormate += " Travel Agent does not provide compensation for travel on other airlines, meals, lodging or ground transportation."
                TicketFormate += "</li>"
                TicketFormate += "<li style='font-size:10.5px;'>Bookings made under the Armed Forces quota are non cancelable and non- changeable.</li>"

                TicketFormate += "<li style='font-size:10.5px;'>Guests are advised to check their all flight details (including their Name, Flight numbers, Date of Departure, Sectors) before leaving the Agent Counter.</li>"
                TicketFormate += "<li style='font-size:10.5px;'>Cancellation amount will be charged as per airline rule.</li>"
                TicketFormate += "<li style='font-size:10.5px;'>Guests requiring wheelchair assistance, stretcher, Guests traveling with infants and unaccompanied minors need to be booked in advance since the inventory for these special service requests are limited per flight.</li>"
                TicketFormate += "</ul>"
                TicketFormate += "</td>"
                TicketFormate += "</tr>"
                TicketFormate += "</table>"
                TicketFormate += "<table style='width: 100%;'>"
                TicketFormate += "<tr>"
                TicketFormate += "<td colspan='8' style='background-color: #eee; color: #424242; font-size: 12px; font-weight: bold; padding: 5px;'>Baggage Information :"
                TicketFormate += "</td>"
                TicketFormate += "</tr>"
                Bag = False
                If Not String.IsNullOrEmpty(Convert.ToString(SelectedFltDS.Tables(0).Rows(0)("IsBagFare"))) Then
                    Bag = Convert.ToBoolean(SelectedFltDS.Tables(0).Rows(0)("IsBagFare"))
                End If

                Dim dtbaggage As New DataTable
                dtbaggage = objTranDom.GetBaggageInformation("D", FltHeaderList.Rows(0)("VC"), Bag).Tables(0)
                Dim bginfo As String = GetBagInfo(Convert.ToString(SelectedFltDS.Tables(0).Rows(0)("Provider")), Convert.ToString(SelectedFltDS.Tables(0).Rows(0)("AirlineRemark")))


                If (Convert.ToString(FltHeaderList.Rows(0)("Trip")).ToUpper = "I" And (FltHeaderList.Rows(0)("VC").ToUpper() = "SG" Or FltHeaderList.Rows(0)("VC").ToString().ToUpper = "6E")) Then


                    If (Convert.ToString(FltHeaderList.Rows(0)("VC")).ToUpper = "SG") Then
                        TicketFormate += "<tr>"
                        TicketFormate += "<td style='font-size:10.5px; colspan='4'>Spice jet baggage Allowance are India to Male,Bangkok : 20 Kgs and  India to Dubai,Kabul,Muscat,Colombo : 30 kgs and From Colombo,Dubai,Kabul : 30 Kgs And Muscat,Bangkok,Dhaka : 20kgs</td>"

                        TicketFormate += "</tr>"


                    End If

                    If (Convert.ToString(FltHeaderList.Rows(0)("VC")).ToUpper = "6E") Then

                        TicketFormate += "<tr>"
                        TicketFormate += "<td style='font-size:10.5px; colspan='4'>IndiGo allows free Checked Baggage up to 20kgs per adult and child (any Customer above 2 yrs of age).Free checked baggage allowance for travel to and from Colombo, Dubai, Doha, Muscat, Sharjah, Singapore, Abu Dhabi and Kuwait is up to 30kgs per adult and child. (any Customer above 2 yrs of age).</td>"

                        TicketFormate += "</tr>"

                    End If
                Else

                    If bginfo = "" Then

                        For Each drbg In dtbaggage.Rows

                            TicketFormate += "<tr>"
                            TicketFormate += "<td colspan='2'>" & drbg("BaggageName") & "</td>"
                            TicketFormate += "<td colspan='2'>" & drbg("Weight") & "</td>"
                            TicketFormate += "</tr>"
                        Next


                    Else
                        TicketFormate += "<tr>"
                        TicketFormate += "<td colspan='2'></td>"
                        TicketFormate += "<td colspan='2'>" & bginfo & "</td>"
                        TicketFormate += "</tr>"

                    End If
                End If


                TicketFormate += "</table>"


                If (FltDetailsList.Rows.Count = 1) Then
                ElseIf (FltDetailsList.Rows.Count = 2) Then

                    TicketFormate += "</td>"
                    TicketFormate += "</tr>"
                    TicketFormate += "</table>"

                ElseIf (FltDetailsList.Rows.Count = 3) Then
                    TicketFormate += "</td>"
                    TicketFormate += "</tr>"
                    TicketFormate += "</table>"
                    TicketFormate += "</td>"
                    TicketFormate += "</tr>"
                    TicketFormate += "</table>"

                ElseIf (FltDetailsList.Rows.Count = 4) Then
                    TicketFormate += "</td>"
                    TicketFormate += "</tr>"
                    TicketFormate += "</table>"
                    TicketFormate += "</td>"
                    TicketFormate += "</tr>"
                    TicketFormate += "</table>"
                    TicketFormate += "</td>"
                    TicketFormate += "</tr>"
                    TicketFormate += "</table>"
                ElseIf (FltDetailsList.Rows.Count = 5) Then

                    TicketFormate += "</td>"
                    TicketFormate += "</tr>"
                    TicketFormate += "</table>"
                    TicketFormate += "</td>"
                    TicketFormate += "</tr>"
                    TicketFormate += "</table>"
                    TicketFormate += "</td>"
                    TicketFormate += "</tr>"
                    TicketFormate += "</table>"
                    TicketFormate += "</td>"
                    TicketFormate += "</tr>"
                    TicketFormate += "</table>"

                ElseIf (FltDetailsList.Rows.Count = 6) Then

                    TicketFormate += "</td>"
                    TicketFormate += "</tr>"
                    TicketFormate += "</table>"
                    TicketFormate += "</td>"
                    TicketFormate += "</tr>"
                    TicketFormate += "</table>"
                    TicketFormate += "</td>"
                    TicketFormate += "</tr>"
                    TicketFormate += "</table>"
                    TicketFormate += "</td>"
                    TicketFormate += "</tr>"
                    TicketFormate += "</table>"
                    TicketFormate += "</td>"
                    TicketFormate += "</tr>"
                    TicketFormate += "</table>"

                ElseIf (FltDetailsList.Rows.Count = 7) Then

                    TicketFormate += "</td>"
                    TicketFormate += "</tr>"
                    TicketFormate += "</table>"
                    TicketFormate += "</td>"
                    TicketFormate += "</tr>"
                    TicketFormate += "</table>"
                    TicketFormate += "</td>"
                    TicketFormate += "</tr>"
                    TicketFormate += "</table>"
                    TicketFormate += "</td>"
                    TicketFormate += "</tr>"
                    TicketFormate += "</table>"
                    TicketFormate += "</td>"
                    TicketFormate += "</tr>"
                    TicketFormate += "</table>"
                    TicketFormate += "</td>"
                    TicketFormate += "</tr>"
                    TicketFormate += "</table>"

                ElseIf (FltDetailsList.Rows.Count = 8) Then

                    TicketFormate += "</td>"
                    TicketFormate += "</tr>"
                    TicketFormate += "</table>"
                    TicketFormate += "</td>"
                    TicketFormate += "</tr>"
                    TicketFormate += "</table>"
                    TicketFormate += "</td>"
                    TicketFormate += "</tr>"
                    TicketFormate += "</table>"
                    TicketFormate += "</td>"
                    TicketFormate += "</tr>"
                    TicketFormate += "</table>"
                    TicketFormate += "</td>"
                    TicketFormate += "</tr>"
                    TicketFormate += "</table>"
                    TicketFormate += "</td>"
                    TicketFormate += "</tr>"
                    TicketFormate += "</table>"
                    TicketFormate += "</td>"
                    TicketFormate += "</tr>"
                    TicketFormate += "</table>"

                End If


                'TicketFormate += "</td>"
                'TicketFormate += "</tr>"
                'TicketFormate += "</table>"
            End If

            'TicketFormate += "<table style='width: 100%;'>"
            'TicketFormate += "<tr>"
            'TicketFormate += "<td style='width: 100%; text-align: justify; color: #0f4da2; font-size: 11px; padding: 10px; font-size:10.5px;'>"
            ''TicketFormate += "For any assistance contact: ATPI International Pvt. Ltd. | Tel: 00-91-2240095555 | Fax: 00-91-2240095556 | "

            'TicketFormate += "</td>"
            'TicketFormate += "</tr>"
            'TicketFormate += "</table>"
            '#End Region
            'Dim Body As String = ""

            'Dim status As Integer = 0
            'Try

            '    strFileNmPdf = ConfigurationManager.AppSettings("HTMLtoPDF").ToString().Trim() + FltHeaderList.Rows(0)("GdsPnr") + "-" + DateTime.Now.ToString().Replace(":", "").Replace("/", "-").Replace(" ", "-").Trim() + ".pdf"
            '    Dim pdfDoc As New iTextSharp.text.Document(PageSize.A4)
            '    Dim writer As PdfWriter = PdfWriter.GetInstance(pdfDoc, New FileStream(strFileNmPdf, FileMode.Create, FileAccess.ReadWrite, FileShare.None))
            '    pdfDoc.Open()
            '    Dim sr As New StringReader(TicketFormate)
            '    iTextSharp.tool.xml.XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, sr)
            '    pdfDoc.Close()
            '    writer.Dispose()
            '    sr.Dispose()
            '    writePDF = True
            '    Return TicketFormate
            'Catch ex As Exception
            'End Try
            Return TicketFormate
        Catch ex As Exception
            ' Response.Write(ex.Message & ex.StackTrace.ToString())
            clsErrorLog.LogInfo(ex)



        End Try
    End Function

    Public Function TicketCopyExportPDF(OrderId As String, TransID As String) As String

        Dim strFileNmPdf As String = ""
        Dim writePDF As Boolean = False
        Dim TktCopy As String = ""
        Dim Gtotal As Integer = 0
        Dim initialAdt As Integer = 0
        Dim initalChld As Integer = 0
        Dim initialift As Integer = 0
        Dim MealBagTotalPrice As Decimal = 0
        Dim AdtTtlFare As Decimal = 0
        Dim ChdTtlFare As Decimal = 0
        Dim INFTtlFare As Decimal = 0
        Dim fare As Decimal = 0
        Dim TBasefare As Decimal = 0
        Dim ABasefare As Decimal = 0
        Dim CBasefare As Decimal = 0
        Dim IBasefare As Decimal = 0

        Dim Tfuel As Decimal = 0
        Dim Afuel As Decimal = 0
        Dim Cfuel As Decimal = 0
        Dim Ifuel As Decimal = 0

        Dim Ttax As Decimal = 0
        Dim Atax As Decimal = 0
        Dim Ctax As Decimal = 0
        Dim Itax As Decimal = 0

        Dim Tgst As Decimal = 0
        Dim Agst As Decimal = 0
        Dim Cgst As Decimal = 0
        Dim Igst As Decimal = 0

        Dim TTransfee As Decimal = 0
        Dim ATransfee As Decimal = 0
        Dim CTransfee As Decimal = 0
        Dim ITransfee As Decimal = 0

        Dim TTranscharge As Decimal = 0
        Dim ATranscharge As Decimal = 0
        Dim CTranscharge As Decimal = 0
        Dim ITranscharge As Decimal = 0

        'Dim OrderId As String = "1c2019deXCP9cVSU"
        'Dim TransID As String = ""


        Dim objTranDom As New SqlTransactionDom()
        Dim SqlTrasaction As New SqlTransaction()
        Dim objSql As New SqlTransactionNew()
        Dim FltPaxList As New DataTable()

        Dim FltDetailsList As New DataTable()
        Dim FltProvider As New DataTable()
        Dim FltBaggage As New DataTable()
        Dim dtagentid As New DataTable()
        Dim FltagentDetail As New DataTable()
        Dim fltTerminal As New DataTable()
        Dim fltFare As New DataTable()
        Dim fltMealAndBag As New DataTable()
        Dim fltMealAndBag1 As New DataTable()
        Dim FltHeaderList As New DataTable()
        Dim fltTerminalDetails As New DataTable()
        Dim SelectedFltDS As New DataSet()

        Dim SeatListO As List(Of STD.Shared.Seat)

        Dim bgdetail As String = ""
        FltPaxList = SelectPaxDetail(OrderId, TransID)
        FltHeaderList = objTktCopy.SelectHeaderDetail(OrderId)
        FltDetailsList = objTktCopy.SelectFlightDetail(OrderId)
        FltProvider = (objTranDom.GetTicketingProvider(OrderId)).Tables(0)
        dtagentid = objTktCopy.SelectAgent(OrderId)
        SelectedFltDS = SqlTrasaction.GetFltDtls(OrderId, dtagentid.Rows(0)("AgentID").ToString())
        Dim Bag As Boolean = False
        If Not String.IsNullOrEmpty(Convert.ToString(SelectedFltDS.Tables(0).Rows(0)("IsBagFare"))) Then
            Bag = Convert.ToBoolean(SelectedFltDS.Tables(0).Rows(0)("IsBagFare"))
        End If
        FltBaggage = (objTranDom.GetBaggageInformation(Convert.ToString(FltHeaderList.Rows(0)("Trip")), Convert.ToString(FltHeaderList.Rows(0)("VC")), Bag)).Tables(0)
        FltagentDetail = objTktCopy.SelectAgencyDetail(dtagentid.Rows(0)("AgentID").ToString())
        fltFare = objTktCopy.SelectFareDetail(OrderId, TransID)
        Dim dt As DateTime = Convert.ToDateTime(Convert.ToString(FltHeaderList.Rows(0)("CreateDate")))
        Dim [date] As String = dt.ToString("dd/MMM/yyyy").Replace("-", "/")

        Dim Createddate As String = [date].Split("/")(0) + " " + [date].Split("/")(1) + " " + [date].Split("/")(2)

        Dim fltmealbag As DataRow() = objSql.Get_MEAL_BAG_FareDetails(OrderId, TransID).Tables(0).Select("MealPrice>0 or BaggagePrice>0 ")
        fltMealAndBag1 = objSql.Get_MEAL_BAG_FareDetails(OrderId, TransID).Tables(0) '.Select("MealPrice>0 or BaggagePrice>0 ").CopyToDataTable()
        If fltmealbag.Length > 0 Then

            fltMealAndBag = fltMealAndBag1.Select("MealPrice>0 or BaggagePrice>0 ").CopyToDataTable()
        End If
        ''Dim IFLT As FlightCommonBAL = New FlightCommonBAL(ConfigurationManager.ConnectionStrings("myAmdDB").ConnectionString)
        ''SeatListO = IFLT.SeatDetails(OrderId)
        Try
            Dim TicketFormate As String = ""
            If ((Convert.ToString(FltHeaderList.Rows(0)("Status")).ToLower().Trim() = "confirm" OrElse Convert.ToString(FltHeaderList.Rows(0)("Status")).ToLower().Trim() = "confirmbyagent")) Then
                TicketFormate += "<table style='width:100%;'>"
                TicketFormate += "<tr>"
                TicketFormate += "<td style='font-size: 15px; width: 15%; text-align: left; padding: 5px;'>"
                TicketFormate += "<b>Booking Reference No. " & OrderId & "</b>"
                TicketFormate += "</td>"
                TicketFormate += "</tr>"
                TicketFormate += "<tr>"
                TicketFormate += "<td style='font-size: 14px; width: 15%; text-align: left; padding: 5px;'>"
                ''TicketFormate += "The PNR-<b>" & FltHeaderList.Rows(0)("GdsPnr") & " </b>is on <b>HOLD</b> and contact customer care for issuance."
                TicketFormate += "PNR-<b>" & FltHeaderList.Rows(0)("GdsPnr") & " </b>is on <b>HOLD</b>.Functioning team is working on it and may take 15 minutes to resolve. Please contact our customer care representative at <b>+91 00 0000 0000</b> for any further assistance"
                TicketFormate += "</td>"
                TicketFormate += "</tr>"
                TicketFormate += "</table>"
            End If
            'Dim strAirline As String = "SG6EG8"
            Dim dd As String = ""
            Dim AgentAddress As String = ""
            'Try
            '    Dim filepath As String = Server.MapPath("~\AgentLogo") + "\" + Convert.ToString(dtagentid.Rows(0)("AgentID")) + ".jpg" 'Server.MapPath("~/AgentLogo/" + LogoName)
            '    If (System.IO.File.Exists(filepath)) Then

            '        dd = "http://tripforo.com/AgentLogo/" + Convert.ToString(dtagentid.Rows(0)("AgentID")) + ".jpg"
            '        AgentAddress = "<p style='font-weight: bolder; font-size: 20px;height: 8px;margin: 0px 0 20px !important;text-align:right;'>" + Convert.ToString(FltagentDetail.Rows(0)("Agency_Name")) + "</p><p style='font-size: 13px;height: 8px;margin: 0px 0 13px !important;text-align:right;'>" + Convert.ToString(FltagentDetail.Rows(0)("Address")) + "</p><p style='font-size: 13px;height: 8px;margin: 0px 0 13px !important;text-align:right;'>" + Convert.ToString(FltagentDetail.Rows(0)("Address1")) + "</p><p style='font-size: 13px;height: 8px;margin: 0px 0 13px !important;text-align:right;'>Mobile:" + Convert.ToString(FltagentDetail.Rows(0)("Mobile")) + "</p><p style='font-size: 13px;height: 8px;margin: 0px 0 13px !important;text-align:right;'>Email:" + Convert.ToString(FltagentDetail.Rows(0)("Email")) + "</p>"
            '    Else
            '        'dd = "http://tripforo.com/Advance_CSS/Icons/logo(ft).png"
            '        AgentAddress = "<p style='font-weight: bolder; font-size: 20px;height: 8px;margin: 0px 0 20px !important;text-align:right;'>" + Convert.ToString(FltagentDetail.Rows(0)("Agency_Name")) + "</p><p style='font-size: 13px;height: 8px;margin: 0px 0 13px !important;text-align:right;'>" + Convert.ToString(FltagentDetail.Rows(0)("Address")) + "</p><p style='font-size: 13px;height: 8px;margin: 0px 0 13px !important;text-align:right;'>" + Convert.ToString(FltagentDetail.Rows(0)("Address1")) + "</p><p style='font-size: 13px;height: 8px;margin: 0px 0 13px !important;text-align:right;'>Mobile:" + Convert.ToString(FltagentDetail.Rows(0)("Mobile")) + "</p><p style='font-size: 13px;height: 8px;margin: 0px 0 13px !important;text-align:right;'>Email:" + Convert.ToString(FltagentDetail.Rows(0)("Email")) + "</p>"
            '    End If
            'Catch ex As Exception
            '    'clsErrorLog.LogInfo(ex)
            '    'dd = "../Advance_CSS/Icons/logo(ft).png"
            'End Try
            Try
                Dim filepath As String = Server.MapPath("~\AgentLogo") + "\" + Convert.ToString(dtagentid.Rows(0)("AgentID")) + ".jpg" 'Server.MapPath("~/AgentLogo/" + LogoName)
                If (System.IO.File.Exists(filepath)) Then
                    dd = "/AgentLogo/" + Convert.ToString(dtagentid.Rows(0)("AgentID")) + ".jpg"
                    AgentAddress = Convert.ToString(FltagentDetail.Rows(0)("Agency_Name")) + "<br/>" + Convert.ToString(FltagentDetail.Rows(0)("Address")) + "<br/>" + Convert.ToString(FltagentDetail.Rows(0)("Address1")) + "<br/>Mobile:" + Convert.ToString(FltagentDetail.Rows(0)("Mobile")) + "<br/>Email:" + Convert.ToString(FltagentDetail.Rows(0)("Email"))
                Else
                    AgentAddress = Convert.ToString(FltagentDetail.Rows(0)("Agency_Name")) + "<br/>" + Convert.ToString(FltagentDetail.Rows(0)("Address")) + "<br/>" + Convert.ToString(FltagentDetail.Rows(0)("Address1")) + "<br/>Mobile:" + Convert.ToString(FltagentDetail.Rows(0)("Mobile")) + "<br/>Email:" + Convert.ToString(FltagentDetail.Rows(0)("Email"))
                End If
            Catch ex As Exception
            End Try

            If (FltDetailsList.Rows(0)("AirlineCode") = "SG") Then
                BackClor = "#9c1304"
            ElseIf (FltDetailsList.Rows(0)("AirlineCode") = "6E") Then
                BackClor = "#2d3c9a"
            ElseIf (FltDetailsList.Rows(0)("AirlineCode") = "G8") Then
                BackClor = "#172169"
            ElseIf (FltDetailsList.Rows(0)("AirlineCode") = "UK") Then
                BackClor = "#47143d"
            ElseIf (FltDetailsList.Rows(0)("AirlineCode") = "AK") Then
                BackClor = "#e32525"
            ElseIf (FltDetailsList.Rows(0)("AirlineCode") = "I5") Then
                BackClor = "#e32525"

            ElseIf (FltDetailsList.Rows(0)("AirlineCode") = "AI") Then
                BackClor = "#e32525"
            Else

                BackClor = "#000"
            End If


            TicketFormate += "<div class='large-12 medium-12 small-12' style='border:1px solid " + BackClor + "'>"
            TicketFormate += "<table style='width: 100%;'>"
            TicketFormate += "<tbody>"
            TicketFormate += "<tr id='trLogoWithAgencySection'>"
            TicketFormate += "<td colspan='4'>"
            If Not String.IsNullOrEmpty(dd) Then
                TicketFormate += "<img id='agencylogo' src='" + dd + "' alt='Logo' width='244' height='87'/>"
            End If
            TicketFormate += "</td>"
            TicketFormate += "<td style='background-color: white;width:55%; text-align:right;font-size: 15px;' colspan='4'>"
            TicketFormate += AgentAddress
            'TicketFormate += "<img src='/prepod/Images/icons/plane.png' style='width: 40px; position: relative; bottom: -25px; bottom: 56px; bottom: -35px;' />"
            'TicketFormate += "<font face='Arial' style='font-weight: 900; font-size: 30px; position: relative; left: 8px; bottom: -38px;'>E-Ticket</font><font style='position: relative; left: 176px; bottom: 54px; left: 21px; bottom: -36px;'>Reference No." + OrderId + " </font>" ''ttt
            TicketFormate += "</td>"
            'TicketFormate += "</tr>"
            'TicketFormate += "<tr><td class='pri' class='pri' style='background-color:#fff; color: #000; font-size: 12px; font-weight: bold; padding: 5px;' colspan='8'><font face='Arial' style='font-weight: 100;font-size: 25px;position: relative;left: 8px;bottom: 0px;'><img alt='Logo Not Found' style='width: 35px;margin-left: 20px;' src='http://richatravels.in/AirLogo/sm" + FltDetailsList.Rows(0)("AirlineCode") + ".gif'/><span style='font-weight: bold;font-size: 15px;margin-left: 15px;'>" + FltDetailsList.Rows(0)("AirlineName") + "</span></td></tr>"
            'TicketFormate += "<tr>"
            'TicketFormate += "</tr>"
            'TicketFormate += "<tr style='display:none;'><td class='pri' class='pri' style='background-color:" + BackClor + "; color: #fff; font-size: 12px; font-weight: bold; padding: 5px;' colspan='8'><font face='Arial' style='font-weight: 100;font-size: 25px;position: relative;left: 8px;bottom: 0px;'>E-Ticket</font><font style='position: relative; left: 176px; bottom: 54px; left: 21px; bottom: 0px;z-index:1011'>Reference No." + OrderId + "  </font><input type='hidden' id='hdnReferenceNo' value='" + OrderId + "' /></td></tr>"
            'TicketFormate += "<tr>"
            TicketFormate += "</tr>"
            TicketFormate += "<tr>"

            Dim IsAllPaxCancelled As Boolean = False
            For p As Integer = 0 To FltPaxList.Rows.Count - 1
                If FltPaxList.Rows(p)("PaxStatus").ToString().ToLower() <> "cancelled" Then
                    IsAllPaxCancelled = True
                End If
            Next

            TicketFormate += "<td  colspan='4' style='background-color: white;border: 1px solid #ccc;'>"
            TicketFormate += "<p style='font-size: 15px;height: 8px;'>Booking Date : " + FltHeaderList.Rows(0)("Createdate") + "</p>"
            TicketFormate += "<p style='font-size: 15px;height: 8px;'>Ref No : " + OrderId + "</p>"

            If IsAllPaxCancelled = True Then
                TicketFormate += "<p style='font-size: 15px;height: 8px;'>Status : <span style='color:green;text-transform: uppercase;font-weight: bold;'>" + FltHeaderList.Rows(0)("Status").ToString().ToUpper() + "</span></p>"
            Else
                TicketFormate += "<p style='font-size: 15px;height: 8px;'>Status : <span style='color:red;text-transform: uppercase;font-weight: bold;'>CANCELLED</span></p>"
            End If


            TicketFormate += "</td>"
            ' TicketFormate += "<td style='background-color: white;border: 1px solid #ccc;'><div id='barcodeTarget' style='float:right;' class='barcodeTarget'></div><canvas id='canvasTarget' style='width:150px; height:150px;'></canvas> "'
            TicketFormate += "<td colspan='2' style='background-color: white;border: 1px solid #ccc;width: 270px;border-right: 0px solid #fff;'><img alt='Logo Not Found' style='width: 35px;margin-left: 20px;' src='/AirLogo/sm" + FltDetailsList.Rows(0)("AirlineCode") + ".gif'/><i style='font-weight: bold;font-size: 15px;margin-left: 15px;margin: 0px 0 0px !important;'>" + FltDetailsList.Rows(0)("AirlineName") + "</i>"
            TicketFormate += "</td>"
            TicketFormate += "<td colspan='2' style='background-color: white;border: 1px solid #ccc;width: 176px;border-left: 0px solid;'><p style='font-weight: bold;font-size: 15px;margin: 0px 0 0px !important;text-transform:uppercase;'>Airline PNR : " + FltHeaderList.Rows(0)("GdsPnr") + ""
            TicketFormate += "</td>"
            TicketFormate += "</tr>"
            TicketFormate += "</tbody>"
            TicketFormate += "</table>"


            If (Convert.ToString(FltHeaderList.Rows(0)("Status")).ToLower().Trim() = "ticketed") Then
                For f As Integer = 0 To FltDetailsList.Rows.Count - 1
                    TicketFormate += "<table style='border: font-size: 12px; padding: 0px !important; width: 100%;'>"
                    TicketFormate += " <tbody>"
                    TicketFormate += "<tr><td class='pri' style='background-color:" + BackClor + "; color: #fff; font-size: 12px; font-weight: bold; padding: 5px;width: 40%;text-transform:uppercase;' colspan='4'>Airline Pnr :  " + FltHeaderList.Rows(0)("GdsPnr") + "/" + FltHeaderList.Rows(0)("AirlinePnr") + "</td><td class='pri' style='background-color: " + BackClor + "; color: #fff; font-size: 12px; font-weight: bold; padding: 5px;text-align: end;width: 60%' colspan='4'>Confirmed Booked On:" + FltHeaderList.Rows(0)("Createdate") + "</td></tr>"
                    TicketFormate += " <tr>"
                    TicketFormate += "<td colspan='8' style='font-size: 12px; padding: 5px; width: 100%'>"
                    TicketFormate += "<table style='font-size: 12px; padding: 5px; width: 100%;'>"
                    TicketFormate += " <tbody>"
                    TicketFormate += " <tr>"

                    'Try
                    '    Dim dtbaggage As New DataTable
                    '    dtbaggage = objTranDom.GetBaggageInformation("D", FltHeaderList.Rows(0)("VC"), Bag).Tables(0)
                    '    Dim bginfo As String = GetBagInfo(Convert.ToString(SelectedFltDS.Tables(0).Rows(0)("Provider")), Convert.ToString(SelectedFltDS.Tables(0).Rows(0)("AirlineRemark")))
                    '    If bginfo = "" Then

                    '        For Each drbg In dtbaggage.Rows

                    '            bgdetail += drbg("BaggageName") & "|" & drbg("Weight")
                    '        Next
                    '    Else
                    '        bgdetail = bginfo

                    '    End If


                    'Catch ex As Exception

                    'End Try
                    Dim strDepdt As String = Convert.ToString(FltDetailsList.Rows(f)("DepDate"))
                    strDepdt = IIf(strDepdt.Length = 8, STD.BAL.Utility.Left(strDepdt, 4) & "-" & STD.BAL.Utility.Mid(strDepdt, 4, 2) & "-" & STD.BAL.Utility.Right(strDepdt, 2), "20" & STD.BAL.Utility.Right(strDepdt, 2) & "-" & STD.BAL.Utility.Mid(strDepdt, 2, 2) & "-" & STD.BAL.Utility.Left(strDepdt, 2))
                    Dim deptdt As DateTime = Convert.ToDateTime(strDepdt)
                    strDepdt = deptdt.ToString("dd/MMM/yy").Replace("-", "/")

                    'Response.Write(strDepdt)

                    Dim depDay As String = Convert.ToString(deptdt.DayOfWeek)
                    strDepdt = strDepdt.Split("/")(0) + " " + strDepdt.Split("/")(1) + " " + strDepdt.Split("/")(2)
                    Dim strdeptime As String = Convert.ToString(FltDetailsList.Rows(f)("DepTime")).Replace(":", "")
                    Try
                        If strdeptime.Length > 4 Then
                            strdeptime = strdeptime.Substring(0, 2) + " : " + strdeptime.Substring(3, 2)
                        Else
                            strdeptime = strdeptime.Substring(0, 2) + " : " + strdeptime.Substring(2, 2)
                        End If
                    Catch ex As Exception
                        clsErrorLog.LogInfo(ex)
                    End Try
                    TicketFormate += "<td colspan='2' style='font-size: 12px; text-align: left; vertical-align: top;'><img alt='Logo Not Found' style='width: 50px;' src='/AirLogo/sm" + FltDetailsList.Rows(f)("AirlineCode") + ".gif'/><br />" + FltDetailsList.Rows(f)("AirlineName") + "<br />" + FltDetailsList.Rows(f)("AirlineCode") + " " + FltDetailsList.Rows(f)("FltNumber") + "<br/>" + bgdetail + "</td>"
                    'TicketFormate += "<td colspan='2' width='100'></td><td colspan='2' width='100'></td><td colspan='2' width='100'></td>"
                    TicketFormate += "<td colspan='2' style='font-size: 12px; text-align: left; padding: 2px; text-align: right;'>"
                    TicketFormate += "<p style='font-size: 16px;font-weight:600;text-align:right;'>" + FltDetailsList.Rows(f)("DepAirName") + "( " + FltDetailsList.Rows(f)("DFrom") + ")" + " " + strdeptime + "</p>" ''tttt

                    TicketFormate += "<br />" + strDepdt + "<br />" ''tttt
                    fltTerminalDetails = TerminalDetails(OrderId, FltDetailsList.Rows(f)("DFrom"), "")
                    If String.IsNullOrEmpty(Convert.ToString(fltTerminalDetails.Rows(0)("DepartureTerminal"))) Then
                        TicketFormate += fltTerminalDetails.Rows(0)("DepAirportName") + " - Trml: NA"
                    Else
                        TicketFormate += fltTerminalDetails.Rows(0)("DepAirportName") + " - Trml:" + fltTerminalDetails.Rows(0)("DepartureTerminal")
                    End If
                    TicketFormate += "</td>"
                    Dim strArvdt As String = Convert.ToString(FltDetailsList.Rows(f)("ArrDate"))
                    strArvdt = IIf(strArvdt.Length = 8, STD.BAL.Utility.Left(strArvdt, 4) & "-" & STD.BAL.Utility.Mid(strArvdt, 4, 2) & "-" & STD.BAL.Utility.Right(strArvdt, 2), "20" & STD.BAL.Utility.Right(strArvdt, 2) & "-" & STD.BAL.Utility.Mid(strArvdt, 2, 2) & "-" & STD.BAL.Utility.Left(strArvdt, 2))
                    Dim Arrdt As DateTime = Convert.ToDateTime(strArvdt)
                    strArvdt = Arrdt.ToString("dd/MMM/yy").Replace("-", "/")
                    Dim ArrDay As String = Convert.ToString(Arrdt.DayOfWeek)
                    strArvdt = strArvdt.Split("/")(0) + " " + strArvdt.Split("/")(1) + " " + strArvdt.Split("/")(2)
                    Dim strArrtime As String = Convert.ToString(FltDetailsList.Rows(f)("ArrTime")).Replace(":", "")
                    Try
                        If strArrtime.Length > 4 Then
                            strArrtime = strArrtime.Substring(0, 2) + " : " + strArrtime.Substring(3, 2)
                        Else
                            strArrtime = strArrtime.Substring(0, 2) + " : " + strArrtime.Substring(2, 2)
                        End If
                    Catch ex As Exception
                        clsErrorLog.LogInfo(ex)
                    End Try
                    Dim totdur As String = ""
                    Dim stops As String = ""
                    Try
                        totdur = Convert.ToString(SelectedFltDS.Tables(0).Rows(0)("Tot_Dur"))
                        stops = Convert.ToString(SelectedFltDS.Tables(0).Rows(0)("Stops"))
                    Catch ex As Exception
                        totdur = Convert.ToString(SelectedFltDS.Tables(0).Rows(0)("Duration"))
                        stops = Convert.ToString(FltDetailsList.Rows.Count)

                    End Try
                    TicketFormate += "<td colspan='2' style='text-align: center;'><i style='font-weight: 600'>" + totdur + "</i><br />" ''tttt
                    TicketFormate += "------------------<br />"
                    TicketFormate += "" + stops + "<br />" '''tttt
                    TicketFormate += "" + Convert.ToString(SelectedFltDS.Tables(0).Rows(0)("AdtFareType")) + "</td>" '''ttt
                    TicketFormate += "<td colspan='2' style='font-size: 12px; text-align: left; padding: 2px;'>"
                    TicketFormate += "<p style='font-size: 16px; font-weight: 600;'>" + strArrtime + " " + FltDetailsList.Rows(f)("ArrAirName").ToString().Trim() + " (" + FltDetailsList.Rows(f)("ATo").ToString().Trim() + ")</p>" ''ttt



                    TicketFormate += "<br />" + strArvdt + "<br />" ''''tttttt

                    fltTerminalDetails = TerminalDetails(OrderId, "", FltDetailsList.Rows(f)("ATo"))
                    If String.IsNullOrEmpty(Convert.ToString(fltTerminalDetails.Rows(0)("ArrivalTerminal"))) Then
                        TicketFormate += fltTerminalDetails.Rows(0)("ArrvlAirportName") + " - Trml: NA"
                    Else
                        TicketFormate += fltTerminalDetails.Rows(0)("ArrvlAirportName") + " - Trml:" + fltTerminalDetails.Rows(0)("ArrivalTerminal")
                    End If
                    TicketFormate += "</td>" '''tttt
                    TicketFormate += "</tr>"
                    TicketFormate += "</tbody>"
                    TicketFormate += "</table>"
                    TicketFormate += "</td>"
                    TicketFormate += "</tr>"
                    TicketFormate += " </tbody>"
                    TicketFormate += "</table>"
                Next

                '''''''''''''''''end''''''''''''''''''''''
                ''''''''''''''''''''Passenger &amp; Ticket Information'''''''''''''''''''''''''''

                TicketFormate += " <table style='border: 1px solid #0b2759; font-size: 12px; padding: 0px !important; width: 100%;'>"
                TicketFormate += "<tbody>"
                TicketFormate += "<tr>"
                TicketFormate += "<td class='pri' style='background-color: " + BackClor + "; color: #fff; font-size: 12px; font-weight: bold; padding: 5px;' colspan='6'>Passenger &amp; Ticket Information</td>"
                TicketFormate += "</tr>"
                TicketFormate += "<tr>"
                TicketFormate += "<td colspan='6' style='font-size: 12px; padding: 5px; width: 100%'>"
                TicketFormate += "<table style='font-size: 12px; padding: 5px; width: 100%; border: 1px solid #000'>"
                TicketFormate += "<tbody style='border: 1px solid #000;'>"
                TicketFormate += "<tr style='border: 1px solid #000;'>"
                TicketFormate += "<th style='font-size: 12px; text-align: left; padding: 5px; border-right: 1px solid #000;'>Passenger Information</th>"
                TicketFormate += "<th style='font-size: 12px; text-align: left; padding: 5px; border-right: 1px solid #000;'>Sector</th>"
                TicketFormate += "<th style='font-size: 12px; text-align: left; padding: 5px; border-right: 1px solid #000;'>Ticket No</th>"
                TicketFormate += "<th style='font-size: 12px; text-align: left; padding: 5px; border-right: 1px solid #000;'>Status</th>"
                TicketFormate += "<th style='font-size: 12px; text-align: left; padding: 5px; border-right: 1px solid #000;'>Seat</th>"
                TicketFormate += "<th style='font-size: 12px; text-align: left; padding: 5px; border-right: 1px solid #000;'>Meal</th>"
                ''TicketFormate += "<th style='font-size: 12px; text-align: left; padding: 5px; border-right: 1px solid #000;'>Class</th>"
                ''TicketFormate += "<th style='font-size: 12px; text-align: left; padding: 5px;'>Status</th>"
                TicketFormate += "<th style='font-size: 12px; text-align: left; padding: 5px;'>Extra</th>"
                TicketFormate += "</tr>"

                For p As Integer = 0 To FltPaxList.Rows.Count - 1
                    TicketFormate += "<tr style='border: 1px solid #000;'>"
                    TicketFormate += "<td style='font-size: 12px; text-align: left; padding: 5px; border-right: 1px solid #000;text-transform:uppercase;'>" + FltPaxList.Rows(p)("Name") + "</td>" ''ttt
                    TicketFormate += "<td style='font-size: 12px; text-align: left; padding: 5px; border-right: 1px solid #000;border-collapse:collapse !important; padding: 0px 0px !important; margin:0 !important; text-indent:10px;'>"
                    TicketFormate += "<table style='font-size: 12px; padding: 5px; width: 100%; border: 1px solid #000;margin-bottom: 0px;'>" ''ttt
                    TicketFormate += "<tbody>"
                    For f As Integer = 0 To FltDetailsList.Rows.Count - 1

                        TicketFormate += "<tr  >"
                        TicketFormate += "<td style='font-size: 12px; text-align: left; padding: 5px;'>" + FltDetailsList.Rows(f)("DFrom") + "-" + FltDetailsList.Rows(f)("ATo") + "</td>" ''ttt
                        TicketFormate += "</tr>"



                    Next
                    TicketFormate += "</tbody>"
                    TicketFormate += "</table>" ''ttt
                    TicketFormate += "</td>"
                    TicketFormate += "<td style='font-size: 12px; text-align: left; padding: 5px; border-right: 1px solid #000;text-transform:uppercase;'>" + Convert.ToString(FltPaxList.Rows(p)("TicketNumber")) + "</td>" ''ttt

                    If FltPaxList.Rows(p)("PaxStatus").ToString().ToLower() = "cancelled" Then
                        TicketFormate += "<td style='font-size: 12px; text-align: left; padding: 5px; border-right: 1px solid #000;text-transform:uppercase;color:red;font-weight:bold;'>" + Convert.ToString(FltPaxList.Rows(p)("PaxStatus")) + "</td>"
                    Else
                        TicketFormate += "<td style='font-size: 12px; text-align: left; padding: 5px; border-right: 1px solid #000;text-transform:uppercase;color:green;font-weight:bold;'>" + Convert.ToString(FltPaxList.Rows(p)("PaxStatus")) + "</td>"
                    End If


                    If (Not SeatListO Is Nothing) Then


                        For i As Integer = 0 To SeatListO.Count - 1
                            Dim dts As DataTable = New DataTable()
                            dts = SelectPaxDetail(OrderId, SeatListO(i).PaxId)
                            TicketFormate += "<td style='font-size: 12px; text-align: left; padding: 5px; border-right: 1px solid #000;'>" + SeatListO(i).SeatDesignator + "</td>" ''Seat Number
                            TicketFormate += "<td style='font-size: 12px; text-align: left; padding: 5px; border-right: 1px solid #000;'>" & dts.Rows(0)("MealType") & "</td>" ''ttt meal
                            TicketFormate += "<td style='font-size: 12px; text-align: left; padding: 5px; border-right: 1px solid #000;'>" & SeatListO(i).Amount & "</td>" ''ttt amount
                        Next
                    Else
                        TicketFormate += "<td style='font-size: 12px; text-align: left; padding: 5px; border-right: 1px solid #000;'>None</td>"
                        TicketFormate += "<td style='font-size: 12px; text-align: left; padding: 5px; border-right: 1px solid #000;'>None</td>"
                        TicketFormate += "<td style='font-size: 12px; text-align: left; padding: 5px; border-right: 1px solid #000;'>None</td>"
                    End If



                    ''TicketFormate += "<td style='font-size: 12px; text-align: left; padding: 5px; border-right: 1px solid #000;'>" + GetCabin(Convert.ToString(SelectedFltDS.Tables(0).Rows(0)("Provider")), Convert.ToString(SelectedFltDS.Tables(0).Rows(0)("AdtCabin")), Convert.ToString(SelectedFltDS.Tables(0).Rows(0)("ValiDatingCarrier"))) + "</td>" ''ttt
                    ''TicketFormate += "<td style='font-size: 12px; text-align: left; padding: 5px;'>CONFIRMED</td>" ''ttt
                    ''ttt
                    TicketFormate += "</tr>"
                Next

                TicketFormate += "</tbody>"
                TicketFormate += "</table>"
                TicketFormate += "</td>"
                TicketFormate += "</tr>"
                TicketFormate += "</tbody>"
                TicketFormate += "</table>"
                ''''''''''''''''''''End''''''''''''''
                ''''''''''''''''''''Fare Breakup''''''''''
                If TransID = "" OrElse TransID Is Nothing Then
                    TicketFormate += "<table style='border: 1px solid #0b2759; font-size: 12px; padding: 0px !important; width: 100%;'>"
                    TicketFormate += "<tbody>"
                    TicketFormate += "<tr class='pri2'>"
                    TicketFormate += "<td colspan='6' style= 'background-color:#fff;width:100%;border: 1px solid #000;'>"
                    TicketFormate += "<table style='width:100%;' id='fareinfo'>"
                    TicketFormate += "<tbody style='border: 1px solid #000;'>"
                    TicketFormate += "<tr>"
                    TicketFormate += "<td class='pri' style='background-color: " + BackClor + "; color: #fff; font-size: 12px; font-weight: bold; padding: 5px;' colspan='9'>Fare Information</td>"
                    TicketFormate += "</tr>"
                    TicketFormate += "<tr style='border: 1px solid #000;'>"
                    TicketFormate += "<th style='font-size: 12px; text-align: left; padding: 5px; border-right: 1px solid #000;'>Pax Type</th>"
                    TicketFormate += "<th style='font-size: 12px; text-align: left; padding: 5px; border-right: 1px solid #000;'>Pax Count</th>"
                    TicketFormate += "<th style='font-size: 12px; text-align: left; padding: 5px; border-right: 1px solid #000;'>Base fare</th>"
                    TicketFormate += "<th class='pri2' style='font-size: 12px; text-align: left; padding: 5px; border-right: 1px solid #000;'>Fuel Surcharge</th>"
                    TicketFormate += "<th style='font-size: 12px; text-align: left; padding: 5px; border-right: 1px solid #000;'>Tax</th>"
                    TicketFormate += "<th style='font-size: 12px; text-align: left; padding: 5px; border-right: 1px solid #000;'>STax</th>"
                    TicketFormate += "<th style='font-size: 12px; text-align: left; padding: 5px; border-right: 1px solid #000;'>Trans Fee</th>"
                    TicketFormate += "<th style='font-size: 12px; text-align: left; padding: 5px; border-right: 1px solid #000;'>Trans Charge</th>"
                    TicketFormate += "<th style='font-size: 12px; text-align: left; padding: 5px; border-right: 1px solid #000;'>Total</th>"
                    TicketFormate += "</tr>"
                    For fd As Integer = 0 To fltFare.Rows.Count - 1

                        If fltFare.Rows(fd)("PaxType").ToString() = "ADT" AndAlso initialAdt = 0 Then
                            Dim numberOfADT As Integer = FltPaxList.AsEnumerable().Where(Function(x) x("PaxType").ToString() = "ADT").ToList().Count
                            TicketFormate += "<tr>"
                            TicketFormate += "<td style='font-size: 12px; text-align: left; padding: 5px; border-right: 1px solid #000;'  id='td_perpaxtype'>"
                            TicketFormate += fltFare.Rows(fd)("PaxType")
                            TicketFormate += "</td>"
                            TicketFormate += "<td style='font-size: 12px; text-align: left; padding: 5px; border-right: 1px solid #000;' id='td_adtcnt'>" & numberOfADT & "</td>"
                            TicketFormate += "<td style='font-size: 12px; text-align: left; padding: 5px; border-right: 1px solid #000;'>"
                            TicketFormate += (Convert.ToDecimal(fltFare.Rows(fd)("BaseFare")) * numberOfADT).ToString
                            ABasefare = Convert.ToDecimal(fltFare.Rows(fd)("BaseFare")) * numberOfADT
                            TicketFormate += "</td>"
                            TicketFormate += "<td style='font-size: 12px; text-align: left; padding: 5px; border-right: 1px solid #000;'>"
                            TicketFormate += (Convert.ToDecimal(fltFare.Rows(fd)("Fuel")) * numberOfADT).ToString
                            Afuel = Convert.ToDecimal(fltFare.Rows(fd)("Fuel")) * numberOfADT
                            TicketFormate += "</td>"
                            TicketFormate += "<td style='font-size: 12px; text-align: left; padding: 5px; border-right: 1px solid #000;'id='td_taxadt'>"
                            'TicketFormate += ((Convert.ToDecimal(fltFare.Rows(fd)("Tax")) * numberOfADT) + (Convert.ToDecimal(fltFare.Rows(fd)("TCharge")) * numberOfADT)).ToString
                            'TicketFormate += (Convert.ToDecimal(fltFare.Rows(fd)("Tax")) * numberOfADT).ToString
                            Atax = (Convert.ToDecimal(fltFare.Rows(fd)("Tax")) * numberOfADT) + (Convert.ToDecimal(fltFare.Rows(fd)("TCharge")) * numberOfADT) + (Convert.ToDecimal(fltFare.Rows(fd)("ticketcopymarkupforTAX")) * numberOfADT)
                            Atax = (Convert.ToDecimal(fltFare.Rows(fd)("Tax")) * numberOfADT)
                            TicketFormate += Atax & "</td>"
                            TicketFormate += "<td style='font-size: 12px; text-align: left; padding: 5px; border-right: 1px solid #000;'>"
                            TicketFormate += (Convert.ToDecimal(fltFare.Rows(fd)("ServiceTax")) * numberOfADT).ToString
                            Agst = Convert.ToDecimal(fltFare.Rows(fd)("ServiceTax")) * numberOfADT
                            TicketFormate += "</td>"
                            TicketFormate += "<td style='font-size: 12px; text-align: left; padding: 5px; border-right: 1px solid #000;'>"
                            TicketFormate += (Convert.ToDecimal(fltFare.Rows(fd)("TFee")) * numberOfADT).ToString
                            TTransfee = Convert.ToDecimal(fltFare.Rows(fd)("TFee")) * numberOfADT
                            TicketFormate += "</td>"
                            TicketFormate += "<td style='font-size: 12px; text-align: left; padding: 5px; border-right: 1px solid #000;'id='td_tcadt'>"
                            TicketFormate += (Convert.ToDecimal(fltFare.Rows(fd)("TCharge")) * numberOfADT).ToString
                            ATranscharge = Convert.ToDecimal(fltFare.Rows(fd)("TCharge")) * numberOfADT
                            TicketFormate += "</td>"
                            TicketFormate += "<td style='font-size: 12px; text-align: left; padding: 5px; border-right: 1px solid #000;' id='td_adttot'>"
                            AdtTtlFare = (Convert.ToDecimal(fltFare.Rows(fd)("Total")) * numberOfADT).ToString
                            TicketFormate += AdtTtlFare.ToString
                            TicketFormate += "</td>"

                            TicketFormate += "</tr>"

                            initialAdt += 1
                        End If

                        If fltFare.Rows(fd)("PaxType").ToString() = "CHD" AndAlso initalChld = 0 Then
                            Dim numberOfCHD As Integer = FltPaxList.AsEnumerable().Where(Function(x) x("PaxType").ToString() = "CHD").ToList().Count
                            TicketFormate += "<tr>"
                            TicketFormate += "<td style='font-size: 12px; text-align: left; padding: 5px; border-right: 1px solid #000;border-top: 1px solid #000;background: #fff;'  id='td_perpaxtype'>"
                            TicketFormate += fltFare.Rows(fd)("PaxType")
                            TicketFormate += "</td>"
                            TicketFormate += "<td style='font-size: 12px; text-align: left; padding: 5px; border-right: 1px solid #000;border-top: 1px solid #000;background: #fff;' id='td_chdcnt'>" & numberOfCHD & "</td>"
                            TicketFormate += "<td style='font-size: 12px; text-align: left; padding: 5px; border-right: 1px solid #000;border-top: 1px solid #000;background: #fff;'>"
                            TicketFormate += (Convert.ToDecimal(fltFare.Rows(fd)("BaseFare")) * numberOfCHD).ToString
                            CBasefare = Convert.ToDecimal(fltFare.Rows(fd)("BaseFare")) * numberOfCHD
                            TicketFormate += "</td>"
                            TicketFormate += "<td style='font-size: 12px; text-align: left; padding: 5px; border-right: 1px solid #000;border-top: 1px solid #000;background: #fff;'>"
                            TicketFormate += (Convert.ToDecimal(fltFare.Rows(fd)("Fuel")) * numberOfCHD).ToString
                            Cfuel = Convert.ToDecimal(fltFare.Rows(fd)("Fuel")) * numberOfCHD
                            TicketFormate += "</td>"
                            TicketFormate += "<td style='font-size: 12px; text-align: left; padding: 5px; border-right: 1px solid #000;border-top: 1px solid #000;background: #fff;'id='td_taxchd'>"
                            TicketFormate += (Convert.ToDecimal(fltFare.Rows(fd)("Tax")) * numberOfCHD).ToString
                            Ctax = Convert.ToDecimal(fltFare.Rows(fd)("Tax")) * numberOfCHD
                            TicketFormate += "</td>"
                            TicketFormate += "<td style='font-size: 12px; text-align: left; padding: 5px; border-right: 1px solid #000;border-top: 1px solid #000;background: #fff;'>"
                            TicketFormate += (Convert.ToDecimal(fltFare.Rows(fd)("ServiceTax")) * numberOfCHD).ToString
                            Cgst = Convert.ToDecimal(fltFare.Rows(fd)("ServiceTax")) * numberOfCHD
                            TicketFormate += "</td>"
                            TicketFormate += "<td style='font-size: 12px; text-align: left; padding: 5px; border-right: 1px solid #000;border-top: 1px solid #000;background: #fff;'>"
                            TicketFormate += (Convert.ToDecimal(fltFare.Rows(fd)("TFee")) * numberOfCHD).ToString
                            CTransfee = Convert.ToDecimal(fltFare.Rows(fd)("TFee")) * numberOfCHD
                            TicketFormate += "</td>"
                            TicketFormate += "<td style='font-size: 12px; text-align: left; padding: 5px; border-right: 1px solid #000;border-top: 1px solid #000;background: #fff;'id='td_tcchd'>"
                            TicketFormate += (Convert.ToDecimal(fltFare.Rows(fd)("TCharge")) * numberOfCHD).ToString
                            CTranscharge = Convert.ToDecimal(fltFare.Rows(fd)("TCharge")) * numberOfCHD
                            TicketFormate += "</td>"
                            TicketFormate += "<td style='font-size: 12px; text-align: left; padding: 5px; border-right: 1px solid #000;border-top: 1px solid #000;background: #fff;'id='td_chdtot'>"
                            ChdTtlFare = (Convert.ToDecimal(fltFare.Rows(fd)("Total")) * numberOfCHD).ToString
                            TicketFormate += ChdTtlFare.ToString
                            TicketFormate += "</td>"

                            TicketFormate += "</tr>"

                            initalChld += 1
                        End If
                        If fltFare.Rows(fd)("PaxType").ToString() = "INF" AndAlso initialift = 0 Then
                            Dim numberOfINF As Integer = FltPaxList.AsEnumerable().Where(Function(x) x("PaxType").ToString() = "INF").ToList().Count
                            TicketFormate += "<tr>"
                            TicketFormate += "<td style='font-size: 12px; text-align: left; padding: 5px; border-right: 1px solid #000;border-top: 1px solid #000;background: #fff;' id='td_perpaxtype'>"
                            TicketFormate += fltFare.Rows(fd)("PaxType")
                            TicketFormate += "</td>"
                            TicketFormate += "<td style='font-size: 12px; text-align: left; padding: 5px; border-right: 1px solid #000;border-top: 1px solid #000;background: #fff;' id='td_infcnt'>" & numberOfINF & "</td>"
                            TicketFormate += "<td style='font-size: 12px; text-align: left; padding: 5px; border-right: 1px solid #000;border-top: 1px solid #000;background: #fff;'>"
                            TicketFormate += (Convert.ToDecimal(fltFare.Rows(fd)("BaseFare")) * numberOfINF).ToString
                            IBasefare = Convert.ToDecimal(fltFare.Rows(fd)("BaseFare")) * numberOfINF
                            TicketFormate += "</td>"
                            TicketFormate += "<td style='font-size: 12px; text-align: left; padding: 5px; border-right: 1px solid #000;border-top: 1px solid #000;background: #fff;'>"
                            TicketFormate += (Convert.ToDecimal(fltFare.Rows(fd)("Fuel")) * numberOfINF).ToString
                            Ifuel = Convert.ToDecimal(fltFare.Rows(fd)("Fuel")) * numberOfINF
                            TicketFormate += "</td>"
                            TicketFormate += "<td style='font-size: 12px; text-align: left; padding: 5px; border-right: 1px solid #000;border-top: 1px solid #000;background: #fff;'>"
                            TicketFormate += (Convert.ToDecimal(fltFare.Rows(fd)("Tax")) * numberOfINF).ToString
                            Itax = (Convert.ToDecimal(fltFare.Rows(fd)("Tax")) * numberOfINF).ToString
                            TicketFormate += "</td>"
                            TicketFormate += "<td style='font-size: 12px; text-align: left; padding: 5px; border-right: 1px solid #000;border-top: 1px solid #000;background: #fff;'>"
                            TicketFormate += (Convert.ToDecimal(fltFare.Rows(fd)("ServiceTax")) * numberOfINF).ToString
                            Igst = Convert.ToDecimal(fltFare.Rows(fd)("ServiceTax")) * numberOfINF
                            TicketFormate += "</td>"
                            TicketFormate += "<td style='font-size: 12px; text-align: left; padding: 5px; border-right: 1px solid #000;border-top: 1px solid #000;background: #fff;'>"
                            TicketFormate += (Convert.ToDecimal(fltFare.Rows(fd)("TFee")) * numberOfINF).ToString
                            ITransfee = Convert.ToDecimal(fltFare.Rows(fd)("TFee")) * numberOfINF
                            TicketFormate += "</td>"
                            TicketFormate += "<td style='font-size: 12px; text-align: left; padding: 5px; border-right: 1px solid #000;border-top: 1px solid #000;background: #fff;'>"
                            TicketFormate += (Convert.ToDecimal(fltFare.Rows(fd)("TCharge")) * numberOfINF).ToString
                            ITranscharge = Convert.ToDecimal(fltFare.Rows(fd)("TCharge")) * numberOfINF
                            TicketFormate += "</td>"
                            TicketFormate += "<td style='font-size: 12px; text-align: left; padding: 5px; border-right: 1px solid #000;border-top: 1px solid #000;background: #fff;'id='td_Inftot'>"
                            INFTtlFare = (Convert.ToDecimal(fltFare.Rows(fd)("Total")) * numberOfINF).ToString
                            TicketFormate += INFTtlFare.ToString
                            TicketFormate += "</td>"
                            TicketFormate += "</tr>"
                            initialift += 1

                        End If
                    Next
                    TicketFormate += "</tbody>"
                    TicketFormate += "</table>"
                    TicketFormate += "</td>"
                    TicketFormate += "</tr>"
                    TicketFormate += "</tbody>"
                    TicketFormate += "</table>"
                    'Remark--Commented line
                    Dim ticketcopymarkup As Decimal = 0
                    Dim ticketcopymarkTC As Decimal = 0

                    If (fltFare.Rows.Count > 0) Then

                        If (Convert.ToString(fltFare.Rows(0)("ticketcopymarkupforTax")) <> "0") Then
                            ticketcopymarkup = Convert.ToDecimal(fltFare.Rows(0)("ticketcopymarkupforTax"))
                        End If
                        If (Convert.ToString(fltFare.Rows(0)("ticketcopymarkupforTC")) <> "0") Then
                            ticketcopymarkTC = Convert.ToDecimal(fltFare.Rows(0)("ticketcopymarkupforTC"))
                        End If
                    End If
                    Dim numADT As Integer = FltPaxList.AsEnumerable().Where(Function(x) x("PaxType").ToString() = "ADT").ToList().Count
                    Dim numCHD As Integer = FltPaxList.AsEnumerable().Where(Function(x) x("PaxType").ToString() = "CHD").ToList().Count
                    Dim tpaxcount As Integer = numADT + numCHD
                    ticketcopymarkup = ticketcopymarkup * tpaxcount
                    TBasefare = ABasefare + CBasefare + IBasefare
                    TTransfee = ATransfee + CTransfee + ITransfee
                    TTranscharge = ATranscharge + CTranscharge + ITranscharge + ticketcopymarkTC
                    Tfuel = Afuel + Cfuel + Ifuel
                    'Ttax = Atax + Ctax + Itax + TTransfee + TTranscharge + ticketcopymarkup
                    Ttax = Atax + Ctax + Itax + TTransfee + ticketcopymarkup
                    Tgst = Agst + Cgst + Igst

                    fare = AdtTtlFare + ChdTtlFare + INFTtlFare
                    basetaxfarenrm.Value = Atax + Ctax + Itax + TTransfee + TTranscharge
                Else
                    TicketFormate += "<tr>"
                    TicketFormate += "<td colspan='2' style='width:100%;'>"
                    TicketFormate += "<table style='width:100%;'>"


                    TicketFormate += "<tr>"
                    TicketFormate += "<td style='font-size: 10px; width: 50%; text-align: left; vertical-align: top; display:none;'id='td_perpaxtype'>" + FltPaxList.Rows(0)("PaxType") + "</td>"
                    TicketFormate += "<td style='font-size: 10px; width: 50%; text-align: left; vertical-align: top;'>Base Fare</td>"
                    TicketFormate += "<td style='font-size: 10px; width: 50%; text-align: left; vertical-align: top;'>"
                    TicketFormate += fltFare.Rows(0)("BaseFare").ToString
                    TicketFormate += "</td>"
                    TicketFormate += "</tr>"
                    TicketFormate += "<tr>"
                    TicketFormate += "<td style='font-size: 10px; width: 50%; text-align: left; vertical-align: top;'>Fuel Surcharge</td>"
                    TicketFormate += "<td style='font-size: 10px; width: 50%; text-align: left; vertical-align: top;'>"
                    TicketFormate += fltFare.Rows(0)("Fuel").ToString
                    TicketFormate += "</td>"
                    TicketFormate += "</tr>"
                    TicketFormate += "<tr>"
                    TicketFormate += "<td style='font-size: 10px; width: 50%; text-align: left; vertical-align: top;'>Tax</td>"
                    TicketFormate += "<td style='font-size: 10px; width: 50%; text-align: left; vertical-align: top;' id='td_perpaxtax'>"
                    TicketFormate += fltFare.Rows(0)("Tax").ToString
                    TicketFormate += "</td>"
                    TicketFormate += "</tr>"
                    TicketFormate += "<tr>"
                    TicketFormate += "<td style='font-size: 10px; width: 50%; text-align: left; vertical-align: top;'>STax</td>"
                    TicketFormate += "<td style='font-size: 10px; width: 50%; text-align: left; vertical-align: top;'>"
                    TicketFormate += fltFare.Rows(0)("ServiceTax").ToString
                    TicketFormate += "</td>"
                    TicketFormate += "</tr>"
                    TicketFormate += "<tr >"
                    TicketFormate += "<td style='font-size: 10px; width: 50%; text-align: left; vertical-align: top;'>Trans Fee</td>"
                    TicketFormate += "<td style='font-size: 10px; width: 50%; text-align: left; vertical-align: top;'>"
                    TicketFormate += fltFare.Rows(0)("TFee").ToString
                    TicketFormate += "</td>"
                    TicketFormate += "</tr>"
                    TicketFormate += "<tr>"
                    TicketFormate += "<td style='font-size: 10px; width: 50%; text-align: left; vertical-align: top;'>Trans Charge</td>"
                    TicketFormate += "<td style='font-size: 10px; width: 50%; text-align: left; vertical-align: top;'id='td_perpaxtc'>"
                    TicketFormate += fltFare.Rows(0)("TCharge").ToString
                    TicketFormate += "</td>"
                    TicketFormate += "</tr>"
                    TicketFormate += "<tr>"

                    Dim ResuCharge As Decimal = 0
                    Dim ResuServiseCharge As Decimal = 0
                    Dim ResuFareDiff As Decimal = 0
                    If Convert.ToString(FltHeaderList.Rows(0)("ResuCharge")) IsNot Nothing AndAlso Convert.ToString(FltHeaderList.Rows(0)("ResuCharge")) <> "" Then
                        TicketFormate += "<tr>"
                        TicketFormate += "<td style='font-size: 10px; width: 50%; text-align: left; vertical-align: top;'>Reissue Charge</td>"
                        TicketFormate += "<td style='font-size: 10px; width: 50%; text-align: left; vertical-align: top;'>"
                        TicketFormate += FltHeaderList.Rows(0)("ResuCharge").ToString
                        ResuCharge = (Convert.ToDecimal(FltHeaderList.Rows(0)("ResuCharge"))).ToString
                        TicketFormate += "</td>"
                        TicketFormate += "</tr>"
                    End If
                    If Convert.ToString(FltHeaderList.Rows(0)("ResuServiseCharge")) IsNot Nothing AndAlso Convert.ToString(FltHeaderList.Rows(0)("ResuServiseCharge")) <> "" Then
                        TicketFormate += "<tr>"
                        TicketFormate += "<td style='font-size: 10px; width: 50%; text-align: left; vertical-align: top;'>Reissue Srv. Charge</td>"
                        TicketFormate += "<td style='font-size: 10px; width: 50%; text-align: left; vertical-align: top;'>"
                        TicketFormate += FltHeaderList.Rows(0)("ResuServiseCharge").ToString
                        ResuServiseCharge = (Convert.ToDecimal(FltHeaderList.Rows(0)("ResuServiseCharge"))).ToString
                        TicketFormate += "</td>"
                        TicketFormate += "</tr>"
                    End If
                    If Convert.ToString(FltHeaderList.Rows(0)("ResuFareDiff")) IsNot Nothing AndAlso Convert.ToString(FltHeaderList.Rows(0)("ResuFareDiff")) <> "" Then
                        TicketFormate += "<tr>"
                        TicketFormate += "<td style='font-size: 10px; width: 50%; text-align: left; vertical-align: top;'>Reissue Fare Diff</td>"
                        TicketFormate += "<td style='font-size: 10px; width: 50%; text-align: left; vertical-align: top;'>"
                        TicketFormate += FltHeaderList.Rows(0)("ResuFareDiff").ToString
                        ResuFareDiff = (Convert.ToDecimal(FltHeaderList.Rows(0)("ResuFareDiff"))).ToString
                        TicketFormate += "</td>"
                        TicketFormate += "</tr>"
                    End If
                    TicketFormate += "<td style='font-size: 11px; width: 50%; text-align: left; vertical-align: top;'>TOTAL</td>"
                    TicketFormate += "<td style='font-size: 11px; width: 50%; text-align: left; vertical-align: top;' id='td_totalfare'>"
                    fare = (Convert.ToDecimal(fltFare.Rows(0)("BaseFare")) + Convert.ToDecimal(fltFare.Rows(0)("Fuel")) + Convert.ToDecimal(fltFare.Rows(0)("Tax")) + Convert.ToDecimal(fltFare.Rows(0)("ServiceTax")) + Convert.ToDecimal(fltFare.Rows(0)("TCharge")) + Convert.ToDecimal(fltFare.Rows(0)("TFee")) + ResuCharge + ResuServiseCharge + ResuFareDiff).ToString
                    TicketFormate += fare.ToString
                    TicketFormate += "</td>"

                    'fare = Convert.ToDecimal(fltFare.Rows[0]["Total"]) + ResuCharge + ResuServiseCharge + ResuFareDiff;
                    TicketFormate += "</tr>"
                End If
                ''''''''''''''''''''End'''''''''''''''''''

                ''''''''''''''''''''Meal & Baggege'''''''''''
                If fltMealAndBag.Rows.Count > 0 Then
                    TicketFormate += "<tr>"
                    TicketFormate += "<td colspan='8' style= 'background-color: #0b2759;width:100%;'>"
                    TicketFormate += "<table style='width:100%;'>"
                    TicketFormate += "<tbody style='border: 1px solid #000;'>"
                    TicketFormate += "<tr>"
                    TicketFormate += "<td style='background-color: " + BackClor + "; color: #fff; font-size: 12px; font-weight: bold; padding: 5px;' colspan='9'>Meal & Baggage Information</td>"
                    TicketFormate += "</tr>"
                    TicketFormate += "<tr style='border: 1px solid #000;'>"
                    TicketFormate += "<th style='font-size: 12px; text-align: left; padding: 5px; border-right: 1px solid #000;'>Pax Name</th>"
                    TicketFormate += "<th style='font-size: 12px; text-align: left; padding: 5px; border-right: 1px solid #000;'>Trip Type</th>"
                    TicketFormate += "<th style='font-size: 12px; text-align: left; padding: 5px; border-right: 1px solid #000;'>Meal Code</th>"
                    TicketFormate += "<th style='font-size: 12px; text-align: left; padding: 5px; border-right: 1px solid #000;'>Meal Price</th>"
                    TicketFormate += "<th style='font-size: 12px; text-align: left; padding: 5px; border-right: 1px solid #000;'>Baggage Code</th>"
                    TicketFormate += "<th style='font-size: 12px; text-align: left; padding: 5px; border-right: 1px solid #000;'>Baggage Price</th>"
                    TicketFormate += "<th style='font-size: 12px; text-align: left; padding: 5px; border-right: 1px solid #000;'>Total</th>"
                    TicketFormate += "</tr>"



                    For i As Integer = 0 To fltMealAndBag.Rows.Count - 1
                        'If Convert.ToString(fltMealAndBag.Rows(i)("MealPrice")) <> "0.00" AndAlso Convert.ToString(fltMealAndBag.Rows(i)("BaggagePrice")) <> "0.00" Then
                        TicketFormate += "<tr>"
                        TicketFormate += "<td style='font-size: 11px; width: 10%; text-align: left; vertical-align: top;'>"
                        TicketFormate += Convert.ToString(fltMealAndBag.Rows(i)("Name"))
                        TicketFormate += "</td>"
                        TicketFormate += "<td style='font-size: 11px; width: 10%; text-align: left; vertical-align: top;'>"
                        TicketFormate += Convert.ToString(fltMealAndBag.Rows(i)("TripType"))
                        TicketFormate += "</td>"
                        TicketFormate += "<td style='font-size: 11px; width: 10%; text-align: left; vertical-align: top;'>"
                        TicketFormate += Convert.ToString(fltMealAndBag.Rows(i)("MealCode"))
                        TicketFormate += "</td>"
                        TicketFormate += "<td style='font-size: 11px; width: 10%; text-align: left; vertical-align: top;'>"
                        TicketFormate += Convert.ToString(fltMealAndBag.Rows(i)("MealPrice"))
                        TicketFormate += "</td>"
                        TicketFormate += "<td style='font-size: 11px; width: 10%; text-align: left; vertical-align: top;'>"
                        TicketFormate += Convert.ToString(fltMealAndBag.Rows(i)("BaggageCode"))
                        TicketFormate += "</td>"
                        TicketFormate += "<td style='font-size: 11px; width: 10%; text-align: left; vertical-align: top;'>"
                        TicketFormate += Convert.ToString(fltMealAndBag.Rows(i)("BaggagePrice"))
                        TicketFormate += "</td>"
                        TicketFormate += "<td style='font-size: 11px; width: 10%; text-align: left; vertical-align: top;'>"
                        MealBagTotalPrice += Convert.ToDecimal(fltMealAndBag.Rows(i)("TotalPrice"))
                        TicketFormate += Convert.ToString(fltMealAndBag.Rows(i)("TotalPrice"))
                        TicketFormate += "</td>"

                        TicketFormate += "</tr>"
                        'End If
                    Next
                    TicketFormate += "</tbody>"
                    TicketFormate += "</table>"
                    TicketFormate += "</td>"
                    TicketFormate += "</tr>"
                    fare = AdtTtlFare + ChdTtlFare + INFTtlFare + MealBagTotalPrice
                End If

                '''''''''''''''''''''End'''''''''''''''''''''
                '''''''''''''''''''Seat Details''''''''''''''
                Dim seatdetails As String = ""
                Dim seatFareO As Integer = 0
                'If SeatListO.Count > 0 Then
                '    TicketFormate += "<tr>"
                '    TicketFormate += "<td colspan='8' style= 'background-color: #0b2759;width:100%;'>"
                '    TicketFormate += "<table style='width:100%;'>"
                '    TicketFormate += "<tbody style='border: 1px solid #000;'>"
                '    TicketFormate += "<tr>"
                '    TicketFormate += "<td style='background-color: " + BackClor + "; color: #fff; font-size: 12px; font-weight: bold; padding: 5px;' colspan='9'>Traveller Seat Information:</td>"
                '    TicketFormate += "</tr>"

                '    TicketFormate += "<tr style='border: 1px solid #000;'>"
                '    TicketFormate += "<th style='font-size: 12px; text-align: left; padding: 5px; border-right: 1px solid #000;'>Traveller</th>"
                '    TicketFormate += "<th style='font-size: 12px; text-align: left; padding: 5px; border-right: 1px solid #000;'>Sector</th>"
                '    TicketFormate += "<th style='font-size: 12px; text-align: left; padding: 5px; border-right: 1px solid #000;'>Seat</th>"
                '    TicketFormate += "<th style='font-size: 12px; text-align: left; padding: 5px; border-right: 1px solid #000;'>Type</th>"
                '    TicketFormate += "<th style='font-size: 12px; text-align: left; padding: 5px; border-right: 1px solid #000;'>Amount</th>"
                '    TicketFormate += "</tr>"

                '    For i As Integer = 0 To SeatListO.Count - 1
                '        Dim dts As DataTable = New DataTable()
                '        dts = SelectPaxDetail(OrderId, SeatListO(i).PaxId)
                '        TicketFormate += "<tr>"
                '        TicketFormate &= "<td style='font-size: 11px; width: 10%; text-align: left; vertical-align: top;'>" & dts.Rows(0)("Name") & "</td>"
                '        TicketFormate &= "<td style='font-size: 11px; width: 10%; text-align: left; vertical-align: top;'>" & SeatListO(i).Origin & "-" & SeatListO(i).Destination & "</div>"
                '        TicketFormate &= "<td style='font-size: 11px; width: 10%; text-align: left; vertical-align: top;'>" & SeatListO(i).SeatDesignator & "</td>"
                '        TicketFormate &= "<td style='font-size: 11px; width: 10%; text-align: left; vertical-align: top;'>" & SeatListO(i).SeatAlignment & "</td>"
                '        TicketFormate &= "<td style='font-size: 11px; width: 10%; text-align: left; vertical-align: top;'>" & SeatListO(i).Amount & "</td>"
                '        seatFareO = seatFareO + Convert.ToInt32(SeatListO(i).Amount)
                '        TicketFormate += "</tr>"
                '    Next
                '    fare = AdtTtlFare + ChdTtlFare + INFTtlFare + MealBagTotalPrice + seatFareO
                '    TicketFormate += "</tbody>"
                '    TicketFormate += "</table>"
                '    TicketFormate += "</td>"
                '    TicketFormate += "</tr>"
                'End If

                '''''''''''''''''''''End''''''''''''''''''''''
                '''''''''''''''Fare Information'''''''''''''''''''
                TicketFormate += "<table id='disfareinfoheader'  style='border: 1px solid #0b2759; font-size: 12px; padding: 0px !important; width: 100%;' >"
                TicketFormate += "<tbody>"
                TicketFormate += "<tr id='TR_FareInformation1'>"
                TicketFormate += "<td class='pri' colspan='9' style='background-color: " + BackClor + "; color: #fff; font-size: 12px; font-weight: bold; padding: 5px;'>Redemption Details</td>"
                TicketFormate += "</tr>"
                TicketFormate += "</tbody>"
                TicketFormate += "</table>"
                TicketFormate += "<table id='disfareinfo' style='border: 1px solid #0b2759; font-size: 12px; padding: 0px !important; width: 100%;'>"
                TicketFormate += "<tbody>"
                TicketFormate += "<tr>"
                TicketFormate += "<td colspan='9' style='font-size: 12px; padding: 5px; width: 30%'>"
                TicketFormate += "<table style='font-size: 12px; padding: 5px; width: 100%; border: 1px solid #000'>"
                TicketFormate += "<tbody style='border: 1px solid #000;'>"
                TicketFormate += "<tr>"
                TicketFormate += "<th style='font-size: 12px; color: #424242; text-align: left; padding: 5px; font-weight: bold;'>Base fare</th>"
                TicketFormate += "<td style='font-size: 12px; text-align: left; vertical-align: top;'>" + TBasefare.ToString() + "</td>"
                TicketFormate += "</tr>"
                TicketFormate += "<tr>"
                TicketFormate += "<th class='pri2' style='font-size: 12px; color: #424242; text-align: left; padding: 5px; font-weight: bold;'>Fuel Surcharge</th>"
                TicketFormate += "<td class='pri2' style='font-size: 12px; text-align: left; vertical-align: top;'>" + Tfuel.ToString() + "</td>" ''ttt
                TicketFormate += "</tr>"
                TicketFormate += "<tr>"
                TicketFormate += "<th style='font-size: 12px; color: #424242; text-align: left; padding: 5px; font-weight: bold;'>Tax</th>"
                TicketFormate += "<td style='font-size: 12px; text-align: left; vertical-align: top;' id='td_taxadt' class='taxclass'>" + Ttax.ToString() + "</td>" ''ttt
                TicketFormate += "</tr>"
                TicketFormate += "<tr>"
                TicketFormate += "<th class='pri2' style='font-size: 12px; color: #424242; text-align: left; padding: 5px; font-weight: bold;'>Supp.SGST</th>"
                TicketFormate += "<td class='pri2' style='font-size: 12px; text-align: left; vertical-align: top;'>" + Tgst.ToString() + "</td>" ''ttt
                TicketFormate += "</tr>"
                TicketFormate += "<tr>"
                TicketFormate += "<th style='font-size: 12px; color: #424242; text-align: left; padding: 5px; font-weight: bold;'>Supp.CGST</th>"
                TicketFormate += "<td style='font-size: 12px; text-align: left; vertical-align: top;'>0</td>" ''ttt
                TicketFormate += "</tr>"
                'TicketFormate += "<tr style='display:none' id='trtransfee'>"
                'TicketFormate += "<th class='pri2' style='font-size: 12px; color: #424242; text-align: left; padding: 5px; font-weight: bold;'>Trans Fee</th>"
                'TicketFormate += "<td class='pri2' style='font-size: 12px; text-align: left; vertical-align: top;' id='lbltransfee'>" + TTransfee.ToString() + "</td>" ''ttt
                'TicketFormate += "</tr>"
                'TicketFormate += "<tr style='display:none'>"
                'TicketFormate += "<th style='font-size: 12px; color: #424242; text-align: left; padding: 5px; font-weight: bold;'>Trans Charge</th>"
                'TicketFormate += "<td style='font-size: 12px; text-align: left; vertical-align: top;' id='td_allcharge'>" + TTranscharge.ToString() + "</td>" ''ttt
                'TicketFormate += "</tr>"
                TicketFormate += "<tr " + If(TTranscharge = 0, "style='display:none'", "") + ">"
                TicketFormate += "<th style='font-size: 12px; color: #424242; text-align: left; padding: 5px; font-weight: bold;'>Trans Charge</th>"
                TicketFormate += "<td style='font-size: 12px; text-align: left; vertical-align: top;' id='td_allcharge'>" + TTranscharge.ToString() + "</td>" ''ttt
                TicketFormate += "</tr>"
                TicketFormate += "<tr style='border: 1px solid #000;'>"
                TicketFormate += "<th class='pri2' style='font-size: 12px; color: #424242; text-align: left; padding: 5px; font-weight: bold;'>Grand Total</th>"
                TicketFormate += "<td class='pri2' style='font-size: 12px; text-align: left; vertical-align: top;' id='td_grandtot'>" + fare.ToString() + "</td>" ''ttt
                TicketFormate += "</tr>"
                TicketFormate += "</tbody>"
                TicketFormate += "</table>"
                TicketFormate += "</td>"
                TicketFormate += "<td colspan='4' style='font-size: 12px; padding: 5px; width: 100%'>"
                TicketFormate += " <table style='width:50%;margin-top: -82px;float: right;border: 1px solid #000;'>"
                TicketFormate += " <tbody style='border: 1px solid #000;'>"
                TicketFormate += " <tr style='border: 1px solid #000;'><td style='font-weight: bold;font-size: 12px;'>Passenger Information:</td><td style=''></td> </tr>"
                TicketFormate += " <tr><td class='pri2' style='font-weight: bold;font-size: 12px;'>Email:</td><td class='pri2' style='font-weight: bold;font-size: 12px;'>" + FltHeaderList.Rows(0)("PgEmail") + "</td></tr>"
                TicketFormate += " <tr><td style='font-weight: bold;font-size: 12px;'>Contact No:</td><td style='font-weight: bold;font-size: 12px;'>" + FltHeaderList.Rows(0)("PgMobile") + "</td></tr>"

                TicketFormate += " </tbody>"
                TicketFormate += " </table>"
                TicketFormate += "</td>"
                TicketFormate += "</tr >"
                TicketFormate += " </tbody>"
                TicketFormate += " </table>"


                'TicketFormate += "<table id='hdpaxinfo' style='display:none;border: 1px solid #0b2759; font-size: 12px; padding: 0px !important; width: 100%;' >"
                'TicketFormate += "<tbody>"
                'TicketFormate += "<tr>"
                'TicketFormate += "<td style='font-size: 12px; padding: 5px; width: 100%'>"
                'TicketFormate += " <table style='float:right'>"
                'TicketFormate += " <tbody style='border: 1px solid #000;'>"
                'TicketFormate += " <tr style='border: 1px solid #000;'><td style='font-weight: bold;font-size: 12px;'>Passenger Information:</td><td style=''></td> </tr>"
                'TicketFormate += " <tr><td style='font-weight: bold;font-size: 12px;'>Email:</td><td style='font-weight: bold;font-size: 12px;'>" + FltHeaderList.Rows(0)("PgEmail") + "</td></tr>"
                'TicketFormate += " <tr><td style='font-weight: bold;font-size: 12px;'>Contact No:</td><td style='font-weight: bold;font-size: 12px;'>" + FltHeaderList.Rows(0)("PgMobile") + "</td></tr>"
                'TicketFormate += " </tbody>"
                'TicketFormate += " </table>"
                'TicketFormate += "</td>"
                'TicketFormate += "</tr>"
                'TicketFormate += " </tbody>"
                'TicketFormate += " </table>"
                ''''''''''''''''''''''''''''End'''''''''''''''''''''''''''''''

                '''''''''''''''''''''''''Terms & Conditions'''''''''''''''''''''''
                TicketFormate += "<table style='border: 1px solid #0b2759; font-size: 12px; padding: 0px !important; width: 100%;'>"
                TicketFormate += "<tbody>"
                TicketFormate += "<tr>"
                TicketFormate += "<td colspan='8'>"
                TicketFormate += "<ul style='list-style-image: url(https://RWT.co/Images/bullet.png);'>"
                'TicketFormate += "<li style='font-size: 10.5px;'>Kindly confirm the status of your PNR within 24 hrs of booking, as at times the same may fail on account of payment failure, internet connectivity, booking engine or due to any other reason beyond our control.For Customers who book their flights well in advance of the scheduled departure date it is necessary that you re-confirm the departure time of your flight between 72 and 24 hours before the Scheduled Departure Time.</li>"
                TicketFormate += "</ul>"
                TicketFormate += "</td>"
                TicketFormate += "</tr>"
                TicketFormate += "<tr>"
                TicketFormate += "<td class='pri' colspan='8' style='background-color: " + BackClor + "; color: #fff; font-size: 11px; font-weight: bold; padding: 5px;'>IMPORTANT INFORMATION :</td>"
                TicketFormate += "</tr>"
                TicketFormate += "<tr>"
                TicketFormate += "<td colspan='8'>"
                TicketFormate += "<ul style='list-style-image: url(https://RWT.co/Images/bullet.png);'>"
                TicketFormate += "<li style='font-size: 10.5px;margin-left: 15px !important;list-style: disc !important;'>For any queries or communication with tripforo regarding this booking, please use the Booking ID as a reference.</li>"
                TicketFormate += "<li style='font-size: 10.5px;margin-left: 15px !important;list-style: disc !important;'>Please note that for all domestic flights, check-in counters close 60 minutes prior to flight departure.</li>"
                TicketFormate += "<li style='font-size: 10.5px;margin-left: 15px !important;list-style: disc !important;'>It is mandatory for the passenger to carry a valid photo ID proof in order to enter the airport and show at the time of check-in. Permissible ID proofs include - Aadhaar Card, Passport or any other government recognized ID proof. For infant travellers (0-2 yrs), it is mandatory to carry the birth certificate as a proof</li>"
                TicketFormate += "<li style='font-size: 10.5px;margin-left: 15px !important;list-style: disc !important;'>Kindly carry a copy of your e-ticket on a tablet/ mobile/ laptop or a printed copy of the ticket to enter the airport and show at the time of check-in.</li>"
                'TicketFormate += "<li style='font-size: 10.5px;'>Flight schedules are subject to change and approval by authorities.<br>"
                'TicketFormate += "<li style='font-size: 10.5px;'>Name Changes on a confirmed booking are strictly prohibited. Please ensure that the name given at the time of booking matches as mentioned on the traveling Guests valid photo ID Proof.<br>"
                'TicketFormate += "Travel Agent does not provide compensation for travel on other airlines, meals, lodging or ground transportation.</li>"
                'TicketFormate += "<li style='font-size: 10.5px;'>Bookings made under the Armed Forces quota are non cancelable and non- changeable.</li>"
                'TicketFormate += "<li style='font-size: 10.5px;'>Guests are advised to check their all flight details (including their Name, Flight numbers, Date of Departure, Sectors) before leaving the Agent Counter.</li>"
                'TicketFormate += "<li style='font-size: 10.5px;'>Cancellation amount will be charged as per airline rule.</li>"
                'TicketFormate += "<li style='font-size: 10.5px;'>Guests requiring wheelchair assistance, stretcher, Guests traveling with infants and unaccompanied minors need to be booked in advance since the inventory for these special service requests are limited per flight.</li>"
                TicketFormate += "</ul>"
                TicketFormate += " </td>"
                TicketFormate += "</tr>"
                TicketFormate += "</tbody>"
                TicketFormate += "</table>"
                TicketFormate += "</div>"



                'TicketFormate += " <script type='text/javascript'>"
                'TicketFormate += " decodeURI(window.location.search).substring(1).split('&');"
                'TicketFormate += "  var btype = 'code128';"
                'TicketFormate += "  var renderer = 'css';"
                'TicketFormate += "  var quietZone = false;"
                'TicketFormate += " if ($('#quietzone').is(':checked') || $('#quietzone').attr('checked'))"
                'TicketFormate += "  { quietZone = true; }"
                'TicketFormate += "  var settings = { output: renderer, bgColor: '#FFFFFF', color: '#000000', barWidth: '1', barHeight: '110', moduleSize: '5', posX: '10', posY: '20', addQuietZone: '1' };"
                'TicketFormate += "   $('#canvasTarget').hide();"
                'TicketFormate += "  $('#barcodeTarget').html('').show().barcode('" + OrderId + "', btype, settings);"
                'TicketFormate += "  </script>"

            End If

            Return TicketFormate
        Catch ex As Exception
            clsErrorLog.LogInfo(ex)
        End Try

        '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''change
    End Function

    Public Function GetCabin(ByVal Provider As String, ByVal cabin As String, ByVal VC As String) As String
        Dim cabininfo As String = ""
        Try


            If Provider = "TB" And VC = "G8" Then

            ElseIf Provider = "TB" Then

                cabininfo = "Economy"


            Else

                If cabin.ToUpper().Trim() = "Y" Then

                    cabininfo = "Economy"
                ElseIf cabin.ToUpper().Trim() = "C" OrElse cabin.ToUpper().Trim() = "Z" Then
                    cabininfo = "Business"
                ElseIf cabin.ToUpper().Trim() = "F" Then
                    cabininfo = "First"
                ElseIf cabin.ToUpper().Trim() = "W" Then
                    cabininfo = "Premium Economy"

                Else

                    cabininfo = cabin

                End If



            End If

        Catch ex As Exception

        End Try
        Return cabininfo
    End Function

    Public Function GetBagInfo(ByVal Provider As String, ByVal Remark As String) As String

        Dim baginfo As String = ""
        If Provider = "TB" Then

            If Remark.Contains("Hand") Then
                baginfo = Remark

            End If
        ElseIf Provider = "YA" Then

            If Remark.Contains("Hand") Then
                baginfo = Remark

            ElseIf Not String.IsNullOrEmpty(Remark) Then

                baginfo = Remark & " Baggage allowance"

            End If


        ElseIf Provider = "1G" Then

            If Remark.Contains("PC") Then

                baginfo = Remark.Replace("PC", " Piece(s) Baggage allowance")
            ElseIf Remark.Contains("K") Then

                baginfo = Remark.Replace("K", " Kg Baggage allowance")

            End If



        End If
        Return baginfo

    End Function

    Public Function AgentIDInfo(ByVal AgentID As String) As DataTable

        Dim con As SqlConnection
        Dim adap As SqlDataAdapter
        ' Dim ds As DataSet
        con = New SqlConnection(ConfigurationManager.ConnectionStrings("myAmdDB").ConnectionString)
        Dim dt As New DataTable()
        adap = New SqlDataAdapter("select * from agent_register where user_id='" & AgentID & "'", con)
        adap.Fill(dt)
        Return dt
    End Function

    Protected Sub btn_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btn.Click
        Try
            Try

                Dim OrderId As String = Request.QueryString("OrderId")
                Dim dtaid As New DataTable()
                dtaid = ObjIntDetails.EmailID(OrderId)
                agent_id = dtaid.Rows(0)("AgentId").ToString()
                Dim dtemail As New DataTable()
                dtemail = AgentIDInfo(agent_id)
                email_id = dtemail.Rows(0)("Email").ToString()
            Catch ex As Exception
                email_id = "info@RWT.com"
            End Try

            Try

                FltHeaderList = objTktCopy.SelectHeaderDetail(Request.QueryString("OrderId"))
                'Dim sb As New StringBuilder()
                'Dim s1 As String
                'divprint.RenderControl(New HtmlTextWriter(New StringWriter(sb)))
                's1 = sb.ToString()
                ''s1 = divprint.InnerHtml.ToString()
                Dim s As String = ""
                'Dim str As String = 
                Dim sw As New StringWriter()
                Dim w As New HtmlTextWriter(sw)
                LabelTkt.RenderControl(w)

                If (Request("Hidden1") <> "") Then
                    s = Hidden1.Value().Trim()
                    s = s.Replace("<img src=""https://RWT.co/images/logo.png"" alt=""Logo"" style=""height:54px; width:104px"">", "<img src=""https://RWT.co/images/logo.png"" alt=""Logo"" style=""height:54px; width:104px""/>").Replace("<img alt=""Logo Not Found"" src=""https://RWT.co/AirLogo/sm" & FltHeaderList.Rows(0)("VC") & ".gif"">", "<img alt=""Logo Not Found"" src=""https://RWT.co/AirLogo/sm" & FltHeaderList.Rows(0)("VC") & ".gif""/>").Replace("<span id=""LabelTkt"">", "").Replace("<title>ticket details</title>", "").Replace("</span>", "").Replace("<br>", "<br/>")
                Else
                    s = sw.GetStringBuilder().ToString()
                    s = s.Replace("<img src=""https://RWT.co/images/logo.png"" alt=""Logo"" style=""height:54px; width:104px"">", "<img src=""https://RWT.co/images/logo.png"" alt=""Logo"" style=""height:54px; width:104px""/>").Replace("<img alt=""Logo Not Found"" src=""https://RWT.co/AirLogo/sm" & FltHeaderList.Rows(0)("VC") & ".gif"">", "<img alt=""Logo Not Found"" src=""https://RWT.co/AirLogo/sm" & FltHeaderList.Rows(0)("VC") & ".gif""/>").Replace("<span id=""LabelTkt"">", "").Replace("<title>ticket details</title>", "").Replace("</span>", "").Replace("<br>", "<br/>")
                End If
                '''' Ticketcopy Convert into PDF 
                Try
                    Dim Body As String = ""
                    Dim strFileNmPdf As String = ""
                    Dim TicketFormate As String
                    Dim strMailMsg As String
                    Dim writePDF As Boolean = False
                    Dim status1 As Integer = 0
                    Try
                        TicketFormate = s.Trim.ToString
                        strFileNmPdf = ConfigurationManager.AppSettings("HTMLtoPDF").ToString().Trim() + Request.QueryString("OrderId").ToString + "-" + DateTime.Now.ToString().Replace(":", "").Replace("/", "-").Replace(" ", "-").Trim() + ".pdf"
                        Dim pdfDoc As New iTextSharp.text.Document(PageSize.A4, 2, 2, 0, 0)
                        Dim writer As PdfWriter = PdfWriter.GetInstance(pdfDoc, New FileStream(strFileNmPdf, FileMode.Create, FileAccess.ReadWrite, FileShare.None))
                        pdfDoc.Open()
                        Dim sr As New StringReader(TicketFormate.ToString)
                        iTextSharp.tool.xml.XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, sr)
                        pdfDoc.Close()
                        writer.Dispose()
                        sr.Dispose()
                        pdfDoc.Dispose()
                        writePDF = True

                    Catch ex As Exception

                        Dim stt As String = ex.Message
                    End Try
                    strMailMsg = "<p style='font-family:verdana; font-size:12px'>Dear Customer<br /><br />"
                    strMailMsg = strMailMsg & "Greetings of the day !!!!<br /><br />"
                    strMailMsg = strMailMsg & "Please find an attachment for your E-ticket, kindly carry the print out of the same for hassle-free travel. Your onward booking for is confirmed on for .<br /><br />"
                    strMailMsg = strMailMsg & "Have a nice &amp; wonderful trip.<br /><br />"

                    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

                    '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                    Dim STDOM As New SqlTransactionDom
                    Dim MailDt As New DataTable
                    MailDt = STDOM.GetMailingDetails(MAILING.AIR_PNRSUMMARY.ToString(), "").Tables(0)

                    Dim email As String = Request("txt_email")

                    If (MailDt.Rows.Count > 0) Then
                        Dim Status As Boolean = False
                        Status = Convert.ToBoolean(MailDt.Rows(0)("Status").ToString())
                        Try
                            If Status = True Then
                                Dim i As Integer = STDOM.SendMail(email, Convert.ToString(MailDt.Rows(0)("MAILFROM")), email_id, MailDt.Rows(0)("CC").ToString(), MailDt.Rows(0)("SMTPCLIENT").ToString(), MailDt.Rows(0)("UserId").ToString(), MailDt.Rows(0)("Pass").ToString(), s.Trim.ToString, MailDt.Rows(0)("SUBJECT").ToString(), strFileNmPdf)
                                If i = 1 Then
                                    ShowAlertMessage("Mail sent successfully.")
                                    'mailmsg.Text = "Mail sent successfully."
                                Else
                                    ShowAlertMessage("Unable to send mail.Please try again")
                                    'mailmsg.Text = "Unable to send mail.Please try again"
                                End If
                            End If


                            txt_email.Text = ""
                            ddl_srvtype.SelectedIndex = 0

                        Catch ex As Exception
                            Response.Write(ex.Message)
                            LogInfo(ex)
                            mailmsg.Text = ex.Message.ToString
                        End Try
                    Else
                        mailmsg.Text = "Unable to send mail.Please contact to administrator"
                    End If

                Catch ex As Exception
                    Response.Write(ex.Message)
                    LogInfo(ex)
                End Try


            Catch ex As Exception
                Response.Write(ex.Message)
                LogInfo(ex)
            End Try

        Catch ex As Exception
            Response.Write(ex.Message)
            LogInfo(ex)

        End Try
    End Sub

    Public Shared Function LogInfo(ByVal ex As Exception) As Integer
        Dim con As SqlConnection
        Dim cmd As SqlCommand
        Dim Temp As Integer = 0
        Try
            con = New SqlConnection(ConfigurationManager.ConnectionStrings("myAmdDB").ConnectionString)
            Dim trace As New System.Diagnostics.StackTrace(ex, True)
            Dim linenumber As Integer = trace.GetFrame((trace.FrameCount - 1)).GetFileLineNumber()
            Dim ErrorMsg As String = ex.Message
            Dim fileNames As String = trace.GetFrame((trace.FrameCount - 1)).GetFileName()
            con.Open()
            cmd = New SqlCommand("InsertErrorLog", con)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@PageName", fileNames)
            cmd.Parameters.AddWithValue("@ErrorMessage", ErrorMsg)
            cmd.Parameters.AddWithValue("@LineNumber", linenumber)
            Temp = cmd.ExecuteNonQuery()
        Catch ex1 As Exception
        Finally
            con.Close()
        End Try
        Return 0
    End Function

    Protected Sub btn_exporttoecel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_exporttoecel.Click
        Try
            Dim sexcel As String = ""

            Dim filename As String = ""
            filename = "TicketReport.xls"
            Response.Clear()
            Response.AddHeader("content-disposition", "attachment;filename=" & filename & "")

            Response.Charset = ""
            Response.ContentType = "application/vnd.xls"
            Dim stringWrite As New System.IO.StringWriter()
            Dim htmlWrite As System.Web.UI.HtmlTextWriter = New HtmlTextWriter(stringWrite)
            LabelTkt.RenderControl(htmlWrite)
            If (Request("Hidden1") <> "") Then
                sexcel = Request("Hidden1")
            Else
                sexcel = stringWrite.GetStringBuilder().ToString()
            End If
            Response.Write(sexcel)
            Response.[End]()
        Catch ex As Exception
            clsErrorLog.LogInfo(ex)
        End Try
    End Sub

    Protected Sub btn_exporttoword_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_exporttoword.Click
        Try
            div1.Visible = False
            div2.Visible = False
            Dim sword As String = ""
            Dim filename As String = ""
            filename = "TicketReport.doc"
            Response.Clear()
            Response.AddHeader("content-disposition", "attachment;filename=" & filename & "")
            Response.Charset = ""
            Response.ContentType = "application/doc"
            Dim stringWrite As New System.IO.StringWriter()
            Dim htmlWrite As System.Web.UI.HtmlTextWriter = New HtmlTextWriter(stringWrite)
            LabelTkt.RenderControl(htmlWrite)
            If (Request("Hidden1") <> "") Then
                sword = Request("Hidden1")
            Else
                sword = stringWrite.GetStringBuilder().ToString()
            End If

            Response.Write(sword)
            Response.[End]()
            div1.Visible = True
            div2.Visible = True
        Catch ex As Exception
            div1.Visible = True
            div2.Visible = True
            clsErrorLog.LogInfo(ex)
        End Try
    End Sub

    Public Function SelectPaxDetail(ByVal OrderId As String, ByVal TID As String) As DataTable
        Dim adap As New SqlDataAdapter()
        Dim con As New SqlConnection(ConfigurationManager.ConnectionStrings("myAmdDB").ConnectionString)
        If String.IsNullOrEmpty(TID) Then
            Dim dt As New DataTable()

            adap = New SqlDataAdapter("SELECT PaxId, OrderId, Title + '  ' + FName + '  ' + MName + '  ' + LName AS Name,ISNULL(MordifyStatus, 'Ticketed') as PaxStatus, PaxType, TicketNumber,DOB,FFNumber,FFAirline,MealType,SeatType FROM   FltPaxDetails WHERE OrderId = '" & OrderId & "' ", con)
            'adap = New SqlDataAdapter("SELECT PaxId, OrderId, Title + '  ' + FName + '  ' + MName + '  ' + LName AS Name, PaxType, TicketNumber,DOB FROM   FltPaxDetails WHERE OrderId = '" & OrderId & "' ", con)
            adap.Fill(dt)

            Return dt
        Else
            Dim dt As New DataTable()
            adap = New SqlDataAdapter("SELECT PaxId, OrderId, PaxId, Title + '  ' + FName + '  ' + MName + '  ' + LName AS Name, PaxType, TicketNumber,DOB,FFNumber,FFAirline,MealType,SeatType FROM   FltPaxDetails WHERE OrderId = '" & OrderId & "' and PaxId= '" & TID & "' ", con)
            'adap = New SqlDataAdapter("SELECT PaxId, OrderId, PaxId, Title + '  ' + FName + '  ' + MName + '  ' + LName AS Name, PaxType, TicketNumber,DOB FROM   FltPaxDetails WHERE OrderId = '" & OrderId & "' and PaxId= '" & TID & "' ", con)
            adap.Fill(dt)
            Return dt
        End If
    End Function

    Public Function TerminalDetails(ByVal OrderID As String, ByVal DepCity As String, ByVal ArrvlCity As String) As DataTable
        Dim con1 As New SqlConnection(ConfigurationManager.ConnectionStrings("myAmdDB").ConnectionString)
        Dim adap As New SqlDataAdapter("USP_TERMINAL_INFO", con1)
        adap.SelectCommand.CommandType = CommandType.StoredProcedure
        adap.SelectCommand.Parameters.AddWithValue("@DEPARTURECITY", DepCity)
        adap.SelectCommand.Parameters.AddWithValue("@ARRIVALCITY", ArrvlCity)
        adap.SelectCommand.Parameters.AddWithValue("@ORDERID", OrderID)
        Dim dt1 As New DataTable()
        con1.Open()
        adap.Fill(dt1)
        con1.Close()
        Return dt1
    End Function

    Public Shared Sub ShowAlertMessage(ByVal [error] As String)
        Try


            Dim page As Page = TryCast(HttpContext.Current.Handler, Page)
            If page IsNot Nothing Then
                [error] = [error].Replace("'", "'")
                ScriptManager.RegisterStartupScript(page, page.[GetType](), "err_msg", "alert('" & [error] & "');", True)
            End If
        Catch ex As Exception
            clsErrorLog.LogInfo(ex)

        End Try

    End Sub

    <System.Web.Services.WebMethod()>
    Public Shared Function UpdateCharges(ByVal OrderId As String, ByVal Amount As String, ByVal ChargeType As String) As String
        Dim flag As Integer = 0
        Dim Msg As String = ""
        If OrderId = "" Or OrderId Is Nothing Or Amount = "" Or Amount Is Nothing Or ChargeType = "" Or ChargeType Is Nothing Then
            flag = 0
        Else
            Try
                flag = UpdateChargesOnTicket(OrderId, Amount, ChargeType)

                'If flag > 0 Then
                '    Dim objPageSchema As New Report_PnrSummaryIntlNew
                '    objPageSchema.LabelTkt.Text = String.Empty
                '    objPageSchema.LabelTkt.Text = objPageSchema.TicketCopyExportPDF(OrderId, "")
                'End If

            Catch ex As Exception
                clsErrorLog.LogInfo(ex)
            End Try

        End If
        Msg = Convert.ToString(flag)
        Return Msg
    End Function

    Public Shared Function UpdateChargesOnTicket(ByVal OrderId As String, ByVal Amount As String, ByVal ChargeType As String) As Integer
        Dim flag As Integer = 0
        Dim MsgOut As String = ""
        Dim con As SqlConnection = New SqlConnection(ConfigurationManager.ConnectionStrings("myAmdDB").ConnectionString)
        'con = New SqlConnection(ConfigurationManager.ConnectionStrings("myAmdDB").ConnectionString)
        Try
            'AgentId = Convert.ToString(Session("UID"))
            'IPAddress = Request.UserHostAddress
            Dim cmd As SqlCommand = New SqlCommand("SP_UPDATE_TICKETTRANS_CHARGES", con)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@AgentId", AgentId)
            cmd.Parameters.AddWithValue("@OrderId", OrderId)
            cmd.Parameters.AddWithValue("@Amount", Convert.ToDecimal(Amount))
            cmd.Parameters.AddWithValue("@ChargeType", ChargeType)
            cmd.Parameters.AddWithValue("@CreatedBy", AgentId)
            cmd.Parameters.AddWithValue("@IPAddress", IPAddress)
            cmd.Parameters.AddWithValue("@ActionType", ChargeType)
            cmd.Parameters.Add("@Msg", SqlDbType.VarChar, 100)
            cmd.Parameters("@Msg").Direction = ParameterDirection.Output
            If con.State = ConnectionState.Closed Then con.Open()
            flag = cmd.ExecuteNonQuery()
            con.Close()
            MsgOut = cmd.Parameters("@Msg").Value.ToString()
        Catch ex As Exception
            clsErrorLog.LogInfo(ex)
            con.Close()
        End Try

        Return flag
    End Function



End Class
