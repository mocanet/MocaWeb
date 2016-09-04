
Namespace Web

	''' <summary>
	''' Http時のインタセプターで使用するコンテンツインタフェース
	''' </summary>
	''' <remarks></remarks>
	Public Interface IHttpContents

		''' <summary>
		''' 対象となるオブジェクト
		''' </summary>
		''' <value></value>
		''' <returns></returns>
		''' <remarks></remarks>
		ReadOnly Property Target() As Object

		''' <summary>
		''' アプリケーションプロパティ
		''' </summary>
		''' <value></value>
		''' <returns></returns>
		''' <remarks>
		''' Page 又は、MasterPage のどちらかにオブジェクトが存在するときは、存在しているオブジェクトの Application を返す。
		''' 両方にオブジェクトが存在しているときは、現状ではありえない。
		''' </remarks>
		ReadOnly Property Application() As HttpApplicationState

		''' <summary>
		''' リクエストプロパティ
		''' </summary>
		''' <value></value>
		''' <returns></returns>
		''' <remarks>
		''' Page 又は、MasterPage のどちらかにオブジェクトが存在するときは、存在しているオブジェクトの Request を返す。
		''' 両方にオブジェクトが存在しているときは、現状ではありえない。
		''' </remarks>
		ReadOnly Property Request() As HttpRequest

		''' <summary>
		''' レスポンスプロパティ
		''' </summary>
		''' <value></value>
		''' <returns></returns>
		''' <remarks>
		''' Page 又は、MasterPage のどちらかにオブジェクトが存在するときは、存在しているオブジェクトの Response を返す。
		''' 両方にオブジェクトが存在しているときは、現状ではありえない。
		''' </remarks>
		ReadOnly Property Response() As HttpResponse

		''' <summary>
		''' セッションプロパティ
		''' </summary>
		''' <value></value>
		''' <returns></returns>
		''' <remarks>
		''' Page 又は、MasterPage のどちらかにオブジェクトが存在するときは、存在しているオブジェクトの Session を返す。
		''' 両方にオブジェクトが存在しているときは、現状ではありえない。
		''' </remarks>
		ReadOnly Property Session() As HttpSessionState

		''' <summary>
		''' クエリー内容を一時的に保存する為のプロパティ
		''' </summary>
		''' <value></value>
		''' <returns></returns>
		''' <remarks>
		''' 新しい値でクエリー文字列を作成するときなどのため。
		''' </remarks>
		ReadOnly Property QueryStringMap() As Hashtable

	End Interface

End Namespace
