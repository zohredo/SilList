//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SO.SilList.CodeGeneration.DbContexts.SilList
{
    using System;
    using System.Collections.Generic;
    
    public partial class Rating
    {
        public Rating()
        {
            this.BusinessRatings = new HashSet<BusinessRating>();
        }
    
        public System.Guid ratingId { get; set; }
        public Nullable<int> rating1 { get; set; }
        public string review { get; set; }
        public Nullable<int> memberId { get; set; }
        public System.DateTime created { get; set; }
        public System.DateTime modified { get; set; }
        public Nullable<int> createdBy { get; set; }
        public Nullable<int> modifiedBy { get; set; }
        public Nullable<bool> isActive { get; set; }
    
        public virtual ICollection<BusinessRating> BusinessRatings { get; set; }
    }
}
