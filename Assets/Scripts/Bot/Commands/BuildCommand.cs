using System.Collections;

public class BuildCommand : BotCommand
{
    private readonly Builder _builder;

    public BuildCommand(Builder builder)
    {
        _builder = builder;
    }

    public override IEnumerator Execute(Bot bot)
    {
        yield return Build(bot, _builder);
    }

    public IEnumerator Build(Bot bot, Builder builder)
    {
        Facility newFacility = builder.Build();

        bot.CurrentFacility.RemoveBot(bot);

        bot.SetFacility(newFacility);

        bot.CurrentFacility.AddBot(bot);

        yield return null;
    }
}