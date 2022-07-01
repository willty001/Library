#if UNITY_EDITOR
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace HS {
	/// <summary> Inspector�g�����s��Serializable�N���X��Property�̋��ʉ��p�N���X. </summary>
	public abstract class PropertyDrawerProperties {
		/// <summary> �W��1�s��. </summary>
		public static readonly float LineHeight = EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;

		/// <summary> ����x. </summary>
		public float X { get; protected set; } = 0f;
		/// <summary> ����y. </summary>
		public float Y { get; protected set; } = 0f;

		/// <summary> �`�敝. </summary>
		public float Width { get; protected set; } = 0f;
		/// <summary> �`�捂. </summary>
		public float Height { get; protected set; } = 0f;

		/// <summary> �s�Ԃ̌Œ蒷�̎g�p��. </summary>
		float usedFixedWidth = 0f;
		EditorGUI.IndentLevelScope NoIndent { get; set; } = null;

		/// <summary> �Ώۂ�SerializedProperty. </summary>
		protected SerializedProperty SerializedProperty { get; private set; } = null;
		/// <summary> ��x�擾����SerializedPropety�̃L���b�V��. </summary>
		Dictionary<string, SerializedProperty> properties = new Dictionary<string, SerializedProperty>();

		public PropertyDrawerProperties( SerializedProperty serializedProperty ) {
			SerializedProperty = serializedProperty;
		}

		/// <summary> key��n�����ƂŁA���g��SerializedObject��SerializedPropety�ɃA�N�Z�X����. </summary>
		protected SerializedProperty this[ string key ] {
			get {
				SerializedProperty property = null;
				return properties.TryGetValue( key, out property )? property : properties[key] = SerializedProperty.FindPropertyRelative( key );
			}
		}

		/// <summary> GUI�̕`��. </summary>
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

		/// <summary> �e�N���X��p��GUI�̕`��. </summary>
		protected abstract void GUI( ref Rect position, GUIContent label );

		/// <summary> ���̌Œ�g�p����ݒ肷��. </summary>
		protected void SetColumnUse( float pixel ) {
			usedFixedWidth = pixel;
		}
		/// <summary> ���̎w��l��ݒ肷��. </summary>
		protected void SetColumn( ref Rect position, float pixel ) {
			position.width = pixel;
		}

		/// <summary> ���̊�����ݒ肷��. </summary>
		protected void SetColumnRate( ref Rect position, float rate ) {
			position.width = ( Width - usedFixedWidth ) * rate;
		}

		/// <summary> GUI�̎��̍��ڂɈړ�. </summary>
		protected void NextColumn( ref Rect position ) {
			position.x += position.width;
			if( NoIndent != null ) NoIndent = new EditorGUI.IndentLevelScope( -EditorGUI.indentLevel );
		}

		/// <summary> GUI�̉��s. </summary>
		protected void NextLine( ref Rect position ) {
			position.y += position.height;
			Height += position.height;
			position.x = X;
			usedFixedWidth = 0;
			NoIndentDispose();
		}
		/// <summary> �C���f���g�������̉���. </summary>
		void NoIndentDispose() {
			if( NoIndent != null ) {
				NoIndent.Dispose();
				NoIndent = null;
			}
		}
	}
}
#endif