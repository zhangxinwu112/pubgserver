/*
Navicat MySQL Data Transfer

Source Server         : bendi
Source Server Version : 50528
Source Host           : localhost:3306
Source Database       : cs

Target Server Type    : MYSQL
Target Server Version : 50528
File Encoding         : 65001

Date: 2020-02-23 20:23:20
*/

SET FOREIGN_KEY_CHECKS=0;

-- ----------------------------
-- Table structure for code
-- ----------------------------
DROP TABLE IF EXISTS `code`;
CREATE TABLE `code` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `name` varchar(255) DEFAULT NULL,
  `ctype` smallint(6) DEFAULT NULL COMMENT '0为次数，1为日期',
  `count` smallint(6) DEFAULT NULL,
  `expiretime` bigint(15) NOT NULL DEFAULT '0',
  `userType` smallint(2) DEFAULT '0' COMMENT '管理员和玩家分开',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of code
-- ----------------------------
INSERT INTO `code` VALUES ('1', '000001', '0', '-1', '0', '0');
INSERT INTO `code` VALUES ('2', '000002', '1', '0', '1563960333', '0');
INSERT INTO `code` VALUES ('3', '000003', '0', '15', '0', '1');

-- ----------------------------
-- Table structure for grounp
-- ----------------------------
DROP TABLE IF EXISTS `grounp`;
CREATE TABLE `grounp` (
  `id` int(11) NOT NULL AUTO_INCREMENT COMMENT '运行状态-表示停止，0表示运行。',
  `name` varchar(10) DEFAULT NULL,
  `area` varchar(10) DEFAULT '陕西地区',
  `userId` int(11) DEFAULT NULL,
  `runState` smallint(2) DEFAULT '-1',
  `playerTime` smallint(2) DEFAULT '60',
  `fenceLon` double(12,6) DEFAULT '-1.000000',
  `fenceLat` double(12,6) DEFAULT '-1.000000',
  `fenceRadius` int(10) DEFAULT '2000',
  `fenceTotalRadius` int(10) DEFAULT '2000',
  `checkCode` varchar(10) DEFAULT '123456',
  `roomCount` int(5) DEFAULT '10',
  PRIMARY KEY (`id`)
) ENGINE=MyISAM AUTO_INCREMENT=123 DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of grounp
-- ----------------------------
INSERT INTO `grounp` VALUES ('120', '管理员', '陕西地区', '41', '-1', '60', '-1.000000', '-1.000000', '2000', '2000', '123456', '10');
INSERT INTO `grounp` VALUES ('121', '二管理', '陕西地区', '42', '-1', '10', '-1.000000', '-1.000000', '2000', '2000', '123456', '10');
INSERT INTO `grounp` VALUES ('122', '天涯1', '陕西地区', '43', '-1', '60', '-1.000000', '-1.000000', '2000', '2000', '123456', '10');

-- ----------------------------
-- Table structure for life
-- ----------------------------
DROP TABLE IF EXISTS `life`;
CREATE TABLE `life` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `bulletCount` smallint(3) DEFAULT '80' COMMENT '弹量',
  `lifeValue` smallint(3) DEFAULT '50' COMMENT '生命值',
  `fightScore` smallint(3) DEFAULT '35' COMMENT '战绩',
  `userId` int(11) DEFAULT '-1',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=9 DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of life
-- ----------------------------
INSERT INTO `life` VALUES ('6', '80', '0', '35', '38');
INSERT INTO `life` VALUES ('7', '80', '0', '35', '44');
INSERT INTO `life` VALUES ('8', '80', '50', '35', '45');

-- ----------------------------
-- Table structure for machine
-- ----------------------------
DROP TABLE IF EXISTS `machine`;
CREATE TABLE `machine` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `codeid` int(11) DEFAULT NULL,
  `deviceUniqueIdentifier` varchar(150) DEFAULT NULL,
  `plat` varchar(20) DEFAULT NULL,
  `system` varchar(150) DEFAULT NULL,
  `userId` int(11) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=239 DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of machine
