using System.Collections.Generic;
using System.Data;
using Mono.Data.Sqlite;
using TMPro;
using UnityEngine;

public class GridControlle : MonoSingletone<GridControlle>
{

    delegate void AddDelegate();

    [Header("Connection")]
    [SerializeField] string DBName;
    IDataReader reader;

    [Header("ID")]

    [SerializeField] int _curThemeId;
    public int CurThemeId { get => _curThemeId; set => _curThemeId = value; }

    [SerializeField] int _curTestId;
    public int CurTestId { get => _curTestId; set => _curTestId = value; }

    [SerializeField] int _curQuId;
    public int CurQuId { get => _curQuId; set => _curQuId = value; }

    [SerializeField] int _curAnswId;
    public int CurAnswId { get => _curAnswId; set => _curAnswId = value; }

    [Header("Grid")]
    [SerializeField] Transform gridTransform;
    [SerializeField] Transform answerGridTransform;
    [SerializeField] List<GridButton> gridButtons;

    [Header("UI")]
    [SerializeField] TMP_Text modeText;


    void ClearGrid()
    {
        foreach (var button in gridButtons.ToArray())
        {
            Destroy(button.gameObject);
            gridButtons.Remove(button);
        }
    }


    public enum ObjectMode
    {
        Theme, Test, Question, Answer,
    }
    [SerializeField] ObjectMode objectMode;
    public ObjectMode ObjectModeVar { get => objectMode; set => objectMode = value; }

    void OpenThemes()
    {
        objectMode = ObjectMode.Theme;
        Refresh();
    }
    public void OpenTests(int ThemeId)
    {
        objectMode = ObjectMode.Test;
        _curThemeId = ThemeId;
        Refresh();
    }
    public void OpenQuestions(int testId)
    {
        objectMode = ObjectMode.Question;
        _curTestId = testId;
        Refresh();
    }
    public void OpenAnswers(int quId)
    {
        objectMode = ObjectMode.Answer;
        _curQuId = quId;
        Refresh();
    }




    public override void OnAwake()
    {
        base.OnAwake();

        objectMode = ObjectMode.Theme;
        Refresh();
    }

    public void Search(string keyWord)
    {
        Debug.Log("Search");
        ClearGrid();

        switch (objectMode)
        {
            case ObjectMode.Theme:
                {
                    Query(searchQuery(keyWord), AddTheme);
                    break;
                }
            case ObjectMode.Test:
                {
                    Query(searchQuery(keyWord), AddTest);
                    break;
                }
            case ObjectMode.Question:
                {
                    Query(searchQuery(keyWord), AddQuestion);
                    break;
                }

        }


    }
    string searchQuery(string keyWord)
    {
        string quer = "";


        switch (objectMode)
        {
            case ObjectMode.Theme: quer = $"Select * from Темы where low like Lower('%{keyWord}%')"; break;
            case ObjectMode.Test: quer = $"Select * from Тесты where Id_Темы = {_curThemeId} and low like Lower('%{keyWord}%')"; break;
            case ObjectMode.Question: quer = $"Select * from Вопросы where Id_Теста = {_curTestId} and low like Lower('%{keyWord}%')"; break;
        }

        return quer;
    }

    public void Refresh()
    {
        Debug.Log("Refresh");
        modeText.text = ModeText();
        ClearGrid();

        gridTransform.gameObject.SetActive(true);
        answerGridTransform.gameObject.SetActive(false);
        SearchController.Instance.UnBlock();

        RedactorController.Instance.UnBlockAddButton();

        switch (objectMode)
        {
            case ObjectMode.Theme:
                {
                    Query(ThemesQuery(), AddTheme);
                    break;
                }
            case ObjectMode.Test:
                {
                    Query(TestQuery(), AddTest);
                    break;
                }
            case ObjectMode.Question:
                {
                    Query(QuestionQuery(), AddQuestion);
                    break;


                }
            case ObjectMode.Answer:
                {
                    gridTransform.gameObject.SetActive(false);
                    answerGridTransform.gameObject.SetActive(true);
                    SearchController.Instance.Block();

                    Query(AnswerQuery(), AddAnswer);
                    if (gridButtons.Count >= 4)
                        RedactorController.Instance.BlockAddButton();

                    break;
                }
        }

        SearchController.Instance.Clear();

    }


