/*
USE CONTROLGASTOS
GO

CREATE PROCEDURE [dbo].[spNewInventario]
    @ProductoId int,
    @TipoMovimiento NVARCHAR(10),
	@Cantidad int,
	@Precio Decimal(10,2)

AS
BEGIN

    SET NOCOUNT ON;
    SET XACT_ABORT ON;

    DECLARE @Resultado INT;

    BEGIN TRY
        BEGIN TRANSACTION;

        INSERT INTO CONTROLGASTOS.[dbo].[INVENTARIO] (PRODUCTOID, TIPOMOVIMIENTO, CANTIDAD, PRECIO, FECHAMOVIMIENTO, FECHACADUCIDAD)
        VALUES (@ProductoId, @TipoMovimiento, @Cantidad, @Precio, GETDATE(), DATEADD(DAY,+10,GETDATE()));

        COMMIT TRANSACTION;
        SET @Resultado = 1;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
        BEGIN
            ROLLBACK TRANSACTION;
        END
        SET @Resultado = 0;
    END CATCH

    SELECT @Resultado AS Resultado;
END
------------------------------------------------------------------

--CREA SP PARA EL PUT DE AUTOR, REALMENTE ES UN UPDATE

CREATE PROCEDURE [dbo].[spUpdateInventario]
    @InventarioId int,
	@ProductoId int,
    @TipoMovimiento NVARCHAR(10),
	@Cantidad int,
	@Precio Decimal(10,2)
AS
BEGIN

    SET NOCOUNT ON;
    SET XACT_ABORT ON;

    DECLARE @Resultado INT;

    BEGIN TRY
        BEGIN TRANSACTION;

        UPDATE CONTROLGASTOS.DBO.INVENTARIO
        SET PRODUCTOID = @ProductoId, TIPOMOVIMIENTO = @TipoMovimiento, CANTIDAD = @Cantidad, PRECIO = @Precio, FECHAMOVIMIENTO = GETDATE(), FECHACADUCIDAD = DATEADD(DAY,+10,GETDATE())
        WHERE ID = @InventarioId;

        COMMIT TRANSACTION;
        SET @Resultado = 1;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
        BEGIN
            ROLLBACK TRANSACTION;
        END
        SET @Resultado = 0;
    END CATCH

    SELECT @Resultado AS Resultado;
END

-------------------------------------------------------------------

CREATE PROCEDURE [dbo].[spNewProducto]
    @NOMBRE NVARCHAR(100),
    @PROVEEDORID INT

AS
BEGIN

    SET NOCOUNT ON;
    SET XACT_ABORT ON;

    DECLARE @Resultado INT;

    BEGIN TRY
        BEGIN TRANSACTION;

        INSERT INTO CONTROLGASTOS.[dbo].PRODUCTOS (NOMBRE, PROVEEDORID)
        VALUES (@NOMBRE, @PROVEEDORID);

        COMMIT TRANSACTION;
        SET @Resultado = 1;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
        BEGIN
            ROLLBACK TRANSACTION;
        END
        SET @Resultado = 0;
    END CATCH

    SELECT @Resultado AS Resultado;
END
------------------------------------------------------------------

--CREA SP PARA EL PUT DE AUTOR, REALMENTE ES UN UPDATE

CREATE PROCEDURE [dbo].[spUpdateProducto]
    @PRODUCTOID INT,
	@NOMBRE NVARCHAR(100),
    @PROVEEDORID INT
AS
BEGIN

    SET NOCOUNT ON;
    SET XACT_ABORT ON;

    DECLARE @Resultado INT;

    BEGIN TRY
        BEGIN TRANSACTION;

        UPDATE CONTROLGASTOS.DBO.PRODUCTOS
        SET NOMBRE = @NOMBRE, PROVEEDORID = @PROVEEDORID
        WHERE ID = @PRODUCTOID;

        COMMIT TRANSACTION;
        SET @Resultado = 1;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
        BEGIN
            ROLLBACK TRANSACTION;
        END
        SET @Resultado = 0;
    END CATCH

    SELECT @Resultado AS Resultado;
END


---------------------------------------------------------

CREATE PROCEDURE [dbo].[spNewProveedor]
    @NOMBRE NVARCHAR(100),
    @TELEFONOCONTACTO NVARCHAR(20),
	@EMAILCONTACTO NVARCHAR(100),
	@DIRECCION NVARCHAR(200)

AS
BEGIN

    SET NOCOUNT ON;
    SET XACT_ABORT ON;

    DECLARE @Resultado INT;

    BEGIN TRY
        BEGIN TRANSACTION;

        INSERT INTO CONTROLGASTOS.[dbo].PROVEEDORES (NOMBRE, TELEFONOCONTACTO, EMAILCONTACTO, DIRECCION)
        VALUES (@NOMBRE, @TELEFONOCONTACTO,@EMAILCONTACTO,@DIRECCION);

        COMMIT TRANSACTION;
        SET @Resultado = 1;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
        BEGIN
            ROLLBACK TRANSACTION;
        END
        SET @Resultado = 0;
    END CATCH

    SELECT @Resultado AS Resultado;
END
------------------------------------------------------------------

--CREA SP PARA EL PUT DE AUTOR, REALMENTE ES UN UPDATE

CREATE PROCEDURE [dbo].[spUpdateProveedor]
    @PROVEEDORID INT,
	@NOMBRE NVARCHAR(100),
    @TELEFONOCONTACTO NVARCHAR(20),
	@EMAILCONTACTO NVARCHAR(100),
	@DIRECCION NVARCHAR(200)
AS
BEGIN

    SET NOCOUNT ON;
    SET XACT_ABORT ON;

    DECLARE @Resultado INT;

    BEGIN TRY
        BEGIN TRANSACTION;

        UPDATE CONTROLGASTOS.DBO.PROVEEDORES
        SET NOMBRE = @NOMBRE, TELEFONOCONTACTO = @TELEFONOCONTACTO, EMAILCONTACTO = @EMAILCONTACTO, DIRECCION = @DIRECCION
        WHERE ID = @PROVEEDORID;

        COMMIT TRANSACTION;
        SET @Resultado = 1;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
        BEGIN
            ROLLBACK TRANSACTION;
        END
        SET @Resultado = 0;
    END CATCH

    SELECT @Resultado AS Resultado;
END


 
 
 */
