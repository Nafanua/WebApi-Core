using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Model
{
    public class LogDbo
    {
        public int Id { get; set; }

        public DatasourceDbo Datasource { get; set; }

        public DateTime StartDate { get; set; }

        public TimeSpan Durotation { get; set; }

        public string ComplitionStatus { get; set; }

        public string Error { get; set; }
    }
}
