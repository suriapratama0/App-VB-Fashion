Public Class Form2
    Sub TAMPILDIGRID()
        Call KONEKSIKAN()
        DA = New Odbc.OdbcDataAdapter("SELECT * FROM TBARANG", KONEKSI)
        DS = New DataSet
        DA.Fill(DS)
        DataGridView1.DataSource = DS.Tables(0)
        DataGridView1.ReadOnly = True
    End Sub
    Sub KOSONGKANFIELD()
        TextBox2.Text = ""
        TextBox2.Text = ""
        TextBox3.Text = ""
    End Sub
    Sub MATIKANFIELD()
        TextBox2.Enabled = False
        TextBox2.Enabled = False
        TextBox3.Enabled = False
    End Sub
    Sub HIDUPKANFIELD()
        TextBox2.Enabled = True
        TextBox2.Enabled = True
        TextBox3.Enabled = True
    End Sub
    Sub REFRESHGRID()
        DA = New Odbc.OdbcDataAdapter("SELECT * FROM TBARANG", KONEKSI)
        DS = New DataSet
        DS.Clear()
        DA.Fill(DS, "TBARANG")
        DataGridView1.DataSource = (DS.Tables("TBARANG"))
    End Sub

    Private Sub Form2_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Call TAMPILDIGRID()
        Call KOSONGKANFIELD()
        Call MATIKANFIELD()
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Call HIDUPKANFIELD()
        Call KOSONGKANFIELD()
        TextBox2.Focus()
        TextBox3.Text = "0"
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        If TextBox2.Text = "" Or TextBox3.Text = "" Then
            MsgBox("Data belum lengkap")
            Exit Sub
        Else
            Call KONEKSIKAN()
            CMD = New Odbc.OdbcCommand("select * from tbarang where kodebarang='" & TextBox2.Text & "'", KONEKSI)
            DR = CMD.ExecuteReader
            DR.Read()
            If Not DR.HasRows Then
                Call KONEKSIKAN()
                Dim simpan As String
                simpan = "insert into tbarang values ('" & TextBox1.Text & "','" & TextBox2.Text & "','" & TextBox3.Text & "')"
                CMD = New Odbc.OdbcCommand(simpan, KONEKSI)
                CMD.ExecuteNonQuery()
            Else
                MsgBox("Kode barang sudah ada")
            End If
            Call REFRESHGRID()
            Call TAMPILDIGRID()
            Call KOSONGKANFIELD()
            Call MATIKANFIELD()
        End If
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        Call KOSONGKANFIELD()
        Call MATIKANFIELD()
    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        If TextBox1.Text = "" Or TextBox2.Text = "" Or TextBox3.Text = "" Then
            MsgBox("Data belum lengkap")
            Exit Sub
        Else
            Call KONEKSIKAN()
            CMD = New Odbc.OdbcCommand("UPDATE TBARANG SET NAMABARANG='" & TextBox2.Text & "',PERSEDIAAN='" & TextBox3.Text & "'WHERE KODEBARANG='" & TextBox1.Text & "'", KONEKSI)
            CMD.ExecuteNonQuery()
            MsgBox("Perubahan data sukses")
        End If
        Call MATIKANFIELD()
        Call KOSONGKANFIELD()
        Call TAMPILDIGRID()
    End Sub

    Private Sub DataGridView1_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick

    End Sub

    Private Sub DataGridView1_CellMouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles DataGridView1.CellMouseClick
        On Error Resume Next
        TextBox1.Text = DataGridView1.Rows(e.RowIndex).Cells(0).Value
        TextBox2.Text = DataGridView1.Rows(e.RowIndex).Cells(1).Value
        TextBox3.Text = DataGridView1.Rows(e.RowIndex).Cells(2).Value
        Call HIDUPKANFIELD()
        TextBox2.Enabled = False
    End Sub

    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
        If TextBox1.Text = "" Then
            MsgBox("Data yang akan dihapus tidak bisa")
            Exit Sub
        Else
            If MessageBox.Show("Yakin menghapus data ini?", "Konfirmasi", MessageBoxButtons.YesNo) = Windows.Forms.DialogResult.Yes Then
                Call KONEKSIKAN()
                CMD = New Odbc.OdbcCommand("delete from tbarang where kodebarang='" & TextBox1.Text & "'", KONEKSI)
                CMD.ExecuteNonQuery()
                MsgBox("penghapusan data sukses")
                Call MATIKANFIELD()
                Call TAMPILDIGRID()
            Else
                Call MATIKANFIELD()
                Call KOSONGKANFIELD()
            End If
        End If
    End Sub

    Private Sub Button6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button6.Click
        Me.Close()
    End Sub

    Private Sub TextBox1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub Label3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label3.Click

    End Sub

    Private Sub Button7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button7.Click
        Form5.Show()
    End Sub
End Class