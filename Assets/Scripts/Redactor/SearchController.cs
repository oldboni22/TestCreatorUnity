using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Text;
using System;

public class SearchController : MonoSingletone<SearchController>
{
    [SerializeField] TMP_InputField input;

    public override void OnAwake()
    {
        Console.OutputEncoding = Encoding.UTF8;


        base.OnAwake();
        input.onValueChanged.AddListener(Search);
    }

    void Search(string keyWord)
    {
        GridControlle.Instance.Search(keyWord.ToLower());
    }

    public void Clear()
    {
        input.text = string.Empty;

    }

    public void Block()
    {
        input.interactable = false;
    }
    public void UnBlock()
    {
        input.interactable= true;
    }
}
