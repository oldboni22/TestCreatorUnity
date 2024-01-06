using Mono.Data.Sqlite;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using TMPro;
using UnityEngine;

public class TestController : MonoSingletone<TestController>
{
    [SerializeField] ReaderAnswerButtonsController buttonsController;
    [SerializeField] TopPanelController topPanelController;
    [SerializeField] ReaderQuestion[] questions;
    [SerializeField] TMP_Text correctCountText;
    public override void OnAwake()
    {
        base.OnAwake();
        buttonsController = ReaderAnswerButtonsController.Instance;
        topPanelController = TopPanelController.Instance;

        
        InstantinateQuestions(SceneTransitor.Instance.TestId);
        topPanelController.StartTest();


        buttonsController.blocked = false;
    }

    public void CheckButtonOnClick()
    {
        buttonsController.blocked = true;
        buttonsController.HighlightCorrect();

        correctCountText.text = $"{CorrectQuestionsCount()}/{questions.Length}";
        correctCountText.gameObject.SetActive(true);

    }
    
    int CorrectQuestionsCount()
    {
        int count = 0;
        foreach(var question in questions)
        {
            if(question.AnsweredCorrecty())
                count++;
        }
        return count;
    }




    void InstantinateQuestions(int testId)
    {
        Debug.Log("Query");

        string connectionString = SetDataBaseClass.SetDataBase("DB.db");
        var connection = new SqliteConnection(connectionString);
        string commandText = $"Select * from 'Вопросы' Where Id_Теста = {testId};";

        using (connection)
        {
            connection.Open();
            IDbCommand command = connection.CreateCommand();
            using (command)
            {
                command.CommandText = commandText;
                IDataReader reader = command.ExecuteReader();
                using (reader)
                {

                    
                    List<ReaderQuestion> questions1 = new List<ReaderQuestion>();

                    Debug.Log(commandText);
                    int i = 0;
                    while (reader.Read())
                    {
                        int ind = reader.GetInt32(0);
                        string text = reader.GetString(2);
                        questions1.Add(new ReaderQuestion(ind,text));
                        i++;
                    }

                    int arrayLen = i >= 10 ? 10 : i;
                    questions = new ReaderQuestion[arrayLen];


                    for(i = 0; i < arrayLen; i++)
                    {
                        questions[i] = GetRandomFromList(questions1);
                        topPanelController.AddButton(questions[i]);
                    }

                }
            }

        }
    }

    ReaderQuestion GetRandomFromList(List<ReaderQuestion> questionsList)
    {
        int ind = 0;
        Debug.Log(questionsList.Count - 1);
        ind = Random.Range(0, questionsList.Count);

        Debug.Log(ind);

        var retQuestion = questionsList[ind];
        questionsList.Remove(questionsList[ind]);

        return retQuestion;
    }


}


