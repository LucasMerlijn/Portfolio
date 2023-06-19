using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTextElement : MonoBehaviour
{
    // Unused code
    [SerializeField]
    [Tooltip("0 = Question, 1 = Action, 2 = Trust")]
    [Range(0,2)]
    private int ID_type;
    public int ID_Type
    {
        get { return ID_type; }
    }

    [SerializeField]
    [Tooltip("0 = Nothing, 1 = Action, 2 = Trust")]
    [Range(0, 2)]
    private int actionOrTruth;
    public int ActionOrTruth
    {
        get { return actionOrTruth; }
    }
}