CREATE TABLE [dbo].[inventario] (
    [id]         INT           NOT NULL,
    [producto]   NVARCHAR (50) NULL,
    [existencia] INT           NULL,
    [precio]     FLOAT (53)    NULL,
    [proveedor]  NVARCHAR (50) NULL,
    PRIMARY KEY CLUSTERED ([id] ASC) WITH (FILLFACTOR = 100)
);

