using System.Collections;
using UnityEngine;

public class RotateCommand : BotCommand
{
    private readonly MovingConfiguration _configuration;
    private readonly Vector3 _targetPosition;

    public RotateCommand(MovingConfiguration movingConfiguration, Vector3 targetPosition)
    {
        _configuration = movingConfiguration;
        _targetPosition = targetPosition;
    }

    public override IEnumerator Execute(Bot bot)
    {
        yield return RotateTo(bot, _targetPosition);
    }

    public IEnumerator RotateTo(Bot bot, Vector3 targetPosition)
    {
        float dotThreshold = 1f;

        Vector3 direction;
        Quaternion targetRotation;

        direction = targetPosition - bot.transform.position;
        targetRotation = Quaternion.LookRotation(bot.transform.forward);

        while (Vector3.Dot(bot.transform.forward, direction.normalized) < dotThreshold)
        {
            if (direction != Vector3.zero)
                targetRotation = Quaternion.LookRotation(direction);

            bot.transform.rotation = Quaternion.Slerp(bot.transform.rotation, targetRotation, _configuration.RotateSpeed * Time.deltaTime);
            direction = targetPosition - bot.transform.position;

            yield return null;
        }

        bot.transform.rotation = targetRotation;
    }
}