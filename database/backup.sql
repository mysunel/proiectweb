--
-- PostgreSQL database dump
--

\restrict 0io4OlJXcR0Ycs5A8xUsGPB7t7JGeSNFrvP8EYFuy8ktpZbRYITpHwuo3SzaSQC

-- Dumped from database version 18.0 (Debian 18.0-1.pgdg13+3)
-- Dumped by pg_dump version 18.0 (Debian 18.0-1.pgdg13+3)

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

SET default_tablespace = '';

SET default_table_access_method = heap;

--
-- Name: admin; Type: TABLE; Schema: public; Owner: admin
--

CREATE TABLE public.admin (
    idadmin character varying NOT NULL,
    username character varying NOT NULL,
    password character varying NOT NULL
);


ALTER TABLE public.admin OWNER TO admin;

--
-- Name: coach; Type: TABLE; Schema: public; Owner: admin
--

CREATE TABLE public.coach (
    idcoach integer NOT NULL,
    firstname character varying NOT NULL,
    lastname character varying NOT NULL,
    nationality character varying NOT NULL,
    birthdate date NOT NULL,
    CONSTRAINT coach_check CHECK ((birthdate < CURRENT_DATE))
);


ALTER TABLE public.coach OWNER TO admin;

--
-- Name: TABLE coach; Type: COMMENT; Schema: public; Owner: admin
--

COMMENT ON TABLE public.coach IS 'tabela antrenori';


--
-- Name: coach_idcoach_seq; Type: SEQUENCE; Schema: public; Owner: admin
--

CREATE SEQUENCE public.coach_idcoach_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public.coach_idcoach_seq OWNER TO admin;

--
-- Name: coach_idcoach_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: admin
--

ALTER SEQUENCE public.coach_idcoach_seq OWNED BY public.coach.idcoach;


--
-- Name: match; Type: TABLE; Schema: public; Owner: admin
--

CREATE TABLE public.match (
    idmatch integer NOT NULL,
    date date NOT NULL,
    scorehome integer,
    scoreguest integer,
    idhome integer NOT NULL,
    idguest integer NOT NULL
);


ALTER TABLE public.match OWNER TO admin;

--
-- Name: match_idmatch_seq; Type: SEQUENCE; Schema: public; Owner: admin
--

CREATE SEQUENCE public.match_idmatch_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public.match_idmatch_seq OWNER TO admin;

--
-- Name: match_idmatch_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: admin
--

ALTER SEQUENCE public.match_idmatch_seq OWNED BY public.match.idmatch;


--
-- Name: player; Type: TABLE; Schema: public; Owner: admin
--

CREATE TABLE public.player (
    idplayer integer NOT NULL,
    firstname character varying NOT NULL,
    lastname character varying NOT NULL,
    birthdate date NOT NULL,
    "position" character varying NOT NULL,
    height integer NOT NULL,
    idteam integer NOT NULL
);


ALTER TABLE public.player OWNER TO admin;

--
-- Name: player_idplayer_seq; Type: SEQUENCE; Schema: public; Owner: admin
--

CREATE SEQUENCE public.player_idplayer_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public.player_idplayer_seq OWNER TO admin;

--
-- Name: player_idplayer_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: admin
--

ALTER SEQUENCE public.player_idplayer_seq OWNED BY public.player.idplayer;


--
-- Name: player_match_stats; Type: TABLE; Schema: public; Owner: admin
--

CREATE TABLE public.player_match_stats (
    idpms integer NOT NULL,
    points integer,
    rebounds integer,
    assists integer,
    idplayer integer NOT NULL,
    idmatch integer NOT NULL
);


ALTER TABLE public.player_match_stats OWNER TO admin;

--
-- Name: player_match_stats_idpms_seq; Type: SEQUENCE; Schema: public; Owner: admin
--

CREATE SEQUENCE public.player_match_stats_idpms_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public.player_match_stats_idpms_seq OWNER TO admin;

--
-- Name: player_match_stats_idpms_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: admin
--

ALTER SEQUENCE public.player_match_stats_idpms_seq OWNED BY public.player_match_stats.idpms;


--
-- Name: team; Type: TABLE; Schema: public; Owner: admin
--

CREATE TABLE public.team (
    idteam integer NOT NULL,
    name character varying NOT NULL,
    city character varying,
    idcoach integer NOT NULL
);


