START TRANSACTION;

ALTER TABLE `User` ADD `finance` int NOT NULL DEFAULT 0;

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20211216035530_MigrationV4', '5.0.5');

COMMIT;

