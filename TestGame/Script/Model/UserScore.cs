using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProtoBuf;
using ZyGames.Framework.Model;

// Model类的命名空间最好安装官方的写法：GameServer.Model，不然可能无法写入SQL数据库
namespace GameServer.Model
{
    [Serializable, ProtoContract]
    [EntityTable(CacheType.Entity, "TestGameData")]
    public class UserScore : ShareEntity
    {
        public UserScore() : base(false) { date = DateTime.Now; }

        [ProtoMember(1)]
        [EntityField(true)]
        public int userid { get; set; }

        [ProtoMember(2)]
        [EntityField]
        public string username { get; set; }

        [ProtoMember(3)]
        [EntityField]
        public int score { get; set; }

        [ProtoMember(4)]
        [EntityField]
        public DateTime date { get; set; }

        protected override int GetIdentityId() { return userid; }
    }
}
