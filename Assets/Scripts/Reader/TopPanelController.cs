using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TopPanelController : MonoSingletone<TopPanelController>
{
    [SerializeField] List<Button> buttons;
    [SerializeField] Transform panelTransform;
    [SerializeField] TMP_Text text;
    void OpenQuestion(ReaderQuestion question)
    {
        ReaderAnswerButtonsController.Instance.SetQuestion(question);
        text.text = question.text;
    }
    void BlockButton(Button button)
    {
        foreach(var butt in buttons)
        {
            butt.interactable = true;
        }
        button.interactable = false;
    }

    public void AddButton(ReaderQuestion question)
    {
        var prefab = GameObject.Instantiate(Storage.Instance.prefabs.ReaderTopGridButton,panelTransform);
        var newButton = prefab.GetComponent<Button>();     
        newButton.onClick.AddListener(delegate { OpenQuestion(question); BlockButton(newButton);});
        buttons.Add(newButton);
        newButton.GetComponentInChildren<TMP_Text>().text = $"{buttons.Count}";
    }

    public void StartTest()
    {
        buttons[0].onClick.Invoke();
    }

}