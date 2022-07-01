#if UNITY_EDITOR
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace HS {
	/// <summary> Inspector�g�����s���N���X��Property�̋��ʉ��p�N���X. </summary>
	public abstract class EditorProperties {

		/// <summary> �Ώۂ�SerializedObject. </summary>
		protected SerializedObject SerializedObject { get; private set; } = null;
		/// <summary> ��x�擾����SerializedPropety�̃L���b�V��. </summary>
		Dictionary<string, SerializedProperty> properties = new Dictionary<string, SerializedProperty>();

		public EditorProperties( SerializedObject serializedObject ) {
			SerializedObject = serializedObject;
		}

		/// <summary> key��n�����ƂŁA���g��SerializedObject��SerializedPropety�ɃA�N�Z�X����. </summary>
		protected SerializedProperty this[ string key ] {
			get {
				SerializedProperty property = null;
				return properties.TryGetValue( key, out property )? property : properties[key] = SerializedObject.FindProperty( key );
			}
		}

		/// <summary> GUI�̕`��. </summary>
		public void OnInspectorGUI() {
			SerializedObject.Update();

			EditorGUILayout.ObjectField( SerializedObject.targetObject, typeof( Component ), true );

			InspectorGUI();

			SerializedObject.ApplyModifiedPropertiesWithoutUndo();
		}

		/// <summary> �e�N���X��p��GUI�̕`��. </summary>
		protected abstract void InspectorGUI();
	}
	/// <summary> Inspector�g�����s���N���X��Property�̋��ʉ��p�N���X. </summary>
	public abstract class EditorProperties<T> : EditorProperties where T : UnityEngine.Object {
		protected T Script { get; set; }

		public EditorProperties( SerializedObject serializedObject ) : base( serializedObject ) {
			Script = serializedObject.targetObject as T;
		}
	}

}
#endif