CREATE DATABASE  IF NOT EXISTS `iktato` /*!40100 DEFAULT CHARACTER SET utf8 */ /*!80016 DEFAULT ENCRYPTION='N' */;
USE `iktato`;
-- MySQL dump 10.13  Distrib 5.7.26, for Win32 (AMD64)
--
-- Host: 127.0.0.1    Database: iktato
-- ------------------------------------------------------
-- Server version	8.0.18

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Temporary table structure for view `createiktszam`
--

DROP TABLE IF EXISTS `createiktszam`;
/*!50001 DROP VIEW IF EXISTS `createiktszam`*/;
SET @saved_cs_client     = @@character_set_client;
SET character_set_client = utf8;
/*!50001 CREATE VIEW `createiktszam` AS SELECT 
 1 AS `id`,
 1 AS `generatedIktSzam`*/;
SET character_set_client = @saved_cs_client;

--
-- Table structure for table `csoport`
--

DROP TABLE IF EXISTS `csoport`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `csoport` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `name` varchar(100) NOT NULL,
  `telephely` int(11) NOT NULL,
  `deleted` tinyint(4) NOT NULL DEFAULT '0',
  `created_by` int(11) NOT NULL,
  `created_at` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `deleted_by` int(11) DEFAULT NULL,
  `deleted_at` timestamp NULL DEFAULT NULL,
  `shortname` varchar(3) NOT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `telephely_UNIQUE` (`telephely`,`name`) /*!80000 INVISIBLE */,
  UNIQUE KEY `fk_group_telephelyShortname_UNIQUE` (`telephely`,`shortname`),
  KEY `fk_group_premise_id_idx` (`telephely`),
  KEY `fk_group_user_id_idx` (`created_by`),
  KEY `fk_group_del_user_id_idx` (`deleted_by`),
  CONSTRAINT `fk_group_del_user_id` FOREIGN KEY (`deleted_by`) REFERENCES `user` (`id`),
  CONSTRAINT `fk_group_telephely_id` FOREIGN KEY (`telephely`) REFERENCES `telephely` (`id`),
  CONSTRAINT `fk_group_user_id` FOREIGN KEY (`created_by`) REFERENCES `user` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=11 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `csoport`
--

LOCK TABLES `csoport` WRITE;
/*!40000 ALTER TABLE `csoport` DISABLE KEYS */;
INSERT INTO `csoport` VALUES (1,'Munkaügy',1,0,2,'2020-05-13 10:58:35',NULL,NULL,'M'),(2,'Munkaügy',2,0,2,'2020-05-13 10:58:35',NULL,NULL,'M'),(3,'Szerződés',1,0,2,'2020-05-13 10:58:35',NULL,NULL,'SZ'),(4,'Szerződés',2,0,2,'2020-05-13 10:58:35',NULL,NULL,'SZ'),(5,'Ellátotti iratok',1,0,2,'2020-05-13 10:58:35',NULL,NULL,'E'),(6,'Ellátotti iratok',2,0,2,'2020-05-13 10:58:35',NULL,NULL,'E'),(7,'Egyéb',1,0,2,'2020-05-13 10:58:35',NULL,NULL,'V'),(8,'Munkaügy',3,0,2,'2020-05-13 10:58:35',NULL,NULL,'M'),(9,'Ellátotti iratok',3,0,2,'2020-05-13 10:58:35',NULL,NULL,'E'),(10,'Szerződés',3,0,2,'2020-05-13 10:58:35',NULL,NULL,'SZ');
/*!40000 ALTER TABLE `csoport` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Temporary table structure for view `currentyearikonyv`
--

DROP TABLE IF EXISTS `currentyearikonyv`;
/*!50001 DROP VIEW IF EXISTS `currentyearikonyv`*/;
SET @saved_cs_client     = @@character_set_client;
SET character_set_client = utf8;
/*!50001 CREATE VIEW `currentyearikonyv` AS SELECT 
 1 AS `ikonyvid`,
 1 AS `iktatoszam`,
 1 AS `targy`,
 1 AS `irany`,
 1 AS `hivszam`,
 1 AS `erkezett`,
 1 AS `hatido`,
 1 AS `telephely`,
 1 AS `deleted`,
 1 AS `szoveg`,
 1 AS `partnerid`,
 1 AS `partnername`,
 1 AS `partnerugyintezoid`,
 1 AS `partnerugyintezoname`,
 1 AS `ugyintezoid`,
 1 AS `ugyintezoname`,
 1 AS `csoportid`,
 1 AS `csoportname`,
 1 AS `csoportshortname`,
 1 AS `jellegid`,
 1 AS `jellegname`,
 1 AS `doccount`*/;
SET character_set_client = @saved_cs_client;

--
-- Table structure for table `doc`
--

DROP TABLE IF EXISTS `doc`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `doc` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `name` varchar(250) NOT NULL,
  `ext` varchar(10) NOT NULL,
  `path` text NOT NULL,
  `size` double NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `doc`
--

