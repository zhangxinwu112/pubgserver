/*
Navicat MySQL Data Transfer

Source Server         : localhost
Source Server Version : 50528
Source Host           : localhost:3306
Source Database       : cs

Target Server Type    : MYSQL
Target Server Version : 50528
File Encoding         : 65001

Date: 2020-03-11 22:01:32
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
) ENGINE=MyISAM AUTO_INCREMENT=135 DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of grounp
-- ----------------------------
INSERT INTO `grounp` VALUES ('132', 'cs', '陕西地区', '80', '-1', '15', '108.966431', '34.013476', '4373', '4373', '123456', '10', '900');
INSERT INTO `grounp` VALUES ('133', '玩家1', '陕西地区', '83', '-1', '60', '-1.000000', '-1.000000', '2000', '2000', '123456', '10', '3600');
INSERT INTO `grounp` VALUES ('134', '汤峪cs', '陕西地区', '88', '-1', '60', '-1.000000', '-1.000000', '2000', '2000', '456789', '10', '3600');

-- ----------------------------
-- Table structure for life
-- ----------------------------
DROP TABLE IF EXISTS `life`;
CREATE TABLE `life` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `bulletCount` smallint(3) DEFAULT '0' COMMENT '弹量',
  `lifeValue` smallint(3) DEFAULT '0' COMMENT '生命值',
  `fightScore` smallint(3) DEFAULT '0' COMMENT '战绩',
  `userId` int(11) DEFAULT '-1',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=42 DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of life
-- ----------------------------
INSERT INTO `life` VALUES ('32', '0', '0', '35', '78');
INSERT INTO `life` VALUES ('33', '0', '0', '35', '79');
INSERT INTO `life` VALUES ('34', '0', '0', '35', '81');
INSERT INTO `life` VALUES ('38', '0', '30', '35', '86');
INSERT INTO `life` VALUES ('39', '80', '25', '35', '87');
INSERT INTO `life` VALUES ('40', '0', '145', '35', '89');
INSERT INTO `life` VALUES ('41', '0', '0', '35', '90');
INSERT INTO `life` VALUES ('350', '0', '0', '35', '82');
INSERT INTO `life` VALUES ('370', '0', '89', '35', '85');
INSERT INTO `life` VALUES ('3600', '0', '0', '35', '84');

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
) ENGINE=InnoDB AUTO_INCREMENT=434 DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of machine
-- ----------------------------
INSERT INTO `machine` VALUES ('352', '1', 'bc377acf35e322f29c45b07f9b773282', 'Handheld', 'Android OS 10 / API-29 (HUAWEITNY-AL00/10.0.0.189C00)', '78');
INSERT INTO `machine` VALUES ('353', '1', 'bc377acf35e322f29c45b07f9b773282', 'Handheld', 'Android OS 10 / API-29 (HUAWEITNY-AL00/10.0.0.189C00)', '78');
INSERT INTO `machine` VALUES ('354', '3', 'bc377acf35e322f29c45b07f9b773282', 'Handheld', 'Android OS 10 / API-29 (HUAWEITNY-AL00/10.0.0.189C00)', null);
INSERT INTO `machine` VALUES ('355', '3', 'bc377acf35e322f29c45b07f9b773282', 'Handheld', 'Android OS 10 / API-29 (HUAWEITNY-AL00/10.0.0.189C00)', '80');
INSERT INTO `machine` VALUES ('356', '3', 'ed5b44d5a642195d47dad4a01df8cc57', 'Handheld', 'Android OS 8.1.0 / API-27 (OPM1.171019.026/eng.compil.20191220.211147)', null);
INSERT INTO `machine` VALUES ('357', '3', 'ed5b44d5a642195d47dad4a01df8cc57', 'Handheld', 'Android OS 8.1.0 / API-27 (OPM1.171019.026/eng.compil.20191220.211147)', '80');
INSERT INTO `machine` VALUES ('358', '3', 'bc377acf35e322f29c45b07f9b773282', 'Handheld', 'Android OS 10 / API-29 (HUAWEITNY-AL00/10.0.0.189C00)', '80');
INSERT INTO `machine` VALUES ('359', '3', 'bc377acf35e322f29c45b07f9b773282', 'Handheld', 'Android OS 10 / API-29 (HUAWEITNY-AL00/10.0.0.189C00)', '80');
INSERT INTO `machine` VALUES ('360', '1', 'bc377acf35e322f29c45b07f9b773282', 'Handheld', 'Android OS 10 / API-29 (HUAWEITNY-AL00/10.0.0.189C00)', '78');
INSERT INTO `machine` VALUES ('361', '1', 'ed5b44d5a642195d47dad4a01df8cc57', 'Handheld', 'Android OS 8.1.0 / API-27 (OPM1.171019.026/eng.compil.20191220.211147)', '81');
INSERT INTO `machine` VALUES ('362', '1', 'bc377acf35e322f29c45b07f9b773282', 'Handheld', 'Android OS 10 / API-29 (HUAWEITNY-AL00/10.0.0.189C00)', '79');
INSERT INTO `machine` VALUES ('363', '1', 'bc377acf35e322f29c45b07f9b773282', 'Handheld', 'Android OS 10 / API-29 (HUAWEITNY-AL00/10.0.0.189C00)', '78');
INSERT INTO `machine` VALUES ('364', '1', 'bc377acf35e322f29c45b07f9b773282', 'Handheld', 'Android OS 10 / API-29 (HUAWEITNY-AL00/10.0.0.189C00)', '79');
INSERT INTO `machine` VALUES ('365', '1', '1550ab4501145f6b242c72d66d28c36e', 'Handheld', 'Android OS 9 / API-28 (HONORLLD-AL10/9.1.0.130C00)', '82');
INSERT INTO `machine` VALUES ('366', '1', 'ed5b44d5a642195d47dad4a01df8cc57', 'Handheld', 'Android OS 8.1.0 / API-27 (OPM1.171019.026/eng.compil.20191220.211147)', '81');
INSERT INTO `machine` VALUES ('367', '1', 'bc377acf35e322f29c45b07f9b773282', 'Handheld', 'Android OS 10 / API-29 (HUAWEITNY-AL00/10.0.0.189C00)', '79');
INSERT INTO `machine` VALUES ('368', '1', 'bc377acf35e322f29c45b07f9b773282', 'Handheld', 'Android OS 10 / API-29 (HUAWEITNY-AL00/10.0.0.189C00)', '78');
INSERT INTO `machine` VALUES ('369', '1', 'cda5d6a114f48c53a029df3c64ade7f5', 'Handheld', 'Android OS 8.1.0 / API-27 (OPM1.171019.019/V9.5.6.0.OEBCNFA)', '84');
INSERT INTO `machine` VALUES ('370', '3', 'bc377acf35e322f29c45b07f9b773282', 'Handheld', 'Android OS 10 / API-29 (HUAWEITNY-AL00/10.0.0.189C00)', '80');
INSERT INTO `machine` VALUES ('371', '1', '5211d2fb5748d054994ab2d2854cda8d', 'Handheld', 'Android OS 9 / API-28 (HUAWEIGLK-AL00/9.1.0.185C00)', '85');
INSERT INTO `machine` VALUES ('372', '1', 'bc377acf35e322f29c45b07f9b773282', 'Handheld', 'Android OS 10 / API-29 (HUAWEITNY-AL00/10.0.0.189C00)', '78');
INSERT INTO `machine` VALUES ('373', '1', '1550ab4501145f6b242c72d66d28c36e', 'Handheld', 'Android OS 9 / API-28 (HONORLLD-AL10/9.1.0.130C00)', '82');
INSERT INTO `machine` VALUES ('374', '1', 'cda5d6a114f48c53a029df3c64ade7f5', 'Handheld', 'Android OS 8.1.0 / API-27 (OPM1.171019.019/V9.5.6.0.OEBCNFA)', '84');
INSERT INTO `machine` VALUES ('375', '1', 'ed5b44d5a642195d47dad4a01df8cc57', 'Handheld', 'Android OS 8.1.0 / API-27 (OPM1.171019.026/eng.compil.20191220.211147)', '81');
INSERT INTO `machine` VALUES ('376', '1', 'ff18a155b50a86719620f42b178290fc', 'Handheld', 'Android OS 10 / API-29 (HUAWEIYAL-AL00/10.0.0.190C00)', '86');
INSERT INTO `machine` VALUES ('377', '1', 'bc377acf35e322f29c45b07f9b773282', 'Handheld', 'Android OS 10 / API-29 (HUAWEITNY-AL00/10.0.0.189C00)', '79');
INSERT INTO `machine` VALUES ('378', '3', 'bc377acf35e322f29c45b07f9b773282', 'Handheld', 'Android OS 10 / API-29 (HUAWEITNY-AL00/10.0.0.189C00)', '80');
INSERT INTO `machine` VALUES ('379', '1', 'ed5b44d5a642195d47dad4a01df8cc57', 'Handheld', 'Android OS 8.1.0 / API-27 (OPM1.171019.026/eng.compil.20191220.211147)', '81');
INSERT INTO `machine` VALUES ('380', '3', 'bc377acf35e322f29c45b07f9b773282', 'Handheld', 'Android OS 10 / API-29 (HUAWEITNY-AL00/10.0.0.189C00)', '80');
INSERT INTO `machine` VALUES ('381', '1', 'bc377acf35e322f29c45b07f9b773282', 'Handheld', 'Android OS 10 / API-29 (HUAWEITNY-AL00/10.0.0.189C00)', '87');
INSERT INTO `machine` VALUES ('382', '3', 'ed5b44d5a642195d47dad4a01df8cc57', 'Handheld', 'Android OS 8.1.0 / API-27 (OPM1.171019.026/eng.compil.20191220.211147)', '88');
INSERT INTO `machine` VALUES ('383', '1', '5f00ab0828e04e4ba294fb16a9f05a89', 'Handheld', 'Android OS 9 / API-28 (PPR1.180610.011/V11.0.3.0.PCGCNXM)', '81');
INSERT INTO `machine` VALUES ('384', '1', '5f00ab0828e04e4ba294fb16a9f05a89', 'Handheld', 'Android OS 9 / API-28 (PPR1.180610.011/V11.0.3.0.PCGCNXM)', '89');
INSERT INTO `machine` VALUES ('385', '1', '1550ab4501145f6b242c72d66d28c36e', 'Handheld', 'Android OS 9 / API-28 (HONORLLD-AL10/9.1.0.130C00)', '82');
INSERT INTO `machine` VALUES ('386', '1', '83e2ab31a28bf66269ffe2a021757744', 'Handheld', 'Android OS 7.1.1 / API-25 (N6F26Q/1564746880)', '90');
INSERT INTO `machine` VALUES ('387', '3', 'bc377acf35e322f29c45b07f9b773282', 'Handheld', 'Android OS 10 / API-29 (HUAWEITNY-AL00/10.0.0.189C00)', '80');
INSERT INTO `machine` VALUES ('388', '1', 'cda5d6a114f48c53a029df3c64ade7f5', 'Handheld', 'Android OS 8.1.0 / API-27 (OPM1.171019.019/V9.5.6.0.OEBCNFA)', '84');
INSERT INTO `machine` VALUES ('389', '1', 'bc377acf35e322f29c45b07f9b773282', 'Handheld', 'Android OS 10 / API-29 (HUAWEITNY-AL00/10.0.0.189C00)', '87');
INSERT INTO `machine` VALUES ('390', '1', '83e2ab31a28bf66269ffe2a021757744', 'Handheld', 'Android OS 7.1.1 / API-25 (N6F26Q/1564746880)', '90');
INSERT INTO `machine` VALUES ('391', '1', '83e2ab31a28bf66269ffe2a021757744', 'Handheld', 'Android OS 7.1.1 / API-25 (N6F26Q/1564746880)', '90');
INSERT INTO `machine` VALUES ('392', '1', '83e2ab31a28bf66269ffe2a021757744', 'Handheld', 'Android OS 7.1.1 / API-25 (N6F26Q/1564746880)', '89');
INSERT INTO `machine` VALUES ('393', '1', '5f00ab0828e04e4ba294fb16a9f05a89', 'Handheld', 'Android OS 9 / API-28 (PPR1.180610.011/V11.0.3.0.PCGCNXM)', '90');
INSERT INTO `machine` VALUES ('394', '3', '5f00ab0828e04e4ba294fb16a9f05a89', 'Handheld', 'Android OS 9 / API-28 (PPR1.180610.011/V11.0.3.0.PCGCNXM)', null);
INSERT INTO `machine` VALUES ('395', '3', '5f00ab0828e04e4ba294fb16a9f05a89', 'Handheld', 'Android OS 9 / API-28 (PPR1.180610.011/V11.0.3.0.PCGCNXM)', '88');
INSERT INTO `machine` VALUES ('396', '1', '4c177a35947eac9e2aed9743a1bd4a9d07aadf96', 'Desktop', 'Windows 7  (6.1.0) 64bit', '89');
INSERT INTO `machine` VALUES ('397', '1', '4c177a35947eac9e2aed9743a1bd4a9d07aadf96', 'Desktop', 'Windows 7  (6.1.0) 64bit', '89');
INSERT INTO `machine` VALUES ('398', '1', '4c177a35947eac9e2aed9743a1bd4a9d07aadf96', 'Desktop', 'Windows 7  (6.1.0) 64bit', '89');
INSERT INTO `machine` VALUES ('399', '1', '4c177a35947eac9e2aed9743a1bd4a9d07aadf96', 'Desktop', 'Windows 7  (6.1.0) 64bit', '89');
INSERT INTO `machine` VALUES ('400', '3', '4c177a35947eac9e2aed9743a1bd4a9d07aadf96', 'Desktop', 'Windows 7  (6.1.0) 64bit', null);
INSERT INTO `machine` VALUES ('401', '3', '4c177a35947eac9e2aed9743a1bd4a9d07aadf96', 'Desktop', 'Windows 7  (6.1.0) 64bit', '88');
INSERT INTO `machine` VALUES ('402', '3', '4c177a35947eac9e2aed9743a1bd4a9d07aadf96', 'Desktop', 'Windows 7  (6.1.0) 64bit', '88');
INSERT INTO `machine` VALUES ('403', '3', '4c177a35947eac9e2aed9743a1bd4a9d07aadf96', 'Desktop', 'Windows 7  (6.1.0) 64bit', '88');
INSERT INTO `machine` VALUES ('404', '3', '4c177a35947eac9e2aed9743a1bd4a9d07aadf96', 'Desktop', 'Windows 7  (6.1.0) 64bit', '88');
INSERT INTO `machine` VALUES ('405', '3', '4c177a35947eac9e2aed9743a1bd4a9d07aadf96', 'Desktop', 'Windows 7  (6.1.0) 64bit', '88');
INSERT INTO `machine` VALUES ('406', '3', '4c177a35947eac9e2aed9743a1bd4a9d07aadf96', 'Desktop', 'Windows 7  (6.1.0) 64bit', '88');
INSERT INTO `machine` VALUES ('407', '3', '4c177a35947eac9e2aed9743a1bd4a9d07aadf96', 'Desktop', 'Windows 7  (6.1.0) 64bit', '88');
INSERT INTO `machine` VALUES ('408', '3', '4c177a35947eac9e2aed9743a1bd4a9d07aadf96', 'Desktop', 'Windows 7  (6.1.0) 64bit', '88');
INSERT INTO `machine` VALUES ('409', '3', '4c177a35947eac9e2aed9743a1bd4a9d07aadf96', 'Desktop', 'Windows 7  (6.1.0) 64bit', '88');
INSERT INTO `machine` VALUES ('410', '3', '4c177a35947eac9e2aed9743a1bd4a9d07aadf96', 'Desktop', 'Windows 7  (6.1.0) 64bit', '88');
INSERT INTO `machine` VALUES ('411', '3', '4c177a35947eac9e2aed9743a1bd4a9d07aadf96', 'Desktop', 'Windows 7  (6.1.0) 64bit', '88');
INSERT INTO `machine` VALUES ('412', '3', '4c177a35947eac9e2aed9743a1bd4a9d07aadf96', 'Desktop', 'Windows 7  (6.1.0) 64bit', '88');
INSERT INTO `machine` VALUES ('413', '3', '4c177a35947eac9e2aed9743a1bd4a9d07aadf96', 'Desktop', 'Windows 7  (6.1.0) 64bit', '88');
INSERT INTO `machine` VALUES ('414', '3', '4c177a35947eac9e2aed9743a1bd4a9d07aadf96', 'Desktop', 'Windows 7  (6.1.0) 64bit', '88');
INSERT INTO `machine` VALUES ('415', '3', '4c177a35947eac9e2aed9743a1bd4a9d07aadf96', 'Desktop', 'Windows 7  (6.1.0) 64bit', '88');
INSERT INTO `machine` VALUES ('416', '3', '4c177a35947eac9e2aed9743a1bd4a9d07aadf96', 'Desktop', 'Windows 7  (6.1.0) 64bit', '88');
INSERT INTO `machine` VALUES ('417', '3', '4c177a35947eac9e2aed9743a1bd4a9d07aadf96', 'Desktop', 'Windows 7  (6.1.0) 64bit', '88');
INSERT INTO `machine` VALUES ('418', '3', '4c177a35947eac9e2aed9743a1bd4a9d07aadf96', 'Desktop', 'Windows 7  (6.1.0) 64bit', '88');
INSERT INTO `machine` VALUES ('419', '3', '4c177a35947eac9e2aed9743a1bd4a9d07aadf96', 'Desktop', 'Windows 7  (6.1.0) 64bit', '88');
INSERT INTO `machine` VALUES ('420', '3', '4c177a35947eac9e2aed9743a1bd4a9d07aadf96', 'Desktop', 'Windows 7  (6.1.0) 64bit', '88');
INSERT INTO `machine` VALUES ('421', '1', '4c177a35947eac9e2aed9743a1bd4a9d07aadf96', 'Desktop', 'Windows 7  (6.1.0) 64bit', '89');
INSERT INTO `machine` VALUES ('422', '1', '537c7560eb5630de1b5e1115b91b2847', 'Handheld', 'Android OS 5.1.1 / API-22 (LMY47I/N9500ZHS2BRC2)', '89');
INSERT INTO `machine` VALUES ('423', '1', '4bc2577d5991c03d3a99dff8b61ad690', 'Handheld', 'Android OS 6.0.1 / API-23 (V417IR/eng.root.20191227.103137)', '87');
INSERT INTO `machine` VALUES ('424', '1', '4bc2577d5991c03d3a99dff8b61ad690', 'Handheld', 'Android OS 6.0.1 / API-23 (V417IR/eng.root.20191227.103137)', '87');
INSERT INTO `machine` VALUES ('425', '1', '4bc2577d5991c03d3a99dff8b61ad690', 'Handheld', 'Android OS 6.0.1 / API-23 (V417IR/eng.root.20191227.103137)', '87');
INSERT INTO `machine` VALUES ('426', '1', '4bc2577d5991c03d3a99dff8b61ad690', 'Handheld', 'Android OS 6.0.1 / API-23 (V417IR/eng.root.20191227.103137)', '87');
INSERT INTO `machine` VALUES ('427', '1', '4bc2577d5991c03d3a99dff8b61ad690', 'Handheld', 'Android OS 6.0.1 / API-23 (V417IR/eng.root.20191227.103137)', '87');
INSERT INTO `machine` VALUES ('428', '1', '537c7560eb5630de1b5e1115b91b2847', 'Handheld', 'Android OS 5.1.1 / API-22 (LMY47I/N9500ZHS2BRC2)', '89');
INSERT INTO `machine` VALUES ('429', '1', '4bc2577d5991c03d3a99dff8b61ad690', 'Handheld', 'Android OS 6.0.1 / API-23 (V417IR/eng.root.20191227.103137)', '87');
INSERT INTO `machine` VALUES ('430', '1', '4bc2577d5991c03d3a99dff8b61ad690', 'Handheld', 'Android OS 6.0.1 / API-23 (V417IR/eng.root.20191227.103137)', '87');
INSERT INTO `machine` VALUES ('431', '1', '4bc2577d5991c03d3a99dff8b61ad690', 'Handheld', 'Android OS 6.0.1 / API-23 (V417IR/eng.root.20191227.103137)', '87');
INSERT INTO `machine` VALUES ('432', '3', '4c177a35947eac9e2aed9743a1bd4a9d07aadf96', 'Desktop', 'Windows 7  (6.1.0) 64bit', '88');
INSERT INTO `machine` VALUES ('433', '3', '4c177a35947eac9e2aed9743a1bd4a9d07aadf96', 'Desktop', 'Windows 7  (6.1.0) 64bit', '88');

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
) ENGINE=MyISAM AUTO_INCREMENT=535 DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of room
-- ----------------------------
INSERT INTO `room` VALUES ('531', '132', '野狼战队', '123456', '81', '-1');
INSERT INTO `room` VALUES ('532', '132', '野狼！', '123456', '78', '-1');
INSERT INTO `room` VALUES ('533', '134', '高堡队', '123456', '87', '-1');
INSERT INTO `room` VALUES ('534', '134', '红队', '180092', '82', '-1');

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
) ENGINE=MyISAM AUTO_INCREMENT=109 DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of room_user
-- ----------------------------
INSERT INTO `room_user` VALUES ('98', '531', '81', '-1');
INSERT INTO `room_user` VALUES ('99', '532', '78', '-1');
INSERT INTO `room_user` VALUES ('100', '531', '79', '-1');
INSERT INTO `room_user` VALUES ('107', '534', '82', '-1');
INSERT INTO `room_user` VALUES ('108', '534', '84', '-1');
INSERT INTO `room_user` VALUES ('103', '531', '85', '-1');
INSERT INTO `room_user` VALUES ('104', '531', '86', '-1');
INSERT INTO `room_user` VALUES ('105', '533', '87', '-1');
INSERT INTO `room_user` VALUES ('106', '533', '89', '-1');

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
) ENGINE=InnoDB AUTO_INCREMENT=98 DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of score
-- ----------------------------
INSERT INTO `score` VALUES ('78', '1582840328', '80', '50', '35', '532', '132', '78', '王毅正');
INSERT INTO `score` VALUES ('79', '1582840328', '80', '50', '35', '531', '132', '79', '台台王');
INSERT INTO `score` VALUES ('80', '1582840328', '80', '9', '35', '531', '132', '81', 'cs队长');
INSERT INTO `score` VALUES ('81', '1582840328', '80', '25', '35', '531', '132', '82', '星辰');
INSERT INTO `score` VALUES ('82', '1582840328', '80', '26', '35', '532', '132', '84', '玩家2');
INSERT INTO `score` VALUES ('83', '1582842278', '80', '50', '35', '532', '132', '78', '王毅正');
INSERT INTO `score` VALUES ('84', '1582842278', '80', '89', '35', '531', '132', '79', '台台王');
INSERT INTO `score` VALUES ('85', '1582842278', '80', '0', '35', '531', '132', '81', 'cs队长');
INSERT INTO `score` VALUES ('86', '1582842278', '80', '20', '35', '531', '132', '82', '星辰');
INSERT INTO `score` VALUES ('87', '1582842278', '80', '0', '35', '532', '132', '84', '玩家2');
INSERT INTO `score` VALUES ('88', '1582842278', '80', '89', '35', '531', '132', '85', '迷糊');
INSERT INTO `score` VALUES ('89', '1582842278', '80', '31', '35', '531', '132', '86', '无极');
INSERT INTO `score` VALUES ('90', '1582927709', '80', '15', '35', '534', '134', '82', '星辰');
INSERT INTO `score` VALUES ('91', '1582927709', '80', '0', '35', '534', '134', '84', '玩家2');
INSERT INTO `score` VALUES ('92', '1582927709', '80', '0', '35', '533', '134', '87', '王');
INSERT INTO `score` VALUES ('93', '1582927709', '80', '48', '35', '533', '134', '89', '天涯3');
INSERT INTO `score` VALUES ('94', '1583962272', '80', '15', '35', '534', '134', '82', '星辰');
INSERT INTO `score` VALUES ('95', '1583962272', '80', '0', '35', '534', '134', '84', '玩家2');
INSERT INTO `score` VALUES ('96', '1583962272', '80', '0', '35', '533', '134', '87', '王');
INSERT INTO `score` VALUES ('97', '1583962273', '80', '145', '35', '533', '134', '89', '天涯3');

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
) ENGINE=InnoDB AUTO_INCREMENT=91 DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of user
-- ----------------------------
INSERT INTO `user` VALUES ('78', '18066501400', '123456', '王毅正', 'image1', '0');
INSERT INTO `user` VALUES ('79', '13909241463', '123456', '台台王', 'image1', '0');
INSERT INTO `user` VALUES ('80', '15702955391', '111111', 'cs', 'image1', '1');
INSERT INTO `user` VALUES ('81', '17391767972', '123456', 'cs队长', 'image1', '0');
INSERT INTO `user` VALUES ('82', '17792018038', '123456', '星辰', 'image1', '0');
INSERT INTO `user` VALUES ('83', '18009256071', '123456', '玩家1', 'image1', '1');
INSERT INTO `user` VALUES ('84', '15009287112', '123456', '玩家2', 'image1', '0');
INSERT INTO `user` VALUES ('85', '13319234739', '123456789', '迷糊', 'image1', '0');
INSERT INTO `user` VALUES ('86', '13759974762', 'wuji19901005', '无极', 'image1', '0');
INSERT INTO `user` VALUES ('87', '15702955392', '111111', '王', 'image1', '0');
INSERT INTO `user` VALUES ('88', '18392120357', '123456', '汤峪cs', 'image1', '1');
INSERT INTO `user` VALUES ('89', '17391767973', '123456', '天涯3', 'image1', '0');
INSERT INTO `user` VALUES ('90', '17391767974', '123456', '天涯4', 'image1', '0');
