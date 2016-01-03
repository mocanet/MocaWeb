
Imports Moca.Aop
Imports Moca.Exceptions

Namespace Web.Interceptor

	''' <summary>
	''' �A�v���P�[�V�����������Ƃ��Ɏg�p���� Setter ���\�b�h�C���^�[�Z�v�^�[
	''' </summary>
	''' <remarks></remarks>
	Public Class ApplicationSetInterceptor
		Inherits AbstractHttpInterceptor
		Implements IMethodInterceptor

		''' <summary>�A�v���P�[�V������</summary>
		Private _name As String

		''' <summary>log4net logger</summary>
		Private ReadOnly _mylog As log4net.ILog = log4net.LogManager.GetLogger(String.Empty)

#Region " �R���X�g���N�^ "

		''' <summary>
		''' �R���X�g���N�^
		''' </summary>
		''' <param name="name">�A�v���P�[�V������</param>
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
		''' �A�v���P�[�V�����������ɃA�v���P�[�V�����փI�u�W�F�N�g��ݒ肷��B<br/>
		''' �ݒ���e�� Nothing �̎��́A�A�v���P�[�V��������폜���܂��B
		''' </remarks>
		Public Function Invoke(ByVal invocation As Aop.IMethodInvocation) As Object Implements Aop.IMethodInterceptor.Invoke
			Dim contents As IHttpContents
			Dim methodName As String = invocation.This.GetType.FullName & "." & invocation.Method.Name

			checkHttpContents(invocation.This)

			contents = DirectCast(invocation.This, IHttpContents)

			_mylog.DebugFormat("(Aspect:{0}) Application Setter.{1}", methodName, _name)

			' Nothing ��ݒ肷��Ƃ��́A�폜����
			If invocation.Args(0) Is Nothing Then
				contents.Application.Remove(_name)
			Else
				contents.Application(_name) = invocation.Args(0)
			End If

			Return Nothing
		End Function

	End Class

End Namespace
