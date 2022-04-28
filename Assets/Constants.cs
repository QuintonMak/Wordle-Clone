using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

/**
 * Stores the possible words, should not be mutable from outside this file
 * 
 * Sources: 
 *  https://gist.github.com/cfreshman/a03ef2cba789d8cf00c08f767e0fad7b
 *  https://gist.github.com/cfreshman/cdcdf777450c5b5301e439061d29694c
 * */

public class Constants : MonoBehaviour
{
    [SerializeField] private TextAsset guesses;
    [SerializeField] private TextAsset answers;
    private static string[] ALLOWED_GUESSES; // the user is allowed to guess these words
    private static string[] ALLOWED_ANSWERS; // the answer is a word from this list
    // Start is called before the first frame update
    void Awake()
    {
        //ALLOWED_GUESSES = System.IO.File.ReadAllLines(@"Assets\guesses.txt");
        ALLOWED_GUESSES = guesses.text.Split('\n', '\r');
        //ALLOWED_ANSWERS = System.IO.File.ReadAllLines(@"Assets\answers.txt");
        ALLOWED_ANSWERS = answers.text.Split('\n', '\r', ' ');
        Array.Sort(ALLOWED_ANSWERS);
        Array.Sort(ALLOWED_GUESSES);
        ALLOWED_GUESSES = ALLOWED_GUESSES.Where(a => a.Length != 0).ToArray();
        ALLOWED_ANSWERS = ALLOWED_ANSWERS.Where(a => a.Length != 0).ToArray();
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public string[] getGuesses()
    {
        return ALLOWED_GUESSES;
    }

    public string[] getAnswers()
    {
        return ALLOWED_ANSWERS;
    }
    
    /**
     * Returns whether s is a valid input. A valid input is a string that exists within ALLOWED_GUESSES or ALLOWED_ANSWERS
     * @param s the user's input
     * @return whether the input is valid
     * efficiency: O(2logn) = O(logn)
     */
    public static bool validInput(string s)
    {
        s = s.ToLower();
        if (Array.BinarySearch(ALLOWED_GUESSES, s) >= 0) return true;
        //foreach (string str in ALLOWED_GUESSES)
        //{
        //    Debug.Log("|" + str + "|" + s + "|" + streq(s, str));
        //    if (streq(s, str)) return true;
        //}
        //foreach (string str in ALLOWED_ANSWERS)
        //{
        //    Debug.Log("|" + str + "|" + s + "|" + streq(s, str));
        //    if (streq(s, str)) return true;
        //}
        if (Array.BinarySearch(ALLOWED_ANSWERS, s) >= 0) return true;
        Debug.Log("Invalid");
        return false;
    }

    private static bool streq(string a, string b)
    {
        Debug.Log(a.Length + "|" + b.Length);
        foreach (char c in a)
        {
            Debug.Log((int)c);
        }
        foreach (char c in b)
        {
            Debug.Log((int)c);
        }
        if (a.Length != b.Length) return false;

        for (int i = 0; i < b.Length; i++)
        {
            if (a[i] != b[i]) return false;
        }
        return true;
    }

    /** Gets a random string from the answers array 
     * @return a random string from ALLOWED_ANSWERS
     */
    public static string getRandomAns()
    {
        int index = (int)UnityEngine.Random.Range(0f, ALLOWED_ANSWERS.Length - 1);
        Debug.Log(ALLOWED_ANSWERS[index] + index);
        return ALLOWED_ANSWERS[index];
    }
}
