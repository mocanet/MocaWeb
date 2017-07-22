
Imports System.Reflection

Namespace Web

	''' <summary>
	''' Http時のインタセプターで使用するHttpApplicationコンテンツ
	''' </summary>
	''' <remarks></remarks>
	Public Class HttpContentsApplication
		Inherits MarshalByRefObject
		Implements IHttpContents

		Private _target As HttpApplication

#Region " コンストラクタ "

		''' <summary>
		''' コンストラクタ
		''' </summary>
		''' <param name="target">HttpApplication</param>
		''' <remarks></remarks>
		Public Sub New(ByVal target As HttpApplication)
			_target = target
		End Sub

#End Region

#Region " プロパティ "

		''' <summary>
		''' HttpApplicationプロパティ
		''' </summary>
		''' <value></value>
		''' <returns></returns>
		''' <remarks></remarks>
		Public Property WebService() As System.Web.HttpApplication
			Get
				Return _target
			End Get
			Set(ByVal value As System.Web.HttpApplication)
				_target = value
			End Set
		End Property

#End Region

#Region " Implements "

		Public ReadOnly Property Target As Object Implements IHttpContents.Target
			Get
				Return _target
			End Get
		End Property

		Public ReadOnly Property Application As System.Web.HttpApplicationState Implements IHttpContents.Application
			Get
				Return _target.Application
			End Get
		End Property

		Public ReadOnly Property QueryStringMap As System.Collections.Hashtable Implements IHttpContents.QueryStringMap
			Get
				Return Nothing
			End Get
		End Property

		Public ReadOnly Property Request As System.Web.HttpRequest Implements IHttpContents.Request
			Get
				Return _target.Context.Request
			End Get
		End Property

		Public ReadOnly Property Response As System.Web.HttpResponse Implements IHttpContents.Response
			Get
				Return _target.Context.Response
			End Get
		End Property

		Public ReadOnly Property Session As System.Web.SessionState.HttpSessionState Implements IHttpContents.Session
			Get
				Return _target.Session
			End Get
		End Property

#End Region

	End Class

End Namespace
