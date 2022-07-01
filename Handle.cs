/*
 * HS���C�u������Mono�N���X.
 * ���s���f�o�b�O�@�\�Ȃǂ�񋟗\��.
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
