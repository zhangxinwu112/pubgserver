/*
Navicat MySQL Data Transfer

Source Server         : localhost
Source Server Version : 50528
Source Host           : localhost:3306
Source Database       : cs

Target Server Type    : MYSQL
Target Server Version : 50528
File Encoding         : 65001

Date: 2019-11-03 22:13:23
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
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of code
-- ----------------------------
INSERT INTO `code` VALUES ('1', '00001', '0', '-1', '0');
INSERT INTO `code` VALUES ('2', '00002', '1', '0', '1563960333');
INSERT INTO `code` VALUES ('3', '00003', '0', '15', '0');

-- ----------------------------
-- Table structure for grounp
-- ----------------------------
DROP TABLE IF EXISTS `grounp`;
CREATE TABLE `grounp` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `roomId` int(11) DEFAULT NULL,
  `code` smallint(4) DEFAULT NULL,
  `name` varchar(10) DEFAULT NULL,
  `checkCode` varchar(10) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=MyISAM DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of grounp
-- ----------------------------

-- ----------------------------
-- Table structure for grounp_user
-- ----------------------------
DROP TABLE IF EXISTS `grounp_user`;
CREATE TABLE `grounp_user` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `grounp_id` int(11) DEFAULT NULL,
  `user_id` int(11) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=MyISAM DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of grounp_user
-- ----------------------------

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
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=15 DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of machine
-- ----------------------------
INSERT INTO `machine` VALUES ('1', '3', '071b41fa83732de2a508ab0a6d933b17fa6275851', 'Desktop', 'Windows 7 Service Pack 1 (6.1.7601) 64bit');
INSERT INTO `machine` VALUES ('2', '3', '071b41fa83732de2a508ab0a6d933b17fa6275852', 'Desktop', 'Windows 7 Service Pack 1 (6.1.7601) 64bit');
INSERT INTO `machine` VALUES ('5', '3', '071b41fa83732de2a508ab0a6d933b17fa627585', 'Desktop', 'Windows 7 Service Pack 1 (6.1.7601) 64bit');
INSERT INTO `machine` VALUES ('6', '3', '9150917497e113d324b8e65b08ea4792', 'Handheld', 'Android OS 8.1.0 / API-27 (OPM1.171019.026/eng.compil.20190620.170734)');
INSERT INTO `machine` VALUES ('7', '3', '6d848b05df0133b1fb81ccc44ecd9f5b', 'Handheld', 'Android OS 6.0.1 / API-23 (V417IR/eng.root.20190613.105054)');
INSERT INTO `machine` VALUES ('8', '3', 'a598ce756d134b321855df25a392afdb', 'Handheld', 'Android OS 8.0.0 / API-26 (HUAWEIBKL-AL20/172(C00))');
INSERT INTO `machine` VALUES ('9', '3', '877d946d60b29a01bc830439a3e8ed9b73dadced', 'Desktop', 'Windows 10  (10.0.0) 64bit');
INSERT INTO `machine` VALUES ('10', '3', '0246b7842dad61b69c62c62b34680952', 'Handheld', 'Android OS 7.0 / API-24 (HUAWEIMHA-AL00/C00B233)');
INSERT INTO `machine` VALUES ('11', '3', '55c6d53cc3e45dd66e2fd6e570c6e0a6', 'Handheld', 'Android OS 9 / API-28 (HUAWEIYAL-AL00/140C00)');
INSERT INTO `machine` VALUES ('12', '3', '4bbf1e67960665ad269ee9210b0863a0', 'Handheld', 'Android OS 6.0 / API-23 (MRA58K/1557974676)');
INSERT INTO `machine` VALUES ('13', '3', '6b80b8354c1aa5c3ed4e6cb0f64f4c42', 'Handheld', 'Android OS 9 / API-28 (HUAWEIHMA-AL00/200C00R1)');
INSERT INTO `machine` VALUES ('14', '3', '28eefa521846b97fc3a45d618e70f14c', 'Handheld', 'Android OS 9 / API-28 (HUAWEIPAR-AL00/187C00R1)');

-- ----------------------------
-- Table structure for room
-- ----------------------------
DROP TABLE IF EXISTS `room`;
CREATE TABLE `room` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `name` varchar(10) DEFAULT NULL,
  `area` varchar(10) DEFAULT NULL,
  `userId` int(11) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=MyISAM DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of room
-- ----------------------------

-- ----------------------------
-- Table structure for user
-- ----------------------------
DROP TABLE IF EXISTS `user`;
CREATE TABLE `user` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `telephone` varchar(255) DEFAULT '',
  `password` varchar(255) DEFAULT '',
  `nick` varchar(255) DEFAULT '',
  `image` varchar(255) DEFAULT '',
  `type` smallint(2) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=15 DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of user
-- ----------------------------
INSERT INTO `user` VALUES ('6', '18392120350', '123456', '你好', 'image1', null);
INSERT INTO `user` VALUES ('7', '18392120357', '123', '天涯', 'image1', null);
INSERT INTO `user` VALUES ('8', '15129651983', '12345', '蜡笔小新', 'image1', null);
INSERT INTO `user` VALUES ('9', '18991843282', '57y3vrtj', '古代', 'image1', null);
INSERT INTO `user` VALUES ('10', '18392120358', '123', '真人cs', 'image1', null);
INSERT INTO `user` VALUES ('11', '13759974762', 'wsf81130464', '清风无极', 'image1', null);
INSERT INTO `user` VALUES ('12', '13319234739', '123789', '末见', 'image1', null);
INSERT INTO `user` VALUES ('13', '13309234732', '123789', '莫看', 'image1', null);
INSERT INTO `user` VALUES ('14', '18392120355', '123', 'CS', 'image1', null);
