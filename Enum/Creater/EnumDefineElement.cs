using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace HS {

	[Serializable]
	public abstract class aEnumDefineElements {
		protected int count = -1;
		public abstract int Count { get; }
		public abstract IEnumerable<string> Keys();
	}

	[Serializable]
	public class EnumDefineElements : aEnumDefineElements {
		[SerializeField]
		List<EnumDefineElement> defines = new List<EnumDefineElement>();

		public override int Count { get { return ( count >= 0 ) ? count : count = defines.Count; } }
		public override IEnumerable<string> Keys() { return defines.Select( def => def.Key ); }
	}

	[Serializable]
	public class EnumDefineElementsInt : aEnumDefineElements {
		[SerializeField]
		List<EnumDefineElementInt> defines = new List<EnumDefineElementInt>();
		public List<EnumDefineElementInt> Defines { get { return defines; } }
		public bool TryGetValue( string key, out int value ) {
			for( int i = 0, num = Defines.Count; i < num; ++i ) {
				var define = defines[i];
				if( define.Key == key ) {
					value = define.Value;
					return true;
				}
			}
			value = 0;
			return false;
		}

		public override int Count { get { return ( count >= 0 ) ? count : count = defines.Count; } }
		public override IEnumerable<string> Keys() { return defines.Select( def => def.Key ); }
	}

	[Serializable]
	public class EnumDefineElementsFloat : aEnumDefineElements {
		[SerializeField]
		List<EnumDefineElementFloat> defines = new List<EnumDefineElementFloat>();
		public List<EnumDefineElementFloat> Defines { get { return defines; } }
		public bool TryGetValue( string key, out float value ) {
			for( int i = 0, num = Defines.Count; i < num; ++i ) {
				var define = defines[i];
				if( define.Key == key ) {
					value = define.Value;
					return true;
				}
			}
			value = 0;
			return false;
		}

		public override int Count { get { return ( count >= 0 ) ? count : count = defines.Count; } }
		public override IEnumerable<string> Keys() { return defines.Select( def => def.Key ); }
	}

	[Serializable]
	public class EnumDefineElementsDouble : aEnumDefineElements {
		[SerializeField]
		List<EnumDefineElementDouble> defines = new List<EnumDefineElementDouble>();
		public List<EnumDefineElementDouble> Defines { get { return defines; } }
		public bool TryGetValue( string key, out double value ) {
			for( int i = 0, num = Defines.Count; i < num; ++i ) {
				var define = defines[i];
				if( define.Key == key ) {
					value = define.Value;
					return true;
				}
			}
			value = 0;
			return false;
		}

		public override int Count { get { return ( count >= 0 ) ? count : count = defines.Count; } }
		public override IEnumerable<string> Keys() { return defines.Select( def => def.Key ); }
	}

	[Serializable]
	public class EnumDefineElementsChar : aEnumDefineElements {
		[SerializeField]
		List<EnumDefineElementChar> defines = new List<EnumDefineElementChar>();
		public List<EnumDefineElementChar> Defines { get { return defines; } }
		public bool TryGetValue( string key, out char value ) {
			for( int i = 0, num = Defines.Count; i < num; ++i ) {
				var define = defines[i];
				if( define.Key == key ) {
					value = define.Value;
					return true;
				}
			}
			value = (char)0;
			return false;
		}

		public override int Count { get { return ( count >= 0 ) ? count : count = defines.Count; } }
		public override IEnumerable<string> Keys() { return defines.Select( def => def.Key ); }
	}

	[Serializable]
	public class EnumDefineElementsString : aEnumDefineElements {
		[SerializeField]
		List<EnumDefineElementString> defines = new List<EnumDefineElementString>();
		public List<EnumDefineElementString> Defines { get { return defines; } }
		public bool TryGetValue( string key, out string value ) {
			for( int i = 0, num = Defines.Count; i < num; ++i ) {
				var define = defines[i];
				if( define.Key == key ) {
					value = define.Value;
					return true;
				}
			}
			value = null;
			return false;
		}

		public override int Count { get { return ( count >= 0 ) ? count : count = defines.Count; } }
		public override IEnumerable<string> Keys() { return defines.Select( def => def.Key ); }
	}

	[Serializable]
	public class EnumDefineElementsObject : aEnumDefineElements {
		[SerializeField]
		List<EnumDefineElementObject> defines = new List<EnumDefineElementObject>();
		public List<EnumDefineElementObject> Defines { get { return defines; } }
		public bool TryGetValue( string key, out UnityEngine.Object value ) {
			for( int i = 0, num = Defines.Count; i < num; ++i ) {
				var define = defines[i];
				if( define.Key == key ) {
					value = define.Value;
					return true;
				}
			}
			value = null;
			return false;
		}

		public override int Count { get { return ( count >= 0 ) ? count : count = defines.Count; } }
		public override IEnumerable<string> Keys() { return defines.Select( def => def.Key ); }
	}


	[Serializable]
	public class EnumDefineElement {
		/// <summary> 定義のキー. </summary>
		public string Key { get { return key; } }

		[SerializeField]
		string key = "";
	}
	[Serializable]
	public class EnumDefineElementInt : EnumDefineElement {
		/// <summary> 定義の値. </summary>
		public int Value { get { return value; } }

		[SerializeField]
		int value = 0;
	}
	[Serializable]
	public class EnumDefineElementFloat : EnumDefineElement {
		/// <summary> 定義の値. </summary>
		public float Value { get { return value; } }

		[SerializeField]
		float value = 0f;
	}
	[Serializable]
	public class EnumDefineElementDouble : EnumDefineElement {
		/// <summary> 定義の値. </summary>
		public double Value { get { return value; } }

		[SerializeField]
		double value = 0f;
	}
	[Serializable]
	public class EnumDefineElementChar : EnumDefineElement {
		/// <summary> 定義の値. </summary>
		public char Value { get { return value; } }

		[SerializeField]
		char value = (char)0;
	}
	[Serializable]
	public class EnumDefineElementString : EnumDefineElement {
		/// <summary> 定義の値. </summary>
		public string Value { get { return value; } }

		[SerializeField]
		string value = "";
	}

	[Serializable]
	public class EnumDefineElementObject : EnumDefineElement {
		/// <summary> 定義の値. </summary>
		public UnityEngine.Object Value { get { return value; } }

		[SerializeField]
		UnityEngine.Object value = null;
	}
}