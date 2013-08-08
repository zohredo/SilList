
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

namespace SO.SilList.CodeGeneration.DbContexts.SilList
{
     
    [Table("ListingType", Schema = "dbo" )]
    [Serializable]
    public partial class ListingTypeVo
    {
    		
    	[DisplayName("listing Type Id")]
    	[Required]
        public int listingTypeId { get; set; }
    		
    	[DisplayName("name")]
    	[StringLength(50)]
        public string name { get; set; }
    		
    	[DisplayName("description")]
    	[StringLength(50)]
        public string description { get; set; }
    		
    	[DisplayName("created By_")]
        public Nullable<int> createdBy_ { get; set; }
    		
    	[DisplayName("modified By")]
        public Nullable<int> modifiedBy { get; set; }
    		
    	[DisplayName("created")]
    	[Required]
        public System.DateTime created { get; set; }
    		
    	[DisplayName("modified")]
    	[Required]
        public System.DateTime modified { get; set; }
    		
    	[DisplayName("is Active")]
    	[Required]
        public bool isActive { get; set; }
      
    	public ListingTypeVo(){
    			
    	
    	 //this.isActive = true;
    	}
    }
}