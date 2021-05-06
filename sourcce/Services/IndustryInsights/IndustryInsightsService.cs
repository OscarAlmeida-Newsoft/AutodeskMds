using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTOs;
using IServices;
using IRepositories;
using Entities;
using AutoMapper;
using DTOs.Utils;


namespace Services 
{
    public class IndustryInsightsService : IIndustryInsightsService
    {
        
        private readonly IIndustryInsightsRepository industryInsightsRepository;
        private readonly IProcessPreLoadedRepository processPreLoadedRepository;
        private readonly ITranslatorUtility translatorUtility;
        private readonly IProblemPreLoadedRepository problemPreLoadedRepository;
        private readonly IDigitalTransformationPreLoadedRepository digitalTransformationPreLoadedRepository;
        private readonly IProcessRepository processRepository;
        private readonly IProblemRepository problemRepository;
        private readonly IDigitalTransformationRepository digitalTranformationRepository;
        private readonly IAssessmentSummaryRepository assessmentSummaryRepository;
        private readonly ILeadRepository leadRepository;
        private readonly IActivityService activityService;
        protected IUnitOfWork.IUnitOfWork unitOfWork;
        private readonly IAssessmentService assessmentService;

        public IndustryInsightsService(
            IIndustryInsightsRepository pIndustryInsightsRepository,
            IProcessPreLoadedRepository pProcessPreLoadedRepository,
            ITranslatorUtility pTranslatorUtility,
            IProblemPreLoadedRepository pProblemPreLoadedRepository,
            IDigitalTransformationPreLoadedRepository pDigitalTransformationRepository,
            IProcessRepository pProcessRepository,
            IProblemRepository pProblemRepository,
            IDigitalTransformationRepository pDigitalTranformationRepository,
            IAssessmentSummaryRepository pAssessmentSummaryRepository,
            ILeadRepository pLeadRepository,
            IActivityService pActivityService,
            IUnitOfWork.IUnitOfWork pUnitOfWork,
            IAssessmentService pAssessmentService) {

            
            industryInsightsRepository = pIndustryInsightsRepository;
            processPreLoadedRepository = pProcessPreLoadedRepository;
            translatorUtility = pTranslatorUtility;
            problemPreLoadedRepository = pProblemPreLoadedRepository;
            digitalTransformationPreLoadedRepository = pDigitalTransformationRepository;
            processRepository = pProcessRepository;
            problemRepository = pProblemRepository;
            digitalTranformationRepository = pDigitalTranformationRepository;
            assessmentSummaryRepository = pAssessmentSummaryRepository;
            leadRepository = pLeadRepository;
            activityService = pActivityService;
            unitOfWork = pUnitOfWork;
            assessmentService = pAssessmentService;
        }

