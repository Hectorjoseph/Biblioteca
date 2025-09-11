-- ======================================
-- CREAR BASE DE DATOS SOLO SI NO EXISTE
-- ======================================
IF DB_ID('BibliotecaDB') IS NULL
BEGIN
    CREATE DATABASE BibliotecaDB;
END
GO

USE BibliotecaDB;
GO

-- ======================================
-- TABLAS PRINCIPALES
-- ======================================

CREATE TABLE Autores (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Nombre NVARCHAR(100) NOT NULL,
    Nacionalidad NVARCHAR(100)
);

CREATE TABLE Temas (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Nombre_Tema NVARCHAR(100) NOT NULL
);

CREATE TABLE Editoriales (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Nombre_Editorial NVARCHAR(100) NOT NULL
);

CREATE TABLE Paises (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Nombre_Pais NVARCHAR(100) NOT NULL
);

CREATE TABLE Tipos (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Nombre_Tipo NVARCHAR(100) NOT NULL
);

CREATE TABLE Libros (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Editorial INT FOREIGN KEY REFERENCES Editoriales(Id),
    Pais INT FOREIGN KEY REFERENCES Paises(Id),
    Tipo INT FOREIGN KEY REFERENCES Tipos(Id),
    Isbn NVARCHAR(20) UNIQUE,
    Titulo NVARCHAR(200) NOT NULL,
    Edicion NVARCHAR(50),
    Fecha_Lanzamiento DATE
);

CREATE TABLE Libros_Autores (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Libro INT FOREIGN KEY REFERENCES Libros(Id),
    Autor INT FOREIGN KEY REFERENCES Autores(Id)
);

CREATE TABLE Libros_Temas (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Libro INT FOREIGN KEY REFERENCES Libros(Id),
    Tema INT FOREIGN KEY REFERENCES Temas(Id)
);

CREATE TABLE Existencias (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Libro INT FOREIGN KEY REFERENCES Libros(Id),
    Codigo_Ejemplar NVARCHAR(50) UNIQUE NOT NULL
);

CREATE TABLE Estados (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Nombre_Estado NVARCHAR(100) NOT NULL
);

CREATE TABLE Estados_Existencias (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Existencia INT FOREIGN KEY REFERENCES Existencias(Id),
    Estado INT FOREIGN KEY REFERENCES Estados(Id),
    Fecha_Cambio DATETIME DEFAULT GETDATE()
);

CREATE TABLE Usuarios (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Nombre NVARCHAR(100) NOT NULL,
    Documento NVARCHAR(50) UNIQUE,
    Telefono NVARCHAR(20),
    Direccion NVARCHAR(200),
    Fecha_Nacimiento DATE,
    Correo NVARCHAR(150) UNIQUE,
    Contraseña NVARCHAR(255) NOT NULL -- HASH de la clave
);

CREATE TABLE Tipos_Prestamos (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Descripcion NVARCHAR(100)
);

CREATE TABLE Prestamos (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Usuario INT FOREIGN KEY REFERENCES Usuarios(Id),
    Tipo_Prestamo INT FOREIGN KEY REFERENCES Tipos_Prestamos(Id),
    Existencia INT FOREIGN KEY REFERENCES Existencias(Id),
    Fecha_Prestamo DATE NOT NULL,
    Fecha_Devolucion DATE,
    Fecha_Entrega_Real DATE
);

CREATE TABLE Sanciones (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Usuario INT FOREIGN KEY REFERENCES Usuarios(Id),
    Descripcion NVARCHAR(MAX),
    Fecha_Inicio DATE,
    Fecha_Fin DATE
);

-- ======================================
-- TABLA DE AUDITORÍAS
-- ======================================

