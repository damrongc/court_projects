using CourtJustice.Domain.Models;
using CourtJustice.Domain.ViewModels;
using CourtJustice.Infrastructure.Helpers;
using CourtJustice.Infrastructure.Interfaces;
using CourtJustice.Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace CourtJustice.Web.Controllers
{
    public class GroupUsersController : BaseController<GroupUsersController>
    {
        private readonly IGroupUserRepository _groupUserRepository;
        private readonly IUserPermissionRepository _userPermissionRepository;
        private readonly IAppProgramRepository _appProgramRepository;

        public GroupUsersController(IGroupUserRepository groupUserRepository,
            IUserPermissionRepository userPermissionRepository,
            IAppProgramRepository appProgramRepository)
        {
            _groupUserRepository = groupUserRepository;
            _userPermissionRepository = userPermissionRepository;
            _appProgramRepository = appProgramRepository;
        }

        public async Task<IActionResult> Index()
        {
            return View(await GetAll());
        }



        private async Task<List<GroupUser>> GetAll()
        {
            var results = await _groupUserRepository.GetAll();
            return results.ToList();
        }

        public IActionResult Create()
        {
            return View(new GroupUser { IsActive=true});
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(GroupUser model)
        {
            if (ModelState.IsValid)
            {
                await _groupUserRepository.Create(model);
                _notify.Success($"{model.GroupName} is Created.");
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var model = await _groupUserRepository.GetByKey(id);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, GroupUser model)
        {
            var oldEntity = await _groupUserRepository.GetByKey(id);

            if (oldEntity == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _groupUserRepository.Update(id, model);
                _notify.Success($"{model.GroupName} is Updated");

                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        [HttpDelete, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var count = await _groupUserRepository.CheckExistingAtUser(id);
                if (count > 0)
                {
                    return new JsonResult(new { isValid = false, message = "ไม่สามารถลบข้อมูลได้ \r\nเนื่องจากมีการใช้งานแล้ว!" });
                }
                await _groupUserRepository.Delete(id);
                //_notify.Success($"Delete is Success.");
                var results = await GetAll();
                var html = RenderRazorViewHelper.RenderRazorViewToString(this, "_ViewTable", results);
                return new JsonResult(new { isValid = true, message = "", html });
            }
            catch (Exception ex)
            {
                return new JsonResult(new { isValid = false, message = ex.Message });

            }

        }

        public async Task<IActionResult> SetPermission(int id)
        {
            var groupUser = await _groupUserRepository.GetByKey(id);
            var userPermissionView = new UserPermissionViewModel
            {
                GroupId = groupUser.GroupId,
                GroupName = groupUser.GroupName
            };

            var userPermissions = await _userPermissionRepository.GetByGroupId(id);
            var programs =await _appProgramRepository.GetAll();

            var appPrograms = new List<AppProgramViewModel>();
            if (userPermissions == null)
            {

                foreach (var item in programs)
                {
                    if (item.ParentProgramId == 0)
                    {
                        var program = new AppProgramViewModel
                        {
                            ProgramId = item.ProgramId,
                            ProgramName = item.ProgramName
                        };
                        if (userPermissions!.Where(i => i.ProgramId == item.ProgramId) != null)
                        {
                            program.IsCheck = true;
                        }
                        program.ProgramParents = new List<AppProgramViewModel>();
                        foreach (var item2 in programs)
                        {
                            if (item2.ParentProgramId == item.ProgramId)
                            {
                                var m2 = new AppProgramViewModel
                                {
                                    ProgramId = item2.ProgramId,
                                    ProgramName = item2.ProgramName,
                                    ParentProgramId = item.ParentProgramId
                                };
                                if (userPermissions!.Where(i => i.ProgramId == item2.ProgramId) != null)
                                {
                                    m2.IsCheck = true;
                                }
                                program.ProgramParents.Add(m2);
                            }
                        }

                        appPrograms.Add(program);
                    }
                }
            }
            else
            {
                foreach (var item in programs)
                {
                    if (item.ParentProgramId == 0)
                    {
                        var m = new AppProgramViewModel
                        {
                            ProgramId = item.ProgramId,
                            ProgramName = item.ProgramName,
                            ParentProgramId = item.ParentProgramId,
                        };

                        var obj = userPermissions.Where(i => i.ProgramId == item.ProgramId);
                        if (obj.ToList().Count > 0)
                        {
                            m.IsCheck = true;
                        }
                        m.ProgramParents = new List<AppProgramViewModel>();
                        foreach (var item2 in programs)
                        {
                            if (item2.ParentProgramId == item.ProgramId)
                            {
                                var m2 = new AppProgramViewModel
                                {
                                    ProgramId = item2.ProgramId,
                                    ProgramName = item2.ProgramName,
                                    ParentProgramId = item2.ParentProgramId
                                };
                                var obj2 = userPermissions.Where(i => i.ProgramId == item2.ProgramId);
                                if (obj2.ToList().Count > 0)
                                {
                                    m2.IsCheck = true;
                                }
                                m.ProgramParents.Add(m2);
                            }
                        }
                        appPrograms.Add(m);
                    }
                }
            }
            userPermissionView.AppPrograms = appPrograms;
            return View(userPermissionView);
        }

        [HttpPost]
        public async Task<IActionResult> SetPermission([FromBody] List<UserPermissionViewModel> userPermissions)
        {
            try
            {
                await _userPermissionRepository.DeleteByGroupId(userPermissions[0].GroupId);
                foreach (var item in userPermissions)
                {
                    var userPermission = new UserPermission();
                    var program = await _appProgramRepository.GetById(item.ProgramId);
                    if (program.ParentProgramId != 0)
                    {
                        var root = await _userPermissionRepository.GetRootMenu((int)program.ParentProgramId, item.GroupId);
                        if (root == null)
                        {
                            userPermission = new UserPermission
                            {
                                ProgramId = (int)program.ParentProgramId,
                                GroupId = item.GroupId
                            };
                            await _userPermissionRepository.Create(userPermission);
                        }
                    }
                    userPermission = new UserPermission
                    {
                        ProgramId = item.ProgramId,
                        GroupId = item.GroupId
                    };
                    await _userPermissionRepository.Create(userPermission);

                }
                //var oldPermission = _context.UserPermissions.Where(p => p.GroupId == userPermissions[0].GroupId);
                //_context.UserPermissions.RemoveRange(oldPermission);
                //await _context.SaveChangesAsync();


                //foreach (var item in userPermissions)
                //{
                //    var userPermission = new UserPermission();
                //    var program = await _context.AppPrograms.FindAsync(item.ProgramId);
                //    if (program.ParentProgramId != null)
                //    {
                //        var root = await _context.UserPermissions.Where(p => p.ProgramId == program.ParentProgramId && p.GroupId == item.GroupId).FirstOrDefaultAsync();
                //        if (root == null)
                //        {
                //            userPermission = new UserPermission();
                //            userPermission.ProgramId = (int)program.ParentProgramId;
                //            userPermission.GroupId = item.GroupId;
                //            _context.UserPermissions.Add(userPermission);
                //            await _context.SaveChangesAsync();
                //        }
                //    }


                //    userPermission = new UserPermission();
                //    userPermission.ProgramId = item.ProgramId;
                //    userPermission.GroupId = item.GroupId;
                //    _context.UserPermissions.Add(userPermission);
                //    await _context.SaveChangesAsync();
                //}

                //await _context.SaveChangesAsync();
                _notify.Success($"User Permission is Updated");

                return new JsonResult(new { isValid = true, message = "" });
            }
            catch (Exception ex)
            {
                return new JsonResult(new { isValid = false, message = ex.ToString() });
            }

        }
    }
}
