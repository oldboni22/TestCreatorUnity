using UnityEngine;
using TMPro;
public class CaptionsController : MonoSingletone<CaptionsController>
{
    [SerializeField] TMP_Text text;

    public string theme, test, question;

    public void SetCaption(string caption)
    {
        switch (GridControlle.Instance.ObjectModeVar)
        {
            case GridControlle.ObjectMode.Theme: theme = caption; break;
            case GridControlle.ObjectMode.Test: test = caption; break;
            case GridControlle.ObjectMode.Question: question = caption; break;
        }

    }
    public void UpdateCaption()
    {
        switch (GridControlle.Instance.ObjectModeVar)
        {
            case GridControlle.ObjectMode.Theme: text.text = string.Empty; break;
            case GridControlle.ObjectMode.Test: text.text = theme; break;
            case GridControlle.ObjectMode.Question: text.text = test; break;
            case GridControlle.ObjectMode.Answer: text.text = question; break;
        }

    }
}
