using System;

public interface ICommand
{
    event Action Completed;
    event Action Cancelled;

    bool IsCompleted { get; }

    void Execute();
    bool CanExecute();
    void Undo();
}