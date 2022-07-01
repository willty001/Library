#if UNITY_EDITOR
using System;
using UnityEditor;
using UnityEngine;

namespace HS {
	public static class GUIExpansion {
		/// <summary> Vector�^�̃��x���� </summary>
		const float VectorLabelWidth = 10f;

		/// <summary> [�g��] EditorGUILayout.PropertyField. </summary>
		public static void PropertyGUI( this SerializedProperty property ) {
			EditorGUILayout.PropertyField( property, true );
		}
		/// <summary> [�g��] EditorGUILayout.PropertyField. </summary>
		public static void PropertyGUI( this SerializedProperty property, GUIContent label ) {
			EditorGUILayout.PropertyField( property, label, true );
		}
		/// <summary> [�g��] EditorGUI.TextField �̕\�� EditorGUI.PropertyField �łȂ��ׁA�ϐ����͏o�������Ȃ��Ɖ��肷��. </summary>
		public static void TextGUI( this SerializedProperty property ) {
			property.stringValue = EditorGUILayout.TextField( property.stringValue );
		}
		/// <summary> [�g��] EditorGUI.IntField �̕\�� EditorGUI.PropertyField �łȂ��ׁA�ϐ����͏o�������Ȃ��Ɖ��肷��. </summary>
		public static void IntGUI( this SerializedProperty property ) {
			property.intValue = EditorGUILayout.IntField( property.intValue );
		}
		/// <summary> [�g��] EditorGUI.FloatField �̕\�� EditorGUI.PropertyField �łȂ��ׁA�ϐ����͏o�������Ȃ��Ɖ��肷��. </summary>
		public static void FloatGUI( this SerializedProperty property ) {
			property.floatValue = EditorGUILayout.FloatField( property.floatValue );
		}
		/// <summary> [�g��] EditorGUI.DoubleField �̕\�� EditorGUI.PropertyField �łȂ��ׁA�ϐ����͏o�������Ȃ��Ɖ��肷��. </summary>
		public static void DoubleGUI( this SerializedProperty property ) {
			property.doubleValue = EditorGUILayout.DoubleField( property.doubleValue );
		}
		/// <summary> [�g��] EditorGUI.ColorField �̕\�� EditorGUI.PropertyField �łȂ��ׁA�ϐ����͏o�������Ȃ��Ɖ��肷��. </summary>
		public static void ColorGUI( this SerializedProperty property) {
			property.colorValue = EditorGUILayout.ColorField( property.colorValue );
		}

