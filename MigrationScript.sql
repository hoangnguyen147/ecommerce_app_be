ALTER DATABASE CHARACTER SET utf8mb4;
CREATE TABLE IF NOT EXISTS `__EFMigrationsHistory` (
    `MigrationId` varchar(150) CHARACTER SET utf8mb4 NOT NULL,
    `ProductVersion` varchar(32) CHARACTER SET utf8mb4 NOT NULL,
    CONSTRAINT `PK___EFMigrationsHistory` PRIMARY KEY (`MigrationId`)
) CHARACTER SET utf8mb4;

START TRANSACTION;

ALTER DATABASE CHARACTER SET utf8mb4;

CREATE TABLE `Category` (
    `id` bigint NOT NULL AUTO_INCREMENT,
    `name` longtext CHARACTER SET utf8mb4 NULL,
    `description` longtext CHARACTER SET utf8mb4 NULL,
    `image` longtext CHARACTER SET utf8mb4 NULL,
    CONSTRAINT `PK_Category` PRIMARY KEY (`id`)
) CHARACTER SET utf8mb4;

CREATE TABLE `Product` (
    `id` bigint NOT NULL AUTO_INCREMENT,
    `category_id` longtext CHARACTER SET utf8mb4 NULL,
    `name` longtext CHARACTER SET utf8mb4 NULL,
    `image` longtext CHARACTER SET utf8mb4 NULL,
    `price` int NOT NULL,
    `quantity` int NOT NULL,
    `vote` float NOT NULL,
    `is_hot` tinyint(1) NOT NULL,
    CONSTRAINT `PK_Product` PRIMARY KEY (`id`)
) CHARACTER SET utf8mb4;

CREATE TABLE `User` (
    `id` bigint NOT NULL AUTO_INCREMENT,
    `username` longtext CHARACTER SET utf8mb4 NULL,
    `password` longtext CHARACTER SET utf8mb4 NULL,
    `fullname` longtext CHARACTER SET utf8mb4 NULL,
    `phone` longtext CHARACTER SET utf8mb4 NULL,
    `email` longtext CHARACTER SET utf8mb4 NULL,
    `address` longtext CHARACTER SET utf8mb4 NULL,
    `avatar` longtext CHARACTER SET utf8mb4 NULL,
    `is_admin` tinyint(1) NOT NULL,
    CONSTRAINT `PK_User` PRIMARY KEY (`id`)
) CHARACTER SET utf8mb4;

CREATE TABLE `Comment` (
    `id` bigint NOT NULL AUTO_INCREMENT,
    `content` longtext CHARACTER SET utf8mb4 NULL,
    `product_id` bigint NOT NULL,
    `user_id` bigint NOT NULL,
    `vote` float NOT NULL,
    CONSTRAINT `PK_Comment` PRIMARY KEY (`id`),
    CONSTRAINT `FK_Comment_Product_product_id` FOREIGN KEY (`product_id`) REFERENCES `Product` (`id`) ON DELETE CASCADE,
    CONSTRAINT `FK_Comment_User_user_id` FOREIGN KEY (`user_id`) REFERENCES `User` (`id`) ON DELETE CASCADE
) CHARACTER SET utf8mb4;

CREATE INDEX `IX_Comment_product_id` ON `Comment` (`product_id`);

CREATE INDEX `IX_Comment_user_id` ON `Comment` (`user_id`);

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20211206074209_MigrationV1', '5.0.5');

COMMIT;

START TRANSACTION;

ALTER TABLE `Product` MODIFY COLUMN `category_id` bigint NOT NULL DEFAULT 0;

CREATE INDEX `IX_Product_category_id` ON `Product` (`category_id`);

ALTER TABLE `Product` ADD CONSTRAINT `FK_Product_Category_category_id` FOREIGN KEY (`category_id`) REFERENCES `Category` (`id`) ON DELETE CASCADE;

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20211208091416_MigrationV2', '5.0.5');

COMMIT;
