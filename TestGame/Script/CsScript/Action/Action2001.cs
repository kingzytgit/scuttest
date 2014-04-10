using System;
using System.Collections.Generic;
using System.Data;
using GameServer.Model;
using ZyGames.Framework.Game.Contract;
using ZyGames.Framework.Game.Service;
using ZyGames.Framework.Cache.Generic;
using ZyGames.Framework.Common;


namespace GameServer.CsScript.Action
{
   
    /// <summary>
    /// 2001_测试2001
    /// </summary>
    public class Action2001 : BaseAction
    {
        private int _gametype;
        private int _serverid;
        private int _pindex;
        private int _psize;
        private int _pcount;
        private List<Notice> _dsItemList_1;
        

        public Action2001(HttpGet httpGet)
            : base(ActionIDDefine.Cst_Action2001, httpGet)
        {
            
        }

        public override void BuildPacket()
        {
            this.PushIntoStack(_pcount);
            this.PushIntoStack(_dsItemList_1.Count);
            foreach (var item in _dsItemList_1)
            {
                DataStruct dsItem = new DataStruct();
                dsItem.PushIntoStack(item.title);
                dsItem.PushIntoStack(item.content);
                dsItem.PushIntoStack(item.time.ToString());

                this.PushIntoStack(dsItem);
            }

        }

        public override bool GetUrlElement()
        {
            if (httpGet.GetInt("gametype", ref _gametype, 0, 100000 )            
                && httpGet.GetInt("serverid", ref _serverid, 0, 100000 )            
                && httpGet.GetInt("pindex", ref _pindex, 0, 10000 ))
            {
                httpGet.GetInt("psize", ref _psize, 0, 1000 );
                return true;
            }
            return false;
        }

        public override bool TakeAction()
        {
            int rcount = 0;
            /*
             * new ShareCacheStruct<Notice> 实际上不是一个存储空间，
             * 而是指向redis数据库的一个入口，
             * 无论多少次调用new ShareCacheStruct<Notice>，得到的入口都是一样的
             * 这里通过指定类型Notice来获得Notice表的指针，
             * 然后就可以做查询或修改操作
             */
            _dsItemList_1 = MathUtils.GetPaging<Notice>(new ShareCacheStruct<Notice>().FindAll(), _pindex, _psize, out _pcount, out rcount);
            return true;
        }
    }
}
