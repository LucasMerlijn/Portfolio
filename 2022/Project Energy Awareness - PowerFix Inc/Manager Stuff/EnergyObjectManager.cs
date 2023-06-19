using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyObjectManager : MonoBehaviour
{
    [SerializeField]
    private List<EnergyObject> prefabList = new List<EnergyObject>();

    [SerializeField]
    private List<EnergyObject> foundObjectList = new List<EnergyObject>();
    [SerializeField]
    private List<EnergyObject> replacementObjects = new List<EnergyObject>();

    [SerializeField]
    private Transform target;

    private int energyLevel;

    private bool objectAlreadyFound = false;

    /// <summary>
    /// Add object to the list if it doesnt exist in the list
    /// </summary>
    /// <param name="obj"></param>
    public void AddObjectToList(EnergyObject obj)
    {
        for (int i = 0; i < foundObjectList.Count; i++)
        {
            if (obj == foundObjectList[i])
            {
                objectAlreadyFound = true;
                break;
            }
        }
        if (!objectAlreadyFound)
        {
            foundObjectList.Add(obj);
            energyLevel += obj.EnergyLabel;
            UpdateEnergyBar(false);
        }
        objectAlreadyFound = false;
    }

    /// <summary>
    /// Update Energy Bar
    /// </summary>
    private void UpdateEnergyBar(bool ClearedOBJ)
    {
        if (ClearedOBJ)
        {
            // Call Energy Bar to shift more green
            // Replace their value with the energyLevel
        }
        else
        {
            // Call Energy Bar to Edit the limit
        }
    }

    /// <summary>
    /// Remove object from the list
    /// </summary>
    /// <param name="obj"></param>
    public void RemoveObjectFromList(EnergyObject obj)
    {
        for (int i = 0; i < foundObjectList.Count; i++)
        {
            if (obj == foundObjectList[i])
            {
                foundObjectList.RemoveAt(i);
                break;
            }
        }

        UpdateEnergyBar(true);
    }

    /// <summary>
    /// Call this to spawn in the objects
    /// </summary>
    public void SpawnObjs()
    {
        StartCoroutine(GenerateGoodObjs());
    }

    /// <summary>
    /// Spawning Ienumerator
    /// </summary>
    /// <returns></returns>
    IEnumerator GenerateGoodObjs()
    {
        for (int i = 0; i < foundObjectList.Count; i++)
        {
            for (int x = 0; x < prefabList.Count; x++)
            {
                if (foundObjectList[i].OBJ_ID == prefabList[x].OBJ_ID)
                {
                    //Debug.Log("Found ID: " + foundObjectList[i].OBJ_ID + " || Prefab ID " + prefabList[x].OBJ_ID);

                    EnergyObject Obj = Instantiate(prefabList[x], target);
                    Obj.GetComponent<EnergyObject>().EnergyLabel = 1;
                    replacementObjects.Add(Obj);
                }
                yield return new WaitForSeconds(0.05f);
            }
        }
    }
}