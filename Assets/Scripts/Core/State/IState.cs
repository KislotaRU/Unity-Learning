public interface IState
{
    void Enter();
    void Exit();
    void Update();
    void FixedUpdate();

    void Pause();
    void Resume();
    void HandleInput();
}