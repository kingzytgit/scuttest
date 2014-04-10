using System;
using System.Data;
using GameServer.Model;
using ZyGames.Framework.Cache.Generic;
using ZyGames.Framework.Game.Contract;


namespace GameServer.CsScript.Action
{
   
    /// <summary>
    /// 2003_userrankingtest
    /// </summary>
    public class Action2003 : BaseAction
    {
        private string _username;
        private int _score;
        

        public Action2003(HttpGet httpGet)
            : base(ActionIDDefine.Cst_Action2003, httpGet)
        {
            
        }

        public override void BuildPacket()
        {

        }

        public override bool GetUrlElement()
        {
            if (httpGet.GetString("username", ref _username)            
                && httpGet.GetInt("score", ref _score, 0, 10000 ))
            {
                return true;
            }
            return false;
        }

        public override bool TakeAction()
        {
            var cache = new ShareCacheStruct<UserScore>();
            var ranking = cache.Find(m => m.username == _username);
            if (ranking == null)
            {
                var user = new User() { userid = (int)cache.GetNextNo(), nickname = _username };
                new PersonalCacheStruct<User>().Add(user);
                ranking = new UserScore();
                ranking.userid = user.userid;
                ranking.username = _username;
                ranking.score = _score;
                cache.Add(ranking);
            }
            else
            {
                ranking.username = _username;
                ranking.score = _score;
            }
            return true;
        }
    }
}
