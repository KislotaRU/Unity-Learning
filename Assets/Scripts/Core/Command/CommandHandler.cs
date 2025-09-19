using System;
using System.Collections.Generic;

public class CommandHandler
{
    private Queue<ICommand> _commandQueue;
    private ICommand _currentCommand;

    public event Action CompletedCommand;

    public bool IsProcess {  get; private set; }

    public CommandHandler()
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
        if (_currentCommand != null && _currentCommand.IsCompleted == false)
            return;

        if (_commandQueue.Count > 0)
        {
            IsProcess = true;

            _currentCommand = _commandQueue.Dequeue();
            _currentCommand.Execute();

            _currentCommand.Completed += HandleCommandCompleted;
        }
        else
        {
            IsProcess = false;
        }
    }

    private void HandleCommandCompleted()
    {
        _currentCommand.Completed -= HandleCommandCompleted;
        _currentCommand = null;

        if (_commandQueue.Count == 0)
            IsProcess = false;

        CompletedCommand?.Invoke();

        Process();
    }
}