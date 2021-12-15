START TRANSACTION;

ALTER TABLE `Product` ADD `count_vote` int NOT NULL DEFAULT 0;

ALTER TABLE `Product` ADD `overview` longtext CHARACTER SET utf8mb4 NULL;

ALTER TABLE `Comment` ADD `created_at` datetime(6) NOT NULL DEFAULT '0001-01-01 00:00:00';

CREATE TABLE `Cart` (
    `id` bigint NOT NULL AUTO_INCREMENT,
    `user_id` bigint NOT NULL,
    `status` longtext CHARACTER SET utf8mb4 NULL,
    `created_at` datetime(6) NOT NULL,
    `message_from_user` longtext CHARACTER SET utf8mb4 NULL,
    `message_from_shop` longtext CHARACTER SET utf8mb4 NULL,
    CONSTRAINT `PK_Cart` PRIMARY KEY (`id`),
    CONSTRAINT `FK_Cart_User_user_id` FOREIGN KEY (`user_id`) REFERENCES `User` (`id`) ON DELETE CASCADE
) CHARACTER SET utf8mb4;

CREATE TABLE `CartItem` (
    `id` bigint NOT NULL AUTO_INCREMENT,
    `cart_id` bigint NOT NULL,
    `product_id` bigint NOT NULL,
    `quantity` int NOT NULL,
    `price` int NOT NULL,
    CONSTRAINT `PK_CartItem` PRIMARY KEY (`id`),
    CONSTRAINT `FK_CartItem_Cart_cart_id` FOREIGN KEY (`cart_id`) REFERENCES `Cart` (`id`) ON DELETE CASCADE,
    CONSTRAINT `FK_CartItem_Product_product_id` FOREIGN KEY (`product_id`) REFERENCES `Product` (`id`) ON DELETE CASCADE
) CHARACTER SET utf8mb4;

CREATE INDEX `IX_Cart_user_id` ON `Cart` (`user_id`);

CREATE INDEX `IX_CartItem_cart_id` ON `CartItem` (`cart_id`);

CREATE INDEX `IX_CartItem_product_id` ON `CartItem` (`product_id`);

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20211215112240_MigrationV3', '5.0.5');

COMMIT;

