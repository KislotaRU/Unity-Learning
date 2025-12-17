using UnityEngine;

public class PlayButton : CommonButton
{
    protected override void HandleClick()
    {
        Debug.Log("Play");
    }
}