using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZyGames.Framework.Game.Context;
using ProtoBuf;
using ZyGames.Framework.Model;

// Model类的命名空间最好安装官方的写法：GameServer.Model，不然可能无法写入SQL数据库
namespace GameServer.Model
{
    [Serializable, ProtoContract]
    [EntityTable("TestGameData")]//默认CacheType.Dictionary，可读写，存数据库，用类名作表名，永久缓存，不绑定主键
    public class User : BaseUser
    {
        [ProtoMember(1)]
        [EntityField(true)]
        public int userid { get; set; }

        [ProtoMember(2)]
        [EntityField]
        public string nickname { get; set; }

        [ProtoMember(3)]
        [EntityField]
        public string passportid { get; set; }

        [ProtoMember(4)]
        [EntityField]
        public string retailid { get; set; }

        public override string GetNickName()
        {
            return nickname;
        }

        public override string GetPassportId()
        {
            return passportid;
        }

        public override string GetRetailId()
        {
            return retailid;
        }

        public override int GetUserId()
        {
            return userid;
        }

        public override bool IsFengJinStatus
        {
            get { return false; }
        }

        public override DateTime OnlineDate
        {
            get;
            set;
        }

        protected override int GetIdentityId()
        {
            return userid;
        }
    }
}
