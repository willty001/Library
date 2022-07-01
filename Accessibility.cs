/*
 * C#のアクセシビリティの定義.
 */
using System;

namespace HS {
	public enum AccessibilityType {
		Public,
		Protected,
		Internal,
		ProtectedInternal,
		Private,
		PrivateProtected,
	}

	public static class AccessibilityExpansion {
		public static string ToCode( this AccessibilityType type ) {
			switch( type ) {
				case AccessibilityType.Public: return "public";
				case AccessibilityType.Protected: return "protected";
				case AccessibilityType.ProtectedInternal: return "protected internal";
				case AccessibilityType.Internal: return "internal";
				case AccessibilityType.Private: return "private";
				case AccessibilityType.PrivateProtected: return "private protected";
				default:
					// TODO: typeが不正.
					throw new Exception();
			}
		}
	}
}
