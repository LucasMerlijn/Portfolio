using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameCore : MonoBehaviour
{
    #region Variables

    // Class Variables
    private Spinner spinner; // Class variable that handles the spinning

    // Bool Variables
    private bool spinningDone = false; // Check if the spinning is done
    public bool SpinningDone
    {
        set { spinningDone = value; }
    } // Get the value for spinningDone from the Spinner class

    private bool isSpinning = false; // If the spinner is spinning

    private bool isOnAnswer = false; // If this is true and the next button will be pressed the spinner will pop up.

    // Int Variables
    private int spinningOutcome; // Value of what will happen next
    public int SpinningOutcome
    {
        set { spinningOutcome = value; }
    } // Get the value from the spinner class

    private int questionType; // Type of Question (What list)
    private int questionIndex; // Index Value of Question List

    private int endListAmount = 0; // Amount of lists removed out of the GameList

    private int actionIndex; // Get the Action Index
    private int trustIndex; // Get the trust Index

    private int lastAction = 0; // save the last action.
    // Float Variables


    // List Variables
    private List<List<string>> problemAnswerListsDone = new List<List<string>>();
    private List<List<string>> problemAnswerListsHolder = new List<List<string>>();

    private List<List<string>> problemQuestionListsDone = new List<List<string>>();
    private List<List<string>> problemQuestionListsHolder = new List<List<string>>();

    [SerializeField]
    private List<string> actionEvents;

    [SerializeField]
    private List<string> trustQuestions;
    private List<string> trustDone = new List<string>();
    [SerializeField]
    private List<string> trustAnswers;
    private List<string> trustAnswersDone = new List<string>();

    [SerializeField]
    private List<string> problemEmotion; // Mentality problem statements.
    [SerializeField]
    private List<string> problemsEmotionAwnser; // Mentality problem answers.
    private List<string> problemsEmotionDone = new List<string>(); // Empty list to move the mentality problem into.
    private List<string> problemsEmotionAnswerDone = new List<string>(); // Empty list to move the awnser into.

    [SerializeField]
    private List<string> problemsLocation; // Location problem statements.
    [SerializeField]
    private List<string> problemsLocationAnswer; // location problem answers.
    private List<string> problemsLocationDone = new List<string>(); // Empty list to move the location problem into.
    private List<string> problemsLocationAnswerDone = new List<string>(); // Empty list to move the location answer into.

    [SerializeField]
    private List<string> problemsLogistics; // Logistics problem statements.
    [SerializeField]
    private List<string> problemsLogisticsAnswer; // Logistics problem answers.
    private List<string> problemsLogisticsDone = new List<string>(); // Empty list to move the logistics problem into.
    private List<string> problemsLogisticsAnswerDone = new List<string>(); // Empty list to move the logistics answer into.

    [SerializeField]
    private List<string> problemsCulture; // Culture problem statements.
    [SerializeField]
    private List<string> problemsCultureAnswer; // Culture problem answers.
    private List<string> problemsCultureDone = new List<string>(); // Empty list to move the culture problem into.
    private List<string> problemsCultureAnswerDone = new List<string>(); // Empty list to move the culture answer into.

    [SerializeField]
    private List<Text> textFields;

    // Text & Button Variables
    //[SerializeField]
    //private Text weatherSpinnerText;

    [SerializeField]
    private Image spinnerPanel;

    [SerializeField]
    private Image questionPanel;

    [SerializeField]
    private Image actionPanel;

    [SerializeField]
    private Image truthPanel;

    [SerializeField]
    private Text actionInfoText;

    [SerializeField]
    private Image errorPanel;

    [SerializeField]
    private Button endGameButtonQuestions;

    [SerializeField]
    private Button endGameButtonTrust;

    [SerializeField]
    private Button continueGameButtonTrust;

    [SerializeField]
    private Text errorTextMessage;

    [SerializeField]
    private GameObject NextButtonQuestion;
    
    [SerializeField]
    private GameObject BackButtonQuestion;

    [SerializeField]
    private GameObject NextButtonTrust;

    [SerializeField]
    private GameObject BackButtonTrust;

    [SerializeField]
    private GameObject NextButtonAction;

    [SerializeField]
    private GameObject BackButtonAction;

    #endregion

    /// <summary>
    /// Starts when the scene is loaded.
    /// It sets up everything we need at the start.
    /// </summary>
    private void Start()
    {
        spinner = this.GetComponent<Spinner>();
        spinner.GC_Obj = this;

        Setlists(); // Set all lists
    }

    /// <summary>
    /// add all the lists into the header lists.
    /// Main recycling goal is to not throw away items from lists but re-use them later on if needs be
    /// </summary>
    private void Setlists()
    {
        problemQuestionListsHolder.Add(problemsLocation);
        problemQuestionListsHolder.Add(problemsLogistics);
        problemQuestionListsHolder.Add(problemEmotion);
        problemQuestionListsHolder.Add(problemsCulture);

        problemAnswerListsHolder.Add(problemsLocationAnswer);
        problemAnswerListsHolder.Add(problemsLogisticsAnswer);
        problemAnswerListsHolder.Add(problemsEmotionAwnser);
        problemAnswerListsHolder.Add(problemsCultureAnswer);

        problemQuestionListsDone.Add(problemsLocationDone);
        problemQuestionListsDone.Add(problemsLogisticsDone);
        problemQuestionListsDone.Add(problemsEmotionDone);
        problemQuestionListsDone.Add(problemsCultureDone);

        problemAnswerListsDone.Add(problemsLocationAnswerDone);
        problemAnswerListsDone.Add(problemsLogisticsAnswerDone);
        problemAnswerListsDone.Add(problemsEmotionAnswerDone);
        problemAnswerListsDone.Add(problemsCultureAnswerDone);
    }

    /// <summary>
    /// Called in unity by pressing the spinner button
    /// </summary>
    public void PlayerSpinnerButton()
    {
        PlayAudio(); // Play audio (done at the end. I wanted to set it up differently)


        if (spinningDone) // If spinning is done then go to the output and return
        {
            PlayerSpinnerOutcome();
            return;
        }

        if (!isSpinning) // If it isn't spinning then we set spinning to true so it wont spin double. We also set the text to a 0 alpha and call the spinner to start the animation
        {
            isSpinning = true;
            spinner.Instructions.color = new Color(spinner.Instructions.color.r, spinner.Instructions.color.g, spinner.Instructions.color.b, 0);
            spinner.StartSpinnerAnimation();
        }
    }

    /// <summary>
    /// The spinner tells us the outcome then we use a Switch case to tell what needs to happen on what outcome it gets
    /// </summary>
    private void PlayerSpinnerOutcome()
    {
        switch (spinningOutcome)
        {
            case 0: // Question normal is the outcome
                //Debug.Log(spinningOutcome + " Question");
                UnloadSpinner(); // unload the spinner
                LoadPanel(true, false, false, true); // load the first panel, to not load the second or third and set it to true (active) LoadPanel(Panel1, Panel2, Panel3, SetActive)
                CalculateNewRandomQuestion(true, false, false); // Set a new question
                LoadQuestion(); // Load in the question

                break; // Return out of the switch case.

            case 1: // Action is the Outcome
                //Debug.Log(spinningOutcome + " Action");
                UnloadSpinner();
                LoadPanel(false, true, false, true);
                CalculateNewRandomQuestion(true, false, false);
                LoadActionEvent();
                LoadQuestion();

                break;

            case 2: // Truth is the Outcome
                //Debug.Log(spinningOutcome + " Truth");
                UnloadSpinner();
                LoadPanel(false, false, true, true);
                CalculateNewRandomQuestion(true, false, false);
                LoadTrustQuestion();
                LoadQuestion();

                break;

            default:
                Debug.Log("Something Went Wrong!"); // Oopsie, something went wrong. 
                break;
        }
    }

    /// <summary>
    /// Used to check if the questions or truths were out of questions.
    /// If so it throws an error message
    /// </summary>
    /// <param name="checkQuestions"></param>
    /// <param name="checkTruth"></param>
    public void CheckGameLists(bool checkQuestions, bool checkTruth)
    {
        if (checkQuestions)
        {
            if (problemQuestionListsHolder.Count == 0)
            {
                errorPanel.gameObject.SetActive(true);
                errorTextMessage.gameObject.SetActive(true);
                errorTextMessage.text = "!! Out Of Questions !!";
                endGameButtonQuestions.gameObject.SetActive(true);

                return;
            }
        }

        if (checkTruth)
        {
            if (trustQuestions.Count == 0)
            {
                errorPanel.gameObject.SetActive(true);
                errorTextMessage.gameObject.SetActive(true);
                errorTextMessage.text = "!! Out of Truth Questions !!";
                endGameButtonTrust.gameObject.SetActive(true);
                continueGameButtonTrust.gameObject.SetActive(true);
            }
        }
    }

    /// <summary>
    /// Go out of the game back to main menu
    /// </summary>
    public void EndGameButton()
    {
        PlayAudio();
        SceneManager.LoadScene("Menu");
    }

    /// <summary>
    /// get the audio component and play it.
    /// </summary>
    private void PlayAudio()
    {
        this.GetComponent<AudioSource>().Play();
    }

    /// <summary>
    /// This is to stay in the game while there was an error
    /// </summary>
    public void ContinueGameButton()
    {
        PlayAudio();
        errorPanel.gameObject.SetActive(false);
    }

    /// <summary>
    /// Loads in the settings needed for the spinner
    /// </summary>
    private void LoadSpinner()
    {
        spinner.MakeAlphaZero();
        //spinner.Instructions.color = new Color(spinner.Instructions.color.r, spinner.Instructions.color.g, spinner.Instructions.color.b, 1);
        spinningDone = false;
        isSpinning = false;
        spinnerPanel.gameObject.SetActive(true);
    }
    /// <summary>
    /// Unloads the spinner by setting is inactive
    /// </summary>
    private void UnloadSpinner()
    {
        spinnerPanel.gameObject.SetActive(false);
    }

    /// <summary>
    /// Used to load the panel. Depending on what panel you need just enter the right true values.
    /// You can also unload it with setting the load param. to false.
    /// </summary>
    /// <param name="QuestionPanel"></param>
    /// <param name="ActionPanel"></param>
    /// <param name="TrustPanel"></param>
    /// <param name="Load"></param>
    private void LoadPanel(bool QuestionPanel, bool ActionPanel, bool TrustPanel, bool Load)
    {
        if (QuestionPanel)
            questionPanel.gameObject.SetActive(Load);

        if (ActionPanel)
            actionPanel.gameObject.SetActive(Load);

        if (TrustPanel)
            truthPanel.gameObject.SetActive(Load);
    }

    /// <summary>
    /// Player input. 
    /// Get a bool with the button clicks. That is used to determine what button is clicked.
    /// With a true it is the forward button. Then we know that the player moves to the answer of the question. 
    /// Then if it is pressed again we know it is on the answer already due to the bool being set to true.
    /// Then we will go and calculate that the answer has to be removed
    /// remove the trust answer
    /// and set the variables back to normal and then load the spinner
    /// </summary>
    /// <param name="forward"></param>
    public void ButtonClick(bool forward)
    {
        PlayAudio(); // play audio

        if (isOnAnswer && forward)
        {
            CalculateNewRandomQuestion(false, true, true);

            if (spinningOutcome == 2)
                RemoveTrust();

            isOnAnswer = false;
            ButtonUpdater(isOnAnswer);
            LoadPanel(true, true, true, false);
            LoadSpinner();
            return;
        }

        if (forward)
        {
            isOnAnswer = true;
            LoadAnswer();
            ButtonUpdater(isOnAnswer);
        }
        else
        {
            isOnAnswer = false;
            LoadQuestion();
            ButtonUpdater(isOnAnswer);
        }
    }

    /// <summary>
    /// used to turn on the button or off the buttons.
    /// If there were no pages to go back on the back button would be removed.
    /// as well as the forward button
    /// </summary>
    /// <param name="turnOn"></param>
    private void ButtonUpdater(bool turnOn)
    {
        Debug.Log(spinningOutcome);
        switch (spinningOutcome)
        {
            case 0:
                BackButtonQuestion.SetActive(turnOn);
                break;

            case 1:
                BackButtonAction.SetActive(turnOn);
                break;

            case 2:
                BackButtonTrust.SetActive(turnOn);
                break;
        }
    }

    /// <summary>
    /// set the text field to the question.
    /// </summary>
    private void LoadQuestion()
    {
        switch (spinningOutcome)
        {
            case 1: // Only on 1
                actionInfoText.text = actionEvents[actionIndex];
                textFields[spinningOutcome].text = problemQuestionListsHolder[questionType][questionIndex];

                break;

            case 2: // Only on 2
                textFields[spinningOutcome].text = trustQuestions[trustIndex];
                //textFields[spinningOutcome].text = problemQuestionListsHolder[questionType][questionIndex];

                break;

            default: // Everything besides 1 or 2
                textFields[spinningOutcome].text = problemQuestionListsHolder[questionType][questionIndex];

                break;
        }
    }

    /// <summary>
    /// Set the text field to the answer
    /// </summary>
    private void LoadAnswer()
    {
        switch (spinningOutcome)
        {
            case 1: // Only on 1
                //LoadActionEvent();
                actionInfoText.text = actionEvents[actionIndex];
                textFields[spinningOutcome].text = problemAnswerListsHolder[questionType][questionIndex];

                break;

            case 2: // Only on 2
                //LoadTrustQuestion();
                textFields[spinningOutcome].text = trustAnswers[trustIndex];
                //textFields[spinningOutcome].text = problemAnswerListsHolder[questionType][questionIndex];

                break;

            default: // Everything besides 1 or 2
                textFields[spinningOutcome].text = problemAnswerListsHolder[questionType][questionIndex];

                break;
        }

    }

    /// <summary>
    /// Set the special text field to the event
    /// Cannot be the same event 2 times in a row due to the while loop
    /// </summary>
    private void LoadActionEvent()
    {
        actionIndex = Random.Range(0, actionEvents.Count);

        if (actionIndex == lastAction)
        {
            while(actionIndex == lastAction)
            {
                actionIndex = Random.Range(0, actionEvents.Count);
            }
        }

        lastAction = actionIndex;
    }

    /// <summary>
    /// Load a trust question from a random range
    /// add the question to the done list of trust questions
    /// </summary>
    private void LoadTrustQuestion()
    {
        trustIndex = Random.Range(0, trustQuestions.Count);
        trustDone.Add(trustQuestions[trustIndex]);
    }

    /// <summary>
    /// Remove the trust question
    /// Add the trust answer to the done list
    /// remove the trust answer from the answer list
    /// </summary>
    private void RemoveTrust()
    {
        trustQuestions.RemoveAt(trustIndex);
        trustAnswersDone.Add(trustAnswers[trustIndex]);
        trustAnswers.RemoveAt(trustIndex);
    }

    /// <summary>
    /// Used to remove questions and answers from the lists. Also calculates if the multidimentional lists have no more lists in them
    /// </summary>
    /// <param name="newQuestion"></param>
    /// <param name="removeAnswer"></param>
    /// <param name="removeQuestion"></param>
    private void CalculateNewRandomQuestion(bool newQuestion, bool removeAnswer, bool removeQuestion)
    {
        // Set a new theme
        // Set a new questionType &
        // set a new questionIndex
        if (newQuestion)
        {
            // Set questionType to a random range between 0 and the holder.count. so it cannot go beyond the few types of questions
            questionType = Random.Range(0, problemQuestionListsHolder.Count);
            // Call the change Theme Method. And pass in the type of question
            //ChangeTheme(questionType);
            // Set questionIndex to a random range between 0 and the holder.count. so it cannot go beyond the amount of questions
            questionIndex = Random.Range(0, problemQuestionListsHolder[questionType].Count);
        }

        // Check the remove answer boolean that got parched through calling the method
        // If true then it will have to remove an answer from the list
        if (removeAnswer)
        {
            // we use the questionType as an index to find what question type it was. Then add the question index from the holder list to the done list
            problemAnswerListsDone[questionType].Add(problemAnswerListsHolder[questionType][questionIndex]);
            problemAnswerListsHolder[questionType].RemoveAt(questionIndex); // Then we remove the question at the given index.

            // We run a for loop to the length of the holder.count
            for (int i = 0; i < problemAnswerListsHolder.Count; i++)
            {
                // If any of the lists in this listholder is empty go and do something
                if (problemAnswerListsHolder[i].Count == 0)
                {
                    // Add the empty list to the done list
                    problemAnswerListsDone.Add(problemAnswerListsHolder[i]);
                    // Remove the empty list from the holder list
                    problemAnswerListsHolder.RemoveAt(i);
                    // Add 1 end of questions each time it removes a list
                    endListAmount++;
                }
                // if the end of questions == 5 (i got to change this to the original length of the list of lists .count
                if (endListAmount == 5)
                {
                    Debug.Log("END!"); // Say it ends
                    return; // leave this method
                }
            }
        }

        // If the boolean removeQuestion is true then do the following:
        // NOTE: this has the exact same code as the remove Answer
        if (removeQuestion)
        {
            // we use the questionType as an index to find what question type it was. Then add the question index from the holder list to the done list
            problemQuestionListsDone[questionType].Add(problemQuestionListsHolder[questionType][questionIndex]);
            problemQuestionListsHolder[questionType].RemoveAt(questionIndex); // Then we remove the question at the given index

            // We run a for loop to the length of the holder.count
            for (int i = 0; i < problemQuestionListsHolder.Count; i++)
            {
                // Check if the i.count is empty (if the list is empty)
                if (problemQuestionListsHolder[i].Count == 0)
                {
                    // add the list to done from holder
                    problemQuestionListsDone.Add(problemQuestionListsHolder[i]);
                    // remove the list from the holder list
                    problemQuestionListsHolder.RemoveAt(i);
                }
            }
        }


    }
}
