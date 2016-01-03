
Imports System.Reflection
Imports Moca.Aop
Imports Moca.Di
Imports Moca.Util
Imports Moca.Web.Interceptor

Namespace Web.Attr

	''' <summary>
	''' �A�v���P�[�V��������
	''' </summary>
	''' <remarks>
	''' �A�v���P�[�V�����Ƃ��Ĉ��������C���^�t�F�[�X�ɑ΂��Ďw�肵�܂��B
	''' </remarks>
	<AttributeUsage(AttributeTargets.Interface)> _
	Public Class ApplicationAttribute
		Inherits Attribute

		''' <summary>
		''' �R���|�[�l���g�쐬
		''' </summary>
		''' <param name="target">�ΏۂƂȂ�I�u�W�F�N�g</param>
		''' <param name="field">�ΏۂƂȂ�t�B�[���h</param>
		''' <returns>�R���|�[�l���g</returns>
		''' <remarks></remarks>
		Public Function CreateComponent(ByVal target As Object, ByVal field As FieldInfo) As MocaComponent
			Dim aspects As ArrayList
			Dim props() As PropertyInfo

			aspects = New ArrayList()

			' �t�B�[���h�̃C���^�t�F�[�X�����
			props = ClassUtil.GetProperties(field.FieldType)
			For Each prop As PropertyInfo In props
				Dim name As String
				Dim attr As ApplicationNameAttribute

				name = prop.Name
				attr = ClassUtil.GetCustomAttribute(Of ApplicationNameAttribute)(prop)
				If attr IsNot Nothing Then
					name = attr.Name
				End If

				' Getter/Setter �̃A�X�y�N�g���쐬
				Dim pointcut As IPointcut
				pointcut = New Pointcut(New String() {prop.GetGetMethod().ToString})
				aspects.Add(New Aspect(New ApplicationGetInterceptor(name), pointcut))
				pointcut = New Pointcut(New String() {prop.GetSetMethod().ToString})
				aspects.Add(New Aspect(New ApplicationSetInterceptor(name), pointcut))
			Next

			' �R���|�[�l���g���쐬
			Dim component As MocaComponent4Http
			component = New MocaComponent4Http(field.FieldType, field.FieldType)
			component.Aspects = DirectCast(aspects.ToArray(GetType(IAspect)), IAspect())
			Return component
		End Function

	End Class

End Namespace
