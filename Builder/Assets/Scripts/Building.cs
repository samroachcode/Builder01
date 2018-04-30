using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

namespace Builder
{
    public enum BuildingType { Unique, Common }
    public class Building : MonoBehaviour
    {
        //public int level;
        //public float cost; 
        //public GameObject buildingObject;
        public Image inventoryImage;
        public BuildingType buildingType;
        private bool onMap;
    }
}
