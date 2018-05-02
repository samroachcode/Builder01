using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Builder
{
    [System.Serializable]
    public class InventoryData
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
