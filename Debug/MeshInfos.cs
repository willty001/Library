using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[RequireComponent( typeof(MeshFilter), typeof(MeshRenderer))]
public class MeshInfos : MonoBehaviour {
	[SerializeField]
	MeshFilter meshFilter;
	[SerializeField]
	MeshRenderer meshRenderer;

	public MeshFilter Filter { get { return meshFilter; } set { meshFilter = value; } }
	public MeshRenderer Renderer { get { return meshRenderer; } set { meshRenderer = value; } }

    void Reset() {
        meshFilter = GetComponent<MeshFilter>();
		meshRenderer = GetComponent<MeshRenderer>(); 
    }

}
#if UNITY_EDITOR
[CustomEditor( typeof(MeshInfos) )]
public class MeshInfosEditor : Editor {
	Dictionary<string,bool> foldouts = new Dictionary<string, bool>();
	
	public override void OnInspectorGUI() {
		base.OnInspectorGUI();

		var script = target as MeshInfos;
		var mesh = script.Filter.sharedMesh;
	}

//	bool Matrix4x4GUI( string label, Matrix4x4 matrix ) {
//		EditorGUILayout.Foldout(  label );
//	}
}
#endif