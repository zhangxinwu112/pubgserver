/*
Navicat MySQL Data Transfer

Source Server         : localhost
Source Server Version : 50528
Source Host           : localhost:3306
Source Database       : cs

Target Server Type    : MYSQL
Target Server Version : 50528
File Encoding         : 65001

Date: 2020-02-03 21:10:30
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
  `name` varchar(10) DEFAULT NULL,
  `area` varchar(10) DEFAULT NULL,
  `userId` int(11) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=MyISAM AUTO_INCREMENT=55 DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of grounp
-- ----------------------------
INSERT INTO `grounp` VALUES ('53', '123', 'cs', '16');
INSERT INTO `grounp` VALUES ('54', '456', 'cs', '16');

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
) ENGINE=MyISAM AUTO_INCREMENT=212 DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of room
-- ----------------------------
INSERT INTO `room` VALUES ('208', '54', '2', '房间2', '123456');
INSERT INTO `room` VALUES ('207', '54', '1', '房间456', '123456');
INSERT INTO `room` VALUES ('206', '53', '5', '房间5', '123456');
INSERT INTO `room` VALUES ('205', '53', '4', '房间4', '123456');
INSERT INTO `room` VALUES ('202', '53', '1', '房间1', '123456');
INSERT INTO `room` VALUES ('203', '53', '2', '房间2', '123456');
INSERT INTO `room` VALUES ('204', '53', '3', '房间3', '123456');
INSERT INTO `room` VALUES ('210', '54', '4', '房间4', '123456');
INSERT INTO `room` VALUES ('209', '54', '3', '房间3', '123456');
INSERT INTO `room` VALUES ('211', '54', '5', '房间5', '123456');

-- ----------------------------
-- Table structure for room_user
-- ----------------------------
DROP TABLE IF EXISTS `room_user`;
CREATE TABLE `room_user` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `room_id` int(11) DEFAULT NULL,
  `user_id` int(11) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=MyISAM AUTO_INCREMENT=21 DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of room_user
-- ----------------------------
INSERT INTO `room_user` VALUES ('20', '207', '15');

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
) ENGINE=InnoDB AUTO_INCREMENT=19 DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of user
-- ----------------------------
INSERT INTO `user` VALUES ('15', '17391767972', '123456', '天涯', 'image1', '0');
INSERT INTO `user` VALUES ('16', '17391767973', '123456', '管理员', 'image1', '1');
INSERT INTO `user` VALUES ('17', '17391767974', '123456', '测试管理员', 'image1', '1');
INSERT INTO `user` VALUES ('18', '17391767979', '123456', '玩家测试', 'image1', '0');
