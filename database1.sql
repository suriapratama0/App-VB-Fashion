-- phpMyAdmin SQL Dump
-- version 5.2.0
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Generation Time: Jan 09, 2023 at 07:01 AM
-- Server version: 10.4.24-MariaDB
-- PHP Version: 8.1.4

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `database1`
--

-- --------------------------------------------------------

--
-- Table structure for table `tbarang`
--

CREATE TABLE `tbarang` (
  `KODEBARANG` int(5) NOT NULL,
  `NAMABARANG` varchar(15) NOT NULL,
  `PERSEDIAAN` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Dumping data for table `tbarang`
--

INSERT INTO `tbarang` (`KODEBARANG`, `NAMABARANG`, `PERSEDIAAN`) VALUES
(14, 'tas', 10),
(20, 'Sepatu', 0);

-- --------------------------------------------------------

--
-- Table structure for table `tbarangkeluar`
--

CREATE TABLE `tbarangkeluar` (
  `KODEBK` varchar(5) NOT NULL,
  `TANGGAL` date NOT NULL,
  `KODEBARANG` int(5) NOT NULL,
  `JUMLAH` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Dumping data for table `tbarangkeluar`
--

INSERT INTO `tbarangkeluar` (`KODEBK`, `TANGGAL`, `KODEBARANG`, `JUMLAH`) VALUES
('1', '2023-01-03', 14, 1),
('2', '2023-01-03', 14, 10),
('3', '2023-01-03', 14, 10);

-- --------------------------------------------------------

--
-- Table structure for table `tbarangmasuk`
--

CREATE TABLE `tbarangmasuk` (
  `KODEBM` varchar(5) NOT NULL,
  `TANGGAL` date NOT NULL,
  `KODEBARANG` int(5) NOT NULL,
  `JUMLAH` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Dumping data for table `tbarangmasuk`
--

INSERT INTO `tbarangmasuk` (`KODEBM`, `TANGGAL`, `KODEBARANG`, `JUMLAH`) VALUES
('1', '2023-01-03', 14, 2),
('2', '2023-01-03', 14, 20),
('3', '2023-01-03', 14, 10),
('4', '2023-01-03', 14, 10),
('5', '2023-01-03', 14, 10);

--
-- Indexes for dumped tables
--

--
-- Indexes for table `tbarang`
--
ALTER TABLE `tbarang`
  ADD PRIMARY KEY (`KODEBARANG`);

--
-- Indexes for table `tbarangkeluar`
--
ALTER TABLE `tbarangkeluar`
  ADD PRIMARY KEY (`KODEBK`),
  ADD KEY `KODEBARANG` (`KODEBARANG`);

--
-- Indexes for table `tbarangmasuk`
--
ALTER TABLE `tbarangmasuk`
  ADD PRIMARY KEY (`KODEBM`),
  ADD KEY `KODEBARANG` (`KODEBARANG`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `tbarang`
--
ALTER TABLE `tbarang`
  MODIFY `KODEBARANG` int(5) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=21;

--
-- Constraints for dumped tables
--

--
-- Constraints for table `tbarangkeluar`
--
ALTER TABLE `tbarangkeluar`
  ADD CONSTRAINT `tbarangkeluar_ibfk_1` FOREIGN KEY (`KODEBARANG`) REFERENCES `tbarang` (`KODEBARANG`) ON DELETE CASCADE ON UPDATE CASCADE;

--
-- Constraints for table `tbarangmasuk`
--
ALTER TABLE `tbarangmasuk`
  ADD CONSTRAINT `tbarangmasuk_ibfk_1` FOREIGN KEY (`KODEBARANG`) REFERENCES `tbarang` (`KODEBARANG`) ON DELETE CASCADE ON UPDATE CASCADE;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
