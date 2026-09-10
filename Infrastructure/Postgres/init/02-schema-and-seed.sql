--
-- PostgreSQL database dump
--

-- Dumped from database version 17.4 (Debian 17.4-1.pgdg120+2)
-- Dumped by pg_dump version 17.4

SET statement_timeout = 0;
SET lock_timeout = 0;
SET idle_in_transaction_session_timeout = 0;
SET transaction_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SELECT pg_catalog.set_config('search_path', '', false);
SET check_function_bodies = false;
SET xmloption = content;
SET client_min_messages = warning;
SET row_security = off;

ALTER TABLE IF EXISTS ONLY public.wishlists DROP CONSTRAINT IF EXISTS "FK_wishlists_users_user_id";
ALTER TABLE IF EXISTS ONLY public.wished_products DROP CONSTRAINT IF EXISTS "FK_wished_products_wishlists_wishlist_id";
ALTER TABLE IF EXISTS ONLY public.wished_products DROP CONSTRAINT IF EXISTS "FK_wished_products_products_product_id";
ALTER TABLE IF EXISTS ONLY public.user_pickup_points DROP CONSTRAINT IF EXISTS "FK_user_pickup_points_users_user_id";
ALTER TABLE IF EXISTS ONLY public.user_pickup_points DROP CONSTRAINT IF EXISTS "FK_user_pickup_points_pickup_points_pickup_point_id";
ALTER TABLE IF EXISTS ONLY public.reviews DROP CONSTRAINT IF EXISTS "FK_reviews_users_user_id";
ALTER TABLE IF EXISTS ONLY public.reviews DROP CONSTRAINT IF EXISTS "FK_reviews_products_product_id";
ALTER TABLE IF EXISTS ONLY public.refresh_token DROP CONSTRAINT IF EXISTS "FK_refresh_token_users_user_id";
ALTER TABLE IF EXISTS ONLY public.products DROP CONSTRAINT IF EXISTS "FK_products_sellers_maker_id";
ALTER TABLE IF EXISTS ONLY public.products DROP CONSTRAINT IF EXISTS "FK_products_categories_category_id";
ALTER TABLE IF EXISTS ONLY public.product_images DROP CONSTRAINT IF EXISTS "FK_product_images_products_product_id";
ALTER TABLE IF EXISTS ONLY public.orders DROP CONSTRAINT IF EXISTS "FK_orders_users_user_id";
ALTER TABLE IF EXISTS ONLY public.orders DROP CONSTRAINT IF EXISTS "FK_orders_pickup_points_pickup_point_id";
ALTER TABLE IF EXISTS ONLY public.order_positions DROP CONSTRAINT IF EXISTS "FK_order_positions_products_product_id";
ALTER TABLE IF EXISTS ONLY public.order_positions DROP CONSTRAINT IF EXISTS "FK_order_positions_orders_order_id";
ALTER TABLE IF EXISTS ONLY public.carts DROP CONSTRAINT IF EXISTS "FK_carts_users_user_id";
ALTER TABLE IF EXISTS ONLY public.cart_positions DROP CONSTRAINT IF EXISTS "FK_cart_positions_products_product_id";
ALTER TABLE IF EXISTS ONLY public.cart_positions DROP CONSTRAINT IF EXISTS "FK_cart_positions_carts_cart_id";
DROP INDEX IF EXISTS public."IX_wishlists_user_id";
DROP INDEX IF EXISTS public."IX_wished_products_wishlist_id";
DROP INDEX IF EXISTS public."IX_wished_products_product_id";
DROP INDEX IF EXISTS public."IX_users_email";
DROP INDEX IF EXISTS public."IX_user_pickup_points_pickup_point_id";
DROP INDEX IF EXISTS public."IX_sellers_name";
DROP INDEX IF EXISTS public."IX_reviews_user_id_product_id";
DROP INDEX IF EXISTS public."IX_reviews_user_id";
DROP INDEX IF EXISTS public."IX_reviews_product_id";
DROP INDEX IF EXISTS public."IX_refresh_token_user_id";
DROP INDEX IF EXISTS public."IX_refresh_token_token";
DROP INDEX IF EXISTS public."IX_products_maker_id";
DROP INDEX IF EXISTS public."IX_products_category_id";
DROP INDEX IF EXISTS public."IX_product_images_product_id_position";
DROP INDEX IF EXISTS public."IX_product_images_product_id";
DROP INDEX IF EXISTS public."IX_orders_user_id";
DROP INDEX IF EXISTS public."IX_orders_status";
DROP INDEX IF EXISTS public."IX_orders_pickup_point_id";
DROP INDEX IF EXISTS public."IX_order_positions_product_id";
DROP INDEX IF EXISTS public."IX_order_positions_order_id";
DROP INDEX IF EXISTS public."IX_categories_name";
DROP INDEX IF EXISTS public."IX_carts_user_id";
DROP INDEX IF EXISTS public."IX_cart_positions_product_id";
DROP INDEX IF EXISTS public."IX_cart_positions_cart_id";
ALTER TABLE IF EXISTS ONLY public.wishlists DROP CONSTRAINT IF EXISTS "PK_wishlists";
ALTER TABLE IF EXISTS ONLY public.wished_products DROP CONSTRAINT IF EXISTS "PK_wished_products";
ALTER TABLE IF EXISTS ONLY public.users DROP CONSTRAINT IF EXISTS "PK_users";
ALTER TABLE IF EXISTS ONLY public.user_pickup_points DROP CONSTRAINT IF EXISTS "PK_user_pickup_points";
ALTER TABLE IF EXISTS ONLY public.sellers DROP CONSTRAINT IF EXISTS "PK_sellers";
ALTER TABLE IF EXISTS ONLY public.reviews DROP CONSTRAINT IF EXISTS "PK_reviews";
ALTER TABLE IF EXISTS ONLY public.refresh_token DROP CONSTRAINT IF EXISTS "PK_refresh_token";
ALTER TABLE IF EXISTS ONLY public.products DROP CONSTRAINT IF EXISTS "PK_products";
ALTER TABLE IF EXISTS ONLY public.product_images DROP CONSTRAINT IF EXISTS "PK_product_images";
ALTER TABLE IF EXISTS ONLY public.pickup_points DROP CONSTRAINT IF EXISTS "PK_pickup_points";
ALTER TABLE IF EXISTS ONLY public.orders DROP CONSTRAINT IF EXISTS "PK_orders";
ALTER TABLE IF EXISTS ONLY public.order_positions DROP CONSTRAINT IF EXISTS "PK_order_positions";
ALTER TABLE IF EXISTS ONLY public.categories DROP CONSTRAINT IF EXISTS "PK_categories";
ALTER TABLE IF EXISTS ONLY public.carts DROP CONSTRAINT IF EXISTS "PK_carts";
ALTER TABLE IF EXISTS ONLY public.cart_positions DROP CONSTRAINT IF EXISTS "PK_cart_positions";
ALTER TABLE IF EXISTS ONLY public."__EFMigrationsHistory" DROP CONSTRAINT IF EXISTS "PK___EFMigrationsHistory";
DROP TABLE IF EXISTS public.wishlists;
DROP TABLE IF EXISTS public.wished_products;
DROP TABLE IF EXISTS public.users;
DROP TABLE IF EXISTS public.user_pickup_points;
DROP TABLE IF EXISTS public.sellers;
DROP TABLE IF EXISTS public.reviews;
DROP TABLE IF EXISTS public.refresh_token;
DROP TABLE IF EXISTS public.products;
DROP TABLE IF EXISTS public.product_images;
DROP TABLE IF EXISTS public.pickup_points;
DROP TABLE IF EXISTS public.orders;
DROP TABLE IF EXISTS public.order_positions;
DROP TABLE IF EXISTS public.categories;
DROP TABLE IF EXISTS public.carts;
DROP TABLE IF EXISTS public.cart_positions;
DROP TABLE IF EXISTS public."__EFMigrationsHistory";
--
-- Name: SCHEMA public; Type: COMMENT; Schema: -; Owner: -
--

COMMENT ON SCHEMA public IS '';


SET default_tablespace = '';

SET default_table_access_method = heap;

--
-- Name: __EFMigrationsHistory; Type: TABLE; Schema: public; Owner: -
--

CREATE TABLE public."__EFMigrationsHistory" (
    "MigrationId" character varying(150) NOT NULL,
    "ProductVersion" character varying(32) NOT NULL
);


--
-- Name: cart_positions; Type: TABLE; Schema: public; Owner: -
--

CREATE TABLE public.cart_positions (
    cart_id integer NOT NULL,
    product_id integer NOT NULL,
    quantity integer NOT NULL,
    added_at timestamp with time zone NOT NULL
);


--
-- Name: carts; Type: TABLE; Schema: public; Owner: -
--

CREATE TABLE public.carts (
    id integer NOT NULL,
    user_id integer NOT NULL
);


--
-- Name: carts_id_seq; Type: SEQUENCE; Schema: public; Owner: -
--

ALTER TABLE public.carts ALTER COLUMN id ADD GENERATED BY DEFAULT AS IDENTITY (
    SEQUENCE NAME public.carts_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);


--
-- Name: categories; Type: TABLE; Schema: public; Owner: -
--

CREATE TABLE public.categories (
    id integer NOT NULL,
    name character varying(40) NOT NULL
);


--
-- Name: categories_id_seq; Type: SEQUENCE; Schema: public; Owner: -
--

ALTER TABLE public.categories ALTER COLUMN id ADD GENERATED BY DEFAULT AS IDENTITY (
    SEQUENCE NAME public.categories_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);


--
-- Name: order_positions; Type: TABLE; Schema: public; Owner: -
--

CREATE TABLE public.order_positions (
    order_id integer NOT NULL,
    product_id integer NOT NULL,
    quantity integer NOT NULL,
    discount_percent smallint DEFAULT 0 NOT NULL,
    price numeric(10,2) NOT NULL,
    delivery_date timestamp with time zone DEFAULT '-infinity'::timestamp with time zone NOT NULL
);


--
-- Name: orders; Type: TABLE; Schema: public; Owner: -
--

CREATE TABLE public.orders (
    id integer NOT NULL,
    status character varying(20) NOT NULL,
    created_at timestamp with time zone NOT NULL,
    deletion_time timestamp with time zone NOT NULL,
    paid_at timestamp with time zone,
    user_id integer NOT NULL,
    pickup_point_id integer DEFAULT 0 NOT NULL
);


--
-- Name: orders_id_seq; Type: SEQUENCE; Schema: public; Owner: -
--

ALTER TABLE public.orders ALTER COLUMN id ADD GENERATED BY DEFAULT AS IDENTITY (
    SEQUENCE NAME public.orders_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);


--
-- Name: pickup_points; Type: TABLE; Schema: public; Owner: -
--

CREATE TABLE public.pickup_points (
    id integer NOT NULL,
    city character varying(100) NOT NULL,
    street_type character varying(30) NOT NULL,
    street_name character varying(150) NOT NULL,
    house character varying(30) NOT NULL,
    building character varying(30),
    shelf_lifetime integer NOT NULL,
    added_at timestamp with time zone NOT NULL
);


--
-- Name: pickup_points_id_seq; Type: SEQUENCE; Schema: public; Owner: -
--

ALTER TABLE public.pickup_points ALTER COLUMN id ADD GENERATED BY DEFAULT AS IDENTITY (
    SEQUENCE NAME public.pickup_points_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);


--
-- Name: product_images; Type: TABLE; Schema: public; Owner: -
--

CREATE TABLE public.product_images (
    id integer NOT NULL,
    "position" integer NOT NULL,
    path text NOT NULL,
    product_id integer NOT NULL
);


--
-- Name: product_images_id_seq; Type: SEQUENCE; Schema: public; Owner: -
--

ALTER TABLE public.product_images ALTER COLUMN id ADD GENERATED BY DEFAULT AS IDENTITY (
    SEQUENCE NAME public.product_images_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);


--
-- Name: products; Type: TABLE; Schema: public; Owner: -
--

CREATE TABLE public.products (
    id integer NOT NULL,
    name character varying(100) NOT NULL,
    description text,
    price numeric(10,2) NOT NULL,
    discount_percent smallint DEFAULT 0 NOT NULL,
    quantity integer DEFAULT 0 NOT NULL,
    maker_id integer NOT NULL,
    category_id integer NOT NULL
);


--
-- Name: products_id_seq; Type: SEQUENCE; Schema: public; Owner: -
--

ALTER TABLE public.products ALTER COLUMN id ADD GENERATED BY DEFAULT AS IDENTITY (
    SEQUENCE NAME public.products_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);


--
-- Name: refresh_token; Type: TABLE; Schema: public; Owner: -
--

CREATE TABLE public.refresh_token (
    id integer NOT NULL,
    token text NOT NULL,
    expires_at timestamp with time zone NOT NULL,
    created_at timestamp with time zone NOT NULL,
    is_revoked boolean DEFAULT false NOT NULL,
    user_id integer NOT NULL
);


--
-- Name: refresh_token_id_seq; Type: SEQUENCE; Schema: public; Owner: -
--

ALTER TABLE public.refresh_token ALTER COLUMN id ADD GENERATED BY DEFAULT AS IDENTITY (
    SEQUENCE NAME public.refresh_token_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);


--
-- Name: reviews; Type: TABLE; Schema: public; Owner: -
--

CREATE TABLE public.reviews (
    id integer NOT NULL,
    score integer NOT NULL,
    text text,
    created_at timestamp with time zone NOT NULL,
    updated_at timestamp with time zone,
    user_id integer NOT NULL,
    product_id integer NOT NULL
);


--
-- Name: reviews_id_seq; Type: SEQUENCE; Schema: public; Owner: -
--

ALTER TABLE public.reviews ALTER COLUMN id ADD GENERATED BY DEFAULT AS IDENTITY (
    SEQUENCE NAME public.reviews_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);


--
-- Name: sellers; Type: TABLE; Schema: public; Owner: -
--

CREATE TABLE public.sellers (
    id integer NOT NULL,
    name character varying(100) NOT NULL,
    description text
);


--
-- Name: sellers_id_seq; Type: SEQUENCE; Schema: public; Owner: -
--

ALTER TABLE public.sellers ALTER COLUMN id ADD GENERATED BY DEFAULT AS IDENTITY (
    SEQUENCE NAME public.sellers_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);


--
-- Name: user_pickup_points; Type: TABLE; Schema: public; Owner: -
--

CREATE TABLE public.user_pickup_points (
    user_id integer NOT NULL,
    pickup_point_id integer NOT NULL,
    selected_at timestamp with time zone NOT NULL,
    added_at timestamp with time zone NOT NULL
);


--
-- Name: users; Type: TABLE; Schema: public; Owner: -
--

