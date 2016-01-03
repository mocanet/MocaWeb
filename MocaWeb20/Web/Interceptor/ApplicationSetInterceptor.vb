
Imports Moca.Aop
Imports Moca.Exceptions

Namespace Web.Interceptor

	''' <summary>
	''' アプリケーションを扱うときに使用する Setter メソッドインターセプター
	''' </summary>
	''' <remarks></remarks>
	Public Class ApplicationSetInterceptor
		Inherits AbstractHttpInterceptor
		Implements IMethodInterceptor

		''' <summary>アプリケーション名</summary>
		Private _name As String

		''' <summary>log4net logger</summary>
		Private ReadOnly _mylog As log4net.ILog = log4net.LogManager.GetLogger(String.Empty)

#Region " コンストラクタ "

		''' <summary>
		''' コンストラクタ
		''' </summary>
		''' <param name="name">アプリケーション名</param>
		''' <remarks></remarks>
		Public Sub New(ByVal name As String)
			_name = name
		End Sub

#End Region

		''' <summary>
		''' メソッド実行
		''' </summary>
		''' <param name="invocation">Interceptorからインターセプトされているメソッドの情報</param>
		''' <returns>無し</returns>
		''' <remarks>
		''' アプリケーション名を元にアプリケーションへオブジェクトを設定する。<br/>
		''' 設定内容が Nothing の時は、アプリケーションから削除します。
		''' </remarks>
		Public Function Invoke(ByVal invocation As Aop.IMethodInvocation) As Object Implements Aop.IMethodInterceptor.Invoke
			Dim contents As IHttpContents
			Dim methodName As String = invocation.This.GetType.FullName & "." & invocation.Method.Name

			checkHttpContents(invocation.This)

			contents = DirectCast(invocation.This, IHttpContents)

			_mylog.DebugFormat("(Aspect:{0}) Application Setter.{1}", methodName, _name)

			' Nothing を設定するときは、削除する
			If invocation.Args(0) Is Nothing Then
				contents.Application.Remove(_name)
			Else
				contents.Application(_name) = invocation.Args(0)
			End If

			Return Nothing
		End Function

	End Class

End Namespace
