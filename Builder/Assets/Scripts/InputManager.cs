using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Builder
{
    public class InputManager : MonoBehaviour
    {
        private bool userInput;

        private void Awake()
        {
            userInput = false;
        }

        private void Update()
        {
            if (Input.GetMouseButton(0))
            {
                RaycastToBuildings(Input.mousePosition);
                userInput = true;
            }
            if(Input.touchCount > 0)
            {
                RaycastToBuildings(Input.GetTouch(1).position);
                userInput = true;
            }
            else
                userInput = false;
        }

        private void RaycastToBuildings(Vector3 position)
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(position);
            if(Physics.Raycast(ray, out hit))
            {
                if (hit.transform.tag == "Building")
                {
                    Debug.Log("DRAG");
                    //hit.transform.position = new Vector3(ray.x, 0, ray.z);
                }
            }
        }
    }
}
