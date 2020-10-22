using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Fias.Models
{
    [Table("ADDROBJ", Schema = "fias")]
    public class Model
    {
        public string AOGUID { get; set; }
        public string FORMALNAME { get; set; }
        public string REGIONCODE { get; set; }
        public string AUTOCODE { get; set; }
        public string AREACODE { get; set; }
        public string CITYCODE { get; set; }
        public string CTARCODE { get; set; }
        public string PLACECODE { get; set; }
        public string PLANCODE { get; set; }
        public string STREETCODE { get; set; }
        public string EXTRCODE { get; set; }
        public string SEXTCODE { get; set; }
        public string OFFNAME { get; set; }
        public string POSTALCODE { get; set; }
        public string IFNSFL { get; set; }
        [Column("TERRINFSFL")]
        public string TERRIFNSFL { get; set; }
        public string IFNSUL { get; set; }
        [Column("TERRINFSUL")]
        public string TERRIFNSUL { get; set; }
        public string OKATO { get; set; }
        public string OKTMO { get; set; }
        public DateTime UPDATEDATE { get; set; }
        public string SHORTNAME { get; set; }
        public int AOLEVEL { get; set; }
        public string PARENTGUID { get; set; }
        public string AOID { get; set; }
        public string PREVID { get; set; }
        public string NEXTID { get; set; }
        public string CODE { get; set; }
        public string PLAINCODE { get; set; }
        public int ACTSTATUS { get; set; }
        public int CENTSTATUS { get; set; }
        public int OPERSTATUS { get; set; }
        public int CURRSTATUS { get; set; }
        public DateTime STARTDATE { get; set; }
        public DateTime ENDDATE { get; set; }
        public string NORMDOC { get; set; }
        public byte LIVESTATUS { get; set; }
        public int DIVTYPE { get; set; }
    }
}