/*
 * HS���C�u������Mono�N���X.
 * ���s���f�o�b�O�@�\�Ȃǂ�񋟗\��.
 */ 
using UnityEngine;
using System;

namespace HS {
	public class Mono : MonoBehaviour {
		public event Action AddOnDestroy = null; 
		
		protected void OnDestroy() {
			AddOnDestroy?.Invoke();
			AddOnDestroy = null;
		}
	}
}
