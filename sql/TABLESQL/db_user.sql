create table if not exists yupi_db.user
(
id bigint not null auto_increment comment '主键' primary key,
username varchar(256) not null comment '用户名',
create_time datetime default CURRENT_TIMESTAMP not null comment '创建时间',
update_time datetime default CURRENT_TIMESTAMP not null on update CURRENT_TIMESTAMP comment '更新时间',
is_deleted tinyint default 0 not null comment '是否删除(0-未删 1-已删)'
) comment '用户表'