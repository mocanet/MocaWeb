
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
	''' モジュールの初期化イベントおよび破棄イベントの共通処理
	''' </summary>
	''' <remarks>
	''' web.config ファイルへ下記を追記してください。<br/>
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

		''' <summary>ページに対しての依存性注入</summary>
		Private _injector As MocaWebInjector

		''' <summary>Logging For Log4net</summary>
		Private ReadOnly _mylog As log4net.ILog = log4net.LogManager.GetLogger(String.Empty)

#Region " コンストラクタ／デコンストラクタ "

		''' <summary>
		''' デフォルトコンストラクタ
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
		''' モジュールを初期化し、要求を処理できるように準備します。
		''' </summary>
		''' <param name="context"></param>
		''' <remarks></remarks>
		Public Sub Init(ByVal context As System.Web.HttpApplication) Implements System.Web.IHttpModule.Init
			' log4net 準備
			Dim rootPath As String = context.Context.Server.MapPath("~")
			Dim log4netConfig As String = System.IO.Path.Combine(rootPath, "log4net.config")
			log4net.Config.XmlConfigurator.Configure(New System.IO.FileInfo(log4netConfig))

			' コンテナ 準備
			_injector = New MocaWebInjector()

			' ハンドラー 準備
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
			' イベント ハンドラが実行されます。
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
		''' ASP.NET が要求に応答するときに、実行の HTTP パイプライン チェインの最初のイベントとして発生します。
		''' </summary>
		''' <param name="sender"></param>
		''' <param name="e"></param>
		''' <remarks></remarks>
		Private Sub _BeginRequest(ByVal sender As Object, ByVal e As System.EventArgs)
			''_mylog.Debug("BeginRequest")
		End Sub

		''' <summary>
		''' セキュリティ モジュールがユーザーの ID を確立すると発生します。
		''' </summary>
		''' <param name="sender"></param>
		''' <param name="e"></param>
		''' <remarks></remarks>
		Private Sub _AuthenticateRequest(ByVal sender As Object, ByVal e As System.EventArgs)
			'_mylog.Debug("AuthenticateRequest")
		End Sub

		''' <summary>
		''' セキュリティ モジュールがユーザーの ID を確立すると発生します。
		''' </summary>
		''' <param name="sender"></param>
		''' <param name="e"></param>
		''' <remarks></remarks>
		Private Sub _PostAuthenticateRequest(ByVal sender As Object, ByVal e As System.EventArgs)
			'_mylog.Debug("PostAuthenticateRequest")
		End Sub

		''' <summary>
		''' セキュリティ モジュールによってユーザーが承認されると発生します。
		''' </summary>
		''' <param name="sender"></param>
		''' <param name="e"></param>
		''' <remarks></remarks>
		Private Sub _AuthorizeRequest(ByVal sender As Object, ByVal e As System.EventArgs)
			'_mylog.Debug("AuthorizeRequest")
		End Sub

		''' <summary>
		''' 現在の要求のユーザーが承認されると発生します。
		''' </summary>
		''' <param name="sender"></param>
		''' <param name="e"></param>
		''' <remarks></remarks>
		Private Sub _PostAuthorizeRequest(ByVal sender As Object, ByVal e As System.EventArgs)
			'_mylog.Debug("PostAuthorizeRequest")
		End Sub

		''' <summary>
		''' イベント ハンドラ (ページまたは Web サービスなど) の実行を省略して
		''' キャッシング モジュールでキャッシュからの要求を処理できるようにするために、
		''' ASP.NET が承認イベントを完了したときに発生します
		''' </summary>
		''' <param name="sender"></param>
		''' <param name="e"></param>
		''' <remarks></remarks>
		Private Sub _ResolveRequestCache(ByVal sender As Object, ByVal e As System.EventArgs)
			'_mylog.Debug("ResolveRequestCache")
		End Sub

		''' <summary>
		''' ASP.NET が現在のイベント ハンドラの実行を省略し、
		''' キャッシング モジュールに対してキャッシュからの要求の処理を許可した場合に発生します。
		''' </summary>
		''' <param name="sender"></param>
		''' <param name="e"></param>
		''' <remarks></remarks>
		Private Sub _PostResolveRequestCache(ByVal sender As Object, ByVal e As System.EventArgs)
			'_mylog.Debug("PostResolveRequestCache")
		End Sub

		''' <summary>
		''' ASP.NET が現在の要求を適切なイベント ハンドラにマップすると発生します。
		''' </summary>
		''' <param name="sender"></param>
		''' <param name="e"></param>
		''' <remarks></remarks>
		Private Sub _PostMapRequestHandler(ByVal sender As Object, ByVal e As System.EventArgs)
			'_mylog.Debug("PostMapRequestHandler")
		End Sub

		''' <summary>
		''' 現在の要求に関連付けられた現在の状態 (セッション状態など) を ASP.NET が取得すると発生します。
		''' </summary>
		''' <param name="sender"></param>
		''' <param name="e"></param>
		''' <remarks></remarks>
		Private Sub _AcquireRequestState(ByVal sender As Object, ByVal e As System.EventArgs)
			_inject(HttpContext.Current.Handler)
		End Sub

		''' <summary>
		''' 現在の要求に関連付けられた要求状態 (セッション状態など) が取得されると発生します。
		''' </summary>
		''' <param name="sender"></param>
		''' <param name="e"></param>
		''' <remarks></remarks>
		Private Sub _PostAcquireRequestState(ByVal sender As Object, ByVal e As System.EventArgs)
			'_mylog.Debug("PostAcquireRequestState")
		End Sub

		''' <summary>
		''' ASP.NET がイベント ハンドラ (ページ、XML Web サービスなど) の実行を開始する直前に発生します。
		''' </summary>
		''' <param name="sender"></param>
		''' <param name="e"></param>
		''' <remarks></remarks>
		Private Sub _PreRequestHandlerExecute(ByVal sender As Object, ByVal e As System.EventArgs)
			'_mylog.Debug("PreRequestHandlerExecute")
		End Sub

		''' <summary>
		''' ASP.NET イベント ハンドラ (ページ、XML Web サービスなど) の実行が完了すると発生します。
		''' </summary>
		''' <param name="sender"></param>
		''' <param name="e"></param>
		''' <remarks></remarks>
		Private Sub _PostRequestHandlerExecute(ByVal sender As Object, ByVal e As System.EventArgs)
			'_mylog.Debug("PostRequestHandlerExecute")
		End Sub

		''' <summary>
		''' ASP.NET がすべての要求イベント ハンドラの実行を終了すると発生します。
		''' このイベントが発生すると、状態モジュールが現在の状態データを保存します。
		''' </summary>
		''' <param name="sender"></param>
		''' <param name="e"></param>
		''' <remarks></remarks>
		Private Sub _ReleaseRequestState(ByVal sender As Object, ByVal e As System.EventArgs)
			'_mylog.Debug("ReleaseRequestState")
		End Sub

		''' <summary>
		''' ASP.NET がすべての要求イベント ハンドラの実行を完了し、要求状態データが格納されると発生します。
		''' </summary>
		''' <param name="sender"></param>
		''' <param name="e"></param>
		''' <remarks></remarks>
		Private Sub _PostReleaseRequestState(ByVal sender As Object, ByVal e As System.EventArgs)
			'_mylog.Debug("PostReleaseRequestState")
		End Sub

		''' <summary>
		''' キャッシュからの後続の要求を処理するために使用する応答を
		''' キャッシング モジュールで格納できるようにするために、
		''' ASP.NET がイベント ハンドラの実行を完了したときに発生します。
		''' </summary>
		''' <param name="sender"></param>
		''' <param name="e"></param>
		''' <remarks></remarks>
		Private Sub _UpdateRequestCache(ByVal sender As Object, ByVal e As System.EventArgs)
			'_mylog.Debug("UpdateRequestCache")
		End Sub

		''' <summary>
		''' ASP.NET が、キャッシング モジュールの更新、およびキャッシュからの後続の要求の処理に使用する応答の格納を終了すると発生します。
		''' </summary>
		''' <param name="sender"></param>
		''' <param name="e"></param>
		''' <remarks></remarks>
		Private Sub _PostUpdateRequestCache(ByVal sender As Object, ByVal e As System.EventArgs)
			'_mylog.Debug("PostUpdateRequestCache")
		End Sub

		''' <summary>
		''' ASP.NET が要求に応答するときに、実行の HTTP パイプライン チェインの最後のイベントとして発生します。
		''' </summary>
		''' <param name="sender"></param>
		''' <param name="e"></param>
		''' <remarks></remarks>
		Private Sub _EndRequest(ByVal sender As Object, ByVal e As System.EventArgs)
			_daoDisposeHttpHandler(HttpContext.Current.Handler)
		End Sub

		''' <summary>
		''' Page の Init イベントが発生します。
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
		''' 依存性の注入
		''' </summary>
		''' <param name="target"></param>
		''' <remarks>
		''' HttpHandler の依存性注入
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
		''' 依存性の注入
		''' </summary>
		''' <param name="target"></param>
		''' <remarks>
		''' Page の依存性注入
		''' </remarks>
		Private Sub _inject(ByVal target As Page)
			_injector.Inject(target)
		End Sub

		''' <summary>
		''' 依存性の注入
		''' </summary>
		''' <param name="target"></param>
		''' <remarks>
		''' MasterPage の依存性注入
		''' </remarks>
		Private Sub _inject(ByVal target As MasterPage)
			If target Is Nothing Then
				Exit Sub
			End If

			_injector.Inject(target)
			_inject(target.Master)
		End Sub

		''' <summary>
		''' DAO インスタンスの開放
		''' </summary>
		''' <param name="target"></param>
		''' <remarks>
		''' HttpHandler の DAO インスタンス開放
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
		''' DAO インスタンスの開放
		''' </summary>
		''' <param name="target"></param>
		''' <remarks>
		''' MasterPage の DAO インスタンス開放
		''' </remarks>
		Private Sub _daoDisposeMaster(ByVal target As MasterPage)
			_injector.DaoDispose(target)

			If target Is Nothing Then
				Exit Sub
			End If

			_daoDisposeMaster(target.Master)
		End Sub

		''' <summary>
		''' DAO インスタンスの開放
		''' </summary>
		''' <param name="target"></param>
		''' <remarks>
		''' Page の DAO インスタンス開放
		''' </remarks>
		Private Sub _daoDisposePage(ByVal target As Page)
			_injector.DaoDispose(target)
		End Sub

	End Class

End Namespace
