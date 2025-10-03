/* Made By kron0800 - GitHub: https://github.com/kron0800 */

CREATE DATABASE modeloparcial_progii
GO

USE modeloparcial_progii
GO


CREATE TABLE Countries(
	id INT IDENTITY(1,1) PRIMARY KEY,
	country NVARCHAR(MAX) NOT NULL
);

CREATE TABLE Users (
	id INT IDENTITY(1,1) PRIMARY KEY,
	username NVARCHAR(MAX) NOT NULL,
	id_country INT NOT NULL,
	isActive BIT NOT NULL,
	FOREIGN KEY (id_country) REFERENCES Countries(id)
);

CREATE TABLE Tweets (
	id INT IDENTITY(1,1) PRIMARY KEY,
	id_user INT NOT NULL,
	content NVARCHAR(MAX) NOT NULL,
	publish_datetime DATETIME NOT NULL,
	isPublic BIT NOT NULL,
	FOREIGN KEY (id_user) REFERENCES Users(id)
);


INSERT INTO Countries(country) 
VALUES ('Argentina'), ('United States Of America'), ('Spain');


INSERT INTO Users (username, id_country, isActive)
VALUES
('bottita_oscar', 1, 1),  
('kkundalover', 1, 1), 
('chadtoro', 2, 1),   
('vegetta777', 3, 1);

INSERT INTO Tweets (id_user, content, publish_datetime, isPublic)
VALUES
-- bottita_oscar
(1, 'A ver chicos vamos a levantar las manitosss, las manitos arribaaaa', '2025-09-05 08:00:00', 1),
(1, 'Quien fue el gracioso que borro la tabla de Envios?', '2025-09-15 12:00:00', 1),

-- kkundalover
(2, 'Que es T-SQL??', '2025-09-08 10:30:00', 1),
(2, 'El proximo que ponga sum en el where lo mando a nivelacion', '2025-09-20 15:45:00', 1),

-- chadtoro
(3, 'Just boched half the class, feeling good!', '2025-09-12 09:00:00', 1),
(3, 'Luzz canal gratis is active for videocall!!!!', '2025-09-25 17:30:00', 1),

-- vegetta777
(4, 'Asi que, click derecho... Me ha salido negro.', '2025-09-18 13:00:00', 1),
(4, 'Ya me quede sin ideas loco. https://www.youtube.com/watch?v=ANFkRoB9U5U', '2025-10-01 18:20:00', 1);
