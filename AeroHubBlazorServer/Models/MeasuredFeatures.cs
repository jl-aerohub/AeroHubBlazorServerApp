using System;
using System.Collections.Generic;

namespace AeroHubBlazorServer.Models
{
    public partial class MeasuredFeatures
    {
        public MeasuredFeatures()
        {
            MeasuredCharacterFeatureReference = new HashSet<MeasuredCharacterFeatureReference>();
        }

        public int Resultsid { get; set; }
        public int Updateid { get; set; }
        public string FeatureType { get; set; }
        public int ActualComponentid { get; set; }
        public Guid QPID { get; set; }
        public int Origid { get; set; }
        public int FeatureItemid { get; set; }
        public DateTime FileVersion { get; set; }

        public virtual ActualComponentSet ActualComponentSet { get; set; }
        public virtual MeasurementResults MeasurementResults { get; set; }
        public virtual QIFDocument QP { get; set; }
        public virtual ICollection<MeasuredCharacterFeatureReference> MeasuredCharacterFeatureReference { get; set; }
    }
}
