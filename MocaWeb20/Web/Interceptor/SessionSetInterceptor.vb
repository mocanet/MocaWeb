
Imports Moca.Aop
Imports Moca.Exceptions

Namespace Web.Interceptor

	''' <summary>
	''' �Z�b�V�����������Ƃ��Ɏg�p���� Setter ���\�b�h�C���^�[�Z�v�^�[
	''' </summary>
	''' <remarks></remarks>
	Public Class SessionSetInterceptor
		Inherits AbstractHttpInterceptor
		Implements IMethodInterceptor

		''' <summary>�Z�b�V������</summary>
		Private _name As String

		''' <summary>log4net logger</summary>
		Private ReadOnly _mylog As log4net.ILog = log4net.LogManager.GetLogger(String.Empty)

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

		''' <summary>
		''' ���\�b�h���s
		''' </summary>
		''' <param name="invocation">Interceptor����C���^�[�Z�v�g����Ă��郁�\�b�h�̏��</param>
		''' <returns>����</returns>
		''' <remarks>
		''' �Z�b�V�����������ɃZ�b�V�����փI�u�W�F�N�g��ݒ肷��B<br/>
		''' �ݒ���e�� Nothing �̎��́A�Z�b�V��������폜���܂��B
		''' </remarks>
		Public Function Invoke(ByVal invocation As Aop.IMethodInvocation) As Object Implements Aop.IMethodInterceptor.Invoke
			Dim contents As IHttpContents
			Dim methodName As String = invocation.This.GetType.FullName & "." & invocation.Method.Name

			checkHttpContents(invocation.This)

			contents = DirectCast(invocation.This, IHttpContents)

			_mylog.DebugFormat("(Aspect:{0}) SessionID({1}) Setter.{2}={3}", methodName, contents.Session.SessionID, _name, invocation.Args(0))

			' Nothing ��ݒ肷��Ƃ��́A�폜����
			If invocation.Args(0) Is Nothing Then
				contents.Session.Remove(_name)
			Else
				contents.Session(_name) = invocation.Args(0)
			End If

			Return Nothing
		End Function

	End Class

End Namespace
