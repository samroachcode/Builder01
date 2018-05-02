using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class generates the grid system, the size of this can be set in the inspector. 
/// </summary>

namespace Builder
{
    public class GridGenerator : MonoBehaviour
    {
        public GameObject gridSquare; 
        public GameObject plane;
        public GameObject gridParent;

        [SerializeField] private int gridSize = 20;
        [SerializeField] private float planeOffset = 0.2f;

        private float planeSize;
        private float gridCountXY;
        private float gridSpacing;
        private bool gridGenerate;
        public ArrayList gridcellArray = new ArrayList(); // it would be better if this was private or read only

        public void Awake()
        {
            gridSpacing = 1.1f;
            SetPlaneSize();
            if (!DrawGrid())
            {
                DrawGrid();
            }
        }

        public Vector3 GetNearestPointOnGrid(Vector3 position) //uses spacing and iteration to set points on the grid. 
        {
            int xCount = Mathf.RoundToInt(position.x / gridSpacing);
            int zCount = Mathf.RoundToInt(position.z / gridSpacing);
            Vector3 result = new Vector3((float)xCount * gridSpacing, 0, (float)zCount * gridSpacing);
            result += transform.position;
            return result;
        }

        public bool DrawGrid()//this method draws the grid using the Vector3 method to iterate across the x and z axis. GO are added to an array list for later reference
        {
            for (float x = 0; x < gridSize; x += gridSpacing)
            {
                for (float z = 0; z < gridSize; z += gridSpacing)
                {
                    Vector3 point = GetNearestPointOnGrid(new Vector3(x, 0f, z));
                    GameObject go = Instantiate(gridSquare,new Vector3(point.x, 0, point.z), Quaternion.identity);
                    go.transform.SetParent(gridParent.transform, true);
                    GridCellManager gsm = new GridCellManager();
                    gsm.location = new Vector3(point.x - (gridSize / 2) + planeOffset, 0, point.z - (gridSize / 2) + planeOffset);
                    gsm.tile = go;
                    gsm.tileInUse = false;
                    go.name = "Tile " + x + ", z" + z;
                    gridcellArray.Add(gsm);
                }
            }
            
            gridParent.transform.position = new Vector3(-(gridSize /2) + planeOffset, 0, -(gridSize/ 2) + planeOffset);
            #region list locations of tiles
            //foreach (GridCellManager gsm in gridcellArray)
            //{
            //    Debug.Log(gsm.location + ": location " + gsm.tile.name + "tile");
            //}
            #endregion
            return (true);
        }

        private void SetPlaneSize() // plane scales dependent on number of tiles
        {
            planeSize = gridSize / 10;
            plane.transform.localScale += new Vector3(planeSize+ planeOffset, 0, planeSize+ planeOffset);
        }
    }
}
