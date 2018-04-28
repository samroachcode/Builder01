using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Builder
{
    public class GridGenerator : MonoBehaviour
    {

        public GameObject gridSquare; 
        public float gridSize = 20;
        public GameObject plane;

        private float planeSize;
        private float gridCountXY;
        private float gizmoDistance;
        private bool gridGenerate;

        public void Awake()
        {
            gizmoDistance = 1f;
            DrawGrid();
        }

        public Vector3 GetNearestPointOnGrid(Vector3 position)
        {
            int xCount = Mathf.RoundToInt(position.x / gizmoDistance);
            int yCount = Mathf.RoundToInt(position.y / gizmoDistance);
            int zCount = Mathf.RoundToInt(position.z / gizmoDistance);

            Vector3 result = new Vector3((float)xCount * gizmoDistance, (float)yCount * gizmoDistance, (float)zCount * gizmoDistance);

            result += transform.position;

            return result;
        }

        public bool DrawGrid()
        {
            for (float x = 0; x < gridSize; x += gizmoDistance)
            {
                for (float z = 0; z < gridSize; z += gizmoDistance)
                {
                    Vector3 point = GetNearestPointOnGrid(new Vector3(x, 0f, z));

                    Instantiate(gridSquare, new Vector3(point.x, 0, point.z), Quaternion.identity);
                }
            }
            return (true);
        }
        //private void SetPlaneSize()
        //{
        //    planeSize = gridSize / 10;
        //    plane.transform.localScale += new Vector3(planeSize, 0, planeSize);
        //}


        //private void OnDrawGizmos()
        //{
        //    Gizmos.color = Color.yellow;
        //    for (float x = 0; x < gridSize; x += gizmoDistance)
        //    {
        //        for (float z = 0; z < gridSize; z += gizmoDistance)
        //        {
        //            var point = GetNearestPointOnGrid(new Vector3(x, 0f, z));
        //            Gizmos.DrawSphere(point, 0.1f);
        //        }
        //    }
            //    SetPlaneSize(); 
        //}
    }
}
