namespace HS {
	// TODO: SerializedObject‰»‚µ‚½‚¢.
	public class OutputCommonSetting {
		static OutputCommonSetting instance = null;
		static OutputCommonSetting Instance { get { return ( instance != null )? instance : instance = new OutputCommonSetting(); } }

		public static int UsingAfterLF { get { return Instance.usingAfterLinefeed; } }
		public static int NamespaceAfterLF { get { return Instance.namespaceAfterLinefeed; } }
		public static string LF { get { return Instance.linefeed; } }
		public static string Tab { get { return Instance.tab; } }

		int usingAfterLinefeed = 2;
		int namespaceAfterLinefeed = 0;
		string linefeed = "\n";
		string tab = "\t";
	}
}
