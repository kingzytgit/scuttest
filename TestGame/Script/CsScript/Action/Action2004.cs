using System;
using System.Collections.Generic;
using System.Data;
using GameServer.Model;
using ZyGames.Framework.Cache.Generic;
using ZyGames.Framework.Common;
using ZyGames.Framework.Game.Contract;
using ZyGames.Framework.Game.Service;


namespace GameServer.CsScript.Action
{
   
    /// <summary>
    /// 2004_userranking_list
    /// </summary>
    public class Action2004 : BaseAction
    {
        private int _pindex;
        private int _psize;
        private int _pcount;
        private List<UserScore> _dsItemList_1;
        

        public Action2004(HttpGet httpGet)
            : base(ActionIDDefine.Cst_Action2004, httpGet)
        {
            
        }

        public override void BuildPacket()
        {
            this.PushIntoStack(_pcount);
            this.PushIntoStack(_dsItemList_1.Count);
            foreach (var item in _dsItemList_1)
            {
                DataStruct dsItem = new DataStruct();
                dsItem.PushIntoStack(item.username);
                dsItem.PushIntoStack(item.score);

                this.PushIntoStack(dsItem);
            }

        }

        public override bool GetUrlElement()
        {
            if (httpGet.GetInt("pindex", ref _pindex, 0, 10000 )            
                && httpGet.GetInt("psize", ref _psize, 0, 1000 ))
            {
                return true;
            }
            return false;
        }

        public override bool TakeAction()
        {
            var cache = new ShareCacheStruct<UserScore>();
            _dsItemList_1 = cache.FindAll(false);
            _dsItemList_1 = MathUtils.QuickSort<UserScore>(_dsItemList_1, compareTo);
            _dsItemList_1 = _dsItemList_1.GetPaging(_pindex, _psize, out _pcount);
            return true;
        }

        private int compareTo(UserScore x, UserScore y)
        {
            int result = y.score - x.score;
            if (result == 0)
            {
                result = y.userid - x.userid;
            }
            return result;
        }
    }
}
