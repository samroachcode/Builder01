using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// This is a dataclass which stores the building type and surrounding information (this could be updated to add more types than just common and unique).
/// The important thing here is to store variable for this building which is why it could extend to cost and level.
/// </summary>

namespace Builder
{
    public enum BuildingType { Unique, Common }
    [System.Serializable]
    public class BuildingDataclass: MonoBehaviour
    {
        //public int level;
        //public float cost; 
        //public GameObject buildingObject;
        public BuildingType buildingType;
        public bool onMap;
    }
}
