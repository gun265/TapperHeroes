public abstract class Hero_FSM<T>
{
    public abstract void Enter(T _Owner);
    public abstract void Update(T _Owner, float _Deltatime);
    public abstract void Exit(T _Owner);
}
