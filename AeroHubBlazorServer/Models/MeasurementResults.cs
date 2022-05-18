using System;
using System.Collections.Generic;

namespace AeroHubBlazorServer.Models
{
    public partial class MeasurementResults
    {
        public MeasurementResults()
        {
            MeasuredCharacteristics = new HashSet<MeasuredCharacteristics>();
            MeasuredFeatures = new HashSet<MeasuredFeatures>();
        }

        public int Updateid { get; set; }
        public string InspectionStatus { get; set; }
        public int ActualComponentid { get; set; }
        public Guid QPID { get; set; }
        public int Origid { get; set; }

        public virtual QIFDocument QP { get; set; }
        public virtual ICollection<MeasuredCharacteristics> MeasuredCharacteristics { get; set; }
        public virtual ICollection<MeasuredFeatures> MeasuredFeatures { get; set; }
    }
}
