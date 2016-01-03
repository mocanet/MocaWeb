
Namespace Web

	''' <summary>
	''' Http���̃C���^�Z�v�^�[�Ŏg�p����R���e���c�C���^�t�F�[�X
	''' </summary>
	''' <remarks></remarks>
	Public Interface IHttpContents

		''' <summary>
		''' �ΏۂƂȂ�I�u�W�F�N�g
		''' </summary>
		''' <value></value>
		''' <returns></returns>
		''' <remarks></remarks>
		ReadOnly Property Target() As Object

		''' <summary>
		''' �A�v���P�[�V�����v���p�e�B
		''' </summary>
		''' <value></value>
		''' <returns></returns>
		''' <remarks>
		''' Page ���́AMasterPage �̂ǂ��炩�ɃI�u�W�F�N�g�����݂���Ƃ��́A���݂��Ă���I�u�W�F�N�g�� Application ��Ԃ��B
		''' �����ɃI�u�W�F�N�g�����݂��Ă���Ƃ��́A����ł͂��肦�Ȃ��B
		''' </remarks>
		ReadOnly Property Application() As HttpApplicationState

		''' <summary>
		''' ���N�G�X�g�v���p�e�B
		''' </summary>
		''' <value></value>
		''' <returns></returns>
		''' <remarks>
		''' Page ���́AMasterPage �̂ǂ��炩�ɃI�u�W�F�N�g�����݂���Ƃ��́A���݂��Ă���I�u�W�F�N�g�� Request ��Ԃ��B
		''' �����ɃI�u�W�F�N�g�����݂��Ă���Ƃ��́A����ł͂��肦�Ȃ��B
		''' </remarks>
		ReadOnly Property Request() As HttpRequest

		''' <summary>
		''' ���X�|���X�v���p�e�B
		''' </summary>
		''' <value></value>
		''' <returns></returns>
		''' <remarks>
		''' Page ���́AMasterPage �̂ǂ��炩�ɃI�u�W�F�N�g�����݂���Ƃ��́A���݂��Ă���I�u�W�F�N�g�� Response ��Ԃ��B
		''' �����ɃI�u�W�F�N�g�����݂��Ă���Ƃ��́A����ł͂��肦�Ȃ��B
		''' </remarks>
		ReadOnly Property Response() As HttpResponse

		''' <summary>
		''' �Z�b�V�����v���p�e�B
		''' </summary>
		''' <value></value>
		''' <returns></returns>
		''' <remarks>
		''' Page ���́AMasterPage �̂ǂ��炩�ɃI�u�W�F�N�g�����݂���Ƃ��́A���݂��Ă���I�u�W�F�N�g�� Session ��Ԃ��B
		''' �����ɃI�u�W�F�N�g�����݂��Ă���Ƃ��́A����ł͂��肦�Ȃ��B
		''' </remarks>
		ReadOnly Property Session() As HttpSessionState

		''' <summary>
		''' �N�G���[���e���ꎞ�I�ɕۑ�����ׂ̃v���p�e�B
		''' </summary>
		''' <value></value>
		''' <returns></returns>
		''' <remarks>
		''' �V�����l�ŃN�G���[��������쐬����Ƃ��Ȃǂ̂��߁B
		''' </remarks>
		ReadOnly Property QueryStringMap() As Hashtable

	End Interface

End Namespace