CREATE TABLE Auditorias (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Tabla NVARCHAR(50) NOT NULL,
    Operacion NVARCHAR(10) NOT NULL,
    UsuarioBD NVARCHAR(100) DEFAULT SUSER_SNAME(),
    Fecha DATETIME DEFAULT GETDATE(),
    Datos_Antiguos NVARCHAR(MAX),
    Datos_Nuevos NVARCHAR(MAX)
);

-- ======================================
-- TRIGGERS DE AUDITORÍA
-- ======================================

-- USUARIOS
CREATE TRIGGER trg_usuarios_insert
ON Usuarios
AFTER INSERT
AS	
BEGIN
    INSERT INTO Auditorias (Tabla, Operacion, Datos_Nuevos)
    SELECT 'Usuarios', 'INSERT',
           'Id=' + CAST(i.Id AS NVARCHAR) + ', Correo=' + ISNULL(i.Correo,'')
    FROM inserted i;
END
GO

CREATE TRIGGER trg_usuarios_update
ON Usuarios
AFTER UPDATE
AS
BEGIN
    INSERT INTO Auditorias (Tabla, Operacion, Datos_Antiguos, Datos_Nuevos)
    SELECT 'Usuarios', 'UPDATE',
           'Id=' + CAST(d.Id AS NVARCHAR) + ', Correo=' + ISNULL(d.Correo,''),
           'Id=' + CAST(i.Id AS NVARCHAR) + ', Correo=' + ISNULL(i.Correo,'')
    FROM deleted d
    INNER JOIN inserted i ON d.Id = i.Id;
END
GO

CREATE TRIGGER trg_usuarios_delete
ON Usuarios
AFTER DELETE
AS
BEGIN
    INSERT INTO Auditorias (Tabla, Operacion, Datos_Antiguos)
    SELECT 'Usuarios', 'DELETE',
           'Id=' + CAST(d.Id AS NVARCHAR) + ', Correo=' + ISNULL(d.Correo,'')
    FROM deleted d;
END
GO

-- LIBROS
CREATE TRIGGER trg_libros_insert
ON Libros
AFTER INSERT
AS
BEGIN
    INSERT INTO Auditorias (Tabla, Operacion, Datos_Nuevos)
    SELECT 'Libros', 'INSERT',
           'Id=' + CAST(i.Id AS NVARCHAR) + ', Titulo=' + ISNULL(i.Titulo,'')
    FROM inserted i;
END
GO

-- PRESTAMOS
CREATE TRIGGER trg_prestamos_insert
ON Prestamos
AFTER INSERT
AS
BEGIN
    INSERT INTO Auditorias (Tabla, Operacion, Datos_Nuevos)
    SELECT 'Prestamos', 'INSERT',
           'Id=' + CAST(i.Id AS NVARCHAR) +
           ', Usuario=' + CAST(i.Usuario AS NVARCHAR) +
           ', Existencia=' + CAST(i.Existencia AS NVARCHAR)
    FROM inserted i;
END
GO

-- ======================================
-- INSERTS DE DATOS
-- ======================================

-- AUTORES
INSERT INTO Autores (Nombre, Nacionalidad) VALUES
('Gabriel García Márquez', 'Colombiana'),
('Isabel Allende', 'Chilena'),
('Mario Vargas Llosa', 'Peruana'),
('Julio Cortázar', 'Argentina'),
('J. K. Rowling', 'Británica');

-- TEMAS
INSERT INTO Temas (Nombre_Tema) VALUES
('Realismo Mágico'),
('Fantasía'),
('Historia'),
('Ciencia Ficción'),
('Literatura Infantil');

-- EDITORIALES
INSERT INTO Editoriales (Nombre_Editorial) VALUES
('Editorial Sudamericana'),
('Alfaguara'),
('Planeta'),
('Penguin Random House'),
('Seix Barral');

-- PAISES
INSERT INTO Paises (Nombre_Pais) VALUES
('Colombia'),
('Chile'),
('Perú'),
('Argentina'),
('Reino Unido');

