using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class LooterEditorWindow : EditorWindow {

    [MenuItem("Tools/Looter")]
    public static void ShowWindow()
    {
        GetWindow(typeof(LooterEditorWindow));
    }

    private void OnGUI()
    {
        if (GUILayout.Button(new GUIContent("\u25C1", "Previous Base")))
        {
            Debug.Log("WORDKED");
        }
    }
}
