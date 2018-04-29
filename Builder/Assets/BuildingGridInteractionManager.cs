﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Builder
{
    public class BuildingGridInteractionManager : MonoBehaviour
    {
        [SerializeField] private GridGenerator m_GridGenerator; 

        public void Drag(GameObject buildingSelected, Vector3 position)
        {
            Vector3 inputWorldPos = Camera.main.ScreenToWorldPoint(new Vector3(position.x, position.y, 29));
            buildingSelected.transform.position = ClosestTile(inputWorldPos);
        }

        Vector3 ClosestTile(Vector3 currentpos)
        {
            Vector3 closest = currentpos;
            float closestDistSqr = Mathf.Infinity;
            foreach (GridCellManager gsm in m_GridGenerator.gridcellArray)
            {
                if (gsm.building == null)
                {
                    Vector3 directionToTarget = gsm.location - currentpos;
                    float distSqrToTarget = directionToTarget.sqrMagnitude;
                    if (distSqrToTarget < closestDistSqr)
                    {
                        closestDistSqr = distSqrToTarget;
                        closest = gsm.location;
                    }
                }
            }
            return closest;
        }
    }
}
