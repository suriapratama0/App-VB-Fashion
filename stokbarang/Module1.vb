Imports System.Data.Odbc
Module Module1
    Public KONEKSI As OdbcConnection
    Public DA As OdbcDataAdapter
    Public DS As DataSet
    Public CMD As OdbcCommand
    Public DR As OdbcDataReader
    Public RECORD As New BindingSource

    Sub KONEKSIKAN()
        KONEKSI = New OdbcConnection("Driver={Mysql ODBC 5.1 Driver};Database=database1;server=localhost;uid=root")
        KONEKSI.Open()
    End Sub
End Module
