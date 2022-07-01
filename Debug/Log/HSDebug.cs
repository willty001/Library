using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace HS {
	public class HSDebug {
//		SerializableDictionary<int,bool> keyValuePairs = new SerializableDictionary<int, bool>();
		public static bool drawGUI = false;

		const DebugType DefaultDebugType = DebugType.System;

		/// <summary> 一時的テストに使用するログ. </summary>
		[System.Diagnostics.Conditional("DEBUG")]
		public static void TestLog( string format, params string[] args ) {
			format = $"■■■<color=red>{format}</color>";
			Log( format, args );
		}

		/// <summary> 通常ログ. </summary>
		public static void Log( string format, params string[] args ) {
			Log( DefaultDebugType, format, args );
		}
		/// <summary> 注意ログ. </summary>
		public static void Log( MessageType message, params string[] args ) {
			Log( DefaultDebugType, message, args );
		}
		/// <summary> 注意ログ. </summary>
		public static void Log( DebugType type, MessageType message, params string[] args ) {
			string format = SystemMessage.GetMessage( message );
			Log( type, format, args );
		}
		/// <summary> 通常ログ. </summary>
		public static void Log( DebugType type, string format, params string[] args ) {
			if( args == null ) format = string.Format( format, args );
			Debug.Log( format );
		}

		/// <summary> 注意ログ. </summary>
		public static void LogWarning( string format, params string[] args ) {
			LogWarning( DefaultDebugType, format, args );
		}
		/// <summary> 注意ログ. </summary>
		public static void LogWarning( MessageType message, params string[] args ) {
			LogWarning( DefaultDebugType, message, args );
		}
		/// <summary> 注意ログ. </summary>
		public static void LogWarning( DebugType type, MessageType message, params string[] args ) {
			string format = SystemMessage.GetMessage( message );
			LogWarning( type, format, args );
		}
		/// <summary> 注意ログ. </summary>
		public static void LogWarning( DebugType type, string format, params string[] args ) {
			if( args == null ) format = string.Format( format, args );
			Debug.LogWarning( format );
		}
	}
}
