/*
 * クラスソースコードの出力クラス.
 */
using System.Collections.Generic;
using System.Linq;

namespace HS {
	public class OutputSourceCodeMethod : IOutputSouce {
		public string Type;
		public string Name;
		public AccessibilityType Accessibility;
		public bool Static;
		public Dictionary<string, CodeArgument> arguments = new Dictionary<string, CodeArgument>();
		List<ICode> codes = new List<ICode>();

		public void Output( OutputSourceSetting output ) {
			string functionDeclare = $"{OutputSourceUtility.AccessModifier( Accessibility, Static )}{Type} {Name}( {string.Join( ",", arguments.Select( a => a.Value.ToCode( output ) ) )} )";
			output.WriteLine( $"{functionDeclare} {{" );
			using( output.CreateTabDisposable() ) {
				foreach( var code in codes ) {
					code.Output( output );
				}
			}
			output.WriteLine( $"}} // {functionDeclare}" );
		}

		public void Argument( string type, string name, bool _ref = false, bool _out = false, bool _this = false, bool _params = false ) {
			arguments.Add( name, new CodeArgument() { Type = type, Name = name, This = _this, Ref = _ref, Out = _out, Params = _params } );
			
		}
		public void Argument<T>( string name, bool _ref = false, bool _out = false, bool _this = false, bool _params = false ) {
			Argument( typeof( T ).Name, name, _ref, _out, _this, _params );
		}

		public void Code( string str ) {
			Code( new Code() { Value = str } );
		}
		public void Code( ICode code ) {
			codes.Add( code );
		}

		public void Clear() {
			arguments.Clear();
			codes.Clear();
		}
	}
}
