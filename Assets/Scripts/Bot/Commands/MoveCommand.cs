using System.Collections;
using UnityEngine;

public class MoveCommand : BotCommand
{
    private readonly MovingConfiguration _configuration;
    private readonly Vector3 _targetPosition;

    public MoveCommand(MovingConfiguration movingConfiguration, Vector3 targetPosition)
    {
        _configuration = movingConfiguration;
        _targetPosition = targetPosition;
    }

    public override IEnumerator Execute(Bot bot)
    {
        yield return MoveTo(bot, _targetPosition);
    }

    public IEnumerator MoveTo(Bot bot, Vector3 targetPosition)
    {
        Vector3 direction = targetPosition - bot.transform.position;

        while (direction.sqrMagnitude > 0)
        {
            bot.transform.position = Vector3.MoveTowards(bot.transform.position, targetPosition, _configuration.MoveSpeed * Time.deltaTime);
            direction = targetPosition - bot.transform.position;

            yield return null;
        }
    }
}