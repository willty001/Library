#if UNITY_EDITOR
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace HS {
	public abstract class PropertyDrawerBase<T> : PropertyDrawer where T : PropertyDrawerProperties {
		public Dictionary<string, T> propertiesList = new Dictionary<string, T>();

		public override void OnGUI( Rect position, SerializedProperty property, GUIContent label ) {
			T properties = null;
			if( !propertiesList.TryGetValue( property.propertyPath, out properties ) ) properties = propertiesList[property.propertyPath] = CreateProperties( property );

			properties?.OnGUI( ref position, label );
		}

		protected abstract T CreateProperties( SerializedProperty property );

		public override float GetPropertyHeight( SerializedProperty property, GUIContent label ) {
			T properties = null;
			if( propertiesList.TryGetValue( property.propertyPath, out properties ) ) return properties.Height;
			return base.GetPropertyHeight( property, label );
		}
	}
}
#endif