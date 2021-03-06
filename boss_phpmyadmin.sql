-- phpMyAdmin SQL Dump
-- version 4.7.0
-- https://www.phpmyadmin.net/
--
-- Servidor: 127.0.0.1
-- Tiempo de generación: 14-09-2017 a las 04:41:23
-- Versión del servidor: 10.1.22-MariaDB
-- Versión de PHP: 7.1.4

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET AUTOCOMMIT = 0;
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Base de datos: `boss_mandados`
--

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `manboss_chat`
--

CREATE TABLE `manboss_chat` (
  `id` int(11) NOT NULL,
  `mandado` int(11) NOT NULL,
  `repartidor` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `manboss_chat_mensajes`
--

CREATE TABLE `manboss_chat_mensajes` (
  `id` int(11) NOT NULL,
  `chat` int(11) NOT NULL,
  `mensaje` longtext NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `manboss_clientes`
--

CREATE TABLE `manboss_clientes` (
  `id` int(11) NOT NULL,
  `nombre` varchar(50) NOT NULL,
  `correo` varchar(50) NOT NULL,
  `red_social` varchar(50) NOT NULL,
  `direccion` varchar(50) NOT NULL,
  `latitud` double NOT NULL,
  `longitud` double NOT NULL,
  `telefono` varchar(50) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Volcado de datos para la tabla `manboss_clientes`
--

INSERT INTO `manboss_clientes` (`id`, `nombre`, `correo`, `red_social`, `direccion`, `latitud`, `longitud`, `telefono`) VALUES
(1, 'Isaac Méndez', 'isaac95mendez@gmail.com', 'google-plus', 'Tláloc 504 Prados del Sur', 21.85748, -102.29925, '4491139044');

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `manboss_clientes_direcciones`
--

CREATE TABLE `manboss_clientes_direcciones` (
  `id` int(11) NOT NULL,
  `cliente` int(11) NOT NULL,
  `direccion` longtext NOT NULL,
  `latitud` double NOT NULL,
  `longitud` double NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Volcado de datos para la tabla `manboss_clientes_direcciones`
--

INSERT INTO `manboss_clientes_direcciones` (`id`, `cliente`, `direccion`, `latitud`, `longitud`) VALUES
(1, 1, 'Plaza de La Patria Oriente, Col. Centro, Zona Centro, 20000 Aguascalientes, Ags., México', 21.880637, -102.297114);

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `manboss_comisiones`
--

CREATE TABLE `manboss_comisiones` (
  `id` int(11) NOT NULL,
  `mandado` int(11) NOT NULL,
  `repartidor` int(11) NOT NULL,
  `comision` double NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `manboss_mandados`
--

CREATE TABLE `manboss_mandados` (
  `id` int(11) NOT NULL,
  `estado` int(11) NOT NULL,
  `cliente` int(11) NOT NULL,
  `repartidor` int(11) NOT NULL,
  `total` double NOT NULL,
  `fecha` date NOT NULL,
  `tiempo_trayecto` time NOT NULL,
  `tiempo_total` time NOT NULL,
  `cuenta_pendiente` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Volcado de datos para la tabla `manboss_mandados`
--

INSERT INTO `manboss_mandados` (`id`, `estado`, `cliente`, `repartidor`, `total`, `fecha`, `tiempo_trayecto`, `tiempo_total`, `cuenta_pendiente`) VALUES
(1, 4, 1, 1, 100, '2017-07-25', '10:33:00', '20:33:00', 0),
(2, 4, 1, 1, 100, '2017-06-25', '10:33:00', '20:33:00', 0),
(3, 4, 1, 1, 100, '2017-05-25', '10:33:00', '20:33:00', 0),
(4, 4, 1, 1, 100, '2016-05-23', '10:33:00', '20:33:00', 0),
(5, 4, 1, 1, 100, '2017-04-24', '10:33:00', '20:33:00', 0),
(6, 4, 1, 1, 100, '2017-04-15', '10:33:00', '20:33:00', 0),
(7, 4, 1, 1, 100, '2017-07-15', '10:33:00', '20:33:00', 0),
(8, 4, 1, 1, 100, '2017-08-25', '15:33:00', '20:33:00', 0),
(9, 4, 1, 1, 100, '2017-08-22', '15:33:00', '20:33:00', 0),
(10, 4, 1, 1, 100, '2017-08-05', '15:33:00', '20:33:00', 0);

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `manboss_mandados_estados`
--

CREATE TABLE `manboss_mandados_estados` (
  `id` int(11) NOT NULL,
  `nombre` varchar(50) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Volcado de datos para la tabla `manboss_mandados_estados`
--

INSERT INTO `manboss_mandados_estados` (`id`, `nombre`) VALUES
(1, 'En espera'),
(2, 'Asignado'),
(3, 'En proceso'),
(4, 'Entregado');

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `manboss_mandados_rutas`
--

CREATE TABLE `manboss_mandados_rutas` (
  `id` int(11) NOT NULL,
  `mandado` int(11) NOT NULL,
  `servicio` int(11) NOT NULL,
  `latitud` double NOT NULL,
  `longitud` double NOT NULL,
  `comentarios` longtext NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Volcado de datos para la tabla `manboss_mandados_rutas`
--

INSERT INTO `manboss_mandados_rutas` (`id`, `mandado`, `servicio`, `latitud`, `longitud`, `comentarios`) VALUES
(1, 1, 1, 21.880637, 21.880637, 'Comentarios');

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `manboss_permisos`
--

CREATE TABLE `manboss_permisos` (
  `id` int(11) NOT NULL,
  `usuario` int(11) NOT NULL,
  `menu` int(11) NOT NULL,
  `estado` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Volcado de datos para la tabla `manboss_permisos`
--

INSERT INTO `manboss_permisos` (`id`, `usuario`, `menu`, `estado`) VALUES
(1, 3, 1, 1),
(2, 3, 2, 1),
(3, 3, 3, 1),
(4, 3, 4, 1),
(5, 3, 5, 1),
(6, 3, 6, 1),
(7, 3, 7, 0),
(8, 3, 8, 0),
(9, 3, 9, 0),
(10, 1, 1, 1),
(11, 1, 2, 1),
(12, 1, 3, 1),
(13, 1, 4, 1),
(14, 1, 5, 1),
(15, 1, 6, 1),
(16, 1, 7, 1),
(17, 1, 8, 1),
(18, 1, 9, 1),
(19, 4, 1, 1),
(20, 4, 2, 1),
(21, 4, 3, 1),
(22, 4, 4, 1),
(23, 4, 5, 1),
(24, 4, 6, 1),
(25, 4, 7, 0),
(26, 4, 8, 0),
(27, 4, 9, 0),
(28, 1, 10, 1),
(29, 4, 10, 0);

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `manboss_permisos_menu`
--

CREATE TABLE `manboss_permisos_menu` (
  `id` int(11) NOT NULL,
  `nombre` varchar(50) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Volcado de datos para la tabla `manboss_permisos_menu`
--

INSERT INTO `manboss_permisos_menu` (`id`, `nombre`) VALUES
(1, 'Panel'),
(2, 'Clientes'),
(3, 'Mandados'),
(4, 'Servicios'),
(5, 'Promociones'),
(6, 'Reportes'),
(7, 'Repartidores'),
(8, 'Chat'),
(9, 'Administradores');

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `manboss_promociones`
--

CREATE TABLE `manboss_promociones` (
  `id` int(11) NOT NULL,
  `nombre` varchar(50) NOT NULL,
  `descripcion` varchar(50) NOT NULL,
  `descuento` int(11) DEFAULT NULL,
  `cupon` varchar(50) DEFAULT NULL,
  `fecha_inicio` date NOT NULL,
  `fecha_fin` date NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `manboss_promociones_clientes`
--

CREATE TABLE `manboss_promociones_clientes` (
  `id` int(11) NOT NULL,
  `promocion` int(11) NOT NULL,
  `cliente` int(11) NOT NULL,
  `fecha` date NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `manboss_repartidores`
--

CREATE TABLE `manboss_repartidores` (
  `id` int(11) NOT NULL,
  `repartidor` int(11) NOT NULL,
  `latitud` double NOT NULL,
  `longitud` double NOT NULL,
  `rating` double NOT NULL,
  `efectivo` double NOT NULL,
  `estado` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Volcado de datos para la tabla `manboss_repartidores`
--

INSERT INTO `manboss_repartidores` (`id`, `repartidor`, `latitud`, `longitud`, `rating`, `efectivo`, `estado`) VALUES
(1, 7, 0, 0, 5, 500, 0);

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `manboss_repartidores_calificaciones`
--

CREATE TABLE `manboss_repartidores_calificaciones` (
  `id` int(11) NOT NULL,
  `mandado` int(11) NOT NULL,
  `cliente` int(11) NOT NULL,
  `repartidor` int(11) NOT NULL,
  `calificacion` double NOT NULL,
  `comentarios` varchar(50) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `manboss_rol_usuario`
--

CREATE TABLE `manboss_rol_usuario` (
  `id` int(11) NOT NULL,
  `nombre` varchar(50) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Volcado de datos para la tabla `manboss_rol_usuario`
--

INSERT INTO `manboss_rol_usuario` (`id`, `nombre`) VALUES
(1, 'Administrador'),
(2, 'Repartidor');

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `manboss_servicios`
--

CREATE TABLE `manboss_servicios` (
  `id` int(11) NOT NULL,
  `nombre` varchar(50) NOT NULL,
  `tarifa_base_ex` double NOT NULL,
  `costo_minuto_ex` double NOT NULL,
  `costo_km_ex` double NOT NULL,
  `tarifa_base_co` double NOT NULL,
  `costo_minuto_co` double NOT NULL,
  `costo_km_co` double NOT NULL,
  `foto` longtext NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Volcado de datos para la tabla `manboss_servicios`
--

INSERT INTO `manboss_servicios` (`id`, `nombre`, `tarifa_base_ex`, `costo_minuto_ex`, `costo_km_ex`, `tarifa_base_co`, `costo_minuto_co`, `costo_km_co`, `foto`) VALUES
(1, 'Entrega', 50, 2, 5, 0, 0, 0, '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');

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `manboss_usuarios`
--

CREATE TABLE `manboss_usuarios` (
  `id` int(11) NOT NULL,
  `nombre` varchar(50) NOT NULL,
  `correo` varchar(50) NOT NULL,
  `usuario` varchar(50) NOT NULL,
  `contrasenia` varchar(100) NOT NULL,
  `hash` varchar(100) NOT NULL,
  `rol` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Volcado de datos para la tabla `manboss_usuarios`
--

INSERT INTO `manboss_usuarios` (`id`, `nombre`, `correo`, `usuario`, `contrasenia`, `hash`, `rol`) VALUES
(1, 'Boss Admin', 'admin@bossmandados.com', 'admin', '6f61e0da3789441bee67237749352a72962e681ca2bbc063ca56c970173d761d', 'AR45O9IKGTZ0', 1),
(4, 'Juan Perez', 'juan@gmail.com', 'juan', '068144369e73f924eba837ab1cc694828fa009fe0f5cd4d55f98100af66a0934', '6DRWVE8ZCQE2', 1),
(7, 'Luis López', 'luis@gmail.com', 'luis', '0fa589b696c1c597ddba0df317d523b68c462f27991e44e9b734db49ce4b78c4', 'JX9R553CTGEY', 3);

--
-- Índices para tablas volcadas
--

--
-- Indices de la tabla `manboss_chat`
--
ALTER TABLE `manboss_chat`
  ADD PRIMARY KEY (`id`);

--
-- Indices de la tabla `manboss_chat_mensajes`
--
ALTER TABLE `manboss_chat_mensajes`
  ADD PRIMARY KEY (`id`);

--
-- Indices de la tabla `manboss_clientes`
--
ALTER TABLE `manboss_clientes`
  ADD PRIMARY KEY (`id`);

--
-- Indices de la tabla `manboss_clientes_direcciones`
--
ALTER TABLE `manboss_clientes_direcciones`
  ADD PRIMARY KEY (`id`);

--
-- Indices de la tabla `manboss_comisiones`
--
ALTER TABLE `manboss_comisiones`
  ADD PRIMARY KEY (`id`);

--
-- Indices de la tabla `manboss_mandados`
--
ALTER TABLE `manboss_mandados`
  ADD PRIMARY KEY (`id`);

--
-- Indices de la tabla `manboss_mandados_estados`
--
ALTER TABLE `manboss_mandados_estados`
  ADD PRIMARY KEY (`id`);

--
-- Indices de la tabla `manboss_mandados_rutas`
--
ALTER TABLE `manboss_mandados_rutas`
  ADD PRIMARY KEY (`id`);

--
-- Indices de la tabla `manboss_permisos`
--
ALTER TABLE `manboss_permisos`
  ADD PRIMARY KEY (`id`);

--
-- Indices de la tabla `manboss_permisos_menu`
--
ALTER TABLE `manboss_permisos_menu`
  ADD PRIMARY KEY (`id`);

--
-- Indices de la tabla `manboss_promociones`
--
ALTER TABLE `manboss_promociones`
  ADD PRIMARY KEY (`id`);

--
-- Indices de la tabla `manboss_promociones_clientes`
--
ALTER TABLE `manboss_promociones_clientes`
  ADD PRIMARY KEY (`id`);

--
-- Indices de la tabla `manboss_repartidores`
--
ALTER TABLE `manboss_repartidores`
  ADD PRIMARY KEY (`id`);

--
-- Indices de la tabla `manboss_repartidores_calificaciones`
--
ALTER TABLE `manboss_repartidores_calificaciones`
  ADD PRIMARY KEY (`id`);

--
-- Indices de la tabla `manboss_rol_usuario`
--
ALTER TABLE `manboss_rol_usuario`
  ADD PRIMARY KEY (`id`);

--
-- Indices de la tabla `manboss_servicios`
--
ALTER TABLE `manboss_servicios`
  ADD PRIMARY KEY (`id`);

--
-- Indices de la tabla `manboss_usuarios`
--
ALTER TABLE `manboss_usuarios`
  ADD PRIMARY KEY (`id`);

--
-- AUTO_INCREMENT de las tablas volcadas
--

--
-- AUTO_INCREMENT de la tabla `manboss_chat`
--
ALTER TABLE `manboss_chat`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT;
--
-- AUTO_INCREMENT de la tabla `manboss_chat_mensajes`
--
ALTER TABLE `manboss_chat_mensajes`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT;
--
-- AUTO_INCREMENT de la tabla `manboss_clientes`
--
ALTER TABLE `manboss_clientes`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=2;
--
-- AUTO_INCREMENT de la tabla `manboss_clientes_direcciones`
--
ALTER TABLE `manboss_clientes_direcciones`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=2;
--
-- AUTO_INCREMENT de la tabla `manboss_comisiones`
--
ALTER TABLE `manboss_comisiones`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT;
--
-- AUTO_INCREMENT de la tabla `manboss_mandados`
--
ALTER TABLE `manboss_mandados`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=11;
--
-- AUTO_INCREMENT de la tabla `manboss_mandados_estados`
--
ALTER TABLE `manboss_mandados_estados`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=5;
--
-- AUTO_INCREMENT de la tabla `manboss_mandados_rutas`
--
ALTER TABLE `manboss_mandados_rutas`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=2;
--
-- AUTO_INCREMENT de la tabla `manboss_permisos`
--
ALTER TABLE `manboss_permisos`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=30;
--
-- AUTO_INCREMENT de la tabla `manboss_permisos_menu`
--
ALTER TABLE `manboss_permisos_menu`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=10;
--
-- AUTO_INCREMENT de la tabla `manboss_promociones`
--
ALTER TABLE `manboss_promociones`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT;
--
-- AUTO_INCREMENT de la tabla `manboss_promociones_clientes`
--
ALTER TABLE `manboss_promociones_clientes`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT;
--
-- AUTO_INCREMENT de la tabla `manboss_repartidores`
--
ALTER TABLE `manboss_repartidores`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=2;
--
-- AUTO_INCREMENT de la tabla `manboss_repartidores_calificaciones`
--
ALTER TABLE `manboss_repartidores_calificaciones`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT;
--
-- AUTO_INCREMENT de la tabla `manboss_rol_usuario`
--
ALTER TABLE `manboss_rol_usuario`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=3;
--
-- AUTO_INCREMENT de la tabla `manboss_servicios`
--
ALTER TABLE `manboss_servicios`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=2;
--
-- AUTO_INCREMENT de la tabla `manboss_usuarios`
--
ALTER TABLE `manboss_usuarios`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=8;COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
