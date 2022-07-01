/*
 * GUI系のラップクラス.
 * UnityEditor外でも利用できることを目指す.
 */ 

using UnityEngine;
using UnityEditor;

namespace HS {
	public enum GUIType {
		Invalid,
		EditorGUI,
		GUI,
		GUILayout,
		EditorGUILayout,
	}

	public class GUICommon {
		Rect position = Rect.zero;
		Rect current = Rect.zero;
		public GUIType GUIType { get; set; } = GUIType.Invalid;
		public Rect Position { 
			get { return position; }
			set { 
				position.x = value.x;
				position.y = value.y;
				position.width = value.width;
				position.height = value.height;
			}
		}

		public GUIScope CreateHorizontalScope() {
			return new HorizontalGUIScope( this );
		}

		public void BeginHorizontal() {
			switch( GUIType ) {
				case GUIType.EditorGUILayout:
					EditorGUILayout.BeginHorizontal();
					break;
				case GUIType.GUILayout:
					GUILayout.BeginHorizontal();
					break;
			}
		}

		public void EndHorizontal() {
			switch( GUIType ) {
				case GUIType.EditorGUILayout:
					EditorGUILayout.BeginHorizontal();
					break;
				case GUIType.GUILayout:
					GUILayout.EndHorizontal();
					break;
			}
		}

		public int IntField( string label, int value ) {
			BeforeDraw();
			switch( GUIType ) {
				case GUIType.EditorGUILayout:
					value = EditorGUILayout.IntField( label, value );
					break;
				case GUIType.GUILayout:
					value = EditorGUI.IntField( current, label, value );
					break;
			}
			AfterDraw();
			return value;
		}
		public int IntField( int value ) {
			BeforeDraw();
			switch( GUIType ) {
				case GUIType.EditorGUILayout:
					value = EditorGUILayout.IntField( value );
					break;
				case GUIType.GUILayout:
					value = EditorGUI.IntField( current, value );
					break;
			}
			AfterDraw();
			return value;
		}
		public string TextField( string label, string value ) {
			BeforeDraw();
			switch( GUIType ) {
				case GUIType.EditorGUILayout:
					value = EditorGUILayout.TextField( label, value );
					break;
				case GUIType.GUILayout:
					value = EditorGUI.TextField( current, label, value );
					break;
			}
			AfterDraw();
			return value;
		}
		public string TextField( string value ) {
			BeforeDraw();
			switch( GUIType ) {
				case GUIType.EditorGUILayout:
					value = EditorGUILayout.TextField( value );
					break;
				case GUIType.GUILayout:
					value = EditorGUI.TextField( current, value );
					break;
			}
			AfterDraw();
			return value;
		}
		public bool Toggle( string label, bool value ) {
			BeforeDraw();
			switch( GUIType ) {
				case GUIType.EditorGUILayout:
					value = EditorGUILayout.Toggle( label, value );
					break;
				case GUIType.GUILayout:
					value = EditorGUI.Toggle( current, label, value );
					break;
			}
			AfterDraw();
			return value;
		}
		public bool Toggle( bool value ) {
			BeforeDraw();
			switch( GUIType ) {
				case GUIType.EditorGUILayout:
					value = EditorGUILayout.Toggle( value );
					break;
				case GUIType.GUILayout:
					value = EditorGUI.Toggle( current, value );
					break;
			}
			AfterDraw();
			return value;
		}
		public void Label( string label, string value ) {
			BeforeDraw();
			switch( GUIType ) {
				case GUIType.EditorGUILayout:
					EditorGUILayout.LabelField( label, value );
					break;
				case GUIType.GUILayout:
					EditorGUI.LabelField( current, label, value );
					break;
			}
			AfterDraw();
		}
		public void Label( string value ) {
			BeforeDraw();
			switch( GUIType ) {
				case GUIType.EditorGUILayout:
					EditorGUILayout.LabelField( value );
					break;
				case GUIType.GUILayout:
					EditorGUI.LabelField( current, value );
					break;
			}
			AfterDraw();
		}

		void BeforeDraw() {

		}
		void AfterDraw() {

		}
	}
}