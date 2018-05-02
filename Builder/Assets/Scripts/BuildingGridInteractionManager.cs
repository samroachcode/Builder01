using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This is the middleground between the grid and the buildings / input.
/// Checking whether a grid section is available and making objects clip to to nearest grid sectoin. 
/// 
/// </summary>

namespace Builder
{
    public class BuildingGridInteractionManager : MonoBehaviour
    {
        [SerializeField] private GridGenerator m_GridGenerator;
        private GridCellManager currentCell;
        private ArrayList availableCells = new ArrayList();
        private ArrayList unavailableCells = new ArrayList();

        private void Start()
        {
            foreach (GridCellManager gsm in m_GridGenerator.gridcellArray)
            {
                if (gsm.building == null && !gsm.tileInUse)
                    availableCells.Add(gsm);
            }
        }

        Vector3 ClosestTile(Vector3 currentpos) // returns the nearest available tile. 
        {
            Vector3 closest = currentpos;
            float closestDistSqr = Mathf.Infinity;
            //foreach (GridCellManager gsm in m_GridGenerator.gridcellArray)
            //{
            //    if (gsm.building == null && !gsm.tileInUse)
            //    {
            //        Vector3 directionToTarget = gsm.location - currentpos;
            //        float distSqrToTarget = directionToTarget.sqrMagnitude;
            //        if (distSqrToTarget < closestDistSqr)
            //        {
            //            closestDistSqr = distSqrToTarget;
            //            closest = gsm.location;
            //            currentCell = gsm; 
            //        }
            //    }
            //}
            foreach (GridCellManager gsm in availableCells)
            {
                Vector3 directionToTarget = gsm.location - currentpos;
                float distSqrToTarget = directionToTarget.sqrMagnitude;
                if (distSqrToTarget < closestDistSqr)
                {
                    closestDistSqr = distSqrToTarget;
                    closest = gsm.location;
                    currentCell = gsm;
                }
            }
            return closest;
        }

        public void Drag(GameObject buildingSelected, Vector3 position) //when dragging an object around the map it will clip to the nearest available cell. 
        {

            Vector3 inputWorldPos = Camera.main.ScreenToWorldPoint(new Vector3(position.x, position.y, 29));
            buildingSelected.transform.position = ClosestTile(inputWorldPos);
        }

        public IEnumerator PutOnMap(GameObject buildingSelected)// this puts the building on the map. Setting the cell as unavailable so that multiple objects cannot be held in one place. 
        {
            GameObject go = buildingSelected;
            availableCells.Remove(currentCell);
            unavailableCells.Add(currentCell);
            go.transform.position = ClosestTile(new Vector3(0, 0, 0));
            currentCell.building = go;
            Instantiate(go, currentCell.tile.transform.position, Quaternion.identity);
            currentCell.tileInUse = true;
            go.GetComponent<BuildingDataclass>().onMap = true;
            //buildingSelected.transform.position = ClosestTile(new Vector3(0, 0, 0));
            //currentCell.building = buildingSelected;
            //Instantiate(buildingSelected, currentCell.location, Quaternion.identity);
            //currentCell.tileInUse = true; 
            //buildingSelected.GetComponent<BuildingDataclass>().onMap = true;
            yield return null; 
        }
    }
}
