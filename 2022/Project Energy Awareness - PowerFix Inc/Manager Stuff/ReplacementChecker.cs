using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReplacementChecker : MonoBehaviour
{
    private bool LeftArea = false;
    private bool triggerOnce = false;
    private bool completed = false;

    [SerializeField]
    private int replacementID;
    public int ReplacementID
    {
        //set { replacementID = value; }
        get { return replacementID; }
    }

    private ScoreManager SM;
    private EnergyObjectManager EOM;
    private Material completedMat;
    private Material oldMat;

    public void Start()
    {
        SM = FindObjectOfType<ScoreManager>();
        GetComponent<Renderer>().enabled = false;
        completedMat = Resources.Load("CompletedMat", typeof(Material)) as Material;
        oldMat = Resources.Load("NewOBJ", typeof(Material)) as Material;
        EOM = FindObjectOfType<EnergyObjectManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<InteractableObject>())
        {
            InteractableObject interactable = other.GetComponent<InteractableObject>();

            if (interactable.NewObject)
            {
                if (!triggerOnce)
                {
                    triggerOnce = true;

                    if (interactable.GetComponent<EnergyObject>().OBJ_ID == replacementID)
                    {
                        LeftArea = false;
                        StartCoroutine(UpdateObject(interactable));
                    }
                }
            }
        }
    }

    /// <summary>
    /// On trigger exit change the Left area to true, this will stop the coroutine from calling its function.
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<InteractableObject>())
        {
            InteractableObject interactable = other.GetComponent<InteractableObject>();

            if (interactable.NewObject)
            {
                LeftArea = true;
                interactable.GetComponent<Renderer>().material = oldMat;
                EOM.AddObjectToList(interactable.GetComponent<EnergyObject>());
                interactable.IsGrabbable = true;
            }
        }
    }
    /// <summary>
    /// If the object stays in the trigger for 2 seconds, then the object will become ungrabbable and change its material.
    /// </summary>
    /// <param name="interactable"></param>
    /// <returns></returns>
    IEnumerator UpdateObject(InteractableObject interactable)
    {
        yield return new WaitForSeconds(2f);
        if (!LeftArea)
        {
            interactable.GetComponent<Renderer>().material = completedMat;
            EOM.RemoveObjectFromList(interactable.GetComponent<EnergyObject>()); // This will remove the object from the list and call the energy bar manager
            interactable.IsGrabbable = false;

            SM.Score = interactable.GetComponent<EnergyObject>().EnergyLabel;

            completed = true;
        }
        triggerOnce = false;
    }
}