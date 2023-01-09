Public Class Form3
    Sub abc()

        DateTimePicker1.Format = DateTimePickerFormat.Custom
        DateTimePicker1.CustomFormat = "yyyy/MM/dd"
    End Sub
    Sub SIMPAN_DATA()
        Call KONEKSIKAN()
        DA = New Odbc.OdbcDataAdapter("SELECT * FROM TBARANG", KONEKSI)
        DS = New DataSet
        DA.Fill(DS)
        RECORD.DataSource = DS
        RECORD.DataMember = DS.Tables(0).ToString
    End Sub
    Sub ITEM_COMBO()
        Call KONEKSIKAN()
        DA = New Data.Odbc.OdbcDataAdapter("SELECT * FROM TBARANG", KONEKSI)
        DS = New DataSet
        DA.Fill(DS)
        RECORD.DataSource = DS
        RECORD.DataMember = DS.Tables(0).ToString
        Dim a As DataRow
        ComboBox1.Items.Clear()
        For Each a In DS.Tables(0).Rows
            ComboBox1.Items.Add(a.Item(0))
        Next a
    End Sub
    Sub TAMPILDIGRID()
        Call KONEKSIKAN()
        DA = New Odbc.OdbcDataAdapter("SELECT * FROM TBARANGMASUK", KONEKSI)
        DS = New DataSet
        DA.Fill(DS)
        DataGridView1.DataSource = DS.Tables(0)
        DataGridView1.ReadOnly = True
    End Sub
    Sub MATIKANFIELD()
        TextBox1.Enabled = False
        DateTimePicker1.Enabled = False
        ComboBox1.Enabled = False
        TextBox3.Enabled = False
        TextBox4.Enabled = False
        TextBox5.Enabled = False

    End Sub
    Sub KOSONGKANFIELD()
        TextBox1.Text = ""
        DateTimePicker1.Text = ""
        ComboBox1.Text = ""
        TextBox3.Text = ""
        TextBox4.Text = ""
        TextBox5.Text = ""
    End Sub
    Sub HIDUPKANFIELD()
        TextBox1.Enabled = True
        DateTimePicker1.Enabled = True
        ComboBox1.Enabled = True
        TextBox5.Enabled = True

    End Sub
    Sub REFRESHGRID()
        DA = New Odbc.OdbcDataAdapter("SELECT * FROM TBARANGMASUK", KONEKSI)
        DS = New DataSet
        DS.Clear()
        DA.Fill(DS, "TBARANGMASUK")
        DataGridView1.DataSource = (DS.Tables("TBARANGMASUK"))
    End Sub

    Private Sub Form3_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Call KONEKSIKAN()
        Call ITEM_COMBO()
        Call SIMPAN_DATA()
        Call TAMPILDIGRID()
        Call MATIKANFIELD()
        Call KOSONGKANFIELD()

    End Sub

    Private Sub DataGridView1_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick

    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox1.SelectedIndexChanged

        Call KONEKSIKAN()
        CMD = New Odbc.OdbcCommand("SELECT * FROM TBARANG WHERE KODEBARANG='" & ComboBox1.Text & "'", KONEKSI)
        DR = CMD.ExecuteReader
        DR.Read()
        TextBox3.Text = DR.Item(1)
        TextBox4.Text = DR.Item(2)
        TextBox5.Focus()
    End Sub

    Private Sub Button6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button6.Click
        Me.Close()

    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Call HIDUPKANFIELD()
        Call KOSONGKANFIELD()
        TextBox1.Focus()

    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Call abc()
        If TextBox1.Text = "" Or DateTimePicker1.Text = "" Or ComboBox1.Text = "" Or TextBox3.Text = "" Or TextBox4.Text = "" Or TextBox5.Text = "" Then
            MsgBox("Data Belum Lengkap")
            Exit Sub
        Else
            Call KONEKSIKAN()
            CMD = New Odbc.OdbcCommand("SELECT * FROM TBARANGMASUK WHERE KODEBM='" & TextBox1.Text & "'", KONEKSI)
            DR = CMD.ExecuteReader
            DR.Read()
            If Not DR.HasRows Then

                Call KONEKSIKAN()
                Dim SIMPAN As String
                SIMPAN = "INSERT INTO TBARANGMASUK VALUES('" & TextBox1.Text & "','" & DateTimePicker1.Text & "','" & ComboBox1.Text & "','" & TextBox5.Text & "')"
                CMD = New Odbc.OdbcCommand(SIMPAN, KONEKSI)
                CMD.ExecuteNonQuery()

                Call KONEKSIKAN()
                TextBox4.Text = Val(TextBox4.Text) + Val(TextBox5.Text)
                CMD = New Odbc.OdbcCommand("UPDATE TBARANG SET PERSEDIAAN ='" & TextBox4.Text & "'WHERE KODEBARANG='" & ComboBox1.Text & "'", KONEKSI)
                CMD.ExecuteNonQuery()
                MsgBox("Persediaan Telah Bertambah")
            Else
                MsgBox("Kode Transaksi Sudah Ada")

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

    Private Sub DataGridView1_CellMouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles DataGridView1.CellMouseClick
        On Error Resume Next
        TextBox1.Text = DataGridView1.Rows(e.RowIndex).Cells(0).Value
        DateTimePicker1.Text = DataGridView1.Rows(e.RowIndex).Cells(1).Value
        ComboBox1.Text = DataGridView1.Rows(e.RowIndex).Cells(2).Value
        TextBox5.Text = DataGridView1.Rows(e.RowIndex).Cells(3).Value
        TextBox7.Text = TextBox5.Text
        Call HIDUPKANFIELD()
        TextBox1.Enabled = False

    End Sub

    Private Sub TextBox6_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox6.TextChanged
        Call KONEKSIKAN()
        CMD = New Odbc.OdbcCommand("select * from tbarangmasuk where kodebm like '%" & TextBox6.Text & "%'", KONEKSI)
        DR = CMD.ExecuteReader
        DR.Read()
        If DR.HasRows Then
            DA = New Odbc.OdbcDataAdapter("select * from tbarangmasuk where kodebm like '%" & TextBox6.Text & "%'", KONEKSI)
            DS = New DataSet
            DA.Fill(DS)
            DataGridView1.DataSource = DS.Tables(0)
        Else
            MsgBox("Data Tidak Ditemukan")
        End If
    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        If TextBox1.Text = "" Then
            MsgBox("Data yang akan diubah belum ada")
            Exit Sub
        Else
            Call KONEKSIKAN()
            CMD = New Odbc.OdbcCommand("UPDATE TBARANGMASUK SET TANGGAL='" & DateTimePicker1.Text & "',KODEBARANG='" & ComboBox1.Text & "',JUMLAH='" & TextBox5.Text & "' WHERE KODEBM ='" & TextBox1.Text & "'", KONEKSI)
            CMD.ExecuteNonQuery()

            TextBox4.Text = Val(TextBox4.Text) - Val(TextBox7.Text) + Val(TextBox5.Text)
            CMD = New Odbc.OdbcCommand("UPDATE TBARANG SET PERSEDIAAN='" & TextBox4.Text & "' WHERE KODEBARANG='" & ComboBox1.Text & "'", KONEKSI)
            CMD.ExecuteNonQuery()
            MsgBox("Perubahan data sukses")
        End If
        Call MATIKANFIELD()
        Call KOSONGKANFIELD()
        Call TAMPILDIGRID()

    End Sub

    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
        If TextBox1.Text = "" Then
            MsgBox("Data yang akan dihapus belum ada")
            Exit Sub
        Else
            If MessageBox.Show("Yakin menghapus data ini?", "Konfirmasi", MessageBoxButtons.YesNo) = Windows.Forms.DialogResult.Yes Then
                TextBox4.Text = Val(TextBox4.Text) - Val(TextBox5.Text)
                CMD = New Odbc.OdbcCommand("UPDATE TBARANG SET PERSEDIAAN='" & TextBox4.Text & "' WHERE KODEBARANG='" & ComboBox1.Text & "'", KONEKSI)
                CMD.ExecuteNonQuery()
                Call KONEKSIKAN()
                CMD = New Odbc.OdbcCommand("DELETE FROM TBARANGMASUK WHERE KODEBM='" & TextBox1.Text & "'", KONEKSI)
                CMD.ExecuteNonQuery()
                MsgBox("Data Telah Berhasil Dihapus")
                Call MATIKANFIELD()
                Call KOSONGKANFIELD()
                Call TAMPILDIGRID()
            End If
        End If
    End Sub

    Private Sub TextBox1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox1.TextChanged

    End Sub

    Private Sub DateTimePicker1_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DateTimePicker1.ValueChanged

    End Sub

    Private Sub Button7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button7.Click
        Form6.Show()
    End Sub
End Class