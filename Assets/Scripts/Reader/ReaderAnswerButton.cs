
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ReaderAnswerButton : MonoBeh
{
    [SerializeField] string buttonName;
    [SerializeField] Button button;
    public delegate void deleg(ReaderAnswerButton button);
    [SerializeField] bool _selected;
    [SerializeField] TMP_Text text;
    [SerializeField] ReaderAnswer answer;

    [SerializeField] bool _blocked;
    public bool blocked
    {
        set
        {
            _blocked = value;
            button.interactable = !value;
        }
    }

    public bool Selected => _selected;

    public bool Correct => answer.correct;
    
    
    public override void OnAwake()
    {
        base.OnAwake();
        _selected = false;
        button = GetComponent<Button>();
        blocked = false;

    }

    void SetText(string text) => this.text.text = text;

    public void SetColor(Color color) => button.image.color = color;
    public void Refresh()
    {
        _selected = false;
    }

    public void AddListener(deleg deleg)
    {
        this.button.onClick.AddListener(delegate{deleg(this);});
    }

    public void Select()
    {
        _selected = !_selected;
        answer.selected = _selected;
        Debug.LogWarning(_selected);
    }

    public void SetAnswer(ReaderAnswer answer)
    {
        this.answer = answer;
        SetText(answer.text);
    }

}
