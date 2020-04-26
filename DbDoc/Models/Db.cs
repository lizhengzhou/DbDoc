using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace DbDoc.Models
{

    [Table("DB")]
    [Description("数据库")]
    public class Db
    {
        [Key]
        [DataMember]
        [Description("编号")]
        public Guid ID { get; set; }
        [DataMember]
        [Description("名称")]
        public string Name { get; set; }
        [DataMember]
        [Description("描述")]
        public string Desc { get; set; }
        [DataMember]
        [Description("链接")]
        public string ConnectionString { get; set; }
        [DataMember]
        [Description("数据库文档")]
        public string Html { get; set; }
    }
}
