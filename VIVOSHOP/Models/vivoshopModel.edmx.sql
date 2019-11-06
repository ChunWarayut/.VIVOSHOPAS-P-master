
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 10/08/2019 21:21:46
-- Generated from EDMX file: D:\aaa\VIVOSHOPASP-master190862\VIVOSHOP\Models\vivoshopModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [vivoshopDB];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_OrderDetails_ToTable]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[OrderDetails] DROP CONSTRAINT [FK_OrderDetails_ToTable];
GO
IF OBJECT_ID(N'[dbo].[FK_OrderDetails_ToTable_1]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[OrderDetails] DROP CONSTRAINT [FK_OrderDetails_ToTable_1];
GO
IF OBJECT_ID(N'[dbo].[FK_Product_ToTable]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Product] DROP CONSTRAINT [FK_Product_ToTable];
GO
IF OBJECT_ID(N'[dbo].[FK_ProductOrder_ToTable]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ProductOrder] DROP CONSTRAINT [FK_ProductOrder_ToTable];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Bank]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Bank];
GO
IF OBJECT_ID(N'[dbo].[OrderDetails]', 'U') IS NOT NULL
    DROP TABLE [dbo].[OrderDetails];
GO
IF OBJECT_ID(N'[dbo].[Product]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Product];
GO
IF OBJECT_ID(N'[dbo].[ProductOrder]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ProductOrder];
GO
IF OBJECT_ID(N'[dbo].[ProductType]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ProductType];
GO
IF OBJECT_ID(N'[dbo].[UserAccout]', 'U') IS NOT NULL
    DROP TABLE [dbo].[UserAccout];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Banks'
CREATE TABLE [dbo].[Banks] (
    [Bank_Number] nvarchar(10)  NOT NULL,
    [Bank_Name] nvarchar(200)  NOT NULL,
    [Bank_User] nvarchar(200)  NOT NULL
);
GO

-- Creating table 'OrderDetails'
CREATE TABLE [dbo].[OrderDetails] (
    [Order_Id] int IDENTITY(1,1) NOT NULL,
    [Pro_Id] int  NOT NULL,
    [OrderDetails_Number] int  NOT NULL,
    [Pro_Price] decimal(7,2)  NOT NULL,
    [ProOrderId] int  NOT NULL,
    [User_Id] int  NULL
);
GO

-- Creating table 'Products'
CREATE TABLE [dbo].[Products] (
    [Pro_Id] int IDENTITY(1,1) NOT NULL,
    [ProType_Id] int  NOT NULL,
    [Pro_Name] nvarchar(50)  NOT NULL,
    [Pro_Details] nvarchar(max)  NOT NULL,
    [Pro_Price] decimal(7,2)  NOT NULL,
    [Pro_Color] nvarchar(max)  NOT NULL,
    [Pro_Picture] nvarchar(max)  NULL,
    [Pro_Amout] int  NULL
);
GO

-- Creating table 'ProductOrders'
CREATE TABLE [dbo].[ProductOrders] (
    [Order_Id] int IDENTITY(1,1) NOT NULL,
    [User_Id] int  NOT NULL,
    [Order_Date] datetime  NOT NULL,
    [Order_Price] decimal(7,2)  NOT NULL,
    [Order_Status] nvarchar(50)  NOT NULL,
    [Order_Parcel] nvarchar(13)  NULL,
    [Order_img] nvarchar(max)  NULL
);
GO

-- Creating table 'ProductTypes'
CREATE TABLE [dbo].[ProductTypes] (
    [ProType_Id] int IDENTITY(1,1) NOT NULL,
    [ProType_Name] nvarchar(50)  NOT NULL
);
GO

-- Creating table 'UserAccouts'
CREATE TABLE [dbo].[UserAccouts] (
    [User_Id] int IDENTITY(1,1) NOT NULL,
    [User_Name] nvarchar(20)  NOT NULL,
    [User_Lastname] nvarchar(20)  NOT NULL,
    [User_Sex] nvarchar(5)  NOT NULL,
    [User_Tel] nvarchar(10)  NOT NULL,
    [User_Email] nvarchar(50)  NOT NULL,
    [User_Address] nvarchar(200)  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Bank_Number] in table 'Banks'
ALTER TABLE [dbo].[Banks]
ADD CONSTRAINT [PK_Banks]
    PRIMARY KEY CLUSTERED ([Bank_Number] ASC);
GO

-- Creating primary key on [Order_Id] in table 'OrderDetails'
ALTER TABLE [dbo].[OrderDetails]
ADD CONSTRAINT [PK_OrderDetails]
    PRIMARY KEY CLUSTERED ([Order_Id] ASC);
GO

-- Creating primary key on [Pro_Id] in table 'Products'
ALTER TABLE [dbo].[Products]
ADD CONSTRAINT [PK_Products]
    PRIMARY KEY CLUSTERED ([Pro_Id] ASC);
GO

-- Creating primary key on [Order_Id] in table 'ProductOrders'
ALTER TABLE [dbo].[ProductOrders]
ADD CONSTRAINT [PK_ProductOrders]
    PRIMARY KEY CLUSTERED ([Order_Id] ASC);
GO

-- Creating primary key on [ProType_Id] in table 'ProductTypes'
ALTER TABLE [dbo].[ProductTypes]
ADD CONSTRAINT [PK_ProductTypes]
    PRIMARY KEY CLUSTERED ([ProType_Id] ASC);
GO

-- Creating primary key on [User_Id] in table 'UserAccouts'
ALTER TABLE [dbo].[UserAccouts]
ADD CONSTRAINT [PK_UserAccouts]
    PRIMARY KEY CLUSTERED ([User_Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [Pro_Id] in table 'OrderDetails'
ALTER TABLE [dbo].[OrderDetails]
ADD CONSTRAINT [FK_OrderDetails_ToTable]
    FOREIGN KEY ([Pro_Id])
    REFERENCES [dbo].[Products]
        ([Pro_Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_OrderDetails_ToTable'
CREATE INDEX [IX_FK_OrderDetails_ToTable]
ON [dbo].[OrderDetails]
    ([Pro_Id]);
GO

-- Creating foreign key on [ProType_Id] in table 'Products'
ALTER TABLE [dbo].[Products]
ADD CONSTRAINT [FK_Product_ToTable]
    FOREIGN KEY ([ProType_Id])
    REFERENCES [dbo].[ProductTypes]
        ([ProType_Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Product_ToTable'
CREATE INDEX [IX_FK_Product_ToTable]
ON [dbo].[Products]
    ([ProType_Id]);
GO

-- Creating foreign key on [User_Id] in table 'ProductOrders'
ALTER TABLE [dbo].[ProductOrders]
ADD CONSTRAINT [FK_ProductOrder_ToTable]
    FOREIGN KEY ([User_Id])
    REFERENCES [dbo].[UserAccouts]
        ([User_Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ProductOrder_ToTable'
CREATE INDEX [IX_FK_ProductOrder_ToTable]
ON [dbo].[ProductOrders]
    ([User_Id]);
GO

-- Creating foreign key on [User_Id] in table 'OrderDetails'
ALTER TABLE [dbo].[OrderDetails]
ADD CONSTRAINT [FK_OrderDetails_ToTable_1]
    FOREIGN KEY ([User_Id])
    REFERENCES [dbo].[UserAccouts]
        ([User_Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_OrderDetails_ToTable_1'
CREATE INDEX [IX_FK_OrderDetails_ToTable_1]
ON [dbo].[OrderDetails]
    ([User_Id]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------