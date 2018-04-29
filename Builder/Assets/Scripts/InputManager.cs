using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Builder
{
    public class InputManager : MonoBehaviour
    {
        private bool userInput;
        private GameObject go;
        //private float inputTimer; 
        private void Awake()
        {
            go = null; 
            userInput = false;
            inputTimer = 0; 
        }

        private void Update() // add taps and drags. Not worth doing an event system for this style 
        {
            if (Input.GetMouseButton(0))
            {

                RaycastToBuildings(Input.mousePosition);
                userInput = true;
            }
            if (Input.touchCount > 0)
            {
                RaycastToBuildings(Input.GetTouch(1).position);
                userInput = true;
            }
            if (!Input.GetMouseButton(0))
            {
                userInput = false;
                go = null;
            }
            if (userInput && go != null)
            {
                Vector3 inputWorldPos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y,29)) ;
                
                go.transform.position = inputWorldPos;
            }
            else if (!userInput)
                go = null; 
            
        }

        private void RaycastToBuildings(Vector3 position)
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(position);
            if(Physics.Raycast(ray, out hit))
            {
                if (hit.transform.tag == "Building" && go == null)
                {
                    go = hit.transform.gameObject;
                }
            }
        }


    }
}
