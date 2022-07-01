#if UNITY_EDITOR
/*
 * ソースコードの出力クラス.
 */
using System.IO;
using UniRx;
using UnityEditor;
using UnityEngine;

namespace HS {
	[CustomPropertyDrawer( typeof( OutputSourceCode ) )]
	public class OutputSourceCodeEdior : PropertyDrawerBase<OutputSourceCodeEdior.Properties> {
		protected override Properties CreateProperties( SerializedProperty property ) {
			return new Properties( property );
		}

		public class Properties : PropertyDrawerProperties {
			const float BUTTON_WIDTH = 30;
			const float REFERENCE_RATE = 0.2f; // *2.
			const float DIRECTORY_RATE = 0.4f;
			const float FILENAME_RATE = 0.2f;
			SerializedProperty outputDirectory = null;
			SerializedProperty fileName = null;
			SerializedProperty sort = null;

			SerializedProperty OutputDirectory { get { return ( outputDirectory != null )? outputDirectory : outputDirectory = this[nameof(outputDirectory)]; } }
			SerializedProperty FileName { get { return ( fileName != null )? fileName : fileName = this[nameof( fileName )]; } }
			SerializedProperty Sort { get { return ( sort != null )? sort : sort = this[nameof( sort )]; } }

			string folderPath = null;
			Object folder = null;
			string filePath = null;
			Object file = null;

			public Properties( SerializedProperty property ) : base( property ) {
				UpdateFolder();
				UpdateFile();
			}

			void UpdateFolder() {
				string path = OutputDirectory.stringValue;
				if( folderPath != path ) {
					folderPath = path;
					folder = AssetDatabase.LoadAssetAtPath<Object>( path );
				}
			}
			void UpdateFile() {
				string path = $"{OutputDirectory.stringValue}/{FileName.stringValue}";
				if( filePath != path ) {
					filePath = path;
					file = AssetDatabase.LoadAssetAtPath<Object>( path );
				}
			}

			protected override void GUI( ref Rect position, GUIContent label ) {
				LabelGUI( label, ref position );

				SetColumnUse( BUTTON_WIDTH * 2 );

				DirectorySelectGUI( ref position );
				DirectoryGUI( ref position );

				FileNameSelectGUI( ref position );
				FileNameGUI( ref position );

				NextLine( ref position );

				"オプション".LabelGUI( ref position, Width );
				NextLine( ref position );

				using( new EditorGUI.IndentLevelScope() ) {
					SortGUI( ref position );
				}
			}
			void LabelGUI( GUIContent label, ref Rect position ) {
				label.text.LabelGUI( ref position, Width );
				NextLine( ref position );
			}
			void DirectorySelectGUI( ref Rect position ) {
				SetColumn( ref position, BUTTON_WIDTH );
				if( UnityEngine.GUI.Button( position, "■" ) ) {
					var select = EditorUtility.OpenFolderPanel( "", string.IsNullOrEmpty( OutputDirectory.stringValue )? Application.dataPath : OutputDirectory.stringValue, "" );
					if( !string.IsNullOrEmpty( select ) ) {
						OutputDirectory.stringValue = select.ToAssetPath();
						OutputDirectory.serializedObject.ApplyModifiedProperties();
						GUIUtility.ExitGUI();
					}
					UpdateFolder();
				}
				NextColumn( ref position );
			}
			void DirectoryGUI( ref Rect position ) {
				SetColumnRate( ref position, DIRECTORY_RATE );
				OutputDirectory.TextGUI( ref position );
				NextColumn( ref position );
				UpdateFolder();

				SetColumnRate( ref position, REFERENCE_RATE );
		
				folder.ObjectGUI( ref position, position.width, false );
			}

			void FileNameSelectGUI( ref Rect position ) {
				SetColumn( ref position, BUTTON_WIDTH );
				if( UnityEngine.GUI.Button( position, "■" ) ) {
					var select = EditorUtility.OpenFolderPanel( "", OutputDirectory.stringValue, FileName.stringValue );
					if( !string.IsNullOrEmpty( select ) ) {
						FileName.stringValue = Path.GetFileName( select );
						FileName.serializedObject.ApplyModifiedProperties();
						GUIUtility.ExitGUI();
					}
					UpdateFile();
				}
				NextColumn( ref position );
			}
			void FileNameGUI( ref Rect position ) {
				SetColumnRate( ref position, FILENAME_RATE );
				FileName.TextGUI( ref position );
				NextColumn( ref position );

				SetColumnRate( ref position, REFERENCE_RATE );
				file.ObjectGUI( ref position, position.width, false );
			}
			void SortGUI( ref Rect position ) {
				Sort.PropertyGUI( ref position );
			}
		}
	}
}
#endif