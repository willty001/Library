using System;
using System.Collections.Generic;
using UnityEngine;

namespace HS {
	class EventManager : Singleton<EventManager> {
		/// <summary> 管理ID発酵用マネージャー </summary>
		private IDManager m_id_mgr = new IDManager();

		/// <summary> イベントトリガーのリスト. </summary>
		private SafeList<Trigger> m_trigger_list = new SafeList<Trigger>();
		/// <summary> トリガーが発生した時の呼び出しリスナー. </summary>
		private Dictionary<int,SafeList<Listener>> m_listener_list = new Dictionary<int, SafeList<Listener>>();

		/// <summary> コンストラクタ. </summary>
		private EventManager() : base() {
		}

		/// <summary> Triggerからのイベント発生通知. </summary>
		/// <param name="trigger"> 呼び出したTrigger. </param>
		private void Nortify( Trigger trigger, object obj ) {
			int event_type = trigger.EventType;
			SafeList<Listener> listener_list;
			if( m_listener_list.TryGetValue( event_type, out listener_list ) ) {
				listener_list.ForEach( ( Listener listener ) => {
					listener.Triggered( obj );
				} );
			}
		}

		/// <summary> イベントトリガーの作成. </summary>
		/// <param name="event_type"> ゲーム任意のイベント種別. </param>
		/// <returns> イベントトリガーとなるトリガークラス. </returns>
		public Trigger CreateTrigger( int event_type ) {
			return CreateTrigger( event_type, typeof( Trigger ) );
		}
		/// <summary> イベントトリガーの作成. </summary>
		/// <param name="event_type"> ゲーム任意のイベント種別. </param>
		/// <param name="arg_type"> トリガー時の引数の型. </param>
		/// <returns> イベントトリガーとなるトリガークラス. </returns>
		public Trigger CreateTrigger( int event_type, Type arg_type ) {
			int id = m_id_mgr.IssueID();
			Trigger trigger = new Trigger( id, event_type, arg_type );
			m_trigger_list.Add( trigger );
			return trigger;
		}

		/// <summary> イベントトリガーに対するリスナーの作成. </summary>
		/// <param name="event_type"> ゲーム任意のイベント種別. </param>
		/// <param name="action"> トリガーが発生した際の処理. </param>
		/// <returns> リスナークラス. </returns>
		public Listener CreateListener( int event_type, Action<object> action ) {
			int id = m_id_mgr.IssueID();
			Listener listener = new Listener( id, event_type, action );
			SafeList<Listener> listener_list;
			if( m_listener_list.TryGetValue( event_type, out listener_list ) ) {
				m_listener_list[event_type].Add( listener );
			} else {
				listener_list = new SafeList<Listener>();
				listener_list.Add( listener );
				m_listener_list.Add( event_type, listener_list );
			}
			return listener;
		}

		/// <summary> イベントの発生を通知するトリガークラス. </summary>
		public class Trigger {
			private int m_id = IDManager.INVALID_ID;
			Type m_arg_type;

			/// <summary> 自身のイベント種別. </summary>
			public int EventType {
				get;
				private set;
			}

			/// <summary> 初期設定コンストラクタ. </summary>
			/// <param name="id"> 一意の識別子. </param>
			/// <param name="event_type"> ゲーム規定のイベント番号. </param>
			/// <param name="arg_type"> 引数の型. </param>
			public Trigger( int id, int event_type, Type arg_type ) {
				m_id = id;
				EventType = event_type;
				m_arg_type = arg_type;
			}

			/// <summary> イベント発生. </summary>
			public void Pull() {
				Pull( null );
			}
			/// <summary> 引数付きイベント発生. </summary>
			/// <param name="arg"> リスナー登録時に設定した型の引数. </param>
			public void Pull( object arg ) {
				if( arg == null ) {
					Type type = arg.GetType();
					if( String.Compare( type.Name, m_arg_type.Name ) != 0 ) {
						Debug.LogError( "Event Trigger's Argument Invalid! Arg:" + type.Name + " Registered: " + m_arg_type.Name );
					}
				}
				EventManager.Instance.Nortify( this, arg );
			}

		}
		/// <summary> イベントリスナー </summary>
		public class Listener {
			private int m_id = IDManager.INVALID_ID;
			private int m_event_type = -1;
			/// <summary> イベント実行時に呼ぶ処理. </summary>
			public Action<object> Triggered {
				get;
				private set;
			}

			/// <summary> 初期設定コンストラクタ. </summary>
			/// <param name="id"> 一意の識別子. </param>
			/// <param name="event_type"> ゲーム規定のイベント番号. </param>
			/// <param name="triggered"> イベント発生時の処理関数. </param>
			public Listener( int id, int event_type, Action<object> triggered ) {
				m_id = id;
				m_event_type = event_type;
				Triggered = triggered;
			}
		}
	}
}