/*
 * GUIŒn‚ÌScopeƒNƒ‰ƒX.
 */ 

using System;

namespace HS {
	public class HorizontalGUIScope : GUIScope {
		GUICommon common = null;
		public HorizontalGUIScope( GUICommon common ) {
			this.common = common;
			this.common?.BeginHorizontal();
		}
		public override void Dispose() {
			common?.EndHorizontal();
		}
	}
}