using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReaderAnswer
{
    public bool selected;
    public bool correct;
    public string text;

    public ReaderAnswer(bool correct, string text)
    {
        this.correct = correct;
        this.text = text;
    }


    public bool ChosenCorrectly() => selected == correct;
}
