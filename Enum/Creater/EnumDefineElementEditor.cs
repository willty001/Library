#if UNITY_EDITOR

using UnityEngine;
using UnityEditor;

namespace HS {
	[CustomPropertyDrawer( typeof( EnumDefineElement ) )]
	public class EnumDefineElementDrawerProperties : PropertyDrawerProperties {
		SerializedProperty key = null;
		protected SerializedProperty Key { get { return ( key != null )? key : key = this[nameof( key )]; } }

		protected float WidthRateKey { get; set; } = 1.0f;

		public EnumDefineElementDrawerProperties( SerializedProperty serializedProperty ) : base( serializedProperty ) { }

		protected override void GUI( ref Rect position, GUIContent label ) {
			KeyGUI( ref position );
		}
		protected void KeyGUI( ref Rect position ) {
			SetColumnRate( ref position, WidthRateKey );
			Key.TextGUI( ref position );
			NextColumn( ref position );
		}
	}

	public class EnumDefineElementDrawerPropertiesBase : EnumDefineElementDrawerProperties {
		SerializedProperty value = null;
		protected SerializedProperty Value { get { return ( value != null )? value : value = this[nameof( value )]; } }
		protected float WidthRateValue { get; set; } = 0.5f;

		public EnumDefineElementDrawerPropertiesBase( SerializedProperty serializedProperty ) : base( serializedProperty ) {
			WidthRateKey = 0.5f;
		}

		protected override void GUI( ref Rect position, GUIContent label ) {
			base.GUI( ref position, label );
			ValueGUI( ref position );
		}

		protected virtual void ValueGUI( ref Rect position ) {
			SetColumnRate( ref position, WidthRateValue );
			Value.PropertyGUI( ref position );
			NextColumn( ref position );
		}
	}

	[CustomPropertyDrawer( typeof( EnumDefineElementInt ) )]
	public class EnumDefineElementIntDrawerProperties : EnumDefineElementDrawerPropertiesBase {
		public EnumDefineElementIntDrawerProperties( SerializedProperty serializedProperty ) : base( serializedProperty ){}

		protected override void ValueGUI( ref Rect position ) {
			SetColumnRate( ref position, WidthRateValue );
			Value.IntGUI( ref position );
			NextColumn( ref position );
		}
	}

	[CustomPropertyDrawer( typeof( EnumDefineElementFloat ) )]
	public class EnumDefineElementFloatDrawerProperties : EnumDefineElementDrawerPropertiesBase {
		public EnumDefineElementFloatDrawerProperties( SerializedProperty serializedProperty ) : base( serializedProperty ) { }

		protected override void ValueGUI( ref Rect position ) {
			SetColumnRate( ref position, WidthRateValue );
			Value.FloatGUI( ref position );
			NextColumn( ref position );
		}
	}

	[CustomPropertyDrawer( typeof( EnumDefineElementDouble ) )]
	public class EnumDefineElementDoubleDrawerProperties : EnumDefineElementDrawerPropertiesBase {
		public EnumDefineElementDoubleDrawerProperties( SerializedProperty serializedProperty ) : base( serializedProperty ) { }

		protected override void ValueGUI( ref Rect position ) {
			SetColumnRate( ref position, WidthRateValue );
			Value.DoubleGUI( ref position );
			NextColumn( ref position );
		}
	}

	[CustomPropertyDrawer( typeof( EnumDefineElementChar ) )]
	public class EnumDefineElementCharDrawerProperties : EnumDefineElementDrawerPropertiesBase {
		public EnumDefineElementCharDrawerProperties( SerializedProperty serializedProperty ) : base( serializedProperty ) { }

		protected override void ValueGUI( ref Rect position ) {
			SetColumnRate( ref position, WidthRateValue );
			Value.PropertyGUI( ref position );
			NextColumn( ref position );
		}
	}

	[CustomPropertyDrawer( typeof( EnumDefineElementString ) )]
	public class EnumDefineElementStringDrawerProperties : EnumDefineElementDrawerPropertiesBase {
		public EnumDefineElementStringDrawerProperties( SerializedProperty serializedProperty ) : base( serializedProperty ) { }

		protected override void ValueGUI( ref Rect position ) {
			SetColumnRate( ref position, WidthRateValue );
			Value.TextGUI( ref position );
			NextColumn( ref position );
		}
	}

	[CustomPropertyDrawer( typeof( EnumDefineElementObject ) )]
	public class EnumDefineElementObjectDrawerProperties : EnumDefineElementDrawerPropertiesBase {
		public EnumDefineElementObjectDrawerProperties( SerializedProperty serializedProperty ) : base( serializedProperty ) { }

		protected override void ValueGUI( ref Rect position ) {
			SetColumnRate( ref position, WidthRateValue );
			Value.ObjectGUI<Object>( ref position, false );
			NextColumn( ref position );
		}
	}

	[CustomPropertyDrawer( typeof( EnumDefineElement ) )]
	public class EnumDefineElementDrawer : PropertyDrawerBase<EnumDefineElementDrawerProperties> {
		protected override EnumDefineElementDrawerProperties CreateProperties( SerializedProperty property ) {
			return new EnumDefineElementDrawerProperties( property );
		}
	}

	[CustomPropertyDrawer( typeof( EnumDefineElementInt ) )]
	public class EnumDefineElementIntDrawer : PropertyDrawerBase<EnumDefineElementIntDrawerProperties> {
		protected override EnumDefineElementIntDrawerProperties CreateProperties( SerializedProperty property ) {
			return new EnumDefineElementIntDrawerProperties( property );
		}
	}

	[CustomPropertyDrawer( typeof( EnumDefineElementFloat ) )]
	public class EnumDefineElementFloatDrawer : PropertyDrawerBase<EnumDefineElementFloatDrawerProperties> {
		protected override EnumDefineElementFloatDrawerProperties CreateProperties( SerializedProperty property ) {
			return new EnumDefineElementFloatDrawerProperties( property );
		}
	}

	[CustomPropertyDrawer( typeof( EnumDefineElementDouble ) )]
	public class EnumDefineElementDoubleDrawer : PropertyDrawerBase<EnumDefineElementDoubleDrawerProperties> {
		protected override EnumDefineElementDoubleDrawerProperties CreateProperties( SerializedProperty property ) {
			return new EnumDefineElementDoubleDrawerProperties( property );
		}
	}

	[CustomPropertyDrawer( typeof( EnumDefineElementChar ) )]
	public class EnumDefineElementCharDrawer : PropertyDrawerBase<EnumDefineElementCharDrawerProperties> {
		protected override EnumDefineElementCharDrawerProperties CreateProperties( SerializedProperty property ) {
			return new EnumDefineElementCharDrawerProperties( property );
		}
	}

	[CustomPropertyDrawer( typeof( EnumDefineElementString ) )]
	public class EnumDefineElementStringDrawer : PropertyDrawerBase<EnumDefineElementStringDrawerProperties> {
		protected override EnumDefineElementStringDrawerProperties CreateProperties( SerializedProperty property ) {
			return new EnumDefineElementStringDrawerProperties( property );
		}
	}

	[CustomPropertyDrawer( typeof( EnumDefineElementObject ) )]
	public class EnumDefineElementObjectDrawer : PropertyDrawerBase<EnumDefineElementObjectDrawerProperties> {
		protected override EnumDefineElementObjectDrawerProperties CreateProperties( SerializedProperty property ) {
			return new EnumDefineElementObjectDrawerProperties( property );
		}
	}

}
#endif