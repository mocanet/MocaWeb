
Imports System.Reflection
Imports System.Text
Imports System.Web
Imports System.Web.UI

Imports Moca.Di
Imports Moca.Util
Imports Moca.Db
Imports Moca.Db.Attr

Namespace Web

	''' <summary>
	''' ���W���[���̏������C�x���g����єj���C�x���g�̋��ʏ���
	''' </summary>
	''' <remarks>
	''' web.config �t�@�C���։��L��ǋL���Ă��������B<br/>
	''' <example>
	''' <code lang="xml">
	''' <system.web>
	''' 	<httpModules>
	''' 		<add name="MocaHTTPModule" type="Moca.Web.MocaHTTPModule" />
	''' 	</httpModules>
	''' </system.web>
	''' </code>
	''' </example>
	''' </remarks>
	Public Class MocaHTTPModule
		Implements IHttpModule

		''' <summary>�y�[�W�ɑ΂��Ă̈ˑ�������</summary>
		Private _injector As MocaWebInjector

		''' <summary>Logging For Log4net</summary>
		Private ReadOnly _mylog As log4net.ILog = log4net.LogManager.GetLogger(String.Empty)

#Region " �R���X�g���N�^�^�f�R���X�g���N�^ "

		''' <summary>
		''' �f�t�H���g�R���X�g���N�^
		''' </summary>
		''' <remarks></remarks>
		Public Sub New()
		End Sub

		Protected Overrides Sub Finalize()
			MyBase.Finalize()
		End Sub

#End Region
#Region " IHttpModule "

		''' <summary>
		''' ���W���[�������������A�v���������ł���悤�ɏ������܂��B
		''' </summary>
		''' <param name="context"></param>
		''' <remarks></remarks>
		Public Sub Init(ByVal context As System.Web.HttpApplication) Implements System.Web.IHttpModule.Init
			' log4net ����
			Dim rootPath As String = context.Context.Server.MapPath("~")
			Dim log4netConfig As String = System.IO.Path.Combine(rootPath, "log4net.config")
			log4net.Config.XmlConfigurator.Configure(New System.IO.FileInfo(log4netConfig))

			' �R���e�i ����
			_injector = New MocaWebInjector()

			' �n���h���[ ����
			AddHandler context.BeginRequest, AddressOf Me._BeginRequest
			AddHandler context.AuthenticateRequest, AddressOf Me._AuthenticateRequest
			AddHandler context.PostAuthenticateRequest, AddressOf Me._PostAuthenticateRequest
			AddHandler context.AuthorizeRequest, AddressOf Me._AuthorizeRequest
			AddHandler context.PostAuthorizeRequest, AddressOf Me._PostAuthorizeRequest
			AddHandler context.ResolveRequestCache, AddressOf Me._ResolveRequestCache
			AddHandler context.PostResolveRequestCache, AddressOf Me._PostResolveRequestCache
			AddHandler context.PostMapRequestHandler, AddressOf Me._PostMapRequestHandler
			AddHandler context.AcquireRequestState, AddressOf Me._AcquireRequestState
			AddHandler context.PostAcquireRequestState, AddressOf Me._PostAcquireRequestState
			AddHandler context.PreRequestHandlerExecute, AddressOf Me._PreRequestHandlerExecute
			' �C�x���g �n���h�������s����܂��B
			AddHandler context.PostRequestHandlerExecute, AddressOf Me._PostRequestHandlerExecute
			AddHandler context.ReleaseRequestState, AddressOf Me._ReleaseRequestState
			AddHandler context.PostReleaseRequestState, AddressOf Me._PostReleaseRequestState
			AddHandler context.UpdateRequestCache, AddressOf Me._UpdateRequestCache
			AddHandler context.PostUpdateRequestCache, AddressOf Me._PostUpdateRequestCache
			AddHandler context.EndRequest, AddressOf Me._EndRequest
		End Sub

		Public Sub Dispose() Implements System.Web.IHttpModule.Dispose
		End Sub

#End Region
#Region " Handler "

		''' <summary>
		''' ASP.NET ���v���ɉ�������Ƃ��ɁA���s�� HTTP �p�C�v���C�� �`�F�C���̍ŏ��̃C�x���g�Ƃ��Ĕ������܂��B
		''' </summary>
		''' <param name="sender"></param>
		''' <param name="e"></param>
		''' <remarks></remarks>
		Private Sub _BeginRequest(ByVal sender As Object, ByVal e As System.EventArgs)
			''_mylog.Debug("BeginRequest")
		End Sub

		''' <summary>
		''' �Z�L�����e�B ���W���[�������[�U�[�� ID ���m������Ɣ������܂��B
		''' </summary>
		''' <param name="sender"></param>
		''' <param name="e"></param>
		''' <remarks></remarks>
		Private Sub _AuthenticateRequest(ByVal sender As Object, ByVal e As System.EventArgs)
			'_mylog.Debug("AuthenticateRequest")
		End Sub

		''' <summary>
		''' �Z�L�����e�B ���W���[�������[�U�[�� ID ���m������Ɣ������܂��B
		''' </summary>
		''' <param name="sender"></param>
		''' <param name="e"></param>
		''' <remarks></remarks>
		Private Sub _PostAuthenticateRequest(ByVal sender As Object, ByVal e As System.EventArgs)
			'_mylog.Debug("PostAuthenticateRequest")
		End Sub

		''' <summary>
		''' �Z�L�����e�B ���W���[���ɂ���ă��[�U�[�����F�����Ɣ������܂��B
		''' </summary>
		''' <param name="sender"></param>
		''' <param name="e"></param>
		''' <remarks></remarks>
		Private Sub _AuthorizeRequest(ByVal sender As Object, ByVal e As System.EventArgs)
			'_mylog.Debug("AuthorizeRequest")
		End Sub

		''' <summary>
		''' ���݂̗v���̃��[�U�[�����F�����Ɣ������܂��B
		''' </summary>
		''' <param name="sender"></param>
		''' <param name="e"></param>
		''' <remarks></remarks>
		Private Sub _PostAuthorizeRequest(ByVal sender As Object, ByVal e As System.EventArgs)
			'_mylog.Debug("PostAuthorizeRequest")
		End Sub

		''' <summary>
		''' �C�x���g �n���h�� (�y�[�W�܂��� Web �T�[�r�X�Ȃ�) �̎��s���ȗ�����
		''' �L���b�V���O ���W���[���ŃL���b�V������̗v���������ł���悤�ɂ��邽�߂ɁA
		''' ASP.NET �����F�C�x���g�����������Ƃ��ɔ������܂�
		''' </summary>
		''' <param name="sender"></param>
		''' <param name="e"></param>
		''' <remarks></remarks>
		Private Sub _ResolveRequestCache(ByVal sender As Object, ByVal e As System.EventArgs)
			'_mylog.Debug("ResolveRequestCache")
		End Sub

		''' <summary>
		''' ASP.NET �����݂̃C�x���g �n���h���̎��s���ȗ����A
		''' �L���b�V���O ���W���[���ɑ΂��ăL���b�V������̗v���̏������������ꍇ�ɔ������܂��B
		''' </summary>
		''' <param name="sender"></param>
		''' <param name="e"></param>
		''' <remarks></remarks>
		Private Sub _PostResolveRequestCache(ByVal sender As Object, ByVal e As System.EventArgs)
			'_mylog.Debug("PostResolveRequestCache")
		End Sub

		''' <summary>
		''' ASP.NET �����݂̗v����K�؂ȃC�x���g �n���h���Ƀ}�b�v����Ɣ������܂��B
		''' </summary>
		''' <param name="sender"></param>
		''' <param name="e"></param>
		''' <remarks></remarks>
		Private Sub _PostMapRequestHandler(ByVal sender As Object, ByVal e As System.EventArgs)
			'_mylog.Debug("PostMapRequestHandler")
		End Sub

		''' <summary>
		''' ���݂̗v���Ɋ֘A�t����ꂽ���݂̏�� (�Z�b�V������ԂȂ�) �� ASP.NET ���擾����Ɣ������܂��B
		''' </summary>
		''' <param name="sender"></param>
		''' <param name="e"></param>
		''' <remarks></remarks>
		Private Sub _AcquireRequestState(ByVal sender As Object, ByVal e As System.EventArgs)
			_inject(HttpContext.Current.Handler)
		End Sub

		''' <summary>
		''' ���݂̗v���Ɋ֘A�t����ꂽ�v����� (�Z�b�V������ԂȂ�) ���擾�����Ɣ������܂��B
		''' </summary>
		''' <param name="sender"></param>
		''' <param name="e"></param>
		''' <remarks></remarks>
		Private Sub _PostAcquireRequestState(ByVal sender As Object, ByVal e As System.EventArgs)
			'_mylog.Debug("PostAcquireRequestState")
		End Sub

		''' <summary>
		''' ASP.NET ���C�x���g �n���h�� (�y�[�W�AXML Web �T�[�r�X�Ȃ�) �̎��s���J�n���钼�O�ɔ������܂��B
		''' </summary>
		''' <param name="sender"></param>
		''' <param name="e"></param>
		''' <remarks></remarks>
		Private Sub _PreRequestHandlerExecute(ByVal sender As Object, ByVal e As System.EventArgs)
			'_mylog.Debug("PreRequestHandlerExecute")
		End Sub

		''' <summary>
		''' ASP.NET �C�x���g �n���h�� (�y�[�W�AXML Web �T�[�r�X�Ȃ�) �̎��s����������Ɣ������܂��B
		''' </summary>
		''' <param name="sender"></param>
		''' <param name="e"></param>
		''' <remarks></remarks>
		Private Sub _PostRequestHandlerExecute(ByVal sender As Object, ByVal e As System.EventArgs)
			'_mylog.Debug("PostRequestHandlerExecute")
		End Sub

		''' <summary>
		''' ASP.NET �����ׂĂ̗v���C�x���g �n���h���̎��s���I������Ɣ������܂��B
		''' ���̃C�x���g����������ƁA��ԃ��W���[�������݂̏�ԃf�[�^��ۑ����܂��B
		''' </summary>
		''' <param name="sender"></param>
		''' <param name="e"></param>
		''' <remarks></remarks>
		Private Sub _ReleaseRequestState(ByVal sender As Object, ByVal e As System.EventArgs)
			'_mylog.Debug("ReleaseRequestState")
		End Sub

		''' <summary>
		''' ASP.NET �����ׂĂ̗v���C�x���g �n���h���̎��s���������A�v����ԃf�[�^���i�[�����Ɣ������܂��B
		''' </summary>
		''' <param name="sender"></param>
		''' <param name="e"></param>
		''' <remarks></remarks>
		Private Sub _PostReleaseRequestState(ByVal sender As Object, ByVal e As System.EventArgs)
			'_mylog.Debug("PostReleaseRequestState")
		End Sub

		''' <summary>
		''' �L���b�V������̌㑱�̗v�����������邽�߂Ɏg�p���鉞����
		''' �L���b�V���O ���W���[���Ŋi�[�ł���悤�ɂ��邽�߂ɁA
		''' ASP.NET ���C�x���g �n���h���̎��s�����������Ƃ��ɔ������܂��B
		''' </summary>
		''' <param name="sender"></param>
		''' <param name="e"></param>
		''' <remarks></remarks>
		Private Sub _UpdateRequestCache(ByVal sender As Object, ByVal e As System.EventArgs)
			'_mylog.Debug("UpdateRequestCache")
		End Sub

		''' <summary>
		''' ASP.NET ���A�L���b�V���O ���W���[���̍X�V�A����уL���b�V������̌㑱�̗v���̏����Ɏg�p���鉞���̊i�[���I������Ɣ������܂��B
		''' </summary>
		''' <param name="sender"></param>
		''' <param name="e"></param>
		''' <remarks></remarks>
		Private Sub _PostUpdateRequestCache(ByVal sender As Object, ByVal e As System.EventArgs)
			'_mylog.Debug("PostUpdateRequestCache")
		End Sub

		''' <summary>
		''' ASP.NET ���v���ɉ�������Ƃ��ɁA���s�� HTTP �p�C�v���C�� �`�F�C���̍Ō�̃C�x���g�Ƃ��Ĕ������܂��B
		''' </summary>
		''' <param name="sender"></param>
		''' <param name="e"></param>
		''' <remarks></remarks>
		Private Sub _EndRequest(ByVal sender As Object, ByVal e As System.EventArgs)
			_daoDisposeHttpHandler(HttpContext.Current.Handler)
		End Sub

		''' <summary>
		''' Page �� Init �C�x���g���������܂��B
		''' </summary>
		''' <param name="sender"></param>
		''' <param name="e"></param>
		''' <remarks></remarks>
		Private Sub _Page_Init(ByVal sender As Object, ByVal e As System.EventArgs)
			Dim currentPage As Page

			currentPage = TryCast(HttpContext.Current.Handler, Page)
			If currentPage Is Nothing Then
				Exit Sub
			End If

			_inject(currentPage.Master)
		End Sub

#End Region

		''' <summary>
		''' �ˑ����̒���
		''' </summary>
		''' <param name="target"></param>
		''' <remarks>
		''' HttpHandler �̈ˑ�������
		''' </remarks>
		Private Sub _inject(ByVal target As IHttpHandler)
			If target Is Nothing Then
				Exit Sub
			End If

			Dim currentPage As Page

			currentPage = TryCast(target, Page)
			If currentPage Is Nothing Then
				Exit Sub
			End If

			AddHandler currentPage.Init, AddressOf _Page_Init

			_inject(currentPage)
		End Sub

		''' <summary>
		''' �ˑ����̒���
		''' </summary>
		''' <param name="target"></param>
		''' <remarks>
		''' Page �̈ˑ�������
		''' </remarks>
		Private Sub _inject(ByVal target As Page)
			_injector.Inject(target)
		End Sub

		''' <summary>
		''' �ˑ����̒���
		''' </summary>
		''' <param name="target"></param>
		''' <remarks>
		''' MasterPage �̈ˑ�������
		''' </remarks>
		Private Sub _inject(ByVal target As MasterPage)
			If target Is Nothing Then
				Exit Sub
			End If

			_injector.Inject(target)
			_inject(target.Master)
		End Sub

		''' <summary>
		''' DAO �C���X�^���X�̊J��
		''' </summary>
		''' <param name="target"></param>
		''' <remarks>
		''' HttpHandler �� DAO �C���X�^���X�J��
		''' </remarks>
		Private Sub _daoDisposeHttpHandler(ByVal target As IHttpHandler)
			If target Is Nothing Then
				Exit Sub
			End If

			Dim currentPage As Page

			currentPage = TryCast(target, Page)
			If currentPage Is Nothing Then
				Exit Sub
			End If

			_daoDisposeMaster(currentPage.Master)
			_daoDisposePage(currentPage)
		End Sub

		''' <summary>
		''' DAO �C���X�^���X�̊J��
		''' </summary>
		''' <param name="target"></param>
		''' <remarks>
		''' MasterPage �� DAO �C���X�^���X�J��
		''' </remarks>
		Private Sub _daoDisposeMaster(ByVal target As MasterPage)
			_injector.DaoDispose(target)

			If target Is Nothing Then
				Exit Sub
			End If

			_daoDisposeMaster(target.Master)
		End Sub

		''' <summary>
		''' DAO �C���X�^���X�̊J��
		''' </summary>
		''' <param name="target"></param>
		''' <remarks>
		''' Page �� DAO �C���X�^���X�J��
		''' </remarks>
		Private Sub _daoDisposePage(ByVal target As Page)
			_injector.DaoDispose(target)
		End Sub

	End Class

End Namespace
