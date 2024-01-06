using System.Collections;
using System.Collections.Generic;
using System.Data;
using Mono.Data.Sqlite;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class GridButton : MonoBehaviour
{
    [SerializeField] TMP_Text text;
    [SerializeField] protected Button button;
    [SerializeField] protected string RemovalText;
    [SerializeField] string DBName;
    public string myCaption;
    public void SetName(string name)
    {
        text.text = name;
        myCaption = name;
        button.onClick.AddListener(OnClick);
    }
    public string GetName() => text.text    ; 
    public virtual void Removal()
    {
        string connectionString = SetDataBaseClass.SetDataBase(DBName + ".db");
        var connection = new SqliteConnection(connectionString);

        using (connection)
        {
            connection.Open();
            IDbCommand command = connection.CreateCommand();
            
            using (command)
            {
                command.CommandText = RemovalText;
                Debug.Log(command.CommandText);
                command.ExecuteNonQuery();
            }

        }

        GridControlle.Instance.Refresh();

    }
    public virtual void Selection() { }
    public virtual void Redact() 
    {
        var editScript = GameObject.Instantiate(Storage.Instance.menuStorage.RedactMenu).GetComponent<EditorMenu>();
        editScript.prevText = myCaption;
        editScript.OnAwake();
    }
    
    public void OnClick()
    {
        switch (RedactorController.Instance.mode)
        {
            case RedactorController.Mode.neutral:
                {
                    Selection();
                    break;
                }
            case RedactorController.Mode.DeleteMode:
                {
                    Removal();
                    break;
                }
            case RedactorController.Mode.EditMode:
                {
                    Redact();
                    break;
                }

        }
    }


    public void SetColor(UnityEngine.Color color)
    {
        button.image.color = color;
    }
}
