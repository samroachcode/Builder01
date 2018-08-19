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
        private List<GridCellManager> availableCells;
        private List<GridCellManager> unavailableCells;
        private List<GridCellManager> totalCells;
        private GridCellManager currentSelection;
        private Vector3 currentLocation; 
        private void Start()
        {
            availableCells = new List<GridCellManager>();
            unavailableCells = new List<GridCellManager>();
            totalCells = new List<GridCellManager>();
            if (m_GridGenerator.gridGenerated)
            {
                for (int i = 0; i < m_GridGenerator.gridcellArray.Count; i++)
                {
                    GridCellManager gsm = m_GridGenerator.gridcellArray[i];
                    availableCells.Add(gsm);
                    totalCells.Add(gsm);
                }
            }
        }

        private void Update()
        {
            if (!m_GridGenerator.gridGenerated)
            {
                Start(); 
            }
        }

        Vector3 ClosestTile(Vector3 currentpos) // returns the nearest available tile. 
        {
            Vector3 closest = currentpos;
            float closestDistSqr = Mathf.Infinity;
            for (int i = 0; i < availableCells.Count; i++)
            {
                GridCellManager gsm = availableCells[i];
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

        public void BeginDrag(GameObject buildingSelected)
        {
            Vector3 selectedLocation = buildingSelected.transform.position;

            for (int i = 0; i < unavailableCells.Count; i++)
            {
                if(unavailableCells[i].location == selectedLocation)
                {
                    GridCellManager gsm = unavailableCells[i];
                    Debug.Log("building found");
                    StartCoroutine(SetAvailable(gsm));
                }
            }
        }

        public IEnumerator Drag(GameObject buildingSelected, Vector3 position) //when dragging an object around the map it will clip to the nearest available cell. 
        {
            Vector3 inputWorldPos = Camera.main.ScreenToWorldPoint(new Vector3(position.x, position.y, 29));
            buildingSelected.transform.position = ClosestTile(inputWorldPos);
            currentLocation = buildingSelected.transform.position; 
            yield return null; 
        }

        public void DragOff(Vector3 position)
        {
            for (int i = 0; i < availableCells.Count; i++)
            {
                GridCellManager gsm = availableCells[i];
                if (gsm.location == currentLocation)
                {
                    Debug.Log("Set Unavailable");
                    StartCoroutine(SetUnavailable(gsm));
                }
            }
        }

        public IEnumerator SetAvailable(GridCellManager gsm)
        {
            Debug.Log(gsm.location);
            unavailableCells.Remove(gsm);
            availableCells.Add(gsm);
            //availableCellsUpdating = false; 
            yield return null; 
        }

        public IEnumerator SetUnavailable(GridCellManager gsm)
        {
            Debug.Log(gsm.location);
            availableCells.Remove(gsm);
            unavailableCells.Add(gsm);
            yield return null;
        }


        public IEnumerator PutOnMap(GameObject buildingSelected)// this puts the building on the map. Setting the cell as unavailable so that multiple objects cannot be held in one place. 
        {
            buildingSelected.transform.position = ClosestTile(new Vector3(0, 0, 0));
            for (int i = 0; i < availableCells.Count; i++)
            {
                if(buildingSelected.transform.position == availableCells[i].location)
                {
                    GridCellManager gsm = availableCells[i];
                    currentCell = gsm;
                    currentCell.building = buildingSelected;
                    buildingSelected.transform.position = currentCell.location;
                    StartCoroutine(SetUnavailable(gsm));
                    availableCells.Remove(gsm);
                    unavailableCells.Add(gsm);
                    Debug.Log("set");
                }
            }
            Instantiate(buildingSelected, currentCell.tile.transform.position, Quaternion.identity);
            currentCell.tileInUse = true;
            buildingSelected.GetComponent<BuildingDataclass>().onMap = true;
            yield return null; 
        }
    }
}
