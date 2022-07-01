using System;

/* 仕様：
 * ・関数を登録する為のクラス.
 * ・Managerを介して登録キーに合致するものを一括処理したりする.
 * ・実行処理の登録に関しては Priority<T> クラスを利用.
 */

/// <summary> 実行処理の登録クラス. </summary>
/// <typeparam name="T"> 登録処理関数の引数型. </typeparam>
public class ActionElement<T> {
	/// <summary> キーの初期値. </summary>
	const int NO_KEY = 0x00000000;
	
	/// <summary> 登録タスク. </summary>
	private Priority<Action<T>> m_task = new Priority<Action<T>>();
	/// <summary> マネージャーの発行ID. </summary>
	private int m_id = IDManager.INVALID_ID;
	/// <summary> 登録キー. </summary>
	private int m_key_value = NO_KEY;

	/// <summary> 処理登録用クラス. </summary>
	/// <param name="id"> 発行id. </param>
	/// <param name="key_value"> 登録キー. </param>
	public ActionElement( int id,int key_value ) {
		m_id = id;
		m_key_value = key_value;
	}

	/// <summary> key の OR 検索. </summary>
	/// <param name="key"> OR 検索するキー. </param>
	/// <returns> 一致した true / 一致しない false. </returns>
	public bool Or( int key ) {
		return ( m_key_value & key ) > 0;
	}
	
	/// <summary> key の AND 検索. </summary>
	/// <param name="key"> AND 検索するキー. </param>
	/// <returns> 一致した true / 一致しない false. </returns>
	public bool And( int key ) {
		return ( m_key_value & key ) == key;
	}

	/// <summary> 処理の登録. </summary>
	/// <param name="action"> 登録する処理. </param>
	/// <param name="priority"> 優先度. </param>
	/// <returns> 登録 ID. </returns>
	public int Register( Action<T> action ) {
		return m_task.Add( action, Priority.NORMAL_PRIORITY );
	}

	/// <summary> 登録処理の実行. </summary>
	/// <param name="arg"> 実行時の引数. </param>
	public void Execute( T arg ) {
		m_task.HighestForEach( ( Action<T> action ) => {
			action( arg );
		} );
	}
}

