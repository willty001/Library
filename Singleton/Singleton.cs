using UnityEngine;

namespace HS {

	public class Singleton<T> where T : Singleton<T> {
		static private T instance = null;

		static public T Instance {
			get {
				if( instance == null ) {
					instance = default( T );
				}
				return instance;
			}
		}

		protected Singleton() {
		}
	}

	public class MonoSingleton<T> : Mono where T : Mono {
		static T instance = null;
		static GameObject m_gameObject = null;

		static protected T Instance {
			get {
				if( !Application.isPlaying ) {
					HSDebug.LogWarning( MessageType.KeyNotExist );
					return null;
				}
				if( instance == null ) {
					var type = typeof(T);
					m_gameObject = new GameObject( type.Name, type );
					DontDestroyOnLoad( m_gameObject );
					instance = m_gameObject.GetComponent<T>();
				}
				return instance;
			}
		}
		static protected bool Instanted { get { return instance != null; } }

		public new void OnDestroy() {
			if( instance ) {
				instance = null;
				m_gameObject = null;
			}
			base.OnDestroy();
		}
	}
	public class ScriptableSingleton<T> : ScriptableObject where T : ScriptableSingleton<T> {
	
		private static T instance;
		protected static T LoadFrom( string path ) {
			//初アクセス時にロードする
			if( instance == null ){ 
			  instance = Resources.Load<T>( path );
		
			  //ロード出来なかった場合はエラーログを表示
			  if( instance == null ) {
				Debug.LogError( path + " not found");
			  }
			}
		
			return instance;
		}
	}
}