
Imports Moca.Exceptions

Namespace Web.Interceptor

	''' <summary>
	''' HTTP�n�A�v���ŃC���^�[�Z�v�^�[���g���Ƃ��ɕ֗��Ȓ��ۃN���X
	''' </summary>
	''' <remarks></remarks>
	Public MustInherit Class AbstractHttpInterceptor

		''' <summary>log4net logger</summary>
		Private ReadOnly _mylog As log4net.ILog = log4net.LogManager.GetLogger(String.Empty)

		''' <summary>
		''' �R���e���c�`�F�b�N
		''' </summary>
		''' <param name="target">�`�F�b�N�Ώ�</param>
		''' <remarks></remarks>
		Protected Sub checkHttpContents(ByVal target As Object)
			If TryCast(target, IHttpContents) Is Nothing Then
				Throw New MocaRuntimeException("Http �ʐM�ň����I�u�W�F�N�g�� IHttpContents ���������Ă��܂���B")
			End If
		End Sub

	End Class

End Namespace
