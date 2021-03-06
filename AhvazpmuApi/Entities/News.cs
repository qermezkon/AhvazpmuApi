﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AhvazpmuApi.Entities
{
    [Table("tbNews")]
    public class News
    {        
        public Guid tbNewsID { get; set; }
        public Guid tbNewsGroupID { get; set; }
        public Guid tbUserID { get; set; }
        public string fldNewsTitle { get; set; }
        public string fldNewsDescription { get; set; }
        public string fldNewsDate { get; set; }
        public string fldRegisterDate { get; set; }
        public long? fldNewsVisited { get; set; }
        public string fldSummaryNews { get; set; }
        public string fldRegisterTime { get; set; }
        public string fldNewsExternalLink { get; set; }
        public virtual NewsImage NewsImage { get; set; }
    }
}
