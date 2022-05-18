using System;
using System.Collections.Generic;

namespace AeroHubBlazorServer.Models
{
    public partial class CharacteristicItems
    {
        public CharacteristicItems()
        {
            CharacteristicFeatureReference = new HashSet<CharacteristicFeatureReference>();
        }

        public string CharacteristicType { get; set; }
        public int Updateid { get; set; }
        public string Name { get; set; }
        public int CharacteristicNominalid { get; set; }
        public Guid QPID { get; set; }
        public int Origid { get; set; }
        public DateTime FileVersion { get; set; }

        public virtual QIFDocument QP { get; set; }
        public virtual ICollection<CharacteristicFeatureReference> CharacteristicFeatureReference { get; set; }
    }
}
