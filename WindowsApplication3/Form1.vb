Imports Microsoft.VisualBasic.FileIO
Imports System.Collections.ObjectModel

Public Class Form1
    Enum Txt
        _END
        _BPM
        _BPM_INFO
        _BEAT_INFO
        _END_POSISION
        _TILT_MODE_INFO
        _TRACK1_START
        _TRACK2_START
        _TRACK3_START
        _TRACK4_START
        _TRACK5_START
        _TRACK6_START
        _TRACK7_START
        _TRACK8_START
        _FORMAT_VERSION
    End Enum
    Dim test_value As Integer = 6
    Dim mode As UInteger = 0
    Public MAX_MEASURE = 300
    Dim format_version As UInteger
    'Data Here
    Dim END_POSISION As String
    Dim BPM_INFO As New ArrayList
    Dim BEAT_INFO As New ArrayList
    Dim TILT_MODE_INFO As New ArrayList
    Dim TRACK1_START As New ArrayList 'L-Laser
    Dim TRACK2_START As New ArrayList 'FX-L
    Dim TRACK3_START As New ArrayList 'BT-A
    Dim TRACK4_START As New ArrayList 'BT-B
    Dim TRACK5_START As New ArrayList 'BT-C
    Dim TRACK6_START As New ArrayList 'BT-D
    Dim TRACK7_START As New ArrayList 'FX-R
    Dim TRACK8_START As New ArrayList 'R-Laser
    'Data End Here

    Private Sub OpenFileDialog1_FileOk(sender As System.Object, e As System.ComponentModel.CancelEventArgs) Handles OpenFileDialog1.FileOk
        TextBox1.Text = OpenFileDialog1.FileName
        END_POSISION = ""
        BPM_INFO.Clear()
        BEAT_INFO.Clear()
        TILT_MODE_INFO.Clear()
        TRACK1_START.Clear()
        TRACK2_START.Clear()
        TRACK3_START.Clear()
        TRACK4_START.Clear()
        TRACK5_START.Clear()
        TRACK6_START.Clear()
        TRACK7_START.Clear()
        TRACK8_START.Clear()
        Dim value As String = ""
        Dim MySF As IO.StreamReader = New IO.StreamReader(TextBox1.Text, System.Text.Encoding.Default)
        Do While Not MySF.EndOfStream
            value = MySF.ReadLine '讀取單行 
            If (value.Length > 0) And Not (value.StartsWith("//")) Then
                If value.StartsWith("#") Then
                    Mode_Changer(value)
                ElseIf mode > 0 Then 'NOT END
                    Select Case mode
                        Case Txt._FORMAT_VERSION
                            format_version = value
                        Case Txt._BPM
                            BPM_INFO.Add("001,01,00" & Chr(9) & value & Chr(9) & "4")
                        Case Txt._BPM_INFO
                            BPM_INFO.Add(value)
                        Case Txt._BEAT_INFO
                            BEAT_INFO.Add(value)
                        Case Txt._END_POSISION
                            END_POSISION = value
                        Case Txt._TILT_MODE_INFO
                            TILT_MODE_INFO.Add(value)
                        Case Txt._TRACK1_START
                            TRACK1_START.Add(value)
                        Case Txt._TRACK2_START
                            TRACK2_START.Add(value)
                        Case Txt._TRACK3_START
                            TRACK3_START.Add(value)
                        Case Txt._TRACK4_START
                            TRACK4_START.Add(value)
                        Case Txt._TRACK5_START
                            TRACK5_START.Add(value)
                        Case Txt._TRACK6_START
                            TRACK6_START.Add(value)
                        Case Txt._TRACK7_START
                            TRACK7_START.Add(value)
                        Case Txt._TRACK8_START
                            TRACK8_START.Add(value)
                    End Select

                End If
            End If
        Loop
        MySF.Dispose()
        ListBox1.Visible = True
    End Sub

    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs) Handles Button1.Click
        OpenFileDialog1.ShowDialog()
    End Sub


    Public Sub Mode_Changer(str As String)
        If str.StartsWith("#FORMAT VERSION") Then
            mode = Txt._FORMAT_VERSION
        ElseIf str.StartsWith("#END POSISION") Or str.StartsWith("#END POSITION") Then
            mode = Txt._END_POSISION
        ElseIf str.StartsWith("#BPM INFO") Then
            mode = Txt._BPM_INFO
        ElseIf str.StartsWith("#BPM") Then
            mode = Txt._BPM
        ElseIf str.StartsWith("#BEAT") Then
            mode = Txt._BEAT_INFO
        ElseIf str.StartsWith("#END") Then
            mode = Txt._END
        ElseIf str.StartsWith("#TILT MODE INFO") Then
            mode = Txt._TILT_MODE_INFO
        ElseIf str.StartsWith("#TRACK1") Then
            mode = Txt._TRACK1_START
        ElseIf str.StartsWith("#TRACK2") Then
            mode = Txt._TRACK2_START
        ElseIf str.StartsWith("#TRACK3") Then
            mode = Txt._TRACK3_START
        ElseIf str.StartsWith("#TRACK4") Then
            mode = Txt._TRACK4_START
        ElseIf str.StartsWith("#TRACK5") Then
            mode = Txt._TRACK5_START
        ElseIf str.StartsWith("#TRACK6") Then
            mode = Txt._TRACK6_START
        ElseIf str.StartsWith("#TRACK7") Then
            mode = Txt._TRACK7_START
        ElseIf str.StartsWith("#TRACK8") Then
            mode = Txt._TRACK8_START
        End If
    End Sub

    Private Sub ListBox1_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles ListBox1.SelectedIndexChanged
        If (OpenFileDialog1.FileName.Length = 0) Then
            Return
        End If
        TextBox2.Visible = True
        Button2.Visible = False
        Select Case (ListBox1.SelectedIndex + 2)
            Case Txt._END_POSISION
                TextBox2.Text = END_POSISION
            Case Txt._BPM_INFO
                If (BPM_INFO.Count = 0) Then
                    TextBox2.Text = ""
                End If
                For j = 0 To BPM_INFO.Count - 1
                    If (j = 0) Then
                        TextBox2.Text = BPM_INFO.Item(j)
                    Else
                        TextBox2.Text = TextBox2.Text & vbCrLf & BPM_INFO.Item(j)
                    End If
                Next
            Case Txt._BEAT_INFO
                If (BEAT_INFO.Count = 0) Then
                    TextBox2.Text = ""
                End If
                For j = 0 To BEAT_INFO.Count - 1
                    If (j = 0) Then
                        TextBox2.Text = BEAT_INFO.Item(j)
                    Else
                        TextBox2.Text = TextBox2.Text & vbCrLf & BEAT_INFO.Item(j)
                    End If
                Next
            Case Txt._TILT_MODE_INFO
                If (TILT_MODE_INFO.Count = 0) Then
                    TextBox2.Text = ""
                End If
                For j = 0 To TILT_MODE_INFO.Count - 1
                    If (j = 0) Then
                        TextBox2.Text = TILT_MODE_INFO.Item(j)
                    Else
                        TextBox2.Text = TextBox2.Text & vbCrLf & TILT_MODE_INFO.Item(j)
                    End If
                Next
            Case Txt._TRACK1_START
                If (TRACK1_START.Count = 0) Then
                    TextBox2.Text = ""
                End If
                For j = 0 To TRACK1_START.Count - 1
                    If (j = 0) Then
                        TextBox2.Text = TRACK1_START.Item(j)
                    Else
                        TextBox2.Text = TextBox2.Text & vbCrLf & TRACK1_START.Item(j)
                    End If
                Next
            Case Txt._TRACK2_START
                If (TRACK2_START.Count = 0) Then
                    TextBox2.Text = ""
                End If
                For j = 0 To TRACK2_START.Count - 1
                    If (j = 0) Then
                        TextBox2.Text = TRACK2_START.Item(j)
                    Else
                        TextBox2.Text = TextBox2.Text & vbCrLf & TRACK2_START.Item(j)
                    End If
                Next
            Case Txt._TRACK3_START
                If (TRACK3_START.Count = 0) Then
                    TextBox2.Text = ""
                End If
                For j = 0 To TRACK3_START.Count - 1
                    If (j = 0) Then
                        TextBox2.Text = TRACK3_START.Item(j)
                    Else
                        TextBox2.Text = TextBox2.Text & vbCrLf & TRACK3_START.Item(j)
                    End If
                Next
            Case Txt._TRACK4_START
                If (TRACK4_START.Count = 0) Then
                    TextBox2.Text = ""
                End If
                For j = 0 To TRACK4_START.Count - 1
                    If (j = 0) Then
                        TextBox2.Text = TRACK4_START.Item(j)
                    Else
                        TextBox2.Text = TextBox2.Text & vbCrLf & TRACK4_START.Item(j)
                    End If
                Next
            Case Txt._TRACK5_START
                If (TRACK5_START.Count = 0) Then
                    TextBox2.Text = ""
                End If
                For j = 0 To TRACK5_START.Count - 1
                    If (j = 0) Then
                        TextBox2.Text = TRACK5_START.Item(j)
                    Else
                        TextBox2.Text = TextBox2.Text & vbCrLf & TRACK5_START.Item(j)
                    End If
                Next
            Case Txt._TRACK6_START
                If (TRACK6_START.Count = 0) Then
                    TextBox2.Text = ""
                End If
                For j = 0 To TRACK6_START.Count - 1
                    If (j = 0) Then
                        TextBox2.Text = TRACK6_START.Item(j)
                    Else
                        TextBox2.Text = TextBox2.Text & vbCrLf & TRACK6_START.Item(j)
                    End If
                Next
            Case Txt._TRACK7_START
                If (TRACK7_START.Count = 0) Then
                    TextBox2.Text = ""
                End If
                For j = 0 To TRACK7_START.Count - 1
                    If (j = 0) Then
                        TextBox2.Text = TRACK7_START.Item(j)
                    Else
                        TextBox2.Text = TextBox2.Text & vbCrLf & TRACK7_START.Item(j)
                    End If
                Next
            Case Txt._TRACK8_START
                If (TRACK8_START.Count = 0) Then
                    TextBox2.Text = ""
                End If
                For j = 0 To TRACK8_START.Count - 1
                    If (j = 0) Then
                        TextBox2.Text = TRACK8_START.Item(j)
                    Else
                        TextBox2.Text = TextBox2.Text & vbCrLf & TRACK8_START.Item(j)
                    End If
                Next

            Case Txt._TRACK8_START + 1
                TextBox2.Visible = False
                Button2.Visible = True
        End Select

    End Sub

    Public Sub create_chart()
        Dim fileExists As Boolean
        fileExists = My.Computer.FileSystem.FileExists(SaveFileDialog1.FileName)
        If (fileExists = True) Then
            My.Computer.FileSystem.DeleteFile(SaveFileDialog1.FileName, FileIO.UIOption.OnlyErrorDialogs, FileIO.RecycleOption.DeletePermanently)
        End If
        ListBox1.Enabled = False
        TextBox2.Text = ""
        TextBox2.Visible = False
        ProgressBar1.Visible = True
        ProgressBar1.Value = 0
        'Data Store
        Dim end_measure(3) As UInt16
        Dim Laser_convert As String = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnoo"
        Dim FX_convert As String = "11STIFUH1ADXVB11111"
        Dim Laser_effect_convect() As String = {"filtertype=peak", "filtertype=peak", "filtertype=lpf1", "filtertype=peak", "filtertype=hpf1", "filtertype=fx;bitc", "filtertype=peak", "filtertype=peak"}
        Dim Rotate_convert() As String = {"", "@)192", "@(192", "@)48", "@(48", "@)96", "@(96", "", "", "@>96", "@<96"}
        Dim current_laser() As Integer = {-1, -1} 'L / R
        Dim current_laser_effect As UInteger
        Dim current_FX_Effect() As UInteger = {0, 0}
        Dim Track_Data(8, MAX_MEASURE, 32, 192, 6) As Integer
        Dim Data_Beat(MAX_MEASURE, 2) As UInt16
        Dim Data_BPM(MAX_MEASURE, 36, 192) As Double
        Dim Data_Tilt(MAX_MEASURE, 36, 192) As UInt16
        Dim current_tilt As UShort
        Dim LongBTFX(6) As UInt16
        Dim current_beat(2) As Integer
        Dim output_data As String = ""
        'Data Convert
        Dim tmp_strarray As String()
        tmp_strarray = END_POSISION.Split(",")

        For j = 0 To tmp_strarray.Length - 1
            end_measure(j) = tmp_strarray(j)
        Next
        If (BEAT_INFO.Count > 0) Then
            For j = 0 To BEAT_INFO.Count - 1
                'tmp_strarray(0) ~ (2) = measure /else = data
                Dim tmp_strarray2 As String()
                tmp_strarray = BEAT_INFO.Item(j).Split(",")
                tmp_strarray2 = tmp_strarray(2).Split("	")
                Data_Beat(tmp_strarray(0) - 1, 0) = tmp_strarray2(1)
                Data_Beat(tmp_strarray(0) - 1, 1) = tmp_strarray2(2)
            Next
        End If

        If (TILT_MODE_INFO.Count > 1) Then
            For j = 1 To TILT_MODE_INFO.Count - 1
                'tmp_strarray(0) ~ (2) = measure /else = data
                Dim tmp_strarray2 As String()
                tmp_strarray = TILT_MODE_INFO.Item(j).Split(",")
                tmp_strarray2 = tmp_strarray(2).Split("	")
                Data_Tilt(tmp_strarray(0) - 1, tmp_strarray(1) - 1, tmp_strarray2(0)) = tmp_strarray2(1) + 1
            Next
        End If

        If (BPM_INFO.Count > 0) Then
            For j = 0 To BPM_INFO.Count - 1
                'tmp_strarray(0) ~ (2) = measure /else = data
                Dim tmp_strarray2 As String()
                tmp_strarray = BPM_INFO.Item(j).Split(",")
                tmp_strarray2 = tmp_strarray(2).Split("	")
                Data_BPM(tmp_strarray(0) - 1, tmp_strarray(1) - 1, tmp_strarray2(0)) = tmp_strarray2(1)
            Next
        End If
        If (TRACK1_START.Count > 0) Then
            For j = 0 To TRACK1_START.Count - 1
                Dim tmp_strarray2 As String()
                'tmp_strarray(0) ~ (2) = measure else = data
                tmp_strarray = TRACK1_START.Item(j).Split(",")
                tmp_strarray2 = tmp_strarray(2).Split("	")
                If (tmp_strarray2(1) = 64) Then
                    tmp_strarray2(1) = 63
                End If
                'slam
                If (Track_Data(0, tmp_strarray(0) - 1, tmp_strarray(1) - 1, tmp_strarray2(0), 0) > 0 Or Track_Data(0, tmp_strarray(0) - 1, tmp_strarray(1) - 1, tmp_strarray2(0), 1) > 0) Then
                    Track_Data(0, tmp_strarray(0) - 1, tmp_strarray(1) - 1, tmp_strarray2(0) + test_value, 0) = tmp_strarray2(1) + 1
                    Track_Data(0, tmp_strarray(0) - 1, tmp_strarray(1) - 1, tmp_strarray2(0) + test_value, 1) = tmp_strarray2(2)
                    If (Track_Data(0, tmp_strarray(0) - 1, tmp_strarray(1) - 1, tmp_strarray2(0), 2) > 0) Then
                        Dim t As Integer = Track_Data(0, tmp_strarray(0) - 1, tmp_strarray(1) - 1, tmp_strarray2(0), 0)
                        t -= 1
                        If (tmp_strarray2(1) < t) Then
                            Track_Data(0, tmp_strarray(0) - 1, tmp_strarray(1) - 1, tmp_strarray2(0), 2) = tmp_strarray2(3) * 2 - 1
                        Else
                            Track_Data(0, tmp_strarray(0) - 1, tmp_strarray(1) - 1, tmp_strarray2(0), 2) = tmp_strarray2(3) * 2
                        End If
                    End If
                    If (format_version >= 4) Then ' LASER EFFECT IN SDVX II
                        Track_Data(0, tmp_strarray(0) - 1, tmp_strarray(1) - 1, tmp_strarray2(0) + test_value, 3) = tmp_strarray2(4)
                    End If
                Else
                    If (tmp_strarray2(2) = 1) Then
                        Track_Data(0, tmp_strarray(0) - 1, tmp_strarray(1) - 1, tmp_strarray2(0), 0) = 1
                    Else
                        Track_Data(0, tmp_strarray(0) - 1, tmp_strarray(1) - 1, tmp_strarray2(0), 0) = tmp_strarray2(1) + 1
                    End If
                    Track_Data(0, tmp_strarray(0) - 1, tmp_strarray(1) - 1, tmp_strarray2(0), 1) = tmp_strarray2(2)
                    Track_Data(0, tmp_strarray(0) - 1, tmp_strarray(1) - 1, tmp_strarray2(0), 2) = tmp_strarray2(3)
                    If (format_version >= 4) Then ' LASER EFFECT IN SDVX II
                        Track_Data(0, tmp_strarray(0) - 1, tmp_strarray(1) - 1, tmp_strarray2(0), 3) = tmp_strarray2(4)
                    End If
                End If
            Next
        End If

        If (TRACK2_START.Count > 0) Then
            For j = 0 To TRACK2_START.Count - 1
                Dim tmp_strarray2 As String()
                'tmp_strarray(0) ~ (2) = measure else = data
                tmp_strarray = TRACK2_START.Item(j).Split(",")
                tmp_strarray2 = tmp_strarray(2).Split("	")
                If (tmp_strarray2(1) = 0) Then 'SDVX II Fix
                    tmp_strarray2(2) = 255
                End If
                Track_Data(1, tmp_strarray(0) - 1, tmp_strarray(1) - 1, tmp_strarray2(0), 0) = tmp_strarray2(1)
                If (format_version > 3) Then
                    Track_Data(1, tmp_strarray(0) - 1, tmp_strarray(1) - 1, tmp_strarray2(0), 1) = tmp_strarray2(2)
                Else
                    Track_Data(1, tmp_strarray(0) - 1, tmp_strarray(1) - 1, tmp_strarray2(0), 1) = tmp_strarray2(3).Replace("00_fx_pattern_0", "") + 1
                End If
            Next
        End If

        If (TRACK3_START.Count > 0) Then
            For j = 0 To TRACK3_START.Count - 1
                Dim tmp_strarray2 As String()
                'tmp_strarray(0) ~ (2) = measure else = data
                tmp_strarray = TRACK3_START.Item(j).Split(",")
                tmp_strarray2 = tmp_strarray(2).Split("	")
                If (tmp_strarray2(1) = 0) Then 'SDVX II Fix
                    tmp_strarray2(2) = 255
                End If
                Track_Data(2, tmp_strarray(0) - 1, tmp_strarray(1) - 1, tmp_strarray2(0), 0) = tmp_strarray2(1)
                Track_Data(2, tmp_strarray(0) - 1, tmp_strarray(1) - 1, tmp_strarray2(0), 1) = tmp_strarray2(2)

                If (tmp_strarray2.Contains("sd_off")) Then
                    Track_Data(2, tmp_strarray(0) - 1, tmp_strarray(1) - 1, tmp_strarray2(0), 0) = 0
                    Track_Data(2, tmp_strarray(0) - 1, tmp_strarray(1) - 1, tmp_strarray2(0), 1) = 255
                End If
            Next
        End If

        If (TRACK4_START.Count > 0) Then
            For j = 0 To TRACK4_START.Count - 1
                Dim tmp_strarray2 As String()
                'tmp_strarray(0) ~ (2) = measure else = data
                tmp_strarray = TRACK4_START.Item(j).Split(",")
                tmp_strarray2 = tmp_strarray(2).Split("	")
                If (tmp_strarray2(1) = 0) Then 'SDVX II Fix
                    tmp_strarray2(2) = 255
                End If
                Track_Data(3, tmp_strarray(0) - 1, tmp_strarray(1) - 1, tmp_strarray2(0), 0) = tmp_strarray2(1)
                Track_Data(3, tmp_strarray(0) - 1, tmp_strarray(1) - 1, tmp_strarray2(0), 1) = tmp_strarray2(2)

                If (tmp_strarray2.Contains("sd_off")) Then
                    Track_Data(3, tmp_strarray(0) - 1, tmp_strarray(1) - 1, tmp_strarray2(0), 0) = 0
                    Track_Data(3, tmp_strarray(0) - 1, tmp_strarray(1) - 1, tmp_strarray2(0), 1) = 255
                End If
            Next
        End If
        If (TRACK5_START.Count > 0) Then
            For j = 0 To TRACK5_START.Count - 1
                Dim tmp_strarray2 As String()
                'tmp_strarray(0) ~ (2) = measure else = data
                tmp_strarray = TRACK5_START.Item(j).Split(",")
                tmp_strarray2 = tmp_strarray(2).Split("	")
                If (tmp_strarray2(1) = 0) Then 'SDVX II Fix
                    tmp_strarray2(2) = 255
                End If
                Track_Data(4, tmp_strarray(0) - 1, tmp_strarray(1) - 1, tmp_strarray2(0), 0) = tmp_strarray2(1)
                Track_Data(4, tmp_strarray(0) - 1, tmp_strarray(1) - 1, tmp_strarray2(0), 1) = tmp_strarray2(2)
                If (tmp_strarray2.Contains("sd_off")) Then
                    Track_Data(4, tmp_strarray(0) - 1, tmp_strarray(1) - 1, tmp_strarray2(0), 0) = 0
                    Track_Data(4, tmp_strarray(0) - 1, tmp_strarray(1) - 1, tmp_strarray2(0), 1) = 255
                End If
            Next
        End If
        If (TRACK6_START.Count > 0) Then
            For j = 0 To TRACK6_START.Count - 1
                Dim tmp_strarray2 As String()
                'tmp_strarray(0) ~ (2) = measure else = data
                tmp_strarray = TRACK6_START.Item(j).Split(",")
                tmp_strarray2 = tmp_strarray(2).Split("	")
                If (tmp_strarray2(1) = 0) Then 'SDVX II Fix
                    tmp_strarray2(2) = 255
                End If
                Track_Data(5, tmp_strarray(0) - 1, tmp_strarray(1) - 1, tmp_strarray2(0), 0) = tmp_strarray2(1)
                Track_Data(5, tmp_strarray(0) - 1, tmp_strarray(1) - 1, tmp_strarray2(0), 1) = tmp_strarray2(2)
                If (tmp_strarray2.Contains("sd_off")) Then
                    Track_Data(5, tmp_strarray(0) - 1, tmp_strarray(1) - 1, tmp_strarray2(0), 0) = 0
                    Track_Data(5, tmp_strarray(0) - 1, tmp_strarray(1) - 1, tmp_strarray2(0), 1) = 255
                End If
            Next
        End If
        If (TRACK7_START.Count > 0) Then
            For j = 0 To TRACK7_START.Count - 1
                Dim tmp_strarray2 As String()
                'tmp_strarray(0) ~ (2) = measure else = data
                tmp_strarray = TRACK7_START.Item(j).Split(",")
                tmp_strarray2 = tmp_strarray(2).Split("	")
                If (tmp_strarray2(1) = 0) Then 'SDVX II Fix
                    tmp_strarray2(2) = 255
                End If
                Track_Data(6, tmp_strarray(0) - 1, tmp_strarray(1) - 1, tmp_strarray2(0), 0) = tmp_strarray2(1)
                If (format_version > 3) Then
                    Track_Data(6, tmp_strarray(0) - 1, tmp_strarray(1) - 1, tmp_strarray2(0), 1) = tmp_strarray2(2)
                Else
                    Track_Data(6, tmp_strarray(0) - 1, tmp_strarray(1) - 1, tmp_strarray2(0), 1) = tmp_strarray2(3).Replace("00_fx_pattern_0", "") + 1
                End If
            Next
        End If
        If (TRACK8_START.Count > 0) Then
            For j = 0 To TRACK8_START.Count - 1
                Dim tmp_strarray2 As String()
                'tmp_strarray(0) ~ (2) = measure else = data
                tmp_strarray = TRACK8_START.Item(j).Split(",")
                tmp_strarray2 = tmp_strarray(2).Split("	")
                If (tmp_strarray2(1) = 64) Then
                    tmp_strarray2(1) = 63
                End If
                'slam
                If (Track_Data(7, tmp_strarray(0) - 1, tmp_strarray(1) - 1, tmp_strarray2(0), 0) > 0 Or Track_Data(7, tmp_strarray(0) - 1, tmp_strarray(1) - 1, tmp_strarray2(0), 1) > 0) Then
                    Track_Data(7, tmp_strarray(0) - 1, tmp_strarray(1) - 1, tmp_strarray2(0) + test_value, 0) = tmp_strarray2(1) + 1
                    Track_Data(7, tmp_strarray(0) - 1, tmp_strarray(1) - 1, tmp_strarray2(0) + test_value, 1) = tmp_strarray2(2)
                    If (Track_Data(7, tmp_strarray(0) - 1, tmp_strarray(1) - 1, tmp_strarray2(0), 2) > 0) Then
                        Dim t As Integer = Track_Data(7, tmp_strarray(0) - 1, tmp_strarray(1) - 1, tmp_strarray2(0), 0)
                        t -= 1
                        If (tmp_strarray2(1) < t) Then
                            Track_Data(7, tmp_strarray(0) - 1, tmp_strarray(1) - 1, tmp_strarray2(0), 2) = tmp_strarray2(3) * 2 - 1
                        Else
                            Track_Data(7, tmp_strarray(0) - 1, tmp_strarray(1) - 1, tmp_strarray2(0), 2) = tmp_strarray2(3) * 2
                        End If
                    End If
                    If (format_version >= 4) Then ' LASER EFFECT IN SDVX II
                        Track_Data(7, tmp_strarray(0) - 1, tmp_strarray(1) - 1, tmp_strarray2(0) + test_value, 3) = tmp_strarray2(4)
                    End If
                Else
                    If (tmp_strarray2(2) = 1) Then
                        Track_Data(7, tmp_strarray(0) - 1, tmp_strarray(1) - 1, tmp_strarray2(0), 0) = 128
                    Else
                        Track_Data(7, tmp_strarray(0) - 1, tmp_strarray(1) - 1, tmp_strarray2(0), 0) = tmp_strarray2(1) + 1
                    End If
                    Track_Data(7, tmp_strarray(0) - 1, tmp_strarray(1) - 1, tmp_strarray2(0), 1) = tmp_strarray2(2)
                    Track_Data(7, tmp_strarray(0) - 1, tmp_strarray(1) - 1, tmp_strarray2(0), 2) = tmp_strarray2(3)
                    If (format_version >= 4) Then ' LASER EFFECT IN SDVX II
                        Track_Data(7, tmp_strarray(0) - 1, tmp_strarray(1) - 1, tmp_strarray2(0), 3) = tmp_strarray2(4)
                    End If
                End If
            Next
        End If
        'header
        output_data = "title=" & vbCrLf & "artist=" & vbCrLf & "effect=" & vbCrLf & "jacket=.jpg" & vbCrLf &
