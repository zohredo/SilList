﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using EntityFramework.Extensions;
using SO.SilList.Manager.Models.ValueObjects;
using SO.SilList.Manager.Interfaces;
using SO.SilList.Manager.DbContexts;
using SO.SilList.Manager.Models.ViewModels;

namespace SO.SilList.Manager.Managers
{
    public class RentalManager: IRentalManager
    {
        public RentalVo get(Guid rentalId)
        {
            using (var db = new MainDb())
            {
                var result = db.rental
                            .Include(r => r.propertyType)
                            .Include(t => t.leaseTermType)
                            .Include(c => c.rentType)
                            .Include(s => s.site)
                            .Include(m => m.member)
                            .Include(i => i.cityType)
                            .Include(o => o.countryType)
                            .Include(u => u.stateType) 
                            .FirstOrDefault(r => r.rentalId == rentalId);

                return result;
            }
        }

        /// <summary>
        /// Get First Item
        /// </summary>
        public RentalVo getFirst()
        {
            using (var db = new MainDb())
            {
                var res = db.rental
                            .FirstOrDefault();

                return res;
            }
        }

        public RentalVm search(RentalVm input)
        {

            using (var db = new MainDb())
            {
                var query = db.rental
                            .Include(r => r.propertyType)
                            .Include(t => t.leaseTermType)
                            .Include(c => c.rentType)
                            .Include(s => s.site)
                            .Include(i => i.cityType)
                            .Include(o => o.countryType)
                            .Include(u => u.stateType) 
                            .OrderBy(b => b.description)
                            .Where(e => (input.isActive == null || e.isActive == input.isActive)
                                      && (e.description.Contains(input.keyword) || string.IsNullOrEmpty(input.keyword))
                             );
                input.paging.totalCount = query.Count();
                input.result = query
                             .Skip(input.paging.skip)
                             .Take(input.paging.rowCount)

                             .ToList();

                return input;
            }
        }
        
        public List<RentalVo> getAll(bool? isActive = true)
        {
            using (var db = new MainDb())
            {
                var list = db.rental
                            .Include(r => r.propertyType)
                            .Include(t => t.leaseTermType)
                            .Include(c => c.rentType)
                            .Include(s => s.site)
                            .Include(i => i.cityType)
                            .Include(o => o.countryType)
                            .Include(u => u.stateType) 

                            .Where(e => isActive == null || e.isActive == isActive)
                            .ToList();

                return list;
            }
        }

        public bool delete(Guid rentalId)
        {
            using (var db = new MainDb())
            {
                var res = db.rental
                     .Where(e => e.rentalId == rentalId)
                     .Delete();
                return true;
            }
        }

        public RentalVo update(RentalVo input, Guid? rentalId = null)
        {
            using (var db = new MainDb())
            {

                if (rentalId == null)
                    rentalId = input.rentalId;

                var res = db.rental.FirstOrDefault(e => e.rentalId == rentalId);

                if (res == null) return null;

                input.created = res.created;
                input.createdBy = res.createdBy;
                db.Entry(res).CurrentValues.SetValues(input);


                db.SaveChanges();
                return res;

            }
        }

        public RentalVo insert(RentalVo input)
        {
            using (var db = new MainDb())
            {

                db.rental.Add(input);
                db.SaveChanges();

                return input;
            }
        }

        public int count()
        {
            using (var db = new MainDb())
            {
                return db.rental.Count();
            }
        }
    }
}
