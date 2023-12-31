CREATE TABLE `Managers` (
    `Id` INT(10) NOT NULL,
    `FullName` VARCHAR(255) NOT NULL,
    `Phone` VARCHAR(255) NOT NULL,
    `Status` INT(10) NOT NULL,
    `CreatedAt` DATETIME NOT NULL,
    FOREIGN KEY (`Id`) REFERENCES `Users`(`Id`) ON DELETE CASCADE ON UPDATE CASCADE
);