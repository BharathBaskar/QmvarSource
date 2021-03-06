﻿Imports System
Imports System.Data
Imports System.Web.Security
Imports System.Collections.Generic
Imports Ganges33.Ganges33.dao
Imports Ganges33.Ganges33.model
Imports Ganges33.Ganges33.logic

Public Class SonyOutBoundControl
    Public Function AddModifyOutBound(ByVal csvData()() As String, queryParams As SonyOutBoundModel) As Boolean
        Log4NetControl.ComInfoLogWrite(Log4NetControl.UserID)
        'Mandatory Column 39 from CSV
        Dim flag As Boolean = True
        If csvData(0).Length < 16 Then
            Return False
        End If

        '       Log4NetControl.ComInfoLogWrite(Log4NetControl.UserID)
        Dim DateTimeNow As DateTime = DateTime.Now
        Dim dtNow As DateTime = DateTimeNow.AddMinutes(ConfigurationManager.AppSettings("TimeDiff"))
        Dim dbConn As DBUtility = New DBUtility()
        Dim dt As DataTable = New DataTable()
        dbConn.sqlTrn = dbConn.sqlConn.BeginTransaction()
        dbConn.sqlCmd.Transaction = dbConn.sqlTrn
        '       Dim flag As Boolean = True
        Dim flagAll As Boolean = True
        Dim sqlStr As String = ""
        Dim dtSawDiscountExist As DataTable

        Dim isExist As Integer = 0
        '1st check PARTS_NO
        sqlStr = "SELECT TOP 1 PART_NO FROM SONY_OUT_BOUND "
        sqlStr = sqlStr & " WHERE DELFG = 0 AND SRC_FILE_NAME='" & queryParams.SRC_FILE_NAME & "'"
        'sqlStr = sqlStr & " AND SHIP_TO_BRANCH_CODE='" & queryParams.ShipToBranchCode & "'"
        dtSawDiscountExist = dbConn.GetDataSet(sqlStr)
        'if exist then need to update delfg=1 then insert the record as new
        If (dtSawDiscountExist Is Nothing) Or (dtSawDiscountExist.Rows.Count = 0) Then
            'isExist = 0
        Else 'The records is already exist, need to update DELFg=0
            ' isExist = 1
            sqlStr = "UPDATE SONY_OUT_BOUND SET DELFG=1  "
            sqlStr = sqlStr & "WHERE DELFG=0 "
            sqlStr = sqlStr & "AND SRC_FILE_NAME = @SRC_FILE_NAME"
            'sqlStr = sqlStr & "AND SHIP_TO_BRANCH_CODE = @SHIP_TO_BRANCH_CODE "
            dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@SRC_FILE_NAME", queryParams.SRC_FILE_NAME))
            flag = dbConn.ExecSQL(sqlStr)
            dbConn.sqlCmd.Parameters.Clear()
            'If Error occurs then will store the flag as false
            If Not flag Then
                flagAll = False
            End If
        End If
        For i = 0 To csvData.Length - 1
            If i > 0 Then '0  Header
                'If isExist = 1 Then
                sqlStr = "Insert into SONY_OUT_BOUND ("
                sqlStr = sqlStr & "CRTDT, "
                sqlStr = sqlStr & "CRTCD, "
                ' sqlStr = sqlStr & "UPDDT, "
                sqlStr = sqlStr & "UPDCD, "
                sqlStr = sqlStr & "UPDPG, "
                sqlStr = sqlStr & "DELFG, "
                '             sqlStr = sqlStr & "UNQ_NO, "
                sqlStr = sqlStr & "UPLOAD_USER, "
                sqlStr = sqlStr & "UPLOAD_DATE, "
                sqlStr = sqlStr & "SHIP_TO_BRANCH_CODE, "
                sqlStr = sqlStr & "SHIP_TO_BRANCH, "

                sqlStr = sqlStr & "PART_NO, "
                sqlStr = sqlStr & "PART_DESC, "
                sqlStr = sqlStr & "QUANTITY, "
                sqlStr = sqlStr & "COST, "
                sqlStr = sqlStr & "REF_NUMBER, "
                sqlStr = sqlStr & "OUTBOUND_DATE, "
                sqlStr = sqlStr & "OUTBOUND_WAY, "
                sqlStr = sqlStr & "REQUEST_BY, "
                sqlStr = sqlStr & "PIC, "
                sqlStr = sqlStr & "ACTUAL_SELLING_UNIT_PRICE, "
                sqlStr = sqlStr & "OW_BASE_PRICE, "
                sqlStr = sqlStr & "CUSTOMER_NAME, "
                sqlStr = sqlStr & "FEE_TYPE, "
                sqlStr = sqlStr & "PART_REGISTERATION_MODEL, "
                sqlStr = sqlStr & "REMARK, "
                sqlStr = sqlStr & "FORMNO, "


                sqlStr = sqlStr & "FILE_NAME, "
                sqlStr = sqlStr & "SRC_FILE_NAME "
                sqlStr = sqlStr & " ) "
                sqlStr = sqlStr & " values ( "
                sqlStr = sqlStr & "@CRTDT, "
                sqlStr = sqlStr & "@CRTCD, "
                'sqlStr = sqlStr & "@UPDDT, "
                sqlStr = sqlStr & "@UPDCD, "
                sqlStr = sqlStr & "@UPDPG, "
                sqlStr = sqlStr & "@DELFG, "
                '              sqlStr = sqlStr & " (select max(UNQ_NO)+1 from SAW_DISCOUNT) , "
                sqlStr = sqlStr & "@UPLOAD_USER, "
                sqlStr = sqlStr & "@UPLOAD_DATE, "
                sqlStr = sqlStr & "@SHIP_TO_BRANCH_CODE, "
                sqlStr = sqlStr & "@SHIP_TO_BRANCH, "


                sqlStr = sqlStr & "@PART_NO, "
                sqlStr = sqlStr & "@PART_DESC, "
                sqlStr = sqlStr & "@QUANTITY, "
                sqlStr = sqlStr & "@COST, "
                sqlStr = sqlStr & "@REF_NUMBER, "
                sqlStr = sqlStr & "@OUTBOUND_DATE, "
                sqlStr = sqlStr & "@OUTBOUND_WAY, "
                sqlStr = sqlStr & "@REQUEST_BY, "
                sqlStr = sqlStr & "@PIC, "
                sqlStr = sqlStr & "@ACTUAL_SELLING_UNIT_PRICE, "
                sqlStr = sqlStr & "@OW_BASE_PRICE, "
                sqlStr = sqlStr & "@CUSTOMER_NAME, "
                sqlStr = sqlStr & "@FEE_TYPE, "
                sqlStr = sqlStr & "@PART_REGISTERATION_MODEL, "
                sqlStr = sqlStr & "@REMARK, "
                sqlStr = sqlStr & "@FORMNO, "






                sqlStr = sqlStr & "@FILE_NAME, "
                sqlStr = sqlStr & "@SRC_FILE_NAME "
                sqlStr = sqlStr & " )"
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@CRTDT", dtNow))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@CRTCD", queryParams.UserId))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@UPDCD", "")) '?????????????????????????
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@UPDPG", queryParams.UPDPG))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@DELFG", 0))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@UPLOAD_USER", queryParams.UPLOAD_USER))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@UPLOAD_DATE", dtNow))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@SHIP_TO_BRANCH_CODE", queryParams.SHIP_TO_BRANCH_CODE))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@SHIP_TO_BRANCH", queryParams.SHIP_TO_BRANCH))

                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@PART_NO", csvData(i)(0)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@PART_DESC", csvData(i)(1)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@QUANTITY", csvData(i)(2)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@COST", csvData(i)(3)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@REF_NUMBER", csvData(i)(4)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@OUTBOUND_DATE", csvData(i)(5)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@OUTBOUND_WAY", csvData(i)(6)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@REQUEST_BY", csvData(i)(7)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@PIC", csvData(i)(8)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@ACTUAL_SELLING_UNIT_PRICE", csvData(i)(9)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@OW_BASE_PRICE", csvData(i)(10)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@CUSTOMER_NAME", csvData(i)(11)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@FEE_TYPE", csvData(i)(12)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@PART_REGISTERATION_MODEL", csvData(i)(13)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@REMARK", csvData(i)(14)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@FORMNO", csvData(i)(15)))




                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@FILE_NAME", queryParams.FILE_NAME))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@SRC_FILE_NAME", queryParams.SRC_FILE_NAME))

                flag = dbConn.ExecSQL(sqlStr)
                dbConn.sqlCmd.Parameters.Clear()
                'If Error occurs then will store the flag as false
                If Not flag Then
                    flagAll = False
                    Exit For
                End If
                'End If
            End If 'Other than header - End
        Next
        If flagAll Then
            flag = True
            dbConn.sqlTrn.Commit()
        Else
            flag = False
            dbConn.sqlTrn.Rollback()
        End If
        dbConn.CloseConnection()
        Return flag
    End Function

    Public Function SelectOutBound(ByVal queryParams As SonyOutBoundModel) As DataTable
        Log4NetControl.ComInfoLogWrite(Log4NetControl.UserID)
        Dim dbConn As DBUtility = New DBUtility()
        Dim dt As DataTable = New DataTable()
        Dim sqlStr As String = "SELECT "
        sqlStr = sqlStr & "PART_NO as 'Part No.',PART_DESC as 'Part Desc',QUANTITY as 'Quantity',COST as 'Cost',REF_NUMBER as 'Ref. Number',OUTBOUND_DATE as 'Outbound Date',OUTBOUND_WAY as 'Outbound Way',REQUEST_BY as 'Request By',PIC as 'PIC',ACTUAL_SELLING_UNIT_PRICE as 'Actual Selling Unit Price', OW_BASE_PRICE as 'OW Base Price',CUSTOMER_NAME as 'Customer Name',FEE_TYPE as 'Fee Type',PART_REGISTERATION_MODEL as 'Part Registeration Model',REMARK as 'Remark',FORMNO as 'FormNo' "
        sqlStr = sqlStr & "FROM "
        sqlStr = sqlStr & "SONY_OUT_BOUND "
        sqlStr = sqlStr & "WHERE "
        sqlStr = sqlStr & "DELFG=0 "

        If Not String.IsNullOrEmpty(queryParams.SHIP_TO_BRANCH_CODE) Then
            sqlStr = sqlStr & "AND SHIP_TO_BRANCH_CODE = @SHIP_TO_BRANCH_CODE "
            dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@SHIP_TO_BRANCH_CODE", queryParams.SHIP_TO_BRANCH_CODE))
        End If

        If Not String.IsNullOrEmpty(queryParams.SHIP_TO_BRANCH) Then
            sqlStr = sqlStr & "AND SHIP_TO_BRANCH = @SHIP_TO_BRANCH "
            dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@SHIP_TO_BRANCH", queryParams.SHIP_TO_BRANCH))
        End If

        If (Not (String.IsNullOrEmpty(queryParams.DateFrom)) And (Not (String.IsNullOrEmpty(queryParams.DateTo)))) Then
            sqlStr = sqlStr & "AND LEFT(CONVERT(VARCHAR, CRTDT, 111), 10) >= @DateFrom and LEFT(CONVERT(VARCHAR, CRTDT, 111), 10) <= @DateTo "
            'sqlStr = sqlStr & "AND INVOICE_DATE >= @DateFrom and INVOICE_DATE <= @DateTo "
            dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@DateFrom", queryParams.DateFrom))
            dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@DateTo", queryParams.DateTo))
        ElseIf Not String.IsNullOrEmpty(queryParams.DateFrom) Then
            'sqlStr = sqlStr & "AND LEFT(CONVERT(VARCHAR, INVOICE_DATE, 111), 10) = @DateFrom "
            sqlStr = sqlStr & "AND CRTDT = @DateFrom "
            dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@DateFrom", queryParams.DateFrom))
        ElseIf Not String.IsNullOrEmpty(queryParams.DateTo) Then
            sqlStr = sqlStr & "AND CRTDT = @DateTo "
            'sqlStr = sqlStr & "AND LEFT(CONVERT(VARCHAR, INVOICE_DATE, 111), 10) = @DateTo "
            dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@DateTo", queryParams.DateTo))
        End If

        Dim _DataTable As DataTable = dbConn.GetDataSet(sqlStr)
        dbConn.CloseConnection()
        Return _DataTable
    End Function
End Class
