using System;
using System.Collections.Generic;

namespace AeroHubBlazorServer.Models
{
    public partial class ActualComponentSet
    {
        public ActualComponentSet()
        {
            MeasuredCharacteristics = new HashSet<MeasuredCharacteristics>();
            MeasuredFeatures = new HashSet<MeasuredFeatures>();
        }

        public int Updateid { get; set; }
        public string SerialNumber { get; set; }
        public string Status { get; set; }
        public Guid QPID { get; set; }
        public int Origid { get; set; }

        public virtual QIFDocument QP { get; set; }
        public virtual ICollection<MeasuredCharacteristics> MeasuredCharacteristics { get; set; }
        public virtual ICollection<MeasuredFeatures> MeasuredFeatures { get; set; }
    }
}
