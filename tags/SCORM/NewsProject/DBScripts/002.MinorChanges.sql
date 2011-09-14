alter table tblComment alter column UserRef int null

alter table relUserRole add constraint PK_relUserRole
	primary key (UserRef, RoleRef)