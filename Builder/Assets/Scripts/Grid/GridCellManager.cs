using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Builder
{
    [System.Serializable]
    public class GridCellManager
    {
        public Vector3 location;
        public GameObject tile;
        public GameObject building;
        public bool tileInUse;
    }
}
