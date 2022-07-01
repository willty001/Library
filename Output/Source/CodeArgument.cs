/*
 * クラスソースコードの出力クラス.
 */
using System.Collections.Generic;

namespace HS {
	public class CodeArgument : ICode {
		public string Type;
		public string Name;
		public bool Ref;
		public bool Out;
		public bool This;
		public bool Params;

		public string ToCode( OutputSourceSetting output ) {
			return $"{OutputSourceUtility.OutToCode( Out )}{OutputSourceUtility.ThisToCode( This )}{OutputSourceUtility.RefToCode( Ref )}{OutputSourceUtility.ParamsToCode( Params )}{Type} {Name}";
		}

		public void Output( OutputSourceSetting output ) {
			output.Write( ToCode( output ) );
		}
	}
}
