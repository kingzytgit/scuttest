/****************************************************************************
Copyright (c) 2013-2015 scutgame.com

http://www.scutgame.com

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE.
****************************************************************************/
using System;
using GameServer.Model;
using GameServer.Test;
using ZyGames.Framework.Cache.Generic;
using ZyGames.Framework.Game.Context;
using ZyGames.Framework.Game.Contract;
using ZyGames.Framework.Game.Runtime;
using ZyGames.Framework.Script;

/*
 * 这里的Game.Script命名空间是GameServer.exe内部直接使用的名字，写死的名字，
 * 所以在不改宿主程序的情况下，这里的命名空间不能修改
 * 除了MainClass外的其他类都在AGameServer这个命名空间下
 */
namespace Game.Script
{
    public class MainClass : GameSocketHost, IMainScript
    {
        protected override BaseUser GetUser(int userId)
        {
            return (BaseUser)CacheFactory.GetPersonalEntity("GameServer.Model.User", userId.ToString(), userId);
        }

        protected override void OnStartAffer()
        {
            // 仅在使用python的情况下，需要调用SetActionIgnoreAuthorize来方便test
            // ActionFactory.SetActionIgnoreAuthorize(2001, 2002, 2003);
            InitNotices();

            DelegateTest d = new DelegateTest();
            d.call();
        }

        protected override void OnServiceStop()
        {
            GameEnvironment.Stop();
        }

        void InitNotices()
        {
            /*
             * new ShareCacheStruct<Notice> 实际上不是一个存储空间，
             * 而是指向redis数据库的一个入口，
             * 这里通过指定类型Notice来获得Notice表的指针，
             * 然后就可以做查询或修改操作
             * 
             *     public abstract class BaseCacheStruct<T> : BaseDisposable where T : AbstractEntity, new()
             *     {
             *            static BaseCacheStruct()
             *            {
             *            // 这里根据<T>中的类型来确定表的名字，
             *            // 再根据T上面的EntityTable来确定连接的数据库，
             *            // [EntityTable(CacheType.Entity, "TestGameData")]
             *            //
             *            // 如果已经有表就操作表，没有就新建后操作，
             *            // 如果使用这个接口来将数据直接保存到sql数据库中的话，需要连接数据库，而且可能操作磁盘
             *                EntitySchemaSet.InitSchema(typeof(T));
             *                CacheFactory.RegistUpdateNotify(new DefaultCacheStruct<T>());
             *            }
             *           ...
             *     }
             */
            var cacheSet = new ShareCacheStruct<Notice>();
            for (int i = 0; i < 50; i++)
            {
                int id = (int)cacheSet.GetNextNo();
                Notice notice = new Notice(id);
                notice.title = "tile" + id;
                notice.content = "Content" + id;
                notice.time = DateTime.Now;
                cacheSet.Add(notice);
            }
        }
    }

}