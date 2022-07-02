using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using UnityEngine.UI;

public class TurretPlacement : MonoBehaviour
{
    [SerializeField] private int maxTowers;
    [SerializeField] private float minRadius;
    [SerializeField] LayerMask layersToCollide;
    [SerializeField] LayerMask layersTerrain;
    [SerializeField] LayerMask turretsLayer;
    [SerializeField] Text text;
    [SerializeField] Material[] materials;
    [SerializeField] Terrain terrain;
    [Header("Torres")]
    [SerializeField] GameObject[] torresPrefab;
    public static GameObject torreSelected;
    [SerializeField] GameObject previewTower;

    [HideInInspector]
    public TowerInfo towerInfo;
    [HideInInspector]
    public bool isUpgraded = false;
    
    bool active;
    Vector3 pos;
    int towerCount;
    bool removeTurret;
    Ray ray;
    GameObject turretToBuild;

    private void Update()
    {
        if (removeTurret && Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(ray, out RaycastHit HitInfo, Mathf.Infinity, turretsLayer))
            {
                Currency.Increase(HitInfo.collider.gameObject.GetComponent<Turret>().placeCost / 2);
                Destroy(HitInfo.collider.gameObject);
                Decrease();
            }
            removeTurret = false;
        }
        if (!active || maxTowers <= towerCount)
            return;

        previewTower.transform.position = pos;

        if (Physics.OverlapSphere(pos, minRadius, layersToCollide).Any() ||(pos != Vector3.zero && TextureTest(pos,terrain) == "gravel"))
        {
            previewTower.GetComponent<MeshRenderer>().material = materials[0];
            return;
        }
        else
        {
            previewTower.GetComponent<MeshRenderer>().material = materials[1];
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (!Currency.Decrease(turretToBuild.GetComponent<Turret>().placeCost))
            {
                Destruct();
                return;
            }
            Instantiate(turretToBuild, pos, transform.rotation);
            Increase();
            Destruct();
        }
    }
    void FixedUpdate()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (!Physics.Raycast(ray, out RaycastHit HitInfo, Mathf.Infinity, layersTerrain))
        {
            pos = Vector3.zero;
            previewTower.SetActive(false);
            return;
        }
        else
        {
            previewTower.SetActive(true);
        }
        pos = HitInfo.point;
    }
    string TextureTest(Vector3 turretPos,Terrain t)
    {
        float[] cellMix = GetTextureMix(turretPos, t);
        float strongest = 0;
        int maxIndex = 0;
        for(int i = 0; i < cellMix.Length; i++)
        {
            if(cellMix[i] > strongest)
            {
                maxIndex = i;
                strongest = cellMix[i];
            }
        }
        return t.terrainData.terrainLayers[maxIndex].name;
    }
    float[] GetTextureMix(Vector3 turretPos,Terrain t)
    {
        Vector3 tPos = t.transform.position;
        TerrainData tData = t.terrainData;

        int mapX = Mathf.RoundToInt((turretPos.x - tPos.x) / tData.size.x * tData.alphamapWidth);
        int mapZ = Mathf.RoundToInt((turretPos.z - tPos.z) / tData.size.z * tData.alphamapHeight);
        float[,,] splatMapData = tData.GetAlphamaps(mapX, mapZ, 1, 1);

        float[] cellmix = new float[splatMapData.GetUpperBound(2)+1];
        for(int i = 0; i < cellmix.Length; i++)
        {
            cellmix[i] = splatMapData[0,0,i];
        }
        return cellmix;
    } 
    private void Destruct()
    {
        pos = Vector3.zero;
        previewTower.transform.position = pos;
        previewTower.SetActive(false);
        active = false;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.black;
        if (previewTower != null)
            Gizmos.DrawWireSphere(previewTower.transform.position, minRadius);
    }
    public void Change(int index)
    {
        turretToBuild = torresPrefab[index];
        active = true;
    }
    public void Remove()
    {
        removeTurret = !removeTurret;
        Destruct();
    }
    private void Increase()
    {
        towerCount++;
        text.text = $"towers: {towerCount}";
    }
    private void Decrease()
    {
        towerCount--;
        text.text = $"towers: {towerCount}";
    }

    public void UpgradeTurret ()
	{
        if(torreSelected is null)
        {
            Debug.Log("Nenhuma Torre selecionada");
            return;
        }
        var torrescript = torreSelected.GetComponent<Turret>();
        if(!Currency.Decrease(torrescript.upgradeCost))
        {
            Debug.Log("Sem moeda");
            return;
        }
        torrescript.Upgrade();
        Debug.Log("melhorado");
	}

    public static void SelectNode (GameObject turret)
	{
		if (torreSelected == turret)
		{
			DeselectNode();
			return;
		}

		torreSelected = turret;
	}

	public static void DeselectNode()
	{
		torreSelected = null;
	}
}
