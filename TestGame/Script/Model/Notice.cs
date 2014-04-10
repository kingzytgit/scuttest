using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZyGames.Framework.Model;

// Model类的命名空间最好安装官方的写法：GameServer.Model，不然可能无法写入SQL数据库
namespace GameServer.Model
{
    [Serializable, ProtoContract]
    [EntityTable(CacheType.Entity, "TestGameData")] // connectKey对应于GameServer.exe.config中connectionStrings下的每一项中的name
    public class Notice : ShareEntity
    {
        public Notice() : base(false) { }
        public Notice(int id_) : this() { id = id_; }

        [ProtoMember(1)]
        [EntityField(true)]
        public int id { get; private set; }

        [ProtoMember(2)]
        [EntityField]
        public string title { get; set; }

        [ProtoMember(3)]
        [EntityField]
        public string content { get; set; }

        [ProtoMember(4)]
        [EntityField]
        public DateTime time { get; set; }
    }
}
