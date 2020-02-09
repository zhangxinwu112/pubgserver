/*
Navicat MySQL Data Transfer

Source Server         : localhost
Source Server Version : 50528
Source Host           : localhost:3306
Source Database       : cs

Target Server Type    : MYSQL
Target Server Version : 50528
File Encoding         : 65001

Date: 2020-02-09 12:54:01
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
INSERT INTO `code` VALUES ('1', '000001', '0', '-1', '0');
INSERT INTO `code` VALUES ('2', '000002', '1', '0', '1563960333');
INSERT INTO `code` VALUES ('3', '000003', '0', '15', '0');

-- ----------------------------
-- Table structure for grounp
-- ----------------------------
DROP TABLE IF EXISTS `grounp`;
CREATE TABLE `grounp` (
  `id` int(11) NOT NULL AUTO_INCREMENT COMMENT '运行状态-表示停止，0表示运行。',
  `name` varchar(10) DEFAULT NULL,
  `area` varchar(10) DEFAULT NULL,
  `userId` int(11) DEFAULT NULL,
  `runState` smallint(2) DEFAULT '-1',
  `playerTime` smallint(2) DEFAULT '0',
  `fenceLon` double(12,6) DEFAULT '-1.000000',
  `fenceLat` double(12,6) DEFAULT '-1.000000',
  `fenceRadius` int(10) DEFAULT '2000',
  `fenceTotalRadius` int(10) DEFAULT '2000',
  PRIMARY KEY (`id`)
) ENGINE=MyISAM AUTO_INCREMENT=115 DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of grounp
-- ----------------------------
INSERT INTO `grounp` VALUES ('114', '123', 'cs', '27', '-1', '60', '-1.000000', '-1.000000', '2000', '2000');

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
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of life
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
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of machine
-- ----------------------------

-- ----------------------------
-- Table structure for room
-- ----------------------------
DROP TABLE IF EXISTS `room`;
CREATE TABLE `room` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `grounpId` int(11) DEFAULT NULL,
  `code` smallint(4) DEFAULT NULL,
  `name` varchar(10) DEFAULT NULL,
  `checkCode` varchar(10) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=MyISAM AUTO_INCREMENT=512 DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of room
-- ----------------------------
INSERT INTO `room` VALUES ('507', '114', '1', '房间1', '123456');
INSERT INTO `room` VALUES ('508', '114', '2', '房间2', '123456');
INSERT INTO `room` VALUES ('509', '114', '3', '房间3', '123456');
INSERT INTO `room` VALUES ('510', '114', '4', '房间4', '123456');
INSERT INTO `room` VALUES ('511', '114', '5', '房间5', '123456');

-- ----------------------------
-- Table structure for room_user
-- ----------------------------
DROP TABLE IF EXISTS `room_user`;
CREATE TABLE `room_user` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `room_id` int(11) DEFAULT NULL,
  `user_id` int(11) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=MyISAM AUTO_INCREMENT=46 DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of room_user
-- ----------------------------
INSERT INTO `room_user` VALUES ('45', '507', '28');

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
) ENGINE=InnoDB AUTO_INCREMENT=29 DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of user
-- ----------------------------
INSERT INTO `user` VALUES ('27', '17391767973', '123456', '管理员', 'image1', '1');
INSERT INTO `user` VALUES ('28', '17391767972', '123456', '天涯', 'image1', '0');