CREATE TABLE public.users (
    id integer NOT NULL,
    login character varying(100) NOT NULL,
    email character varying(256) NOT NULL,
    created_at timestamp with time zone DEFAULT '-infinity'::timestamp with time zone NOT NULL,
    last_login_at timestamp with time zone,
    phone_number character varying(20) DEFAULT ''::character varying NOT NULL
);


--
-- Name: users_id_seq; Type: SEQUENCE; Schema: public; Owner: -
--

ALTER TABLE public.users ALTER COLUMN id ADD GENERATED BY DEFAULT AS IDENTITY (
    SEQUENCE NAME public.users_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);


--
-- Name: wished_products; Type: TABLE; Schema: public; Owner: -
--

CREATE TABLE public.wished_products (
    wishlist_id integer NOT NULL,
    product_id integer NOT NULL,
    added_at timestamp with time zone NOT NULL
);


--
-- Name: wishlists; Type: TABLE; Schema: public; Owner: -
--

CREATE TABLE public.wishlists (
    id integer NOT NULL,
    user_id integer NOT NULL
);


--
-- Name: wishlists_id_seq; Type: SEQUENCE; Schema: public; Owner: -
--

ALTER TABLE public.wishlists ALTER COLUMN id ADD GENERATED BY DEFAULT AS IDENTITY (
    SEQUENCE NAME public.wishlists_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);


--
-- Data for Name: __EFMigrationsHistory; Type: TABLE DATA; Schema: public; Owner: -
--

INSERT INTO public."__EFMigrationsHistory" ("MigrationId", "ProductVersion") VALUES ('20260906150512_Initial', '9.0.1');


--
-- Data for Name: cart_positions; Type: TABLE DATA; Schema: public; Owner: -
--

INSERT INTO public.cart_positions (cart_id, product_id, quantity, added_at) VALUES (1, 22, 1, '2026-09-04 16:33:26.628981+00');
INSERT INTO public.cart_positions (cart_id, product_id, quantity, added_at) VALUES (1, 20, 2, '2026-09-04 16:33:23.569716+00');
INSERT INTO public.cart_positions (cart_id, product_id, quantity, added_at) VALUES (1, 26, 3, '2026-09-04 16:33:22.782355+00');
INSERT INTO public.cart_positions (cart_id, product_id, quantity, added_at) VALUES (1, 28, 1, '2026-09-04 16:33:23.137398+00');


--
-- Data for Name: carts; Type: TABLE DATA; Schema: public; Owner: -
--

INSERT INTO public.carts (id, user_id) VALUES (1, 1);
INSERT INTO public.carts (id, user_id) VALUES (2, 2);
INSERT INTO public.carts (id, user_id) VALUES (3, 3);
INSERT INTO public.carts (id, user_id) VALUES (4, 4);
INSERT INTO public.carts (id, user_id) VALUES (5, 5);
INSERT INTO public.carts (id, user_id) VALUES (6, 6);
INSERT INTO public.carts (id, user_id) VALUES (7, 7);
INSERT INTO public.carts (id, user_id) VALUES (8, 8);
INSERT INTO public.carts (id, user_id) VALUES (9, 9);
INSERT INTO public.carts (id, user_id) VALUES (10, 10);
INSERT INTO public.carts (id, user_id) VALUES (11, 11);
INSERT INTO public.carts (id, user_id) VALUES (12, 12);
INSERT INTO public.carts (id, user_id) VALUES (13, 13);
INSERT INTO public.carts (id, user_id) VALUES (14, 14);
INSERT INTO public.carts (id, user_id) VALUES (15, 15);
INSERT INTO public.carts (id, user_id) VALUES (16, 16);
INSERT INTO public.carts (id, user_id) VALUES (17, 17);
INSERT INTO public.carts (id, user_id) VALUES (18, 18);
INSERT INTO public.carts (id, user_id) VALUES (19, 19);
INSERT INTO public.carts (id, user_id) VALUES (20, 20);


--
-- Data for Name: categories; Type: TABLE DATA; Schema: public; Owner: -
--

INSERT INTO public.categories (id, name) VALUES (1, 'Электроника');
INSERT INTO public.categories (id, name) VALUES (2, 'Одежда');
INSERT INTO public.categories (id, name) VALUES (3, 'Обувь');
INSERT INTO public.categories (id, name) VALUES (4, 'Дом и сад');
INSERT INTO public.categories (id, name) VALUES (5, 'Детские товары');
INSERT INTO public.categories (id, name) VALUES (6, 'Красота и здоровье');
INSERT INTO public.categories (id, name) VALUES (7, 'Бытовая техника');
INSERT INTO public.categories (id, name) VALUES (8, 'Спорт и отдых');
INSERT INTO public.categories (id, name) VALUES (9, 'Строительство и ремонт');
INSERT INTO public.categories (id, name) VALUES (10, 'Продукты питания');
INSERT INTO public.categories (id, name) VALUES (11, 'Аптека');
INSERT INTO public.categories (id, name) VALUES (12, 'Товары для животных');
INSERT INTO public.categories (id, name) VALUES (13, 'Книги');
INSERT INTO public.categories (id, name) VALUES (14, 'Туризм, рыбалка, охота');
INSERT INTO public.categories (id, name) VALUES (15, 'Автотовары');
INSERT INTO public.categories (id, name) VALUES (16, 'Мебель');
INSERT INTO public.categories (id, name) VALUES (17, 'Хобби и творчество');
INSERT INTO public.categories (id, name) VALUES (18, 'Ювелирные украшения');
INSERT INTO public.categories (id, name) VALUES (19, 'Аксессуары');
INSERT INTO public.categories (id, name) VALUES (20, 'Игры и консоли');
INSERT INTO public.categories (id, name) VALUES (21, 'Канцелярские товары');
INSERT INTO public.categories (id, name) VALUES (22, 'Товары для взрослых');
INSERT INTO public.categories (id, name) VALUES (23, 'Антиквариат и коллекционирование');
INSERT INTO public.categories (id, name) VALUES (24, 'Цифровые товары');
INSERT INTO public.categories (id, name) VALUES (25, 'Подарочные сертификаты OZON');
INSERT INTO public.categories (id, name) VALUES (26, 'Бытовая химия и гигиена');
INSERT INTO public.categories (id, name) VALUES (27, 'Музыка и видео');
INSERT INTO public.categories (id, name) VALUES (28, 'Автомобили');
INSERT INTO public.categories (id, name) VALUES (29, 'Товары для курения и аксессуары');


--
-- Data for Name: order_positions; Type: TABLE DATA; Schema: public; Owner: -
--

INSERT INTO public.order_positions (order_id, product_id, quantity, discount_percent, price, delivery_date) VALUES (35, 3, 3, 15, 27990.00, '2026-09-08 13:02:46.965097+00');
INSERT INTO public.order_positions (order_id, product_id, quantity, discount_percent, price, delivery_date) VALUES (35, 6, 1, 25, 1490.00, '2026-09-06 13:02:46.965093+00');
INSERT INTO public.order_positions (order_id, product_id, quantity, discount_percent, price, delivery_date) VALUES (35, 20, 3, 0, 650.00, '2026-09-06 13:02:46.959717+00');
INSERT INTO public.order_positions (order_id, product_id, quantity, discount_percent, price, delivery_date) VALUES (35, 26, 1, 15, 380.00, '2026-09-05 13:02:46.965096+00');
INSERT INTO public.order_positions (order_id, product_id, quantity, discount_percent, price, delivery_date) VALUES (35, 28, 2, 10, 450.00, '2026-09-14 13:02:46.965079+00');
INSERT INTO public.order_positions (order_id, product_id, quantity, discount_percent, price, delivery_date) VALUES (50, 20, 1, 0, 650.00, '2026-09-07 15:56:29.9898+00');
INSERT INTO public.order_positions (order_id, product_id, quantity, discount_percent, price, delivery_date) VALUES (50, 26, 3, 15, 380.00, '2026-09-07 15:56:30.001883+00');
INSERT INTO public.order_positions (order_id, product_id, quantity, discount_percent, price, delivery_date) VALUES (50, 28, 3, 10, 450.00, '2026-09-16 15:56:30.001901+00');
INSERT INTO public.order_positions (order_id, product_id, quantity, discount_percent, price, delivery_date) VALUES (51, 20, 3, 0, 650.00, '2026-09-14 16:27:59.120712+00');
INSERT INTO public.order_positions (order_id, product_id, quantity, discount_percent, price, delivery_date) VALUES (51, 26, 1, 15, 380.00, '2026-09-18 16:27:59.120682+00');
INSERT INTO public.order_positions (order_id, product_id, quantity, discount_percent, price, delivery_date) VALUES (51, 28, 2, 10, 450.00, '2026-09-18 16:27:59.12071+00');
INSERT INTO public.order_positions (order_id, product_id, quantity, discount_percent, price, delivery_date) VALUES (52, 26, 1, 15, 380.00, '2026-09-09 16:32:05.135176+00');
INSERT INTO public.order_positions (order_id, product_id, quantity, discount_percent, price, delivery_date) VALUES (52, 27, 2, 0, 1150.00, '2026-09-12 16:32:05.135146+00');
INSERT INTO public.order_positions (order_id, product_id, quantity, discount_percent, price, delivery_date) VALUES (52, 28, 1, 10, 450.00, '2026-09-08 16:32:05.135173+00');


--
-- Data for Name: orders; Type: TABLE DATA; Schema: public; Owner: -
--

INSERT INTO public.orders (id, status, created_at, deletion_time, paid_at, user_id, pickup_point_id) VALUES (52, 'Paid', '2026-09-04 16:32:05.136895+00', '2026-09-04 16:37:05.136895+00', '2026-09-04 16:32:06.802274+00', 1, 1);
INSERT INTO public.orders (id, status, created_at, deletion_time, paid_at, user_id, pickup_point_id) VALUES (35, 'Paid', '2026-09-01 13:02:46.994449+00', '2026-09-01 13:07:46.994449+00', '2026-09-01 13:04:29.42065+00', 1, 4);
INSERT INTO public.orders (id, status, created_at, deletion_time, paid_at, user_id, pickup_point_id) VALUES (50, 'Paid', '2026-09-04 15:56:30.040559+00', '2026-09-04 16:01:30.040559+00', '2026-09-04 15:57:43.688108+00', 1, 1);
INSERT INTO public.orders (id, status, created_at, deletion_time, paid_at, user_id, pickup_point_id) VALUES (51, 'Paid', '2026-09-04 16:27:59.123871+00', '2026-09-04 16:32:59.123871+00', '2026-09-04 16:28:02.160052+00', 1, 1);


--
-- Data for Name: pickup_points; Type: TABLE DATA; Schema: public; Owner: -
--

INSERT INTO public.pickup_points (id, city, street_type, street_name, house, building, shelf_lifetime, added_at) VALUES (1, 'Москва', 'Street', 'Тверская', '12', 'стр 1', 7, '2025-01-10 07:00:00+00');
INSERT INTO public.pickup_points (id, city, street_type, street_name, house, building, shelf_lifetime, added_at) VALUES (2, 'Москва', 'Avenue', 'Ленинский', '45', NULL, 5, '2025-02-15 08:20:00+00');
INSERT INTO public.pickup_points (id, city, street_type, street_name, house, building, shelf_lifetime, added_at) VALUES (3, 'Москва', 'Boulevard', 'Цветной', '21', '1', 7, '2025-03-01 06:15:00+00');
INSERT INTO public.pickup_points (id, city, street_type, street_name, house, building, shelf_lifetime, added_at) VALUES (4, 'Москва', 'Lane', 'Сивцев Вражек', '8', NULL, 3, '2025-03-20 11:00:00+00');
INSERT INTO public.pickup_points (id, city, street_type, street_name, house, building, shelf_lifetime, added_at) VALUES (5, 'Москва', 'Highway', 'Варшавское', '142', 'к 2', 10, '2025-04-05 13:45:00+00');
INSERT INTO public.pickup_points (id, city, street_type, street_name, house, building, shelf_lifetime, added_at) VALUES (6, 'Москва', 'Square', 'Комсомольская', '3', NULL, 5, '2025-04-12 05:30:00+00');
INSERT INTO public.pickup_points (id, city, street_type, street_name, house, building, shelf_lifetime, added_at) VALUES (7, 'Москва', 'Passage', 'Багратионовский', '7', 'к 3', 7, '2025-05-02 09:00:00+00');
INSERT INTO public.pickup_points (id, city, street_type, street_name, house, building, shelf_lifetime, added_at) VALUES (8, 'Санкт-Петербург', 'Avenue', 'Невский', '28', NULL, 7, '2025-01-12 07:00:00+00');
INSERT INTO public.pickup_points (id, city, street_type, street_name, house, building, shelf_lifetime, added_at) VALUES (9, 'Санкт-Петербург', 'Street', 'Рубинштейна', '15', 'А', 5, '2025-02-20 14:10:00+00');
INSERT INTO public.pickup_points (id, city, street_type, street_name, house, building, shelf_lifetime, added_at) VALUES (10, 'Санкт-Петербург', 'Embankment', 'Реки Фонтанки', '44', NULL, 7, '2025-03-11 08:00:00+00');
INSERT INTO public.pickup_points (id, city, street_type, street_name, house, building, shelf_lifetime, added_at) VALUES (11, 'Санкт-Петербург', 'Lane', 'Гривцова', '4', 'Б', 3, '2025-03-25 12:30:00+00');
INSERT INTO public.pickup_points (id, city, street_type, street_name, house, building, shelf_lifetime, added_at) VALUES (12, 'Санкт-Петербург', 'Alley', 'Чернореченский', '6', NULL, 5, '2025-04-01 07:00:00+00');
INSERT INTO public.pickup_points (id, city, street_type, street_name, house, building, shelf_lifetime, added_at) VALUES (13, 'Санкт-Петербург', 'Boulevard', 'Новаторов', '11', 'к 2', 7, '2025-04-18 10:15:00+00');
INSERT INTO public.pickup_points (id, city, street_type, street_name, house, building, shelf_lifetime, added_at) VALUES (14, 'Санкт-Петербург', 'Square', 'Восстания', '2', NULL, 5, '2025-04-29 06:00:00+00');
INSERT INTO public.pickup_points (id, city, street_type, street_name, house, building, shelf_lifetime, added_at) VALUES (15, 'Новосибирск', 'Street', 'Ленина', '12', NULL, 7, '2025-01-15 09:00:00+00');
INSERT INTO public.pickup_points (id, city, street_type, street_name, house, building, shelf_lifetime, added_at) VALUES (16, 'Новосибирск', 'Avenue', 'Красный', '33', NULL, 5, '2025-02-18 11:20:00+00');
INSERT INTO public.pickup_points (id, city, street_type, street_name, house, building, shelf_lifetime, added_at) VALUES (17, 'Новосибирск', 'Passage', 'Вокзальный', '15', '1', 7, '2025-03-05 07:40:00+00');
INSERT INTO public.pickup_points (id, city, street_type, street_name, house, building, shelf_lifetime, added_at) VALUES (18, 'Новосибирск', 'Highway', 'Бердское', '270', 'к 1', 14, '2025-03-22 13:00:00+00');
INSERT INTO public.pickup_points (id, city, street_type, street_name, house, building, shelf_lifetime, added_at) VALUES (19, 'Новосибирск', 'Square', 'Карла Маркса', '5', NULL, 5, '2025-04-10 08:15:00+00');


