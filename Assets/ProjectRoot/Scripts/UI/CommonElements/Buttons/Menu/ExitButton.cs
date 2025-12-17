using UnityEngine;

public class ExitButton : CommonButton
{
    protected override void HandleClick()
    {
        Debug.Log("Exit");
        Application.Quit();
    }
}