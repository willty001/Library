#if UNITY_EDITOR
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace HS {
	/// <summary> Inspector拡張を行うSerializableクラスのPropertyの共通化用クラス. </summary>
	public abstract class PropertyDrawerProperties {
		/// <summary> 標準1行幅. </summary>
		public static readonly float LineHeight = EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;

		/// <summary> 初期x. </summary>
		public float X { get; protected set; } = 0f;
		/// <summary> 初期y. </summary>
		public float Y { get; protected set; } = 0f;

		/// <summary> 描画幅. </summary>
		public float Width { get; protected set; } = 0f;
		/// <summary> 描画高. </summary>
		public float Height { get; protected set; } = 0f;

		/// <summary> 行間の固定長の使用量. </summary>
		float usedFixedWidth = 0f;
		EditorGUI.IndentLevelScope NoIndent { get; set; } = null;

		/// <summary> 対象のSerializedProperty. </summary>
		protected SerializedProperty SerializedProperty { get; private set; } = null;
		/// <summary> 一度取得したSerializedPropetyのキャッシュ. </summary>
		Dictionary<string, SerializedProperty> properties = new Dictionary<string, SerializedProperty>();

		public PropertyDrawerProperties( SerializedProperty serializedProperty ) {
			SerializedProperty = serializedProperty;
		}

		/// <summary> keyを渡すことで、自身のSerializedObjectのSerializedPropetyにアクセスする. </summary>
		protected SerializedProperty this[ string key ] {
			get {
				SerializedProperty property = null;
				return properties.TryGetValue( key, out property )? property : properties[key] = SerializedProperty.FindPropertyRelative( key );
			}
		}

		/// <summary> GUIの描画. </summary>
		public void OnGUI( ref Rect position, GUIContent label ) {
			X = position.x;
			Y = position.y;
			Width = position.width;
			Height = 0;
			position.height = LineHeight;
			using( new EditorGUI.PropertyScope( position, label, SerializedProperty ) ) {
				GUI( ref position, label );
			}
			NextLine( ref position );
		}

		/// <summary> 各クラス専用のGUIの描画. </summary>
		protected abstract void GUI( ref Rect position, GUIContent label );

		/// <summary> 幅の固定使用長を設定する. </summary>
		protected void SetColumnUse( float pixel ) {
			usedFixedWidth = pixel;
		}
		/// <summary> 幅の指定値を設定する. </summary>
		protected void SetColumn( ref Rect position, float pixel ) {
			position.width = pixel;
		}

		/// <summary> 幅の割合を設定する. </summary>
		protected void SetColumnRate( ref Rect position, float rate ) {
			position.width = ( Width - usedFixedWidth ) * rate;
		}

		/// <summary> GUIの次の項目に移動. </summary>
		protected void NextColumn( ref Rect position ) {
			position.x += position.width;
			if( NoIndent != null ) NoIndent = new EditorGUI.IndentLevelScope( -EditorGUI.indentLevel );
		}

		/// <summary> GUIの改行. </summary>
		protected void NextLine( ref Rect position ) {
			position.y += position.height;
			Height += position.height;
			position.x = X;
			usedFixedWidth = 0;
			NoIndentDispose();
		}
		/// <summary> インデント向こうの解除. </summary>
		void NoIndentDispose() {
			if( NoIndent != null ) {
				NoIndent.Dispose();
				NoIndent = null;
			}
		}
	}
}
#endif