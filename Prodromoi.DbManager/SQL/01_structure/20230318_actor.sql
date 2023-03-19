create table public.actors (
    id serial4 not null,
    "name" varchar not null,
    constraint actors_pk primary key (id)
);

create table public.test (
    id serial4 not null,
    "name" varchar not null,
    constraint test_pk primary key (id)
);