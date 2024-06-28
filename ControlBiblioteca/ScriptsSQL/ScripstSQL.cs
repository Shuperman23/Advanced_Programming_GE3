/*
 --PARA LAS TABLAS DE LA BD LUEGO DE GENERAR EL SCAFFOLDING, CORRER EL COMANDO update database EN LA CONSOLA PARA QUE EL MIGRATION GENERE LAS TABLAS.

--CREA SP PARA REALIZAR INSERT DE UN VALOR EN LA TABLA PARAMETRO, ENCRIPTANDO EL DATO QUE SE INGRESE EN COLUMNA VALOR
USE BIBLIOTECA
GO
CREATE PROCEDURE DBO.sp_InsertarValorParametro
    @Nombre NVARCHAR(100),
    @Valor NVARCHAR(100)
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @HashValor NVARCHAR(256) = CONVERT(VARCHAR(256), HASHBYTES('SHA2_256', @Valor), 2);

    INSERT INTO BIBLIOTECA..PARAMETRO (Nombre, Valor)
    VALUES (@Nombre, @HashValor);

    SELECT SCOPE_IDENTITY() AS NuevoParametroId;
END
GO
--------------------------------------------------------------------------------------------------------------


--INSERTA UN VALOR EN LA TABLA PARAMETRO PARA TOMAR COMO CONTRASEÑA PARA EL APIKEY
DECLARE @NuevoParametroId INT;
EXEC @NuevoParametroId = sp_InsertarValorParametro
    @Nombre = 'ApiKey',
    @Valor = 'Hola123';

------------------------------------------------------------------------

--ESTE ES UN SP DE CONTROL, ES SOLO PARA VALIDAR QUE LA ENCRIPTACION ESTA CORRECTA
CREATE PROCEDURE DBO.sp_VerificaValorParametro
    @Id INT,
    @ValorIngresado NVARCHAR(100)
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @HashAlmacenado NVARCHAR(256);
    DECLARE @HashIngresado NVARCHAR(256);

    SELECT @HashAlmacenado = Valor
    FROM BIBLIOTECA..PARAMETRO
    WHERE ParametroID = @Id;

    SET @HashIngresado = CONVERT(VARCHAR(256), HASHBYTES('SHA2_256', @ValorIngresado), 2);

    IF @HashIngresado = @HashAlmacenado
        SELECT 'Valor correcto' AS Resultado;
    ELSE
        SELECT 'Valor incorrecto' AS Resultado;
END
GO



--------------------------------------------------------------------------
--EJECUTA LA VALIDACION DE LA CONTRASEÑA
EXEC sp_VerificaValorParametro
    @Id = 1,
    @ValorIngresado = 'Hola123';

--NOTA: RECUERDEN GUARDAR EN EL POSTMAN LA CONTRASEÑA ENCRIPTADA EN LA VALIDACION DEL APIKEY, SINO SE CAE LA CONEXION, UN LINDO 402

--------------------------------------------------------------------------

--*********************************AUTOR**********************************************************--

--CREACION SP POSTAUTORES, INSERT REALMENTE, TOMEN EN CUENTA QUE YO NO APLICO USE-GO, QUEMO LA BASE DE DATOS EN CADA CONEXION, LINKSERVER SE LE LLAMA
CREATE PROCEDURE [dbo].[spNewAutor]
    @Descripcion VARCHAR(256),
    @Estado VARCHAR(2)
AS
BEGIN

    SET NOCOUNT ON;
    SET XACT_ABORT ON; -- Esto asegura que la transacción se aborte automáticamente en caso de error//COMENTARIO DEL PROFE, PERO EL COMANDO ES EXCELENTE HACE QUE EL MIDDLEWARE TRABAJE EN LO QUE DEBE

    DECLARE @Resultado INT;

    BEGIN TRY
        BEGIN TRANSACTION;

        INSERT INTO BIBLIOTECA.[dbo].[AUTOR] (Descripcion, Estado, FechaCreacion)
        VALUES (@Descripcion, @Estado, GETDATE());

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

CREATE PROCEDURE [dbo].[spUpdateAutor]
    @AutorId INT,
    @Descripcion VARCHAR(256),
    @Estado VARCHAR(2)
AS
BEGIN

    SET NOCOUNT ON;
    SET XACT_ABORT ON;

    DECLARE @Resultado INT;

    BEGIN TRY
        BEGIN TRANSACTION;

        UPDATE BIBLIOTECA.[dbo].[AUTOR]
        SET Descripcion = @Descripcion, Estado = @Estado, FechaCreacion = GETDATE()
        WHERE AutorId = @AutorId;

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

--*********************************GENERO LITERARIO**********************************************************--

--CREA SP PARA POST DE GENEROLITERARIO
CREATE PROCEDURE [dbo].[spNewGeneroLiterario]
    @Descripcion VARCHAR(256),
    @Estado VARCHAR(2)
AS
BEGIN

    SET NOCOUNT ON;
    SET XACT_ABORT ON;

    DECLARE @Resultado INT;

    BEGIN TRY
        BEGIN TRANSACTION;

        INSERT INTO BIBLIOTECA.[dbo].[GENERO_LITERARIO] (Descripcion, Estado, FechaCreacion)
        VALUES (@Descripcion, @Estado, GETDATE());

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

    SELECT @Resultado AS Resultado;
END

-----------------------------------------------------------

--CREA SP PARA PUT DE GENEROLITERARIO
CREATE PROCEDURE [dbo].[spUpdateGeneroLiterario]
    @GeneroLiterarioId INT,
    @Descripcion VARCHAR(256),
    @Estado VARCHAR(2)
AS
BEGIN

    SET NOCOUNT ON;
    SET XACT_ABORT ON;

    DECLARE @Resultado INT;

    BEGIN TRY
        BEGIN TRANSACTION;

        UPDATE BIBLIOTECA.[dbo].GENERO_LITERARIO
        SET Descripcion = @Descripcion, Estado = @Estado, FechaCreacion = GETDATE()
        WHERE GeneroLiterarioID = @GeneroLiterarioId;

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


--*********************************LIBRO**********************************************************--

--CREA SP PARA POST DE LIBRO
CREATE PROCEDURE [dbo].[spNewLibro]
	@AutorId int,
	@GeneroLiterario int,
    @NombreLibro VARCHAR(256),
    @Estado VARCHAR(2)
AS
BEGIN

    SET NOCOUNT ON;
    SET XACT_ABORT ON;

    DECLARE @Resultado INT;

    BEGIN TRY
        BEGIN TRANSACTION;

        INSERT INTO BIBLIOTECA.[dbo].LIBRO (GeneroLiterarioID, AutorID, NombreLibro, Estado, FechaCreacion)
        VALUES (@GeneroLiterario, @AutorId, @NombreLibro, @Estado, GETDATE());

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

    SELECT @Resultado AS Resultado;
END

-------------------------------------------------------------------------------

--CREA SP PARA PUT DE LIBRO
CREATE PROCEDURE [dbo].[spUpdateLibro]
	@LibroId int,
	@GeneroLiterario int,
	@AutorId int,
    @NombreLibro VARCHAR(256),
    @Estado VARCHAR(2)
AS
BEGIN

    SET NOCOUNT ON;
    SET XACT_ABORT ON;

    DECLARE @Resultado INT;

    BEGIN TRY
        BEGIN TRANSACTION;

        UPDATE BIBLIOTECA.[dbo].LIBRO
        SET GeneroLiterarioID = @GeneroLiterario, AutorID = @AutorId, NombreLibro = @NombreLibro, Estado = @Estado, FechaCreacion = GETDATE()
        WHERE LibroID = @LibroId;

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

    SELECT @Resultado AS Resultado;
END
 
 
 
 */
