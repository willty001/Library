#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace HS {
	public abstract class EditorBase : Editor {
		protected bool DebugGUI { get; set; } = false;

		public override void OnInspectorGUI() {
			base.OnInspectorGUI();

			if( DebugGUI ) OnDebugGUI();
		}

		protected virtual void OnDebugGUI() { }
	}
	public abstract class EditorBase<T> : Editor where T : EditorProperties {
		protected abstract T CreateProperties( SerializedObject serializedObject );

		T properties = null;

		protected T Properties { get { return ( properties != null )? properties : properties = CreateProperties( serializedObject ); } }
		protected bool DebugGUI { get; set; } = false;

		public override void OnInspectorGUI() {
			Properties.OnInspectorGUI();

			if( DebugGUI ) OnDebugGUI();
		}

		protected virtual void OnDebugGUI() {}
	}
	public abstract class EditorSBase<C> : Editor where C : Object {
		C script = null;

		protected C Script { get { return ( script != null ) ? script : script = target as C; } }
		protected bool ScriptGUI { get; set; } = true;

		public override void OnInspectorGUI() {
			base.OnInspectorGUI();
			if( ScriptGUI = EditorGUILayout.Foldout( ScriptGUI, "ScriptèÓïÒ" ) ) {
				using( new EditorGUI.IndentLevelScope() ) {
					OnScriptGUI( Script );
				}
			}
		}

		protected virtual void OnScriptGUI( C script ) { }
	}
	public abstract class EditorSBase<T, C> : EditorBase<T> where T : EditorProperties where C : Object {
		C script = null;

		protected C Script { get { return ( script != null ) ? script : script = target as C; } }
		protected bool ScriptGUI { get; set; } = true;

		public override void OnInspectorGUI() {
			base.OnInspectorGUI();
			if( ScriptGUI = EditorGUILayout.Foldout( ScriptGUI, "ScriptèÓïÒ" ) ) {
				using( new EditorGUI.IndentLevelScope() ) {
					OnScriptGUI( Script );
				}
			}
		}

		protected virtual void OnScriptGUI( C script ) { }
	}
}
#endif