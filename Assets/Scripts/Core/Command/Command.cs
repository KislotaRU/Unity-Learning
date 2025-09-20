using System;

public abstract class Command : ICommand
{
    public event Action Completed;

    public bool IsCompleted { get; protected set; }

    public abstract void Execute();

    public virtual void CanExecute() { }

    public virtual void Undo()
    {
        HandleCommandCompleted();
    }

    protected virtual void HandleCommandCompleted()
    {
        IsCompleted = true;

        Completed?.Invoke();
    }
}