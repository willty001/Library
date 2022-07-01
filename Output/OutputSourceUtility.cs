namespace HS {
	/// <summary> コード出力の処理の共通化用クラス. </summary>
	public static class OutputSourceUtility {
		public static string AccessModifier( AccessibilityType accessibility, bool _static ) {
			return $"{accessibility.ToCode()} {StaticToCode( _static )}";
		}
		public static string StaticToCode( bool _static ) {
			return _static? "static " : "";
		}
		public static string RefToCode( bool _ref ) {
			return _ref? "ref " : "";
		}
		public static string OutToCode( bool _out ) {
			return _out? "out " : "";
		}
		public static string ThisToCode( bool _this ) {
			return _this? "this " : "";
		}
		public static string ParamsToCode( bool _params ) {
			return _params? "params " : "";
		}
	}
}
