using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PasswordRessetButton : MonoBeh
{

    [SerializeField] Button button;
    public override void OnAwake()
    {
        base.OnAwake();
        button = GetComponent<Button>();
        button.onClick.AddListener(OnClick);
    }

    void OnClick()
    {
        var pref = Storage.Instance.menuStorage.PasswordRessetMenu;
        GameObject.Instantiate(pref);
    }
}
