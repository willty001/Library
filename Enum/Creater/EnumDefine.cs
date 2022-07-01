using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace HS {
	[CreateAssetMenu( fileName = nameof( EnumDefine ), menuName = Define.MENU + "/" + nameof( EnumDefine ), order = Define.MENU_PRIORITY )]
	public class EnumDefine : ScriptableObject {
		[SerializeField]
		EnumDefineTypes type = EnumDefineTypes.tNon;
		[SerializeReference]
		aEnumDefineElements elements = null;
		[SerializeField]
		OutputSourceCode setting = new OutputSourceCode();

		public int GetValue( string key, int errorValue ) {
			if( elements is EnumDefineElementsInt ints ) {
				int value;
				if( ints.TryGetValue( key, out value ) ) return value;
			}
			return errorValue;
		}
		public float GetValue( string key, float errorValue ) {
			if( elements is EnumDefineElementsFloat floats ) {
				float value;
				if( floats.TryGetValue( key, out value ) ) return value;
			}
			return errorValue;
		}
		public double GetValue( string key, double errorValue ) {
			if( elements is EnumDefineElementsDouble doubles ) {
				double value;
				if( doubles.TryGetValue( key, out value ) ) return value;
			}
			return errorValue;
		}
		public char GetValue( string key, char errorValue ) {
			if( elements is EnumDefineElementsChar chars ) {
				char value;
				if( chars.TryGetValue( key, out value ) ) return value;
			}
			return errorValue;
		}
		public string GetValue( string key, string errorValue ) {
			if( elements is EnumDefineElementsString strings ) {
				string value;
				if( strings.TryGetValue( key, out value ) ) return value;
			}
			return errorValue;
		}
		public UnityEngine.Object GetValue( string key, UnityEngine.Object errorValue ) {
			if( elements is EnumDefineElementsObject objects ) {
				UnityEngine.Object value;
				if( objects.TryGetValue( key, out value ) ) return value;
			}
			return errorValue;
		}

		[System.Diagnostics.Conditional( "UNITY_EDITOR" )]
		public void OutputEnum() {
			const string MethodName = "ToValue";
			const string ArgumentName = "value";
			setting.Clear();

			var e = setting.Namespace( "HS" ).Enum( name );

			if( elements is EnumDefineElementsInt intElements ) {
				foreach( var element in intElements.Defines ) {
					e.AddValue( element.Key, element.Value );
				}
			} else {
				foreach( var element in elements.Keys() ) {
					e.AddValue( element );
				}
			}

			bool intValue = ( elements != null ) && ( elements is EnumDefineElementsInt );
			bool relativeValue = ( elements != null ) && !( elements is EnumDefineElements );
			if( relativeValue ) {
				setting.Using( "UnityEngine" );

				string className = $"{name}Expansion";
				var c = setting.Class( className, AccessibilityType.Public, _static:true );
				OutputSourceCodeMethod method = null;

				if( !intValue ) {
					const string instanceName = "assetReference";
					const string InstanceName = "AssetReference";
					c.Field<EnumDefine>( $"{instanceName} = null", AccessibilityType.Private, true );
					c.Property<EnumDefine>( InstanceName, AccessibilityType.Private, true )
						.Get( $"return ( {instanceName} != null )? {instanceName} : {instanceName} = Resources.Load<{nameof(EnumDefine)}>( \"{AssetDatabase.GetAssetPath( this ).ToResourcesPath()}\" );" );

					string returnCode = $"return {InstanceName}.{nameof( GetValue )}( {ArgumentName}.ToString(), {{0}} );";
					switch( type ) {
						case EnumDefineTypes.tFloat:
							method = c.Method( "float", MethodName, AccessibilityType.Public, true );
							method.Code( string.Format( returnCode, "0.0f" ) );
							break;
						case EnumDefineTypes.tDouble:
							method = c.Method( "double", MethodName, AccessibilityType.Public, true );
							method.Code( string.Format( returnCode, "0.0" ) );
							break;
						case EnumDefineTypes.tChar:
							method = c.Method( "char", MethodName, AccessibilityType.Public, true );
							method.Code( string.Format( returnCode, "(char)0" ) );
							break;
						case EnumDefineTypes.tString:
							method = c.Method( "string", MethodName, AccessibilityType.Public, true );
							method.Code( string.Format( returnCode, "\"\"" ) );
							break;
						case EnumDefineTypes.tObject:
							method = c.Method<UnityEngine.Object>( MethodName, AccessibilityType.Public, true );
							method.Code( string.Format( returnCode, "(Object)null" ) );
							break;
					}

				} else if( intValue ) {
					method = c.Method( "int", MethodName, AccessibilityType.Public, true );

					method.Code( $"return (int){ArgumentName};" );
				}
				method.Argument( name, ArgumentName, _this:true );
			}
			setting.Output();

			#if UNITY_EDITOR
			UnityEditor.AssetDatabase.Refresh();
			#endif
		}

		public static bool IsValidType( aEnumDefineElements elements, EnumDefineTypes type ) {
			switch( type ) {
				case EnumDefineTypes.tNon:
					return elements is EnumDefineElements;
				case EnumDefineTypes.tInt:
					return elements is EnumDefineElementsInt;
				case EnumDefineTypes.tFloat:
					return elements is EnumDefineElementsFloat;
				case EnumDefineTypes.tDouble:
					return elements is EnumDefineElementsDouble;
				case EnumDefineTypes.tChar:
					return elements is EnumDefineElementsChar;
				case EnumDefineTypes.tString:
					return elements is EnumDefineElementsString;
				case EnumDefineTypes.tObject:
					return elements is EnumDefineElementsObject;

				default:
					// TODO: Assert
					return false;
			}
		}
		public static aEnumDefineElements GetTypeClass( EnumDefineTypes type ) {
			aEnumDefineElements elements;
			switch( type ) {
				case EnumDefineTypes.tNon:
					elements = new EnumDefineElements();
					break;
				case EnumDefineTypes.tInt:
					elements = new EnumDefineElementsInt();
					break;
				case EnumDefineTypes.tFloat:
					elements = new EnumDefineElementsFloat();
					break;
				case EnumDefineTypes.tDouble:
					elements = new EnumDefineElementsDouble();
					break;
				case EnumDefineTypes.tChar:
					elements = new EnumDefineElementsChar();
					break;
				case EnumDefineTypes.tString:
					elements = new EnumDefineElementsString();
					break;
				case EnumDefineTypes.tObject:
					elements = new EnumDefineElementsObject();
					break;

				default:
					// TODO: Assert
					elements = new EnumDefineElements();
					break;
			}
			return elements;
		}

		public bool IsValidElements() {
			return IsValidType( elements, type );
		}

		public void Reset() {
			elements = GetTypeClass( type );
		}
	}
}