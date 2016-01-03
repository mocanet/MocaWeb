
Imports System.Reflection
Imports Moca.Aop
Imports Moca.Di
Imports Moca.Util
Imports Moca.Web.Interceptor

Namespace Web.Attr

	''' <summary>
	''' �Z�b�V��������
	''' </summary>
	''' <remarks>
	''' �Z�b�V�����Ƃ��Ĉ��������C���^�t�F�[�X�ɑ΂��Ďw�肵�܂��B
	''' </remarks>
	<AttributeUsage(AttributeTargets.Interface)> _
	Public Class SessionAttribute
		Inherits Attribute

		''' <summary>
		''' �R���|�[�l���g�쐬
		''' </summary>
		''' <param name="target">�ΏۂƂȂ�I�u�W�F�N�g</param>
		''' <param name="field">�ΏۂƂȂ�t�B�[���h</param>
		''' <returns></returns>
		''' <remarks></remarks>
        Public Function CreateComponent(Of T)(ByVal target As Object, ByVal field As FieldInfo) As MocaComponent
            Dim aspects As ArrayList
            Dim props() As PropertyInfo

            aspects = New ArrayList()

            ' �t�B�[���h�̃C���^�t�F�[�X�����
            props = ClassUtil.GetProperties(field.FieldType)
            For Each prop As PropertyInfo In props
                Dim name As String
                Dim attr As SessionNameAttribute

                name = prop.Name
                attr = ClassUtil.GetCustomAttribute(Of SessionNameAttribute)(prop)
                If attr IsNot Nothing Then
                    name = attr.Name
                End If

                ' Getter/Setter ���\�b�h�̃A�X�y�N�g�쐬
                Dim pointcut As IPointcut
                pointcut = New Pointcut(New String() {prop.GetGetMethod().ToString})
                aspects.Add(New Aspect(New SessionGetInterceptor(name), pointcut))
                pointcut = New Pointcut(New String() {prop.GetSetMethod().ToString})
                aspects.Add(New Aspect(New SessionSetInterceptor(name), pointcut))
            Next

            ' �R���|�[�l���g�쐬
            Dim component As MocaComponent

            component = CType(Moca.Util.ClassUtil.NewInstance(GetType(T), New Object() {field.FieldType, field.FieldType}), MocaComponent)
            component.Aspects = DirectCast(aspects.ToArray(GetType(IAspect)), IAspect())
            Return component
        End Function

    End Class

End Namespace
