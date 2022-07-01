using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class SafeList<T> : List<T> {
	private object m_lock = new object();

	/// <summary> ロックを使用するAdd. </summary>
	/// <param name="item"> 追加する要素. </param>
	new public void Add( T item ) {
		lock( m_lock ) {
			base.Add( item );
		}
	}

	/// <summary> ロックを使用するRemove. </summary>
	/// <param name="item"> 削除する要素. </param>
	new public bool Remove( T item ) {
		bool result = false;
		lock( m_lock ) {
			result = base.Remove( item );
		}
		return result;
	}

	/// <summary> ロックを使用するRemoveAll. </summary>
	/// <param name="match"> 削除条件. </param>
	/// <returns> 削除数. </returns>
	new public int RemoveAll( Predicate<T> match ) {
		int result = 0;
		lock( m_lock ) {
			result = base.RemoveAll( match );
		}
		return result;
	}

	/// <summary> ロックを使用するRemoveAt. </summary>
	/// <param name="index"> 削除番号. </param>
	new public void RemoveAt( int index ) {
		lock( m_lock ) {
			base.RemoveAt( index );
		}
	}

	/// <summary> ロックを使用するRemoveRange. </summary>
	/// <param name="index"> 削除番号. </param>
	/// <param name="count"> 削除数. </param>
	new public void RemoveRange( int index, int count ) {
		lock( m_lock ) {
			base.RemoveRange( index, count );
		}
	}

	/// <summary> ロックを使用するClear. </summary>
	new public void Clear() {
		lock( m_lock ) {
			base.Clear();
		}
	}
}
