using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Item_Database))]
public class Item_Database_Editor : Editor
{
    override public void OnInspectorGUI()
    {
        serializedObject.Update();
        Item_Database myDB = (Item_Database)target;

        EditorGUI.BeginChangeCheck();
        GUILayout.BeginVertical();
        GUIContent addButtonContent = new GUIContent("Add Items", "Search for and add Items to database");
        if (GUILayout.Button(addButtonContent))
        {
            myDB.addItems = true;
        }
        GUIContent updateButtonContent = new GUIContent("Update Items", "Updates prefabs in /Prefabs/Items to match json data");
        if (GUILayout.Button(updateButtonContent))
        {
            myDB.updateDatabase = true;
        }
        GUILayout.EndVertical();
        if (EditorGUI.EndChangeCheck())
        {
            Debug.Log("Editor Change Detected");
        }
        #region EditorActions
        if (myDB.updateDatabase)
        {
            myDB.initialiseJsonFile();
            Debug.Log("Checking for updates");
            myDB.updateLocalJsonData();
            Debug.Log("local items updated");
            myDB.updateDatabase = false;
        }
        if (myDB.addItems)
        {
            myDB.initialiseJsonFile();
            GameObject[] prefabs = Resources.LoadAll<GameObject>("Prefabs/Items");
            foreach (GameObject item in prefabs)
            {
                Item_Component itemComponent = item.GetComponent<Item_Component>();
                if (itemComponent.exists() == false)
                {
                    if (itemComponent.needsUpdating())
                    {
                        Debug.Log("needs updating");
                        //myDB.syncItemFromJson(item);
                    }
                    else
                    {
                        Debug.Log("Creating new Json data for item found");
                        itemComponent.Init();
                        myDB.createItem(item);
                    }
                }
                else
                {
                    Debug.Log("Item Found, Didn't need to update");
                }
            }
            myDB.addItems = false;
        }
        #endregion
    }
}

