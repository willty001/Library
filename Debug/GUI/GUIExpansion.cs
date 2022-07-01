#if UNITY_EDITOR
using System;
using UnityEditor;
using UnityEngine;

namespace HS {
	public static class GUIExpansion {
		/// <summary> Vector型のラベル長 </summary>
		const float VectorLabelWidth = 10f;

		/// <summary> [拡張] EditorGUILayout.PropertyField. </summary>
		public static void PropertyGUI( this SerializedProperty property ) {
			EditorGUILayout.PropertyField( property, true );
		}
		/// <summary> [拡張] EditorGUILayout.PropertyField. </summary>
		public static void PropertyGUI( this SerializedProperty property, GUIContent label ) {
			EditorGUILayout.PropertyField( property, label, true );
		}
		/// <summary> [拡張] EditorGUI.TextField の表示 EditorGUI.PropertyField でない為、変数名は出したくないと仮定する. </summary>
		public static void TextGUI( this SerializedProperty property ) {
			property.stringValue = EditorGUILayout.TextField( property.stringValue );
		}
		/// <summary> [拡張] EditorGUI.IntField の表示 EditorGUI.PropertyField でない為、変数名は出したくないと仮定する. </summary>
		public static void IntGUI( this SerializedProperty property ) {
			property.intValue = EditorGUILayout.IntField( property.intValue );
		}
		/// <summary> [拡張] EditorGUI.FloatField の表示 EditorGUI.PropertyField でない為、変数名は出したくないと仮定する. </summary>
		public static void FloatGUI( this SerializedProperty property ) {
			property.floatValue = EditorGUILayout.FloatField( property.floatValue );
		}
		/// <summary> [拡張] EditorGUI.DoubleField の表示 EditorGUI.PropertyField でない為、変数名は出したくないと仮定する. </summary>
		public static void DoubleGUI( this SerializedProperty property ) {
			property.doubleValue = EditorGUILayout.DoubleField( property.doubleValue );
		}
		/// <summary> [拡張] EditorGUI.ColorField の表示 EditorGUI.PropertyField でない為、変数名は出したくないと仮定する. </summary>
		public static void ColorGUI( this SerializedProperty property) {
			property.colorValue = EditorGUILayout.ColorField( property.colorValue );
		}

		/// <summary> [拡張] EditorGUI.PropertyField. </summary>
		public static void PropertyGUI( this SerializedProperty property, ref Rect rect ) {
			EditorGUI.PropertyField( rect, property, true );
		}
		/// <summary> [拡張] EditorGUI.PropertyField. </summary>
		public static void PropertyGUI( this SerializedProperty property, ref Rect rect, GUIContent label ) {
			EditorGUI.PropertyField( rect, property, label, true );
		}
		/// <summary> [拡張] Char用独自GUIの表示 EditorGUI.PropertyField でない為、変数名は出したくないと仮定する. </summary>
		public static void CharGUI( this SerializedProperty property, ref Rect rect ) {
			// TODO: Intに繋いどく.
			property.IntGUI( ref rect );
		}
		/// <summary> [拡張] EditorGUI.TextField の表示 EditorGUI.PropertyField でない為、変数名は出したくないと仮定する. </summary>
		public static void TextGUI( this SerializedProperty property, ref Rect rect ) {
			property.stringValue = EditorGUI.TextField( rect, property.stringValue );
		}
		/// <summary> [拡張] EditorGUI.IntField の表示 EditorGUI.PropertyField でない為、変数名は出したくないと仮定する. </summary>
		public static void IntGUI( this SerializedProperty property, ref Rect rect ) {
			property.intValue = EditorGUI.IntField( rect, property.intValue );
		}
		/// <summary> [拡張] EditorGUI.FloatField の表示 EditorGUI.PropertyField でない為、変数名は出したくないと仮定する. </summary>
		public static void FloatGUI( this SerializedProperty property, ref Rect rect ) {
			property.floatValue = EditorGUI.FloatField( rect, property.floatValue );
		}
		/// <summary> [拡張] EditorGUI.DoubleField の表示 EditorGUI.PropertyField でない為、変数名は出したくないと仮定する. </summary>
		public static void DoubleGUI( this SerializedProperty property, ref Rect rect ) {
			property.doubleValue = EditorGUI.DoubleField( rect, property.doubleValue );
		}
		/// <summary> [拡張] EditorGUI.ColorField の表示 EditorGUI.PropertyField でない為、変数名は出したくないと仮定する. </summary>
		public static void ColorGUI( this SerializedProperty property, ref Rect rect ) {
			property.colorValue = EditorGUI.ColorField( rect, property.colorValue );
		}
		/// <summary> [拡張] EditorGUI.Vector2IntField の表示 EditorGUI.PropertyField でない為、変数名は出したくないと仮定する. </summary>
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
		/// <summary> [拡張] EditorGUI.Vector2Field の表示 EditorGUI.PropertyField でない為、変数名は出したくないと仮定する. </summary>
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
		/// <summary> [拡張] EditorGUI.Vector3IntField の表示 EditorGUI.PropertyField でない為、変数名は出したくないと仮定する. </summary>
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
		/// <summary> [拡張] EditorGUI.Vector3Field の表示 EditorGUI.PropertyField でない為、変数名は出したくないと仮定する. </summary>
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
		/// <summary> [拡張] EditorGUI.ObjectField の表示 EditorGUI.PropertyField でない為、変数名は出したくないと仮定する. </summary>
		public static void ObjectGUI<T>( this SerializedProperty property, ref Rect rect, bool allowSceneObjects ) where T : UnityEngine.Object {
			property.objectReferenceValue = EditorGUI.ObjectField( rect, property.objectReferenceValue, typeof( T ), allowSceneObjects );
		}