LOCK TABLES `doc` WRITE;
/*!40000 ALTER TABLE `doc` DISABLE KEYS */;
INSERT INTO `doc` VALUES (1,'1. Optimumszámítási modellek','pdf','C:\\Users\\user\\Desktop\\Debug\\Upload\\2020051219330879324cut41irrqd',0.291015625),(2,'dokumentum','docx','C:\\Users\\user\\Desktop\\Debug\\Upload\\2020051219333180642kk5raaewgy',3.95216178894043);
/*!40000 ALTER TABLE `doc` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `evek`
--

DROP TABLE IF EXISTS `evek`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `evek` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `year` smallint(6) NOT NULL,
  `active` tinyint(4) NOT NULL DEFAULT '0',
  `created_by` int(11) NOT NULL,
  `deactivated_by` int(11) DEFAULT NULL,
  `deactivated_at` timestamp NULL DEFAULT NULL,
  `created_at` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (`id`),
  UNIQUE KEY `year_UNIQUE` (`year`),
  KEY `fk_evek_user_id_idx` (`created_by`),
  KEY `fk_evek_deactivated_user_id_idx` (`deactivated_by`),
  CONSTRAINT `fk_evek_deactivated_user_id` FOREIGN KEY (`deactivated_by`) REFERENCES `user` (`id`),
  CONSTRAINT `fk_evek_user_id` FOREIGN KEY (`created_by`) REFERENCES `user` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `evek`
--

LOCK TABLES `evek` WRITE;
/*!40000 ALTER TABLE `evek` DISABLE KEYS */;
INSERT INTO `evek` VALUES (1,2020,1,1,NULL,NULL,'2020-05-13 10:58:35');
/*!40000 ALTER TABLE `evek` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `felh_telephely`
--

DROP TABLE IF EXISTS `felh_telephely`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `felh_telephely` (
  `user_id` int(11) NOT NULL,
  `telephely_id` int(11) NOT NULL,
  KEY `fk_users_premises_user_id_idx` (`user_id`),
  KEY `fk_users_premises_premise_id_idx` (`telephely_id`),
  CONSTRAINT `fk_users_telephely_premise_id` FOREIGN KEY (`telephely_id`) REFERENCES `telephely` (`id`),
  CONSTRAINT `fk_users_telephely_user_id` FOREIGN KEY (`user_id`) REFERENCES `user` (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `felh_telephely`
--

LOCK TABLES `felh_telephely` WRITE;
/*!40000 ALTER TABLE `felh_telephely` DISABLE KEYS */;
INSERT INTO `felh_telephely` VALUES (2,1),(2,2),(2,3),(2,4),(3,1),(3,3),(3,2),(3,4);
/*!40000 ALTER TABLE `felh_telephely` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `ikonyv`
--

DROP TABLE IF EXISTS `ikonyv`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `ikonyv` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `targy` varchar(100) NOT NULL,
  `hivszam` varchar(50) NOT NULL,
  `ugyintezo` int(11) NOT NULL,
  `partner` int(11) DEFAULT NULL,
  `created_at` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `created_by` int(11) NOT NULL,
  `deleted` tinyint(4) DEFAULT '0',
  `deleted_at` timestamp NULL DEFAULT NULL,
  `deleted_by` int(11) DEFAULT NULL,
  `telephely` int(11) NOT NULL,
  `csoport` int(11) NOT NULL,
  `jelleg` int(11) NOT NULL,
  `irany` tinyint(4) NOT NULL,
  `erkezett` datetime NOT NULL,
  `hatido` datetime NOT NULL,
  `valaszid` int(11) DEFAULT NULL,
  `iktatottid` int(11) DEFAULT NULL,
  `iktatoszam` varchar(45) NOT NULL,
  `szoveg` text,
  `yearid` int(11) DEFAULT NULL,
  PRIMARY KEY (`id`),
  KEY `fk_reg_user_id_idx` (`created_by`),
  KEY `fk_reg_admins_id_idx` (`ugyintezo`),
  KEY `fk_reg_premise_id_idx` (`telephely`),
  KEY `fk_reg_group_id_idx` (`csoport`),
  KEY `fk_reg_type_id_idx` (`jelleg`),
  KEY `fk_ikonyv_self_idx` (`valaszid`),
  KEY `fk_ikonyv_year_id_idx` (`yearid`),
  KEY `fk_ikonyv_del_user_id_idx` (`deleted_by`),
  KEY `fk_ikonyv_partner_idx` (`partner`),
  CONSTRAINT `fk_ikonyv_admins_id` FOREIGN KEY (`ugyintezo`) REFERENCES `ugyintezo` (`id`),
  CONSTRAINT `fk_ikonyv_csoport_id` FOREIGN KEY (`csoport`) REFERENCES `csoport` (`id`),
  CONSTRAINT `fk_ikonyv_del_user_id` FOREIGN KEY (`deleted_by`) REFERENCES `user` (`id`),
  CONSTRAINT `fk_ikonyv_jellege_id` FOREIGN KEY (`jelleg`) REFERENCES `jelleg` (`id`),
  CONSTRAINT `fk_ikonyv_partner` FOREIGN KEY (`partner`) REFERENCES `ikonyvpartnerei` (`id`),
  CONSTRAINT `fk_ikonyv_self` FOREIGN KEY (`valaszid`) REFERENCES `ikonyv` (`id`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `fk_ikonyv_telephely_id` FOREIGN KEY (`telephely`) REFERENCES `telephely` (`id`),
  CONSTRAINT `fk_ikonyv_user_id` FOREIGN KEY (`created_by`) REFERENCES `user` (`id`),
  CONSTRAINT `fk_ikonyv_year_id` FOREIGN KEY (`yearid`) REFERENCES `evek` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=26 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `ikonyv`
--

LOCK TABLES `ikonyv` WRITE;
/*!40000 ALTER TABLE `ikonyv` DISABLE KEYS */;
INSERT INTO `ikonyv` VALUES (1,'Vállalkozói szerződés videó rendszerre','',2,4,'2020-05-13 10:58:35',2,0,NULL,NULL,1,3,2,0,'2020-02-14 00:00:00','2020-02-28 00:00:00',NULL,1,'B-SZ/V/1/2020','',1),(2,'Kivitelezési tervezet videó rendszerre','',2,5,'2020-05-13 10:58:35',2,0,NULL,NULL,1,3,2,0,'2020-02-21 00:00:00','2020-02-28 00:00:00',1,1,'B-SZ/V/1-1/2020','',1),(3,'Munkaszerződés módosítása bankszámlaváltozás miatt','',6,6,'2020-05-13 10:58:35',2,0,NULL,NULL,1,1,3,0,'2020-03-12 00:00:00','2020-03-12 00:00:00',NULL,2,'B-M/V/2/2020','',1),(4,'Munkaszerződés módosítása lakcímváltozás miatt','',6,7,'2020-05-13 10:58:35',2,0,NULL,NULL,1,1,3,0,'2020-04-02 00:00:00','2020-04-02 00:00:00',NULL,3,'B-M/V/3/2020','',1),(5,'Munkaszerződés módosítása lakcímváltozás miatt','',6,8,'2020-05-13 10:58:35',2,1,'2020-05-12 07:41:52',2,1,1,3,0,'2020-04-02 00:00:00','2020-04-02 00:00:00',NULL,4,'B-M/V/4/2020','',1),(6,'Öregségi nyugdíj megállapítása','BE-02/L502/4425-8/2020',4,9,'2020-05-13 10:58:35',2,0,NULL,NULL,1,5,2,1,'2020-04-15 00:00:00','2020-04-15 00:00:00',NULL,5,'K-E/V/5/2020','',1),(7,'Válasz öregségi nyugdíj kérelemre','',4,10,'2020-05-13 10:58:35',2,1,'2020-05-12 07:59:35',2,1,5,2,0,'2020-04-23 00:00:00','2020-04-23 00:00:00',6,1,'K-E/V/5-1/2020','',1),(8,'Válasz öregségi nyugdíj kérelemre','',4,11,'2020-05-13 10:58:35',2,0,NULL,NULL,1,5,2,0,'2020-04-23 00:00:00','2020-04-23 00:00:00',6,2,'K-E/V/5-2/2020','',1),(9,'Munkaszerződés megszüntetése','',6,12,'2020-05-13 10:58:35',2,0,NULL,NULL,1,1,2,0,'2020-05-04 00:00:00','2020-05-04 00:00:00',NULL,6,'B-M/V/6/2020','',1),(10,'Földgáz szerződés','AKV01-GE-15/2020',2,13,'2020-05-13 10:58:35',2,0,NULL,NULL,1,3,2,0,'2020-01-06 00:00:00','2020-01-06 00:00:00',NULL,7,'B-SZ/V/7/2020','',1),(11,'Közigazgatási bírság','',2,14,'2020-05-13 10:58:35',2,0,NULL,NULL,1,7,2,0,'2020-04-01 00:00:00','2020-04-01 00:00:00',NULL,8,'B-V/V/8/2020','',1),(12,'Közfoglalkoztatási szerződés','',6,15,'2020-05-13 10:58:35',2,0,NULL,NULL,1,1,3,0,'2020-02-03 00:00:00','2020-02-03 00:00:00',NULL,9,'B-M/V/9/2020','',1),(13,'Munkaszerződés megszüntetése','',1,16,'2020-05-13 10:58:35',2,0,NULL,NULL,2,2,4,0,'2020-05-15 00:00:00','2020-05-15 00:00:00',NULL,1,'B-M/R/1/2020','',1),(14,'Munkaszerződés módosítása illetményváltozás miatt','',1,17,'2020-05-13 10:58:35',2,0,NULL,NULL,2,2,4,0,'2020-04-15 00:00:00','2020-04-15 00:00:00',NULL,2,'B-M/R/2/2020','',1),(15,'Kormányzati funkció megkérése','',1,18,'2020-05-13 10:58:35',2,0,NULL,NULL,2,2,1,1,'2020-03-12 00:00:00','2020-03-12 00:00:00',NULL,3,'K-M/R/3/2020','',1),(16,'Válasz kormányzati funkció beállításáról','',1,19,'2020-05-13 10:58:35',2,0,NULL,NULL,2,2,1,0,'2020-03-20 00:00:00','2020-03-20 00:00:00',15,1,'K-M/R/3-1/2020','',1),(17,'Megkeresés munkavállaló letiltásával kapcsolatban','112.V.395-5/2020',1,20,'2020-05-13 10:58:35',2,0,NULL,NULL,2,2,1,0,'2020-05-12 00:00:00','2020-05-12 00:00:00',NULL,4,'B-M/R/4/2020','',1),(18,'Winidoki karbantartási szerződés','',5,21,'2020-05-13 10:58:35',2,0,NULL,NULL,2,4,7,0,'2020-04-10 00:00:00','2020-04-10 00:00:00',NULL,5,'B-SZ/R/5/2020','',1),(19,'Hagyatéki jegyzőkönyv beküldése','',7,22,'2020-05-13 10:58:35',2,0,NULL,NULL,2,6,1,1,'2020-01-23 00:00:00','2020-01-23 00:00:00',NULL,6,'K-E/R/6/2020','',1),(20,'Hagyatéki eljárás elindításáról tájékoztatás','',7,23,'2020-05-13 10:58:35',2,0,NULL,NULL,2,6,1,0,'2020-02-13 00:00:00','2020-02-13 00:00:00',19,1,'K-E/R/6-1/2020','',1),(21,'Nyilatkozat nyugdíj melleti munkavégzésről','',1,24,'2020-05-13 10:58:35',2,0,NULL,NULL,2,2,4,0,'2020-04-13 00:00:00','2020-04-13 00:00:00',NULL,7,'B-M/R/7/2020','',1),(22,'Közszolgálati szerődések felülvizsgálata','DBRZRT-58-6/2020',8,25,'2020-05-13 10:58:35',2,0,NULL,NULL,2,4,1,0,'2020-02-25 00:00:00','2020-03-04 00:00:00',NULL,8,'B-SZ/R/8/2020','',1),(23,'Megbízás gépjármű vezetésére','',10,26,'2020-05-13 10:58:35',2,0,NULL,NULL,3,8,5,0,'2020-04-27 00:00:00','2020-04-27 00:00:00',NULL,1,'B-M/C/1/2020','',1),(24,'Fejlesztési szerződés elláttotti foglalkoztatásra','',10,27,'2020-05-13 10:58:35',2,0,NULL,NULL,3,9,5,0,'2020-03-11 00:00:00','2020-03-11 00:00:00',NULL,2,'B-E/C/2/2020','',1),(25,'Lift karabantartási szerződés','',9,28,'2020-05-13 10:58:35',2,0,NULL,NULL,3,10,6,0,'2020-01-10 00:00:00','2020-01-10 00:00:00',NULL,3,'B-SZ/C/3/2020','',1);
/*!40000 ALTER TABLE `ikonyv` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `ikonyvdocs`
--

DROP TABLE IF EXISTS `ikonyvdocs`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `ikonyvdocs` (
  `ikonyv_id` int(11) NOT NULL,
  `doc_id` int(11) NOT NULL,
  `deleted` tinyint(4) NOT NULL DEFAULT '0',
  `created_by` int(11) NOT NULL,
  `created_at` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `deleted_by` int(11) DEFAULT NULL,
  `deleted_at` timestamp NULL DEFAULT NULL,
  KEY `FK_registrationpdf_pdf_id_idx` (`doc_id`),
  KEY `FK_registrationpdf_reg_id_idx` (`ikonyv_id`),
  KEY `fk_ikonyvdocs_created_idx` (`created_by`),
  KEY `fk_ikonyvdoc_deleted_idx` (`deleted_by`),
  CONSTRAINT `FK_ikonyvdocs_pdf_id` FOREIGN KEY (`doc_id`) REFERENCES `doc` (`id`),
  CONSTRAINT `FK_ikonyvdocsf_reg_id` FOREIGN KEY (`ikonyv_id`) REFERENCES `ikonyv` (`id`),
  CONSTRAINT `fk_ikonyvdoc_deleted` FOREIGN KEY (`deleted_by`) REFERENCES `user` (`id`),
  CONSTRAINT `fk_ikonyvdocs_created` FOREIGN KEY (`created_by`) REFERENCES `user` (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `ikonyvdocs`
--

LOCK TABLES `ikonyvdocs` WRITE;
/*!40000 ALTER TABLE `ikonyvdocs` DISABLE KEYS */;
INSERT INTO `ikonyvdocs` VALUES (21,1,0,2,'2020-05-13 10:58:35',NULL,NULL),(21,2,0,2,'2020-05-13 10:58:35',NULL,NULL);
/*!40000 ALTER TABLE `ikonyvdocs` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `ikonyvpartnerei`
--

DROP TABLE IF EXISTS `ikonyvpartnerei`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `ikonyvpartnerei` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `partner_id` int(11) NOT NULL,
  `partnerugyintezo_id` int(11) DEFAULT NULL,
  PRIMARY KEY (`id`),
  KEY `fk_ikonyvpartnerei_partner_idx` (`partner_id`),
  KEY `fk_ikonyvpartnerei_ugyintezo_idx` (`partnerugyintezo_id`),
  CONSTRAINT `fk_ikonyvpartnerei_partner` FOREIGN KEY (`partner_id`) REFERENCES `partner` (`id`),
  CONSTRAINT `fk_ikonyvpartnerei_ugyintezo` FOREIGN KEY (`partnerugyintezo_id`) REFERENCES `partnerugyintezo` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=29 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `ikonyvpartnerei`
--

LOCK TABLES `ikonyvpartnerei` WRITE;
/*!40000 ALTER TABLE `ikonyvpartnerei` DISABLE KEYS */;
INSERT INTO `ikonyvpartnerei` VALUES (1,4,NULL),(2,4,NULL),(3,4,NULL),(4,4,NULL),(5,4,NULL),(6,5,NULL),(7,6,NULL),(8,6,NULL),(9,9,3),(10,7,NULL),(11,9,3),(12,8,NULL),(13,11,4),(14,12,6),(15,7,NULL),(16,13,NULL),(17,16,NULL),(18,17,9),(19,17,9),(20,18,NULL),(21,20,7),(22,22,10),(23,22,10),(24,21,NULL),(25,19,8),(26,23,NULL),(27,24,NULL),(28,25,11);
/*!40000 ALTER TABLE `ikonyvpartnerei` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `jelleg`
--

DROP TABLE IF EXISTS `jelleg`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `jelleg` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `name` varchar(100) NOT NULL,
  `deleted` tinyint(4) NOT NULL DEFAULT '0',
  `created_by` int(11) NOT NULL,
  `created_at` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `deleted_by` int(11) DEFAULT NULL,
  `deleted_at` timestamp NULL DEFAULT NULL,
  `telephely` int(11) NOT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `fk_jelleg_unqiueBytelephely` (`name`,`telephely`),
  KEY `fk_type_telephely_id` (`telephely`),
  KEY `fk_type_user_id_idx` (`created_by`),
  KEY `fk_type_del_user_id_idx` (`deleted_by`),
  CONSTRAINT `fk_type_del_user_id` FOREIGN KEY (`deleted_by`) REFERENCES `user` (`id`),
  CONSTRAINT `fk_type_telephely_id` FOREIGN KEY (`telephely`) REFERENCES `telephely` (`id`),
  CONSTRAINT `fk_type_user_id` FOREIGN KEY (`created_by`) REFERENCES `user` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=8 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `jelleg`
--

LOCK TABLES `jelleg` WRITE;
/*!40000 ALTER TABLE `jelleg` DISABLE KEYS */;
INSERT INTO `jelleg` VALUES (1,'levél',0,2,'2020-05-13 10:58:35',NULL,NULL,2),(2,'levél',0,2,'2020-05-13 10:58:35',NULL,NULL,1),(3,'munkaügyi irat',0,2,'2020-05-13 10:58:35',NULL,NULL,1),(4,'munkaügyi irat',0,2,'2020-05-13 10:58:35',NULL,NULL,2),(5,'munkaügyi irat',0,2,'2020-05-13 10:58:35',NULL,NULL,3),(6,'levél',0,2,'2020-05-13 10:58:35',NULL,NULL,3),(7,'e-mail',0,2,'2020-05-13 10:58:35',NULL,NULL,2);
/*!40000 ALTER TABLE `jelleg` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `partner`
--

DROP TABLE IF EXISTS `partner`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `partner` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `name` varchar(250) NOT NULL,
  `telephely` int(11) NOT NULL,
  `created_by` int(11) NOT NULL,
  `created_at` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `deleted` tinyint(4) NOT NULL DEFAULT '0',
  `deleted_by` int(11) DEFAULT NULL,
  `deleted_at` timestamp NULL DEFAULT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `FK_partner_uniqueBytelephely` (`name`,`telephely`,`deleted`),
  KEY `FK_partner_telephey_id_idx` (`telephely`),
  KEY `FK_partner_users_id_idx` (`created_by`),
  KEY `FK_partner_deleted_users_id_idx` (`deleted_by`),
  CONSTRAINT `FK_partner_deleted_users_id` FOREIGN KEY (`deleted_by`) REFERENCES `user` (`id`),
  CONSTRAINT `FK_partner_telephey_id` FOREIGN KEY (`telephely`) REFERENCES `telephely` (`id`),
  CONSTRAINT `FK_partner_users_id` FOREIGN KEY (`created_by`) REFERENCES `user` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=26 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `partner`
--

LOCK TABLES `partner` WRITE;
/*!40000 ALTER TABLE `partner` DISABLE KEYS */;
INSERT INTO `partner` VALUES (1,'E-on',2,2,'2020-05-13 10:58:35',0,NULL,NULL),(2,'Jelenker',2,2,'2020-05-13 10:58:35',0,NULL,NULL),(3,'Jelenker',1,2,'2020-05-13 10:58:35',0,NULL,NULL),(4,'Szalai László e.v.',1,2,'2020-05-13 10:58:35',0,NULL,NULL),(5,'Kovács Tünde',1,2,'2020-05-13 10:58:35',0,NULL,NULL),(6,'Molnár Zoltán',1,2,'2020-05-13 10:58:35',0,NULL,NULL),(7,'Takács Olga',1,2,'2020-05-13 10:58:35',0,NULL,NULL),(8,'Szűcs Margit',1,2,'2020-05-13 10:58:35',0,NULL,NULL),(9,'Békés Megyei Kormányhivatal',1,2,'2020-05-13 10:58:35',0,NULL,NULL),(10,'Ótemplomi Szeretetszolgálat',1,2,'2020-05-13 10:58:35',0,NULL,NULL),(11,'E-on',1,2,'2020-05-13 10:58:35',0,NULL,NULL),(12,'SZ.SZ.B.MI Rendőrfőkapitányság',1,2,'2020-05-13 10:58:35',0,NULL,NULL),(13,'Győri Dániel',2,2,'2020-05-13 10:58:35',0,NULL,NULL),(14,'Harmati Krisztina',2,2,'2020-05-13 10:58:35',0,NULL,NULL),(15,'Ótemplomi Szeretetszolgálat',2,2,'2020-05-13 10:58:35',0,NULL,NULL),(16,'Nagy Attila',2,2,'2020-05-13 10:58:35',0,NULL,NULL),(17,'Magyar Államkincstár',2,2,'2020-05-13 10:58:35',0,NULL,NULL),(18,'Mitykó Pál végrehajtó',2,2,'2020-05-13 10:58:35',0,NULL,NULL),(19,'DAREH BÁZIS Hulladékgazdálkodási Nonprofit Zrt.',2,2,'2020-05-13 10:58:35',0,NULL,NULL),(20,'Gekkosoft Kft.',2,2,'2020-05-13 10:58:35',0,NULL,NULL),(21,'Gulyás Pál',2,2,'2020-05-13 10:58:35',0,NULL,NULL),(22,'Szarvasi Polgármesteri Hivatal',2,2,'2020-05-13 10:58:35',0,NULL,NULL),(23,'Rácz György',3,2,'2020-05-13 10:58:35',0,NULL,NULL),(24,'Kozák Anikó',3,2,'2020-05-13 10:58:35',0,NULL,NULL),(25,'Kombi Plusz Kft.',3,2,'2020-05-13 10:58:35',0,NULL,NULL);
/*!40000 ALTER TABLE `partner` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `partnerugyintezo`
--

DROP TABLE IF EXISTS `partnerugyintezo`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `partnerugyintezo` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `name` varchar(255) NOT NULL,
  `partnerid` int(11) NOT NULL,
  `deleted` tinyint(4) DEFAULT '0',
  `created_by` int(11) NOT NULL,
  `created_at` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `deleted_by` int(11) DEFAULT NULL,
  `deleted_at` timestamp NULL DEFAULT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `partnerid_UNIQUE` (`partnerid`,`name`),
  KEY `FK_pugyintezo_partnerid_idx` (`partnerid`),
  KEY `FK_pugyintezo_createdid_idx` (`created_by`),
  KEY `FK_pugyintezo_deletedid_idx` (`deleted_by`),
  CONSTRAINT `FK_pugyintezo_createdid` FOREIGN KEY (`created_by`) REFERENCES `user` (`id`),
  CONSTRAINT `FK_pugyintezo_deletedid` FOREIGN KEY (`deleted_by`) REFERENCES `user` (`id`),
  CONSTRAINT `FK_pugyintezo_partnerid` FOREIGN KEY (`partnerid`) REFERENCES `partner` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=12 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `partnerugyintezo`
--

LOCK TABLES `partnerugyintezo` WRITE;
/*!40000 ALTER TABLE `partnerugyintezo` DISABLE KEYS */;
INSERT INTO `partnerugyintezo` VALUES (1,'Nagy Béla',2,0,2,'2020-05-13 10:58:35',NULL,NULL),(2,'Kis István',3,0,2,'2020-05-13 10:58:35',NULL,NULL),(3,'Gyányi Beáta',9,0,2,'2020-05-13 10:58:35',NULL,NULL),(4,'Farkas Petronella',11,0,2,'2020-05-13 10:58:35',NULL,NULL),(5,'Kohut Pál tű.százados',12,1,2,'2020-05-13 10:58:35',2,'2020-05-12 08:36:14'),(6,'Varga Viktória',12,0,2,'2020-05-13 10:58:35',NULL,NULL),(7,'Koppány Attila',20,0,2,'2020-05-13 10:58:35',NULL,NULL),(8,'Urbancsok Imre',19,0,2,'2020-05-13 10:58:35',NULL,NULL),(9,'Szaszákné Balázs Anett',17,0,2,'2020-05-13 10:58:35',NULL,NULL),(10,'Dr. Bangóczné Ordovics Katalin',22,0,2,'2020-05-13 10:58:35',NULL,NULL),(11,'Farkas János',25,0,2,'2020-05-13 10:58:35',NULL,NULL);
/*!40000 ALTER TABLE `partnerugyintezo` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Temporary table structure for view `prevyearikonyv`
--

DROP TABLE IF EXISTS `prevyearikonyv`;
/*!50001 DROP VIEW IF EXISTS `prevyearikonyv`*/;
SET @saved_cs_client     = @@character_set_client;
SET character_set_client = utf8;
/*!50001 CREATE VIEW `prevyearikonyv` AS SELECT 
 1 AS `ikonyvid`,
 1 AS `iktatoszam`,
 1 AS `irany`,
 1 AS `targy`,
 1 AS `hivszam`,
 1 AS `year`,
 1 AS `erkezett`,
 1 AS `hatido`,
 1 AS `telephely`,
 1 AS `deleted`,
 1 AS `szoveg`,
 1 AS `partnerid`,
 1 AS `partnername`,
 1 AS `partnerugyintezoid`,
 1 AS `partnerugyintezoname`,
 1 AS `ugyintezoid`,
 1 AS `ugyintezoname`,
 1 AS `csoportid`,
 1 AS `csoportname`,
 1 AS `csoportshortname`,
 1 AS `jellegid`,
 1 AS `jellegname`,
 1 AS `doccount`*/;
