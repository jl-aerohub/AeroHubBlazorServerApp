using System;
using System.Collections.Generic;

namespace AeroHubBlazorServer.Models
{
    public partial class MetaData
    {
        public string FileName { get; set; }
        public int id { get; set; }
        public string DocumentType { get; set; }
        public int? FeatureItemids { get; set; }
        public string PartNumber { get; set; }
        public string FileLink { get; set; }
        public string User { get; set; }
        public DateTime DateTime { get; set; }
        public bool? Signatures { get; set; }
        public string SupplierName { get; set; }
        public string ProcessNumber { get; set; }
        public Guid QPID { get; set; }
        public int? NDANumber { get; set; }
        public int? IPNumber { get; set; }
        public int? LTANumber { get; set; }
        public int? MVANumber { get; set; }
        public int? SupplierDocumentNumber { get; set; }
        public string EngineSerialNumber { get; set; }
        public string EngineOperator { get; set; }
        public DateTime? OverhaulDate { get; set; }

        public virtual QIFDocument QP { get; set; }
    }
}
