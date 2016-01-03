
Imports Moca.Attr
Imports Moca.Di
Imports Moca.Util
Imports Moca.Web.Attr

Namespace Di

	''' <summary>
	''' �y�[�W�ɑ΂��Ă̈ˑ�������
	''' </summary>
	''' <remarks></remarks>
	Public Class MocaWebInjector
		Inherits MocaInjector

#Region " �R���X�g���N�^ "

		''' <summary>
		''' �f�t�H���g�R���X�g���N�^
		''' </summary>
		''' <remarks></remarks>
		Public Sub New()
			MyBase.New()

            Me.Analyzer.FieldInject = AddressOf Me.fieldInject
		End Sub

#End Region

		''' <summary>
		''' �t�B�[���h�փC���X�^���X�̒���
		''' </summary>
		''' <param name="target">�ΏۂƂȂ�I�u�W�F�N�g</param>
		''' <param name="field">�ΏۂƂȂ�t�B�[���h</param>
		''' <param name="component">�ΏۂƂȂ�R���|�[�l���g</param>
		''' <returns>���������C���X�^���X</returns>
		''' <remarks>
		''' MocaComponent4Http �Ƃ��Ĉ����������߃I�[�o�[���C�h
		''' </remarks>
		Protected Shadows Function fieldInject(ByVal target As Object, ByVal field As System.Reflection.FieldInfo, ByVal component As MocaComponent) As Object
			Dim instance As Object
			Dim componentHttp As MocaComponent4Http

			componentHttp = TryCast(component, MocaComponent4Http)
			If componentHttp Is Nothing Then
				instance = component.Create()
				ClassUtil.Inject(target, field, New Object() {instance})
				Return instance
			End If

			Dim typ As Type

			If TypeOf target Is System.Web.HttpApplication Then
				typ = GetType(Web.HttpContentsApplication)
			Else
				typ = GetType(Web.HttpContents)
			End If

			instance = componentHttp.Create(target, typ)
			ClassUtil.Inject(target, field, New Object() {instance})
			Return instance
		End Function

	End Class

End Namespace
