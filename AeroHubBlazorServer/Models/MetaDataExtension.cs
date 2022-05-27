using Microsoft.AspNetCore.Components.Forms;
using System.ComponentModel.DataAnnotations.Schema;

namespace AeroHubBlazorServer.Models
{
    public partial class MetaData
    {
        [NotMapped]
        public IBrowserFile? BrowserFile { get; set; }
    }
}
