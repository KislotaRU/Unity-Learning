using System.Collections.Generic;
using UnityEngine;

public class HandlerCommand
{
    private Queue<ICommand> _commandQueue;
    private ICommand _currentCommand;

    public HandlerCommand()
    {
        _commandQueue = new Queue<ICommand>();
    }

    public void Enqueue(ICommand command)
    {
        _commandQueue.Enqueue(command);

        Process();
    }

    public void Clear()
    {
        _commandQueue.Clear();
        _currentCommand?.Undo();
    }

    private void Process()
    {
        if (_currentCommand != null && !_currentCommand.IsCompleted)
            return;

        if (_commandQueue.Count > 0)
        {
            _currentCommand = _commandQueue.Dequeue();
            _currentCommand.Execute();

            _currentCommand.Completed += HandleCommandCompleted;
        }
    }

    private void HandleCommandCompleted()
    {
        _currentCommand.Completed -= HandleCommandCompleted;
        _currentCommand = null;

        Process();
    }
}