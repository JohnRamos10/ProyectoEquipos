USE EquiposController;

DROP TABLE IF EXISTS Tarea;


CREATE TABLE Tarea (
    id INT PRIMARY KEY IDENTITY(1,1),
    Estado NVARCHAR(50),
    prioridad NVARCHAR(50),
    FechaVencimiento DATE,
    ProyectoId INT,
    UsuarioAsignadoId INT

);