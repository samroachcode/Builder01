using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Builder
{
    public class BuildingManager : MonoBehaviour
    {
        [SerializeField] private BuildingDataclass[] buildings;
        [SerializeField] private Inventory inventory; 

        private void Awake()
        {
            GenerateInventory(); 
        }

        public bool GenerateInventory()
        {
            foreach (BuildingDataclass building in buildings)
            {
                inventory.inInventory.Add(building);
            }
            return true;
        }


    }
}