--
-- Data for Name: product_images; Type: TABLE DATA; Schema: public; Owner: -
--



--
-- Data for Name: products; Type: TABLE DATA; Schema: public; Owner: -
--

INSERT INTO public.products (id, name, description, price, discount_percent, quantity, maker_id, category_id) VALUES (1, 'Смартфон Apple iPhone 15 Pro 128GB (Черный титан)', 'Флагманский смартфон с процессором A17 Pro, титановым корпусом и улучшенной системой трех камер.', 119990.00, 5, 14, 1, 1);
INSERT INTO public.products (id, name, description, price, discount_percent, quantity, maker_id, category_id) VALUES (2, 'Смартфон Samsung Galaxy S24 Ultra 12/512GB (Серый)', 'Премиальный Android-смартфон с поддержкой искусственного интеллекта Galaxy AI и встроенным пером S Pen.', 134990.00, 10, 8, 1, 1);
INSERT INTO public.products (id, name, description, price, discount_percent, quantity, maker_id, category_id) VALUES (4, 'Беспроводные наушники Apple AirPods Pro (2-го поколения, USB-C)', 'Наушники с активным шумоподавлением, адаптивным аудио и зарядным кейсом MagSafe.', 24990.00, 0, 30, 1, 1);
INSERT INTO public.products (id, name, description, price, discount_percent, quantity, maker_id, category_id) VALUES (5, 'Ноутбук ASUS ROG Strix G16 G614JV-N3106 (Серый)', 'Мощный игровой ноутбук на базе процессора Intel Core i7 и видеокарты NVIDIA GeForce RTX 4060.', 145000.00, 7, 12, 1, 1);
INSERT INTO public.products (id, name, description, price, discount_percent, quantity, maker_id, category_id) VALUES (7, 'Худи ТВОЕ с начесом (Светло-серый меланж)', 'Утепленная толстовка с капюшоном и карманом-кенгуру. Состав: 80% хлопок, 20% полиэстер.', 2190.00, 30, 85, 2, 2);
INSERT INTO public.products (id, name, description, price, discount_percent, quantity, maker_id, category_id) VALUES (8, 'Джинсы Levi''s 501 Original Fit (Синие)', 'Классические прямые джинсы из плотного жесткого денима. Средняя посадка, застежка на болтах.', 9990.00, 10, 18, 2, 2);
INSERT INTO public.products (id, name, description, price, discount_percent, quantity, maker_id, category_id) VALUES (9, 'Куртка-бомбер мужская стёганая Zarina (Хаки)', 'Легкая демисезонная куртка на молнии с воротником-стойкой и трикотажными манжетами.', 4590.00, 15, 40, 2, 2);
INSERT INTO public.products (id, name, description, price, discount_percent, quantity, maker_id, category_id) VALUES (10, 'Платье женское трикотажное миди Concept Club', 'Элегантное повседневное платье приталенного силуэта с длинным рукавом и разрезом на бедре.', 2990.00, 20, 60, 2, 2);
INSERT INTO public.products (id, name, description, price, discount_percent, quantity, maker_id, category_id) VALUES (11, 'Кроссовки Nike Air Force 1 ''07 (Белые)', 'Легендарные низкие баскетбольные кроссовки из натуральной кожи. Классика уличной моды.', 13990.00, 0, 12, 3, 3);
INSERT INTO public.products (id, name, description, price, discount_percent, quantity, maker_id, category_id) VALUES (12, 'Кеды Converse Chuck Taylor All Star (Черные, высокие)', 'Классические тряпичные кеды на прочной резиновой подошве с узнаваемым логотипом на щиколотке.', 8490.00, 5, 25, 3, 3);
INSERT INTO public.products (id, name, description, price, discount_percent, quantity, maker_id, category_id) VALUES (13, 'Ботинки мужские демисезонные Rieker (Кожаные)', 'Комфортная классическая обувь со специальной анатомической подошвой Antistress.', 7800.00, 15, 30, 3, 3);
INSERT INTO public.products (id, name, description, price, discount_percent, quantity, maker_id, category_id) VALUES (14, 'Туфли женские лодочки EKONIKA (Бежевые)', 'Элегантные туфли из натуральной замши на шпильке высотой 8 см. Идеально для офиса и вечерних выходов.', 12990.00, 10, 15, 3, 3);
INSERT INTO public.products (id, name, description, price, discount_percent, quantity, maker_id, category_id) VALUES (16, 'Комплект постельного белья Euro Сатин (Изумрудный)', 'Высококачественный 100% хлопковый сатин с благородным блеском. Плотность ткани 125 г/м².', 4890.00, 15, 35, 4, 4);
INSERT INTO public.products (id, name, description, price, discount_percent, quantity, maker_id, category_id) VALUES (17, 'Плед велсофт пушистый 200х220 см (Бежевый)', 'Мягкий, приятный к телу плед из микрофибры. Быстро сохнет, не линяет и не скатывается.', 1850.00, 10, 70, 4, 4);
INSERT INTO public.products (id, name, description, price, discount_percent, quantity, maker_id, category_id) VALUES (18, 'Набор столовой посуды Luminarc Carine (18 предметов)', 'Сервиз из ударопрочного стекла на 6 персон (тарелки обеденные, глубокие и десертные).', 3990.00, 5, 22, 4, 4);
INSERT INTO public.products (id, name, description, price, discount_percent, quantity, maker_id, category_id) VALUES (21, 'Конструктор LEGO City 60316 Полицейский участок', 'Большой игровой набор, включающий трехэтажный участок, мусоровоз, вертолет и 5 минифигурок.', 6490.00, 8, 15, 5, 5);
INSERT INTO public.products (id, name, description, price, discount_percent, quantity, maker_id, category_id) VALUES (23, 'Настольная игра Имаджинариум Детский', 'Популярная игра на ассоциации с адаптированными добрыми иллюстрациями для детей от 6 лет.', 1750.00, 0, 30, 5, 5);
INSERT INTO public.products (id, name, description, price, discount_percent, quantity, maker_id, category_id) VALUES (25, 'Колияска прогулочная Carrello Bravo Plus', 'Всесезонная коляска с надежной рамой, большим капюшоном и хорошей амортизацией колес.', 18990.00, 5, 10, 5, 5);
INSERT INTO public.products (id, name, description, price, discount_percent, quantity, maker_id, category_id) VALUES (3, 'Смартфон Xiaomi Redmi Note 13 Pro 8/256GB (Черный)', 'Среднебюджетный смартфон с камерой 200 Мп, AMOLED-экраном 120 Гц и поддержкой быстрой зарядки 67 Вт.', 27990.00, 15, 42, 1, 1);
INSERT INTO public.products (id, name, description, price, discount_percent, quantity, maker_id, category_id) VALUES (15, 'Сандалии спортивные детские Crosby', 'Легкие летние сандалии на регулируемых липучках с рельефной нескользящей стелькой.', 2490.00, 20, 50, 3, 3);
INSERT INTO public.products (id, name, description, price, discount_percent, quantity, maker_id, category_id) VALUES (27, 'Увлажняющий крем для лица CeraVe для сухой кожи 50 мл', 'Крем с церамидами и гиалуроновой кислотой для восстановления защитного барьера кожи.', 1150.00, 0, 63, 6, 6);
INSERT INTO public.products (id, name, description, price, discount_percent, quantity, maker_id, category_id) VALUES (19, 'Шторы интерьерные блэкаут на ленте (Серо-голубые, 2 шт)', 'Плотные портьеры (размер 150х270 см), блокирующие до 90% солнечного света.', 2750.00, 25, 40, 4, 4);
INSERT INTO public.products (id, name, description, price, discount_percent, quantity, maker_id, category_id) VALUES (22, 'Развивающий сортер Полесье "Садовый домик"', 'Классическая развивающая игрушка с фигурками животных и геометрическими формами для малышей.', 1150.00, 12, 45, 5, 5);
INSERT INTO public.products (id, name, description, price, discount_percent, quantity, maker_id, category_id) VALUES (29, 'Парфюмерная вода Dolce & Gabbana Imperatrice 50 мл', 'Знаменитый цветочно-фруктовый аромат с выразительными нотами арбуза, киви и мускуса.', 6800.00, 20, 18, 6, 6);
INSERT INTO public.products (id, name, description, price, discount_percent, quantity, maker_id, category_id) VALUES (31, 'Робот-пылесос Xiaomi Robot Vacuum S10', 'Робот-пылесос с лазерной навигацией LDS, мощностью всасывания 4000 Па и влажной уборкой.', 18990.00, 10, 16, 7, 7);
INSERT INTO public.products (id, name, description, price, discount_percent, quantity, maker_id, category_id) VALUES (32, 'Автоматическая кофемашина DeLonghi Magnifica Start', 'Приготовление эспрессо, кофе и капучино одной кнопкой. Встроенная жерновая кофемолка.', 42990.00, 5, 5, 7, 7);
INSERT INTO public.products (id, name, description, price, discount_percent, quantity, maker_id, category_id) VALUES (33, 'Электрический чайник Bosch TWK3P420 (Стальной)', 'Надежный чайник из нержавеющей стали с закрытой спиралью и защитой от перегрева.', 4490.00, 0, 25, 7, 7);
INSERT INTO public.products (id, name, description, price, discount_percent, quantity, maker_id, category_id) VALUES (34, 'Микроволновая печь Solo Samsung MS23F302TAS', 'Объем 23 литра, биокерамическое внутреннее покрытие, электронное управление.', 9990.00, 12, 14, 7, 7);
INSERT INTO public.products (id, name, description, price, discount_percent, quantity, maker_id, category_id) VALUES (37, 'Набор регулируемых гантелей неопреновых (2 шт по 2.5 кг)', 'Гантели с мягким нескользящим покрытием, которое защищает пол от повреждений при падении.', 2100.00, 10, 40, 8, 8);
INSERT INTO public.products (id, name, description, price, discount_percent, quantity, maker_id, category_id) VALUES (40, 'Велосипедный шлем спортивный регулируемый', 'Легкий шлем со множеством вентиляционных отверстий и съемным козырьком от солнца.', 2450.00, 5, 25, 8, 8);
INSERT INTO public.products (id, name, description, price, discount_percent, quantity, maker_id, category_id) VALUES (41, 'Дрель-шуруповерт аккумуляторная Makita DF333DWYE', 'Компактный инструмент профессионального класса. В комплекте 2 литий-ионных аккумулятора 1.5 Ач.', 8990.00, 5, 20, 9, 9);
INSERT INTO public.products (id, name, description, price, discount_percent, quantity, maker_id, category_id) VALUES (42, 'Перфоратор сетевой Bosch GBH 240', 'Надежный трехрежимный перфоратор мощностью 790 Вт с патроном SDS-plus для тяжелых работ.', 11490.00, 10, 15, 9, 9);
INSERT INTO public.products (id, name, description, price, discount_percent, quantity, maker_id, category_id) VALUES (43, 'Набор слесарно-монтажного инструмента малый (82 предмета)', 'Торцевые головки, биты, ключи из хромованадиевой стали в жестком противоударном кейсе.', 5600.00, 18, 35, 9, 9);
INSERT INTO public.products (id, name, description, price, discount_percent, quantity, maker_id, category_id) VALUES (44, 'Лазерный уровень нивелир DEKO DKLL12PB1', 'Построитель плоскостей с зеленым лучом 3D (12 линий), дальность работы до 20 метров.', 3890.00, 25, 50, 9, 9);
INSERT INTO public.products (id, name, description, price, discount_percent, quantity, maker_id, category_id) VALUES (46, 'Кофе в зернах Lavazza Oro 100% Арабика 1 кг', 'Классический итальянский кофе средней обжарки со сладковатым вкусом и фруктовыми нотками.', 1890.00, 15, 60, 10, 10);
INSERT INTO public.products (id, name, description, price, discount_percent, quantity, maker_id, category_id) VALUES (47, 'Чай зеленый листовой Молочный Улун премиум 100 г', 'Китайский крупнолистовой чай с нежным сливочно-карамельным ароматом и долгим послевкусием.', 450.00, 10, 120, 10, 10);
INSERT INTO public.products (id, name, description, price, discount_percent, quantity, maker_id, category_id) VALUES (51, 'Сухой корм Royal Canin Sterilised', 'Полнорационный сухой корм для стерилизованных кошек в возрасте от 1 до 7 лет. Вес 2 кг.', 1850.00, 5, 120, 11, 12);
INSERT INTO public.products (id, name, description, price, discount_percent, quantity, maker_id, category_id) VALUES (30, 'Сыворотка для лица с ниацинамидом The Ordinary 30 мл', 'Сыворотка Niacinamide 10% + Zinc 1% для борьбы с несовершенствами кожи и регуляции себума.', 950.00, 5, 80, 6, 6);
INSERT INTO public.products (id, name, description, price, discount_percent, quantity, maker_id, category_id) VALUES (35, 'Блендер погружной Polaris PHB 1589A', 'Мультифункциональный прибор 4-в-1: блендер, миксер, измельчитель и насадка для пюре.', 4990.00, 25, 32, 7, 7);
INSERT INTO public.products (id, name, description, price, discount_percent, quantity, maker_id, category_id) VALUES (36, 'Коврик для йоги и фитнеса нескользящий (TPE, 6 мм)', 'Двухслойный экологичный коврик с разметкой для правильного позиционирования тела во время асан.', 1650.00, 15, 80, 8, 8);
INSERT INTO public.products (id, name, description, price, discount_percent, quantity, maker_id, category_id) VALUES (49, 'Паста из твердых сортов пшеницы Barilla Spaghetti n.5 500 г', 'Классические итальянские спагетти, превосходно сохраняющие форму при варке до состояния al dente.', 95.00, 20, 400, 10, 10);
INSERT INTO public.products (id, name, description, price, discount_percent, quantity, maker_id, category_id) VALUES (50, 'Натуральный мед цветочный разнотравье Алтайский 500 г', 'Натуральный пчелиный мед, собранный на пасеках Алтая. Без добавления сахара и консервантов.', 380.00, 5, 110, 10, 10);
INSERT INTO public.products (id, name, description, price, discount_percent, quantity, maker_id, category_id) VALUES (56, 'Комикс ''Человек-Паук: Последняя охота Крэйвена''', 'Культовый сюжет о Человеке-Пауке. Полное издание, мягкая обложка.', 1200.00, 0, 30, 12, 13);
INSERT INTO public.products (id, name, description, price, discount_percent, quantity, maker_id, category_id) VALUES (57, 'Английский язык. Грамматика, Голицынский', 'Сборник упражнений по грамматике английского языка для школьников.', 450.00, 5, 150, 12, 13);
INSERT INTO public.products (id, name, description, price, discount_percent, quantity, maker_id, category_id) VALUES (58, 'Властелин Колец. Подарочное издание', 'Трилогия Дж.Р.Р. Толкина в одном томе с иллюстрациями Джона Хау.', 3500.00, 20, 15, 12, 13);
INSERT INTO public.products (id, name, description, price, discount_percent, quantity, maker_id, category_id) VALUES (59, 'Моторное масло Mobil 1 FS 5W-40', 'Синтетическое моторное масло для бензиновых и дизельных двигателей. Канистра 4 л.', 4200.00, 10, 60, 13, 15);
INSERT INTO public.products (id, name, description, price, discount_percent, quantity, maker_id, category_id) VALUES (60, 'Видеорегистратор 70mai Dash Cam Pro Plus+', 'Автомобильный видеорегистратор с разрешением 2.7K и встроенным GPS-модулем.', 6500.00, 5, 40, 13, 15);
INSERT INTO public.products (id, name, description, price, discount_percent, quantity, maker_id, category_id) VALUES (61, 'Набор инструментов Ombra OMT94S', 'Универсальный набор автомобильных инструментов, 94 предмета в пластиковом кейсе.', 8900.00, 15, 25, 13, 15);
INSERT INTO public.products (id, name, description, price, discount_percent, quantity, maker_id, category_id) VALUES (62, 'Щетки стеклоочистителя Bosch Aerotwin', 'Комплект бескаркасных дворников 600/400 мм, всесезонные.', 1800.00, 0, 100, 13, 15);
INSERT INTO public.products (id, name, description, price, discount_percent, quantity, maker_id, category_id) VALUES (63, 'Диван-кровать угловой ''Атланта''', 'Вместительный угловой диван с механизмом дельфин. Обивка: серый велюр.', 35000.00, 25, 10, 14, 16);
INSERT INTO public.products (id, name, description, price, discount_percent, quantity, maker_id, category_id) VALUES (64, 'Компьютерный стол Сокол КСТ-109', 'Стол для работы и учебы с надстройкой и тумбой. Цвет: дуб сонома.', 6800.00, 10, 35, 14, 16);
INSERT INTO public.products (id, name, description, price, discount_percent, quantity, maker_id, category_id) VALUES (65, 'Шкаф-купе трехдверный ''Бася''', 'Шкаф-купе с центральным зеркалом и штангой для одежды. Цвет: белый.', 22000.00, 15, 12, 14, 16);
INSERT INTO public.products (id, name, description, price, discount_percent, quantity, maker_id, category_id) VALUES (66, 'Кресло офисное Chairman 696', 'Удобное компьютерное кресло с дышащей сетчатой спинкой. Цвет: черный.', 5500.00, 5, 50, 14, 16);
INSERT INTO public.products (id, name, description, price, discount_percent, quantity, maker_id, category_id) VALUES (67, 'Картина по номерам ''Звездная ночь''', 'Набор для рисования по номерам на холсте. Репродукция Ван Гога, 40х50 см.', 850.00, 20, 120, 15, 17);
INSERT INTO public.products (id, name, description, price, discount_percent, quantity, maker_id, category_id) VALUES (69, 'Скетчбук Malevich для графики', 'Блокнот для эскизов и зарисовок на пружине, 80 плотных листов, формат А5.', 450.00, 10, 80, 15, 17);
INSERT INTO public.products (id, name, description, price, discount_percent, quantity, maker_id, category_id) VALUES (70, 'Набор акриловых красок Brauberg', 'Акриловые краски для живописи и декора, 24 базовых цвета в тубах по 12 мл.', 750.00, 5, 90, 15, 17);
INSERT INTO public.products (id, name, description, price, discount_percent, quantity, maker_id, category_id) VALUES (71, 'Золотое кольцо 585 пробы с бриллиантом', 'Классическое помолвочное кольцо из красного золота с одним бриллиантом 0.1 карат.', 28500.00, 10, 8, 16, 18);
INSERT INTO public.products (id, name, description, price, discount_percent, quantity, maker_id, category_id) VALUES (72, 'Серебряная цепь ''Бисмарк''', 'Мужская массивная цепь плетения Бисмарк. Серебро 925 пробы, длина 50 см.', 3200.00, 15, 45, 16, 18);
INSERT INTO public.products (id, name, description, price, discount_percent, quantity, maker_id, category_id) VALUES (73, 'Серьги-пусеты с сапфирами', 'Элегантные пусеты (гвоздики) из белого золота 585 пробы с натуральными сапфирами.', 45000.00, 20, 5, 16, 18);
INSERT INTO public.products (id, name, description, price, discount_percent, quantity, maker_id, category_id) VALUES (74, 'Подвеска ''Знак зодиака Лев''', 'Небольшая аккуратная подвеска со знаком зодиака из красного золота.', 12500.00, 5, 15, 16, 18);
INSERT INTO public.products (id, name, description, price, discount_percent, quantity, maker_id, category_id) VALUES (75, 'Стиральный порошок Ariel Горный родник', 'Порошок для автоматических стиральных машин, отлично справляется с пятнами. Вес 3 кг.', 650.00, 30, 200, 17, 26);
INSERT INTO public.products (id, name, description, price, discount_percent, quantity, maker_id, category_id) VALUES (76, 'Средство для мытья посуды Fairy', 'Густой гель с ароматом сочного лимона. Эффективно расщепляет жир. Объем 900 мл.', 220.00, 10, 350, 17, 26);
INSERT INTO public.products (id, name, description, price, discount_percent, quantity, maker_id, category_id) VALUES (77, 'Зубная паста Splat Professional Биокальций', 'Зубная паста для восстановления эмали и безопасного отбеливания, 100 мл.', 180.00, 5, 150, 17, 26);
INSERT INTO public.products (id, name, description, price, discount_percent, quantity, maker_id, category_id) VALUES (78, 'Гель для душа Palmolive Натурэль Олива', 'Увлажняющий гель для душа с экстрактом оливы и увлажняющим молочком. 750 мл.', 450.00, 15, 120, 17, 26);
INSERT INTO public.products (id, name, description, price, discount_percent, quantity, maker_id, category_id) VALUES (79, 'Консоль Sony PlayStation 5 Blu-ray Edition', 'Игровая приставка нового поколения с дисководом. В комплекте один геймпад DualSense.', 55000.00, 5, 25, 18, 20);
INSERT INTO public.products (id, name, description, price, discount_percent, quantity, maker_id, category_id) VALUES (80, 'Геймпад Microsoft Xbox Wireless Controller', 'Беспроводной контроллер последней ревизии для Xbox Series X/S и ПК. Цвет: черный.', 6500.00, 10, 60, 18, 20);
INSERT INTO public.products (id, name, description, price, discount_percent, quantity, maker_id, category_id) VALUES (81, 'Игра Cyberpunk 2077 Ultimate Edition', 'Диск с игрой Cyberpunk 2077 со всеми дополнениями для PlayStation 5.', 4500.00, 15, 40, 18, 20);
INSERT INTO public.products (id, name, description, price, discount_percent, quantity, maker_id, category_id) VALUES (82, 'Игровая гарнитура HyperX Cloud II', 'Проводные наушники с микрофоном, объемный звук 7.1, алюминиевая рама.', 8900.00, 0, 35, 18, 20);
INSERT INTO public.products (id, name, description, price, discount_percent, quantity, maker_id, category_id) VALUES (83, 'Мужской кошелек бифолд ручной работы', 'Вместительный кошелек из натуральной кожи растительного дубления. Ручной шов.', 3500.00, 10, 20, 19, 19);
INSERT INTO public.products (id, name, description, price, discount_percent, quantity, maker_id, category_id) VALUES (84, 'Кожаный ремень с классической пряжкой', 'Мужской ремень из толстой бычьей кожи с цельнолитой латунной пряжкой.', 2800.00, 5, 45, 19, 19);
INSERT INTO public.products (id, name, description, price, discount_percent, quantity, maker_id, category_id) VALUES (85, 'Сумка-шоппер Crazy Horse', 'Вместительная женская сумка-шоппер из винтажной натуральной кожи. Без подклада.', 8500.00, 15, 10, 19, 19);
INSERT INTO public.products (id, name, description, price, discount_percent, quantity, maker_id, category_id) VALUES (86, 'Обложка для паспорта', 'Минималистичная обложка для паспорта из гладкой итальянской кожи черного цвета.', 1500.00, 0, 60, 19, 19);
INSERT INTO public.products (id, name, description, price, discount_percent, quantity, maker_id, category_id) VALUES (87, 'Палатка кемпинговая Tramp Bell 4', 'Двухслойная четырехместная палатка с просторным тамбуром и москитными сетками.', 18500.00, 20, 15, 20, 14);
INSERT INTO public.products (id, name, description, price, discount_percent, quantity, maker_id, category_id) VALUES (88, 'Спальный мешок Alexika Siberia Wide Plus', 'Теплый спальный мешок-одеяло с фланелевой подкладкой. Температура комфорта 0 °C.', 9200.00, 10, 30, 20, 14);
INSERT INTO public.products (id, name, description, price, discount_percent, quantity, maker_id, category_id) VALUES (89, 'Спиннинг Shimano Catana FX', 'Универсальное спиннинговое удилище из карбона, тест 10-30 г, длина 2.4 м.', 4800.00, 5, 50, 20, 14);
INSERT INTO public.products (id, name, description, price, discount_percent, quantity, maker_id, category_id) VALUES (90, 'Термос Биосталь Классика', 'Термос из нержавеющей стали с узким горлышком для напитков. Объем 1 литр.', 1200.00, 15, 100, 20, 14);
INSERT INTO public.products (id, name, description, price, discount_percent, quantity, maker_id, category_id) VALUES (26, 'Мицеллярная вода Garnier для всех типов кожи 400 мл', 'Эффективно снимает макияж, очищает и успокаивает кожу лица без необходимости смывания.', 380.00, 15, 145, 6, 6);
INSERT INTO public.products (id, name, description, price, discount_percent, quantity, maker_id, category_id) VALUES (24, 'Подгузники-трусики Huggies Elite Soft 4 (9-14 кг, 38 шт)', 'Мягкие дышащие трусики с индикатором влаги и эластичным пояском для защиты от протеканий.', 1690.00, 20, 90, 5, 5);
INSERT INTO public.products (id, name, description, price, discount_percent, quantity, maker_id, category_id) VALUES (38, 'Фитнес-резинки тканевые (Набор 3 штуки)', 'Тканевые ленточные эспандеры разного уровня сопротивления (Light, Medium, Heavy) с мешочком.', 890.00, 30, 120, 8, 8);
INSERT INTO public.products (id, name, description, price, discount_percent, quantity, maker_id, category_id) VALUES (39, 'Спортивная бутылка для воды емкостью 750 мл', 'Стильная эргономичная бутылка из безопасного пластика Tritan с фиксатором крышки.', 650.00, 0, 150, 8, 8);
INSERT INTO public.products (id, name, description, price, discount_percent, quantity, maker_id, category_id) VALUES (45, 'Рулетка строительная 5 метров с автостопом', 'Эргономичный прорезиненный корпус, стальная лента с нейлоновым защитным покрытием.', 450.00, 0, 100, 9, 9);
INSERT INTO public.products (id, name, description, price, discount_percent, quantity, maker_id, category_id) VALUES (48, 'Оливковое масло Borges Extra Virgin первого отжима 500 мл', 'Натуральное нерафинированное оливковое масло высшего качества, идеально подходит для салатов.', 790.00, 0, 85, 10, 10);
INSERT INTO public.products (id, name, description, price, discount_percent, quantity, maker_id, category_id) VALUES (52, 'Древесный наполнитель Сибирская Кошка', 'Экологически чистый древесный наполнитель для кошачьего туалета, отлично впитывает запахи. Объем 5 л.', 350.00, 0, 200, 11, 12);
INSERT INTO public.products (id, name, description, price, discount_percent, quantity, maker_id, category_id) VALUES (20, 'Ароматическая свеча в стекле "Ваниль и сандал"', 'Свеча из натурального соевого воска с хлопковым фитилем. Время горения до 30 часов.', 650.00, 0, 104, 4, 4);
INSERT INTO public.products (id, name, description, price, discount_percent, quantity, maker_id, category_id) VALUES (6, 'Футболка хлопковая базовая Oversize (Черная)', 'Плотная однотонная футболка из 100% хлопка (кулирная гладь, 240 г/м²). Свободный крой, унисекс.', 1490.00, 25, 200, 2, 2);
INSERT INTO public.products (id, name, description, price, discount_percent, quantity, maker_id, category_id) VALUES (53, 'Игрушка для собак Trixie Канат', 'Прочный канат-грейфер из хлопка для активных игр с собакой. Длина 20 см.', 250.00, 10, 50, 11, 12);
INSERT INTO public.products (id, name, description, price, discount_percent, quantity, maker_id, category_id) VALUES (54, 'Влажный корм Pedigree для собак', 'Пауч с говядиной в густом соусе для взрослых собак всех пород. Вес 85 г.', 35.00, 0, 500, 11, 12);
INSERT INTO public.products (id, name, description, price, discount_percent, quantity, maker_id, category_id) VALUES (55, 'Роман ''1984'', Джордж Оруэлл', 'Классическая антиутопия в твердом переплете. Издательство АСТ.', 650.00, 15, 80, 12, 13);
INSERT INTO public.products (id, name, description, price, discount_percent, quantity, maker_id, category_id) VALUES (28, 'Тушь для ресниц Vivienne Sabo Cabaret Premiere (Черная)', 'Легендарная тушь со сценическим эффектом суперобъема и удобной пластиковой щеточкой.', 450.00, 10, 195, 6, 6);
INSERT INTO public.products (id, name, description, price, discount_percent, quantity, maker_id, category_id) VALUES (68, 'Пряжа Alize Puffy', 'Мягкая плюшевая пряжа для вязания руками без спиц. 100% микрополиэстер, 100 г.', 180.00, 0, 300, 15, 17);


