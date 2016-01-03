
Imports Moca.Exceptions

Namespace Web.Interceptor

	''' <summary>
	''' HTTP系アプリでインターセプターを使うときに便利な抽象クラス
	''' </summary>
	''' <remarks></remarks>
	Public MustInherit Class AbstractHttpInterceptor

		''' <summary>log4net logger</summary>
		Private ReadOnly _mylog As log4net.ILog = log4net.LogManager.GetLogger(String.Empty)

		''' <summary>
		''' コンテンツチェック
		''' </summary>
		''' <param name="target">チェック対象</param>
		''' <remarks></remarks>
		Protected Sub checkHttpContents(ByVal target As Object)
			If TryCast(target, IHttpContents) Is Nothing Then
				Throw New MocaRuntimeException("Http 通信で扱うオブジェクトが IHttpContents を実装していません。")
			End If
		End Sub

	End Class

End Namespace
