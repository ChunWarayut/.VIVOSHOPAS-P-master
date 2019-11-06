DROP TABLE [dbo].[Banks]
DROP TABLE [dbo].[ProductTypes] 
DROP TABLE [dbo].[Products] 
DROP TABLE [dbo].[UserAccouts]
DROP TABLE [dbo].[OrderDetails] 
DROP TABLE [dbo].[ProductOrders]

CREATE TABLE [dbo].[Banks] (
    [Bank_Number] NVARCHAR (10)  NOT NULL,
    [Bank_Name]   NVARCHAR (200) NOT NULL,
    [Bank_User]   NVARCHAR (200) NOT NULL,
    CONSTRAINT [PK_Banks] PRIMARY KEY CLUSTERED ([Bank_Number] ASC)
);

CREATE TABLE [dbo].[ProductTypes] (
    [ProType_Id]   INT           IDENTITY (40001, 1) NOT NULL,
    [ProType_Name] NVARCHAR (50) NOT NULL,
    CONSTRAINT [PK_ProductTypes] PRIMARY KEY CLUSTERED ([ProType_Id] ASC)
);
CREATE TABLE [dbo].[Products] (
    [Pro_Id]      INT            IDENTITY (30001, 1) NOT NULL,
    [ProType_Id]  INT            NOT NULL,
    [Pro_Name]    NVARCHAR (50)  NOT NULL,
    [Pro_Details] NVARCHAR (MAX) NOT NULL,
    [Pro_Price]   DECIMAL (7, 2) NOT NULL,
    [Pro_Color]   NVARCHAR (MAX) NOT NULL,
    [Pro_Picture] NVARCHAR (MAX) NULL,
    [Pro_Amout]   INT            NULL,
    CONSTRAINT [PK_Products] PRIMARY KEY CLUSTERED ([Pro_Id] ASC),
    CONSTRAINT [FK_Product_ToTable] FOREIGN KEY ([ProType_Id]) REFERENCES [dbo].[ProductTypes] ([ProType_Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_FK_Product_ToTable]
    ON [dbo].[Products]([ProType_Id] ASC);


CREATE TABLE [dbo].[UserAccouts] (
    [User_Id]       INT            IDENTITY (50001, 1) NOT NULL,
    [User_Name]     NVARCHAR (20)  NOT NULL,
    [User_Lastname] NVARCHAR (20)  NOT NULL,
    [User_Sex]      NVARCHAR (5)   NOT NULL,
    [User_Tel]      NVARCHAR (10)  NOT NULL,
    [User_Email]    NVARCHAR (50)  NOT NULL,
    [User_Address]  NVARCHAR (200) NOT NULL,
    CONSTRAINT [PK_UserAccouts] PRIMARY KEY CLUSTERED ([User_Id] ASC)
);





CREATE TABLE [dbo].[OrderDetails] (
    [Order_Id]            INT            IDENTITY (10001, 1) NOT NULL,
    [Pro_Id]              INT            NOT NULL,
    [OrderDetails_Number] INT            NOT NULL,
    [Pro_Price]           DECIMAL (7, 2) NOT NULL,
    [ProOrderId]          INT            NOT NULL,
    [User_Id]             INT            NULL,
    CONSTRAINT [PK_OrderDetails] PRIMARY KEY CLUSTERED ([Order_Id] ASC),
    CONSTRAINT [FK_OrderDetails_ToTable_1] FOREIGN KEY ([User_Id]) REFERENCES [dbo].[UserAccouts] ([User_Id]),
    CONSTRAINT [FK_OrderDetails_ToTable] FOREIGN KEY ([Pro_Id]) REFERENCES [dbo].[Products] ([Pro_Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_FK_OrderDetails_ToTable]
    ON [dbo].[OrderDetails]([Pro_Id] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_FK_OrderDetails_ToTable_1]
    ON [dbo].[OrderDetails]([User_Id] ASC);



CREATE TABLE [dbo].[ProductOrders] (
    [Order_Id]      INT            IDENTITY (20001, 1) NOT NULL,
    [User_Id]       INT            NOT NULL,
    [Order_Date]    DATETIME       NOT NULL,
    [Order_Price]   DECIMAL (7, 2) NOT NULL,
    [Order_Status]  NVARCHAR (50)  NOT NULL,
    [Order_Parcel]  NVARCHAR (13)  NULL,
    [Order_img]     NVARCHAR (MAX) NULL,
    [Order_Address] NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_ProductOrders] PRIMARY KEY CLUSTERED ([Order_Id] ASC),
    CONSTRAINT [FK_ProductOrder_ToTable] FOREIGN KEY ([User_Id]) REFERENCES [dbo].[UserAccouts] ([User_Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_FK_ProductOrder_ToTable]
    ON [dbo].[ProductOrders]([User_Id] ASC);

