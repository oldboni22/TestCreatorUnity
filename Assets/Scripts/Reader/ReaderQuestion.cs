using Mono.Data.Sqlite;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class ReaderQuestion 
{
    public List<ReaderAnswer> answers;
    public string text;

    public ReaderQuestion(int questionId,string text)
    {
        Debug.Log("Query");

        string connectionString = SetDataBaseClass.SetDataBase("DB.db");
        var connection = new SqliteConnection(connectionString);
        string commandText = $"Select * from 'Ответы' Where IdВопроса = {questionId};";
        answers = new List<ReaderAnswer>();
        this.text = text;

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


                    Debug.Log(commandText);
                    int i = 0;
                    while(reader.Read())
                    {
                        bool correctInterpritation = reader.GetInt32(3) == 1;
                        string answerText = reader.GetString(2);
                        var newAnswer = new ReaderAnswer(correctInterpritation,answerText);
                        answers.Add(newAnswer);
                        i++;
                    }
                }
            }

        }
    }



    public bool AnsweredCorrecty()
    {

        bool result = true;
        foreach(var answ in answers)
        {
            if (answ.ChosenCorrectly())
                continue;

            result = false;
            break;
        }


        return result;
    }


}
