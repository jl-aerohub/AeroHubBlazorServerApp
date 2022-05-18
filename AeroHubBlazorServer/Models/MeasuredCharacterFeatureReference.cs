using System;
using System.Collections.Generic;

namespace AeroHubBlazorServer.Models
{
    public partial class MeasuredCharacterFeatureReference
    {
        public Guid QPID { get; set; }
        public int Featureid { get; set; }
        public int Characteristicid { get; set; }
        public int UpdatedFeatureid { get; set; }
        public int UpdatedCharacteristicid { get; set; }
        public DateTime FIleVersion { get; set; }

        public virtual MeasuredCharacteristics MeasuredCharacteristics { get; set; }
        public virtual MeasuredFeatures MeasuredFeatures { get; set; }
    }
}
