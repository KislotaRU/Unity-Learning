using UnityEngine;

public class ExitButton : ActionButton
{
    protected override void OnClick()
    {
        Debug.Log("Exit");
        Application.Quit();
    }
}