
Imports System.Reflection
Imports Moca.Aop
Imports Moca.Di
Imports Moca.Util
Imports Moca.Web.Interceptor

Namespace Web.Attr

	''' <summary>
	''' �N�b�L�[�̎��
	''' </summary>
	''' <remarks></remarks>
	Public Enum CookieType
		''' <summary>Request ���̃N�b�L�[</summary>
		Request = 0
		''' <summary>Response ���̃N�b�L�[</summary>
		Response
	End Enum

	''' <summary>
	''' �N�b�L�[����
	''' </summary>
	''' <remarks>
	''' �N�b�L�[�Ƃ��Ĉ��������t�B�[���h�ɑ΂��Ďw�肵�܂��B
	''' </remarks>
	<AttributeUsage(AttributeTargets.Field)> _
	Public Class CookieAttribute
		Inherits Attribute

		''' <summary>�N�b�L�[��</summary>
		Private _cookieType As CookieType

#Region " �R���X�g���N�^ "

		''' <summary>
		''' �R���X�g���N�^
		''' </summary>
		''' <param name="dataType">�N�b�L�[��</param>
		''' <remarks></remarks>
		Public Sub New(ByVal dataType As CookieType)
			_cookieType = dataType
		End Sub

#End Region

#Region " �v���p�e�B "

		''' <summary>
		''' �N�b�L�[���v���p�e�B
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
                Dim attr As CookieNameAttribute

                name = prop.Name
                attr = ClassUtil.GetCustomAttribute(Of CookieNameAttribute)(prop)
                If attr IsNot Nothing Then
                    name = attr.Name
                End If

                ' Getter ���\�b�h�̃A�X�y�N�g�쐬
                Dim pointcut As IPointcut
                pointcut = New Pointcut(New String() {prop.GetGetMethod().ToString})
                aspects.Add(New Aspect(New CookieInterceptor(DataType, name), pointcut))
            Next

            ' �R���|�[�l���g�쐬
            ' �L�[�ŃR���|�[�l���g�͊i�[����B�t�B�[���h�̌^���D�f�[�^�^�C�v�l
            Dim component As MocaComponent
            component = CType(Moca.Util.ClassUtil.NewInstance(GetType(T), New Object() {field.FieldType.FullName & "." & DataType, field.FieldType}), MocaComponent)
            component.Aspects = DirectCast(aspects.ToArray(GetType(IAspect)), IAspect())
            Return component
        End Function

	End Class

End Namespace
