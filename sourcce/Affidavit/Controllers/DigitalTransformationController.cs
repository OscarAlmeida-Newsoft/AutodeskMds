using Affidavit.Models;
using AutoMapper;
using DTOs;
using IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Affidavit.Controllers
{
    public class DigitalTransformationController : Controller
    {
        private readonly IProcessService processService;
        private readonly IDigitalTransformationService digitalTransformationService;

        public DigitalTransformationController(IProcessService pProcessService,
            IDigitalTransformationService pDigitalTransformationService)
        {

            processService = pProcessService;
            digitalTransformationService = pDigitalTransformationService;
        }

        public ActionResult Create(int Id)
        {

            ProcessDTO process = processService.GetProcessById(Id);

            DigitalTransformationVM digitalTransformation = new DigitalTransformationVM();
            digitalTransformation.ProcessId = Id;
            digitalTransformation.ProcessName = process.Name;

            return PartialView("_CreateEdit", digitalTransformation);
        }

        public ActionResult Edit(int Id)
        {

            DigitalTransformationDTO dt = digitalTransformationService.GetDigitalTransformationById(Id);
            DigitalTransformationVM digitalTransformation = Mapper.Map<DigitalTransformationDTO, DigitalTransformationVM>(dt);
            ProcessDTO process = processService.GetProcessById(digitalTransformation.ProcessId);

            digitalTransformation.ProcessName = process.Name;

            return PartialView("_CreateEdit", digitalTransformation);
        }

        [HttpPost]
        public ActionResult CreateEdit(DigitalTransformationVM digitalTransformation)
        {
            Guid _userData;

            if (Session["lead"] != null)
            {
                _userData = (Guid)Session["lead"];
            }
            else
            {
                _userData = (Guid)Session["userId"];
            }

            digitalTransformation.UserLastModification = _userData;
            digitalTransformation.DateLastModification = DateTime.Now;

            if (digitalTransformation.Id == 0)
            {
                digitalTransformation.UserCreation = digitalTransformation.UserLastModification;
                digitalTransformation.DateCreation = digitalTransformation.DateLastModification;

                digitalTransformationService.InsertDigitalTransformation(Mapper.Map<DigitalTransformationVM, DigitalTransformationDTO>(digitalTransformation));
            }
            else
            {
                digitalTransformationService.UpdateDigitalTransformation(Mapper.Map<DigitalTransformationVM, DigitalTransformationDTO>(digitalTransformation));
            }

            return Json(new { Error = false });
        }

        public ActionResult Delete(int pDigitalTransformationId)
        {
            digitalTransformationService.DeleteDigitalTransformation(pDigitalTransformationId);
            return Json(new { Error = false });
        }
    }
}