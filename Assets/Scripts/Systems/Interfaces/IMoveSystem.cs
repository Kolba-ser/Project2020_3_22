namespace Assets.Scripts.System.Interfaces
{
    public interface IMoveSystem
    {
        public void AddMovable(IMovable movable);
        public void RemoveMovable(IMovable movable);
    }
}