		/// <summary> [拡張] EditorGUI.LabelField を描画してその分 x を進める. </summary>
		public static void LabelGUI( this string value, ref Rect rect, float width ) {
			rect.width = width;
			EditorGUI.LabelField( rect, value );
			rect.x += rect.width;
		}
		/// <summary> [拡張] EditorGUI.IntField を描画してその分 x を進める. </summary>
		public static int IntGUI( this int value, ref Rect rect, float width ) {
			rect.width = width;
			value = EditorGUI.IntField( rect, value );
			rect.x += rect.width;
			return value;
		}
		/// <summary> [拡張] EditorGUI.IntField を描画してその分 x を進める. </summary>
		public static float FloatGUI( this float value, ref Rect rect, float width ) {
			rect.width = width;
			value = EditorGUI.FloatField( rect, value );
			rect.x += rect.width;
			return value;
		}
		/// <summary> [拡張] EditorGUI.ObjectGUI を描画してその分 x を進める. </summary>
		public static T ObjectGUI<T>( this T value, ref Rect rect, float width, bool allowSceneObject ) where T : UnityEngine.Object {
			rect.width = width;
			value = EditorGUI.ObjectField( rect, value, typeof( T ), allowSceneObject ) as T;
			rect.x += rect.width;
			return value;
		}
		/// <summary> [拡張] EditorGUILayout.LabelField を描画. </summary>
		public static void LabelGUI( this string value ) {
			EditorGUILayout.LabelField( value );
		}
		/// <summary> [拡張] EditorGUILayout.IntField を描画. </summary>
		public static int IntGUI( this int value ) {
			return EditorGUILayout.IntField( value );
		}
		/// <summary> [拡張] EditorGUI.IntField を描画. </summary>
		public static float FloatGUI( this float value ) {
			return EditorGUILayout.FloatField( value );
		}
		/// <summary> [拡張] EditorGUI.ToggleField を描画. </summary>
		public static bool ToggleGUI( this bool value ) {
			return EditorGUILayout.Toggle( value );
		}
		/// <summary> [拡張] EditorGUI.ToggleField を描画. </summary>
		public static bool ToggleGUI( this bool value, string label ) {
			return EditorGUILayout.Toggle( label, value );
		}
		/// <summary> [拡張] EditorGUI.ObjectGUI を描画. </summary>
		public static T ObjectGUI<T>( this T value, bool allowSceneObject ) where T : UnityEngine.Object {
			return EditorGUILayout.ObjectField( value, typeof( T ), allowSceneObject ) as T;
		}
	}
}
#endif