-- ----------------------------
INSERT INTO `machine` VALUES ('203', '1', 'ed5b44d5a642195d47dad4a01df8cc57', 'Handheld', 'Android OS 8.1.0 / API-27 (OPM1.171019.026/eng.compil.20191220.211147)', '38');
INSERT INTO `machine` VALUES ('204', '3', 'be367d090e0f13218448182032916d36525aeba0', 'Desktop', 'Windows 10  (10.0.0) 64bit', null);
INSERT INTO `machine` VALUES ('205', '3', 'be367d090e0f13218448182032916d36525aeba0', 'Desktop', 'Windows 10  (10.0.0) 64bit', '39');
INSERT INTO `machine` VALUES ('206', '3', 'be367d090e0f13218448182032916d36525aeba0', 'Desktop', 'Windows 10  (10.0.0) 64bit', '40');
INSERT INTO `machine` VALUES ('207', '3', 'be367d090e0f13218448182032916d36525aeba0', 'Desktop', 'Windows 10  (10.0.0) 64bit', '41');
INSERT INTO `machine` VALUES ('208', '3', '6272121cad86df65d005712ff229d381', 'Handheld', 'Android OS 5.1.1 / API-22 (LMY49I/V9.5.3.0.LACCNFA)', null);
INSERT INTO `machine` VALUES ('209', '3', '6272121cad86df65d005712ff229d381', 'Handheld', 'Android OS 5.1.1 / API-22 (LMY49I/V9.5.3.0.LACCNFA)', '41');
INSERT INTO `machine` VALUES ('210', '1', 'f0546f2e84bdb859ad64454d637a9de2', 'Handheld', 'Android OS 6.0.1 / API-23 (V417IR/eng.root.20191227.073224)', '44');
INSERT INTO `machine` VALUES ('211', '1', 'ed5b44d5a642195d47dad4a01df8cc57', 'Handheld', 'Android OS 8.1.0 / API-27 (OPM1.171019.026/eng.compil.20191220.211147)', '45');
INSERT INTO `machine` VALUES ('212', '3', 'be367d090e0f13218448182032916d36525aeba0', 'Desktop', 'Windows 10  (10.0.0) 64bit', '42');
INSERT INTO `machine` VALUES ('213', '3', 'ed5b44d5a642195d47dad4a01df8cc57', 'Handheld', 'Android OS 8.1.0 / API-27 (OPM1.171019.026/eng.compil.20191220.211147)', null);
INSERT INTO `machine` VALUES ('214', '3', 'ed5b44d5a642195d47dad4a01df8cc57', 'Handheld', 'Android OS 8.1.0 / API-27 (OPM1.171019.026/eng.compil.20191220.211147)', '42');
INSERT INTO `machine` VALUES ('215', '3', '6272121cad86df65d005712ff229d381', 'Handheld', 'Android OS 5.1.1 / API-22 (LMY49I/V9.5.3.0.LACCNFA)', '42');
INSERT INTO `machine` VALUES ('216', '3', '6272121cad86df65d005712ff229d381', 'Handheld', 'Android OS 5.1.1 / API-22 (LMY49I/V9.5.3.0.LACCNFA)', '42');
INSERT INTO `machine` VALUES ('217', '1', 'ed5b44d5a642195d47dad4a01df8cc57', 'Handheld', 'Android OS 8.1.0 / API-27 (OPM1.171019.026/eng.compil.20191220.211147)', '38');
INSERT INTO `machine` VALUES ('218', '1', 'f0546f2e84bdb859ad64454d637a9de2', 'Handheld', 'Android OS 6.0.1 / API-23 (V417IR/eng.root.20191227.073224)', '38');
INSERT INTO `machine` VALUES ('219', '1', 'f0546f2e84bdb859ad64454d637a9de2', 'Handheld', 'Android OS 6.0.1 / API-23 (V417IR/eng.root.20191227.073224)', '38');
INSERT INTO `machine` VALUES ('220', '1', '6272121cad86df65d005712ff229d381', 'Handheld', 'Android OS 5.1.1 / API-22 (LMY49I/V9.5.3.0.LACCNFA)', '44');
INSERT INTO `machine` VALUES ('221', '3', '8c6556ded83910822ce65a6ceb166b7f', 'Handheld', 'Android OS 4.4.2 / API-19 (KOT49H/eng.cibuilder.20191112.174801)', null);
INSERT INTO `machine` VALUES ('222', '3', '8c6556ded83910822ce65a6ceb166b7f', 'Handheld', 'Android OS 4.4.2 / API-19 (KOT49H/eng.cibuilder.20191112.174801)', '42');
INSERT INTO `machine` VALUES ('223', '1', '6272121cad86df65d005712ff229d381', 'Handheld', 'Android OS 5.1.1 / API-22 (LMY49I/V9.5.3.0.LACCNFA)', '38');
INSERT INTO `machine` VALUES ('224', '1', 'f0546f2e84bdb859ad64454d637a9de2', 'Handheld', 'Android OS 6.0.1 / API-23 (V417IR/eng.root.20191227.073224)', '44');
INSERT INTO `machine` VALUES ('225', '1', '6272121cad86df65d005712ff229d381', 'Handheld', 'Android OS 5.1.1 / API-22 (LMY49I/V9.5.3.0.LACCNFA)', '38');
INSERT INTO `machine` VALUES ('226', '1', 'f0546f2e84bdb859ad64454d637a9de2', 'Handheld', 'Android OS 6.0.1 / API-23 (V417IR/eng.root.20191227.073224)', '38');
INSERT INTO `machine` VALUES ('227', '1', 'be367d090e0f13218448182032916d36525aeba0', 'Desktop', 'Windows 10  (10.0.0) 64bit', '38');
INSERT INTO `machine` VALUES ('228', '1', 'f0546f2e84bdb859ad64454d637a9de2', 'Handheld', 'Android OS 6.0.1 / API-23 (V417IR/eng.root.20191227.073224)', '38');
INSERT INTO `machine` VALUES ('229', '1', '6272121cad86df65d005712ff229d381', 'Handheld', 'Android OS 5.1.1 / API-22 (LMY49I/V9.5.3.0.LACCNFA)', '44');
INSERT INTO `machine` VALUES ('230', '1', 'be367d090e0f13218448182032916d36525aeba0', 'Desktop', 'Windows 10  (10.0.0) 64bit', '45');
INSERT INTO `machine` VALUES ('231', '1', '6272121cad86df65d005712ff229d381', 'Handheld', 'Android OS 5.1.1 / API-22 (LMY49I/V9.5.3.0.LACCNFA)', '44');
INSERT INTO `machine` VALUES ('232', '3', 'f0546f2e84bdb859ad64454d637a9de2', 'Handheld', 'Android OS 6.0.1 / API-23 (V417IR/eng.root.20191227.073224)', null);
INSERT INTO `machine` VALUES ('233', '3', 'f0546f2e84bdb859ad64454d637a9de2', 'Handheld', 'Android OS 6.0.1 / API-23 (V417IR/eng.root.20191227.073224)', '42');
INSERT INTO `machine` VALUES ('234', '1', 'be367d090e0f13218448182032916d36525aeba0', 'Desktop', 'Windows 10  (10.0.0) 64bit', '45');
INSERT INTO `machine` VALUES ('235', '3', '6272121cad86df65d005712ff229d381', 'Handheld', 'Android OS 5.1.1 / API-22 (LMY49I/V9.5.3.0.LACCNFA)', '42');
INSERT INTO `machine` VALUES ('236', '1', 'be367d090e0f13218448182032916d36525aeba0', 'Desktop', 'Windows 10  (10.0.0) 64bit', '45');
INSERT INTO `machine` VALUES ('237', '3', '6272121cad86df65d005712ff229d381', 'Handheld', 'Android OS 5.1.1 / API-22 (LMY49I/V9.5.3.0.LACCNFA)', '42');
INSERT INTO `machine` VALUES ('238', '1', 'f0546f2e84bdb859ad64454d637a9de2', 'Handheld', 'Android OS 6.0.1 / API-23 (V417IR/eng.root.20191227.073224)', '38');

