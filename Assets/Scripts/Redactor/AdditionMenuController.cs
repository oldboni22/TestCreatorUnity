using UnityEngine;
using System.Data;
using Mono.Data.Sqlite;
using TMPro;
using UnityEngine.UI;

public class AdditionMenuController : MonoBehaviour
{
    [Header("Connection")]
    [SerializeField] string DBName;
    [SerializeField] GridControlle.ObjectMode mode;

    [Header("UI")]
    [SerializeField] TMP_InputField inputField;
    [SerializeField] GameObject menu;
    [SerializeField] TMP_Text text;
    [SerializeField] TMP_Text placeholderText;
    [SerializeField] GameObject toggleObject;
    [SerializeField] Toggle correctToggle;
    public void Cancel() => Destroy(menu);

    public void Add()
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
                    commandText = $"Insert Into ����('��������','low') VALUES('{inputField.text}','{inputField.text.ToLower()}')";
                    break;
                }
            case GridControlle.ObjectMode.Test:
                {
                    commandText = $"Insert Into �����('��������','low','Id_����') values('{inputField.text}','{inputField.text.ToLower()}',{GridControlle.Instance.CurThemeId})";
                    break;
                }
            case GridControlle.ObjectMode.Question:
                {
                    commandText = $"insert into �������('�����','low',Id_�����) values('{inputField.text}','{inputField.text.ToLower()}',{GridControlle.Instance.CurTestId});";
                    break;
                }
            case GridControlle.ObjectMode.Answer:
                {
                    int correctInterpritation = correctToggle.isOn ? 1 : 0;
                    commandText = $"insert into ������('�����������','����������',Id�������) values('{inputField.text}',{correctInterpritation},{GridControlle.Instance.CurQuId});";
                    break;
                }

        }
        return commandText;
    }

    private void Start()
    {
        mode = GridControlle.Instance.ObjectModeVar;
        inputField.textComponent.enableWordWrapping = true;
        switch (mode)
        {
            case GridControlle.ObjectMode.Theme:
                {
                    text.text = "�������� ����";
                    placeholderText.text = "������� �������� ����";
                    break;
                }
            case GridControlle.ObjectMode.Test:
                {
                    text.text = "�������� ����";
                    placeholderText.text = "������� �������� �����";
                    break;
                }
            case GridControlle.ObjectMode.Question:
                {
                    text.text = "�������� ������";
                    placeholderText.text = "������� ����� �������";
                    break;
                }
            case GridControlle.ObjectMode.Answer:
                {
                    toggleObject.SetActive(true);
                    text.text = "�������� �����";
                    placeholderText.text = "������� ����� ������";
                    break;
                }


        }
    }

}
