-- MySqlBackup.NET 2.3.1
-- Dump Time: 2020-03-24 09:33:52
-- --------------------------------------
-- Server version 8.0.18 MySQL Community Server - GPL



DROP DATABASE `iktato`;
CREATE DATABASE IF NOT EXISTS `iktato` /*!40100 DEFAULT CHARACTER SET utf8 */ /*!80016 DEFAULT ENCRYPTION='N' */;
USE `iktato`;



/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8 */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;


-- 
-- Definition of doc
-- 

DROP TABLE IF EXISTS `doc`;
CREATE TABLE IF NOT EXISTS `doc` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `name` varchar(250) NOT NULL,
  `ext` varchar(10) NOT NULL,
  `path` text NOT NULL,
  `size` double NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=39 DEFAULT CHARSET=utf8;

-- 
-- Dumping data for table doc
-- 

/*!40000 ALTER TABLE `doc` DISABLE KEYS */;
INSERT INTO `doc`(`id`,`name`,`ext`,`path`,`size`) VALUES
(1,'December','xlsx','E:\\Repos\\szakdolgozat\\szakdolgozat\\C#\\IktatogRPCServer\\IktatogRPCServer\\bin\\Debug\\Upload\\2020. 03. 20.ydrh3b3jh3t',0.158188819885254),
(2,'December','xlsx','E:\\Repos\\szakdolgozat\\szakdolgozat\\C#\\IktatogRPCServer\\IktatogRPCServer\\bin\\Debug\\Upload\\2020. 03. 20.xopeauyzquq',0.158188819885254),
(3,'Kreditek','xlsx','E:\\Repos\\szakdolgozat\\szakdolgozat\\C#\\IktatogRPCServer\\IktatogRPCServer\\bin\\Debug\\Upload\\2020. 03. 20.kcbeddzdh3e',0.0144815444946289),
(4,'Órarend','xlsx','E:\\Repos\\szakdolgozat\\szakdolgozat\\C#\\IktatogRPCServer\\IktatogRPCServer\\bin\\Debug\\Upload\\2020. 03. 20.hnvfgkd4dxe',0.00896167755126953),
(5,'table-1','xls','E:\\Repos\\szakdolgozat\\szakdolgozat\\C#\\IktatogRPCServer\\IktatogRPCServer\\bin\\Debug\\Upload\\2020. 03. 20.cfbvrsqzvqz',0.078125),
(6,'December','xlsx','E:\\Repos\\szakdolgozat\\szakdolgozat\\C#\\IktatogRPCServer\\IktatogRPCServer\\bin\\Debug\\Upload\\2020. 03. 20.5c2fk2vgwoc',0.158188819885254),
(7,'Órarend','xlsx','E:\\Repos\\szakdolgozat\\szakdolgozat\\C#\\IktatogRPCServer\\IktatogRPCServer\\bin\\Debug\\Upload\\2020. 03. 20.okjd1r2wmgg',0.00896167755126953),
(8,'dokumentum','docx','E:\\Repos\\szakdolgozat\\szakdolgozat\\C#\\IktatogRPCServer\\IktatogRPCServer\\bin\\Debug\\Upload\\2020. 03. 20.dtla1vcyuxw',3.95216178894043),
(9,'LA_PHP_fejleszto','pdf','E:\\Repos\\szakdolgozat\\szakdolgozat\\C#\\IktatogRPCServer\\IktatogRPCServer\\bin\\Debug\\Upload\\2020. 03. 20.auz5qplwjjo',0.630611419677734),
(10,'December','xlsx','E:\\Repos\\szakdolgozat\\szakdolgozat\\C#\\IktatogRPCServer\\IktatogRPCServer\\bin\\Debug\\Upload\\2020. 03. 20.hmz1pmjcujs',0.158188819885254),
(11,'CV-Europass-20200120-Pekar-EN','pdf','E:\\Repos\\szakdolgozat\\szakdolgozat\\C#\\IktatogRPCServer\\IktatogRPCServer\\bin\\Debug\\Upload\\2020. 03. 20.2erlahfq3of',0.234562873840332),
(12,'1. Optimumszámítási modellek','pdf','E:\\Repos\\szakdolgozat\\szakdolgozat\\C#\\IktatogRPCServer\\IktatogRPCServer\\bin\\Debug\\Upload\\2020. 03. 20.2jykatdjlyy',0.291015625),
(13,'December','xlsx','E:\\Repos\\szakdolgozat\\szakdolgozat\\C#\\IktatogRPCServer\\IktatogRPCServer\\bin\\Debug\\Upload\\2020. 03. 20.4gkamjejoxt',0.158188819885254),
(14,'dokumentum','docx','E:\\Repos\\szakdolgozat\\szakdolgozat\\C#\\IktatogRPCServer\\IktatogRPCServer\\bin\\Debug\\Upload\\2020. 03. 20.krkgan2p4j0',3.95216178894043),
(15,'Funkspec_sablon','docx','E:\\Repos\\szakdolgozat\\szakdolgozat\\C#\\IktatogRPCServer\\IktatogRPCServer\\bin\\Debug\\Upload\\2020. 03. 20.v1pxam1ubch',0.0148916244506836),
(16,'1. Optimumszámítási modellek','pdf','E:\\Repos\\szakdolgozat\\szakdolgozat\\C#\\IktatogRPCServer\\IktatogRPCServer\\bin\\Debug\\Upload\\2020. 03. 21.3adcgjr4jjt',0),
(17,'1. Optimumszámítási modellek','pdf','E:\\Repos\\szakdolgozat\\szakdolgozat\\C#\\IktatogRPCServer\\IktatogRPCServer\\bin\\Debug\\Upload\\2020. 03. 21.4xafcpi054t',0),
(18,'December','xlsx','E:\\Repos\\szakdolgozat\\szakdolgozat\\C#\\IktatogRPCServer\\IktatogRPCServer\\bin\\Debug\\Upload\\2020. 03. 21.4lafw2mmq0b',0),
(19,'','','E:\\Repos\\szakdolgozat\\szakdolgozat\\C#\\IktatogRPCServer\\IktatogRPCServer\\bin\\Debug\\Upload\\2020. 03. 21.mxzljpih4qc',0),
(20,'1. Optimumszámítási modellek','pdf','E:\\Repos\\szakdolgozat\\szakdolgozat\\C#\\IktatogRPCServer\\IktatogRPCServer\\bin\\Debug\\Upload\\2020. 03. 21.fhrrsz3a5tq',0.271484375),
(21,'','','E:\\Repos\\szakdolgozat\\szakdolgozat\\C#\\IktatogRPCServer\\IktatogRPCServer\\bin\\Debug\\Upload\\2020. 03. 21.m4ifggys3yf',0),
(22,'1. Optimumszámítási modellek','pdf','E:\\Repos\\szakdolgozat\\szakdolgozat\\C#\\IktatogRPCServer\\IktatogRPCServer\\bin\\Debug\\Upload\\2020. 03. 21.gfig1x032ym',0.271484375),
(23,'fp_dc_setup_guide','pdf','E:\\Repos\\szakdolgozat\\szakdolgozat\\C#\\IktatogRPCServer\\IktatogRPCServer\\bin\\Debug\\Upload\\2020. 03. 21.t3brfnruhk5',4.875),
(24,'ef1710en','pdf','E:\\Repos\\szakdolgozat\\szakdolgozat\\C#\\IktatogRPCServer\\IktatogRPCServer\\bin\\Debug\\Upload\\2020. 03. 21.ptouwu1yyg4',4.51674175262451),
(25,'ef1710en','pdf','E:\\Repos\\szakdolgozat\\szakdolgozat\\C#\\IktatogRPCServer\\IktatogRPCServer\\bin\\Debug\\Upload\\2020. 03. 21.onlu5q3d2gy',4.51674175262451),
(26,'document','pdf','E:\\Repos\\szakdolgozat\\szakdolgozat\\C#\\IktatogRPCServer\\IktatogRPCServer\\bin\\Debug\\Upload\\2020. 03. 21.zlizxmihprn',10.0635747909546),
(27,'1. Optimumszámítási modellek','pdf','E:\\Repos\\szakdolgozat\\szakdolgozat\\C#\\IktatogRPCServer\\IktatogRPCServer\\bin\\Debug\\Upload\\2020. 03. 21.ztn2l5wirxr',0.271488189697266),
(28,'1. Optimumszámítási modellek','pdf','E:\\Repos\\szakdolgozat\\szakdolgozat\\C#\\IktatogRPCServer\\IktatogRPCServer\\bin\\Debug\\Upload\\2020. 03. 21.ufg4syopfmf',0.125),
(29,'1. Optimumszámítási modellek','pdf','E:\\Repos\\szakdolgozat\\szakdolgozat\\C#\\IktatogRPCServer\\IktatogRPCServer\\bin\\Debug\\Upload\\2020. 03. 21.aa2yedt4qnl',0.25),
(30,'1. Optimumszámítási modellek','pdf','E:\\Repos\\szakdolgozat\\szakdolgozat\\C#\\IktatogRPCServer\\IktatogRPCServer\\bin\\Debug\\Upload\\2020. 03. 21.njm14dxozlg',0.25),
(31,'1. Optimumszámítási modellek','pdf','E:\\Repos\\szakdolgozat\\szakdolgozat\\C#\\IktatogRPCServer\\IktatogRPCServer\\bin\\Debug\\Upload\\2020. 03. 21.ysphnaepage',0.291015625),
(32,'document','pdf','E:\\Repos\\szakdolgozat\\szakdolgozat\\C#\\IktatogRPCServer\\IktatogRPCServer\\bin\\Debug\\Upload\\2020. 03. 21.3k2mxjr5dzw',10.1239252090454),
(33,'ef1710en','pdf','E:\\Repos\\szakdolgozat\\szakdolgozat\\C#\\IktatogRPCServer\\IktatogRPCServer\\bin\\Debug\\Upload\\2020. 03. 21.4etyjmcyaan',4.54575824737549),
(34,'document','pdf','E:\\Repos\\szakdolgozat\\szakdolgozat\\C#\\IktatogRPCServer\\IktatogRPCServer\\bin\\Debug\\Upload\\2020. 03. 21.0mbaxbthvpm',10.1239252090454),
(35,'ef1710en','pdf','E:\\Repos\\szakdolgozat\\szakdolgozat\\C#\\IktatogRPCServer\\IktatogRPCServer\\bin\\Debug\\Upload\\2020. 03. 21.2233gr5lgvk',4.54575824737549),
(36,'December','xlsx','E:\\Repos\\szakdolgozat\\szakdolgozat\\C#\\IktatogRPCServer\\IktatogRPCServer\\bin\\Debug\\Upload\\2020. 03. 21.15w1y1ekatc',0.158188819885254),
(37,'1. Optimumszámítási modellek','pdf','E:\\Repos\\szakdolgozat\\szakdolgozat\\C#\\IktatogRPCServer\\IktatogRPCServer\\bin\\Debug\\Upload\\2020. 03. 21.a3solvb33r3',0.291015625),
(38,'document','pdf','E:\\Repos\\szakdolgozat\\szakdolgozat\\C#\\IktatogRPCServer\\IktatogRPCServer\\bin\\Debug\\Upload\\2020. 03. 21.cw2tlxgxpt2',10.1239252090454);
/*!40000 ALTER TABLE `doc` ENABLE KEYS */;

