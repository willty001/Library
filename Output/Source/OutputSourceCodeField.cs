/*
 * クラスソースコードの出力クラス.
 */
namespace HS {
	public class OutputSourceCodeField : IOutputSouce {
		public string Type;
		public string Name;
		public AccessibilityType Accessibility;
		public bool Static;

		public void Output( OutputSourceSetting output ) {
			output.WriteLine( $"{OutputSourceUtility.AccessModifier( Accessibility, Static )}{Type} {Name};" );
		}

		public void Clear() {}
	}
}
