using System;
using UnityEngine;

namespace HS {
	/// <summary> パス関連の変換の拡張クラス. </summary>
	public static class PathExpansion {
		const string ROOT_FOLDER = "Assets";
		const int ROOT_FOLDER_LENGTH = 6;
		static string projectPath = null;
		static string ProjectPath { get { return ( projectPath != null )? projectPath : projectPath = Application.dataPath.Normalize().Replace( ROOT_FOLDER, "" ); } }
		static int assetPathStartIndex = -1;
		static int AssetPathStartIndex { get { return ( assetPathStartIndex >= 0 )? assetPathStartIndex : assetPathStartIndex = ProjectPath.Length; } }

		/// <summary> [拡張] スラッシュに統一する. </summary> 
		public static string Normalize( this string path ) {
			return path.Replace( '\\', '/' );
		}

		/// <summary> [拡張] フルパスを AssetDatabase のパスに変換する. </summary> 
		public static string ToAssetPath( this string path ) {
			path = path.Normalize();
			if( path.StartsWith( ProjectPath ) ) {
				path = path.Substring( AssetPathStartIndex );
			} else {
				throw new FormatException( path );
			}
			return path;
		}

		/// <summary> [拡張] AssetDatabase のパスをフルパスに変換する. </summary> 
		public static string ToFullPath( this string path ) {
			path = path.Normalize();
			if( path.StartsWith( ROOT_FOLDER ) ) {
				path = $"{ProjectPath}/{path}";
			} else {
				throw new FormatException( path );
			}
			return path;
		}
		/// <summary> [拡張] フルパスを AssetDatabase のパスに変換する. </summary> 
		public static string ToResourcesPath( this string path ) {
			path = path.Normalize();
			var split = path.Split( '/' );
			for( int i = 0, num = split.Length; i < num; ++i ) {
				var s = split[i];
				if( s == "Resources" ) {
					path = "";
					continue;
				}
				if( i == num - 1 ) s = s.Contains( "." )? s.Split( '.' )[0] : s;
				path = string.IsNullOrEmpty( path )? s : $"{path}/{s}";
			}

			return path;
		}
	}
}