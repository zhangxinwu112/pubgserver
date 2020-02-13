/*
Navicat MySQL Data Transfer

Source Server         : bendi
Source Server Version : 50528
Source Host           : localhost:3306
Source Database       : cs

Target Server Type    : MYSQL
Target Server Version : 50528
File Encoding         : 65001

Date: 2020-02-13 22:33:06
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
) ENGINE=MyISAM AUTO_INCREMENT=118 DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of grounp
-- ----------------------------
INSERT INTO `grounp` VALUES ('116', '管六安22', '陕西地区', '31', '-1', '25', '-1.000000', '-1.000000', '2000', '2000', '25896', '10');
INSERT INTO `grounp` VALUES ('117', '二管家', '陕西地区', '33', '-1', '60', '-1.000000', '-1.000000', '2000', '2000', '123456', '10');

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
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of life
-- ----------------------------
INSERT INTO `life` VALUES ('1', '80', '50', '35', '32');

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
) ENGINE=InnoDB AUTO_INCREMENT=52 DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of machine
-- ----------------------------
INSERT INTO `machine` VALUES ('1', '1', 'be367d090e0f13218448182032916d36525aeba0', 'Desktop', 'Windows 10  (10.0.0) 64bit', '31');
INSERT INTO `machine` VALUES ('2', '1', 'be367d090e0f13218448182032916d36525aeba0', 'Desktop', 'Windows 10  (10.0.0) 64bit', '31');
INSERT INTO `machine` VALUES ('3', '3', 'be367d090e0f13218448182032916d36525aeba0', 'Desktop', 'Windows 10  (10.0.0) 64bit', null);
INSERT INTO `machine` VALUES ('4', '3', 'be367d090e0f13218448182032916d36525aeba0', 'Desktop', 'Windows 10  (10.0.0) 64bit', '31');
INSERT INTO `machine` VALUES ('5', '1', 'be367d090e0f13218448182032916d36525aeba0', 'Desktop', 'Windows 10  (10.0.0) 64bit', '32');
INSERT INTO `machine` VALUES ('6', '1', 'be367d090e0f13218448182032916d36525aeba0', 'Desktop', 'Windows 10  (10.0.0) 64bit', '32');
INSERT INTO `machine` VALUES ('7', '3', 'be367d090e0f13218448182032916d36525aeba0', 'Desktop', 'Windows 10  (10.0.0) 64bit', '31');
INSERT INTO `machine` VALUES ('8', '3', 'be367d090e0f13218448182032916d36525aeba0', 'Desktop', 'Windows 10  (10.0.0) 64bit', '31');
INSERT INTO `machine` VALUES ('9', '3', 'be367d090e0f13218448182032916d36525aeba0', 'Desktop', 'Windows 10  (10.0.0) 64bit', '31');
INSERT INTO `machine` VALUES ('10', '3', 'be367d090e0f13218448182032916d36525aeba0', 'Desktop', 'Windows 10  (10.0.0) 64bit', '31');
INSERT INTO `machine` VALUES ('11', '3', 'be367d090e0f13218448182032916d36525aeba0', 'Desktop', 'Windows 10  (10.0.0) 64bit', '31');
INSERT INTO `machine` VALUES ('12', '1', 'be367d090e0f13218448182032916d36525aeba0', 'Desktop', 'Windows 10  (10.0.0) 64bit', '32');
INSERT INTO `machine` VALUES ('13', '3', 'be367d090e0f13218448182032916d36525aeba0', 'Desktop', 'Windows 10  (10.0.0) 64bit', '31');
INSERT INTO `machine` VALUES ('14', '3', 'be367d090e0f13218448182032916d36525aeba0', 'Desktop', 'Windows 10  (10.0.0) 64bit', '31');
INSERT INTO `machine` VALUES ('15', '3', 'be367d090e0f13218448182032916d36525aeba0', 'Desktop', 'Windows 10  (10.0.0) 64bit', '31');
INSERT INTO `machine` VALUES ('16', '3', 'be367d090e0f13218448182032916d36525aeba0', 'Desktop', 'Windows 10  (10.0.0) 64bit', '31');
INSERT INTO `machine` VALUES ('17', '3', 'be367d090e0f13218448182032916d36525aeba0', 'Desktop', 'Windows 10  (10.0.0) 64bit', '31');
INSERT INTO `machine` VALUES ('18', '3', 'be367d090e0f13218448182032916d36525aeba0', 'Desktop', 'Windows 10  (10.0.0) 64bit', '31');
INSERT INTO `machine` VALUES ('19', '3', 'be367d090e0f13218448182032916d36525aeba0', 'Desktop', 'Windows 10  (10.0.0) 64bit', '31');
INSERT INTO `machine` VALUES ('20', '1', 'be367d090e0f13218448182032916d36525aeba0', 'Desktop', 'Windows 10  (10.0.0) 64bit', '32');
INSERT INTO `machine` VALUES ('21', '1', 'be367d090e0f13218448182032916d36525aeba0', 'Desktop', 'Windows 10  (10.0.0) 64bit', '32');
INSERT INTO `machine` VALUES ('22', '3', 'be367d090e0f13218448182032916d36525aeba0', 'Desktop', 'Windows 10  (10.0.0) 64bit', '31');
INSERT INTO `machine` VALUES ('23', '3', 'be367d090e0f13218448182032916d36525aeba0', 'Desktop', 'Windows 10  (10.0.0) 64bit', '31');
INSERT INTO `machine` VALUES ('24', '3', 'be367d090e0f13218448182032916d36525aeba0', 'Desktop', 'Windows 10  (10.0.0) 64bit', '31');
INSERT INTO `machine` VALUES ('25', '3', 'be367d090e0f13218448182032916d36525aeba0', 'Desktop', 'Windows 10  (10.0.0) 64bit', '33');
INSERT INTO `machine` VALUES ('26', '3', 'be367d090e0f13218448182032916d36525aeba0', 'Desktop', 'Windows 10  (10.0.0) 64bit', '33');
INSERT INTO `machine` VALUES ('27', '3', 'be367d090e0f13218448182032916d36525aeba0', 'Desktop', 'Windows 10  (10.0.0) 64bit', '31');
INSERT INTO `machine` VALUES ('28', '3', 'be367d090e0f13218448182032916d36525aeba0', 'Desktop', 'Windows 10  (10.0.0) 64bit', '31');
INSERT INTO `machine` VALUES ('29', '1', 'be367d090e0f13218448182032916d36525aeba0', 'Desktop', 'Windows 10  (10.0.0) 64bit', '32');
INSERT INTO `machine` VALUES ('30', '3', 'be367d090e0f13218448182032916d36525aeba0', 'Desktop', 'Windows 10  (10.0.0) 64bit', '31');
INSERT INTO `machine` VALUES ('31', '3', 'be367d090e0f13218448182032916d36525aeba0', 'Desktop', 'Windows 10  (10.0.0) 64bit', '31');
INSERT INTO `machine` VALUES ('32', '3', 'be367d090e0f13218448182032916d36525aeba0', 'Desktop', 'Windows 10  (10.0.0) 64bit', '31');
INSERT INTO `machine` VALUES ('33', '3', 'be367d090e0f13218448182032916d36525aeba0', 'Desktop', 'Windows 10  (10.0.0) 64bit', '31');
INSERT INTO `machine` VALUES ('34', '3', 'be367d090e0f13218448182032916d36525aeba0', 'Desktop', 'Windows 10  (10.0.0) 64bit', '31');
INSERT INTO `machine` VALUES ('35', '3', 'be367d090e0f13218448182032916d36525aeba0', 'Desktop', 'Windows 10  (10.0.0) 64bit', '31');
INSERT INTO `machine` VALUES ('36', '3', 'be367d090e0f13218448182032916d36525aeba0', 'Desktop', 'Windows 10  (10.0.0) 64bit', '31');
INSERT INTO `machine` VALUES ('37', '1', 'be367d090e0f13218448182032916d36525aeba0', 'Desktop', 'Windows 10  (10.0.0) 64bit', '32');
INSERT INTO `machine` VALUES ('38', '1', 'be367d090e0f13218448182032916d36525aeba0', 'Desktop', 'Windows 10  (10.0.0) 64bit', '32');
INSERT INTO `machine` VALUES ('39', '1', 'be367d090e0f13218448182032916d36525aeba0', 'Desktop', 'Windows 10  (10.0.0) 64bit', '32');
INSERT INTO `machine` VALUES ('40', '1', 'be367d090e0f13218448182032916d36525aeba0', 'Desktop', 'Windows 10  (10.0.0) 64bit', '32');
INSERT INTO `machine` VALUES ('41', '1', 'be367d090e0f13218448182032916d36525aeba0', 'Desktop', 'Windows 10  (10.0.0) 64bit', '32');
INSERT INTO `machine` VALUES ('42', '1', 'be367d090e0f13218448182032916d36525aeba0', 'Desktop', 'Windows 10  (10.0.0) 64bit', '32');
INSERT INTO `machine` VALUES ('43', '1', 'be367d090e0f13218448182032916d36525aeba0', 'Desktop', 'Windows 10  (10.0.0) 64bit', '32');
INSERT INTO `machine` VALUES ('44', '1', 'be367d090e0f13218448182032916d36525aeba0', 'Desktop', 'Windows 10  (10.0.0) 64bit', '32');
INSERT INTO `machine` VALUES ('45', '1', 'be367d090e0f13218448182032916d36525aeba0', 'Desktop', 'Windows 10  (10.0.0) 64bit', '32');
INSERT INTO `machine` VALUES ('46', '1', 'be367d090e0f13218448182032916d36525aeba0', 'Desktop', 'Windows 10  (10.0.0) 64bit', '32');
INSERT INTO `machine` VALUES ('47', '1', 'be367d090e0f13218448182032916d36525aeba0', 'Desktop', 'Windows 10  (10.0.0) 64bit', '32');
INSERT INTO `machine` VALUES ('48', '1', 'be367d090e0f13218448182032916d36525aeba0', 'Desktop', 'Windows 10  (10.0.0) 64bit', '32');
INSERT INTO `machine` VALUES ('49', '1', 'be367d090e0f13218448182032916d36525aeba0', 'Desktop', 'Windows 10  (10.0.0) 64bit', '32');
INSERT INTO `machine` VALUES ('50', '1', 'be367d090e0f13218448182032916d36525aeba0', 'Desktop', 'Windows 10  (10.0.0) 64bit', '32');
INSERT INTO `machine` VALUES ('51', '1', 'be367d090e0f13218448182032916d36525aeba0', 'Desktop', 'Windows 10  (10.0.0) 64bit', '32');

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
) ENGINE=MyISAM AUTO_INCREMENT=513 DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of room
-- ----------------------------
INSERT INTO `room` VALUES ('512', '117', '344', '44', '32', '-1');

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
) ENGINE=MyISAM AUTO_INCREMENT=46 DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of room_user
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
) ENGINE=InnoDB AUTO_INCREMENT=34 DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of user
-- ----------------------------
INSERT INTO `user` VALUES ('31', '17391767973', '123456', '管六安', 'image1', '1');
INSERT INTO `user` VALUES ('32', '17391767972', '123456', '天涯', 'image1', '0');
INSERT INTO `user` VALUES ('33', '17391767974', '123456', '二管家', 'image1', '1');
