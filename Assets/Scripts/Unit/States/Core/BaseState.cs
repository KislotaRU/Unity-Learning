public abstract class BaseState : IState
{
    public virtual void Enter() { }

    public virtual void Exit() { }

    public virtual void Update() { }

    public virtual void FixedUpdate() { }

    public virtual void Pause() { }

    public virtual void Resume() { }

    public virtual void HandleInput() { }
}