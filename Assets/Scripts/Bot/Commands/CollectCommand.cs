using System.Collections;
using UnityEngine;

public class CollectCommand : BotCommand
{
    private readonly CollectingConfiguration _configuration;
    private readonly Item _item;

    public CollectCommand(CollectingConfiguration collectingConfiguration, Item item)
    {
        _configuration = collectingConfiguration;
        _item = item;
    }

    public override IEnumerator Execute(Bot bot)
    {
        yield return Collect(bot.transform.position, _item, bot.Storage);
    }

    public IEnumerator Collect(Vector3 position, Item item, Transform storage)
    {
        Collider[] colliders = Physics.OverlapSphere(position, _configuration.CaptureRadius, _configuration.TargetMask);

        foreach (Collider collider in colliders)
        {
            if (collider.TryGetComponent(out Item temporaryItem) == false)
                continue;

            if (temporaryItem.GetType() != item.GetType())
                continue;

            item.transform.SetParent(storage);
            item.transform.position = storage.position;

            item.HandleCollect();
            break;
        }

        yield return null;
    }
}