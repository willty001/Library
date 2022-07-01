/*
 * SerializeFieldâ¬î\Ç»é´èëÉNÉâÉX.
 */
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace HS {

	[System.Serializable]
	public partial class SerializableDictionary<TKey,TValue> : ICollection<KeyValuePair<TKey, TValue>>, IEnumerable<KeyValuePair<TKey, TValue>>, IEnumerable, IDictionary<TKey, TValue>, IReadOnlyCollection<KeyValuePair<TKey, TValue>>, IReadOnlyDictionary<TKey, TValue>, ICollection, IDictionary{
		[System.Serializable]
		public class Pair {
			public TKey Key;
			public TValue Value;
		}
		public List<Pair> datas = new List<Pair>();

		public int Count { get { return datas.Count; } }

		public bool IsReadOnly { get; set; } = false;

		public ICollection<TKey> Keys { get { return datas.Select( d => d.Key ).ToArray(); } }

		public ICollection<TValue> Values { get { return datas.Select( d => d.Value ).ToArray(); } }

		IEnumerable<TKey> IReadOnlyDictionary<TKey, TValue>.Keys => datas.Select( d => d.Key );

		IEnumerable<TValue> IReadOnlyDictionary<TKey, TValue>.Values => datas.Select( d => d.Value );

		public bool IsSynchronized { get; set; } = false;

		public object SyncRoot { get { return datas; } }

		public bool IsFixedSize => throw new NotImplementedException();

		ICollection IDictionary.Keys => throw new NotImplementedException();

		ICollection IDictionary.Values => throw new NotImplementedException();

		public object this[object key] { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

		public TValue this[TKey key] {
			get {
				TValue find = default;
				for( int i = 0, num = Count; i < num; ++i ) {
					var data = datas[i];
					if( EqualityComparer<TKey>.Default.Equals( data.Key, key ) ) {
						find = data.Value;
						break;
					}
				}
				return find;
			}
			set {
				for( int i = 0, num = Count; i < num; ++i ) {
					var data = datas[i];
					if( EqualityComparer<TKey>.Default.Equals( data.Key, key ) ) {
						data.Value = value;
						break;
					}
				}
			}
		}

		public void Add( KeyValuePair<TKey, TValue> item ) {
			if( ContainsKey( key => EqualityComparer<TKey>.Default.Equals( key, item.Key ) ) ) {
				HSDebug.LogWarning( MessageType.KeyAlreadyExist, item.Key.ToString() );
				return;
			}
			datas.Add( new Pair(){ Key = item.Key, Value = item.Value } );
		}

		public void Clear() {
			datas.Clear();
		}

		public bool Contains( KeyValuePair<TKey, TValue> item ) {
			return Contains( d => EqualityComparer<TKey>.Default.Equals( d.Key, item.Key ) && EqualityComparer<TValue>.Default.Equals( d.Value, item.Value ) );
		}
		public bool Contains( System.Predicate<Pair> match ) {
			for( int i = 0, num = Count; i < num; ++i ) {
				Pair data = datas[i];
				if( match( data ) ) return true;
			}
			return false;
		}
		public bool ContainsKey( System.Predicate<TKey> match ) {
			for( int i = 0, num = Count; i < num; ++i ) {
				Pair data = datas[i];
				if( match( data.Key ) ) return true;
			}
			return false;
		}

		public void CopyTo( KeyValuePair<TKey, TValue>[] array, int arrayIndex ) {
			int num = array.Length;
			if( num < arrayIndex ) arrayIndex = num;

			for( int i = 0; i < num; ++i ) {
				var pair = array[i];
				datas.Insert( arrayIndex + i, new Pair(){ Key = pair.Key, Value = pair.Value } );
			}	
		}

		public bool Remove( KeyValuePair<TKey, TValue> item ) {
			return datas.RemoveAll( d => EqualityComparer<TKey>.Default.Equals( d.Key, item.Key ) && EqualityComparer<TValue>.Default.Equals( d.Value, item.Value ) ) > 0;
		}

		public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator() {
			var data = datas.GetEnumerator();
			yield return new KeyValuePair<TKey, TValue>( data.Current.Key, data.Current.Value );
		}

		IEnumerator IEnumerable.GetEnumerator() {
			yield return datas.GetEnumerator();
		}

		public void Add( TKey key, TValue value ) {
			if( ContainsKey( key ) ) return;
			datas.Add( new Pair(){ Key = key, Value = value } );
		}

		public bool ContainsKey( TKey key ) {
			return Contains( d => EqualityComparer<TKey>.Default.Equals( d.Key, key ) );
		}

		public bool Remove( TKey key ) {
			return datas.RemoveAll( d => EqualityComparer<TKey>.Default.Equals( d.Key, key ) ) > 0;
		}

		public bool TryGetValue( TKey key, out TValue value ) {
			value = default;
			
			for( int i = 0, num = Count; i < num; ++i ) {
				Pair data = datas[i];
				if( EqualityComparer<TKey>.Default.Equals( data.Key, key ) ) {
					value = data.Value;
					return true;
				}
			}
			return false;
		}

		public void CopyTo( Array array, int index ) {
//			datas.CopyTo( array, index );
		}

		public void Add( object key, object value ) {
			Add( (TKey)key, (TValue)value );
		}

		public bool Contains( object key ) {
			return ContainsKey( (TKey)key );
		}

		IDictionaryEnumerator IDictionary.GetEnumerator() {
			return null;
		}

		public void Remove( object key ) {
			Remove( (TKey)key );
		}
	}
}
