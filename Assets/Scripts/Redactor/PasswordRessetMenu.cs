using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PasswordRessetMenu : MonoBehaviour
{
    [SerializeField] TMP_InputField inputField;
    [SerializeField] TMP_Text prevPasswordText;

    private void Awake()
    {
        prevPasswordText.text = PlayerPrefs.GetString("ADMIN_PASSWORD");
    }

    public void ChangePassword()
    {
        PlayerPrefs.SetString("ADMIN_PASSWORD", inputField.text);
        PlayerPrefs.Save();
        
    }

    public void Exit()
    {
        Destroy(gameObject);
    }

}
