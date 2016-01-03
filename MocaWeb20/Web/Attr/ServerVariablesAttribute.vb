
Imports System.Reflection
Imports Moca.Aop
Imports Moca.Di
Imports Moca.Util
Imports Moca.Web.Interceptor

Namespace Web.Attr

	''' <summary>
	''' Web サーバー変数コレクション属性
	''' </summary>
	''' <remarks>
	''' Web サーバー変数コレクションとして扱いたいインタフェースに対して指定します。
	''' </remarks>
	<AttributeUsage(AttributeTargets.Interface)> _
	Public Class ServerVariablesAttribute
		Inherits Attribute

		''' <summary>
		''' コンポーネント作成
		''' </summary>
		''' <param name="target">対象となるオブジェクト</param>
		''' <param name="field">対象となるフィールド</param>
		''' <returns>コンポーネント</returns>
		''' <remarks></remarks>
		Public Function CreateComponent(ByVal target As Object, ByVal field As FieldInfo) As MocaComponent
			Dim aspects As ArrayList
			Dim props() As PropertyInfo

			aspects = New ArrayList()

			' フィールドのインタフェースを解析
			props = ClassUtil.GetProperties(field.FieldType)
			For Each prop As PropertyInfo In props
				Dim name As String
				Dim attr As ServerVariableNameAttribute

				name = prop.Name
				attr = ClassUtil.GetCustomAttribute(Of ServerVariableNameAttribute)(prop)
				If attr IsNot Nothing Then
					name = attr.Name
				End If

				' Getter のアスペクトを作成
				Dim pointcut As IPointcut
				pointcut = New Pointcut(New String() {prop.GetGetMethod().ToString})
				aspects.Add(New Aspect(New ServerVariableGetInterceptor(name), pointcut))
			Next

			' コンポーネントを作成
			Dim component As MocaComponent4Http
			component = New MocaComponent4Http(field.FieldType, field.FieldType)
			component.Aspects = DirectCast(aspects.ToArray(GetType(IAspect)), IAspect())
			Return component
		End Function

	End Class

End Namespace
