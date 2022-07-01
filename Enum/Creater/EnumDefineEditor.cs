#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;

namespace HS {
	/// <summary> EnumDefine系クラスのInspector用基礎クラス. </summary>
	public abstract class EnumDefineEditorBase<C> : EditorSBase<EnumDefineEditorBase<C>.PropertiesBase, C> where C : Object {
		/// <summary> EnumDefine系クラスのInspector用共通Propertyアクセスkyラス. </summary>
		public class PropertiesBase : EditorProperties {
			SerializedProperty type = null;
			SerializedProperty elements = null;
			SerializedProperty setting = null;

			protected SerializedProperty Type { get { return ( type != null )? type : type = this[nameof( type )]; } }
			protected SerializedProperty Elements { get { return ( elements != null )? elements : elements = this[nameof( elements )]; } }
			protected SerializedProperty Setting { get { return ( setting != null )? setting : setting = this[nameof( setting )]; } }

			GUIContent listContent = null;
			GUIContent settingContent = null;

			public PropertiesBase( SerializedObject serializedObject ) : base( serializedObject ) {
				// TODO: システムメッセージから取得する.
				listContent = new GUIContent() { text = "定義一覧" };
				settingContent = new GUIContent() { text = "出力設定" };
			}

			protected override void InspectorGUI() {
				Type.PropertyGUI();
				Elements.PropertyGUI( listContent );
				Setting.PropertyGUI( settingContent );
			}
		}
	}

	[CustomEditor( typeof( EnumDefine ) )]
	public class EnumDefineEditor : EnumDefineEditorBase<EnumDefine> {

		void OnEnable() {
			ScriptGUI = true;
		}

		protected override PropertiesBase CreateProperties( SerializedObject serializedObject ) {
			return new PropertiesBase( serializedObject );
		}

		protected override void OnScriptGUI( EnumDefine script ) {
			if( !script.IsValidElements() ) {
				script.Reset();
			}
			if( GUILayout.Button( "出力" ) ) script.OutputEnum();
		}
	}
}
#endif