--
-- Data for Name: refresh_token; Type: TABLE DATA; Schema: public; Owner: -
--



--
-- Data for Name: reviews; Type: TABLE DATA; Schema: public; Owner: -
--

INSERT INTO public.reviews (id, score, text, created_at, updated_at, user_id, product_id) VALUES (1, 5, 'Перешел с 13 Pro, разница в весе ощутима. Титан очень приятный тактильно, камера снимает отлично.', '2024-01-15 07:20:00+00', NULL, 3, 1);
INSERT INTO public.reviews (id, score, text, created_at, updated_at, user_id, product_id) VALUES (2, 4, 'Всё супер, но батарея держит чуть хуже, чем ожидал. На день хватает впритык.', '2024-01-20 11:15:00+00', NULL, 7, 1);
INSERT INTO public.reviews (id, score, text, created_at, updated_at, user_id, product_id) VALUES (3, 5, 'Отличный аппарат. Цвет "черный титан" вживую смотрится премиально.', '2024-02-01 06:30:00+00', NULL, 12, 1);
INSERT INTO public.reviews (id, score, text, created_at, updated_at, user_id, product_id) VALUES (4, 3, 'Телефон как телефон. Вау-эффекта после 14 Pro нет. Греется при быстрой зарядке.', '2024-02-15 15:45:00+00', '2024-02-16 07:00:00+00', 4, 1);
INSERT INTO public.reviews (id, score, text, created_at, updated_at, user_id, product_id) VALUES (5, 5, 'Лучший смартфон на рынке, экосистема Apple решает.', '2024-03-05 08:10:00+00', NULL, 15, 1);
INSERT INTO public.reviews (id, score, text, created_at, updated_at, user_id, product_id) VALUES (6, 5, 'Экран просто невероятный. Антибликовое покрытие реально работает на солнце.', '2024-02-10 09:00:00+00', NULL, 1, 2);
INSERT INTO public.reviews (id, score, text, created_at, updated_at, user_id, product_id) VALUES (7, 5, 'Стилусом пользуюсь редко, но камеры шикарные. Зум x10 - моё почтение.', '2024-02-18 13:30:00+00', NULL, 8, 2);
INSERT INTO public.reviews (id, score, text, created_at, updated_at, user_id, product_id) VALUES (8, 4, 'Аппарат тяжелый, одной рукой пользоваться сложно. В остальном топ.', '2024-03-01 05:45:00+00', NULL, 14, 2);
INSERT INTO public.reviews (id, score, text, created_at, updated_at, user_id, product_id) VALUES (9, 5, 'Перешел с iOS, привык за неделю. OneUI стала очень плавной.', '2024-03-10 17:15:00+00', NULL, 5, 2);
INSERT INTO public.reviews (id, score, text, created_at, updated_at, user_id, product_id) VALUES (10, 2, 'Через месяц начал хрипеть динамик. Отдал по гарантии.', '2024-04-05 06:20:00+00', NULL, 19, 2);
INSERT INTO public.reviews (id, score, text, created_at, updated_at, user_id, product_id) VALUES (11, 5, 'Батарея держит два дня спокойного использования. Идеальный рабочий инструмент.', '2024-04-12 12:00:00+00', '2024-04-12 12:05:00+00', 9, 2);
INSERT INTO public.reviews (id, score, text, created_at, updated_at, user_id, product_id) VALUES (12, 5, 'Классика. Сели идеально, деним плотный, нитки не торчат.', '2023-11-25 10:40:00+00', NULL, 2, 8);
INSERT INTO public.reviews (id, score, text, created_at, updated_at, user_id, product_id) VALUES (13, 3, 'Большемерят на размер. Пришлось оформлять возврат и заказывать другие.', '2023-12-05 14:10:00+00', NULL, 11, 8);
INSERT INTO public.reviews (id, score, text, created_at, updated_at, user_id, product_id) VALUES (14, 4, 'Качество хорошее, но кажется, раньше ткань была немного плотнее.', '2024-01-10 08:25:00+00', NULL, 16, 8);
INSERT INTO public.reviews (id, score, text, created_at, updated_at, user_id, product_id) VALUES (15, 2, 'Есть сомнения в оригинальности. Бирки выглядят странно, кривой шов на кармане.', '2024-02-20 16:30:00+00', NULL, 6, 8);
INSERT INTO public.reviews (id, score, text, created_at, updated_at, user_id, product_id) VALUES (16, 5, 'Отличная куртка на весну. Цвет хаки приятный, не кричащий.', '2024-03-15 11:00:00+00', NULL, 10, 9);
INSERT INTO public.reviews (id, score, text, created_at, updated_at, user_id, product_id) VALUES (17, 4, 'Тонковата, чисто на температуру +10...+15. Сшита аккуратно.', '2024-04-02 07:15:00+00', NULL, 13, 9);
INSERT INTO public.reviews (id, score, text, created_at, updated_at, user_id, product_id) VALUES (18, 5, 'За свои деньги отличный вариант. Молния работает плавно, карманы глубокие.', '2024-04-20 09:45:00+00', NULL, 3, 9);
INSERT INTO public.reviews (id, score, text, created_at, updated_at, user_id, product_id) VALUES (19, 2, 'После первой же стирки село на размер. Стирала строго по инструкции на бирке.', '2023-10-10 13:50:00+00', NULL, 18, 10);
INSERT INTO public.reviews (id, score, text, created_at, updated_at, user_id, product_id) VALUES (20, 3, 'На каждый день пойдет, но трикотаж довольно тонкий, просвечивает белье.', '2023-11-01 06:10:00+00', NULL, 7, 10);
INSERT INTO public.reviews (id, score, text, created_at, updated_at, user_id, product_id) VALUES (21, 4, 'Фасон удачный, скрывает недостатки фигуры. Цвет немного бледнее, чем на фото.', '2023-12-12 15:00:00+00', NULL, 20, 10);
INSERT INTO public.reviews (id, score, text, created_at, updated_at, user_id, product_id) VALUES (22, 2, 'Много торчащих ниток на швах, смотрится дешево. Ожидала большего.', '2024-01-05 10:20:00+00', NULL, 4, 10);
INSERT INTO public.reviews (id, score, text, created_at, updated_at, user_id, product_id) VALUES (23, 5, 'Аромат волшебный! Пахнет на всю комнату даже когда не зажжена.', '2024-01-25 18:00:00+00', NULL, 1, 20);
INSERT INTO public.reviews (id, score, text, created_at, updated_at, user_id, product_id) VALUES (24, 5, 'Стекло толстое, фитиль качественный, не коптит. Хватает надолго.', '2024-02-14 16:30:00+00', NULL, 8, 20);
INSERT INTO public.reviews (id, score, text, created_at, updated_at, user_id, product_id) VALUES (25, 4, 'Для меня запах оказался немного приторным, но это вкусовщина. Жена в восторге.', '2024-03-08 07:15:00+00', NULL, 15, 20);
INSERT INTO public.reviews (id, score, text, created_at, updated_at, user_id, product_id) VALUES (26, 5, 'Брала в подарок. Упаковано надежно, доехало целым. Аромат дорогой.', '2024-04-01 13:40:00+00', NULL, 12, 20);
INSERT INTO public.reviews (id, score, text, created_at, updated_at, user_id, product_id) VALUES (27, 5, 'Отличная свеча для создания уюта осенними вечерами.', '2024-04-18 17:20:00+00', NULL, 9, 20);
INSERT INTO public.reviews (id, score, text, created_at, updated_at, user_id, product_id) VALUES (28, 3, 'Обычная рулетка. Лента гнется уже на 1.5 метрах, ожидал большей жесткости.', '2023-09-10 08:30:00+00', NULL, 14, 45);
INSERT INTO public.reviews (id, score, text, created_at, updated_at, user_id, product_id) VALUES (29, 2, 'Автостоп сломался на второй день использования. Теперь лента сматывается с дикой скоростью.', '2023-10-05 11:20:00+00', '2023-10-06 06:00:00+00', 5, 45);
INSERT INTO public.reviews (id, score, text, created_at, updated_at, user_id, product_id) VALUES (30, 2, 'Корпус хлипкий, при падении с высоты стола откололся кусок пластика.', '2023-11-20 07:00:00+00', NULL, 17, 45);
INSERT INTO public.reviews (id, score, text, created_at, updated_at, user_id, product_id) VALUES (31, 5, 'Пью этот кофе годами. Мягкий вкус, без лишней кислоты, 100% арабика чувствуется.', '2024-01-10 05:00:00+00', NULL, 2, 46);
INSERT INTO public.reviews (id, score, text, created_at, updated_at, user_id, product_id) VALUES (32, 4, 'Хороший кофе для рожковой кофеварки, но цена последнее время сильно кусается.', '2024-02-05 06:30:00+00', NULL, 11, 46);
INSERT INTO public.reviews (id, score, text, created_at, updated_at, user_id, product_id) VALUES (33, 2, 'В этот раз пришла странная партия. Зерна пережарены, вкус горелый. Либо брак, либо подделка.', '2024-03-12 09:15:00+00', NULL, 6, 46);
INSERT INTO public.reviews (id, score, text, created_at, updated_at, user_id, product_id) VALUES (34, 3, 'Ничего особенного. Кофе как кофе. Для молочных напитков нормально, эспрессо плоский.', '2024-04-02 11:50:00+00', NULL, 19, 46);
INSERT INTO public.reviews (id, score, text, created_at, updated_at, user_id, product_id) VALUES (35, 5, 'Срок годности свежий. Упаковка с клапаном не повреждена. Вкус отличный.', '2024-04-25 08:10:00+00', NULL, 13, 46);
INSERT INTO public.reviews (id, score, text, created_at, updated_at, user_id, product_id) VALUES (36, 5, 'Классика, по которой училось не одно поколение. Идеально для отработки грамматики до автоматизма.', '2023-09-01 12:40:00+00', NULL, 10, 57);
INSERT INTO public.reviews (id, score, text, created_at, updated_at, user_id, product_id) VALUES (37, 4, 'Упражнения скучноватые, но это работает. Репетитор посоветовала купить именно это издание.', '2023-10-15 14:20:00+00', NULL, 3, 57);
INSERT INTO public.reviews (id, score, text, created_at, updated_at, user_id, product_id) VALUES (38, 5, 'Шикарное издание! Плотная бумага, потрясающие иллюстрации Алана Ли. Тяжелый том.', '2023-12-25 07:00:00+00', NULL, 1, 58);
INSERT INTO public.reviews (id, score, text, created_at, updated_at, user_id, product_id) VALUES (39, 5, 'Брал в подарок сыну. Пришло в заводской пленке, углы не помяты. Перевод Кистяковского и Муравьева.', '2024-01-10 10:15:00+00', NULL, 16, 58);
INSERT INTO public.reviews (id, score, text, created_at, updated_at, user_id, product_id) VALUES (40, 4, 'Книга великолепна, но шрифт мог бы быть чуть крупнее. Читать вечером тяжеловато для глаз.', '2024-02-20 18:30:00+00', '2024-02-21 09:00:00+00', 8, 58);
INSERT INTO public.reviews (id, score, text, created_at, updated_at, user_id, product_id) VALUES (41, 5, 'Консоль тихая, загрузки моментальные. Геймпад DualSense - это просто революция в ощущениях.', '2023-11-11 15:00:00+00', NULL, 5, 79);
INSERT INTO public.reviews (id, score, text, created_at, updated_at, user_id, product_id) VALUES (42, 4, 'Всё отлично, но бесит размер. Она просто огромная, еле влезла в тумбу под телевизором.', '2023-12-01 16:45:00+00', NULL, 12, 79);
INSERT INTO public.reviews (id, score, text, created_at, updated_at, user_id, product_id) VALUES (43, 5, 'Эксклюзивы Sony как всегда на высоте. Работает без нареканий уже полгода.', '2024-02-15 13:20:00+00', NULL, 4, 79);
INSERT INTO public.reviews (id, score, text, created_at, updated_at, user_id, product_id) VALUES (44, 3, 'Мало памяти. С учетом размера современных игр 825 ГБ забиваются очень быстро, пришлось докупать SSD.', '2024-04-10 08:10:00+00', NULL, 7, 79);
INSERT INTO public.reviews (id, score, text, created_at, updated_at, user_id, product_id) VALUES (45, 5, 'Теплый, просторный (я 190 см, помещаюсь с запасом). В -5 спал в одном термобелье, было комфортно.', '2023-08-20 06:30:00+00', NULL, 14, 88);
INSERT INTO public.reviews (id, score, text, created_at, updated_at, user_id, product_id) VALUES (46, 4, 'Хороший спальник, но в компрессионный мешок влезает с большим трудом. Тяжеловат для пеших походов.', '2023-09-15 11:15:00+00', NULL, 9, 88);
INSERT INTO public.reviews (id, score, text, created_at, updated_at, user_id, product_id) VALUES (47, 3, 'Качество материалов хорошее, но заявленная температура комфорта явно завышена. При +2 уже было зябко.', '2023-10-05 07:50:00+00', NULL, 17, 88);
INSERT INTO public.reviews (id, score, text, created_at, updated_at, user_id, product_id) VALUES (48, 5, 'Отличный звук и шумоподавление. Полностью оправдали ожидания.', '2026-05-07 13:37:11.025481+00', NULL, 1, 4);
INSERT INTO public.reviews (id, score, text, created_at, updated_at, user_id, product_id) VALUES (49, 5, 'Лучшие наушники, которыми пользовался.', '2026-05-09 13:37:11.025481+00', NULL, 3, 4);
INSERT INTO public.reviews (id, score, text, created_at, updated_at, user_id, product_id) VALUES (50, 4, 'Звук отличный, но цена высоковата.', '2026-05-13 13:37:11.025481+00', NULL, 5, 4);
INSERT INTO public.reviews (id, score, text, created_at, updated_at, user_id, product_id) VALUES (51, 5, 'Очень удобно сидят в ушах.', '2026-05-18 13:37:11.025481+00', NULL, 8, 4);
INSERT INTO public.reviews (id, score, text, created_at, updated_at, user_id, product_id) VALUES (52, 5, 'Батареи хватает надолго.', '2026-05-22 13:37:11.025481+00', NULL, 9, 4);
INSERT INTO public.reviews (id, score, text, created_at, updated_at, user_id, product_id) VALUES (53, 4, 'Иногда переключение между устройствами работает странно.', '2026-05-26 13:37:11.025481+00', NULL, 10, 4);
INSERT INTO public.reviews (id, score, text, created_at, updated_at, user_id, product_id) VALUES (54, 5, 'Отличная покупка.', '2026-06-01 13:37:11.025481+00', NULL, 13, 4);
INSERT INTO public.reviews (id, score, text, created_at, updated_at, user_id, product_id) VALUES (55, 5, 'Использую каждый день, полностью доволен.', '2026-06-09 13:37:11.025481+00', NULL, 15, 4);
INSERT INTO public.reviews (id, score, text, created_at, updated_at, user_id, product_id) VALUES (56, 4, 'Качество сборки на высоте.', '2026-06-18 13:37:11.025481+00', NULL, 18, 4);
INSERT INTO public.reviews (id, score, text, created_at, updated_at, user_id, product_id) VALUES (57, 5, 'Рекомендую.', '2026-06-28 13:37:11.025481+00', NULL, 20, 4);
INSERT INTO public.reviews (id, score, text, created_at, updated_at, user_id, product_id) VALUES (58, 5, 'Очень производительный ноутбук.', '2026-04-27 13:37:11.025481+00', NULL, 2, 5);
INSERT INTO public.reviews (id, score, text, created_at, updated_at, user_id, product_id) VALUES (59, 5, 'Современные игры идут без проблем.', '2026-05-04 13:37:11.025481+00', NULL, 4, 5);
INSERT INTO public.reviews (id, score, text, created_at, updated_at, user_id, product_id) VALUES (60, 4, 'Шумит под нагрузкой, но это ожидаемо.', '2026-05-15 13:37:11.025481+00', NULL, 6, 5);
INSERT INTO public.reviews (id, score, text, created_at, updated_at, user_id, product_id) VALUES (61, 5, 'Экран отличный.', '2026-05-27 13:37:11.025481+00', NULL, 8, 5);
INSERT INTO public.reviews (id, score, text, created_at, updated_at, user_id, product_id) VALUES (62, 5, 'Покупкой доволен.', '2026-06-05 13:37:11.025481+00', NULL, 11, 5);
INSERT INTO public.reviews (id, score, text, created_at, updated_at, user_id, product_id) VALUES (63, 4, 'Автономность могла быть лучше.', '2026-06-15 13:37:11.025481+00', NULL, 16, 5);
INSERT INTO public.reviews (id, score, text, created_at, updated_at, user_id, product_id) VALUES (64, 5, 'Идеален для работы и игр.', '2026-06-30 13:37:11.025481+00', NULL, 19, 5);
INSERT INTO public.reviews (id, score, text, created_at, updated_at, user_id, product_id) VALUES (65, 5, 'Хорошее качество ткани.', '2026-05-12 13:37:11.025481+00', NULL, 1, 6);
INSERT INTO public.reviews (id, score, text, created_at, updated_at, user_id, product_id) VALUES (66, 4, 'После стирки форму сохранила.', '2026-05-24 13:37:11.025481+00', NULL, 7, 6);
INSERT INTO public.reviews (id, score, text, created_at, updated_at, user_id, product_id) VALUES (67, 5, 'Размер соответствует.', '2026-06-04 13:37:11.025481+00', NULL, 9, 6);
INSERT INTO public.reviews (id, score, text, created_at, updated_at, user_id, product_id) VALUES (68, 5, 'Очень удобная.', '2026-06-16 13:37:11.025481+00', NULL, 12, 6);
INSERT INTO public.reviews (id, score, text, created_at, updated_at, user_id, product_id) VALUES (69, 4, 'За свою цену отличный вариант.', '2026-06-29 13:37:11.025481+00', NULL, 14, 6);
INSERT INTO public.reviews (id, score, text, created_at, updated_at, user_id, product_id) VALUES (70, 5, 'Очень удобные.', '2026-04-29 13:37:11.025481+00', NULL, 2, 13);
INSERT INTO public.reviews (id, score, text, created_at, updated_at, user_id, product_id) VALUES (71, 4, 'Качественная кожа.', '2026-05-21 13:37:11.025481+00', NULL, 5, 13);
INSERT INTO public.reviews (id, score, text, created_at, updated_at, user_id, product_id) VALUES (72, 5, 'Ношу ежедневно.', '2026-06-11 13:37:11.025481+00', NULL, 10, 13);
INSERT INTO public.reviews (id, score, text, created_at, updated_at, user_id, product_id) VALUES (73, 5, 'Полностью устраивают.', '2026-06-25 13:37:11.025481+00', NULL, 17, 13);
INSERT INTO public.reviews (id, score, text, created_at, updated_at, user_id, product_id) VALUES (74, 5, 'Красивые и удобные.', '2026-05-10 13:37:11.025481+00', NULL, 4, 14);
INSERT INTO public.reviews (id, score, text, created_at, updated_at, user_id, product_id) VALUES (75, 4, 'На узкую ногу подходят отлично.', '2026-06-07 13:37:11.025481+00', NULL, 6, 14);
INSERT INTO public.reviews (id, score, text, created_at, updated_at, user_id, product_id) VALUES (76, 5, 'Выглядят дорого.', '2026-07-01 13:37:11.025481+00', NULL, 18, 14);
INSERT INTO public.reviews (id, score, text, created_at, updated_at, user_id, product_id) VALUES (77, 5, 'Ребенку удобно бегать.', '2026-05-19 13:37:11.025481+00', NULL, 7, 15);
INSERT INTO public.reviews (id, score, text, created_at, updated_at, user_id, product_id) VALUES (78, 4, 'Хорошее качество.', '2026-06-21 13:37:11.025481+00', NULL, 13, 15);
INSERT INTO public.reviews (id, score, text, created_at, updated_at, user_id, product_id) VALUES (79, 5, 'Очень мягкий и теплый.', '2026-05-16 13:37:11.025481+00', NULL, 8, 17);
INSERT INTO public.reviews (id, score, text, created_at, updated_at, user_id, product_id) VALUES (80, 5, 'Приятный материал.', '2026-06-12 13:37:11.025481+00', NULL, 11, 17);
INSERT INTO public.reviews (id, score, text, created_at, updated_at, user_id, product_id) VALUES (81, 4, 'После стирки не потерял вид.', '2026-06-27 13:37:11.025481+00', NULL, 16, 17);
INSERT INTO public.reviews (id, score, text, created_at, updated_at, user_id, product_id) VALUES (82, 5, 'Красивый набор.', '2026-05-05 13:37:11.025481+00', NULL, 3, 18);
INSERT INTO public.reviews (id, score, text, created_at, updated_at, user_id, product_id) VALUES (83, 5, 'Используем каждый день.', '2026-05-31 13:37:11.025481+00', NULL, 9, 18);
INSERT INTO public.reviews (id, score, text, created_at, updated_at, user_id, product_id) VALUES (84, 4, 'Все пришло целым.', '2026-06-26 13:37:11.025481+00', NULL, 20, 18);
INSERT INTO public.reviews (id, score, text, created_at, updated_at, user_id, product_id) VALUES (85, 5, 'Хорошо снимает макияж.', '2026-05-23 13:37:11.025481+00', NULL, 5, 26);
INSERT INTO public.reviews (id, score, text, created_at, updated_at, user_id, product_id) VALUES (86, 5, 'Не раздражает кожу.', '2026-06-08 13:37:11.025481+00', NULL, 12, 26);
INSERT INTO public.reviews (id, score, text, created_at, updated_at, user_id, product_id) VALUES (87, 4, 'Пользуюсь давно.', '2026-06-24 13:37:11.025481+00', NULL, 15, 26);
INSERT INTO public.reviews (id, score, text, created_at, updated_at, user_id, product_id) VALUES (88, 5, 'Отлично увлажняет.', '2026-05-08 13:37:11.025481+00', NULL, 1, 27);
INSERT INTO public.reviews (id, score, text, created_at, updated_at, user_id, product_id) VALUES (89, 5, 'Подошел чувствительной коже.', '2026-05-14 13:37:11.025481+00', NULL, 4, 27);
INSERT INTO public.reviews (id, score, text, created_at, updated_at, user_id, product_id) VALUES (90, 4, 'Экономичный расход.', '2026-05-25 13:37:11.025481+00', NULL, 6, 27);
INSERT INTO public.reviews (id, score, text, created_at, updated_at, user_id, product_id) VALUES (91, 5, 'Теперь покупаю только его.', '2026-06-06 13:37:11.025481+00', NULL, 10, 27);
INSERT INTO public.reviews (id, score, text, created_at, updated_at, user_id, product_id) VALUES (92, 5, 'Очень хороший крем.', '2026-06-19 13:37:11.025481+00', NULL, 17, 27);
INSERT INTO public.reviews (id, score, text, created_at, updated_at, user_id, product_id) VALUES (93, 5, 'Рекомендовал дерматолог.', '2026-07-02 13:37:11.025481+00', NULL, 19, 27);
INSERT INTO public.reviews (id, score, text, created_at, updated_at, user_id, product_id) VALUES (94, 5, 'Греет быстро и равномерно.', '2026-05-02 13:37:11.025481+00', NULL, 2, 34);
INSERT INTO public.reviews (id, score, text, created_at, updated_at, user_id, product_id) VALUES (95, 4, 'Простое управление.', '2026-05-28 13:37:11.025481+00', NULL, 8, 34);
INSERT INTO public.reviews (id, score, text, created_at, updated_at, user_id, product_id) VALUES (96, 5, 'Работает тихо.', '2026-06-14 13:37:11.025481+00', NULL, 14, 34);
INSERT INTO public.reviews (id, score, text, created_at, updated_at, user_id, product_id) VALUES (97, 5, 'Отличная микроволновка.', '2026-07-03 13:37:11.025481+00', NULL, 18, 34);