        public void GeneratePreLoadInfo(int AssesmentSummaryId, int IndustryId, int LanguageId, Guid LeadId)
        {
            //Obtengo todos los procesos asociados
            IEnumerable<NS_TblProcessPreLoaded> prs = processPreLoadedRepository.GetAllProccessPreLoadedByIndustry(IndustryId);
            IEnumerable<ProcessDTO> processes = Mapper.Map<IEnumerable<NS_TblProcessPreLoaded>, IEnumerable<ProcessDTO>>(prs);

            foreach (ProcessDTO proc in processes)
            {

                //Obtengo traducciones

                proc.AssessmentSummaryId = AssesmentSummaryId;
                proc.Name = translatorUtility.TranslateTextById(proc.TranslatorAdministratorNameId, LanguageId);
                proc.Description = translatorUtility.TranslateTextById(proc.TranslatorAdministratorDescriptionId, LanguageId);

                //PROBLEMAS
                //Obtengo listado de problemas
                IEnumerable<NS_TblProblemPreLoaded> pbls = problemPreLoadedRepository.GetAllProblemPreLoadedByProcess(proc.Id);
                IEnumerable<ProblemDTO> problems = Mapper.Map<IEnumerable<NS_TblProblemPreLoaded>, IEnumerable<ProblemDTO>>(pbls);

                foreach (ProblemDTO prob in problems)
                {
                    prob.ProblemDescription = translatorUtility.TranslateTextById(prob.TranslatorAdministratorProblemId, LanguageId);
                    prob.Opportunity = translatorUtility.TranslateTextById(prob.TranslatorAdministratorOpportunityId, LanguageId);
                    prob.Technology = translatorUtility.TranslateTextById(prob.TranslatorAdministratorTechnologyId, LanguageId);
                    prob.Inventory = translatorUtility.TranslateTextById(prob.TranslatorAdministratorInventoryId, LanguageId);
                    prob.Solution = (prob.TranslatorAdministratorSolutionId == null ? "" : translatorUtility.TranslateTextById(prob.TranslatorAdministratorSolutionId.Value, LanguageId));
                }
                proc.Problems = problems;

                //TRANSFORMACION DIGITAL
                //Obtengo listado de digital transformation
                IEnumerable<NS_TblDigitalTransformationPreLoaded> dt = digitalTransformationPreLoadedRepository.GetAllDigitalTransformationPreLoadedByProcess(proc.Id);
                IEnumerable<DigitalTransformationDTO> digitalTransformations = Mapper.Map<IEnumerable<NS_TblDigitalTransformationPreLoaded>, IEnumerable<DigitalTransformationDTO>>(dt);

                foreach (DigitalTransformationDTO digTrans in digitalTransformations)
                {

                    digTrans.Pillar = translatorUtility.TranslateTextById(digTrans.TranslatorAdministratorPillarId, LanguageId);
                    digTrans.Technology = translatorUtility.TranslateTextById(digTrans.TranslatorAdministratorTechnologyId, LanguageId);
                    digTrans.Brand = translatorUtility.TranslateTextById(digTrans.TranslatorAdministratorBrandId, LanguageId);
                    digTrans.Comment = translatorUtility.TranslateTextById(digTrans.TranslatorAdministratorCommentId, LanguageId);
                    digTrans.Solution = (digTrans.TranslatorAdministratorSolutionId == null ? "" : translatorUtility.TranslateTextById(digTrans.TranslatorAdministratorSolutionId.Value, LanguageId));
                }
                proc.DigitalTransformations = digitalTransformations;
            }

            //INSERTO DATA ASOCIADA AL USUARIO

            //Inserto procesos
            foreach (ProcessDTO proc in processes)
            {
                NS_TblProcess newProcess = Mapper.Map<ProcessDTO, NS_TblProcess>(proc);
                newProcess.UserCreation = LeadId;
                newProcess.DateCreation = DateTime.Now;
                newProcess.UserLastModification = newProcess.UserCreation;
                newProcess.DateLastModification = newProcess.DateCreation;

                newProcess.Id = 0;
                int IdProcess = processRepository.InsertProcess(newProcess);

                //Inserto problemas
                foreach (ProblemDTO prob in proc.Problems)
                {
                    NS_TblProblem newProblem = Mapper.Map<ProblemDTO, NS_TblProblem>(prob);
                    newProblem.UserCreation = LeadId;
                    newProblem.DateCreation = DateTime.Now;
                    newProblem.UserLastModification = newProcess.UserCreation;
                    newProblem.DateLastModification = newProcess.DateCreation;
                    newProblem.Id = 0;
                    newProblem.ProcessId = IdProcess;

                    problemRepository.InsertProblem(newProblem);
                }

                //Inserto Transformación digital
                foreach (DigitalTransformationDTO digital in proc.DigitalTransformations)
                {
                    NS_TblDigitalTransformation newDigitalTranformation = Mapper.Map<DigitalTransformationDTO, NS_TblDigitalTransformation>(digital);
                    newDigitalTranformation.UserCreation = LeadId;
                    newDigitalTranformation.DateCreation = DateTime.Now;
                    newDigitalTranformation.UserLastModification = newProcess.UserCreation;
                    newDigitalTranformation.DateLastModification = newProcess.DateCreation;
                    newDigitalTranformation.Id = 0;
                    newDigitalTranformation.ProcessId = IdProcess;

                    digitalTranformationRepository.InsertDigitalTransformation(newDigitalTranformation);
                }
            }

            unitOfWork.Complete();
        }

