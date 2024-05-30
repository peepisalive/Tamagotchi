namespace Scenes
{
    public interface ISceneLoadHandler<T>
    {
        public void OnSceneLoad(T argument);
    }
}