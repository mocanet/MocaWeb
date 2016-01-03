
Namespace Web.Attr

	''' <summary>
	''' Web サーバー変数名属性
	''' </summary>
	''' <remarks>
	''' 通常はプロパティ名をそのままWeb サーバー変数のキーとして使用しますが、
	''' プロパティ名とは別に指定したいときは、この属性で指定します。
	''' </remarks>
	<AttributeUsage(AttributeTargets.Property)> _
	Public Class ServerVariableNameAttribute
		Inherits Attribute

		''' <summary>セッション名</summary>
		Private _name As String

#Region " コンストラクタ "

		''' <summary>
		''' コンストラクタ
		''' </summary>
		''' <param name="name">セッション名</param>
		''' <remarks></remarks>
		Public Sub New(ByVal name As String)
			_name = name
		End Sub

#End Region
#Region " プロパティ "

		''' <summary>
		''' セッション名プロパティ
		''' </summary>
		''' <value></value>
		''' <returns></returns>
		''' <remarks></remarks>
		Public ReadOnly Property Name() As String
			Get
				Return _name
			End Get
		End Property

#End Region

	End Class

End Namespace
