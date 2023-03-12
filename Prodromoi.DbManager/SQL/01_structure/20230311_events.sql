create table public.events (
    id serial4 not null,
    "name" varchar not null,
    start date not null,
    "end" date null,
    constraint events_pk primary key (id)
);  