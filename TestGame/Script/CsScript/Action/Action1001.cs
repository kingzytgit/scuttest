using System;
using System.Data;
using ZyGames.Framework.Game.Contract;

// Action类的命名空间最好安装官方的写法：GameServer.CsScript.Action，不然可能出现各种莫名情况
namespace GameServer.CsScript.Action
{
   
    /// <summary>
    /// 1001_测试1
    /// </summary>
    public class Action1001 : BaseAction
    {
        private string _content;
        

        public Action1001(HttpGet httpGet)
            : base(ActionIDDefine.Cst_Action1001, httpGet)
        {
            
        }

        public override void BuildPacket()
        {
            this.PushIntoStack(_content);

        }

        public override bool GetUrlElement()
        {
            return true;
        }

        public override bool TakeAction()
        {
            _content = "TestGameServer Action1001";
            return true;
        }
    }
}
