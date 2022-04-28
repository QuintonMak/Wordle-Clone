using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/**
 * The "central" class of the game, managing the user input and correctness of the answer
 */

public class UserInput : MonoBehaviour
{
    private const int maxLen = 5; //const for now
    private const int numLines = 6;
    // measurements for the word squares
    private const float topOffset = 2.8f;
    private const float vIncrement = 1.4f;
    private const float hIncrement = 0f;
    private int lineNum;
    [SerializeField] string display;
    [SerializeField] private GameObject displayLine;
    [SerializeField] private GameObject displayLinePrefab;
    [SerializeField] private GameObject[] displayLines = new GameObject[numLines];
    [SerializeField] private List<char> textList; // List representing the current input
    [SerializeField] private List<char> ans; //temp answer
    private string ansString;
    private bool isCorrect;
    [SerializeField] private GameObject resultDisplay;


    [SerializeField] private int[] correctness = new int[maxLen];
    // Predefined values
    private int[] initCorrect = { 0, 0, 0, 0, 0 };
    [SerializeField] private int[] correct = { 2, 2, 2, 2, 2 };


    // Initializes all values
    void init()
    {
        textList = new List<char>();
        ans = new List<char>();
        correctness = initCorrect;
        setAns(Constants.getRandomAns());
        display = "";
        lineNum = 0;
        isCorrect = false;
    }

    // Renders the lines to the screen (numLines (6) lines)
    void renderLines()
    {
        for (int i = 0; i < numLines; i++)
        {
            displayLines[i] = Instantiate(displayLinePrefab, new Vector3(hIncrement, topOffset - i*vIncrement, 0), Quaternion.identity);
        }
        displayLine = displayLines[0];
    }

    // Set the answer to the current wordle
    // requires: s be length maxLen
    /**
     * @param s the answer in string form
     */
    void setAns(String s)
    {
        ansString = s;
        ans.Clear();
        for (int i = 0; i < s.Length; i++)
        {
            ans.Add(s[i]);
        }
    }

    /**
     * Sets the correctness array values to determine which chars the user got correct or incorrect
     * efficiency: O(n^2)
     */
    void setCorrectness()
    {
        char curr;
        List<char> ansChars = new List<char>(ans); // the characters existing in the answer that have not been found yet
        for (int i = 0; i < maxLen; i++)
        {
            curr = textList[i];
            correctness[i] = 0; // Reset to 0
            if (curr == ans[i]) {  // correct position in ACTUAL answer
                // find all that are correct first
                correctness[i] = 2;
                ansChars.Remove(curr);
            } 
        }
        for (int i = 0; i < maxLen; i++)
        {
            curr = textList[i];
            if (ansChars.IndexOf(curr) >= 0 && correctness[i] != 2)
            { // exists in the answer and not correct position - remove once found to account for repeats
                correctness[i] = 1;
                ansChars.Remove(curr);
            }
            else if (correctness[i] != 2) // if not in the word and not already found to be correct pos
            {
                correctness[i] = 0;
            }
        }

        isCorrect = true;
        foreach (int num in correctness)
        {
            if (num != 2)
            {
                isCorrect = false;
            }
        }
    }

    // Submits the user input, checks if it is correct, and moves to the next line.
    void tryAnswer()
    {
        //check the correctness of the answer
        setCorrectness();
        displayLine.GetComponent<TextLine>().setColors(correctness);

        // Move to next line 
        if (lineNum < numLines - 1)
        {
            lineNum++;
            //reset text input
            if (!isCorrect)
            {
                textList.Clear();
            }
            display = "";
            displayLine = displayLines[lineNum];
        } 
        else
        {
            lineNum = numLines;
            //if (correct.Equals(correctness))
            //{
            //    Debug.Log("correct!");
            //}
            //else
            //{
            //    Debug.Log("Correct answer is: " + ansString);
            //}
        }

        // did the user get the answer correct, or run out of attempts? 
        if (lineNum == numLines || isCorrect)
        {
            Result r = resultDisplay.GetComponentInChildren<Result>();
            if (r != null)
            {
                r.displayMessage(isCorrect, lineNum, ansString);
            }
        }

    } 

    // Resets the game. Clears the lines of input, resets values, and assigns a new answer from the answers list.
    void resetLines()
    {
        foreach (GameObject displayLine in displayLines)
        {
            displayLine.GetComponent<TextLine>().reset();
        }
        display = "";
        textList.Clear();
        lineNum = 0;
        displayLine = displayLines[lineNum];
        setAns(Constants.getRandomAns());
        isCorrect = false;
        Result r = resultDisplay.GetComponentInChildren<Result>();
        if (r != null)
        {
            r.clear();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        init();
        renderLines();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.anyKeyDown)
        {
            foreach (char c in Input.inputString)
            {
                if (Input.GetKeyDown("backspace")) // has backspace/delete been pressed?
                {
                    if (textList.Count > 0)
                    {
                        display = display.Substring(0, textList.Count - 1);
                        textList.RemoveAt(textList.Count - 1);
                    }
                } 
                else if (Input.GetKeyDown("return")) // user submits answer by pressing return
                {

                    if (lineNum == numLines || isCorrect)
                    {
                        resetLines();
                    }
                    else if (textList.Count == maxLen && Constants.validInput(display)) // is the line filled and the input valid?
                    {
                        tryAnswer();
                    }
                    


                }
                else if ('a' <= c && c <= 'z')
                {
                    if (textList.Count < maxLen)
                    {   
                        textList.Add(c);
                        display += c; // convert to uppercase for display
                    }
                }
            }
        }
        displayLine.GetComponent<TextLine>().setText(display.ToUpper());
    }

    
    
}