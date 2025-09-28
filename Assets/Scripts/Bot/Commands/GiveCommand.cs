using System.Collections;
using UnityEngine;

public class GiveCommand : BotCommand
{
    private readonly StatValue _resourcesCapacity;
    private readonly Item _item;

    public GiveCommand(Item item, StatValue resourcesCapacity)
    {
        _resourcesCapacity = resourcesCapacity;
        _item = item;
    }

    public override IEnumerator Execute(Bot bot)
    {
        yield return GiveItem(_item, bot.Storage, _resourcesCapacity);
    }

    public IEnumerator GiveItem(Item item, Transform botStorage, StatValue resourcesCapacity)
    {
        for (int i = 0; i < botStorage.transform.childCount; i++)
        {
            if (botStorage.GetChild(i).TryGetComponent(out Item temporaryItem) == false)
                continue;

            if (temporaryItem.GetType() != item.GetType())
                continue;

            resourcesCapacity.Increase();
            item.HandleDestroy();
        }

        yield return null;
    }
}