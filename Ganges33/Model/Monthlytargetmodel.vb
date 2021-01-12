Imports System
Imports System.Collections.Generic
Imports System.Configuration
Namespace Ganges33.model
    Public Class Monthlytargetmodel
        Public Sub Monthlytargetmodel()
            UNQ_NO = String.Empty
            CRTDT = String.Empty
            CRTCD = String.Empty
            UPDDT = String.Empty
            UPDCD = String.Empty
            UPDPG = String.Empty
            DELFG = String.Empty
            UserId = String.Empty
            'UNQ_NO = String.Empty
            UPLOAD_USER = String.Empty
            UPLOAD_DATE = String.Empty
            SHIP_TO_BRANCH_CODE = String.Empty
            SHIP_TO_BRANCH = String.Empty
            'VALUE = String.Empty
            TARGET_MONTH = String.Empty
            TARGET_YEAR = String.Empty
            TARGET_MONTH_AMOUNT = String.Empty
            TARGET_DAY_AMOUNT = String.Empty
            FILE_NAME = String.Empty
            SRC_FILE_NAME = String.Empty

        End Sub

        Public Property UNQ_NO As String
        Public Property CRTDT As String
        Public Property CRTCD As String
        Public Property UPDDT As String
        Public Property UPDCD As String
        Public Property UserId As String

        Public Property DELFG As String
        'Public Property UNQ_NO As String
        Public Property UPDPG As String

        Public Property UPLOAD_USER As String
        Public Property UPLOAD_DATE As String
        Public Property SHIP_TO_BRANCH_CODE As String
        Public Property SHIP_TO_BRANCH As String
        'Public Property VALUE As String
        Public Property TARGET_MONTH As String
        Public Property TARGET_YEAR As String
        Public Property TARGET_MONTH_AMOUNT As String
        Public Property TARGET_DAY_AMOUNT As String
        Public Property FILE_NAME As String
        Public Property SRC_FILE_NAME As String
    End Class
End Namespace

