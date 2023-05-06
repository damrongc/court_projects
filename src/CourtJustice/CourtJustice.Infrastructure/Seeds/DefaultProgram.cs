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
                    ParentProgramId= 0,
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


                //var masterMenu = new AppProgram
                //{
                //    ProgramName = "ข้อมูล Master",
                //    MenuIcon = "mdi mdi-database menu-icon"
                //};
                //context.AppPrograms.Add(masterMenu);
                //context.SaveChanges();

                //programList = new()
                //{
                //    new AppProgram
                //    {
                //        ProgramName = "ข้อมูลกลุ่มสินค้า",
                //        ParentProgramId= masterMenu.ProgramId,
                //        ControllerName= "ProductGroups",
                //        ActionName="Index"
                //    },
                //    new app_program
                //    {
                //        program_name = "ข้อมูลประเถทสินค้า",
                //        parent_program_id= masterMenu.id,
                //         controller_name= "ProductTypes",
                //        action_name="Index"
                //    },
                //    new app_program
                //    {
                //        program_name = "ข้อมูลสินค้า",
                //        parent_program_id= masterMenu.id,
                //         controller_name= "Products",
                //        action_name="Index"
                //    },
                //    new app_program
                //    {
                //        program_name = "ข้อมูลตำแหน่งจัดเก็บ",
                //        parent_program_id= masterMenu.id,
                //         controller_name= "Locations",
                //        action_name="Index"
                //    },

                //};
                //context.AppPrograms.AddRange(programList);
                //context.SaveChanges();


                //var processMenu = new app_program
                //{
                //    program_name = "หน้าจอการทำงาน",
                //    menu_icon = "mdi mdi-airplay menu-icon"
                //};
                //context.AppPrograms.Add(processMenu);
                //context.SaveChanges();

                //programList = new()
                //{
                //    new app_program
                //    {
                //        program_name = "การรับเข้าสินค้า",
                //        parent_program_id= processMenu.id,
                //        controller_name= "StockReceives",
                //        action_name="Index"
                //    },
                //    new app_program
                //    {
                //        program_name = "การเบิกจ่ายสินค้า",
                //        parent_program_id= processMenu.id,
                //         controller_name= "StockIssues",
                //        action_name="Index"
                //    },


                //};
                //context.AppPrograms.AddRange(programList);
                //context.SaveChanges();
            }

            if (!context.AppPrograms.Any(p => p.ControllerName== "Loanees"))
            {
                context.AppPrograms.Add(new AppProgram { ProgramName = "ข้อมูลลูกหนี้", ParentProgramId=1,ControllerName= "Loanees" ,ActionName="Index"});
                context.SaveChanges();
            }

        }
    }
}
