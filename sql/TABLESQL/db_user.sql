create table if not exists yupi_db.user
(
id bigint not null auto_increment comment '����' primary key,
username varchar(256) not null comment '�û���',
create_time datetime default CURRENT_TIMESTAMP not null comment '����ʱ��',
update_time datetime default CURRENT_TIMESTAMP not null on update CURRENT_TIMESTAMP comment '����ʱ��',
is_deleted tinyint default 0 not null comment '�Ƿ�ɾ��(0-δɾ 1-��ɾ)'
) comment '�û���'