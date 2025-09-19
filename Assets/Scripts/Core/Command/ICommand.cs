using System;

public interface ICommand
{
    event Action Completed;

    bool IsCompleted { get; }

    void Execute();
    void CanExecute();
    void Undo();
}