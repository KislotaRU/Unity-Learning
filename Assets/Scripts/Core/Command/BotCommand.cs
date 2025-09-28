using System.Collections;

public abstract class BotCommand : IBotCommand
{
    public abstract IEnumerator Execute(Bot unit);
}