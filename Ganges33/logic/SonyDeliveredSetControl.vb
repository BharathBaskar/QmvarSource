Imports System
Imports System.Data
Imports System.Web.Security
Imports System.Collections.Generic
Imports Ganges33.Ganges33.dao
Imports Ganges33.Ganges33.model
Imports Ganges33.Ganges33.logic
Imports System.Data.SqlClient

Public Class SonyDeliveredSetControl
    Public Function AddModifyDeliveredSet(ByVal csvData()() As String, queryParams As SonyDeliveredSetModel) As Boolean

        Log4NetControl.ComInfoLogWrite(Log4NetControl.UserID)
        'Return Status 
        Dim flag As Boolean = False
        'Ship Code
        Dim strShipCode As String = ""
        Try
            'Reading ShipCode from database to Datatable 
            Dim _ShipBaseControl As ShipBaseControl = New ShipBaseControl()
            Dim dtShipCode As DataTable = _ShipBaseControl.SelectBranchCode()

            Dim blShipCodeExist As Boolean = False
            Dim strDate As String = ""

            'Define Database 
            Dim DateTimeNow As DateTime = DateTime.Now
            Dim dtNow As DateTime = DateTimeNow.AddMinutes(ConfigurationManager.AppSettings("TimeDiff"))
            Dim con As SqlConnection = New SqlConnection(ConfigurationManager.ConnectionStrings("cnstr").ConnectionString)
            'Open Database

            con.Open()
            Dim trn As SqlTransaction = con.BeginTransaction(IsolationLevel.ReadCommitted)
            Try
                Dim newData As Integer
                Dim add As Integer
                For i = 0 To csvData.Length - 1
                    If i <> 0 Then
                        blShipCodeExist = False
                        ''Ship Code
                        'strShipCode = CommonControl.ConvertShipCode(queryParams.SHIP_TO_BRANCH_CODE)

                        strShipCode = queryParams.SHIP_TO_BRANCH_CODE
                        blShipCodeExist = CommonControl.CheckDataTable(dtShipCode, Function(x) x("ship_code") = strShipCode)

                        If blShipCodeExist = False Then
                            Log4NetControl.ComErrorLogWrite(strShipCode & " is not exist")
                            flag = False
                            trn.Rollback()
                            Return flag
                            Exit Function
                        End If

                        'Bydefault Assing Zero
                        add = 0
                        newData = 0
                        'Query 
                        'If Record is exit then it will update otherwise it will create new records
                        Dim select_sql1 As String = ""
                        select_sql1 = "SELECT * FROM SONY_DELIVERED_SET WHERE DELFG = 0 "
                        select_sql1 &= "AND SHIP_TO_BRANCH_CODE = '" & strShipCode & "' "
                        select_sql1 &= "AND JOB_ID = '" & csvData(i)(4) & "' "

                        'DataAdpater and DataSet Define
                        Dim sqlSelect1 As New SqlCommand(select_sql1, con, trn)
                        Dim Adapter1 As New SqlDataAdapter(sqlSelect1)
                        Dim Builder1 As New SqlCommandBuilder(Adapter1)
                        Dim ds1 As New DataSet
                        Dim dr1 As DataRow
                        Adapter1.Fill(ds1)
                        'If Record exist update otherwise insert as new record
                        If ds1.Tables(0).Rows.Count = 1 Then
                            dr1 = ds1.Tables(0).Rows(0)
                            add = 1
                        Else
                            dr1 = ds1.Tables(0).NewRow
                            newData = 1
                        End If

                        If newData = 1 Then
                            dr1("CRTDT") = dtNow
                            dr1("CRTCD") = queryParams.UserId
                        End If

                        If add = 1 Then
                            dr1("UPDDT") = dtNow
                            dr1("UPDCD") = queryParams.UserId
                        End If


                        dr1("UPDPG") = "PrSummaryControl.vb"
                        dr1("DELFG") = 0
                        dr1("UPLOAD_USER") = queryParams.UPLOAD_USER
                        dr1("UPLOAD_DATE") = dtNow

                        dr1("SHIP_TO_BRANCH_CODE") = strShipCode ''csvData(i)(0)
                        dr1("SHIP_TO_BRANCH") = queryParams.SHIP_TO_BRANCH

                        'strDate = Left(csvData(i)(2), 4) & "/" & Mid(csvData(i)(2), 5, 2) & "/" & Right(csvData(i)(2), 2)

                        dr1("REGION") = csvData(i)(0)
                        dr1("ASC_LEVEL") = csvData(i)(1)
                        dr1("ASC_CODE") = csvData(i)(2)
                        dr1("ASC_NAME") = csvData(i)(3)
                        dr1("JOB_ID") = csvData(i)(4)
                        dr1("CUSTOMER_GROUP") = csvData(i)(5)
                        dr1("MANUFACTURE") = csvData(i)(6)
                        dr1("WARRANTY_TYPE") = csvData(i)(7)
                        dr1("WARRANTY_CATEGORY") = csvData(i)(8)
                        dr1("SERVICE_TYPE") = csvData(i)(9)
                        dr1("PRODUCT_CATEGORY_NAME") = csvData(i)(10)
                        dr1("PRODUCT_SUB_CATEGORY_NAME") = csvData(i)(11)
                        dr1("SET_MODEL") = csvData(i)(12)
                        dr1("MODEL_NAME") = csvData(i)(13)
                        dr1("SERIAL_NO") = csvData(i)(14)
                        dr1("DEALER_NAME") = csvData(i)(15)

                        Dim convtDate As DateTime
                        If DateTime.TryParse(csvData(i)(16), convtDate) Then
                            dr1("PURCHASED_DATE") = csvData(i)(16)
                        End If

                        If DateTime.TryParse(csvData(i)(17), convtDate) Then
                            dr1("RESERVATION_CREATE_DATE") = csvData(i)(17)
                        End If

                        If DateTime.TryParse(csvData(i)(18), convtDate) Then
                            dr1("APPOINTMENT_DATE") = csvData(i)(18)
                        End If

                        If DateTime.TryParse(csvData(i)(19), convtDate) Then
                            dr1("BOOKED_IN_DATE") = csvData(i)(19)
                        End If

                        If DateTime.TryParse(csvData(i)(20), convtDate) Then
                            dr1("REPAIR_COMPLETED_DATE") = csvData(i)(20)
                        End If

                        dr1("REPAIR_STATUS") = csvData(i)(21)

                        If DateTime.TryParse(csvData(i)(22), convtDate) Then
                            dr1("COLLECT_DATE") = csvData(i)(22)
                        End If

                        dr1("RECEPTIONIST") = csvData(i)(23)
                        dr1("TECHNICIAN") = csvData(i)(24)
                        dr1("ONSITE_PEOPLE") = csvData(i)(25)
                        dr1("DISPATCHED_BY") = csvData(i)(26)
                        dr1("LOT_NO") = csvData(i)(27)
                        dr1("WARRANTY_NO") = csvData(i)(28)
                        dr1("REPAIR_LEVEL") = csvData(i)(29)
                        Dim convtDecimal As Decimal
                        If Decimal.TryParse(csvData(i)(30), convtDecimal) Then
                            dr1("TOTAL_AMOUNT_OF_ACCOUNT_PAYABLE") = csvData(i)(30)
                        End If

                        If Decimal.TryParse(csvData(i)(31), convtDecimal) Then
                            dr1("ACCOUNT_PAYABLE_BY_CUSTOMER") = csvData(i)(31)
                        End If

                        If Decimal.TryParse(csvData(i)(32), convtDecimal) Then
                            dr1("SONY_NEEDS_TO_PAY") = csvData(i)(32)
                        End If

                        If Decimal.TryParse(csvData(i)(33), convtDecimal) Then
                            dr1("ACCOUNT_PAYABLE_BY_ASC") = csvData(i)(33)
                        End If


                        dr1("CUSTOMER_NAME") = csvData(i)(34)
                        dr1("CONTACT_NO") = csvData(i)(35)
                        dr1("MOBILE_NO") = csvData(i)(36)
                        dr1("CITY") = csvData(i)(37)
                        dr1("STATE") = csvData(i)(38)
                        dr1("POSTAL_CODE") = csvData(i)(39)
                        Dim convtInt As Integer
                        If Integer.TryParse(csvData(i)(40), convtInt) Then
                            dr1("PENDIND_DELIVERT_AGE") = csvData(i)(40)
                        End If

                        If Decimal.TryParse(csvData(i)(41), convtDecimal) Then
                            dr1("TOTAL_PART_FEE") = csvData(i)(41)
                        End If

                        If Decimal.TryParse(csvData(i)(42), convtDecimal) Then
                            dr1("LABOUR_FEE") = csvData(i)(42)
                        End If

                        If Decimal.TryParse(csvData(i)(43), convtDecimal) Then
                            dr1("INSPECTION_FEE") = csvData(i)(43)
                        End If

                        If Decimal.TryParse(csvData(i)(44), convtDecimal) Then
                            dr1("HANDLING_FEE") = csvData(i)(44)
                        End If

                        If Decimal.TryParse(csvData(i)(45), convtDecimal) Then
                            dr1("TRANSPORT_FEE") = csvData(i)(45)
                        End If

                        If Decimal.TryParse(csvData(i)(46), convtDecimal) Then
                            dr1("HOMESERVICE_FEE") = csvData(i)(46)
                        End If

                        If Decimal.TryParse(csvData(i)(47), convtDecimal) Then
                            dr1("LONGDISTANCE_FEE") = csvData(i)(47)
                        End If

                        If Decimal.TryParse(csvData(i)(48), convtDecimal) Then
                            dr1("TRAVELALLOWANCE_FEE") = csvData(i)(48)
                        End If

                        If Decimal.TryParse(csvData(i)(49), convtDecimal) Then
                            dr1("DA_FEE") = csvData(i)(49)
                        End If

                        If Decimal.TryParse(csvData(i)(50), convtDecimal) Then
                            dr1("DEMO_CHARGE") = csvData(i)(50)
                        End If

                        If Decimal.TryParse(csvData(i)(51), convtDecimal) Then
                            dr1("INSTALLATION_FEE") = csvData(i)(51)
                        End If

                        If Decimal.TryParse(csvData(i)(52), convtDecimal) Then
                            dr1("ECALL_CHARGE") = csvData(i)(52)
                        End If

                        If Decimal.TryParse(csvData(i)(53), convtDecimal) Then
                            dr1("COMBAT_FEE") = csvData(i)(53)
                        End If

                        dr1("ADDRESS") = csvData(i)(54)
                            dr1("FAX_NO") = csvData(i)(55)
                            dr1("EMAIL_ID") = csvData(i)(56)
                            If DateTime.TryParse(csvData(i)(57), convtDate) Then
                                dr1("LAST_CONTACT_UPDATE_DATE") = csvData(i)(57)
                            End If

                            dr1("LAST_CONTACT_UPDATE_CONTENT") = csvData(i)(58)
                            dr1("REPAIR_SET_BIN_LOCATION") = csvData(i)(59)
                            dr1("HSN_CODE_OF_MODEL") = csvData(i)(60)
                            dr1("PLACE_OF_SUPPLY") = csvData(i)(61)

                            dr1("FILE_NAME") = queryParams.FILE_NAME
                            dr1("SRC_FILE_NAME") = queryParams.SRC_FILE_NAME


                            If newData = 1 Then
                                ds1.Tables(0).Rows.Add(dr1)
                            End If
                            Adapter1.Update(ds1)
                        End If
                Next i

                trn.Commit()
                'Transaction Successful, return true
                flag = True
            Catch ex As Exception
                trn.Rollback()
                Log4NetControl.ComErrorLogWrite(ex.ToString())
                'Failure, return as false
                flag = False
            Finally
                If con.State <> ConnectionState.Closed Then
                    con.Close()
                End If
            End Try

        Catch ex As Exception
            flag = False
            Log4NetControl.ComErrorLogWrite(ex.ToString())
        End Try
        'Return Transaction Status 
        Return flag

    End Function


    Public Function SelectDeliveredSet(ByVal queryParams As SonyDeliveredSetModel) As DataTable
        Log4NetControl.ComInfoLogWrite(Log4NetControl.UserID)
        Dim dbConn As DBUtility = New DBUtility()
        Dim dt As DataTable = New DataTable()
        Dim sqlStr As String = "SELECT "
        sqlStr = sqlStr & "REGION as 'Region', ASC_LEVEL as 'ASC Level', ASC_CODE as 'ASC Code', ASC_NAME as 'ASC Name', JOB_ID as 'Job ID', CUSTOMER_GROUP as 'Customer Group', MANUFACTURE as 'Manufacture', WARRANTY_TYPE as 'Warranty Type', WARRANTY_CATEGORY as 'Warranty Category', SERVICE_TYPE as 'Service Type', PRODUCT_CATEGORY_NAME as 'Product category name', PRODUCT_SUB_CATEGORY_NAME as 'Product sub category name', SET_MODEL as 'Set Model', MODEL_NAME as 'Model Name', SERIAL_NO as 'Serial No', DEALER_NAME as 'Dealer Name', PURCHASED_DATE as 'Purchased Date', RESERVATION_CREATE_DATE as 'Reservation Create Date', APPOINTMENT_DATE as 'Appointment Date', BOOKED_IN_DATE as 'Booked In Date', REPAIR_COMPLETED_DATE as 'Repair Completed Date', REPAIR_STATUS as 'Repair Status', COLLECT_DATE as 'Collect Date', RECEPTIONIST as 'Receptionist', TECHNICIAN as 'Technician', ONSITE_PEOPLE as 'Onsite People', DISPATCHED_BY as 'Dispatched By', LOT_NO as 'Lot No', WARRANTY_NO as 'Warranty No', REPAIR_LEVEL as 'Repair Level', TOTAL_AMOUNT_OF_ACCOUNT_PAYABLE as 'Total Amount Of Account Payable', ACCOUNT_PAYABLE_BY_CUSTOMER as 'Account Payable By Customer', SONY_NEEDS_TO_PAY as 'Sony Needs To Pay', ACCOUNT_PAYABLE_BY_ASC as 'Account Payable By ASC', CUSTOMER_NAME as 'Customer Name', CONTACT_NO as 'Contact No', MOBILE_NO as 'Mobile No', CITY as 'City', STATE as 'State', POSTAL_CODE as 'Postal Code', PENDIND_DELIVERT_AGE as 'Pendind Delivert Age', TOTAL_PART_FEE as 'Total Part Fee', LABOUR_FEE as 'Labour Fee', INSPECTION_FEE as 'Inspection Fee', HANDLING_FEE as 'Handling Fee', TRANSPORT_FEE as 'Transport Fee', HOMESERVICE_FEE as 'Homeservice Fee', LONGDISTANCE_FEE as 'Longdistance Fee', TRAVELALLOWANCE_FEE as 'Travelallowance Fee', DA_FEE as 'Da Fee', DEMO_CHARGE as 'Demo Charge', INSTALLATION_FEE as 'Installation Fee', ECALL_CHARGE as 'ECall Charge', COMBAT_FEE as 'Combat Fee', ADDRESS as 'Address', FAX_NO as 'Fax No', EMAIL_ID as 'E-mail ID', LAST_CONTACT_UPDATE_DATE as 'Last Contact Update Date', LAST_CONTACT_UPDATE_CONTENT as 'Last Contact Update Content', REPAIR_SET_BIN_LOCATION as 'Repair Set Bin Location', HSN_CODE_OF_MODEL as 'HSN Code of Model', PLACE_OF_SUPPLY as 'Place of Supply' "
        sqlStr = sqlStr & "FROM "
        sqlStr = sqlStr & "SONY_DELIVERED_SET "
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


    Public Function SelectPLDeliveredSet(ByVal queryParams As SonyDeliveredSetModel) As DataTable
        Log4NetControl.ComInfoLogWrite(Log4NetControl.UserID)
        Dim dbConn As DBUtility = New DBUtility()
        Dim dt As DataTable = New DataTable()
        Dim sqlStr As String = "SELECT "
        sqlStr = sqlStr & "REGION, ASC_LEVEL, ASC_CODE, ASC_NAME, JOB_ID, CUSTOMER_GROUP , MANUFACTURE, WARRANTY_TYPE, WARRANTY_CATEGORY, SERVICE_TYPE, PRODUCT_CATEGORY_NAME, PRODUCT_SUB_CATEGORY_NAME, SET_MODEL, MODEL_NAME, SERIAL_NO, DEALER_NAME, PURCHASED_DATE, RESERVATION_CREATE_DATE, APPOINTMENT_DATE, BOOKED_IN_DATE, REPAIR_COMPLETED_DATE, REPAIR_STATUS, COLLECT_DATE, RECEPTIONIST, TECHNICIAN, ONSITE_PEOPLE, DISPATCHED_BY, LOT_NO, WARRANTY_NO, REPAIR_LEVEL, TOTAL_AMOUNT_OF_ACCOUNT_PAYABLE, ACCOUNT_PAYABLE_BY_CUSTOMER, SONY_NEEDS_TO_PAY, ACCOUNT_PAYABLE_BY_ASC, CUSTOMER_NAME, CONTACT_NO, MOBILE_NO, CITY, STATE, POSTAL_CODE, PENDIND_DELIVERT_AGE, TOTAL_PART_FEE, LABOUR_FEE, INSPECTION_FEE, HANDLING_FEE, TRANSPORT_FEE, HOMESERVICE_FEE, LONGDISTANCE_FEE, TRAVELALLOWANCE_FEE, DA_FEE, DEMO_CHARGE, INSTALLATION_FEE, ECALL_CHARGE, COMBAT_FEE, ADDRESS, FAX_NO, EMAIL_ID, LAST_CONTACT_UPDATE_DATE, LAST_CONTACT_UPDATE_CONTENT, REPAIR_SET_BIN_LOCATION, HSN_CODE_OF_MODEL, PLACE_OF_SUPPLY "
        sqlStr = sqlStr & "FROM "
        sqlStr = sqlStr & "SONY_DELIVERED_SET "
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
            sqlStr = sqlStr & "AND LEFT(CONVERT(VARCHAR, COLLECT_DATE, 111), 10) >= @DateFrom and LEFT(CONVERT(VARCHAR, COLLECT_DATE, 111), 10) <= @DateTo "
            'sqlStr = sqlStr & "AND INVOICE_DATE >= @DateFrom and INVOICE_DATE <= @DateTo "
            dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@DateFrom", queryParams.DateFrom))
            dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@DateTo", queryParams.DateTo))
        ElseIf Not String.IsNullOrEmpty(queryParams.DateFrom) Then
            'sqlStr = sqlStr & "AND LEFT(CONVERT(VARCHAR, INVOICE_DATE, 111), 10) = @DateFrom "
            sqlStr = sqlStr & "AND COLLECT_DATE = @DateFrom "
            dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@DateFrom", queryParams.DateFrom))
        ElseIf Not String.IsNullOrEmpty(queryParams.DateTo) Then
            sqlStr = sqlStr & "AND COLLECT_DATE = @DateTo "
            'sqlStr = sqlStr & "AND LEFT(CONVERT(VARCHAR, INVOICE_DATE, 111), 10) = @DateTo "
            dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@DateTo", queryParams.DateTo))
        End If

        Dim _DataTable As DataTable = dbConn.GetDataSet(sqlStr)
        dbConn.CloseConnection()
        Return _DataTable
    End Function


    Public Function AddModifyDeliveredSetOld(ByVal csvData()() As String, queryParams As SonyDeliveredSetModel) As Boolean
        Log4NetControl.ComInfoLogWrite(Log4NetControl.UserID)
        'Mandatory Column 62 from CSV
        Dim flag As Boolean = True
        If csvData(0).Length < 62 Then
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
        sqlStr = "SELECT TOP 1 REGION FROM SONY_DELIVERED_SET "
        sqlStr = sqlStr & " WHERE DELFG = 0 AND SRC_FILE_NAME='" & queryParams.SRC_FILE_NAME & "'"
        'sqlStr = sqlStr & " AND SHIP_TO_BRANCH_CODE='" & queryParams.ShipToBranchCode & "'"
        dtSawDiscountExist = dbConn.GetDataSet(sqlStr)
        'if exist then need to update delfg=1 then insert the record as new
        If (dtSawDiscountExist Is Nothing) Or (dtSawDiscountExist.Rows.Count = 0) Then
            'isExist = 0
        Else 'The records is already exist, need to update DELFg=0
            ' isExist = 1
            sqlStr = "UPDATE SONY_DELIVERED_SET SET DELFG=1  "
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
                sqlStr = "Insert into SONY_DELIVERED_SET ("
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


                sqlStr = sqlStr & "REGION, "
                sqlStr = sqlStr & "ASC_LEVEL, "
                sqlStr = sqlStr & "ASC_CODE, "
                sqlStr = sqlStr & "ASC_NAME, "
                sqlStr = sqlStr & "JOB_ID, "
                sqlStr = sqlStr & "CUSTOMER_GROUP, "
                sqlStr = sqlStr & "MANUFACTURE, "
                sqlStr = sqlStr & "WARRANTY_TYPE, "
                sqlStr = sqlStr & "WARRANTY_CATEGORY, "
                sqlStr = sqlStr & "SERVICE_TYPE, "
                sqlStr = sqlStr & "PRODUCT_CATEGORY_NAME, "
                sqlStr = sqlStr & "PRODUCT_SUB_CATEGORY_NAME, "
                sqlStr = sqlStr & "SET_MODEL, "
                sqlStr = sqlStr & "MODEL_NAME, "
                sqlStr = sqlStr & "SERIAL_NO, "
                sqlStr = sqlStr & "DEALER_NAME, "
                sqlStr = sqlStr & "PURCHASED_DATE, "
                sqlStr = sqlStr & "RESERVATION_CREATE_DATE, "
                sqlStr = sqlStr & "APPOINTMENT_DATE, "
                sqlStr = sqlStr & "BOOKED_IN_DATE, "
                sqlStr = sqlStr & "REPAIR_COMPLETED_DATE, "
                sqlStr = sqlStr & "REPAIR_STATUS, "
                sqlStr = sqlStr & "COLLECT_DATE, "
                sqlStr = sqlStr & "RECEPTIONIST, "
                sqlStr = sqlStr & "TECHNICIAN, "
                sqlStr = sqlStr & "ONSITE_PEOPLE, "
                sqlStr = sqlStr & "DISPATCHED_BY, "
                sqlStr = sqlStr & "LOT_NO, "
                sqlStr = sqlStr & "WARRANTY_NO, "
                sqlStr = sqlStr & "REPAIR_LEVEL, "
                sqlStr = sqlStr & "TOTAL_AMOUNT_OF_ACCOUNT_PAYABLE, "
                sqlStr = sqlStr & "ACCOUNT_PAYABLE_BY_CUSTOMER, "
                sqlStr = sqlStr & "SONY_NEEDS_TO_PAY, "
                sqlStr = sqlStr & "ACCOUNT_PAYABLE_BY_ASC, "
                sqlStr = sqlStr & "CUSTOMER_NAME, "
                sqlStr = sqlStr & "CONTACT_NO, "
                sqlStr = sqlStr & "MOBILE_NO, "
                sqlStr = sqlStr & "CITY, "
                sqlStr = sqlStr & "STATE, "
                sqlStr = sqlStr & "POSTAL_CODE, "
                sqlStr = sqlStr & "PENDIND_DELIVERT_AGE, "
                sqlStr = sqlStr & "TOTAL_PART_FEE, "
                sqlStr = sqlStr & "LABOUR_FEE, "
                sqlStr = sqlStr & "INSPECTION_FEE, "
                sqlStr = sqlStr & "HANDLING_FEE, "
                sqlStr = sqlStr & "TRANSPORT_FEE, "
                sqlStr = sqlStr & "HOMESERVICE_FEE, "
                sqlStr = sqlStr & "LONGDISTANCE_FEE, "
                sqlStr = sqlStr & "TRAVELALLOWANCE_FEE, "
                sqlStr = sqlStr & "DA_FEE, "
                sqlStr = sqlStr & "DEMO_CHARGE, "
                sqlStr = sqlStr & "INSTALLATION_FEE, "
                sqlStr = sqlStr & "ECALL_CHARGE, "
                sqlStr = sqlStr & "COMBAT_FEE, "
                sqlStr = sqlStr & "ADDRESS, "
                sqlStr = sqlStr & "FAX_NO, "
                sqlStr = sqlStr & "EMAIL_ID, "
                sqlStr = sqlStr & "LAST_CONTACT_UPDATE_DATE, "
                sqlStr = sqlStr & "LAST_CONTACT_UPDATE_CONTENT, "
                sqlStr = sqlStr & "REPAIR_SET_BIN_LOCATION, "
                sqlStr = sqlStr & "HSN_CODE_OF_MODEL, "
                sqlStr = sqlStr & "PLACE_OF_SUPPLY, "



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


                sqlStr = sqlStr & "@REGION, "
                sqlStr = sqlStr & "@ASC_LEVEL, "
                sqlStr = sqlStr & "@ASC_CODE, "
                sqlStr = sqlStr & "@ASC_NAME, "
                sqlStr = sqlStr & "@JOB_ID, "
                sqlStr = sqlStr & "@CUSTOMER_GROUP, "
                sqlStr = sqlStr & "@MANUFACTURE, "
                sqlStr = sqlStr & "@WARRANTY_TYPE, "
                sqlStr = sqlStr & "@WARRANTY_CATEGORY, "
                sqlStr = sqlStr & "@SERVICE_TYPE, "
                sqlStr = sqlStr & "@PRODUCT_CATEGORY_NAME, "
                sqlStr = sqlStr & "@PRODUCT_SUB_CATEGORY_NAME, "
                sqlStr = sqlStr & "@SET_MODEL, "
                sqlStr = sqlStr & "@MODEL_NAME, "
                sqlStr = sqlStr & "@SERIAL_NO, "
                sqlStr = sqlStr & "@DEALER_NAME, "
                sqlStr = sqlStr & "@PURCHASED_DATE, "
                sqlStr = sqlStr & "@RESERVATION_CREATE_DATE, "
                sqlStr = sqlStr & "@APPOINTMENT_DATE, "
                sqlStr = sqlStr & "@BOOKED_IN_DATE, "
                sqlStr = sqlStr & "@REPAIR_COMPLETED_DATE, "
                sqlStr = sqlStr & "@REPAIR_STATUS, "
                sqlStr = sqlStr & "@COLLECT_DATE, "
                sqlStr = sqlStr & "@RECEPTIONIST, "
                sqlStr = sqlStr & "@TECHNICIAN, "
                sqlStr = sqlStr & "@ONSITE_PEOPLE, "
                sqlStr = sqlStr & "@DISPATCHED_BY, "
                sqlStr = sqlStr & "@LOT_NO, "
                sqlStr = sqlStr & "@WARRANTY_NO, "
                sqlStr = sqlStr & "@REPAIR_LEVEL, "
                sqlStr = sqlStr & "@TOTAL_AMOUNT_OF_ACCOUNT_PAYABLE, "
                sqlStr = sqlStr & "@ACCOUNT_PAYABLE_BY_CUSTOMER, "
                sqlStr = sqlStr & "@SONY_NEEDS_TO_PAY, "
                sqlStr = sqlStr & "@ACCOUNT_PAYABLE_BY_ASC, "
                sqlStr = sqlStr & "@CUSTOMER_NAME, "
                sqlStr = sqlStr & "@CONTACT_NO, "
                sqlStr = sqlStr & "@MOBILE_NO, "
                sqlStr = sqlStr & "@CITY, "
                sqlStr = sqlStr & "@STATE, "
                sqlStr = sqlStr & "@POSTAL_CODE, "
                sqlStr = sqlStr & "@PENDIND_DELIVERT_AGE, "
                sqlStr = sqlStr & "@TOTAL_PART_FEE, "
                sqlStr = sqlStr & "@LABOUR_FEE, "
                sqlStr = sqlStr & "@INSPECTION_FEE, "
                sqlStr = sqlStr & "@HANDLING_FEE, "
                sqlStr = sqlStr & "@TRANSPORT_FEE, "
                sqlStr = sqlStr & "@HOMESERVICE_FEE, "
                sqlStr = sqlStr & "@LONGDISTANCE_FEE, "
                sqlStr = sqlStr & "@TRAVELALLOWANCE_FEE, "
                sqlStr = sqlStr & "@DA_FEE, "
                sqlStr = sqlStr & "@DEMO_CHARGE, "
                sqlStr = sqlStr & "@INSTALLATION_FEE, "
                sqlStr = sqlStr & "@ECALL_CHARGE, "
                sqlStr = sqlStr & "@COMBAT_FEE, "
                sqlStr = sqlStr & "@ADDRESS, "
                sqlStr = sqlStr & "@FAX_NO, "
                sqlStr = sqlStr & "@EMAIL_ID, "
                sqlStr = sqlStr & "@LAST_CONTACT_UPDATE_DATE, "
                sqlStr = sqlStr & "@LAST_CONTACT_UPDATE_CONTENT, "
                sqlStr = sqlStr & "@REPAIR_SET_BIN_LOCATION, "
                sqlStr = sqlStr & "@HSN_CODE_OF_MODEL, "
                sqlStr = sqlStr & "@PLACE_OF_SUPPLY, "


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

                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@REGION", csvData(i)(0)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@ASC_LEVEL", csvData(i)(1)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@ASC_CODE", csvData(i)(2)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@ASC_NAME", csvData(i)(3)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@JOB_ID", csvData(i)(4)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@CUSTOMER_GROUP", csvData(i)(5)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@MANUFACTURE", csvData(i)(6)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@WARRANTY_TYPE", csvData(i)(7)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@WARRANTY_CATEGORY", csvData(i)(8)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@SERVICE_TYPE", csvData(i)(9)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@PRODUCT_CATEGORY_NAME", csvData(i)(10)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@PRODUCT_SUB_CATEGORY_NAME", csvData(i)(11)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@SET_MODEL", csvData(i)(12)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@MODEL_NAME", csvData(i)(13)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@SERIAL_NO", csvData(i)(14)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@DEALER_NAME", csvData(i)(15)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@PURCHASED_DATE", csvData(i)(16)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@RESERVATION_CREATE_DATE", csvData(i)(17)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@APPOINTMENT_DATE", csvData(i)(18)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@BOOKED_IN_DATE", csvData(i)(19)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@REPAIR_COMPLETED_DATE", csvData(i)(20)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@REPAIR_STATUS", csvData(i)(21)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@COLLECT_DATE", csvData(i)(22)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@RECEPTIONIST", csvData(i)(23)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@TECHNICIAN", csvData(i)(24)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@ONSITE_PEOPLE", csvData(i)(25)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@DISPATCHED_BY", csvData(i)(26)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@LOT_NO", csvData(i)(27)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@WARRANTY_NO", csvData(i)(28)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@REPAIR_LEVEL", csvData(i)(29)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@TOTAL_AMOUNT_OF_ACCOUNT_PAYABLE", csvData(i)(30)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@ACCOUNT_PAYABLE_BY_CUSTOMER", csvData(i)(31)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@SONY_NEEDS_TO_PAY", csvData(i)(32)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@ACCOUNT_PAYABLE_BY_ASC", csvData(i)(33)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@CUSTOMER_NAME", csvData(i)(34)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@CONTACT_NO", csvData(i)(35)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@MOBILE_NO", csvData(i)(36)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@CITY", csvData(i)(37)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@STATE", csvData(i)(38)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@POSTAL_CODE", csvData(i)(39)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@PENDIND_DELIVERT_AGE", csvData(i)(40)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@TOTAL_PART_FEE", csvData(i)(41)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@LABOUR_FEE", csvData(i)(42)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@INSPECTION_FEE", csvData(i)(43)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@HANDLING_FEE", csvData(i)(44)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@TRANSPORT_FEE", csvData(i)(45)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@HOMESERVICE_FEE", csvData(i)(46)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@LONGDISTANCE_FEE", csvData(i)(47)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@TRAVELALLOWANCE_FEE", csvData(i)(48)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@DA_FEE", csvData(i)(49)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@DEMO_CHARGE", csvData(i)(50)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@INSTALLATION_FEE", csvData(i)(51)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@ECALL_CHARGE", csvData(i)(52)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@COMBAT_FEE", csvData(i)(53)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@ADDRESS", csvData(i)(54)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@FAX_NO", csvData(i)(55)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@EMAIL_ID", csvData(i)(56)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@LAST_CONTACT_UPDATE_DATE", csvData(i)(57)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@LAST_CONTACT_UPDATE_CONTENT", csvData(i)(58)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@REPAIR_SET_BIN_LOCATION", csvData(i)(59)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@HSN_CODE_OF_MODEL", csvData(i)(60)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@PLACE_OF_SUPPLY", csvData(i)(61)))



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



End Class
