namespace HS {
	public class Code : ICode {
		public string Value;

		public string ToCode( OutputSourceSetting output ) {
			return Value;
		}

		public void Output( OutputSourceSetting output ) {
			output.WriteLine( ToCode( output ) );
		}
	}
}