        public IEnumerable<IndustryIndInsDTO> GetAllIndustries()
        {
            IEnumerable<NS_tblIndustryIndIns> data = industryInsightsRepository.GetAllIndustries();
            var number = data.Count();
            IEnumerable<IndustryIndInsDTO> industries = Mapper.Map<IEnumerable<NS_tblIndustryIndIns>, IEnumerable<IndustryIndInsDTO>>(data);

            return industries;
        }

        public IEnumerable<IndustryInsightsAdminDTO> GetAllIndustryInsightsAssessments(int pPageNumber, int pPageSize)
        {
            IEnumerable<NS_TblAssessmentType> assessments = assessmentService.GetAssessmentType();
            int idIndustryInsights = assessments.SingleOrDefault(d => d.IsIndustryInsights).Id;

            return industryInsightsRepository.GetAllIndustryInsightsAssessments(pPageNumber, pPageSize, idIndustryInsights);
        }

        public IndustryIndInsDTO GetIndustryById(int pId)
        {
            var _industry = industryInsightsRepository.GetIndustryById(pId);

            return Mapper.Map<NS_tblIndustryIndIns, IndustryIndInsDTO>(_industry);
        }

        public int GetNumbreAllIndustryInsightsAssessments()
        {
            IEnumerable<NS_TblAssessmentType> assessments = assessmentService.GetAssessmentType();
            int idIndustryInsights = assessments.SingleOrDefault(d => d.IsIndustryInsights).Id;

            return industryInsightsRepository.GetNumbreAllIndustryInsightsAssessments(idIndustryInsights);
        }

        public RecommendationsDocumentDTO GetRecommendationInfo(int pAssessmentSumaryId)
        {
            RecommendationsDocumentDTO recommendations = new RecommendationsDocumentDTO();
            recommendations.Year = DateTime.Now.Year;
            recommendations.Month = string.Format("{0:MMMM}", DateTime.Now);
                        
            NS_TblAssessmentSummary summary = assessmentSummaryRepository.GetAssessmentsSummaryById(pAssessmentSumaryId);
            LeadInformation user=  leadRepository.GetByLeadId(summary.CompanyId);
            

            recommendations.CompanyName = user.CompanyName;
            recommendations.CompanyDescription = "Compañía dedicada a la elaboración y comercialización de " +
                "productos de consumo masivo para el aseo y limpieza del " +
                "hogar, industriales, agropecuarios y químicos para tratamiento " +
                "de aguas y químicos industriales";


            //Obtengo Actividades 
            IEnumerable<ActivityDTO> activities = activityService.GetAllActivities();

            //Obtengo los procesos
            IEnumerable<NS_TblProcess> prs = processRepository.GetProcessesByAssesmentSummaryId(pAssessmentSumaryId);

            if (prs != null)
            {

                IEnumerable<ProcessDTO> processes = Mapper.Map<IEnumerable<NS_TblProcess>, IEnumerable<ProcessDTO>>(prs);
                recommendations.Processes = processes;

                //Obtengo los problemas
                foreach (ProcessDTO process in recommendations.Processes)
                {
                    process.TranslatorAdministratorActivityDescriptionId = activities.SingleOrDefault(d => d.Id == process.ActivityId).TranslatorAdministratorDescriptionId;

                    IEnumerable<NS_TblProblem> prbs = problemRepository.GetAllProblemByProcess(process.Id);
                    process.Problems = Mapper.Map<IEnumerable<NS_TblProblem>, IEnumerable<ProblemDTO>>(prbs);
                }

                //Obtengo la transformacion digital
                foreach (ProcessDTO process in recommendations.Processes)
                {

                    IEnumerable<NS_TblDigitalTransformation> dt = digitalTranformationRepository.GetAllDigitalTransformationByProcess(process.Id);
                    process.DigitalTransformations = Mapper.Map<IEnumerable<NS_TblDigitalTransformation>, IEnumerable<DigitalTransformationDTO>>(dt);
                }

            }

            //Genero lista next steps
            //recommendations.NextSteps = new List<string>();
            //recommendations.NextSteps.Add("Prueba de concepto Mobile");
            //recommendations.NextSteps.Add("Prueba de concepto IoT");

            recommendations.NextSteps = summary.NextSteps ?? "Please wait consultant contact you for get next steps to follow";
            recommendations.Activities = activities;


            //Logica para calcular el scoring
            foreach (ActivityDTO activ in recommendations.Activities) {
                decimal totalProbAndDT = 0;
                decimal totalProbAndDT_Resolved = 0;

                activ.TieneProcesos = false;

                
                if(recommendations.Processes != null)
                {
                    foreach (ProcessDTO proc in recommendations.Processes.Where(d => d.ActivityId == activ.Id))
                    {

                        if (!activ.TieneProcesos)
                        {
                            activ.TieneProcesos = true;
                        }

                        decimal total = proc.Problems.Count() + proc.DigitalTransformations.Count();
                        decimal totalResolved = proc.Problems.Count(d => d.Solved) + proc.DigitalTransformations.Count(d => d.Solved);

                        if (total > 0)
                        {
                            //Aumento el contador de la actividad
                            totalProbAndDT += total;
                            totalProbAndDT_Resolved += totalResolved;

                            proc.Score = decimal.Round((totalResolved / total) * 100);
                            proc.ClassCss = getClassCss(false, proc.Score);
                        }

                    }

                }
                

                if (activ.TieneProcesos && totalProbAndDT > 0)
                {
                    activ.Score = decimal.Round((totalProbAndDT_Resolved / totalProbAndDT) * 100);
                    activ.ClassCss = getClassCss(true, activ.Score);
                }
                else {
                    activ.TieneProcesos = false;
                }

            }

            return recommendations;
        }