-- TIPOS
INSERT INTO Tipos (Nombre_Tipo) VALUES
('Novela'),
('Cuento'),
('Ensayo'),
('Poesía'),
('Biografía');

-- LIBROS
INSERT INTO Libros (Editorial, Pais, Tipo, Isbn, Titulo, Edicion, Fecha_Lanzamiento) VALUES
(1, 1, 1, '9780307474728', 'Cien Años de Soledad', '1ra', '1967-05-30'),
(2, 2, 1, '9789505116024', 'La Casa de los Espíritus', '2da', '1982-01-01'),
(3, 3, 1, '9788432227764', 'La Ciudad y los Perros', '3ra', '1963-01-01'),
(4, 4, 2, '9788497592208', 'Bestiario', '1ra', '1951-01-01'),
(5, 5, 2, '9780747532743', 'Harry Potter y la Piedra Filosofal', '1ra', '1997-06-26');

-- LIBROS_AUTORES
INSERT INTO Libros_Autores (Libro, Autor) VALUES
(1, 1),
(2, 2),
(3, 3),
(4, 4),
(5, 5);

-- LIBROS_TEMAS
INSERT INTO Libros_Temas (Libro, Tema) VALUES
(1, 1),
(2, 1),
(3, 3),
(4, 4),
(5, 2);

-- EXISTENCIAS
INSERT INTO Existencias (Libro, Codigo_Ejemplar) VALUES
(1, 'CA001'),
(1, 'CA002'),
(2, 'CE001'),
(3, 'CP001'),
(4, 'CAJ001'),
(5, 'HP001');

-- ESTADOS
INSERT INTO Estados (Nombre_Estado) VALUES
('Disponible'),
('Prestado'),
('En reparación'),
('Perdido'),
('Reservado');

-- ESTADOS_EXISTENCIAS
INSERT INTO Estados_Existencias (Existencia, Estado) VALUES
(1, 1),
(2, 2),
(3, 1),
(4, 1),
(5, 1),
(6, 1);

-- USUARIOS
INSERT INTO Usuarios (Nombre, Documento, Telefono, Direccion, Fecha_Nacimiento, Correo, Contraseña) VALUES
('Juan Pérez', '100200300', '3001234567', 'Calle Falsa 123', '1990-05-20', 'juan.perez@mail.com', 'hash1'),
('María Gómez', '200300400', '3019876543', 'Carrera 45 #12', '1985-11-10', 'maria.gomez@mail.com', 'hash2'),
('Carlos Ramírez', '300400500', '3024567890', 'Av. 7 #89', '1992-02-15', 'carlos.ramirez@mail.com', 'hash3'),
('Ana Torres', '400500600', '3036549871', 'Calle 9 #32', '2000-09-25', 'ana.torres@mail.com', 'hash4'),
('Luis Fernández', '500600700', '3047412589', 'Transv. 21 #14', '1995-03-30', 'luis.fernandez@mail.com', 'hash5');

-- TIPOS_PRESTAMOS
INSERT INTO Tipos_Prestamos (Descripcion) VALUES
('Consulta en sala'),
('Préstamo a domicilio'),
('Préstamo interbibliotecario');

-- PRESTAMOS
INSERT INTO Prestamos (Usuario, Tipo_Prestamo, Existencia, Fecha_Prestamo, Fecha_Devolucion, Fecha_Entrega_Real) VALUES
(1, 2, 1, '2025-09-01', '2025-09-15', NULL),
(2, 1, 3, '2025-09-05', '2025-09-05', '2025-09-05'),
(3, 2, 6, '2025-08-20', '2025-09-03', '2025-09-02');

-- SANCIONES
INSERT INTO Sanciones (Usuario, Descripcion, Fecha_Inicio, Fecha_Fin) VALUES
(3, 'Devolución tardía de ejemplar HP001', '2025-09-04', '2025-09-11'),
(2, 'Mal estado en el libro CE001', '2025-09-07', '2025-09-14');
