using System.Collections;

public interface IBotCommand
{
    IEnumerator Execute(Bot unit);
}