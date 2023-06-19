using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Spinner : MonoBehaviour
{
    [SerializeField]
    [Range(2.0f, 8.5f)]
    private float fadeTime; // Speed of the spinner. 

    [SerializeField]
    private List<Image> spinnerOptionBackgrounds;

    [SerializeField]
    private List<Text> spinnerOptionTexts;

    [SerializeField]
    private Text instructions;
    public Text Instructions
    {
        get { return instructions; }
        set { instructions = value; }
    }

    private int listSelector = 0;
    public int ListSelector
    {
        get { return listSelector; }
    }

    private float x = 1;

    private int spinnerNumber;
    private bool spinnerDoneWithAnimation = false;

    private GameCore GC;
    public GameCore GC_Obj
    {
        set { GC = value; }
    }


    /// <summary>
    /// StartAnimation will be called from the gameMaster code. This will set up the variables and such for the animation.
    /// </summary>
    public void StartSpinnerAnimation()
    {
        // Remember to set the objects to Active before editing their colours.

        MakeAlphaZero();

        spinnerNumber = Random.Range(5, 13); // Get a random value for the spinner
        //spinnerNumber = 2; // For debugging the Action event. (change to 1 or 3 for question or trust events.
        listSelector = 0; // Reset the list Selector. Used to keep track of what is selected.

        Instructions.text = "Tap to CHANGE the weather!";

        spinnerDoneWithAnimation = true;
        StartCoroutine(SpinnerAnimationLooper(spinnerNumber)); // Call the Coroutinee and add the spinner number (the amount of spins it does)
    }

    public void MakeAlphaZero()
    {
        for (int i = 0; i < spinnerOptionBackgrounds.Count; i++) // Set the alpha colour to 0. So they are completely transparent.
        {
            // Be sure to keep the original colour of the image and text. so we grab their original R G & B colours.
            spinnerOptionBackgrounds[i].color = new Color(spinnerOptionBackgrounds[i].color.r, spinnerOptionBackgrounds[i].color.g, spinnerOptionBackgrounds[i].color.b, 0);
            spinnerOptionTexts[i].color = new Color(spinnerOptionTexts[i].color.r, spinnerOptionTexts[i].color.g, spinnerOptionTexts[i].color.b, 0);
        }
    }

    /// <summary>
    /// Animation of the Spinner
    /// </summary>
    /// <param name="rndNM"></param>
    /// <returns></returns>
    IEnumerator SpinnerAnimationLooper(int rndNM)
    {
        while (rndNM > 0)
        {

            // loop over 1 second
            for (float i = 0; i <= 1; i += fadeTime * Time.deltaTime)
            {
                x -= fadeTime * Time.deltaTime;
                // set color with i as alpha
                spinnerOptionBackgrounds[listSelector].color = new Color(spinnerOptionBackgrounds[listSelector].color.r, spinnerOptionBackgrounds[listSelector].color.g, spinnerOptionBackgrounds[listSelector].color.b, i);
                spinnerOptionTexts[listSelector].color = new Color(spinnerOptionTexts[listSelector].color.r, spinnerOptionTexts[listSelector].color.g, spinnerOptionTexts[listSelector].color.b, i);
                yield return null;
            }
            x = 0;

            if (rndNM == 1) // End spinner
            {
                //Debug.Log(spinnerNumber);
                break; // break it out
            }

            // loop over 1 second backwards
            for (float i = 1; i >= 0; i -= fadeTime * Time.deltaTime)
            {
                x += fadeTime * Time.deltaTime;
                // set color with i as alpha
                spinnerOptionBackgrounds[listSelector].color = new Color(spinnerOptionBackgrounds[listSelector].color.r, spinnerOptionBackgrounds[listSelector].color.g, spinnerOptionBackgrounds[listSelector].color.b, i);
                spinnerOptionTexts[listSelector].color = new Color(spinnerOptionTexts[listSelector].color.r, spinnerOptionTexts[listSelector].color.g, spinnerOptionTexts[listSelector].color.b, i);
                yield return null;
            }
            spinnerOptionBackgrounds[listSelector].color = new Color(spinnerOptionBackgrounds[listSelector].color.r, spinnerOptionBackgrounds[listSelector].color.g, spinnerOptionBackgrounds[listSelector].color.b, 0);
            spinnerOptionTexts[listSelector].color = new Color(spinnerOptionTexts[listSelector].color.r, spinnerOptionTexts[listSelector].color.g, spinnerOptionTexts[listSelector].color.b, 0);

            x = 1;

            if (listSelector == 2) // if at end of list go to the beginning
                listSelector = 0; // Set the list selector to the beginning
            else
                listSelector++; // ++ the list

            rndNM--;
        }
        // Winner is the ListSelector value. 0 = Question, 1 = Action, 2 = Truth
        SetSpinnerDone();
    }

    private void SetSpinnerDone()
    {
        GC.CheckGameLists(true, true); // Check if truth or actions are out of questions

        instructions.color = new Color(instructions.color.r, instructions.color.g, instructions.color.b, 1); // turn the tap text back on
        Instructions.text = "Tap to continue!"; // Set new text in the tap text. Telling the player to tap to continue

        spinnerDoneWithAnimation = true; // Set a bool that will be passed to the GC "Game Core" script

        GC.SpinningDone = spinnerDoneWithAnimation; // use the Setter to set the bool in GC to spinnerdonewithanimation
        GC.SpinningOutcome = ListSelector; // Tell GC the outcome
    }
}