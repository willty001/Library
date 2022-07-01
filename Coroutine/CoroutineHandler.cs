using UnityEngine;
using System.Collections;

namespace HS {
	/// <summary> staticなCoroutineの呼び出しを行うクラス. </summary>
	public class CoroutineHandler : MonoSingleton<CoroutineHandler> {

		/// <summary> Coroutineの開始. </summary>
		/// <param name="enumerator"> 登録する処理. </param>
		/// <returns> 実行Coroutine. </returns>
		static public Coroutine Start( IEnumerator enumerator ) {
			return Instance.StartCoroutine( enumerator );
		}
	}
}