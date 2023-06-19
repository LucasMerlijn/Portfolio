using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WordMaster : MonoBehaviour
{
    // THIS WAS USED TO GENERATE INFORMATION WORDS.
    // WE WANTED TO PUT SOME WORDS IN A LITTLE MENU THAT MIGHT HAVE BEEN HARD TO UNDERSTAND FOR NEW STUDENTS
    // BUT DUE TO TIME CONSTRAINTS WE REMOVED IT AND MOVED ON TO EASIER WORDS

    private int iD;
    public int getSet_ID
    {
        get { return iD; }
        set { iD = value; }
    }

    public List<string> infoWords = new List<string>()
    {
        "Word 1",
        "Word 2",
        "Word 3",
        "Word 4",
        "Word 5",
        "Word 6",
        "Word 7",
        "Word 8",
        "Word 9",
        "Word 10",
        "Word 11",
        "Word 12",
        "Word 13",
        "Word 14",
        "Word 15",
        "Word 16"
    };

    public List<string> infoDescription = new List<string>()
    {
        "Description 1",
        "Description 2",
        "Description 3",
        "Description 4",
        "Description 5",
        "Description 6",
        "Description 7",
        "Description 8",
        "Description 9",
        "Description 10",
        "Description 11",
        "Description 12",
        "Description 13",
        "Description 14",
        "Description 15",
        "Description 16"
    };

}