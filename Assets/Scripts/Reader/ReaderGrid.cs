using System.Data;
using System.Collections.Generic;
using System.Linq;
using Mono.Data.Sqlite;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Unity.VisualScripting;

public class ReaderGrid : MonoSingletone<ReaderGrid>
{
    [SerializeField] List<Button> buttons;
    [SerializeField] Transform gridTransform;
    [SerializeField] TMP_InputField inputField;
    [SerializeField] Button backButton;

    [SerializeField] string AdminPassword;
    enum GridState
    {
        theme, test,
    }

    GridState state;
    int themeId;

    public override void OnAwake()
    {
        base.OnAwake();
        inputField.onValueChanged.AddListener(Search);
        backButton.onClick.AddListener(BackButton);
        AdminPassword = PlayerPrefs.GetString("ADMIN_PASSWORD");
        state = GridState.theme;
        LoadGrid();
    }

    void LoadGrid()
    {
        ClearGrid();

        string queryTxt;

        if (state == GridState.theme)
            queryTxt = $"Select * from Темы";
        else queryTxt = $"Select * from Тесты where Id_Темы = {themeId} and visible = 1";

        var conStr = SetDataBaseClass.SetDataBase("DB.db");
        var connection = new SqliteConnection(conStr);
        using (connection)
        {
            connection.Open();
            using (IDbCommand command = connection.CreateCommand())
            {
                command.CommandText = queryTxt;
                using (IDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {

                        AddButton(reader);

                    }
                }
            }

        }
    }
    void AddButton(IDataReader r)
    {
        var pref = Storage.Instance.prefabs.GridButton;

        var button = GameObject.Instantiate(pref,gridTransform).GetComponent<Button>();
        buttons.Add(button);


        TMP_Text text = button.GetComponentInChildren<TMP_Text>();

        int id = r.GetInt32(0);
        

        if (state == GridState.theme) 
        {
            text.text = r.GetString(1);
        }
        else 
        {
            text.text = r.GetString(2);
        }

        button.onClick.AddListener(delegate { GridButtonOnClick(id);});

    }
    void GridButtonOnClick(int id)
    {
        if (state == GridState.theme)
        {
            themeId = id;
            state = GridState.test;
            LoadGrid();
        }
        else
        {
            SceneTransitor.Instance.OpenTest(id);
        }
    }

    void ClearGrid()
    {
        foreach (Button button in buttons.ToArray())
        {
            Destroy(button.gameObject);
            buttons.Remove(button);
        }
    }

    void BackButton()
    {
        if (state == GridState.theme)
        {
            Application.Quit();
        }
        else
        {
            state = GridState.theme;
            LoadGrid();
        }
    }

    void Search(string keyWord)
    {
        ClearGrid();

        string queryTxt;


        if (state == GridState.theme)
            queryTxt = $"Select * from Темы where low like Lower('%{keyWord}%')";
        else queryTxt = $"Select * from Тесты where Id_Темы = {themeId} and low like Lower('%{keyWord}%') and visible = 1";


        var conStr = SetDataBaseClass.SetDataBase("DB.db");
        var connection = new SqliteConnection(conStr);
        using (connection)
        {
            connection.Open();
            using (IDbCommand command = connection.CreateCommand())
            {
                command.CommandText = queryTxt;
                using (IDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {

                        AddButton(reader);

                    }
                }
            }

        }

        if (keyWord == AdminPassword)
        {
            SceneTransitor.Instance.OpenScene("TestEditor");
        }

    }


 

}
