using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace sampleCRUD.Model
{
    [NotMapped]
    public class Error
    {
        public string Code { get; set; }
        public string Message { get; set; }
    }
}
