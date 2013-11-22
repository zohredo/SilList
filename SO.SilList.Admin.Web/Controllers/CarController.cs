﻿using SO.SilList.Manager.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SO.SilList.Manager.Models.ValueObjects;
using SO.SilList.Manager.Models.ViewModels;
using SO.SilList.Utility.Classes;

namespace SO.SilList.Admin.Web.Controllers
{
    public class CarController : Controller
    {
        private CarManager carManager = new CarManager();
        // private EntryStatusTypeManager<CarVo> entryStatusTypeManager = new EntryStatusTypeManager<CarVo>();

        //
        // GET: /Car/

        public ActionResult Index(CarVm input = null,Paging paging = null)
        {
            if (input == null) input = new CarVm();
            input.car = new CarVo();
            input.paging = paging;
            if (this.ModelState.IsValid)
            {
                if (input.submitButton != null)
                    input.paging.pageNumber = 1;
                input = carManager.search(input);
                return View(input);
            }
            return View();
        }

        public ActionResult Menu()
        {
            return PartialView("_Menu");
        }

        public ActionResult List()
        {
            var results = carManager.getAll(null);
            return PartialView(results);
        }

        [HttpPost]
        public ActionResult Create(CarVo input)
        {

            if (this.ModelState.IsValid)
            {
                var item = carManager.insert(input);

                ImageManager imageManager = new ImageManager();
                imageManager.InsertUploadImages(item.carId, Request.Files, Server, SO.SilList.Manager.Managers.ImageCategory.carImage);

                return RedirectToAction("Index");
            }


            return View();

        }

        public ActionResult Create()
        {
            var vo = new CarVo();
            return View(vo);
        }

        [HttpPost]
        public ActionResult Edit(Guid id, CarVm input)
        {
            if (this.ModelState.IsValid && input.car != null)
        {
                var res = carManager.update(input.car, id);

                // Car Images stuff
                ImageManager imageManager = new ImageManager();
                // removing unchecked images
                imageManager.RemoveImages(id, input.imagesToRemove, ImageCategory.carImage);
                // uploading new images from edit page
                imageManager.InsertUploadImages(id, Request.Files, Server, ImageCategory.carImage);

                return RedirectToAction("Index");
            }

            return View(input);

        }
        public ActionResult Edit(Guid id)
        {
            var result = carManager.get(id);

            if (result.modelTypeId != null)
                result.makeTypeId = (int)result.modelType.makeTypeId;

            // Images
            ImageManager imageManager = new ImageManager();
            var carImages = imageManager.getCarImages(id);
            CarVm carVm = new CarVm(result);
            carVm.imagesToRemove = imageManager.CreateOrAddToImageList(carImages, true);

            return View(carVm);
        }

        public ActionResult Details(Guid id)
        {
            var result = carManager.get(id);

            // Images
            ImageManager imageManager = new ImageManager();
            ViewBag.Images = imageManager.getCarImages(id);

            return View(result);
        }

        public ActionResult Delete(Guid id)
        {
            carManager.delete(id);
            return RedirectToAction("Index");
        }

        public ActionResult DropDownList(Guid? id = null)
        {
            ViewBag.cars = carManager.getAll(null);
            var car = new CarVo();
            if (id != null)
            {
                car = carManager.get(id.Value);
            }
            return PartialView("_DropDownList", car);
        }

        public ActionResult Pagination(Paging input)
        {
            return PartialView("_Pagination",input);
        }

        public ActionResult Filter(CarVm input)
        {
            return PartialView("_Filter", input);
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////////////
        // Entry Status Type stuff
        public ActionResult EntryStatusIndex(CarVm input = null, Paging paging = null)
        {
            if (input == null)
                input = new CarVm();
            input.showPendingOnly = true;
            return Index(input, paging);
        }

        public ActionResult EntryStatusApprove(Guid id)
        {
            var result = carManager.get(id);
            if (result != null)
                carManager.Approve(id);
            return RedirectToAction("EntryStatusIndex");
        }
        public ActionResult EntryStatusDecline(Guid id)
        {
            var result = carManager.get(id);
            if (result != null)
                carManager.Decline(id);
            return RedirectToAction("EntryStatusIndex");
        }
        // End of Entry Status Type stuff
    }
}
