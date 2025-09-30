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
        yield return Build(_builder);
    }

    public IEnumerator Build(Builder builder)
    {
        builder.Build();

        yield return null;
    }
}