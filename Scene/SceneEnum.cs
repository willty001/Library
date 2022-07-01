/*
このファイルは、自動出力です。
*/
public enum SceneEnum {
	Battle,
	Title,
} // public enum SceneEnum.
public static class SceneUtility {
	public static string ScenePath( SceneEnum scene ) {
		switch( scene ) {
			case SceneEnum.Battle: return "Asset/Scene/Battle/Battle.unity";
			case SceneEnum.Title: return "Asset/Scene/Title/Title.unity";
			default: return "";
		} // switch( scene ).
	} // public static string ScenePath( SceneEnum scene ).
} // public static class SceneUtility.
