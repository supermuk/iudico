﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Linq;
using System.Linq;
using System.Text;
using System.Data.Linq.Mapping;

namespace IUDICO.Common.Models.Shared
{
    [global::System.Data.Linq.Mapping.TableAttribute(Name = "dbo.Computers")]
    public partial class Computer : INotifyPropertyChanging, INotifyPropertyChanged
    {

        private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);

        private string _IpAddress;

        private bool _Banned;

        private string _CurrentUser;

        private System.Nullable<int> _RoomRef;

        private EntityRef<Room> _Room;

        #region Extensibility Method Definitions
        partial void OnLoaded();
        partial void OnValidate(System.Data.Linq.ChangeAction action);
        partial void OnCreated();
        partial void OnIpAddressChanging(string value);
        partial void OnIpAddressChanged();
        partial void OnBannedChanging(bool value);
        partial void OnBannedChanged();
        partial void OnCurrentUserChanging(string value);
        partial void OnCurrentUserChanged();
        partial void OnRoomRefChanging(System.Nullable<int> value);
        partial void OnRoomRefChanged();
        #endregion

        public Computer()
        {
            this._Room = default(EntityRef<Room>);
            OnCreated();
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_IpAddress", DbType = "NVarChar(20) NOT NULL", CanBeNull = false, IsPrimaryKey = true)]
        public string IpAddress
        {
            get
            {
                return this._IpAddress;
            }
            set
            {
                if ((this._IpAddress != value))
                {
                    this.OnIpAddressChanging(value);
                    this.SendPropertyChanging();
                    this._IpAddress = value;
                    this.SendPropertyChanged("IpAddress");
                    this.OnIpAddressChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Banned", DbType = "Bit NOT NULL")]
        public bool Banned
        {
            get
            {
                return this._Banned;
            }
            set
            {
                if ((this._Banned != value))
                {
                    this.OnBannedChanging(value);
                    this.SendPropertyChanging();
                    this._Banned = value;
                    this.SendPropertyChanged("Banned");
                    this.OnBannedChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_CurrentUser", DbType = "NVarChar(100)")]
        public string CurrentUser
        {
            get
            {
                return this._CurrentUser;
            }
            set
            {
                if ((this._CurrentUser != value))
                {
                    this.OnCurrentUserChanging(value);
                    this.SendPropertyChanging();
                    this._CurrentUser = value;
                    this.SendPropertyChanged("CurrentUser");
                    this.OnCurrentUserChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_RoomRef", DbType = "Int")]
        public System.Nullable<int> RoomRef
        {
            get
            {
                return this._RoomRef;
            }
            set
            {
                if ((this._RoomRef != value))
                {
                    if (this._Room.HasLoadedOrAssignedValue)
                    {
                        throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
                    }
                    this.OnRoomRefChanging(value);
                    this.SendPropertyChanging();
                    this._RoomRef = value;
                    this.SendPropertyChanged("RoomRef");
                    this.OnRoomRefChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.AssociationAttribute(Name = "Room_Computer", Storage = "_Room", ThisKey = "RoomRef", OtherKey = "Id", IsForeignKey = true)]
        public Room Room
        {
            get
            {
                return this._Room.Entity;
            }
            set
            {
                Room previousValue = this._Room.Entity;
                if (((previousValue != value)
                            || (this._Room.HasLoadedOrAssignedValue == false)))
                {
                    this.SendPropertyChanging();
                    if ((previousValue != null))
                    {
                        this._Room.Entity = null;
                        previousValue.Computers.Remove(this);
                    }
                    this._Room.Entity = value;
                    if ((value != null))
                    {
                        value.Computers.Add(this);
                        this._RoomRef = value.Id;
                    }
                    else
                    {
                        this._RoomRef = default(Nullable<int>);
                    }
                    this.SendPropertyChanged("Room");
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

    [global::System.Data.Linq.Mapping.TableAttribute(Name = "dbo.UserRoles")]
    public partial class UserRole : INotifyPropertyChanging, INotifyPropertyChanged
    {

        private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);

        private System.Guid _UserRef;

        private int _RoleRef;

        private EntityRef<User> _User;

        #region Extensibility Method Definitions
        partial void OnLoaded();
        partial void OnValidate(System.Data.Linq.ChangeAction action);
        partial void OnCreated();
        partial void OnUserRefChanging(System.Guid value);
        partial void OnUserRefChanged();
        partial void OnRoleRefChanging(int value);
        partial void OnRoleRefChanged();
        #endregion

        public UserRole()
        {
            this._User = default(EntityRef<User>);
            OnCreated();
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_UserRef", DbType = "UniqueIdentifier NOT NULL", IsPrimaryKey = true)]
        public System.Guid UserRef
        {
            get
            {
                return this._UserRef;
            }
            set
            {
                if ((this._UserRef != value))
                {
                    if (this._User.HasLoadedOrAssignedValue)
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

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_RoleRef", DbType = "Int NOT NULL", IsPrimaryKey = true)]
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
                    this.OnRoleRefChanging(value);
                    this.SendPropertyChanging();
                    this._RoleRef = value;
                    this.SendPropertyChanged("RoleRef");
                    this.OnRoleRefChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.AssociationAttribute(Name = "User_UserRole", Storage = "_User", ThisKey = "UserRef", OtherKey = "Id", IsForeignKey = true)]
        public User User
        {
            get
            {
                return this._User.Entity;
            }
            set
            {
                User previousValue = this._User.Entity;
                if (((previousValue != value)
                            || (this._User.HasLoadedOrAssignedValue == false)))
                {
                    this.SendPropertyChanging();
                    if ((previousValue != null))
                    {
                        this._User.Entity = null;
                        previousValue.UserRoles.Remove(this);
                    }
                    this._User.Entity = value;
                    if ((value != null))
                    {
                        value.UserRoles.Add(this);
                        this._UserRef = value.Id;
                    }
                    else
                    {
                        this._UserRef = default(System.Guid);
                    }
                    this.SendPropertyChanged("User");
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

    [global::System.Data.Linq.Mapping.TableAttribute(Name = "dbo.Courses")]
    public partial class Course : INotifyPropertyChanging, INotifyPropertyChanged
    {

        private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);

        private int _Id;

        private string _Name;

        private string _Owner;

        private System.DateTime _Created;

        private System.DateTime _Updated;

        private bool _Deleted;

        private System.Nullable<bool> _Locked;

        private System.Xml.Linq.XElement _Sequencing;

        private EntitySet<CourseUser> _CourseUsers;

        private EntitySet<Node> _Nodes;

        private EntitySet<StudyResult> _StudyResults;

        #region Extensibility Method Definitions
        partial void OnLoaded();
        partial void OnValidate(System.Data.Linq.ChangeAction action);
        partial void OnCreated();
        partial void OnIdChanging(int value);
        partial void OnIdChanged();
        partial void OnNameChanging(string value);
        partial void OnNameChanged();
        partial void OnOwnerChanging(string value);
        partial void OnOwnerChanged();
        partial void OnCreatedChanging(System.DateTime value);
        partial void OnCreatedChanged();
        partial void OnUpdatedChanging(System.DateTime value);
        partial void OnUpdatedChanged();
        partial void OnDeletedChanging(bool value);
        partial void OnDeletedChanged();
        partial void OnLockedChanging(System.Nullable<bool> value);
        partial void OnLockedChanged();
        partial void OnSequencingChanging(System.Xml.Linq.XElement value);
        partial void OnSequencingChanged();
        #endregion

        public Course()
        {
            this._CourseUsers = new EntitySet<CourseUser>(new Action<CourseUser>(this.attach_CourseUsers), new Action<CourseUser>(this.detach_CourseUsers));
            this._Nodes = new EntitySet<Node>(new Action<Node>(this.attach_Nodes), new Action<Node>(this.detach_Nodes));
            this._StudyResults = new EntitySet<StudyResult>(new Action<StudyResult>(this.attach_StudyResults), new Action<StudyResult>(this.detach_StudyResults));
            OnCreated();
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Id", AutoSync = AutoSync.OnInsert, DbType = "Int NOT NULL IDENTITY", IsPrimaryKey = true, IsDbGenerated = true)]
        public int Id
        {
            get
            {
                return this._Id;
            }
            set
            {
                if ((this._Id != value))
                {
                    this.OnIdChanging(value);
                    this.SendPropertyChanging();
                    this._Id = value;
                    this.SendPropertyChanged("Id");
                    this.OnIdChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Name", DbType = "NVarChar(50) NOT NULL", CanBeNull = false)]
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

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Owner", DbType = "NVarChar(50)")]
        public string Owner
        {
            get
            {
                return this._Owner;
            }
            set
            {
                if ((this._Owner != value))
                {
                    this.OnOwnerChanging(value);
                    this.SendPropertyChanging();
                    this._Owner = value;
                    this.SendPropertyChanged("Owner");
                    this.OnOwnerChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Created", DbType = "DateTime NOT NULL")]
        public System.DateTime Created
        {
            get
            {
                return this._Created;
            }
            set
            {
                if ((this._Created != value))
                {
                    this.OnCreatedChanging(value);
                    this.SendPropertyChanging();
                    this._Created = value;
                    this.SendPropertyChanged("Created");
                    this.OnCreatedChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Updated", DbType = "DateTime NOT NULL")]
        public System.DateTime Updated
        {
            get
            {
                return this._Updated;
            }
            set
            {
                if ((this._Updated != value))
                {
                    this.OnUpdatedChanging(value);
                    this.SendPropertyChanging();
                    this._Updated = value;
                    this.SendPropertyChanged("Updated");
                    this.OnUpdatedChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Deleted", DbType = "Bit NOT NULL")]
        public bool Deleted
        {
            get
            {
                return this._Deleted;
            }
            set
            {
                if ((this._Deleted != value))
                {
                    this.OnDeletedChanging(value);
                    this.SendPropertyChanging();
                    this._Deleted = value;
                    this.SendPropertyChanged("Deleted");
                    this.OnDeletedChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Locked", DbType = "Bit")]
        public System.Nullable<bool> Locked
        {
            get
            {
                return this._Locked;
            }
            set
            {
                if ((this._Locked != value))
                {
                    this.OnLockedChanging(value);
                    this.SendPropertyChanging();
                    this._Locked = value;
                    this.SendPropertyChanged("Locked");
                    this.OnLockedChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Sequencing", DbType = "Xml", UpdateCheck = UpdateCheck.Never)]
        public System.Xml.Linq.XElement Sequencing
        {
            get
            {
                return this._Sequencing;
            }
            set
            {
                if ((this._Sequencing != value))
                {
                    this.OnSequencingChanging(value);
                    this.SendPropertyChanging();
                    this._Sequencing = value;
                    this.SendPropertyChanged("Sequencing");
                    this.OnSequencingChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.AssociationAttribute(Name = "Course_CourseUser", Storage = "_CourseUsers", ThisKey = "Id", OtherKey = "CourseRef")]
        public EntitySet<CourseUser> CourseUsers
        {
            get
            {
                return this._CourseUsers;
            }
            set
            {
                this._CourseUsers.Assign(value);
            }
        }

        [global::System.Data.Linq.Mapping.AssociationAttribute(Name = "Course_Node", Storage = "_Nodes", ThisKey = "Id", OtherKey = "CourseId")]
        public EntitySet<Node> Nodes
        {
            get
            {
                return this._Nodes;
            }
            set
            {
                this._Nodes.Assign(value);
            }
        }

        [global::System.Data.Linq.Mapping.AssociationAttribute(Name = "Course_StudyResult", Storage = "_StudyResults", ThisKey = "Id", OtherKey = "CourseRef")]
        public EntitySet<StudyResult> StudyResults
        {
            get
            {
                return this._StudyResults;
            }
            set
            {
                this._StudyResults.Assign(value);
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

        private void attach_CourseUsers(CourseUser entity)
        {
            this.SendPropertyChanging();
            entity.Course = this;
        }

        private void detach_CourseUsers(CourseUser entity)
        {
            this.SendPropertyChanging();
            entity.Course = null;
        }

        private void attach_Nodes(Node entity)
        {
            this.SendPropertyChanging();
            entity.Course = this;
        }

        private void detach_Nodes(Node entity)
        {
            this.SendPropertyChanging();
            entity.Course = null;
        }

        private void attach_StudyResults(StudyResult entity)
        {
            this.SendPropertyChanging();
            entity.Course = this;
        }

        private void detach_StudyResults(StudyResult entity)
        {
            this.SendPropertyChanging();
            entity.Course = null;
        }
    }

    [global::System.Data.Linq.Mapping.TableAttribute(Name = "dbo.CourseUsers")]
    public partial class CourseUser : INotifyPropertyChanging, INotifyPropertyChanged
    {

        private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);

        private int _CourseRef;

        private System.Guid _UserRef;

        private EntityRef<Course> _Course;

        #region Extensibility Method Definitions
        partial void OnLoaded();
        partial void OnValidate(System.Data.Linq.ChangeAction action);
        partial void OnCreated();
        partial void OnCourseRefChanging(int value);
        partial void OnCourseRefChanged();
        partial void OnUserRefChanging(System.Guid value);
        partial void OnUserRefChanged();
        #endregion

        public CourseUser()
        {
            this._Course = default(EntityRef<Course>);
            OnCreated();
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_CourseRef", DbType = "Int NOT NULL", IsPrimaryKey = true)]
        public int CourseRef
        {
            get
            {
                return this._CourseRef;
            }
            set
            {
                if ((this._CourseRef != value))
                {
                    if (this._Course.HasLoadedOrAssignedValue)
                    {
                        throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
                    }
                    this.OnCourseRefChanging(value);
                    this.SendPropertyChanging();
                    this._CourseRef = value;
                    this.SendPropertyChanged("CourseRef");
                    this.OnCourseRefChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_UserRef", DbType = "UniqueIdentifier NOT NULL", IsPrimaryKey = true)]
        public System.Guid UserRef
        {
            get
            {
                return this._UserRef;
            }
            set
            {
                if ((this._UserRef != value))
                {
                    this.OnUserRefChanging(value);
                    this.SendPropertyChanging();
                    this._UserRef = value;
                    this.SendPropertyChanged("UserRef");
                    this.OnUserRefChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.AssociationAttribute(Name = "Course_CourseUser", Storage = "_Course", ThisKey = "CourseRef", OtherKey = "Id", IsForeignKey = true)]
        public Course Course
        {
            get
            {
                return this._Course.Entity;
            }
            set
            {
                Course previousValue = this._Course.Entity;
                if (((previousValue != value)
                            || (this._Course.HasLoadedOrAssignedValue == false)))
                {
                    this.SendPropertyChanging();
                    if ((previousValue != null))
                    {
                        this._Course.Entity = null;
                        previousValue.CourseUsers.Remove(this);
                    }
                    this._Course.Entity = value;
                    if ((value != null))
                    {
                        value.CourseUsers.Add(this);
                        this._CourseRef = value.Id;
                    }
                    else
                    {
                        this._CourseRef = default(int);
                    }
                    this.SendPropertyChanged("Course");
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

    [global::System.Data.Linq.Mapping.TableAttribute(Name = "dbo.CurriculumAssignments")]
    public partial class CurriculumAssignment : INotifyPropertyChanging, INotifyPropertyChanged
    {

        private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);

        private int _Id;

        private int _UserGroupRef;

        private int _CurriculumRef;

        private bool _IsDeleted;

        private bool _IsValid;

        private EntitySet<ThemeAssignment> _ThemeAssignments;

        private EntitySet<Timeline> _Timelines;

        private EntityRef<Curriculum> _Curriculum;

        #region Extensibility Method Definitions
        partial void OnLoaded();
        partial void OnValidate(System.Data.Linq.ChangeAction action);
        partial void OnCreated();
        partial void OnIdChanging(int value);
        partial void OnIdChanged();
        partial void OnUserGroupRefChanging(int value);
        partial void OnUserGroupRefChanged();
        partial void OnCurriculumRefChanging(int value);
        partial void OnCurriculumRefChanged();
        partial void OnIsDeletedChanging(bool value);
        partial void OnIsDeletedChanged();
        partial void OnIsValidChanging(bool value);
        partial void OnIsValidChanged();
        #endregion

        public CurriculumAssignment()
        {
            this._ThemeAssignments = new EntitySet<ThemeAssignment>(new Action<ThemeAssignment>(this.attach_ThemeAssignments), new Action<ThemeAssignment>(this.detach_ThemeAssignments));
            this._Timelines = new EntitySet<Timeline>(new Action<Timeline>(this.attach_Timelines), new Action<Timeline>(this.detach_Timelines));
            this._Curriculum = default(EntityRef<Curriculum>);
            OnCreated();
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Id", AutoSync = AutoSync.OnInsert, DbType = "Int NOT NULL IDENTITY", IsPrimaryKey = true, IsDbGenerated = true)]
        public int Id
        {
            get
            {
                return this._Id;
            }
            set
            {
                if ((this._Id != value))
                {
                    this.OnIdChanging(value);
                    this.SendPropertyChanging();
                    this._Id = value;
                    this.SendPropertyChanged("Id");
                    this.OnIdChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_UserGroupRef", DbType = "Int NOT NULL")]
        public int UserGroupRef
        {
            get
            {
                return this._UserGroupRef;
            }
            set
            {
                if ((this._UserGroupRef != value))
                {
                    this.OnUserGroupRefChanging(value);
                    this.SendPropertyChanging();
                    this._UserGroupRef = value;
                    this.SendPropertyChanged("UserGroupRef");
                    this.OnUserGroupRefChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_CurriculumRef", DbType = "Int NOT NULL")]
        public int CurriculumRef
        {
            get
            {
                return this._CurriculumRef;
            }
            set
            {
                if ((this._CurriculumRef != value))
                {
                    if (this._Curriculum.HasLoadedOrAssignedValue)
                    {
                        throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
                    }
                    this.OnCurriculumRefChanging(value);
                    this.SendPropertyChanging();
                    this._CurriculumRef = value;
                    this.SendPropertyChanged("CurriculumRef");
                    this.OnCurriculumRefChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_IsDeleted", DbType = "Bit NOT NULL")]
        public bool IsDeleted
        {
            get
            {
                return this._IsDeleted;
            }
            set
            {
                if ((this._IsDeleted != value))
                {
                    this.OnIsDeletedChanging(value);
                    this.SendPropertyChanging();
                    this._IsDeleted = value;
                    this.SendPropertyChanged("IsDeleted");
                    this.OnIsDeletedChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_IsValid", DbType = "Bit NOT NULL")]
        public bool IsValid
        {
            get
            {
                return this._IsValid;
            }
            set
            {
                if ((this._IsValid != value))
                {
                    this.OnIsValidChanging(value);
                    this.SendPropertyChanging();
                    this._IsValid = value;
                    this.SendPropertyChanged("IsValid");
                    this.OnIsValidChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.AssociationAttribute(Name = "CurriculumAssignment_ThemeAssignment", Storage = "_ThemeAssignments", ThisKey = "Id", OtherKey = "CurriculumAssignmentRef")]
        public EntitySet<ThemeAssignment> ThemeAssignments
        {
            get
            {
                return this._ThemeAssignments;
            }
            set
            {
                this._ThemeAssignments.Assign(value);
            }
        }

        [global::System.Data.Linq.Mapping.AssociationAttribute(Name = "CurriculumAssignment_Timeline", Storage = "_Timelines", ThisKey = "Id", OtherKey = "CurriculumAssignmentRef")]
        public EntitySet<Timeline> Timelines
        {
            get
            {
                return this._Timelines;
            }
            set
            {
                this._Timelines.Assign(value);
            }
        }

        [global::System.Data.Linq.Mapping.AssociationAttribute(Name = "Curriculum_CurriculumAssignment", Storage = "_Curriculum", ThisKey = "CurriculumRef", OtherKey = "Id", IsForeignKey = true)]
        public Curriculum Curriculum
        {
            get
            {
                return this._Curriculum.Entity;
            }
            set
            {
                Curriculum previousValue = this._Curriculum.Entity;
                if (((previousValue != value)
                            || (this._Curriculum.HasLoadedOrAssignedValue == false)))
                {
                    this.SendPropertyChanging();
                    if ((previousValue != null))
                    {
                        this._Curriculum.Entity = null;
                        previousValue.CurriculumAssignments.Remove(this);
                    }
                    this._Curriculum.Entity = value;
                    if ((value != null))
                    {
                        value.CurriculumAssignments.Add(this);
                        this._CurriculumRef = value.Id;
                    }
                    else
                    {
                        this._CurriculumRef = default(int);
                    }
                    this.SendPropertyChanged("Curriculum");
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

        private void attach_ThemeAssignments(ThemeAssignment entity)
        {
            this.SendPropertyChanging();
            entity.CurriculumAssignment = this;
        }

        private void detach_ThemeAssignments(ThemeAssignment entity)
        {
            this.SendPropertyChanging();
            entity.CurriculumAssignment = null;
        }

        private void attach_Timelines(Timeline entity)
        {
            this.SendPropertyChanging();
            entity.CurriculumAssignment = this;
        }

        private void detach_Timelines(Timeline entity)
        {
            this.SendPropertyChanging();
            entity.CurriculumAssignment = null;
        }
    }

    [global::System.Data.Linq.Mapping.TableAttribute(Name = "dbo.Curriculums")]
    public partial class Curriculum : INotifyPropertyChanging, INotifyPropertyChanged
    {

        private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);

        private int _Id;

        private string _Name;

        private System.DateTime _Created;

        private System.DateTime _Updated;

        private string _Owner;

        private bool _IsDeleted;

        private bool _IsValid;

        private EntitySet<CurriculumAssignment> _CurriculumAssignments;

        private EntitySet<Stage> _Stages;

        #region Extensibility Method Definitions
        partial void OnLoaded();
        partial void OnValidate(System.Data.Linq.ChangeAction action);
        partial void OnCreated();
        partial void OnIdChanging(int value);
        partial void OnIdChanged();
        partial void OnNameChanging(string value);
        partial void OnNameChanged();
        partial void OnCreatedChanging(System.DateTime value);
        partial void OnCreatedChanged();
        partial void OnUpdatedChanging(System.DateTime value);
        partial void OnUpdatedChanged();
        partial void OnOwnerChanging(string value);
        partial void OnOwnerChanged();
        partial void OnIsDeletedChanging(bool value);
        partial void OnIsDeletedChanged();
        partial void OnIsValidChanging(bool value);
        partial void OnIsValidChanged();
        #endregion

        public Curriculum()
        {
            this._CurriculumAssignments = new EntitySet<CurriculumAssignment>(new Action<CurriculumAssignment>(this.attach_CurriculumAssignments), new Action<CurriculumAssignment>(this.detach_CurriculumAssignments));
            this._Stages = new EntitySet<Stage>(new Action<Stage>(this.attach_Stages), new Action<Stage>(this.detach_Stages));
            OnCreated();
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Id", AutoSync = AutoSync.OnInsert, DbType = "Int NOT NULL IDENTITY", IsPrimaryKey = true, IsDbGenerated = true)]
        public int Id
        {
            get
            {
                return this._Id;
            }
            set
            {
                if ((this._Id != value))
                {
                    this.OnIdChanging(value);
                    this.SendPropertyChanging();
                    this._Id = value;
                    this.SendPropertyChanged("Id");
                    this.OnIdChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Name", DbType = "NVarChar(50) NOT NULL", CanBeNull = false)]
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

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Created", DbType = "DateTime NOT NULL")]
        public System.DateTime Created
        {
            get
            {
                return this._Created;
            }
            set
            {
                if ((this._Created != value))
                {
                    this.OnCreatedChanging(value);
                    this.SendPropertyChanging();
                    this._Created = value;
                    this.SendPropertyChanged("Created");
                    this.OnCreatedChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Updated", DbType = "DateTime NOT NULL")]
        public System.DateTime Updated
        {
            get
            {
                return this._Updated;
            }
            set
            {
                if ((this._Updated != value))
                {
                    this.OnUpdatedChanging(value);
                    this.SendPropertyChanging();
                    this._Updated = value;
                    this.SendPropertyChanged("Updated");
                    this.OnUpdatedChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Owner", DbType = "NVarChar(100) NOT NULL", CanBeNull = false)]
        public string Owner
        {
            get
            {
                return this._Owner;
            }
            set
            {
                if ((this._Owner != value))
                {
                    this.OnOwnerChanging(value);
                    this.SendPropertyChanging();
                    this._Owner = value;
                    this.SendPropertyChanged("Owner");
                    this.OnOwnerChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_IsDeleted", DbType = "Bit NOT NULL")]
        public bool IsDeleted
        {
            get
            {
                return this._IsDeleted;
            }
            set
            {
                if ((this._IsDeleted != value))
                {
                    this.OnIsDeletedChanging(value);
                    this.SendPropertyChanging();
                    this._IsDeleted = value;
                    this.SendPropertyChanged("IsDeleted");
                    this.OnIsDeletedChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_IsValid", DbType = "Bit NOT NULL")]
        public bool IsValid
        {
            get
            {
                return this._IsValid;
            }
            set
            {
                if ((this._IsValid != value))
                {
                    this.OnIsValidChanging(value);
                    this.SendPropertyChanging();
                    this._IsValid = value;
                    this.SendPropertyChanged("IsValid");
                    this.OnIsValidChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.AssociationAttribute(Name = "Curriculum_CurriculumAssignment", Storage = "_CurriculumAssignments", ThisKey = "Id", OtherKey = "CurriculumRef")]
        public EntitySet<CurriculumAssignment> CurriculumAssignments
        {
            get
            {
                return this._CurriculumAssignments;
            }
            set
            {
                this._CurriculumAssignments.Assign(value);
            }
        }

        [global::System.Data.Linq.Mapping.AssociationAttribute(Name = "Curriculum_Stage", Storage = "_Stages", ThisKey = "Id", OtherKey = "CurriculumRef")]
        public EntitySet<Stage> Stages
        {
            get
            {
                return this._Stages;
            }
            set
            {
                this._Stages.Assign(value);
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

        private void attach_CurriculumAssignments(CurriculumAssignment entity)
        {
            this.SendPropertyChanging();
            entity.Curriculum = this;
        }

        private void detach_CurriculumAssignments(CurriculumAssignment entity)
        {
            this.SendPropertyChanging();
            entity.Curriculum = null;
        }

        private void attach_Stages(Stage entity)
        {
            this.SendPropertyChanging();
            entity.Curriculum = this;
        }

        private void detach_Stages(Stage entity)
        {
            this.SendPropertyChanging();
            entity.Curriculum = null;
        }
    }

    [global::System.Data.Linq.Mapping.TableAttribute(Name = "dbo.ForecastingResults")]
    public partial class ForecastingResult : INotifyPropertyChanging, INotifyPropertyChanged
    {

        private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);

        private int _Id;

        private System.Guid _StudentRef;

        private int _TreeRef;

        private System.Nullable<int> _ForecastingPoint;

        private System.Nullable<double> _ForecastingPercent;

        private EntityRef<ForecastingTree> _ForecastingTree;

        private EntityRef<User> _User;

        #region Extensibility Method Definitions
        partial void OnLoaded();
        partial void OnValidate(System.Data.Linq.ChangeAction action);
        partial void OnCreated();
        partial void OnIdChanging(int value);
        partial void OnIdChanged();
        partial void OnStudentRefChanging(System.Guid value);
        partial void OnStudentRefChanged();
        partial void OnTreeRefChanging(int value);
        partial void OnTreeRefChanged();
        partial void OnForecastingPointChanging(System.Nullable<int> value);
        partial void OnForecastingPointChanged();
        partial void OnForecastingPercentChanging(System.Nullable<double> value);
        partial void OnForecastingPercentChanged();
        #endregion

        public ForecastingResult()
        {
            this._ForecastingTree = default(EntityRef<ForecastingTree>);
            this._User = default(EntityRef<User>);
            OnCreated();
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Id", AutoSync = AutoSync.OnInsert, DbType = "Int NOT NULL IDENTITY", IsPrimaryKey = true, IsDbGenerated = true)]
        public int Id
        {
            get
            {
                return this._Id;
            }
            set
            {
                if ((this._Id != value))
                {
                    this.OnIdChanging(value);
                    this.SendPropertyChanging();
                    this._Id = value;
                    this.SendPropertyChanged("Id");
                    this.OnIdChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_StudentRef", DbType = "UniqueIdentifier NOT NULL")]
        public System.Guid StudentRef
        {
            get
            {
                return this._StudentRef;
            }
            set
            {
                if ((this._StudentRef != value))
                {
                    if (this._User.HasLoadedOrAssignedValue)
                    {
                        throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
                    }
                    this.OnStudentRefChanging(value);
                    this.SendPropertyChanging();
                    this._StudentRef = value;
                    this.SendPropertyChanged("StudentRef");
                    this.OnStudentRefChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_TreeRef", DbType = "Int NOT NULL")]
        public int TreeRef
        {
            get
            {
                return this._TreeRef;
            }
            set
            {
                if ((this._TreeRef != value))
                {
                    if (this._ForecastingTree.HasLoadedOrAssignedValue)
                    {
                        throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
                    }
                    this.OnTreeRefChanging(value);
                    this.SendPropertyChanging();
                    this._TreeRef = value;
                    this.SendPropertyChanged("TreeRef");
                    this.OnTreeRefChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_ForecastingPoint", DbType = "Int")]
        public System.Nullable<int> ForecastingPoint
        {
            get
            {
                return this._ForecastingPoint;
            }
            set
            {
                if ((this._ForecastingPoint != value))
                {
                    this.OnForecastingPointChanging(value);
                    this.SendPropertyChanging();
                    this._ForecastingPoint = value;
                    this.SendPropertyChanged("ForecastingPoint");
                    this.OnForecastingPointChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_ForecastingPercent", DbType = "Float")]
        public System.Nullable<double> ForecastingPercent
        {
            get
            {
                return this._ForecastingPercent;
            }
            set
            {
                if ((this._ForecastingPercent != value))
                {
                    this.OnForecastingPercentChanging(value);
                    this.SendPropertyChanging();
                    this._ForecastingPercent = value;
                    this.SendPropertyChanged("ForecastingPercent");
                    this.OnForecastingPercentChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.AssociationAttribute(Name = "ForecastingTree_ForecastingResult", Storage = "_ForecastingTree", ThisKey = "TreeRef", OtherKey = "Id", IsForeignKey = true)]
        public ForecastingTree ForecastingTree
        {
            get
            {
                return this._ForecastingTree.Entity;
            }
            set
            {
                ForecastingTree previousValue = this._ForecastingTree.Entity;
                if (((previousValue != value)
                            || (this._ForecastingTree.HasLoadedOrAssignedValue == false)))
                {
                    this.SendPropertyChanging();
                    if ((previousValue != null))
                    {
                        this._ForecastingTree.Entity = null;
                        previousValue.ForecastingResults.Remove(this);
                    }
                    this._ForecastingTree.Entity = value;
                    if ((value != null))
                    {
                        value.ForecastingResults.Add(this);
                        this._TreeRef = value.Id;
                    }
                    else
                    {
                        this._TreeRef = default(int);
                    }
                    this.SendPropertyChanged("ForecastingTree");
                }
            }
        }

        [global::System.Data.Linq.Mapping.AssociationAttribute(Name = "User_ForecastingResult", Storage = "_User", ThisKey = "StudentRef", OtherKey = "Id", IsForeignKey = true)]
        public User User
        {
            get
            {
                return this._User.Entity;
            }
            set
            {
                User previousValue = this._User.Entity;
                if (((previousValue != value)
                            || (this._User.HasLoadedOrAssignedValue == false)))
                {
                    this.SendPropertyChanging();
                    if ((previousValue != null))
                    {
                        this._User.Entity = null;
                        previousValue.ForecastingResults.Remove(this);
                    }
                    this._User.Entity = value;
                    if ((value != null))
                    {
                        value.ForecastingResults.Add(this);
                        this._StudentRef = value.Id;
                    }
                    else
                    {
                        this._StudentRef = default(System.Guid);
                    }
                    this.SendPropertyChanged("User");
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

    [global::System.Data.Linq.Mapping.TableAttribute(Name = "dbo.ForecastingTree")]
    public partial class ForecastingTree : INotifyPropertyChanging, INotifyPropertyChanged
    {

        private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);

        private int _Id;

        private string _Name;

        private System.Guid _UserRef;

        private System.DateTime _Created;

        private System.DateTime _Updated;

        private bool _IsDeleted;

        private EntitySet<ForecastingResult> _ForecastingResults;

        private EntitySet<ForecastingTreeGroup> _ForecastingTreeGroups;

        private EntitySet<ForecastingTreeNode> _ForecastingTreeNodes;

        #region Extensibility Method Definitions
        partial void OnLoaded();
        partial void OnValidate(System.Data.Linq.ChangeAction action);
        partial void OnCreated();
        partial void OnIdChanging(int value);
        partial void OnIdChanged();
        partial void OnNameChanging(string value);
        partial void OnNameChanged();
        partial void OnUserRefChanging(System.Guid value);
        partial void OnUserRefChanged();
        partial void OnCreatedChanging(System.DateTime value);
        partial void OnCreatedChanged();
        partial void OnUpdatedChanging(System.DateTime value);
        partial void OnUpdatedChanged();
        partial void OnIsDeletedChanging(bool value);
        partial void OnIsDeletedChanged();
        #endregion

        public ForecastingTree()
        {
            this._ForecastingResults = new EntitySet<ForecastingResult>(new Action<ForecastingResult>(this.attach_ForecastingResults), new Action<ForecastingResult>(this.detach_ForecastingResults));
            this._ForecastingTreeGroups = new EntitySet<ForecastingTreeGroup>(new Action<ForecastingTreeGroup>(this.attach_ForecastingTreeGroups), new Action<ForecastingTreeGroup>(this.detach_ForecastingTreeGroups));
            this._ForecastingTreeNodes = new EntitySet<ForecastingTreeNode>(new Action<ForecastingTreeNode>(this.attach_ForecastingTreeNodes), new Action<ForecastingTreeNode>(this.detach_ForecastingTreeNodes));
            OnCreated();
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Id", AutoSync = AutoSync.OnInsert, DbType = "Int NOT NULL IDENTITY", IsPrimaryKey = true, IsDbGenerated = true)]
        public int Id
        {
            get
            {
                return this._Id;
            }
            set
            {
                if ((this._Id != value))
                {
                    this.OnIdChanging(value);
                    this.SendPropertyChanging();
                    this._Id = value;
                    this.SendPropertyChanged("Id");
                    this.OnIdChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Name", DbType = "NVarChar(50) NOT NULL", CanBeNull = false)]
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

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_UserRef", DbType = "UniqueIdentifier NOT NULL")]
        public System.Guid UserRef
        {
            get
            {
                return this._UserRef;
            }
            set
            {
                if ((this._UserRef != value))
                {
                    this.OnUserRefChanging(value);
                    this.SendPropertyChanging();
                    this._UserRef = value;
                    this.SendPropertyChanged("UserRef");
                    this.OnUserRefChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Created", DbType = "DateTime NOT NULL")]
        public System.DateTime Created
        {
            get
            {
                return this._Created;
            }
            set
            {
                if ((this._Created != value))
                {
                    this.OnCreatedChanging(value);
                    this.SendPropertyChanging();
                    this._Created = value;
                    this.SendPropertyChanged("Created");
                    this.OnCreatedChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Updated", DbType = "DateTime NOT NULL")]
        public System.DateTime Updated
        {
            get
            {
                return this._Updated;
            }
            set
            {
                if ((this._Updated != value))
                {
                    this.OnUpdatedChanging(value);
                    this.SendPropertyChanging();
                    this._Updated = value;
                    this.SendPropertyChanged("Updated");
                    this.OnUpdatedChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_IsDeleted", DbType = "Bit NOT NULL")]
        public bool IsDeleted
        {
            get
            {
                return this._IsDeleted;
            }
            set
            {
                if ((this._IsDeleted != value))
                {
                    this.OnIsDeletedChanging(value);
                    this.SendPropertyChanging();
                    this._IsDeleted = value;
                    this.SendPropertyChanged("IsDeleted");
                    this.OnIsDeletedChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.AssociationAttribute(Name = "ForecastingTree_ForecastingResult", Storage = "_ForecastingResults", ThisKey = "Id", OtherKey = "TreeRef")]
        public EntitySet<ForecastingResult> ForecastingResults
        {
            get
            {
                return this._ForecastingResults;
            }
            set
            {
                this._ForecastingResults.Assign(value);
            }
        }

        [global::System.Data.Linq.Mapping.AssociationAttribute(Name = "ForecastingTree_ForecastingTreeGroup", Storage = "_ForecastingTreeGroups", ThisKey = "Id", OtherKey = "TreeRef")]
        public EntitySet<ForecastingTreeGroup> ForecastingTreeGroups
        {
            get
            {
                return this._ForecastingTreeGroups;
            }
            set
            {
                this._ForecastingTreeGroups.Assign(value);
            }
        }

        [global::System.Data.Linq.Mapping.AssociationAttribute(Name = "ForecastingTree_ForecastingTreeNode", Storage = "_ForecastingTreeNodes", ThisKey = "Id", OtherKey = "TreeRef")]
        public EntitySet<ForecastingTreeNode> ForecastingTreeNodes
        {
            get
            {
                return this._ForecastingTreeNodes;
            }
            set
            {
                this._ForecastingTreeNodes.Assign(value);
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

        private void attach_ForecastingResults(ForecastingResult entity)
        {
            this.SendPropertyChanging();
            entity.ForecastingTree = this;
        }

        private void detach_ForecastingResults(ForecastingResult entity)
        {
            this.SendPropertyChanging();
            entity.ForecastingTree = null;
        }

        private void attach_ForecastingTreeGroups(ForecastingTreeGroup entity)
        {
            this.SendPropertyChanging();
            entity.ForecastingTree = this;
        }

        private void detach_ForecastingTreeGroups(ForecastingTreeGroup entity)
        {
            this.SendPropertyChanging();
            entity.ForecastingTree = null;
        }

        private void attach_ForecastingTreeNodes(ForecastingTreeNode entity)
        {
            this.SendPropertyChanging();
            entity.ForecastingTree = this;
        }

        private void detach_ForecastingTreeNodes(ForecastingTreeNode entity)
        {
            this.SendPropertyChanging();
            entity.ForecastingTree = null;
        }
    }

    [global::System.Data.Linq.Mapping.TableAttribute(Name = "dbo.ForecastingTreeGroups")]
    public partial class ForecastingTreeGroup : INotifyPropertyChanging, INotifyPropertyChanged
    {

        private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);

        private int _Id;

        private int _TreeRef;

        private int _GroupRef;

        private EntityRef<ForecastingTree> _ForecastingTree;

        private EntityRef<Group> _Group;

        #region Extensibility Method Definitions
        partial void OnLoaded();
        partial void OnValidate(System.Data.Linq.ChangeAction action);
        partial void OnCreated();
        partial void OnIdChanging(int value);
        partial void OnIdChanged();
        partial void OnTreeRefChanging(int value);
        partial void OnTreeRefChanged();
        partial void OnGroupRefChanging(int value);
        partial void OnGroupRefChanged();
        #endregion

        public ForecastingTreeGroup()
        {
            this._ForecastingTree = default(EntityRef<ForecastingTree>);
            this._Group = default(EntityRef<Group>);
            OnCreated();
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Id", AutoSync = AutoSync.OnInsert, DbType = "Int NOT NULL IDENTITY", IsPrimaryKey = true, IsDbGenerated = true)]
        public int Id
        {
            get
            {
                return this._Id;
            }
            set
            {
                if ((this._Id != value))
                {
                    this.OnIdChanging(value);
                    this.SendPropertyChanging();
                    this._Id = value;
                    this.SendPropertyChanged("Id");
                    this.OnIdChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_TreeRef", DbType = "Int NOT NULL")]
        public int TreeRef
        {
            get
            {
                return this._TreeRef;
            }
            set
            {
                if ((this._TreeRef != value))
                {
                    if (this._ForecastingTree.HasLoadedOrAssignedValue)
                    {
                        throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
                    }
                    this.OnTreeRefChanging(value);
                    this.SendPropertyChanging();
                    this._TreeRef = value;
                    this.SendPropertyChanged("TreeRef");
                    this.OnTreeRefChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_GroupRef", DbType = "Int NOT NULL")]
        public int GroupRef
        {
            get
            {
                return this._GroupRef;
            }
            set
            {
                if ((this._GroupRef != value))
                {
                    if (this._Group.HasLoadedOrAssignedValue)
                    {
                        throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
                    }
                    this.OnGroupRefChanging(value);
                    this.SendPropertyChanging();
                    this._GroupRef = value;
                    this.SendPropertyChanged("GroupRef");
                    this.OnGroupRefChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.AssociationAttribute(Name = "ForecastingTree_ForecastingTreeGroup", Storage = "_ForecastingTree", ThisKey = "TreeRef", OtherKey = "Id", IsForeignKey = true)]
        public ForecastingTree ForecastingTree
        {
            get
            {
                return this._ForecastingTree.Entity;
            }
            set
            {
                ForecastingTree previousValue = this._ForecastingTree.Entity;
                if (((previousValue != value)
                            || (this._ForecastingTree.HasLoadedOrAssignedValue == false)))
                {
                    this.SendPropertyChanging();
                    if ((previousValue != null))
                    {
                        this._ForecastingTree.Entity = null;
                        previousValue.ForecastingTreeGroups.Remove(this);
                    }
                    this._ForecastingTree.Entity = value;
                    if ((value != null))
                    {
                        value.ForecastingTreeGroups.Add(this);
                        this._TreeRef = value.Id;
                    }
                    else
                    {
                        this._TreeRef = default(int);
                    }
                    this.SendPropertyChanged("ForecastingTree");
                }
            }
        }

        [global::System.Data.Linq.Mapping.AssociationAttribute(Name = "Group_ForecastingTreeGroup", Storage = "_Group", ThisKey = "GroupRef", OtherKey = "Id", IsForeignKey = true)]
        public Group Group
        {
            get
            {
                return this._Group.Entity;
            }
            set
            {
                Group previousValue = this._Group.Entity;
                if (((previousValue != value)
                            || (this._Group.HasLoadedOrAssignedValue == false)))
                {
                    this.SendPropertyChanging();
                    if ((previousValue != null))
                    {
                        this._Group.Entity = null;
                        previousValue.ForecastingTreeGroups.Remove(this);
                    }
                    this._Group.Entity = value;
                    if ((value != null))
                    {
                        value.ForecastingTreeGroups.Add(this);
                        this._GroupRef = value.Id;
                    }
                    else
                    {
                        this._GroupRef = default(int);
                    }
                    this.SendPropertyChanged("Group");
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

    [global::System.Data.Linq.Mapping.TableAttribute(Name = "dbo.ForecastingTreeNode")]
    public partial class ForecastingTreeNode : INotifyPropertyChanging, INotifyPropertyChanged
    {

        private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);

        private int _Id;

        private string _Name;

        private System.DateTime _Created;

        private System.DateTime _Updated;

        private int _TreeRef;

        private System.Nullable<int> _ParentRef;

        private System.Nullable<int> _CourseRef;

        private bool _IsDeleted;

        private EntitySet<ForecastingTreeNode> _ForecastingTreeNodes;

        private EntityRef<ForecastingTree> _ForecastingTree;

        private EntityRef<ForecastingTreeNode> _ForecastingTreeNode1;

        #region Extensibility Method Definitions
        partial void OnLoaded();
        partial void OnValidate(System.Data.Linq.ChangeAction action);
        partial void OnCreated();
        partial void OnIdChanging(int value);
        partial void OnIdChanged();
        partial void OnNameChanging(string value);
        partial void OnNameChanged();
        partial void OnCreatedChanging(System.DateTime value);
        partial void OnCreatedChanged();
        partial void OnUpdatedChanging(System.DateTime value);
        partial void OnUpdatedChanged();
        partial void OnTreeRefChanging(int value);
        partial void OnTreeRefChanged();
        partial void OnParentRefChanging(System.Nullable<int> value);
        partial void OnParentRefChanged();
        partial void OnCourseRefChanging(System.Nullable<int> value);
        partial void OnCourseRefChanged();
        partial void OnIsDeletedChanging(bool value);
        partial void OnIsDeletedChanged();
        #endregion

        public ForecastingTreeNode()
        {
            this._ForecastingTreeNodes = new EntitySet<ForecastingTreeNode>(new Action<ForecastingTreeNode>(this.attach_ForecastingTreeNodes), new Action<ForecastingTreeNode>(this.detach_ForecastingTreeNodes));
            this._ForecastingTree = default(EntityRef<ForecastingTree>);
            this._ForecastingTreeNode1 = default(EntityRef<ForecastingTreeNode>);
            OnCreated();
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Id", AutoSync = AutoSync.OnInsert, DbType = "Int NOT NULL IDENTITY", IsPrimaryKey = true, IsDbGenerated = true)]
        public int Id
        {
            get
            {
                return this._Id;
            }
            set
            {
                if ((this._Id != value))
                {
                    this.OnIdChanging(value);
                    this.SendPropertyChanging();
                    this._Id = value;
                    this.SendPropertyChanged("Id");
                    this.OnIdChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Name", DbType = "NVarChar(50) NOT NULL", CanBeNull = false)]
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

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Created", DbType = "DateTime NOT NULL")]
        public System.DateTime Created
        {
            get
            {
                return this._Created;
            }
            set
            {
                if ((this._Created != value))
                {
                    this.OnCreatedChanging(value);
                    this.SendPropertyChanging();
                    this._Created = value;
                    this.SendPropertyChanged("Created");
                    this.OnCreatedChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Updated", DbType = "DateTime NOT NULL")]
        public System.DateTime Updated
        {
            get
            {
                return this._Updated;
            }
            set
            {
                if ((this._Updated != value))
                {
                    this.OnUpdatedChanging(value);
                    this.SendPropertyChanging();
                    this._Updated = value;
                    this.SendPropertyChanged("Updated");
                    this.OnUpdatedChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_TreeRef", DbType = "Int NOT NULL")]
        public int TreeRef
        {
            get
            {
                return this._TreeRef;
            }
            set
            {
                if ((this._TreeRef != value))
                {
                    if (this._ForecastingTree.HasLoadedOrAssignedValue)
                    {
                        throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
                    }
                    this.OnTreeRefChanging(value);
                    this.SendPropertyChanging();
                    this._TreeRef = value;
                    this.SendPropertyChanged("TreeRef");
                    this.OnTreeRefChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_ParentRef", DbType = "Int")]
        public System.Nullable<int> ParentRef
        {
            get
            {
                return this._ParentRef;
            }
            set
            {
                if ((this._ParentRef != value))
                {
                    if (this._ForecastingTreeNode1.HasLoadedOrAssignedValue)
                    {
                        throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
                    }
                    this.OnParentRefChanging(value);
                    this.SendPropertyChanging();
                    this._ParentRef = value;
                    this.SendPropertyChanged("ParentRef");
                    this.OnParentRefChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_CourseRef", DbType = "Int")]
        public System.Nullable<int> CourseRef
        {
            get
            {
                return this._CourseRef;
            }
            set
            {
                if ((this._CourseRef != value))
                {
                    this.OnCourseRefChanging(value);
                    this.SendPropertyChanging();
                    this._CourseRef = value;
                    this.SendPropertyChanged("CourseRef");
                    this.OnCourseRefChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_IsDeleted", DbType = "Bit NOT NULL")]
        public bool IsDeleted
        {
            get
            {
                return this._IsDeleted;
            }
            set
            {
                if ((this._IsDeleted != value))
                {
                    this.OnIsDeletedChanging(value);
                    this.SendPropertyChanging();
                    this._IsDeleted = value;
                    this.SendPropertyChanged("IsDeleted");
                    this.OnIsDeletedChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.AssociationAttribute(Name = "ForecastingTreeNode_ForecastingTreeNode", Storage = "_ForecastingTreeNodes", ThisKey = "Id", OtherKey = "ParentRef")]
        public EntitySet<ForecastingTreeNode> ForecastingTreeNodes
        {
            get
            {
                return this._ForecastingTreeNodes;
            }
            set
            {
                this._ForecastingTreeNodes.Assign(value);
            }
        }

        [global::System.Data.Linq.Mapping.AssociationAttribute(Name = "ForecastingTree_ForecastingTreeNode", Storage = "_ForecastingTree", ThisKey = "TreeRef", OtherKey = "Id", IsForeignKey = true)]
        public ForecastingTree ForecastingTree
        {
            get
            {
                return this._ForecastingTree.Entity;
            }
            set
            {
                ForecastingTree previousValue = this._ForecastingTree.Entity;
                if (((previousValue != value)
                            || (this._ForecastingTree.HasLoadedOrAssignedValue == false)))
                {
                    this.SendPropertyChanging();
                    if ((previousValue != null))
                    {
                        this._ForecastingTree.Entity = null;
                        previousValue.ForecastingTreeNodes.Remove(this);
                    }
                    this._ForecastingTree.Entity = value;
                    if ((value != null))
                    {
                        value.ForecastingTreeNodes.Add(this);
                        this._TreeRef = value.Id;
                    }
                    else
                    {
                        this._TreeRef = default(int);
                    }
                    this.SendPropertyChanged("ForecastingTree");
                }
            }
        }

        [global::System.Data.Linq.Mapping.AssociationAttribute(Name = "ForecastingTreeNode_ForecastingTreeNode", Storage = "_ForecastingTreeNode1", ThisKey = "ParentRef", OtherKey = "Id", IsForeignKey = true)]
        public ForecastingTreeNode ForecastingTreeNode1
        {
            get
            {
                return this._ForecastingTreeNode1.Entity;
            }
            set
            {
                ForecastingTreeNode previousValue = this._ForecastingTreeNode1.Entity;
                if (((previousValue != value)
                            || (this._ForecastingTreeNode1.HasLoadedOrAssignedValue == false)))
                {
                    this.SendPropertyChanging();
                    if ((previousValue != null))
                    {
                        this._ForecastingTreeNode1.Entity = null;
                        previousValue.ForecastingTreeNodes.Remove(this);
                    }
                    this._ForecastingTreeNode1.Entity = value;
                    if ((value != null))
                    {
                        value.ForecastingTreeNodes.Add(this);
                        this._ParentRef = value.Id;
                    }
                    else
                    {
                        this._ParentRef = default(Nullable<int>);
                    }
                    this.SendPropertyChanged("ForecastingTreeNode1");
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

        private void attach_ForecastingTreeNodes(ForecastingTreeNode entity)
        {
            this.SendPropertyChanging();
            entity.ForecastingTreeNode1 = this;
        }

        private void detach_ForecastingTreeNodes(ForecastingTreeNode entity)
        {
            this.SendPropertyChanging();
            entity.ForecastingTreeNode1 = null;
        }
    }

    [global::System.Data.Linq.Mapping.TableAttribute(Name = "dbo.[Group]")]
    public partial class Group : INotifyPropertyChanging, INotifyPropertyChanged
    {

        private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);

        private int _Id;

        private string _Name;

        private bool _Deleted;

        private EntitySet<ForecastingTreeGroup> _ForecastingTreeGroups;

        private EntitySet<GroupUser> _GroupUsers;

        #region Extensibility Method Definitions
        partial void OnLoaded();
        partial void OnValidate(System.Data.Linq.ChangeAction action);
        partial void OnCreated();
        partial void OnIdChanging(int value);
        partial void OnIdChanged();
        partial void OnNameChanging(string value);
        partial void OnNameChanged();
        partial void OnDeletedChanging(bool value);
        partial void OnDeletedChanged();
        #endregion

        public Group()
        {
            this._ForecastingTreeGroups = new EntitySet<ForecastingTreeGroup>(new Action<ForecastingTreeGroup>(this.attach_ForecastingTreeGroups), new Action<ForecastingTreeGroup>(this.detach_ForecastingTreeGroups));
            this._GroupUsers = new EntitySet<GroupUser>(new Action<GroupUser>(this.attach_GroupUsers), new Action<GroupUser>(this.detach_GroupUsers));
            OnCreated();
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Id", AutoSync = AutoSync.OnInsert, DbType = "Int NOT NULL IDENTITY", IsPrimaryKey = true, IsDbGenerated = true)]
        public int Id
        {
            get
            {
                return this._Id;
            }
            set
            {
                if ((this._Id != value))
                {
                    this.OnIdChanging(value);
                    this.SendPropertyChanging();
                    this._Id = value;
                    this.SendPropertyChanged("Id");
                    this.OnIdChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Name", DbType = "NVarChar(50) NOT NULL", CanBeNull = false)]
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

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Deleted", DbType = "Bit NOT NULL")]
        public bool Deleted
        {
            get
            {
                return this._Deleted;
            }
            set
            {
                if ((this._Deleted != value))
                {
                    this.OnDeletedChanging(value);
                    this.SendPropertyChanging();
                    this._Deleted = value;
                    this.SendPropertyChanged("Deleted");
                    this.OnDeletedChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.AssociationAttribute(Name = "Group_ForecastingTreeGroup", Storage = "_ForecastingTreeGroups", ThisKey = "Id", OtherKey = "GroupRef")]
        public EntitySet<ForecastingTreeGroup> ForecastingTreeGroups
        {
            get
            {
                return this._ForecastingTreeGroups;
            }
            set
            {
                this._ForecastingTreeGroups.Assign(value);
            }
        }

        [global::System.Data.Linq.Mapping.AssociationAttribute(Name = "Group_GroupUser", Storage = "_GroupUsers", ThisKey = "Id", OtherKey = "GroupRef")]
        public EntitySet<GroupUser> GroupUsers
        {
            get
            {
                return this._GroupUsers;
            }
            set
            {
                this._GroupUsers.Assign(value);
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

        private void attach_ForecastingTreeGroups(ForecastingTreeGroup entity)
        {
            this.SendPropertyChanging();
            entity.Group = this;
        }

        private void detach_ForecastingTreeGroups(ForecastingTreeGroup entity)
        {
            this.SendPropertyChanging();
            entity.Group = null;
        }

        private void attach_GroupUsers(GroupUser entity)
        {
            this.SendPropertyChanging();
            entity.Group = this;
        }

        private void detach_GroupUsers(GroupUser entity)
        {
            this.SendPropertyChanging();
            entity.Group = null;
        }
    }

    [global::System.Data.Linq.Mapping.TableAttribute(Name = "dbo.GroupUsers")]
    public partial class GroupUser : INotifyPropertyChanging, INotifyPropertyChanged
    {

        private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);

        private int _GroupRef;

        private System.Guid _UserRef;

        private EntityRef<Group> _Group;

        private EntityRef<User> _User;

        #region Extensibility Method Definitions
        partial void OnLoaded();
        partial void OnValidate(System.Data.Linq.ChangeAction action);
        partial void OnCreated();
        partial void OnGroupRefChanging(int value);
        partial void OnGroupRefChanged();
        partial void OnUserRefChanging(System.Guid value);
        partial void OnUserRefChanged();
        #endregion

        public GroupUser()
        {
            this._Group = default(EntityRef<Group>);
            this._User = default(EntityRef<User>);
            OnCreated();
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_GroupRef", DbType = "Int NOT NULL", IsPrimaryKey = true)]
        public int GroupRef
        {
            get
            {
                return this._GroupRef;
            }
            set
            {
                if ((this._GroupRef != value))
                {
                    if (this._Group.HasLoadedOrAssignedValue)
                    {
                        throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
                    }
                    this.OnGroupRefChanging(value);
                    this.SendPropertyChanging();
                    this._GroupRef = value;
                    this.SendPropertyChanged("GroupRef");
                    this.OnGroupRefChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_UserRef", DbType = "UniqueIdentifier NOT NULL", IsPrimaryKey = true)]
        public System.Guid UserRef
        {
            get
            {
                return this._UserRef;
            }
            set
            {
                if ((this._UserRef != value))
                {
                    if (this._User.HasLoadedOrAssignedValue)
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

        [global::System.Data.Linq.Mapping.AssociationAttribute(Name = "Group_GroupUser", Storage = "_Group", ThisKey = "GroupRef", OtherKey = "Id", IsForeignKey = true, DeleteOnNull = true, DeleteRule = "CASCADE")]
        public Group Group
        {
            get
            {
                return this._Group.Entity;
            }
            set
            {
                Group previousValue = this._Group.Entity;
                if (((previousValue != value)
                            || (this._Group.HasLoadedOrAssignedValue == false)))
                {
                    this.SendPropertyChanging();
                    if ((previousValue != null))
                    {
                        this._Group.Entity = null;
                        previousValue.GroupUsers.Remove(this);
                    }
                    this._Group.Entity = value;
                    if ((value != null))
                    {
                        value.GroupUsers.Add(this);
                        this._GroupRef = value.Id;
                    }
                    else
                    {
                        this._GroupRef = default(int);
                    }
                    this.SendPropertyChanged("Group");
                }
            }
        }

        [global::System.Data.Linq.Mapping.AssociationAttribute(Name = "User_GroupUser", Storage = "_User", ThisKey = "UserRef", OtherKey = "Id", IsForeignKey = true, DeleteOnNull = true, DeleteRule = "CASCADE")]
        public User User
        {
            get
            {
                return this._User.Entity;
            }
            set
            {
                User previousValue = this._User.Entity;
                if (((previousValue != value)
                            || (this._User.HasLoadedOrAssignedValue == false)))
                {
                    this.SendPropertyChanging();
                    if ((previousValue != null))
                    {
                        this._User.Entity = null;
                        previousValue.GroupUsers.Remove(this);
                    }
                    this._User.Entity = value;
                    if ((value != null))
                    {
                        value.GroupUsers.Add(this);
                        this._UserRef = value.Id;
                    }
                    else
                    {
                        this._UserRef = default(System.Guid);
                    }
                    this.SendPropertyChanged("User");
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

    [global::System.Data.Linq.Mapping.TableAttribute(Name = "dbo.NodeResources")]
    public partial class NodeResource : INotifyPropertyChanging, INotifyPropertyChanged
    {

        private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);

        private int _Id;

        private System.Nullable<int> _NodeId;

        private System.Nullable<int> _Type;

        private string _Name;

        private string _Path;

        private EntityRef<Node> _Node;

        #region Extensibility Method Definitions
        partial void OnLoaded();
        partial void OnValidate(System.Data.Linq.ChangeAction action);
        partial void OnCreated();
        partial void OnIdChanging(int value);
        partial void OnIdChanged();
        partial void OnNodeIdChanging(System.Nullable<int> value);
        partial void OnNodeIdChanged();
        partial void OnTypeChanging(System.Nullable<int> value);
        partial void OnTypeChanged();
        partial void OnNameChanging(string value);
        partial void OnNameChanged();
        partial void OnPathChanging(string value);
        partial void OnPathChanged();
        #endregion

        public NodeResource()
        {
            this._Node = default(EntityRef<Node>);
            OnCreated();
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Id", AutoSync = AutoSync.OnInsert, DbType = "Int NOT NULL IDENTITY", IsPrimaryKey = true, IsDbGenerated = true)]
        public int Id
        {
            get
            {
                return this._Id;
            }
            set
            {
                if ((this._Id != value))
                {
                    this.OnIdChanging(value);
                    this.SendPropertyChanging();
                    this._Id = value;
                    this.SendPropertyChanged("Id");
                    this.OnIdChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_NodeId", DbType = "Int")]
        public System.Nullable<int> NodeId
        {
            get
            {
                return this._NodeId;
            }
            set
            {
                if ((this._NodeId != value))
                {
                    if (this._Node.HasLoadedOrAssignedValue)
                    {
                        throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
                    }
                    this.OnNodeIdChanging(value);
                    this.SendPropertyChanging();
                    this._NodeId = value;
                    this.SendPropertyChanged("NodeId");
                    this.OnNodeIdChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Type", DbType = "Int")]
        public System.Nullable<int> Type
        {
            get
            {
                return this._Type;
            }
            set
            {
                if ((this._Type != value))
                {
                    this.OnTypeChanging(value);
                    this.SendPropertyChanging();
                    this._Type = value;
                    this.SendPropertyChanged("Type");
                    this.OnTypeChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Name", DbType = "NVarChar(50) NOT NULL", CanBeNull = false)]
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

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Path", DbType = "NVarChar(MAX) NOT NULL", CanBeNull = false)]
        public string Path
        {
            get
            {
                return this._Path;
            }
            set
            {
                if ((this._Path != value))
                {
                    this.OnPathChanging(value);
                    this.SendPropertyChanging();
                    this._Path = value;
                    this.SendPropertyChanged("Path");
                    this.OnPathChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.AssociationAttribute(Name = "Node_NodeResource", Storage = "_Node", ThisKey = "NodeId", OtherKey = "Id", IsForeignKey = true)]
        public Node Node
        {
            get
            {
                return this._Node.Entity;
            }
            set
            {
                Node previousValue = this._Node.Entity;
                if (((previousValue != value)
                            || (this._Node.HasLoadedOrAssignedValue == false)))
                {
                    this.SendPropertyChanging();
                    if ((previousValue != null))
                    {
                        this._Node.Entity = null;
                        previousValue.NodeResources.Remove(this);
                    }
                    this._Node.Entity = value;
                    if ((value != null))
                    {
                        value.NodeResources.Add(this);
                        this._NodeId = value.Id;
                    }
                    else
                    {
                        this._NodeId = default(Nullable<int>);
                    }
                    this.SendPropertyChanged("Node");
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

    [global::System.Data.Linq.Mapping.TableAttribute(Name = "dbo.Nodes")]
    public partial class Node : INotifyPropertyChanging, INotifyPropertyChanged
    {

        private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);

        private int _Id;

        private string _Name;

        private int _CourseId;

        private System.Nullable<int> _ParentId;

        private bool _IsFolder;

        private int _Position;

        private System.Xml.Linq.XElement _Sequencing;

        private EntitySet<NodeResource> _NodeResources;

        private EntitySet<Node> _Nodes;

        private EntityRef<Course> _Course;

        private EntityRef<Node> _Node1;

        #region Extensibility Method Definitions
        partial void OnLoaded();
        partial void OnValidate(System.Data.Linq.ChangeAction action);
        partial void OnCreated();
        partial void OnIdChanging(int value);
        partial void OnIdChanged();
        partial void OnNameChanging(string value);
        partial void OnNameChanged();
        partial void OnCourseIdChanging(int value);
        partial void OnCourseIdChanged();
        partial void OnParentIdChanging(System.Nullable<int> value);
        partial void OnParentIdChanged();
        partial void OnIsFolderChanging(bool value);
        partial void OnIsFolderChanged();
        partial void OnPositionChanging(int value);
        partial void OnPositionChanged();
        partial void OnSequencingChanging(System.Xml.Linq.XElement value);
        partial void OnSequencingChanged();
        #endregion

        public Node()
        {
            this._NodeResources = new EntitySet<NodeResource>(new Action<NodeResource>(this.attach_NodeResources), new Action<NodeResource>(this.detach_NodeResources));
            this._Nodes = new EntitySet<Node>(new Action<Node>(this.attach_Nodes), new Action<Node>(this.detach_Nodes));
            this._Course = default(EntityRef<Course>);
            this._Node1 = default(EntityRef<Node>);
            OnCreated();
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Id", AutoSync = AutoSync.OnInsert, DbType = "Int NOT NULL IDENTITY", IsPrimaryKey = true, IsDbGenerated = true)]
        public int Id
        {
            get
            {
                return this._Id;
            }
            set
            {
                if ((this._Id != value))
                {
                    this.OnIdChanging(value);
                    this.SendPropertyChanging();
                    this._Id = value;
                    this.SendPropertyChanged("Id");
                    this.OnIdChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Name", DbType = "NVarChar(50) NOT NULL", CanBeNull = false)]
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

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_CourseId", DbType = "Int NOT NULL")]
        public int CourseId
        {
            get
            {
                return this._CourseId;
            }
            set
            {
                if ((this._CourseId != value))
                {
                    if (this._Course.HasLoadedOrAssignedValue)
                    {
                        throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
                    }
                    this.OnCourseIdChanging(value);
                    this.SendPropertyChanging();
                    this._CourseId = value;
                    this.SendPropertyChanged("CourseId");
                    this.OnCourseIdChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_ParentId", DbType = "Int")]
        public System.Nullable<int> ParentId
        {
            get
            {
                return this._ParentId;
            }
            set
            {
                if ((this._ParentId != value))
                {
                    if (this._Node1.HasLoadedOrAssignedValue)
                    {
                        throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
                    }
                    this.OnParentIdChanging(value);
                    this.SendPropertyChanging();
                    this._ParentId = value;
                    this.SendPropertyChanged("ParentId");
                    this.OnParentIdChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_IsFolder", DbType = "Bit NOT NULL")]
        public bool IsFolder
        {
            get
            {
                return this._IsFolder;
            }
            set
            {
                if ((this._IsFolder != value))
                {
                    this.OnIsFolderChanging(value);
                    this.SendPropertyChanging();
                    this._IsFolder = value;
                    this.SendPropertyChanged("IsFolder");
                    this.OnIsFolderChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Position", DbType = "Int NOT NULL")]
        public int Position
        {
            get
            {
                return this._Position;
            }
            set
            {
                if ((this._Position != value))
                {
                    this.OnPositionChanging(value);
                    this.SendPropertyChanging();
                    this._Position = value;
                    this.SendPropertyChanged("Position");
                    this.OnPositionChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Sequencing", DbType = "Xml", UpdateCheck = UpdateCheck.Never)]
        public System.Xml.Linq.XElement Sequencing
        {
            get
            {
                return this._Sequencing;
            }
            set
            {
                if ((this._Sequencing != value))
                {
                    this.OnSequencingChanging(value);
                    this.SendPropertyChanging();
                    this._Sequencing = value;
                    this.SendPropertyChanged("Sequencing");
                    this.OnSequencingChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.AssociationAttribute(Name = "Node_NodeResource", Storage = "_NodeResources", ThisKey = "Id", OtherKey = "NodeId")]
        public EntitySet<NodeResource> NodeResources
        {
            get
            {
                return this._NodeResources;
            }
            set
            {
                this._NodeResources.Assign(value);
            }
        }

        [global::System.Data.Linq.Mapping.AssociationAttribute(Name = "Node_Node", Storage = "_Nodes", ThisKey = "Id", OtherKey = "ParentId")]
        public EntitySet<Node> Nodes
        {
            get
            {
                return this._Nodes;
            }
            set
            {
                this._Nodes.Assign(value);
            }
        }

        [global::System.Data.Linq.Mapping.AssociationAttribute(Name = "Course_Node", Storage = "_Course", ThisKey = "CourseId", OtherKey = "Id", IsForeignKey = true)]
        public Course Course
        {
            get
            {
                return this._Course.Entity;
            }
            set
            {
                Course previousValue = this._Course.Entity;
                if (((previousValue != value)
                            || (this._Course.HasLoadedOrAssignedValue == false)))
                {
                    this.SendPropertyChanging();
                    if ((previousValue != null))
                    {
                        this._Course.Entity = null;
                        previousValue.Nodes.Remove(this);
                    }
                    this._Course.Entity = value;
                    if ((value != null))
                    {
                        value.Nodes.Add(this);
                        this._CourseId = value.Id;
                    }
                    else
                    {
                        this._CourseId = default(int);
                    }
                    this.SendPropertyChanged("Course");
                }
            }
        }

        [global::System.Data.Linq.Mapping.AssociationAttribute(Name = "Node_Node", Storage = "_Node1", ThisKey = "ParentId", OtherKey = "Id", IsForeignKey = true)]
        public Node Node1
        {
            get
            {
                return this._Node1.Entity;
            }
            set
            {
                Node previousValue = this._Node1.Entity;
                if (((previousValue != value)
                            || (this._Node1.HasLoadedOrAssignedValue == false)))
                {
                    this.SendPropertyChanging();
                    if ((previousValue != null))
                    {
                        this._Node1.Entity = null;
                        previousValue.Nodes.Remove(this);
                    }
                    this._Node1.Entity = value;
                    if ((value != null))
                    {
                        value.Nodes.Add(this);
                        this._ParentId = value.Id;
                    }
                    else
                    {
                        this._ParentId = default(Nullable<int>);
                    }
                    this.SendPropertyChanged("Node1");
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

        private void attach_NodeResources(NodeResource entity)
        {
            this.SendPropertyChanging();
            entity.Node = this;
        }

        private void detach_NodeResources(NodeResource entity)
        {
            this.SendPropertyChanging();
            entity.Node = null;
        }

        private void attach_Nodes(Node entity)
        {
            this.SendPropertyChanging();
            entity.Node1 = this;
        }

        private void detach_Nodes(Node entity)
        {
            this.SendPropertyChanging();
            entity.Node1 = null;
        }
    }
    /*
    [global::System.Data.Linq.Mapping.TableAttribute(Name = "dbo.Roles")]
    public partial class Role
    {

        private int _Id;

        private string _Name;

        private System.Nullable<int> _ParentId;

        public Role()
        {
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Id", AutoSync = AutoSync.Always, DbType = "Int NOT NULL IDENTITY", IsDbGenerated = true)]
        public int Id
        {
            get
            {
                return this._Id;
            }
            set
            {
                if ((this._Id != value))
                {
                    this._Id = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Name", DbType = "NVarChar(50) NOT NULL", CanBeNull = false)]
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
                    this._Name = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_ParentId", DbType = "Int")]
        public System.Nullable<int> ParentId
        {
            get
            {
                return this._ParentId;
            }
            set
            {
                if ((this._ParentId != value))
                {
                    this._ParentId = value;
                }
            }
        }
    }
    */
    [global::System.Data.Linq.Mapping.TableAttribute(Name = "dbo.Rooms")]
    public partial class Room : INotifyPropertyChanging, INotifyPropertyChanged
    {

        private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);

        private int _Id;

        private string _Name;

        private bool _Allowed;

        private EntitySet<Computer> _Computers;

        #region Extensibility Method Definitions
        partial void OnLoaded();
        partial void OnValidate(System.Data.Linq.ChangeAction action);
        partial void OnCreated();
        partial void OnIdChanging(int value);
        partial void OnIdChanged();
        partial void OnNameChanging(string value);
        partial void OnNameChanged();
        partial void OnAllowedChanging(bool value);
        partial void OnAllowedChanged();
        #endregion

        public Room()
        {
            this._Computers = new EntitySet<Computer>(new Action<Computer>(this.attach_Computers), new Action<Computer>(this.detach_Computers));
            OnCreated();
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Id", AutoSync = AutoSync.OnInsert, DbType = "Int NOT NULL IDENTITY", IsPrimaryKey = true, IsDbGenerated = true)]
        public int Id
        {
            get
            {
                return this._Id;
            }
            set
            {
                if ((this._Id != value))
                {
                    this.OnIdChanging(value);
                    this.SendPropertyChanging();
                    this._Id = value;
                    this.SendPropertyChanged("Id");
                    this.OnIdChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Name", DbType = "NVarChar(50) NOT NULL", CanBeNull = false)]
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

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Allowed", DbType = "Bit NOT NULL")]
        public bool Allowed
        {
            get
            {
                return this._Allowed;
            }
            set
            {
                if ((this._Allowed != value))
                {
                    this.OnAllowedChanging(value);
                    this.SendPropertyChanging();
                    this._Allowed = value;
                    this.SendPropertyChanged("Allowed");
                    this.OnAllowedChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.AssociationAttribute(Name = "Room_Computer", Storage = "_Computers", ThisKey = "Id", OtherKey = "RoomRef")]
        public EntitySet<Computer> Computers
        {
            get
            {
                return this._Computers;
            }
            set
            {
                this._Computers.Assign(value);
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

        private void attach_Computers(Computer entity)
        {
            this.SendPropertyChanging();
            entity.Room = this;
        }

        private void detach_Computers(Computer entity)
        {
            this.SendPropertyChanging();
            entity.Room = null;
        }
    }

    [global::System.Data.Linq.Mapping.TableAttribute(Name = "dbo.Stages")]
    public partial class Stage : INotifyPropertyChanging, INotifyPropertyChanged
    {

        private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);

        private int _Id;

        private string _Name;

        private System.DateTime _Created;

        private System.DateTime _Updated;

        private int _CurriculumRef;

        private bool _IsDeleted;

        private EntitySet<Theme> _Themes;

        private EntityRef<Curriculum> _Curriculum;

        #region Extensibility Method Definitions
        partial void OnLoaded();
        partial void OnValidate(System.Data.Linq.ChangeAction action);
        partial void OnCreated();
        partial void OnIdChanging(int value);
        partial void OnIdChanged();
        partial void OnNameChanging(string value);
        partial void OnNameChanged();
        partial void OnCreatedChanging(System.DateTime value);
        partial void OnCreatedChanged();
        partial void OnUpdatedChanging(System.DateTime value);
        partial void OnUpdatedChanged();
        partial void OnCurriculumRefChanging(int value);
        partial void OnCurriculumRefChanged();
        partial void OnIsDeletedChanging(bool value);
        partial void OnIsDeletedChanged();
        #endregion

        public Stage()
        {
            this._Themes = new EntitySet<Theme>(new Action<Theme>(this.attach_Themes), new Action<Theme>(this.detach_Themes));
            this._Curriculum = default(EntityRef<Curriculum>);
            OnCreated();
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Id", AutoSync = AutoSync.OnInsert, DbType = "Int NOT NULL IDENTITY", IsPrimaryKey = true, IsDbGenerated = true)]
        public int Id
        {
            get
            {
                return this._Id;
            }
            set
            {
                if ((this._Id != value))
                {
                    this.OnIdChanging(value);
                    this.SendPropertyChanging();
                    this._Id = value;
                    this.SendPropertyChanged("Id");
                    this.OnIdChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Name", DbType = "NVarChar(50) NOT NULL", CanBeNull = false)]
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

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Created", DbType = "DateTime NOT NULL")]
        public System.DateTime Created
        {
            get
            {
                return this._Created;
            }
            set
            {
                if ((this._Created != value))
                {
                    this.OnCreatedChanging(value);
                    this.SendPropertyChanging();
                    this._Created = value;
                    this.SendPropertyChanged("Created");
                    this.OnCreatedChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Updated", DbType = "DateTime NOT NULL")]
        public System.DateTime Updated
        {
            get
            {
                return this._Updated;
            }
            set
            {
                if ((this._Updated != value))
                {
                    this.OnUpdatedChanging(value);
                    this.SendPropertyChanging();
                    this._Updated = value;
                    this.SendPropertyChanged("Updated");
                    this.OnUpdatedChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_CurriculumRef", DbType = "Int NOT NULL")]
        public int CurriculumRef
        {
            get
            {
                return this._CurriculumRef;
            }
            set
            {
                if ((this._CurriculumRef != value))
                {
                    if (this._Curriculum.HasLoadedOrAssignedValue)
                    {
                        throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
                    }
                    this.OnCurriculumRefChanging(value);
                    this.SendPropertyChanging();
                    this._CurriculumRef = value;
                    this.SendPropertyChanged("CurriculumRef");
                    this.OnCurriculumRefChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_IsDeleted", DbType = "Bit NOT NULL")]
        public bool IsDeleted
        {
            get
            {
                return this._IsDeleted;
            }
            set
            {
                if ((this._IsDeleted != value))
                {
                    this.OnIsDeletedChanging(value);
                    this.SendPropertyChanging();
                    this._IsDeleted = value;
                    this.SendPropertyChanged("IsDeleted");
                    this.OnIsDeletedChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.AssociationAttribute(Name = "Stage_Theme", Storage = "_Themes", ThisKey = "Id", OtherKey = "StageRef")]
        public EntitySet<Theme> Themes
        {
            get
            {
                return this._Themes;
            }
            set
            {
                this._Themes.Assign(value);
            }
        }

        [global::System.Data.Linq.Mapping.AssociationAttribute(Name = "Curriculum_Stage", Storage = "_Curriculum", ThisKey = "CurriculumRef", OtherKey = "Id", IsForeignKey = true)]
        public Curriculum Curriculum
        {
            get
            {
                return this._Curriculum.Entity;
            }
            set
            {
                Curriculum previousValue = this._Curriculum.Entity;
                if (((previousValue != value)
                            || (this._Curriculum.HasLoadedOrAssignedValue == false)))
                {
                    this.SendPropertyChanging();
                    if ((previousValue != null))
                    {
                        this._Curriculum.Entity = null;
                        previousValue.Stages.Remove(this);
                    }
                    this._Curriculum.Entity = value;
                    if ((value != null))
                    {
                        value.Stages.Add(this);
                        this._CurriculumRef = value.Id;
                    }
                    else
                    {
                        this._CurriculumRef = default(int);
                    }
                    this.SendPropertyChanged("Curriculum");
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

        private void attach_Themes(Theme entity)
        {
            this.SendPropertyChanging();
            entity.Stage = this;
        }

        private void detach_Themes(Theme entity)
        {
            this.SendPropertyChanging();
            entity.Stage = null;
        }
    }

    [global::System.Data.Linq.Mapping.TableAttribute(Name = "dbo.StudyResults")]
    public partial class StudyResult : INotifyPropertyChanging, INotifyPropertyChanged
    {

        private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);

        private int _Id;

        private System.Guid _StudentRef;

        private int _CourseRef;

        private int _Point;

        private EntityRef<Course> _Course;

        private EntityRef<User> _User;

        #region Extensibility Method Definitions
        partial void OnLoaded();
        partial void OnValidate(System.Data.Linq.ChangeAction action);
        partial void OnCreated();
        partial void OnIdChanging(int value);
        partial void OnIdChanged();
        partial void OnStudentRefChanging(System.Guid value);
        partial void OnStudentRefChanged();
        partial void OnCourseRefChanging(int value);
        partial void OnCourseRefChanged();
        partial void OnPointChanging(int value);
        partial void OnPointChanged();
        #endregion

        public StudyResult()
        {
            this._Course = default(EntityRef<Course>);
            this._User = default(EntityRef<User>);
            OnCreated();
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Id", AutoSync = AutoSync.OnInsert, DbType = "Int NOT NULL IDENTITY", IsPrimaryKey = true, IsDbGenerated = true)]
        public int Id
        {
            get
            {
                return this._Id;
            }
            set
            {
                if ((this._Id != value))
                {
                    this.OnIdChanging(value);
                    this.SendPropertyChanging();
                    this._Id = value;
                    this.SendPropertyChanged("Id");
                    this.OnIdChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_StudentRef", DbType = "UniqueIdentifier NOT NULL")]
        public System.Guid StudentRef
        {
            get
            {
                return this._StudentRef;
            }
            set
            {
                if ((this._StudentRef != value))
                {
                    if (this._User.HasLoadedOrAssignedValue)
                    {
                        throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
                    }
                    this.OnStudentRefChanging(value);
                    this.SendPropertyChanging();
                    this._StudentRef = value;
                    this.SendPropertyChanged("StudentRef");
                    this.OnStudentRefChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_CourseRef", DbType = "Int NOT NULL")]
        public int CourseRef
        {
            get
            {
                return this._CourseRef;
            }
            set
            {
                if ((this._CourseRef != value))
                {
                    if (this._Course.HasLoadedOrAssignedValue)
                    {
                        throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
                    }
                    this.OnCourseRefChanging(value);
                    this.SendPropertyChanging();
                    this._CourseRef = value;
                    this.SendPropertyChanged("CourseRef");
                    this.OnCourseRefChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Point", DbType = "Int NOT NULL")]
        public int Point
        {
            get
            {
                return this._Point;
            }
            set
            {
                if ((this._Point != value))
                {
                    this.OnPointChanging(value);
                    this.SendPropertyChanging();
                    this._Point = value;
                    this.SendPropertyChanged("Point");
                    this.OnPointChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.AssociationAttribute(Name = "Course_StudyResult", Storage = "_Course", ThisKey = "CourseRef", OtherKey = "Id", IsForeignKey = true)]
        public Course Course
        {
            get
            {
                return this._Course.Entity;
            }
            set
            {
                Course previousValue = this._Course.Entity;
                if (((previousValue != value)
                            || (this._Course.HasLoadedOrAssignedValue == false)))
                {
                    this.SendPropertyChanging();
                    if ((previousValue != null))
                    {
                        this._Course.Entity = null;
                        previousValue.StudyResults.Remove(this);
                    }
                    this._Course.Entity = value;
                    if ((value != null))
                    {
                        value.StudyResults.Add(this);
                        this._CourseRef = value.Id;
                    }
                    else
                    {
                        this._CourseRef = default(int);
                    }
                    this.SendPropertyChanged("Course");
                }
            }
        }

        [global::System.Data.Linq.Mapping.AssociationAttribute(Name = "User_StudyResult", Storage = "_User", ThisKey = "StudentRef", OtherKey = "Id", IsForeignKey = true)]
        public User User
        {
            get
            {
                return this._User.Entity;
            }
            set
            {
                User previousValue = this._User.Entity;
                if (((previousValue != value)
                            || (this._User.HasLoadedOrAssignedValue == false)))
                {
                    this.SendPropertyChanging();
                    if ((previousValue != null))
                    {
                        this._User.Entity = null;
                        previousValue.StudyResults.Remove(this);
                    }
                    this._User.Entity = value;
                    if ((value != null))
                    {
                        value.StudyResults.Add(this);
                        this._StudentRef = value.Id;
                    }
                    else
                    {
                        this._StudentRef = default(System.Guid);
                    }
                    this.SendPropertyChanged("User");
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

    [global::System.Data.Linq.Mapping.TableAttribute(Name = "dbo.ThemeAssignments")]
    public partial class ThemeAssignment : INotifyPropertyChanging, INotifyPropertyChanged
    {

        private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);

        private int _Id;

        private int _ThemeRef;

        private int _CurriculumAssignmentRef;

        private int _MaxScore;

        private bool _IsDeleted;

        private EntityRef<CurriculumAssignment> _CurriculumAssignment;

        private EntityRef<Theme> _Theme;

        #region Extensibility Method Definitions
        partial void OnLoaded();
        partial void OnValidate(System.Data.Linq.ChangeAction action);
        partial void OnCreated();
        partial void OnIdChanging(int value);
        partial void OnIdChanged();
        partial void OnThemeRefChanging(int value);
        partial void OnThemeRefChanged();
        partial void OnCurriculumAssignmentRefChanging(int value);
        partial void OnCurriculumAssignmentRefChanged();
        partial void OnMaxScoreChanging(int value);
        partial void OnMaxScoreChanged();
        partial void OnIsDeletedChanging(bool value);
        partial void OnIsDeletedChanged();
        #endregion

        public ThemeAssignment()
        {
            this._CurriculumAssignment = default(EntityRef<CurriculumAssignment>);
            this._Theme = default(EntityRef<Theme>);
            OnCreated();
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Id", AutoSync = AutoSync.OnInsert, DbType = "Int NOT NULL IDENTITY", IsPrimaryKey = true, IsDbGenerated = true)]
        public int Id
        {
            get
            {
                return this._Id;
            }
            set
            {
                if ((this._Id != value))
                {
                    this.OnIdChanging(value);
                    this.SendPropertyChanging();
                    this._Id = value;
                    this.SendPropertyChanged("Id");
                    this.OnIdChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_ThemeRef", DbType = "Int NOT NULL")]
        public int ThemeRef
        {
            get
            {
                return this._ThemeRef;
            }
            set
            {
                if ((this._ThemeRef != value))
                {
                    if (this._Theme.HasLoadedOrAssignedValue)
                    {
                        throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
                    }
                    this.OnThemeRefChanging(value);
                    this.SendPropertyChanging();
                    this._ThemeRef = value;
                    this.SendPropertyChanged("ThemeRef");
                    this.OnThemeRefChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_CurriculumAssignmentRef", DbType = "Int NOT NULL")]
        public int CurriculumAssignmentRef
        {
            get
            {
                return this._CurriculumAssignmentRef;
            }
            set
            {
                if ((this._CurriculumAssignmentRef != value))
                {
                    if (this._CurriculumAssignment.HasLoadedOrAssignedValue)
                    {
                        throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
                    }
                    this.OnCurriculumAssignmentRefChanging(value);
                    this.SendPropertyChanging();
                    this._CurriculumAssignmentRef = value;
                    this.SendPropertyChanged("CurriculumAssignmentRef");
                    this.OnCurriculumAssignmentRefChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_MaxScore", DbType = "Int NOT NULL")]
        public int MaxScore
        {
            get
            {
                return this._MaxScore;
            }
            set
            {
                if ((this._MaxScore != value))
                {
                    this.OnMaxScoreChanging(value);
                    this.SendPropertyChanging();
                    this._MaxScore = value;
                    this.SendPropertyChanged("MaxScore");
                    this.OnMaxScoreChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_IsDeleted", DbType = "Bit NOT NULL")]
        public bool IsDeleted
        {
            get
            {
                return this._IsDeleted;
            }
            set
            {
                if ((this._IsDeleted != value))
                {
                    this.OnIsDeletedChanging(value);
                    this.SendPropertyChanging();
                    this._IsDeleted = value;
                    this.SendPropertyChanged("IsDeleted");
                    this.OnIsDeletedChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.AssociationAttribute(Name = "CurriculumAssignment_ThemeAssignment", Storage = "_CurriculumAssignment", ThisKey = "CurriculumAssignmentRef", OtherKey = "Id", IsForeignKey = true)]
        public CurriculumAssignment CurriculumAssignment
        {
            get
            {
                return this._CurriculumAssignment.Entity;
            }
            set
            {
                CurriculumAssignment previousValue = this._CurriculumAssignment.Entity;
                if (((previousValue != value)
                            || (this._CurriculumAssignment.HasLoadedOrAssignedValue == false)))
                {
                    this.SendPropertyChanging();
                    if ((previousValue != null))
                    {
                        this._CurriculumAssignment.Entity = null;
                        previousValue.ThemeAssignments.Remove(this);
                    }
                    this._CurriculumAssignment.Entity = value;
                    if ((value != null))
                    {
                        value.ThemeAssignments.Add(this);
                        this._CurriculumAssignmentRef = value.Id;
                    }
                    else
                    {
                        this._CurriculumAssignmentRef = default(int);
                    }
                    this.SendPropertyChanged("CurriculumAssignment");
                }
            }
        }

        [global::System.Data.Linq.Mapping.AssociationAttribute(Name = "Theme_ThemeAssignment", Storage = "_Theme", ThisKey = "ThemeRef", OtherKey = "Id", IsForeignKey = true)]
        public Theme Theme
        {
            get
            {
                return this._Theme.Entity;
            }
            set
            {
                Theme previousValue = this._Theme.Entity;
                if (((previousValue != value)
                            || (this._Theme.HasLoadedOrAssignedValue == false)))
                {
                    this.SendPropertyChanging();
                    if ((previousValue != null))
                    {
                        this._Theme.Entity = null;
                        previousValue.ThemeAssignments.Remove(this);
                    }
                    this._Theme.Entity = value;
                    if ((value != null))
                    {
                        value.ThemeAssignments.Add(this);
                        this._ThemeRef = value.Id;
                    }
                    else
                    {
                        this._ThemeRef = default(int);
                    }
                    this.SendPropertyChanged("Theme");
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

    [global::System.Data.Linq.Mapping.TableAttribute(Name = "dbo.Themes")]
    public partial class Theme : INotifyPropertyChanging, INotifyPropertyChanged
    {

        private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);

        private int _Id;

        private string _Name;

        private System.DateTime _Created;

        private System.DateTime _Updated;

        private int _StageRef;

        private System.Nullable<int> _CourseRef;

        private int _SortOrder;

        private int _ThemeTypeRef;

        private bool _IsDeleted;

        private EntitySet<ThemeAssignment> _ThemeAssignments;

        private EntityRef<Stage> _Stage;

        private EntityRef<ThemeType> _ThemeType;

        #region Extensibility Method Definitions
        partial void OnLoaded();
        partial void OnValidate(System.Data.Linq.ChangeAction action);
        partial void OnCreated();
        partial void OnIdChanging(int value);
        partial void OnIdChanged();
        partial void OnNameChanging(string value);
        partial void OnNameChanged();
        partial void OnCreatedChanging(System.DateTime value);
        partial void OnCreatedChanged();
        partial void OnUpdatedChanging(System.DateTime value);
        partial void OnUpdatedChanged();
        partial void OnStageRefChanging(int value);
        partial void OnStageRefChanged();
        partial void OnCourseRefChanging(System.Nullable<int> value);
        partial void OnCourseRefChanged();
        partial void OnSortOrderChanging(int value);
        partial void OnSortOrderChanged();
        partial void OnThemeTypeRefChanging(int value);
        partial void OnThemeTypeRefChanged();
        partial void OnIsDeletedChanging(bool value);
        partial void OnIsDeletedChanged();
        #endregion

        public Theme()
        {
            this._ThemeAssignments = new EntitySet<ThemeAssignment>(new Action<ThemeAssignment>(this.attach_ThemeAssignments), new Action<ThemeAssignment>(this.detach_ThemeAssignments));
            this._Stage = default(EntityRef<Stage>);
            this._ThemeType = default(EntityRef<ThemeType>);
            OnCreated();
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Id", AutoSync = AutoSync.OnInsert, DbType = "Int NOT NULL IDENTITY", IsPrimaryKey = true, IsDbGenerated = true)]
        public int Id
        {
            get
            {
                return this._Id;
            }
            set
            {
                if ((this._Id != value))
                {
                    this.OnIdChanging(value);
                    this.SendPropertyChanging();
                    this._Id = value;
                    this.SendPropertyChanged("Id");
                    this.OnIdChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Name", DbType = "NVarChar(50) NOT NULL", CanBeNull = false)]
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

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Created", DbType = "DateTime NOT NULL")]
        public System.DateTime Created
        {
            get
            {
                return this._Created;
            }
            set
            {
                if ((this._Created != value))
                {
                    this.OnCreatedChanging(value);
                    this.SendPropertyChanging();
                    this._Created = value;
                    this.SendPropertyChanged("Created");
                    this.OnCreatedChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Updated", DbType = "DateTime NOT NULL")]
        public System.DateTime Updated
        {
            get
            {
                return this._Updated;
            }
            set
            {
                if ((this._Updated != value))
                {
                    this.OnUpdatedChanging(value);
                    this.SendPropertyChanging();
                    this._Updated = value;
                    this.SendPropertyChanged("Updated");
                    this.OnUpdatedChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_StageRef", DbType = "Int NOT NULL")]
        public int StageRef
        {
            get
            {
                return this._StageRef;
            }
            set
            {
                if ((this._StageRef != value))
                {
                    if (this._Stage.HasLoadedOrAssignedValue)
                    {
                        throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
                    }
                    this.OnStageRefChanging(value);
                    this.SendPropertyChanging();
                    this._StageRef = value;
                    this.SendPropertyChanged("StageRef");
                    this.OnStageRefChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_CourseRef", DbType = "Int")]
        public System.Nullable<int> CourseRef
        {
            get
            {
                return this._CourseRef;
            }
            set
            {
                if ((this._CourseRef != value))
                {
                    this.OnCourseRefChanging(value);
                    this.SendPropertyChanging();
                    this._CourseRef = value;
                    this.SendPropertyChanged("CourseRef");
                    this.OnCourseRefChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_SortOrder", DbType = "Int NOT NULL")]
        public int SortOrder
        {
            get
            {
                return this._SortOrder;
            }
            set
            {
                if ((this._SortOrder != value))
                {
                    this.OnSortOrderChanging(value);
                    this.SendPropertyChanging();
                    this._SortOrder = value;
                    this.SendPropertyChanged("SortOrder");
                    this.OnSortOrderChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_ThemeTypeRef", DbType = "Int NOT NULL")]
        public int ThemeTypeRef
        {
            get
            {
                return this._ThemeTypeRef;
            }
            set
            {
                if ((this._ThemeTypeRef != value))
                {
                    if (this._ThemeType.HasLoadedOrAssignedValue)
                    {
                        throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
                    }
                    this.OnThemeTypeRefChanging(value);
                    this.SendPropertyChanging();
                    this._ThemeTypeRef = value;
                    this.SendPropertyChanged("ThemeTypeRef");
                    this.OnThemeTypeRefChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_IsDeleted", DbType = "Bit NOT NULL")]
        public bool IsDeleted
        {
            get
            {
                return this._IsDeleted;
            }
            set
            {
                if ((this._IsDeleted != value))
                {
                    this.OnIsDeletedChanging(value);
                    this.SendPropertyChanging();
                    this._IsDeleted = value;
                    this.SendPropertyChanged("IsDeleted");
                    this.OnIsDeletedChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.AssociationAttribute(Name = "Theme_ThemeAssignment", Storage = "_ThemeAssignments", ThisKey = "Id", OtherKey = "ThemeRef")]
        public EntitySet<ThemeAssignment> ThemeAssignments
        {
            get
            {
                return this._ThemeAssignments;
            }
            set
            {
                this._ThemeAssignments.Assign(value);
            }
        }

        [global::System.Data.Linq.Mapping.AssociationAttribute(Name = "Stage_Theme", Storage = "_Stage", ThisKey = "StageRef", OtherKey = "Id", IsForeignKey = true)]
        public Stage Stage
        {
            get
            {
                return this._Stage.Entity;
            }
            set
            {
                Stage previousValue = this._Stage.Entity;
                if (((previousValue != value)
                            || (this._Stage.HasLoadedOrAssignedValue == false)))
                {
                    this.SendPropertyChanging();
                    if ((previousValue != null))
                    {
                        this._Stage.Entity = null;
                        previousValue.Themes.Remove(this);
                    }
                    this._Stage.Entity = value;
                    if ((value != null))
                    {
                        value.Themes.Add(this);
                        this._StageRef = value.Id;
                    }
                    else
                    {
                        this._StageRef = default(int);
                    }
                    this.SendPropertyChanged("Stage");
                }
            }
        }

        [global::System.Data.Linq.Mapping.AssociationAttribute(Name = "ThemeType_Theme", Storage = "_ThemeType", ThisKey = "ThemeTypeRef", OtherKey = "Id", IsForeignKey = true)]
        public ThemeType ThemeType
        {
            get
            {
                return this._ThemeType.Entity;
            }
            set
            {
                ThemeType previousValue = this._ThemeType.Entity;
                if (((previousValue != value)
                            || (this._ThemeType.HasLoadedOrAssignedValue == false)))
                {
                    this.SendPropertyChanging();
                    if ((previousValue != null))
                    {
                        this._ThemeType.Entity = null;
                        previousValue.Themes.Remove(this);
                    }
                    this._ThemeType.Entity = value;
                    if ((value != null))
                    {
                        value.Themes.Add(this);
                        this._ThemeTypeRef = value.Id;
                    }
                    else
                    {
                        this._ThemeTypeRef = default(int);
                    }
                    this.SendPropertyChanged("ThemeType");
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

        private void attach_ThemeAssignments(ThemeAssignment entity)
        {
            this.SendPropertyChanging();
            entity.Theme = this;
        }

        private void detach_ThemeAssignments(ThemeAssignment entity)
        {
            this.SendPropertyChanging();
            entity.Theme = null;
        }
    }

    [global::System.Data.Linq.Mapping.TableAttribute(Name = "dbo.ThemeTypes")]
    public partial class ThemeType : INotifyPropertyChanging, INotifyPropertyChanged
    {

        private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);

        private int _Id;

        private string _Name;

        private EntitySet<Theme> _Themes;

        #region Extensibility Method Definitions
        partial void OnLoaded();
        partial void OnValidate(System.Data.Linq.ChangeAction action);
        partial void OnCreated();
        partial void OnIdChanging(int value);
        partial void OnIdChanged();
        partial void OnNameChanging(string value);
        partial void OnNameChanged();
        #endregion

        public ThemeType()
        {
            this._Themes = new EntitySet<Theme>(new Action<Theme>(this.attach_Themes), new Action<Theme>(this.detach_Themes));
            OnCreated();
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Id", DbType = "Int NOT NULL", IsPrimaryKey = true)]
        public int Id
        {
            get
            {
                return this._Id;
            }
            set
            {
                if ((this._Id != value))
                {
                    this.OnIdChanging(value);
                    this.SendPropertyChanging();
                    this._Id = value;
                    this.SendPropertyChanged("Id");
                    this.OnIdChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Name", DbType = "NVarChar(50) NOT NULL", CanBeNull = false)]
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

        [global::System.Data.Linq.Mapping.AssociationAttribute(Name = "ThemeType_Theme", Storage = "_Themes", ThisKey = "Id", OtherKey = "ThemeTypeRef")]
        public EntitySet<Theme> Themes
        {
            get
            {
                return this._Themes;
            }
            set
            {
                this._Themes.Assign(value);
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

        private void attach_Themes(Theme entity)
        {
            this.SendPropertyChanging();
            entity.ThemeType = this;
        }

        private void detach_Themes(Theme entity)
        {
            this.SendPropertyChanging();
            entity.ThemeType = null;
        }
    }

    [global::System.Data.Linq.Mapping.TableAttribute(Name = "dbo.Timelines")]
    public partial class Timeline : INotifyPropertyChanging, INotifyPropertyChanged
    {

        private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);

        private int _Id;

        private System.DateTime _StartDate;

        private System.DateTime _EndDate;

        private int _CurriculumAssignmentRef;

        private System.Nullable<int> _StageRef;

        private bool _IsDeleted;

        private EntityRef<CurriculumAssignment> _CurriculumAssignment;

        #region Extensibility Method Definitions
        partial void OnLoaded();
        partial void OnValidate(System.Data.Linq.ChangeAction action);
        partial void OnCreated();
        partial void OnIdChanging(int value);
        partial void OnIdChanged();
        partial void OnStartDateChanging(System.DateTime value);
        partial void OnStartDateChanged();
        partial void OnEndDateChanging(System.DateTime value);
        partial void OnEndDateChanged();
        partial void OnCurriculumAssignmentRefChanging(int value);
        partial void OnCurriculumAssignmentRefChanged();
        partial void OnStageRefChanging(System.Nullable<int> value);
        partial void OnStageRefChanged();
        partial void OnIsDeletedChanging(bool value);
        partial void OnIsDeletedChanged();
        #endregion

        public Timeline()
        {
            this._CurriculumAssignment = default(EntityRef<CurriculumAssignment>);
            OnCreated();
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Id", AutoSync = AutoSync.OnInsert, DbType = "Int NOT NULL IDENTITY", IsPrimaryKey = true, IsDbGenerated = true)]
        public int Id
        {
            get
            {
                return this._Id;
            }
            set
            {
                if ((this._Id != value))
                {
                    this.OnIdChanging(value);
                    this.SendPropertyChanging();
                    this._Id = value;
                    this.SendPropertyChanged("Id");
                    this.OnIdChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_StartDate", DbType = "DateTime NOT NULL")]
        public System.DateTime StartDate
        {
            get
            {
                return this._StartDate;
            }
            set
            {
                if ((this._StartDate != value))
                {
                    this.OnStartDateChanging(value);
                    this.SendPropertyChanging();
                    this._StartDate = value;
                    this.SendPropertyChanged("StartDate");
                    this.OnStartDateChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_EndDate", DbType = "DateTime NOT NULL")]
        public System.DateTime EndDate
        {
            get
            {
                return this._EndDate;
            }
            set
            {
                if ((this._EndDate != value))
                {
                    this.OnEndDateChanging(value);
                    this.SendPropertyChanging();
                    this._EndDate = value;
                    this.SendPropertyChanged("EndDate");
                    this.OnEndDateChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_CurriculumAssignmentRef", DbType = "Int NOT NULL")]
        public int CurriculumAssignmentRef
        {
            get
            {
                return this._CurriculumAssignmentRef;
            }
            set
            {
                if ((this._CurriculumAssignmentRef != value))
                {
                    if (this._CurriculumAssignment.HasLoadedOrAssignedValue)
                    {
                        throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
                    }
                    this.OnCurriculumAssignmentRefChanging(value);
                    this.SendPropertyChanging();
                    this._CurriculumAssignmentRef = value;
                    this.SendPropertyChanged("CurriculumAssignmentRef");
                    this.OnCurriculumAssignmentRefChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_StageRef", DbType = "Int")]
        public System.Nullable<int> StageRef
        {
            get
            {
                return this._StageRef;
            }
            set
            {
                if ((this._StageRef != value))
                {
                    this.OnStageRefChanging(value);
                    this.SendPropertyChanging();
                    this._StageRef = value;
                    this.SendPropertyChanged("StageRef");
                    this.OnStageRefChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_IsDeleted", DbType = "Bit NOT NULL")]
        public bool IsDeleted
        {
            get
            {
                return this._IsDeleted;
            }
            set
            {
                if ((this._IsDeleted != value))
                {
                    this.OnIsDeletedChanging(value);
                    this.SendPropertyChanging();
                    this._IsDeleted = value;
                    this.SendPropertyChanged("IsDeleted");
                    this.OnIsDeletedChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.AssociationAttribute(Name = "CurriculumAssignment_Timeline", Storage = "_CurriculumAssignment", ThisKey = "CurriculumAssignmentRef", OtherKey = "Id", IsForeignKey = true)]
        public CurriculumAssignment CurriculumAssignment
        {
            get
            {
                return this._CurriculumAssignment.Entity;
            }
            set
            {
                CurriculumAssignment previousValue = this._CurriculumAssignment.Entity;
                if (((previousValue != value)
                            || (this._CurriculumAssignment.HasLoadedOrAssignedValue == false)))
                {
                    this.SendPropertyChanging();
                    if ((previousValue != null))
                    {
                        this._CurriculumAssignment.Entity = null;
                        previousValue.Timelines.Remove(this);
                    }
                    this._CurriculumAssignment.Entity = value;
                    if ((value != null))
                    {
                        value.Timelines.Add(this);
                        this._CurriculumAssignmentRef = value.Id;
                    }
                    else
                    {
                        this._CurriculumAssignmentRef = default(int);
                    }
                    this.SendPropertyChanged("CurriculumAssignment");
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

    [global::System.Data.Linq.Mapping.TableAttribute(Name = "dbo.[User]")]
    public partial class User : INotifyPropertyChanging, INotifyPropertyChanged
    {

        private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);

        private System.Guid _Id;

        private string _Username;

        private string _Password;

        private string _Email;

        private string _OpenId;

        private string _Name;

        private bool _IsApproved;

        private bool _Deleted;

        private System.DateTime _CreationDate;

        private System.Nullable<System.Guid> _ApprovedBy;

        private string _UserId;

        private EntitySet<UserRole> _UserRoles;

        private EntitySet<ForecastingResult> _ForecastingResults;

        private EntitySet<GroupUser> _GroupUsers;

        private EntitySet<StudyResult> _StudyResults;

        private EntitySet<User> _Users;

        private EntityRef<User> _User1;

        #region Extensibility Method Definitions
        partial void OnLoaded();
        partial void OnValidate(System.Data.Linq.ChangeAction action);
        partial void OnCreated();
        partial void OnIdChanging(System.Guid value);
        partial void OnIdChanged();
        partial void OnUsernameChanging(string value);
        partial void OnUsernameChanged();
        partial void OnPasswordChanging(string value);
        partial void OnPasswordChanged();
        partial void OnEmailChanging(string value);
        partial void OnEmailChanged();
        partial void OnOpenIdChanging(string value);
        partial void OnOpenIdChanged();
        partial void OnNameChanging(string value);
        partial void OnNameChanged();
        partial void OnIsApprovedChanging(bool value);
        partial void OnIsApprovedChanged();
        partial void OnDeletedChanging(bool value);
        partial void OnDeletedChanged();
        partial void OnCreationDateChanging(System.DateTime value);
        partial void OnCreationDateChanged();
        partial void OnApprovedByChanging(System.Nullable<System.Guid> value);
        partial void OnApprovedByChanged();
        partial void OnUserIdChanging(string value);
        partial void OnUserIdChanged();
        #endregion

        public User()
        {
            this._UserRoles = new EntitySet<UserRole>(new Action<UserRole>(this.attach_UserRoles), new Action<UserRole>(this.detach_UserRoles));
            this._ForecastingResults = new EntitySet<ForecastingResult>(new Action<ForecastingResult>(this.attach_ForecastingResults), new Action<ForecastingResult>(this.detach_ForecastingResults));
            this._GroupUsers = new EntitySet<GroupUser>(new Action<GroupUser>(this.attach_GroupUsers), new Action<GroupUser>(this.detach_GroupUsers));
            this._StudyResults = new EntitySet<StudyResult>(new Action<StudyResult>(this.attach_StudyResults), new Action<StudyResult>(this.detach_StudyResults));
            this._Users = new EntitySet<User>(new Action<User>(this.attach_Users), new Action<User>(this.detach_Users));
            this._User1 = default(EntityRef<User>);
            OnCreated();
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Id", DbType = "UniqueIdentifier NOT NULL", IsPrimaryKey = true)]
        public System.Guid Id
        {
            get
            {
                return this._Id;
            }
            set
            {
                if ((this._Id != value))
                {
                    this.OnIdChanging(value);
                    this.SendPropertyChanging();
                    this._Id = value;
                    this.SendPropertyChanged("Id");
                    this.OnIdChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Username", DbType = "NVarChar(100) NOT NULL", CanBeNull = false)]
        public string Username
        {
            get
            {
                return this._Username;
            }
            set
            {
                if ((this._Username != value))
                {
                    this.OnUsernameChanging(value);
                    this.SendPropertyChanging();
                    this._Username = value;
                    this.SendPropertyChanged("Username");
                    this.OnUsernameChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Password", DbType = "NVarChar(50) NOT NULL", CanBeNull = false)]
        public string Password
        {
            get
            {
                return this._Password;
            }
            set
            {
                if ((this._Password != value))
                {
                    this.OnPasswordChanging(value);
                    this.SendPropertyChanging();
                    this._Password = value;
                    this.SendPropertyChanged("Password");
                    this.OnPasswordChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Email", DbType = "NVarChar(100) NOT NULL", CanBeNull = false)]
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

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_OpenId", DbType = "NVarChar(200) NOT NULL", CanBeNull = false)]
        public string OpenId
        {
            get
            {
                return this._OpenId;
            }
            set
            {
                if ((this._OpenId != value))
                {
                    this.OnOpenIdChanging(value);
                    this.SendPropertyChanging();
                    this._OpenId = value;
                    this.SendPropertyChanged("OpenId");
                    this.OnOpenIdChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Name", DbType = "NVarChar(200) NOT NULL", CanBeNull = false)]
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

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_IsApproved", DbType = "Bit NOT NULL")]
        public bool IsApproved
        {
            get
            {
                return this._IsApproved;
            }
            set
            {
                if ((this._IsApproved != value))
                {
                    this.OnIsApprovedChanging(value);
                    this.SendPropertyChanging();
                    this._IsApproved = value;
                    this.SendPropertyChanged("IsApproved");
                    this.OnIsApprovedChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Deleted", DbType = "Bit NOT NULL")]
        public bool Deleted
        {
            get
            {
                return this._Deleted;
            }
            set
            {
                if ((this._Deleted != value))
                {
                    this.OnDeletedChanging(value);
                    this.SendPropertyChanging();
                    this._Deleted = value;
                    this.SendPropertyChanged("Deleted");
                    this.OnDeletedChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_CreationDate", DbType = "DateTime NOT NULL")]
        public System.DateTime CreationDate
        {
            get
            {
                return this._CreationDate;
            }
            set
            {
                if ((this._CreationDate != value))
                {
                    this.OnCreationDateChanging(value);
                    this.SendPropertyChanging();
                    this._CreationDate = value;
                    this.SendPropertyChanged("CreationDate");
                    this.OnCreationDateChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_ApprovedBy", DbType = "UniqueIdentifier")]
        public System.Nullable<System.Guid> ApprovedBy
        {
            get
            {
                return this._ApprovedBy;
            }
            set
            {
                if ((this._ApprovedBy != value))
                {
                    if (this._User1.HasLoadedOrAssignedValue)
                    {
                        throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
                    }
                    this.OnApprovedByChanging(value);
                    this.SendPropertyChanging();
                    this._ApprovedBy = value;
                    this.SendPropertyChanged("ApprovedBy");
                    this.OnApprovedByChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_UserId", DbType = "NVarChar(100)")]
        public string UserId
        {
            get
            {
                return this._UserId;
            }
            set
            {
                if ((this._UserId != value))
                {
                    this.OnUserIdChanging(value);
                    this.SendPropertyChanging();
                    this._UserId = value;
                    this.SendPropertyChanged("UserId");
                    this.OnUserIdChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.AssociationAttribute(Name = "User_UserRole", Storage = "_UserRoles", ThisKey = "Id", OtherKey = "UserRef")]
        public EntitySet<UserRole> UserRoles
        {
            get
            {
                return this._UserRoles;
            }
            set
            {
                this._UserRoles.Assign(value);
            }
        }

        [global::System.Data.Linq.Mapping.AssociationAttribute(Name = "User_ForecastingResult", Storage = "_ForecastingResults", ThisKey = "Id", OtherKey = "StudentRef")]
        public EntitySet<ForecastingResult> ForecastingResults
        {
            get
            {
                return this._ForecastingResults;
            }
            set
            {
                this._ForecastingResults.Assign(value);
            }
        }

        [global::System.Data.Linq.Mapping.AssociationAttribute(Name = "User_GroupUser", Storage = "_GroupUsers", ThisKey = "Id", OtherKey = "UserRef")]
        public EntitySet<GroupUser> GroupUsers
        {
            get
            {
                return this._GroupUsers;
            }
            set
            {
                this._GroupUsers.Assign(value);
            }
        }

        [global::System.Data.Linq.Mapping.AssociationAttribute(Name = "User_StudyResult", Storage = "_StudyResults", ThisKey = "Id", OtherKey = "StudentRef")]
        public EntitySet<StudyResult> StudyResults
        {
            get
            {
                return this._StudyResults;
            }
            set
            {
                this._StudyResults.Assign(value);
            }
        }

        [global::System.Data.Linq.Mapping.AssociationAttribute(Name = "User_User", Storage = "_Users", ThisKey = "Id", OtherKey = "ApprovedBy")]
        public EntitySet<User> Users
        {
            get
            {
                return this._Users;
            }
            set
            {
                this._Users.Assign(value);
            }
        }

        [global::System.Data.Linq.Mapping.AssociationAttribute(Name = "User_User", Storage = "_User1", ThisKey = "ApprovedBy", OtherKey = "Id", IsForeignKey = true)]
        public User User1
        {
            get
            {
                return this._User1.Entity;
            }
            set
            {
                User previousValue = this._User1.Entity;
                if (((previousValue != value)
                            || (this._User1.HasLoadedOrAssignedValue == false)))
                {
                    this.SendPropertyChanging();
                    if ((previousValue != null))
                    {
                        this._User1.Entity = null;
                        previousValue.Users.Remove(this);
                    }
                    this._User1.Entity = value;
                    if ((value != null))
                    {
                        value.Users.Add(this);
                        this._ApprovedBy = value.Id;
                    }
                    else
                    {
                        this._ApprovedBy = default(Nullable<System.Guid>);
                    }
                    this.SendPropertyChanged("User1");
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

        private void attach_UserRoles(UserRole entity)
        {
            this.SendPropertyChanging();
            entity.User = this;
        }

        private void detach_UserRoles(UserRole entity)
        {
            this.SendPropertyChanging();
            entity.User = null;
        }

        private void attach_ForecastingResults(ForecastingResult entity)
        {
            this.SendPropertyChanging();
            entity.User = this;
        }

        private void detach_ForecastingResults(ForecastingResult entity)
        {
            this.SendPropertyChanging();
            entity.User = null;
        }

        private void attach_GroupUsers(GroupUser entity)
        {
            this.SendPropertyChanging();
            entity.User = this;
        }

        private void detach_GroupUsers(GroupUser entity)
        {
            this.SendPropertyChanging();
            entity.User = null;
        }

        private void attach_StudyResults(StudyResult entity)
        {
            this.SendPropertyChanging();
            entity.User = this;
        }

        private void detach_StudyResults(StudyResult entity)
        {
            this.SendPropertyChanging();
            entity.User = null;
        }

        private void attach_Users(User entity)
        {
            this.SendPropertyChanging();
            entity.User1 = this;
        }

        private void detach_Users(User entity)
        {
            this.SendPropertyChanging();
            entity.User1 = null;
        }
    }

    [global::System.Data.Linq.Mapping.TableAttribute(Name = "dbo.UserActivities")]
    public partial class UserActivity : INotifyPropertyChanging, INotifyPropertyChanged
    {

        private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);

        private int _Id;

        private System.Nullable<System.Guid> _UserRef;

        private System.DateTime _RequestStartTime;

        private System.DateTime _RequestEndTime;

        private int _RequestLength;

        private int _ResponseLength;

        private string _Request;

        #region Extensibility Method Definitions
        partial void OnLoaded();
        partial void OnValidate(System.Data.Linq.ChangeAction action);
        partial void OnCreated();
        partial void OnIdChanging(int value);
        partial void OnIdChanged();
        partial void OnUserRefChanging(System.Nullable<System.Guid> value);
        partial void OnUserRefChanged();
        partial void OnRequestStartTimeChanging(System.DateTime value);
        partial void OnRequestStartTimeChanged();
        partial void OnRequestEndTimeChanging(System.DateTime value);
        partial void OnRequestEndTimeChanged();
        partial void OnRequestLengthChanging(int value);
        partial void OnRequestLengthChanged();
        partial void OnResponseLengthChanging(int value);
        partial void OnResponseLengthChanged();
        partial void OnRequestChanging(string value);
        partial void OnRequestChanged();
        #endregion

        public UserActivity()
        {
            OnCreated();
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Id", AutoSync = AutoSync.OnInsert, DbType = "Int NOT NULL IDENTITY", IsPrimaryKey = true, IsDbGenerated = true)]
        public int Id
        {
            get
            {
                return this._Id;
            }
            set
            {
                if ((this._Id != value))
                {
                    this.OnIdChanging(value);
                    this.SendPropertyChanging();
                    this._Id = value;
                    this.SendPropertyChanged("Id");
                    this.OnIdChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_UserRef", DbType = "UniqueIdentifier")]
        public System.Nullable<System.Guid> UserRef
        {
            get
            {
                return this._UserRef;
            }
            set
            {
                if ((this._UserRef != value))
                {
                    this.OnUserRefChanging(value);
                    this.SendPropertyChanging();
                    this._UserRef = value;
                    this.SendPropertyChanged("UserRef");
                    this.OnUserRefChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_RequestStartTime", DbType = "DateTime NOT NULL")]
        public System.DateTime RequestStartTime
        {
            get
            {
                return this._RequestStartTime;
            }
            set
            {
                if ((this._RequestStartTime != value))
                {
                    this.OnRequestStartTimeChanging(value);
                    this.SendPropertyChanging();
                    this._RequestStartTime = value;
                    this.SendPropertyChanged("RequestStartTime");
                    this.OnRequestStartTimeChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_RequestEndTime", DbType = "DateTime NOT NULL")]
        public System.DateTime RequestEndTime
        {
            get
            {
                return this._RequestEndTime;
            }
            set
            {
                if ((this._RequestEndTime != value))
                {
                    this.OnRequestEndTimeChanging(value);
                    this.SendPropertyChanging();
                    this._RequestEndTime = value;
                    this.SendPropertyChanged("RequestEndTime");
                    this.OnRequestEndTimeChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_RequestLength", DbType = "Int NOT NULL")]
        public int RequestLength
        {
            get
            {
                return this._RequestLength;
            }
            set
            {
                if ((this._RequestLength != value))
                {
                    this.OnRequestLengthChanging(value);
                    this.SendPropertyChanging();
                    this._RequestLength = value;
                    this.SendPropertyChanged("RequestLength");
                    this.OnRequestLengthChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_ResponseLength", DbType = "Int NOT NULL")]
        public int ResponseLength
        {
            get
            {
                return this._ResponseLength;
            }
            set
            {
                if ((this._ResponseLength != value))
                {
                    this.OnResponseLengthChanging(value);
                    this.SendPropertyChanging();
                    this._ResponseLength = value;
                    this.SendPropertyChanged("ResponseLength");
                    this.OnResponseLengthChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Request", DbType = "NVarChar(MAX) NOT NULL", CanBeNull = false)]
        public string Request
        {
            get
            {
                return this._Request;
            }
            set
            {
                if ((this._Request != value))
                {
                    this.OnRequestChanging(value);
                    this.SendPropertyChanging();
                    this._Request = value;
                    this.SendPropertyChanged("Request");
                    this.OnRequestChanged();
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
/*
    [global::System.Data.Linq.Mapping.TableAttribute(Name = "dbo.CourseUsers")]
    public partial class CourseUser : INotifyPropertyChanging, INotifyPropertyChanged
    {

        private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);

        private int _CourseRef;

        private System.Guid _UserRef;

        private EntityRef<Course> _Course;

        #region Extensibility Method Definitions
        partial void OnLoaded();
        partial void OnValidate(System.Data.Linq.ChangeAction action);
        partial void OnCreated();
        partial void OnCourseRefChanging(int value);
        partial void OnCourseRefChanged();
        partial void OnUserRefChanging(System.Guid value);
        partial void OnUserRefChanged();
        #endregion

        public CourseUser()
        {
            this._Course = default(EntityRef<Course>);
            OnCreated();
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_CourseRef", DbType = "Int NOT NULL", IsPrimaryKey = true)]
        public int CourseRef
        {
            get
            {
                return this._CourseRef;
            }
            set
            {
                if ((this._CourseRef != value))
                {
                    if (this._Course.HasLoadedOrAssignedValue)
                    {
                        throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
                    }
                    this.OnCourseRefChanging(value);
                    this.SendPropertyChanging();
                    this._CourseRef = value;
                    this.SendPropertyChanged("CourseRef");
                    this.OnCourseRefChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_UserRef", DbType = "UniqueIdentifier NOT NULL", IsPrimaryKey = true)]
        public System.Guid UserRef
        {
            get
            {
                return this._UserRef;
            }
            set
            {
                if ((this._UserRef != value))
                {
                    this.OnUserRefChanging(value);
                    this.SendPropertyChanging();
                    this._UserRef = value;
                    this.SendPropertyChanged("UserRef");
                    this.OnUserRefChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.AssociationAttribute(Name = "Course_CourseUser", Storage = "_Course", ThisKey = "CourseRef", OtherKey = "Id", IsForeignKey = true)]
        public Course Course
        {
            get
            {
                return this._Course.Entity;
            }
            set
            {
                Course previousValue = this._Course.Entity;
                if (((previousValue != value)
                            || (this._Course.HasLoadedOrAssignedValue == false)))
                {
                    this.SendPropertyChanging();
                    if ((previousValue != null))
                    {
                        this._Course.Entity = null;
                        previousValue.CourseUsers.Remove(this);
                    }
                    this._Course.Entity = value;
                    if ((value != null))
                    {
                        value.CourseUsers.Add(this);
                        this._CourseRef = value.Id;
                    }
                    else
                    {
                        this._CourseRef = default(int);
                    }
                    this.SendPropertyChanged("Course");
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

    [global::System.Data.Linq.Mapping.TableAttribute(Name = "dbo.CurriculumAssignments")]
    public partial class CurriculumAssignment : INotifyPropertyChanging, INotifyPropertyChanged
    {

        private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);

        private int _Id;

        private int _UserGroupRef;

        private int _CurriculumRef;

        private bool _IsDeleted;

        private EntitySet<ThemeAssignment> _ThemeAssignments;

        private EntitySet<Timeline> _Timelines;

        private EntityRef<Curriculum> _Curriculum;

        #region Extensibility Method Definitions
        partial void OnLoaded();
        partial void OnValidate(System.Data.Linq.ChangeAction action);
        partial void OnCreated();
        partial void OnIdChanging(int value);
        partial void OnIdChanged();
        partial void OnUserGroupRefChanging(int value);
        partial void OnUserGroupRefChanged();
        partial void OnCurriculumRefChanging(int value);
        partial void OnCurriculumRefChanged();
        partial void OnIsDeletedChanging(bool value);
        partial void OnIsDeletedChanged();
        #endregion

        public CurriculumAssignment()
        {
            this._ThemeAssignments = new EntitySet<ThemeAssignment>(new Action<ThemeAssignment>(this.attach_ThemeAssignments), new Action<ThemeAssignment>(this.detach_ThemeAssignments));
            this._Timelines = new EntitySet<Timeline>(new Action<Timeline>(this.attach_Timelines), new Action<Timeline>(this.detach_Timelines));
            this._Curriculum = default(EntityRef<Curriculum>);
            OnCreated();
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Id", AutoSync = AutoSync.OnInsert, DbType = "Int NOT NULL IDENTITY", IsPrimaryKey = true, IsDbGenerated = true)]
        public int Id
        {
            get
            {
                return this._Id;
            }
            set
            {
                if ((this._Id != value))
                {
                    this.OnIdChanging(value);
                    this.SendPropertyChanging();
                    this._Id = value;
                    this.SendPropertyChanged("Id");
                    this.OnIdChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_UserGroupRef", DbType = "Int NOT NULL")]
        public int UserGroupRef
        {
            get
            {
                return this._UserGroupRef;
            }
            set
            {
                if ((this._UserGroupRef != value))
                {
                    this.OnUserGroupRefChanging(value);
                    this.SendPropertyChanging();
                    this._UserGroupRef = value;
                    this.SendPropertyChanged("UserGroupRef");
                    this.OnUserGroupRefChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_CurriculumRef", DbType = "Int NOT NULL")]
        public int CurriculumRef
        {
            get
            {
                return this._CurriculumRef;
            }
            set
            {
                if ((this._CurriculumRef != value))
                {
                    if (this._Curriculum.HasLoadedOrAssignedValue)
                    {
                        throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
                    }
                    this.OnCurriculumRefChanging(value);
                    this.SendPropertyChanging();
                    this._CurriculumRef = value;
                    this.SendPropertyChanged("CurriculumRef");
                    this.OnCurriculumRefChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_IsDeleted", DbType = "Bit NOT NULL")]
        public bool IsDeleted
        {
            get
            {
                return this._IsDeleted;
            }
            set
            {
                if ((this._IsDeleted != value))
                {
                    this.OnIsDeletedChanging(value);
                    this.SendPropertyChanging();
                    this._IsDeleted = value;
                    this.SendPropertyChanged("IsDeleted");
                    this.OnIsDeletedChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.AssociationAttribute(Name = "CurriculumAssignment_ThemeAssignment", Storage = "_ThemeAssignments", ThisKey = "Id", OtherKey = "CurriculumAssignmentRef")]
        public EntitySet<ThemeAssignment> ThemeAssignments
        {
            get
            {
                return this._ThemeAssignments;
            }
            set
            {
                this._ThemeAssignments.Assign(value);
            }
        }

        [global::System.Data.Linq.Mapping.AssociationAttribute(Name = "CurriculumAssignment_Timeline", Storage = "_Timelines", ThisKey = "Id", OtherKey = "CurriculumAssignmentRef")]
        public EntitySet<Timeline> Timelines
        {
            get
            {
                return this._Timelines;
            }
            set
            {
                this._Timelines.Assign(value);
            }
        }

        [global::System.Data.Linq.Mapping.AssociationAttribute(Name = "Curriculum_CurriculumAssignment", Storage = "_Curriculum", ThisKey = "CurriculumRef", OtherKey = "Id", IsForeignKey = true)]
        public Curriculum Curriculum
        {
            get
            {
                return this._Curriculum.Entity;
            }
            set
            {
                Curriculum previousValue = this._Curriculum.Entity;
                if (((previousValue != value)
                            || (this._Curriculum.HasLoadedOrAssignedValue == false)))
                {
                    this.SendPropertyChanging();
                    if ((previousValue != null))
                    {
                        this._Curriculum.Entity = null;
                        previousValue.CurriculumAssignments.Remove(this);
                    }
                    this._Curriculum.Entity = value;
                    if ((value != null))
                    {
                        value.CurriculumAssignments.Add(this);
                        this._CurriculumRef = value.Id;
                    }
                    else
                    {
                        this._CurriculumRef = default(int);
                    }
                    this.SendPropertyChanged("Curriculum");
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

        private void attach_ThemeAssignments(ThemeAssignment entity)
        {
            this.SendPropertyChanging();
            entity.CurriculumAssignment = this;
        }

        private void detach_ThemeAssignments(ThemeAssignment entity)
        {
            this.SendPropertyChanging();
            entity.CurriculumAssignment = null;
        }

        private void attach_Timelines(Timeline entity)
        {
            this.SendPropertyChanging();
            entity.CurriculumAssignment = this;
        }

        private void detach_Timelines(Timeline entity)
        {
            this.SendPropertyChanging();
            entity.CurriculumAssignment = null;
        }
    }

    [global::System.Data.Linq.Mapping.TableAttribute(Name = "dbo.GroupUsers")]
    public partial class GroupUser : INotifyPropertyChanging, INotifyPropertyChanged
    {

        private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);

        private int _GroupRef;

        private System.Guid _UserRef;

        private EntityRef<Group> _Group;

        private EntityRef<User> _User;

        #region Extensibility Method Definitions
        partial void OnLoaded();
        partial void OnValidate(System.Data.Linq.ChangeAction action);
        partial void OnCreated();
        partial void OnGroupRefChanging(int value);
        partial void OnGroupRefChanged();
        partial void OnUserRefChanging(System.Guid value);
        partial void OnUserRefChanged();
        #endregion

        public GroupUser()
        {
            this._Group = default(EntityRef<Group>);
            this._User = default(EntityRef<User>);
            OnCreated();
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_GroupRef", DbType = "Int NOT NULL", IsPrimaryKey = true)]
        public int GroupRef
        {
            get
            {
                return this._GroupRef;
            }
            set
            {
                if ((this._GroupRef != value))
                {
                    if (this._Group.HasLoadedOrAssignedValue)
                    {
                        throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
                    }
                    this.OnGroupRefChanging(value);
                    this.SendPropertyChanging();
                    this._GroupRef = value;
                    this.SendPropertyChanged("GroupRef");
                    this.OnGroupRefChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_UserRef", DbType = "UniqueIdentifier NOT NULL", IsPrimaryKey = true)]
        public System.Guid UserRef
        {
            get
            {
                return this._UserRef;
            }
            set
            {
                if ((this._UserRef != value))
                {
                    if (this._User.HasLoadedOrAssignedValue)
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

        [global::System.Data.Linq.Mapping.AssociationAttribute(Name = "Group_GroupUser", Storage = "_Group", ThisKey = "GroupRef", OtherKey = "Id", IsForeignKey = true)]
        public Group Group
        {
            get
            {
                return this._Group.Entity;
            }
            set
            {
                Group previousValue = this._Group.Entity;
                if (((previousValue != value)
                            || (this._Group.HasLoadedOrAssignedValue == false)))
                {
                    this.SendPropertyChanging();
                    if ((previousValue != null))
                    {
                        this._Group.Entity = null;
                        previousValue.GroupUsers.Remove(this);
                    }
                    this._Group.Entity = value;
                    if ((value != null))
                    {
                        value.GroupUsers.Add(this);
                        this._GroupRef = value.Id;
                    }
                    else
                    {
                        this._GroupRef = default(int);
                    }
                    this.SendPropertyChanged("Group");
                }
            }
        }

        [global::System.Data.Linq.Mapping.AssociationAttribute(Name = "User_GroupUser", Storage = "_User", ThisKey = "UserRef", OtherKey = "Id", IsForeignKey = true, DeleteOnNull = true, DeleteRule = "CASCADE")]
        public User User
        {
            get
            {
                return this._User.Entity;
            }
            set
            {
                User previousValue = this._User.Entity;
                if (((previousValue != value)
                            || (this._User.HasLoadedOrAssignedValue == false)))
                {
                    this.SendPropertyChanging();
                    if ((previousValue != null))
                    {
                        this._User.Entity = null;
                        previousValue.GroupUsers.Remove(this);
                    }
                    this._User.Entity = value;
                    if ((value != null))
                    {
                        value.GroupUsers.Add(this);
                        this._UserRef = value.Id;
                    }
                    else
                    {
                        this._UserRef = default(System.Guid);
                    }
                    this.SendPropertyChanged("User");
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

    [global::System.Data.Linq.Mapping.TableAttribute(Name = "dbo.Themes")]
    public partial class Theme : INotifyPropertyChanging, INotifyPropertyChanged
    {

        private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);

        private int _Id;

        private string _Name;

        private System.DateTime _Created;

        private System.DateTime _Updated;

        private int _StageRef;

        private System.Nullable<int> _CourseRef;

        private int _SortOrder;

        private int _ThemeTypeRef;

        private bool _IsDeleted;

        private EntitySet<ThemeAssignment> _ThemeAssignments;

        private EntityRef<ThemeType> _ThemeType;

        private EntityRef<Stage> _Stage;

        #region Extensibility Method Definitions
        partial void OnLoaded();
        partial void OnValidate(System.Data.Linq.ChangeAction action);
        partial void OnCreated();
        partial void OnIdChanging(int value);
        partial void OnIdChanged();
        partial void OnNameChanging(string value);
        partial void OnNameChanged();
        partial void OnCreatedChanging(System.DateTime value);
        partial void OnCreatedChanged();
        partial void OnUpdatedChanging(System.DateTime value);
        partial void OnUpdatedChanged();
        partial void OnStageRefChanging(int value);
        partial void OnStageRefChanged();
        partial void OnCourseRefChanging(System.Nullable<int> value);
        partial void OnCourseRefChanged();
        partial void OnSortOrderChanging(int value);
        partial void OnSortOrderChanged();
        partial void OnThemeTypeRefChanging(int value);
        partial void OnThemeTypeRefChanged();
        partial void OnIsDeletedChanging(bool value);
        partial void OnIsDeletedChanged();
        #endregion

        public Theme()
        {
            this._ThemeAssignments = new EntitySet<ThemeAssignment>(new Action<ThemeAssignment>(this.attach_ThemeAssignments), new Action<ThemeAssignment>(this.detach_ThemeAssignments));
            this._ThemeType = default(EntityRef<ThemeType>);
            this._Stage = default(EntityRef<Stage>);
            OnCreated();
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Id", AutoSync = AutoSync.OnInsert, DbType = "Int NOT NULL IDENTITY", IsPrimaryKey = true, IsDbGenerated = true)]
        public int Id
        {
            get
            {
                return this._Id;
            }
            set
            {
                if ((this._Id != value))
                {
                    this.OnIdChanging(value);
                    this.SendPropertyChanging();
                    this._Id = value;
                    this.SendPropertyChanged("Id");
                    this.OnIdChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Name", DbType = "NVarChar(50) NOT NULL", CanBeNull = false)]
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

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Created", DbType = "DateTime NOT NULL")]
        public System.DateTime Created
        {
            get
            {
                return this._Created;
            }
            set
            {
                if ((this._Created != value))
                {
                    this.OnCreatedChanging(value);
                    this.SendPropertyChanging();
                    this._Created = value;
                    this.SendPropertyChanged("Created");
                    this.OnCreatedChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Updated", DbType = "DateTime NOT NULL")]
        public System.DateTime Updated
        {
            get
            {
                return this._Updated;
            }
            set
            {
                if ((this._Updated != value))
                {
                    this.OnUpdatedChanging(value);
                    this.SendPropertyChanging();
                    this._Updated = value;
                    this.SendPropertyChanged("Updated");
                    this.OnUpdatedChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_StageRef", DbType = "Int NOT NULL")]
        public int StageRef
        {
            get
            {
                return this._StageRef;
            }
            set
            {
                if ((this._StageRef != value))
                {
                    if (this._Stage.HasLoadedOrAssignedValue)
                    {
                        throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
                    }
                    this.OnStageRefChanging(value);
                    this.SendPropertyChanging();
                    this._StageRef = value;
                    this.SendPropertyChanged("StageRef");
                    this.OnStageRefChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_CourseRef", DbType = "Int")]
        public System.Nullable<int> CourseRef
        {
            get
            {
                return this._CourseRef;
            }
            set
            {
                if ((this._CourseRef != value))
                {
                    this.OnCourseRefChanging(value);
                    this.SendPropertyChanging();
                    this._CourseRef = value;
                    this.SendPropertyChanged("CourseRef");
                    this.OnCourseRefChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_SortOrder", DbType = "Int NOT NULL")]
        public int SortOrder
        {
            get
            {
                return this._SortOrder;
            }
            set
            {
                if ((this._SortOrder != value))
                {
                    this.OnSortOrderChanging(value);
                    this.SendPropertyChanging();
                    this._SortOrder = value;
                    this.SendPropertyChanged("SortOrder");
                    this.OnSortOrderChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_ThemeTypeRef", DbType = "Int NOT NULL")]
        public int ThemeTypeRef
        {
            get
            {
                return this._ThemeTypeRef;
            }
            set
            {
                if ((this._ThemeTypeRef != value))
                {
                    if (this._ThemeType.HasLoadedOrAssignedValue)
                    {
                        throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
                    }
                    this.OnThemeTypeRefChanging(value);
                    this.SendPropertyChanging();
                    this._ThemeTypeRef = value;
                    this.SendPropertyChanged("ThemeTypeRef");
                    this.OnThemeTypeRefChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_IsDeleted", DbType = "Bit NOT NULL")]
        public bool IsDeleted
        {
            get
            {
                return this._IsDeleted;
            }
            set
            {
                if ((this._IsDeleted != value))
                {
                    this.OnIsDeletedChanging(value);
                    this.SendPropertyChanging();
                    this._IsDeleted = value;
                    this.SendPropertyChanged("IsDeleted");
                    this.OnIsDeletedChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.AssociationAttribute(Name = "Theme_ThemeAssignment", Storage = "_ThemeAssignments", ThisKey = "Id", OtherKey = "ThemeRef")]
        public EntitySet<ThemeAssignment> ThemeAssignments
        {
            get
            {
                return this._ThemeAssignments;
            }
            set
            {
                this._ThemeAssignments.Assign(value);
            }
        }

        [global::System.Data.Linq.Mapping.AssociationAttribute(Name = "ThemeType_Theme", Storage = "_ThemeType", ThisKey = "ThemeTypeRef", OtherKey = "Id", IsForeignKey = true)]
        public ThemeType ThemeType
        {
            get
            {
                return this._ThemeType.Entity;
            }
            set
            {
                ThemeType previousValue = this._ThemeType.Entity;
                if (((previousValue != value)
                            || (this._ThemeType.HasLoadedOrAssignedValue == false)))
                {
                    this.SendPropertyChanging();
                    if ((previousValue != null))
                    {
                        this._ThemeType.Entity = null;
                        previousValue.Themes.Remove(this);
                    }
                    this._ThemeType.Entity = value;
                    if ((value != null))
                    {
                        value.Themes.Add(this);
                        this._ThemeTypeRef = value.Id;
                    }
                    else
                    {
                        this._ThemeTypeRef = default(int);
                    }
                    this.SendPropertyChanged("ThemeType");
                }
            }
        }

        [global::System.Data.Linq.Mapping.AssociationAttribute(Name = "Stage_Theme", Storage = "_Stage", ThisKey = "StageRef", OtherKey = "Id", IsForeignKey = true)]
        public Stage Stage
        {
            get
            {
                return this._Stage.Entity;
            }
            set
            {
                Stage previousValue = this._Stage.Entity;
                if (((previousValue != value)
                            || (this._Stage.HasLoadedOrAssignedValue == false)))
                {
                    this.SendPropertyChanging();
                    if ((previousValue != null))
                    {
                        this._Stage.Entity = null;
                        previousValue.Themes.Remove(this);
                    }
                    this._Stage.Entity = value;
                    if ((value != null))
                    {
                        value.Themes.Add(this);
                        this._StageRef = value.Id;
                    }
                    else
                    {
                        this._StageRef = default(int);
                    }
                    this.SendPropertyChanged("Stage");
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

        private void attach_ThemeAssignments(ThemeAssignment entity)
        {
            this.SendPropertyChanging();
            entity.Theme = this;
        }

        private void detach_ThemeAssignments(ThemeAssignment entity)
        {
            this.SendPropertyChanging();
            entity.Theme = null;
        }
    }

    [global::System.Data.Linq.Mapping.TableAttribute(Name = "dbo.ThemeTypes")]
    public partial class ThemeType : INotifyPropertyChanging, INotifyPropertyChanged
    {

        private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);

        private int _Id;

        private string _Name;

        private EntitySet<Theme> _Themes;

        #region Extensibility Method Definitions
        partial void OnLoaded();
        partial void OnValidate(System.Data.Linq.ChangeAction action);
        partial void OnCreated();
        partial void OnIdChanging(int value);
        partial void OnIdChanged();
        partial void OnNameChanging(string value);
        partial void OnNameChanged();
        #endregion

        public ThemeType()
        {
            this._Themes = new EntitySet<Theme>(new Action<Theme>(this.attach_Themes), new Action<Theme>(this.detach_Themes));
            OnCreated();
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Id", DbType = "Int NOT NULL", IsPrimaryKey = true)]
        public int Id
        {
            get
            {
                return this._Id;
            }
            set
            {
                if ((this._Id != value))
                {
                    this.OnIdChanging(value);
                    this.SendPropertyChanging();
                    this._Id = value;
                    this.SendPropertyChanged("Id");
                    this.OnIdChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Name", DbType = "NVarChar(50) NOT NULL", CanBeNull = false)]
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

        [global::System.Data.Linq.Mapping.AssociationAttribute(Name = "ThemeType_Theme", Storage = "_Themes", ThisKey = "Id", OtherKey = "ThemeTypeRef")]
        public EntitySet<Theme> Themes
        {
            get
            {
                return this._Themes;
            }
            set
            {
                this._Themes.Assign(value);
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

        private void attach_Themes(Theme entity)
        {
            this.SendPropertyChanging();
            entity.ThemeType = this;
        }

        private void detach_Themes(Theme entity)
        {
            this.SendPropertyChanging();
            entity.ThemeType = null;
        }
    }

    [global::System.Data.Linq.Mapping.TableAttribute(Name = "dbo.ThemeAssignments")]
    public partial class ThemeAssignment : INotifyPropertyChanging, INotifyPropertyChanged
    {

        private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);

        private int _Id;

        private int _ThemeRef;

        private int _CurriculumAssignmentRef;

        private int _MaxScore;

        private bool _IsDeleted;

        private EntityRef<CurriculumAssignment> _CurriculumAssignment;

        private EntityRef<Theme> _Theme;

        #region Extensibility Method Definitions
        partial void OnLoaded();
        partial void OnValidate(System.Data.Linq.ChangeAction action);
        partial void OnCreated();
        partial void OnIdChanging(int value);
        partial void OnIdChanged();
        partial void OnThemeRefChanging(int value);
        partial void OnThemeRefChanged();
        partial void OnCurriculumAssignmentRefChanging(int value);
        partial void OnCurriculumAssignmentRefChanged();
        partial void OnMaxScoreChanging(int value);
        partial void OnMaxScoreChanged();
        partial void OnIsDeletedChanging(bool value);
        partial void OnIsDeletedChanged();
        #endregion

        public ThemeAssignment()
        {
            this._CurriculumAssignment = default(EntityRef<CurriculumAssignment>);
            this._Theme = default(EntityRef<Theme>);
            OnCreated();
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Id", AutoSync = AutoSync.OnInsert, DbType = "Int NOT NULL IDENTITY", IsPrimaryKey = true, IsDbGenerated = true)]
        public int Id
        {
            get
            {
                return this._Id;
            }
            set
            {
                if ((this._Id != value))
                {
                    this.OnIdChanging(value);
                    this.SendPropertyChanging();
                    this._Id = value;
                    this.SendPropertyChanged("Id");
                    this.OnIdChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_ThemeRef", DbType = "Int NOT NULL")]
        public int ThemeRef
        {
            get
            {
                return this._ThemeRef;
            }
            set
            {
                if ((this._ThemeRef != value))
                {
                    if (this._Theme.HasLoadedOrAssignedValue)
                    {
                        throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
                    }
                    this.OnThemeRefChanging(value);
                    this.SendPropertyChanging();
                    this._ThemeRef = value;
                    this.SendPropertyChanged("ThemeRef");
                    this.OnThemeRefChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_CurriculumAssignmentRef", DbType = "Int NOT NULL")]
        public int CurriculumAssignmentRef
        {
            get
            {
                return this._CurriculumAssignmentRef;
            }
            set
            {
                if ((this._CurriculumAssignmentRef != value))
                {
                    if (this._CurriculumAssignment.HasLoadedOrAssignedValue)
                    {
                        throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
                    }
                    this.OnCurriculumAssignmentRefChanging(value);
                    this.SendPropertyChanging();
                    this._CurriculumAssignmentRef = value;
                    this.SendPropertyChanged("CurriculumAssignmentRef");
                    this.OnCurriculumAssignmentRefChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_MaxScore", DbType = "Int NOT NULL")]
        public int MaxScore
        {
            get
            {
                return this._MaxScore;
            }
            set
            {
                if ((this._MaxScore != value))
                {
                    this.OnMaxScoreChanging(value);
                    this.SendPropertyChanging();
                    this._MaxScore = value;
                    this.SendPropertyChanged("MaxScore");
                    this.OnMaxScoreChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_IsDeleted", DbType = "Bit NOT NULL")]
        public bool IsDeleted
        {
            get
            {
                return this._IsDeleted;
            }
            set
            {
                if ((this._IsDeleted != value))
                {
                    this.OnIsDeletedChanging(value);
                    this.SendPropertyChanging();
                    this._IsDeleted = value;
                    this.SendPropertyChanged("IsDeleted");
                    this.OnIsDeletedChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.AssociationAttribute(Name = "CurriculumAssignment_ThemeAssignment", Storage = "_CurriculumAssignment", ThisKey = "CurriculumAssignmentRef", OtherKey = "Id", IsForeignKey = true)]
        public CurriculumAssignment CurriculumAssignment
        {
            get
            {
                return this._CurriculumAssignment.Entity;
            }
            set
            {
                CurriculumAssignment previousValue = this._CurriculumAssignment.Entity;
                if (((previousValue != value)
                            || (this._CurriculumAssignment.HasLoadedOrAssignedValue == false)))
                {
                    this.SendPropertyChanging();
                    if ((previousValue != null))
                    {
                        this._CurriculumAssignment.Entity = null;
                        previousValue.ThemeAssignments.Remove(this);
                    }
                    this._CurriculumAssignment.Entity = value;
                    if ((value != null))
                    {
                        value.ThemeAssignments.Add(this);
                        this._CurriculumAssignmentRef = value.Id;
                    }
                    else
                    {
                        this._CurriculumAssignmentRef = default(int);
                    }
                    this.SendPropertyChanged("CurriculumAssignment");
                }
            }
        }

        [global::System.Data.Linq.Mapping.AssociationAttribute(Name = "Theme_ThemeAssignment", Storage = "_Theme", ThisKey = "ThemeRef", OtherKey = "Id", IsForeignKey = true)]
        public Theme Theme
        {
            get
            {
                return this._Theme.Entity;
            }
            set
            {
                Theme previousValue = this._Theme.Entity;
                if (((previousValue != value)
                            || (this._Theme.HasLoadedOrAssignedValue == false)))
                {
                    this.SendPropertyChanging();
                    if ((previousValue != null))
                    {
                        this._Theme.Entity = null;
                        previousValue.ThemeAssignments.Remove(this);
                    }
                    this._Theme.Entity = value;
                    if ((value != null))
                    {
                        value.ThemeAssignments.Add(this);
                        this._ThemeRef = value.Id;
                    }
                    else
                    {
                        this._ThemeRef = default(int);
                    }
                    this.SendPropertyChanged("Theme");
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

    [global::System.Data.Linq.Mapping.TableAttribute(Name = "dbo.[Group]")]
    public partial class Group : INotifyPropertyChanging, INotifyPropertyChanged
    {

        private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);

        private int _Id;

        private string _Name;

        private bool _Deleted;

        private EntitySet<GroupUser> _GroupUsers;

        #region Extensibility Method Definitions
        partial void OnLoaded();
        partial void OnValidate(System.Data.Linq.ChangeAction action);
        partial void OnCreated();
        partial void OnIdChanging(int value);
        partial void OnIdChanged();
        partial void OnNameChanging(string value);
        partial void OnNameChanged();
        partial void OnDeletedChanging(bool value);
        partial void OnDeletedChanged();
        #endregion

        public Group()
        {
            this._GroupUsers = new EntitySet<GroupUser>(new Action<GroupUser>(this.attach_GroupUsers), new Action<GroupUser>(this.detach_GroupUsers));
            OnCreated();
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Id", AutoSync = AutoSync.OnInsert, DbType = "Int NOT NULL IDENTITY", IsPrimaryKey = true, IsDbGenerated = true)]
        public int Id
        {
            get
            {
                return this._Id;
            }
            set
            {
                if ((this._Id != value))
                {
                    this.OnIdChanging(value);
                    this.SendPropertyChanging();
                    this._Id = value;
                    this.SendPropertyChanged("Id");
                    this.OnIdChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Name", DbType = "NVarChar(50) NOT NULL", CanBeNull = false)]
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

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Deleted", DbType = "Bit NOT NULL")]
        public bool Deleted
        {
            get
            {
                return this._Deleted;
            }
            set
            {
                if ((this._Deleted != value))
                {
                    this.OnDeletedChanging(value);
                    this.SendPropertyChanging();
                    this._Deleted = value;
                    this.SendPropertyChanged("Deleted");
                    this.OnDeletedChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.AssociationAttribute(Name = "Group_GroupUser", Storage = "_GroupUsers", ThisKey = "Id", OtherKey = "GroupRef")]
        public EntitySet<GroupUser> GroupUsers
        {
            get
            {
                return this._GroupUsers;
            }
            set
            {
                this._GroupUsers.Assign(value);
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

        private void attach_GroupUsers(GroupUser entity)
        {
            this.SendPropertyChanging();
            entity.Group = this;
        }

        private void detach_GroupUsers(GroupUser entity)
        {
            this.SendPropertyChanging();
            entity.Group = null;
        }
    }

    [global::System.Data.Linq.Mapping.TableAttribute(Name = "dbo.Timelines")]
    public partial class Timeline : INotifyPropertyChanging, INotifyPropertyChanged
    {

        private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);

        private int _Id;

        private System.DateTime _StartDate;

        private System.DateTime _EndDate;

        private int _CurriculumAssignmentRef;

        private System.Nullable<int> _StageRef;

        private bool _IsDeleted;

        private EntityRef<CurriculumAssignment> _CurriculumAssignment;

        #region Extensibility Method Definitions
        partial void OnLoaded();
        partial void OnValidate(System.Data.Linq.ChangeAction action);
        partial void OnCreated();
        partial void OnIdChanging(int value);
        partial void OnIdChanged();
        partial void OnStartDateChanging(System.DateTime value);
        partial void OnStartDateChanged();
        partial void OnEndDateChanging(System.DateTime value);
        partial void OnEndDateChanged();
        partial void OnCurriculumAssignmentRefChanging(int value);
        partial void OnCurriculumAssignmentRefChanged();
        partial void OnStageRefChanging(System.Nullable<int> value);
        partial void OnStageRefChanged();
        partial void OnIsDeletedChanging(bool value);
        partial void OnIsDeletedChanged();
        #endregion

        public Timeline()
        {
            this._CurriculumAssignment = default(EntityRef<CurriculumAssignment>);
            OnCreated();
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Id", AutoSync = AutoSync.OnInsert, DbType = "Int NOT NULL IDENTITY", IsPrimaryKey = true, IsDbGenerated = true)]
        public int Id
        {
            get
            {
                return this._Id;
            }
            set
            {
                if ((this._Id != value))
                {
                    this.OnIdChanging(value);
                    this.SendPropertyChanging();
                    this._Id = value;
                    this.SendPropertyChanged("Id");
                    this.OnIdChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_StartDate", DbType = "DateTime NOT NULL")]
        public System.DateTime StartDate
        {
            get
            {
                return this._StartDate;
            }
            set
            {
                if ((this._StartDate != value))
                {
                    this.OnStartDateChanging(value);
                    this.SendPropertyChanging();
                    this._StartDate = value;
                    this.SendPropertyChanged("StartDate");
                    this.OnStartDateChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_EndDate", DbType = "DateTime NOT NULL")]
        public System.DateTime EndDate
        {
            get
            {
                return this._EndDate;
            }
            set
            {
                if ((this._EndDate != value))
                {
                    this.OnEndDateChanging(value);
                    this.SendPropertyChanging();
                    this._EndDate = value;
                    this.SendPropertyChanged("EndDate");
                    this.OnEndDateChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_CurriculumAssignmentRef", DbType = "Int NOT NULL")]
        public int CurriculumAssignmentRef
        {
            get
            {
                return this._CurriculumAssignmentRef;
            }
            set
            {
                if ((this._CurriculumAssignmentRef != value))
                {
                    if (this._CurriculumAssignment.HasLoadedOrAssignedValue)
                    {
                        throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
                    }
                    this.OnCurriculumAssignmentRefChanging(value);
                    this.SendPropertyChanging();
                    this._CurriculumAssignmentRef = value;
                    this.SendPropertyChanged("CurriculumAssignmentRef");
                    this.OnCurriculumAssignmentRefChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_StageRef", DbType = "Int")]
        public System.Nullable<int> StageRef
        {
            get
            {
                return this._StageRef;
            }
            set
            {
                if ((this._StageRef != value))
                {
                    this.OnStageRefChanging(value);
                    this.SendPropertyChanging();
                    this._StageRef = value;
                    this.SendPropertyChanged("StageRef");
                    this.OnStageRefChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_IsDeleted", DbType = "Bit NOT NULL")]
        public bool IsDeleted
        {
            get
            {
                return this._IsDeleted;
            }
            set
            {
                if ((this._IsDeleted != value))
                {
                    this.OnIsDeletedChanging(value);
                    this.SendPropertyChanging();
                    this._IsDeleted = value;
                    this.SendPropertyChanged("IsDeleted");
                    this.OnIsDeletedChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.AssociationAttribute(Name = "CurriculumAssignment_Timeline", Storage = "_CurriculumAssignment", ThisKey = "CurriculumAssignmentRef", OtherKey = "Id", IsForeignKey = true)]
        public CurriculumAssignment CurriculumAssignment
        {
            get
            {
                return this._CurriculumAssignment.Entity;
            }
            set
            {
                CurriculumAssignment previousValue = this._CurriculumAssignment.Entity;
                if (((previousValue != value)
                            || (this._CurriculumAssignment.HasLoadedOrAssignedValue == false)))
                {
                    this.SendPropertyChanging();
                    if ((previousValue != null))
                    {
                        this._CurriculumAssignment.Entity = null;
                        previousValue.Timelines.Remove(this);
                    }
                    this._CurriculumAssignment.Entity = value;
                    if ((value != null))
                    {
                        value.Timelines.Add(this);
                        this._CurriculumAssignmentRef = value.Id;
                    }
                    else
                    {
                        this._CurriculumAssignmentRef = default(int);
                    }
                    this.SendPropertyChanged("CurriculumAssignment");
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

    [global::System.Data.Linq.Mapping.TableAttribute(Name = "dbo.Curriculums")]
    public partial class Curriculum : INotifyPropertyChanging, INotifyPropertyChanged
    {

        private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);

        private int _Id;

        private string _Name;

        private System.DateTime _Created;

        private System.DateTime _Updated;

        private string _Owner;

        private bool _IsDeleted;

        private bool _IsValid;

        private EntitySet<CurriculumAssignment> _CurriculumAssignments;

        private EntitySet<Stage> _Stages;

        #region Extensibility Method Definitions
        partial void OnLoaded();
        partial void OnValidate(System.Data.Linq.ChangeAction action);
        partial void OnCreated();
        partial void OnIdChanging(int value);
        partial void OnIdChanged();
        partial void OnNameChanging(string value);
        partial void OnNameChanged();
        partial void OnCreatedChanging(System.DateTime value);
        partial void OnCreatedChanged();
        partial void OnUpdatedChanging(System.DateTime value);
        partial void OnUpdatedChanged();
        partial void OnOwnerChanging(string value);
        partial void OnOwnerChanged();
        partial void OnIsDeletedChanging(bool value);
        partial void OnIsDeletedChanged();
        partial void OnIsValidChanging(bool value);
        partial void OnIsValidChanged();
        #endregion

        public Curriculum()
        {
            this._CurriculumAssignments = new EntitySet<CurriculumAssignment>(new Action<CurriculumAssignment>(this.attach_CurriculumAssignments), new Action<CurriculumAssignment>(this.detach_CurriculumAssignments));
            this._Stages = new EntitySet<Stage>(new Action<Stage>(this.attach_Stages), new Action<Stage>(this.detach_Stages));
            OnCreated();
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Id", AutoSync = AutoSync.OnInsert, DbType = "Int NOT NULL IDENTITY", IsPrimaryKey = true, IsDbGenerated = true)]
        public int Id
        {
            get
            {
                return this._Id;
            }
            set
            {
                if ((this._Id != value))
                {
                    this.OnIdChanging(value);
                    this.SendPropertyChanging();
                    this._Id = value;
                    this.SendPropertyChanged("Id");
                    this.OnIdChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Name", DbType = "NVarChar(50) NOT NULL", CanBeNull = false)]
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

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Created", DbType = "DateTime NOT NULL")]
        public System.DateTime Created
        {
            get
            {
                return this._Created;
            }
            set
            {
                if ((this._Created != value))
                {
                    this.OnCreatedChanging(value);
                    this.SendPropertyChanging();
                    this._Created = value;
                    this.SendPropertyChanged("Created");
                    this.OnCreatedChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Updated", DbType = "DateTime NOT NULL")]
        public System.DateTime Updated
        {
            get
            {
                return this._Updated;
            }
            set
            {
                if ((this._Updated != value))
                {
                    this.OnUpdatedChanging(value);
                    this.SendPropertyChanging();
                    this._Updated = value;
                    this.SendPropertyChanged("Updated");
                    this.OnUpdatedChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Owner", DbType = "NVarChar(100) NOT NULL", CanBeNull = false)]
        public string Owner
        {
            get
            {
                return this._Owner;
            }
            set
            {
                if ((this._Owner != value))
                {
                    this.OnOwnerChanging(value);
                    this.SendPropertyChanging();
                    this._Owner = value;
                    this.SendPropertyChanged("Owner");
                    this.OnOwnerChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_IsDeleted", DbType = "Bit NOT NULL")]
        public bool IsDeleted
        {
            get
            {
                return this._IsDeleted;
            }
            set
            {
                if ((this._IsDeleted != value))
                {
                    this.OnIsDeletedChanging(value);
                    this.SendPropertyChanging();
                    this._IsDeleted = value;
                    this.SendPropertyChanged("IsDeleted");
                    this.OnIsDeletedChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_IsValid", DbType = "Bit NOT NULL")]
        public bool IsValid
        {
            get
            {
                return this._IsValid;
            }
            set
            {
                if ((this._IsValid != value))
                {
                    this.OnIsValidChanging(value);
                    this.SendPropertyChanging();
                    this._IsValid = value;
                    this.SendPropertyChanged("IsValid");
                    this.OnIsValidChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.AssociationAttribute(Name = "Curriculum_CurriculumAssignment", Storage = "_CurriculumAssignments", ThisKey = "Id", OtherKey = "CurriculumRef")]
        public EntitySet<CurriculumAssignment> CurriculumAssignments
        {
            get
            {
                return this._CurriculumAssignments;
            }
            set
            {
                this._CurriculumAssignments.Assign(value);
            }
        }

        [global::System.Data.Linq.Mapping.AssociationAttribute(Name = "Curriculum_Stage", Storage = "_Stages", ThisKey = "Id", OtherKey = "CurriculumRef")]
        public EntitySet<Stage> Stages
        {
            get
            {
                return this._Stages;
            }
            set
            {
                this._Stages.Assign(value);
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

        private void attach_CurriculumAssignments(CurriculumAssignment entity)
        {
            this.SendPropertyChanging();
            entity.Curriculum = this;
        }

        private void detach_CurriculumAssignments(CurriculumAssignment entity)
        {
            this.SendPropertyChanging();
            entity.Curriculum = null;
        }

        private void attach_Stages(Stage entity)
        {
            this.SendPropertyChanging();
            entity.Curriculum = this;
        }

        private void detach_Stages(Stage entity)
        {
            this.SendPropertyChanging();
            entity.Curriculum = null;
        }
    }

    [global::System.Data.Linq.Mapping.TableAttribute(Name = "dbo.Stages")]
    public partial class Stage : INotifyPropertyChanging, INotifyPropertyChanged
    {

        private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);

        private int _Id;

        private string _Name;

        private System.DateTime _Created;

        private System.DateTime _Updated;

        private int _CurriculumRef;

        private bool _IsDeleted;

        private EntitySet<Theme> _Themes;

        private EntityRef<Curriculum> _Curriculum;

        #region Extensibility Method Definitions
        partial void OnLoaded();
        partial void OnValidate(System.Data.Linq.ChangeAction action);
        partial void OnCreated();
        partial void OnIdChanging(int value);
        partial void OnIdChanged();
        partial void OnNameChanging(string value);
        partial void OnNameChanged();
        partial void OnCreatedChanging(System.DateTime value);
        partial void OnCreatedChanged();
        partial void OnUpdatedChanging(System.DateTime value);
        partial void OnUpdatedChanged();
        partial void OnCurriculumRefChanging(int value);
        partial void OnCurriculumRefChanged();
        partial void OnIsDeletedChanging(bool value);
        partial void OnIsDeletedChanged();
        #endregion

        public Stage()
        {
            this._Themes = new EntitySet<Theme>(new Action<Theme>(this.attach_Themes), new Action<Theme>(this.detach_Themes));
            this._Curriculum = default(EntityRef<Curriculum>);
            OnCreated();
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Id", AutoSync = AutoSync.OnInsert, DbType = "Int NOT NULL IDENTITY", IsPrimaryKey = true, IsDbGenerated = true)]
        public int Id
        {
            get
            {
                return this._Id;
            }
            set
            {
                if ((this._Id != value))
                {
                    this.OnIdChanging(value);
                    this.SendPropertyChanging();
                    this._Id = value;
                    this.SendPropertyChanged("Id");
                    this.OnIdChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Name", DbType = "NVarChar(50) NOT NULL", CanBeNull = false)]
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

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Created", DbType = "DateTime NOT NULL")]
        public System.DateTime Created
        {
            get
            {
                return this._Created;
            }
            set
            {
                if ((this._Created != value))
                {
                    this.OnCreatedChanging(value);
                    this.SendPropertyChanging();
                    this._Created = value;
                    this.SendPropertyChanged("Created");
                    this.OnCreatedChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Updated", DbType = "DateTime NOT NULL")]
        public System.DateTime Updated
        {
            get
            {
                return this._Updated;
            }
            set
            {
                if ((this._Updated != value))
                {
                    this.OnUpdatedChanging(value);
                    this.SendPropertyChanging();
                    this._Updated = value;
                    this.SendPropertyChanged("Updated");
                    this.OnUpdatedChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_CurriculumRef", DbType = "Int NOT NULL")]
        public int CurriculumRef
        {
            get
            {
                return this._CurriculumRef;
            }
            set
            {
                if ((this._CurriculumRef != value))
                {
                    if (this._Curriculum.HasLoadedOrAssignedValue)
                    {
                        throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
                    }
                    this.OnCurriculumRefChanging(value);
                    this.SendPropertyChanging();
                    this._CurriculumRef = value;
                    this.SendPropertyChanged("CurriculumRef");
                    this.OnCurriculumRefChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_IsDeleted", DbType = "Bit NOT NULL")]
        public bool IsDeleted
        {
            get
            {
                return this._IsDeleted;
            }
            set
            {
                if ((this._IsDeleted != value))
                {
                    this.OnIsDeletedChanging(value);
                    this.SendPropertyChanging();
                    this._IsDeleted = value;
                    this.SendPropertyChanged("IsDeleted");
                    this.OnIsDeletedChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.AssociationAttribute(Name = "Stage_Theme", Storage = "_Themes", ThisKey = "Id", OtherKey = "StageRef")]
        public EntitySet<Theme> Themes
        {
            get
            {
                return this._Themes;
            }
            set
            {
                this._Themes.Assign(value);
            }
        }

        [global::System.Data.Linq.Mapping.AssociationAttribute(Name = "Curriculum_Stage", Storage = "_Curriculum", ThisKey = "CurriculumRef", OtherKey = "Id", IsForeignKey = true)]
        public Curriculum Curriculum
        {
            get
            {
                return this._Curriculum.Entity;
            }
            set
            {
                Curriculum previousValue = this._Curriculum.Entity;
                if (((previousValue != value)
                            || (this._Curriculum.HasLoadedOrAssignedValue == false)))
                {
                    this.SendPropertyChanging();
                    if ((previousValue != null))
                    {
                        this._Curriculum.Entity = null;
                        previousValue.Stages.Remove(this);
                    }
                    this._Curriculum.Entity = value;
                    if ((value != null))
                    {
                        value.Stages.Add(this);
                        this._CurriculumRef = value.Id;
                    }
                    else
                    {
                        this._CurriculumRef = default(int);
                    }
                    this.SendPropertyChanged("Curriculum");
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

        private void attach_Themes(Theme entity)
        {
            this.SendPropertyChanging();
            entity.Stage = this;
        }

        private void detach_Themes(Theme entity)
        {
            this.SendPropertyChanging();
            entity.Stage = null;
        }
    }

    [global::System.Data.Linq.Mapping.TableAttribute(Name = "dbo.Nodes")]
    public partial class Node : INotifyPropertyChanging, INotifyPropertyChanged
    {

        private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);

        private int _Id;

        private string _Name;

        private int _CourseId;

        private System.Nullable<int> _ParentId;

        private bool _IsFolder;

        private int _Position;

        private System.Xml.Linq.XElement _Sequencing;

        private EntitySet<Node> _Nodes;

        private EntityRef<Node> _Node1;

        private EntityRef<Course> _Course;

        #region Extensibility Method Definitions
        partial void OnLoaded();
        partial void OnValidate(System.Data.Linq.ChangeAction action);
        partial void OnCreated();
        partial void OnIdChanging(int value);
        partial void OnIdChanged();
        partial void OnNameChanging(string value);
        partial void OnNameChanged();
        partial void OnCourseIdChanging(int value);
        partial void OnCourseIdChanged();
        partial void OnParentIdChanging(System.Nullable<int> value);
        partial void OnParentIdChanged();
        partial void OnIsFolderChanging(bool value);
        partial void OnIsFolderChanged();
        partial void OnPositionChanging(int value);
        partial void OnPositionChanged();
        partial void OnSequencingChanging(System.Xml.Linq.XElement value);
        partial void OnSequencingChanged();
        #endregion

        public Node()
        {
            this._Nodes = new EntitySet<Node>(new Action<Node>(this.attach_Nodes), new Action<Node>(this.detach_Nodes));
            this._Node1 = default(EntityRef<Node>);
            this._Course = default(EntityRef<Course>);
            OnCreated();
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Id", AutoSync = AutoSync.OnInsert, DbType = "Int NOT NULL IDENTITY", IsPrimaryKey = true, IsDbGenerated = true)]
        public int Id
        {
            get
            {
                return this._Id;
            }
            set
            {
                if ((this._Id != value))
                {
                    this.OnIdChanging(value);
                    this.SendPropertyChanging();
                    this._Id = value;
                    this.SendPropertyChanged("Id");
                    this.OnIdChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Name", DbType = "NVarChar(50) NOT NULL", CanBeNull = false)]
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

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_CourseId", DbType = "Int NOT NULL")]
        public int CourseId
        {
            get
            {
                return this._CourseId;
            }
            set
            {
                if ((this._CourseId != value))
                {
                    if (this._Course.HasLoadedOrAssignedValue)
                    {
                        throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
                    }
                    this.OnCourseIdChanging(value);
                    this.SendPropertyChanging();
                    this._CourseId = value;
                    this.SendPropertyChanged("CourseId");
                    this.OnCourseIdChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_ParentId", DbType = "Int")]
        public System.Nullable<int> ParentId
        {
            get
            {
                return this._ParentId;
            }
            set
            {
                if ((this._ParentId != value))
                {
                    if (this._Node1.HasLoadedOrAssignedValue)
                    {
                        throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
                    }
                    this.OnParentIdChanging(value);
                    this.SendPropertyChanging();
                    this._ParentId = value;
                    this.SendPropertyChanged("ParentId");
                    this.OnParentIdChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_IsFolder", DbType = "Bit NOT NULL")]
        public bool IsFolder
        {
            get
            {
                return this._IsFolder;
            }
            set
            {
                if ((this._IsFolder != value))
                {
                    this.OnIsFolderChanging(value);
                    this.SendPropertyChanging();
                    this._IsFolder = value;
                    this.SendPropertyChanged("IsFolder");
                    this.OnIsFolderChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Position", DbType = "Int NOT NULL")]
        public int Position
        {
            get
            {
                return this._Position;
            }
            set
            {
                if ((this._Position != value))
                {
                    this.OnPositionChanging(value);
                    this.SendPropertyChanging();
                    this._Position = value;
                    this.SendPropertyChanged("Position");
                    this.OnPositionChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Sequencing", DbType = "Xml", UpdateCheck = UpdateCheck.Never)]
        public System.Xml.Linq.XElement Sequencing
        {
            get
            {
                return this._Sequencing;
            }
            set
            {
                if ((this._Sequencing != value))
                {
                    this.OnSequencingChanging(value);
                    this.SendPropertyChanging();
                    this._Sequencing = value;
                    this.SendPropertyChanged("Sequencing");
                    this.OnSequencingChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.AssociationAttribute(Name = "Node_Node", Storage = "_Nodes", ThisKey = "Id", OtherKey = "ParentId")]
        public EntitySet<Node> Nodes
        {
            get
            {
                return this._Nodes;
            }
            set
            {
                this._Nodes.Assign(value);
            }
        }

        [global::System.Data.Linq.Mapping.AssociationAttribute(Name = "Node_Node", Storage = "_Node1", ThisKey = "ParentId", OtherKey = "Id", IsForeignKey = true)]
        public Node Node1
        {
            get
            {
                return this._Node1.Entity;
            }
            set
            {
                Node previousValue = this._Node1.Entity;
                if (((previousValue != value)
                            || (this._Node1.HasLoadedOrAssignedValue == false)))
                {
                    this.SendPropertyChanging();
                    if ((previousValue != null))
                    {
                        this._Node1.Entity = null;
                        previousValue.Nodes.Remove(this);
                    }
                    this._Node1.Entity = value;
                    if ((value != null))
                    {
                        value.Nodes.Add(this);
                        this._ParentId = value.Id;
                    }
                    else
                    {
                        this._ParentId = default(Nullable<int>);
                    }
                    this.SendPropertyChanged("Node1");
                }
            }
        }

        [global::System.Data.Linq.Mapping.AssociationAttribute(Name = "Course_Node", Storage = "_Course", ThisKey = "CourseId", OtherKey = "Id", IsForeignKey = true)]
        public Course Course
        {
            get
            {
                return this._Course.Entity;
            }
            set
            {
                Course previousValue = this._Course.Entity;
                if (((previousValue != value)
                            || (this._Course.HasLoadedOrAssignedValue == false)))
                {
                    this.SendPropertyChanging();
                    if ((previousValue != null))
                    {
                        this._Course.Entity = null;
                        previousValue.Nodes.Remove(this);
                    }
                    this._Course.Entity = value;
                    if ((value != null))
                    {
                        value.Nodes.Add(this);
                        this._CourseId = value.Id;
                    }
                    else
                    {
                        this._CourseId = default(int);
                    }
                    this.SendPropertyChanged("Course");
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

        private void attach_Nodes(Node entity)
        {
            this.SendPropertyChanging();
            entity.Node1 = this;
        }

        private void detach_Nodes(Node entity)
        {
            this.SendPropertyChanging();
            entity.Node1 = null;
        }
    }

    [global::System.Data.Linq.Mapping.TableAttribute(Name = "dbo.Courses")]
    public partial class Course : INotifyPropertyChanging, INotifyPropertyChanged
    {

        private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);

        private int _Id;

        private string _Name;

        private string _Owner;

        private System.DateTime _Created;

        private System.DateTime _Updated;

        private bool _Deleted;

        private System.Nullable<bool> _Locked;

        private System.Xml.Linq.XElement _Sequencing;

        private EntitySet<CourseUser> _CourseUsers;

        private EntitySet<Node> _Nodes;

        #region Extensibility Method Definitions
        partial void OnLoaded();
        partial void OnValidate(System.Data.Linq.ChangeAction action);
        partial void OnCreated();
        partial void OnIdChanging(int value);
        partial void OnIdChanged();
        partial void OnNameChanging(string value);
        partial void OnNameChanged();
        partial void OnOwnerChanging(string value);
        partial void OnOwnerChanged();
        partial void OnCreatedChanging(System.DateTime value);
        partial void OnCreatedChanged();
        partial void OnUpdatedChanging(System.DateTime value);
        partial void OnUpdatedChanged();
        partial void OnDeletedChanging(bool value);
        partial void OnDeletedChanged();
        partial void OnLockedChanging(System.Nullable<bool> value);
        partial void OnLockedChanged();
        partial void OnSequencingChanging(System.Xml.Linq.XElement value);
        partial void OnSequencingChanged();
        #endregion

        public Course()
        {
            this._CourseUsers = new EntitySet<CourseUser>(new Action<CourseUser>(this.attach_CourseUsers), new Action<CourseUser>(this.detach_CourseUsers));
            this._Nodes = new EntitySet<Node>(new Action<Node>(this.attach_Nodes), new Action<Node>(this.detach_Nodes));
            OnCreated();
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Id", AutoSync = AutoSync.OnInsert, DbType = "Int NOT NULL IDENTITY", IsPrimaryKey = true, IsDbGenerated = true)]
        public int Id
        {
            get
            {
                return this._Id;
            }
            set
            {
                if ((this._Id != value))
                {
                    this.OnIdChanging(value);
                    this.SendPropertyChanging();
                    this._Id = value;
                    this.SendPropertyChanged("Id");
                    this.OnIdChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Name", DbType = "NVarChar(50) NOT NULL", CanBeNull = false)]
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

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Owner", DbType = "NVarChar(50)")]
        public string Owner
        {
            get
            {
                return this._Owner;
            }
            set
            {
                if ((this._Owner != value))
                {
                    this.OnOwnerChanging(value);
                    this.SendPropertyChanging();
                    this._Owner = value;
                    this.SendPropertyChanged("Owner");
                    this.OnOwnerChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Created", DbType = "DateTime NOT NULL")]
        public System.DateTime Created
        {
            get
            {
                return this._Created;
            }
            set
            {
                if ((this._Created != value))
                {
                    this.OnCreatedChanging(value);
                    this.SendPropertyChanging();
                    this._Created = value;
                    this.SendPropertyChanged("Created");
                    this.OnCreatedChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Updated", DbType = "DateTime NOT NULL")]
        public System.DateTime Updated
        {
            get
            {
                return this._Updated;
            }
            set
            {
                if ((this._Updated != value))
                {
                    this.OnUpdatedChanging(value);
                    this.SendPropertyChanging();
                    this._Updated = value;
                    this.SendPropertyChanged("Updated");
                    this.OnUpdatedChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Deleted", DbType = "Bit NOT NULL")]
        public bool Deleted
        {
            get
            {
                return this._Deleted;
            }
            set
            {
                if ((this._Deleted != value))
                {
                    this.OnDeletedChanging(value);
                    this.SendPropertyChanging();
                    this._Deleted = value;
                    this.SendPropertyChanged("Deleted");
                    this.OnDeletedChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Locked", DbType = "Bit")]
        public System.Nullable<bool> Locked
        {
            get
            {
                return this._Locked;
            }
            set
            {
                if ((this._Locked != value))
                {
                    this.OnLockedChanging(value);
                    this.SendPropertyChanging();
                    this._Locked = value;
                    this.SendPropertyChanged("Locked");
                    this.OnLockedChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Sequencing", DbType = "Xml", UpdateCheck = UpdateCheck.Never)]
        public System.Xml.Linq.XElement Sequencing
        {
            get
            {
                return this._Sequencing;
            }
            set
            {
                if ((this._Sequencing != value))
                {
                    this.OnSequencingChanging(value);
                    this.SendPropertyChanging();
                    this._Sequencing = value;
                    this.SendPropertyChanged("Sequencing");
                    this.OnSequencingChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.AssociationAttribute(Name = "Course_CourseUser", Storage = "_CourseUsers", ThisKey = "Id", OtherKey = "CourseRef")]
        public EntitySet<CourseUser> CourseUsers
        {
            get
            {
                return this._CourseUsers;
            }
            set
            {
                this._CourseUsers.Assign(value);
            }
        }

        [global::System.Data.Linq.Mapping.AssociationAttribute(Name = "Course_Node", Storage = "_Nodes", ThisKey = "Id", OtherKey = "CourseId")]
        public EntitySet<Node> Nodes
        {
            get
            {
                return this._Nodes;
            }
            set
            {
                this._Nodes.Assign(value);
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

        private void attach_CourseUsers(CourseUser entity)
        {
            this.SendPropertyChanging();
            entity.Course = this;
        }

        private void detach_CourseUsers(CourseUser entity)
        {
            this.SendPropertyChanging();
            entity.Course = null;
        }

        private void attach_Nodes(Node entity)
        {
            this.SendPropertyChanging();
            entity.Course = this;
        }

        private void detach_Nodes(Node entity)
        {
            this.SendPropertyChanging();
            entity.Course = null;
        }
    }

    [global::System.Data.Linq.Mapping.TableAttribute(Name = "dbo.ForecastingTree")]
    public partial class ForecastingTree : INotifyPropertyChanging, INotifyPropertyChanged
    {

        private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);

        private int _Id;

        private string _Name;

        private System.Guid _UserRef;

        private System.DateTime _Created;

        private System.DateTime _Updated;

        private bool _IsDeleted;

        #region Extensibility Method Definitions
        partial void OnLoaded();
        partial void OnValidate(System.Data.Linq.ChangeAction action);
        partial void OnCreated();
        partial void OnIdChanging(int value);
        partial void OnIdChanged();
        partial void OnNameChanging(string value);
        partial void OnNameChanged();
        partial void OnUserRefChanging(System.Guid value);
        partial void OnUserRefChanged();
        partial void OnCreatedChanging(System.DateTime value);
        partial void OnCreatedChanged();
        partial void OnUpdatedChanging(System.DateTime value);
        partial void OnUpdatedChanged();
        partial void OnIsDeletedChanging(bool value);
        partial void OnIsDeletedChanged();
        #endregion

        public ForecastingTree()
        {
            OnCreated();
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Id", AutoSync = AutoSync.OnInsert, DbType = "Int NOT NULL IDENTITY", IsPrimaryKey = true, IsDbGenerated = true)]
        public int Id
        {
            get
            {
                return this._Id;
            }
            set
            {
                if ((this._Id != value))
                {
                    this.OnIdChanging(value);
                    this.SendPropertyChanging();
                    this._Id = value;
                    this.SendPropertyChanged("Id");
                    this.OnIdChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Name", DbType = "NVarChar(50) NOT NULL", CanBeNull = false)]
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

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_UserRef", DbType = "UniqueIdentifier NOT NULL")]
        public System.Guid UserRef
        {
            get
            {
                return this._UserRef;
            }
            set
            {
                if ((this._UserRef != value))
                {
                    this.OnUserRefChanging(value);
                    this.SendPropertyChanging();
                    this._UserRef = value;
                    this.SendPropertyChanged("UserRef");
                    this.OnUserRefChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Created", DbType = "DateTime NOT NULL")]
        public System.DateTime Created
        {
            get
            {
                return this._Created;
            }
            set
            {
                if ((this._Created != value))
                {
                    this.OnCreatedChanging(value);
                    this.SendPropertyChanging();
                    this._Created = value;
                    this.SendPropertyChanged("Created");
                    this.OnCreatedChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Updated", DbType = "DateTime NOT NULL")]
        public System.DateTime Updated
        {
            get
            {
                return this._Updated;
            }
            set
            {
                if ((this._Updated != value))
                {
                    this.OnUpdatedChanging(value);
                    this.SendPropertyChanging();
                    this._Updated = value;
                    this.SendPropertyChanged("Updated");
                    this.OnUpdatedChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_IsDeleted", DbType = "Bit NOT NULL")]
        public bool IsDeleted
        {
            get
            {
                return this._IsDeleted;
            }
            set
            {
                if ((this._IsDeleted != value))
                {
                    this.OnIsDeletedChanging(value);
                    this.SendPropertyChanging();
                    this._IsDeleted = value;
                    this.SendPropertyChanged("IsDeleted");
                    this.OnIsDeletedChanged();
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

    [global::System.Data.Linq.Mapping.TableAttribute(Name = "dbo.UserRoles")]
    public partial class UserRole : INotifyPropertyChanging, INotifyPropertyChanged
    {

        private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);

        private System.Guid _UserRef;

        private int _RoleRef;

        private EntityRef<User> _User;

        #region Extensibility Method Definitions
        partial void OnLoaded();
        partial void OnValidate(System.Data.Linq.ChangeAction action);
        partial void OnCreated();
        partial void OnUserRefChanging(System.Guid value);
        partial void OnUserRefChanged();
        partial void OnRoleRefChanging(int value);
        partial void OnRoleRefChanged();
        #endregion

        public UserRole()
        {
            this._User = default(EntityRef<User>);
            OnCreated();
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_UserRef", DbType = "UniqueIdentifier NOT NULL", IsPrimaryKey = true)]
        public System.Guid UserRef
        {
            get
            {
                return this._UserRef;
            }
            set
            {
                if ((this._UserRef != value))
                {
                    if (this._User.HasLoadedOrAssignedValue)
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

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_RoleRef", DbType = "Int NOT NULL", IsPrimaryKey = true)]
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
                    this.OnRoleRefChanging(value);
                    this.SendPropertyChanging();
                    this._RoleRef = value;
                    this.SendPropertyChanged("RoleRef");
                    this.OnRoleRefChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.AssociationAttribute(Name = "User_UserRole", Storage = "_User", ThisKey = "UserRef", OtherKey = "Id", IsForeignKey = true)]
        public User User
        {
            get
            {
                return this._User.Entity;
            }
            set
            {
                User previousValue = this._User.Entity;
                if (((previousValue != value)
                            || (this._User.HasLoadedOrAssignedValue == false)))
                {
                    this.SendPropertyChanging();
                    if ((previousValue != null))
                    {
                        this._User.Entity = null;
                        previousValue.UserRoles.Remove(this);
                    }
                    this._User.Entity = value;
                    if ((value != null))
                    {
                        value.UserRoles.Add(this);
                        this._UserRef = value.Id;
                    }
                    else
                    {
                        this._UserRef = default(System.Guid);
                    }
                    this.SendPropertyChanged("User");
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

    [global::System.Data.Linq.Mapping.TableAttribute(Name = "dbo.[User]")]
    public partial class User : INotifyPropertyChanging, INotifyPropertyChanged
    {

        private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);

        private System.Guid _Id;

        private string _Username;

        private string _Password;

        private string _Email;

        private string _OpenId;

        private string _Name;

        private bool _IsApproved;

        private bool _Deleted;

        private System.DateTime _CreationDate;

        private System.Nullable<System.Guid> _ApprovedBy;

        private string _UserId;

        private EntitySet<GroupUser> _GroupUsers;

        private EntitySet<UserRole> _UserRoles;

        private EntitySet<User> _Users;

        private EntityRef<User> _User1;

        #region Extensibility Method Definitions
        partial void OnLoaded();
        partial void OnValidate(System.Data.Linq.ChangeAction action);
        partial void OnCreated();
        partial void OnIdChanging(System.Guid value);
        partial void OnIdChanged();
        partial void OnUsernameChanging(string value);
        partial void OnUsernameChanged();
        partial void OnPasswordChanging(string value);
        partial void OnPasswordChanged();
        partial void OnEmailChanging(string value);
        partial void OnEmailChanged();
        partial void OnOpenIdChanging(string value);
        partial void OnOpenIdChanged();
        partial void OnNameChanging(string value);
        partial void OnNameChanged();
        partial void OnIsApprovedChanging(bool value);
        partial void OnIsApprovedChanged();
        partial void OnDeletedChanging(bool value);
        partial void OnDeletedChanged();
        partial void OnCreationDateChanging(System.DateTime value);
        partial void OnCreationDateChanged();
        partial void OnApprovedByChanging(System.Nullable<System.Guid> value);
        partial void OnApprovedByChanged();
        partial void OnUserIdChanging(string value);
        partial void OnUserIdChanged();
        #endregion

        public User()
        {
            this._GroupUsers = new EntitySet<GroupUser>(new Action<GroupUser>(this.attach_GroupUsers), new Action<GroupUser>(this.detach_GroupUsers));
            this._UserRoles = new EntitySet<UserRole>(new Action<UserRole>(this.attach_UserRoles), new Action<UserRole>(this.detach_UserRoles));
            this._Users = new EntitySet<User>(new Action<User>(this.attach_Users), new Action<User>(this.detach_Users));
            this._User1 = default(EntityRef<User>);
            OnCreated();
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Id", DbType = "UniqueIdentifier NOT NULL", IsPrimaryKey = true)]
        public System.Guid Id
        {
            get
            {
                return this._Id;
            }
            set
            {
                if ((this._Id != value))
                {
                    this.OnIdChanging(value);
                    this.SendPropertyChanging();
                    this._Id = value;
                    this.SendPropertyChanged("Id");
                    this.OnIdChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Username", DbType = "NVarChar(100) NOT NULL", CanBeNull = false)]
        public string Username
        {
            get
            {
                return this._Username;
            }
            set
            {
                if ((this._Username != value))
                {
                    this.OnUsernameChanging(value);
                    this.SendPropertyChanging();
                    this._Username = value;
                    this.SendPropertyChanged("Username");
                    this.OnUsernameChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Password", DbType = "NVarChar(50) NOT NULL", CanBeNull = false)]
        public string Password
        {
            get
            {
                return this._Password;
            }
            set
            {
                if ((this._Password != value))
                {
                    this.OnPasswordChanging(value);
                    this.SendPropertyChanging();
                    this._Password = value;
                    this.SendPropertyChanged("Password");
                    this.OnPasswordChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Email", DbType = "NVarChar(100) NOT NULL", CanBeNull = false)]
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

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_OpenId", DbType = "NVarChar(200) NOT NULL", CanBeNull = false)]
        public string OpenId
        {
            get
            {
                return this._OpenId;
            }
            set
            {
                if ((this._OpenId != value))
                {
                    this.OnOpenIdChanging(value);
                    this.SendPropertyChanging();
                    this._OpenId = value;
                    this.SendPropertyChanged("OpenId");
                    this.OnOpenIdChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Name", DbType = "NVarChar(200) NOT NULL", CanBeNull = false)]
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

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_IsApproved", DbType = "Bit NOT NULL")]
        public bool IsApproved
        {
            get
            {
                return this._IsApproved;
            }
            set
            {
                if ((this._IsApproved != value))
                {
                    this.OnIsApprovedChanging(value);
                    this.SendPropertyChanging();
                    this._IsApproved = value;
                    this.SendPropertyChanged("IsApproved");
                    this.OnIsApprovedChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Deleted", DbType = "Bit NOT NULL")]
        public bool Deleted
        {
            get
            {
                return this._Deleted;
            }
            set
            {
                if ((this._Deleted != value))
                {
                    this.OnDeletedChanging(value);
                    this.SendPropertyChanging();
                    this._Deleted = value;
                    this.SendPropertyChanged("Deleted");
                    this.OnDeletedChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_CreationDate", DbType = "DateTime NOT NULL")]
        public System.DateTime CreationDate
        {
            get
            {
                return this._CreationDate;
            }
            set
            {
                if ((this._CreationDate != value))
                {
                    this.OnCreationDateChanging(value);
                    this.SendPropertyChanging();
                    this._CreationDate = value;
                    this.SendPropertyChanged("CreationDate");
                    this.OnCreationDateChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_ApprovedBy", DbType = "UniqueIdentifier")]
        public System.Nullable<System.Guid> ApprovedBy
        {
            get
            {
                return this._ApprovedBy;
            }
            set
            {
                if ((this._ApprovedBy != value))
                {
                    if (this._User1.HasLoadedOrAssignedValue)
                    {
                        throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
                    }
                    this.OnApprovedByChanging(value);
                    this.SendPropertyChanging();
                    this._ApprovedBy = value;
                    this.SendPropertyChanged("ApprovedBy");
                    this.OnApprovedByChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_UserId", DbType = "NVarChar(100)")]
        public string UserId
        {
            get
            {
                return this._UserId;
            }
            set
            {
                if ((this._UserId != value))
                {
                    this.OnUserIdChanging(value);
                    this.SendPropertyChanging();
                    this._UserId = value;
                    this.SendPropertyChanged("UserId");
                    this.OnUserIdChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.AssociationAttribute(Name = "User_GroupUser", Storage = "_GroupUsers", ThisKey = "Id", OtherKey = "UserRef")]
        public EntitySet<GroupUser> GroupUsers
        {
            get
            {
                return this._GroupUsers;
            }
            set
            {
                this._GroupUsers.Assign(value);
            }
        }

        [global::System.Data.Linq.Mapping.AssociationAttribute(Name = "User_UserRole", Storage = "_UserRoles", ThisKey = "Id", OtherKey = "UserRef")]
        public EntitySet<UserRole> UserRoles
        {
            get
            {
                return this._UserRoles;
            }
            set
            {
                this._UserRoles.Assign(value);
            }
        }

        [global::System.Data.Linq.Mapping.AssociationAttribute(Name = "User_User", Storage = "_Users", ThisKey = "Id", OtherKey = "ApprovedBy")]
        public EntitySet<User> Users
        {
            get
            {
                return this._Users;
            }
            set
            {
                this._Users.Assign(value);
            }
        }

        [global::System.Data.Linq.Mapping.AssociationAttribute(Name = "User_User", Storage = "_User1", ThisKey = "ApprovedBy", OtherKey = "Id", IsForeignKey = true)]
        public User User1
        {
            get
            {
                return this._User1.Entity;
            }
            set
            {
                User previousValue = this._User1.Entity;
                if (((previousValue != value)
                            || (this._User1.HasLoadedOrAssignedValue == false)))
                {
                    this.SendPropertyChanging();
                    if ((previousValue != null))
                    {
                        this._User1.Entity = null;
                        previousValue.Users.Remove(this);
                    }
                    this._User1.Entity = value;
                    if ((value != null))
                    {
                        value.Users.Add(this);
                        this._ApprovedBy = value.Id;
                    }
                    else
                    {
                        this._ApprovedBy = default(Nullable<System.Guid>);
                    }
                    this.SendPropertyChanged("User1");
                }
            }
        }

        public IEnumerable<Role> Roles
        {
            get { return UserRoles.Select(u => (Role)u.RoleRef); }
        }

        public string GroupsLine
        {
            get
            {
                if (GroupUsers.Count > 0)
                    return GroupUsers.Select(g => g.Group.Name).Aggregate((a, b) => a + ", " + b);

                return "";
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

        private void attach_GroupUsers(GroupUser entity)
        {
            this.SendPropertyChanging();
            entity.User = this;
        }

        private void detach_GroupUsers(GroupUser entity)
        {
            this.SendPropertyChanging();
            entity.User = null;
        }

        private void attach_UserRoles(UserRole entity)
        {
            this.SendPropertyChanging();
            entity.User = this;
        }

        private void detach_UserRoles(UserRole entity)
        {
            this.SendPropertyChanging();
            entity.User = null;
        }

        private void attach_Users(User entity)
        {
            this.SendPropertyChanging();
            entity.User1 = this;
        }

        private void detach_Users(User entity)
        {
            this.SendPropertyChanging();
            entity.User1 = null;
        }
    }*/
}
