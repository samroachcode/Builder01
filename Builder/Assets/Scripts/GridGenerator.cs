using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Builder
{
    public class GridGenerator : MonoBehaviour
    {
        public int gridSize = 20;
        public float offset = 0.2f; 
        public GameObject gridSquare; 
        public GameObject plane;
        public GameObject gridParent; 
        private float planeSize;
        private float gridCountXY;
        private float gridSpacing;

        private bool gridGenerate;

        private Transform[] gridTrans; 

        public void Awake()
        {
            gridSpacing = 1f;
            SetPlaneSize();
            DrawGrid();
        }

        public Vector3 GetNearestPointOnGrid(Vector3 position)
        {
            int xCount = Mathf.RoundToInt(position.x / gridSpacing);
            int yCount = Mathf.RoundToInt(position.y / gridSpacing);
            int zCount = Mathf.RoundToInt(position.z / gridSpacing);
            Vector3 result = new Vector3((float)xCount * gridSpacing, (float)yCount * gridSpacing, (float)zCount * gridSpacing);
            result += transform.position;
            return result;
        }

        public bool DrawGrid()
        {
            for (float x = 0; x < gridSize; x += gridSpacing)
            {
                for (float z = 0; z < gridSize; z += gridSpacing)
                {
                    Vector3 point = GetNearestPointOnGrid(new Vector3(x, 0f, z));
                    GameObject go = Instantiate(gridSquare,new Vector3(point.x, 0, point.z), Quaternion.identity);
                    go.transform.SetParent(gridParent.transform, true);
                }
            }
            gridParent.transform.position = new Vector3(-(gridSize+offset)/2,0, -(gridSize + offset) / 2);
            return (true);
        }

        private void SetPlaneSize()
        {
            planeSize = gridSize / 10;
            plane.transform.localScale += new Vector3(planeSize+offset, 0, planeSize+offset);
        }
    }
}
