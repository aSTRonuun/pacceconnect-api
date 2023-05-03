CREATE TABLE `Users` (
    `Id` INT(10) NOT NULL AUTO_INCREMENT,
    `UserName` VARCHAR(255) NOT NULL,
    `Email` VARCHAR(255) NOT NULL,
    `PasswordHash` BLOB NOT NULL,
    `PasswordSalt` BLOB NOT NULL,
    `Role` INT(10) NOT NULL,
    PRIMARY KEY (`Id`),
    UNIQUE (`Email`) USING BTREE,
    UNIQUE (`UserName`) USING BTREE
);
