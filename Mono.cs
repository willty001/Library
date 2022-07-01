/*
 * HSライブラリのMonoクラス.
 * 実行時デバッグ機能などを提供予定.
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
