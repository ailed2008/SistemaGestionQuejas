Create database GestionQuejasBD;

Use GestionQuejasBD;

CREATE TABLE CatalogoEstados (
    Id_estado INT PRIMARY KEY IDENTITY(1,1),
    Nombre NVARCHAR(50) NOT NULL -- Registrada,En análisis,En proceso,Resuelta,Rechazada,Cerrada
);
CREATE TABLE CatalogoCanales (
    Id_canal INT PRIMARY KEY IDENTITY(1,1),
    Nombre NVARCHAR(50) NOT NULL -- Web, App, Teléfono, Presencial
);
CREATE TABLE CatalogoCategorias (
    Id_categoria INT PRIMARY KEY IDENTITY(1,1),
    Nombre NVARCHAR(100) NOT NULL 
);
----------------Datos para los catalogos----------------------------------
INSERT INTO [dbo].[CatalogoCanales] ([Nombre]) VALUES 
('Web'), 
('App'),
('Telefono'),
('Presencial');

INSERT INTO [dbo].[CatalogoEstados] ([Nombre]) 
VALUES 
('Registrada'), 
('En análisis'),
('En proceso'),
('Resuelta'),
('Rechazada'),
('Cerrada');

INSERT INTO [dbo].[CatalogoCategorias] ([Nombre]) 
VALUES 
('Falla técnica de equipo'),
('Lentitud'),
('Accesos'),
('Red');
----------------------------------------------------------

CREATE TABLE Quejas (
    Id_queja INT PRIMARY KEY IDENTITY(1,1),
    Folio NVARCHAR(20) UNIQUE NOT NULL, 
    Titulo NVARCHAR(100) NOT NULL,
    Descripcion NVARCHAR(MAX) NOT NULL,
	Nombre_cliente NVARCHAR(100) NOT NULL,
	Correo NVARCHAR(50) NOT NULL,
	Id_categoria INT NOT NULL, 
    Id_canal INT NOT NULL, 
    Id_estado INT NOT NULL DEFAULT 1, --Por defecto estatus 1 - Registrada
    Fecha_registro DATETIME DEFAULT GETDATE(),
    Fecha_actualizacion DATETIME,

    CONSTRAINT FK_Quejas_Canales FOREIGN KEY (Id_canal) 
        REFERENCES CatalogoCanales(Id_canal),
    
    CONSTRAINT FK_Quejas_Estados FOREIGN KEY (Id_estado) 
        REFERENCES CatalogoEstados(Id_estado),

    CONSTRAINT FK_Quejas_Categorias FOREIGN KEY (Id_categoria) 
        REFERENCES CatalogoCategorias(Id_categoria)
);


CREATE TABLE HistorialQuejas (
    Id_historial INT PRIMARY KEY IDENTITY(1,1),
    Id_queja INT NOT NULL, 
    Id_estado_anterior INT NOT NULL,
	Id_estado_nuevo INT NOT NULL,
    Fecha_cambio DATETIME DEFAULT GETDATE(),
	Usuario NVARCHAR(100) NOT NULL,
	Comentarios NVARCHAR(150),
    CONSTRAINT FK_Historial_Quejas FOREIGN KEY (Id_queja) 
        REFERENCES Quejas(Id_queja),
    
    CONSTRAINT FK_Historial_EstadoAnterior FOREIGN KEY (Id_estado_anterior) 
        REFERENCES CatalogoEstados(Id_estado),

    CONSTRAINT FK_Historial_EstadoNuevo FOREIGN KEY (Id_estado_nuevo) 
        REFERENCES CatalogoEstados(Id_estado),
);


----------------índices--------------
CREATE UNIQUE INDEX IX_Quejas_Folio ON Quejas(Folio);
CREATE INDEX IX_Quejas_Id_estado ON Quejas(Id_estado);
CREATE INDEX IX_Historial_Id_queja ON HistorialQuejas(Id_queja);




----------Datos------------------------------

INSERT INTO [dbo].[Quejas]
           ([Folio]
           ,[Titulo]
           ,[Descripcion]
           ,[Nombre_cliente]
           ,[Correo]
           ,[Id_categoria]
           ,[Id_canal]
           ,[Id_estado]
           ,[Fecha_registro]
           ,[Fecha_actualizacion])
     VALUES
           ('F-20260228-AC01'
           ,'Falla con mi equipo'
           ,'Estaba utilizando mi equipo y de pronto dio pantalla negra'
           ,'Diana Bracho'
           ,'dbracho@gmail.com'
           ,1
           ,1
           ,1
           ,'2026-02-28T20:22:08.251Z'
           ,'2026-02-28T20:22:20.251Z')


INSERT INTO [dbo].[HistorialQuejas]
           (
		    [Id_queja],
            [Id_estado_anterior]
           ,[Id_estado_nuevo]
           ,[Fecha_cambio]
           ,[Usuario]
           ,[Comentarios])
     VALUES
           (
		   1
           ,1
           ,2
           ,'2026-02-28T20:22:20.251Z'
           ,'Hilaria Sanz'
           ,'Se está revisando el historial del equipo');