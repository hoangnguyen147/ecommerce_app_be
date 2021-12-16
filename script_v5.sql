START TRANSACTION;

CREATE TABLE `Order` (
    `id` bigint NOT NULL AUTO_INCREMENT,
    `user_id` bigint NOT NULL,
    `status` longtext CHARACTER SET utf8mb4 NULL,
    `created_at` datetime(6) NOT NULL,
    `message` longtext CHARACTER SET utf8mb4 NULL,
    CONSTRAINT `PK_Order` PRIMARY KEY (`id`),
    CONSTRAINT `FK_Order_User_user_id` FOREIGN KEY (`user_id`) REFERENCES `User` (`id`) ON DELETE CASCADE
) CHARACTER SET utf8mb4;

CREATE TABLE `OrderItem` (
    `id` bigint NOT NULL AUTO_INCREMENT,
    `order_id` bigint NOT NULL,
    `product_id` bigint NOT NULL,
    `quantity` int NOT NULL,
    `price` int NOT NULL,
    CONSTRAINT `PK_OrderItem` PRIMARY KEY (`id`),
    CONSTRAINT `FK_OrderItem_Order_order_id` FOREIGN KEY (`order_id`) REFERENCES `Order` (`id`) ON DELETE CASCADE,
    CONSTRAINT `FK_OrderItem_Product_product_id` FOREIGN KEY (`product_id`) REFERENCES `Product` (`id`) ON DELETE CASCADE
) CHARACTER SET utf8mb4;

CREATE INDEX `IX_Order_user_id` ON `Order` (`user_id`);

CREATE INDEX `IX_OrderItem_order_id` ON `OrderItem` (`order_id`);

CREATE INDEX `IX_OrderItem_product_id` ON `OrderItem` (`product_id`);

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20211216040318_MigrationV5', '5.0.5');

COMMIT;