ALTER TABLE public.team OWNER TO admin;

--
-- Name: team_idteam_seq; Type: SEQUENCE; Schema: public; Owner: admin
--

CREATE SEQUENCE public.team_idteam_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public.team_idteam_seq OWNER TO admin;

--
-- Name: team_idteam_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: admin
--

ALTER SEQUENCE public.team_idteam_seq OWNED BY public.team.idteam;


--
-- Name: coach idcoach; Type: DEFAULT; Schema: public; Owner: admin
--

ALTER TABLE ONLY public.coach ALTER COLUMN idcoach SET DEFAULT nextval('public.coach_idcoach_seq'::regclass);


--
-- Name: match idmatch; Type: DEFAULT; Schema: public; Owner: admin
--

ALTER TABLE ONLY public.match ALTER COLUMN idmatch SET DEFAULT nextval('public.match_idmatch_seq'::regclass);


--
-- Name: player idplayer; Type: DEFAULT; Schema: public; Owner: admin
--

ALTER TABLE ONLY public.player ALTER COLUMN idplayer SET DEFAULT nextval('public.player_idplayer_seq'::regclass);


--
-- Name: player_match_stats idpms; Type: DEFAULT; Schema: public; Owner: admin
--

ALTER TABLE ONLY public.player_match_stats ALTER COLUMN idpms SET DEFAULT nextval('public.player_match_stats_idpms_seq'::regclass);


--
-- Name: team idteam; Type: DEFAULT; Schema: public; Owner: admin
--

ALTER TABLE ONLY public.team ALTER COLUMN idteam SET DEFAULT nextval('public.team_idteam_seq'::regclass);


--
-- Data for Name: admin; Type: TABLE DATA; Schema: public; Owner: admin
--

COPY public.admin (idadmin, username, password) FROM stdin;
\.


--
-- Data for Name: coach; Type: TABLE DATA; Schema: public; Owner: admin
--

COPY public.coach (idcoach, firstname, lastname, nationality, birthdate) FROM stdin;
\.


--
-- Data for Name: match; Type: TABLE DATA; Schema: public; Owner: admin
--

COPY public.match (idmatch, date, scorehome, scoreguest, idhome, idguest) FROM stdin;
\.


--
-- Data for Name: player; Type: TABLE DATA; Schema: public; Owner: admin
--

COPY public.player (idplayer, firstname, lastname, birthdate, "position", height, idteam) FROM stdin;
\.


--
-- Data for Name: player_match_stats; Type: TABLE DATA; Schema: public; Owner: admin
--

COPY public.player_match_stats (idpms, points, rebounds, assists, idplayer, idmatch) FROM stdin;
\.


--
-- Data for Name: team; Type: TABLE DATA; Schema: public; Owner: admin
--

COPY public.team (idteam, name, city, idcoach) FROM stdin;
\.


--
-- Name: coach_idcoach_seq; Type: SEQUENCE SET; Schema: public; Owner: admin
--

SELECT pg_catalog.setval('public.coach_idcoach_seq', 1, false);


--
-- Name: match_idmatch_seq; Type: SEQUENCE SET; Schema: public; Owner: admin
--

SELECT pg_catalog.setval('public.match_idmatch_seq', 1, false);


--
-- Name: player_idplayer_seq; Type: SEQUENCE SET; Schema: public; Owner: admin
--

SELECT pg_catalog.setval('public.player_idplayer_seq', 1, false);


--
-- Name: player_match_stats_idpms_seq; Type: SEQUENCE SET; Schema: public; Owner: admin
--

SELECT pg_catalog.setval('public.player_match_stats_idpms_seq', 1, false);


--
-- Name: team_idteam_seq; Type: SEQUENCE SET; Schema: public; Owner: admin
--

SELECT pg_catalog.setval('public.team_idteam_seq', 1, false);


--
-- Name: admin admin_pk; Type: CONSTRAINT; Schema: public; Owner: admin
--

ALTER TABLE ONLY public.admin
    ADD CONSTRAINT admin_pk PRIMARY KEY (idadmin);


--
-- Name: admin admin_user_uk; Type: CONSTRAINT; Schema: public; Owner: admin
--

