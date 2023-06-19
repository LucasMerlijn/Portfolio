using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameMaster : MonoBehaviour
{
    #region Variables

    // Button to explain some more dutch culture related words
    [SerializeField]
    private Button infoButton;

    // Button to go to the next question
    [SerializeField]
    private Button nextProblemButton;

    // The background of the text.
    [SerializeField]
    private Image textBackground;

    // The text that will change from question to answer
    [SerializeField]
    private Text problemTextField;

    [SerializeField]
    private Image infoMenuHolder;

    [SerializeField]
    private Image InfoMenuWords;

    [SerializeField]
    private Button nextInfoScreen;

    [SerializeField]
    private Button backInfoScreen;

    [SerializeField]
    private Image WinScreen;

    [SerializeField]
    private Button EndGame;

    [SerializeField]
    private Button Back;

    #region List Variables

    // Lists of Problem Statements 
    // NOTE: THE INDEX IS THE SAME AS THE INDEX OF THE ANSWERS
    // NOTE: 2 LISTS ARE SERIALIZED! MEANING WE EDIT THEM IN THE INSPECTOR FOR EASE!
    // NOTE: THESE LISTS WILL GO INTO 4 MULTIDIMENTIONAL LISTS FOR EASE OF USE!

    [SerializeField]
    private List<string> problemsMentality; // Mentality problem statements.
    [SerializeField]
    private List<string> problemsMentalityAwnser; // Mentality problem answers.
    private List<string> problemsMentalityDone = new List<string>(); // Empty list to move the mentality problem into.
    private List<string> problemsMentalityAnswerDone = new List<string>(); // Empty list to move the awnser into.

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

    // OLD TYPE OF PROBLEMS, PERHAPS NEEDED SO WONT REMOVE YET!
    //[SerializeField]
    //private List<string> problemsWild;
    //[SerializeField]
    //private List<string> problemsWildAnswer;
    //private List<string> problemWildDone = new List<string>();
    //private List<string> problemWildAnswerDone = new List<string>();


    private List<List<string>> problemQuestionListsHolder = new List<List<string>>(); // THIS LIST WILL HOLD ALL PROBLEM LISTS
    private List<List<string>> problemQuestionListsDone = new List<List<string>>(); // THIS LIST WILL HOLD THE LISTS THAT HAVE NO MORE QUESTIONS

    private List<List<string>> problemAnswerListsHolder = new List<List<string>>(); // THIS LIST WILL HOLD ALL PROBLEM ANSWER LISTS
    private List<List<string>> problemAnswerListDone = new List<List<string>>(); // THIS LIST WILL HOLD THE LISTS THAT HAVE NO MORE ANSWERS

    // The number to decide the next question
    private int questionIndex; // This int is used to generate a random question out of the question list.
    private bool questionIsOn = false; // This boolean is used to know when to switch between answers and questions.
    private int questionType; // this int is used to pick a random list to grab a question from.

    #endregion

    private bool nextPressGoesBackToMenu = false; // Boolean is used to go back to the main menu (at the end of the game)

    [SerializeField] // To show this variable in the inspector as it might need to be edited often
    [Range(5, 15)] // Set a min and max range of the amount of questions
    private int questionAmount; // When this reaches 0 the game ends.

    private int endOfListsQuestions; // This int is used to end the game if the entire game is out of questions. 


    // Info Variables
    private bool infoOpen = false; // This variable is used to control the info menu. If it is false then it is closed.
    [SerializeField]
    private int InfoWordAmount;
    private int infoIndex = 0;
    private int infoPage = 1;
    [SerializeField]
    [Tooltip("DO NOT CHANGE. This is calculated automatically depending on 'infoWordAmount'. It is used for the info menu.")]
    private int infoMaxPages;

    [SerializeField]
    private WordMaster prefabInfoWord;

    [SerializeField]
    private List<WordMaster> InfoWords;

    // Make lists of words
    // Load set amount of words per page.
    // Then load the other list on the other pages


    private bool spinnerTime = false;

    private Spinner spinner;

    [SerializeField]
    private Image spinnerHolder;

    private int spinnerOutcome; // This number determines the spinner outcome.
    public int SpinnerOutcome
    {
        set { spinnerOutcome = value; }
    }

    private bool spinnerDone = false;
    public bool SpinnerDone
    {
        set { spinnerDone = value; }
    }
    private bool StartSpinner = true;

    [SerializeField]
    private Button winningMenuButton;

    List<GameObject> UIelements = new List<GameObject>();

    #endregion


    // NOTE!!! CODE NOT USED ANYMORE. FIRST PROTOTYPE OF THE CODE STRUCTURE. THIS CODE IS MAINLY USED IN GAME CORE NOW INSTEAD!!!

    // Start function that boots up each time you start running the scene.
    private void Start()
    {
        for (int i = 0; i < InfoWordAmount; i++) // For loop used to generate the amount of word prefabs for the info menu.
        {
            WordMaster obj = Instantiate(prefabInfoWord, InfoMenuWords.transform); // Instantiate the prefab

            if (i > 3) // If it isn't one of the first 4 info words.
                obj.gameObject.SetActive(false); // Set it then to false.

            obj.getSet_ID = i; // Set the ID (just in case we need to change something outside this for loop)
            obj.gameObject.name = obj.infoWords[i]; // Set the name to the internal list they have.
            obj.gameObject.GetComponentInChildren<InfoTitleHolder>().GetComponent<Text>().text = obj.infoWords[i]; // Set their child object title to the word
            obj.gameObject.GetComponentInChildren<InfoDescHolder>().GetComponent<Text>().text = obj.infoDescription[i]; // Set their child object description box to the description
            InfoWords.Add(obj); // Add the object(s) to a list. Used to loop throught so we can show X amount per screen.
        }

        spinner = gameObject.GetComponent<Spinner>();
        //spinner.GM_Obj = this;

        infoMaxPages = (Mathf.CeilToInt((float)InfoWords.Count / (float)4)); // Set a max amount of pages for the info screen. (this can change if we need more words explained. So I made it scalable.
        //Debug.Log(infoMaxPages);
        if (infoMaxPages == 0) // If only 1 page is required. We make sure to at least give 1 page. as it rounds down.
            infoMaxPages = 1; // Set the pages to 1

        UIelements.Add(nextProblemButton.gameObject);
        UIelements.Add(textBackground.gameObject);
        UIelements.Add(problemTextField.gameObject);
        UIelements.Add(infoButton.gameObject);
        UIelements.Add(winningMenuButton.gameObject);


        // LETS ADD ALL THE LISTS MANUALLY! :D I know another way of doing this in the variables...
        // But Multidimention lists aren't shown in the inspector. So hence why we only add them here.
        problemQuestionListsHolder.Add(problemsLocation);
        problemQuestionListsHolder.Add(problemsLogistics);
        problemQuestionListsHolder.Add(problemsMentality);
        problemQuestionListsHolder.Add(problemsCulture);

        problemAnswerListsHolder.Add(problemsLocationAnswer);
        problemAnswerListsHolder.Add(problemsLogisticsAnswer);
        problemAnswerListsHolder.Add(problemsMentalityAwnser);
        problemAnswerListsHolder.Add(problemsCultureAnswer);

        problemQuestionListsDone.Add(problemsLocationDone);
        problemQuestionListsDone.Add(problemsLogisticsDone);
        problemQuestionListsDone.Add(problemsMentalityDone);
        problemQuestionListsDone.Add(problemsCultureDone);

        problemAnswerListDone.Add(problemsLocationAnswerDone);
        problemAnswerListDone.Add(problemsLogisticsAnswerDone);
        problemAnswerListDone.Add(problemsMentalityAnswerDone);
        problemAnswerListDone.Add(problemsCultureAnswerDone);

        // Call the method that generated a new question
        // it requires 3 bools to be passed into:
        // 1: a new theme (this is going to be used to load in new images / change the look of the app depending on the type of question
        // 2: When it needs to remove an answer out of the answer list
        // 3: When it needs to remove an question out of the question list
        CalculateNewRandomQuestion(true, false, false);

    }

    #region GameLoop

    /// <summary>
    /// Remove questions & answers
    /// Generate new questions
    /// Call ChangeTheme() when it requires to.
    /// </summary>
    /// <param name="newTheme"></param>
    /// <param name="removeAnswer"></param>
    /// <param name="removeQuestion"></param>
    private void CalculateNewRandomQuestion(bool newTheme, bool removeAnswer, bool removeQuestion)
    {
        // Check the remove answer boolean that got parched through calling the method
        // If true then it will have to remove an answer from the list
        if (removeAnswer)
        {
            // we use the questionType as an index to find what question type it was. Then add the question index from the holder list to the done list
            problemAnswerListDone[questionType].Add(problemAnswerListsHolder[questionType][questionIndex]);
            problemAnswerListsHolder[questionType].RemoveAt(questionIndex); // Then we remove the question at the given index.

            // We run a for loop to the length of the holder.count
            for (int i = 0; i < problemAnswerListsHolder.Count; i++)
            {
                // If any of the lists in this listholder is empty go and do something
                if (problemAnswerListsHolder[i].Count == 0)
                {
                    // Add the empty list to the done list
                    problemAnswerListDone.Add(problemAnswerListsHolder[i]);
                    // Remove the empty list from the holder list
                    problemAnswerListsHolder.RemoveAt(i);
                    // Add 1 end of questions each time it removes a list
                    endOfListsQuestions++;
                }
                // if the end of questions == 5 (i got to change this to the original length of the list of lists .count
                if (endOfListsQuestions == 5)
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

        // Set a new theme
        // Set a new questionType &
        // set a new questionIndex
        if (newTheme)
        {
            // Set questionType to a random range between 0 and the holder.count. so it cannot go beyond the few types of questions
            questionType = Random.Range(0, problemQuestionListsHolder.Count);
            // Call the change Theme Method. And pass in the type of question
            ChangeTheme(questionType);
            // Set questionIndex to a random range between 0 and the holder.count. so it cannot go beyond the amount of questions
            questionIndex = Random.Range(0, problemQuestionListsHolder[questionType].Count);
        }
    }

    // Call this method when the next question button is pressed!
    public void ButtonPress()
    {
        //// if the nextPressGoesBackToMenu is true
        //if (nextPressGoesBackToMenu)
        //    SceneManager.LoadScene("Menu");

        if (spinnerTime)
        {
            LoadSpinner();
            StartSpinner = true;
            spinnerDone = false;
            return;
        }


        if (questionAmount == 0 || endOfListsQuestions == 5)
        {
            // Out of Questions!
            // Go to the game over screen

            // Change the text field to the last statement.
            problemTextField.text = "Question limit reached! On a tie, do rock paper scisors to determine the winner! Goodluck!";

            // Set the next press to true. So if the player presses again then it will leave and go back to the menu.
            nextPressGoesBackToMenu = true;

            // return out of this method.
            return;
        }
        else
        {
            // If the question is on display right now then do the following:
            if (questionIsOn)
            {
                LoadAnswer();
            }
            else
            {
                LoadQuestion();
            }
        }
    }

    private void LoadQuestion()
    {
        // Set the questionIsOn to true so next time it will reply with an answer
        questionIsOn = true;
        // Change the problem text field to the answer of the problem
        problemTextField.text = problemQuestionListsHolder[questionType][questionIndex];
        // Call this method to remove the question from the list
        CalculateNewRandomQuestion(false, false, true);
        textBackground.color = new Color(0.6f, 0.92f, 0.94f, 1);
    }
    private void LoadAnswer()
    {
        // Set the questionIsOn to false. So next time it will not go in this if statement.
        questionIsOn = false;
        // change the problem text field to the answer of the problem
        problemTextField.text = problemAnswerListsHolder[questionType][questionIndex];

        // Call the calculateNewRandomQuestion method to remove the answer and generate a new question.
        CalculateNewRandomQuestion(true, true, false);

        // Change the background colour to a pinkish colour right now.
        textBackground.color = new Color(0.93f, 0.6f, 0.93f, 1);
        spinnerTime = true;
    }

    #endregion

    #region Theme

    /// <summary>
    /// In here we will switch assets around to change to the different types of weather NOTE: weather = types of questions
    /// </summary>
    /// <param name="TypeOfTheme"></param>
    private void ChangeTheme(int TypeOfTheme)
    {
        // Location
        // Logistics
        // Emotional
        // Culture

    }
    #endregion

    #region Info

    public void InfoMenu()
    {
        if (infoOpen) // Go back to game
        {
            infoOpen = false;
            UnloadAllWords();
            infoIndex = 0;
            infoPage = 1;

            loadUI(true);

            infoMenuHolder.gameObject.SetActive(false);
        }
        else
        {
            infoOpen = true;

            LoadWords();
            loadUI(false);

            infoMenuHolder.gameObject.SetActive(true);
        }
    }

    public void PreviousInfoList()
    {
        if (infoPage == 1)
            return;

        infoPage--;
        UnloadAllWords();

        infoIndex -= 4;
        LoadWords();
    }

    public void NextInfoList()
    {
        if (infoPage == infoMaxPages || infoMaxPages == 1)
            return;

        infoPage++;
        UnloadAllWords();

        infoIndex += 4;
        LoadWords();

    }

    [SerializeField]
    [Range(1f, 4f)]
    private float fadeTime = 1;

    private void loadUI(bool load)
    {
        nextProblemButton.gameObject.SetActive(load);
        textBackground.gameObject.SetActive(load);
        problemTextField.gameObject.SetActive(load);
        infoButton.gameObject.SetActive(load);
        winningMenuButton.gameObject.SetActive(load);
    }

    private void UnloadAllWords()
    {
        for (int i = 0; i < InfoWords.Count; i++)
        {
            InfoWords[i].gameObject.SetActive(false);
        }
    }

    private void LoadWords()
    {
        for (int i = infoIndex; i < InfoWords.Count; i++)
        {
            InfoWords[i].gameObject.SetActive(true);
            if (i == (infoIndex + 3))
                break;
        }
    }

    #endregion

    #region Spinner

    [SerializeField]
    private Text spinnerTapText;

    private void LoadSpinner()
    {
        spinnerHolder.gameObject.SetActive(true);
        spinner.MakeAlphaZero();
        spinnerTapText.gameObject.SetActive(true);
        loadUI(false);
    }

    private void SpinSpinner()
    {
        StartSpinner = false;
        spinner.StartSpinnerAnimation();
    }

    /// <summary>
    /// SpinnerControl manages the next state of the game.
    /// Whether it is a Question, Action or Truth
    /// </summary>
    private void SpinnerControl()
    {
        spinnerTime = false;

        Debug.Log(spinnerOutcome);

        switch (spinnerOutcome)
        {
            case 0: // Quesiton
                spinnerHolder.gameObject.SetActive(false);
                loadUI(true);
                LoadQuestion();
                Debug.Log("Question");
                break;

            case 1: // Action
                spinnerHolder.gameObject.SetActive(false);
                loadUI(true);
                LoadActionOrTruth(true);
                Debug.Log("Action");
                break;

            case 2: // Truth
                spinnerHolder.gameObject.SetActive(false);
                loadUI(true);
                LoadActionOrTruth(false);
                Debug.Log("Truth");
                break;
            default:
                Debug.Log("Default???");
                break;
        }
    }

    /// <summary>
    /// Player input to spin the button or to go to the result.
    /// </summary>
    public void SpinButton()
    {
        // First press start spinning.
        if (StartSpinner)
        {
            SpinSpinner();
            spinnerTapText.gameObject.SetActive(false);
        }

        if (spinnerDone)
        {
            SpinnerControl();
        }
    }

    #endregion

    /// <summary>
    /// If it is an action then send a true in.
    /// </summary>
    /// <param name="isAction"></param>
    private void LoadActionOrTruth(bool isAction)
    {
        if (isAction)
        {
            problemTextField.text = "Draw a action card!";
            textBackground.color = new Color(0.95f, 0.2f, 0.2f, 1);
        }
        else
        {
            problemTextField.text = "Draw a truth card!";
            textBackground.color = new Color(0.2f, 0.95f, 0.2f, 1);
        }
    }


    public void LoadWinning()
    {
        loadUI(false);
        WinScreen.gameObject.SetActive(true);
    }

    public void WinGame()
    {
        SceneManager.LoadScene("Menu");
    }

    public void GoBackOutOfWinMenuO()
    {
        WinScreen.gameObject.SetActive(false);
        loadUI(true);
    }


}