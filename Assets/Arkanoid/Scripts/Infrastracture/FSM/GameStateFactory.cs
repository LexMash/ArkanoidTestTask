using Infrastructure;
using Zenject;

public class GameStateFactory
{
    private readonly DiContainer _container;

    public GameStateFactory(DiContainer container)
    {
        _container = container;
    }

    public T CreateState<T>() where T : IGameState 
        => _container.Resolve<T>();
}
