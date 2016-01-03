
Imports Moca.Aop
Imports Moca.Util
Imports Moca.Web

Namespace Di

	''' <summary>
	''' コンテナに格納するHttpを扱うコンポーネント
	''' </summary>
	''' <remarks></remarks>
	Public Class MocaComponent4Http
		Inherits MocaComponent

#Region " コンストラクタ "

		''' <summary>
		''' コンストラクタ
		''' </summary>
		''' <param name="implType">実態の型</param>
		''' <param name="fieldType">フィールドの型</param>
		''' <remarks></remarks>
		Public Sub New(ByVal implType As Type, ByVal fieldType As Type)
			MyBase.New(implType, fieldType)
		End Sub

		''' <summary>
		''' コンストラクタ
		''' </summary>
		''' <param name="key">コンポーネントのキー</param>
		''' <param name="fieldType">フィールドの型</param>
		''' <remarks></remarks>
		Public Sub New(ByVal key As String, ByVal fieldType As Type)
			MyBase.New(key, fieldType)
		End Sub

#End Region

		''' <summary>
		''' オブジェクトをインスタンス化して返します。
		''' </summary>
		''' <param name="target">対象となるページ</param>
		''' <returns></returns>
		''' <remarks></remarks>
        Public Shadows Function Create(ByVal target As Object, ByVal httpContentsType As Type) As Object
            If Aspects.Length = 0 Then
                Return createObject(target)
            End If
            Return createProxyObject(target, httpContentsType)
        End Function

		''' <summary>
		''' オブジェクトをインスタンス化して返します。
		''' </summary>
		''' <param name="target">対象となるページ</param>
		''' <returns></returns>
		''' <remarks></remarks>
        Protected Shadows Function createObject(ByVal target As Object) As Object
            Dim val As Object
            val = ClassUtil.NewInstance(ImplType, New Object() {target})
            Return val
        End Function

		''' <summary>
		''' オブジェクトをプロキシとしてインスタンス化して返します。
		''' </summary>
		''' <param name="target">対象となるページ</param>
		''' <returns></returns>
		''' <remarks>
		''' HttpContents をインスタンス化して FieldType の型に合わせてプロキシを作成してます。
		''' Webではマルチスレッドになるので、Interceptor で対象となる Page を特定する為には、
		''' 対象のセッション上の Page インスタンスが必要となる。
		''' その為、ASP上では Page を取得出来るように必ず、HttpContents を実体化することにした。
		''' </remarks>
        Protected Shadows Function createProxyObject(ByVal target As Object, ByVal httpContentsType As Type) As Object
            Dim val As Object = Nothing
            Dim proxy As AopProxy

            val = ClassUtil.NewInstance(httpContentsType, New Object() {target})
            proxy = New AopProxy(FieldType, Aspects, val)
            val = proxy.Create()
            Return val
        End Function

	End Class

End Namespace