		/// <summary> [�g��] EditorGUI.PropertyField. </summary>
		public static void PropertyGUI( this SerializedProperty property, ref Rect rect ) {
			EditorGUI.PropertyField( rect, property, true );
		}
		/// <summary> [�g��] EditorGUI.PropertyField. </summary>
		public static void PropertyGUI( this SerializedProperty property, ref Rect rect, GUIContent label ) {
			EditorGUI.PropertyField( rect, property, label, true );
		}
		/// <summary> [�g��] Char�p�Ǝ�GUI�̕\�� EditorGUI.PropertyField �łȂ��ׁA�ϐ����͏o�������Ȃ��Ɖ��肷��. </summary>
		public static void CharGUI( this SerializedProperty property, ref Rect rect ) {
			// TODO: Int�Ɍq���ǂ�.
			property.IntGUI( ref rect );
		}
		/// <summary> [�g��] EditorGUI.TextField �̕\�� EditorGUI.PropertyField �łȂ��ׁA�ϐ����͏o�������Ȃ��Ɖ��肷��. </summary>
		public static void TextGUI( this SerializedProperty property, ref Rect rect ) {
			property.stringValue = EditorGUI.TextField( rect, property.stringValue );
		}
		/// <summary> [�g��] EditorGUI.IntField �̕\�� EditorGUI.PropertyField �łȂ��ׁA�ϐ����͏o�������Ȃ��Ɖ��肷��. </summary>
		public static void IntGUI( this SerializedProperty property, ref Rect rect ) {
			property.intValue = EditorGUI.IntField( rect, property.intValue );
		}
		/// <summary> [�g��] EditorGUI.FloatField �̕\�� EditorGUI.PropertyField �łȂ��ׁA�ϐ����͏o�������Ȃ��Ɖ��肷��. </summary>
		public static void FloatGUI( this SerializedProperty property, ref Rect rect ) {
			property.floatValue = EditorGUI.FloatField( rect, property.floatValue );
		}
		/// <summary> [�g��] EditorGUI.DoubleField �̕\�� EditorGUI.PropertyField �łȂ��ׁA�ϐ����͏o�������Ȃ��Ɖ��肷��. </summary>
		public static void DoubleGUI( this SerializedProperty property, ref Rect rect ) {
			property.doubleValue = EditorGUI.DoubleField( rect, property.doubleValue );
		}
		/// <summary> [�g��] EditorGUI.ColorField �̕\�� EditorGUI.PropertyField �łȂ��ׁA�ϐ����͏o�������Ȃ��Ɖ��肷��. </summary>
		public static void ColorGUI( this SerializedProperty property, ref Rect rect ) {
			property.colorValue = EditorGUI.ColorField( rect, property.colorValue );
		}
		/// <summary> [�g��] EditorGUI.Vector2IntField �̕\�� EditorGUI.PropertyField �łȂ��ׁA�ϐ����͏o�������Ȃ��Ɖ��肷��. </summary>
		public static void Vector2IntGUI( this SerializedProperty property, ref Rect rect ) {
			var vector = property.vector2IntValue;
			float x = rect.x;
			float width = rect.width - 2 * VectorLabelWidth;
			float valueWidth = width * 0.5f;

			"x".LabelGUI( ref rect, VectorLabelWidth );

			using( new EditorGUI.IndentLevelScope( -EditorGUI.indentLevel ) ) {
				vector.x = vector.x.IntGUI( ref rect, valueWidth );
				"y".LabelGUI( ref rect, VectorLabelWidth );
				vector.y = vector.y.IntGUI( ref rect, valueWidth );
			}
			rect.x = x;
			rect.width = width;

			property.vector2IntValue = vector;
		}
		/// <summary> [�g��] EditorGUI.Vector2Field �̕\�� EditorGUI.PropertyField �łȂ��ׁA�ϐ����͏o�������Ȃ��Ɖ��肷��. </summary>
		public static void Vector2GUI( this SerializedProperty property, ref Rect rect ) {
			var vector = property.vector2Value;
			float x = rect.x;
			float width = rect.width - 2 * VectorLabelWidth;
			float valueWidth = width * 0.5f;

			"x".LabelGUI( ref rect, VectorLabelWidth );

			using( new EditorGUI.IndentLevelScope( -EditorGUI.indentLevel ) ) {
				vector.x = vector.x.FloatGUI( ref rect, valueWidth );
				"y".LabelGUI( ref rect, VectorLabelWidth );
				vector.y = vector.y.FloatGUI( ref rect, valueWidth );
			}
			rect.x = x;
			rect.width = width;

			property.vector2Value = vector;
		}
		/// <summary> [�g��] EditorGUI.Vector3IntField �̕\�� EditorGUI.PropertyField �łȂ��ׁA�ϐ����͏o�������Ȃ��Ɖ��肷��. </summary>
		public static void Vector3IntGUI( this SerializedProperty property, ref Rect rect ) {
			var vector = property.vector3IntValue;
			float x = rect.x;
			float width = rect.width - 3 * VectorLabelWidth;
			float valueWidth = width / 3.0f;

			"x".LabelGUI( ref rect, VectorLabelWidth );

			using( new EditorGUI.IndentLevelScope( -EditorGUI.indentLevel ) ) {
				vector.x = vector.x.IntGUI( ref rect, valueWidth );
				"y".LabelGUI( ref rect, VectorLabelWidth );
				vector.y = vector.y.IntGUI( ref rect, valueWidth );
				"z".LabelGUI( ref rect, VectorLabelWidth );
				vector.z = vector.z.IntGUI( ref rect, valueWidth );
			}
			rect.x = x;
			rect.width = width;

			property.vector3IntValue = vector;
		}
		/// <summary> [�g��] EditorGUI.Vector3Field �̕\�� EditorGUI.PropertyField �łȂ��ׁA�ϐ����͏o�������Ȃ��Ɖ��肷��. </summary>
		public static void Vector3GUI( this SerializedProperty property, ref Rect rect ) {
			var vector = property.vector3Value;
			float x = rect.x;
			float width = rect.width - 3 * VectorLabelWidth;
			float valueWidth = width / 3.0f;

			"x".LabelGUI( ref rect, VectorLabelWidth );

			using( new EditorGUI.IndentLevelScope( -EditorGUI.indentLevel ) ) {
				vector.x = vector.x.FloatGUI( ref rect, valueWidth );
				"y".LabelGUI( ref rect, VectorLabelWidth );
				vector.y = vector.y.FloatGUI( ref rect, valueWidth );
				"z".LabelGUI( ref rect, VectorLabelWidth );
				vector.z = vector.z.FloatGUI( ref rect, valueWidth );
			}
			rect.x = x;
			rect.width = width;

			property.vector3Value = vector;
		}
		/// <summary> [�g��] EditorGUI.ObjectField �̕\�� EditorGUI.PropertyField �łȂ��ׁA�ϐ����͏o�������Ȃ��Ɖ��肷��. </summary>
		public static void ObjectGUI<T>( this SerializedProperty property, ref Rect rect, bool allowSceneObjects ) where T : UnityEngine.Object {
			property.objectReferenceValue = EditorGUI.ObjectField( rect, property.objectReferenceValue, typeof( T ), allowSceneObjects );
		}

