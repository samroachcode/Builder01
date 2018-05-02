using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

        Vector3 ClosestTile(Vector3 currentpos)
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

        public void Drag(GameObject buildingSelected, Vector3 position)
        {
            Vector3 inputWorldPos = Camera.main.ScreenToWorldPoint(new Vector3(position.x, position.y, 29));
            buildingSelected.transform.position = ClosestTile(inputWorldPos);
        }

        public IEnumerator PutOnMap(GameObject buildingSelected)
        {

            availableCells.Remove(currentCell);
            unavailableCells.Add(currentCell);
            buildingSelected.transform.position = ClosestTile(new Vector3(0, 0, 0));
            currentCell.building = buildingSelected;
            Instantiate(buildingSelected, currentCell.location, Quaternion.identity);
            currentCell.tileInUse = true; 
            buildingSelected.GetComponent<BuildingDataclass>().onMap = true;
            yield return null; 
        }
    }
}
