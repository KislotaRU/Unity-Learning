using System;

public abstract class Command : ICommand
{
    public event Action Completed;
    public event Action Cancelled;

    public bool IsCompleted { get; protected set; }

    public abstract void Execute();

    public virtual bool CanExecute()
    {
        return false;
    }

    public virtual void Undo()
    {
        HandleCommandCancelled();
    }

    protected virtual void HandleCommandCompleted()
    {
        IsCompleted = true;

        Completed?.Invoke();
    }

    protected virtual void HandleCommandCancelled()
    {
        IsCompleted = false;

        Cancelled?.Invoke();
    }
}