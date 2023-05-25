CREATE TABLE `Cells` (
    `Id` INT(10) NOT NULL AUTO_INCREMENT,
    `Name` VARCHAR(255) NOT NULL,
    `Status` INT(10) NOT NULL,
    `CreatedAt` DATETIME NOT NULL,
    `UpdatedAt` DATETIME NOT NULL,
    `ArticulatorId` INT(10) NOT NULL,
    `CellPlanId` INT(10),
    PRIMARY KEY (`Id`) USING BTREE,
    FOREIGN KEY (`ArticulatorId`) REFERENCES `Articulators`(`Id`),
    FOREIGN KEY (`CellPlanId`) REFERENCES `CellPlans`(`Id`),
    UNIQUE (`ArticulatorId`) USING BTREE
);