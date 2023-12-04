CREATE DATABASE MSandovalConicimientoHabilidades

CREATE TABLE Usuario(
IdUsuario INT PRIMARY KEY IDENTITY(1,1),
Nombre VARCHAR(50) NOT NULL,
ApellidoPaterno VARCHAR(50) NOT NULL,
ApellidoMaterno VARCHAR(50)
)

CREATE TABLE Medicamentos(
IdMedicamento INT PRIMARY KEY IDENTITY(1,1),
Precio FLOAT NOT NULL,
NombreMedicamento VARCHAR(50) NOT NULL
)

ALTER TABLE Medicamentos
ADD Cantidad INT
ALTER TABLE Medicamentos
ADD Total FLOAT NOT NULL

ALTER TABLE Usuario
ADD Email VARCHAR(100) 

ALTER TABLE Usuario
ALTER COLUMN Email VARCHAR(100) NOT NULL

ALTER TABLE Usuario
ADD Password VARCHAR(50) 

ALTER TABLE Usuario
ALTER COLUMN Password VARCHAR(50) NOT NULL

ALTER TABLE Usuario
ADD UNIQUE (Email)

CREATE TABLE UsuarioMedicamento(
IdUsuario INT REFERENCES Usuario(IdUsuario),
IdMedicamento INT REFERENCES Medicamentos(IdMedicamento),
PRIMARY KEY(IdUsuario,IdMedicamento)
)

CREATE PROCEDURE UsuarioGetAll
AS
SELECT
IdUsuario,
Nombre AS NombreUsuario,
ApellidoPaterno,
ApellidoMaterno
FROM Usuario

CREATE PROCEDURE UsuarioAdd 'Mauricio','Sandoval','Garcia'
@Nombre VARCHAR(50),
@ApellidoPaterno VARCHAR(50),
@ApellidoMaterno VARCHAR(50)
AS
INSERT INTO Usuario(
Nombre,ApellidoPaterno,ApellidoMaterno)
VALUES(@Nombre,@ApellidoPaterno,@ApellidoMaterno)

CREATE PROCEDURE UsuarioUpdate
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

CREATE PROCEDURE UsuarioDelete
@IdUsuario INT
AS
DELETE FROM Usuario WHERE IdUsuario=@IdUsuario
CREATE PROCEDURE UsuarioGetById
@IdUsuario INT
AS
SELECT
IdUsuario,
Nombre AS NombreUsuario,
ApellidoPaterno,
ApellidoMaterno
FROM Usuario
WHERE IdUsuario=@IdUsuario

CREATE PROCEDURE MedicamentoGetAll
AS
SELECT 
IdMedicamento,
Precio,
NombreMedicamento,
Cantidad,
Total
FROM Medicamentos

ALTER PROCEDURE MedicamentoGetById
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

CREATE PROCEDURE MedicamentoAdd
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

CREATE PROCEDURE MedicamentoUpdate
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

CREATE PROCEDURE MedicamentoDelete
@IdMedicamento INT
AS
DELETE FROM Medicamentos 
WHERE 
IdMedicamento=@IdMedicamento

CREATE TABLE Rol(
IdRol INT PRIMARY KEY IDENTITY(1,1),
Nombre VARCHAR(50) NOT NULL
)
INSERT INTO Rol(
Nombre)VALUES('Administrador'),('Usuario')

ALTER TABLE Usuario
ADD IdRol INT REFERENCES Rol(IdRol)


ALTER PROCEDURE UsuarioGetByEmail 'mau@gmail.com'
@Email VARCHAR(100)
AS
SELECT 
IdUsuario,
Email,
Password,
Rol.IdRol,
Rol.Nombre AS NombreRol
FROM Usuario
INNER JOIN Rol ON(Usuario.IdRol=Rol.IdRol)
WHERE Email=@Email