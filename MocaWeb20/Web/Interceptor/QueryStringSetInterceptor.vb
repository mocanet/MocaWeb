
Imports Moca.Aop

Namespace Web.Interceptor

	''' <summary>
	''' �N�G���[������ϐ��������Ƃ��Ɏg�p���� Setter ���\�b�h�C���^�[�Z�v�^�[
	''' </summary>
	''' <remarks>
	''' ���N�G�X�g�̃N�G���[��ύX���邱�Ƃ͖����̂ŁASetter ���\�b�h�͕K�v�ł͂Ȃ����A
	''' ���_�C���N�g����Ƃ��ȂǂɃN�G���[��������쐬����ꍇ�ȂǂɎg�p����B
	''' </remarks>
	Public Class QueryStringSetInterceptor
		Inherits AbstractHttpInterceptor
		Implements IMethodInterceptor

		''' <summary>�N�G���[������</summary>
		Private _name As String

		''' <summary>log4net logger</summary>
		Private ReadOnly _mylog As log4net.ILog = log4net.LogManager.GetLogger(String.Empty)

#Region " �R���X�g���N�^ "

		''' <summary>
		''' �R���X�g���N�^
		''' </summary>
		''' <param name="name">�N�G���[������</param>
		''' <remarks></remarks>
		Public Sub New(ByVal name As String)
			_name = name
		End Sub

#End Region

		''' <summary>
		''' ���\�b�h���s
		''' </summary>
		''' <param name="invocation">Interceptor����C���^�[�Z�v�g����Ă��郁�\�b�h�̏��</param>
		''' <returns>�Y������A�v���P�[�V�����I�u�W�F�N�g</returns>
		''' <remarks>
		''' �A�v���P�[�V�����������ɃA�v���P�[�V��������I�u�W�F�N�g��ݒ肷��B
		''' </remarks>
		Public Function Invoke(ByVal invocation As Aop.IMethodInvocation) As Object Implements Aop.IMethodInterceptor.Invoke
			Dim contents As IHttpContents
			Dim methodName As String = invocation.This.GetType.FullName & "." & invocation.Method.Name

			checkHttpContents(invocation.This)

			contents = DirectCast(invocation.This, IHttpContents)

			_mylog.DebugFormat("(Aspect:{0}) QueryString Setter.{1}={2}", methodName, _name, CStr(invocation.Args(0)))

			' Nothing ��ݒ肷��Ƃ��́A�폜����
			If invocation.Args(0) Is Nothing Then
				contents.QueryStringMap.Remove(_name)
			Else
				If contents.QueryStringMap.ContainsKey(_name) Then
					contents.QueryStringMap.Item(_name) = CStr(invocation.Args(0))
				Else
					contents.QueryStringMap.Add(_name, CStr(invocation.Args(0)))
				End If
			End If

			Return Nothing
		End Function

	End Class

End Namespace
