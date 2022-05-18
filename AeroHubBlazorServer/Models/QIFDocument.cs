using System;
using System.Collections.Generic;

namespace AeroHubBlazorServer.Models
{
    public partial class QIFDocument
    {
        public QIFDocument()
        {
            ActualComponentSet = new HashSet<ActualComponentSet>();
            CharacteristicFeatureReference = new HashSet<CharacteristicFeatureReference>();
            CharacteristicItems = new HashSet<CharacteristicItems>();
            FeatureItems = new HashSet<FeatureItems>();
            MeasuredCharacteristics = new HashSet<MeasuredCharacteristics>();
            MeasuredFeatures = new HashSet<MeasuredFeatures>();
            MeasurementResults = new HashSet<MeasurementResults>();
            MetaData = new HashSet<MetaData>();
        }

        public Guid QPID { get; set; }
        public int IDMax { get; set; }
        public string MBD { get; set; }

        public virtual ICollection<ActualComponentSet> ActualComponentSet { get; set; }
        public virtual ICollection<CharacteristicFeatureReference> CharacteristicFeatureReference { get; set; }
        public virtual ICollection<CharacteristicItems> CharacteristicItems { get; set; }
        public virtual ICollection<FeatureItems> FeatureItems { get; set; }
        public virtual ICollection<MeasuredCharacteristics> MeasuredCharacteristics { get; set; }
        public virtual ICollection<MeasuredFeatures> MeasuredFeatures { get; set; }
        public virtual ICollection<MeasurementResults> MeasurementResults { get; set; }
        public virtual ICollection<MetaData> MetaData { get; set; }
    }
}
