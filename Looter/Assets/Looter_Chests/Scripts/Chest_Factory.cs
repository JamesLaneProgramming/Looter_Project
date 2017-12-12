using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(MeshFilter))]
public class Chest_Factory : MonoBehaviour {

    public bool randomiseOnLoad;
    [HideInInspector]
    public GameObject[] chest_Lids;
    [HideInInspector]
    public GameObject[] chest_Bases;
    [HideInInspector]
    public GameObject[] chest_Latches;

    private Transform baseJoint;
    private Transform lidJoint;
    private Transform latchJoint;

    [HideInInspector]
    public int baseIndex;
    [HideInInspector]
    public int lidIndex;
    [HideInInspector]
    public int latchIndex;

    [SerializeField]
    public GameObject currentBasePiece;
    [SerializeField]
    public GameObject currentLidPiece;
    [SerializeField]
    public GameObject currentLatchPiece;

    private void Init() {
        chest_Lids = Resources.LoadAll<GameObject>("Prefabs/Parts/Lids");
        chest_Bases = Resources.LoadAll<GameObject>("Prefabs/Parts/Bases");
        chest_Latches = Resources.LoadAll<GameObject>("Prefabs/Parts/Latches");

        baseJoint = gameObject.transform.GetChild(0);
        lidJoint = baseJoint.transform.GetChild(0);
        latchJoint = lidJoint.transform.GetChild(0);
    }

    private void OnEnable() {
        Init();
        currentBasePiece = baseJoint.transform.GetChild(1).gameObject;
        currentLidPiece = lidJoint.transform.GetChild(1).gameObject;
        currentLatchPiece = latchJoint.transform.GetChild(0).gameObject;
        if (randomiseOnLoad) {
            randomise();
            randomiseOnLoad = false;
        }
    }

    public void setBase(int baseIndex) {
        Init();

        Dictionary<int, Material[]> LODSavedMaterials = new Dictionary<int, Material[]>();
        Renderer[] LODRenderers;
        LODRenderers = GetComponentsInChildren<Renderer>();
        for (int i = 0; i < LODRenderers.Length; i++)
        {
            LODSavedMaterials.Add(i, LODRenderers[i].sharedMaterials);
        }

        DestroyImmediate(currentBasePiece, true);
        currentBasePiece = Instantiate(chest_Bases[baseIndex], transform.position, transform.rotation, baseJoint.transform);
        currentBasePiece.transform.localPosition = new Vector3(0, 0, 0);

        LODRenderers = GetComponentsInChildren<Renderer>();
        for (int i = 0; i < LODRenderers.Length; i++)
        {
            if (LODSavedMaterials.ContainsKey(i))
            {
                setMaterials(LODRenderers[i], LODSavedMaterials[i]);
            }
        }
    }
    public void setLid(int lidIndex) {
        Init();

        Dictionary<int, Material[]> LODSavedMaterials = new Dictionary<int, Material[]>();
        Renderer[] LODRenderers;
        LODRenderers = GetComponentsInChildren<Renderer>();
        for (int i = 0; i < LODRenderers.Length; i++)
        {
            LODSavedMaterials.Add(i, LODRenderers[i].sharedMaterials);
        }

        DestroyImmediate(currentLidPiece, true);
        currentLidPiece = Instantiate(chest_Lids[lidIndex], transform.position, transform.rotation, lidJoint.transform);
        currentLidPiece.transform.localPosition = new Vector3(0, 0, 0);
        LODRenderers = GetComponentsInChildren<Renderer>();
        for (int i = 0; i < LODRenderers.Length; i++)
        {
            if (LODSavedMaterials.ContainsKey(i))
            {
                setMaterials(LODRenderers[i], LODSavedMaterials[i]);
            }
        }
    }
    public void setLatch(int latchIndex) {
        Init();

        Dictionary<int, Material[]> LODSavedMaterials = new Dictionary<int, Material[]>();
        Renderer[] LODRenderers;
        LODRenderers = GetComponentsInChildren<Renderer>();
        for (int i = 0; i < LODRenderers.Length; i++)
        {
            LODSavedMaterials.Add(i, LODRenderers[i].sharedMaterials);
        }

        DestroyImmediate(currentLatchPiece, true);
        currentLatchPiece = Instantiate(chest_Latches[latchIndex], transform.position, transform.rotation, latchJoint.transform);
        currentLatchPiece.transform.localPosition = new Vector3(0, 0, 0);

        LODRenderers = GetComponentsInChildren<Renderer>();
        for (int i = 0; i < LODRenderers.Length; i++)
        {
            if (LODSavedMaterials.ContainsKey(i))
            {
                setMaterials(LODRenderers[i], LODSavedMaterials[i]);
            }
        }
    }

    public void randomise() {
        setBase(Random.Range(0, chest_Bases.Length));
        setLid(Random.Range(0, chest_Lids.Length));
        setLatch(Random.Range(0, chest_Latches.Length));
    }
    public void setMaterials(Renderer target, Material[] materialArray)
    {
        target.sharedMaterials = materialArray;
    }
}



