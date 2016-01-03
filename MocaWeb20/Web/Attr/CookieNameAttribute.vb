
Imports System.Reflection
Imports Moca.Aop
Imports Moca.Util
Imports Moca.Web.Interceptor

Namespace Web.Attr

	''' <summary>
	''' �N�b�L�[������
	''' </summary>
	''' <remarks>
	''' �ʏ�̓v���p�e�B�������̂܂܃N�b�L�[�̃L�[�Ƃ��Ďg�p���܂����A
	''' �v���p�e�B���Ƃ͕ʂɎw�肵�����Ƃ��́A���̑����Ŏw�肵�܂��B
	''' </remarks>
	<AttributeUsage(AttributeTargets.Property)> _
	Public Class CookieNameAttribute
		Inherits Attribute

		''' <summary>�N�b�L�[��</summary>
		Private _name As String

#Region " �R���X�g���N�^ "

		''' <summary>
		''' �R���X�g���N�^
		''' </summary>
		''' <param name="name">�N�b�L�[��</param>
		''' <remarks></remarks>
		Public Sub New(ByVal name As String)
			_name = name
		End Sub

#End Region
#Region " �v���p�e�B "

		''' <summary>
		''' �N�b�L�[���v���p�e�B
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
