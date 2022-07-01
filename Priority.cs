using System;
using System.Collections.Generic;

/* 仕様：
 * ・Template型で指定したデータに大して優先度をつけ登録できる.
 * ・登録時の発行IDの使用して個別操作（削除・優先度変更）を行う.
 * ・登録優先度の最高のデータを全取得できる.
 * ・登録優先順にデータを全取得できる.
 */ 

/// <summary> 固定値の取得用にベースとして使用. </summary>
public class Priority {
	/// <summary> 無効プライオリティ. </summary>
	public const char INVALID_PRIORITY = (char)0;
	/// <summary> 低プライオリティ. </summary>
	public const char LOW_PRIORITY     = (char)32;
	/// <summary> 通常プライオリティ. </summary>
	public const char NORMAL_PRIORITY  = (char)64;
	/// <summary> 高プライオリティ. </summary>
	public const char high_priority    = (char)96;
}

/// <summary> 優先度処理クラス. </summary>
/// <typeparam name="T"> 扱いたいデータ. </typeparam>
public class Priority<T> : Priority {

	/// <summary> IDの管理クラス. </summary>
	private IDManager m_id_mgr = new IDManager();

	/// <summary> 管理情報. </summary>
	private struct Set {
		/// <summary> 主データ. </summary>
		public T m_data;
		/// <summary> Priorityクラスのインスタンス内の一意なID. </summary>
		public int m_id;
		/// <summary> 登録優先度. </summary>
		public char m_priority;
	};

	/// <summary> 管理情報リスト. </summary>
	private SafeList<Set> m_data_list = new SafeList<Set>();

	/// <summary> データの追加. </summary>
	/// <param name="data"> 登録データ. </param>
	/// <param name="priority"> 優先度. </param>
	/// <returns> 発行ID. </returns>
	public int Add( T data, char priority ) {
		Set set = new Set(); 

		set.m_id       = m_id_mgr.IssueID();
		set.m_priority = priority;
		set.m_data     = data;

		m_data_list.Add( set );

		return set.m_id;
	}

	/// <summary> 最高Priorityの処理のみを呼び出す. </summary>
	/// <param name="action"> 呼び出す処理. </param>
	/// <param name="done_remove"> 処理完了後にリストから削除するか. </param>
	public void HighestForEach( Action<T> action, bool done_remove = false ) {
		if( m_data_list.Count == 0 ) return;

		HighestSort();
		char highest = m_data_list[0].m_priority;

		for( int i = 0, num = m_data_list.Count; i < num; ++i ) { 
			if( highest == m_data_list[i].m_priority ) {
				action( m_data_list[i].m_data );
			}
			if( done_remove ) {
				m_data_list.RemoveAt( i );
				--i;
				--num;
			}
		}
	}

	/// <summary> 高い順ソート. </summary>
	private void HighestSort() {
		m_data_list.Sort( ( Set a, Set b ) => {
			return a.m_priority - b.m_priority;
		} );
	}
}
