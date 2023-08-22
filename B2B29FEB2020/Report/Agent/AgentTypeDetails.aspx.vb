Imports System.Data
Imports System.Data.SqlClient

Partial Class SprReports_Agent_AgentTypeDetails
    Inherits System.Web.UI.Page


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        Dim AgentID As String = ""
        Dim msg As String = ""
        LableMsg.Text = msg
        If Not Page.IsPostBack Then

            'DropDownListType.DataSource = GroupTypeMGMT("", TextBoxDesc.Text.Trim(), "MultipleSelect", msg)
            'DropDownListType.DataTextField = "GroupType"
            'DropDownListType.DataValueField = "GroupType"
            'DropDownListType.DataBind()

            BindData()

        End If



    End Sub

    Protected Sub ButtonSubmit_Click(sender As Object, e As EventArgs) Handles ButtonSubmit.Click

        Dim dt_comm As New DataTable()
        Dim dt_Gt As New DataTable()
        Dim msg As String = ""
        Try
            GroupTypeMGMT(TextBoxGroupType.Text.Trim().Replace(" ", [String].Empty), TextBoxDesc.Text.Trim(), "Insert", msg)

            If msg = "ADDED" Then

                dt_Gt = GetGroupType()
                dt_comm = GetCommissionRecord(Convert.ToString(dt_Gt.Rows(0)("Agent_Type").ToString))

                Dim cnt As Integer = 0
                For Each row As DataRow In dt_comm.Rows
                    InsertCommissionMaster(Convert.ToInt32(dt_comm.Rows(cnt)("ID")), Convert.ToString(dt_comm.Rows(cnt)("BookingFromDate").ToString), Convert.ToString(dt_comm.Rows(cnt)("BookingToDate").ToString), Convert.ToString(dt_comm.Rows(cnt)("OnwardTravelFromDate").ToString), Convert.ToString(dt_comm.Rows(cnt)("OnwardTravelToDate").ToString), Convert.ToString(dt_comm.Rows(cnt)("ReturnTravelFromDate").ToString), Convert.ToString(dt_comm.Rows(cnt)("ReturnTravelToDate").ToString), Convert.ToString(dt_comm.Rows(cnt)("BookingClassInclude").ToString), Convert.ToString(dt_comm.Rows(cnt)("BookingClassExclude").ToString), Convert.ToString(dt_comm.Rows(cnt)("FareBasisInclude").ToString), Convert.ToString(dt_comm.Rows(cnt)("FareBasisExclude").ToString), Convert.ToString(dt_comm.Rows(cnt)("OrginAirportInclude").ToString), Convert.ToString(dt_comm.Rows(cnt)("OrginAirportExclude").ToString), Convert.ToString(dt_comm.Rows(cnt)("DestinationAirportInclude").ToString), Convert.ToString(dt_comm.Rows(cnt)("DestinationAirportExclude").ToString), Convert.ToString(dt_comm.Rows(cnt)("FlightNoInclude").ToString), Convert.ToString(dt_comm.Rows(cnt)("FlightNoExclude").ToString), Convert.ToString(dt_comm.Rows(cnt)("OperatingCarrierInclude").ToString), Convert.ToString(dt_comm.Rows(cnt)("OperatingCarrierExclude").ToString), Convert.ToString(dt_comm.Rows(cnt)("MarketingCarrierInclude").ToString), Convert.ToString(dt_comm.Rows(cnt)("MarketingCarrierExclude").ToString), Convert.ToString(dt_comm.Rows(cnt)("CabinClassInclude").ToString), Convert.ToString(dt_comm.Rows(cnt)("CabinClassExclude").ToString), Convert.ToString(dt_comm.Rows(cnt)("FareType").ToString), Convert.ToString(dt_comm.Rows(cnt)("BookingChannel").ToString), Convert.ToString(dt_comm.Rows(cnt)("Active").ToString), Convert.ToString(dt_comm.Rows(cnt)("Status").ToString), Convert.ToString(dt_comm.Rows(cnt)("CreatedBy").ToString), "insert", TextBoxGroupType.Text.Trim(), Convert.ToDecimal(dt_comm.Rows(cnt)("CommisionOnBasic")), Convert.ToDecimal(dt_comm.Rows(cnt)("CommisionOnBasicYq")), Convert.ToDecimal(dt_comm.Rows(cnt)("CommissionOnYq")), Convert.ToDecimal(dt_comm.Rows(cnt)("PlbOnBasic")), Convert.ToDecimal(dt_comm.Rows(cnt)("PlbOnBasicYq")), Convert.ToString(dt_comm.Rows(cnt)("AirlineCode").ToString), Convert.ToString(dt_comm.Rows(cnt)("TripType").ToString), Convert.ToString(dt_comm.Rows(cnt)("OriginCountryInclude").ToString), Convert.ToString(dt_comm.Rows(cnt)("OriginCountryExclude").ToString), Convert.ToString(dt_comm.Rows(cnt)("DestCountryInclude").ToString), Convert.ToString(dt_comm.Rows(cnt)("DestCountryExclude").ToString), Convert.ToString(dt_comm.Rows(cnt)("AirlineName").ToString), Convert.ToString(dt_comm.Rows(cnt)("TripTypeName").ToString), Convert.ToString(dt_comm.Rows(cnt)("FareTypeName").ToString), Convert.ToDecimal(dt_comm.Rows(cnt)("CashBackAmt")), Convert.ToString(dt_comm.Rows(cnt)("RestrictionOn").ToString), Convert.ToString(dt_comm.Rows(cnt)("PPPType").ToString))

                    ''   InsertCommissionMaster(BookingFromDate, BookingToDate, OnwardTravelFromDate, OnwardTravelToDate, ReturnTravelFromDate, ReturnTravelToDate, BookingClassInclude, BookingClassExclude, FareBasisInclude, FareBasisExclude, OrginAirportInclude, OrginAirportExclude, DestinationAirportInclude, DestinationAirportExclude, FlightNoInclude, FlightNoExclude, OperatingCarrierInclude, OperatingCarrierExclude, MarketingCarrierInclude, MarketingCarrierExclude, CabinClassInclude, CabinClassExclude, FareType, BookingChannel, Active, Status, CreatedBy, ActionType, GroupType, CommisionOnBasic, CommisionOnBasicYq, CommissionOnYq, PlbOnBasic, PlbOnBasicYq, AirlineCode, TripType, OriginCountryInclude, OriginCountryExclude, DestCountryInclude, DestCountryExclude, AirlineName, TripTypeName, FareTypeName, CashBackAmt, RestrictionOn, PPPType)
                    cnt = cnt + 1
                Next
                msg = "Group Type successfully inserted."
            End If
            LableMsg.Text = msg
            BindData()

        Catch ex As Exception
            msg = "Try again"
            clsErrorLog.LogInfo(ex)
        End Try
    End Sub
    Private Sub BindData()
        Dim msg As String = ""
        Try

            GridView1.DataSource = GroupTypeMGMT("", TextBoxDesc.Text.Trim(), "MultipleSelect", msg)
            GridView1.DataBind()
        Catch ex As Exception
            clsErrorLog.LogInfo(ex)
        End Try
    End Sub

    Public Function GroupTypeMGMT(ByVal type As String, ByVal desc As String, ByVal cmdType As String, ByRef msg As String) As DataTable
        Dim con As New SqlConnection(ConfigurationManager.ConnectionStrings("myAmdDB").ConnectionString)
        Dim dt As New DataTable()
        Try

            con.Open()

            Dim cmd As New SqlCommand()

            cmd.CommandText = "usp_agentTypeMGMT_V1_NM"
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.Add("@UserId", SqlDbType.VarChar, 200).Value = Convert.ToString(Session("UID"))
            cmd.Parameters.Add("@UserType", SqlDbType.VarChar, 200).Value = type
            cmd.Parameters.Add("@desc", SqlDbType.VarChar, 500).Value = desc
            cmd.Parameters.Add("@cmdType", SqlDbType.VarChar, 50).Value = cmdType
            cmd.Parameters.Add("@msg", SqlDbType.VarChar, 500)
            cmd.Parameters("@msg").Direction = ParameterDirection.Output

            cmd.Connection = con
            Dim da As New SqlDataAdapter(cmd)
            da.Fill(dt)
            msg = cmd.Parameters("@msg").Value.ToString().Trim()



            con.Close()


        Catch ex As Exception
            clsErrorLog.LogInfo(ex)
            con.Close()

        End Try
        Return dt
    End Function

    Protected Sub GridView1_RowCancelingEdit(sender As Object, e As GridViewCancelEditEventArgs) Handles GridView1.RowCancelingEdit
        Try
            GridView1.EditIndex = -1
            BindData()

        Catch ex As Exception
            clsErrorLog.LogInfo(ex)

        End Try
    End Sub

    Protected Sub GridView1_RowEditing(sender As Object, e As GridViewEditEventArgs) Handles GridView1.RowEditing

        Try
            GridView1.EditIndex = e.NewEditIndex
            BindData()

        Catch ex As Exception
            clsErrorLog.LogInfo(ex)

        End Try
    End Sub

    Protected Sub GridView1_RowDeleting(sender As Object, e As GridViewDeleteEventArgs) Handles GridView1.RowDeleting
        Dim msg As String = ""
        Try
            Dim userType As String = UCase(DirectCast(GridView1.Rows(e.RowIndex).FindControl("LableGroupType"), Label).Text.ToUpper)
            GroupTypeMGMT(userType, "", "Delete", msg)

            If msg = "1" Then
                msg = "Group Type succesfully deleted."
            Else

            End If
            LableMsg.Text = msg
            BindData()

        Catch ex As Exception
            clsErrorLog.LogInfo(ex)
        End Try

    End Sub

    Protected Sub GridView1_RowUpdating(sender As Object, e As GridViewUpdateEventArgs) Handles GridView1.RowUpdating

        Dim msg As String = ""
        Try

            If DirectCast(GridView1.Rows(e.RowIndex).Cells(0).Controls(0), LinkButton).Text = "Update" Then

                Dim userType As String = UCase(DirectCast(GridView1.Rows(e.RowIndex).FindControl("LableGroupType"), Label).Text.ToUpper)
                Dim desc As String = UCase(DirectCast(GridView1.Rows(e.RowIndex).FindControl("TextBoxDesc"), TextBox).Text.ToUpper)

                GroupTypeMGMT(userType.Trim(), desc.Trim(), "Update", msg)

            End If
            GridView1.EditIndex = -1
            BindData()

        Catch ex As Exception
            clsErrorLog.LogInfo(ex)

        End Try
    End Sub

    Public Function GetCommissionRecord(ByVal groupType As String) As DataTable
        Dim dt As DataTable = New DataTable()
        Dim con As SqlConnection = New SqlConnection(ConfigurationManager.ConnectionStrings("myAmdDB").ConnectionString)
        Dim adap As SqlDataAdapter = New SqlDataAdapter()
        Try
            adap = New SqlDataAdapter("UspFlightCommissionFilter_forStockist_FetchRecordByDist", con)
            adap.SelectCommand.CommandType = CommandType.StoredProcedure
            adap.SelectCommand.Parameters.AddWithValue("@GroupType", groupType)
            adap.Fill(dt)
        Catch ex As Exception
            clsErrorLog.LogInfo(ex)
        Finally
            adap.Dispose()
        End Try

        Return dt
    End Function

    Public Function GetGroupType() As DataTable
        Dim dt_GROUP As DataTable = New DataTable()
        Dim con As SqlConnection = New SqlConnection(ConfigurationManager.ConnectionStrings("myAmdDB").ConnectionString)
        Dim adap As SqlDataAdapter = New SqlDataAdapter()
        Try
            adap = New SqlDataAdapter("GetGroupTypeDistr", con)
            adap.SelectCommand.CommandType = CommandType.StoredProcedure
            adap.SelectCommand.Parameters.AddWithValue("@UserID", Session("UID").ToString())
            adap.Fill(dt_GROUP)
        Catch ex As Exception
            clsErrorLog.LogInfo(ex)
        Finally
            adap.Dispose()
        End Try

        Return dt_GROUP
    End Function

    Private Function InsertCommissionMaster(ByVal idbk As Integer, ByVal BookingFromDate As String, ByVal BookingToDate As String, ByVal OnwardTravelFromDate As String, ByVal OnwardTravelToDate As String, ByVal ReturnTravelFromDate As String, ByVal ReturnTravelToDate As String, ByVal BookingClassInclude As String, ByVal BookingClassExclude As String, ByVal FareBasisInclude As String, ByVal FareBasisExclude As String, ByVal OrginAirportInclude As String, ByVal OrginAirportExclude As String, ByVal DestinationAirportInclude As String, ByVal DestinationAirportExclude As String, ByVal FlightNoInclude As String, ByVal FlightNoExclude As String, ByVal OperatingCarrierInclude As String, ByVal OperatingCarrierExclude As String, ByVal MarketingCarrierInclude As String, ByVal MarketingCarrierExclude As String, ByVal CabinClassInclude As String, ByVal CabinClassExclude As String, ByVal FareType As String, ByVal BookingChannel As String, ByVal Active As String, ByVal Status As String, ByVal CreatedBy As String, ByVal ActionType As String, ByVal GroupType As String, ByVal CommisionOnBasic As Decimal, ByVal CommisionOnBasicYq As Decimal, ByVal CommissionOnYq As Decimal, ByVal PlbOnBasic As Decimal, ByVal PlbOnBasicYq As Decimal, ByVal AirlineCode As String, ByVal TripType As String, ByVal OriginCountryInclude As String, ByVal OriginCountryExclude As String, ByVal DestCountryInclude As String, ByVal DestCountryExclude As String, ByVal AirlineName As String, ByVal TripTypeName As String, ByVal FareTypeName As String, ByVal CashBackAmt As Decimal, ByVal RestrictionOn As String, ByVal PPPType As String) As Integer
        Dim flag As Integer = 0
        Dim con As SqlConnection = New SqlConnection(ConfigurationManager.ConnectionStrings("myAmdDB").ConnectionString)
        Try

            Dim cmd As SqlCommand = New SqlCommand("UspFlightCommissionFilter_forStockist_v1_NM", con)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@BookingFromDate", BookingFromDate)
            cmd.Parameters.AddWithValue("@BookingToDate", BookingToDate)
            cmd.Parameters.AddWithValue("@OnwardTravelFromDate", OnwardTravelFromDate)
            cmd.Parameters.AddWithValue("@OnwardTravelToDate", OnwardTravelToDate)
            cmd.Parameters.AddWithValue("@ReturnTravelFromDate", ReturnTravelFromDate)
            cmd.Parameters.AddWithValue("@ReturnTravelToDate", ReturnTravelToDate)
            cmd.Parameters.AddWithValue("@BookingClassInclude", BookingClassInclude)
            cmd.Parameters.AddWithValue("@BookingClassExclude", BookingClassExclude)
            cmd.Parameters.AddWithValue("@FareBasisInclude", FareBasisInclude)
            cmd.Parameters.AddWithValue("@FareBasisExclude", FareBasisExclude)
            cmd.Parameters.AddWithValue("@OrginAirportInclude", OrginAirportInclude)
            cmd.Parameters.AddWithValue("@OrginAirportExclude", OrginAirportExclude)
            cmd.Parameters.AddWithValue("@DestinationAirportInclude", DestinationAirportInclude)
            cmd.Parameters.AddWithValue("@DestinationAirportExclude", DestinationAirportExclude)
            cmd.Parameters.AddWithValue("@FlightNoInclude", FlightNoInclude)
            cmd.Parameters.AddWithValue("@FlightNoExclude", FlightNoExclude)
            cmd.Parameters.AddWithValue("@OperatingCarrierInclude", OperatingCarrierInclude)
            cmd.Parameters.AddWithValue("@OperatingCarrierExclude", OperatingCarrierExclude)
            cmd.Parameters.AddWithValue("@MarketingCarrierInclude", MarketingCarrierInclude)
            cmd.Parameters.AddWithValue("@MarketingCarrierExclude", MarketingCarrierExclude)
            cmd.Parameters.AddWithValue("@CabinClassInclude", CabinClassInclude)
            cmd.Parameters.AddWithValue("@CabinClassExclude", CabinClassExclude)
            cmd.Parameters.AddWithValue("@FareType", FareType)
            cmd.Parameters.AddWithValue("@BookingChannel", BookingChannel)
            cmd.Parameters.AddWithValue("@Active", Active)
            cmd.Parameters.AddWithValue("@Status", Status)
            cmd.Parameters.AddWithValue("@CreatedBy", CreatedBy)
            cmd.Parameters.AddWithValue("@ActionType", ActionType)
            cmd.Parameters.AddWithValue("@GroupType", GroupType)
            cmd.Parameters.AddWithValue("@CommisionOnBasic", CommisionOnBasic)
            cmd.Parameters.AddWithValue("@CommisionOnBasicYq", CommisionOnBasicYq)
            cmd.Parameters.AddWithValue("@CommissionOnYq", CommissionOnYq)
            cmd.Parameters.AddWithValue("@PlbOnBasic", PlbOnBasic)
            cmd.Parameters.AddWithValue("@PlbOnBasicYq", PlbOnBasicYq)
            cmd.Parameters.AddWithValue("@AirlineCode", AirlineCode)
            cmd.Parameters.AddWithValue("@TripType", TripType)
            cmd.Parameters.AddWithValue("@OriginCountryInclude", OriginCountryInclude)
            cmd.Parameters.AddWithValue("@OriginCountryExclude", OriginCountryExclude)
            cmd.Parameters.AddWithValue("@DestCountryInclude", DestCountryInclude)
            cmd.Parameters.AddWithValue("@DestCountryExclude", DestCountryExclude)
            cmd.Parameters.AddWithValue("@AirlineName", AirlineName)
            cmd.Parameters.AddWithValue("@TripTypeName", TripTypeName)
            cmd.Parameters.AddWithValue("@FareTypeName", FareTypeName)
            cmd.Parameters.AddWithValue("@CashBackAmt", CashBackAmt)
            cmd.Parameters.AddWithValue("@RestrictionOn", RestrictionOn)
            cmd.Parameters.AddWithValue("@PPPType", PPPType)
            cmd.Parameters.AddWithValue("@UserId", Session("UID").ToString())
            cmd.Parameters.AddWithValue("@IDBK", idbk)
            If con.State = ConnectionState.Closed Then con.Open()
            flag = cmd.ExecuteNonQuery()
            con.Close()
        Catch ex As Exception
            con.Close()
        End Try

        Return flag
    End Function
End Class
