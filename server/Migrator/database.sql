CREATE TABLE IF NOT EXISTS `__EFMigrationsHistory` (
    `MigrationId` varchar(150) CHARACTER SET utf8mb4 NOT NULL,
    `ProductVersion` varchar(32) CHARACTER SET utf8mb4 NOT NULL,
    CONSTRAINT `PK___EFMigrationsHistory` PRIMARY KEY (`MigrationId`)
) CHARACTER SET=utf8mb4;

START TRANSACTION;

ALTER DATABASE CHARACTER SET utf8mb4;

CREATE TABLE `Book` (
    `Id` bigint NOT NULL,
    `Title` varchar(127) CHARACTER SET utf8mb4 NOT NULL,
    `Subtitle` varchar(127) CHARACTER SET utf8mb4 NOT NULL,
    `Cover` varchar(255) CHARACTER SET utf8mb4 NOT NULL,
    `Author` varchar(100) CHARACTER SET utf8mb4 NULL,
    `PubDate` date NOT NULL,
    `Intro` text CHARACTER SET utf8mb4 NOT NULL,
    `OrgUrl` varchar(127) CHARACTER SET utf8mb4 NULL,
    `Formats` varchar(50) CHARACTER SET utf8mb4 NOT NULL,
    `FetchUrl` varchar(127) CHARACTER SET utf8mb4 NOT NULL,
    `FetchCode` varchar(10) CHARACTER SET utf8mb4 NULL,
    `Views` int NOT NULL,
    `Downloads` int NOT NULL,
    `CreatedAt` datetime(0) NOT NULL,
    `UpdatedAt` datetime(0) NOT NULL,
    CONSTRAINT `PK_Book` PRIMARY KEY (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `Tag` (
    `Id` bigint NOT NULL,
    `Name` varchar(20) CHARACTER SET utf8mb4 NOT NULL,
    CONSTRAINT `PK_Tag` PRIMARY KEY (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `BookTag` (
    `BookId` bigint NOT NULL,
    `TagId` bigint NOT NULL,
    CONSTRAINT `PK_BookTag` PRIMARY KEY (`BookId`, `TagId`),
    CONSTRAINT `FK_BookTag_Book_BookId` FOREIGN KEY (`BookId`) REFERENCES `Book` (`Id`) ON DELETE CASCADE,
    CONSTRAINT `FK_BookTag_Tag_TagId` FOREIGN KEY (`TagId`) REFERENCES `Tag` (`Id`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

CREATE INDEX `IX_BookTag_TagId` ON `BookTag` (`TagId`);

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20220208101053_00', '6.0.1');

COMMIT;

