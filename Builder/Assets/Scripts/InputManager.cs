using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Builder
{
    public class InputManager : MonoBehaviour
    {
        public float tapDurection = 0.1f; 
        private bool userInput;
        private GameObject buildingSelected;
        private float inputTimer; 

        private void Awake()
        {
            buildingSelected = null; 
            userInput = false;
            inputTimer = 0; 
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
            #endregion
            if (!Input.GetMouseButton(0) && userInput && inputTimer <= tapDurection)
            {
                userInput = false;
                Tap();
            }
            if (userInput && buildingSelected != null && inputTimer >= tapDurection)
            {
                Drag();
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
            //Show UI
        }

        private void Drag()
        {
            Vector3 inputWorldPos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 29));
            buildingSelected.transform.position = inputWorldPos;
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
