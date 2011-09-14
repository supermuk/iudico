﻿#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:2.0.50727.3053
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace IUDICO.DataModel.DB
{
	using System.Data.Linq;
	using System.Data.Linq.Mapping;
	using System.Data;
	using System.Collections.Generic;
	using System.Reflection;
	using System.Linq;
	using System.Linq.Expressions;
	using System.ComponentModel;
	using System;
	
	
	[System.Data.Linq.Mapping.DatabaseAttribute(Name="NEWS")]
	public partial class DatabaseModel : System.Data.Linq.DataContext
	{
		
		private static System.Data.Linq.Mapping.MappingSource mappingSource = new AttributeMappingSource();
		
    #region Extensibility Method Definitions
    partial void OnCreated();
    partial void InsertFxCategoryOperation(FxCategoryOperation instance);
    partial void UpdateFxCategoryOperation(FxCategoryOperation instance);
    partial void DeleteFxCategoryOperation(FxCategoryOperation instance);
    partial void InsertFxNewsOperation(FxNewsOperation instance);
    partial void UpdateFxNewsOperation(FxNewsOperation instance);
    partial void DeleteFxNewsOperation(FxNewsOperation instance);
    partial void InsertFxRole(FxRole instance);
    partial void UpdateFxRole(FxRole instance);
    partial void DeleteFxRole(FxRole instance);
    partial void InsertRelUserRole(RelUserRole instance);
    partial void UpdateRelUserRole(RelUserRole instance);
    partial void DeleteRelUserRole(RelUserRole instance);
    partial void InsertTblCategory(TblCategory instance);
    partial void UpdateTblCategory(TblCategory instance);
    partial void DeleteTblCategory(TblCategory instance);
    partial void InsertTblComment(TblComment instance);
    partial void UpdateTblComment(TblComment instance);
    partial void DeleteTblComment(TblComment instance);
    partial void InsertTblNews(TblNews instance);
    partial void UpdateTblNews(TblNews instance);
    partial void DeleteTblNews(TblNews instance);
    partial void InsertTblUser(TblUser instance);
    partial void UpdateTblUser(TblUser instance);
    partial void DeleteTblUser(TblUser instance);
    #endregion
		
		public DatabaseModel(string connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public DatabaseModel(System.Data.IDbConnection connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public DatabaseModel(string connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public DatabaseModel(System.Data.IDbConnection connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public System.Data.Linq.Table<FxCategoryOperation> FxCategoryOperation
		{
			get
			{
				return this.GetTable<FxCategoryOperation>();
			}
		}
		
		public System.Data.Linq.Table<FxNewsOperation> FxNewsOperation
		{
			get
			{
				return this.GetTable<FxNewsOperation>();
			}
		}
		
		public System.Data.Linq.Table<FxRole> FxRole
		{
			get
			{
				return this.GetTable<FxRole>();
			}
		}
		
		public System.Data.Linq.Table<RelUserRole> RelUserRole
		{
			get
			{
				return this.GetTable<RelUserRole>();
			}
		}
		
		public System.Data.Linq.Table<SysDBVersion> SysDBVersion
		{
			get
			{
				return this.GetTable<SysDBVersion>();
			}
		}
		
		public System.Data.Linq.Table<TblCategory> TblCategory
		{
			get
			{
				return this.GetTable<TblCategory>();
			}
		}
		
		public System.Data.Linq.Table<TblComment> TblComment
		{
			get
			{
				return this.GetTable<TblComment>();
			}
		}
		
		public System.Data.Linq.Table<TblNews> TblNews
		{
			get
			{
				return this.GetTable<TblNews>();
			}
		}
		
		public System.Data.Linq.Table<TblUser> TblUser
		{
			get
			{
				return this.GetTable<TblUser>();
			}
		}
		
		[Function(Name="dbo.GetDBVersion", IsComposable=true)]
		[return: Parameter(DbType="Int")]
		public System.Nullable<int> GetDBVersion()
		{
			return ((System.Nullable<int>)(this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod()))).ReturnValue));
		}
		
		[Function(Name="dbo.UpgradeDB")]
		[return: Parameter(DbType="Int")]
		public int UpgradeDB([Parameter(DbType="Int")] System.Nullable<int> version, [Parameter(DbType="NVarChar(MAX)")] string script)
		{
			IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), version, script);
			return ((int)(result.ReturnValue));
		}
	}
	
	[Table(Name="dbo.fxCategoryOperation")]
	public partial class FxCategoryOperation : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _ID;
		
		private string _Name;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnIDChanging(int value);
    partial void OnIDChanged();
    partial void OnNameChanging(string value);
    partial void OnNameChanged();
    #endregion
		
		public FxCategoryOperation()
		{
			OnCreated();
		}
		
		[Column(Storage="_ID", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public int ID
		{
			get
			{
				return this._ID;
			}
			set
			{
				if ((this._ID != value))
				{
					this.OnIDChanging(value);
					this.SendPropertyChanging();
					this._ID = value;
					this.SendPropertyChanged("ID");
					this.OnIDChanged();
				}
			}
		}
		
		[Column(Storage="_Name", DbType="NVarChar(32)")]
		public string Name
		{
			get
			{
				return this._Name;
			}
			set
			{
				if ((this._Name != value))
				{
					this.OnNameChanging(value);
					this.SendPropertyChanging();
					this._Name = value;
					this.SendPropertyChanged("Name");
					this.OnNameChanged();
				}
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
	
	[Table(Name="dbo.fxNewsOperation")]
	public partial class FxNewsOperation : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _ID;
		
		private string _Name;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnIDChanging(int value);
    partial void OnIDChanged();
    partial void OnNameChanging(string value);
    partial void OnNameChanged();
    #endregion
		
		public FxNewsOperation()
		{
			OnCreated();
		}
		
		[Column(Storage="_ID", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public int ID
		{
			get
			{
				return this._ID;
			}
			set
			{
				if ((this._ID != value))
				{
					this.OnIDChanging(value);
					this.SendPropertyChanging();
					this._ID = value;
					this.SendPropertyChanged("ID");
					this.OnIDChanged();
				}
			}
		}
		
		[Column(Storage="_Name", DbType="NVarChar(32)")]
		public string Name
		{
			get
			{
				return this._Name;
			}
			set
			{
				if ((this._Name != value))
				{
					this.OnNameChanging(value);
					this.SendPropertyChanging();
					this._Name = value;
					this.SendPropertyChanged("Name");
					this.OnNameChanged();
				}
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
	
	[Table(Name="dbo.fxRole")]
	public partial class FxRole : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _ID;
		
		private string _Name;
		
		private string _Description;
		
		private EntitySet<RelUserRole> _RelUserRole;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnIDChanging(int value);
    partial void OnIDChanged();
    partial void OnNameChanging(string value);
    partial void OnNameChanged();
    partial void OnDescriptionChanging(string value);
    partial void OnDescriptionChanged();
    #endregion
		
		public FxRole()
		{
			this._RelUserRole = new EntitySet<RelUserRole>(new Action<RelUserRole>(this.attach_RelUserRole), new Action<RelUserRole>(this.detach_RelUserRole));
			OnCreated();
		}
		
		[Column(Storage="_ID", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public int ID
		{
			get
			{
				return this._ID;
			}
			set
			{
				if ((this._ID != value))
				{
					this.OnIDChanging(value);
					this.SendPropertyChanging();
					this._ID = value;
					this.SendPropertyChanged("ID");
					this.OnIDChanged();
				}
			}
		}
		
		[Column(Storage="_Name", DbType="NVarChar(32) NOT NULL", CanBeNull=false)]
		public string Name
		{
			get
			{
				return this._Name;
			}
			set
			{
				if ((this._Name != value))
				{
					this.OnNameChanging(value);
					this.SendPropertyChanging();
					this._Name = value;
					this.SendPropertyChanged("Name");
					this.OnNameChanged();
				}
			}
		}
		
		[Column(Storage="_Description", DbType="NVarChar(MAX)", UpdateCheck=UpdateCheck.Never)]
		public string Description
		{
			get
			{
				return this._Description;
			}
			set
			{
				if ((this._Description != value))
				{
					this.OnDescriptionChanging(value);
					this.SendPropertyChanging();
					this._Description = value;
					this.SendPropertyChanged("Description");
					this.OnDescriptionChanged();
				}
			}
		}
		
		[Association(Name="relUserRole_RoleRef", Storage="_RelUserRole", OtherKey="RoleRef", DeleteRule="NO ACTION")]
		public EntitySet<RelUserRole> RelUserRole
		{
			get
			{
				return this._RelUserRole;
			}
			set
			{
				this._RelUserRole.Assign(value);
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
		
		private void attach_RelUserRole(RelUserRole entity)
		{
			this.SendPropertyChanging();
			entity.FxRole = this;
		}
		
		private void detach_RelUserRole(RelUserRole entity)
		{
			this.SendPropertyChanging();
			entity.FxRole = null;
		}
	}
	
	[Table(Name="dbo.relUserRole")]
	public partial class RelUserRole : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _UserRef;
		
		private int _RoleRef;
		
		private EntityRef<FxRole> _FxRole;
		
		private EntityRef<TblUser> _TblUser;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnUserRefChanging(int value);
    partial void OnUserRefChanged();
    partial void OnRoleRefChanging(int value);
    partial void OnRoleRefChanged();
    #endregion
		
		public RelUserRole()
		{
			this._FxRole = default(EntityRef<FxRole>);
			this._TblUser = default(EntityRef<TblUser>);
			OnCreated();
		}
		
		[Column(Storage="_UserRef", DbType="Int NOT NULL", IsPrimaryKey=true)]
		public int UserRef
		{
			get
			{
				return this._UserRef;
			}
			set
			{
				if ((this._UserRef != value))
				{
					if (this._TblUser.HasLoadedOrAssignedValue)
					{
						throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
					}
					this.OnUserRefChanging(value);
					this.SendPropertyChanging();
					this._UserRef = value;
					this.SendPropertyChanged("UserRef");
					this.OnUserRefChanged();
				}
			}
		}
		
		[Column(Storage="_RoleRef", DbType="Int NOT NULL", IsPrimaryKey=true)]
		public int RoleRef
		{
			get
			{
				return this._RoleRef;
			}
			set
			{
				if ((this._RoleRef != value))
				{
					if (this._FxRole.HasLoadedOrAssignedValue)
					{
						throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
					}
					this.OnRoleRefChanging(value);
					this.SendPropertyChanging();
					this._RoleRef = value;
					this.SendPropertyChanged("RoleRef");
					this.OnRoleRefChanged();
				}
			}
		}
		
		[Association(Name="relUserRole_RoleRef", Storage="_FxRole", ThisKey="RoleRef", IsForeignKey=true)]
		public FxRole FxRole
		{
			get
			{
				return this._FxRole.Entity;
			}
			set
			{
				FxRole previousValue = this._FxRole.Entity;
				if (((previousValue != value) 
							|| (this._FxRole.HasLoadedOrAssignedValue == false)))
				{
					this.SendPropertyChanging();
					if ((previousValue != null))
					{
						this._FxRole.Entity = null;
						previousValue.RelUserRole.Remove(this);
					}
					this._FxRole.Entity = value;
					if ((value != null))
					{
						value.RelUserRole.Add(this);
						this._RoleRef = value.ID;
					}
					else
					{
						this._RoleRef = default(int);
					}
					this.SendPropertyChanged("FxRole");
				}
			}
		}
		
		[Association(Name="relUserRole_UserRef", Storage="_TblUser", ThisKey="UserRef", IsForeignKey=true)]
		public TblUser TblUser
		{
			get
			{
				return this._TblUser.Entity;
			}
			set
			{
				TblUser previousValue = this._TblUser.Entity;
				if (((previousValue != value) 
							|| (this._TblUser.HasLoadedOrAssignedValue == false)))
				{
					this.SendPropertyChanging();
					if ((previousValue != null))
					{
						this._TblUser.Entity = null;
						previousValue.RelUserRole.Remove(this);
					}
					this._TblUser.Entity = value;
					if ((value != null))
					{
						value.RelUserRole.Add(this);
						this._UserRef = value.ID;
					}
					else
					{
						this._UserRef = default(int);
					}
					this.SendPropertyChanged("TblUser");
				}
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
	
	[Table(Name="dbo.sysDBVersion")]
	public partial class SysDBVersion
	{
		
		private int _VersionNumber;
		
		private string _ScriptFileName;
		
		private System.DateTime _Date;
		
		public SysDBVersion()
		{
		}
		
		[Column(Storage="_VersionNumber", DbType="Int NOT NULL")]
		public int VersionNumber
		{
			get
			{
				return this._VersionNumber;
			}
			set
			{
				if ((this._VersionNumber != value))
				{
					this._VersionNumber = value;
				}
			}
		}
		
		[Column(Storage="_ScriptFileName", DbType="NVarChar(255) NOT NULL", CanBeNull=false)]
		public string ScriptFileName
		{
			get
			{
				return this._ScriptFileName;
			}
			set
			{
				if ((this._ScriptFileName != value))
				{
					this._ScriptFileName = value;
				}
			}
		}
		
		[Column(Storage="_Date", DbType="DateTime NOT NULL")]
		public System.DateTime Date
		{
			get
			{
				return this._Date;
			}
			set
			{
				if ((this._Date != value))
				{
					this._Date = value;
				}
			}
		}
	}
	
	[Table(Name="dbo.tblCategory")]
	public partial class TblCategory : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _ID;
		
		private string _Name;
		
		private string _Description;
		
		private EntitySet<TblNews> _TblNews;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnIDChanging(int value);
    partial void OnIDChanged();
    partial void OnNameChanging(string value);
    partial void OnNameChanged();
    partial void OnDescriptionChanging(string value);
    partial void OnDescriptionChanged();
    #endregion
		
		public TblCategory()
		{
			this._TblNews = new EntitySet<TblNews>(new Action<TblNews>(this.attach_TblNews), new Action<TblNews>(this.detach_TblNews));
			OnCreated();
		}
		
		[Column(Storage="_ID", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public int ID
		{
			get
			{
				return this._ID;
			}
			set
			{
				if ((this._ID != value))
				{
					this.OnIDChanging(value);
					this.SendPropertyChanging();
					this._ID = value;
					this.SendPropertyChanged("ID");
					this.OnIDChanged();
				}
			}
		}
		
		[Column(Storage="_Name", DbType="NVarChar(50) NOT NULL", CanBeNull=false)]
		public string Name
		{
			get
			{
				return this._Name;
			}
			set
			{
				if ((this._Name != value))
				{
					this.OnNameChanging(value);
					this.SendPropertyChanging();
					this._Name = value;
					this.SendPropertyChanged("Name");
					this.OnNameChanged();
				}
			}
		}
		
		[Column(Storage="_Description", DbType="NVarChar(MAX)", UpdateCheck=UpdateCheck.Never)]
		public string Description
		{
			get
			{
				return this._Description;
			}
			set
			{
				if ((this._Description != value))
				{
					this.OnDescriptionChanging(value);
					this.SendPropertyChanging();
					this._Description = value;
					this.SendPropertyChanged("Description");
					this.OnDescriptionChanged();
				}
			}
		}
		
		[Association(Name="tlbNews_CategoryRef", Storage="_TblNews", OtherKey="CategoryRef", DeleteRule="NO ACTION")]
		public EntitySet<TblNews> TblNews
		{
			get
			{
				return this._TblNews;
			}
			set
			{
				this._TblNews.Assign(value);
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
		
		private void attach_TblNews(TblNews entity)
		{
			this.SendPropertyChanging();
			entity.TblCategory = this;
		}
		
		private void detach_TblNews(TblNews entity)
		{
			this.SendPropertyChanging();
			entity.TblCategory = null;
		}
	}
	
	[Table(Name="dbo.tblComment")]
	public partial class TblComment : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _ID;
		
		private System.Nullable<int> _ParentCommentRef;
		
		private System.Nullable<int> _UserRef;
		
		private int _NewsRef;
		
		private string _Content;
		
		private EntityRef<TblNews> _TblNews;
		
		private EntityRef<TblComment> _ParentCommentRefTblComment;
		
		private EntitySet<TblComment> _TblComment_ParentCommentRef;
		
		private EntityRef<TblUser> _TblUser;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnIDChanging(int value);
    partial void OnIDChanged();
    partial void OnParentCommentRefChanging(System.Nullable<int> value);
    partial void OnParentCommentRefChanged();
    partial void OnUserRefChanging(System.Nullable<int> value);
    partial void OnUserRefChanged();
    partial void OnNewsRefChanging(int value);
    partial void OnNewsRefChanged();
    partial void OnContentChanging(string value);
    partial void OnContentChanged();
    #endregion
		
		public TblComment()
		{
			this._TblNews = default(EntityRef<TblNews>);
			this._ParentCommentRefTblComment = default(EntityRef<TblComment>);
			this._TblComment_ParentCommentRef = new EntitySet<TblComment>(new Action<TblComment>(this.attach_TblComment_ParentCommentRef), new Action<TblComment>(this.detach_TblComment_ParentCommentRef));
			this._TblUser = default(EntityRef<TblUser>);
			OnCreated();
		}
		
		[Column(Storage="_ID", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public int ID
		{
			get
			{
				return this._ID;
			}
			set
			{
				if ((this._ID != value))
				{
					this.OnIDChanging(value);
					this.SendPropertyChanging();
					this._ID = value;
					this.SendPropertyChanged("ID");
					this.OnIDChanged();
				}
			}
		}
		
		[Column(Storage="_ParentCommentRef", DbType="Int")]
		public System.Nullable<int> ParentCommentRef
		{
			get
			{
				return this._ParentCommentRef;
			}
			set
			{
				if ((this._ParentCommentRef != value))
				{
					if (this._ParentCommentRefTblComment.HasLoadedOrAssignedValue)
					{
						throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
					}
					this.OnParentCommentRefChanging(value);
					this.SendPropertyChanging();
					this._ParentCommentRef = value;
					this.SendPropertyChanged("ParentCommentRef");
					this.OnParentCommentRefChanged();
				}
			}
		}
		
		[Column(Storage="_UserRef", DbType="Int")]
		public System.Nullable<int> UserRef
		{
			get
			{
				return this._UserRef;
			}
			set
			{
				if ((this._UserRef != value))
				{
					if (this._TblUser.HasLoadedOrAssignedValue)
					{
						throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
					}
					this.OnUserRefChanging(value);
					this.SendPropertyChanging();
					this._UserRef = value;
					this.SendPropertyChanged("UserRef");
					this.OnUserRefChanged();
				}
			}
		}
		
		[Column(Storage="_NewsRef", DbType="Int NOT NULL")]
		public int NewsRef
		{
			get
			{
				return this._NewsRef;
			}
			set
			{
				if ((this._NewsRef != value))
				{
					if (this._TblNews.HasLoadedOrAssignedValue)
					{
						throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
					}
					this.OnNewsRefChanging(value);
					this.SendPropertyChanging();
					this._NewsRef = value;
					this.SendPropertyChanged("NewsRef");
					this.OnNewsRefChanged();
				}
			}
		}
		
		[Column(Storage="_Content", DbType="NVarChar(MAX)", UpdateCheck=UpdateCheck.Never)]
		public string Content
		{
			get
			{
				return this._Content;
			}
			set
			{
				if ((this._Content != value))
				{
					this.OnContentChanging(value);
					this.SendPropertyChanging();
					this._Content = value;
					this.SendPropertyChanged("Content");
					this.OnContentChanged();
				}
			}
		}
		
		[Association(Name="tblComment_NewsRef", Storage="_TblNews", ThisKey="NewsRef", IsForeignKey=true)]
		public TblNews TblNews
		{
			get
			{
				return this._TblNews.Entity;
			}
			set
			{
				TblNews previousValue = this._TblNews.Entity;
				if (((previousValue != value) 
							|| (this._TblNews.HasLoadedOrAssignedValue == false)))
				{
					this.SendPropertyChanging();
					if ((previousValue != null))
					{
						this._TblNews.Entity = null;
						previousValue.TblComment.Remove(this);
					}
					this._TblNews.Entity = value;
					if ((value != null))
					{
						value.TblComment.Add(this);
						this._NewsRef = value.ID;
					}
					else
					{
						this._NewsRef = default(int);
					}
					this.SendPropertyChanged("TblNews");
				}
			}
		}
		
		[Association(Name="tblComment_ParentCommentRef", Storage="_ParentCommentRefTblComment", ThisKey="ParentCommentRef", IsForeignKey=true)]
		public TblComment ParentCommentRefTblComment
		{
			get
			{
				return this._ParentCommentRefTblComment.Entity;
			}
			set
			{
				TblComment previousValue = this._ParentCommentRefTblComment.Entity;
				if (((previousValue != value) 
							|| (this._ParentCommentRefTblComment.HasLoadedOrAssignedValue == false)))
				{
					this.SendPropertyChanging();
					if ((previousValue != null))
					{
						this._ParentCommentRefTblComment.Entity = null;
						previousValue.TblComment_ParentCommentRef.Remove(this);
					}
					this._ParentCommentRefTblComment.Entity = value;
					if ((value != null))
					{
						value.TblComment_ParentCommentRef.Add(this);
						this._ParentCommentRef = value.ID;
					}
					else
					{
						this._ParentCommentRef = default(Nullable<int>);
					}
					this.SendPropertyChanged("ParentCommentRefTblComment");
				}
			}
		}
		
		[Association(Name="tblComment_ParentCommentRef", Storage="_TblComment_ParentCommentRef", OtherKey="ParentCommentRef", DeleteRule="NO ACTION")]
		public EntitySet<TblComment> TblComment_ParentCommentRef
		{
			get
			{
				return this._TblComment_ParentCommentRef;
			}
			set
			{
				this._TblComment_ParentCommentRef.Assign(value);
			}
		}
		
		[Association(Name="tblComment_UserRef", Storage="_TblUser", ThisKey="UserRef", IsForeignKey=true)]
		public TblUser TblUser
		{
			get
			{
				return this._TblUser.Entity;
			}
			set
			{
				TblUser previousValue = this._TblUser.Entity;
				if (((previousValue != value) 
							|| (this._TblUser.HasLoadedOrAssignedValue == false)))
				{
					this.SendPropertyChanging();
					if ((previousValue != null))
					{
						this._TblUser.Entity = null;
						previousValue.TblComment.Remove(this);
					}
					this._TblUser.Entity = value;
					if ((value != null))
					{
						value.TblComment.Add(this);
						this._UserRef = value.ID;
					}
					else
					{
						this._UserRef = default(Nullable<int>);
					}
					this.SendPropertyChanged("TblUser");
				}
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
		
		private void attach_TblComment_ParentCommentRef(TblComment entity)
		{
			this.SendPropertyChanging();
			entity.ParentCommentRefTblComment = this;
		}
		
		private void detach_TblComment_ParentCommentRef(TblComment entity)
		{
			this.SendPropertyChanging();
			entity.ParentCommentRefTblComment = null;
		}
	}
	
	[Table(Name="dbo.tblNews")]
	public partial class TblNews : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _ID;
		
		private int _CategoryRef;
		
		private string _Title;
		
		private string _Contents;
		
		private System.Data.Linq.Binary _Files;
		
		private EntitySet<TblComment> _TblComment;
		
		private EntityRef<TblCategory> _TblCategory;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnIDChanging(int value);
    partial void OnIDChanged();
    partial void OnCategoryRefChanging(int value);
    partial void OnCategoryRefChanged();
    partial void OnTitleChanging(string value);
    partial void OnTitleChanged();
    partial void OnContentsChanging(string value);
    partial void OnContentsChanged();
    partial void OnFilesChanging(System.Data.Linq.Binary value);
    partial void OnFilesChanged();
    #endregion
		
		public TblNews()
		{
			this._TblComment = new EntitySet<TblComment>(new Action<TblComment>(this.attach_TblComment), new Action<TblComment>(this.detach_TblComment));
			this._TblCategory = default(EntityRef<TblCategory>);
			OnCreated();
		}
		
		[Column(Storage="_ID", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public int ID
		{
			get
			{
				return this._ID;
			}
			set
			{
				if ((this._ID != value))
				{
					this.OnIDChanging(value);
					this.SendPropertyChanging();
					this._ID = value;
					this.SendPropertyChanged("ID");
					this.OnIDChanged();
				}
			}
		}
		
		[Column(Storage="_CategoryRef", DbType="Int NOT NULL")]
		public int CategoryRef
		{
			get
			{
				return this._CategoryRef;
			}
			set
			{
				if ((this._CategoryRef != value))
				{
					if (this._TblCategory.HasLoadedOrAssignedValue)
					{
						throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
					}
					this.OnCategoryRefChanging(value);
					this.SendPropertyChanging();
					this._CategoryRef = value;
					this.SendPropertyChanged("CategoryRef");
					this.OnCategoryRefChanged();
				}
			}
		}
		
		[Column(Storage="_Title", DbType="NVarChar(MAX) NOT NULL", CanBeNull=false, UpdateCheck=UpdateCheck.Never)]
		public string Title
		{
			get
			{
				return this._Title;
			}
			set
			{
				if ((this._Title != value))
				{
					this.OnTitleChanging(value);
					this.SendPropertyChanging();
					this._Title = value;
					this.SendPropertyChanged("Title");
					this.OnTitleChanged();
				}
			}
		}
		
		[Column(Storage="_Contents", DbType="NVarChar(MAX) NOT NULL", CanBeNull=false, UpdateCheck=UpdateCheck.Never)]
		public string Contents
		{
			get
			{
				return this._Contents;
			}
			set
			{
				if ((this._Contents != value))
				{
					this.OnContentsChanging(value);
					this.SendPropertyChanging();
					this._Contents = value;
					this.SendPropertyChanged("Contents");
					this.OnContentsChanged();
				}
			}
		}
		
		[Column(Storage="_Files", DbType="Binary(1)", CanBeNull=true)]
		public System.Data.Linq.Binary Files
		{
			get
			{
				return this._Files;
			}
			set
			{
				if ((this._Files != value))
				{
					this.OnFilesChanging(value);
					this.SendPropertyChanging();
					this._Files = value;
					this.SendPropertyChanged("Files");
					this.OnFilesChanged();
				}
			}
		}
		
		[Association(Name="tblComment_NewsRef", Storage="_TblComment", OtherKey="NewsRef", DeleteRule="NO ACTION")]
		public EntitySet<TblComment> TblComment
		{
			get
			{
				return this._TblComment;
			}
			set
			{
				this._TblComment.Assign(value);
			}
		}
		
		[Association(Name="tlbNews_CategoryRef", Storage="_TblCategory", ThisKey="CategoryRef", IsForeignKey=true)]
		public TblCategory TblCategory
		{
			get
			{
				return this._TblCategory.Entity;
			}
			set
			{
				TblCategory previousValue = this._TblCategory.Entity;
				if (((previousValue != value) 
							|| (this._TblCategory.HasLoadedOrAssignedValue == false)))
				{
					this.SendPropertyChanging();
					if ((previousValue != null))
					{
						this._TblCategory.Entity = null;
						previousValue.TblNews.Remove(this);
					}
					this._TblCategory.Entity = value;
					if ((value != null))
					{
						value.TblNews.Add(this);
						this._CategoryRef = value.ID;
					}
					else
					{
						this._CategoryRef = default(int);
					}
					this.SendPropertyChanged("TblCategory");
				}
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
		
		private void attach_TblComment(TblComment entity)
		{
			this.SendPropertyChanging();
			entity.TblNews = this;
		}
		
		private void detach_TblComment(TblComment entity)
		{
			this.SendPropertyChanging();
			entity.TblNews = null;
		}
	}
	
	[Table(Name="dbo.tblUser")]
	public partial class TblUser : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _ID;
		
		private string _FirstName;
		
		private string _SecondName;
		
		private string _Email;
		
		private string _PasswordHash;
		
		private EntitySet<RelUserRole> _RelUserRole;
		
		private EntitySet<TblComment> _TblComment;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnIDChanging(int value);
    partial void OnIDChanged();
    partial void OnFirstNameChanging(string value);
    partial void OnFirstNameChanged();
    partial void OnSecondNameChanging(string value);
    partial void OnSecondNameChanged();
    partial void OnEmailChanging(string value);
    partial void OnEmailChanged();
    partial void OnPasswordHashChanging(string value);
    partial void OnPasswordHashChanged();
    #endregion
		
		public TblUser()
		{
			this._RelUserRole = new EntitySet<RelUserRole>(new Action<RelUserRole>(this.attach_RelUserRole), new Action<RelUserRole>(this.detach_RelUserRole));
			this._TblComment = new EntitySet<TblComment>(new Action<TblComment>(this.attach_TblComment), new Action<TblComment>(this.detach_TblComment));
			OnCreated();
		}
		
		[Column(Storage="_ID", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public int ID
		{
			get
			{
				return this._ID;
			}
			set
			{
				if ((this._ID != value))
				{
					this.OnIDChanging(value);
					this.SendPropertyChanging();
					this._ID = value;
					this.SendPropertyChanged("ID");
					this.OnIDChanged();
				}
			}
		}
		
		[Column(Storage="_FirstName", DbType="NVarChar(32) NOT NULL", CanBeNull=false)]
		public string FirstName
		{
			get
			{
				return this._FirstName;
			}
			set
			{
				if ((this._FirstName != value))
				{
					this.OnFirstNameChanging(value);
					this.SendPropertyChanging();
					this._FirstName = value;
					this.SendPropertyChanged("FirstName");
					this.OnFirstNameChanged();
				}
			}
		}
		
		[Column(Storage="_SecondName", DbType="NVarChar(32) NOT NULL", CanBeNull=false)]
		public string SecondName
		{
			get
			{
				return this._SecondName;
			}
			set
			{
				if ((this._SecondName != value))
				{
					this.OnSecondNameChanging(value);
					this.SendPropertyChanging();
					this._SecondName = value;
					this.SendPropertyChanged("SecondName");
					this.OnSecondNameChanged();
				}
			}
		}
		
		[Column(Storage="_Email", DbType="NVarChar(32) NOT NULL", CanBeNull=false)]
		public string Email
		{
			get
			{
				return this._Email;
			}
			set
			{
				if ((this._Email != value))
				{
					this.OnEmailChanging(value);
					this.SendPropertyChanging();
					this._Email = value;
					this.SendPropertyChanged("Email");
					this.OnEmailChanged();
				}
			}
		}
		
		[Column(Storage="_PasswordHash", DbType="Char(32) NOT NULL", CanBeNull=false)]
		public string PasswordHash
		{
			get
			{
				return this._PasswordHash;
			}
			set
			{
				if ((this._PasswordHash != value))
				{
					this.OnPasswordHashChanging(value);
					this.SendPropertyChanging();
					this._PasswordHash = value;
					this.SendPropertyChanged("PasswordHash");
					this.OnPasswordHashChanged();
				}
			}
		}
		
		[Association(Name="relUserRole_UserRef", Storage="_RelUserRole", OtherKey="UserRef", DeleteRule="NO ACTION")]
		public EntitySet<RelUserRole> RelUserRole
		{
			get
			{
				return this._RelUserRole;
			}
			set
			{
				this._RelUserRole.Assign(value);
			}
		}
		
		[Association(Name="tblComment_UserRef", Storage="_TblComment", OtherKey="UserRef", DeleteRule="NO ACTION")]
		public EntitySet<TblComment> TblComment
		{
			get
			{
				return this._TblComment;
			}
			set
			{
				this._TblComment.Assign(value);
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
		
		private void attach_RelUserRole(RelUserRole entity)
		{
			this.SendPropertyChanging();
			entity.TblUser = this;
		}
		
		private void detach_RelUserRole(RelUserRole entity)
		{
			this.SendPropertyChanging();
			entity.TblUser = null;
		}
		
		private void attach_TblComment(TblComment entity)
		{
			this.SendPropertyChanging();
			entity.TblUser = this;
		}
		
		private void detach_TblComment(TblComment entity)
		{
			this.SendPropertyChanging();
			entity.TblUser = null;
		}
	}
}
#pragma warning restore 1591
