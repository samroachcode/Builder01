using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

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

        //UnityEvent _DragStart; 
        private bool beginDrag;
        private bool userInput;
        private float inputTimer;
        private GameObject buildingSelected;

        private void Awake()
        {
            buildingSelected = null;
            userInput = false;
            inputTimer = 0;
            //_DragStart.AddListener(DragStart);
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
                //if (_Drag != null)
                //{
                    Drag();
                    
                //    Debug.Log("Drag");
                //}
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
        }

        private void Drag()//make this an event system 
        {
            //m_BuildingGridInteractionManager.Drag(buildingSelected, Input.mousePosition);
            if (!beginDrag)
            {
                m_BuildingGridInteractionManager.BeginDrag(buildingSelected);
                Debug.Log("Begin Drag");
                beginDrag = true; 
            }
            StartCoroutine(m_BuildingGridInteractionManager.Drag(buildingSelected, Input.mousePosition));
        }

        private void DragOff()
        {
            m_BuildingGridInteractionManager.DragOff(Input.mousePosition);
        }

        private bool ResetInput()
        {
            if (beginDrag)
            {
                beginDrag = false;
                DragOff(); 
            }
            userInput = false;
            inputTimer = 0;
            buildingSelected = null;
            return true; 
        }

        
    }
}
