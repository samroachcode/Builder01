using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Builder
{
    public class BuildingGridInteractionManager : MonoBehaviour
    {
        [SerializeField] private GridGenerator m_GridGenerator; 

        public void Drag(GameObject buildingSelected, Vector3 position)
        {
            Vector3 inputWorldPos = Camera.main.ScreenToWorldPoint(new Vector3(position.x, position.y, 29));
            //Mesh mesh = buildingSelected.GetComponent<MeshFilter>().mesh;
            //inputWorldPos = new Vector3(inputWorldPos.x - (mesh.bounds.size.x), inputWorldPos.y, inputWorldPos.z - (mesh.bounds.size.z));
            //Vector3 newPosition = ClosestTile(inputWorldPos);
            buildingSelected.transform.position = ClosestTile(inputWorldPos); 
        }

        Vector3 ClosestTile(Vector3 currentpos)
        {
            Vector3 closest = currentpos;
            float closestDistSqr = Mathf.Infinity;
            foreach (GridCellManager gsm in m_GridGenerator.gridcellArray)
            {
                if (gsm.building == null)
                {
                    Vector3 directionToTarget = gsm.location - currentpos;
                    float distSqrToTarget = directionToTarget.sqrMagnitude;
                    if (distSqrToTarget < closestDistSqr)
                    {
                        closestDistSqr = distSqrToTarget;
                        closest = gsm.location;
                    }
                }
            }
            return closest;
        }

        public void PutOnMap(GameObject buildingSelected)
        {
            Instantiate(buildingSelected);
            buildingSelected.transform.position = ClosestTile(new Vector3 (0,0,0));
            foreach (GridCellManager gsm in m_GridGenerator.gridcellArray)
            {
                if (buildingSelected.transform.position == gsm.location)
                {
                    gsm.building = buildingSelected;
                    buildingSelected.GetComponent<BuildingDataclass>().onMap = true; 
                }
            }
        }
    }
}