--
-- Data for Name: sellers; Type: TABLE DATA; Schema: public; Owner: -
--

INSERT INTO public.sellers (id, name, description) VALUES (1, 'TechnoMarket', 'Широкий выбор современной электроники, смартфонов и аксессуаров от ведущих мировых брендов.');
INSERT INTO public.sellers (id, name, description) VALUES (2, 'МодныйГардероб', 'Стильная повседневная одежда для мужчин и женщин. Обновление коллекций каждый сезон.');
INSERT INTO public.sellers (id, name, description) VALUES (3, 'Шаг за Шагом', 'Качественная мужская, женская и детская обувь на любой сезон и вкус.');
INSERT INTO public.sellers (id, name, description) VALUES (4, 'CozyHome', 'Товары для интерьера, текстиль, посуда и декор, которые создадут уют в вашей квартире.');
INSERT INTO public.sellers (id, name, description) VALUES (5, 'Детский Мир Плюс', 'Игрушки, развивающие игры и товары для ухода за детьми всех возрастов.');
INSERT INTO public.sellers (id, name, description) VALUES (6, 'BeautyZone', 'Оригинальная уходовая и декоративная косметика, а также парфюмерия мировых брендов.');
INSERT INTO public.sellers (id, name, description) VALUES (7, 'ElectroCity', 'Крупная и мелкая бытовая техника для кухни и дома с официальной гарантией.');
INSERT INTO public.sellers (id, name, description) VALUES (8, 'SportDrive', 'Спортивная экипировка, тренажеры, одежда и аксессуары для активного образа жизни.');
INSERT INTO public.sellers (id, name, description) VALUES (9, 'МастерИнструмент', 'Профессиональный электроинструмент и строительные материалы для ремонта любой сложности.');
INSERT INTO public.sellers (id, name, description) VALUES (10, 'EcoFoods', 'Натуральные, органические и фермерские продукты питания с доставкой прямо к вашему столу.');
INSERT INTO public.sellers (id, name, description) VALUES (11, 'ЗооСфера', 'Корма, игрушки, лакомства и аксессуары для ваших домашних питомцев.');
INSERT INTO public.sellers (id, name, description) VALUES (12, 'Книжная Лавка', 'Художественная литература, учебные пособия, комиксы и подарочные издания книг.');
INSERT INTO public.sellers (id, name, description) VALUES (13, 'AutoPro', 'Автомобильная электроника, инструменты, масла и полезные аксессуары для автовладельцев.');
INSERT INTO public.sellers (id, name, description) VALUES (14, 'Мебельный Стиль', 'Современная и функциональная корпусная и мягкая мебель для дома и офиса.');
INSERT INTO public.sellers (id, name, description) VALUES (15, 'HobbyPlanet', 'Всё для рукоделия, рисования, моделирования и других видов творчества.');
INSERT INTO public.sellers (id, name, description) VALUES (16, 'Золотой Век', 'Изысканные ювелирные украшения из золота и серебра с драгоценными камнями.');
INSERT INTO public.sellers (id, name, description) VALUES (17, 'CleanHome', 'Эффективная бытовая химия, средства для стирки и личной гигиены для всей семьи.');
INSERT INTO public.sellers (id, name, description) VALUES (18, 'GameGamer', 'Игровые консоли, диски с играми, геймпады и периферия для настоящих геймеров.');
INSERT INTO public.sellers (id, name, description) VALUES (19, 'ИП Алексеев А.В.', 'Небольшое локальное производство крафтовых изделий и кожаных аксессуаров ручной работы.');
INSERT INTO public.sellers (id, name, description) VALUES (20, 'OutdoorLife', 'Палатки, спальные мешки, снаряжение для рыбалки, охоты и кемпинга.');


