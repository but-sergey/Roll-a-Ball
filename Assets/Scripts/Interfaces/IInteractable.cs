namespace GB.Interfaces
{
    interface IInteractable : IAction, IInitialization
    {
        bool IsInteractable { get; }
    }
}
