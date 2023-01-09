Public Class Form4
    Sub abc()

        DateTimePicker1.Format = DateTimePickerFormat.Custom
        DateTimePicker1.CustomFormat = "yyyy/MM/dd"
    End Sub
    Sub TAMPILDIGRID()
        Call KONEKSIKAN()
        DA = New Odbc.OdbcDataAdapter("SELECT * FROM TBARANGKELUAR", KONEKSI)
        DS = New DataSet
        DA.Fill(DS)
        DataGridView1.DataSource = DS.Tables(0)
        DataGridView1.ReadOnly = True

    End Sub

    Sub REFRESHGRID()
        DA = New Odbc.OdbcDataAdapter("SELECT * FROM TBARANGKELUAR", KONEKSI)
        DS = New DataSet
        DS.Clear()
        DA.Fill(DS, "TBARANGKELUAR")
        DataGridView1.DataSource = (DS.Tables("TBARANGKELUAR"))
    End Sub
    Sub KOSONGKAN_FIELD()
        TextBox1.Text = ""
        DateTimePicker1.Text = ""
        ComboBox1.Text = ""
        TextBox3.Text = ""
        TextBox4.Text = ""
        TextBox5.Text = ""
    End Sub
    Sub MATIKAN_FIELD()
        TextBox1.Enabled = False
        DateTimePicker1.Enabled = False
        ComboBox1.Enabled = False
        TextBox3.Enabled = False
        TextBox4.Enabled = False
        TextBox5.Enabled = False
    End Sub
    Sub HIDUPKAN_FIELD()
        TextBox1.Enabled = True
        DateTimePicker1.Enabled = True
        ComboBox1.Enabled = True
        TextBox5.Enabled = True
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

    Private Sub Form4_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Call TAMPILDIGRID()
        Call KOSONGKAN_FIELD()
        Call MATIKAN_FIELD()
        Call ITEM_COMBO()
        Call SIMPAN_DATA()

    End Sub

    Private Sub Button6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button6.Click
        Me.Close()

    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Call HIDUPKAN_FIELD()
        Call KOSONGKAN_FIELD()
        TextBox1.Focus()
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

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Call abc()
        If TextBox1.Text = "" Or DateTimePicker1.Text = "" Or ComboBox1.Text = "" Or TextBox3.Text = "" Or TextBox4.Text = "" Or TextBox5.Text = "" Then
            MsgBox("Data Belum Lengkap")
            Exit Sub
        Else
            Call KONEKSIKAN()
            CMD = New Odbc.OdbcCommand("SELECT * FROM TBARANGKELUAR WHERE KODEBK='" & TextBox1.Text & "'", KONEKSI)
            DR = CMD.ExecuteReader
            DR.Read()
            If Not DR.HasRows Then
                Call KONEKSIKAN()
                Dim SIMPAN As String
                SIMPAN = "INSERT INTO TBARANGKELUAR VALUES('" & TextBox1.Text & "','" & DateTimePicker1.Text & "','" & ComboBox1.Text & "','" & TextBox5.Text & "')"
                CMD = New Odbc.OdbcCommand(SIMPAN, KONEKSI)
                CMD.ExecuteNonQuery()

                Call KONEKSIKAN()
                TextBox4.Text = Val(TextBox4.Text) - Val(TextBox5.Text)
                CMD = New Odbc.OdbcCommand("UPDATE TBARANG SET PERSEDIAAN ='" & TextBox4.Text & "'WHERE KODEBARANG='" & ComboBox1.Text & "'", KONEKSI)
                CMD.ExecuteNonQuery()
                MsgBox("Persediaan Telah Berkurang")
            Else
                MsgBox("Kode Transaksi Sudah Ada")

            End If
            Call REFRESHGRID()
            Call TAMPILDIGRID()
            Call KOSONGKAN_FIELD()
            Call MATIKAN_FIELD()
        End If
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        Call KOSONGKAN_FIELD()
        Call MATIKAN_FIELD()

    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        If TextBox1.Text = "" Then
            MsgBox("Data yang akan diubah belum ada")
            Exit Sub
        Else
            Call KONEKSIKAN()
            CMD = New Odbc.OdbcCommand("UPDATE TBARANGKELUAR SET TANGGAL='" & DateTimePicker1.Text & "',KODEBARANG='" & ComboBox1.Text & "',JUMLAH='" & TextBox5.Text & "' WHERE KODEBK ='" & TextBox1.Text & "'", KONEKSI)
            CMD.ExecuteNonQuery()

            TextBox4.Text = Val(TextBox4.Text) + Val(TextBox7.Text) - Val(TextBox5.Text)
            CMD = New Odbc.OdbcCommand("UPDATE TBARANG SET PERSEDIAAN='" & TextBox4.Text & "' WHERE KODEBARANG='" & ComboBox1.Text & "'", KONEKSI)
            CMD.ExecuteNonQuery()
            MsgBox("Perubahan data sukses")
        End If
        Call MATIKAN_FIELD()
        Call KOSONGKAN_FIELD()
        Call TAMPILDIGRID()
    End Sub

    Private Sub DataGridView1_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick

    End Sub

    Private Sub DataGridView1_CellMouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles DataGridView1.CellMouseClick
        On Error Resume Next
        TextBox1.Text = DataGridView1.Rows(e.RowIndex).Cells(0).Value
        DateTimePicker1.Text = DataGridView1.Rows(e.RowIndex).Cells(1).Value
        ComboBox1.Text = DataGridView1.Rows(e.RowIndex).Cells(2).Value
        TextBox5.Text = DataGridView1.Rows(e.RowIndex).Cells(3).Value
        TextBox7.Text = TextBox5.Text
        Call HIDUPKAN_FIELD()
        TextBox1.Enabled = False
    End Sub

    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
        If TextBox1.Text = "" Then
            MsgBox("Data yang akan dihapus belum ada")
            Exit Sub
        Else
            If MessageBox.Show("Yakin menghapus data ini?", "Konfirmasi", MessageBoxButtons.YesNo) = Windows.Forms.DialogResult.Yes Then
                TextBox4.Text = Val(TextBox4.Text) + Val(TextBox5.Text)
                CMD = New Odbc.OdbcCommand("UPDATE TBARANG SET PERSEDIAAN='" & TextBox4.Text & "' WHERE KODEBARANG='" & ComboBox1.Text & "'", KONEKSI)
                CMD.ExecuteNonQuery()
                Call KONEKSIKAN()
                CMD = New Odbc.OdbcCommand("DELETE FROM TBARANGKELUAR WHERE KODEBK='" & TextBox1.Text & "'", KONEKSI)
                CMD.ExecuteNonQuery()
                MsgBox("Data Telah Berhasil Dihapus")
                Call MATIKAN_FIELD()
                Call KOSONGKAN_FIELD()
                Call TAMPILDIGRID()
            End If
        End If
    End Sub

    Private Sub TextBox6_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox6.TextChanged
        Call KONEKSIKAN()
        CMD = New Odbc.OdbcCommand("select * from TBARANGKELUAR where kodebK like '%" & TextBox6.Text & "%'", KONEKSI)
        DR = CMD.ExecuteReader
        DR.Read()
        If DR.HasRows Then
            DA = New Odbc.OdbcDataAdapter("select * from TBARANGKELUAR where kodebK like '%" & TextBox6.Text & "%'", KONEKSI)
            DS = New DataSet
            DA.Fill(DS)
            DataGridView1.DataSource = DS.Tables(0)
        Else
            MsgBox("Data Tidak Ditemukan")
        End If
    End Sub

    Private Sub TextBox1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox1.TextChanged

    End Sub

    Private Sub Button7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button7.Click
        Form7.Show()
    End Sub
End Class