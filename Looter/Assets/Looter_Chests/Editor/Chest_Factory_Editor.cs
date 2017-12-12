using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Chest_Factory))]
public class chest_Factory_Editor : Editor
{
    override public void OnInspectorGUI()
    {
        Chest_Factory myFactory = (Chest_Factory)target;

        EditorGUI.BeginChangeCheck();
        GUILayout.BeginHorizontal();
        myFactory.randomiseOnLoad = GUILayout.Toggle(myFactory.randomiseOnLoad, "Randomise on Load");
        GUILayout.EndHorizontal();

        GUILayout.Label("Base: ");
        GUILayout.BeginVertical();
        GUILayout.BeginHorizontal();
        if (GUILayout.Button(new GUIContent("\u25C1", "Previous Base")))
        {
            myFactory.baseIndex--;
            if (myFactory.baseIndex < 0)
            {
                myFactory.baseIndex = myFactory.chest_Bases.Length - 1;
            }
            myFactory.setBase(myFactory.baseIndex);
        }

        if (GUILayout.Button(new GUIContent("\u25B7", "Next Base")))
        {
            myFactory.baseIndex++;
            if (myFactory.baseIndex > myFactory.chest_Bases.Length - 1)
            {
                myFactory.baseIndex = 0;
            }
            myFactory.setBase(myFactory.baseIndex);
        }
        GUILayout.EndHorizontal();
        GUILayout.EndVertical();

        GUILayout.Space(20);

        GUILayout.Label("Lid: ");
        GUILayout.BeginVertical();
        GUILayout.BeginHorizontal();
        if (GUILayout.Button(new GUIContent("\u25C1", "Previous Lid")))
        {
            myFactory.lidIndex--;
            if (myFactory.lidIndex < 0)
            {
                myFactory.lidIndex = myFactory.chest_Lids.Length - 1;
            }
            myFactory.setLid(myFactory.lidIndex);
        }
        if (GUILayout.Button(new GUIContent("\u25B7", "Next Lid")))
        {
            myFactory.lidIndex++;
            if (myFactory.lidIndex > myFactory.chest_Lids.Length - 1)
            {
                myFactory.lidIndex = 0;
            }
            myFactory.setLid(myFactory.lidIndex);
        }
        GUILayout.EndHorizontal();
        GUILayout.EndVertical();

        GUILayout.Space(20);

        GUILayout.Label("Latch: ");
        GUILayout.BeginHorizontal();
        if (GUILayout.Button(new GUIContent("\u25C1", "Previous Latch")))
        {
            myFactory.latchIndex--;
            if (myFactory.latchIndex < 0)
            {
                myFactory.latchIndex = myFactory.chest_Latches.Length - 1;
            }
            myFactory.setLatch(myFactory.latchIndex);
        }
        if (GUILayout.Button(new GUIContent("\u25B7", "Next Latch")))
        {
            myFactory.latchIndex++;
            if (myFactory.latchIndex > myFactory.chest_Latches.Length - 1)
            {
                myFactory.latchIndex = 0;
            }
            myFactory.setLatch(myFactory.latchIndex);
        }
        GUILayout.EndHorizontal();

        GUILayout.Space(20);

        if (GUILayout.Button("Create Prefab"))
        {
            MeshFilter[] meshFilters = myFactory.gameObject.GetComponentsInChildren<MeshFilter>();
            CombineInstance[] combine = new CombineInstance[meshFilters.Length];
            int i = 0;
            while (i < meshFilters.Length)
            {
                combine[i].mesh = meshFilters[i].sharedMesh;
                combine[i].transform = meshFilters[i].transform.localToWorldMatrix;
                i++;
            }
            myFactory.gameObject.transform.GetComponent<MeshFilter>().mesh = new Mesh();
            myFactory.gameObject.transform.GetComponent<MeshFilter>().sharedMesh.CombineMeshes(combine);

            AssetDatabase.CreateAsset(myFactory.gameObject.GetComponent<MeshFilter>().sharedMesh, "Assets/" + myFactory.gameObject.name + "_M" + ".asset");
            PrefabUtility.CreatePrefab("Assets/" + myFactory.gameObject.name + ".prefab", myFactory.gameObject);
            Debug.Log("Finished creating prefab in 'Assets' folder. You are free to move these into subfolders at your discretion");
        }
        if (GUI.changed)
        {
            EditorUtility.SetDirty(target);
        }
        serializedObject.ApplyModifiedProperties();
        serializedObject.Update();
    }
}
