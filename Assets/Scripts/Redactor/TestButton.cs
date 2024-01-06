using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestButton : GridButton
{
    [SerializeField] int testId;
    public int TestId { get { return testId; } set { testId = value; } }


    public override void Selection()
    {
        CaptionsController.Instance.SetCaption(myCaption);
        GridControlle.Instance.OpenQuestions(testId);
        CaptionsController.Instance.UpdateCaption();
    }

    public override void Removal()
    {
        RemovalText = $"delete from 'Тесты' where IdТеста = {testId}";
        base.Removal();
    }

    public override void Redact()
    {
        GridControlle.Instance.CurTestId = testId;
        base.Redact();
    }
}
