using System.Threading;

/* 仕様:
 * ・IDの発行時にはInterlockedロックを行う為、スレッドセーフ.
 * ・別のManagerのインスタンスは同じIDを発行する.
 */
/// <summary> IDの発行システム. </summary>
public class IDManager {

	/// <summary> IDの不整値. </summary>
	public const int INVALID_ID = -1;
	/// <summary> IDManagerのIDとして正常値か. </summary>
	/// <param name="check_id"> 判定するID. </param>
	/// <returns> 正常 true / 不正値 false. </returns>
	static public bool IsValidID( int check_id ) {
		return !IsInvalidID( check_id );
	}
	/// <summary> IDManagerのIDとして不正値か. </summary>
	/// <param name="check_id"> 判定するID. </param>
	/// <returns> 不正値 true / 正常 false. </returns>
	static public bool IsInvalidID( int check_id ) {
		return ( check_id == INVALID_ID );
	}
	
	/// <summary> 発行用兼、発行総数. </summary>
	private int m_id = 0;

	/// <summary> 新規ID発行. </summary>
	/// <returns> 発行したID値. </returns>
	public int IssueID() {
		return Interlocked.Increment( ref m_id );
	}
}
