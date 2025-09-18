using System;

public abstract class Command : ICommand
{
    public abstract event Action Completed;

    public bool IsCompleted { get; protected set; }

    public abstract void Execute();

    public abstract void Undo();
}