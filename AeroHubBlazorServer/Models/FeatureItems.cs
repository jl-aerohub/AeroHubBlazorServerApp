using System;
using System.Collections.Generic;

namespace AeroHubBlazorServer.Models
{
    public partial class FeatureItems
    {
        public FeatureItems()
        {
            CharacteristicFeatureReference = new HashSet<CharacteristicFeatureReference>();
        }

        public string FeatureType { get; set; }
        public int Updateid { get; set; }
        public int FeatureNominalid { get; set; }
        public string FeatureName { get; set; }
        public Guid QPID { get; set; }
        public int Origid { get; set; }
        public DateTime FileVersion { get; set; }

        public virtual QIFDocument QP { get; set; }
        public virtual ICollection<CharacteristicFeatureReference> CharacteristicFeatureReference { get; set; }
    }
}
