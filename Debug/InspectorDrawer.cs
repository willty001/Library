/*
 * PropertyDrawer‚ÌŠî‘bƒNƒ‰ƒX.
 */ 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace HS {
	public abstract class InspectorDrawer<T>
#if UNITY_EDITOR
	: PropertyDrawer
#endif
	where T : class {
		GUICommon gui = new GUICommon();

		public abstract class DrawerProperties : Properties<T> {
			public float Height { get; set; } = 0;	

			public DrawerProperties( Properties<T> properties ) : base( properties ) {}
			public abstract void OnInspectorGUI( GUICommon gui );
		}

		Dictionary<string,DrawerProperties> properties = new Dictionary<string, DrawerProperties>();

#if UNITY_EDITOR
		public override void OnGUI( Rect position, SerializedProperty property, GUIContent label ) {
			if( !properties.ContainsKey( property.propertyPath ) ) {
				Properties<T> temp = new Properties<T>( property );
				properties.Add( property.propertyPath, CreateProperties( temp ) );
			}

			gui.Position = position;
			gui.GUIType = GUIType.GUILayout;
			using( new EditorGUI.PropertyScope( position, label, property ) ) {
				properties[property.propertyPath].OnInspectorGUI( gui );
			}
		}
#endif

		protected abstract DrawerProperties CreateProperties( Properties<T> properties );

	}
}