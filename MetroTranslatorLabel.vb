Option Strict On
Imports System.Text
Imports MadMilkman.Ini
Public Class MetroTranslatorLabel : Inherits MetroSuite.MetroLabel ' under construction

    Public Enum LanguageList
        Undefinied
        German
        English
    End Enum

#Region " Functions "

    Private Function MakeLinuxCompatible(ByVal InputText As String) As String
        If QClipboard.LinuxMode = False Then
            Return InputText
        Else
            Return InputText.Replace(CChar("\"), CChar("/"))
        End If
    End Function

    Private Function Coding(ByVal input As String) As String
        If input.Contains("<br>") Then
            Return input.Replace("<br>", Environment.NewLine) ' Linebreak
        Else
            Return input
        End If
    End Function

#End Region

    Public Property LanguageKey As String ' Usage: SECTION:KEY

    Private _SelectedLanguage As LanguageList = LanguageList.German ' Default is German at the moment for testing purposes
    Public Property Language() As LanguageList
        Get
            Return _SelectedLanguage
        End Get
        Set(ByVal value As LanguageList)
            _SelectedLanguage = value ' SetValue
            SetLanguage(value) ' SetLanguage
        End Set
    End Property

    Private Sub SetLanguage(ByVal LANG As LanguageList)
        Try
            Dim NewIniOptions As New IniOptions() With {.Encoding = Encoding.UTF8}
            Dim _IniFile As New IniFile(NewIniOptions)

            Select Case LANG ' Selected Language

                Case LanguageList.Undefinied
                    ' Nothing

                Case LanguageList.English
                    _IniFile.Load(MakeLinuxCompatible(".\Data\Language\English.lang")) ' Load English Language File (ini file)
                    Dim _Section As String = Me.LanguageKey.Split(CChar(":"))(0) ' Splits Section from LanguageKey
                    Dim _Key As String = Me.LanguageKey.Split(CChar(":"))(1) ' Splits Key from LanguageKey

                    Me.Text = Coding(_IniFile.Sections(_Section).Keys(_Key).Value) ' Get value from file, encode text, finally sets text

                Case LanguageList.German
                    _IniFile.Load(MakeLinuxCompatible(".\Data\Language\German.lang")) ' Load German Language File (ini file)
                    Dim _Section As String = Me.LanguageKey.Split(CChar(":"))(0) ' Splits Section from LanguageKey
                    Dim _Key As String = Me.LanguageKey.Split(CChar(":"))(1) ' Splits Key from LanguageKey

                    Me.Text = Coding(_IniFile.Sections(_Section).Keys(_Key).Value) ' Get value from file, encode text, finally sets text

            End Select

        Catch ex As Exception
            Me.Text = ex.Message ' Shows error message
        End Try

    End Sub

End Class