    void Query(string commandText, AddDelegate addDelegate)
    {
        Debug.Log("Query");

        string connectionString = SetDataBaseClass.SetDataBase(DBName + ".db");
        var connection = new SqliteConnection(connectionString);

        using (connection)
        {
            connection.Open();
            IDbCommand command = connection.CreateCommand();
            using (command)
            {
                command.CommandText = commandText;
                reader = command.ExecuteReader();
                using (reader)
                {
                    Debug.Log(commandText);

                    while (reader.Read())
                    {
                        addDelegate();
                    }
                }
            }

        }
    }



    void AddTheme()
    {
        string themeName = reader.GetString(1);
        Debug.Log(themeName);

        Debug.Log($"{themeName} added");

        GameObject pref = Storage.Instance.prefabs.themeButtonPrefab;
        GameObject obj = GameObject.Instantiate(pref, gridTransform);

        var gridButton = obj.GetComponent<ThemeButton>();
        gridButton.SetName(themeName);
        
        gridButton.ThemeId = reader.GetInt32(0);

        gridButtons.Add(gridButton);

    }
    void AddTest()
    {
        string testName = reader.GetString(2);
        Debug.Log(testName);

        Debug.Log($"{testName} added");

        GameObject pref = Storage.Instance.prefabs.testButtonPrefab;
        GameObject obj = GameObject.Instantiate(pref, gridTransform);

        var gridButton = obj.GetComponent<TestButton>();
        gridButton.SetName(testName);
        gridButton.TestId = reader.GetInt32(0);

        gridButtons.Add(gridButton);

        if(reader.GetInt32(4) == 1)
        {
            UnityEngine.Color color = new Color(0.25f, 0.8f, 0.25f);
            gridButton.SetColor(color);
        }

    }

    void AddQuestion()
    {


        GameObject pref = Storage.Instance.prefabs.quButtonPrefab;
        GameObject obj = GameObject.Instantiate(pref, gridTransform);

        string quName = reader.GetString(2);
        Debug.Log($"{quName} added");

        var gridButton = obj.GetComponent<QuestionButton>();
        gridButton.SetName(quName);
        gridButton.QuId = reader.GetInt32(0);

        gridButtons.Add(gridButton);

    }

    void AddAnswer()
    {

        GameObject pref = Storage.Instance.prefabs.answButtonPrefab;
        GameObject obj = GameObject.Instantiate(pref, answerGridTransform);

        string answText = reader.GetString(2);
        Debug.Log($"{answText} added");



        var gridButton = obj.GetComponent<AnswerButton>();
        gridButton.SetName(answText);
        gridButton.AnswId = reader.GetInt32(0);
        if (reader.GetInt32(3) == 1)
        {
            gridButton.SetCorrectColor();
        }

        gridButtons.Add(gridButton);

    }

    string ThemesQuery() => $"select * from 'Темы';";
    string TestQuery() => $"Select * from 'Тесты' Where Id_Темы = {_curThemeId};";
    string QuestionQuery() => $"Select * from 'Вопросы' Where Id_Теста = {_curTestId};";
    string AnswerQuery() => $"Select * from 'Ответы' Where IdВопроса = {_curQuId};";


    public void BackButton()
    {
        switch (objectMode)
        {
            case ObjectMode.Theme:
                {
                    SceneTransitor.Instance.OpenScene("MainScene");
                    break;
                }

            case ObjectMode.Test:
                {
                    OpenThemes();
                    break;
                }
            case ObjectMode.Question:
                {
                    OpenTests(_curThemeId);
                    break;
                }
            case ObjectMode.Answer:
                {
                    OpenQuestions(_curTestId);
                    break;
                }
        }

        CaptionsController.Instance.UpdateCaption();
        RedactorController.Instance.SetNeutral();
        SearchController.Instance.Clear();
    }

    string ModeText()
    {
        string text = null;
        switch (objectMode)
        {
            case ObjectMode.Theme:
                {
                    text = "Темы";
                    break;
                }

            case ObjectMode.Test:
                {
                    text = "Тесты";
                    break;
                }
            case ObjectMode.Question:
                {
                    text = "Вопросы";
                    break;
                }
            case ObjectMode.Answer:
                {
                    text = "Ответы";
                    break;
                }
        }
        return text;
    }

}
