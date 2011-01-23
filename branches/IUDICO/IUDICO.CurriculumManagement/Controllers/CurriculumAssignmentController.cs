using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IUDICO.Common.Models;
using IUDICO.CurriculumManagement.Models.Storage;
using IUDICO.CurriculumManagement.Controllers;
using IUDICO.CurriculumManagement.Models;


namespace IUDICO.CurriculumManagement.Controllers
{
    public class CurriculumAssignmentController : CurriculumBaseController
    {
        public CurriculumAssignmentController(ICurriculumStorage curriculumStorage)
            : base(curriculumStorage)
        {

        }

        public ActionResult Index(int curriculumId)
        {
            try
            {
                Curriculum curriculum = Storage.GetCurriculum(curriculumId);
                var curriculumAssignments = Storage.GetCurriculumAssignmnetsByCurriculumId(curriculumId);

                if (curriculumAssignments != null && curriculum != null)
                {
                    ViewData["CurriculumName"] = curriculum.Name;
                    return View
                    (
                        from curriculumAssignment in curriculumAssignments
                        select new ViewCurriculumAssignmentModel
                        {
                            Id = curriculumAssignment.Id,
                            GroupName = Storage.GetGroup(curriculumAssignment.UserGroupRef).Name
                        }
                    );
                }
                else
                {
                    throw new Exception("Cannot read records");
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        [HttpGet]
        public ActionResult Create(int curriculumId)
        {
            try
            {
                IEnumerable<Group> groups = Storage.GetNotAssignedGroups(curriculumId);

                CreateCurriculumAssignmentModel createAssignmentModel = new CreateCurriculumAssignmentModel()
                {
                    Groups = from item in groups
                             select new SelectListItem { Text = item.Name, Value = item.Id.ToString(), Selected = false }
                };

                return View(createAssignmentModel);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        [HttpPost]
        public ActionResult Create(int curriculumId, CreateCurriculumAssignmentModel createAssignmentModel)
        {
            try
            {
                if (createAssignmentModel.GroupId == 0)
                    return RedirectToAction("Index");
                
                CurriculumAssignment newCurriculumAssignment = new CurriculumAssignment();
                newCurriculumAssignment.UserGroupRef = createAssignmentModel.GroupId;
                newCurriculumAssignment.CurriculumRef = curriculumId;

                int curriculumAssingnmentId = Storage.AddCurriculumAssignment(newCurriculumAssignment);

                //add themeAssignments
                var themesInCurrentCurriculum = Storage.GetThemesByCurriculumId(curriculumId);

                foreach (var theme in themesInCurrentCurriculum)
                {
                    if (theme.ThemeTypeRef == 1)
                    {
                        ThemeAssignment newThemeAssingment = new ThemeAssignment()
                        {
                            CurriculumAssignmentRef = curriculumAssingnmentId,
                            ThemeRef = theme.Id,
                            MaxScore = Constants.DefaultThemeMaxScore // треба втикнути шо тут ставити
                        };

                        Storage.AddThemeAssignment(newThemeAssingment);
                    }
                }
                
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        [HttpGet]
        public ActionResult Edit(int curriculumAssignmentId)
        {
            try
            {
                int curriculumId = Storage.GetCurriculumAssignment(curriculumAssignmentId).CurriculumRef;

                IEnumerable<Group> groups = Storage.GetNotAssignedGroups(curriculumId);

                //add current group
                int assignmentGroupId = Storage.GetCurriculumAssignment(curriculumAssignmentId).UserGroupRef;
                List<Group> assignmentedGroup = new List<Group>();
                assignmentedGroup.Add(Storage.GetGroup(assignmentGroupId));

                groups = groups.Concat(assignmentedGroup as IEnumerable<Group>);
 
                CreateCurriculumAssignmentModel editAssignmentModel = new CreateCurriculumAssignmentModel()
                {
                    Groups = from item in groups
                             select new SelectListItem { Text = item.Name, Value = item.Id.ToString(), Selected = false },
                    GroupId = assignmentGroupId
                };

                Session["CurriculumId"] = curriculumId;
                return View(editAssignmentModel);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        [HttpPost]
        public ActionResult Edit(int curriculumAssignmentId, CreateCurriculumAssignmentModel editAssignmentModel)
        {
            try
            {
                if (editAssignmentModel.GroupId == 0)
                    return RedirectToAction("Index");

                CurriculumAssignment curriculumAssignment = new CurriculumAssignment();
                curriculumAssignment.UserGroupRef = editAssignmentModel.GroupId;
                curriculumAssignment.Id = curriculumAssignmentId;
                Storage.UpdateCurriculumAssignment(curriculumAssignment);

                return RedirectToRoute("CurriculumAssignments", new { action="Index", CurriculumId = Session["CurriculumId"] });
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        [HttpPost]
        public JsonResult DeleteItem(int curriculumAssignmentId)
        {
            try
            {
                Storage.DeleteCurriculumAssignment(curriculumAssignmentId);

                return Json(new { success = true });
            }
            catch (Exception e)
            {
                return Json(new { success = false, message = e.Message });
            }
        }

        [HttpPost]
        public JsonResult DeleteItems(int[] curriculumAssignmentIds)
        {
            try
            {
                Storage.DeleteCurriculumAssignments(curriculumAssignmentIds);

                return Json(new { success = true });
            }
            catch (Exception e)
            {
                return Json(new { success = false, message = e.Message });
            }
        }
    }
}