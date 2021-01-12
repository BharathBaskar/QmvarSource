﻿Public Class Site6
    Inherits System.Web.UI.MasterPage

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim userid As String = Session("user_id")
        Dim setShipname As String = Session("ship_Name")
        Dim userName As String = Session("user_Name")

        If setShipname IsNot Nothing Then
            LblShipName.Text = setShipname
        End If

        If userid IsNot Nothing Then
            lblUser.Text = userid & " " & userName
        End If

    End Sub

    Protected Sub BtnHome_Click(sender As Object, e As ImageClickEventArgs) Handles BtnHome.Click

        Call repair_reSetSession()

        Dim userLevel As String = Session("user_level")
        Dim adminFlg As Boolean = Session("admin_Flg")

        If userLevel = "0" Or userLevel = "1" Or userLevel = "2" Or adminFlg = True Then
            Response.Redirect("Menu.aspx")
        Else
            Response.Redirect("Menu2.aspx")
        End If

    End Sub

    Protected Sub BtnLogof_Click(sender As Object, e As ImageClickEventArgs) Handles BtnLogof.Click
        Call repair_reSetSession()
        Response.Redirect("Login.aspx")
    End Sub

    'セッション開放
    Protected Sub repair_reSetSession()

        '後始末　サーバ側に保存された不要なCSVファイルを削除
        Dim csvFileName As String = Session("csv_FileName")

        If csvFileName <> "" Then
            If System.IO.File.Exists(csvFileName) = True Then
                System.IO.File.Delete(csvFileName)
            End If
        End If

        Session("csv_FileName") = Nothing

    End Sub

    Protected Sub btnSearch_Click(sender As Object, e As ImageClickEventArgs) Handles btnSearch.Click
        Call repair_reSetSession()
        Response.Redirect("Repair_2.aspx")
    End Sub

    Protected Sub btnRecordGspn_Click(sender As Object, e As ImageClickEventArgs) Handles btnRecordGspn.Click
        Call repair_reSetSession()
        Response.Redirect("Money_Record_1.aspx")
    End Sub

End Class