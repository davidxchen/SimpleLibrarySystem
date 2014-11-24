
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 11/24/2014 22:36:38
-- Generated from EDMX file: C:\Users\David\documents\visual studio 2013\Projects\SimpleLibrarySystem\SimpleLibrary.Data\SimpLibSys.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [SimpleLibraryDB];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_LibraryUsersBookRentHistory]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[BookRentHistories] DROP CONSTRAINT [FK_LibraryUsersBookRentHistory];
GO
IF OBJECT_ID(N'[dbo].[FK_BooksBookRentHistory]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[BookRentHistories] DROP CONSTRAINT [FK_BooksBookRentHistory];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[LibraryUsers]', 'U') IS NOT NULL
    DROP TABLE [dbo].[LibraryUsers];
GO
IF OBJECT_ID(N'[dbo].[Books]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Books];
GO
IF OBJECT_ID(N'[dbo].[BookRentHistories]', 'U') IS NOT NULL
    DROP TABLE [dbo].[BookRentHistories];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'LibraryUsers'
CREATE TABLE [dbo].[LibraryUsers] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [UserName] nvarchar(max)  NOT NULL,
    [UserRole] nvarchar(max)  NOT NULL,
    [Status] smallint  NOT NULL
);
GO

-- Creating table 'Books'
CREATE TABLE [dbo].[Books] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [ISBN] nvarchar(max)  NOT NULL,
    [BookName] nvarchar(max)  NOT NULL,
    [Cover] tinyint  NOT NULL,
    [Description] nvarchar(max)  NOT NULL,
    [CreatedOn] datetime  NOT NULL,
    [CreatedBy] int  NOT NULL,
    [ModifiedOn] datetime  NOT NULL,
    [ModifiedBy] int  NOT NULL,
    [Status] smallint  NOT NULL,
    [LastRentOn] datetime  NOT NULL
);
GO

-- Creating table 'BookRentHistories'
CREATE TABLE [dbo].[BookRentHistories] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [ISBN] nvarchar(max)  NOT NULL,
    [UserId] int  NOT NULL,
    [RentOn] datetime  NOT NULL,
    [ReturnedOn] datetime  NOT NULL,
    [LibraryUser_Id] int  NOT NULL,
    [Book_Id] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'LibraryUsers'
ALTER TABLE [dbo].[LibraryUsers]
ADD CONSTRAINT [PK_LibraryUsers]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Books'
ALTER TABLE [dbo].[Books]
ADD CONSTRAINT [PK_Books]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'BookRentHistories'
ALTER TABLE [dbo].[BookRentHistories]
ADD CONSTRAINT [PK_BookRentHistories]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [LibraryUser_Id] in table 'BookRentHistories'
ALTER TABLE [dbo].[BookRentHistories]
ADD CONSTRAINT [FK_LibraryUsersBookRentHistory]
    FOREIGN KEY ([LibraryUser_Id])
    REFERENCES [dbo].[LibraryUsers]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_LibraryUsersBookRentHistory'
CREATE INDEX [IX_FK_LibraryUsersBookRentHistory]
ON [dbo].[BookRentHistories]
    ([LibraryUser_Id]);
GO

-- Creating foreign key on [Book_Id] in table 'BookRentHistories'
ALTER TABLE [dbo].[BookRentHistories]
ADD CONSTRAINT [FK_BooksBookRentHistory]
    FOREIGN KEY ([Book_Id])
    REFERENCES [dbo].[Books]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_BooksBookRentHistory'
CREATE INDEX [IX_FK_BooksBookRentHistory]
ON [dbo].[BookRentHistories]
    ([Book_Id]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------