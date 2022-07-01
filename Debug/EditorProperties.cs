#if UNITY_EDITOR
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace HS {
	/// <summary> Inspector拡張を行うクラスのPropertyの共通化用クラス. </summary>
	public abstract class EditorProperties {

		/// <summary> 対象のSerializedObject. </summary>
		protected SerializedObject SerializedObject { get; private set; } = null;
		/// <summary> 一度取得したSerializedPropetyのキャッシュ. </summary>
		Dictionary<string, SerializedProperty> properties = new Dictionary<string, SerializedProperty>();

		public EditorProperties( SerializedObject serializedObject ) {
			SerializedObject = serializedObject;
		}

		/// <summary> keyを渡すことで、自身のSerializedObjectのSerializedPropetyにアクセスする. </summary>
		protected SerializedProperty this[ string key ] {
			get {
				SerializedProperty property = null;
				return properties.TryGetValue( key, out property )? property : properties[key] = SerializedObject.FindProperty( key );
			}
		}

		/// <summary> GUIの描画. </summary>
		public void OnInspectorGUI() {
			SerializedObject.Update();

			EditorGUILayout.ObjectField( SerializedObject.targetObject, typeof( Component ), true );

			InspectorGUI();

			SerializedObject.ApplyModifiedPropertiesWithoutUndo();
		}

		/// <summary> 各クラス専用のGUIの描画. </summary>
		protected abstract void InspectorGUI();
	}
	/// <summary> Inspector拡張を行うクラスのPropertyの共通化用クラス. </summary>
	public abstract class EditorProperties<T> : EditorProperties where T : UnityEngine.Object {
		protected T Script { get; set; }

		public EditorProperties( SerializedObject serializedObject ) : base( serializedObject ) {
			Script = serializedObject.targetObject as T;
		}
	}

}
#endif