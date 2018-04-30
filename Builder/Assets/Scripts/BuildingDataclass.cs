using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 
namespace Builder
{

    public enum BuildingType { Unique, Common }
    [System.Serializable]
    public class BuildingDataclass: MonoBehaviour
    {
        public GameObject building;
        public int maxOwnership;
        //public int level;
        //public float cost; 
        //public GameObject buildingObject;
        public Image inventoryImage;
        public BuildingType buildingType;
        private bool onMap;
    }
}
