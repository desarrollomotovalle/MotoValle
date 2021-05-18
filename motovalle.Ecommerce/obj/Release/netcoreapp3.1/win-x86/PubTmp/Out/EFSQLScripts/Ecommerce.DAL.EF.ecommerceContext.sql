CREATE TABLE IF NOT EXISTS `__EFMigrationsHistory` (
    `MigrationId` varchar(95) NOT NULL,
    `ProductVersion` varchar(32) NOT NULL,
    CONSTRAINT `PK___EFMigrationsHistory` PRIMARY KEY (`MigrationId`)
);


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20200128153729_Initial') THEN

    CREATE TABLE `body_styles` (
        `body_styles_id` int(11) NOT NULL AUTO_INCREMENT,
        `body_style_name` varchar(255) NOT NULL,
        CONSTRAINT `PK_body_styles` PRIMARY KEY (`body_styles_id`)
    );

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20200128153729_Initial') THEN

    CREATE TABLE `categories` (
        `categories_id` int(11) NOT NULL AUTO_INCREMENT,
        `category_name` varchar(45) NOT NULL,
        `description` varchar(255) NULL,
        CONSTRAINT `PK_categories` PRIMARY KEY (`categories_id`)
    );

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20200128153729_Initial') THEN

    CREATE TABLE `customers` (
        `customers_id` int(11) NOT NULL AUTO_INCREMENT,
        `account_number` varchar(45) NULL,
        `full_name` varchar(100) NOT NULL,
        `email_address` varchar(45) NULL,
        `phone_number` varchar(45) NULL,
        `alternate_number` varchar(45) NULL,
        `bill_to_address` varchar(45) NULL,
        `bill_to_city` varchar(45) NULL,
        `bill_to_state` varchar(45) NULL,
        `bill_to_zipcode` varchar(45) NULL,
        `ship_to_address` varchar(45) NULL,
        `ship_to_city` varchar(45) NULL,
        `ship_to_state` varchar(45) NULL,
        `ship_to_zipcode` varchar(45) NULL,
        CONSTRAINT `PK_customers` PRIMARY KEY (`customers_id`)
    );

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20200128153729_Initial') THEN

    CREATE TABLE `drive_trains` (
        `drive_trains_id` int(11) NOT NULL AUTO_INCREMENT,
        `drive_train_name` varchar(255) NOT NULL,
        CONSTRAINT `PK_drive_trains` PRIMARY KEY (`drive_trains_id`)
    );

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20200128153729_Initial') THEN

    CREATE TABLE `engine_types` (
        `engine_types_id` int(11) NOT NULL AUTO_INCREMENT,
        `engine_type_name` varchar(255) NOT NULL,
        CONSTRAINT `PK_engine_types` PRIMARY KEY (`engine_types_id`)
    );

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20200128153729_Initial') THEN

    CREATE TABLE `epa_classes` (
        `epa_classes_id` int(11) NOT NULL AUTO_INCREMENT,
        `epa_class_name` varchar(45) NOT NULL,
        CONSTRAINT `PK_epa_classes` PRIMARY KEY (`epa_classes_id`)
    );

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20200128153729_Initial') THEN

    CREATE TABLE `makes` (
        `makes_id` int(11) NOT NULL AUTO_INCREMENT,
        `make_name` varchar(45) NOT NULL,
        `description` varchar(255) NULL,
        CONSTRAINT `PK_makes` PRIMARY KEY (`makes_id`)
    );

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20200128153729_Initial') THEN

    CREATE TABLE `transmissions` (
        `transmissions_id` int(11) NOT NULL AUTO_INCREMENT,
        `transmission_name` varchar(255) NOT NULL,
        CONSTRAINT `PK_transmissions` PRIMARY KEY (`transmissions_id`)
    );

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20200128153729_Initial') THEN

    CREATE TABLE `warranties` (
        `warranties_id` int(11) NOT NULL AUTO_INCREMENT,
        `warranty_name` varchar(45) NOT NULL,
        `description` longtext NULL,
        CONSTRAINT `PK_warranties` PRIMARY KEY (`warranties_id`)
    );

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20200128153729_Initial') THEN

    CREATE TABLE `category_pictures` (
        `category_pictures_id` int(11) NOT NULL AUTO_INCREMENT,
        `picture_name` varchar(45) NOT NULL,
        `description` varchar(255) NULL,
        `picture_url` varchar(255) NOT NULL,
        `fk_categories_id` int(11) NOT NULL,
        `picture_type` varchar(45) NOT NULL,
        CONSTRAINT `PK_category_pictures` PRIMARY KEY (`category_pictures_id`),
        CONSTRAINT `category_pictures_categories` FOREIGN KEY (`fk_categories_id`) REFERENCES `categories` (`categories_id`) ON DELETE RESTRICT
    );

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20200128153729_Initial') THEN

    CREATE TABLE `orders` (
        `orders_id` int(11) NOT NULL AUTO_INCREMENT,
        `order_number` int(11) NOT NULL,
        `fk_customers_id` int(11) NOT NULL,
        `order_date` datetime NOT NULL,
        `subtotal` decimal(19,2) NULL,
        `tax` decimal(19,2) NULL,
        `total` decimal(19,2) NULL,
        `notes` varchar(255) NULL,
        `invoice_url` varchar(255) NULL,
        CONSTRAINT `PK_orders` PRIMARY KEY (`orders_id`),
        CONSTRAINT `orders_customers` FOREIGN KEY (`fk_customers_id`) REFERENCES `customers` (`customers_id`) ON DELETE RESTRICT
    );

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20200128153729_Initial') THEN

    CREATE TABLE `colors` (
        `colors_id` int(11) NOT NULL AUTO_INCREMENT,
        `color_name` varchar(45) NOT NULL,
        `fk_makes_id` int(11) NULL,
        CONSTRAINT `PK_colors` PRIMARY KEY (`colors_id`),
        CONSTRAINT `colors_makes` FOREIGN KEY (`fk_makes_id`) REFERENCES `makes` (`makes_id`) ON DELETE RESTRICT
    );

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20200128153729_Initial') THEN

    CREATE TABLE `models` (
        `models_id` int(11) NOT NULL AUTO_INCREMENT,
        `fk_makes_id` int(11) NOT NULL,
        `model_name` varchar(45) NOT NULL,
        CONSTRAINT `PK_models` PRIMARY KEY (`models_id`),
        CONSTRAINT `makes_models` FOREIGN KEY (`fk_makes_id`) REFERENCES `makes` (`makes_id`) ON DELETE RESTRICT
    );

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20200128153729_Initial') THEN

    CREATE TABLE `products` (
        `products_id` int(11) NOT NULL AUTO_INCREMENT,
        `sku` varchar(45) NOT NULL,
        `upc` varchar(45) NULL,
        `description` varchar(255) NOT NULL,
        `long_description` longtext NULL,
        `fk_categories_id` int(11) NULL,
        `is_featured` bit(1) NULL,
        `cost` decimal(19,2) NULL,
        `sales_price` decimal(19,2) NULL,
        `quantity_in_stock` int(11) NULL,
        `trim` varchar(255) NULL,
        `msrp` decimal(19,2) NULL,
        `fk_makes_id` int(11) NOT NULL,
        `fk_models_id` int(11) NOT NULL,
        `year` int(11) NOT NULL,
        `horsepower` int(11) NULL,
        `gas_mileage` varchar(255) NULL,
        `fk_engine_types_id` int(11) NULL,
        `fk_epa_classes_id` int(11) NULL,
        `fk_drive_trains_id` int(11) NULL,
        `passengers` int(11) NULL,
        `passenger_doors` int(11) NULL,
        `fk_body_styles_id` int(11) NULL,
        `fk_transmissions_id` int(11) NULL,
        `base_weight` int(11) NULL,
        CONSTRAINT `PK_products` PRIMARY KEY (`products_id`),
        CONSTRAINT `body_styles_products` FOREIGN KEY (`fk_body_styles_id`) REFERENCES `body_styles` (`body_styles_id`) ON DELETE RESTRICT,
        CONSTRAINT `product_categories` FOREIGN KEY (`fk_categories_id`) REFERENCES `categories` (`categories_id`) ON DELETE RESTRICT,
        CONSTRAINT `drive_trains_products` FOREIGN KEY (`fk_drive_trains_id`) REFERENCES `drive_trains` (`drive_trains_id`) ON DELETE RESTRICT,
        CONSTRAINT `engine_types_products` FOREIGN KEY (`fk_engine_types_id`) REFERENCES `engine_types` (`engine_types_id`) ON DELETE RESTRICT,
        CONSTRAINT `epa_classes_products` FOREIGN KEY (`fk_epa_classes_id`) REFERENCES `epa_classes` (`epa_classes_id`) ON DELETE RESTRICT,
        CONSTRAINT `makes_products` FOREIGN KEY (`fk_makes_id`) REFERENCES `makes` (`makes_id`) ON DELETE RESTRICT,
        CONSTRAINT `models_products` FOREIGN KEY (`fk_models_id`) REFERENCES `models` (`models_id`) ON DELETE RESTRICT,
        CONSTRAINT `transmissions_products` FOREIGN KEY (`fk_transmissions_id`) REFERENCES `transmissions` (`transmissions_id`) ON DELETE RESTRICT
    );

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20200128153729_Initial') THEN

    CREATE TABLE `inventory_items` (
        `inventory_items_id` int(11) NOT NULL AUTO_INCREMENT,
        `fk_products_id` int(11) NOT NULL,
        `vin` varchar(100) NULL,
        `fk_colors_id` int(11) NULL,
        `cost` decimal(19,2) NULL,
        `sales_price` decimal(19,2) NULL,
        `mileage` int(11) NULL,
        `fk_warranties_id` int(11) NULL,
        `is_new` tinyint(4) NULL,
        CONSTRAINT `PK_inventory_items` PRIMARY KEY (`inventory_items_id`),
        CONSTRAINT `colors_inventory_Items` FOREIGN KEY (`fk_colors_id`) REFERENCES `colors` (`colors_id`) ON DELETE RESTRICT,
        CONSTRAINT `products_inventory_items` FOREIGN KEY (`fk_products_id`) REFERENCES `products` (`products_id`) ON DELETE RESTRICT,
        CONSTRAINT `warranties_inventory_items` FOREIGN KEY (`fk_warranties_id`) REFERENCES `warranties` (`warranties_id`) ON DELETE RESTRICT
    );

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20200128153729_Initial') THEN

    CREATE TABLE `order_details` (
        `order_details_id` int(11) NOT NULL AUTO_INCREMENT,
        `fk_orders_id` int(11) NOT NULL,
        `fk_products_id` int(11) NOT NULL,
        `fk_inventory_items_id` int(11) NOT NULL,
        `item_price` decimal(19,2) NOT NULL,
        `quantity` int(11) NOT NULL,
        `tax` decimal(19,2) NULL,
        `shipdate` datetime NULL,
        `notes` varchar(255) CHARACTER SET utf8mb4 NULL,
        CONSTRAINT `PK_order_details` PRIMARY KEY (`order_details_id`),
        CONSTRAINT `order_details_orders` FOREIGN KEY (`fk_orders_id`) REFERENCES `orders` (`orders_id`) ON DELETE RESTRICT,
        CONSTRAINT `order_details_products` FOREIGN KEY (`fk_products_id`) REFERENCES `products` (`products_id`) ON DELETE RESTRICT,
        CONSTRAINT `order_details_inventory_items` FOREIGN KEY (`fk_products_id`) REFERENCES `inventory_items` (`inventory_items_id`) ON DELETE RESTRICT
    );

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20200128153729_Initial') THEN

    CREATE TABLE `shopping_cart_records` (
        `shopping_cart_records_id` int(11) NOT NULL AUTO_INCREMENT,
        `fk_customers_id` int(11) NOT NULL,
        `fk_products_id` int(11) NOT NULL,
        `date_created` datetime NOT NULL,
        `quantity` int(11) NOT NULL,
        CONSTRAINT `PK_shopping_cart_records` PRIMARY KEY (`shopping_cart_records_id`),
        CONSTRAINT `shopping_customers` FOREIGN KEY (`fk_customers_id`) REFERENCES `customers` (`customers_id`) ON DELETE RESTRICT,
        CONSTRAINT `shopping_products` FOREIGN KEY (`fk_products_id`) REFERENCES `products` (`products_id`) ON DELETE RESTRICT
    );

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20200128153729_Initial') THEN

    CREATE TABLE `inventory_item_pictures` (
        `inventory_item_pictures_id` int(11) NOT NULL AUTO_INCREMENT,
        `fk_inventory_items_id` int(11) NOT NULL,
        `picture_name` varchar(45) NOT NULL,
        `picture_url` varchar(255) NOT NULL,
        `picture_width` int(11) NULL,
        `picture_height` int(11) NULL,
        `alternate_text` varchar(100) NULL,
        CONSTRAINT `PK_inventory_item_pictures` PRIMARY KEY (`inventory_item_pictures_id`),
        CONSTRAINT `inventory_items_inv_itmpic` FOREIGN KEY (`fk_inventory_items_id`) REFERENCES `inventory_items` (`inventory_items_id`) ON DELETE RESTRICT
    );

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20200128153729_Initial') THEN

    CREATE INDEX `idxbodystyle` ON `body_styles` (`body_style_name`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20200128153729_Initial') THEN

    CREATE INDEX `idxcategoryname` ON `categories` (`category_name`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20200128153729_Initial') THEN

    CREATE INDEX `category_pictures_categories_idx` ON `category_pictures` (`fk_categories_id`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20200128153729_Initial') THEN

    CREATE INDEX `idxpicturename` ON `category_pictures` (`picture_name`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20200128153729_Initial') THEN

    CREATE INDEX `idxcolorname` ON `colors` (`color_name`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20200128153729_Initial') THEN

    CREATE INDEX `colors_makes_idx` ON `colors` (`fk_makes_id`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20200128153729_Initial') THEN

    CREATE INDEX `idxaccountnumber` ON `customers` (`account_number`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20200128153729_Initial') THEN

    CREATE INDEX `idxalternate` ON `customers` (`alternate_number`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20200128153729_Initial') THEN

    CREATE INDEX `idxemail` ON `customers` (`email_address`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20200128153729_Initial') THEN

    CREATE INDEX `idxfullname` ON `customers` (`full_name`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20200128153729_Initial') THEN

    CREATE INDEX `idxphonenumber` ON `customers` (`phone_number`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20200128153729_Initial') THEN

    CREATE INDEX `idxdrivetrainname` ON `drive_trains` (`drive_train_name`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20200128153729_Initial') THEN

    CREATE INDEX `idxenginetypename` ON `engine_types` (`engine_type_name`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20200128153729_Initial') THEN

    CREATE INDEX `idxepaclassname` ON `epa_classes` (`epa_class_name`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20200128153729_Initial') THEN

    CREATE INDEX `inventory_items_inv_itmpic_idx` ON `inventory_item_pictures` (`fk_inventory_items_id`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20200128153729_Initial') THEN

    CREATE INDEX `idxpicturename` ON `inventory_item_pictures` (`picture_name`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20200128153729_Initial') THEN

    CREATE INDEX `colors_inventory_Items_idx` ON `inventory_items` (`fk_colors_id`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20200128153729_Initial') THEN

    CREATE INDEX `products_inventory_items_idx` ON `inventory_items` (`fk_products_id`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20200128153729_Initial') THEN

    CREATE INDEX `warranties_inventory_items_idx` ON `inventory_items` (`fk_warranties_id`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20200128153729_Initial') THEN

    CREATE INDEX `idxvin` ON `inventory_items` (`vin`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20200128153729_Initial') THEN

    CREATE INDEX `idxmakename` ON `makes` (`make_name`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20200128153729_Initial') THEN

    CREATE INDEX `makes_models_idx` ON `models` (`fk_makes_id`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20200128153729_Initial') THEN

    CREATE INDEX `idxmodelname` ON `models` (`model_name`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20200128153729_Initial') THEN

    CREATE INDEX `order_details_orders_idx` ON `order_details` (`fk_orders_id`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20200128153729_Initial') THEN

    CREATE INDEX `order_details_products_idx` ON `order_details` (`fk_products_id`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20200128153729_Initial') THEN

    CREATE INDEX `orders_customers_idx` ON `orders` (`fk_customers_id`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20200128153729_Initial') THEN

    CREATE INDEX `idxdescription` ON `products` (`description`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20200128153729_Initial') THEN

    CREATE INDEX `body_styles_products_idx` ON `products` (`fk_body_styles_id`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20200128153729_Initial') THEN

    CREATE INDEX `product_categories_idx` ON `products` (`fk_categories_id`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20200128153729_Initial') THEN

    CREATE INDEX `drive_trains_products_idx` ON `products` (`fk_drive_trains_id`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20200128153729_Initial') THEN

    CREATE INDEX `engine_types_products_idx` ON `products` (`fk_engine_types_id`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20200128153729_Initial') THEN

    CREATE INDEX `epa_classes_products_idx` ON `products` (`fk_epa_classes_id`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20200128153729_Initial') THEN

    CREATE INDEX `makes_products_idx` ON `products` (`fk_makes_id`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20200128153729_Initial') THEN

    CREATE INDEX `models_products_idx` ON `products` (`fk_models_id`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20200128153729_Initial') THEN

    CREATE INDEX `transmissions_products_idx` ON `products` (`fk_transmissions_id`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20200128153729_Initial') THEN

    CREATE INDEX `idxsku` ON `products` (`sku`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20200128153729_Initial') THEN

    CREATE INDEX `idxupc` ON `products` (`upc`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20200128153729_Initial') THEN

    CREATE INDEX `shopping_customers_idx` ON `shopping_cart_records` (`fk_customers_id`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20200128153729_Initial') THEN

    CREATE INDEX `shopping_products_idx` ON `shopping_cart_records` (`fk_products_id`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20200128153729_Initial') THEN

    CREATE INDEX `idxtransname` ON `transmissions` (`transmission_name`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20200128153729_Initial') THEN

    CREATE INDEX `idxwarrantyname` ON `warranties` (`warranty_name`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20200128153729_Initial') THEN

    INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
    VALUES ('20200128153729_Initial', '3.1.10');

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20200129172033_AddCategoryIdToModels') THEN

    ALTER TABLE `models` ADD `fk_categories_id` int(11) NOT NULL DEFAULT 0;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20200129172033_AddCategoryIdToModels') THEN

    ALTER TABLE `models` ADD `fk_categories_id` int(11) NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20200129172033_AddCategoryIdToModels') THEN

    CREATE INDEX `IX_models_fk_categories_id` ON `models` (`fk_categories_id`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20200129172033_AddCategoryIdToModels') THEN

    ALTER TABLE `models` ADD CONSTRAINT `categories_models` FOREIGN KEY (`fk_categories_id`) REFERENCES `categories` (`categories_id`) ON DELETE RESTRICT;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20200129172033_AddCategoryIdToModels') THEN

    INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
    VALUES ('20200129172033_AddCategoryIdToModels', '3.1.10');

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

