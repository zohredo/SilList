﻿using SO.SilList.Manager.Managers;
using SO.SilList.Manager.Models.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SO.SilList.Admin.Web.Controllers
{
    public class RentTypeController : Controller
    {
        //
        // GET: /RentType/

        private RentTypeManager rentalTypeManager = new RentTypeManager();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult List()
        {
            var results = rentalTypeManager.getAll(null);
            return PartialView(results);  //"_List"
        }

        [HttpPost]
        public ActionResult Edit(int id, RentTypeVo input)
        {
            if (this.ModelState.IsValid)
            {
                var result = rentalTypeManager.update(input, id);
                return RedirectToAction("Index");
            }
            return View();
        }

        public ActionResult Edit(int id)
        {
            var result = rentalTypeManager.get(id);
            return View(result);
        }

        [HttpPost]
        public ActionResult Create(RentTypeVo input)
        {
            if (this.ModelState.IsValid)
            {
                var rentalItem = rentalTypeManager.insert(input);
                return RedirectToAction("Index");
            }
            return View();
        }

        public ActionResult Create()
        {
            return View();
        }

        public ActionResult Details(int id)
        {
            var result = rentalTypeManager.get(id);
            return View(result);
        }

        public ActionResult Menu()
        {
            return PartialView("../Rental/_Menu");
        }

        public ActionResult Delete(int id)
        {
            rentalTypeManager.delete(id);
            return RedirectToAction("index");
        }

        public ActionResult DropDownList(int? id = null)
        {
            ViewBag.rentType = rentalTypeManager.getAll(null);
            var rentType = new RentTypeVo();
            if (id != null)
            {
                rentType = rentalTypeManager.get(id.Value);
            }
            return PartialView("_DropDownList", rentType);
        }
    }
}