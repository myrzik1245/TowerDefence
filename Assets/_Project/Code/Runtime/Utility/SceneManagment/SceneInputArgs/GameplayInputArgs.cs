namespace _Project.Code.Runtime.Utility.SceneManagment.SceneInputArgs
{
    public class GameplayInputArgs : IInputSceneArgs
    {
        public int Level { get; }
        
        public GameplayInputArgs(int level)
        {
            Level = level;
        }
    }
}
