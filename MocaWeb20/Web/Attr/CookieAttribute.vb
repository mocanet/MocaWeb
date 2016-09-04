
Imports System.Reflection
Imports Moca.Aop
Imports Moca.Di
Imports Moca.Util
Imports Moca.Web.Interceptor

Namespace Web.Attr

	''' <summary>
	''' クッキーの種別
	''' </summary>
	''' <remarks></remarks>
	Public Enum CookieType
		''' <summary>Request 側のクッキー</summary>
		Request = 0
		''' <summary>Response 側のクッキー</summary>
		Response
	End Enum

	''' <summary>
	''' クッキー属性
	''' </summary>
	''' <remarks>
	''' クッキーとして扱いたいフィールドに対して指定します。
	''' </remarks>
	<AttributeUsage(AttributeTargets.Field)> _
	Public Class CookieAttribute
		Inherits Attribute

		''' <summary>クッキー名</summary>
		Private _cookieType As CookieType

#Region " コンストラクタ "

		''' <summary>
		''' コンストラクタ
		''' </summary>
		''' <param name="dataType">クッキー名</param>
		''' <remarks></remarks>
		Public Sub New(ByVal dataType As CookieType)
			_cookieType = dataType
		End Sub

#End Region

#Region " プロパティ "

		''' <summary>
		''' クッキー名プロパティ
		''' </summary>
		''' <value></value>
		''' <returns></returns>
		''' <remarks></remarks>
		Public ReadOnly Property DataType() As CookieType
			Get
				Return _cookieType
			End Get
		End Property

#End Region

		''' <summary>
		''' コンポーネント作成
		''' </summary>
		''' <param name="target">対象となるオブジェクト</param>
		''' <param name="field">対象となるフィールド</param>
		''' <returns></returns>
		''' <remarks></remarks>
        Public Function CreateComponent(Of T)(ByVal target As Object, ByVal field As FieldInfo) As MocaComponent
            Dim aspects As ArrayList
            Dim props() As PropertyInfo

            aspects = New ArrayList()

            ' フィールドのインタフェースを解析
            props = ClassUtil.GetProperties(field.FieldType)
            For Each prop As PropertyInfo In props
                Dim name As String
                Dim attr As CookieNameAttribute

                name = prop.Name
                attr = ClassUtil.GetCustomAttribute(Of CookieNameAttribute)(prop)
                If attr IsNot Nothing Then
                    name = attr.Name
                End If

                ' Getter メソッドのアスペクト作成
                Dim pointcut As IPointcut
                pointcut = New Pointcut(New String() {prop.GetGetMethod().ToString})
                aspects.Add(New Aspect(New CookieInterceptor(DataType, name), pointcut))
            Next

            ' コンポーネント作成
            ' キーでコンポーネントは格納する。フィールドの型名．データタイプ値
            Dim component As MocaComponent
            component = CType(Moca.Util.ClassUtil.NewInstance(GetType(T), New Object() {field.FieldType.FullName & "." & DataType, field.FieldType}), MocaComponent)
            component.Aspects = DirectCast(aspects.ToArray(GetType(IAspect)), IAspect())
            Return component
        End Function

	End Class

End Namespace
