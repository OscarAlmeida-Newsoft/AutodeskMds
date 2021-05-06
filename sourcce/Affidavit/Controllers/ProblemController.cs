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
    public class ProblemController : Controller
    {
        private readonly IProcessService processService;
        private readonly IProblemService problemService;

        public ProblemController(IProcessService pProcessService,
            IProblemService pProblemService)
        {

            processService = pProcessService;
            problemService = pProblemService;
        }

        public ActionResult Create(int Id)
        {

            ProcessDTO process = processService.GetProcessById(Id);

            ProblemVM problem = new ProblemVM();
            problem.ProcessId = Id;
            problem.ProcessName = process.Name;

            return PartialView("_CreateEdit", problem);
        }

        public ActionResult Edit(int Id)
        {
            ProblemDTO pr = problemService.GetProblemById(Id);
            ProblemVM problem = Mapper.Map<ProblemDTO, ProblemVM>(pr);
            ProcessDTO process = processService.GetProcessById(problem.ProcessId);

            problem.ProcessName = process.Name;

            return PartialView("_CreateEdit", problem);
        }

        [HttpPost]
        public ActionResult CreateEdit(ProblemVM problem)
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

            problem.UserLastModification = _userData;
            problem.DateLastModification = DateTime.Now;

            if (problem.Id == 0)
            {
                problem.UserCreation = problem.UserLastModification;
                problem.DateCreation = problem.DateLastModification;

                problemService.InsertProblem(Mapper.Map<ProblemVM, ProblemDTO>(problem));
            }
            else
            {
                problemService.UpdateProblem(Mapper.Map<ProblemVM, ProblemDTO>(problem));
            }

            return Json(new { Error = false });
        }

        public ActionResult Delete(int pProblemId)
        {
            problemService.DeleteProblem(pProblemId);
            return Json(new { Error = false });
        }
    }
}