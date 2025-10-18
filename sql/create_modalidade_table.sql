-- Garante que o banco exista e está em uso
CREATE DATABASE IF NOT EXISTS `tst` DEFAULT CHARACTER SET utf8mb4;
USE `tst`;

DROP TABLE IF EXISTS `modalidade`;
CREATE TABLE `modalidade` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `Nome` varchar(100) NOT NULL,
    `TurmaId` int NULL,
    PRIMARY KEY (`Id`),
    KEY `IX_Modalidade_TurmaId` (`TurmaId`),
    CONSTRAINT `FK_Modalidade_Turma` FOREIGN KEY (`TurmaId`) 
    REFERENCES `turma` (`Id`) ON DELETE SET NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;
