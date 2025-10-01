using System.Collections;

public class TransitCommand : BotCommand
{
    private Facility _facility;

    public TransitCommand(Facility facility)
    {
        _facility = facility;
    }

    public override IEnumerator Execute(Bot bot)
    {
        yield return Transit(bot, _facility);
    }

    public IEnumerator Transit(Bot bot, Facility facility)
    {


        yield return null;
    }
}