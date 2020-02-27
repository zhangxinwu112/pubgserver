/*
Navicat MySQL Data Transfer

Source Server         : bendi
Source Server Version : 50528
Source Host           : localhost:3306
Source Database       : cs

Target Server Type    : MYSQL
Target Server Version : 50528
File Encoding         : 65001

Date: 2020-02-27 12:03:46
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
  `remainTime` int(10) DEFAULT '3600',
  PRIMARY KEY (`id`)
) ENGINE=MyISAM AUTO_INCREMENT=132 DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of grounp
-- ----------------------------
INSERT INTO `grounp` VALUES ('131', '管理', '陕西地区', '75', '0', '65', '116.415036', '39.912279', '1530', '2278', '123456', '10', '3180');

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
) ENGINE=InnoDB AUTO_INCREMENT=32 DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of life
-- ----------------------------
INSERT INTO `life` VALUES ('30', '80', '50', '35', '76');
INSERT INTO `life` VALUES ('31', '80', '75', '35', '77');

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
) ENGINE=InnoDB AUTO_INCREMENT=352 DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of machine
-- ----------------------------
INSERT INTO `machine` VALUES ('343', '3', 'f0546f2e84bdb859ad64454d637a9de2', 'Handheld', 'Android OS 6.0.1 / API-23 (V417IR/eng.root.20191227.073224)', null);
INSERT INTO `machine` VALUES ('344', '3', 'f0546f2e84bdb859ad64454d637a9de2', 'Handheld', 'Android OS 6.0.1 / API-23 (V417IR/eng.root.20191227.073224)', '75');
INSERT INTO `machine` VALUES ('345', '1', '6272121cad86df65d005712ff229d381', 'Handheld', 'Android OS 5.1.1 / API-22 (LMY49I/V9.5.3.0.LACCNFA)', '76');
INSERT INTO `machine` VALUES ('346', '1', '0f1d5f0165787b54f64607de2cf818ba', 'Handheld', 'Android OS 5.1.1 / API-22 (HUAWEIMLA-AL10/500191128)', '77');
INSERT INTO `machine` VALUES ('347', '3', 'f0546f2e84bdb859ad64454d637a9de2', 'Handheld', 'Android OS 6.0.1 / API-23 (V417IR/eng.root.20191227.073224)', '75');
INSERT INTO `machine` VALUES ('348', '3', 'f0546f2e84bdb859ad64454d637a9de2', 'Handheld', 'Android OS 6.0.1 / API-23 (V417IR/eng.root.20191227.073224)', '75');
INSERT INTO `machine` VALUES ('349', '3', '6272121cad86df65d005712ff229d381', 'Handheld', 'Android OS 5.1.1 / API-22 (LMY49I/V9.5.3.0.LACCNFA)', null);
INSERT INTO `machine` VALUES ('350', '3', '6272121cad86df65d005712ff229d381', 'Handheld', 'Android OS 5.1.1 / API-22 (LMY49I/V9.5.3.0.LACCNFA)', '75');
INSERT INTO `machine` VALUES ('351', '3', '6272121cad86df65d005712ff229d381', 'Handheld', 'Android OS 5.1.1 / API-22 (LMY49I/V9.5.3.0.LACCNFA)', '75');

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
) ENGINE=MyISAM AUTO_INCREMENT=531 DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of room
-- ----------------------------
INSERT INTO `room` VALUES ('530', '131', 'cs战队', '123456', '76', '0');

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
) ENGINE=MyISAM AUTO_INCREMENT=98 DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of room_user
-- ----------------------------
INSERT INTO `room_user` VALUES ('96', '530', '76', '0');
INSERT INTO `room_user` VALUES ('97', '530', '77', '0');

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
) ENGINE=InnoDB AUTO_INCREMENT=78 DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of score
-- ----------------------------

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
) ENGINE=InnoDB AUTO_INCREMENT=78 DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of user
-- ----------------------------
INSERT INTO `user` VALUES ('75', '18392120357', '123456', '管理', 'image1', '1');
INSERT INTO `user` VALUES ('76', '17391767972', '123456', '天涯', 'image1', '0');
INSERT INTO `user` VALUES ('77', '17391767971', '123456', '天涯1', 'image1', '0');
