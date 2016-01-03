
Imports Moca.Aop
Imports Moca.Exceptions

Namespace Web.Interceptor

	''' <summary>
	''' クッキーを扱うときに使用する Getter メソッドインターセプター
	''' </summary>
	''' <remarks></remarks>
	Public Class CookieInterceptor
		Inherits AbstractHttpInterceptor
		Implements IMethodInterceptor

		''' <summary>クッキー種別</summary>
		Private _type As Attr.CookieType
		''' <summary>クッキー名</summary>
		Private _name As String

		''' <summary>log4net logger</summary>
		Private ReadOnly _mylog As log4net.ILog = log4net.LogManager.GetLogger(String.Empty)

#Region " コンストラクタ "

		''' <summary>
		''' コンストラクタ
		''' </summary>
		''' <param name="typ">扱うクッキーの種別</param>
		''' <param name="name">クッキー名</param>
		''' <remarks></remarks>
		Public Sub New(ByVal typ As Attr.CookieType, ByVal name As String)
			_type = typ
			_name = name
		End Sub

#End Region

		''' <summary>
		''' メソッド実行
		''' </summary>
		''' <param name="invocation">Interceptorからインターセプトされているメソッドの情報</param>
		''' <returns>該当する HttpCookie</returns>
		''' <remarks>
		''' 指定されているクッキー種別からクッキー名を元に HttpCookie を返す。
		''' </remarks>
		Public Function Invoke(ByVal invocation As Aop.IMethodInvocation) As Object Implements Aop.IMethodInterceptor.Invoke
			Dim contents As IHttpContents
			Dim methodName As String = invocation.This.GetType.FullName & "." & invocation.Method.Name

			checkHttpContents(invocation.This)

			contents = DirectCast(invocation.This, IHttpContents)

			methodName = contents.Target.GetType.FullName
			_mylog.DebugFormat("(Aspect:{0}) {1}.Cookies(""{2}"")", methodName, System.Enum.GetName(GetType(Attr.CookieType), _type), _name)

			Dim cookies As HttpCookieCollection

			If _type = Attr.CookieType.Request Then
				cookies = contents.Request.Cookies
			Else
				cookies = contents.Response.Cookies
			End If

			Return cookies(_name)
		End Function

	End Class

End Namespace
