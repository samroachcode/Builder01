using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class manages user input and hands off to the buildinggridinteractionmanager which allows for the placement and movement of buildings. 
/// The start of mobile input methods are commented in the "Touch inputs" region so that this can easily be extended. 
/// </summary>

namespace Builder
{
    public class InputManager : MonoBehaviour
    {
        public float tapDuration = 0.1f;

        [SerializeField] private BuildingGridInteractionManager m_BuildingGridInteractionManager; 

        private bool userInput;
        private float inputTimer;
        private GameObject buildingSelected;
        private GameObject buildingCanvas;

        private void Awake()
        {
            buildingSelected = null; 
            userInput = false;
            inputTimer = 0;
            buildingCanvas = null; 
        }

        private void Update() 
        {
            InputMethods(); 
        }

        private void InputMethods() //complete mobile input methods 
        {
            if (Input.GetMouseButton(0))
            {
                inputTimer += Time.deltaTime;
                RaycastToBuildings(Input.mousePosition);
                userInput = true;
            }
            #region Touch Inputs
            //else if (Input.touchCount > 0)
            //{
            //    inputTimer += Time.deltaTime;
            //    RaycastToBuildings(Input.GetTouch(1).position);
            //    userInput = true;
            //}
            #endregion // open for extention to mobile
            if (!Input.GetMouseButton(0) && userInput && inputTimer <= tapDuration)
            {
                userInput = false;
                Tap();
            }
            if (userInput && buildingSelected != null && inputTimer >= tapDuration)
            {
                m_BuildingGridInteractionManager.Drag(buildingSelected, Input.mousePosition);
            }
            if (!Input.GetMouseButton(0) && !ResetInput())
                ResetInput(); 
        }

        private void RaycastToBuildings(Vector3 position)
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(position);
            if(Physics.Raycast(ray, out hit))
            {
                if (hit.transform.tag == "Building" && buildingSelected == null)
                {
                    buildingSelected = hit.transform.gameObject;
                    
                }
            }
        }

        private void Tap()
        {
            //if (buildingSelected != null)
            //{
            //    buildingCanvas = buildingSelected.transform.Find("Canvas");
            //}
            //if (!buildingCanvas.enabled)
            //    buildingCanvas.enabled = true;
            //else
            //    buildingCanvas.enabled = false;
        }

        private bool ResetInput()
        {
            userInput = false;
            inputTimer = 0;
            buildingSelected = null;
            return true; 
        }

    }
}
