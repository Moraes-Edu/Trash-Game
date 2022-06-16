using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class Terreno : MonoBehaviour
{
    [SerializeField]    private float minRadius;
    [SerializeField]    private List<GameObject> list = new List<GameObject>();
    [SerializeField]    LayerMask layersToCollide;
    [SerializeField]    LayerMask layersTerrain;
    public GameObject turretToBuild;
    public bool active;
    GameObject go = null;

    void Update()
    {
        if (!active)
            return;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (!Physics.Raycast(ray, out RaycastHit HitInfo,Mathf.Infinity,Physics.AllLayers))
            return;
        if (!HitInfo.collider.CompareTag("Terrain"))
            return;
        
        (go = go != null ? go : Instantiate(turretToBuild,HitInfo.point,Quaternion.identity)).transform.position = HitInfo.point;

        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Botao");
            return;
            if (Physics.OverlapSphere(HitInfo.point, minRadius, layersToCollide).Length > 1)
            {
                Debug.Log("W");
            }

            GameObject turret = (GameObject)Instantiate(turretToBuild, HitInfo.point, transform.rotation);
            list.Add(turret);
            Destroy(go);
            go = null;
            active = false;
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.black;
        if (go != null)
        Gizmos.DrawWireSphere(go.transform.position, minRadius);
    }

}
