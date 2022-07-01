/*
 * Enumソースコードの出力クラス.
 */
using System.Collections.Generic;
using System.Linq;

namespace HS {
	public class OutputSourceCodeEnum : IOutputSouce {
		string Name { get; set; } = null;
		AccessibilityType Accessibility { get; set; } = AccessibilityType.Public;

		Dictionary<string, int?> values = new Dictionary<string, int?>();

		public OutputSourceCodeEnum( string name, AccessibilityType accessibility ) {
			Name = name;
			Accessibility = accessibility;
		}

		public OutputSourceCodeEnum AddValue( string name, int? value = null ) {
			values.Add( name, value );
			return this;
		}
		public void Output( OutputSourceSetting output ) {
			var pairs = output.Sort? values.OrderBy( pair => pair.Key ) : values.Select( _ => _ );
			output.WriteLine( $"{Accessibility.ToCode()} enum {Name} {{" );
			using( output.CreateTabDisposable() ) {
				for( int i = 0, num = pairs.Count(); i < num; ++i ) {
					var pair = pairs.ElementAt( i );
					output.WriteLine( $"{pair.Key} = {pair.Value?? i}," );
				}
			}
			output.WriteLine( $"}} // {Accessibility.ToCode()} enum {Name}" );
		}

		public void Clear() {
			values.Clear();
		}
	}
}