        private string getClassCss(bool isActivity, decimal score) {
            string theClass = "";

            if (score >= 0 && score <= 10) {
                theClass = "color_0_10";
                
            } else if (score >= 11 && score <= 20) {
                theClass = "color_11_20";
            }
            else if (score >= 21 && score <= 30)
            {
                theClass = "color_21_30";
            }
            else if (score >= 31 && score <= 40)
            {
                theClass = "color_31_40";
            }
            else if (score >= 41 && score <= 50)
            {
                theClass = "color_41_50";
            }
            else if (score >= 51 && score <= 60)
            {
                theClass = "color_51_60";
            }
            else if (score >= 61 && score <= 70)
            {
                theClass = "color_61_70";
            }
            else if (score >= 71 && score <= 80)
            {
                theClass = "color_71_80";
            }
            else if (score >= 81 && score <= 90)
            {
                theClass = "color_81_90";
            }
            else if (score >= 91 && score <= 100)
            {
                theClass = "color_91_100";
            }

            if (!isActivity)
            {
                theClass = "text" + theClass;
            }
           
            return theClass;
        }

        public void MarkIndustryInsightAsFinished(Guid LeadId, Guid CampaignId, int pAssessmentSumaryId)
        {
            assessmentSummaryRepository.MarkAssessmentSummaryAsFinished(LeadId, CampaignId, pAssessmentSumaryId, "D");
            unitOfWork.Complete();
        }

        public bool ValidateFinalIndustryInsigth(int pAssessmentSumaryId)
        {
            var _data = GetRecommendationInfo(pAssessmentSumaryId);

            if (_data.Processes != null)
            {
                foreach (var item in _data.Processes)
                {
                    if (item.Problems.Count() == 0 || item.DigitalTransformations.Count() == 0)
                    {
                        return false;
                    }
                }
            }
            else {
                return false;
            }

            

            return true;
        }
    }
}
