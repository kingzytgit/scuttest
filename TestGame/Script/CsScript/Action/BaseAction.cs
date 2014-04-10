using ZyGames.Framework.Game.Contract;
using ZyGames.Framework.Game.Service;

namespace GameServer.CsScript.Action
{
    public abstract class BaseAction : BaseStruct
    {
        protected BaseAction(short aActionId, HttpGet httpGet)
            : base(aActionId, httpGet)
        {
        }
    }
}