create table formations (
    id serial4 primary key,
    name varchar not null
);

create table formation_sections (
    id serial4 primary key,
    formation_id int4 not null references formations(id),
    section_type int2 not null default (0),
    regular_meeting_day int2 null,
    regular_meeting_time time null,
    friendly_code varchar null
);

create index formation_sections_formation_id_idx on formation_sections (formation_id);
create index formation_sections_friendly_code_id_idx on formation_sections (friendly_code);

insert into formations (name) values ('Unknown');

with unknown_formation as (
    select * from formations
)

insert into formation_sections (formation_id, section_type)
select uf.id, v.name
    from unknown_formation uf
    cross join ( values (0) ) as v(name);

alter table section_recorded_attendances add formation_section_id int4 not null default (1) references formation_sections(id);