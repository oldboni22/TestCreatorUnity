using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThemeButton : GridButton
{
    [SerializeField] int themeId;
    public int ThemeId { get { return themeId; } set {  themeId = value; } }


    public override void Selection()
    {
        CaptionsController.Instance.SetCaption(myCaption);
        GridControlle.Instance.OpenTests(themeId);
        CaptionsController.Instance.UpdateCaption();
    }

    public override void Removal()
    {
        RemovalText = $"DELETE FROM 'Темы' WHERE IdТемы = {themeId};";
        base.Removal();
    }

    public override void Redact()
    {
        GridControlle.Instance.CurThemeId = themeId;
        base.Redact();
    }
}
