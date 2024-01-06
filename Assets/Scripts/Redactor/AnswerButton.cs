using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnswerButton : GridButton
{
    [SerializeField] int answId;
    public int AnswId { get { return answId; } set { answId = value; } }

    public override void Removal()
    {
        RemovalText = $"delete from 'Ответы' where IdОтвета = {answId}";
        base.Removal();
    }
    public override void Redact()
    {
        GridControlle.Instance.CurAnswId = answId;
        base.Redact();
    }

    public void SetCorrectColor()
    {
        UnityEngine.Color color = new Color(0.25f,0.8f,0.25f);
        button.image.color = color;
    }
}
