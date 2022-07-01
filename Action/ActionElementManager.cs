/* 仕様：
 * ・ActionElement クラスのインスタンスを発行するためのマネージャークラス.
 * ・登録時の key を利用して一括処理などを行う.
 * ・key は bit 演算で利用する.
 */
/// <summary> ActionElement を管理するマネージャ. </summary>
public class ActionElementManager {
	
	/// <summary> ID 発行用マネージャ. </summary>
	static private IDManager id_manager = new IDManager();

	/// <summary> ActionElement の発行. </summary>
	/// <typeparam name="T"> 利用する関数の引数型. </typeparam>
	/// <param name="key_value"> 登録 key 値. </param>
	/// <returns> 発行した ActionElement. </returns>
	static public ActionElement<T> CreateElement<T>( int key_value ) {
		int elemenet_id = id_manager.IssueID();
		return new ActionElement<T>( elemenet_id, key_value );
	}
}
