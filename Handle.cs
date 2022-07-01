/*
 * HSライブラリのMonoクラス.
 * 実行時デバッグ機能などを提供予定.
 */ 
using UnityEngine;
using System;

namespace HS {
	public class Handle : IDisposable {
		protected bool Once { get; set; } = true; 
		protected Action Action { get; set; } = null;

		protected Handle() {} 
		public Handle( Action action, bool once = true ) {
			Action = action;
			Once = once;
		}
		public void Dispose() {
			Action?.Invoke();
			if( Once ) Action = null;
		}
	}
}
