Imports iFont = iTextSharp.text.Font
Imports iTextSharp.text
Imports iTextSharp.text.pdf
Imports System.IO
Imports System.Text
Imports System.Data.SqlClient
Imports System.Globalization

Public Structure SONY_ACTIVITY_REPORT

    Dim Customer_Visit_S As Integer
    Dim Call_Registerd_S As Integer
    Dim Repair_Completed_S As Integer
    Dim Goods_Delivered_S As Integer
    Dim Pending_Calls_S As Integer
    Dim Cancelled_Calls_S As Integer
    Dim Reservation_S As Integer

    Dim Customer_Visit_D As Integer
    Dim Call_Registerd_D As Integer
    Dim Repair_Completed_D As Integer
    Dim Goods_Delivered_D As Integer
    Dim Pending_Calls_D As Integer
    Dim Cancelled_Calls_D As Integer
    Dim Reservation_D As Integer

    Dim Customer_Visit_T As Integer
    Dim Call_Registerd_T As Integer
    Dim Repair_Completed_T As Integer
    Dim Goods_Delivered_T As Integer
    Dim Pending_Calls_T As Integer
    Dim Cancelled_Calls_T As Integer
    Dim Reservation_T As Integer

    Dim day As String  'mon-dd' 例）jun-01
    Dim day2 As String 'YYYY/MM/DD'
    Dim youbi As String
    Dim month As String 'YYYY/MM'
    Dim ActYear As String 'YYYY /* Added Mon Year VJ 20100106*/
    Dim ActMonth As String  'MM /* Added Mon Year VJ 20100106*/
    Dim note As String
    Dim location As String

    'Comment on 20200106
    Dim Labor As Decimal
    Dim Parts As Decimal
    Dim Billing_date As String

End Structure

