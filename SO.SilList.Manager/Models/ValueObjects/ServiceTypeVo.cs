
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace SO.SilList.Manager.Models.ValueObjects
{
     
    [Table("ServiceType", Schema = "app" )]
    [Serializable]
    public partial class ServiceTypeVo
    {
    		
    	[DisplayName("service Type Id")]
    	[Key]
        public int serviceTypeId { get; set; }
    		
    	[DisplayName("Description")]
        public string description { get; set; }
    		
    	[DisplayName("Name")]
    	[StringLength(50)]
        public string name { get; set; }
    		
    	[DisplayName("Site Id")]
        public Nullable<int> siteId { get; set; }
    		
    	[DisplayName("Created")]
    	[Required]
        public System.DateTime created { get; set; }
    		
    	[DisplayName("modified")]
    	[Required]
        public System.DateTime modified { get; set; }
    		
    	[DisplayName("created By")]
        public Nullable<int> createdBy { get; set; }
    		
    	[DisplayName("modified By")]
        public Nullable<int> modifiedBy { get; set; }

        [DisplayName("Is Active")]
        [Required]
        public bool isActive { get; set; }

        [Association("ServiceType_BusinessService", "serviceTypeId", "serviceTypeId", IsForeignKey = true)]
        public List<BusinessServicesVo> businessServices { get; set; }

    	public ServiceTypeVo(){

            this.serviceTypeId = 000;

            this.isActive = true;
    	}
    }
}
