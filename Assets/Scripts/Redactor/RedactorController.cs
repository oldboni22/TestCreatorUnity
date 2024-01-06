using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class RedactorController : MonoSingletone<RedactorController>
{
    [Header("Buttons")]
    [SerializeField] Button deleteButton;
    [SerializeField] Button editButton;
    [SerializeField] Button addButton;
    [SerializeField] Color defaultColor;
    [SerializeField] Color deleteColor;
    [SerializeField] Color editColor;

    public enum Mode
    {
        neutral, EditMode, DeleteMode
    }

    public Mode mode;

    public void OpenAddMenu()
    {
        GameObject.Instantiate(Storage.Instance.menuStorage.AddMenu);
        SetNeutral();
    }

    public void BlockAddButton()
    {
        addButton.interactable = false;
    }
    public void UnBlockAddButton()
    {
        addButton.interactable = true;
        addButton.image.color = defaultColor;
    }


    public void TogleDeleteMode()
    {
        if (mode == Mode.EditMode)
            TogleEditMode();

        mode = mode == Mode.DeleteMode? Mode.neutral : Mode.DeleteMode;
        deleteButton.image.color = mode != Mode.DeleteMode ? defaultColor : deleteColor;
    }
    public void TogleEditMode()
    {
        if(mode == Mode.DeleteMode)
            TogleDeleteMode();

        mode = mode == Mode.EditMode? Mode.neutral : Mode.EditMode;
        editButton.image.color = mode != Mode.EditMode ? defaultColor : editColor;
    }
    public void SetNeutral()
    {
        if (mode == Mode.DeleteMode)
            TogleDeleteMode();
        if (mode == Mode.EditMode)
            TogleEditMode();

        mode = Mode.neutral;
    }

}
