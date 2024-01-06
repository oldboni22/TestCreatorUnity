using Mono.Data.Sqlite;
using System.Data;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EditorMenu : MonoBeh
{
    [Header("Connection")]
    [SerializeField] string DBName;
    [SerializeField] GridControlle.ObjectMode mode;

    [Header("UI")]
    [SerializeField] TMP_InputField inputField;
    [SerializeField] GameObject menu;
    [SerializeField] TMP_Text text;
    [SerializeField] TMP_Text previousText;
    [SerializeField] TMP_Text placeholderText;


    [SerializeField] GameObject toggleObject;
    [SerializeField] Toggle correctToggle;
    [SerializeField] TMP_Text toggleText;

    public string prevText;

    public void Cancel() => Destroy(menu);


    public void Edit()
    {

        string connectionString = SetDataBaseClass.SetDataBase(DBName + ".db");
        var connection = new SqliteConnection(connectionString);

        using (connection)
        {
            connection.Open();
            IDbCommand command = connection.CreateCommand();
            using (command)
            {
                command.CommandText = CommandText();
                Debug.Log(CommandText());
                IDataReader reader = command.ExecuteReader();
                using (reader)
                {
                }

            }
        }

        GridControlle.Instance.Refresh();
        Cancel();

    }


    string CommandText()
    {
        string commandText = null;
        switch (mode)
        {
            case GridControlle.ObjectMode.Theme:
                {
                    commandText = $"Update Темы set 'Название' = \"{inputField.text}\",'low' = \"{inputField.text.ToLower()}\"  where IdТемы = {GridControlle.Instance.CurThemeId};";
                    break;
                }
            case GridControlle.ObjectMode.Test:
                {
                    int visibleInterpritation = correctToggle.isOn ? 1 : 0;
                    commandText = $"Update Тесты set 'Название' = \"{inputField.text}\",'low' = \"{inputField.text.ToLower()}\",'visible' = {visibleInterpritation} where IdТеста = {GridControlle.Instance.CurTestId};";
                    break;
                }
            case GridControlle.ObjectMode.Question:
                {
                    commandText = $"Update Вопросы set 'Текст' = \"{inputField.text}\",'low' = \"{inputField.text.ToLower()}\" where IdВопроса = {GridControlle.Instance.CurQuId}";
                    break;
                }
            case GridControlle.ObjectMode.Answer:
                {
                    int correctInterpritation = correctToggle.isOn ? 1 : 0;
                    commandText = $"Update Ответы set 'ТекстОтвета' = \"{inputField.text}\", 'Правильный' = {correctInterpritation} where IdОтвета = {GridControlle.Instance.CurAnswId}";
                    break;
                }

        }
        return commandText;
    }

    public override void OnAwake()
    {
        mode = GridControlle.Instance.ObjectModeVar;
        previousText.text = prevText;
        inputField.text = prevText;

        switch (mode)
        {
            case GridControlle.ObjectMode.Theme:
                {
                    
                    text.text = "Изменить тему";
                    placeholderText.text = "Введите новое название темы";
                    break;
                }
            case GridControlle.ObjectMode.Test:
                {
                    toggleObject.SetActive(true);
                    toggleText.text = "Видимый";
                    text.text = "Изменить тест";
                    placeholderText.text = "Введите новое название теста";
                    break;
                }
            case GridControlle.ObjectMode.Question:
                {
                    text.text = "Изменить вопрос";
                    placeholderText.text = "Введите новый текст вопроса";
                    break;
                }
            case GridControlle.ObjectMode.Answer:
                {
                    toggleObject.SetActive(true);
                    text.text = "Изменить ответ";
                    placeholderText.text = "Введите новый текст ответа";
                    break;
                }


        }
    }


}
