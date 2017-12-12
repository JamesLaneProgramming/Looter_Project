using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Material_Manager : MonoBehaviour {
    public Material material;
    public bool includeInactiveGameobjects;

    /// <summary>
    /// Applies a given material to all children in the current transform.
    /// </summary>
    /// <param name="material">Material to apply to selected</param>
    public void ApplyMaterialToChildrenRenderers(Material material) {
        //Find Renderers in children
        List<Renderer> renderers = new List<Renderer>(GetComponentsInChildren<Renderer>(includeInactiveGameobjects));
        foreach (Renderer _r in renderers) {
            if(_r.gameObject.activeInHierarchy != false || includeInactiveGameobjects)
            {
                for (int i = 0; i < _r.sharedMaterials.Length; i++)
                {
                    //If their is a missing material
                    if (_r.sharedMaterials[i] == null)
                    {
                        Material[] selectionMaterials = _r.sharedMaterials;
                        selectionMaterials[i] = material;
                        _r.sharedMaterials = selectionMaterials;
                    }

                    //If the material prefix matches an existing prefix
                    string matName = material.name.ToString().Split(char.Parse("_"))[0];
                    if (_r.sharedMaterials[i].name.Split(char.Parse("_"))[0] == matName)
                    {
                        Material[] selectionMaterials = _r.sharedMaterials;
                        selectionMaterials[i] = material;
                        _r.sharedMaterials = selectionMaterials;
                    }
                }
            }
        }
    }
}