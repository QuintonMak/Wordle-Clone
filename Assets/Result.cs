using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class Result : MonoBehaviour
{
    private const string correct = "You guessed correctly! You took __ tries";
    private const string wrong = "Sorry, the correct word was ";
    private const int fadeTime = 120;
    [SerializeField] private TextMeshPro display;
    [SerializeField] private GameObject bg;
    private SpriteRenderer bgRender;
    // Start is called before the first frame update
    void Start()
    {
        bgRender = bg.GetComponent<SpriteRenderer>();
        clear();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void displayMessage(bool correct, int numtries, string ans)
    {
        transform.parent.gameObject.SetActive(true);
        if (correct)
        {
            if (numtries != 1) display.text = "You guessed " + ans.ToUpper() + " correctly! \nYou took " + numtries + " tries.";
            else display.text = "You guessed " + ans.ToUpper() + " correctly! \nYou took 1 try.";
        }
        else
        {
            display.text = "Sorry, the correct word was " + ans.ToUpper() + ".";
        }

        display.text += "\n\nPress ENTER to start a new game.";
    }

    public void clear()
    {
        display.text = "";
        transform.parent.gameObject.SetActive(false);
    }

    // Probably hard to implement, have to figure out transparency of text AND bg
    IEnumerator message(bool correct, int numtries, string ans)
    {
        for (int i = 0; i <= fadeTime; i++)
        {
            if (i == fadeTime)
            {
                if (correct)
                {
                    if (numtries != 1) display.text = "You guessed correctly! \nYou took " + numtries + " tries.";
                    else display.text = "You guessed correctly! \nYou took 1 try.";
                }
                else
                {
                    display.text = "Sorry, the correct word was " + ans.ToUpper() + ".";
                }

                display.text += "\n\nPress ENTER to start a new game.";
            }
            yield return null;
        }
    }

    

}
