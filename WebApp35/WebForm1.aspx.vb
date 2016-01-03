
Public Class WebForm1
    Inherits System.Web.UI.Page

    <Moca.Attr.Implementation(GetType(Test))>
    Protected tes As ITest

    Private Sub WebForm1_Init(sender As Object, e As EventArgs) Handles Me.Init
        Dim inject As New Moca.Di.MocaWebInjector
        inject.Inject(Me)
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.Label1.Text = tes.Test
    End Sub

End Class

Public Interface ITest

    Function Test() As String

End Interface

Public Class Test
    Inherits MarshalByRefObject
    Implements ITest

    Private Function ITest_Test() As String Implements ITest.Test
        Return "Test Return"
    End Function

End Class
