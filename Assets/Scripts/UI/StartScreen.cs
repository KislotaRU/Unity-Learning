using System;

public class StartScreen : Window
{
    public event Action PlayButtonClicked;

    public override void Close()
    {
        WindowGroup.alpha = 0f;
        WindowGroup.blocksRaycasts = false;
        ActionButton.interactable = false;
    }

    public override void Open()
    {
        WindowGroup.alpha = 1f;
        WindowGroup.blocksRaycasts = true;
        ActionButton.interactable = true;
    }

    protected override void HandleButtonClick()
    {
        PlayButtonClicked?.Invoke();
    }
}