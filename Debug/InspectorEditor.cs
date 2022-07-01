using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class InspectorEditor
#if UNITY_EDITOR
: Editor
#endif
{
}

public class Property<T> where T : class {
	T variable = null;
	PropertyInfo propertyInfo = null;
	public Property( T variable, PropertyInfo propertyInfo ) {
		this.variable = variable;
		this.propertyInfo = propertyInfo;
	}
#if UNITY_EDITOR
	SerializedProperty serializedProperty = null;
	public Property( SerializedProperty serializedProperty ) {
		this.serializedProperty = serializedProperty;
	}
#endif
	public int intValue {
		get {
#if UNITY_EDITOR
			if( serializedProperty != null ) return serializedProperty.intValue;
#endif
			if( propertyInfo != null ) return (int)propertyInfo.GetValue( variable );
			return 0;
		}
		set {
#if UNITY_EDITOR
			if( serializedProperty != null ) serializedProperty.intValue = value;
#endif
			if( propertyInfo != null ) propertyInfo.SetValue( variable, value );
		}
	}
	public bool boolValue {
		get {
#if UNITY_EDITOR
			if( serializedProperty != null ) return serializedProperty.boolValue;
#endif
			if( propertyInfo != null ) return (bool)propertyInfo.GetValue( variable );
			return false;
		}
		set {
#if UNITY_EDITOR
			if( serializedProperty != null ) serializedProperty.boolValue = value;
#endif
			if( propertyInfo != null ) propertyInfo.SetValue( variable, value );
		}
	}
	public string stringValue {
		get {
#if UNITY_EDITOR
			if( serializedProperty != null ) return serializedProperty.stringValue;
#endif
			if( propertyInfo != null ) return (string)propertyInfo.GetValue( variable );
			return string.Empty;
		}
		set {
#if UNITY_EDITOR
			if( serializedProperty != null ) serializedProperty.stringValue = value;
#endif
			if( propertyInfo != null ) propertyInfo.SetValue( variable, value );
		}
	}
}

public class Properties<T> where T : class {
	T variable = null;
	PropertyInfo[] propertyInfos = null;
	Dictionary<string,Property<T>> properties = new Dictionary<string, Property<T>>();

	public Properties( T variable ) {
		this.variable = variable;
		propertyInfos = variable.GetType().GetProperties();
	}

	public Properties( Properties<T> properties ) {
	}

	public Property<T> this[string key] {
		get{
			Property<T> property = null;
			if( !properties.TryGetValue( key, out property ) ) {
#if UNITY_EDITOR
				if( serializedProperty != null ) property = new Property<T>( serializedProperty.FindPropertyRelative( key ) );
				if( propertyInfos != null ) {
					foreach( var info in propertyInfos ) {
						if( info.Name == key ) {
							property = new Property<T>( variable, info );
							break;
						}
					}
				}

				if( property == null ) {
					Debug.Log( $"{key} " );
				}
#endif
				properties.Add( key, property );
			}
			return property;
		}
	}

#if UNITY_EDITOR
	SerializedProperty serializedProperty = null;
	public Properties( SerializedProperty serializedProperty ) {
		this.serializedProperty = serializedProperty;
	}
#endif
}