"illustrator=" & vbCrLf & "difficulty=extended" & vbCrLf & "level=1" & vbCrLf & "t=" & Data_BPM(0, 0, 0) & vbCrLf &
"m=.mp3" & vbCrLf & "o=0" & vbCrLf & "bg=desert" & vbCrLf & "layer=arrow" & vbCrLf & "po=0" & vbCrLf & "plength=15000" & vbCrLf &
"pfiltergain=50" & vbCrLf & "filtertype=peak" & vbCrLf & "chokkakuvol=50" & vbCrLf & "ver=133" & vbCrLf & "--" & vbCrLf
        'Show Data
        ProgressBar1.Maximum = end_measure(0)
        For k = 0 To end_measure(0)
            ProgressBar1.Refresh()
            Me.Refresh()
            ProgressBar1.Value = k
            If (Data_Beat(k, 0) > 0) Then
                output_data = output_data & "beat=" & Data_Beat(k, 0) & "/" & Data_Beat(k, 1) & vbCrLf
                current_beat(0) = Data_Beat(k, 0)
                current_beat(1) = Data_Beat(k, 1)
            End If

            For l = 0 To current_beat(0) - 1
                For m = 0 To 192 / current_beat(1) - 1
                    If (format_version >= 4) Then 'SDVX II Laser Effect
                        Dim temptxt As String = ""
                        If ((Track_Data(0, k, l, m, 0) >= 1) And (Not current_laser_effect = Track_Data(0, k, l, m, 3))) Then
                            temptxt = Laser_effect_convect(Track_Data(0, k, l, m, 3))
                            current_laser_effect = Track_Data(0, k, l, m, 3)
                        End If
                        If ((Track_Data(7, k, l, m, 0) >= 1) And (Not current_laser_effect = Track_Data(7, k, l, m, 3))) Then
                            temptxt = Laser_effect_convect(Track_Data(7, k, l, m, 3))
                            current_laser_effect = Track_Data(7, k, l, m, 3)
                        End If
                        If (Not temptxt = "") Then
                            output_data = output_data & temptxt & vbCrLf
                        End If


                        If (Data_Tilt(k, l, m) > 0) Then
                            If (Data_Tilt(k, l, m) = 3) Then
                                If (current_tilt = 2) Then
                                    output_data = output_data & "tilt=keep_biggest"
                                End If
                                If (current_tilt = 1) Then
                                    output_data = output_data & "tilt=keep_normal"
                                End If
                            ElseIf (Data_Tilt(k, l, m) = 2) Then
                                output_data = output_data & "tilt=biggest"
                            ElseIf (Data_Tilt(k, l, m) = 1) Then
                                output_data = output_data & "tilt=normal"
                            End If
                            current_tilt = Data_Tilt(k, l, m)
                            output_data = output_data & vbCrLf
                        End If

                    End If
                    Dim push_string() As String = {"0", "0", "0", "0", "|", "0", "0", "|", "-", "-"}
                    'bpm change first
                    If (Data_BPM(k, l, m) > 0 And k > 0) Then
                        output_data = output_data & "t=" & Data_BPM(k, l, m) & vbCrLf
                    End If
                    'Long BTFX
                    For n = 0 To 5
                        If (LongBTFX(n) > 0) Then
                            If (n > 3) Then
                                push_string(n + 1) = FX_convert(current_FX_Effect(n - 4)) 'FX
                            Else
                                push_string(n) = 2 'BT
                            End If
                            LongBTFX(n) -= 1
                        End If
                    Next
                    'FX-L
                    If (Track_Data(0, k, l, m, 0) > 0 Or Track_Data(0, k, l, m, 1) > 0) Then
                        Track_Data(0, k, l, m, 0) -= 1


                        If (Track_Data(0, k, l, m, 0) - current_laser(0) = 1 Or Track_Data(0, k, l, m, 0) - current_laser(0) = -1 Or Track_Data(0, k, l, m, 0) - current_laser(0) = 2 Or Track_Data(0, k, l, m, 0) - current_laser(0) = -2) Then
                            push_string(8) = Laser_convert(current_laser(0) / 127 * 51)
                        Else
                            push_string(8) = Laser_convert(Track_Data(0, k, l, m, 0) / 127 * 51)
                        End If

                        current_laser(0) = Track_Data(0, k, l, m, 0)
                        If (Track_Data(0, k, l, m, 1) = 1) Then
                            push_string(8) = "0"
                            current_laser(0) = 0
                        End If
                        If (Track_Data(0, k, l, m, 1) = 2) Then
                            current_laser(0) = -1
                        End If
                        If (Track_Data(0, k, l, m, 2) > 0) Then
                            push_string(9) = push_string(9) & Rotate_convert(Track_Data(0, k, l, m, 2))
                        End If
                    ElseIf (Not current_laser(0) = -1) Then
                        push_string(8) = ":"
                    End If
                    If (Track_Data(1, k, l, m, 1) > 0 Or Track_Data(1, k, l, m, 0) > 0) Then
                        If (Track_Data(1, k, l, m, 0) > 0) Then
                            'Long L-FX
                            LongBTFX(4) = Track_Data(1, k, l, m, 0) - 1
                            push_string(5) = FX_convert(Track_Data(1, k, l, m, 1))
                            current_FX_Effect(0) = Track_Data(1, k, l, m, 1)
                            If (current_FX_Effect(0) = 13) Then
                                output_data = output_data & "fx-l_param1=10" & vbCrLf
                            ElseIf (current_FX_Effect(0) = 9) Then
                                output_data = output_data & "fx-l_param1=50" & vbCrLf
                            End If
                        Else
                            push_string(5) = 2
                        End If
                    End If

                    If (Track_Data(2, k, l, m, 1) > 0 Or Track_Data(2, k, l, m, 0) > 0) Then
                        If (Track_Data(2, k, l, m, 0) > 0) Then
                            'Long BT
                            LongBTFX(0) = Track_Data(2, k, l, m, 0) - 1
                            push_string(0) = 2
                        Else
                            push_string(0) = 1
                        End If
                    End If
                    If (Track_Data(3, k, l, m, 1) > 0 Or Track_Data(3, k, l, m, 0) > 0) Then
                        If (Track_Data(3, k, l, m, 0) > 0) Then
                            'Long BT
                            LongBTFX(1) = Track_Data(3, k, l, m, 0) - 1
                            push_string(1) = 2
                        Else
                            push_string(1) = 1
                        End If
                    End If
                    If (Track_Data(4, k, l, m, 1) > 0 Or Track_Data(4, k, l, m, 0) > 0) Then
                        If (Track_Data(4, k, l, m, 0) > 0) Then
                            'Long BT
                            LongBTFX(2) = Track_Data(4, k, l, m, 0) - 1
                            push_string(2) = 2
                        Else
                            push_string(2) = 1
                        End If
                    End If
                    If (Track_Data(5, k, l, m, 1) > 0 Or Track_Data(5, k, l, m, 0) > 0) Then
                        If (Track_Data(5, k, l, m, 0) > 0) Then
                            'Long BT
                            LongBTFX(3) = Track_Data(5, k, l, m, 0) - 1
                            push_string(3) = 2
                        Else
                            push_string(3) = 1
                        End If
                    End If
                    If (Track_Data(6, k, l, m, 1) > 0 Or Track_Data(6, k, l, m, 0) > 0) Then
                        If (Track_Data(6, k, l, m, 0) > 0) Then
                            'Long R-FX
                            LongBTFX(5) = Track_Data(6, k, l, m, 0) - 1
                            push_string(6) = FX_convert(Track_Data(6, k, l, m, 1))
                            current_FX_Effect(1) = Track_Data(6, k, l, m, 1)
                            If (current_FX_Effect(1) = 13) Then
                                output_data = output_data & "fx-r_param1=10" & vbCrLf
                            ElseIf (current_FX_Effect(1) = 9) Then
                                output_data = output_data & "fx-r_param1=50" & vbCrLf
                            End If
                        Else
                            push_string(6) = 2
                        End If
                    End If
                    If (Track_Data(7, k, l, m, 0) > 0 Or Track_Data(7, k, l, m, 1) > 0) Then
                        Track_Data(7, k, l, m, 0) -= 1


                        If (Track_Data(7, k, l, m, 0) - current_laser(1) = 1 Or Track_Data(7, k, l, m, 0) - current_laser(1) = -1 Or Track_Data(7, k, l, m, 0) - current_laser(1) = 2 Or Track_Data(7, k, l, m, 0) - current_laser(1) = -2) Then
                            push_string(9) = Laser_convert(current_laser(1) / 127 * 51)
                        Else
                            push_string(9) = Laser_convert(Track_Data(7, k, l, m, 0) / 127 * 51)
                        End If
                        current_laser(1) = Track_Data(7, k, l, m, 0)
                        If (Track_Data(7, k, l, m, 1) = 1) Then
                            push_string(9) = "o"
                            current_laser(1) = 127
                        End If
                        If (Track_Data(7, k, l, m, 1) = 2) Then
                            current_laser(1) = -1
                        End If
                    ElseIf (Not current_laser(1) = -1) Then
                        push_string(9) = ":"
                    End If
                    If (Track_Data(7, k, l, m, 2) > 0) Then
                        push_string(9) = push_string(9) & Rotate_convert(Track_Data(7, k, l, m, 2))
                    End If
                    output_data = output_data & push_string(0) & push_string(1) & push_string(2) & push_string(3) & push_string(4) & push_string(5) & push_string(6) & push_string(7) & push_string(8) & push_string(9) & vbCrLf
                Next
            Next

            output_data = output_data & "--" & vbCrLf
            My.Computer.FileSystem.WriteAllText(SaveFileDialog1.FileName, output_data, True)
            output_data = ""
        Next

        ProgressBar1.Visible = False
        ListBox1.Enabled = True
    End Sub

    Private Sub SaveFileDialog1_FileOk(sender As System.Object, e As System.ComponentModel.CancelEventArgs) Handles SaveFileDialog1.FileOk
        Button2.Text = SaveFileDialog1.FileName
        create_chart()
    End Sub

    Private Sub Button2_Click(sender As System.Object, e As System.EventArgs) Handles Button2.Click
        SaveFileDialog1.ShowDialog()
    End Sub

    Private Sub LinkLabel1_LinkClicked(sender As System.Object, e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
        AboutBox1.Show()
    End Sub

    Private Sub Button3_Click(sender As System.Object, e As System.EventArgs)
        FolderBrowserDialog1.ShowDialog()
    End Sub

    Private Sub LinkLabel2_LinkClicked(sender As System.Object, e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LinkLabel2.LinkClicked
        Form2.Show()
    End Sub

End Class
