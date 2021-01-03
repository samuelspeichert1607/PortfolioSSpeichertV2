-- phpMyAdmin SQL Dump
-- version 4.6.5.2
-- https://www.phpmyadmin.net/
--
-- Client :  127.0.0.1
-- Généré le :  Mer 29 Mars 2017 à 05:39
-- Version du serveur :  10.1.21-MariaDB
-- Version de PHP :  5.6.30

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Base de données :  `tp2_bd_portair`
--

-- --------------------------------------------------------

--
-- Structure de la table `aeroport`
--

CREATE TABLE `aeroport` (
  `Aeroport_id` varchar(3) NOT NULL,
  `Aeroport_ville` varchar(45) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Contenu de la table `aeroport`
--

INSERT INTO `aeroport` (`Aeroport_id`, `Aeroport_ville`) VALUES
('QBC', 'Baie-Comeau'),
('QGA', 'Gaspé'),
('QHP', 'Havre Saint-Pierre'),
('QMJ', 'Mont-Joli'),
('QRY', 'Rimouski'),
('QSI', 'Sept-Iles');

-- --------------------------------------------------------

--
-- Structure de la table `avion`
--

CREATE TABLE `avion` (
  `Avion_id` varchar(10) NOT NULL,
  `Avion_nbPlace` varchar(45) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Contenu de la table `avion`
--

INSERT INTO `avion` (`Avion_id`, `Avion_nbPlace`) VALUES
('CADM', '32'),
('COPA', '48');

-- --------------------------------------------------------

--
-- Structure de la table `envolee`
--

CREATE TABLE `envolee` (
  `Envolee_id` int(11) NOT NULL,
  `Envolee_date` date NOT NULL,
  `pilote_idPilote` int(11) NOT NULL,
  `avion_Avion_id` varchar(10) NOT NULL,
  `vol_has_segment_vol_Vol_id` int(11) NOT NULL,
  `vol_has_segment_segment_Segment_id` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Contenu de la table `envolee`
--

INSERT INTO `envolee` (`Envolee_id`, `Envolee_date`, `pilote_idPilote`, `avion_Avion_id`, `vol_has_segment_vol_Vol_id`, `vol_has_segment_segment_Segment_id`) VALUES
(1, '2017-05-13', 55, 'COPA', 1922, 1),
(2, '2017-05-03', 55, 'COPA', 1922, 2),
(3, '2017-03-05', 55, 'COPA', 1922, 3),
(4, '2017-03-05', 55, 'COPA', 1922, 4),
(5, '2017-05-13', 55, 'COPA', 1823, 1),
(6, '2017-05-13', 55, 'COPA', 1823, 2);

-- --------------------------------------------------------

--
-- Structure de la table `passager`
--

CREATE TABLE `passager` (
  `Passager_id` int(11) NOT NULL,
  `Passager_nom` varchar(45) DEFAULT NULL,
  `Passager_adresse` varchar(45) DEFAULT NULL,
  `Passager_Tel` varchar(45) DEFAULT NULL,
  `Passager_Courriel` varchar(45) DEFAULT NULL,
  `Passager_nbPoint` varchar(45) DEFAULT NULL,
  `siege_Siege_id` int(11) NOT NULL,
  `siege_avion_Avion_id` varchar(10) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Structure de la table `pilote`
--

CREATE TABLE `pilote` (
  `idPilote` int(11) NOT NULL,
  `Pilote_nom` varchar(45) DEFAULT NULL,
  `Pilote_coordonnée` varchar(45) DEFAULT NULL,
  `Pilote_tel` varchar(45) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Contenu de la table `pilote`
--

INSERT INTO `pilote` (`idPilote`, `Pilote_nom`, `Pilote_coordonnée`, `Pilote_tel`) VALUES
(22, 'Lavigne, Roger', '44 Du Rocher', '418 666 3333'),
(34, 'Lapierre, Pierre-Roch', '5353 Monfort', '540 555 3344'),
(55, 'Lavertue, Claudine', '5353 Monfort', '540 555 3344'),
(61, 'Marcotte, Étienne', '1722 Cresent, App. #3', ' 514 555 1234');

-- --------------------------------------------------------

--
-- Structure de la table `segment`
--

CREATE TABLE `segment` (
  `Segment_id` int(11) NOT NULL,
  `Segment_nom` varchar(45) DEFAULT NULL,
  `Segment_duree` varchar(45) DEFAULT NULL,
  `Segment_HeureDepart` varchar(45) DEFAULT NULL,
  `aeroport_AeroportDepart_id` varchar(3) NOT NULL,
  `aeroport_AeroportArrivee_id` varchar(3) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Contenu de la table `segment`
--

INSERT INTO `segment` (`Segment_id`, `Segment_nom`, `Segment_duree`, `Segment_HeureDepart`, `aeroport_AeroportDepart_id`, `aeroport_AeroportArrivee_id`) VALUES
(1, 'A', '30', '06:00', 'QMJ', 'QBC'),
(2, 'B', '30', '07:00', 'QBC', 'QSI'),
(3, 'C', '30', '08:00', 'QSI', 'QGA'),
(4, 'D', '30', '09:00', 'QGA', 'QHP'),
(5, 'A', '30', '17:00', 'QHP', 'QGA'),
(6, 'B', '30', '18:00', 'QGA', 'QSI'),
(7, 'C', '30', '19:00', 'QSI', 'QBC'),
(8, 'D', '30', '20:00', 'QBC', 'QMJ'),
(9, 'A', '30', '17:30', 'QMJ', 'QBC'),
(10, 'B', '30', '18:30', 'QBC', 'QSI'),
(11, 'C', '30', '19:30', 'QSI', 'QGA'),
(12, 'D', '30', '20:30', 'QGA', 'QHP'),
(13, 'A', '30', '05:30', 'QHP', 'QGA'),
(14, 'B', '30', '06:30', 'QGA', 'QSI'),
(15, 'C', '30', '07:30', 'QSI', 'QBC'),
(16, 'D', '30', '08:30', 'QBC', 'QMJ');

-- --------------------------------------------------------

--
-- Structure de la table `siege`
--

CREATE TABLE `siege` (
  `Siege_id` int(11) NOT NULL,
  `avion_Avion_id` varchar(10) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Structure de la table `vol`
--

CREATE TABLE `vol` (
  `Vol_id` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Contenu de la table `vol`
--

INSERT INTO `vol` (`Vol_id`) VALUES
(1822),
(1823),
(1922),
(1923);

-- --------------------------------------------------------

--
-- Structure de la table `vol_has_segment`
--

CREATE TABLE `vol_has_segment` (
  `vol_Vol_id` int(11) NOT NULL,
  `segment_Segment_id` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Contenu de la table `vol_has_segment`
--

INSERT INTO `vol_has_segment` (`vol_Vol_id`, `segment_Segment_id`) VALUES
(1822, 1),
(1822, 2),
(1822, 3),
(1822, 4),
(1823, 1),
(1823, 2),
(1922, 1),
(1922, 2),
(1922, 3),
(1922, 4),
(1923, 1),
(1923, 2),
(1923, 3),
(1923, 4);

--
-- Index pour les tables exportées
--

--
-- Index pour la table `aeroport`
--
ALTER TABLE `aeroport`
  ADD PRIMARY KEY (`Aeroport_id`);

--
-- Index pour la table `avion`
--
ALTER TABLE `avion`
  ADD PRIMARY KEY (`Avion_id`);

--
-- Index pour la table `envolee`
--
ALTER TABLE `envolee`
  ADD PRIMARY KEY (`Envolee_id`,`pilote_idPilote`,`avion_Avion_id`,`vol_has_segment_vol_Vol_id`,`vol_has_segment_segment_Segment_id`),
  ADD KEY `fk_envolee_pilote1_idx` (`pilote_idPilote`),
  ADD KEY `fk_envolee_avion1_idx` (`avion_Avion_id`),
  ADD KEY `fk_envolee_vol_has_segment1_idx` (`vol_has_segment_vol_Vol_id`,`vol_has_segment_segment_Segment_id`);

--
-- Index pour la table `passager`
--
ALTER TABLE `passager`
  ADD PRIMARY KEY (`Passager_id`,`siege_Siege_id`,`siege_avion_Avion_id`),
  ADD KEY `fk_passager_siege1_idx` (`siege_Siege_id`,`siege_avion_Avion_id`);

--
-- Index pour la table `pilote`
--
ALTER TABLE `pilote`
  ADD PRIMARY KEY (`idPilote`);

--
-- Index pour la table `segment`
--
ALTER TABLE `segment`
  ADD PRIMARY KEY (`Segment_id`),
  ADD KEY `fk_segment_aeroport1_idx` (`aeroport_AeroportDepart_id`),
  ADD KEY `fk_segment_aeroport2_idx` (`aeroport_AeroportArrivee_id`);

--
-- Index pour la table `siege`
--
ALTER TABLE `siege`
  ADD PRIMARY KEY (`Siege_id`,`avion_Avion_id`),
  ADD KEY `fk_siege_avion1_idx` (`avion_Avion_id`);

--
-- Index pour la table `vol`
--
ALTER TABLE `vol`
  ADD PRIMARY KEY (`Vol_id`);

--
-- Index pour la table `vol_has_segment`
--
ALTER TABLE `vol_has_segment`
  ADD PRIMARY KEY (`vol_Vol_id`,`segment_Segment_id`),
  ADD KEY `fk_vol_has_segment_segment1_idx` (`segment_Segment_id`),
  ADD KEY `fk_vol_has_segment_vol1_idx` (`vol_Vol_id`);

--
-- Contraintes pour les tables exportées
--

--
-- Contraintes pour la table `envolee`
--
ALTER TABLE `envolee`
  ADD CONSTRAINT `fk_envolee_avion1` FOREIGN KEY (`avion_Avion_id`) REFERENCES `avion` (`Avion_id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  ADD CONSTRAINT `fk_envolee_pilote1` FOREIGN KEY (`pilote_idPilote`) REFERENCES `pilote` (`idPilote`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  ADD CONSTRAINT `fk_envolee_vol_has_segment1` FOREIGN KEY (`vol_has_segment_vol_Vol_id`,`vol_has_segment_segment_Segment_id`) REFERENCES `vol_has_segment` (`vol_Vol_id`, `segment_Segment_id`) ON DELETE NO ACTION ON UPDATE NO ACTION;

--
-- Contraintes pour la table `passager`
--
ALTER TABLE `passager`
  ADD CONSTRAINT `fk_passager_siege1` FOREIGN KEY (`siege_Siege_id`,`siege_avion_Avion_id`) REFERENCES `siege` (`Siege_id`, `avion_Avion_id`) ON DELETE NO ACTION ON UPDATE NO ACTION;

--
-- Contraintes pour la table `segment`
--
ALTER TABLE `segment`
  ADD CONSTRAINT `fk_segment_aeroport1` FOREIGN KEY (`aeroport_AeroportDepart_id`) REFERENCES `aeroport` (`Aeroport_id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  ADD CONSTRAINT `fk_segment_aeroport2` FOREIGN KEY (`aeroport_AeroportArrivee_id`) REFERENCES `aeroport` (`Aeroport_id`) ON DELETE NO ACTION ON UPDATE NO ACTION;

--
-- Contraintes pour la table `siege`
--
ALTER TABLE `siege`
  ADD CONSTRAINT `fk_siege_avion1` FOREIGN KEY (`avion_Avion_id`) REFERENCES `avion` (`Avion_id`) ON DELETE NO ACTION ON UPDATE NO ACTION;

--
-- Contraintes pour la table `vol_has_segment`
--
ALTER TABLE `vol_has_segment`
  ADD CONSTRAINT `fk_vol_has_segment_segment1` FOREIGN KEY (`segment_Segment_id`) REFERENCES `segment` (`Segment_id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  ADD CONSTRAINT `fk_vol_has_segment_vol1` FOREIGN KEY (`vol_Vol_id`) REFERENCES `vol` (`Vol_id`) ON DELETE NO ACTION ON UPDATE NO ACTION;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