		/// <summary> [�g��] EditorGUI.LabelField ��`�悵�Ă��̕� x ��i�߂�. </summary>
		public static void LabelGUI( this string value, ref Rect rect, float width ) {
			rect.width = width;
			EditorGUI.LabelField( rect, value );
			rect.x += rect.width;
		}
		/// <summary> [�g��] EditorGUI.IntField ��`�悵�Ă��̕� x ��i�߂�. </summary>
		public static int IntGUI( this int value, ref Rect rect, float width ) {
			rect.width = width;
			value = EditorGUI.IntField( rect, value );
			rect.x += rect.width;
			return value;
		}
		/// <summary> [�g��] EditorGUI.IntField ��`�悵�Ă��̕� x ��i�߂�. </summary>
		public static float FloatGUI( this float value, ref Rect rect, float width ) {
			rect.width = width;
			value = EditorGUI.FloatField( rect, value );
			rect.x += rect.width;
			return value;
		}
		/// <summary> [�g��] EditorGUI.ObjectGUI ��`�悵�Ă��̕� x ��i�߂�. </summary>
		public static T ObjectGUI<T>( this T value, ref Rect rect, float width, bool allowSceneObject ) where T : UnityEngine.Object {
			rect.width = width;
			value = EditorGUI.ObjectField( rect, value, typeof( T ), allowSceneObject ) as T;
			rect.x += rect.width;
			return value;
		}
		/// <summary> [�g��] EditorGUILayout.LabelField ��`��. </summary>
		public static void LabelGUI( this string value ) {
			EditorGUILayout.LabelField( value );
		}
		/// <summary> [�g��] EditorGUILayout.IntField ��`��. </summary>
		public static int IntGUI( this int value ) {
			return EditorGUILayout.IntField( value );
		}
		/// <summary> [�g��] EditorGUI.IntField ��`��. </summary>
		public static float FloatGUI( this float value ) {
			return EditorGUILayout.FloatField( value );
		}
		/// <summary> [�g��] EditorGUI.ToggleField ��`��. </summary>
		public static bool ToggleGUI( this bool value ) {
			return EditorGUILayout.Toggle( value );
		}
		/// <summary> [�g��] EditorGUI.ToggleField ��`��. </summary>
		public static bool ToggleGUI( this bool value, string label ) {
			return EditorGUILayout.Toggle( label, value );
		}
		/// <summary> [�g��] EditorGUI.ObjectGUI ��`��. </summary>
		public static T ObjectGUI<T>( this T value, bool allowSceneObject ) where T : UnityEngine.Object {
			return EditorGUILayout.ObjectField( value, typeof( T ), allowSceneObject ) as T;
		}
	}
}
#endif