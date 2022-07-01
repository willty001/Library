#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;

namespace HS {
	/// <summary> EnumDefine�n�N���X��Inspector�p��b�N���X. </summary>
	public abstract class EnumDefineEditorBase<C> : EditorSBase<EnumDefineEditorBase<C>.PropertiesBase, C> where C : Object {
		/// <summary> EnumDefine�n�N���X��Inspector�p����Property�A�N�Z�Xky���X. </summary>
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
				// TODO: �V�X�e�����b�Z�[�W����擾����.
				listContent = new GUIContent() { text = "��`�ꗗ" };
				settingContent = new GUIContent() { text = "�o�͐ݒ�" };
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
			if( GUILayout.Button( "�o��" ) ) script.OutputEnum();
		}
	}
}
#endif