SET character_set_client = @saved_cs_client;

--
-- Table structure for table `privilege`
--

DROP TABLE IF EXISTS `privilege`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `privilege` (
  `id` tinyint(4) NOT NULL AUTO_INCREMENT,
  `privilege` varchar(45) NOT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `privilege_UNIQUE` (`privilege`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `privilege`
--

LOCK TABLES `privilege` WRITE;
/*!40000 ALTER TABLE `privilege` DISABLE KEYS */;
INSERT INTO `privilege` VALUES (1,'admin'),(2,'user');
/*!40000 ALTER TABLE `privilege` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `telephely`
--

DROP TABLE IF EXISTS `telephely`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `telephely` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `name` varchar(100) NOT NULL,
  `created_by` int(11) NOT NULL,
  `created_at` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `deleted_by` int(11) DEFAULT NULL,
  `deleted_at` timestamp NULL DEFAULT NULL,
  `deleted` tinyint(4) NOT NULL DEFAULT '0',
  PRIMARY KEY (`id`),
  UNIQUE KEY `name_UNIQUE` (`name`),
  KEY `fk_telephely_created_idx` (`created_by`),
  KEY `fk_telephely_deleted_idx` (`deleted_by`),
  CONSTRAINT `fk_telephely_created` FOREIGN KEY (`created_by`) REFERENCES `user` (`id`),
  CONSTRAINT `fk_telephely_deleted` FOREIGN KEY (`deleted_by`) REFERENCES `user` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `telephely`
--

LOCK TABLES `telephely` WRITE;
/*!40000 ALTER TABLE `telephely` DISABLE KEYS */;
INSERT INTO `telephely` VALUES (1,'Vajda',2,'2020-05-13 10:58:35',NULL,NULL,0),(2,'Rákóczi',2,'2020-05-13 10:58:35',NULL,NULL,0),(3,'Csabacsűd',2,'2020-05-13 10:58:35',NULL,NULL,0),(4,'Örménykút',2,'2020-05-13 10:58:35',NULL,NULL,0);
/*!40000 ALTER TABLE `telephely` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `ugyintezo`
--

DROP TABLE IF EXISTS `ugyintezo`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `ugyintezo` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `name` varchar(100) NOT NULL,
  `deleted` tinyint(4) NOT NULL DEFAULT '0',
  `created_by` int(11) NOT NULL,
  `created_at` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `deleted_by` int(11) DEFAULT NULL,
  `deleted_at` timestamp NULL DEFAULT NULL,
  `telephely` int(11) NOT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `name_UNIQUE` (`name`,`telephely`),
  KEY `fk_admins_premise_id_idx` (`telephely`),
  KEY `fk_admins_user_id_idx` (`created_by`),
  KEY `fk_csoport_del_user_id_idx` (`deleted_by`),
  CONSTRAINT `fk_admins_telephely_id` FOREIGN KEY (`telephely`) REFERENCES `telephely` (`id`),
  CONSTRAINT `fk_admins_user_id` FOREIGN KEY (`created_by`) REFERENCES `user` (`id`),
  CONSTRAINT `fk_csoport_del_user_id` FOREIGN KEY (`deleted_by`) REFERENCES `user` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=11 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `ugyintezo`
--

LOCK TABLES `ugyintezo` WRITE;
/*!40000 ALTER TABLE `ugyintezo` DISABLE KEYS */;
INSERT INTO `ugyintezo` VALUES (1,'Brachna Anita',0,2,'2020-05-13 10:58:36',NULL,NULL,2),(2,'Csík Attila',0,2,'2020-05-13 10:58:36',2,'2020-05-11 19:06:17',1),(4,'Tóthné Tótka Edit',0,2,'2020-05-13 10:58:36',NULL,NULL,1),(5,'Hajdu Ágnes',0,2,'2020-05-13 10:58:36',NULL,NULL,2),(6,'Bencsik Erika',0,2,'2020-05-13 10:58:36',NULL,NULL,1),(7,'Szebeni Gyöngyi',0,2,'2020-05-13 10:58:36',NULL,NULL,2),(8,'Mári Sándor',0,2,'2020-05-13 10:58:36',NULL,NULL,2),(9,'Skorka Zoltán',0,2,'2020-05-13 10:58:36',NULL,NULL,3),(10,'Pekarik Magdolna',0,2,'2020-05-13 10:58:36',NULL,NULL,3);
/*!40000 ALTER TABLE `ugyintezo` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `user`
--

DROP TABLE IF EXISTS `user`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `user` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `username` varchar(45) NOT NULL,
  `password` varchar(250) NOT NULL,
  `fullname` varchar(120) NOT NULL,
  `created_at` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `deleted` tinyint(4) NOT NULL DEFAULT '0',
  `deleted_at` timestamp NULL DEFAULT NULL,
  `deleted_by` int(11) DEFAULT NULL,
  `privilege` tinyint(4) NOT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `username_UNIQUE` (`username`),
  KEY `FK_users_privileges_id_idx` (`privilege`),
  CONSTRAINT `fk_users_privileges_id` FOREIGN KEY (`privilege`) REFERENCES `privilege` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `user`
--

LOCK TABLES `user` WRITE;
/*!40000 ALTER TABLE `user` DISABLE KEYS */;
INSERT INTO `user` VALUES (1,'admin','kiscicakiscica','Administrator','2020-05-13 10:58:35',0,NULL,NULL,1),(2,'pmisi','aee9686b812a98404cf726fff9517dbb27ce9592','Pekár Mihály','2020-05-13 10:58:35',0,NULL,NULL,1),(3,'noname','7c222fb2927d828af22f592134e8932480637c0d','No Name','2020-05-13 10:58:35',0,NULL,NULL,1);
/*!40000 ALTER TABLE `user` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Dumping routines for database 'iktato'
--
/*!50003 DROP FUNCTION IF EXISTS `getActiveYear` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8 */ ;
/*!50003 SET character_set_results = utf8 */ ;
/*!50003 SET collation_connection  = utf8_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'NO_AUTO_VALUE_ON_ZERO' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` FUNCTION `getActiveYear`() RETURNS int(11)
    DETERMINISTIC
BEGIN
declare evid int;
select id into evid from evek where active = 1;
return evid;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP FUNCTION IF EXISTS `getFreshIkonyvID` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8 */ ;
/*!50003 SET character_set_results = utf8 */ ;
/*!50003 SET collation_connection  = utf8_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'NO_AUTO_VALUE_ON_ZERO' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` FUNCTION `getFreshIkonyvID`() RETURNS int(11)
    DETERMINISTIC
BEGIN
declare currentid int;
select id into currentid from ikonyv where id = (select max(id) from ikonyv);
return currentid;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP FUNCTION IF EXISTS `getNextIkonyvIktatottId` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8 */ ;
/*!50003 SET character_set_results = utf8 */ ;
/*!50003 SET collation_connection  = utf8_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'NO_AUTO_VALUE_ON_ZERO' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` FUNCTION `getNextIkonyvIktatottId`(telephely_b int, valaszid_b int) RETURNS int(11)
    DETERMINISTIC
BEGIN
declare nextiktatottid int;
if(valaszid_b is null) then select IFNULL(MAX(iktatottid),0)+1 into nextiktatottid from ikonyv where telephely = telephely_b and valaszid is null and yearid = (select id from evek where active = 1);
else
select IFNULL(MAX(iktatottid),0)+1 into nextiktatottid from ikonyv where telephely = telephely_b and valaszid = valaszid_b and yearid = (select id from evek where active = 1);
end if;
return nextiktatottid;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `AddDoc` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8 */ ;
/*!50003 SET character_set_results = utf8 */ ;
/*!50003 SET collation_connection  = utf8_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'NO_AUTO_VALUE_ON_ZERO' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `AddDoc`(in name_b varchar(250),in ext_b varchar(10),in path_b text,in ikt_id_b int ,in created_by_b int,size_b double ,out newid_b int)
BEGIN
Declare newDocId int;
insert into doc(name,ext,path,size)value(name_b,ext_b,path_b,size_b);
SELECT LAST_INSERT_ID() into newid_b;
insert into ikonyvdocs(ikonyv_id,doc_id,created_by)value(ikt_id_b,newid_b,created_by_b);
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `AddGroup` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8 */ ;
/*!50003 SET character_set_results = utf8 */ ;
/*!50003 SET collation_connection  = utf8_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'NO_AUTO_VALUE_ON_ZERO' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `AddGroup`(in name_b varchar(100),in shortname_b varchar(3),in telephely_b int,in created_by_b int,out newid_b int)
BEGIN
insert into csoport(name,telephely,created_by,shortname)value(name_b,telephely_b,created_by_b,shortname_b) on duplicate key update deleted = if(name = values(name), 0, 1);
SELECT LAST_INSERT_ID() into newid_b;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `AddKind` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8 */ ;
/*!50003 SET character_set_results = utf8 */ ;
/*!50003 SET collation_connection  = utf8_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'NO_AUTO_VALUE_ON_ZERO' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `AddKind`(in name_b varchar(100),in telephely_b int, in created_by_b int , out newid_b int)
BEGIN
insert into jelleg(name,telephely,created_by)value(name_b,telephely_b,created_by_b) on duplicate key update deleted = if(name = values(name), 0, 1);
SELECT LAST_INSERT_ID() into newid_b;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `addPartner` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8 */ ;
/*!50003 SET character_set_results = utf8 */ ;
/*!50003 SET collation_connection  = utf8_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'NO_AUTO_VALUE_ON_ZERO' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `addPartner`(in created_by_b int, in telephely_b int, in name_b varchar(250), out newid_b int)
BEGIN
insert into partner(created_by,telephely,name)value(created_by_b,telephely_b,name_b) on duplicate key update deleted = if(name = values(name), 0, 1);
SELECT LAST_INSERT_ID() into newid_b;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `AddPartnerUgyintezo` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8 */ ;
/*!50003 SET character_set_results = utf8 */ ;
/*!50003 SET collation_connection  = utf8_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'NO_AUTO_VALUE_ON_ZERO' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `AddPartnerUgyintezo`(in name_b varchar(250),in created_by_b int,in partner_b int, out newid_b int)
BEGIN
insert into partnerugyintezo(name,partnerid,created_by)value(name_b,partner_b,created_by_b) on duplicate key update deleted = if(name = values(name), 0, 1);
SELECT LAST_INSERT_ID() into newid_b;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `AddRootIKonyv` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8 */ ;
/*!50003 SET character_set_results = utf8 */ ;
/*!50003 SET collation_connection  = utf8_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'NO_AUTO_VALUE_ON_ZERO' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `AddRootIKonyv`(targy_b varchar(100),hivszam_b varchar(50),ugyintezo_b int, partner_b int,partnerugyint_b int, created_by_b int, telephely_b int,csoport_b int,
jelleg_b int, irany_b tinyint,erkezett_b datetime,hatido_b datetime,szoveg_b text, out newid int, out iktszam varchar(45))
    DETERMINISTIC
BEGIN
DECLARE currentid int;
DECLARE iranya tinyint;
DECLARE currentYear smallint;
Declare nextIktatottId int;
Declare partner_i int;
SET currentYear := getActiveYear();
SET nextIktatottId = getNextIkonyvIktatottId(telephely_b,null);
if partnerugyint_b > 0 Then
insert into ikonyvpartnerei(partner_id,partnerugyintezo_id)value(partner_b,partnerugyint_b);
else
insert into ikonyvpartnerei(partner_id)value(partner_b);
end if;
select max(id) into partner_i from ikonyvpartnerei;
insert into ikonyv(targy,hivszam,ugyintezo,partner,created_by,telephely,csoport,jelleg,irany,erkezett,hatido,iktatoszam,szoveg,yearid,iktatottid)value
(targy_b,hivszam_b,ugyintezo_b,partner_i,created_by_b,telephely_b,csoport_b,jelleg_b,irany_b,erkezett_b,hatido_b,"empty...yet",szoveg_b,currentYear,nextIktatottId);
SET currentid := getFreshIkonyvID();
select generatedIktSzam into iktszam from createIktSzam where id = currentid;
update ikonyv set iktatoszam=iktszam where id = currentid;
SET newid = currentid;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `AddSubIKonyv` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8 */ ;
/*!50003 SET character_set_results = utf8 */ ;
/*!50003 SET collation_connection  = utf8_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'NO_AUTO_VALUE_ON_ZERO' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `AddSubIKonyv`(targy_b varchar(100),hivszam_b varchar(50),ugyintezo_b int,  partner_b int,partnerugyint_b int, created_by_b int, telephely_b int,csoport_b int,
jelleg_b int, irany_b tinyint,erkezett_b datetime,hatido_b datetime,szoveg_b text, parentid_b int, out newId int, out myiktszam varchar(255) )
    DETERMINISTIC
BEGIN
DECLARE nextiktatottid int;
DECLARE currentid int;
DECLARE iktszam varchar(255);
DECLARE currentYear int;
Declare partner_i int;
Declare yearToAddIktSzam int;
SET currentYear := getActiveYear();
select year into yearToAddIktSzam from evek where id = currentyear;
SET nextiktatottid := getNextIkonyvIktatottId(telephely_b,parentid_b);
if partnerugyint_b > 0 Then
insert into ikonyvpartnerei(partner_id,partnerugyintezo_id)value(partner_b,partnerugyint_b);
else
insert into ikonyvpartnerei(partner_id)value(partner_b);
end if;
select max(id) into partner_i from ikonyvpartnerei;
insert into ikonyv(targy,hivszam,ugyintezo,partner,created_by,telephely,csoport,jelleg,irany,erkezett,hatido,iktatottid,iktatoszam,szoveg,yearid,valaszid)value
(targy_b,hivszam_b,ugyintezo_b,partner_i,created_by_b,telephely_b,csoport_b,jelleg_b,irany_b,erkezett_b,hatido_b,nextiktatottid,"empty...yet",szoveg_b,currentYear,parentid_b);
SET currentid := getFreshIkonyvID();
select iktatoszam into iktszam from ikonyv where id = parentid_b;
SET myiktszam := CONCAT(SUBSTRING_INDEX(iktszam,'/',3),"-",nextiktatottid,"/",yearToAddIktSzam);
update ikonyv set iktatoszam=myIktSZam where id = currentid;
set newId =  currentid;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `AddTelephely` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8 */ ;
/*!50003 SET character_set_results = utf8 */ ;
/*!50003 SET collation_connection  = utf8_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'NO_AUTO_VALUE_ON_ZERO' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `AddTelephely`(in name_b varchar(100),in created_by_b int, out newid_b int)
BEGIN
insert into telephely(name,created_by)value(name_b,created_by_b) on duplicate key update deleted = if(name = values(name), 0, 1);
SELECT LAST_INSERT_ID() into newid_b;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `AddUgyintezo` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8 */ ;
/*!50003 SET character_set_results = utf8 */ ;
/*!50003 SET collation_connection  = utf8_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'NO_AUTO_VALUE_ON_ZERO' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `AddUgyintezo`(in name_b varchar(100), in telephely_b int,in created_by_b int , out newid_b int)
BEGIN
insert into ugyintezo(name,telephely,created_by)value(name_b,telephely_b,created_by_b) on duplicate key update deleted = if(name = values(name), 0, 1);
SELECT LAST_INSERT_ID() into newid_b;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `AddUser` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8 */ ;
/*!50003 SET character_set_results = utf8 */ ;
/*!50003 SET collation_connection  = utf8_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'NO_AUTO_VALUE_ON_ZERO' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `AddUser`(in username_b varchar(45),in password_b varchar(250), in fullname_b varchar(120), in privilege_b tinyint, out newid_b int)
BEGIN
insert into user(username,password,fullname,privilege)value(username_b,password_b,fullname_b,privilege_b) on duplicate key update deleted = if(username = values(username), 0, 1);
SELECT LAST_INSERT_ID() into newid_b;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `addUserToTelephely` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8 */ ;
/*!50003 SET character_set_results = utf8 */ ;
/*!50003 SET collation_connection  = utf8_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'NO_AUTO_VALUE_ON_ZERO' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `addUserToTelephely`(in user_id_b int, in telephely_b int)
BEGIN
insert into felh_telephely(user_id,telephely_id)value(user_id_b,telephely_b);
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `addyear` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8 */ ;
/*!50003 SET character_set_results = utf8 */ ;
/*!50003 SET collation_connection  = utf8_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'NO_AUTO_VALUE_ON_ZERO' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `addyear`(in year_b int, in created_by_b int, out newid_b int)
BEGIN
insert into evek(year,created_by,active)value(year_b,created_by_b,1);
SELECT LAST_INSERT_ID() into newid_b;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `AddYearAndActivate` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8 */ ;
/*!50003 SET character_set_results = utf8 */ ;
/*!50003 SET collation_connection  = utf8_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'NO_AUTO_VALUE_ON_ZERO' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `AddYearAndActivate`(in id_b int)
BEGIN
Declare  newyear int;
select max(year)+1 into newyear from evek;
update evek set active = 0, deactivated_at = current_timestamp(), deactivated_by = id_b where active = 1;
insert into evek(year,active,created_by)value(newyear,1,id_b);
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `DeleteDoc` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8 */ ;
/*!50003 SET character_set_results = utf8 */ ;
/*!50003 SET collation_connection  = utf8_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'NO_AUTO_VALUE_ON_ZERO' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `DeleteDoc`(in id_b int, in deleter_b int)
BEGIN
update ikonyvdocs set deleted = 1, deleted_by = deleter_b, deleted_at = current_timestamp() where doc_id = id_b;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `DelGroup` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8 */ ;
/*!50003 SET character_set_results = utf8 */ ;
/*!50003 SET collation_connection  = utf8_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'NO_AUTO_VALUE_ON_ZERO' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `DelGroup`(in id_b int, in deleter_b int)
BEGIN
update csoport set deleted = 1 ,deleted_by = deleter_b , deleted_at = current_timestamp() where id = id_b;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `DelIkonyv` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8 */ ;
/*!50003 SET character_set_results = utf8 */ ;
/*!50003 SET collation_connection  = utf8_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'NO_AUTO_VALUE_ON_ZERO' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `DelIkonyv`(in id_b int, in deleter_b int)
BEGIN
SET max_sp_recursion_depth=255;
call setDeletedByValaszID(id_b,deleter_b);
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `DelKind` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8 */ ;
/*!50003 SET character_set_results = utf8 */ ;
/*!50003 SET collation_connection  = utf8_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'NO_AUTO_VALUE_ON_ZERO' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `DelKind`(in id_b int, in deleter_b int)
BEGIN
update jelleg set deleted = 1 , deleted_by = deleter_b , deleted_at = current_timestamp() where id = id_b;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `delpartner` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8 */ ;
/*!50003 SET character_set_results = utf8 */ ;
/*!50003 SET collation_connection  = utf8_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'NO_AUTO_VALUE_ON_ZERO' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `delpartner`(in id_b int, in deleter_b int)
BEGIN
update partner set deleted = 1, deleted_by = deleter_b, deleted_at = current_timestamp() where id = id_b;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `delpartnerugyintezo` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8 */ ;
/*!50003 SET character_set_results = utf8 */ ;
/*!50003 SET collation_connection  = utf8_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'NO_AUTO_VALUE_ON_ZERO' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `delpartnerugyintezo`(in id_b int, in deleter_b int)
BEGIN
update partnerugyintezo set deleted = 1, deleted_by = deleter_b, deleted_at = current_timestamp() where id = id_b;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `DelTelephely` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8 */ ;
/*!50003 SET character_set_results = utf8 */ ;
/*!50003 SET collation_connection  = utf8_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'NO_AUTO_VALUE_ON_ZERO' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `DelTelephely`(in id_b int , in deleter_b int)
BEGIN
update telephely set deleted = 1 , deleted_by = deleter_b , deleted_at = current_timestamp() where id = id_b;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `DelUgyintezo` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8 */ ;
/*!50003 SET character_set_results = utf8 */ ;
/*!50003 SET collation_connection  = utf8_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'NO_AUTO_VALUE_ON_ZERO' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `DelUgyintezo`(in id_b int, in deleter_b int)
BEGIN
update ugyintezo set deleted = 1 , deleted_by = deleter_b ,deleted_at = current_timestamp() where id = id_b;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `DelUser` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8 */ ;
/*!50003 SET character_set_results = utf8 */ ;
/*!50003 SET collation_connection  = utf8_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'NO_AUTO_VALUE_ON_ZERO' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `DelUser`(in user_id_b int, in deleter_b int)
BEGIN
update user set deleted = 1 , deleted_by = deleter_b , deleted_at = current_timestamp() where id = user_id_b;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `getalltelephely` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8 */ ;
/*!50003 SET character_set_results = utf8 */ ;
/*!50003 SET collation_connection  = utf8_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'NO_AUTO_VALUE_ON_ZERO' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `getalltelephely`()
BEGIN
select * from telephely where deleted = 0;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `getdocsbyikonyvid` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8 */ ;
/*!50003 SET character_set_results = utf8 */ ;
/*!50003 SET collation_connection  = utf8_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'NO_AUTO_VALUE_ON_ZERO' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `getdocsbyikonyvid`(in ikonyvid_b int)
BEGIN
select d.id, d.name, d.ext, d.path,d.size from ikonyvdocs inner join doc d on (ikonyvdocs.doc_id = d.id) where ikonyvdocs.ikonyv_id = ikonyvid_b and deleted = 0;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `getEvek` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8 */ ;
/*!50003 SET character_set_results = utf8 */ ;
/*!50003 SET collation_connection  = utf8_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'NO_AUTO_VALUE_ON_ZERO' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `getEvek`()
BEGIN
select * from evek;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `getGroup` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8 */ ;
/*!50003 SET character_set_results = utf8 */ ;
/*!50003 SET collation_connection  = utf8_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'NO_AUTO_VALUE_ON_ZERO' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `getGroup`(in telephely_b int)
BEGIN
select * from csoport where telephely = telephely_b and deleted = 0;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `getIkonyvById` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8 */ ;
/*!50003 SET character_set_results = utf8 */ ;
/*!50003 SET collation_connection  = utf8_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'NO_AUTO_VALUE_ON_ZERO' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `getIkonyvById`(in id_b int)
BEGIN
select * from currentYearIkonyv where id = id_b and deleted = 0;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `getIkonyvek` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8 */ ;
/*!50003 SET character_set_results = utf8 */ ;
/*!50003 SET collation_connection  = utf8_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'NO_AUTO_VALUE_ON_ZERO' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `getIkonyvek`(in user_id_b int, in year_id_b int, in irany_b tinyint)
BEGIN
Declare currentyear_id int;
select id into currentyear_id from evek where active = 1;
if irany_b < 2 and irany_b > -1 then
if currentyear_id = year_id_b then
select * from currentYearIkonyv where irany = irany_b and telephely in (select telephely_id from felh_telephely where user_id = user_id_b) and deleted = 0;
else
select * from prevYearIkonyv where irany = irany_b and  year = year_id_b and telephely in (select telephely_id from felh_telephely where user_id = user_id_b) and deleted = 0;
end if;
else
if currentyear_id = year_id_b then
select * from currentYearIkonyv where  telephely in (select telephely_id from felh_telephely where user_id = user_id_b) and deleted = 0;
else
select * from prevYearIkonyv where  year = year_id_b and telephely in (select telephely_id from felh_telephely where user_id = user_id_b) and deleted = 0;
end if;
end if;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `getIkonyvekByTelephely` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8 */ ;
/*!50003 SET character_set_results = utf8 */ ;
/*!50003 SET collation_connection  = utf8_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'NO_AUTO_VALUE_ON_ZERO' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `getIkonyvekByTelephely`(in telephely_b int)
BEGIN
select * from currentYearIkonyv where telephely = telephely_b and deleted = 0;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `getJellegek` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8 */ ;
/*!50003 SET character_set_results = utf8 */ ;
/*!50003 SET collation_connection  = utf8_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'NO_AUTO_VALUE_ON_ZERO' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `getJellegek`(in telephely_b int)
BEGIN
select * from jelleg where telephely = telephely_b and deleted = 0;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `getpartners` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8 */ ;
/*!50003 SET character_set_results = utf8 */ ;
/*!50003 SET collation_connection  = utf8_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'NO_AUTO_VALUE_ON_ZERO' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `getpartners`(in telephely_b int)
BEGIN
select * from partner where telephely = telephely_b and deleted = 0;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `getpartnerugyintezok` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8 */ ;
/*!50003 SET character_set_results = utf8 */ ;
/*!50003 SET collation_connection  = utf8_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'NO_AUTO_VALUE_ON_ZERO' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `getpartnerugyintezok`(in partner_b int)
BEGIN
select id, name  from partnerugyintezo where partnerid = partner_b and deleted = 0;
end ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `getPrevYearIkonyv` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8 */ ;
/*!50003 SET character_set_results = utf8 */ ;
/*!50003 SET collation_connection  = utf8_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'NO_AUTO_VALUE_ON_ZERO' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `getPrevYearIkonyv`(in year_b int)
BEGIN
select * from prevYearIkonyv where year = year_b;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `getprivileges` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8 */ ;
/*!50003 SET character_set_results = utf8 */ ;
/*!50003 SET collation_connection  = utf8_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'NO_AUTO_VALUE_ON_ZERO' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `getprivileges`()
BEGIN
select * from privilege;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `getShortIkonyv` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8 */ ;
/*!50003 SET character_set_results = utf8 */ ;
/*!50003 SET collation_connection  = utf8_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'NO_AUTO_VALUE_ON_ZERO' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `getShortIkonyv`(in telephely_b int)
BEGIN
select ikonyvid, iktatoszam from currentYearIkonyv where telephely = telephely_b and deleted = 0;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `getTelephelyek` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8 */ ;
/*!50003 SET character_set_results = utf8 */ ;
/*!50003 SET collation_connection  = utf8_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'NO_AUTO_VALUE_ON_ZERO' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `getTelephelyek`(in user_b int)
BEGIN
select t.id as id, t.name as name from felh_telephely f inner join telephely t on (f.telephely_id = t.id)where f.user_id = user_b and deleted = 0;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `getUgyintezok` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8 */ ;
/*!50003 SET character_set_results = utf8 */ ;
/*!50003 SET collation_connection  = utf8_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'NO_AUTO_VALUE_ON_ZERO' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `getUgyintezok`(in telephely_b int)
BEGIN
select * from ugyintezo where telephely = telephely_b and deleted = 0;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `getUsers` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8 */ ;
/*!50003 SET character_set_results = utf8 */ ;
/*!50003 SET collation_connection  = utf8_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'NO_AUTO_VALUE_ON_ZERO' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `getUsers`()
BEGIN
select Distinct(u.id) as id,u.fullname as fullname, u.username as username, p.id as privilegeid, p.privilege as privilege from user u inner join privilege p on (u.privilege = p.id)   where deleted = 0 and u.id != 1;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `ModifyGroup` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8 */ ;
/*!50003 SET character_set_results = utf8 */ ;
/*!50003 SET collation_connection  = utf8_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'NO_AUTO_VALUE_ON_ZERO' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `ModifyGroup`(in id_b int,in name_b varchar(100), in sname_b varchar(3))
BEGIN
update csoport set name = name_b, shortname = sname_b where id = id_b;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `ModifyIkonyv` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8 */ ;
/*!50003 SET character_set_results = utf8 */ ;
/*!50003 SET collation_connection  = utf8_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'NO_AUTO_VALUE_ON_ZERO' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `ModifyIkonyv`(in id_b int, in targy_b varchar(100),in hivszam_b varchar(50),in ugyintezo_b int, in partner_b int,in partnerugyintezo_b int,
in erkezett_b datetime,in hatido_b datetime, in szoveg_b text, in jelleg_b int)
BEGIN
if partnerugyintezo_b > 0 then
update ikonyvpartnerei join ikonyv on(ikonyvpartnerei.id = ikonyv.partner) set partner_id = partner_b, partnerugyintezo_id = partnerugyintezo_b  where ikonyv.id = id_b;
else
update ikonyvpartnerei join ikonyv on(ikonyvpartnerei.id = ikonyv.partner) set partner_id = partner_b, partnerugyintezo_id = null  where ikonyv.id = id_b;
end if;
update ikonyv set jelleg = jelleg_b, targy = targy_b, hivszam = hivszam_b,ugyintezo = ugyintezo_b,erkezett = erkezett_b,hatido = hatido_b,szoveg = szoveg_b where id = id_b;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `ModifyKind` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8 */ ;
/*!50003 SET character_set_results = utf8 */ ;
/*!50003 SET collation_connection  = utf8_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'NO_AUTO_VALUE_ON_ZERO' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `ModifyKind`(in id_b int,in name_b varchar(100))
BEGIN
update jelleg set name = name_b where id = id_b;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `ModifyPartner` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8 */ ;
/*!50003 SET character_set_results = utf8 */ ;
/*!50003 SET collation_connection  = utf8_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'NO_AUTO_VALUE_ON_ZERO' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `ModifyPartner`(in id_b int,in name_b varchar(100))
BEGIN
update partner set name = name_b  where id = id_b;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `ModifyPartnerUgyintezo` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8 */ ;
/*!50003 SET character_set_results = utf8 */ ;
/*!50003 SET collation_connection  = utf8_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'NO_AUTO_VALUE_ON_ZERO' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `ModifyPartnerUgyintezo`(in id_b int,in name_b varchar(100))
BEGIN
update partnerugyintezo set name = name_b  where id = id_b;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `ModifyTelephely` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8 */ ;
/*!50003 SET character_set_results = utf8 */ ;
/*!50003 SET collation_connection  = utf8_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'NO_AUTO_VALUE_ON_ZERO' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `ModifyTelephely`(in id_b int, in name_b varchar(100))
BEGIN
update telephely set name = name_b where id = id_b;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `ModifyUgyintezo` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8 */ ;
/*!50003 SET character_set_results = utf8 */ ;
/*!50003 SET collation_connection  = utf8_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'NO_AUTO_VALUE_ON_ZERO' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `ModifyUgyintezo`(in id_b int,in name_b varchar(100))
BEGIN
update ugyintezo set name = name_b where id = id_b;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `modifyuser` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8 */ ;
/*!50003 SET character_set_results = utf8 */ ;
/*!50003 SET collation_connection  = utf8_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'NO_AUTO_VALUE_ON_ZERO' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `modifyuser`(in id_b int,in username_b varchar(250), in fullname_b varchar(250),in privilege_b int, in telephelyek_b varchar(30))
BEGIN
DECLARE _next TEXT DEFAULT NULL;
DECLARE _nextlen INT DEFAULT NULL;
DECLARE _value TEXT DEFAULT NULL;
update user set username = username_b, fullname = fullname_b, privilege = privilege_b where id = id_b;
delete from felh_telephely where user_id = id_b;
iterator:
LOOP
IF LENGTH(TRIM(telephelyek_b)) = 0 OR telephelyek_b IS NULL THEN
LEAVE iterator;
END IF;
SET _next = SUBSTRING_INDEX(telephelyek_b,',',1);
SET _nextlen = LENGTH(_next);
SET _value = TRIM(_next);
INSERT INTO felh_telephely(user_id,telephely_id) VALUES(id_b,_value);
SET telephelyek_b = INSERT(telephelyek_b,1,_nextlen + 1,'');
END LOOP;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `ModifyUserPassword` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8 */ ;
/*!50003 SET character_set_results = utf8 */ ;
/*!50003 SET collation_connection  = utf8_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'NO_AUTO_VALUE_ON_ZERO' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `ModifyUserPassword`(in user_id_b int, in password_b varchar(250))
BEGIN
update user set password = password_b where id = user_id_b;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `setDeletedByValaszID` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8 */ ;
/*!50003 SET character_set_results = utf8 */ ;
/*!50003 SET collation_connection  = utf8_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'NO_AUTO_VALUE_ON_ZERO' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `setDeletedByValaszID`(in ikonyv_b int, in user_id int)
BEGIN
declare hasChild int;
declare currentChild int;
select count(*) into hasChild from ikonyv where valaszid = ikonyv_b;
if (hasChild = 0) then
update ikonyv set deleted = 1, deleted_by = user_id, deleted_at = current_timestamp() where id = ikonyv_b;
else
while hasChild > 0 DO
select id into currentChild from ikonyv where valaszid = ikonyv_b and deleted != 1 LIMIT 1;
update ikonyv set deleted = 1, deleted_by = user_id, deleted_at = current_timestamp() where valaszid = currentChild;
call setDeletedByValaszID(currentChild,user_id);
SET hasChild = hasChild - 1;
END WHILE;
update ikonyv set deleted = 1, deleted_by = user_id, deleted_at = current_timestamp() where id = ikonyv_b;
end if;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `UserLogin` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8 */ ;
/*!50003 SET character_set_results = utf8 */ ;
/*!50003 SET collation_connection  = utf8_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'NO_AUTO_VALUE_ON_ZERO' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `UserLogin`(in username_b varchar(100), in password_b varchar(250))
BEGIN
select u.id as id, u.username as username, u.fullname as fullname, p.id as privilegeid, p.privilege as privilegename from user u
inner join privilege  p on (u.privilege = p.id) where username = username_b and password = password_b;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;

--
-- Final view structure for view `createiktszam`
--

/*!50001 DROP VIEW IF EXISTS `createiktszam`*/;
/*!50001 SET @saved_cs_client          = @@character_set_client */;
/*!50001 SET @saved_cs_results         = @@character_set_results */;
/*!50001 SET @saved_col_connection     = @@collation_connection */;
/*!50001 SET character_set_client      = utf8 */;
/*!50001 SET character_set_results     = utf8 */;
/*!50001 SET collation_connection      = utf8_general_ci */;
/*!50001 CREATE ALGORITHM=UNDEFINED */
/*!50013 DEFINER=`root`@`localhost` SQL SECURITY DEFINER */
/*!50001 VIEW `createiktszam` AS select `i`.`id` AS `id`,concat(convert(if((`i`.`irany` = 0),'B','K') using utf8),'-',`ics`.`shortname`,'/',substr(`t`.`name`,1,1),'/',`i`.`iktatottid`,convert(ifnull(concat('-',`i`.`valaszid`),'') using utf8),'/',`evek`.`year`) AS `generatedIktSzam` from (((((`ikonyv` `i` join `telephely` `t` on((`i`.`telephely` = `t`.`id`))) join `csoport` `ics` on((`ics`.`id` = `i`.`csoport`))) join `jelleg` `j` on((`j`.`id` = `i`.`jelleg`))) join `ugyintezo` `ugy` on((`ugy`.`id` = `i`.`ugyintezo`))) join `evek` on((`i`.`yearid` = `evek`.`id`))) */;
/*!50001 SET character_set_client      = @saved_cs_client */;
/*!50001 SET character_set_results     = @saved_cs_results */;
/*!50001 SET collation_connection      = @saved_col_connection */;

--
-- Final view structure for view `currentyearikonyv`
--

/*!50001 DROP VIEW IF EXISTS `currentyearikonyv`*/;
/*!50001 SET @saved_cs_client          = @@character_set_client */;
/*!50001 SET @saved_cs_results         = @@character_set_results */;
/*!50001 SET @saved_col_connection     = @@collation_connection */;
/*!50001 SET character_set_client      = utf8 */;
/*!50001 SET character_set_results     = utf8 */;
/*!50001 SET collation_connection      = utf8_general_ci */;
/*!50001 CREATE ALGORITHM=UNDEFINED */
/*!50013 DEFINER=`root`@`localhost` SQL SECURITY DEFINER */
/*!50001 VIEW `currentyearikonyv` AS select `i`.`id` AS `ikonyvid`,`i`.`iktatoszam` AS `iktatoszam`,`i`.`targy` AS `targy`,`i`.`irany` AS `irany`,`i`.`hivszam` AS `hivszam`,`i`.`erkezett` AS `erkezett`,`i`.`hatido` AS `hatido`,`i`.`telephely` AS `telephely`,`i`.`deleted` AS `deleted`,`i`.`szoveg` AS `szoveg`,`p`.`id` AS `partnerid`,`p`.`name` AS `partnername`,`pu`.`id` AS `partnerugyintezoid`,`pu`.`name` AS `partnerugyintezoname`,`u`.`id` AS `ugyintezoid`,`u`.`name` AS `ugyintezoname`,`cs`.`id` AS `csoportid`,`cs`.`name` AS `csoportname`,`cs`.`shortname` AS `csoportshortname`,`j`.`id` AS `jellegid`,`j`.`name` AS `jellegname`,ifnull(count(`docs`.`ikonyv_id`),0) AS `doccount` from (((((((`ikonyv` `i` join `ikonyvpartnerei` `ip` on((`i`.`partner` = `ip`.`id`))) join `partner` `p` on((`ip`.`partner_id` = `p`.`id`))) left join `partnerugyintezo` `pu` on((`ip`.`partnerugyintezo_id` = `pu`.`id`))) join `csoport` `cs` on((`i`.`csoport` = `cs`.`id`))) join `jelleg` `j` on((`i`.`jelleg` = `j`.`id`))) join `ugyintezo` `u` on((`i`.`ugyintezo` = `u`.`id`))) left join (select `ikonyvdocs`.`ikonyv_id` AS `ikonyv_id`,`ikonyvdocs`.`doc_id` AS `doc_id`,`ikonyvdocs`.`deleted` AS `deleted`,`ikonyvdocs`.`created_by` AS `created_by`,`ikonyvdocs`.`created_at` AS `created_at`,`ikonyvdocs`.`deleted_by` AS `deleted_by`,`ikonyvdocs`.`deleted_at` AS `deleted_at` from `ikonyvdocs` where (`ikonyvdocs`.`deleted` = 0)) `docs` on((`i`.`id` = `docs`.`ikonyv_id`))) where (`i`.`yearid` = (select `evek`.`id` from `evek` where (`evek`.`active` = 1))) group by `i`.`id` order by `i`.`iktatottid`,`i`.`valaszid` */;
/*!50001 SET character_set_client      = @saved_cs_client */;
/*!50001 SET character_set_results     = @saved_cs_results */;
/*!50001 SET collation_connection      = @saved_col_connection */;

--
-- Final view structure for view `prevyearikonyv`
--

/*!50001 DROP VIEW IF EXISTS `prevyearikonyv`*/;
/*!50001 SET @saved_cs_client          = @@character_set_client */;
/*!50001 SET @saved_cs_results         = @@character_set_results */;
/*!50001 SET @saved_col_connection     = @@collation_connection */;
/*!50001 SET character_set_client      = utf8 */;
/*!50001 SET character_set_results     = utf8 */;
/*!50001 SET collation_connection      = utf8_general_ci */;
/*!50001 CREATE ALGORITHM=UNDEFINED */
/*!50013 DEFINER=`root`@`localhost` SQL SECURITY DEFINER */
/*!50001 VIEW `prevyearikonyv` AS select `i`.`id` AS `ikonyvid`,`i`.`iktatoszam` AS `iktatoszam`,`i`.`irany` AS `irany`,`i`.`targy` AS `targy`,`i`.`hivszam` AS `hivszam`,`i`.`yearid` AS `year`,`i`.`erkezett` AS `erkezett`,`i`.`hatido` AS `hatido`,`i`.`telephely` AS `telephely`,`i`.`deleted` AS `deleted`,`i`.`szoveg` AS `szoveg`,`p`.`id` AS `partnerid`,`p`.`name` AS `partnername`,`pu`.`id` AS `partnerugyintezoid`,`pu`.`name` AS `partnerugyintezoname`,`u`.`id` AS `ugyintezoid`,`u`.`name` AS `ugyintezoname`,`cs`.`id` AS `csoportid`,`cs`.`name` AS `csoportname`,`cs`.`shortname` AS `csoportshortname`,`j`.`id` AS `jellegid`,`j`.`name` AS `jellegname`,ifnull(count(`docs`.`ikonyv_id`),0) AS `doccount` from (((((((`ikonyv` `i` join `ikonyvpartnerei` `ip` on((`i`.`partner` = `ip`.`id`))) join `partner` `p` on((`ip`.`partner_id` = `p`.`id`))) left join `partnerugyintezo` `pu` on((`ip`.`partnerugyintezo_id` = `pu`.`id`))) join `csoport` `cs` on((`i`.`csoport` = `cs`.`id`))) join `jelleg` `j` on((`i`.`jelleg` = `j`.`id`))) join `ugyintezo` `u` on((`i`.`ugyintezo` = `u`.`id`))) left join (select `ikonyvdocs`.`ikonyv_id` AS `ikonyv_id`,`ikonyvdocs`.`doc_id` AS `doc_id`,`ikonyvdocs`.`deleted` AS `deleted`,`ikonyvdocs`.`created_by` AS `created_by`,`ikonyvdocs`.`created_at` AS `created_at`,`ikonyvdocs`.`deleted_by` AS `deleted_by`,`ikonyvdocs`.`deleted_at` AS `deleted_at` from `ikonyvdocs` where (`ikonyvdocs`.`deleted` = 0)) `docs` on((`i`.`id` = `docs`.`ikonyv_id`))) group by `i`.`id` order by `i`.`iktatottid`,`i`.`valaszid` */;
/*!50001 SET character_set_client      = @saved_cs_client */;
/*!50001 SET character_set_results     = @saved_cs_results */;
/*!50001 SET collation_connection      = @saved_col_connection */;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2020-05-13 12:59:47
