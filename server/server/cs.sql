/*
Navicat MySQL Data Transfer

Source Server         : localhost_3306
Source Server Version : 50528
Source Host           : localhost:3306
Source Database       : cs

Target Server Type    : MYSQL
Target Server Version : 50528
File Encoding         : 65001

Date: 2019-07-24 20:15:48
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
INSERT INTO `code` VALUES ('3', '00003', '0', '10', '0');

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
) ENGINE=InnoDB AUTO_INCREMENT=8 DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of machine
-- ----------------------------
INSERT INTO `machine` VALUES ('1', '3', '071b41fa83732de2a508ab0a6d933b17fa6275851', 'Desktop', 'Windows 7 Service Pack 1 (6.1.7601) 64bit');
INSERT INTO `machine` VALUES ('2', '3', '071b41fa83732de2a508ab0a6d933b17fa6275852', 'Desktop', 'Windows 7 Service Pack 1 (6.1.7601) 64bit');
INSERT INTO `machine` VALUES ('5', '3', '071b41fa83732de2a508ab0a6d933b17fa627585', 'Desktop', 'Windows 7 Service Pack 1 (6.1.7601) 64bit');
INSERT INTO `machine` VALUES ('6', '3', '9150917497e113d324b8e65b08ea4792', 'Handheld', 'Android OS 8.1.0 / API-27 (OPM1.171019.026/eng.compil.20190620.170734)');
INSERT INTO `machine` VALUES ('7', '3', '6d848b05df0133b1fb81ccc44ecd9f5b', 'Handheld', 'Android OS 6.0.1 / API-23 (V417IR/eng.root.20190613.105054)');

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
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=8 DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of user
-- ----------------------------
INSERT INTO `user` VALUES ('6', '18392120350', '123456', '你好', 'image1');
INSERT INTO `user` VALUES ('7', '18392120357', '123', '天涯', 'image1');
