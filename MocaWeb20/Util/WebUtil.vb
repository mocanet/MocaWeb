
Imports System.Reflection

Imports Moca.Web
Imports Moca.Web.Attr

Namespace Util

	''' <summary>
	''' Web用ユーティリティ
	''' </summary>
	''' <remarks></remarks>
	Public Class WebUtil

		''' <summary>一度解析したEntityを格納しておく</summary>
		Private _toQueryStringEntityTypes As New Dictionary(Of Type, Dictionary(Of String, String))

		''' <summary>
		''' 指定されたプロパティからクエリー文字列を作成する。
		''' </summary>
		''' <param name="values"></param>
		''' <returns></returns>
		''' <remarks>
		''' 実装出来そうで出来ないメソッド。
		''' 透過プロキシインスタンスから型の情報がうまく取れないため。
		''' </remarks>
		<Obsolete("No Support Method")> _
		Public Function ToQueryString(ByVal values As Object) As String
			Dim sb As StringBuilder
			Dim map As Hashtable

			sb = New StringBuilder

			Try
				map = DirectCast(GetType(HttpContents).InvokeMember("QueryStringMap", BindingFlags.GetProperty Or BindingFlags.Public Or BindingFlags.NonPublic Or BindingFlags.Instance, Nothing, values, New Object() {}), Hashtable)
			Catch ex As Exception
				Throw New NotSupportedException("指定されたオブジェクトに QueryString 属性が指定されていません。")
			End Try

			For Each key As String In map.Keys
				If sb.Length > 0 Then
					sb.Append(",")
				End If
				sb.AppendFormat("{0}={1}", key, map.Item(key))
			Next

			Return String.Empty
		End Function

		''' <summary>
		''' 指定されたプロパティからクエリー文字列を作成する。
		''' </summary>
		''' <param name="values">クエリー値を保持したオブジェクト</param>
		''' <returns></returns>
		''' <remarks></remarks>
		Public Function ToQueryString(Of T)(ByVal values As Object, Optional ByVal questionMark As Boolean = True) As String
			Dim sb As StringBuilder
			Dim names As Dictionary(Of String, String)

			sb = New StringBuilder

			names = _analyzeQueryStringNameAttribute(GetType(T))
			For Each pname As String In names.Keys
				Dim name As String
				name = names(pname)
				If sb.Length > 0 Then
					sb.Append("&")
				End If
				Dim val As String
				val = GetType(T).InvokeMember(pname, BindingFlags.GetProperty Or BindingFlags.Public Or BindingFlags.NonPublic Or BindingFlags.Instance, Nothing, values, New Object() {}).ToString
				sb.AppendFormat("{0}={1}", name, HttpUtility.UrlEncode(val))
			Next

			Return CStr(IIf(questionMark, "?", String.Empty)) & sb.ToString
		End Function

		Private Function _analyzeQueryStringNameAttribute(ByVal typ As Type) As Dictionary(Of String, String)
			Dim lst As Dictionary(Of String, String)

			lst = Nothing
			If _toQueryStringEntityTypes.TryGetValue(typ, lst) Then
				Return lst
			End If

			Dim props() As PropertyInfo

			lst = New Dictionary(Of String, String)
			_toQueryStringEntityTypes.Add(typ, lst)

			props = ClassUtil.GetProperties(typ)
			For Each prop As PropertyInfo In props
				If Not prop.CanRead Then
					Continue For
				End If

				Dim name As String
				Dim attr As QueryStringNameAttribute

				name = prop.Name
				attr = ClassUtil.GetCustomAttribute(Of QueryStringNameAttribute)(prop)
				If attr IsNot Nothing Then
					name = attr.Name
				End If

				lst.Add(prop.Name, name)
			Next

			Return lst
		End Function

		Public Function GetRequestConstantDataSet(ByVal request As HttpRequest) As Moca.ConstantDataSet.ConstantDataTable
			Dim dt As Moca.ConstantDataSet.ConstantDataTable

			dt = VBUtil.CreateConstantDataSet("property", False).Constant

			dt.AddRow("ApplicationPath", request.ApplicationPath)
			dt.AddRow("AppRelativeCurrentExecutionFilePath", request.AppRelativeCurrentExecutionFilePath)
			dt.AddRow("Browser.Browser", request.Browser.Browser)
			dt.AddRow("ContentLength", request.ContentLength)
			dt.AddRow("ContentType", request.ContentType)
			dt.AddRow("CurrentExecutionFilePath", request.CurrentExecutionFilePath)
			dt.AddRow("FilePath", request.FilePath)
			dt.AddRow("HttpMethod", request.HttpMethod)
			dt.AddRow("IsAuthenticated", request.IsAuthenticated.ToString)
			dt.AddRow("IsLocal", request.IsLocal.ToString)
			dt.AddRow("IsSecureConnection", request.IsSecureConnection.ToString)
			dt.AddRow("Path", request.Path)
			dt.AddRow("PathInfo", request.PathInfo)
			dt.AddRow("PhysicalApplicationPath", request.PhysicalApplicationPath)
			dt.AddRow("PhysicalPath", request.PhysicalPath)
			dt.AddRow("RawUrl", request.RawUrl)
			dt.AddRow("RequestType", request.RequestType)
			dt.AddRow("TotalBytes", request.TotalBytes)
			dt.AddRow("UserHostAddress", request.UserHostAddress)

			Return dt
		End Function

		Public Function GetRequestUrlConstantDataSet(ByVal request As HttpRequest) As Moca.ConstantDataSet.ConstantDataTable
			Dim dt As Moca.ConstantDataSet.ConstantDataTable

			dt = VBUtil.CreateConstantDataSet("property", False).Constant

			dt.AddRow("AbsolutePath", request.Url.AbsolutePath)
			dt.AddRow("AbsoluteUri", request.Url.AbsoluteUri)
			dt.AddRow("Authority", request.Url.Authority)
			dt.AddRow("DnsSafeHost", request.Url.DnsSafeHost)
			dt.AddRow("Fragment", request.Url.Fragment)
			dt.AddRow("Host", request.Url.Host)
			dt.AddRow("HostNameType", request.Url.HostNameType)
			dt.AddRow("IsAbsoluteUri", request.Url.IsAbsoluteUri.ToString)
			dt.AddRow("IsDefaultPort", request.Url.IsDefaultPort.ToString)
			dt.AddRow("IsFile", request.Url.IsFile.ToString)
			dt.AddRow("IsLoopback", request.Url.IsLoopback.ToString)
			dt.AddRow("IsUnc", request.Url.IsUnc.ToString)
			dt.AddRow("LocalPath", request.Url.LocalPath)
			dt.AddRow("OriginalString", request.Url.OriginalString)
			dt.AddRow("PathAndQuery", request.Url.PathAndQuery)
			dt.AddRow("Port", request.Url.Port)
			dt.AddRow("Query", request.Url.Query)
			dt.AddRow("Scheme", request.Url.Scheme)
			dt.AddRow("Segments", request.Url.Segments.ToString)
			dt.AddRow("UserEscaped", request.Url.UserEscaped.ToString)
			dt.AddRow("UserInfo", request.Url.UserInfo)

			Return dt
		End Function

		Public Function GetRequestUrlReferrerConstantDataSet(ByVal request As HttpRequest) As Moca.ConstantDataSet.ConstantDataTable
			Dim dt As Moca.ConstantDataSet.ConstantDataTable

			If request.UrlReferrer Is Nothing Then
				Return Nothing
			End If

			dt = VBUtil.CreateConstantDataSet("property", False).Constant

			dt.AddRow("AbsolutePath", request.UrlReferrer.AbsolutePath)
			dt.AddRow("AbsoluteUri", request.UrlReferrer.AbsoluteUri)
			dt.AddRow("Authority", request.UrlReferrer.Authority)
			dt.AddRow("DnsSafeHost", request.UrlReferrer.DnsSafeHost)
			dt.AddRow("Fragment", request.UrlReferrer.Fragment)
			dt.AddRow("Host", request.UrlReferrer.Host)
			dt.AddRow("LocalPath", request.UrlReferrer.LocalPath)
			dt.AddRow("OriginalString", request.UrlReferrer.OriginalString)
			dt.AddRow("PathAndQuery", request.UrlReferrer.PathAndQuery)
			dt.AddRow("Port", request.UrlReferrer.Port)
			dt.AddRow("Query", request.UrlReferrer.Query)
			dt.AddRow("Scheme", request.UrlReferrer.Scheme)
			dt.AddRow("UserInfo", request.UrlReferrer.UserInfo)

			Return dt
		End Function

		Public Function GetSessionConstantDataSet(ByVal session As SessionState.HttpSessionState) As Moca.ConstantDataSet.ConstantDataTable
			Dim dt As Moca.ConstantDataSet.ConstantDataTable

			dt = VBUtil.CreateConstantDataSet("property", False).Constant

			Dim enume As IEnumerator = session.GetEnumerator()
			While enume.MoveNext
				dt.AddRow(enume.Current.ToString, session(enume.Current.ToString).ToString)
			End While

			Return dt
		End Function

		Public Function GetServerVariablesConstantDataSet(ByVal serverVariables As IServerVariables) As Moca.ConstantDataSet.ConstantDataTable
			Dim dt As Moca.ConstantDataSet.ConstantDataTable

			dt = VBUtil.CreateConstantDataSet("property", False).Constant

			dt.AddRow("AllHttp", serverVariables.AllHttp)
			dt.AddRow("AllRaw", serverVariables.AllRaw)
			dt.AddRow("AuthPassword", serverVariables.AuthPassword)
			dt.AddRow("AuthType", serverVariables.AuthType)
			dt.AddRow("AuthUser", serverVariables.AuthUser)
			dt.AddRow("CertCookie", serverVariables.CertCookie)
			dt.AddRow("HttpAccept", serverVariables.HttpAccept)
			dt.AddRow("HttpCookie", serverVariables.HttpCookie)
			dt.AddRow("HttpReferer", serverVariables.HttpReferer)
			dt.AddRow("Https", serverVariables.Https)
			dt.AddRow("HttpUserAgent", serverVariables.HttpUserAgent)
			dt.AddRow("LocalAddr", serverVariables.LocalAddr)
			dt.AddRow("LogonUser", serverVariables.LogonUser)
			dt.AddRow("QueryString", serverVariables.QueryString)
			dt.AddRow("RemoteAddr", serverVariables.RemoteAddr)
			dt.AddRow("RemoteHost", serverVariables.RemoteHost)
			dt.AddRow("RemoteUser", serverVariables.RemoteUser)
			dt.AddRow("RequestMethod", serverVariables.RequestMethod)
			dt.AddRow("ServerPort", serverVariables.ServerPort)
			dt.AddRow("ServerProtocol", serverVariables.ServerProtocol)
			dt.AddRow("ServerSoftware", serverVariables.ServerSoftware)
			dt.AddRow("Url", serverVariables.Url)

			Return dt
		End Function

	End Class

End Namespace