--
-- Data for Name: user_pickup_points; Type: TABLE DATA; Schema: public; Owner: -
--

INSERT INTO public.user_pickup_points (user_id, pickup_point_id, selected_at, added_at) VALUES (2, 1, '2025-05-20 11:00:00+00', '2025-01-18 07:00:00+00');
INSERT INTO public.user_pickup_points (user_id, pickup_point_id, selected_at, added_at) VALUES (2, 2, '2025-03-01 12:30:00+00', '2025-03-01 12:30:00+00');
INSERT INTO public.user_pickup_points (user_id, pickup_point_id, selected_at, added_at) VALUES (2, 5, '2025-06-15 18:00:00+00', '2025-04-10 09:00:00+00');
INSERT INTO public.user_pickup_points (user_id, pickup_point_id, selected_at, added_at) VALUES (2, 6, '2025-04-22 07:10:00+00', '2025-04-20 14:15:00+00');
INSERT INTO public.user_pickup_points (user_id, pickup_point_id, selected_at, added_at) VALUES (2, 7, '2025-05-10 08:00:00+00', '2025-05-10 08:00:00+00');
INSERT INTO public.user_pickup_points (user_id, pickup_point_id, selected_at, added_at) VALUES (3, 8, '2025-04-05 09:00:00+00', '2025-01-15 08:00:00+00');
INSERT INTO public.user_pickup_points (user_id, pickup_point_id, selected_at, added_at) VALUES (3, 9, '2025-03-01 10:40:00+00', '2025-03-01 10:40:00+00');
INSERT INTO public.user_pickup_points (user_id, pickup_point_id, selected_at, added_at) VALUES (3, 11, '2025-05-02 15:30:00+00', '2025-03-28 13:00:00+00');
INSERT INTO public.user_pickup_points (user_id, pickup_point_id, selected_at, added_at) VALUES (3, 12, '2025-04-10 11:15:00+00', '2025-04-05 06:30:00+00');
INSERT INTO public.user_pickup_points (user_id, pickup_point_id, selected_at, added_at) VALUES (3, 13, '2025-04-25 08:00:00+00', '2025-04-20 07:00:00+00');
INSERT INTO public.user_pickup_points (user_id, pickup_point_id, selected_at, added_at) VALUES (3, 14, '2025-05-01 12:00:00+00', '2025-05-01 12:00:00+00');
INSERT INTO public.user_pickup_points (user_id, pickup_point_id, selected_at, added_at) VALUES (4, 15, '2025-02-28 16:30:00+00', '2025-01-20 11:00:00+00');
INSERT INTO public.user_pickup_points (user_id, pickup_point_id, selected_at, added_at) VALUES (4, 16, '2025-03-10 09:00:00+00', '2025-02-22 08:15:00+00');
INSERT INTO public.user_pickup_points (user_id, pickup_point_id, selected_at, added_at) VALUES (4, 17, '2025-03-10 06:00:00+00', '2025-03-10 06:00:00+00');
INSERT INTO public.user_pickup_points (user_id, pickup_point_id, selected_at, added_at) VALUES (4, 3, '2025-05-14 12:20:00+00', '2025-03-15 13:40:00+00');
INSERT INTO public.user_pickup_points (user_id, pickup_point_id, selected_at, added_at) VALUES (4, 5, '2025-04-12 10:00:00+00', '2025-04-12 10:00:00+00');
INSERT INTO public.user_pickup_points (user_id, pickup_point_id, selected_at, added_at) VALUES (4, 6, '2025-06-20 08:45:00+00', '2025-04-25 15:20:00+00');
INSERT INTO public.user_pickup_points (user_id, pickup_point_id, selected_at, added_at) VALUES (5, 15, '2025-05-10 13:00:00+00', '2025-01-18 12:30:00+00');
INSERT INTO public.user_pickup_points (user_id, pickup_point_id, selected_at, added_at) VALUES (5, 16, '2025-03-01 07:00:00+00', '2025-03-01 07:00:00+00');
INSERT INTO public.user_pickup_points (user_id, pickup_point_id, selected_at, added_at) VALUES (5, 17, '2025-04-18 16:10:00+00', '2025-03-12 09:20:00+00');
INSERT INTO public.user_pickup_points (user_id, pickup_point_id, selected_at, added_at) VALUES (5, 18, '2025-03-25 11:00:00+00', '2025-03-25 11:00:00+00');
INSERT INTO public.user_pickup_points (user_id, pickup_point_id, selected_at, added_at) VALUES (5, 19, '2025-06-02 10:40:00+00', '2025-04-15 08:35:00+00');
INSERT INTO public.user_pickup_points (user_id, pickup_point_id, selected_at, added_at) VALUES (1, 3, '2026-07-25 16:39:12.791752+00', '2025-03-05 07:00:00+00');
INSERT INTO public.user_pickup_points (user_id, pickup_point_id, selected_at, added_at) VALUES (1, 2, '2026-07-25 19:25:54.649795+00', '2025-02-20 11:00:00+00');
INSERT INTO public.user_pickup_points (user_id, pickup_point_id, selected_at, added_at) VALUES (1, 8, '2026-09-01 13:00:39.425463+00', '2025-01-15 08:00:00+00');
INSERT INTO public.user_pickup_points (user_id, pickup_point_id, selected_at, added_at) VALUES (1, 4, '2026-09-01 13:00:39.882396+00', '2025-03-22 08:30:00+00');
INSERT INTO public.user_pickup_points (user_id, pickup_point_id, selected_at, added_at) VALUES (1, 1, '2026-09-04 17:08:11.053026+00', '2025-01-15 09:00:00+00');


