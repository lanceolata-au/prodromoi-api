create table members (
    id serial4 primary key,
    name varchar not null,
    registration_number int4 null,
    date_of_birth date not null,
    member_type int2 not null,
    phone_number varchar null,
    email varchar null
);

create table section_recorded_attendances (
    id serial8 primary key,
    recording_adult_id int not null references members(id),
    recorded timestamp with time zone not null
);

create table recorded_attendances (
    id serial8 primary key,
    section_attendance_id int not null references section_recorded_attendances(id),
    member_id int not null references members(id)
);
