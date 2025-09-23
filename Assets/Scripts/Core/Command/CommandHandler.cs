using System;
using System.Collections.Generic;

public class CommandHandler
{
    private readonly Queue<ICommand> _commands;

    public event Action Completed;

    private ICommand _currentCommand;

    public bool IsProcess => _currentCommand != null || _commands.Count > 0;

    public CommandHandler()
    {
        _commands = new Queue<ICommand>();
    }

    public void Enqueue(ICommand command)
    {
        _commands.Enqueue(command);

        Process();
    }

    public void Clear()
    {
        _commands.Clear();
        _currentCommand?.Undo();
    }

    private void Process()
    {
        if (_currentCommand?.IsCompleted == false)
            return;

        if (_commands.Count == 0)
        {
            _currentCommand = null;
            Completed?.Invoke();

            return;
        }

        _currentCommand = _commands.Dequeue();
        _currentCommand.Completed += HandleCompleted;

        _currentCommand.Execute();
    }

    private void HandleCompleted()
    {
        _currentCommand.Completed -= HandleCompleted;

        Process();
    }
}