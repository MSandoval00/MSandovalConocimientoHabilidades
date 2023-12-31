USE [master]
GO
/****** Object:  Database [MSandovalConicimientoHabilidades]    Script Date: 12/6/2023 5:59:49 PM ******/
CREATE DATABASE [MSandovalConicimientoHabilidades]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'MSandovalConicimientoHabilidades', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL11.MSSQLSERVER\MSSQL\DATA\MSandovalConicimientoHabilidades.mdf' , SIZE = 4160KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'MSandovalConicimientoHabilidades_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL11.MSSQLSERVER\MSSQL\DATA\MSandovalConicimientoHabilidades_log.ldf' , SIZE = 1040KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [MSandovalConicimientoHabilidades] SET COMPATIBILITY_LEVEL = 110
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [MSandovalConicimientoHabilidades].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [MSandovalConicimientoHabilidades] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [MSandovalConicimientoHabilidades] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [MSandovalConicimientoHabilidades] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [MSandovalConicimientoHabilidades] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [MSandovalConicimientoHabilidades] SET ARITHABORT OFF 
GO
ALTER DATABASE [MSandovalConicimientoHabilidades] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [MSandovalConicimientoHabilidades] SET AUTO_CREATE_STATISTICS ON 
GO
ALTER DATABASE [MSandovalConicimientoHabilidades] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [MSandovalConicimientoHabilidades] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [MSandovalConicimientoHabilidades] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [MSandovalConicimientoHabilidades] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [MSandovalConicimientoHabilidades] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [MSandovalConicimientoHabilidades] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [MSandovalConicimientoHabilidades] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [MSandovalConicimientoHabilidades] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [MSandovalConicimientoHabilidades] SET  ENABLE_BROKER 
GO
ALTER DATABASE [MSandovalConicimientoHabilidades] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [MSandovalConicimientoHabilidades] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [MSandovalConicimientoHabilidades] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [MSandovalConicimientoHabilidades] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [MSandovalConicimientoHabilidades] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [MSandovalConicimientoHabilidades] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [MSandovalConicimientoHabilidades] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [MSandovalConicimientoHabilidades] SET RECOVERY FULL 
GO
ALTER DATABASE [MSandovalConicimientoHabilidades] SET  MULTI_USER 
GO
ALTER DATABASE [MSandovalConicimientoHabilidades] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [MSandovalConicimientoHabilidades] SET DB_CHAINING OFF 
GO
ALTER DATABASE [MSandovalConicimientoHabilidades] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [MSandovalConicimientoHabilidades] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
EXEC sys.sp_db_vardecimal_storage_format N'MSandovalConicimientoHabilidades', N'ON'
GO
USE [MSandovalConicimientoHabilidades]
GO
/****** Object:  StoredProcedure [dbo].[MedicamentoAdd]    Script Date: 12/6/2023 5:59:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[MedicamentoAdd]
@Precio FLOAT,
@NombreMedicamento VARCHAR(50),
@Cantidad INT,
@Total FLOAT
AS
INSERT INTO Medicamentos(
Precio,
NombreMedicamento,
Cantidad,
Total)VALUES(
@Precio,
@NombreMedicamento,
@Cantidad,
@Total)
GO
/****** Object:  StoredProcedure [dbo].[MedicamentoDelete]    Script Date: 12/6/2023 5:59:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[MedicamentoDelete]
@IdMedicamento INT
AS
DELETE FROM Medicamentos WHERE IdMedicamento=@IdMedicamento
GO
/****** Object:  StoredProcedure [dbo].[MedicamentoGetAll]    Script Date: 12/6/2023 5:59:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[MedicamentoGetAll]
AS
SELECT 
IdMedicamento,
Precio,
NombreMedicamento,
Cantidad,
Total
FROM Medicamentos
GO
/****** Object:  StoredProcedure [dbo].[MedicamentoGetById]    Script Date: 12/6/2023 5:59:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[MedicamentoGetById]
@IdMedicamento INT
AS
SELECT 
IdMedicamento,
Precio,
NombreMedicamento,
Cantidad,
Total
FROM Medicamentos
WHERE IdMedicamento=@IdMedicamento
GO
/****** Object:  StoredProcedure [dbo].[MedicamentoUpdate]    Script Date: 12/6/2023 5:59:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[MedicamentoUpdate]
@IdMedicamento INT,
@Precio FLOAT,
@NombreMedicamento VARCHAR(50),
@Cantidad INT,
@Total FLOAT
AS
UPDATE Medicamentos
SET 
Precio=@Precio,
NombreMedicamento=@NombreMedicamento,
Cantidad=@Cantidad,
Total=@Total
WHERE IdMedicamento=@IdMedicamento
GO
/****** Object:  StoredProcedure [dbo].[MedicamentoUpdateCantidad]    Script Date: 12/6/2023 5:59:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[MedicamentoUpdateCantidad]
@IdMedicamento INT,
@Cantidad INT
AS
UPDATE Medicamentos
SET 
Cantidad=@Cantidad
WHERE IdMedicamento=@IdMedicamento
GO
/****** Object:  StoredProcedure [dbo].[UsuarioAdd]    Script Date: 12/6/2023 5:59:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UsuarioAdd]
@Nombre VARCHAR(50),
@ApellidoPaterno VARCHAR(50),
@ApellidoMaterno VARCHAR(50)
AS
INSERT INTO Usuario(
Nombre,ApellidoPaterno,ApellidoMaterno)
VALUES(@Nombre,@ApellidoPaterno,@ApellidoMaterno)
GO
/****** Object:  StoredProcedure [dbo].[UsuarioDelete]    Script Date: 12/6/2023 5:59:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UsuarioDelete]
@IdUsuario INT
AS
DELETE FROM Usuario WHERE IdUsuario=@IdUsuario
GO
/****** Object:  StoredProcedure [dbo].[UsuarioGetAll]    Script Date: 12/6/2023 5:59:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UsuarioGetAll]
AS
SELECT
IdUsuario,
Nombre AS NombreUsuario,
ApellidoPaterno,
ApellidoMaterno
FROM Usuario
GO
/****** Object:  StoredProcedure [dbo].[UsuarioGetByEmail]    Script Date: 12/6/2023 5:59:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UsuarioGetByEmail] 
@Email VARCHAR(100)
AS
SELECT 
IdUsuario,
Usuario.Nombre AS NombreUsuario,
ApellidoPaterno,
ApellidoMaterno,
Email,
Password,
Rol.IdRol,
Rol.Nombre AS NombreRol
FROM Usuario
INNER JOIN Rol ON(Usuario.IdRol=Rol.IdRol)
WHERE Email=@Email
GO
/****** Object:  StoredProcedure [dbo].[UsuarioGetById]    Script Date: 12/6/2023 5:59:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UsuarioGetById]
@IdUsuario INT
AS
SELECT
IdUsuario,
Nombre AS NombreUsuario,
ApellidoPaterno,
ApellidoMaterno
FROM Usuario
WHERE IdUsuario=@IdUsuario
GO
/****** Object:  StoredProcedure [dbo].[UsuarioMedicamentoAdd]    Script Date: 12/6/2023 5:59:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UsuarioMedicamentoAdd]
@IdUsuario INT,
@IdMedicamento INT,
@Piezas INT,
@Total FLOAT
AS
INSERT INTO UsuarioMedicamento(
IdUsuario,IdMedicamento,Piezas,Total)VALUES(
@IdUsuario,@IdMedicamento,@Piezas,@Total)
GO
/****** Object:  StoredProcedure [dbo].[UsuarioUpdate]    Script Date: 12/6/2023 5:59:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UsuarioUpdate]
@IdUsuario INT,
@Nombre VARCHAR(50),
@ApellidoPaterno VARCHAR(50),
@ApellidoMaterno VARCHAR(50)
AS
UPDATE Usuario
SET
Nombre=@Nombre,
ApellidoPaterno=@ApellidoPaterno,
ApellidoMaterno=@ApellidoMaterno
WHERE IdUsuario=@IdUsuario
GO
/****** Object:  Table [dbo].[Medicamentos]    Script Date: 12/6/2023 5:59:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Medicamentos](
	[IdMedicamento] [int] IDENTITY(1,1) NOT NULL,
	[Precio] [float] NOT NULL,
	[NombreMedicamento] [varchar](50) NOT NULL,
	[Cantidad] [int] NULL,
	[Total] [float] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[IdMedicamento] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Rol]    Script Date: 12/6/2023 5:59:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Rol](
	[IdRol] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[IdRol] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Usuario]    Script Date: 12/6/2023 5:59:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Usuario](
	[IdUsuario] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](50) NOT NULL,
	[ApellidoPaterno] [varchar](50) NOT NULL,
	[ApellidoMaterno] [varchar](50) NULL,
	[Email] [varchar](100) NOT NULL,
	[Password] [varchar](50) NOT NULL,
	[IdRol] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[IdUsuario] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[UsuarioMedicamento]    Script Date: 12/6/2023 5:59:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UsuarioMedicamento](
	[IdUsuario] [int] NOT NULL,
	[IdMedicamento] [int] NOT NULL,
	[Piezas] [int] NULL,
	[Total] [float] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[IdUsuario] ASC,
	[IdMedicamento] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET IDENTITY_INSERT [dbo].[Medicamentos] ON 

GO
INSERT [dbo].[Medicamentos] ([IdMedicamento], [Precio], [NombreMedicamento], [Cantidad], [Total]) VALUES (2, 45.650001525878906, N'Diclofenaco', 18, 585)
GO
INSERT [dbo].[Medicamentos] ([IdMedicamento], [Precio], [NombreMedicamento], [Cantidad], [Total]) VALUES (3, 50, N'Paracetamol', 10, 50)
GO
INSERT [dbo].[Medicamentos] ([IdMedicamento], [Precio], [NombreMedicamento], [Cantidad], [Total]) VALUES (4, 10, N'Naproxeno', 15, 150)
GO
INSERT [dbo].[Medicamentos] ([IdMedicamento], [Precio], [NombreMedicamento], [Cantidad], [Total]) VALUES (5, 5, N'Loperamida', 4, 5)
GO
SET IDENTITY_INSERT [dbo].[Medicamentos] OFF
GO
SET IDENTITY_INSERT [dbo].[Rol] ON 

GO
INSERT [dbo].[Rol] ([IdRol], [Nombre]) VALUES (1, N'Administrador')
GO
INSERT [dbo].[Rol] ([IdRol], [Nombre]) VALUES (2, N'Usuario')
GO
SET IDENTITY_INSERT [dbo].[Rol] OFF
GO
SET IDENTITY_INSERT [dbo].[Usuario] ON 

GO
INSERT [dbo].[Usuario] ([IdUsuario], [Nombre], [ApellidoPaterno], [ApellidoMaterno], [Email], [Password], [IdRol]) VALUES (1, N'Mauricio', N'Sandoval', N'Garcia', N'mau@gmail.com', N'123', 1)
GO
INSERT [dbo].[Usuario] ([IdUsuario], [Nombre], [ApellidoPaterno], [ApellidoMaterno], [Email], [Password], [IdRol]) VALUES (2, N'Javier', N'Miguel', N'Sanchez', N'javier@gmail.com', N'123', 2)
GO
SET IDENTITY_INSERT [dbo].[Usuario] OFF
GO
INSERT [dbo].[UsuarioMedicamento] ([IdUsuario], [IdMedicamento], [Piezas], [Total]) VALUES (2, 2, 17, 585)
GO
INSERT [dbo].[UsuarioMedicamento] ([IdUsuario], [IdMedicamento], [Piezas], [Total]) VALUES (2, 3, 1, 50)
GO
INSERT [dbo].[UsuarioMedicamento] ([IdUsuario], [IdMedicamento], [Piezas], [Total]) VALUES (2, 4, 1, 10)
GO
INSERT [dbo].[UsuarioMedicamento] ([IdUsuario], [IdMedicamento], [Piezas], [Total]) VALUES (2, 5, 1, 5)
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [UQ__Usuario__A9D10534EE1B9575]    Script Date: 12/6/2023 5:59:49 PM ******/
ALTER TABLE [dbo].[Usuario] ADD UNIQUE NONCLUSTERED 
(
	[Email] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Usuario]  WITH CHECK ADD FOREIGN KEY([IdRol])
REFERENCES [dbo].[Rol] ([IdRol])
GO
ALTER TABLE [dbo].[UsuarioMedicamento]  WITH CHECK ADD FOREIGN KEY([IdMedicamento])
REFERENCES [dbo].[Medicamentos] ([IdMedicamento])
GO
ALTER TABLE [dbo].[UsuarioMedicamento]  WITH CHECK ADD FOREIGN KEY([IdUsuario])
REFERENCES [dbo].[Usuario] ([IdUsuario])
GO
USE [master]
GO
ALTER DATABASE [MSandovalConicimientoHabilidades] SET  READ_WRITE 
GO
