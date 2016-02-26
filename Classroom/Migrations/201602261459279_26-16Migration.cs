namespace Classroom.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _2616Migration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Classes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Teachers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        UserName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Tasks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SubjectsId = c.Int(nullable: false),
                        DateGiven = c.DateTime(nullable: false),
                        SubmissionDate = c.DateTime(nullable: false),
                        TaskName = c.String(),
                        Description = c.String(),
                        MarkGiven = c.Double(nullable: false),
                        Task_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Tasks", t => t.Task_Id)
                .Index(t => t.Task_Id);
            
            AddColumn("dbo.Students", "TeacherId", c => c.Int(nullable: false));
            AddColumn("dbo.Subjects", "TeacherId", c => c.Int(nullable: false));
            AddColumn("dbo.Subjects", "ClassId", c => c.Int(nullable: false));
            AddColumn("dbo.Subjects", "StudentId", c => c.Int(nullable: false));
            CreateIndex("dbo.Students", "TeacherId");
            CreateIndex("dbo.Subjects", "TeacherId");
            CreateIndex("dbo.Subjects", "ClassId");
            CreateIndex("dbo.Subjects", "StudentId");
            AddForeignKey("dbo.Students", "TeacherId", "dbo.Teachers", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Subjects", "ClassId", "dbo.Classes", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Subjects", "StudentId", "dbo.Students", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Subjects", "TeacherId", "dbo.Teachers", "Id", cascadeDelete: true);
            CreateStoredProcedure(
                "dbo.Class_Insert",
                p => new
                    {
                        Name = p.String(),
                    },
                body:
                    @"INSERT [dbo].[Classes]([Name])
                      VALUES (@Name)
                      
                      DECLARE @Id int
                      SELECT @Id = [Id]
                      FROM [dbo].[Classes]
                      WHERE @@ROWCOUNT > 0 AND [Id] = scope_identity()
                      
                      SELECT t0.[Id]
                      FROM [dbo].[Classes] AS t0
                      WHERE @@ROWCOUNT > 0 AND t0.[Id] = @Id"
            );
            
            CreateStoredProcedure(
                "dbo.Class_Update",
                p => new
                    {
                        Id = p.Int(),
                        Name = p.String(),
                    },
                body:
                    @"UPDATE [dbo].[Classes]
                      SET [Name] = @Name
                      WHERE ([Id] = @Id)"
            );
            
            CreateStoredProcedure(
                "dbo.Class_Delete",
                p => new
                    {
                        Id = p.Int(),
                    },
                body:
                    @"DELETE [dbo].[Classes]
                      WHERE ([Id] = @Id)"
            );
            
            CreateStoredProcedure(
                "dbo.Student_Insert",
                p => new
                    {
                        FirstName = p.String(),
                        LastName = p.String(),
                        Age = p.Int(),
                        TeacherId = p.Int(),
                    },
                body:
                    @"INSERT [dbo].[Students]([FirstName], [LastName], [Age], [TeacherId])
                      VALUES (@FirstName, @LastName, @Age, @TeacherId)
                      
                      DECLARE @Id int
                      SELECT @Id = [Id]
                      FROM [dbo].[Students]
                      WHERE @@ROWCOUNT > 0 AND [Id] = scope_identity()
                      
                      SELECT t0.[Id]
                      FROM [dbo].[Students] AS t0
                      WHERE @@ROWCOUNT > 0 AND t0.[Id] = @Id"
            );
            
            CreateStoredProcedure(
                "dbo.Student_Update",
                p => new
                    {
                        Id = p.Int(),
                        FirstName = p.String(),
                        LastName = p.String(),
                        Age = p.Int(),
                        TeacherId = p.Int(),
                    },
                body:
                    @"UPDATE [dbo].[Students]
                      SET [FirstName] = @FirstName, [LastName] = @LastName, [Age] = @Age, [TeacherId] = @TeacherId
                      WHERE ([Id] = @Id)"
            );
            
            CreateStoredProcedure(
                "dbo.Student_Delete",
                p => new
                    {
                        Id = p.Int(),
                    },
                body:
                    @"DELETE [dbo].[Students]
                      WHERE ([Id] = @Id)"
            );
            
            CreateStoredProcedure(
                "dbo.Teacher_Insert",
                p => new
                    {
                        Name = p.String(),
                        UserName = p.String(),
                    },
                body:
                    @"INSERT [dbo].[Teachers]([Name], [UserName])
                      VALUES (@Name, @UserName)
                      
                      DECLARE @Id int
                      SELECT @Id = [Id]
                      FROM [dbo].[Teachers]
                      WHERE @@ROWCOUNT > 0 AND [Id] = scope_identity()
                      
                      SELECT t0.[Id]
                      FROM [dbo].[Teachers] AS t0
                      WHERE @@ROWCOUNT > 0 AND t0.[Id] = @Id"
            );
            
            CreateStoredProcedure(
                "dbo.Teacher_Update",
                p => new
                    {
                        Id = p.Int(),
                        Name = p.String(),
                        UserName = p.String(),
                    },
                body:
                    @"UPDATE [dbo].[Teachers]
                      SET [Name] = @Name, [UserName] = @UserName
                      WHERE ([Id] = @Id)"
            );
            
            CreateStoredProcedure(
                "dbo.Teacher_Delete",
                p => new
                    {
                        Id = p.Int(),
                    },
                body:
                    @"DELETE [dbo].[Teachers]
                      WHERE ([Id] = @Id)"
            );
            
            CreateStoredProcedure(
                "dbo.Subject_Insert",
                p => new
                    {
                        Name = p.String(),
                        Mark = p.Double(),
                        TeacherId = p.Int(),
                        ClassId = p.Int(),
                        StudentId = p.Int(),
                    },
                body:
                    @"INSERT [dbo].[Subjects]([Name], [Mark], [TeacherId], [ClassId], [StudentId])
                      VALUES (@Name, @Mark, @TeacherId, @ClassId, @StudentId)
                      
                      DECLARE @Id int
                      SELECT @Id = [Id]
                      FROM [dbo].[Subjects]
                      WHERE @@ROWCOUNT > 0 AND [Id] = scope_identity()
                      
                      SELECT t0.[Id]
                      FROM [dbo].[Subjects] AS t0
                      WHERE @@ROWCOUNT > 0 AND t0.[Id] = @Id"
            );
            
            CreateStoredProcedure(
                "dbo.Subject_Update",
                p => new
                    {
                        Id = p.Int(),
                        Name = p.String(),
                        Mark = p.Double(),
                        TeacherId = p.Int(),
                        ClassId = p.Int(),
                        StudentId = p.Int(),
                    },
                body:
                    @"UPDATE [dbo].[Subjects]
                      SET [Name] = @Name, [Mark] = @Mark, [TeacherId] = @TeacherId, [ClassId] = @ClassId, [StudentId] = @StudentId
                      WHERE ([Id] = @Id)"
            );
            
            CreateStoredProcedure(
                "dbo.Subject_Delete",
                p => new
                    {
                        Id = p.Int(),
                    },
                body:
                    @"DELETE [dbo].[Subjects]
                      WHERE ([Id] = @Id)"
            );
            
            CreateStoredProcedure(
                "dbo.Task_Insert",
                p => new
                    {
                        SubjectsId = p.Int(),
                        DateGiven = p.DateTime(),
                        SubmissionDate = p.DateTime(),
                        TaskName = p.String(),
                        Description = p.String(),
                        MarkGiven = p.Double(),
                        Task_Id = p.Int(),
                    },
                body:
                    @"INSERT [dbo].[Tasks]([SubjectsId], [DateGiven], [SubmissionDate], [TaskName], [Description], [MarkGiven], [Task_Id])
                      VALUES (@SubjectsId, @DateGiven, @SubmissionDate, @TaskName, @Description, @MarkGiven, @Task_Id)
                      
                      DECLARE @Id int
                      SELECT @Id = [Id]
                      FROM [dbo].[Tasks]
                      WHERE @@ROWCOUNT > 0 AND [Id] = scope_identity()
                      
                      SELECT t0.[Id]
                      FROM [dbo].[Tasks] AS t0
                      WHERE @@ROWCOUNT > 0 AND t0.[Id] = @Id"
            );
            
            CreateStoredProcedure(
                "dbo.Task_Update",
                p => new
                    {
                        Id = p.Int(),
                        SubjectsId = p.Int(),
                        DateGiven = p.DateTime(),
                        SubmissionDate = p.DateTime(),
                        TaskName = p.String(),
                        Description = p.String(),
                        MarkGiven = p.Double(),
                        Task_Id = p.Int(),
                    },
                body:
                    @"UPDATE [dbo].[Tasks]
                      SET [SubjectsId] = @SubjectsId, [DateGiven] = @DateGiven, [SubmissionDate] = @SubmissionDate, [TaskName] = @TaskName, [Description] = @Description, [MarkGiven] = @MarkGiven, [Task_Id] = @Task_Id
                      WHERE ([Id] = @Id)"
            );
            
            CreateStoredProcedure(
                "dbo.Task_Delete",
                p => new
                    {
                        Id = p.Int(),
                        Task_Id = p.Int(),
                    },
                body:
                    @"DELETE [dbo].[Tasks]
                      WHERE (([Id] = @Id) AND (([Task_Id] = @Task_Id) OR ([Task_Id] IS NULL AND @Task_Id IS NULL)))"
            );
            
        }
        
        public override void Down()
        {
            DropStoredProcedure("dbo.Task_Delete");
            DropStoredProcedure("dbo.Task_Update");
            DropStoredProcedure("dbo.Task_Insert");
            DropStoredProcedure("dbo.Subject_Delete");
            DropStoredProcedure("dbo.Subject_Update");
            DropStoredProcedure("dbo.Subject_Insert");
            DropStoredProcedure("dbo.Teacher_Delete");
            DropStoredProcedure("dbo.Teacher_Update");
            DropStoredProcedure("dbo.Teacher_Insert");
            DropStoredProcedure("dbo.Student_Delete");
            DropStoredProcedure("dbo.Student_Update");
            DropStoredProcedure("dbo.Student_Insert");
            DropStoredProcedure("dbo.Class_Delete");
            DropStoredProcedure("dbo.Class_Update");
            DropStoredProcedure("dbo.Class_Insert");
            DropForeignKey("dbo.Tasks", "Task_Id", "dbo.Tasks");
            DropForeignKey("dbo.Subjects", "TeacherId", "dbo.Teachers");
            DropForeignKey("dbo.Subjects", "StudentId", "dbo.Students");
            DropForeignKey("dbo.Subjects", "ClassId", "dbo.Classes");
            DropForeignKey("dbo.Students", "TeacherId", "dbo.Teachers");
            DropIndex("dbo.Tasks", new[] { "Task_Id" });
            DropIndex("dbo.Subjects", new[] { "StudentId" });
            DropIndex("dbo.Subjects", new[] { "ClassId" });
            DropIndex("dbo.Subjects", new[] { "TeacherId" });
            DropIndex("dbo.Students", new[] { "TeacherId" });
            DropColumn("dbo.Subjects", "StudentId");
            DropColumn("dbo.Subjects", "ClassId");
            DropColumn("dbo.Subjects", "TeacherId");
            DropColumn("dbo.Students", "TeacherId");
            DropTable("dbo.Tasks");
            DropTable("dbo.Teachers");
            DropTable("dbo.Classes");
        }
    }
}