-- ----------------------------
-- Table structure for room
-- ----------------------------
DROP TABLE IF EXISTS `room`;
CREATE TABLE `room` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `grounpId` int(11) DEFAULT NULL,
  `name` varchar(10) DEFAULT NULL,
  `checkCode` varchar(10) DEFAULT NULL,
  `userId` int(10) DEFAULT '-1',
  `runState` smallint(2) DEFAULT '-1',
  PRIMARY KEY (`id`)
) ENGINE=MyISAM AUTO_INCREMENT=522 DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of room
-- ----------------------------
INSERT INTO `room` VALUES ('521', '121', '天涯战队', '123456', '38', '-1');

-- ----------------------------
-- Table structure for room_user
-- ----------------------------
DROP TABLE IF EXISTS `room_user`;
CREATE TABLE `room_user` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `room_id` int(11) DEFAULT NULL,
  `user_id` int(11) DEFAULT NULL,
  `runState` smallint(2) DEFAULT '-1',
  PRIMARY KEY (`id`)
) ENGINE=MyISAM AUTO_INCREMENT=73 DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of room_user
-- ----------------------------
INSERT INTO `room_user` VALUES ('70', '521', '38', '-1');
INSERT INTO `room_user` VALUES ('71', '521', '45', '-1');
INSERT INTO `room_user` VALUES ('72', '521', '44', '-1');

