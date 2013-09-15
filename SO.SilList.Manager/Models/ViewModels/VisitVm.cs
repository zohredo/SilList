﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SO.SilList.Manager.Models.ValueObjects;

namespace SO.SilList.Manager.Models.ViewModels
{
    public class VisitVm
    {
        public List<VisitVo> result { get; set; }

        public int totalRows { get; set; }

        [DisplayName("Keyword: ")]
        public string keyword { get; set; }

        [DisplayName("Site: ")]
        public Nullable<int> siteId { get; set; }

        [ForeignKey("siteId")]
        public SiteVo site { get; set; }

        [DisplayName("Page: ")]
        public int pageNumber { get; set; }

        [DisplayName("Rows per page: ")]
        [Range(5, 50)]
        public int rowsPerPage { get; set; }

        [DisplayName("Show Page Links: ")]
        [Range(2, 5)]
        public int pageLinkCount { get; set; }

        public string btnSearch { get; set; }

        public int skip 
        {
            get
            {
                return (pageNumber - 1) * rowsPerPage;
            }
        }

        public int firstVisibleRow 
        {
            get
            {
                return (totalRows > 0 ? (pageNumber - 1) * rowsPerPage + 1 : 0);
            }
        }

        public int lastVisibleRow 
        {
            get
            {
                return Math.Min(pageNumber * rowsPerPage, totalRows);
            }
        }

        public int firstLinkPage
        {
            get
            {
                return Math.Max(1, pageNumber - pageLinkCount);
            }
        }

        public int lastLinkPage
        {
            get
            {
                return Math.Min(totalPages, pageNumber + pageLinkCount);
            }
        }

        public int totalPages
        {
            get
            {
                return (int)Math.Ceiling((double)totalRows / rowsPerPage);
            }
        }


        public VisitVm()
        {
            this.result = new List<VisitVo>();
            this.totalRows = 0;
            this.pageNumber = 1;

            this.rowsPerPage = 10;
            this.pageLinkCount = 3;            
        }
    }
}