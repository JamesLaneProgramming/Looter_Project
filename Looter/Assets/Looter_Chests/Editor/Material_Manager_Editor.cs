using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Material_Manager))]
public class Material_Manager_Editor : Editor
{
    private Material_Manager manager;
    public override void OnInspectorGUI()
    {
        manager = (Material_Manager)target;

        //Set GUI Styles
        GUIStyle centeredStyle = new GUIStyle();
        GUIStyle centeredRightStyle = new GUIStyle();
        centeredStyle.alignment = TextAnchor.MiddleLeft;
        centeredRightStyle.alignment = TextAnchor.MiddleRight;

        GUILayout.Space(10);
        EditorGUI.BeginChangeCheck();
        GUILayout.BeginVertical();
        GUILayout.BeginHorizontal();
        //Create Object and Toggle field labels
        GUILayout.Label(new GUIContent("Add Material"), centeredStyle, GUILayout.ExpandWidth(true));
        GUILayout.Label(new GUIContent("Include Inactive?"), centeredRightStyle, GUILayout.ExpandWidth(true));
        GUILayout.EndHorizontal();
        GUILayout.Space(10);
        GUILayout.BeginHorizontal();

        //Create rect for Object field
        Rect objectFieldRect = GUILayoutUtility.GetRect(new GUIContent("Add Material"), GUIStyle.none);
        objectFieldRect.width = EditorGUIUtility.currentViewWidth - 125;
        //Create Object field GUI
        manager.material = (Material)EditorGUI.ObjectField(objectFieldRect, manager.material, typeof(Material), false);

        //Create Rect for Toggle field
        Rect toggleFieldRect = new Rect(objectFieldRect.x + objectFieldRect.width + 50, objectFieldRect.y, 50, EditorGUIUtility.singleLineHeight);
        //Create Toggle field GUI
        manager.includeInactiveGameobjects = EditorGUI.Toggle(toggleFieldRect, manager.includeInactiveGameobjects);

        GUILayout.EndHorizontal();
        GUILayout.Space(10);
        GUILayout.EndVertical();
        EditorGUI.EndChangeCheck();

        //Apply Changes to the custom inspector
        if (manager.material != null)
        {
            manager.ApplyMaterialToChildrenRenderers(manager.material);
            serializedObject.ApplyModifiedProperties();
            serializedObject.Update();
            manager.material = null;
        }
    }
}