--
-- Data for Name: users; Type: TABLE DATA; Schema: public; Owner: -
--

INSERT INTO public.users (id, login, email, created_at, last_login_at, phone_number) VALUES (6, 'User6', 'user6@gmail.com', '2026-05-07 19:53:01.600867+00', '2026-05-28 19:52:40.271401+00', '79999999999');
INSERT INTO public.users (id, login, email, created_at, last_login_at, phone_number) VALUES (15, 'User15', 'user15@gmail.com', '2026-05-07 19:53:01.600867+00', '2026-05-28 19:52:40.271401+00', '79999999999');
INSERT INTO public.users (id, login, email, created_at, last_login_at, phone_number) VALUES (12, 'User12', 'user12@gmail.com', '2026-05-07 19:53:01.600867+00', '2026-05-28 19:52:40.271401+00', '79999999999');
INSERT INTO public.users (id, login, email, created_at, last_login_at, phone_number) VALUES (13, 'User13', 'user13@gmail.com', '2026-05-07 19:53:01.600867+00', '2026-05-28 19:52:40.271401+00', '79999999999');
INSERT INTO public.users (id, login, email, created_at, last_login_at, phone_number) VALUES (2, 'User2', 'user2@gmail.com', '2026-05-07 19:53:01.600867+00', '2026-05-28 19:52:40.271401+00', '79096995254');
INSERT INTO public.users (id, login, email, created_at, last_login_at, phone_number) VALUES (4, 'User4', 'user4@gmail.com', '2026-05-07 19:53:01.600867+00', '2026-05-28 19:52:40.271401+00', '79999999999');
INSERT INTO public.users (id, login, email, created_at, last_login_at, phone_number) VALUES (1, 'User1', 'artrad32@gmail.com', '2026-06-15 19:51:28.329363+00', '2026-09-04 10:17:17.763759+00', '79501235424');
INSERT INTO public.users (id, login, email, created_at, last_login_at, phone_number) VALUES (20, 'User20', 'user20@gmail.com', '2026-05-07 19:53:01.600867+00', '2026-05-28 19:52:40.271401+00', '79999999999');
INSERT INTO public.users (id, login, email, created_at, last_login_at, phone_number) VALUES (9, 'User9', 'user9@gmail.com', '2026-05-07 19:53:01.600867+00', '2026-05-28 19:52:40.271401+00', '79999999999');
INSERT INTO public.users (id, login, email, created_at, last_login_at, phone_number) VALUES (11, 'User11', 'user11@gmail.com', '2026-05-07 19:53:01.600867+00', '2026-05-28 19:52:40.271401+00', '79999999999');
INSERT INTO public.users (id, login, email, created_at, last_login_at, phone_number) VALUES (5, 'User5', 'user5@gmail.com', '2026-05-07 19:53:01.600867+00', '2026-05-28 19:52:40.271401+00', '79999999999');
INSERT INTO public.users (id, login, email, created_at, last_login_at, phone_number) VALUES (7, 'User7', 'user7@gmail.com', '2026-05-07 19:53:01.600867+00', '2026-05-28 19:52:40.271401+00', '79999999999');
INSERT INTO public.users (id, login, email, created_at, last_login_at, phone_number) VALUES (10, 'User10', 'user10@gmail.com', '2026-05-07 19:53:01.600867+00', '2026-05-28 19:52:40.271401+00', '79999999999');
INSERT INTO public.users (id, login, email, created_at, last_login_at, phone_number) VALUES (3, 'User3', 'user3@gmail.com', '2026-05-07 19:53:01.600867+00', '2026-05-28 19:52:40.271401+00', '79999999999');
INSERT INTO public.users (id, login, email, created_at, last_login_at, phone_number) VALUES (8, 'User8', 'user8@gmail.com', '2026-05-07 19:53:01.600867+00', '2026-05-28 19:52:40.271401+00', '79999999999');
INSERT INTO public.users (id, login, email, created_at, last_login_at, phone_number) VALUES (14, 'User14', 'user14@gmail.com', '2026-05-07 19:53:01.600867+00', '2026-05-28 19:52:40.271401+00', '79999999999');
INSERT INTO public.users (id, login, email, created_at, last_login_at, phone_number) VALUES (16, 'User16', 'user16@gmail.com', '2026-05-07 19:53:01.600867+00', '2026-05-28 19:52:40.271401+00', '79999999999');
INSERT INTO public.users (id, login, email, created_at, last_login_at, phone_number) VALUES (19, 'User19', 'user19@gmail.com', '2026-05-07 19:53:01.600867+00', '2026-05-28 19:52:40.271401+00', '79999999999');
INSERT INTO public.users (id, login, email, created_at, last_login_at, phone_number) VALUES (17, 'User17', 'user17@gmail.com', '2026-05-07 19:53:01.600867+00', '2026-05-28 19:52:40.271401+00', '79999999999');
INSERT INTO public.users (id, login, email, created_at, last_login_at, phone_number) VALUES (18, 'User18', 'user18@gmail.com', '2026-05-07 19:53:01.600867+00', '2026-05-28 19:52:40.271401+00', '79999999999');


--
-- Data for Name: wished_products; Type: TABLE DATA; Schema: public; Owner: -
--

INSERT INTO public.wished_products (wishlist_id, product_id, added_at) VALUES (1, 6, '2026-07-26 12:04:57.524863+00');
INSERT INTO public.wished_products (wishlist_id, product_id, added_at) VALUES (1, 28, '2026-07-27 12:04:14.806444+00');
INSERT INTO public.wished_products (wishlist_id, product_id, added_at) VALUES (1, 15, '2026-07-27 12:04:19.416638+00');
INSERT INTO public.wished_products (wishlist_id, product_id, added_at) VALUES (1, 20, '2026-07-27 12:04:20.236958+00');
INSERT INTO public.wished_products (wishlist_id, product_id, added_at) VALUES (1, 24, '2026-07-16 13:46:37.327228+00');
INSERT INTO public.wished_products (wishlist_id, product_id, added_at) VALUES (1, 23, '2026-07-16 13:46:37.939056+00');
INSERT INTO public.wished_products (wishlist_id, product_id, added_at) VALUES (1, 22, '2026-07-16 13:46:38.608041+00');
INSERT INTO public.wished_products (wishlist_id, product_id, added_at) VALUES (1, 25, '2026-07-16 13:46:41.36814+00');
INSERT INTO public.wished_products (wishlist_id, product_id, added_at) VALUES (1, 26, '2026-07-16 13:46:41.950009+00');
INSERT INTO public.wished_products (wishlist_id, product_id, added_at) VALUES (1, 27, '2026-07-16 13:46:42.634548+00');
INSERT INTO public.wished_products (wishlist_id, product_id, added_at) VALUES (1, 29, '2026-07-16 13:46:47.016398+00');
INSERT INTO public.wished_products (wishlist_id, product_id, added_at) VALUES (1, 3, '2026-09-01 13:20:08.72037+00');
INSERT INTO public.wished_products (wishlist_id, product_id, added_at) VALUES (1, 11, '2026-07-16 14:23:16.655887+00');
INSERT INTO public.wished_products (wishlist_id, product_id, added_at) VALUES (1, 10, '2026-07-16 14:23:17.356246+00');


--
-- Data for Name: wishlists; Type: TABLE DATA; Schema: public; Owner: -
--

INSERT INTO public.wishlists (id, user_id) VALUES (1, 1);
INSERT INTO public.wishlists (id, user_id) VALUES (2, 2);
INSERT INTO public.wishlists (id, user_id) VALUES (3, 3);
INSERT INTO public.wishlists (id, user_id) VALUES (4, 4);
INSERT INTO public.wishlists (id, user_id) VALUES (5, 5);
INSERT INTO public.wishlists (id, user_id) VALUES (6, 6);
INSERT INTO public.wishlists (id, user_id) VALUES (7, 7);
INSERT INTO public.wishlists (id, user_id) VALUES (8, 8);
INSERT INTO public.wishlists (id, user_id) VALUES (9, 9);
INSERT INTO public.wishlists (id, user_id) VALUES (10, 10);
INSERT INTO public.wishlists (id, user_id) VALUES (11, 11);
INSERT INTO public.wishlists (id, user_id) VALUES (12, 12);
INSERT INTO public.wishlists (id, user_id) VALUES (13, 13);
INSERT INTO public.wishlists (id, user_id) VALUES (14, 14);
INSERT INTO public.wishlists (id, user_id) VALUES (15, 15);
INSERT INTO public.wishlists (id, user_id) VALUES (16, 16);
INSERT INTO public.wishlists (id, user_id) VALUES (17, 17);
INSERT INTO public.wishlists (id, user_id) VALUES (18, 18);
INSERT INTO public.wishlists (id, user_id) VALUES (19, 19);
INSERT INTO public.wishlists (id, user_id) VALUES (20, 20);


--
-- Name: carts_id_seq; Type: SEQUENCE SET; Schema: public; Owner: -
--

SELECT pg_catalog.setval('public.carts_id_seq', 20, true);


--
-- Name: categories_id_seq; Type: SEQUENCE SET; Schema: public; Owner: -
--

SELECT pg_catalog.setval('public.categories_id_seq', 29, true);


--
-- Name: orders_id_seq; Type: SEQUENCE SET; Schema: public; Owner: -
--

SELECT pg_catalog.setval('public.orders_id_seq', 56, true);


--
-- Name: pickup_points_id_seq; Type: SEQUENCE SET; Schema: public; Owner: -
--

SELECT pg_catalog.setval('public.pickup_points_id_seq', 1, false);


--
-- Name: product_images_id_seq; Type: SEQUENCE SET; Schema: public; Owner: -
--

SELECT pg_catalog.setval('public.product_images_id_seq', 1, false);


--
-- Name: products_id_seq; Type: SEQUENCE SET; Schema: public; Owner: -
--

SELECT pg_catalog.setval('public.products_id_seq', 90, true);


--
-- Name: refresh_token_id_seq; Type: SEQUENCE SET; Schema: public; Owner: -
--

SELECT pg_catalog.setval('public.refresh_token_id_seq', 27, true);


--
-- Name: reviews_id_seq; Type: SEQUENCE SET; Schema: public; Owner: -
--

SELECT pg_catalog.setval('public.reviews_id_seq', 97, true);


--
-- Name: sellers_id_seq; Type: SEQUENCE SET; Schema: public; Owner: -
--

SELECT pg_catalog.setval('public.sellers_id_seq', 20, true);


--
-- Name: users_id_seq; Type: SEQUENCE SET; Schema: public; Owner: -
--

SELECT pg_catalog.setval('public.users_id_seq', 20, true);


--
-- Name: wishlists_id_seq; Type: SEQUENCE SET; Schema: public; Owner: -
--

SELECT pg_catalog.setval('public.wishlists_id_seq', 20, true);


--
-- Name: __EFMigrationsHistory PK___EFMigrationsHistory; Type: CONSTRAINT; Schema: public; Owner: -
--

ALTER TABLE ONLY public."__EFMigrationsHistory"
    ADD CONSTRAINT "PK___EFMigrationsHistory" PRIMARY KEY ("MigrationId");


--
-- Name: cart_positions PK_cart_positions; Type: CONSTRAINT; Schema: public; Owner: -
--

