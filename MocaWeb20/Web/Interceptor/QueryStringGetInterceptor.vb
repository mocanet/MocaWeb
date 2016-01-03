
Imports Moca.Aop

Namespace Web.Interceptor

	''' <summary>
	''' クエリー文字列変数を扱うときに使用する Getter メソッドインターセプター
	''' </summary>
	''' <remarks></remarks>
	Public Class QueryStringGetInterceptor
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
		''' アプリケーション名を元にアプリケーションからオブジェクトを返す。
		''' </remarks>
		Public Function Invoke(ByVal invocation As Aop.IMethodInvocation) As Object Implements Aop.IMethodInterceptor.Invoke
			Dim contents As IHttpContents
			Dim methodName As String = invocation.This.GetType.FullName & "." & invocation.Method.Name

			checkHttpContents(invocation.This)

			contents = DirectCast(invocation.This, IHttpContents)

			_mylog.DebugFormat("(Aspect:{0}) QueryString Getter.{1}", methodName, _name)

			If contents.QueryStringMap.ContainsKey(_name) Then
				_mylog.DebugFormat("(Aspect:{0}) QueryStringMap Getter.{1}", methodName, _name)
				Return contents.QueryStringMap.Item(_name)
			End If

			Return contents.Request(_name)
		End Function

	End Class

End Namespace
