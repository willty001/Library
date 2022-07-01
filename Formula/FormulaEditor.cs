using System;
using UnityEditor;
using UnityEngine;

namespace HS {
	[CustomPropertyDrawer( typeof( FormulaPart ) )]
	public class FormulaPartDrawer : InspectorDrawer<FormulaPart> {
		public class FormulaPartProperties : DrawerProperties {
			Property<FormulaPart> plus = null;
			Property<FormulaPart> coefficient = null;
			Property<FormulaPart> variable = null;
			Property<FormulaPart> multiplier = null;

			bool Plus {
				get { 
					if( plus == null ) plus = this["plus"];
					return plus.boolValue;
				}
				set {
					if( plus == null ) plus = this["plus"];
					plus.boolValue = value;
				}
			}
			int Coefficient {
				get {
					if( coefficient == null ) coefficient =  this["coefficient"];
					return coefficient.intValue;
				}
				set {
					if( coefficient == null ) coefficient =  this["coefficient"];
					coefficient.intValue = value;
				}
			}
			string Variable {
				get {
					if( variable == null ) variable =  this["variable"];
					return variable.stringValue;
				}
				set {
					if( variable == null ) variable =  this["variable"];
					variable.stringValue = value;
				}
			}
			int Multiplier {
				get {
					if( multiplier == null ) multiplier =  this["multiplier"];
					return variable.intValue;
				}
				set {
					if( multiplier == null ) multiplier =  this["multiplier"];
					variable.intValue = value;
				}
			}


			public FormulaPartProperties( Properties<FormulaPart> properties ) : base( properties ) {}

			public override void OnInspectorGUI( GUICommon gui ) {
				using( gui.CreateHorizontalScope() ) {
					gui.Label( Plus ? "+" : "-" );
					Coefficient = gui.IntField( Coefficient );
					Variable = gui.TextField( Variable );
					Multiplier = gui.IntField( Multiplier );
				}
			}
		}
		protected override DrawerProperties CreateProperties( Properties<FormulaPart> properties ) {
			return new FormulaPartProperties( properties );
		}
	}
}