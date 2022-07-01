using System.Collections.Generic;

namespace HS {
	public class CodeIF : ICode {
		List<ICode> trueCodes = new List<ICode>();
		List<ICode> falseCodes = new List<ICode>();

		public string Value;

		public CodeIF True( ICode code ) {
			trueCodes.Add( code );
			return this;
		}
		public CodeIF False( ICode code ) {
			falseCodes.Add( code );
			return this;
		}

		public string ToCode( OutputSourceSetting output ) {
			string code = "";
			
			code += output.MultiLineStart( $"if( {Value} ) {{" );

			using( output.CreateTabDisposable() ) {
				foreach( var c in trueCodes ) {
					code += output.MultiLine( c.ToCode( output ) );
				}
			}
			if( falseCodes.Count > 0 ) {
				code += output.MultiLine( $"}} else {{" );
				using( output.CreateTabDisposable() ) {
					foreach( var c in falseCodes ) {
						code += output.MultiLine( c.ToCode( output ) );
					}
				}

			}

			code += output.MultiLineStart( $"}}" );

			return code;
		}

		public void Output( OutputSourceSetting output ) {
			output.WriteLine( ToCode( output ) );
		}
	}
}
