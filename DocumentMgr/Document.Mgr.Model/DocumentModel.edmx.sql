
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 03/12/2017 16:45:22
-- Generated from EDMX file: F:\ALLbishe\DocumentMgr\Document.Mgr.Model\DocumentModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [DocumentMgr];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_TechTypeTech]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Techs] DROP CONSTRAINT [FK_TechTypeTech];
GO
IF OBJECT_ID(N'[dbo].[FK_UserInfoR_UserInfo_Tech]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[R_UserInfo_Techs] DROP CONSTRAINT [FK_UserInfoR_UserInfo_Tech];
GO
IF OBJECT_ID(N'[dbo].[FK_TechR_UserInfo_Tech]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[R_UserInfo_Techs] DROP CONSTRAINT [FK_TechR_UserInfo_Tech];
GO
IF OBJECT_ID(N'[dbo].[FK_CountryCity]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Citys] DROP CONSTRAINT [FK_CountryCity];
GO
IF OBJECT_ID(N'[dbo].[FK_CityGroup]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Groups] DROP CONSTRAINT [FK_CityGroup];
GO
IF OBJECT_ID(N'[dbo].[FK_UserInfoRole]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[UserInfos] DROP CONSTRAINT [FK_UserInfoRole];
GO
IF OBJECT_ID(N'[dbo].[FK_UserInfoPosition]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[UserInfos] DROP CONSTRAINT [FK_UserInfoPosition];
GO
IF OBJECT_ID(N'[dbo].[FK_UserInfoContact]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Contacts] DROP CONSTRAINT [FK_UserInfoContact];
GO
IF OBJECT_ID(N'[dbo].[FK_UserInfoGroup]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[UserInfos] DROP CONSTRAINT [FK_UserInfoGroup];
GO
IF OBJECT_ID(N'[dbo].[FK_R_UserInfo_TechLevelDescription]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[R_UserInfo_Techs] DROP CONSTRAINT [FK_R_UserInfo_TechLevelDescription];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[UserInfos]', 'U') IS NOT NULL
    DROP TABLE [dbo].[UserInfos];
GO
IF OBJECT_ID(N'[dbo].[Positions]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Positions];
GO
IF OBJECT_ID(N'[dbo].[Contacts]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Contacts];
GO
IF OBJECT_ID(N'[dbo].[Roles]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Roles];
GO
IF OBJECT_ID(N'[dbo].[TechTypes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TechTypes];
GO
IF OBJECT_ID(N'[dbo].[Techs]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Techs];
GO
IF OBJECT_ID(N'[dbo].[LevelDescriptions]', 'U') IS NOT NULL
    DROP TABLE [dbo].[LevelDescriptions];
GO
IF OBJECT_ID(N'[dbo].[R_UserInfo_Techs]', 'U') IS NOT NULL
    DROP TABLE [dbo].[R_UserInfo_Techs];
GO
IF OBJECT_ID(N'[dbo].[Countrys]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Countrys];
GO
IF OBJECT_ID(N'[dbo].[Citys]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Citys];
GO
IF OBJECT_ID(N'[dbo].[Groups]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Groups];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'UserInfos'
CREATE TABLE [dbo].[UserInfos] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(32)  NOT NULL,
    [Gender] smallint  NOT NULL,
    [Pwd] nvarchar(128)  NOT NULL,
    [Remark] nvarchar(max)  NULL,
    [HiredTime] datetime  NOT NULL,
    [IsAssigned] bit  NOT NULL,
    [Picture] nvarchar(max)  NULL,
    [Email] nvarchar(max)  NOT NULL,
    [UID] nvarchar(max)  NOT NULL,
    [IsAdmin] bit  NOT NULL,
    [IsDeleted] bit  NOT NULL,
    [IsEnabled] bit  NOT NULL,
    [RoleId] int  NOT NULL,
    [PositionId] int  NOT NULL,
    [GroupId] int  NOT NULL,
    [Auth] smallint  NOT NULL
);
GO

-- Creating table 'Positions'
CREATE TABLE [dbo].[Positions] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Description] nvarchar(max)  NULL
);
GO

-- Creating table 'Contacts'
CREATE TABLE [dbo].[Contacts] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [PersonalEmail] nvarchar(max)  NULL,
    [PhoneNumber] nvarchar(max)  NULL,
    [BlogUrl] nvarchar(max)  NULL,
    [WX] nvarchar(max)  NULL,
    [UserInfo_Id] int  NOT NULL
);
GO

-- Creating table 'Roles'
CREATE TABLE [dbo].[Roles] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(64)  NOT NULL,
    [Description] nvarchar(max)  NULL
);
GO

-- Creating table 'TechTypes'
CREATE TABLE [dbo].[TechTypes] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [TypeName] nvarchar(max)  NOT NULL,
    [Description] nvarchar(max)  NULL
);
GO

-- Creating table 'Techs'
CREATE TABLE [dbo].[Techs] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [TechTypeId] int  NOT NULL,
    [TechName] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'LevelDescriptions'
CREATE TABLE [dbo].[LevelDescriptions] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Level] int  NOT NULL,
    [Description] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'R_UserInfo_Techs'
CREATE TABLE [dbo].[R_UserInfo_Techs] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [UserInfoId] int  NOT NULL,
    [TechId] int  NOT NULL,
    [LevelDescriptionId] int  NOT NULL
);
GO

-- Creating table 'Countrys'
CREATE TABLE [dbo].[Countrys] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Citys'
CREATE TABLE [dbo].[Citys] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [CountryId] int  NOT NULL
);
GO

-- Creating table 'Groups'
CREATE TABLE [dbo].[Groups] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Description] nvarchar(max)  NOT NULL,
    [CityId] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'UserInfos'
ALTER TABLE [dbo].[UserInfos]
ADD CONSTRAINT [PK_UserInfos]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Positions'
ALTER TABLE [dbo].[Positions]
ADD CONSTRAINT [PK_Positions]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Contacts'
ALTER TABLE [dbo].[Contacts]
ADD CONSTRAINT [PK_Contacts]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Roles'
ALTER TABLE [dbo].[Roles]
ADD CONSTRAINT [PK_Roles]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'TechTypes'
ALTER TABLE [dbo].[TechTypes]
ADD CONSTRAINT [PK_TechTypes]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Techs'
ALTER TABLE [dbo].[Techs]
ADD CONSTRAINT [PK_Techs]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'LevelDescriptions'
ALTER TABLE [dbo].[LevelDescriptions]
ADD CONSTRAINT [PK_LevelDescriptions]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'R_UserInfo_Techs'
ALTER TABLE [dbo].[R_UserInfo_Techs]
ADD CONSTRAINT [PK_R_UserInfo_Techs]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Countrys'
ALTER TABLE [dbo].[Countrys]
ADD CONSTRAINT [PK_Countrys]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Citys'
ALTER TABLE [dbo].[Citys]
ADD CONSTRAINT [PK_Citys]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Groups'
ALTER TABLE [dbo].[Groups]
ADD CONSTRAINT [PK_Groups]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [TechTypeId] in table 'Techs'
ALTER TABLE [dbo].[Techs]
ADD CONSTRAINT [FK_TechTypeTech]
    FOREIGN KEY ([TechTypeId])
    REFERENCES [dbo].[TechTypes]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_TechTypeTech'
CREATE INDEX [IX_FK_TechTypeTech]
ON [dbo].[Techs]
    ([TechTypeId]);
GO

-- Creating foreign key on [UserInfoId] in table 'R_UserInfo_Techs'
ALTER TABLE [dbo].[R_UserInfo_Techs]
ADD CONSTRAINT [FK_UserInfoR_UserInfo_Tech]
    FOREIGN KEY ([UserInfoId])
    REFERENCES [dbo].[UserInfos]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_UserInfoR_UserInfo_Tech'
CREATE INDEX [IX_FK_UserInfoR_UserInfo_Tech]
ON [dbo].[R_UserInfo_Techs]
    ([UserInfoId]);
GO

-- Creating foreign key on [TechId] in table 'R_UserInfo_Techs'
ALTER TABLE [dbo].[R_UserInfo_Techs]
ADD CONSTRAINT [FK_TechR_UserInfo_Tech]
    FOREIGN KEY ([TechId])
    REFERENCES [dbo].[Techs]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_TechR_UserInfo_Tech'
CREATE INDEX [IX_FK_TechR_UserInfo_Tech]
ON [dbo].[R_UserInfo_Techs]
    ([TechId]);
GO

-- Creating foreign key on [CountryId] in table 'Citys'
ALTER TABLE [dbo].[Citys]
ADD CONSTRAINT [FK_CountryCity]
    FOREIGN KEY ([CountryId])
    REFERENCES [dbo].[Countrys]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_CountryCity'
CREATE INDEX [IX_FK_CountryCity]
ON [dbo].[Citys]
    ([CountryId]);
GO

-- Creating foreign key on [CityId] in table 'Groups'
ALTER TABLE [dbo].[Groups]
ADD CONSTRAINT [FK_CityGroup]
    FOREIGN KEY ([CityId])
    REFERENCES [dbo].[Citys]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_CityGroup'
CREATE INDEX [IX_FK_CityGroup]
ON [dbo].[Groups]
    ([CityId]);
GO

-- Creating foreign key on [RoleId] in table 'UserInfos'
ALTER TABLE [dbo].[UserInfos]
ADD CONSTRAINT [FK_UserInfoRole]
    FOREIGN KEY ([RoleId])
    REFERENCES [dbo].[Roles]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_UserInfoRole'
CREATE INDEX [IX_FK_UserInfoRole]
ON [dbo].[UserInfos]
    ([RoleId]);
GO

-- Creating foreign key on [PositionId] in table 'UserInfos'
ALTER TABLE [dbo].[UserInfos]
ADD CONSTRAINT [FK_UserInfoPosition]
    FOREIGN KEY ([PositionId])
    REFERENCES [dbo].[Positions]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_UserInfoPosition'
CREATE INDEX [IX_FK_UserInfoPosition]
ON [dbo].[UserInfos]
    ([PositionId]);
GO

-- Creating foreign key on [UserInfo_Id] in table 'Contacts'
ALTER TABLE [dbo].[Contacts]
ADD CONSTRAINT [FK_UserInfoContact]
    FOREIGN KEY ([UserInfo_Id])
    REFERENCES [dbo].[UserInfos]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_UserInfoContact'
CREATE INDEX [IX_FK_UserInfoContact]
ON [dbo].[Contacts]
    ([UserInfo_Id]);
GO

-- Creating foreign key on [GroupId] in table 'UserInfos'
ALTER TABLE [dbo].[UserInfos]
ADD CONSTRAINT [FK_UserInfoGroup]
    FOREIGN KEY ([GroupId])
    REFERENCES [dbo].[Groups]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_UserInfoGroup'
CREATE INDEX [IX_FK_UserInfoGroup]
ON [dbo].[UserInfos]
    ([GroupId]);
GO

-- Creating foreign key on [LevelDescriptionId] in table 'R_UserInfo_Techs'
ALTER TABLE [dbo].[R_UserInfo_Techs]
ADD CONSTRAINT [FK_R_UserInfo_TechLevelDescription]
    FOREIGN KEY ([LevelDescriptionId])
    REFERENCES [dbo].[LevelDescriptions]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_R_UserInfo_TechLevelDescription'
CREATE INDEX [IX_FK_R_UserInfo_TechLevelDescription]
ON [dbo].[R_UserInfo_Techs]
    ([LevelDescriptionId]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------