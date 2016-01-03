
Imports Moca.Web.Attr

Namespace Web

	''' <summary>
	''' Web サーバー変数コレクションエンティティ
	''' </summary>
	''' <remarks></remarks>
	<ServerVariables()> _
	Public Interface IServerVariables

		<ServerVariableName("ALL_HTTP")> _
		ReadOnly Property AllHttp() As String

		<ServerVariableName("ALL_RAW")> _
		ReadOnly Property AllRaw() As String

		<ServerVariableName("AUTH_PASSWORD")> _
		ReadOnly Property AuthPassword() As String

		<ServerVariableName("AUTH_TYPE")> _
		ReadOnly Property AuthType() As String

		<ServerVariableName("AUTH_USER")> _
		ReadOnly Property AuthUser() As String

		<ServerVariableName("CERT_COOKIE")> _
		ReadOnly Property CertCookie() As String

		<ServerVariableName("HTTP_ACCEPT")> _
		ReadOnly Property HttpAccept() As String

		<ServerVariableName("HTTP_USER_AGENT")> _
		ReadOnly Property HttpUserAgent() As String

		<ServerVariableName("HTTP_COOKIE")> _
		ReadOnly Property HttpCookie() As String

		<ServerVariableName("HTTP_REFERER")> _
		ReadOnly Property HttpReferer() As String

		<ServerVariableName("HTTPS")> _
		ReadOnly Property Https() As String

		<ServerVariableName("LOCAL_ADDR")> _
		ReadOnly Property LocalAddr() As String

		<ServerVariableName("LOGON_USER")> _
		ReadOnly Property LogonUser() As String

		<ServerVariableName("QUERY_STRING")> _
		ReadOnly Property QueryString() As String

		<ServerVariableName("REMOTE_ADDR")> _
		ReadOnly Property RemoteAddr() As String

		<ServerVariableName("REMOTE_HOST")> _
		ReadOnly Property RemoteHost() As String

		<ServerVariableName("REMOTE_USER")> _
		ReadOnly Property RemoteUser() As String

		<ServerVariableName("REQUEST_METHOD")> _
		ReadOnly Property RequestMethod() As String

		<ServerVariableName("SERVER_PORT")> _
		ReadOnly Property ServerPort() As String

		<ServerVariableName("SERVER_PROTOCOL")> _
		ReadOnly Property ServerProtocol() As String

		<ServerVariableName("SERVER_SOFTWARE")> _
		ReadOnly Property ServerSoftware() As String

		<ServerVariableName("URL")> _
		ReadOnly Property Url() As String

	End Interface

End Namespace
