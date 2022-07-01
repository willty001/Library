/*
 * クラスソースコードの出力クラス.
 */
using System.Collections.Generic;

namespace HS {
	public class OutputSourceCodeClass : IOutputSouce {

		bool Static { get; set; } = false;
		string Name { get; set; } = null;
		AccessibilityType Accessibility { get; set; } = AccessibilityType.Public;

		Dictionary<string, OutputSourceCodeField> fields = new Dictionary<string, OutputSourceCodeField>();
		Dictionary<string, OutputSourceCodeProperty> properties = new Dictionary<string, OutputSourceCodeProperty>();
		Dictionary<string, OutputSourceCodeMethod> methods = new Dictionary<string, OutputSourceCodeMethod>();

		public OutputSourceCodeClass( string name, AccessibilityType accessibility, bool _static ) {
			Name = name;
			Accessibility = accessibility;
			Static = _static;
		}

		public OutputSourceCodeField Field<T>( string name, AccessibilityType accessibility = AccessibilityType.Private, bool _static = false ) {
			var field = new OutputSourceCodeField() { Name = name, Type = typeof( T ).Name, Accessibility = accessibility, Static = _static };
			fields.Add( name, field );
			return field;
		}
		public OutputSourceCodeProperty Property<T>( string name, AccessibilityType accessibility = AccessibilityType.Public, bool _static = false ) {
			var property = new OutputSourceCodeProperty() { Name = name, Type = typeof( T ).Name, Accessibility = accessibility, Static = _static };
			properties.Add( name, property );
			return property;
		}
		public OutputSourceCodeMethod Method( string type, string name, AccessibilityType accessibility, bool _static = false ) {
			var method = new OutputSourceCodeMethod() { Name = name, Type = type, Accessibility = accessibility, Static = _static };
			methods.Add( Name, method );
			return method;
		}
		public OutputSourceCodeMethod Method<T>( string name, AccessibilityType accessibility, bool _static = false ) {
			return Method( typeof( T ).Name, name, accessibility, _static );
		}

		public void Output( OutputSourceSetting output ) {
			string classDeclare = $"{OutputSourceUtility.AccessModifier( Accessibility, Static )}class {Name}";
			output.WriteLine( $"{classDeclare} {{" );
			using( output.CreateTabDisposable() ) {
				// TODO:改行.
				foreach( var f in fields ) {
					f.Value.Output( output );
				}
				if( fields.Count > 0 ) output.LF( output.FieldAfterLinefeed );
				foreach( var p in properties ) {
					p.Value.Output( output );
				}
				if( properties.Count > 0 ) output.LF( output.PropertyAfterLinefeed );
				foreach( var m in methods ) {
					m.Value.Output( output );
				}
				if( methods.Count > 0 ) output.LF( output.MethodAfterLinefeed );
			}
			output.WriteLine( $"}} // {classDeclare}" );
		}

		public void Clear() {
			foreach( var f in fields ) f.Value.Clear();
			foreach( var p in properties ) p.Value.Clear();
			foreach( var m in methods ) m.Value.Clear();
			fields.Clear();
			properties.Clear();
			methods.Clear();
		}
	}
}
