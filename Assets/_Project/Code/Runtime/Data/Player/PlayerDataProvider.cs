using _Project.Code.Runtime.Utility.DataManagment;
using _Project.Code.Runtime.Utility.DataManagment.DataProviders;

namespace _Project.Code.Runtime.Data.Player
{
    public class PlayerDataProvider : DataProvider<PlayerData>
    {
        public PlayerDataProvider(ISaveLoadSerivce saveLoadService) : base(saveLoadService)
        {
        }
        
        protected override PlayerData GetOriginData()
        {
            return new PlayerData()
            {
                Balance = 50,
                WinCount = 0,
                LoseCount = 0,
            };
        }
    }
}