Public Class SonyAnalysis_Report2
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack = False Then

            '***セッションなしログインユーザの対応***
            Dim userid As String = Session("user_id")
            If userid = "" Then
                Response.Redirect("Login.aspx")
            End If

            '***セッション取得***
            Dim shipCode As String = Session("ship_code")
            Dim setMon As String = Session("set_Mon")
            Dim setYear As String = Session("set_Year")
            ' Session("set_Year")

            '***表示の設定***
            '本日月
            lblMonNow.Text = (Convert.ToInt32(setMon) + 1).ToString("00")

            '***GRIDVIEWへ反映 ***
            '対象年月の設定
            Dim clsSetCommon As New Class_common
            Dim dtNow As DateTime = clsSetCommon.dtIndia
            'Comment by Mohan 
            'Dim monDate As String = Left(dtNow.ToShortDateString, 4) & "/" & Trim(lblMonNow.Text)
            Dim monDate As String = setYear & "/" & Trim(lblMonNow.Text)

            Call RadioAll1()
            'Dim strSQL As String = "SET LANGUAGE us_english;SELECT day + '  ' +   datename(weekday,(convert(datetime, month + '/' + day)) ) as day,Service,D_I,Customer_Visit, Call_Registerd, Repair_Completed,Goods_Delivered, Pending_Calls, Cancelled_Calls "
            'strSQL &= "FROM dbo.SONY_ACTIVITY_REPORT WHERE DELFG = 0 AND Month = '" & monDate & "' AND location = '" & shipCode & "' "
            'strSQL &= "ORDER BY day;"

            'If strSQL <> "" Then
            '    Dim errFlg As Integer
            '    Dim DT_SERCH As DataTable = DBCommon.ExecuteGetDT(strSQL, errFlg)

            '    'If errFlg = 1 Then
            '    '    Call showMsg("Failed to acquire search information.")
            '    '    Exit Sub
            '    'End If

            '    If DT_SERCH IsNot Nothing Then

            '        GridInfo.DataSource = DT_SERCH
            '        GridInfo.DataBind()
            '        '次ページ反映ように取得しておく
            '        Session("Data_DT_SERCH") = DT_SERCH
            '        btnDown.Visible = True
            '    Else

            '        'If RadioAll.Checked = True Then
            '        '    Call RadioAll1()
            '        'End If
            '        GridInfo.DataSource = Nothing
            '        GridInfo.DataBind()
            '        btnDown.Visible = False
            '    End If
            'End If
        End If

    End Sub


    Public Overrides Sub VerifyRenderingInServerForm(control As Control)
        'It solves the error "Control 'GridView1' of type 'GridView' must be placed inside a form tag with runat=server."
    End Sub

    Function RadioAll1()

        Dim shipCode As String = Session("ship_code")
        Dim setMon As String = Session("set_Mon")
        Dim setYear As String = Session("set_Year")
        Dim clsSetCommon As New Class_common
        Dim dtNow As DateTime = clsSetCommon.dtIndia
        Dim monDate As String = setYear & "/" & Trim(lblMonNow.Text)

        If RadioAll.Checked = True Then
            Dim strSQL As String = "select Day + '  ' +   datename(weekday,(convert(datetime, month + '/' + day)) ) as Day, 'Service' as Type,'1' seq, customer_visit_S as CustomerVisit,"
            strSQL &= "Call_Registerd_S as CallRegistered,Repair_Completed_S as RepairComplete,Goods_Delivered_S as GoodsDelivered,Pending_Calls_S as PendingCalls, Cancelled_Calls_S as Cancel,Reservation_S as Reservation"
            strSQL &= " From SONY_ACTIVITY_REPORT WHERE DELFG = 0 AND Month = '" & monDate & "' AND location = '" & shipCode & "'"
            strSQL &= " UNION "
            strSQL &= "select Day + '  ' +   datename(weekday,(convert(datetime, month + '/' + day)) ) as Day, 'D&I'as Type, '2' seq, customer_visit_D as CustomerVisit,"
            strSQL &= "Call_Registerd_D as CallRegistered,Repair_Completed_D as RepairComplete,Goods_Delivered_D as GoodsDelivered,Pending_Calls_D as PendingCalls, Cancelled_Calls_D as Cancel,Reservation_D as Reservation"
            strSQL &= " From SONY_ACTIVITY_REPORT WHERE DELFG = 0 AND Month = '" & monDate & "' AND location = '" & shipCode & "'"
            strSQL &= " UNION "
            strSQL &= "Select Day + '  ' +   datename(weekday,(convert(datetime, month + '/' + day)) ) as Day, 'Total'as Type, '3' seq, sum (customer_visit_S  + customer_visit_D)as CustomerVisit ,"
            strSQL &= "sum(Call_Registerd_S + Call_Registerd_D)As CallRegistered, sum(Repair_Completed_S+Repair_Completed_D) As RepairComplete,"
            strSQL &= "sum(Goods_Delivered_S + Goods_Delivered_D)As GoodsDelivered,sum(Pending_Calls_S+Pending_Calls_D)As PendingCalls,sum(Cancelled_Calls_S+Cancelled_Calls_D) As Cancel, sum(Reservation_S+Reservation_D) As Reservation"
            strSQL &= " From SONY_ACTIVITY_REPORT WHERE DELFG = 0 AND Month = '" & monDate & "' AND location = '" & shipCode & "'  Group By day + '  ' +  datename(weekday,(convert(datetime, month + '/' + day)) ) order by day, seq  ;"

            If strSQL <> "" Then
                Dim errFlg5 As Integer
                ' Dim DataTable DT_SERCH = New DataTable();
                'Dim DT_SERCH As New DataTable
                Dim DT_SERCH As DataTable = DBCommon.ExecuteGetDT(strSQL, errFlg5)
                'DT_SERCH = DBCommon.ExecuteGetDT(strSQL, errFlg5)


                If (DT_SERCH Is Nothing) Then
                    Call showMsg("There was no relevant information")
                    Exit Function
                ElseIf (DT_SERCH.Rows.Count > 0 And DT_SERCH IsNot Nothing) Then
                    DT_SERCH.Columns.RemoveAt(2)
                End If

                'If (DT_SERCH.Rows.Count > 0 And DT_SERCH IsNot Nothing) Then
                '    DT_SERCH.Columns.RemoveAt(2)

                'ElseIf (DT_SERCH Is Nothing) Then
                '    Exit Function
                'End If


                'If (DT_SERCH.Rows.Count > 0 && DT_SERCH.Rows! = Null) Then
                '    DT_SERCH.Columns.RemoveAt(2)
                'End If


                If errFlg5 = 1 Then
                        Call showMsg("Failed to acquire search information.")
                        Exit Function
                    End If

                    If DT_SERCH IsNot Nothing Then
                        GridInfo.DataSource = DT_SERCH
                        GridInfo.DataBind()


                        '次ページ反映ように取得しておく
                        Session("Data_DT_SERCH") = DT_SERCH

                        For i As Integer = GridInfo.Rows.Count - 1 To 1 Step -1
                            Dim row As GridViewRow = GridInfo.Rows(i)
                            Dim previousRow As GridViewRow = GridInfo.Rows(i - 1)
                            For j As Integer = 0 To row.Cells.Count - 1
                                If row.Cells(j).Text = previousRow.Cells(j).Text Then
                                    If previousRow.Cells(j).RowSpan = 0 And j = 0 Then
                                        If row.Cells(j).RowSpan = 0 And j = 0 Then
                                            previousRow.Cells(j).RowSpan += 2
                                        Else
                                            previousRow.Cells(j).RowSpan = row.Cells(j).RowSpan + 1
                                        End If
                                        row.Cells(j).Visible = False
                                    End If
                                End If
                            Next
                        Next

                        btnDown.Visible = True
                    Else
                        GridInfo.DataSource = Nothing
                        GridInfo.DataBind()
                        btnDown.Visible = False
                    End If

                End If

            End If

    End Function



    Protected Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click

        Response.Redirect("SonyAnalysis_Report.aspx")

    End Sub

    Protected Sub showMsg(ByVal Msg As String)

        lblMsg.Text = Msg
        Dim sScript As String = "$(function () { $( ""#dialog"" ).dialog({width: 400, buttons: {""OK"": function () {$(this).dialog('close');}}});});"
        'JavaScriptの埋め込み
        ClientScript.RegisterStartupScript(Me.GetType(), "startup", sScript, True)

    End Sub
    'DLボタン押下処理
    Protected Sub btnDown_Click(sender As Object, e As EventArgs) Handles btnDown.Click

        '***セッション取得***
        Dim adminFlg As Boolean = Session("admin_Flg")
        Dim userLevel As String = Session("user_level")
        Dim setShipname As String = Session("ship_Name")
        Dim shipCode As String = Session("ship_code")
        Dim setMon As String = Session("set_Mon")
        Dim setYear As String = Session("set_Year")
        Dim setDay As String = Session("set_Day")


        '***DL処理***
        Dim errFlg As Integer
        Dim clsSet As New Class_analysis
        Dim dsActivity_report As New DataSet
        'Dim SonyactivityReportData() As Class_analysis.SONY_ACTIVITY_REPORT
        'Dim SonyactivityReportDataSum As Class_analysis.SONY_ACTIVITY_REPORT

        '**データ取得**
        '対象年月の設定
        Dim clsSetCommon As New Class_common
        Dim dtNow As DateTime = clsSetCommon.dtIndia
        setMon = (Convert.ToInt32(setMon) + 1).ToString("00")
        'Comment by Mohan
        'Dim monDate As String = Left(dtNow.ToShortDateString, 4) & "/" & setMon
        Dim monDate As String = setYear & "/" & setMon
        'Dim setDay As String = setDay


        If RadioAll.Checked = True Then

            Dim strSQL As String = "select Day + '  ' +   datename(weekday,(convert(datetime, month + '/' + day)) ) as Day, 'Service' as Type,'1' seq, customer_visit_S as CustomerVisit,"
            strSQL &= "Call_Registerd_S as CallRegistered,Repair_Completed_S as RepairComplete,Goods_Delivered_S as GoodsDelivered,Pending_Calls_S as PendingCalls, Cancelled_Calls_S as Cancel,Reservation_S as Reservation"
            strSQL &= " From SONY_ACTIVITY_REPORT WHERE DELFG = 0 AND Month = '" & monDate & "' AND location = '" & shipCode & "'"
            strSQL &= " UNION "
            strSQL &= "select Day + '  ' +   datename(weekday,(convert(datetime, month + '/' + day)) ) as Day, 'D_I'as Type, '2' seq, customer_visit_D as CustomerVisit,"
            strSQL &= "Call_Registerd_D as CallRegistered,Repair_Completed_D as RepairComplete,Goods_Delivered_D as GoodsDelivered,Pending_Calls_D as PendingCalls, Cancelled_Calls_D as Cancel,Reservation_D as Reservation"
            strSQL &= " From SONY_ACTIVITY_REPORT WHERE DELFG = 0 AND Month = '" & monDate & "' AND location = '" & shipCode & "'"
            strSQL &= " UNION "
            strSQL &= "Select Day + '  ' +   datename(weekday,(convert(datetime, month + '/' + day)) ) as Day, 'Total'as Type, '3' seq, sum (customer_visit_S  + customer_visit_D)as CustomerVisit ,"
            strSQL &= "sum(Call_Registerd_S + Call_Registerd_D)As CallRegistered, sum(Repair_Completed_S+Repair_Completed_D) As RepairComplete,"
            strSQL &= "sum(Goods_Delivered_S + Goods_Delivered_D)As GoodsDelivered,sum(Pending_Calls_S+Pending_Calls_D)As PendingCalls,sum(Cancelled_Calls_S+Cancelled_Calls_D) As Cancel, sum(Reservation_S+Reservation_D) As Reservation"
            strSQL &= " From SONY_ACTIVITY_REPORT WHERE DELFG = 0 AND Month = '" & monDate & "' AND location = '" & shipCode & "'  Group By day + '  ' +  datename(weekday,(convert(datetime, month + '/' + day)) ) order by day, seq  ;"




            If strSQL <> "" Then
                Dim errFlg4 As Integer
                Dim DT_SERCH As DataTable = DBCommon.ExecuteGetDT(strSQL, errFlg4)
                'DT_SERCH.Columns.Remove("seq")
                'DT_SERCH.Columns.RemoveAt(2)

                If (DT_SERCH Is Nothing) Then
                    Call showMsg("There was no relevant information")
                    Exit Sub
                ElseIf (DT_SERCH.Rows.Count > 0 And DT_SERCH IsNot Nothing) Then
                    DT_SERCH.Columns.RemoveAt(2)
                End If





                If errFlg = 1 Then
                    Call showMsg("Failed to acquire search information.")
                    Exit Sub
                End If

                If DT_SERCH IsNot Nothing Then
                    GridInfo.DataSource = DT_SERCH
                    GridInfo.DataBind()
                    '次ページ反映ように取得しておく
                    Session("Data_DT_SERCH") = DT_SERCH

                    For i As Integer = GridInfo.Rows.Count - 1 To 1 Step -1
                        Dim row As GridViewRow = GridInfo.Rows(i)
                        Dim previousRow As GridViewRow = GridInfo.Rows(i - 1)
                        For j As Integer = 0 To row.Cells.Count - 1
                            If row.Cells(j).Text = previousRow.Cells(j).Text Then
                                If previousRow.Cells(j).RowSpan = 0 And j = 0 Then
                                    If row.Cells(j).RowSpan = 0 And j = 0 Then
                                        previousRow.Cells(j).RowSpan += 2
                                    Else
                                        previousRow.Cells(j).RowSpan = row.Cells(j).RowSpan + 1
                                    End If
                                    row.Cells(j).Visible = False
                                End If
                            End If
                        Next
                    Next

                    btnDown.Visible = True
                Else
                    GridInfo.DataSource = Nothing
                    GridInfo.DataBind()
                    btnDown.Visible = False
                End If

            End If

            Dim csvFileName As String = "Sony_Activity_All_report_" & setYear & setMon & setDay & ".csv"


            Response.Clear()
            Response.Buffer = True
            Response.AddHeader("content-disposition", "attachment;filename=" & csvFileName)
            Response.Charset = ""
            Response.ContentType = "application/text/csv"
            GridInfo.AllowPaging = False
            GridInfo.DataBind()

            Dim sb As New StringBuilder()

            For k As Integer = 0 To GridInfo.Columns.Count - 1

                'add separator

                sb.Append(GridInfo.Columns(k).HeaderText + ","c)

            Next

            'append new line

            sb.Append(vbCr & vbLf)

            For i As Integer = 0 To GridInfo.Rows.Count - 1

                For k As Integer = 0 To GridInfo.Columns.Count - 1

                    'add separator

                    sb.Append(GridInfo.Rows(i).Cells(k).Text + ","c)

                Next
                'append new line

                sb.Append(vbCr & vbLf)

            Next

            Response.Output.Write(sb.ToString())
            Response.Flush()
            Response.End()

        End If

        If RadioService.Checked = True Then


            Dim strSQL As String = "SET LANGUAGE us_english;SELECT Day + '  ' +   datename(weekday,(convert(datetime, month + '/' + day)) ) as Day,'Service' as Type, Customer_Visit_S as CustomerVisit, Call_Registerd_S as CallRegistered, Repair_Completed_S as RepairComplete,Goods_Delivered_S as GoodsDelivered, Pending_Calls_S as PendingCalls, Cancelled_Calls_S as Cancel,Reservation_S as Reservation "
            strSQL &= "FROM dbo.SONY_ACTIVITY_REPORT WHERE DELFG = 0 AND Month = '" & monDate & "' AND location = '" & shipCode & "' "
            strSQL &= "ORDER BY day;"


            If strSQL <> "" Then
                Dim errFlg2 As Integer
                Dim DT_SERCH As DataTable = DBCommon.ExecuteGetDT(strSQL, errFlg2)

                If errFlg = 1 Then
                    Call showMsg("Failed to acquire search information.")
                    Exit Sub
                End If

                If DT_SERCH IsNot Nothing Then
                    GridInfo.DataSource = DT_SERCH
                    GridInfo.DataBind()
                    '次ページ反映ように取得しておく
                    Session("Data_DT_SERCH") = DT_SERCH
                    btnDown.Visible = True
                Else
                    GridInfo.DataSource = Nothing
                    GridInfo.DataBind()
                    btnDown.Visible = False
                End If

            End If

            Dim csvFileName As String = "Sony_Activity_Service_report_" & setYear & setMon & setDay & ".csv"

            Response.Clear()
            Response.Buffer = True
            Response.AddHeader("content-disposition", "attachment;filename=" & csvFileName)
            Response.Charset = ""
            Response.ContentType = "application/text/csv"
            GridInfo.AllowPaging = False
            GridInfo.DataBind()

            Dim sb As New StringBuilder()

            For k As Integer = 0 To GridInfo.Columns.Count - 1
                'add separator
                sb.Append(GridInfo.Columns(k).HeaderText + ","c)
            Next

            'append new line

            sb.Append(vbCr & vbLf)
            For i As Integer = 0 To GridInfo.Rows.Count - 1
                For k As Integer = 0 To GridInfo.Columns.Count - 1
                    'add separator
                    sb.Append(GridInfo.Rows(i).Cells(k).Text + ","c)
                Next
                'append new line
                sb.Append(vbCr & vbLf)
            Next

            Response.Output.Write(sb.ToString())
            Response.Flush()
            Response.End()


        End If

        If RadioD_I.Checked = True Then


            Dim strSQL As String = "SELECT Day + '  ' +   datename(weekday,(convert(datetime, month + '/' + day)) ) as Day,'D_I' as Type,Customer_Visit_D as CustomerVisit, Call_Registerd_D as CallRegistered, Repair_Completed_D as RepairComplete,Goods_Delivered_D as GoodsDelivered, Pending_Calls_D as PendingCalls, Cancelled_Calls_D as Cancel,Reservation_D as Reservation "
            strSQL &= "FROM dbo.SONY_ACTIVITY_REPORT WHERE DELFG = 0 AND Month = '" & monDate & "' AND location = '" & shipCode & "' "
            strSQL &= "ORDER BY day;"

            If strSQL <> "" Then
                Dim errFlg1 As Integer
                Dim DT_SERCH As DataTable = DBCommon.ExecuteGetDT(strSQL, errFlg1)

                If errFlg1 = 1 Then
                    Call showMsg("Failed to acquire search information.")
                    Exit Sub
                End If

                If DT_SERCH IsNot Nothing Then
                    GridInfo.DataSource = DT_SERCH
                    GridInfo.DataBind()
                    '    次ページ反映ように取得しておく
                    Session("Data_DT_SERCH") = DT_SERCH
                    btnDown.Visible = True
                Else
                    GridInfo.DataSource = Nothing
                    GridInfo.DataBind()
                    btnDown.Visible = False
                End If

            End If

            Dim csvFileName As String = "Sony_Activity_D&I_report_" & setYear & setMon & setDay & ".csv"

            Response.Clear()

            Response.Buffer = True

            Response.AddHeader("content-disposition", "attachment;filename=" & csvFileName)

            Response.Charset = ""

            Response.ContentType = "application/text/csv"

            GridInfo.AllowPaging = False

            GridInfo.DataBind()

            Dim sb As New StringBuilder()

            For k As Integer = 0 To GridInfo.Columns.Count - 1

                'add separator

                sb.Append(GridInfo.Columns(k).HeaderText + ","c)

            Next

            'append new line

            sb.Append(vbCr & vbLf)

            For i As Integer = 0 To GridInfo.Rows.Count - 1

                For k As Integer = 0 To GridInfo.Columns.Count - 1

                    'add separator

                    sb.Append(GridInfo.Rows(i).Cells(k).Text + ","c)

                Next

                'append new line

                sb.Append(vbCr & vbLf)
            Next
            Response.Output.Write(sb.ToString())
            Response.Flush()
            Response.End()
        End If

        If RadioTotal.Checked = True Then

            Dim strSQL As String = "Select Day + '  ' +   datename(weekday,(convert(datetime, month + '/' + day)) ) as Day, 'Total'as Type,sum (customer_visit_S  + customer_visit_D)as CustomerVisit ,"
            strSQL &= "sum(Call_Registerd_S + Call_Registerd_D)As CallRegistered, sum(Repair_Completed_S+Repair_Completed_D) As RepairComplete,"
            strSQL &= "sum(Goods_Delivered_S + Goods_Delivered_D)As GoodsDelivered,sum(Pending_Calls_S+Pending_Calls_D)As PendingCalls,sum(Cancelled_Calls_S+Cancelled_Calls_D) As Cancel, sum(Reservation_S+Reservation_D) As Reservation"
            strSQL &= " From SONY_ACTIVITY_REPORT WHERE DELFG = 0 AND Month = '" & monDate & "' AND location = '" & shipCode & "' Group By day + '  ' +  datename(weekday,(convert(datetime, month + '/' + day)) ) order by day ;"

            If strSQL <> "" Then
                Dim errFlg3 As Integer
                Dim DT_SERCH As DataTable = DBCommon.ExecuteGetDT(strSQL, errFlg3)

                If errFlg = 1 Then
                    Call showMsg("Failed to acquire search information.")
                    Exit Sub
                End If

                If DT_SERCH IsNot Nothing Then
                    GridInfo.DataSource = DT_SERCH
                    GridInfo.DataBind()
                    '次ページ反映ように取得しておく
                    Session("Data_DT_SERCH") = DT_SERCH
                    btnDown.Visible = True
                Else
                    GridInfo.DataSource = Nothing
                    GridInfo.DataBind()
                    btnDown.Visible = False
                End If

            End If

            Dim csvFileName As String = "Sony_Activity_Total_report_" & setYear & setMon & setDay & ".csv"

            Response.Clear()
            Response.Buffer = True
            Response.AddHeader("content-disposition", "attachment;filename=" & csvFileName)
            Response.Charset = ""
            Response.ContentType = "application/text/csv"
            GridInfo.AllowPaging = False
            GridInfo.DataBind()

            Dim sb As New StringBuilder()

            For k As Integer = 0 To GridInfo.Columns.Count - 1

                'add separator

                sb.Append(GridInfo.Columns(k).HeaderText + ","c)

            Next

            'append new line

            sb.Append(vbCr & vbLf)

            For i As Integer = 0 To GridInfo.Rows.Count - 1

                For k As Integer = 0 To GridInfo.Columns.Count - 1

                    'add separator

                    sb.Append(GridInfo.Rows(i).Cells(k).Text + ","c)

                Next

                'append new line

                sb.Append(vbCr & vbLf)

            Next

            Response.Output.Write(sb.ToString())
            Response.Flush()
            Response.End()


        End If




        ''Call clsSet.exportDataSonyActivityReport(dsActivity_report, SonyactivityReportData, SonyactivityReportDataSum, monDate, shipCode, errFlg)

        'If errFlg = 1 Then
        '    Call showMsg("Acquisition of DL data failed")
        '    Exit Sub
        'End If

        'If dsActivity_report Is Nothing Then
        '    Call showMsg("There is no DL data. Cancel processing.")
        '    Exit Sub
        'Else

        '    Try
        '        ファイル名設定
        '        Dim csvFileName As String = "Sony_Activity_report_" & setMon & ".csv"
        '        Dim csvFileName As String = "Sony_Activity_report_" & dtNow.ToShortDateString & ".csv"
        '        Dim csvFileName As String = "Sony_Activity_report_" & setYear & setMon & setDay & ".csv"
        '        Dim outputPath As String = clsSet.CsvFilePass & csvFileName

        '        項目名設定
        '        Dim rowWork1 As String = setShipname & " Result"

        '        Dim csvContents = New List(Of String)(New String() {rowWork1})
        '        Dim csvContents As String

        '        Dim rowWork2 As String = "output date," & dtNow.ToShortDateString

        '        Dim rowWork3 As String = "Service," & SonyactivityReportDataSum.Service

        '        Dim rowWork4 As String = "D&I," & SonyactivityReportDataSum.D_I

        '        Dim rowWork5 As String = "Customer Visit," & SonyactivityReportDataSum.Customer_Visit

        '        Dim rowWork6 As String = "Repair Completed," & SonyactivityReportDataSum.Repair_Completed

        '        一覧の項目
        '        Dim rowWork7 As String = "Branch,Date,Service,D_I,Customer Visit,Call Registered,Repair Completed,Goods Delivered,Pending Calls,Cancelled Calls,note"

        '        csvContents.Add(rowWork2)
        '        csvContents.Add(rowWork3)
        '        csvContents.Add(rowWork4)
        '        csvContents.Add(rowWork5)
        '        csvContents.Add(rowWork6)
        '        csvContents.Add(rowWork7)

        '        一覧の設定
        '        For i = 0 To SonyactivityReportData.Length - 1
        '            csvContents.Add(activityReportData(i).location & "," & activityReportData(i).month & "/" & activityReportData(i).day & "," & activityReportData(i).Customer_Visit & "," & activityReportData(i).Call_Registerd & "," & activityReportData(i).Repair_Completed & "," & activityReportData(i).Goods_Delivered & "," & activityReportData(i).Pending_Calls & "," & activityReportData(i).Cancelled_Calls & "," & activityReportData(i).note)
        '            '' csvContents.Add(SonyactivityReportData(i).location & "," & SonyactivityReportData(i).month & "/" & SonyactivityReportData(i).day & "," & SonyactivityReportData(i).Service & "," & SonyactivityReportData(i).D_I & "," & SonyactivityReportData(i).Customer_Visit & "," & SonyactivityReportData(i).Call_Registerd & "," & SonyactivityReportData(i).Repair_Completed & "," & SonyactivityReportData(i).Goods_Delivered & "," & SonyactivityReportData(i).Pending_Calls & "," & SonyactivityReportData(i).Cancelled_Calls & "," & SonyactivityReportData(i).note)
        '        Next i

        '        Dim rowWork8 As String
        '        Dim rowWork9 As String = ",," & activityReportDataSum.Customer_Visit & "," & activityReportDataSum.Call_Registerd & "," & activityReportDataSum.Repair_Completed & "," & activityReportDataSum.Goods_Delivered & "," & activityReportDataSum.Pending_Calls & "," & activityReportDataSum.Cancelled_Calls
        '        ''Dim rowWork8 As String = ",," & SonyactivityReportDataSum.Service & "," & SonyactivityReportDataSum.D_I & "," & SonyactivityReportDataSum.Customer_Visit & "," & SonyactivityReportDataSum.Call_Registerd & "," & SonyactivityReportDataSum.Repair_Completed & "," & SonyactivityReportDataSum.Goods_Delivered & "," & SonyactivityReportDataSum.Pending_Calls & "," & SonyactivityReportDataSum.Cancelled_Calls
        '        csvContents.Add(rowWork7)
        '        '' csvContents.Add(rowWork8)

        '        Using writer As New StreamWriter(outputPath, False, Encoding.Default)
        '            writer.WriteLine(String.Join(Environment.NewLine, csvContents))
        '        End Using

        '        ファイルの内容をバイト配列にすべて読み込む
        '        Dim Buffer As Byte() = System.IO.File.ReadAllBytes(outputPath)

        '        サーバに保存されたCSVファイルを削除
        '        ※Response.End以降、ファイル削除処理ができないため、ファイルのダウンロードではなく、一旦ファイルの内容を
        '        上記、Bufferに保存し、ダウンロード処理を行う。

        '        If outputPath <> "" Then
        '            If System.IO.File.Exists(outputPath) = True Then
        '                System.IO.File.Delete(outputPath)
        '            End If
        '        End If

        '        ダウンロード
        '        Response.ContentType = "application/text/csv"
        '        Response.AddHeader("Content-Disposition", "attachment; filename=" & csvFileName)
        '        Response.Flush()
        '        Response.Write("<b>File Contents: </b>")
        '        Response.BinaryWrite(Buffer)
        '        Response.WriteFile(outputPath)
        '        Response.End()

        '    Catch ex As System.Threading.ThreadAbortException
        '        Response.End()の呼び出しによりエラーメッセージを出力しないようにする

        '    Catch ex As Exception
        '        Call showMsg("Failed  to download prcess.")
        '    End Try

        'End If

    End Sub


    Protected Sub btnSend_Click(sender As Object, e As EventArgs) Handles btnSend.Click
        Dim shipCode As String = Session("ship_code")
        Dim setMon As String = Session("set_Mon")
        Dim setYear As String = Session("set_Year")
        Dim clsSetCommon As New Class_common
        Dim dtNow As DateTime = clsSetCommon.dtIndia
        Dim monDate As String = setYear & "/" & Trim(lblMonNow.Text)

        If RadioAll.Checked = True Then

            'Dim strSQL As String = "select day + '  ' +   datename(weekday,(convert(datetime, month + '/' + day)) ) as day, 'service' as type,customer_visit_S as CustomerVisit,"
            'strSQL &= "Call_Registerd_S as CallRegistered,Repair_Completed_S as RepairComplete,Goods_Delivered_S as GoodsDelivered,Pending_Calls_S as PendingCalls, Cancelled_Calls_S as Cancel,Reservation_S as Reservation"
            'strSQL &= " From SONY_ACTIVITY_REPORT WHERE DELFG = 0 AND Month = '" & monDate & "' AND location = '" & shipCode & "'"
            'strSQL &= " UNION "
            'strSQL &= "select day + '  ' +   datename(weekday,(convert(datetime, month + '/' + day)) ) as day, 'D&I'as type,customer_visit_D as CustomerVisit,"
            'strSQL &= "Call_Registerd_D as CallRegistered,Repair_Completed_D as RepairComplete,Goods_Delivered_D as GoodsDelivered,Pending_Calls_D as PendingCalls, Cancelled_Calls_D as Cancel,Reservation_D as Reservation"
            'strSQL &= " From SONY_ACTIVITY_REPORT WHERE DELFG = 0 AND Month = '" & monDate & "' AND location = '" & shipCode & "'"
            'strSQL &= " UNION "
            'strSQL &= "Select day + '  ' +   datename(weekday,(convert(datetime, month + '/' + day)) ) as day, 'Total'as type, sum (customer_visit_S  + customer_visit_D)as CustomerVisit ,"
            'strSQL &= "sum(Call_Registerd_S + Call_Registerd_D)As CallRegistered, sum(Repair_Completed_S+Repair_Completed_D) As RepairComplete,"
            'strSQL &= "sum(Goods_Delivered_S + Goods_Delivered_D)As GoodsDelivered,sum(Pending_Calls_S+Pending_Calls_D)As PendingCalls,sum(Cancelled_Calls_S+Cancelled_Calls_D) As Cancel, sum(Reservation_S+Reservation_D) As Reservation"
            'strSQL &= " From SONY_ACTIVITY_REPORT  Group By day + '  ' +  datename(weekday,(convert(datetime, month + '/' + day)) ) order by day + '  ' +  datename(weekday,(convert(datetime, month + '/' + day)) ) ;"


            Dim strSQL As String = "select Day + '  ' +   datename(weekday,(convert(datetime, month + '/' + day)) ) as Day, 'Service' as Type,'1' seq, customer_visit_S as CustomerVisit,"
            strSQL &= "Call_Registerd_S as CallRegistered,Repair_Completed_S as RepairComplete,Goods_Delivered_S as GoodsDelivered,Pending_Calls_S as PendingCalls, Cancelled_Calls_S as Cancel,Reservation_S as Reservation"
            strSQL &= " From SONY_ACTIVITY_REPORT WHERE DELFG = 0 AND Month = '" & monDate & "' AND location = '" & shipCode & "'"
            strSQL &= " UNION "
            strSQL &= "select Day + '  ' +   datename(weekday,(convert(datetime, month + '/' + day)) ) as Day, 'D&I'as Type, '2' seq, customer_visit_D as CustomerVisit,"
            strSQL &= "Call_Registerd_D as CallRegistered,Repair_Completed_D as RepairComplete,Goods_Delivered_D as GoodsDelivered,Pending_Calls_D as PendingCalls, Cancelled_Calls_D as Cancel,Reservation_D as Reservation"
            strSQL &= " From SONY_ACTIVITY_REPORT WHERE DELFG = 0 AND Month = '" & monDate & "' AND location = '" & shipCode & "'"
            strSQL &= " UNION "
            strSQL &= "Select Day + '  ' +   datename(weekday,(convert(datetime, month + '/' + day)) ) as Day, 'Total'as Type, '3' seq, sum (customer_visit_S  + customer_visit_D)as CustomerVisit ,"
            strSQL &= "sum(Call_Registerd_S + Call_Registerd_D)As CallRegistered, sum(Repair_Completed_S+Repair_Completed_D) As RepairComplete,"
            strSQL &= "sum(Goods_Delivered_S + Goods_Delivered_D)As GoodsDelivered,sum(Pending_Calls_S+Pending_Calls_D)As PendingCalls,sum(Cancelled_Calls_S+Cancelled_Calls_D) As Cancel, sum(Reservation_S+Reservation_D) As Reservation"
            strSQL &= " From SONY_ACTIVITY_REPORT WHERE DELFG = 0 AND Month = '" & monDate & "' AND location = '" & shipCode & "'  Group By day + '  ' +  datename(weekday,(convert(datetime, month + '/' + day)) ) order by day, seq  ;"




            If strSQL <> "" Then
                Dim errFlg As Integer
                Dim DT_SERCH As DataTable = DBCommon.ExecuteGetDT(strSQL, errFlg)
                'DT_SERCH.Columns.Remove("seq")
                'DT_SERCH.Columns.RemoveAt(2)

                If (DT_SERCH Is Nothing) Then
                    Call showMsg("There was no relevant information")
                    Exit Sub
                ElseIf (DT_SERCH.Rows.Count > 0 And DT_SERCH IsNot Nothing) Then
                    DT_SERCH.Columns.RemoveAt(2)
                End If

                If errFlg = 1 Then
                    Call showMsg("Failed to acquire search information.")
                    Exit Sub
                End If

                If DT_SERCH IsNot Nothing Then
                    GridInfo.DataSource = DT_SERCH
                    'GridInfo.Columns(2).Visible = False
                    'GridInfo.Columns(2).Visible = TryCast(sender, e)


                    'If GridInfo.Rows = DataControlRowType.Header Then
                    '    e.Row.Cells(2).Visible = False
                    'End If
                    'If e.Row.RowType = DataControlRowType.DataRow Then
                    '    e.Row.Cells(2).Visible = False
                    'End If

                    'If (GridInfo.Columns.("seq")) Then

                    '    GridInfo.Columns(2).Visible = False
                    'End If



                    GridInfo.DataBind()

                    'If GridInfo.Columns.Count > 0 Then
                    '    GridInfo.Columns(2).Visible = False
                    'End If

                    '次ページ反映ように取得しておく
                    Session("Data_DT_SERCH") = DT_SERCH

                    For i As Integer = GridInfo.Rows.Count - 1 To 1 Step -1
                        Dim row As GridViewRow = GridInfo.Rows(i)
                        Dim previousRow As GridViewRow = GridInfo.Rows(i - 1)
                        For j As Integer = 0 To row.Cells.Count - 1
                            If row.Cells(j).Text = previousRow.Cells(j).Text Then
                                If previousRow.Cells(j).RowSpan = 0 And j = 0 Then
                                    If row.Cells(j).RowSpan = 0 And j = 0 Then
                                        previousRow.Cells(j).RowSpan += 2
                                    Else
                                        previousRow.Cells(j).RowSpan = row.Cells(j).RowSpan + 1
                                    End If
                                    row.Cells(j).Visible = False
                                End If
                            End If
                        Next
                    Next

                    btnDown.Visible = True
                Else
                    GridInfo.DataSource = Nothing
                    GridInfo.DataBind()
                    btnDown.Visible = False
                End If

            End If

        End If

        If RadioService.Checked = True Then

            Dim strSQL As String = "SET LANGUAGE us_english;SELECT Day + '  ' +   datename(weekday,(convert(datetime, month + '/' + day)) ) as Day,'Service' as Type, Customer_Visit_S as CustomerVisit, Call_Registerd_S as CallRegistered, Repair_Completed_S as RepairComplete,Goods_Delivered_S as GoodsDelivered, Pending_Calls_S as PendingCalls, Cancelled_Calls_S as Cancel,Reservation_S as Reservation "
            strSQL &= "FROM dbo.SONY_ACTIVITY_REPORT WHERE DELFG = 0 AND Month = '" & monDate & "' AND location = '" & shipCode & "' "
            strSQL &= "ORDER BY day;"

            'Dim strSQL As String = "SET LANGUAGE us_english;SELECT day + '  ' +   datename(weekday,(convert(datetime, month + '/' + day)) ) as day,'Service' as type, Customer_Visit_S as CustomerVisit, Call_Registerd_S as CallRegistered, Repair_Completed_S as RepairComplete,Goods_Delivered_S as GoodsDelivered, Pending_Calls_S as PendingCalls, Cancelled_Calls_S as Cancel,Reservation_S as Reservation "
            'strSQL &= "FROM dbo.SONY_ACTIVITY_REPORT "
            'strSQL &= "ORDER BY day;"

            If strSQL <> "" Then
                Dim errFlg As Integer
                Dim DT_SERCH As DataTable = DBCommon.ExecuteGetDT(strSQL, errFlg)

                If errFlg = 1 Then
                    Call showMsg("Failed to acquire search information.")
                    Exit Sub
                End If

                If DT_SERCH IsNot Nothing Then
                    GridInfo.DataSource = DT_SERCH
                    GridInfo.DataBind()
                    '次ページ反映ように取得しておく
                    Session("Data_DT_SERCH") = DT_SERCH
                    btnDown.Visible = True
                Else
                    GridInfo.DataSource = Nothing
                    GridInfo.DataBind()
                    btnDown.Visible = False
                End If

            End If
        End If

        If RadioD_I.Checked = True Then

            Dim strSQL As String = "SELECT Day + '  ' +   datename(weekday,(convert(datetime, month + '/' + day)) ) as Day,'D&I' as Type,Customer_Visit_D as CustomerVisit, Call_Registerd_D as CallRegistered, Repair_Completed_D as RepairComplete,Goods_Delivered_D as GoodsDelivered, Pending_Calls_D as PendingCalls, Cancelled_Calls_D as Cancel,Reservation_D as Reservation "
            strSQL &= "FROM dbo.SONY_ACTIVITY_REPORT WHERE DELFG = 0 AND Month = '" & monDate & "' AND location = '" & shipCode & "' "
            strSQL &= "ORDER BY day;"

            If strSQL <> "" Then
                Dim errFlg As Integer
                Dim DT_SERCH As DataTable = DBCommon.ExecuteGetDT(strSQL, errFlg)

                If errFlg = 1 Then
                    Call showMsg("Failed to acquire search information.")
                    Exit Sub
                End If

                If DT_SERCH IsNot Nothing Then
                    GridInfo.DataSource = DT_SERCH
                    GridInfo.DataBind()
                    '次ページ反映ように取得しておく
                    Session("Data_DT_SERCH") = DT_SERCH
                    btnDown.Visible = True
                Else
                    GridInfo.DataSource = Nothing
                    GridInfo.DataBind()
                    btnDown.Visible = False
                End If

            End If

        End If


        If RadioTotal.Checked = True Then

            'Dim strSQL As String = "Select day + '  ' +   datename(weekday,(convert(datetime, month + '/' + day)) ) as day, 'Total'as type,sum (customer_visit_S  + customer_visit_D)as CustomerVisit ,"
            'strSQL &= "sum(Call_Registerd_S + Call_Registerd_D)As CallRegistered, sum(Repair_Completed_S+Repair_Completed_D) As RepairComplete,"
            'strSQL &= "sum(Goods_Delivered_S + Goods_Delivered_D)As GoodsDelivered,sum(Pending_Calls_S+Pending_Calls_D)As PendingCalls,sum(Cancelled_Calls_S+Cancelled_Calls_D) As Cancel, sum(Reservation_S+Reservation_D) As Reservation"
            'strSQL &= " From SONY_ACTIVITY_REPORT  Group By day + '  ' +  datename(weekday,(convert(datetime, month + '/' + day)) ) order by day + '  ' +  datename(weekday,(convert(datetime, month + '/' + day)) ) ;"


            Dim strSQL As String = "Select Day + '  ' +   datename(weekday,(convert(datetime, month + '/' + day)) ) as Day, 'Total'as Type,sum (customer_visit_S  + customer_visit_D)as CustomerVisit ,"
            strSQL &= "sum(Call_Registerd_S + Call_Registerd_D)As CallRegistered, sum(Repair_Completed_S+Repair_Completed_D) As RepairComplete,"
            strSQL &= "sum(Goods_Delivered_S + Goods_Delivered_D)As GoodsDelivered,sum(Pending_Calls_S+Pending_Calls_D)As PendingCalls,sum(Cancelled_Calls_S+Cancelled_Calls_D) As Cancel, sum(Reservation_S+Reservation_D) As Reservation"
            strSQL &= " From SONY_ACTIVITY_REPORT WHERE DELFG = 0 AND Month = '" & monDate & "' AND location = '" & shipCode & "' Group By day + '  ' +  datename(weekday,(convert(datetime, month + '/' + day)) ) order by day ;"




            If strSQL <> "" Then
                Dim errFlg As Integer
                Dim DT_SERCH As DataTable = DBCommon.ExecuteGetDT(strSQL, errFlg)

                If errFlg = 1 Then
                    Call showMsg("Failed to acquire search information.")
                    Exit Sub
                End If

                If DT_SERCH IsNot Nothing Then
                    GridInfo.DataSource = DT_SERCH
                    GridInfo.DataBind()
                    '次ページ反映ように取得しておく
                    Session("Data_DT_SERCH") = DT_SERCH
                    btnDown.Visible = True
                Else
                    GridInfo.DataSource = Nothing
                    GridInfo.DataBind()
                    btnDown.Visible = False
                End If

            End If

        End If

    End Sub

    Protected Sub RadioAll_CheckedChanged(sender As Object, e As EventArgs) Handles RadioAll.CheckedChanged
        If RadioAll.Checked = True Then
            RadioService.Checked = False
            RadioD_I.Checked = False
            RadioTotal.Checked = False
        End If
    End Sub

    Protected Sub RadioService_CheckedChanged(sender As Object, e As EventArgs) Handles RadioService.CheckedChanged
        If RadioService.Checked = True Then
            RadioAll.Checked = False
            RadioD_I.Checked = False
            RadioTotal.Checked = False

        End If
    End Sub

    Protected Sub RadioD_I_CheckedChanged(sender As Object, e As EventArgs) Handles RadioD_I.CheckedChanged
        If RadioD_I.Checked = True Then
            RadioAll.Checked = False
            RadioService.Checked = False
            RadioTotal.Checked = False
        End If
    End Sub

    Protected Sub RadioTotal_CheckedChanged(sender As Object, e As EventArgs) Handles RadioTotal.CheckedChanged
        If RadioTotal.Checked = True Then
            RadioAll.Checked = False
            RadioService.Checked = False
            RadioD_I.Checked = False
        End If
    End Sub


End Class