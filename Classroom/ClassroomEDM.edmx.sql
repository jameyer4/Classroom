
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 03/02/2016 16:53:31
-- Generated from EDMX file: D:\Git-Repos\Classroom\Classroom\ClassroomEDM.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [ClassroomDB];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_StudentMarks_ToSubject]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[StudentMarks] DROP CONSTRAINT [FK_StudentMarks_ToSubject];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[StudentMarks]', 'U') IS NOT NULL
    DROP TABLE [dbo].[StudentMarks];
GO
IF OBJECT_ID(N'[dbo].[Students]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Students];
GO
IF OBJECT_ID(N'[dbo].[Subjects]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Subjects];
GO
IF OBJECT_ID(N'[dbo].[Tasks]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Tasks];
GO
IF OBJECT_ID(N'[dbo].[Teachers]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Teachers];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'StudentMarks'
CREATE TABLE [dbo].[StudentMarks] (
    [Id] int  NOT NULL,
    [SubjectId] int  NOT NULL,
    [Mark] float  NULL,
    [Student_Id] int  NOT NULL
);
GO

-- Creating table 'Students'
CREATE TABLE [dbo].[Students] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [FirstName] varchar(50)  NOT NULL,
    [LastName] varchar(50)  NOT NULL,
    [Age] int  NOT NULL,
    [TeacherId] int  NOT NULL
);
GO

-- Creating table 'Subjects'
CREATE TABLE [dbo].[Subjects] (
    [Id] int  NOT NULL,
    [Name] varchar(50)  NOT NULL,
    [TaskId] int  NOT NULL,
    [Teachers_Id] int  NOT NULL,
    [StudentMark_Id] int  NOT NULL
);
GO

-- Creating table 'Tasks'
CREATE TABLE [dbo].[Tasks] (
    [Id] int  NOT NULL,
    [DateGiven] datetime  NOT NULL,
    [SubmissionDate] datetime  NULL,
    [TaskName] varchar(50)  NOT NULL,
    [Description] varchar(max)  NOT NULL,
    [MarkGiven] int  NULL,
    [Subject_Id] int  NOT NULL
);
GO

-- Creating table 'Teachers'
CREATE TABLE [dbo].[Teachers] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] varchar(50)  NOT NULL,
    [UserName] varchar(50)  NOT NULL,
    [SubjectId] nvarchar(max)  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'StudentMarks'
ALTER TABLE [dbo].[StudentMarks]
ADD CONSTRAINT [PK_StudentMarks]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Students'
ALTER TABLE [dbo].[Students]
ADD CONSTRAINT [PK_Students]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Subjects'
ALTER TABLE [dbo].[Subjects]
ADD CONSTRAINT [PK_Subjects]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Tasks'
ALTER TABLE [dbo].[Tasks]
ADD CONSTRAINT [PK_Tasks]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Teachers'
ALTER TABLE [dbo].[Teachers]
ADD CONSTRAINT [PK_Teachers]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [Teachers_Id] in table 'Subjects'
ALTER TABLE [dbo].[Subjects]
ADD CONSTRAINT [FK_SubjectTeacher]
    FOREIGN KEY ([Teachers_Id])
    REFERENCES [dbo].[Teachers]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_SubjectTeacher'
CREATE INDEX [IX_FK_SubjectTeacher]
ON [dbo].[Subjects]
    ([Teachers_Id]);
GO

-- Creating foreign key on [Subject_Id] in table 'Tasks'
ALTER TABLE [dbo].[Tasks]
ADD CONSTRAINT [FK_SubjectTask]
    FOREIGN KEY ([Subject_Id])
    REFERENCES [dbo].[Subjects]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_SubjectTask'
CREATE INDEX [IX_FK_SubjectTask]
ON [dbo].[Tasks]
    ([Subject_Id]);
GO

-- Creating foreign key on [Student_Id] in table 'StudentMarks'
ALTER TABLE [dbo].[StudentMarks]
ADD CONSTRAINT [FK_StudentStudentMark]
    FOREIGN KEY ([Student_Id])
    REFERENCES [dbo].[Students]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_StudentStudentMark'
CREATE INDEX [IX_FK_StudentStudentMark]
ON [dbo].[StudentMarks]
    ([Student_Id]);
GO

-- Creating foreign key on [StudentMark_Id] in table 'Subjects'
ALTER TABLE [dbo].[Subjects]
ADD CONSTRAINT [FK_StudentMarkSubject]
    FOREIGN KEY ([StudentMark_Id])
    REFERENCES [dbo].[StudentMarks]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_StudentMarkSubject'
CREATE INDEX [IX_FK_StudentMarkSubject]
ON [dbo].[Subjects]
    ([StudentMark_Id]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------