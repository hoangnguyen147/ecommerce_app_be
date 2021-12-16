START TRANSACTION;

ALTER TABLE `Cart` DROP COLUMN `message_from_shop`;

ALTER TABLE `Cart` DROP COLUMN `message_from_user`;

CREATE TABLE `Orders` (
    `id` bigint NOT NULL AUTO_INCREMENT,
    `user_id` bigint NOT NULL,
    `status` longtext CHARACTER SET utf8mb4 NULL,
    `created_at` datetime(6) NOT NULL,
    `message` longtext CHARACTER SET utf8mb4 NULL,
    CONSTRAINT `PK_Orders` PRIMARY KEY (`id`),
    CONSTRAINT `FK_Orders_User_user_id` FOREIGN KEY (`user_id`) REFERENCES `User` (`id`) ON DELETE CASCADE
) CHARACTER SET utf8mb4;

CREATE TABLE `OrderItem` (
    `id` bigint NOT NULL AUTO_INCREMENT,
    `order_id` bigint NOT NULL,
    `product_id` bigint NOT NULL,
    `quantity` int NOT NULL,
    CONSTRAINT `PK_OrderItem` PRIMARY KEY (`id`),
    CONSTRAINT `FK_OrderItem_Orders_order_id` FOREIGN KEY (`order_id`) REFERENCES `Orders` (`id`) ON DELETE CASCADE,
    CONSTRAINT `FK_OrderItem_Product_product_id` FOREIGN KEY (`product_id`) REFERENCES `Product` (`id`) ON DELETE CASCADE
) CHARACTER SET utf8mb4;

CREATE INDEX `IX_OrderItem_order_id` ON `OrderItem` (`order_id`);

CREATE INDEX `IX_OrderItem_product_id` ON `OrderItem` (`product_id`);

CREATE INDEX `IX_Orders_user_id` ON `Orders` (`user_id`);

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20211216065430_MigrationV5', '5.0.5');

COMMIT;