-- 
-- Definition of privilege
-- 

DROP TABLE IF EXISTS `privilege`;
CREATE TABLE IF NOT EXISTS `privilege` (
  `id` tinyint(4) NOT NULL AUTO_INCREMENT,
  `privilege` varchar(45) NOT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `privilege_UNIQUE` (`privilege`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8;

-- 
-- Dumping data for table privilege
-- 

/*!40000 ALTER TABLE `privilege` DISABLE KEYS */;
INSERT INTO `privilege`(`id`,`privilege`) VALUES
(1,'admin'),
(2,'user');
/*!40000 ALTER TABLE `privilege` ENABLE KEYS */;

-- 
-- Definition of user
-- 

DROP TABLE IF EXISTS `user`;
CREATE TABLE IF NOT EXISTS `user` (
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
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8;

-- 
-- Dumping data for table user
-- 

/*!40000 ALTER TABLE `user` DISABLE KEYS */;
INSERT INTO `user`(`id`,`username`,`password`,`fullname`,`deleted`,`deleted_at`,`deleted_by`,`privilege`) VALUES
(1,'admin ','kiscica','Administrator  ',0,NULL,NULL,1),
(2,'misi','19Lyahim0591','Pekár Mihály',0,NULL,NULL,1),
(3,'banita','Gerzson007','Brachna Anita',0,NULL,NULL,1),
(4,'ujfelh','Kiscicaa legjobbjelszo','Uj Felhasználó',1,'2020-03-23 16:39:33',1,2);
/*!40000 ALTER TABLE `user` ENABLE KEYS */;

-- 
-- Definition of csoport
-- 

DROP TABLE IF EXISTS `csoport`;
CREATE TABLE IF NOT EXISTS `csoport` (
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
) ENGINE=InnoDB AUTO_INCREMENT=10 DEFAULT CHARSET=utf8;

-- 
-- Dumping data for table csoport
-- 

/*!40000 ALTER TABLE `csoport` DISABLE KEYS */;
INSERT INTO `csoport`(`id`,`name`,`telephely`,`deleted`,`created_by`,`deleted_by`,`deleted_at`,`shortname`) VALUES
(1,'Munkaügy',1,1,1,NULL,NULL,'M'),
(2,'Munkaügy',2,0,1,NULL,NULL,'M'),
(3,'Konyha',2,0,1,NULL,NULL,'K'),
(4,'Munkaügy',3,0,1,NULL,NULL,'M'),
(5,'Kiscica2',3,1,1,1,'2020-03-20 11:25:41','c'),
(7,'kkkkk',1,0,1,NULL,NULL,'kk'),
(9,'ujcsopi2',2,1,1,1,'2020-03-23 16:36:25','S');
/*!40000 ALTER TABLE `csoport` ENABLE KEYS */;

-- 
-- Definition of evek
-- 

DROP TABLE IF EXISTS `evek`;
CREATE TABLE IF NOT EXISTS `evek` (
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
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8;

-- 
-- Dumping data for table evek
-- 

/*!40000 ALTER TABLE `evek` DISABLE KEYS */;
INSERT INTO `evek`(`id`,`year`,`active`,`created_by`,`deactivated_by`,`deactivated_at`) VALUES
(1,2020,0,1,1,'2020-03-23 16:45:20'),
(2,2021,1,1,NULL,NULL);
/*!40000 ALTER TABLE `evek` ENABLE KEYS */;

-- 
-- Definition of felh_telephely
-- 

DROP TABLE IF EXISTS `felh_telephely`;
CREATE TABLE IF NOT EXISTS `felh_telephely` (
  `user_id` int(11) NOT NULL,
  `telephely_id` int(11) NOT NULL,
  KEY `fk_users_premises_user_id_idx` (`user_id`),
  KEY `fk_users_premises_premise_id_idx` (`telephely_id`),
  CONSTRAINT `fk_users_telephely_premise_id` FOREIGN KEY (`telephely_id`) REFERENCES `telephely` (`id`),
  CONSTRAINT `fk_users_telephely_user_id` FOREIGN KEY (`user_id`) REFERENCES `user` (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- 
-- Dumping data for table felh_telephely
-- 

/*!40000 ALTER TABLE `felh_telephely` DISABLE KEYS */;
INSERT INTO `felh_telephely`(`user_id`,`telephely_id`) VALUES
(2,1),
(2,2),
(4,1),
(4,2),
(3,1),
(3,2),
(3,3),
(3,4),
(3,5),
(1,1),
(1,2),
(1,3),
(1,5);
/*!40000 ALTER TABLE `felh_telephely` ENABLE KEYS */;

-- 
-- Definition of ikonyv
-- 

DROP TABLE IF EXISTS `ikonyv`;
CREATE TABLE IF NOT EXISTS `ikonyv` (
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
) ENGINE=InnoDB AUTO_INCREMENT=21 DEFAULT CHARSET=utf8;

-- 
-- Dumping data for table ikonyv
-- 

/*!40000 ALTER TABLE `ikonyv` DISABLE KEYS */;
INSERT INTO `ikonyv`(`id`,`targy`,`hivszam`,`ugyintezo`,`partner`,`created_by`,`deleted`,`deleted_at`,`deleted_by`,`telephely`,`csoport`,`jelleg`,`irany`,`erkezett`,`hatido`,`valaszid`,`iktatottid`,`iktatoszam`,`szoveg`,`yearid`) VALUES
(1,'Első iktatás','',1,1,1,1,'2020-03-20 10:21:03',1,1,1,1,0,'2020-03-20 00:00:00','2020-03-20 00:00:00',NULL,1,'B-M/V/1/2020','',1),
(2,'Második iktatás','',1,2,1,1,'2020-03-20 10:21:03',1,1,1,1,0,'2020-03-20 00:00:00','2020-03-20 00:00:00',1,1,'B-M/V/1-1/2020','',1),
(3,'Első Rákóczi iktatás','',2,3,1,0,NULL,NULL,2,3,3,0,'2020-03-20 00:00:00','2020-03-20 00:00:00',NULL,1,'B-K/R/1/2020','',1),
(4,'Harmadik vajda iktatás','',1,4,1,1,'2020-03-20 10:21:03',1,1,1,1,0,'2020-03-20 00:00:00','2020-03-20 00:00:00',2,1,'B-M/V/1-1-1/2020','',1),
(5,'Negyedik vajda iktatás','',1,5,1,1,'2020-03-20 10:21:03',1,1,1,2,0,'2020-03-20 00:00:00','2020-03-20 00:00:00',2,2,'B-M/V/1-1-2/2020','',1),
(6,'Iktatás aminek vannak dokumentumai','',1,6,1,1,'2020-03-20 10:19:23',1,1,1,1,0,'2020-03-20 00:00:00','2020-03-20 00:00:00',NULL,2,'B-M/V/2/2020','',1),
(7,'Kimenő','',1,7,1,0,NULL,NULL,1,1,1,1,'2020-03-20 00:00:00','2020-03-20 00:00:00',NULL,3,'K-M/V/3/2020','',1),
(8,'Mosogató javítása4','asdfghjb:Áj',2,8,1,0,NULL,NULL,2,3,4,1,'2020-03-20 00:00:00','2020-03-20 00:00:00',NULL,2,'K-K/R/2/2020','sdfghjkmliujzdrsasdcjk',1),
(9,'Mosogató számla','',2,9,1,1,'2020-03-22 16:20:25',1,2,3,3,0,'2020-03-20 00:00:00','2020-03-20 00:00:00',8,1,'K-K/R/2-1/2020','',1),
(10,'Anita','',1,10,1,0,NULL,NULL,1,1,1,1,'2020-03-20 00:00:00','2020-03-20 00:00:00',NULL,4,'K-M/V/4/2020','',1),
(11,'fghjklé','',1,11,1,1,'2020-03-20 20:23:34',1,1,1,1,0,'2020-03-20 00:00:00','2020-03-20 00:00:00',10,1,'K-M/V/4-1/2020','',1),
(12,'fghjklé','',1,12,1,1,'2020-03-20 20:23:34',1,1,1,1,0,'2020-03-20 00:00:00','2020-03-20 00:00:00',11,1,'K-M/V/4-1-1/2020','',1),
(13,'fghjklé','',1,13,1,1,'2020-03-20 20:23:34',1,1,1,1,0,'2020-03-20 00:00:00','2020-03-20 00:00:00',11,2,'K-M/V/4-1-2/2020','',1),
(14,'fghjklé','',1,14,1,1,'2020-03-20 20:23:34',1,1,1,1,0,'2020-03-20 00:00:00','2020-03-20 00:00:00',11,3,'K-M/V/4-1-3/2020','',1),
(15,'fghjklé','',1,15,1,1,'2020-03-20 20:23:34',1,1,1,1,0,'2020-03-20 00:00:00','2020-03-20 00:00:00',11,4,'K-M/V/4-1-4/2020','',1),
(16,'fghjklé','',1,16,1,1,'2020-03-20 20:23:34',1,1,1,1,0,'2020-03-20 00:00:00','2020-03-20 00:00:00',11,5,'K-M/V/4-1-5/2020','',1),
(17,'fghjklé','',1,17,1,1,'2020-03-20 20:23:34',1,1,1,1,0,'2020-03-20 00:00:00','2020-03-20 00:00:00',11,6,'K-M/V/4-1-6/2020','',1),
(18,'fghjklé','',1,18,1,1,'2020-03-20 20:23:34',1,1,1,1,0,'2020-03-20 00:00:00','2020-03-20 00:00:00',16,1,'K-M/V/4-1-5-1/2020','',1),
(20,'2021','2021',1,20,1,0,NULL,NULL,1,7,1,0,'2020-03-23 00:00:00','2020-03-23 00:00:00',NULL,1,'B-kk/V/1/2021','',2);
/*!40000 ALTER TABLE `ikonyv` ENABLE KEYS */;

-- 
-- Definition of ikonyvdocs
-- 

DROP TABLE IF EXISTS `ikonyvdocs`;
CREATE TABLE IF NOT EXISTS `ikonyvdocs` (
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

-- 
-- Dumping data for table ikonyvdocs
-- 

/*!40000 ALTER TABLE `ikonyvdocs` DISABLE KEYS */;
INSERT INTO `ikonyvdocs`(`ikonyv_id`,`doc_id`,`deleted`,`created_by`,`deleted_by`,`deleted_at`) VALUES
(5,1,1,1,1,'2020-03-20 00:37:27'),
(6,2,0,1,NULL,NULL),
(6,3,0,1,NULL,NULL),
(6,4,0,1,NULL,NULL),
(6,5,0,1,NULL,NULL),
(3,6,1,1,1,'2020-03-20 14:13:03'),
(3,7,1,1,1,'2020-03-20 14:13:03'),
(3,8,1,1,1,'2020-03-20 12:40:14'),
(3,9,1,1,1,'2020-03-20 14:13:02'),
(8,10,1,1,1,'2020-03-22 16:20:31'),
(8,11,1,1,1,'2020-03-20 14:14:20'),
(8,12,1,1,1,'2020-03-22 16:20:30'),
(10,13,1,1,1,'2020-03-20 20:19:32'),
(10,14,1,1,1,'2020-03-20 20:19:31'),
(10,15,1,1,1,'2020-03-22 16:20:03'),
(9,16,1,1,1,'2020-03-21 11:19:03'),
(9,17,1,1,1,'2020-03-21 11:19:03'),
(9,18,1,1,1,'2020-03-21 11:19:02'),
(9,20,1,1,1,'2020-03-21 11:43:53'),
(3,22,1,1,1,'2020-03-22 16:20:09'),
(3,23,1,1,1,'2020-03-22 16:20:09'),
(3,24,1,1,1,'2020-03-22 16:20:09'),
(3,25,1,1,1,'2020-03-22 16:20:10'),
(3,26,1,1,1,'2020-03-22 16:20:10'),
(9,27,1,1,1,'2020-03-21 11:44:33'),
(9,28,1,1,1,'2020-03-21 11:51:24'),
(9,29,1,1,1,'2020-03-21 12:01:07'),
(9,30,1,1,1,'2020-03-21 12:05:05'),
(9,31,1,1,1,'2020-03-21 12:48:42'),
(9,32,1,1,1,'2020-03-21 12:48:42'),
(9,33,1,1,1,'2020-03-21 12:48:43'),
(9,34,1,1,1,'2020-03-21 12:50:14'),
(9,35,1,1,1,'2020-03-21 12:50:14'),
(9,36,1,1,1,'2020-03-21 12:50:13'),
(9,37,0,1,NULL,NULL),
(9,38,0,1,NULL,NULL);
/*!40000 ALTER TABLE `ikonyvdocs` ENABLE KEYS */;

-- 
-- Definition of jelleg
-- 

DROP TABLE IF EXISTS `jelleg`;
CREATE TABLE IF NOT EXISTS `jelleg` (
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
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8;

-- 
-- Dumping data for table jelleg
-- 

/*!40000 ALTER TABLE `jelleg` DISABLE KEYS */;
INSERT INTO `jelleg`(`id`,`name`,`deleted`,`created_by`,`deleted_by`,`deleted_at`,`telephely`) VALUES
(1,'Levél',0,1,NULL,NULL,1),
(2,'E-mail2',1,1,1,'2020-03-20 11:16:20',1),
(3,'Levél2',0,1,NULL,NULL,2),
(4,'Valami',0,1,NULL,NULL,2),
(5,'meowka2',1,1,1,'2020-03-23 16:26:30',2);
/*!40000 ALTER TABLE `jelleg` ENABLE KEYS */;

-- 
-- Definition of partner
-- 

DROP TABLE IF EXISTS `partner`;
CREATE TABLE IF NOT EXISTS `partner` (
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
) ENGINE=InnoDB AUTO_INCREMENT=12 DEFAULT CHARSET=utf8;

-- 
-- Dumping data for table partner
-- 

/*!40000 ALTER TABLE `partner` DISABLE KEYS */;
INSERT INTO `partner`(`id`,`name`,`telephely`,`created_by`,`deleted`,`deleted_by`,`deleted_at`) VALUES
(1,'EKE',1,1,0,NULL,NULL),
(2,'Jelenker',1,1,0,NULL,NULL),
(3,'Jelenker',2,1,0,NULL,NULL),
(4,'ssss',3,1,1,1,'2020-03-20 11:17:01'),
(5,'ssssssss',3,1,1,1,'2020-03-20 11:17:00'),
(6,'sss',2,1,1,1,'2020-03-20 11:16:57'),
(7,'sss',1,1,1,1,'2020-03-20 11:17:09'),
(8,'kiscica',3,1,0,NULL,NULL),
(10,'Kiscica',2,1,1,1,'2020-03-20 11:22:36'),
(11,'kiscica2',2,1,1,1,'2020-03-23 16:26:54');
/*!40000 ALTER TABLE `partner` ENABLE KEYS */;

-- 
-- Definition of partnerugyintezo
-- 

DROP TABLE IF EXISTS `partnerugyintezo`;
CREATE TABLE IF NOT EXISTS `partnerugyintezo` (
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
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8;

-- 
-- Dumping data for table partnerugyintezo
-- 

/*!40000 ALTER TABLE `partnerugyintezo` DISABLE KEYS */;
INSERT INTO `partnerugyintezo`(`id`,`name`,`partnerid`,`deleted`,`created_by`,`deleted_by`,`deleted_at`) VALUES
(1,'Balla Tamás',1,0,1,NULL,NULL),
(2,'Meow2',3,1,1,1,'2020-03-20 11:23:47'),
(3,'Mári Sándor',2,0,1,NULL,NULL),
(4,'scsddadssdadsasadsadsda',1,1,1,1,'2020-03-23 16:27:27'),
(5,'asddsadsd',2,0,1,NULL,NULL);
/*!40000 ALTER TABLE `partnerugyintezo` ENABLE KEYS */;

-- 
-- Definition of ikonyvpartnerei
-- 

DROP TABLE IF EXISTS `ikonyvpartnerei`;
CREATE TABLE IF NOT EXISTS `ikonyvpartnerei` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `partner_id` int(11) NOT NULL,
  `partnerugyintezo_id` int(11) DEFAULT NULL,
  PRIMARY KEY (`id`),
  KEY `fk_ikonyvpartnerei_partner_idx` (`partner_id`),
  KEY `fk_ikonyvpartnerei_ugyintezo_idx` (`partnerugyintezo_id`),
  CONSTRAINT `fk_ikonyvpartnerei_partner` FOREIGN KEY (`partner_id`) REFERENCES `partner` (`id`),
  CONSTRAINT `fk_ikonyvpartnerei_ugyintezo` FOREIGN KEY (`partnerugyintezo_id`) REFERENCES `partnerugyintezo` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=21 DEFAULT CHARSET=utf8;

-- 
-- Dumping data for table ikonyvpartnerei
-- 

/*!40000 ALTER TABLE `ikonyvpartnerei` DISABLE KEYS */;
INSERT INTO `ikonyvpartnerei`(`id`,`partner_id`,`partnerugyintezo_id`) VALUES
(1,1,NULL),
(2,1,1),
(3,3,NULL),
(4,1,NULL),
(5,1,1),
(6,1,1),
(7,1,NULL),
(8,3,NULL),
(9,3,NULL),
(10,1,1),
(11,1,NULL),
(12,1,NULL),
(13,1,NULL),
(14,1,NULL),
(15,1,NULL),
(16,1,NULL),
(17,1,NULL),
(18,1,NULL),
(19,1,1),
(20,1,NULL);
/*!40000 ALTER TABLE `ikonyvpartnerei` ENABLE KEYS */;

-- 
-- Definition of telephely
-- 

DROP TABLE IF EXISTS `telephely`;
CREATE TABLE IF NOT EXISTS `telephely` (
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
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8;

-- 
-- Dumping data for table telephely
-- 

/*!40000 ALTER TABLE `telephely` DISABLE KEYS */;
INSERT INTO `telephely`(`id`,`name`,`created_by`,`deleted_by`,`deleted_at`,`deleted`) VALUES
(1,'Vajda',1,NULL,NULL,0),
(2,'Rákóczi',1,NULL,NULL,0),
(3,'Tessedik',1,NULL,NULL,0),
(4,'Örménykút',1,NULL,NULL,0),
(5,'KiscicaVáros2',1,1,'2020-03-23 16:33:46',1);
/*!40000 ALTER TABLE `telephely` ENABLE KEYS */;

-- 
-- Definition of ugyintezo
-- 

DROP TABLE IF EXISTS `ugyintezo`;
CREATE TABLE IF NOT EXISTS `ugyintezo` (
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
) ENGINE=InnoDB AUTO_INCREMENT=13 DEFAULT CHARSET=utf8;

-- 
-- Dumping data for table ugyintezo
-- 

/*!40000 ALTER TABLE `ugyintezo` DISABLE KEYS */;
INSERT INTO `ugyintezo`(`id`,`name`,`deleted`,`created_by`,`deleted_by`,`deleted_at`,`telephely`) VALUES
(1,'Csík Attila',0,1,NULL,NULL,1),
(2,'Brachna Anita',0,1,NULL,NULL,2),
(4,'Pekár Mihály',0,1,NULL,NULL,2),
(5,'Kis Cica',1,1,1,'2020-03-20 11:15:51',2),
(6,'Meow Meo2',1,1,1,'2020-03-20 11:15:49',2),
(7,'Kis Cica',0,1,NULL,NULL,1),
(8,'Nagy Cica',0,1,NULL,NULL,3),
(9,'Kis NAgy Cica',1,1,1,'2020-03-20 11:15:54',3),
(10,'Nagyon nagy Macska',1,1,1,'2020-03-20 11:15:53',3),
(11,'Szaszák Ildikó2',1,1,1,'2020-03-20 20:21:15',2),
(12,'Meowka2',1,1,1,'2020-03-23 16:26:01',2);
/*!40000 ALTER TABLE `ugyintezo` ENABLE KEYS */;

-- 
-- Dumping functions
-- 

DROP FUNCTION IF EXISTS `getActiveYear`;
DELIMITER |
CREATE FUNCTION `getActiveYear`() RETURNS int(11)
    DETERMINISTIC
BEGIN
declare evid int;
select id into evid from evek where active = 1;
return evid;
END |
DELIMITER ;

DROP FUNCTION IF EXISTS `getFreshIkonyvID`;
DELIMITER |
CREATE FUNCTION `getFreshIkonyvID`() RETURNS int(11)
    DETERMINISTIC
BEGIN
declare currentid int;
select id into currentid from ikonyv where id = (select max(id) from ikonyv);
return currentid;
END |
DELIMITER ;

DROP FUNCTION IF EXISTS `getNextIkonyvIktatottId`;
DELIMITER |
CREATE FUNCTION `getNextIkonyvIktatottId`(telephely_b int, valaszid_b int) RETURNS int(11)
    DETERMINISTIC
BEGIN
declare nextiktatottid int;
if(valaszid_b is null) then select IFNULL(MAX(iktatottid),0)+1 into nextiktatottid from ikonyv where telephely = telephely_b and valaszid is null and yearid = (select id from evek where active = 1);
else 
select IFNULL(MAX(iktatottid),0)+1 into nextiktatottid from ikonyv where telephely = telephely_b and valaszid = valaszid_b and yearid = (select id from evek where active = 1); 
end if;
return nextiktatottid;
END |
DELIMITER ;

-- 
-- Dumping procedures
-- 

DROP PROCEDURE IF EXISTS `AddDoc`;
DELIMITER |
CREATE PROCEDURE `AddDoc`(in name_b varchar(250),in ext_b varchar(10),in path_b text,in ikt_id_b int ,in created_by_b int,size_b double ,out newid_b int)
BEGIN
	Declare newDocId int;
	insert into doc(name,ext,path,size)value(name_b,ext_b,path_b,size_b);
    SELECT LAST_INSERT_ID() into newid_b;
    insert into ikonyvdocs(ikonyv_id,doc_id,created_by)value(ikt_id_b,newid_b,created_by_b);
    
END |
DELIMITER ;

DROP PROCEDURE IF EXISTS `AddGroup`;
DELIMITER |
CREATE PROCEDURE `AddGroup`(in name_b varchar(100),in shortname_b varchar(3),in telephely_b int,in created_by_b int,out newid_b int)
BEGIN
	insert into csoport(name,telephely,created_by,shortname)value(name_b,telephely_b,created_by_b,shortname_b) on duplicate key update deleted = if(name = values(name), 0, 1);
	SELECT LAST_INSERT_ID() into newid_b;
    
END |
DELIMITER ;

DROP PROCEDURE IF EXISTS `AddKind`;
DELIMITER |
CREATE PROCEDURE `AddKind`(in name_b varchar(100),in telephely_b int, in created_by_b int , out newid_b int)
BEGIN
	insert into jelleg(name,telephely,created_by)value(name_b,telephely_b,created_by_b) on duplicate key update deleted = if(name = values(name), 0, 1);
	SELECT LAST_INSERT_ID() into newid_b;
END |
DELIMITER ;

DROP PROCEDURE IF EXISTS `addPartner`;
DELIMITER |
CREATE PROCEDURE `addPartner`(in created_by_b int, in telephely_b int, in name_b varchar(250), out newid_b int)
BEGIN
	insert into partner(created_by,telephely,name)value(created_by_b,telephely_b,name_b) on duplicate key update deleted = if(name = values(name), 0, 1);
    SELECT LAST_INSERT_ID() into newid_b;
END |
DELIMITER ;

DROP PROCEDURE IF EXISTS `AddPartnerUgyintezo`;
DELIMITER |
CREATE PROCEDURE `AddPartnerUgyintezo`(in name_b varchar(250),in created_by_b int,in partner_b int, out newid_b int)
BEGIN
	insert into partnerugyintezo(name,partnerid,created_by)value(name_b,partner_b,created_by_b) on duplicate key update deleted = if(name = values(name), 0, 1);
    SELECT LAST_INSERT_ID() into newid_b;
END |
DELIMITER ;

DROP PROCEDURE IF EXISTS `AddRootIKonyv`;
DELIMITER |
CREATE PROCEDURE `AddRootIKonyv`(targy_b varchar(100),hivszam_b varchar(50),ugyintezo_b int, partner_b int,partnerugyint_b int, created_by_b int, telephely_b int,csoport_b int,
jelleg_b int, irany_b tinyint,erkezett_b datetime,hatido_b datetime,szoveg_b text, out newid int, out iktszam varchar(45))
    DETERMINISTIC
BEGIN
-- Initalize data

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
-- Insert data into ikonyv table

insert into ikonyv(targy,hivszam,ugyintezo,partner,created_by,telephely,csoport,jelleg,irany,erkezett,hatido,iktatoszam,szoveg,yearid,iktatottid)value
(targy_b,hivszam_b,ugyintezo_b,partner_i,created_by_b,telephely_b,csoport_b,jelleg_b,irany_b,erkezett_b,hatido_b,"empty...yet",szoveg_b,currentYear,nextIktatottId);


SET currentid := getFreshIkonyvID();

select generatedIktSzam into iktszam from createIktSzam where id = currentid;
        
update ikonyv set iktatoszam=iktszam where id = currentid;

SET newid = currentid;
END |
DELIMITER ;

DROP PROCEDURE IF EXISTS `AddSubIKonyv`;
DELIMITER |
CREATE PROCEDURE `AddSubIKonyv`(targy_b varchar(100),hivszam_b varchar(50),ugyintezo_b int,  partner_b int,partnerugyint_b int, created_by_b int, telephely_b int,csoport_b int,
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

-- B-SZ/R/3/2020
SET myiktszam := CONCAT(SUBSTRING_INDEX(iktszam,'/',3),"-",nextiktatottid,"/",yearToAddIktSzam);        

update ikonyv set iktatoszam=myIktSZam where id = currentid;

set newId =  currentid;
END |
DELIMITER ;

DROP PROCEDURE IF EXISTS `AddTelephely`;
DELIMITER |
CREATE PROCEDURE `AddTelephely`(in name_b varchar(100),in created_by_b int, out newid_b int)
BEGIN
	insert into telephely(name,created_by)value(name_b,created_by_b) on duplicate key update deleted = if(name = values(name), 0, 1);
    SELECT LAST_INSERT_ID() into newid_b;
END |
DELIMITER ;

DROP PROCEDURE IF EXISTS `AddUgyintezo`;
DELIMITER |
CREATE PROCEDURE `AddUgyintezo`(in name_b varchar(100), in telephely_b int,in created_by_b int , out newid_b int)
BEGIN
	insert into ugyintezo(name,telephely,created_by)value(name_b,telephely_b,created_by_b) on duplicate key update deleted = if(name = values(name), 0, 1);
    SELECT LAST_INSERT_ID() into newid_b;
END |
DELIMITER ;

DROP PROCEDURE IF EXISTS `AddUser`;
DELIMITER |
CREATE PROCEDURE `AddUser`(in username_b varchar(45),in password_b varchar(250), in fullname_b varchar(120), in privilege_b tinyint, out newid_b int)
BEGIN
	insert into user(username,password,fullname,privilege)value(username_b,password_b,fullname_b,privilege_b) on duplicate key update deleted = if(username = values(username), 0, 1);
    SELECT LAST_INSERT_ID() into newid_b;
END |
DELIMITER ;

DROP PROCEDURE IF EXISTS `addUserToTelephely`;
DELIMITER |
CREATE PROCEDURE `addUserToTelephely`(in user_id_b int, in telephely_b int)
BEGIN
	insert into felh_telephely(user_id,telephely_id)value(user_id_b,telephely_b);
END |
DELIMITER ;

DROP PROCEDURE IF EXISTS `addyear`;
DELIMITER |
CREATE PROCEDURE `addyear`(in year_b int, in created_by_b int, out newid_b int)
BEGIN
	insert into evek(year,created_by)value(year_b,created_by_b);
    SELECT LAST_INSERT_ID() into newid_b;
END |
DELIMITER ;

DROP PROCEDURE IF EXISTS `AddYearAndActivate`;
DELIMITER |
CREATE PROCEDURE `AddYearAndActivate`(in id_b int)
BEGIN
Declare  newyear int;
	select max(year)+1 into newyear from evek;
	update evek set active = 0, deactivated_at = current_timestamp(), deactivated_by = id_b where active = 1;
    insert into evek(year,active,created_by)value(newyear,1,id_b);
END |
DELIMITER ;

DROP PROCEDURE IF EXISTS `DeleteDoc`;
DELIMITER |
CREATE PROCEDURE `DeleteDoc`(in id_b int, in deleter_b int)
BEGIN
     update ikonyvdocs set deleted = 1, deleted_by = deleter_b, deleted_at = current_timestamp() where doc_id = id_b; 
END |
DELIMITER ;

DROP PROCEDURE IF EXISTS `DelGroup`;
DELIMITER |
CREATE PROCEDURE `DelGroup`(in id_b int, in deleter_b int)
BEGIN
   update csoport set deleted = 1 ,deleted_by = deleter_b , deleted_at = current_timestamp() where id = id_b;
END |
DELIMITER ;

DROP PROCEDURE IF EXISTS `DelIkonyv`;
DELIMITER |
CREATE PROCEDURE `DelIkonyv`(in id_b int, in deleter_b int)
BEGIN
	SET max_sp_recursion_depth=255;
	call setDeletedByValaszID(id_b,deleter_b);
END |
DELIMITER ;

DROP PROCEDURE IF EXISTS `DelKind`;
DELIMITER |
CREATE PROCEDURE `DelKind`(in id_b int, in deleter_b int)
BEGIN
	update jelleg set deleted = 1 , deleted_by = deleter_b , deleted_at = current_timestamp() where id = id_b;
END |
DELIMITER ;

DROP PROCEDURE IF EXISTS `delpartner`;
DELIMITER |
CREATE PROCEDURE `delpartner`(in id_b int, in deleter_b int)
BEGIN
update partner set deleted = 1, deleted_by = deleter_b, deleted_at = current_timestamp() where id = id_b;
END |
DELIMITER ;

DROP PROCEDURE IF EXISTS `delpartnerugyintezo`;
DELIMITER |
CREATE PROCEDURE `delpartnerugyintezo`(in id_b int, in deleter_b int)
BEGIN
update partnerugyintezo set deleted = 1, deleted_by = deleter_b, deleted_at = current_timestamp() where id = id_b;
END |
DELIMITER ;

DROP PROCEDURE IF EXISTS `DelTelephely`;
DELIMITER |
CREATE PROCEDURE `DelTelephely`(in id_b int , in deleter_b int)
BEGIN
update telephely set deleted = 1 , deleted_by = deleter_b , deleted_at = current_timestamp() where id = id_b;
   
END |
DELIMITER ;

DROP PROCEDURE IF EXISTS `DelUgyintezo`;
DELIMITER |
CREATE PROCEDURE `DelUgyintezo`(in id_b int, in deleter_b int)
BEGIN
	update ugyintezo set deleted = 1 , deleted_by = deleter_b ,deleted_at = current_timestamp() where id = id_b;
END |
DELIMITER ;

DROP PROCEDURE IF EXISTS `DelUser`;
DELIMITER |
CREATE PROCEDURE `DelUser`(in user_id_b int, in deleter_b int)
BEGIN
 update user set deleted = 1 , deleted_by = deleter_b , deleted_at = current_timestamp() where id = user_id_b;
END |
DELIMITER ;

DROP PROCEDURE IF EXISTS `getalltelephely`;
DELIMITER |
CREATE PROCEDURE `getalltelephely`()
BEGIN
	select * from telephely where deleted = 0;
END |
DELIMITER ;

DROP PROCEDURE IF EXISTS `getdocsbyikonyvid`;
DELIMITER |
CREATE PROCEDURE `getdocsbyikonyvid`(in ikonyvid_b int)
BEGIN
 select d.id, d.name, d.ext, d.path,d.size from ikonyvdocs inner join doc d on (ikonyvdocs.doc_id = d.id) where ikonyvdocs.ikonyv_id = ikonyvid_b and deleted = 0;
END |
DELIMITER ;

DROP PROCEDURE IF EXISTS `getEvek`;
DELIMITER |
CREATE PROCEDURE `getEvek`()
BEGIN
	select * from evek;
END |
DELIMITER ;

DROP PROCEDURE IF EXISTS `getGroup`;
DELIMITER |
CREATE PROCEDURE `getGroup`(in telephely_b int)
BEGIN
	select * from csoport where telephely = telephely_b and deleted = 0;
END |
DELIMITER ;

DROP PROCEDURE IF EXISTS `getIkonyvById`;
DELIMITER |
CREATE PROCEDURE `getIkonyvById`(in id_b int)
BEGIN
	select * from currentYearIkonyv where id = id_b and deleted = 0;
END |
DELIMITER ;

DROP PROCEDURE IF EXISTS `getIkonyvek`;
DELIMITER |
CREATE PROCEDURE `getIkonyvek`(in user_id_b int, in year_id_b int, in irany_b tinyint)
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
END |
DELIMITER ;

DROP PROCEDURE IF EXISTS `getIkonyvekByTelephely`;
DELIMITER |
CREATE PROCEDURE `getIkonyvekByTelephely`(in telephely_b int)
BEGIN
	select * from currentYearIkonyv where telephely = telephely_b and deleted = 0;
END |
DELIMITER ;

DROP PROCEDURE IF EXISTS `getJellegek`;
DELIMITER |
CREATE PROCEDURE `getJellegek`(in telephely_b int)
BEGIN
	select * from jelleg where telephely = telephely_b and deleted = 0;
END |
DELIMITER ;

DROP PROCEDURE IF EXISTS `getpartners`;
DELIMITER |
CREATE PROCEDURE `getpartners`(in telephely_b int)
BEGIN
	select * from partner where telephely = telephely_b and deleted = 0;
END |
DELIMITER ;

DROP PROCEDURE IF EXISTS `getpartnerugyintezok`;
DELIMITER |
CREATE PROCEDURE `getpartnerugyintezok`(in partner_b int)
BEGIN
 select id, name  from partnerugyintezo where partnerid = partner_b and deleted = 0;
 end |
DELIMITER ;

DROP PROCEDURE IF EXISTS `getPrevYearIkonyv`;
DELIMITER |
CREATE PROCEDURE `getPrevYearIkonyv`(in year_b int)
BEGIN
	select * from prevYearIkonyv where year = year_b;
END |
DELIMITER ;

DROP PROCEDURE IF EXISTS `getprivileges`;
DELIMITER |
CREATE PROCEDURE `getprivileges`()
BEGIN
select * from privilege;
END |
DELIMITER ;

DROP PROCEDURE IF EXISTS `getShortIkonyv`;
DELIMITER |
CREATE PROCEDURE `getShortIkonyv`(in telephely_b int)
BEGIN
 select ikonyvid, iktatoszam from currentYearIkonyv where telephely = telephely_b and deleted = 0;
END |
DELIMITER ;

DROP PROCEDURE IF EXISTS `getTelephelyek`;
DELIMITER |
CREATE PROCEDURE `getTelephelyek`(in user_b int)
BEGIN
	select t.id as id, t.name as name from felh_telephely f inner join telephely t on (f.telephely_id = t.id)where f.user_id = user_b and deleted = 0;
END |
DELIMITER ;

DROP PROCEDURE IF EXISTS `getUgyintezok`;
DELIMITER |
CREATE PROCEDURE `getUgyintezok`(in telephely_b int)
BEGIN
	select * from ugyintezo where telephely = telephely_b and deleted = 0;
END |
DELIMITER ;

DROP PROCEDURE IF EXISTS `getUsers`;
DELIMITER |
CREATE PROCEDURE `getUsers`()
BEGIN
		select Distinct(u.id) as id,u.fullname as fullname, u.username as username, p.id as privilegeid, p.privilege as privilege from user u inner join privilege p on (u.privilege = p.id)   where deleted = 0;
END |
DELIMITER ;

DROP PROCEDURE IF EXISTS `ModifyGroup`;
DELIMITER |
CREATE PROCEDURE `ModifyGroup`(in id_b int,in name_b varchar(100), in sname_b varchar(3))
BEGIN
	update csoport set name = name_b, shortname = sname_b where id = id_b;
END |
DELIMITER ;

DROP PROCEDURE IF EXISTS `ModifyIkonyv`;
DELIMITER |
CREATE PROCEDURE `ModifyIkonyv`(in id_b int, in targy_b varchar(100),in hivszam_b varchar(50),in ugyintezo_b int, in partner_b int,in partnerugyintezo_b int, 
in erkezett_b datetime,in hatido_b datetime, in szoveg_b text, in jelleg_b int)
BEGIN
	if partnerugyintezo_b > 0 then
	update ikonyvpartnerei join ikonyv on(ikonyvpartnerei.id = ikonyv.partner) set partner_id = partner_b, partnerugyintezo_id = partnerugyintezo_b  where ikonyv.id = id_b;
    else
    update ikonyvpartnerei join ikonyv on(ikonyvpartnerei.id = ikonyv.partner) set partner_id = partner_b, partnerugyintezo_id = null  where ikonyv.id = id_b;
    end if;
	update ikonyv set jelleg = jelleg_b, targy = targy_b, hivszam = hivszam_b,ugyintezo = ugyintezo_b,erkezett = erkezett_b,hatido = hatido_b,szoveg = szoveg_b where id = id_b;
END |
DELIMITER ;

DROP PROCEDURE IF EXISTS `ModifyKind`;
DELIMITER |
CREATE PROCEDURE `ModifyKind`(in id_b int,in name_b varchar(100))
BEGIN
	update jelleg set name = name_b where id = id_b;
END |
DELIMITER ;

DROP PROCEDURE IF EXISTS `ModifyPartner`;
DELIMITER |
CREATE PROCEDURE `ModifyPartner`(in id_b int,in name_b varchar(100))
BEGIN
	update partner set name = name_b  where id = id_b;
END |
DELIMITER ;

DROP PROCEDURE IF EXISTS `ModifyPartnerUgyintezo`;
DELIMITER |
CREATE PROCEDURE `ModifyPartnerUgyintezo`(in id_b int,in name_b varchar(100))
BEGIN
	update partnerugyintezo set name = name_b  where id = id_b;
END |
DELIMITER ;

DROP PROCEDURE IF EXISTS `ModifyTelephely`;
DELIMITER |
CREATE PROCEDURE `ModifyTelephely`(in id_b int, in name_b varchar(100))
BEGIN
	update telephely set name = name_b where id = id_b;
END |
DELIMITER ;

DROP PROCEDURE IF EXISTS `ModifyUgyintezo`;
DELIMITER |
CREATE PROCEDURE `ModifyUgyintezo`(in id_b int,in name_b varchar(100))
BEGIN
	update ugyintezo set name = name_b where id = id_b;
END |
DELIMITER ;

DROP PROCEDURE IF EXISTS `modifyuser`;
DELIMITER |
CREATE PROCEDURE `modifyuser`(in id_b int,in username_b varchar(250), in fullname_b varchar(250),in privilege_b int, in telephelyek_b varchar(30))
BEGIN
	DECLARE _next TEXT DEFAULT NULL;
	DECLARE _nextlen INT DEFAULT NULL;
	DECLARE _value TEXT DEFAULT NULL;
	update user set username = username_b, fullname = fullname_b, privilege = privilege_b where id = id_b; 
    delete from felh_telephely where user_id = id_b;


	iterator:
	LOOP
	-- exit the loop if the list seems empty or was null;
	-- this extra caution is necessary to avoid an endless loop in the proc.
	IF LENGTH(TRIM(telephelyek_b)) = 0 OR telephelyek_b IS NULL THEN
		LEAVE iterator;
	END IF;
	-- capture the next value from the list
	SET _next = SUBSTRING_INDEX(telephelyek_b,',',1);
	-- save the length of the captured value; we will need to remove this
	-- many characters + 1 from the beginning of the string 
	-- before the next iteration
	SET _nextlen = LENGTH(_next);
	-- trim the value of leading and trailing spaces, in case of sloppy CSV strings
	SET _value = TRIM(_next);	
	-- insert the extracted value into the target table
	INSERT INTO felh_telephely(user_id,telephely_id) VALUES(id_b,_value);
	-- rewrite the original string using the `INSERT()` string function,
	-- args are original string, start position, how many characters to remove, 
	-- and what to "insert" in their place (in this case, we "insert"
	-- an empty string, which removes _nextlen + 1 characters)
	SET telephelyek_b = INSERT(telephelyek_b,1,_nextlen + 1,'');
	END LOOP;

	END |
DELIMITER ;

DROP PROCEDURE IF EXISTS `ModifyUserPassword`;
DELIMITER |
CREATE PROCEDURE `ModifyUserPassword`(in user_id_b int, in password_b varchar(250))
BEGIN
	update user set password = password_b where id = user_id_b;
END |
DELIMITER ;

DROP PROCEDURE IF EXISTS `setDeletedByValaszID`;
DELIMITER |
CREATE PROCEDURE `setDeletedByValaszID`(in ikonyv_b int, in user_id int)
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
END |
DELIMITER ;

DROP PROCEDURE IF EXISTS `UserLogin`;
DELIMITER |
CREATE PROCEDURE `UserLogin`(in username_b varchar(100), in password_b varchar(250))
BEGIN
	select u.id as id, u.username as username, u.fullname as fullname, p.id as privilegeid, p.privilege as privilegename from user u 
    inner join privilege  p on (u.privilege = p.id) where username = username_b and password = password_b;
END |
DELIMITER ;

-- 
-- Dumping views
-- 

DROP TABLE IF EXISTS `createiktszam`;
DROP VIEW IF EXISTS `createiktszam`;
CREATE ALGORITHM=UNDEFINED SQL SECURITY DEFINER VIEW `createiktszam` AS select `i`.`id` AS `id`,concat(convert(if((`i`.`irany` = 0),'B','K') using utf8),'-',`ics`.`shortname`,'/',substr(`t`.`name`,1,1),'/',`i`.`iktatottid`,convert(ifnull(concat('-',`i`.`valaszid`),'') using utf8),'/',`evek`.`year`) AS `generatedIktSzam` from (((((`ikonyv` `i` join `telephely` `t` on((`i`.`telephely` = `t`.`id`))) join `csoport` `ics` on((`ics`.`id` = `i`.`csoport`))) join `jelleg` `j` on((`j`.`id` = `i`.`jelleg`))) join `ugyintezo` `ugy` on((`ugy`.`id` = `i`.`ugyintezo`))) join `evek` on((`i`.`yearid` = `evek`.`id`)));

DROP TABLE IF EXISTS `createsubiktszam`;
DROP VIEW IF EXISTS `createsubiktszam`;
CREATE ALGORITHM=UNDEFINED SQL SECURITY DEFINER VIEW `createsubiktszam` AS select `i`.`id` AS `id`,concat(convert(if((`i`.`irany` = 0),'B','K') using utf8),'-',`ics`.`shortname`,'/',substr(`t`.`name`,1,1),'/',`i`.`iktatottid`,convert(ifnull(concat('-',`i`.`valaszid`),'') using utf8),'/',`evek`.`year`) AS `generatedIktSzam` from (((((`ikonyv` `i` join `telephely` `t` on((`i`.`telephely` = `t`.`id`))) join `csoport` `ics` on((`ics`.`id` = `i`.`csoport`))) join `jelleg` `j` on((`j`.`id` = `i`.`jelleg`))) join `ugyintezo` `ugy` on((`ugy`.`id` = `i`.`ugyintezo`))) join `evek` on((`i`.`yearid` = `evek`.`id`)));

DROP TABLE IF EXISTS `currentyearikonyv`;
DROP VIEW IF EXISTS `currentyearikonyv`;
CREATE ALGORITHM=UNDEFINED SQL SECURITY DEFINER VIEW `currentyearikonyv` AS select `i`.`id` AS `ikonyvid`,`i`.`iktatoszam` AS `iktatoszam`,`i`.`targy` AS `targy`,`i`.`irany` AS `irany`,`i`.`hivszam` AS `hivszam`,`i`.`erkezett` AS `erkezett`,`i`.`hatido` AS `hatido`,`i`.`telephely` AS `telephely`,`i`.`deleted` AS `deleted`,`i`.`szoveg` AS `szoveg`,`p`.`id` AS `partnerid`,`p`.`name` AS `partnername`,`pu`.`id` AS `partnerugyintezoid`,`pu`.`name` AS `partnerugyintezoname`,`u`.`id` AS `ugyintezoid`,`u`.`name` AS `ugyintezoname`,`cs`.`id` AS `csoportid`,`cs`.`name` AS `csoportname`,`cs`.`shortname` AS `csoportshortname`,`j`.`id` AS `jellegid`,`j`.`name` AS `jellegname`,ifnull(count(`docs`.`ikonyv_id`),0) AS `doccount` from (((((((`ikonyv` `i` join `ikonyvpartnerei` `ip` on((`i`.`partner` = `ip`.`id`))) join `partner` `p` on((`ip`.`partner_id` = `p`.`id`))) left join `partnerugyintezo` `pu` on((`ip`.`partnerugyintezo_id` = `pu`.`id`))) join `csoport` `cs` on((`i`.`csoport` = `cs`.`id`))) join `jelleg` `j` on((`i`.`jelleg` = `j`.`id`))) join `ugyintezo` `u` on((`i`.`ugyintezo` = `u`.`id`))) left join (select `ikonyvdocs`.`ikonyv_id` AS `ikonyv_id`,`ikonyvdocs`.`doc_id` AS `doc_id`,`ikonyvdocs`.`deleted` AS `deleted`,`ikonyvdocs`.`created_by` AS `created_by`,`ikonyvdocs`.`created_at` AS `created_at`,`ikonyvdocs`.`deleted_by` AS `deleted_by`,`ikonyvdocs`.`deleted_at` AS `deleted_at` from `ikonyvdocs` where (`ikonyvdocs`.`deleted` = 0)) `docs` on((`i`.`id` = `docs`.`ikonyv_id`))) where (`i`.`yearid` = (select `evek`.`id` from `evek` where (`evek`.`active` = 1))) group by `i`.`id` order by `i`.`iktatottid`,`i`.`valaszid`;

DROP TABLE IF EXISTS `prevyearikonyv`;
DROP VIEW IF EXISTS `prevyearikonyv`;
CREATE ALGORITHM=UNDEFINED SQL SECURITY DEFINER VIEW `prevyearikonyv` AS select `i`.`id` AS `ikonyvid`,`i`.`iktatoszam` AS `iktatoszam`,`i`.`irany` AS `irany`,`i`.`targy` AS `targy`,`i`.`yearid` AS `year`,`i`.`hivszam` AS `hivszam`,`i`.`erkezett` AS `erkezett`,`i`.`hatido` AS `hatido`,`i`.`telephely` AS `telephely`,`i`.`deleted` AS `deleted`,`i`.`szoveg` AS `szoveg`,`p`.`id` AS `partnerid`,`p`.`name` AS `partnername`,`pu`.`id` AS `partnerugyintezoid`,`pu`.`name` AS `partnerugyintezoname`,`u`.`id` AS `ugyintezoid`,`u`.`name` AS `ugyintezoname`,`cs`.`id` AS `csoportid`,`cs`.`name` AS `csoportname`,`cs`.`shortname` AS `csoportshortname`,`j`.`id` AS `jellegid`,`j`.`name` AS `jellegname`,ifnull(count(`docs`.`ikonyv_id`),0) AS `doccount` from (((((((`ikonyv` `i` join `ikonyvpartnerei` `ip` on((`i`.`partner` = `ip`.`id`))) join `partner` `p` on((`ip`.`partner_id` = `p`.`id`))) left join `partnerugyintezo` `pu` on((`ip`.`partnerugyintezo_id` = `pu`.`id`))) join `csoport` `cs` on((`i`.`csoport` = `cs`.`id`))) join `jelleg` `j` on((`i`.`jelleg` = `j`.`id`))) join `ugyintezo` `u` on((`i`.`ugyintezo` = `u`.`id`))) left join (select `ikonyvdocs`.`ikonyv_id` AS `ikonyv_id`,`ikonyvdocs`.`doc_id` AS `doc_id`,`ikonyvdocs`.`deleted` AS `deleted`,`ikonyvdocs`.`created_by` AS `created_by`,`ikonyvdocs`.`created_at` AS `created_at`,`ikonyvdocs`.`deleted_by` AS `deleted_by`,`ikonyvdocs`.`deleted_at` AS `deleted_at` from `ikonyvdocs` where (`ikonyvdocs`.`deleted` = 0)) `docs` on((`i`.`id` = `docs`.`ikonyv_id`))) group by `i`.`id` order by `i`.`iktatottid`,`i`.`valaszid`;



/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;


-- Dump completed on 2020-03-24 09:33:52
-- Total time: 0:0:0:0:185 (d:h:m:s:ms)
