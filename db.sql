create database application_tracker go
use application_tracker go

create table job
(
    id               uniqueidentifier default newid() not null
        primary key,
    url              varchar(255)                     not null,
    application_date datetime                         not null,
    role             varchar(255)                     not null,
    requirements     varchar(max)                     not null,
    benefits         varchar(max)                     not null,
    company          varchar(255),
    salary           money,
    response_status  bit,
    response_date    datetime,
    status           int              default 0       not null
        check ([status] = 5 OR [status] = 4 OR [status] = 3 OR [status] = 2 OR [status] = 1 OR [status] = 0),
    archived         bit              default 0       not null,
    deleted_at       datetime
)
go