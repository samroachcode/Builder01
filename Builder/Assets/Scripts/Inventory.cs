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
        public List<BuildingDataclass> inInventory; // this is useful as building manager remains permanent where this likely will scale
        public Canvas canvas;
        public Image imgPrefab;

        [SerializeField] private BuildingManager m_BuildingManager;
        [SerializeField] private BuildingGridInteractionManager m_BuildingGridIntMan;
        public InventoryData[] data; 

        #region Tuesday
        
        


        #endregion Tuesday

        private void Awake()
        {
            canvas = GetComponent<Canvas>();
            GenerateUI();
        }

        void GenerateUI()
        {
            //foreach (BuildingDataclass building in inInventory)// fix overloop
            //    for (int i = 0; i < inInventory.Count; i++)
            //    {
            //        if(imgPrefab != null)
            //    {
            //            var thisBuilding = Instantiate(imgPrefab, this.transform);
            //            imgPrefab = building.inventoryImage;
            //        }
            //        Debug.Log("new prefab");
            //    }
            foreach (InventoryData datacheck in data)
            {
                if(datacheck.numberPermitted > 0)
                {
                    datacheck.uiImage.enabled = true;
                }
                else
                {
                    datacheck.uiImage.enabled = false;
                }
            }

        }

        public void RemoveFromInventory(Image image)
        {
            foreach (InventoryData datacheck in data)
            {
                if (datacheck.uiImage == image && datacheck.numberPermitted >0)
                {
                    datacheck.numberPermitted--;
                    StartCoroutine(m_BuildingGridIntMan.PutOnMap(datacheck.building));  
                }
            }
        }
    }
}
