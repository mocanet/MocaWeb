
Imports Moca.Aop
Imports Moca.Exceptions

Namespace Web.Interceptor

	''' <summary>
	''' �N�b�L�[�������Ƃ��Ɏg�p���� Getter ���\�b�h�C���^�[�Z�v�^�[
	''' </summary>
	''' <remarks></remarks>
	Public Class CookieInterceptor
		Inherits AbstractHttpInterceptor
		Implements IMethodInterceptor

		''' <summary>�N�b�L�[���</summary>
		Private _type As Attr.CookieType
		''' <summary>�N�b�L�[��</summary>
		Private _name As String

		''' <summary>log4net logger</summary>
		Private ReadOnly _mylog As log4net.ILog = log4net.LogManager.GetLogger(String.Empty)

#Region " �R���X�g���N�^ "

		''' <summary>
		''' �R���X�g���N�^
		''' </summary>
		''' <param name="typ">�����N�b�L�[�̎��</param>
		''' <param name="name">�N�b�L�[��</param>
		''' <remarks></remarks>
		Public Sub New(ByVal typ As Attr.CookieType, ByVal name As String)
			_type = typ
			_name = name
		End Sub

#End Region

		''' <summary>
		''' ���\�b�h���s
		''' </summary>
		''' <param name="invocation">Interceptor����C���^�[�Z�v�g����Ă��郁�\�b�h�̏��</param>
		''' <returns>�Y������ HttpCookie</returns>
		''' <remarks>
		''' �w�肳��Ă���N�b�L�[��ʂ���N�b�L�[�������� HttpCookie ��Ԃ��B
		''' </remarks>
		Public Function Invoke(ByVal invocation As Aop.IMethodInvocation) As Object Implements Aop.IMethodInterceptor.Invoke
			Dim contents As IHttpContents
			Dim methodName As String = invocation.This.GetType.FullName & "." & invocation.Method.Name

			checkHttpContents(invocation.This)

			contents = DirectCast(invocation.This, IHttpContents)

			methodName = contents.Target.GetType.FullName
			_mylog.DebugFormat("(Aspect:{0}) {1}.Cookies(""{2}"")", methodName, System.Enum.GetName(GetType(Attr.CookieType), _type), _name)

			Dim cookies As HttpCookieCollection

			If _type = Attr.CookieType.Request Then
				cookies = contents.Request.Cookies
			Else
				cookies = contents.Response.Cookies
			End If

			Return cookies(_name)
		End Function

	End Class

End Namespace
