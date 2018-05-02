using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This is a dataclass for each cell allowing the cell to be a location and object to showcase this. 
/// By allowing for a gameobject (thebuilding) and a boolean the tile can be checked for use. While this 
/// information can also be used to serialize and deserialize game data. 
/// </summary>

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
