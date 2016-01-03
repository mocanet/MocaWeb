
Imports Moca.Aop
Imports Moca.Util
Imports Moca.Web

Namespace Di

	''' <summary>
	''' �R���e�i�Ɋi�[����Http�������R���|�[�l���g
	''' </summary>
	''' <remarks></remarks>
	Public Class MocaComponent4Http
		Inherits MocaComponent

#Region " �R���X�g���N�^ "

		''' <summary>
		''' �R���X�g���N�^
		''' </summary>
		''' <param name="implType">���Ԃ̌^</param>
		''' <param name="fieldType">�t�B�[���h�̌^</param>
		''' <remarks></remarks>
		Public Sub New(ByVal implType As Type, ByVal fieldType As Type)
			MyBase.New(implType, fieldType)
		End Sub

		''' <summary>
		''' �R���X�g���N�^
		''' </summary>
		''' <param name="key">�R���|�[�l���g�̃L�[</param>
		''' <param name="fieldType">�t�B�[���h�̌^</param>
		''' <remarks></remarks>
		Public Sub New(ByVal key As String, ByVal fieldType As Type)
			MyBase.New(key, fieldType)
		End Sub

#End Region

		''' <summary>
		''' �I�u�W�F�N�g���C���X�^���X�����ĕԂ��܂��B
		''' </summary>
		''' <param name="target">�ΏۂƂȂ�y�[�W</param>
		''' <returns></returns>
		''' <remarks></remarks>
        Public Shadows Function Create(ByVal target As Object, ByVal httpContentsType As Type) As Object
            If Aspects.Length = 0 Then
                Return createObject(target)
            End If
            Return createProxyObject(target, httpContentsType)
        End Function

		''' <summary>
		''' �I�u�W�F�N�g���C���X�^���X�����ĕԂ��܂��B
		''' </summary>
		''' <param name="target">�ΏۂƂȂ�y�[�W</param>
		''' <returns></returns>
		''' <remarks></remarks>
        Protected Shadows Function createObject(ByVal target As Object) As Object
            Dim val As Object
            val = ClassUtil.NewInstance(ImplType, New Object() {target})
            Return val
        End Function

		''' <summary>
		''' �I�u�W�F�N�g���v���L�V�Ƃ��ăC���X�^���X�����ĕԂ��܂��B
		''' </summary>
		''' <param name="target">�ΏۂƂȂ�y�[�W</param>
		''' <returns></returns>
		''' <remarks>
		''' HttpContents ���C���X�^���X������ FieldType �̌^�ɍ��킹�ăv���L�V���쐬���Ă܂��B
		''' Web�ł̓}���`�X���b�h�ɂȂ�̂ŁAInterceptor �őΏۂƂȂ� Page ����肷��ׂɂ́A
		''' �Ώۂ̃Z�b�V������� Page �C���X�^���X���K�v�ƂȂ�B
		''' ���ׁ̈AASP��ł� Page ���擾�o����悤�ɕK���AHttpContents �����̉����邱�Ƃɂ����B
		''' </remarks>
        Protected Shadows Function createProxyObject(ByVal target As Object, ByVal httpContentsType As Type) As Object
            Dim val As Object = Nothing
            Dim proxy As AopProxy

            val = ClassUtil.NewInstance(httpContentsType, New Object() {target})
            proxy = New AopProxy(FieldType, Aspects, val)
            val = proxy.Create()
            Return val
        End Function

	End Class

End Namespace
