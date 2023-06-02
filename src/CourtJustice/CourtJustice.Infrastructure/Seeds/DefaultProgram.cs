using CourtJustice.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourtJustice.Infrastructure.Seeds
{
    public static class DefaultProgram
    {
        public static void Seed(ApplicationDbContext context)
        {
            var programs = context.AppPrograms.ToList();
            if (programs.Count == 0)
            {

                var adminMenu = new AppProgram
                {
                    ProgramName = "ผู้ดูแลระบบ",
                    ParentProgramId = 0,
                    MenuIcon = "mdi mdi-shield-check menu-icon"
                };

                context.AppPrograms.Add(adminMenu);
                context.SaveChanges();

                List<AppProgram> programList = new()
                {
                    new AppProgram
                    {
                        ProgramName = "ข้อมูลกลุ่มผู้ใช้งาน",
                        ParentProgramId= adminMenu.ProgramId,
                        ControllerName= "GroupUsers",
                        ActionName="Index"
                    },
                    new AppProgram
                    {
                        ProgramName = "ข้อมูลผู้ใช้งาน",
                        ParentProgramId= adminMenu.ProgramId,
                         ControllerName= "AppUsers",
                        ActionName="Index"
                    },

                };
                context.AppPrograms.AddRange(programList);
                context.SaveChanges();


                var masterMenu = new AppProgram
                {
                    ProgramName = "ข้อมูลหลัก",
                    ParentProgramId = 0,
                    MenuIcon = "mdi mdi-database menu-icon"
                };
                context.AppPrograms.Add(masterMenu);
                context.SaveChanges();

                List<AppProgram> programMasterList = new()
                {
                   
                    //new AppProgram { ProgramName = "Titles", ParentProgramId= masterMenu.ProgramId, ControllerName= "Titles", ActionName="Index" },
                     new AppProgram { ProgramName = "สถานะบัญชี", ParentProgramId= masterMenu.ProgramId, ControllerName= "Buckets", ActionName="Index" },
                    new AppProgram { ProgramName = "ประเภทบัตร", ParentProgramId= masterMenu.ProgramId, ControllerName= "CardTypes", ActionName="Index" },
                    new AppProgram { ProgramName = "ผลคดี", ParentProgramId= masterMenu.ProgramId, ControllerName= "CaseResults", ActionName="Index" },
                    //new AppProgram { ProgramName = "ประเภทสินเชื่อ", ParentProgramId= masterMenu.ProgramId, ControllerName= "CreditTypes", ActionName="Index" },
                    new AppProgram { ProgramName = "สำนักงานที่ดิน", ParentProgramId= masterMenu.ProgramId, ControllerName= "LandOffices", ActionName="Index" },
                    new AppProgram { ProgramName = "คดีความ", ParentProgramId= masterMenu.ProgramId, ControllerName= "LawCases", ActionName="Index" },
                    new AppProgram { ProgramName = "ศาล", ParentProgramId= masterMenu.ProgramId, ControllerName= "Courts", ActionName="Index" },
                    new AppProgram { ProgramName = "ประเภทรถ", ParentProgramId= masterMenu.ProgramId, ControllerName= "CarTypes", ActionName="Index" },
                    new AppProgram{ProgramName = "ประเภทสินเชื่อ",ParentProgramId= masterMenu.ProgramId,ControllerName= "LoanTypes",ActionName="Index"},
                    new AppProgram{ProgramName = "อาชีพ",ParentProgramId= masterMenu.ProgramId,ControllerName= "Occupations",ActionName="Index"},
                   
                    new AppProgram{ProgramName = "กลุ่มงาน",ParentProgramId= masterMenu.ProgramId,ControllerName= "LoanTaskStatuses",ActionName="Index"},
                    //new AppProgram{ProgramName = "กลุ่มงาน",ParentProgramId= masterMenu.ProgramId,ControllerName= "LoanSubTaskStatuses",ActionName="Index"},
                    new AppProgram{ProgramName = "ข้อมูลบริษัท",ParentProgramId= masterMenu.ProgramId,ControllerName= "Companys",ActionName="Index"},
                    //new AppProgram { ProgramName = "ประเภทสินทรัพย์", ParentProgramId= masterMenu.ProgramId, ControllerName= "AssetTypes", ActionName="Index" },
                     new AppProgram { ProgramName = "สำนักงานทนายความ",ParentProgramId= masterMenu.ProgramId, ControllerName= "LawOffices", ActionName="Index" },
                };
                context.AppPrograms.AddRange(programMasterList);
                context.SaveChanges();

                var otherMenu = new AppProgram
                {
                    ProgramName = "ข้อมูล",
                    ParentProgramId = 0,
                    MenuIcon = "mdi mdi-folder-open menu-icon"
                };
                context.AppPrograms.Add(otherMenu);
                context.SaveChanges();

                List<AppProgram> programOtherList = new()
                {
                    new AppProgram { ProgramName = "ข้อมูลทนายความ", ParentProgramId= otherMenu.ProgramId, ControllerName= "Lawyers", ActionName="Index" },
                    new AppProgram { ProgramName = "ผู้ว่าจ้าง", ParentProgramId= otherMenu.ProgramId, ControllerName= "Employers", ActionName="Index" },
                    new AppProgram { ProgramName = "พนักงาน", ParentProgramId= otherMenu.ProgramId, ControllerName= "Employees", ActionName="Index" },
                    new AppProgram { ProgramName = "ข้อมูลลูกหนี้", ParentProgramId= otherMenu.ProgramId, ControllerName= "Loanees", ActionName="Index" },
                    new AppProgram { ProgramName = "ข้อมูลฟ้อง", ParentProgramId= otherMenu.ProgramId, ControllerName= "JusticeCase", ActionName="Index" },
                   
                };
                context.AppPrograms.AddRange(programOtherList);
                context.SaveChanges();
            }

           /* if (!context.AppPrograms.Any(p => p.ControllerName == "Loanees"))
            {
                context.AppPrograms.Add(new AppProgram { ProgramName = "ข้อมูลลูกหนี้", ParentProgramId = 1, ControllerName = "Loanees", ActionName = "Index" });
                context.SaveChanges();
            }*/

        }
    }
}
