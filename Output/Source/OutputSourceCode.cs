/*
 * ソースコードの出力クラス.
 */
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

namespace HS {
	[Serializable]
	public class OutputSourceCode : IOutputSouce {
		class NamespaceDisposable : IDisposable {
			OutputSourceSetting Output { get; set; } = null;
			Stack<string> Namespaces { get; set; } = new Stack<string>();
			Stack<IDisposable> TabDisposables { get; set; } = new Stack<IDisposable>();
			int Num { get; set; } = 0;

			public NamespaceDisposable( OutputSourceSetting output, List<string> namespaces ) {
				Output = output;
				Num = namespaces.Count;

				for( int i = 0; i < Num; ++i ) {
					var ns = namespaces[i];
					Output.WriteLine( $"namespace {ns} {{" );

					TabDisposables.Push( output.CreateTabDisposable() );
					Namespaces.Push( ns );
				}
				if( Namespaces.Count > 0 ) Output.LF( OutputCommonSetting.NamespaceAfterLF );
			}

			public void Dispose() {
				if( Namespaces.Count > 0 ) Output.LF( OutputCommonSetting.NamespaceAfterLF );

				for( int i = 0; i < Num; ++i ) {
					var tab = TabDisposables.Pop();
					var ns = Namespaces.Pop();
					tab?.Dispose();
					Output.WriteLine( $"}} // namespace {ns}" );
				}
			}
		}

		[SerializeField]
		string outputDirectory = "";
		[SerializeField]
		string fileName = "";
		[SerializeField]
		bool sort = true;

		string OutputDirectory { get { return outputDirectory; } }
		string FileName { get { return fileName; } }

		List<string> usings = new List<string>();
		List<string> namespaces = new List<string>();
		Dictionary<string, OutputSourceCodeEnum> enums = new Dictionary<string, OutputSourceCodeEnum>();
		Dictionary<string, OutputSourceCodeClass> classes = new Dictionary<string, OutputSourceCodeClass>();

		public void Output() {
			using( var output = new OutputSourceSetting( Path.Combine( OutputDirectory, FileName ), false, Encoding.UTF8 ) ) {
				output.Sort = sort;
				Output( output );
			}
		}

		public void Output( OutputSourceSetting output ) {
			output.WriteLine( "/*" );
			output.WriteLine( $" * This File Created By [{nameof( OutputSourceCode )}.cs]" );
			output.WriteLine( " */" );

			OutputUsing( output );

			using( new NamespaceDisposable( output, namespaces ) ) {
				foreach( var e in enums ) {
					e.Value.Output( output );
				}
				if( enums.Count > 0 ) output.LF( output.EnumAfterLinefeed );
				foreach( var c in classes ) {
					c.Value.Output( output );
				}
				if( classes.Count > 0 ) output.LF( output.ClassAfterLinefeed );
			}
		}

		public void Clear() {
			foreach( var e in enums ) e.Value.Clear();
			foreach( var c in classes ) c.Value.Clear();

			usings.Clear();
			namespaces.Clear();
			enums.Clear();
			classes.Clear();
		}

		public OutputSourceCode Using( string _namespace ) {
			usings.Add( _namespace );
			return this;
		}
		public OutputSourceCode Namespace( string _namespace ) {
			namespaces.Add( _namespace );
			return this;
		}
		public OutputSourceCodeClass Class( string _class, AccessibilityType accessibility = AccessibilityType.Public, bool _static = false ) {
			OutputSourceCodeClass c = null;
			if( !classes.TryGetValue( _class, out c ) ) {
				c = classes[_class] = new OutputSourceCodeClass( _class, accessibility, _static );
			}
			return c;
		}
		public OutputSourceCodeEnum Enum( string _enum, AccessibilityType accessibility = AccessibilityType.Public ) {
			OutputSourceCodeEnum e = null;
			if( !enums.TryGetValue( _enum, out e ) ) {
				e = enums[_enum] = new OutputSourceCodeEnum( _enum, accessibility );
			}
			return e;
		}

		void OutputUsing( OutputSourceSetting output ) {
			foreach( var ns in usings ) {
				output.WriteLine( $"using {ns};" );
			}
			if( usings.Count > 0 ) output.LF( OutputCommonSetting.UsingAfterLF );
		}

	}
}
