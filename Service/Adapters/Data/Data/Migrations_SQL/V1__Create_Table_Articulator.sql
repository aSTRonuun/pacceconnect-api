CREATE TABLE `Articulators` (
    `Id` INT(10) NOT NULL,
    `Name` VARCHAR(255) NOT NULL,
    `SurName` VARCHAR(255) NOT NULL,
    `StudentId_Matriculation` INT(10) NOT NULL,
    `StudentId_Course` INT(10) NOT NULL,
    `PhoneNumber` VARCHAR(255) NULL DEFAULT NULL,
    FOREIGN KEY (`Id`) REFERENCES `Users`(`Id`) ON DELETE CASCADE ON UPDATE CASCADE
);