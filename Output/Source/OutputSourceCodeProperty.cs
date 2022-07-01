/*
 * クラスソースコードの出力クラス.
 */
using System.Collections.Generic;
using System.Linq;

namespace HS {
	public class OutputSourceCodeProperty : IOutputSouce {
		public string Type;
		public string Name;
		public AccessibilityType Accessibility;
		public bool Static;

		string get;
		string set;

		public OutputSourceCodeProperty Get( string value ) {
			get = value;
			return this;
		}
		public OutputSourceCodeProperty Set( string value ) {
			set = value;
			return this;
		}

		public void Output( OutputSourceSetting output ) {
			bool getEmpty = string.IsNullOrEmpty( get );
			bool setEmpty = string.IsNullOrEmpty( set );

			string declareProperty = $"{OutputSourceUtility.AccessModifier( Accessibility, Static )}{Type} {Name}";
			if( getEmpty && setEmpty ) {
				output.WriteLine( $"{declareProperty} {{ get; set; }}" );
			} else {
				output.WriteLine( $"{declareProperty} {{" );
				using( output.CreateTabDisposable() ) {
					if( !getEmpty ) output.WriteLine( $"get {{ {get} }}" );
					if( !setEmpty ) output.WriteLine( $"set {{ {set} }}" );
				}
				output.WriteLine( "}" );
			}
		}

		public void Clear() {
			get = null;
			set = null;
		}
	}
}
