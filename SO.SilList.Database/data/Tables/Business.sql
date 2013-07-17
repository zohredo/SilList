﻿CREATE TABLE [data].[Business] (
    [businessId] UNIQUEIDENTIFIER CONSTRAINT [DF_Business_businessId] DEFAULT (newid()) NOT NULL,
    [name]       NVARCHAR (250)   NULL,
    [address]    NVARCHAR (250)   NULL,
    [city]       NVARCHAR (250)   NULL,
    [state]      NVARCHAR (50)    NULL,
    [zip]        INT              NULL,
    [createdBy]  INT              NULL,
    [modifiedBy] INT              NULL,
    [modified]   DATETIME         CONSTRAINT [DF_Business_modified] DEFAULT (getdate()) NOT NULL,
    [created]    DATETIME         CONSTRAINT [DF_Business_created] DEFAULT (getdate()) NOT NULL,
    [isActive]   BIT              CONSTRAINT [DF_Business_isActive] DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK_Business] PRIMARY KEY CLUSTERED ([businessId] ASC)
);
