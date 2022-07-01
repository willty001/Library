using System;
using System.IO;
using System.Text;

namespace HS {
	public class OutputSourceSetting : IDisposable {
		class TabDisposable : IDisposable {
			OutputSourceSetting Output { get; set; } = null;
			int Length { get; set; } = 0;

			public TabDisposable( OutputSourceSetting output ) {
				Output = output;

				Output.Indent += OutputCommonSetting.Tab;
				Length = OutputCommonSetting.Tab.Length;
			}

			public void Dispose() {
				Output.Indent = Output.Indent.Substring( 0, Math.Max( Output.Indent.Length - Length, 0 ) );
			}
		}

		public int UsingAfterLinefeed { get; set; } = 2;
		public int NamespaceAfterLinefeed { get; set; } = 0;
		public int EnumAfterLinefeed { get; set; } = 1;
		public int ClassAfterLinefeed { get; set; } = 1;
		public int FieldAfterLinefeed { get; set; } = 1;
		public int PropertyAfterLinefeed { get; set; } = 1;
		public int MethodAfterLinefeed { get; set; } = 1;
		public string Linefeed { get; set; } = "\n";
		public string Tab { get; set; } = "\t";
		public bool Sort { get; set; } = true;
		public string Indent { get; protected set; } = "";
		protected StreamWriter StreamWriter { get; set; } = null;


		public OutputSourceSetting( string path, bool append = false, Encoding encoding = null ) {
			if( encoding == null ) encoding = Encoding.UTF8;
			StreamWriter = new StreamWriter( path, append, encoding );
			StreamWriter.NewLine = Linefeed;
		}

		public void LF( int num ) {
			for( int i = 0; i < num; ++i ) StreamWriter.WriteLine();
		}

		public void Add( string str ) {
			StreamWriter.Write( $"{str}" );
		}
		public void Write( string str ) {
			StreamWriter.Write( $"{Indent}{str}" );
		}
		public void WriteLine( string str ) {
			StreamWriter.WriteLine( $"{Indent}{str}" );
		}

		public IDisposable CreateTabDisposable() {
			return new TabDisposable( this );
		}

		public void Dispose() {
			StreamWriter.Close();
		}

		public string MultiLineStart( string str ) {
			return $"{str}{Linefeed}";
		}
		public string MultiLine( string str ) {
			return $"{Indent}{str}{Linefeed}";
		}
		public string MultiLineEnd( string str ) {
			return $"{Indent}{str}";
		}
	}
}
