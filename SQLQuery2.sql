USE EquiposController;

CREATE TABLE Proyecto (
    id INT PRIMARY KEY IDENTITY,
    nombre NVARCHAR(100),
    descripcion NVARCHAR(500),
    fechaProyecto DATE
);