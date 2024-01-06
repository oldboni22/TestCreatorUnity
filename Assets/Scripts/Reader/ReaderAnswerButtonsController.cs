using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReaderAnswerButtonsController : MonoSingletone<ReaderAnswerButtonsController>
{
    [SerializeField] ReaderAnswerButton[] buttons;
    [SerializeField] Color defaultColor; 
    [SerializeField] Color selectedColor;

    [SerializeField] bool _blocked;
    public bool blocked
    {
        set
        {
            _blocked = value;
            foreach (var button in buttons)
            {
                button.blocked = value;
            }
        }
    }

    public void HighlightCorrect()
    {
        Color col;
        foreach (var button in buttons)
        {
            if (!button.isActiveAndEnabled)
            {
                return;
            }

            if(button.Selected)
            {
                if (button.Correct)
                {
                    col = Color.green;
                    col.a = 0.5f;
                    button.SetColor(col);
                }
                else
                {
                    col = Color.red;
                    col.a = 0.5f;
                    button.SetColor(col);
                }
                continue;
            }
            else if (button.Correct)
            {
                col = Color.yellow;
                col.a = 0.5f;
                button.SetColor(col);
            }

        }
    }

    public override void OnAwake()
    {
        base.OnAwake();

        foreach (var button in buttons) 
        {
            button.OnAwake();
            button.AddListener(delegate { ButtonOnClick(button); });
        }

    }

    void ButtonOnClick(ReaderAnswerButton button)
    {
        button.Select();
        var curButtonColor = button.Selected ? selectedColor : defaultColor;
        button.SetColor(curButtonColor);
    }


    public void SetQuestion(ReaderQuestion question)
    {
        foreach(var button in buttons)
        {
            button.gameObject.SetActive(false);
            button.SetColor(defaultColor);
            button.Refresh();
        }
        

        for(int i = 0; i < question.answers.Count; i++)
        {
            buttons[i].gameObject.SetActive(true);
            buttons[i].SetAnswer(question.answers[i]);

            if (question.answers[i].selected)
            {   
                buttons[i].SetColor(selectedColor);
                buttons[i].Select();
            }

        }

        if (_blocked)
            HighlightCorrect();

    }

}
