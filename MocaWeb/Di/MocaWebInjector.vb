
Imports Moca.Attr
Imports Moca.Di
Imports Moca.Util
Imports Moca.Web.Attr

Namespace Di

	''' <summary>
	''' ページに対しての依存性注入
	''' </summary>
	''' <remarks></remarks>
	Public Class MocaWebInjector
		Inherits MocaInjector

#Region " コンストラクタ "

		''' <summary>
		''' デフォルトコンストラクタ
		''' </summary>
		''' <remarks></remarks>
		Public Sub New()
			MyBase.New()

            Me.Analyzer.FieldInject = AddressOf Me.fieldInject
		End Sub

#End Region

		''' <summary>
		''' フィールドへインスタンスの注入
		''' </summary>
		''' <param name="target">対象となるオブジェクト</param>
		''' <param name="field">対象となるフィールド</param>
		''' <param name="component">対象となるコンポーネント</param>
		''' <returns>生成したインスタンス</returns>
		''' <remarks>
		''' MocaComponent4Http として扱いたいためオーバーライド
		''' </remarks>
		Protected Shadows Function fieldInject(ByVal target As Object, ByVal field As System.Reflection.FieldInfo, ByVal component As MocaComponent) As Object
			Dim instance As Object
			Dim componentHttp As MocaComponent4Http

			componentHttp = TryCast(component, MocaComponent4Http)
			If componentHttp Is Nothing Then
				instance = component.Create()
				ClassUtil.Inject(target, field, New Object() {instance})
				Return instance
			End If

			Dim typ As Type

			If TypeOf target Is System.Web.HttpApplication Then
				typ = GetType(Web.HttpContentsApplication)
			Else
				typ = GetType(Web.HttpContents)
			End If

			instance = componentHttp.Create(target, typ)
			ClassUtil.Inject(target, field, New Object() {instance})
			Return instance
		End Function

	End Class

End Namespace
