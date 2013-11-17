﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using EntityFramework.Extensions;
using SO.SilList.Manager.DbContexts;
using SO.SilList.Manager.Interfaces;
using SO.SilList.Manager.Models.ValueObjects;
using SO.SilList.Manager.Models.ViewModels;

namespace SO.SilList.Manager.Managers
{
    public class MemberManager : IMemberManager
    {
        /// <summary>
        /// Find Member matching the memberId (primary key)
        /// </summary>
        public MemberVo get(int memberId)
        {
            using (var db = new MainDb())
            {
                var res = db.members
                            .Include(s => s.site)
                             .Include(i => i.cityType)
                            .Include(o => o.countryType)
                            .Include(u => u.stateType) 

                            .FirstOrDefault(p => p.memberId == memberId);

                return res;
            }
        }

        /// <summary>
        /// Get First Item
        /// </summary>
        public MemberVo getFirst()
        {
            using (var db = new MainDb())
            {
                var res = db.members
                           .Include(s => s.site)
                             .Include(i => i.cityType)
                            .Include(o => o.countryType)
                            .Include(u => u.stateType) 

                           .FirstOrDefault();

                return res;
            }
        }

        public MemberVo getByUsernameAndPassword(string username, string password)
        {
            using (var db = new MainDb())
            {
                MemberVo mem = null;

                try
                {
                    mem = db.members
                            .Include(s => s.site)
                            .Include(i => i.cityType)
                            .Include(o => o.countryType)
                            .Include(u => u.stateType) 
                            .FirstOrDefault(p => (p.email == username || p.username == username) && p.password == password);
                }
                catch (Exception ex)
                {
                    mem = null;
                }

                return mem;
            }
        }

        public MemberVo getByUsernameOrEmail(string usernameOrEmail)
        {
            using (var db = new MainDb())
            {
                var mem = db.members
                            .Include(s => s.site)
                            .Include(i => i.cityType)
                            .Include(o => o.countryType)
                            .Include(u => u.stateType) 
                            .FirstOrDefault(p => p.email == usernameOrEmail || p.username == usernameOrEmail);

                return mem;
            }
        }

        public bool delete(int memberId)
        {
            using (var db = new MainDb())
            {
                var res = db.members
                     .Where(e => e.memberId == memberId)
                     .Delete();
                return true;
            }
        }

        public MemberVo update(MemberVo input, int? memberId = null)
        {
            using (var db = new MainDb())
            {
                if (memberId == null)
                    memberId = input.memberId;

                var res = db.members.FirstOrDefault(e => e.memberId == memberId);

                if (res == null) return null;

                input.created = res.created;
                input.createdBy = res.createdBy;
                db.Entry(res).CurrentValues.SetValues(input);

                db.SaveChanges();
                return res;

            }
        }

        public MemberVo insert(MemberVo input)
        {
            using (var db = new MainDb())
            {

                db.members.Add(input);
                db.SaveChanges();

                return input;
            }
        }

        public int count()
        {
            using (var db = new MainDb())
            {
                return db.members.Count();
            }
        }

        public List<MemberVo> getAll(bool? isActive = true)
        {
            using (var db = new MainDb())
            {
                try
                {
                    var list = db.members
                                 .Include(s => s.site)
                                 .Include(i => i.cityType)
                                .Include(o => o.countryType)
                                .Include(u => u.stateType) 

                                 .Where(e => isActive == null || e.isActive == isActive)
                                 .ToList();
                    return list;
                }
                catch (Exception e)
                {
                    System.Console.WriteLine(e.ToString());
                }
                return new List<MemberVo>();
            }
        }
        public MemberVm search(MemberVm input)
        {
            using (var db = new MainDb())
            {
                var query = db.members
                             .Include(s => s.site)
                             .Include(i => i.cityType)
                             .Include(o => o.countryType)
                             .Include(u => u.stateType) 

                             .Where(e => (input.filterIsActive == null || e.isActive == input.filterIsActive)
                                      && (string.IsNullOrEmpty(input.keyword) 
                                      || e.firstName.Contains(input.keyword) 
                                      || e.lastName.Contains(input.keyword)
                                      || e.address.Contains(input.keyword)
                                      || e.cityType.name.Contains(input.keyword)
                                      || e.stateType.name.Contains(input.keyword)
                                      || e.countryType.name.Contains(input.keyword)
                                      || e.email.Contains(input.keyword)
                                      || e.username.Contains(input.keyword))
                                    )
                             .OrderBy(b => b.firstName)
                             .ThenBy(b=> b.lastName);

                input.paging.totalCount = query.Count();

                input.result = query
                             .Skip(input.paging.skip)
                             .Take(input.paging.rowCount)
                             .ToList();

                return input;
            }
        }
        
        // Additional methods
        public Nullable<int> GetFirstAvailableSiteId()
        {
            int? siteId = null;
            SiteManager siteManager = new SiteManager();
            try
            {
                var siteList = siteManager.getAll();
                if (siteList.Count == 0)
                {
                    return null; // can not insert without real siteId
                }
                var site = siteList[0];
                siteId = site.siteId;
            }
            catch (NotImplementedException)
            {
            }
            return siteId;

        }

    }
}
