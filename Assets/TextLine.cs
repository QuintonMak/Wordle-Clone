using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextLine : MonoBehaviour
{
    [SerializeField] private GameObject[] squares; // set in inspector
    // Start is called before the first frame update
    void Start()
    {
        setText("     ", 5);
        transform.localScale = new Vector3(1.25f, 1.25f, 1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /**
     * Sets the text that shows up in the boxes
     * @param t The string to display in the boxes
     * requires: t.Length == 5
     */
    public void setText(string t)
    {
        for (int i = 0; i < t.Length; i++)
        {
            (squares[i].GetComponentInChildren(typeof(TextMesh)) as TextMesh).text = "" + t[i];
        }
        for (int i = t.Length; i < 5; i++)
        {
            (squares[i].GetComponentInChildren(typeof(TextMesh)) as TextMesh).text = "";
        }
    }

    /**
     * Sets the text that shows up in the boxes - override for varying word lengths
     */
    public void setText(string t, int maxLen)
    {
        for (int i = 0; i < t.Length; i++)
        {
            (squares[i].GetComponentInChildren(typeof(TextMesh)) as TextMesh).text = "" + t[i];
        }
        for (int i = t.Length; i < maxLen; i++)
        {
            (squares[i].GetComponentInChildren(typeof(TextMesh)) as TextMesh).text = "";
        }
    }

    /**
     * Sets the color of the squares based on the correctness
     * @param correctness an array containing the correctness of the words
     * requires: correctness.Length == squares.Length
     * */
    public void setColors(int[] correctness)
    {
        for(int i = 0; i < correctness.Length; i++)
        {
            switch (correctness[i])
            {
                case 0:
                    squares[i].GetComponent<SpriteRenderer>().color = new Color(215f/255f, 215f/255f, 215f/255f);
                    break;
                case 1:
                    squares[i].GetComponent<SpriteRenderer>().color = Color.yellow;
                    break;
                case 2:
                    squares[i].GetComponent<SpriteRenderer>().color = Color.green;
                    break;
            }
        }
    }

    // Resets each square to the default color (white) and clears the text.
    public void reset()
    {
        for (int i = 0; i < squares.Length; i++)
        {
            squares[i].GetComponent<SpriteRenderer>().color = Color.white;
        }
        setText(""); 
    }
}
