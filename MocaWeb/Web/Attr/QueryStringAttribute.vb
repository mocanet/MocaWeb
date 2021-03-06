
Imports System.Reflection
Imports Moca.Aop
Imports Moca.Di
Imports Moca.Util
Imports Moca.Web.Interceptor

Namespace Web.Attr

	''' <summary>
	''' クエリー文字列属性
	''' </summary>
	''' <remarks>
	''' クエリー文字列として扱いたいインタフェースに対して指定します。
	''' </remarks>
	<AttributeUsage(AttributeTargets.Interface)> _
	Public Class QueryStringAttribute
		Inherits Attribute

		''' <summary>
		''' コンポーネント作成
		''' </summary>
		''' <param name="target">対象となるオブジェクト</param>
		''' <param name="field">対象となるフィールド</param>
		''' <returns></returns>
		''' <remarks></remarks>
		Public Function CreateComponent(ByVal target As Object, ByVal field As FieldInfo) As MocaComponent
			Dim aspects As ArrayList
			Dim props() As PropertyInfo

			aspects = New ArrayList()

			' フィールドのインタフェースを解析
			props = ClassUtil.GetProperties(field.FieldType)
			For Each prop As PropertyInfo In props
				Dim name As String
				Dim attr As QueryStringNameAttribute

				name = prop.Name
				attr = ClassUtil.GetCustomAttribute(Of QueryStringNameAttribute)(prop)
				If attr IsNot Nothing Then
					name = attr.Name
				End If

				' Getter/Setter メソッドのアスペクト作成
				Dim pointcut As IPointcut
				pointcut = New Pointcut(New String() {prop.GetGetMethod().ToString})
				aspects.Add(New Aspect(New QueryStringGetInterceptor(name), pointcut))
				pointcut = New Pointcut(New String() {prop.GetSetMethod().ToString})
				aspects.Add(New Aspect(New QueryStringSetInterceptor(name), pointcut))
			Next

			' コンポーネント作成
			Dim component As MocaComponent4Http
			component = New MocaComponent4Http(field.FieldType, field.FieldType)
			component.Aspects = DirectCast(aspects.ToArray(GetType(IAspect)), IAspect())
			Return component
		End Function

	End Class

End Namespace
