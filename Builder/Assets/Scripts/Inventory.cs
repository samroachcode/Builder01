using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// The inventory generates all buildings allowed within the game. 
/// The objects can be built onto the map through selecting the relevent UI button. 
/// The objects are handed off to the buildinggridinteractionmanager for placement in the nearest available slot,
/// and removed from the inventory.
/// </summary>


namespace Builder
{
    [System.Serializable]
    public class InventoryData // the inventory data class stores the properties of the obects within the inventory such as the max number the user is permitted to build. 
    {
        public GameObject building;
        public int numberPermitted;
        public Image uiImage;
        //private int cost; 
    }

    public class Inventory : MonoBehaviour
    {

        [SerializeField] private BuildingGridInteractionManager m_BuildingGridIntMan;
        public InventoryData[] data; 

        private void Awake()
        {
            GenerateUI();
        }

        void GenerateUI()
        {
            foreach (InventoryData datacheck in data)
            {
                if (datacheck.numberPermitted > 0)
                    datacheck.uiImage.gameObject.SetActive(true);
                else
                    datacheck.uiImage.gameObject.SetActive(false);
            }

        }

        public void RemoveFromInventory(Image image)
        {
            foreach (InventoryData datacheck in data)
            {
                if (datacheck.uiImage == image && datacheck.numberPermitted >0)
                {
                    datacheck.numberPermitted--;
                    GenerateUI();
                    StartCoroutine(m_BuildingGridIntMan.PutOnMap(datacheck.building));  
                }
            }
        }
    }
}
