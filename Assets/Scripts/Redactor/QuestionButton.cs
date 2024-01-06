using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionButton : GridButton
{
    [SerializeField] int quId;
    public int QuId { get { return quId; } set { quId = value; } }


    public override void Selection()
    {
        CaptionsController.Instance.SetCaption(myCaption);
        GridControlle.Instance.OpenAnswers(quId);
        CaptionsController.Instance.UpdateCaption();
    }

    public override void Removal()
    {
        RemovalText = $"delete from 'Вопросы' where IdВопроса = {quId}";
        base.Removal();
    }
    public override void Redact()
    {
        GridControlle.Instance.CurQuId = quId;
        base.Redact();
    }
}
