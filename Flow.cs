using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HS {
	public class Flow
	{
		/// <summary> 更新処理の状態. </summary>
		public enum UpdateRet {
			None,     // 特になし.
			Continue, // 継続.
			Break,    // 中断.
		};

		/// <summary> フローを実行する準備が整ったか. </summary>
		/// <returns> 実行可否. </returns>
		virtual public bool IsReady() {
			return true;
		}

		/// <summary> 開始時. </summary>
		virtual public void Start() {

		}

		/// <summary> 更新処理. </summary>
		/// <returns> 継続可否. </returns>
		virtual public UpdateRet Update() {
			return UpdateRet.Break;
		}

		/// <summary> 終了時. </summary>
		virtual public void Finish() {

		}

		/// <summary> フローを終了できるか. </summary>
		/// <returns> 終了可否. </returns>
		virtual public bool IsFinish() {
			return true;
		}
	}

	/// <summary> フロー処理マネージャー. </summary>
	public class FlowManager {
		/// <summary> 処理中フラグ. </summary>
		public bool IsProcessing {
			get;
			set;
		}

		private IDManager m_id_mgr = new IDManager();
		private SafeList<Flow> m_flow_list = new SafeList<Flow>();

		/// <summary> 初期化コンストラクタ. </summary>
		public FlowManager() {
			IsProcessing = false;
		}

		/// <summary> 処理. </summary>
		/// <returns> nullのみ. </returns>
		private IEnumerator Update() {
			IsProcessing = true;
			while( m_flow_list.Count > 0 ) {
				Flow now = m_flow_list[0];
				while( !now.IsReady() ) {
					yield return null;
				}
				now.Start();

				while( now.Update() == Flow.UpdateRet.Continue ) {
					yield return null;
				}

				while( !now.IsFinish() ) {
					yield return null;
				}
				now.Finish();

				yield return null;
				m_flow_list.RemoveAt( 0 );
				yield return null;
			}
			IsProcessing = false;
		}

		public void Add( Flow flow ) {
			m_flow_list.Add( flow );
			if( !IsProcessing ) {
				CoroutineHandler.Start( Update() );
			}
		}
	}
}