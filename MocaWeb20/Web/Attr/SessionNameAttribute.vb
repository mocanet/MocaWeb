
Namespace Web.Attr

	''' <summary>
	''' �Z�b�V����������
	''' </summary>
	''' <remarks>
	''' �ʏ�̓v���p�e�B�������̂܂܃Z�b�V�����̃L�[�Ƃ��Ďg�p���܂����A
	''' �v���p�e�B���Ƃ͕ʂɎw�肵�����Ƃ��́A���̑����Ŏw�肵�܂��B
	''' </remarks>
	<AttributeUsage(AttributeTargets.Property)> _
	Public Class SessionNameAttribute
		Inherits Attribute

		''' <summary>�Z�b�V������</summary>
		Private _name As String

#Region " �R���X�g���N�^ "

		''' <summary>
		''' �R���X�g���N�^
		''' </summary>
		''' <param name="name">�Z�b�V������</param>
		''' <remarks></remarks>
		Public Sub New(ByVal name As String)
			_name = name
		End Sub

#End Region

#Region " �v���p�e�B "

		''' <summary>
		''' �Z�b�V�������v���p�e�B
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
