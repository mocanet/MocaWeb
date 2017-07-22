
Namespace Web

	Public Class MocaHttpHandler

		Private _injector As Moca.Di.MocaWebInjector

		''' <summary>
		''' コンストラクタ
		''' </summary>
		''' <remarks></remarks>
		Public Sub New()
			_injector = New Moca.Di.MocaWebInjector
			_injector.Inject(Me)
		End Sub

	End Class

End Namespace
