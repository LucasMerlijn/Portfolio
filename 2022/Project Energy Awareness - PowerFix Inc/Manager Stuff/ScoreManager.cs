using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    private int score;
    public int Score
    {
        set 
        {
            score += value;
        }
    }
}