-- ----------------------------
-- Table structure for score
-- ----------------------------
DROP TABLE IF EXISTS `score`;
CREATE TABLE `score` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `createTime` int(11) DEFAULT NULL,
  `bulletCount` smallint(3) DEFAULT NULL,
  `lifeValue` smallint(3) DEFAULT NULL,
  `fightScore` smallint(3) DEFAULT NULL,
  `roomId` int(11) DEFAULT NULL,
  `grounpId` int(11) DEFAULT NULL,
  `userId` int(11) DEFAULT NULL,
  `userName` varchar(255) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=22 DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of score
-- ----------------------------
INSERT INTO `score` VALUES ('12', '1582486305', '80', '0', '35', '521', '121', '38', '天涯');
INSERT INTO `score` VALUES ('13', '1582486311', '80', '0', '35', '521', '121', '44', '天涯1');
INSERT INTO `score` VALUES ('14', '1582486316', '80', '50', '35', '521', '121', '45', '天涯0');
INSERT INTO `score` VALUES ('16', '1582488390', '80', '0', '35', '521', '121', '38', '天涯');
INSERT INTO `score` VALUES ('17', '1582488391', '80', '0', '35', '521', '121', '44', '天涯1');
INSERT INTO `score` VALUES ('18', '1582488391', '80', '50', '35', '521', '121', '45', '天涯0');
INSERT INTO `score` VALUES ('19', '1582488679', '80', '0', '35', '521', '121', '38', '天涯');
INSERT INTO `score` VALUES ('20', '1582488679', '80', '0', '35', '521', '121', '44', '天涯1');
INSERT INTO `score` VALUES ('21', '1582488679', '80', '50', '35', '521', '121', '45', '天涯0');

-- ----------------------------
-- Table structure for user
-- ----------------------------
DROP TABLE IF EXISTS `user`;
CREATE TABLE `user` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `telephone` varchar(255) DEFAULT '',
  `password` varchar(255) DEFAULT '',
  `name` varchar(255) DEFAULT '',
  `image` varchar(255) DEFAULT '',
  `type` smallint(2) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=46 DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of user
-- ----------------------------
INSERT INTO `user` VALUES ('38', '17391767972', '123456', '天涯', 'image1', '0');
INSERT INTO `user` VALUES ('41', '18392120357', '123456', '管理员', 'image1', '1');
INSERT INTO `user` VALUES ('42', '18392120358', '123456', '二管理', 'image1', '1');
INSERT INTO `user` VALUES ('43', '18392120359', '123456', '三管理员', 'image1', '1');
INSERT INTO `user` VALUES ('44', '17391767971', '123456', '天涯1', 'image1', '0');
INSERT INTO `user` VALUES ('45', '17391767970', '123456', '天涯0', 'image1', '0');
