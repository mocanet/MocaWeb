
Imports System.Reflection

Namespace Web

	''' <summary>
	''' Http時のインタセプターで使用するコンテンツ
	''' </summary>
	''' <remarks></remarks>
	Public Class HttpContents
		Inherits MarshalByRefObject
		Implements IHttpContents

		Private _target As Object

		Private _queryStringMap As Hashtable

#Region " コンストラクタ "

		''' <summary>
		''' コンストラクタ
		''' </summary>
		''' <param name="target">Object</param>
		''' <remarks></remarks>
		Public Sub New(ByVal target As Object)
			_target = target
			_queryStringMap = New Hashtable
		End Sub

#End Region

#Region " Implements "

		Public ReadOnly Property Application As System.Web.HttpApplicationState Implements IHttpContents.Application
			Get
				Return Nothing
			End Get
		End Property

		Public ReadOnly Property QueryStringMap As System.Collections.Hashtable Implements IHttpContents.QueryStringMap
			Get
				Return _queryStringMap
			End Get
		End Property

		Public ReadOnly Property Request As System.Web.HttpRequest Implements IHttpContents.Request
			Get
				Return Nothing
			End Get
		End Property

		Public ReadOnly Property Response As System.Web.HttpResponse Implements IHttpContents.Response
			Get
				Return Nothing
			End Get
		End Property

		Public ReadOnly Property Session As System.Web.SessionState.HttpSessionState Implements IHttpContents.Session
			Get
				Return Nothing
			End Get
		End Property

		Public ReadOnly Property Target As Object Implements IHttpContents.Target
			Get
				Return _target
			End Get
		End Property

#End Region

	End Class

End Namespace
