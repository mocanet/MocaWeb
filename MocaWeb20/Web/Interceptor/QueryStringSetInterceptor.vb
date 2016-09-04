
Imports Moca.Aop

Namespace Web.Interceptor

	''' <summary>
	''' クエリー文字列変数を扱うときに使用する Setter メソッドインターセプター
	''' </summary>
	''' <remarks>
	''' リクエストのクエリーを変更することは無いので、Setter メソッドは必要ではないが、
	''' リダイレクトするときなどにクエリー文字列を作成する場合などに使用する。
	''' </remarks>
	Public Class QueryStringSetInterceptor
		Inherits AbstractHttpInterceptor
		Implements IMethodInterceptor

		''' <summary>クエリー文字列名</summary>
		Private _name As String

		''' <summary>log4net logger</summary>
		Private ReadOnly _mylog As log4net.ILog = log4net.LogManager.GetLogger(String.Empty)

#Region " コンストラクタ "

		''' <summary>
		''' コンストラクタ
		''' </summary>
		''' <param name="name">クエリー文字列名</param>
		''' <remarks></remarks>
		Public Sub New(ByVal name As String)
			_name = name
		End Sub

#End Region

		''' <summary>
		''' メソッド実行
		''' </summary>
		''' <param name="invocation">Interceptorからインターセプトされているメソッドの情報</param>
		''' <returns>該当するアプリケーションオブジェクト</returns>
		''' <remarks>
		''' アプリケーション名を元にアプリケーションからオブジェクトを設定する。
		''' </remarks>
		Public Function Invoke(ByVal invocation As Aop.IMethodInvocation) As Object Implements Aop.IMethodInterceptor.Invoke
			Dim contents As IHttpContents
			Dim methodName As String = invocation.This.GetType.FullName & "." & invocation.Method.Name

			checkHttpContents(invocation.This)

			contents = DirectCast(invocation.This, IHttpContents)

			_mylog.DebugFormat("(Aspect:{0}) QueryString Setter.{1}={2}", methodName, _name, CStr(invocation.Args(0)))

			' Nothing を設定するときは、削除する
			If invocation.Args(0) Is Nothing Then
				contents.QueryStringMap.Remove(_name)
			Else
				If contents.QueryStringMap.ContainsKey(_name) Then
					contents.QueryStringMap.Item(_name) = CStr(invocation.Args(0))
				Else
					contents.QueryStringMap.Add(_name, CStr(invocation.Args(0)))
				End If
			End If

			Return Nothing
		End Function

	End Class

End Namespace