ALTER TABLE ONLY public.admin
    ADD CONSTRAINT admin_user_uk UNIQUE (username);


--
-- Name: coach coach_pk; Type: CONSTRAINT; Schema: public; Owner: admin
--

ALTER TABLE ONLY public.coach
    ADD CONSTRAINT coach_pk PRIMARY KEY (idcoach);


--
-- Name: coach coach_uk; Type: CONSTRAINT; Schema: public; Owner: admin
--

ALTER TABLE ONLY public.coach
    ADD CONSTRAINT coach_uk UNIQUE (firstname, lastname, birthdate);


--
-- Name: match match_pk; Type: CONSTRAINT; Schema: public; Owner: admin
--

ALTER TABLE ONLY public.match
    ADD CONSTRAINT match_pk PRIMARY KEY (idmatch);


--
-- Name: player_match_stats player_match_stats_pk; Type: CONSTRAINT; Schema: public; Owner: admin
--

ALTER TABLE ONLY public.player_match_stats
    ADD CONSTRAINT player_match_stats_pk PRIMARY KEY (idpms);


--
-- Name: player_match_stats player_match_stats_unique; Type: CONSTRAINT; Schema: public; Owner: admin
--

ALTER TABLE ONLY public.player_match_stats
    ADD CONSTRAINT player_match_stats_unique UNIQUE (idmatch, idplayer);


--
-- Name: player player_pkey; Type: CONSTRAINT; Schema: public; Owner: admin
--

ALTER TABLE ONLY public.player
    ADD CONSTRAINT player_pkey PRIMARY KEY (idplayer);


--
-- Name: team team_pk; Type: CONSTRAINT; Schema: public; Owner: admin
--

ALTER TABLE ONLY public.team
    ADD CONSTRAINT team_pk PRIMARY KEY (idteam);


--
-- Name: team team_unique; Type: CONSTRAINT; Schema: public; Owner: admin
--

ALTER TABLE ONLY public.team
    ADD CONSTRAINT team_unique UNIQUE (name);


--
-- Name: team team_unique_fk; Type: CONSTRAINT; Schema: public; Owner: admin
--

ALTER TABLE ONLY public.team
    ADD CONSTRAINT team_unique_fk UNIQUE (idcoach);


--
-- Name: match match_guest_fk; Type: FK CONSTRAINT; Schema: public; Owner: admin
--

ALTER TABLE ONLY public.match
    ADD CONSTRAINT match_guest_fk FOREIGN KEY (idguest) REFERENCES public.team(idteam);


--
-- Name: match match_home_fk; Type: FK CONSTRAINT; Schema: public; Owner: admin
--

ALTER TABLE ONLY public.match
    ADD CONSTRAINT match_home_fk FOREIGN KEY (idhome) REFERENCES public.team(idteam);


--
-- Name: player_match_stats player_match_stats_match_fk; Type: FK CONSTRAINT; Schema: public; Owner: admin
--

ALTER TABLE ONLY public.player_match_stats
    ADD CONSTRAINT player_match_stats_match_fk FOREIGN KEY (idmatch) REFERENCES public.match(idmatch) ON DELETE CASCADE;


--
-- Name: player_match_stats player_match_stats_player_fk; Type: FK CONSTRAINT; Schema: public; Owner: admin
--

ALTER TABLE ONLY public.player_match_stats
    ADD CONSTRAINT player_match_stats_player_fk FOREIGN KEY (idplayer) REFERENCES public.player(idplayer);


--
-- Name: player player_team_fk; Type: FK CONSTRAINT; Schema: public; Owner: admin
--

ALTER TABLE ONLY public.player
    ADD CONSTRAINT player_team_fk FOREIGN KEY (idteam) REFERENCES public.team(idteam) ON DELETE CASCADE;


--
-- Name: team team_coach_fk; Type: FK CONSTRAINT; Schema: public; Owner: admin
--

ALTER TABLE ONLY public.team
    ADD CONSTRAINT team_coach_fk FOREIGN KEY (idcoach) REFERENCES public.coach(idcoach) ON DELETE SET NULL;


--
-- PostgreSQL database dump complete
--

\unrestrict 0io4OlJXcR0Ycs5A8xUsGPB7t7JGeSNFrvP8EYFuy8ktpZbRYITpHwuo3SzaSQC

