using System;
using System.Collections.Generic;

namespace AeroHubBlazorServer.Models
{
    public partial class MeasuredCharacteristics
    {
        public MeasuredCharacteristics()
        {
            MeasuredCharacterFeatureReference = new HashSet<MeasuredCharacterFeatureReference>();
        }

        public int Updateid { get; set; }
        public int Resultsid { get; set; }
        public string CharacteristicType { get; set; }
        public string Status { get; set; }
        public int CharacteristicItemid { get; set; }
        public int ActualComponentid { get; set; }
        public double Value { get; set; }
        public int? DecimalPlaces { get; set; }
        public Guid QPID { get; set; }
        public int Origid { get; set; }
        public DateTime FileVersion { get; set; }

        public virtual ActualComponentSet ActualComponentSet { get; set; }
        public virtual MeasurementResults MeasurementResults { get; set; }
        public virtual QIFDocument QP { get; set; }
        public virtual ICollection<MeasuredCharacterFeatureReference> MeasuredCharacterFeatureReference { get; set; }
    }
}
