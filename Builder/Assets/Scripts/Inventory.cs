using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Builder
{
    public class Inventory : MonoBehaviour
    {
        //ui element list
        
        public List<BuildingDataclass> inInventory;
        public Canvas canvas;
        public Image imgPrefab;

        [SerializeField] private BuildingManager m_BuildingManager; 

        private void Awake()
        {
            canvas = GetComponent<Canvas>();
            if(m_BuildingManager.GenerateInventory()) // search grid save for removed objects
                GenerateUI();
        }

        void GenerateUI()
        {
            foreach (BuildingDataclass building in inInventory)
            {
                var thisBuilding = Instantiate(imgPrefab,this.transform);
                //imgPrefab = building.Gameobject.
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
