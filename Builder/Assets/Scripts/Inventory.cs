using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Builder
{
    public class Inventory : MonoBehaviour
    {
        public List<BuildingDataclass> inInventory; // this is useful as building manager remains permanent where this likely will scale
        
        public Canvas canvas;
        public Image imgPrefab;

        [SerializeField] private BuildingManager m_BuildingManager; 

        private void Awake()
        {
            canvas = GetComponent<Canvas>();
            GenerateUI();
        }

        void GenerateUI()
        {
            foreach (BuildingDataclass building in inInventory)// fix overloop
                for (int i = 0; i < inInventory.Count; i++)
                {
                    if(imgPrefab != null)
                {
                        var thisBuilding = Instantiate(imgPrefab, this.transform);
                        imgPrefab = building.inventoryImage;
                    }
                    Debug.Log("new prefab");
                }
        }


        //void PutOnMap(inInventory thisGameObject)
        //{
        //    //-1 UI element
        //    //
        //    inInventory
        //}
    }
}
