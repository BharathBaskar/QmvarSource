Imports System
Imports System.Data
Imports System.Web.Security
Imports System.Collections.Generic
Imports Ganges33.Ganges33.dao
Imports Ganges33.Ganges33.model
Imports Ganges33.Ganges33.logic

Public Class SalesControl

    Function SelectTargetSales(TARGET_YEAR As String, TARGET_MONTH As String) As DataTable
        Log4NetControl.ComInfoLogWrite(Log4NetControl.UserID)
        Dim dbConn As DBUtility = New DBUtility()
        Dim dt As DataTable = New DataTable()
        Dim sqlStr As String = "SELECT "

        sqlStr = sqlStr & "SHIP_TO_BRANCH_CODE, "
        sqlStr = sqlStr & "SHIP_TO_BRANCH, "
        sqlStr = sqlStr & "TARGET_MONTH_AMOUNT, "
        sqlStr = sqlStr & "TARGET_DAY_AMOUNT "


        sqlStr = sqlStr & "FROM "
        sqlStr = sqlStr & "SSC_TARGET_SET "
        sqlStr = sqlStr & "WHERE "
        sqlStr = sqlStr & "DELFG=0 "

        If Not String.IsNullOrEmpty(TARGET_MONTH) Then
            sqlStr = sqlStr & "AND TARGET_MONTH = @TARGET_MONTH "
            dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@TARGET_MONTH", TARGET_MONTH))
        End If

        If Not String.IsNullOrEmpty(TARGET_YEAR) Then
            sqlStr = sqlStr & "AND TARGET_YEAR = @TARGET_YEAR "
            dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@TARGET_YEAR", TARGET_YEAR))
        End If

        Dim _DataTable As DataTable = dbConn.GetDataSet(sqlStr)
        dbConn.CloseConnection()
        Return _DataTable


    End Function
    Function SonyPlTracking(strYear As String, strMonth As String) As DataTable

        'Return Table
        Dim tblSIDSales As DataTable
        tblSIDSales = New DataTable("SIDSALES")
        Dim column1 As DataColumn = New DataColumn("SalesDate")
        column1.DataType = System.Type.GetType("System.String")

        Dim column2 As DataColumn = New DataColumn("SalesAmount")
        column2.DataType = System.Type.GetType("System.String")

        tblSIDSales.Columns.Add(column1)
        tblSIDSales.Columns.Add(column2)




        Dim strDate As String = ""
        Dim strMaxDay As String = ""
        strMaxDay = Date.DaysInMonth(strYear, strMonth)

        Dim strBranchName As String = ""
        Dim clsSetCommon As New Class_common
        Dim comcontrol As New CommonControl

        Dim csvContents = New List(Of String)

        '1st Left Column begin
        ''Dim rowWork1 As String = strBranchName & ","
        ''Dim rowWork2 As String = "Repair,"
        ''Dim rowWork3 As String = "DEMO,"
        ''Dim rowWork4 As String = "Installation,"
        ''Dim rowWork5 As String = ","
        ''Dim rowWork6 As String = "Warranty (Number),"
        ''Dim rowWork7 As String = "In Warranty (Number),"
        ''Dim rowWork8 As String = "Out Warranty (Number),"
        ''Dim rowWork9 As String = "Total Amount IW,"
        ''Dim rowWork10 As String = "Total Amount of Account Payable IW,"
        ''Dim rowWork11 As String = "Account Payable By ASC,"
        ''Dim rowWork12 As String = "Account Payable By Customer,"
        ''Dim rowWork13 As String = "SONY Needs To Pay/IW,"
        ''Dim rowWork14 As String = "SONY Needs To Pay/OW,"
        ''Dim rowWork15 As String = "Total Parts Fee,"
        ''Dim rowWork16 As String = "Labour Fee,"
        ''Dim rowWork17 As String = "Inspection Fee,"
        ''Dim rowWork18 As String = "Handling Fee,"
        ''Dim rowWork19 As String = "Transport Fee,"
        ''Dim rowWork20 As String = "HomeService Fee,"
        ''Dim rowWork21 As String = "Longdistans Fee,"
        ''Dim rowWork22 As String = "Travelallowance Fee,"
        ''Dim rowWork23 As String = "Da Fee,"
        ''Dim rowWork24 As String = "Demo Charge,"
        ''Dim rowWork25 As String = "Installation Fee,"
        ''Dim rowWork26 As String = "Ecall Charge,"
        ''Dim rowWork27 As String = "Combat Fee,"
        ''Dim rowWork28 As String = "Total Amount OW,"
        ''Dim rowWork29 As String = "Total Amount of Account Payable OW,"
        ''Dim rowWork30 As String = "Account Payable By ASC,"
        ''Dim rowWork31 As String = "Account Payable By Customer,"
        ''Dim rowWork32 As String = "SONY Needs To Pay,"
        ''Dim rowWork33 As String = "Total Parts Fee,"
        ''Dim rowWork34 As String = "Labour Fee,"
        ''Dim rowWork35 As String = "Inspection Fee,"
        ''Dim rowWork36 As String = "Handling Fee,"
        ''Dim rowWork37 As String = "Transport Fee,"
        ''Dim rowWork38 As String = "HomeService Fee,"
        ''Dim rowWork39 As String = "Longdistans Fee,"
        ''Dim rowWork40 As String = "Travelallowance Fee,"
        ''Dim rowWork41 As String = "Da Fee,"
        ''Dim rowWork42 As String = "Demo Charge,"
        ''Dim rowWork43 As String = "Installation Fee,"
        ''Dim rowWork44 As String = "Ecall Charge,"
        ''Dim rowWork45 As String = "Combat Fee,"
        ''Dim rowWork46 As String = "Revenue without Tax,"
        ''Dim rowWork47 As String = ","
        ''Dim rowWork48 As String = "IW parts consumed,"
        ''Dim rowWork49 As String = "Total parts consumed,"
        ''Dim rowWork50 As String = "Prime Cost Total,"
        ''Dim rowWork51 As String = "Gross Profit,"
        ''Dim rowWork52 As String = ","
        '1st Left Column End

        'Middles Start




        Dim Repair As Decimal = 0.00 ' C-1 - C-2
        Dim DEMO As Decimal = 0.00 ' Marked as C-1 DEMO_CHARGE
        Dim Installation As Decimal = 0.00 ' Marked as C-2 INSTALLATION_FEE

        Dim WarrantyNumber As Int16 = 0
        Dim InWarrantyNumber As Int16 = 0
        Dim OutWarrantyNumber As Int16 = 0

        Dim IW_TotalAmountIW As Decimal = 0.00
        Dim IW_TotalAmountOfAccountPayableIW As Decimal = 0.00
        Dim IW_AccountPayableByASC As Decimal = 0.00
        Dim IW_AccountPayableByCustomer As Decimal = 0.00
        Dim IW_SONYNeedsToPayIW As Decimal = 0.00
        Dim IW_SONYNeedsToPayOW As Decimal = 0.00
        Dim IW_TotalPartsFee As Decimal = 0.00
        Dim IW_LabourFee As Decimal = 0.00
        Dim IW_InspectionFee As Decimal = 0.00
        Dim IW_HandlingFee As Decimal = 0.00
        Dim IW_TransportFee As Decimal = 0.00
        Dim IW_HomeServiceFee As Decimal = 0.00
        Dim IW_LongdistansFee As Decimal = 0.00
        Dim IW_TravelallowanceFee As Decimal = 0.00
        Dim IW_DaFee As Decimal = 0.00
        Dim IW_DemoCharge As Decimal = 0.00
        Dim IW_InstallationFee As Decimal = 0.00
        Dim IW_EcallCharge As Decimal = 0.00
        Dim IW_CombatFee As Decimal = 0.00


        Dim OW_TotalAmountOW As Decimal = 0.00
        Dim OW_TotalAmountOfAccountPayableOW As Decimal = 0.00
        Dim OW_AccountPayableByASC As Decimal = 0.00
        Dim OW_AccountPayableByCustomer As Decimal = 0.00
        Dim OW_SONYNeedsToPay As Decimal = 0.00
        Dim OW_TotalPartsFee As Decimal = 0.00
        Dim OW_LabourFee As Decimal = 0.00
        Dim OW_InspectionFee As Decimal = 0.00
        Dim OW_HandlingFee As Decimal = 0.00
        Dim OW_TransportFee As Decimal = 0.00
        Dim OW_HomeServiceFee As Decimal = 0.00
        Dim OW_LongdistansFee As Decimal = 0.00
        Dim OW_TravelallowanceFee As Decimal = 0.00
        Dim OW_DaFee As Decimal = 0.00
        Dim OW_DemoCharge As Decimal = 0.00
        Dim OW_InstallationFee As Decimal = 0.00
        Dim OW_EcallCharge As Decimal = 0.00
        Dim OW_CombatFee As Decimal = 0.00



        Dim RevenuewithoutTax As Decimal = 0.00
        Dim IWpartsconsumed As Decimal = 0.00
        Dim Totalpartsconsumed As Decimal = 0.00
        Dim PrimeCostTotal As Decimal = 0.00
        Dim GrossProfit As Decimal = 0.00

        'Total
        Dim TOT_Repair As Decimal = 0.00 ' C-1 - C-2
        Dim TOT_DEMO As Decimal = 0.00 ' Marked as C-1 DEMO_CHARGE
        Dim TOT_Installation As Decimal = 0.00 ' Marked as C-2 INSTALLATION_FEE

        Dim TOT_WarrantyNumber As Decimal = 0.00
        Dim TOT_InWarrantyNumber As Decimal = 0.00
        Dim TOT_OutWarrantyNumber As Decimal = 0.00

        Dim TOT_IW_TotalAmountIW As Decimal = 0.00
        Dim TOT_IW_TotalAmountOfAccountPayableIW As Decimal = 0.00
        Dim TOT_IW_AccountPayableByASC As Decimal = 0.00
        Dim TOT_IW_AccountPayableByCustomer As Decimal = 0.00
        Dim TOT_IW_SONYNeedsToPayIW As Decimal = 0.00
        Dim TOT_IW_SONYNeedsToPayOW As Decimal = 0.00
        Dim TOT_IW_TotalPartsFee As Decimal = 0.00
        Dim TOT_IW_LabourFee As Decimal = 0.00
        Dim TOT_IW_InspectionFee As Decimal = 0.00
        Dim TOT_IW_HandlingFee As Decimal = 0.00
        Dim TOT_IW_TransportFee As Decimal = 0.00
        Dim TOT_IW_HomeServiceFee As Decimal = 0.00
        Dim TOT_IW_LongdistansFee As Decimal = 0.00
        Dim TOT_IW_TravelallowanceFee As Decimal = 0.00
        Dim TOT_IW_DaFee As Decimal = 0.00
        Dim TOT_IW_DemoCharge As Decimal = 0.00
        Dim TOT_IW_InstallationFee As Decimal = 0.00
        Dim TOT_IW_EcallCharge As Decimal = 0.00
        Dim TOT_IW_CombatFee As Decimal = 0.00


        Dim TOT_OW_TotalAmountOW As Decimal = 0.00
        Dim TOT_OW_TotalAmountOfAccountPayableOW As Decimal = 0.00
        Dim TOT_OW_AccountPayableByASC As Decimal = 0.00
        Dim TOT_OW_AccountPayableByCustomer As Decimal = 0.00
        Dim TOT_OW_SONYNeedsToPay As Decimal = 0.00
        Dim TOT_OW_TotalPartsFee As Decimal = 0.00
        Dim TOT_OW_LabourFee As Decimal = 0.00
        Dim TOT_OW_InspectionFee As Decimal = 0.00
        Dim TOT_OW_HandlingFee As Decimal = 0.00
        Dim TOT_OW_TransportFee As Decimal = 0.00
        Dim TOT_OW_HomeServiceFee As Decimal = 0.00
        Dim TOT_OW_LongdistansFee As Decimal = 0.00
        Dim TOT_OW_TravelallowanceFee As Decimal = 0.00
        Dim TOT_OW_DaFee As Decimal = 0.00
        Dim TOT_OW_DemoCharge As Decimal = 0.00
        Dim TOT_OW_InstallationFee As Decimal = 0.00
        Dim TOT_OW_EcallCharge As Decimal = 0.00
        Dim TOT_OW_CombatFee As Decimal = 0.00



        Dim TOT_RevenuewithoutTax As Decimal = 0.00
        Dim TOT_IWpartsconsumed As Decimal = 0.00
        Dim TOT_Totalpartsconsumed As Decimal = 0.00
        Dim TOT_PrimeCostTotal As Decimal = 0.00
        Dim TOT_GrossProfit As Decimal = 0.00

        Dim SONYNeedsToPayIW As Decimal = 0.00
        Dim SONYNeedsToPayOW As Decimal = 0.00

        Dim TOT_SONYNeedsToPayIW As Decimal = 0.00
        Dim TOT_SONYNeedsToPayOW As Decimal = 0.00


        Dim OW_TotalAmountOfAccountPayableIW As Decimal = 0.00
        Dim TOT_OW_TotalAmountOfAccountPayableIW As Decimal = 0.00
        Dim TOT_Percentage As Decimal = 0.00




        'Final For a Day 
        Dim NET_TOT_Repair As Decimal = 0.00
        Dim NET_TOT_DEMO As Decimal = 0.00
        Dim NET_TOT_Installation As Decimal = 0.00

        '# Warranty (Number) = (In Warranty (Number) + Out Warranty (Number)
        Dim NET_TOT_WarrantyNumber As Int16 = 0
        Dim NET_TOT_InWarrantyNumber As Int16 = 0
        Dim NET_TOT_OutWarrantyNumber As Int16 = 0


        '#Total Amount IW (SONY Needs To Pay/IW + SONY Needs To Pay/OW ) /1.18
        Dim NET_TOT_IW_TotalAmountIW As Decimal = 0.00
        Dim NET_TOT_IW_TotalAmountOfAccountPayableIW As Decimal = 0.00
        Dim NET_TOT_IW_AccountPayableByASC As Decimal = 0.00
        Dim NET_TOT_IW_AccountPayableByCustomer As Decimal = 0.00
        Dim NET_TOT_SONYNeedsToPayIW As Decimal = 0.00

        Dim NET_TOT_IW_TotalPartsFee As Decimal = 0.00
        Dim NET_TOT_IW_LabourFee As Decimal = 0.00
        Dim NET_TOT_IW_InspectionFee As Decimal = 0.00
        Dim NET_TOT_IW_HandlingFee As Decimal = 0.00
        Dim NET_TOT_IW_TransportFee As Decimal = 0.00
        Dim NET_TOT_IW_HomeServiceFee As Decimal = 0.00
        Dim NET_TOT_IW_LongdistansFee As Decimal = 0.00
        Dim NET_TOT_IW_TravelallowanceFee As Decimal = 0.00
        Dim NET_TOT_IW_DaFee As Decimal = 0.00
        Dim NET_TOT_IW_DemoCharge As Decimal = 0.00
        Dim NET_TOT_IW_InstallationFee As Decimal = 0.00
        Dim NET_TOT_IW_EcallCharge As Decimal = 0.00
        Dim NET_TOT_IW_CombatFee As Decimal = 0.00


        Dim NET_TOT_OW_TotalAmountOW As Decimal = 0.00
        Dim NET_TOT_OW_TotalAmountOfAccountPayableOW As Decimal = 0.00
        Dim NET_TOT_OW_AccountPayableByASC As Decimal = 0.00
        Dim NET_TOT_OW_AccountPayableByCustomer As Decimal = 0.00
        Dim NET_TOT_SONYNeedsToPayOW As Decimal = 0.00
        Dim NET_TOT_OW_TotalPartsFee As Decimal = 0.00
        Dim NET_TOT_OW_LabourFee As Decimal = 0.00
        Dim NET_TOT_OW_InspectionFee As Decimal = 0.00

        Dim NET_TOT_OW_HandlingFee As Decimal = 0.00
        Dim NET_TOT_OW_TransportFee As Decimal = 0.00
        Dim NET_TOT_OW_HomeServiceFee As Decimal = 0.00
        Dim NET_TOT_OW_LongdistansFee As Decimal = 0.00
        Dim NET_TOT_OW_TravelallowanceFee As Decimal = 0.00
        Dim NET_TOT_OW_DaFee As Decimal = 0.00
        Dim NET_TOT_OW_DemoCharge As Decimal = 0.00
        Dim NET_TOT_OW_InstallationFee As Decimal = 0.00
        Dim NET_TOT_OW_EcallCharge As Decimal = 0.00
        Dim NET_TOT_OW_CombatFee As Decimal = 0.00


        Dim NET_TOT_RevenuewithoutTax As Decimal = 0.00
        Dim NET_TOT_IWpartsconsumed As Decimal = 0.00
        Dim NET_TOT_Totalpartsconsumed As Decimal = 0.00
        Dim NET_TOT_PrimeCostTotal As Decimal = 0.00
        Dim NET_TOT_GrossProfit As Decimal = 0.00
        Dim NET_TOT_Percentage As Decimal = 0.00


        Dim strDay As String = ""
        Dim i As Integer

        For i = 1 To strMaxDay

            strDay = i
            If Len(strDay) = 1 Then
                strDay = "0" & strDay
            End If

            strDate = strYear & "/" & strMonth & "/" & strDay

            Repair = 0.0
            DEMO = 0.0
            Installation = 0.00


            WarrantyNumber = 0
            InWarrantyNumber = 0
            OutWarrantyNumber = 0

            IW_TotalAmountIW = 0.00
            IW_TotalAmountOfAccountPayableIW = 0.00
            IW_AccountPayableByASC = 0.00
            IW_AccountPayableByCustomer = 0.00
            IW_SONYNeedsToPayIW = 0.00
            IW_SONYNeedsToPayOW = 0.00
            IW_TotalPartsFee = 0.00
            IW_LabourFee = 0.00
            IW_InspectionFee = 0.00
            IW_HandlingFee = 0.00
            IW_TransportFee = 0.00
            IW_HomeServiceFee = 0.00
            IW_LongdistansFee = 0.00
            IW_TravelallowanceFee = 0.00
            IW_DaFee = 0.00
            IW_DemoCharge = 0.00
            IW_InstallationFee = 0.00
            IW_EcallCharge = 0.00
            IW_CombatFee = 0.00


            OW_TotalAmountOW = 0.00
            OW_TotalAmountOfAccountPayableOW = 0.00
            OW_AccountPayableByASC = 0.00
            OW_AccountPayableByCustomer = 0.00
            OW_SONYNeedsToPay = 0.00
            OW_TotalPartsFee = 0.00
            OW_LabourFee = 0.00
            OW_InspectionFee = 0.00
            OW_HandlingFee = 0.00
            OW_TransportFee = 0.00
            OW_HomeServiceFee = 0.00
            OW_LongdistansFee = 0.00
            OW_TravelallowanceFee = 0.00
            OW_DaFee = 0.00
            OW_DemoCharge = 0.00
            OW_InstallationFee = 0.00
            OW_EcallCharge = 0.00
            OW_CombatFee = 0.00



            RevenuewithoutTax = 0.00
            IWpartsconsumed = 0.00
            Totalpartsconsumed = 0.00
            PrimeCostTotal = 0.00
            GrossProfit = 0.00

            SONYNeedsToPayIW = 0.0
            SONYNeedsToPayOW = 0.0

            IW_TotalAmountOfAccountPayableIW = 0.0
            OW_TotalAmountOfAccountPayableIW = 0.0


            TOT_Repair = 0
            TOT_DEMO = 0
            TOT_Installation = 0



            TOT_WarrantyNumber = 0
            TOT_WarrantyNumber = 0
            TOT_InWarrantyNumber = 0
            TOT_OutWarrantyNumber = 0


            TOT_IW_TotalAmountIW = 0
            TOT_IW_TotalAmountIW = 0
            TOT_IW_TotalAmountOfAccountPayableIW = 0
            TOT_IW_AccountPayableByASC = 0
            TOT_IW_AccountPayableByCustomer = 0
            TOT_SONYNeedsToPayIW = 0
            TOT_SONYNeedsToPayOW = 0
            TOT_IW_TotalPartsFee = 0
            TOT_IW_LabourFee = 0
            TOT_IW_InspectionFee = 0
            TOT_IW_HandlingFee = 0
            TOT_IW_TransportFee = 0
            TOT_IW_HomeServiceFee = 0
            TOT_IW_LongdistansFee = 0
            TOT_IW_TravelallowanceFee = 0
            TOT_IW_DaFee = 0
            TOT_IW_DemoCharge = 0
            TOT_IW_InstallationFee = 0
            TOT_IW_EcallCharge = 0
            TOT_IW_CombatFee = 0

            TOT_OW_TotalAmountOW = 0
            TOT_OW_TotalAmountOW = 0
            TOT_OW_TotalAmountOfAccountPayableOW = 0
            TOT_OW_AccountPayableByASC = 0
            TOT_OW_AccountPayableByCustomer = 0
            TOT_SONYNeedsToPayOW = 0
            TOT_OW_TotalPartsFee = 0
            TOT_OW_LabourFee = 0
            TOT_OW_InspectionFee = 0
            TOT_OW_HandlingFee = 0
            TOT_OW_TransportFee = 0
            TOT_OW_HomeServiceFee = 0
            TOT_OW_LongdistansFee = 0
            TOT_OW_TravelallowanceFee = 0
            TOT_OW_DaFee = 0
            TOT_OW_DemoCharge = 0
            TOT_OW_InstallationFee = 0
            TOT_OW_EcallCharge = 0
            TOT_OW_CombatFee = 0

            TOT_RevenuewithoutTax = 0
            TOT_IWpartsconsumed = 0
            TOT_Totalpartsconsumed = 0
            TOT_PrimeCostTotal = 0
            TOT_GrossProfit = 0
            TOT_Percentage = 0




            Dim _SonyDeliveredSetModel As SonyDeliveredSetModel = New SonyDeliveredSetModel()
            Dim _SonyDeliveredSetControl As SonyDeliveredSetControl = New SonyDeliveredSetControl()
            _SonyDeliveredSetModel.UserId = "" 'userid
            _SonyDeliveredSetModel.SHIP_TO_BRANCH_CODE = "1019756" 'DropListLocation.SelectedItem.Value
            _SonyDeliveredSetModel.SHIP_TO_BRANCH = "SID1" 'DropListLocation.SelectedItem.Text
            _SonyDeliveredSetModel.DateFrom = strDate 'DropDownYear.SelectedValue & "/" & DropDownMonth.SelectedValue & "/" & "01"
            _SonyDeliveredSetModel.DateTo = strDate 'CommonControl.GetLastDayOfMonth(DropDownMonth.SelectedValue, DropDownYear.SelectedValue)
            _SonyDeliveredSetModel.FILE_NAME = _SonyDeliveredSetModel.SHIP_TO_BRANCH & "_DS" & strYear & strMonth & ".csv" '_SonyDeliveredSetModel.SHIP_TO_BRANCH & "_DS" & DropDownYear.SelectedValue & DropDownMonth.SelectedValue & ".csv"
            Dim dtDeliveredSet As DataTable = _SonyDeliveredSetControl.SelectPLDeliveredSet(_SonyDeliveredSetModel)

            If (dtDeliveredSet Is Nothing) Or (dtDeliveredSet.Rows.Count = 0) Then
                'BYDefauly 
                Repair = 0.0
                DEMO = 0.0
                Installation = 0.00


                WarrantyNumber = 0
                InWarrantyNumber = 0
                OutWarrantyNumber = 0

                IW_TotalAmountIW = 0.00
                IW_TotalAmountOfAccountPayableIW = 0.00
                IW_AccountPayableByASC = 0.00
                IW_AccountPayableByCustomer = 0.00
                IW_SONYNeedsToPayIW = 0.00
                IW_SONYNeedsToPayOW = 0.00
                IW_TotalPartsFee = 0.00
                IW_LabourFee = 0.00
                IW_InspectionFee = 0.00
                IW_HandlingFee = 0.00
                IW_TransportFee = 0.00
                IW_HomeServiceFee = 0.00
                IW_LongdistansFee = 0.00
                IW_TravelallowanceFee = 0.00
                IW_DaFee = 0.00
                IW_DemoCharge = 0.00
                IW_InstallationFee = 0.00
                IW_EcallCharge = 0.00
                IW_CombatFee = 0.00


                OW_TotalAmountOW = 0.00
                OW_TotalAmountOfAccountPayableOW = 0.00
                OW_AccountPayableByASC = 0.00
                OW_AccountPayableByCustomer = 0.00
                OW_SONYNeedsToPay = 0.00
                OW_TotalPartsFee = 0.00
                OW_LabourFee = 0.00
                OW_InspectionFee = 0.00
                OW_HandlingFee = 0.00
                OW_TransportFee = 0.00
                OW_HomeServiceFee = 0.00
                OW_LongdistansFee = 0.00
                OW_TravelallowanceFee = 0.00
                OW_DaFee = 0.00
                OW_DemoCharge = 0.00
                OW_InstallationFee = 0.00
                OW_EcallCharge = 0.00
                OW_CombatFee = 0.00



                RevenuewithoutTax = 0.00
                IWpartsconsumed = 0.00
                Totalpartsconsumed = 0.00
                PrimeCostTotal = 0.00
                GrossProfit = 0.00

                SONYNeedsToPayIW = 0.0
                SONYNeedsToPayOW = 0.0

                IW_TotalAmountOfAccountPayableIW = 0.0
                OW_TotalAmountOfAccountPayableIW = 0.0


                TOT_Repair = 0
                TOT_DEMO = 0
                TOT_Installation = 0



                TOT_WarrantyNumber = 0
                TOT_WarrantyNumber = 0
                TOT_InWarrantyNumber = 0
                TOT_OutWarrantyNumber = 0


                TOT_IW_TotalAmountIW = 0
                TOT_IW_TotalAmountIW = 0
                TOT_IW_TotalAmountOfAccountPayableIW = 0
                TOT_IW_AccountPayableByASC = 0
                TOT_IW_AccountPayableByCustomer = 0
                TOT_SONYNeedsToPayIW = 0
                TOT_SONYNeedsToPayOW = 0
                TOT_IW_TotalPartsFee = 0
                TOT_IW_LabourFee = 0
                TOT_IW_InspectionFee = 0
                TOT_IW_HandlingFee = 0
                TOT_IW_TransportFee = 0
                TOT_IW_HomeServiceFee = 0
                TOT_IW_LongdistansFee = 0
                TOT_IW_TravelallowanceFee = 0
                TOT_IW_DaFee = 0
                TOT_IW_DemoCharge = 0
                TOT_IW_InstallationFee = 0
                TOT_IW_EcallCharge = 0
                TOT_IW_CombatFee = 0

                TOT_OW_TotalAmountOW = 0
                TOT_OW_TotalAmountOW = 0
                TOT_OW_TotalAmountOfAccountPayableOW = 0
                TOT_OW_AccountPayableByASC = 0
                TOT_OW_AccountPayableByCustomer = 0
                TOT_SONYNeedsToPayOW = 0
                TOT_OW_TotalPartsFee = 0
                TOT_OW_LabourFee = 0
                TOT_OW_InspectionFee = 0
                TOT_OW_HandlingFee = 0
                TOT_OW_TransportFee = 0
                TOT_OW_HomeServiceFee = 0
                TOT_OW_LongdistansFee = 0
                TOT_OW_TravelallowanceFee = 0
                TOT_OW_DaFee = 0
                TOT_OW_DemoCharge = 0
                TOT_OW_InstallationFee = 0
                TOT_OW_EcallCharge = 0
                TOT_OW_CombatFee = 0

                TOT_RevenuewithoutTax = 0
                TOT_IWpartsconsumed = 0
                TOT_Totalpartsconsumed = 0
                TOT_PrimeCostTotal = 0
                TOT_GrossProfit = 0
                TOT_Percentage = 0

            Else

                For DsetRow = 0 To dtDeliveredSet.Rows.Count - 1



                    'BYDefauly 
                    Repair = 0.0
                    DEMO = 0.0
                    Installation = 0.00

                    WarrantyNumber = 0
                    InWarrantyNumber = 0
                    OutWarrantyNumber = 0

                    IW_TotalAmountIW = 0.00
                    IW_TotalAmountOfAccountPayableIW = 0.00
                    IW_AccountPayableByASC = 0.00
                    IW_AccountPayableByCustomer = 0.00
                    IW_SONYNeedsToPayIW = 0.00
                    IW_SONYNeedsToPayOW = 0.00
                    IW_TotalPartsFee = 0.00
                    IW_LabourFee = 0.00
                    IW_InspectionFee = 0.00
                    IW_HandlingFee = 0.00
                    IW_TransportFee = 0.00
                    IW_HomeServiceFee = 0.00
                    IW_LongdistansFee = 0.00
                    IW_TravelallowanceFee = 0.00
                    IW_DaFee = 0.00
                    IW_DemoCharge = 0.00
                    IW_InstallationFee = 0.00
                    IW_EcallCharge = 0.00
                    IW_CombatFee = 0.00


                    OW_TotalAmountOW = 0.00
                    OW_TotalAmountOfAccountPayableOW = 0.00
                    OW_AccountPayableByASC = 0.00
                    OW_AccountPayableByCustomer = 0.00
                    OW_SONYNeedsToPay = 0.00
                    OW_TotalPartsFee = 0.00
                    OW_LabourFee = 0.00
                    OW_InspectionFee = 0.00
                    OW_HandlingFee = 0.00
                    OW_TransportFee = 0.00
                    OW_HomeServiceFee = 0.00
                    OW_LongdistansFee = 0.00
                    OW_TravelallowanceFee = 0.00
                    OW_DaFee = 0.00
                    OW_DemoCharge = 0.00
                    OW_InstallationFee = 0.00
                    OW_EcallCharge = 0.00
                    OW_CombatFee = 0.00



                    RevenuewithoutTax = 0.00
                    IWpartsconsumed = 0.00
                    Totalpartsconsumed = 0.00
                    PrimeCostTotal = 0.00
                    GrossProfit = 0.00

                    SONYNeedsToPayIW = 0.0
                    SONYNeedsToPayOW = 0.0

                    IW_TotalAmountOfAccountPayableIW = 0.0
                    OW_TotalAmountOfAccountPayableIW = 0.0

                    'TOT_Repair = 0
                    'TOT_DEMO = 0
                    'TOT_Installation = 0

                    'TOT_WarrantyNumber = 0
                    'TOT_WarrantyNumber = 0
                    'TOT_InWarrantyNumber = 0
                    'TOT_OutWarrantyNumber = 0


                    'TOT_IW_TotalAmountIW = 0
                    'TOT_IW_TotalAmountIW = 0
                    'TOT_IW_TotalAmountOfAccountPayableIW = 0
                    'TOT_IW_AccountPayableByASC = 0
                    'TOT_IW_AccountPayableByCustomer = 0
                    'TOT_SONYNeedsToPayIW = 0
                    'TOT_SONYNeedsToPayOW = 0
                    'TOT_IW_TotalPartsFee = 0
                    'TOT_IW_LabourFee = 0
                    'TOT_IW_InspectionFee = 0
                    'TOT_IW_HandlingFee = 0
                    'TOT_IW_TransportFee = 0
                    'TOT_IW_HomeServiceFee = 0
                    'TOT_IW_LongdistansFee = 0
                    'TOT_IW_TravelallowanceFee = 0
                    'TOT_IW_DaFee = 0
                    'TOT_IW_DemoCharge = 0
                    'TOT_IW_InstallationFee = 0
                    'TOT_IW_EcallCharge = 0
                    'TOT_IW_CombatFee = 0

                    'TOT_OW_TotalAmountOW = 0
                    'TOT_OW_TotalAmountOW = 0
                    'TOT_OW_TotalAmountOfAccountPayableOW = 0
                    'TOT_OW_AccountPayableByASC = 0
                    'TOT_OW_AccountPayableByCustomer = 0
                    'TOT_SONYNeedsToPayOW = 0
                    'TOT_OW_TotalPartsFee = 0
                    'TOT_OW_LabourFee = 0
                    'TOT_OW_InspectionFee = 0
                    'TOT_OW_HandlingFee = 0
                    'TOT_OW_TransportFee = 0
                    'TOT_OW_HomeServiceFee = 0
                    'TOT_OW_LongdistansFee = 0
                    'TOT_OW_TravelallowanceFee = 0
                    'TOT_OW_DaFee = 0
                    'TOT_OW_DemoCharge = 0
                    'TOT_OW_InstallationFee = 0
                    'TOT_OW_EcallCharge = 0
                    'TOT_OW_CombatFee = 0


                    'TOT_RevenuewithoutTax = 0
                    'TOT_IWpartsconsumed = 0
                    'TOT_Totalpartsconsumed = 0
                    'TOT_PrimeCostTotal = 0
                    'TOT_GrossProfit = 0





                    If dtDeliveredSet.Rows(DsetRow).Item("DEMO_CHARGE") IsNot DBNull.Value Then
                        DEMO = comcontrol.Money_Round(dtDeliveredSet.Rows(DsetRow).Item("DEMO_CHARGE"), 2)
                        TOT_DEMO = TOT_DEMO + DEMO
                    End If

                    If dtDeliveredSet.Rows(DsetRow).Item("INSTALLATION_FEE") IsNot DBNull.Value Then
                        Installation = comcontrol.Money_Round(dtDeliveredSet.Rows(DsetRow).Item("INSTALLATION_FEE"), 2)
                        TOT_Installation = TOT_Installation + Installation
                    End If

                    '# C-3 =  C-1 - C - 2
                    TOT_Repair = DEMO - Installation


                    If dtDeliveredSet.Rows(DsetRow).Item("WARRANTY_TYPE") IsNot DBNull.Value Then
                        Select Case UCase(dtDeliveredSet.Rows(DsetRow).Item("WARRANTY_TYPE").ToString())
                            Case "IW"

                                InWarrantyNumber = 1

                                If dtDeliveredSet.Rows(DsetRow).Item("SONY_NEEDS_TO_PAY") IsNot DBNull.Value Then
                                    SONYNeedsToPayIW = comcontrol.Money_Round(dtDeliveredSet.Rows(DsetRow).Item("SONY_NEEDS_TO_PAY"), 2)
                                End If

                                If dtDeliveredSet.Rows(DsetRow).Item("TOTAL_AMOUNT_OF_ACCOUNT_PAYABLE") IsNot DBNull.Value Then
                                    IW_TotalAmountOfAccountPayableIW = comcontrol.Money_Round(dtDeliveredSet.Rows(DsetRow).Item("TOTAL_AMOUNT_OF_ACCOUNT_PAYABLE"), 2)
                                End If


                                If dtDeliveredSet.Rows(DsetRow).Item("ACCOUNT_PAYABLE_BY_ASC") IsNot DBNull.Value Then
                                    IW_AccountPayableByASC = comcontrol.Money_Round(dtDeliveredSet.Rows(DsetRow).Item("ACCOUNT_PAYABLE_BY_ASC"), 2)
                                End If

                                If dtDeliveredSet.Rows(DsetRow).Item("ACCOUNT_PAYABLE_BY_CUSTOMER") IsNot DBNull.Value Then
                                    IW_AccountPayableByCustomer = comcontrol.Money_Round(dtDeliveredSet.Rows(DsetRow).Item("ACCOUNT_PAYABLE_BY_CUSTOMER"), 2)
                                End If

                                If dtDeliveredSet.Rows(DsetRow).Item("TOTAL_PART_FEE") IsNot DBNull.Value Then
                                    IW_TotalPartsFee = comcontrol.Money_Round(dtDeliveredSet.Rows(DsetRow).Item("TOTAL_PART_FEE"), 2)
                                End If

                                If dtDeliveredSet.Rows(DsetRow).Item("LABOUR_FEE") IsNot DBNull.Value Then
                                    IW_LabourFee = comcontrol.Money_Round(dtDeliveredSet.Rows(DsetRow).Item("LABOUR_FEE"), 2)
                                End If

                                If dtDeliveredSet.Rows(DsetRow).Item("INSPECTION_FEE") IsNot DBNull.Value Then
                                    IW_InspectionFee = comcontrol.Money_Round(dtDeliveredSet.Rows(DsetRow).Item("INSPECTION_FEE"), 2)
                                End If

                                If dtDeliveredSet.Rows(DsetRow).Item("HANDLING_FEE") IsNot DBNull.Value Then
                                    IW_HandlingFee = comcontrol.Money_Round(dtDeliveredSet.Rows(DsetRow).Item("HANDLING_FEE"), 2)
                                End If

                                If dtDeliveredSet.Rows(DsetRow).Item("TRANSPORT_FEE") IsNot DBNull.Value Then
                                    IW_TransportFee = comcontrol.Money_Round(dtDeliveredSet.Rows(DsetRow).Item("TRANSPORT_FEE"), 2)
                                End If

                                If dtDeliveredSet.Rows(DsetRow).Item("HOMESERVICE_FEE") IsNot DBNull.Value Then
                                    IW_HomeServiceFee = comcontrol.Money_Round(dtDeliveredSet.Rows(DsetRow).Item("HOMESERVICE_FEE"), 2)
                                End If

                                If dtDeliveredSet.Rows(DsetRow).Item("LONGDISTANCE_FEE") IsNot DBNull.Value Then
                                    IW_LongdistansFee = comcontrol.Money_Round(dtDeliveredSet.Rows(DsetRow).Item("LONGDISTANCE_FEE"), 2)
                                End If

                                If dtDeliveredSet.Rows(DsetRow).Item("TRAVELALLOWANCE_FEE") IsNot DBNull.Value Then
                                    IW_TravelallowanceFee = comcontrol.Money_Round(dtDeliveredSet.Rows(DsetRow).Item("TRAVELALLOWANCE_FEE"), 2)
                                End If

                                If dtDeliveredSet.Rows(DsetRow).Item("DA_FEE") IsNot DBNull.Value Then
                                    IW_DaFee = comcontrol.Money_Round(dtDeliveredSet.Rows(DsetRow).Item("DA_FEE"), 2)
                                End If

                                If dtDeliveredSet.Rows(DsetRow).Item("DEMO_CHARGE") IsNot DBNull.Value Then
                                    IW_DemoCharge = comcontrol.Money_Round(dtDeliveredSet.Rows(DsetRow).Item("DEMO_CHARGE"), 2)
                                End If


                                If dtDeliveredSet.Rows(DsetRow).Item("INSTALLATION_FEE") IsNot DBNull.Value Then
                                    IW_InstallationFee = comcontrol.Money_Round(dtDeliveredSet.Rows(DsetRow).Item("INSTALLATION_FEE"), 2)
                                End If

                                If dtDeliveredSet.Rows(DsetRow).Item("ECALL_CHARGE") IsNot DBNull.Value Then
                                    IW_EcallCharge = comcontrol.Money_Round(dtDeliveredSet.Rows(DsetRow).Item("ECALL_CHARGE"), 2)
                                End If

                                If dtDeliveredSet.Rows(DsetRow).Item("COMBAT_FEE") IsNot DBNull.Value Then
                                    IW_CombatFee = comcontrol.Money_Round(dtDeliveredSet.Rows(DsetRow).Item("COMBAT_FEE"), 2)
                                End If



                            Case "OW"


                                OutWarrantyNumber = 1

                                If dtDeliveredSet.Rows(DsetRow).Item("SONY_NEEDS_TO_PAY") IsNot DBNull.Value Then
                                    SONYNeedsToPayOW = comcontrol.Money_Round(dtDeliveredSet.Rows(DsetRow).Item("SONY_NEEDS_TO_PAY"), 2)
                                End If

                                If dtDeliveredSet.Rows(DsetRow).Item("TOTAL_AMOUNT_OF_ACCOUNT_PAYABLE") IsNot DBNull.Value Then
                                    OW_TotalAmountOfAccountPayableOW = comcontrol.Money_Round(dtDeliveredSet.Rows(DsetRow).Item("TOTAL_AMOUNT_OF_ACCOUNT_PAYABLE"), 2)
                                End If


                                If dtDeliveredSet.Rows(DsetRow).Item("ACCOUNT_PAYABLE_BY_ASC") IsNot DBNull.Value Then
                                    OW_AccountPayableByASC = comcontrol.Money_Round(dtDeliveredSet.Rows(DsetRow).Item("ACCOUNT_PAYABLE_BY_ASC"), 2)
                                End If

                                If dtDeliveredSet.Rows(DsetRow).Item("ACCOUNT_PAYABLE_BY_CUSTOMER") IsNot DBNull.Value Then
                                    OW_AccountPayableByCustomer = comcontrol.Money_Round(dtDeliveredSet.Rows(DsetRow).Item("ACCOUNT_PAYABLE_BY_CUSTOMER"), 2)
                                End If

                                If dtDeliveredSet.Rows(DsetRow).Item("TOTAL_PART_FEE") IsNot DBNull.Value Then
                                    OW_TotalPartsFee = comcontrol.Money_Round(dtDeliveredSet.Rows(DsetRow).Item("TOTAL_PART_FEE"), 2)
                                End If

                                If dtDeliveredSet.Rows(DsetRow).Item("LABOUR_FEE") IsNot DBNull.Value Then
                                    OW_LabourFee = comcontrol.Money_Round(dtDeliveredSet.Rows(DsetRow).Item("LABOUR_FEE"), 2)
                                End If

                                If dtDeliveredSet.Rows(DsetRow).Item("INSPECTION_FEE") IsNot DBNull.Value Then
                                    OW_InspectionFee = comcontrol.Money_Round(dtDeliveredSet.Rows(DsetRow).Item("INSPECTION_FEE"), 2)
                                End If

                                If dtDeliveredSet.Rows(DsetRow).Item("HANDLING_FEE") IsNot DBNull.Value Then
                                    OW_HandlingFee = comcontrol.Money_Round(dtDeliveredSet.Rows(DsetRow).Item("HANDLING_FEE"), 2)
                                End If

                                If dtDeliveredSet.Rows(DsetRow).Item("TRANSPORT_FEE") IsNot DBNull.Value Then
                                    OW_TransportFee = comcontrol.Money_Round(dtDeliveredSet.Rows(DsetRow).Item("TRANSPORT_FEE"), 2)
                                End If

                                If dtDeliveredSet.Rows(DsetRow).Item("HOMESERVICE_FEE") IsNot DBNull.Value Then
                                    OW_HomeServiceFee = comcontrol.Money_Round(dtDeliveredSet.Rows(DsetRow).Item("HOMESERVICE_FEE"), 2)
                                End If

                                If dtDeliveredSet.Rows(DsetRow).Item("LONGDISTANCE_FEE") IsNot DBNull.Value Then
                                    OW_LongdistansFee = comcontrol.Money_Round(dtDeliveredSet.Rows(DsetRow).Item("LONGDISTANCE_FEE"), 2)
                                End If

                                If dtDeliveredSet.Rows(DsetRow).Item("TRAVELALLOWANCE_FEE") IsNot DBNull.Value Then
                                    OW_TravelallowanceFee = comcontrol.Money_Round(dtDeliveredSet.Rows(DsetRow).Item("TRAVELALLOWANCE_FEE"), 2)
                                End If

                                If dtDeliveredSet.Rows(DsetRow).Item("DA_FEE") IsNot DBNull.Value Then
                                    OW_DaFee = comcontrol.Money_Round(dtDeliveredSet.Rows(DsetRow).Item("DA_FEE"), 2)
                                End If

                                If dtDeliveredSet.Rows(DsetRow).Item("DEMO_CHARGE") IsNot DBNull.Value Then
                                    OW_DemoCharge = comcontrol.Money_Round(dtDeliveredSet.Rows(DsetRow).Item("DEMO_CHARGE"), 2)
                                End If

                                If dtDeliveredSet.Rows(DsetRow).Item("INSTALLATION_FEE") IsNot DBNull.Value Then
                                    OW_InstallationFee = comcontrol.Money_Round(dtDeliveredSet.Rows(DsetRow).Item("INSTALLATION_FEE"), 2)
                                End If

                                If dtDeliveredSet.Rows(DsetRow).Item("ECALL_CHARGE") IsNot DBNull.Value Then
                                    OW_EcallCharge = comcontrol.Money_Round(dtDeliveredSet.Rows(DsetRow).Item("ECALL_CHARGE"), 2)
                                End If

                                If dtDeliveredSet.Rows(DsetRow).Item("COMBAT_FEE") IsNot DBNull.Value Then
                                    OW_CombatFee = comcontrol.Money_Round(dtDeliveredSet.Rows(DsetRow).Item("COMBAT_FEE"), 2)
                                End If

                        End Select

                    End If

                    TOT_InWarrantyNumber = TOT_InWarrantyNumber + InWarrantyNumber
                    TOT_OutWarrantyNumber = TOT_OutWarrantyNumber + OutWarrantyNumber

                    TOT_IW_TotalAmountOfAccountPayableIW = TOT_IW_TotalAmountOfAccountPayableIW + IW_TotalAmountOfAccountPayableIW
                    TOT_OW_TotalAmountOfAccountPayableOW = TOT_OW_TotalAmountOfAccountPayableOW + OW_TotalAmountOfAccountPayableOW

                    TOT_IW_AccountPayableByASC = TOT_IW_AccountPayableByASC + IW_AccountPayableByASC
                    TOT_OW_AccountPayableByASC = TOT_OW_AccountPayableByASC + OW_AccountPayableByASC

                    TOT_IW_AccountPayableByCustomer = TOT_IW_AccountPayableByCustomer + IW_AccountPayableByCustomer
                    TOT_OW_AccountPayableByCustomer = TOT_OW_AccountPayableByCustomer + OW_AccountPayableByCustomer

                    TOT_SONYNeedsToPayIW = TOT_SONYNeedsToPayIW + SONYNeedsToPayIW
                    TOT_SONYNeedsToPayOW = TOT_SONYNeedsToPayOW + SONYNeedsToPayOW

                    TOT_IW_TotalPartsFee = TOT_IW_TotalPartsFee + IW_TotalPartsFee
                    TOT_OW_TotalPartsFee = TOT_OW_TotalPartsFee + OW_TotalPartsFee

                    TOT_IW_LabourFee = TOT_IW_LabourFee + IW_LabourFee
                    TOT_OW_LabourFee = TOT_OW_LabourFee + OW_LabourFee

                    TOT_IW_InspectionFee = TOT_IW_InspectionFee + IW_InspectionFee
                    TOT_OW_InspectionFee = TOT_OW_InspectionFee + OW_InspectionFee

                    TOT_IW_HandlingFee = TOT_IW_HandlingFee + IW_HandlingFee
                    TOT_OW_HandlingFee = TOT_OW_HandlingFee + OW_HandlingFee

                    TOT_IW_TransportFee = TOT_IW_TransportFee + IW_TransportFee
                    TOT_OW_TransportFee = TOT_OW_TransportFee + OW_TransportFee

                    TOT_IW_HomeServiceFee = TOT_IW_HomeServiceFee + IW_HomeServiceFee
                    TOT_OW_HomeServiceFee = TOT_OW_HomeServiceFee + OW_HomeServiceFee

                    TOT_IW_LongdistansFee = TOT_IW_LongdistansFee + IW_LongdistansFee
                    TOT_OW_LongdistansFee = TOT_OW_LongdistansFee + OW_LongdistansFee

                    TOT_IW_TravelallowanceFee = TOT_IW_TravelallowanceFee + IW_TravelallowanceFee
                    TOT_OW_TravelallowanceFee = TOT_OW_TravelallowanceFee + OW_TravelallowanceFee

                    TOT_IW_DaFee = TOT_IW_DaFee + IW_DaFee
                    TOT_OW_DaFee = TOT_OW_DaFee + OW_DaFee

                    TOT_IW_DemoCharge = TOT_IW_DemoCharge + IW_DemoCharge
                    TOT_OW_DemoCharge = TOT_OW_DemoCharge + OW_DemoCharge

                    TOT_IW_InstallationFee = TOT_IW_InstallationFee + IW_InstallationFee
                    TOT_OW_InstallationFee = TOT_OW_InstallationFee + OW_InstallationFee

                    TOT_IW_EcallCharge = TOT_IW_EcallCharge + IW_EcallCharge
                    TOT_OW_EcallCharge = TOT_OW_EcallCharge + OW_EcallCharge

                    TOT_IW_CombatFee = TOT_IW_CombatFee + IW_CombatFee
                    TOT_OW_CombatFee = TOT_OW_CombatFee + OW_CombatFee

                Next


            End If


            'Final For a Day 
            '# Warranty (Number) = (In Warranty (Number) + Out Warranty (Number)
            'rowWork1 &= "," & strDate
            'rowWork2 &= "," & TOT_Repair
            'rowWork3 &= "," & TOT_DEMO
            'rowWork4 &= "," & TOT_Installation
            'rowWork5 &= ","

            TOT_WarrantyNumber = TOT_InWarrantyNumber + TOT_OutWarrantyNumber
            'rowWork6 &= "," & TOT_WarrantyNumber
            'rowWork7 &= "," & TOT_InWarrantyNumber
            'rowWork8 &= "," & TOT_OutWarrantyNumber


            '#Total Amount IW (SONY Needs To Pay/IW + SONY Needs To Pay/OW ) /1.18
            TOT_IW_TotalAmountIW = (TOT_SONYNeedsToPayIW + TOT_SONYNeedsToPayOW) / 1.18
            TOT_IW_TotalAmountOfAccountPayableIW = TOT_IW_TotalPartsFee + TOT_IW_LabourFee + TOT_IW_InspectionFee + TOT_IW_HandlingFee + TOT_IW_TransportFee + TOT_IW_HomeServiceFee + TOT_IW_LongdistansFee + TOT_IW_TravelallowanceFee + TOT_IW_DaFee + TOT_IW_DemoCharge + TOT_IW_InstallationFee + TOT_IW_EcallCharge + TOT_IW_CombatFee
            'rowWork9 &= "," & Math.Floor(TOT_IW_TotalAmountIW)
            'rowWork10 &= "," & Math.Floor(TOT_IW_TotalAmountOfAccountPayableIW)
            'rowWork11 &= "," & Math.Floor(TOT_IW_AccountPayableByASC)
            'rowWork12 &= "," & Math.Floor(TOT_IW_AccountPayableByCustomer)
            'rowWork13 &= "," & Math.Floor(TOT_SONYNeedsToPayIW)
            'rowWork14 &= "," & Math.Floor(TOT_SONYNeedsToPayOW)
            'rowWork15 &= "," & Math.Floor(TOT_IW_TotalPartsFee)
            'rowWork16 &= "," & Math.Floor(TOT_IW_LabourFee)
            'rowWork17 &= "," & Math.Floor(TOT_IW_InspectionFee)
            'rowWork18 &= "," & Math.Floor(TOT_IW_HandlingFee)
            'rowWork19 &= "," & Math.Floor(TOT_IW_TransportFee)
            'rowWork20 &= "," & Math.Floor(TOT_IW_HomeServiceFee)
            'rowWork21 &= "," & Math.Floor(TOT_IW_LongdistansFee)
            'rowWork22 &= "," & Math.Floor(TOT_IW_TravelallowanceFee)
            'rowWork23 &= "," & Math.Floor(TOT_IW_DaFee)
            'rowWork24 &= "," & Math.Floor(TOT_IW_DemoCharge)
            'rowWork25 &= "," & Math.Floor(TOT_IW_InstallationFee)
            'rowWork26 &= "," & Math.Floor(TOT_IW_EcallCharge)
            'rowWork27 &= "," & Math.Floor(TOT_IW_CombatFee)

            '#Total Amount OW (Total Amount of Account Payable OW - Account Payable By ASC - SONY Needs To Pay ) / 1.18
            TOT_OW_TotalAmountOW = (TOT_OW_TotalAmountOfAccountPayableOW - TOT_OW_AccountPayableByASC - TOT_SONYNeedsToPayOW) / 1.18
            'rowWork28 &= "," & Math.Floor(TOT_OW_TotalAmountOW) '& ":" & TOT_OW_TotalAmountOfAccountPayableOW & ":" & TOT_IW_AccountPayableByASC & ":" & TOT_SONYNeedsToPayOW
            'rowWork29 &= "," & Math.Floor(TOT_OW_TotalAmountOfAccountPayableOW)
            'rowWork30 &= "," & Math.Floor(TOT_OW_AccountPayableByASC)
            'rowWork31 &= "," & Math.Floor(TOT_OW_AccountPayableByCustomer)
            'rowWork32 &= "," & Math.Floor(TOT_SONYNeedsToPayOW)
            'rowWork33 &= "," & Math.Floor(TOT_OW_TotalPartsFee)
            'rowWork34 &= "," & Math.Floor(TOT_OW_LabourFee)
            'rowWork35 &= "," & Math.Floor(TOT_OW_InspectionFee)
            '#Handling Fees = =D32+D36 (Account Payable By ASC + Labour Fee )
            'TOT_OW_HandlingFee = TOT_OW_AccountPayableByASC + TOT_OW_LabourFee

            'rowWork36 &= "," & Math.Floor(TOT_OW_HandlingFee)
            'rowWork37 &= "," & Math.Floor(TOT_OW_TransportFee)
            'rowWork38 &= "," & Math.Floor(TOT_OW_HomeServiceFee)
            'rowWork39 &= "," & Math.Floor(TOT_OW_LongdistansFee)
            'rowWork40 &= "," & Math.Floor(TOT_OW_TravelallowanceFee)
            'rowWork41 &= "," & Math.Floor(TOT_OW_DaFee)
            'rowWork42 &= "," & Math.Floor(TOT_OW_DemoCharge)
            'rowWork43 &= "," & Math.Floor(TOT_OW_InstallationFee)
            'rowWork44 &= "," & Math.Floor(TOT_OW_EcallCharge)
            'rowWork45 &= "," & Math.Floor(TOT_OW_CombatFee)


            '#RevenuewithoutTax = Total Amount IW + Total Amount OW
            TOT_RevenuewithoutTax = TOT_IW_TotalAmountIW + TOT_OW_TotalAmountOW
            'rowWork46 &= "," & Math.Floor(TOT_RevenuewithoutTax)
            'rowWork47 &= ","
            '#IW parts consumed = Total Parts Fee * 0.88
            TOT_IWpartsconsumed = TOT_IW_TotalPartsFee * 0.88
            'rowWork48 &= "," & Math.Floor(TOT_IWpartsconsumed)

            '#Total parts consumed =   Total Parts Fee (IW)  + Total Parts Fee (OW)
            TOT_Totalpartsconsumed = TOT_IW_TotalPartsFee + TOT_OW_TotalPartsFee
            'rowWork49 &= "," & Math.Floor(TOT_Totalpartsconsumed)
            '#Prime Cost Total =  Totalpartsconsumed -  IWpartsconsumed
            TOT_PrimeCostTotal = TOT_Totalpartsconsumed - TOT_IWpartsconsumed
            'rowWork50 &= "," & Math.Floor(TOT_PrimeCostTotal)
            '#Gross Profit = Revenue without Tax - Prime Cost Total
            TOT_GrossProfit = TOT_RevenuewithoutTax - TOT_PrimeCostTotal
            'rowWork51 &= "," & Math.Floor(TOT_GrossProfit)
            Try
                TOT_Percentage = (TOT_GrossProfit / TOT_RevenuewithoutTax) * 100
            Catch ex As Exception
                TOT_Percentage = 0
            End Try


            'rowWork52 &= "," & Math.Floor(TOT_Percentage) & "%"



            '**************************************
            'Add Single Day Value
            Dim Row1 As DataRow
            Row1 = tblSIDSales.NewRow()

            Row1.Item("SalesDate") = strDate
            Row1.Item("SalesAmount") = "" & Math.Floor(TOT_GrossProfit)
            tblSIDSales.Rows.Add(Row1)
            '***********************************************



            NET_TOT_Repair = NET_TOT_Repair + TOT_Repair
            NET_TOT_DEMO = NET_TOT_DEMO + TOT_DEMO
            NET_TOT_Installation = NET_TOT_Installation + TOT_Installation

            NET_TOT_WarrantyNumber = NET_TOT_WarrantyNumber + TOT_WarrantyNumber
            NET_TOT_InWarrantyNumber = NET_TOT_InWarrantyNumber + TOT_InWarrantyNumber
            NET_TOT_OutWarrantyNumber = NET_TOT_OutWarrantyNumber + TOT_OutWarrantyNumber


            '#Total Amount IW (SONY Needs To Pay/IW + SONY Needs To Pay/OW ) /1.18
            NET_TOT_IW_TotalAmountIW = NET_TOT_IW_TotalAmountIW + TOT_IW_TotalAmountIW
            NET_TOT_IW_TotalAmountOfAccountPayableIW = NET_TOT_IW_TotalAmountOfAccountPayableIW + TOT_IW_TotalAmountOfAccountPayableIW
            NET_TOT_IW_AccountPayableByASC = NET_TOT_IW_AccountPayableByASC + TOT_IW_AccountPayableByASC
            NET_TOT_IW_AccountPayableByCustomer = NET_TOT_IW_AccountPayableByCustomer + TOT_IW_AccountPayableByCustomer
            NET_TOT_SONYNeedsToPayIW = NET_TOT_SONYNeedsToPayIW + TOT_SONYNeedsToPayIW
            NET_TOT_SONYNeedsToPayOW = NET_TOT_SONYNeedsToPayOW + TOT_SONYNeedsToPayOW
            NET_TOT_IW_TotalPartsFee = NET_TOT_IW_TotalPartsFee + TOT_IW_TotalPartsFee
            NET_TOT_IW_LabourFee = NET_TOT_IW_LabourFee + TOT_IW_LabourFee
            NET_TOT_IW_InspectionFee = NET_TOT_IW_InspectionFee + TOT_IW_InspectionFee
            NET_TOT_IW_HandlingFee = NET_TOT_IW_HandlingFee + TOT_IW_HandlingFee
            NET_TOT_IW_TransportFee = NET_TOT_IW_TransportFee + TOT_IW_TransportFee
            NET_TOT_IW_HomeServiceFee = NET_TOT_IW_HomeServiceFee + TOT_IW_HomeServiceFee
            NET_TOT_IW_LongdistansFee = NET_TOT_IW_LongdistansFee + TOT_IW_LongdistansFee
            NET_TOT_IW_TravelallowanceFee = NET_TOT_IW_TravelallowanceFee + TOT_IW_TravelallowanceFee
            NET_TOT_IW_DaFee = NET_TOT_IW_DaFee + TOT_IW_DaFee
            NET_TOT_IW_DemoCharge = NET_TOT_IW_DemoCharge + TOT_IW_DemoCharge
            NET_TOT_IW_InstallationFee = NET_TOT_IW_InstallationFee + TOT_IW_InstallationFee
            NET_TOT_IW_EcallCharge = NET_TOT_IW_EcallCharge + TOT_IW_EcallCharge
            NET_TOT_IW_CombatFee = NET_TOT_IW_CombatFee + TOT_IW_CombatFee

            '#Total Amount OW (Total Amount of Account Payable OW - Account Payable By ASC - SONY Needs To Pay ) / 1.18
            NET_TOT_OW_TotalAmountOW = NET_TOT_OW_TotalAmountOW + TOT_OW_TotalAmountOW
            NET_TOT_OW_TotalAmountOfAccountPayableOW = NET_TOT_OW_TotalAmountOfAccountPayableOW + TOT_OW_TotalAmountOfAccountPayableOW
            NET_TOT_OW_AccountPayableByASC = NET_TOT_OW_AccountPayableByASC + TOT_OW_AccountPayableByASC
            NET_TOT_OW_AccountPayableByCustomer = NET_TOT_OW_AccountPayableByCustomer + TOT_OW_AccountPayableByCustomer
            NET_TOT_SONYNeedsToPayOW = NET_TOT_SONYNeedsToPayOW + TOT_SONYNeedsToPayOW
            NET_TOT_OW_TotalPartsFee = NET_TOT_OW_TotalPartsFee + TOT_OW_TotalPartsFee
            NET_TOT_OW_LabourFee = NET_TOT_OW_LabourFee + TOT_OW_LabourFee
            NET_TOT_OW_InspectionFee = NET_TOT_OW_InspectionFee + TOT_OW_InspectionFee

            '#Handling Fees = =D32+D36 (Account Payable By ASC + Labour Fee )
            NET_TOT_OW_HandlingFee = NET_TOT_OW_HandlingFee + TOT_OW_HandlingFee
            NET_TOT_OW_TransportFee = NET_TOT_OW_TransportFee + TOT_OW_TransportFee
            NET_TOT_OW_HomeServiceFee = NET_TOT_OW_HomeServiceFee + TOT_OW_HomeServiceFee
            NET_TOT_OW_LongdistansFee = NET_TOT_OW_LongdistansFee + TOT_OW_LongdistansFee
            NET_TOT_OW_TravelallowanceFee = NET_TOT_OW_TravelallowanceFee + TOT_OW_TravelallowanceFee
            NET_TOT_OW_DaFee = NET_TOT_OW_DaFee + TOT_OW_DaFee
            NET_TOT_OW_DemoCharge = NET_TOT_OW_DemoCharge + TOT_OW_DemoCharge
            NET_TOT_OW_InstallationFee = NET_TOT_OW_InstallationFee + TOT_OW_InstallationFee
            NET_TOT_OW_EcallCharge = NET_TOT_OW_EcallCharge + TOT_OW_EcallCharge
            NET_TOT_OW_CombatFee = NET_TOT_OW_CombatFee + TOT_OW_CombatFee


            NET_TOT_RevenuewithoutTax = NET_TOT_RevenuewithoutTax + TOT_RevenuewithoutTax
            NET_TOT_IWpartsconsumed = NET_TOT_IWpartsconsumed + TOT_IWpartsconsumed
            NET_TOT_Totalpartsconsumed = NET_TOT_Totalpartsconsumed + TOT_Totalpartsconsumed
            NET_TOT_PrimeCostTotal = NET_TOT_PrimeCostTotal + TOT_PrimeCostTotal
            NET_TOT_GrossProfit = NET_TOT_GrossProfit + TOT_GrossProfit

            '********************************
            'NetTotal
            '*******************************
        Next


        'Final For a Day 
        '# Warranty (Number) = (In Warranty (Number) + Out Warranty (Number)
        'rowWork1 &= "," & "Total"
        'rowWork2 &= "," & NET_TOT_Repair
        'rowWork3 &= "," & NET_TOT_DEMO
        'rowWork4 &= "," & NET_TOT_Installation
        'rowWork5 &= ","


        'rowWork6 &= "," & NET_TOT_WarrantyNumber
        'rowWork7 &= "," & NET_TOT_InWarrantyNumber
        'rowWork8 &= "," & NET_TOT_OutWarrantyNumber


        '#Total Amount IW (SONY Needs To Pay/IW + SONY Needs To Pay/OW ) /1.18

        'rowWork9 &= "," & Math.Floor(NET_TOT_IW_TotalAmountIW)
        'rowWork10 &= "," & Math.Floor(NET_TOT_IW_TotalAmountOfAccountPayableIW)
        'rowWork11 &= "," & Math.Floor(NET_TOT_IW_AccountPayableByASC)
        'rowWork12 &= "," & Math.Floor(NET_TOT_IW_AccountPayableByCustomer)
        'rowWork13 &= "," & Math.Floor(NET_TOT_SONYNeedsToPayIW)
        'rowWork14 &= "," & Math.Floor(NET_TOT_SONYNeedsToPayOW)
        'rowWork15 &= "," & Math.Floor(NET_TOT_IW_TotalPartsFee)
        'rowWork16 &= "," & Math.Floor(NET_TOT_IW_LabourFee)
        'rowWork17 &= "," & Math.Floor(NET_TOT_IW_InspectionFee)
        'rowWork18 &= "," & Math.Floor(NET_TOT_IW_HandlingFee)
        'rowWork19 &= "," & Math.Floor(NET_TOT_IW_TransportFee)
        'rowWork20 &= "," & Math.Floor(NET_TOT_IW_HomeServiceFee)
        'rowWork21 &= "," & Math.Floor(NET_TOT_IW_LongdistansFee)
        'rowWork22 &= "," & Math.Floor(NET_TOT_IW_TravelallowanceFee)
        'rowWork23 &= "," & Math.Floor(NET_TOT_IW_DaFee)
        'rowWork24 &= "," & Math.Floor(NET_TOT_IW_DemoCharge)
        'rowWork25 &= "," & Math.Floor(NET_TOT_IW_InstallationFee)
        'rowWork26 &= "," & Math.Floor(NET_TOT_IW_EcallCharge)
        'rowWork27 &= "," & Math.Floor(NET_TOT_IW_CombatFee)

        '#Total Amount OW (Total Amount of Account Payable OW - Account Payable By ASC - SONY Needs To Pay ) / 1.18

        'rowWork28 &= "," & Math.Floor(NET_TOT_OW_TotalAmountOW)
        'rowWork29 &= "," & Math.Floor(NET_TOT_OW_TotalAmountOfAccountPayableOW)
        'rowWork30 &= "," & Math.Floor(NET_TOT_OW_AccountPayableByASC)
        'rowWork31 &= "," & Math.Floor(NET_TOT_OW_AccountPayableByCustomer)
        'rowWork32 &= "," & Math.Floor(NET_TOT_SONYNeedsToPayOW)
        'rowWork33 &= "," & Math.Floor(NET_TOT_OW_TotalPartsFee)
        'rowWork34 &= "," & Math.Floor(NET_TOT_OW_LabourFee)
        'rowWork35 &= "," & Math.Floor(NET_TOT_OW_InspectionFee)
        '#Handling Fees = =D32+D36 (Account Payable By ASC + Labour Fee )

        'rowWork36 &= "," & Math.Floor(NET_TOT_OW_HandlingFee)
        'rowWork37 &= "," & Math.Floor(NET_TOT_OW_TransportFee)
        'rowWork38 &= "," & Math.Floor(NET_TOT_OW_HomeServiceFee)
        'rowWork39 &= "," & Math.Floor(NET_TOT_OW_LongdistansFee)
        'rowWork40 &= "," & Math.Floor(NET_TOT_OW_TravelallowanceFee)
        'rowWork41 &= "," & Math.Floor(NET_TOT_OW_DaFee)
        'rowWork42 &= "," & Math.Floor(NET_TOT_OW_DemoCharge)
        'rowWork43 &= "," & Math.Floor(NET_TOT_OW_InstallationFee)
        'rowWork44 &= "," & Math.Floor(NET_TOT_OW_EcallCharge)
        'rowWork45 &= "," & Math.Floor(NET_TOT_OW_CombatFee)


        '#RevenuewithoutTax = Total Amount IW + Total Amount OW

        'rowWork46 &= "," & Math.Floor(NET_TOT_RevenuewithoutTax)
        'rowWork47 &= ","
        '#IW parts consumed = Total Parts Fee * 0.88
        'rowWork48 &= "," & Math.Floor(NET_TOT_IWpartsconsumed)
        '#Total parts consumed =   Total Parts Fee (IW)  + Total Parts Fee (OW)
        'rowWork49 &= "," & Math.Floor(NET_TOT_Totalpartsconsumed)
        '#Prime Cost Total =  Totalpartsconsumed -  IWpartsconsumed
        'rowWork50 &= "," & Math.Floor(NET_TOT_PrimeCostTotal)
        '#Gross Profit = Revenue without Tax - Prime Cost Total
        'rowWork51 &= "," & Math.Floor(NET_TOT_GrossProfit)

        Try
            NET_TOT_Percentage = (NET_TOT_GrossProfit / NET_TOT_RevenuewithoutTax) * 100
        Catch ex As Exception
            NET_TOT_Percentage = 0
        End Try


        'rowWork52 &= "," & Math.Floor(NET_TOT_Percentage) & "%"
        '********************************
        'End
        '***********************************
        'Last Left Column Begin
        'rowWork1 &= ",SID1"
        'rowWork2 &= ",Repair"
        'rowWork3 &= ",DEMO"
        'rowWork4 &= ",Installation"
        'rowWork5 &= ","
        'rowWork6 &= ",Warranty (Number)"
        'rowWork7 &= ",In Warranty (Number)"
        'rowWork8 &= ",Out Warranty (Number)"
        'rowWork9 &= ",Total Amount IW"
        'rowWork10 &= ",Total Amount of Account Payable IW"
        'rowWork11 &= ",Account Payable By ASC"
        'rowWork12 &= ",Account Payable By Customer"
        'rowWork13 &= ",SONY Needs To Pay/IW"
        'rowWork14 &= ",SONY Needs To Pay/OW"
        'rowWork15 &= ",Total Parts Fee"
        'rowWork16 &= ",Labour Fee"
        'rowWork17 &= ",Inspection Fee"
        'rowWork18 &= ",Handling Fee"
        'rowWork19 &= ",Transport Fee"
        'rowWork20 &= ",HomeService Fee"
        'rowWork21 &= ",Longdistans Fee"
        'rowWork22 &= ",Travelallowance Fee"
        'rowWork23 &= ",Da Fee"
        'rowWork24 &= ",Demo Charge"
        'rowWork25 &= ",Installation Fee"
        'rowWork26 &= ",Ecall Charge"
        'rowWork27 &= ",Combat Fee"
        'rowWork28 &= ",Total Amount OW"
        'rowWork29 &= ",Total Amount of Account Payable OW"
        'rowWork30 &= ",Account Payable By ASC"
        'rowWork31 &= ",Account Payable By Customer"
        'rowWork32 &= ",SONY Needs To Pay"
        'rowWork33 &= ",Total Parts Fee"
        'rowWork34 &= ",Labour Fee"
        'rowWork35 &= ",Inspection Fee"
        'rowWork36 &= ",Handling Fee"
        'rowWork37 &= ",Transport Fee"
        'rowWork38 &= ",HomeService Fee"
        'rowWork39 &= ",Longdistans Fee"
        'rowWork40 &= ",Travelallowance Fee"
        'rowWork41 &= ",Da Fee"
        'rowWork42 &= ",Demo Charge"
        'rowWork43 &= ",Installation Fee"
        'rowWork44 &= ",Ecall Charge"
        'rowWork45 &= ",Combat Fee"
        'rowWork46 &= ",Revenue without Tax"
        'rowWork47 &= ","
        'rowWork48 &= ",IW parts consumed"
        'rowWork49 &= ",Total parts consumed"
        'rowWork50 &= ",Prime Cost Total"
        'rowWork51 &= ",Gross Profit"
        'rowWork52 &= ","
        'Last Left Column End


        'csvContents.Add(rowWork1)
        'csvContents.Add(rowWork2)
        'csvContents.Add(rowWork3)
        'csvContents.Add(rowWork4)
        'csvContents.Add(rowWork5)
        'csvContents.Add(rowWork6)
        'csvContents.Add(rowWork7)
        'csvContents.Add(rowWork8)
        'csvContents.Add(rowWork9)
        'csvContents.Add(rowWork10)
        'csvContents.Add(rowWork11)
        'csvContents.Add(rowWork12)
        'csvContents.Add(rowWork13)
        'csvContents.Add(rowWork14)
        'csvContents.Add(rowWork15)
        'csvContents.Add(rowWork16)
        'csvContents.Add(rowWork17)
        'csvContents.Add(rowWork18)
        'csvContents.Add(rowWork19)
        'csvContents.Add(rowWork20)
        'csvContents.Add(rowWork21)
        'csvContents.Add(rowWork22)
        'csvContents.Add(rowWork23)
        'csvContents.Add(rowWork24)
        'csvContents.Add(rowWork25)
        'csvContents.Add(rowWork26)
        'csvContents.Add(rowWork27)
        'csvContents.Add(rowWork28)
        'csvContents.Add(rowWork29)
        'csvContents.Add(rowWork30)
        'csvContents.Add(rowWork31)
        'csvContents.Add(rowWork32)
        'csvContents.Add(rowWork33)
        'csvContents.Add(rowWork34)
        'csvContents.Add(rowWork35)
        'csvContents.Add(rowWork36)
        'csvContents.Add(rowWork37)
        'csvContents.Add(rowWork38)
        'csvContents.Add(rowWork39)
        'csvContents.Add(rowWork40)
        'csvContents.Add(rowWork41)
        'csvContents.Add(rowWork42)
        'csvContents.Add(rowWork43)
        'csvContents.Add(rowWork44)
        'csvContents.Add(rowWork45)
        'csvContents.Add(rowWork46)
        'csvContents.Add(rowWork47)
        'csvContents.Add(rowWork48)
        'csvContents.Add(rowWork49)
        'csvContents.Add(rowWork50)
        'csvContents.Add(rowWork51)
        'csvContents.Add(rowWork52)

        'Dim clsSet As New Class_analysis
        'Dim outputPath As String
        'Dim csvFileName As String
        'Dim dateFrom As String = strYear & "/" & strMonth & "/01"
        'Dim dateTo As String = strYear & "/" & strMonth & "/" & strMaxDay
        'Dim exportFile As String = ""
        'Dim setMon As String = ""
        'Dim exportShipName As String = "SID1"
        'Dim dtNow As DateTime = clsSetCommon.dtIndia


        'exportFile = "0.PL_Tracking_Sheet"

        'dateFrom = Replace(dateFrom, "/", "")
        'dateTo = Replace(dateTo, "/", "")

        ''exportFile名の頭、数値を除く
        ''0.PL_Tracking_Sheet
        'exportFile = Right(exportFile, exportFile.Length - 2)

        'If setMon = "00" Then
        '    If dateTo <> "" And dateFrom <> "" Then
        '        csvFileName = exportFile & "_ " & exportShipName & "_" & dateFrom & "_" & dateTo & ".csv"
        '    Else
        '        If dateTo <> "" Then
        '            csvFileName = exportFile & "_ " & exportShipName & "_" & dateTo & ".csv"
        '        End If
        '        If dateFrom <> "" Then
        '            csvFileName = exportFile & "_ " & exportShipName & "_" & dateFrom & ".csv"
        '        End If
        '    End If
        'Else
        '    '月指定のとき
        '    csvFileName = exportFile & "_ " & exportShipName & "_" & dtNow.ToString("yyyy") & "_" & setMon & ".csv"
        'End If

        'outputPath = clsSet.CsvFilePass & csvFileName

        'Using writer As New StreamWriter(outputPath, False, Encoding.Default)
        '    writer.WriteLine(String.Join(Environment.NewLine, csvContents))
        'End Using

        ''ファイルの内容をバイト配列にすべて読み込む 
        'Dim Buffer As Byte() = System.IO.File.ReadAllBytes(outputPath)

        ''サーバに保存されたCSVファイルを削除
        ''※Response.End以降、ファイル削除処理ができないため、ファイルのダウンロードではなく、一旦ファイルの内容を
        ''上記、Bufferに保存し、ダウンロード処理を行う。

        'If outputPath <> "" Then
        '    If System.IO.File.Exists(outputPath) = True Then
        '        System.IO.File.Delete(outputPath)
        '    End If
        'End If

        '' ダウンロード
        'Response.ContentType = "application/text/csv"
        'Response.AddHeader("Content-Disposition", "attachment; filename=" & csvFileName)
        'Response.Flush()
        ''Response.Write("<b>File Contents: </b>")
        'Response.BinaryWrite(Buffer)
        ''Response.WriteFile(outputPath)
        'Response.End()
        Return tblSIDSales

    End Function
End Class
