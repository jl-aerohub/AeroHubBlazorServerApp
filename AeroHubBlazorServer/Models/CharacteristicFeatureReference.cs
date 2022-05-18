using System;
using System.Collections.Generic;

namespace AeroHubBlazorServer.Models
{
    public partial class CharacteristicFeatureReference
    {
        public Guid QPID { get; set; }
        public int Featureid { get; set; }
        public int Characteristicid { get; set; }
        public int UpdatedFeatureid { get; set; }
        public int UpdatedCharacteristicid { get; set; }
        public DateTime FileVersion { get; set; }

        public virtual CharacteristicItems CharacteristicItems { get; set; }
        public virtual FeatureItems FeatureItems { get; set; }
        public virtual QIFDocument QP { get; set; }
    }
}
