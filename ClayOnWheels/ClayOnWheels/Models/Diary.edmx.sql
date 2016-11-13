
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 11/13/2016 22:05:43
-- Generated from EDMX file: C:\Users\bramb\Source\Repos\ClayOnWheels\ClayOnWheels\ClayOnWheels\Models\Diary.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [FullCalendarMVC_Demo];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------


-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[AppointmentDiary]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AppointmentDiary];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'AppointmentDiary'
CREATE TABLE [dbo].[AppointmentDiary] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [Title] nvarchar(100)  NOT NULL,
    [SomeImportantKey] int  NOT NULL,
    [DateTimeScheduled] datetime  NOT NULL,
    [AppointmentLength] int  NOT NULL,
    [StatusENUM] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [ID] in table 'AppointmentDiary'
ALTER TABLE [dbo].[AppointmentDiary]
ADD CONSTRAINT [PK_AppointmentDiary]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------