ALTER TABLE ONLY public.cart_positions
    ADD CONSTRAINT "PK_cart_positions" PRIMARY KEY (cart_id, product_id);


--
-- Name: carts PK_carts; Type: CONSTRAINT; Schema: public; Owner: -
--

ALTER TABLE ONLY public.carts
    ADD CONSTRAINT "PK_carts" PRIMARY KEY (id);


--
-- Name: categories PK_categories; Type: CONSTRAINT; Schema: public; Owner: -
--

ALTER TABLE ONLY public.categories
    ADD CONSTRAINT "PK_categories" PRIMARY KEY (id);


--
-- Name: order_positions PK_order_positions; Type: CONSTRAINT; Schema: public; Owner: -
--

ALTER TABLE ONLY public.order_positions
    ADD CONSTRAINT "PK_order_positions" PRIMARY KEY (order_id, product_id);


--
-- Name: orders PK_orders; Type: CONSTRAINT; Schema: public; Owner: -
--

ALTER TABLE ONLY public.orders
    ADD CONSTRAINT "PK_orders" PRIMARY KEY (id);


--
-- Name: pickup_points PK_pickup_points; Type: CONSTRAINT; Schema: public; Owner: -
--

ALTER TABLE ONLY public.pickup_points
    ADD CONSTRAINT "PK_pickup_points" PRIMARY KEY (id);


--
-- Name: product_images PK_product_images; Type: CONSTRAINT; Schema: public; Owner: -
--

ALTER TABLE ONLY public.product_images
    ADD CONSTRAINT "PK_product_images" PRIMARY KEY (id);


--
-- Name: products PK_products; Type: CONSTRAINT; Schema: public; Owner: -
--

ALTER TABLE ONLY public.products
    ADD CONSTRAINT "PK_products" PRIMARY KEY (id);


--
-- Name: refresh_token PK_refresh_token; Type: CONSTRAINT; Schema: public; Owner: -
--

ALTER TABLE ONLY public.refresh_token
    ADD CONSTRAINT "PK_refresh_token" PRIMARY KEY (id);


--
-- Name: reviews PK_reviews; Type: CONSTRAINT; Schema: public; Owner: -
--

ALTER TABLE ONLY public.reviews
    ADD CONSTRAINT "PK_reviews" PRIMARY KEY (id);


--
-- Name: sellers PK_sellers; Type: CONSTRAINT; Schema: public; Owner: -
--

ALTER TABLE ONLY public.sellers
    ADD CONSTRAINT "PK_sellers" PRIMARY KEY (id);


--
-- Name: user_pickup_points PK_user_pickup_points; Type: CONSTRAINT; Schema: public; Owner: -
--

ALTER TABLE ONLY public.user_pickup_points
    ADD CONSTRAINT "PK_user_pickup_points" PRIMARY KEY (user_id, pickup_point_id);


--
-- Name: users PK_users; Type: CONSTRAINT; Schema: public; Owner: -
--

ALTER TABLE ONLY public.users
    ADD CONSTRAINT "PK_users" PRIMARY KEY (id);


--
-- Name: wished_products PK_wished_products; Type: CONSTRAINT; Schema: public; Owner: -
--

ALTER TABLE ONLY public.wished_products
    ADD CONSTRAINT "PK_wished_products" PRIMARY KEY (wishlist_id, product_id);


--
-- Name: wishlists PK_wishlists; Type: CONSTRAINT; Schema: public; Owner: -
--

ALTER TABLE ONLY public.wishlists
    ADD CONSTRAINT "PK_wishlists" PRIMARY KEY (id);


--
-- Name: IX_cart_positions_cart_id; Type: INDEX; Schema: public; Owner: -
--

CREATE INDEX "IX_cart_positions_cart_id" ON public.cart_positions USING btree (cart_id);


--
-- Name: IX_cart_positions_product_id; Type: INDEX; Schema: public; Owner: -
--

CREATE INDEX "IX_cart_positions_product_id" ON public.cart_positions USING btree (product_id);


--
-- Name: IX_carts_user_id; Type: INDEX; Schema: public; Owner: -
--

CREATE UNIQUE INDEX "IX_carts_user_id" ON public.carts USING btree (user_id);


--
-- Name: IX_categories_name; Type: INDEX; Schema: public; Owner: -
--

CREATE UNIQUE INDEX "IX_categories_name" ON public.categories USING btree (name);


--
-- Name: IX_order_positions_order_id; Type: INDEX; Schema: public; Owner: -
--

CREATE INDEX "IX_order_positions_order_id" ON public.order_positions USING btree (order_id);


--
-- Name: IX_order_positions_product_id; Type: INDEX; Schema: public; Owner: -
--

CREATE INDEX "IX_order_positions_product_id" ON public.order_positions USING btree (product_id);


--
-- Name: IX_orders_pickup_point_id; Type: INDEX; Schema: public; Owner: -
--

CREATE INDEX "IX_orders_pickup_point_id" ON public.orders USING btree (pickup_point_id);


--
-- Name: IX_orders_status; Type: INDEX; Schema: public; Owner: -
--

CREATE INDEX "IX_orders_status" ON public.orders USING btree (status);


--
-- Name: IX_orders_user_id; Type: INDEX; Schema: public; Owner: -
--

CREATE INDEX "IX_orders_user_id" ON public.orders USING btree (user_id);


--
-- Name: IX_product_images_product_id; Type: INDEX; Schema: public; Owner: -
--

CREATE INDEX "IX_product_images_product_id" ON public.product_images USING btree (product_id);


--
-- Name: IX_product_images_product_id_position; Type: INDEX; Schema: public; Owner: -
--

CREATE UNIQUE INDEX "IX_product_images_product_id_position" ON public.product_images USING btree (product_id, "position");


--
-- Name: IX_products_category_id; Type: INDEX; Schema: public; Owner: -
--

CREATE INDEX "IX_products_category_id" ON public.products USING btree (category_id);


--
-- Name: IX_products_maker_id; Type: INDEX; Schema: public; Owner: -
--

CREATE INDEX "IX_products_maker_id" ON public.products USING btree (maker_id);


--
-- Name: IX_refresh_token_token; Type: INDEX; Schema: public; Owner: -
--

CREATE INDEX "IX_refresh_token_token" ON public.refresh_token USING btree (token);


--
-- Name: IX_refresh_token_user_id; Type: INDEX; Schema: public; Owner: -
--

CREATE INDEX "IX_refresh_token_user_id" ON public.refresh_token USING btree (user_id);


--
-- Name: IX_reviews_product_id; Type: INDEX; Schema: public; Owner: -
--

CREATE INDEX "IX_reviews_product_id" ON public.reviews USING btree (product_id);


--
-- Name: IX_reviews_user_id; Type: INDEX; Schema: public; Owner: -
--

CREATE INDEX "IX_reviews_user_id" ON public.reviews USING btree (user_id);


--
-- Name: IX_reviews_user_id_product_id; Type: INDEX; Schema: public; Owner: -
--

CREATE UNIQUE INDEX "IX_reviews_user_id_product_id" ON public.reviews USING btree (user_id, product_id);


--
-- Name: IX_sellers_name; Type: INDEX; Schema: public; Owner: -
--

CREATE UNIQUE INDEX "IX_sellers_name" ON public.sellers USING btree (name);


--
-- Name: IX_user_pickup_points_pickup_point_id; Type: INDEX; Schema: public; Owner: -
--

CREATE INDEX "IX_user_pickup_points_pickup_point_id" ON public.user_pickup_points USING btree (pickup_point_id);


--
-- Name: IX_users_email; Type: INDEX; Schema: public; Owner: -
--

CREATE UNIQUE INDEX "IX_users_email" ON public.users USING btree (email);


--
-- Name: IX_wished_products_product_id; Type: INDEX; Schema: public; Owner: -
--

CREATE INDEX "IX_wished_products_product_id" ON public.wished_products USING btree (product_id);


--
-- Name: IX_wished_products_wishlist_id; Type: INDEX; Schema: public; Owner: -
--

CREATE INDEX "IX_wished_products_wishlist_id" ON public.wished_products USING btree (wishlist_id);


--
-- Name: IX_wishlists_user_id; Type: INDEX; Schema: public; Owner: -
--

CREATE UNIQUE INDEX "IX_wishlists_user_id" ON public.wishlists USING btree (user_id);


--
-- Name: cart_positions FK_cart_positions_carts_cart_id; Type: FK CONSTRAINT; Schema: public; Owner: -
--

ALTER TABLE ONLY public.cart_positions
    ADD CONSTRAINT "FK_cart_positions_carts_cart_id" FOREIGN KEY (cart_id) REFERENCES public.carts(id) ON DELETE CASCADE;


--
-- Name: cart_positions FK_cart_positions_products_product_id; Type: FK CONSTRAINT; Schema: public; Owner: -
--

ALTER TABLE ONLY public.cart_positions
    ADD CONSTRAINT "FK_cart_positions_products_product_id" FOREIGN KEY (product_id) REFERENCES public.products(id) ON DELETE CASCADE;


--
-- Name: carts FK_carts_users_user_id; Type: FK CONSTRAINT; Schema: public; Owner: -
--

ALTER TABLE ONLY public.carts
    ADD CONSTRAINT "FK_carts_users_user_id" FOREIGN KEY (user_id) REFERENCES public.users(id) ON DELETE CASCADE;


--
-- Name: order_positions FK_order_positions_orders_order_id; Type: FK CONSTRAINT; Schema: public; Owner: -
--

ALTER TABLE ONLY public.order_positions
    ADD CONSTRAINT "FK_order_positions_orders_order_id" FOREIGN KEY (order_id) REFERENCES public.orders(id) ON DELETE CASCADE;


--
-- Name: order_positions FK_order_positions_products_product_id; Type: FK CONSTRAINT; Schema: public; Owner: -
--

ALTER TABLE ONLY public.order_positions
    ADD CONSTRAINT "FK_order_positions_products_product_id" FOREIGN KEY (product_id) REFERENCES public.products(id) ON DELETE CASCADE;


--
-- Name: orders FK_orders_pickup_points_pickup_point_id; Type: FK CONSTRAINT; Schema: public; Owner: -
--

ALTER TABLE ONLY public.orders
    ADD CONSTRAINT "FK_orders_pickup_points_pickup_point_id" FOREIGN KEY (pickup_point_id) REFERENCES public.pickup_points(id) ON DELETE CASCADE;


--
-- Name: orders FK_orders_users_user_id; Type: FK CONSTRAINT; Schema: public; Owner: -
--

ALTER TABLE ONLY public.orders
    ADD CONSTRAINT "FK_orders_users_user_id" FOREIGN KEY (user_id) REFERENCES public.users(id) ON DELETE CASCADE;


--
-- Name: product_images FK_product_images_products_product_id; Type: FK CONSTRAINT; Schema: public; Owner: -
--

ALTER TABLE ONLY public.product_images
    ADD CONSTRAINT "FK_product_images_products_product_id" FOREIGN KEY (product_id) REFERENCES public.products(id) ON DELETE CASCADE;


--
-- Name: products FK_products_categories_category_id; Type: FK CONSTRAINT; Schema: public; Owner: -
--

ALTER TABLE ONLY public.products
    ADD CONSTRAINT "FK_products_categories_category_id" FOREIGN KEY (category_id) REFERENCES public.categories(id) ON DELETE CASCADE;


--
-- Name: products FK_products_sellers_maker_id; Type: FK CONSTRAINT; Schema: public; Owner: -
--

ALTER TABLE ONLY public.products
    ADD CONSTRAINT "FK_products_sellers_maker_id" FOREIGN KEY (maker_id) REFERENCES public.sellers(id) ON DELETE CASCADE;


--
-- Name: refresh_token FK_refresh_token_users_user_id; Type: FK CONSTRAINT; Schema: public; Owner: -
--

ALTER TABLE ONLY public.refresh_token
    ADD CONSTRAINT "FK_refresh_token_users_user_id" FOREIGN KEY (user_id) REFERENCES public.users(id) ON DELETE CASCADE;


--
-- Name: reviews FK_reviews_products_product_id; Type: FK CONSTRAINT; Schema: public; Owner: -
--

ALTER TABLE ONLY public.reviews
    ADD CONSTRAINT "FK_reviews_products_product_id" FOREIGN KEY (product_id) REFERENCES public.products(id) ON DELETE CASCADE;


--
-- Name: reviews FK_reviews_users_user_id; Type: FK CONSTRAINT; Schema: public; Owner: -
--

ALTER TABLE ONLY public.reviews
    ADD CONSTRAINT "FK_reviews_users_user_id" FOREIGN KEY (user_id) REFERENCES public.users(id) ON DELETE CASCADE;


--
-- Name: user_pickup_points FK_user_pickup_points_pickup_points_pickup_point_id; Type: FK CONSTRAINT; Schema: public; Owner: -
--

ALTER TABLE ONLY public.user_pickup_points
    ADD CONSTRAINT "FK_user_pickup_points_pickup_points_pickup_point_id" FOREIGN KEY (pickup_point_id) REFERENCES public.pickup_points(id) ON DELETE CASCADE;


--
-- Name: user_pickup_points FK_user_pickup_points_users_user_id; Type: FK CONSTRAINT; Schema: public; Owner: -
--

ALTER TABLE ONLY public.user_pickup_points
    ADD CONSTRAINT "FK_user_pickup_points_users_user_id" FOREIGN KEY (user_id) REFERENCES public.users(id) ON DELETE CASCADE;


--
-- Name: wished_products FK_wished_products_products_product_id; Type: FK CONSTRAINT; Schema: public; Owner: -
--

ALTER TABLE ONLY public.wished_products
    ADD CONSTRAINT "FK_wished_products_products_product_id" FOREIGN KEY (product_id) REFERENCES public.products(id) ON DELETE CASCADE;


--
-- Name: wished_products FK_wished_products_wishlists_wishlist_id; Type: FK CONSTRAINT; Schema: public; Owner: -
--

ALTER TABLE ONLY public.wished_products
    ADD CONSTRAINT "FK_wished_products_wishlists_wishlist_id" FOREIGN KEY (wishlist_id) REFERENCES public.wishlists(id) ON DELETE CASCADE;


--
-- Name: wishlists FK_wishlists_users_user_id; Type: FK CONSTRAINT; Schema: public; Owner: -
--

ALTER TABLE ONLY public.wishlists
    ADD CONSTRAINT "FK_wishlists_users_user_id" FOREIGN KEY (user_id) REFERENCES public.users(id) ON DELETE CASCADE;


--
-- Name: SCHEMA public; Type: ACL; Schema: -; Owner: -
--

REVOKE USAGE ON SCHEMA public FROM PUBLIC;


--
-- PostgreSQL database dump complete
--

