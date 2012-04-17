﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Linq;
using System.Linq;
using System.Text;
using System.Data.Linq.Mapping;

namespace IUDICO.Common.Models.Shared
{
    [global::System.Data.Linq.Mapping.TableAttribute(Name = "dbo.SharedDisciplines")]
    public partial class SharedDiscipline : INotifyPropertyChanging, INotifyPropertyChanged
    {

        private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);

        private int _DisciplineRef;

        private System.Guid _UserRef;

        private EntityRef<Discipline> _Discipline;

        #region Extensibility Method Definitions
        partial void OnLoaded();
        partial void OnValidate(System.Data.Linq.ChangeAction action);
        partial void OnCreated();
        partial void OnDisciplineRefChanging(int value);
        partial void OnDisciplineRefChanged();
        partial void OnUserRefChanging(System.Guid value);
        partial void OnUserRefChanged();
        #endregion

        public SharedDiscipline()
        {
            this._Discipline = default(EntityRef<Discipline>);
            OnCreated();
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_DisciplineRef", DbType = "Int NOT NULL", IsPrimaryKey = true)]
        public int DisciplineRef
        {
            get
            {
                return this._DisciplineRef;
            }
            set
            {
                if ((this._DisciplineRef != value))
                {
                    if (this._Discipline.HasLoadedOrAssignedValue)
                    {
                        throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
                    }
                    this.OnDisciplineRefChanging(value);
                    this.SendPropertyChanging();
                    this._DisciplineRef = value;
                    this.SendPropertyChanged("DisciplineRef");
                    this.OnDisciplineRefChanged();
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

        [global::System.Data.Linq.Mapping.AssociationAttribute(Name = "Discipline_SharedDiscipline", Storage = "_Discipline", ThisKey = "DisciplineRef", OtherKey = "Id", IsForeignKey = true)]
        public Discipline Discipline
        {
            get
            {
                return this._Discipline.Entity;
            }
            set
            {
                Discipline previousValue = this._Discipline.Entity;
                if (((previousValue != value)
                      || (this._Discipline.HasLoadedOrAssignedValue == false)))
                {
                    this.SendPropertyChanging();
                    if ((previousValue != null))
                    {
                        this._Discipline.Entity = null;
                        previousValue.SharedDisciplines.Remove(this);
                    }
                    this._Discipline.Entity = value;
                    if ((value != null))
                    {
                        value.SharedDisciplines.Add(this);
                        this._DisciplineRef = value.Id;
                    }
                    else
                    {
                        this._DisciplineRef = default(int);
                    }
                    this.SendPropertyChanged("Discipline");
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

    [global::System.Data.Linq.Mapping.TableAttribute(Name = "dbo.Disciplines")]
    public partial class Discipline : INotifyPropertyChanging, INotifyPropertyChanged
    {

        private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);

        private int _Id;

        private string _Name;

        private System.DateTime _Created;

        private System.DateTime _Updated;

        private string _Owner;

        private bool _IsDeleted;

        private bool _IsValid;

        private EntitySet<Curriculum> _Curriculums;

        private EntitySet<Chapter> _Chapters;

        private EntitySet<SharedDiscipline> _SharedDisciplines;

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

        public Discipline()
        {
            this._Curriculums = new EntitySet<Curriculum>(new Action<Curriculum>(this.attach_Curriculums), new Action<Curriculum>(this.detach_Curriculums));
            this._Chapters = new EntitySet<Chapter>(new Action<Chapter>(this.attach_Chapters), new Action<Chapter>(this.detach_Chapters));
            this._SharedDisciplines = new EntitySet<SharedDiscipline>(new Action<SharedDiscipline>(this.attach_SharedDisciplines), new Action<SharedDiscipline>(this.detach_SharedDisciplines));
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

        [global::System.Data.Linq.Mapping.AssociationAttribute(Name = "Discipline_Curriculum", Storage = "_Curriculums", ThisKey = "Id", OtherKey = "DisciplineRef")]
        public EntitySet<Curriculum> Curriculums
        {
            get
            {
                return this._Curriculums;
            }
            set
            {
                this._Curriculums.Assign(value);
            }
        }

        [global::System.Data.Linq.Mapping.AssociationAttribute(Name = "Discipline_Chapter", Storage = "_Chapters", ThisKey = "Id", OtherKey = "DisciplineRef")]
        public EntitySet<Chapter> Chapters
        {
            get
            {
                return this._Chapters;
            }
            set
            {
                this._Chapters.Assign(value);
            }
        }

        [global::System.Data.Linq.Mapping.AssociationAttribute(Name = "Discipline_SharedDiscipline", Storage = "_SharedDisciplines", ThisKey = "Id", OtherKey = "DisciplineRef")]
        public EntitySet<SharedDiscipline> SharedDisciplines
        {
            get
            {
                return this._SharedDisciplines;
            }
            set
            {
                this._SharedDisciplines.Assign(value);
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

        private void attach_Curriculums(Curriculum entity)
        {
            this.SendPropertyChanging();
            entity.Discipline = this;
        }

        private void detach_Curriculums(Curriculum entity)
        {
            this.SendPropertyChanging();
            entity.Discipline = null;
        }

        private void attach_Chapters(Chapter entity)
        {
            this.SendPropertyChanging();
            entity.Discipline = this;
        }

        private void detach_Chapters(Chapter entity)
        {
            this.SendPropertyChanging();
            entity.Discipline = null;
        }

        private void attach_SharedDisciplines(SharedDiscipline entity)
        {
            this.SendPropertyChanging();
            entity.Discipline = this;
        }

        private void detach_SharedDisciplines(SharedDiscipline entity)
        {
            this.SendPropertyChanging();
            entity.Discipline = null;
        }
    }

    [global::System.Data.Linq.Mapping.TableAttribute(Name = "dbo.Chapters")]
    public partial class Chapter : INotifyPropertyChanging, INotifyPropertyChanged
    {

        private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);

        private int _Id;

        private int _DisciplineRef;

        private string _Name;

        private System.DateTime _Created;

        private System.DateTime _Updated;

        private bool _IsDeleted;

        private EntitySet<CurriculumChapter> _CurriculumChapters;

        private EntitySet<Topic> _Topics;

        private EntityRef<Discipline> _Discipline;

        #region Extensibility Method Definitions
        partial void OnLoaded();
        partial void OnValidate(System.Data.Linq.ChangeAction action);
        partial void OnCreated();
        partial void OnIdChanging(int value);
        partial void OnIdChanged();
        partial void OnDisciplineRefChanging(int value);
        partial void OnDisciplineRefChanged();
        partial void OnNameChanging(string value);
        partial void OnNameChanged();
        partial void OnCreatedChanging(System.DateTime value);
        partial void OnCreatedChanged();
        partial void OnUpdatedChanging(System.DateTime value);
        partial void OnUpdatedChanged();
        partial void OnIsDeletedChanging(bool value);
        partial void OnIsDeletedChanged();
        #endregion

        public Chapter()
        {
            this._CurriculumChapters = new EntitySet<CurriculumChapter>(new Action<CurriculumChapter>(this.attach_CurriculumChapters), new Action<CurriculumChapter>(this.detach_CurriculumChapters));
            this._Topics = new EntitySet<Topic>(new Action<Topic>(this.attach_Topics), new Action<Topic>(this.detach_Topics));
            this._Discipline = default(EntityRef<Discipline>);
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

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_DisciplineRef", DbType = "Int NOT NULL")]
        public int DisciplineRef
        {
            get
            {
                return this._DisciplineRef;
            }
            set
            {
                if ((this._DisciplineRef != value))
                {
                    if (this._Discipline.HasLoadedOrAssignedValue)
                    {
                        throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
                    }
                    this.OnDisciplineRefChanging(value);
                    this.SendPropertyChanging();
                    this._DisciplineRef = value;
                    this.SendPropertyChanged("DisciplineRef");
                    this.OnDisciplineRefChanged();
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

        [global::System.Data.Linq.Mapping.AssociationAttribute(Name = "Chapter_CurriculumChapter", Storage = "_CurriculumChapters", ThisKey = "Id", OtherKey = "ChapterRef")]
        public EntitySet<CurriculumChapter> CurriculumChapters
        {
            get
            {
                return this._CurriculumChapters;
            }
            set
            {
                this._CurriculumChapters.Assign(value);
            }
        }

        [global::System.Data.Linq.Mapping.AssociationAttribute(Name = "Chapter_Topic", Storage = "_Topics", ThisKey = "Id", OtherKey = "ChapterRef")]
        public EntitySet<Topic> Topics
        {
            get
            {
                return this._Topics;
            }
            set
            {
                this._Topics.Assign(value);
            }
        }

        [global::System.Data.Linq.Mapping.AssociationAttribute(Name = "Discipline_Chapter", Storage = "_Discipline", ThisKey = "DisciplineRef", OtherKey = "Id", IsForeignKey = true)]
        public Discipline Discipline
        {
            get
            {
                return this._Discipline.Entity;
            }
            set
            {
                Discipline previousValue = this._Discipline.Entity;
                if (((previousValue != value)
                            || (this._Discipline.HasLoadedOrAssignedValue == false)))
                {
                    this.SendPropertyChanging();
                    if ((previousValue != null))
                    {
                        this._Discipline.Entity = null;
                        previousValue.Chapters.Remove(this);
                    }
                    this._Discipline.Entity = value;
                    if ((value != null))
                    {
                        value.Chapters.Add(this);
                        this._DisciplineRef = value.Id;
                    }
                    else
                    {
                        this._DisciplineRef = default(int);
                    }
                    this.SendPropertyChanged("Discipline");
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

        private void attach_CurriculumChapters(CurriculumChapter entity)
        {
            this.SendPropertyChanging();
            entity.Chapter = this;
        }

        private void detach_CurriculumChapters(CurriculumChapter entity)
        {
            this.SendPropertyChanging();
            entity.Chapter = null;
        }

        private void attach_Topics(Topic entity)
        {
            this.SendPropertyChanging();
            entity.Chapter = this;
        }

        private void detach_Topics(Topic entity)
        {
            this.SendPropertyChanging();
            entity.Chapter = null;
        }
    }

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

    [global::System.Data.Linq.Mapping.TableAttribute(Name = "dbo.CurriculumChapters")]
    public partial class CurriculumChapter : INotifyPropertyChanging, INotifyPropertyChanged
    {

        private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);

        private int _Id;

        private int _CurriculumRef;

        private int _ChapterRef;

        private System.Nullable<System.DateTime> _StartDate;

        private System.Nullable<System.DateTime> _EndDate;

        private bool _IsDeleted;

        private EntitySet<CurriculumChapterTopic> _CurriculumChapterTopics;

        private EntityRef<Chapter> _Chapter;

        private EntityRef<Curriculum> _Curriculum;

        #region Extensibility Method Definitions
        partial void OnLoaded();
        partial void OnValidate(System.Data.Linq.ChangeAction action);
        partial void OnCreated();
        partial void OnIdChanging(int value);
        partial void OnIdChanged();
        partial void OnCurriculumRefChanging(int value);
        partial void OnCurriculumRefChanged();
        partial void OnChapterRefChanging(int value);
        partial void OnChapterRefChanged();
        partial void OnStartDateChanging(System.Nullable<System.DateTime> value);
        partial void OnStartDateChanged();
        partial void OnEndDateChanging(System.Nullable<System.DateTime> value);
        partial void OnEndDateChanged();
        partial void OnIsDeletedChanging(bool value);
        partial void OnIsDeletedChanged();
        #endregion

        public CurriculumChapter()
        {
            this._CurriculumChapterTopics = new EntitySet<CurriculumChapterTopic>(new Action<CurriculumChapterTopic>(this.attach_CurriculumChapterTopics), new Action<CurriculumChapterTopic>(this.detach_CurriculumChapterTopics));
            this._Chapter = default(EntityRef<Chapter>);
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

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_ChapterRef", DbType = "Int NOT NULL")]
        public int ChapterRef
        {
            get
            {
                return this._ChapterRef;
            }
            set
            {
                if ((this._ChapterRef != value))
                {
                    if (this._Chapter.HasLoadedOrAssignedValue)
                    {
                        throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
                    }
                    this.OnChapterRefChanging(value);
                    this.SendPropertyChanging();
                    this._ChapterRef = value;
                    this.SendPropertyChanged("ChapterRef");
                    this.OnChapterRefChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_StartDate", DbType = "DateTime")]
        public System.Nullable<System.DateTime> StartDate
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

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_EndDate", DbType = "DateTime")]
        public System.Nullable<System.DateTime> EndDate
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

        [global::System.Data.Linq.Mapping.AssociationAttribute(Name = "CurriculumChapter_CurriculumChapterTopic", Storage = "_CurriculumChapterTopics", ThisKey = "Id", OtherKey = "CurriculumChapterRef")]
        public EntitySet<CurriculumChapterTopic> CurriculumChapterTopics
        {
            get
            {
                return this._CurriculumChapterTopics;
            }
            set
            {
                this._CurriculumChapterTopics.Assign(value);
            }
        }

        [global::System.Data.Linq.Mapping.AssociationAttribute(Name = "Chapter_CurriculumChapter", Storage = "_Chapter", ThisKey = "ChapterRef", OtherKey = "Id", IsForeignKey = true)]
        public Chapter Chapter
        {
            get
            {
                return this._Chapter.Entity;
            }
            set
            {
                Chapter previousValue = this._Chapter.Entity;
                if (((previousValue != value)
                            || (this._Chapter.HasLoadedOrAssignedValue == false)))
                {
                    this.SendPropertyChanging();
                    if ((previousValue != null))
                    {
                        this._Chapter.Entity = null;
                        previousValue.CurriculumChapters.Remove(this);
                    }
                    this._Chapter.Entity = value;
                    if ((value != null))
                    {
                        value.CurriculumChapters.Add(this);
                        this._ChapterRef = value.Id;
                    }
                    else
                    {
                        this._ChapterRef = default(int);
                    }
                    this.SendPropertyChanged("Chapter");
                }
            }
        }

        [global::System.Data.Linq.Mapping.AssociationAttribute(Name = "Curriculum_CurriculumChapter", Storage = "_Curriculum", ThisKey = "CurriculumRef", OtherKey = "Id", IsForeignKey = true)]
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
                        previousValue.CurriculumChapters.Remove(this);
                    }
                    this._Curriculum.Entity = value;
                    if ((value != null))
                    {
                        value.CurriculumChapters.Add(this);
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

        private void attach_CurriculumChapterTopics(CurriculumChapterTopic entity)
        {
            this.SendPropertyChanging();
            entity.CurriculumChapter = this;
        }

        private void detach_CurriculumChapterTopics(CurriculumChapterTopic entity)
        {
            this.SendPropertyChanging();
            entity.CurriculumChapter = null;
        }
    }

    [global::System.Data.Linq.Mapping.TableAttribute(Name = "dbo.CurriculumChapterTopics")]
    public partial class CurriculumChapterTopic : INotifyPropertyChanging, INotifyPropertyChanged
    {

        private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);

        private int _Id;

        private int _CurriculumChapterRef;

        private int _TopicRef;

        private System.Nullable<System.DateTime> _TestStartDate;

        private System.Nullable<System.DateTime> _TestEndDate;

        private System.Nullable<System.DateTime> _TheoryStartDate;

        private System.Nullable<System.DateTime> _TheoryEndDate;

        private int _MaxScore;

        private bool _BlockTopicAtTesting;

        private bool _BlockCurriculumAtTesting;

        private bool _IsDeleted;

        private EntityRef<CurriculumChapter> _CurriculumChapter;

        private EntityRef<Topic> _Topic;

        #region Extensibility Method Definitions
        partial void OnLoaded();
        partial void OnValidate(System.Data.Linq.ChangeAction action);
        partial void OnCreated();
        partial void OnIdChanging(int value);
        partial void OnIdChanged();
        partial void OnCurriculumChapterRefChanging(int value);
        partial void OnCurriculumChapterRefChanged();
        partial void OnTopicRefChanging(int value);
        partial void OnTopicRefChanged();
        partial void OnTestStartDateChanging(System.Nullable<System.DateTime> value);
        partial void OnTestStartDateChanged();
        partial void OnTestEndDateChanging(System.Nullable<System.DateTime> value);
        partial void OnTestEndDateChanged();
        partial void OnTheoryStartDateChanging(System.Nullable<System.DateTime> value);
        partial void OnTheoryStartDateChanged();
        partial void OnTheoryEndDateChanging(System.Nullable<System.DateTime> value);
        partial void OnTheoryEndDateChanged();
        partial void OnMaxScoreChanging(int value);
        partial void OnMaxScoreChanged();
        partial void OnBlockTopicAtTestingChanging(bool value);
        partial void OnBlockTopicAtTestingChanged();
        partial void OnBlockCurriculumAtTestingChanging(bool value);
        partial void OnBlockCurriculumAtTestingChanged();
        partial void OnIsDeletedChanging(bool value);
        partial void OnIsDeletedChanged();
        #endregion

        public CurriculumChapterTopic()
        {
            this._CurriculumChapter = default(EntityRef<CurriculumChapter>);
            this._Topic = default(EntityRef<Topic>);
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

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_CurriculumChapterRef", DbType = "Int NOT NULL")]
        public int CurriculumChapterRef
        {
            get
            {
                return this._CurriculumChapterRef;
            }
            set
            {
                if ((this._CurriculumChapterRef != value))
                {
                    if (this._CurriculumChapter.HasLoadedOrAssignedValue)
                    {
                        throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
                    }
                    this.OnCurriculumChapterRefChanging(value);
                    this.SendPropertyChanging();
                    this._CurriculumChapterRef = value;
                    this.SendPropertyChanged("CurriculumChapterRef");
                    this.OnCurriculumChapterRefChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_TopicRef", DbType = "Int NOT NULL")]
        public int TopicRef
        {
            get
            {
                return this._TopicRef;
            }
            set
            {
                if ((this._TopicRef != value))
                {
                    if (this._Topic.HasLoadedOrAssignedValue)
                    {
                        throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
                    }
                    this.OnTopicRefChanging(value);
                    this.SendPropertyChanging();
                    this._TopicRef = value;
                    this.SendPropertyChanged("TopicRef");
                    this.OnTopicRefChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_TestStartDate", DbType = "DateTime")]
        public System.Nullable<System.DateTime> TestStartDate
        {
            get
            {
                return this._TestStartDate;
            }
            set
            {
                if ((this._TestStartDate != value))
                {
                    this.OnTestStartDateChanging(value);
                    this.SendPropertyChanging();
                    this._TestStartDate = value;
                    this.SendPropertyChanged("TestStartDate");
                    this.OnTestStartDateChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_TestEndDate", DbType = "DateTime")]
        public System.Nullable<System.DateTime> TestEndDate
        {
            get
            {
                return this._TestEndDate;
            }
            set
            {
                if ((this._TestEndDate != value))
                {
                    this.OnTestEndDateChanging(value);
                    this.SendPropertyChanging();
                    this._TestEndDate = value;
                    this.SendPropertyChanged("TestEndDate");
                    this.OnTestEndDateChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_TheoryStartDate", DbType = "DateTime")]
        public System.Nullable<System.DateTime> TheoryStartDate
        {
            get
            {
                return this._TheoryStartDate;
            }
            set
            {
                if ((this._TheoryStartDate != value))
                {
                    this.OnTheoryStartDateChanging(value);
                    this.SendPropertyChanging();
                    this._TheoryStartDate = value;
                    this.SendPropertyChanged("TheoryStartDate");
                    this.OnTheoryStartDateChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_TheoryEndDate", DbType = "DateTime")]
        public System.Nullable<System.DateTime> TheoryEndDate
        {
            get
            {
                return this._TheoryEndDate;
            }
            set
            {
                if ((this._TheoryEndDate != value))
                {
                    this.OnTheoryEndDateChanging(value);
                    this.SendPropertyChanging();
                    this._TheoryEndDate = value;
                    this.SendPropertyChanged("TheoryEndDate");
                    this.OnTheoryEndDateChanged();
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

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_BlockTopicAtTesting", DbType = "Bit NOT NULL")]
        public bool BlockTopicAtTesting
        {
            get
            {
                return this._BlockTopicAtTesting;
            }
            set
            {
                if ((this._BlockTopicAtTesting != value))
                {
                    this.OnBlockTopicAtTestingChanging(value);
                    this.SendPropertyChanging();
                    this._BlockTopicAtTesting = value;
                    this.SendPropertyChanged("BlockTopicAtTesting");
                    this.OnBlockTopicAtTestingChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_BlockCurriculumAtTesting", DbType = "Bit NOT NULL")]
        public bool BlockCurriculumAtTesting
        {
            get
            {
                return this._BlockCurriculumAtTesting;
            }
            set
            {
                if ((this._BlockCurriculumAtTesting != value))
                {
                    this.OnBlockCurriculumAtTestingChanging(value);
                    this.SendPropertyChanging();
                    this._BlockCurriculumAtTesting = value;
                    this.SendPropertyChanged("BlockCurriculumAtTesting");
                    this.OnBlockCurriculumAtTestingChanged();
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

        [global::System.Data.Linq.Mapping.AssociationAttribute(Name = "CurriculumChapter_CurriculumChapterTopic", Storage = "_CurriculumChapter", ThisKey = "CurriculumChapterRef", OtherKey = "Id", IsForeignKey = true)]
        public CurriculumChapter CurriculumChapter
        {
            get
            {
                return this._CurriculumChapter.Entity;
            }
            set
            {
                CurriculumChapter previousValue = this._CurriculumChapter.Entity;
                if (((previousValue != value)
                            || (this._CurriculumChapter.HasLoadedOrAssignedValue == false)))
                {
                    this.SendPropertyChanging();
                    if ((previousValue != null))
                    {
                        this._CurriculumChapter.Entity = null;
                        previousValue.CurriculumChapterTopics.Remove(this);
                    }
                    this._CurriculumChapter.Entity = value;
                    if ((value != null))
                    {
                        value.CurriculumChapterTopics.Add(this);
                        this._CurriculumChapterRef = value.Id;
                    }
                    else
                    {
                        this._CurriculumChapterRef = default(int);
                    }
                    this.SendPropertyChanged("CurriculumChapter");
                }
            }
        }

        [global::System.Data.Linq.Mapping.AssociationAttribute(Name = "Topic_CurriculumChapterTopic", Storage = "_Topic", ThisKey = "TopicRef", OtherKey = "Id", IsForeignKey = true)]
        public Topic Topic
        {
            get
            {
                return this._Topic.Entity;
            }
            set
            {
                Topic previousValue = this._Topic.Entity;
                if (((previousValue != value)
                            || (this._Topic.HasLoadedOrAssignedValue == false)))
                {
                    this.SendPropertyChanging();
                    if ((previousValue != null))
                    {
                        this._Topic.Entity = null;
                        previousValue.CurriculumChapterTopics.Remove(this);
                    }
                    this._Topic.Entity = value;
                    if ((value != null))
                    {
                        value.CurriculumChapterTopics.Add(this);
                        this._TopicRef = value.Id;
                    }
                    else
                    {
                        this._TopicRef = default(int);
                    }
                    this.SendPropertyChanged("Topic");
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

        private int _UserGroupRef;

        private int _DisciplineRef;

        private System.Nullable<System.DateTime> _StartDate;

        private System.Nullable<System.DateTime> _EndDate;

        private bool _IsDeleted;

        private bool _IsValid;

        private EntitySet<CurriculumChapter> _CurriculumChapters;

        private EntityRef<Discipline> _Discipline;

        #region Extensibility Method Definitions
        partial void OnLoaded();
        partial void OnValidate(System.Data.Linq.ChangeAction action);
        partial void OnCreated();
        partial void OnIdChanging(int value);
        partial void OnIdChanged();
        partial void OnUserGroupRefChanging(int value);
        partial void OnUserGroupRefChanged();
        partial void OnDisciplineRefChanging(int value);
        partial void OnDisciplineRefChanged();
        partial void OnStartDateChanging(System.Nullable<System.DateTime> value);
        partial void OnStartDateChanged();
        partial void OnEndDateChanging(System.Nullable<System.DateTime> value);
        partial void OnEndDateChanged();
        partial void OnIsDeletedChanging(bool value);
        partial void OnIsDeletedChanged();
        partial void OnIsValidChanging(bool value);
        partial void OnIsValidChanged();
        #endregion

        public Curriculum()
        {
            this._CurriculumChapters = new EntitySet<CurriculumChapter>(new Action<CurriculumChapter>(this.attach_CurriculumChapters), new Action<CurriculumChapter>(this.detach_CurriculumChapters));
            this._Discipline = default(EntityRef<Discipline>);
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

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_DisciplineRef", DbType = "Int NOT NULL")]
        public int DisciplineRef
        {
            get
            {
                return this._DisciplineRef;
            }
            set
            {
                if ((this._DisciplineRef != value))
                {
                    if (this._Discipline.HasLoadedOrAssignedValue)
                    {
                        throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
                    }
                    this.OnDisciplineRefChanging(value);
                    this.SendPropertyChanging();
                    this._DisciplineRef = value;
                    this.SendPropertyChanged("DisciplineRef");
                    this.OnDisciplineRefChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_StartDate", DbType = "DateTime")]
        public System.Nullable<System.DateTime> StartDate
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

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_EndDate", DbType = "DateTime")]
        public System.Nullable<System.DateTime> EndDate
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

        [global::System.Data.Linq.Mapping.AssociationAttribute(Name = "Curriculum_CurriculumChapter", Storage = "_CurriculumChapters", ThisKey = "Id", OtherKey = "CurriculumRef")]
        public EntitySet<CurriculumChapter> CurriculumChapters
        {
            get
            {
                return this._CurriculumChapters;
            }
            set
            {
                this._CurriculumChapters.Assign(value);
            }
        }

        [global::System.Data.Linq.Mapping.AssociationAttribute(Name = "Discipline_Curriculum", Storage = "_Discipline", ThisKey = "DisciplineRef", OtherKey = "Id", IsForeignKey = true)]
        public Discipline Discipline
        {
            get
            {
                return this._Discipline.Entity;
            }
            set
            {
                Discipline previousValue = this._Discipline.Entity;
                if (((previousValue != value)
                            || (this._Discipline.HasLoadedOrAssignedValue == false)))
                {
                    this.SendPropertyChanging();
                    if ((previousValue != null))
                    {
                        this._Discipline.Entity = null;
                        previousValue.Curriculums.Remove(this);
                    }
                    this._Discipline.Entity = value;
                    if ((value != null))
                    {
                        value.Curriculums.Add(this);
                        this._DisciplineRef = value.Id;
                    }
                    else
                    {
                        this._DisciplineRef = default(int);
                    }
                    this.SendPropertyChanged("Discipline");
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

        private void attach_CurriculumChapters(CurriculumChapter entity)
        {
            this.SendPropertyChanging();
            entity.Curriculum = this;
        }

        private void detach_CurriculumChapters(CurriculumChapter entity)
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

    [global::System.Data.Linq.Mapping.TableAttribute(Name = "dbo.Tags")]
    public partial class Tag : INotifyPropertyChanging, INotifyPropertyChanged
    {

        private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);

        private int _Id;

        private string _Name;

        private EntitySet<TopicScore> _TopicScores;

        private EntitySet<TopicTag> _TopicTags;

        private EntitySet<UserScore> _UserScores;

        #region Extensibility Method Definitions
        partial void OnLoaded();
        partial void OnValidate(System.Data.Linq.ChangeAction action);
        partial void OnCreated();
        partial void OnIdChanging(int value);
        partial void OnIdChanged();
        partial void OnNameChanging(string value);
        partial void OnNameChanged();
        #endregion

        public Tag()
        {
            this._TopicScores = new EntitySet<TopicScore>(new Action<TopicScore>(this.attach_TopicScores), new Action<TopicScore>(this.detach_TopicScores));
            this._TopicTags = new EntitySet<TopicTag>(new Action<TopicTag>(this.attach_TopicTags), new Action<TopicTag>(this.detach_TopicTags));
            this._UserScores = new EntitySet<UserScore>(new Action<UserScore>(this.attach_UserScores), new Action<UserScore>(this.detach_UserScores));
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

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Name", DbType = "VarChar(50) NOT NULL", CanBeNull = false)]
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

        [global::System.Data.Linq.Mapping.AssociationAttribute(Name = "Tag_TopicScore", Storage = "_TopicScores", ThisKey = "Id", OtherKey = "TagId")]
        public EntitySet<TopicScore> TopicScores
        {
            get
            {
                return this._TopicScores;
            }
            set
            {
                this._TopicScores.Assign(value);
            }
        }

        [global::System.Data.Linq.Mapping.AssociationAttribute(Name = "Tag_TopicTag", Storage = "_TopicTags", ThisKey = "Id", OtherKey = "TagId")]
        public EntitySet<TopicTag> TopicTags
        {
            get
            {
                return this._TopicTags;
            }
            set
            {
                this._TopicTags.Assign(value);
            }
        }

        [global::System.Data.Linq.Mapping.AssociationAttribute(Name = "Tag_UserScore", Storage = "_UserScores", ThisKey = "Id", OtherKey = "TagId")]
        public EntitySet<UserScore> UserScores
        {
            get
            {
                return this._UserScores;
            }
            set
            {
                this._UserScores.Assign(value);
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

        private void attach_TopicScores(TopicScore entity)
        {
            this.SendPropertyChanging();
            entity.Tag = this;
        }

        private void detach_TopicScores(TopicScore entity)
        {
            this.SendPropertyChanging();
            entity.Tag = null;
        }

        private void attach_TopicTags(TopicTag entity)
        {
            this.SendPropertyChanging();
            entity.Tag = this;
        }

        private void detach_TopicTags(TopicTag entity)
        {
            this.SendPropertyChanging();
            entity.Tag = null;
        }

        private void attach_UserScores(UserScore entity)
        {
            this.SendPropertyChanging();
            entity.Tag = this;
        }

        private void detach_UserScores(UserScore entity)
        {
            this.SendPropertyChanging();
            entity.Tag = null;
        }
    }

    [global::System.Data.Linq.Mapping.TableAttribute(Name = "dbo.Topics")]
    public partial class Topic : INotifyPropertyChanging, INotifyPropertyChanged
    {

        private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);

        private int _Id;

        private string _Name;

        private int _ChapterRef;

        private System.Nullable<int> _TestCourseRef;

        private System.Nullable<int> _TestTopicTypeRef;

        private System.Nullable<int> _TheoryCourseRef;

        private System.Nullable<int> _TheoryTopicTypeRef;

        private int _SortOrder;

        private System.DateTime _Created;

        private System.DateTime _Updated;

        private bool _IsDeleted;

        private EntitySet<CurriculumChapterTopic> _CurriculumChapterTopics;

        private EntityRef<Chapter> _Chapter;

        private EntityRef<TopicType> _TestTopicType;

        private EntityRef<TopicType> _TheoryTopicType;

        #region Extensibility Method Definitions
        partial void OnLoaded();
        partial void OnValidate(System.Data.Linq.ChangeAction action);
        partial void OnCreated();
        partial void OnIdChanging(int value);
        partial void OnIdChanged();
        partial void OnNameChanging(string value);
        partial void OnNameChanged();
        partial void OnChapterRefChanging(int value);
        partial void OnChapterRefChanged();
        partial void OnTestCourseRefChanging(System.Nullable<int> value);
        partial void OnTestCourseRefChanged();
        partial void OnTestTopicTypeRefChanging(System.Nullable<int> value);
        partial void OnTestTopicTypeRefChanged();
        partial void OnTheoryCourseRefChanging(System.Nullable<int> value);
        partial void OnTheoryCourseRefChanged();
        partial void OnTheoryTopicTypeRefChanging(System.Nullable<int> value);
        partial void OnTheoryTopicTypeRefChanged();
        partial void OnSortOrderChanging(int value);
        partial void OnSortOrderChanged();
        partial void OnCreatedChanging(System.DateTime value);
        partial void OnCreatedChanged();
        partial void OnUpdatedChanging(System.DateTime value);
        partial void OnUpdatedChanged();
        partial void OnIsDeletedChanging(bool value);
        partial void OnIsDeletedChanged();
        #endregion

        public Topic()
        {
            this._CurriculumChapterTopics = new EntitySet<CurriculumChapterTopic>(new Action<CurriculumChapterTopic>(this.attach_CurriculumChapterTopics), new Action<CurriculumChapterTopic>(this.detach_CurriculumChapterTopics));
            this._Chapter = default(EntityRef<Chapter>);
            this._TestTopicType = default(EntityRef<TopicType>);
            this._TheoryTopicType = default(EntityRef<TopicType>);
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

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_ChapterRef", DbType = "Int NOT NULL")]
        public int ChapterRef
        {
            get
            {
                return this._ChapterRef;
            }
            set
            {
                if ((this._ChapterRef != value))
                {
                    if (this._Chapter.HasLoadedOrAssignedValue)
                    {
                        throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
                    }
                    this.OnChapterRefChanging(value);
                    this.SendPropertyChanging();
                    this._ChapterRef = value;
                    this.SendPropertyChanged("ChapterRef");
                    this.OnChapterRefChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_TestCourseRef", DbType = "Int")]
        public System.Nullable<int> TestCourseRef
        {
            get
            {
                return this._TestCourseRef;
            }
            set
            {
                if ((this._TestCourseRef != value))
                {
                    this.OnTestCourseRefChanging(value);
                    this.SendPropertyChanging();
                    this._TestCourseRef = value;
                    this.SendPropertyChanged("TestCourseRef");
                    this.OnTestCourseRefChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_TestTopicTypeRef", DbType = "Int")]
        public System.Nullable<int> TestTopicTypeRef
        {
            get
            {
                return this._TestTopicTypeRef;
            }
            set
            {
                if ((this._TestTopicTypeRef != value))
                {
                    if (this._TestTopicType.HasLoadedOrAssignedValue)
                    {
                        throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
                    }
                    this.OnTestTopicTypeRefChanging(value);
                    this.SendPropertyChanging();
                    this._TestTopicTypeRef = value;
                    this.SendPropertyChanged("TestTopicTypeRef");
                    this.OnTestTopicTypeRefChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_TheoryCourseRef", DbType = "Int")]
        public System.Nullable<int> TheoryCourseRef
        {
            get
            {
                return this._TheoryCourseRef;
            }
            set
            {
                if ((this._TheoryCourseRef != value))
                {
                    this.OnTheoryCourseRefChanging(value);
                    this.SendPropertyChanging();
                    this._TheoryCourseRef = value;
                    this.SendPropertyChanged("TheoryCourseRef");
                    this.OnTheoryCourseRefChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_TheoryTopicTypeRef", DbType = "Int")]
        public System.Nullable<int> TheoryTopicTypeRef
        {
            get
            {
                return this._TheoryTopicTypeRef;
            }
            set
            {
                if ((this._TheoryTopicTypeRef != value))
                {
                    if (this._TheoryTopicType.HasLoadedOrAssignedValue)
                    {
                        throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
                    }
                    this.OnTheoryTopicTypeRefChanging(value);
                    this.SendPropertyChanging();
                    this._TheoryTopicTypeRef = value;
                    this.SendPropertyChanged("TheoryTopicTypeRef");
                    this.OnTheoryTopicTypeRefChanged();
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

        [global::System.Data.Linq.Mapping.AssociationAttribute(Name = "Topic_CurriculumChapterTopic", Storage = "_CurriculumChapterTopics", ThisKey = "Id", OtherKey = "TopicRef")]
        public EntitySet<CurriculumChapterTopic> CurriculumChapterTopics
        {
            get
            {
                return this._CurriculumChapterTopics;
            }
            set
            {
                this._CurriculumChapterTopics.Assign(value);
            }
        }

        [global::System.Data.Linq.Mapping.AssociationAttribute(Name = "Chapter_Topic", Storage = "_Chapter", ThisKey = "ChapterRef", OtherKey = "Id", IsForeignKey = true)]
        public Chapter Chapter
        {
            get
            {
                return this._Chapter.Entity;
            }
            set
            {
                Chapter previousValue = this._Chapter.Entity;
                if (((previousValue != value)
                            || (this._Chapter.HasLoadedOrAssignedValue == false)))
                {
                    this.SendPropertyChanging();
                    if ((previousValue != null))
                    {
                        this._Chapter.Entity = null;
                        previousValue.Topics.Remove(this);
                    }
                    this._Chapter.Entity = value;
                    if ((value != null))
                    {
                        value.Topics.Add(this);
                        this._ChapterRef = value.Id;
                    }
                    else
                    {
                        this._ChapterRef = default(int);
                    }
                    this.SendPropertyChanged("Chapter");
                }
            }
        }

        [global::System.Data.Linq.Mapping.AssociationAttribute(Name = "TopicType_Topic", Storage = "_TestTopicType", ThisKey = "TestTopicTypeRef", OtherKey = "Id", IsForeignKey = true)]
        public TopicType TestTopicType
        {
            get
            {
                return this._TestTopicType.Entity;
            }
            set
            {
                TopicType previousValue = this._TestTopicType.Entity;
                if (((previousValue != value)
                            || (this._TestTopicType.HasLoadedOrAssignedValue == false)))
                {
                    this.SendPropertyChanging();
                    if ((previousValue != null))
                    {
                        this._TestTopicType.Entity = null;
                        previousValue.Topics.Remove(this);
                    }
                    this._TestTopicType.Entity = value;
                    if ((value != null))
                    {
                        value.Topics.Add(this);
                        this._TestTopicTypeRef = value.Id;
                    }
                    else
                    {
                        this._TestTopicTypeRef = default(Nullable<int>);
                    }
                    this.SendPropertyChanged("TestTopicType");
                }
            }
        }

        [global::System.Data.Linq.Mapping.AssociationAttribute(Name = "TopicType_Topic1", Storage = "_TheoryTopicType", ThisKey = "TheoryTopicTypeRef", OtherKey = "Id", IsForeignKey = true)]
        public TopicType TheoryTopicType
        {
            get
            {
                return this._TheoryTopicType.Entity;
            }
            set
            {
                TopicType previousValue = this._TheoryTopicType.Entity;
                if (((previousValue != value)
                            || (this._TheoryTopicType.HasLoadedOrAssignedValue == false)))
                {
                    this.SendPropertyChanging();
                    if ((previousValue != null))
                    {
                        this._TheoryTopicType.Entity = null;
                        previousValue.Topics1.Remove(this);
                    }
                    this._TheoryTopicType.Entity = value;
                    if ((value != null))
                    {
                        value.Topics1.Add(this);
                        this._TheoryTopicTypeRef = value.Id;
                    }
                    else
                    {
                        this._TheoryTopicTypeRef = default(Nullable<int>);
                    }
                    this.SendPropertyChanged("TheoryTopicType");
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

        private void attach_CurriculumChapterTopics(CurriculumChapterTopic entity)
        {
            this.SendPropertyChanging();
            entity.Topic = this;
        }

        private void detach_CurriculumChapterTopics(CurriculumChapterTopic entity)
        {
            this.SendPropertyChanging();
            entity.Topic = null;
        }
    }

    [global::System.Data.Linq.Mapping.TableAttribute(Name = "dbo.TopicScores")]
    public partial class TopicScore : INotifyPropertyChanging, INotifyPropertyChanged
    {

        private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);

        private int _TopicId;

        private int _TagId;

        private float _Score;

        private EntityRef<Tag> _Tag;

        #region Extensibility Method Definitions
        partial void OnLoaded();
        partial void OnValidate(System.Data.Linq.ChangeAction action);
        partial void OnCreated();
        partial void OnTopicIdChanging(int value);
        partial void OnTopicIdChanged();
        partial void OnTagIdChanging(int value);
        partial void OnTagIdChanged();
        partial void OnScoreChanging(float value);
        partial void OnScoreChanged();
        #endregion

        public TopicScore()
        {
            this._Tag = default(EntityRef<Tag>);
            OnCreated();
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_TopicId", DbType = "Int NOT NULL", IsPrimaryKey = true)]
        public int TopicId
        {
            get
            {
                return this._TopicId;
            }
            set
            {
                if ((this._TopicId != value))
                {
                    this.OnTopicIdChanging(value);
                    this.SendPropertyChanging();
                    this._TopicId = value;
                    this.SendPropertyChanged("TopicId");
                    this.OnTopicIdChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_TagId", DbType = "Int NOT NULL", IsPrimaryKey = true)]
        public int TagId
        {
            get
            {
                return this._TagId;
            }
            set
            {
                if ((this._TagId != value))
                {
                    if (this._Tag.HasLoadedOrAssignedValue)
                    {
                        throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
                    }
                    this.OnTagIdChanging(value);
                    this.SendPropertyChanging();
                    this._TagId = value;
                    this.SendPropertyChanged("TagId");
                    this.OnTagIdChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Score", DbType = "Real NOT NULL")]
        public float Score
        {
            get
            {
                return this._Score;
            }
            set
            {
                if ((this._Score != value))
                {
                    this.OnScoreChanging(value);
                    this.SendPropertyChanging();
                    this._Score = value;
                    this.SendPropertyChanged("Score");
                    this.OnScoreChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.AssociationAttribute(Name = "Tag_TopicScore", Storage = "_Tag", ThisKey = "TagId", OtherKey = "Id", IsForeignKey = true)]
        public Tag Tag
        {
            get
            {
                return this._Tag.Entity;
            }
            set
            {
                Tag previousValue = this._Tag.Entity;
                if (((previousValue != value)
                            || (this._Tag.HasLoadedOrAssignedValue == false)))
                {
                    this.SendPropertyChanging();
                    if ((previousValue != null))
                    {
                        this._Tag.Entity = null;
                        previousValue.TopicScores.Remove(this);
                    }
                    this._Tag.Entity = value;
                    if ((value != null))
                    {
                        value.TopicScores.Add(this);
                        this._TagId = value.Id;
                    }
                    else
                    {
                        this._TagId = default(int);
                    }
                    this.SendPropertyChanged("Tag");
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

    [global::System.Data.Linq.Mapping.TableAttribute(Name = "dbo.TopicTags")]
    public partial class TopicTag : INotifyPropertyChanging, INotifyPropertyChanged
    {

        private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);

        private int _TopicId;

        private int _TagId;

        private EntityRef<Tag> _Tag;

        #region Extensibility Method Definitions
        partial void OnLoaded();
        partial void OnValidate(System.Data.Linq.ChangeAction action);
        partial void OnCreated();
        partial void OnTopicIdChanging(int value);
        partial void OnTopicIdChanged();
        partial void OnTagIdChanging(int value);
        partial void OnTagIdChanged();
        #endregion

        public TopicTag()
        {
            this._Tag = default(EntityRef<Tag>);
            OnCreated();
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_TopicId", DbType = "Int NOT NULL", IsPrimaryKey = true)]
        public int TopicId
        {
            get
            {
                return this._TopicId;
            }
            set
            {
                if ((this._TopicId != value))
                {
                    this.OnTopicIdChanging(value);
                    this.SendPropertyChanging();
                    this._TopicId = value;
                    this.SendPropertyChanged("TopicId");
                    this.OnTopicIdChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_TagId", DbType = "Int NOT NULL", IsPrimaryKey = true)]
        public int TagId
        {
            get
            {
                return this._TagId;
            }
            set
            {
                if ((this._TagId != value))
                {
                    if (this._Tag.HasLoadedOrAssignedValue)
                    {
                        throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
                    }
                    this.OnTagIdChanging(value);
                    this.SendPropertyChanging();
                    this._TagId = value;
                    this.SendPropertyChanged("TagId");
                    this.OnTagIdChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.AssociationAttribute(Name = "Tag_TopicTag", Storage = "_Tag", ThisKey = "TagId", OtherKey = "Id", IsForeignKey = true)]
        public Tag Tag
        {
            get
            {
                return this._Tag.Entity;
            }
            set
            {
                Tag previousValue = this._Tag.Entity;
                if (((previousValue != value)
                            || (this._Tag.HasLoadedOrAssignedValue == false)))
                {
                    this.SendPropertyChanging();
                    if ((previousValue != null))
                    {
                        this._Tag.Entity = null;
                        previousValue.TopicTags.Remove(this);
                    }
                    this._Tag.Entity = value;
                    if ((value != null))
                    {
                        value.TopicTags.Add(this);
                        this._TagId = value.Id;
                    }
                    else
                    {
                        this._TagId = default(int);
                    }
                    this.SendPropertyChanged("Tag");
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

    [global::System.Data.Linq.Mapping.TableAttribute(Name = "dbo.TopicTypes")]
    public partial class TopicType : INotifyPropertyChanging, INotifyPropertyChanged
    {

        private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);

        private int _Id;

        private string _Name;

        private EntitySet<Topic> _Topics;

        private EntitySet<Topic> _Topics1;

        #region Extensibility Method Definitions
        partial void OnLoaded();
        partial void OnValidate(System.Data.Linq.ChangeAction action);
        partial void OnCreated();
        partial void OnIdChanging(int value);
        partial void OnIdChanged();
        partial void OnNameChanging(string value);
        partial void OnNameChanged();
        #endregion

        public TopicType()
        {
            this._Topics = new EntitySet<Topic>(new Action<Topic>(this.attach_Topics), new Action<Topic>(this.detach_Topics));
            this._Topics1 = new EntitySet<Topic>(new Action<Topic>(this.attach_Topics1), new Action<Topic>(this.detach_Topics1));
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

        [global::System.Data.Linq.Mapping.AssociationAttribute(Name = "TopicType_Topic", Storage = "_Topics", ThisKey = "Id", OtherKey = "TestTopicTypeRef")]
        public EntitySet<Topic> Topics
        {
            get
            {
                return this._Topics;
            }
            set
            {
                this._Topics.Assign(value);
            }
        }

        [global::System.Data.Linq.Mapping.AssociationAttribute(Name = "TopicType_Topic1", Storage = "_Topics1", ThisKey = "Id", OtherKey = "TheoryTopicTypeRef")]
        public EntitySet<Topic> Topics1
        {
            get
            {
                return this._Topics1;
            }
            set
            {
                this._Topics1.Assign(value);
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

        private void attach_Topics(Topic entity)
        {
            this.SendPropertyChanging();
            entity.TestTopicType = this;
        }

        private void detach_Topics(Topic entity)
        {
            this.SendPropertyChanging();
            entity.TestTopicType = null;
        }

        private void attach_Topics1(Topic entity)
        {
            this.SendPropertyChanging();
            entity.TheoryTopicType = this;
        }

        private void detach_Topics1(Topic entity)
        {
            this.SendPropertyChanging();
            entity.TheoryTopicType = null;
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

        private int _TestsSum;

        private int _TestsTotal;

        private EntitySet<ForecastingResult> _ForecastingResults;

        private EntitySet<GroupUser> _GroupUsers;

        private EntitySet<StudyResult> _StudyResults;

        private EntitySet<User> _Users;

        private EntitySet<UserRole> _UserRoles;

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
        partial void OnTestsSumChanging(int value);
        partial void OnTestsSumChanged();
        partial void OnTestsTotalChanging(int value);
        partial void OnTestsTotalChanged();
        #endregion

        public User()
        {
            this._ForecastingResults = new EntitySet<ForecastingResult>(new Action<ForecastingResult>(this.attach_ForecastingResults), new Action<ForecastingResult>(this.detach_ForecastingResults));
            this._GroupUsers = new EntitySet<GroupUser>(new Action<GroupUser>(this.attach_GroupUsers), new Action<GroupUser>(this.detach_GroupUsers));
            this._StudyResults = new EntitySet<StudyResult>(new Action<StudyResult>(this.attach_StudyResults), new Action<StudyResult>(this.detach_StudyResults));
            this._Users = new EntitySet<User>(new Action<User>(this.attach_Users), new Action<User>(this.detach_Users));
            this._UserRoles = new EntitySet<UserRole>(new Action<UserRole>(this.attach_UserRoles), new Action<UserRole>(this.detach_UserRoles));
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

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_TestsSum", DbType = "Int NOT NULL")]
        public int TestsSum
        {
            get
            {
                return this._TestsSum;
            }
            set
            {
                if ((this._TestsSum != value))
                {
                    this.OnTestsSumChanging(value);
                    this.SendPropertyChanging();
                    this._TestsSum = value;
                    this.SendPropertyChanged("TestsSum");
                    this.OnTestsSumChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_TestsTotal", DbType = "Int NOT NULL")]
        public int TestsTotal
        {
            get
            {
                return this._TestsTotal;
            }
            set
            {
                if ((this._TestsTotal != value))
                {
                    this.OnTestsTotalChanging(value);
                    this.SendPropertyChanging();
                    this._TestsTotal = value;
                    this.SendPropertyChanged("TestsTotal");
                    this.OnTestsTotalChanged();
                }
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

    [global::System.Data.Linq.Mapping.TableAttribute(Name = "dbo.UserScores")]
    public partial class UserScore : INotifyPropertyChanging, INotifyPropertyChanged
    {

        private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);

        private System.Guid _UserId;

        private int _TagId;

        private float _Score;

        private EntityRef<Tag> _Tag;

        #region Extensibility Method Definitions
        partial void OnLoaded();
        partial void OnValidate(System.Data.Linq.ChangeAction action);
        partial void OnCreated();
        partial void OnUserIdChanging(System.Guid value);
        partial void OnUserIdChanged();
        partial void OnTagIdChanging(int value);
        partial void OnTagIdChanged();
        partial void OnScoreChanging(float value);
        partial void OnScoreChanged();
        #endregion

        public UserScore()
        {
            this._Tag = default(EntityRef<Tag>);
            OnCreated();
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_UserId", DbType = "UniqueIdentifier NOT NULL", IsPrimaryKey = true)]
        public System.Guid UserId
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

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_TagId", DbType = "Int NOT NULL", IsPrimaryKey = true)]
        public int TagId
        {
            get
            {
                return this._TagId;
            }
            set
            {
                if ((this._TagId != value))
                {
                    if (this._Tag.HasLoadedOrAssignedValue)
                    {
                        throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
                    }
                    this.OnTagIdChanging(value);
                    this.SendPropertyChanging();
                    this._TagId = value;
                    this.SendPropertyChanged("TagId");
                    this.OnTagIdChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Score", DbType = "Real NOT NULL")]
        public float Score
        {
            get
            {
                return this._Score;
            }
            set
            {
                if ((this._Score != value))
                {
                    this.OnScoreChanging(value);
                    this.SendPropertyChanging();
                    this._Score = value;
                    this.SendPropertyChanged("Score");
                    this.OnScoreChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.AssociationAttribute(Name = "Tag_UserScore", Storage = "_Tag", ThisKey = "TagId", OtherKey = "Id", IsForeignKey = true)]
        public Tag Tag
        {
            get
            {
                return this._Tag.Entity;
            }
            set
            {
                Tag previousValue = this._Tag.Entity;
                if (((previousValue != value)
                            || (this._Tag.HasLoadedOrAssignedValue == false)))
                {
                    this.SendPropertyChanging();
                    if ((previousValue != null))
                    {
                        this._Tag.Entity = null;
                        previousValue.UserScores.Remove(this);
                    }
                    this._Tag.Entity = value;
                    if ((value != null))
                    {
                        value.UserScores.Add(this);
                        this._TagId = value.Id;
                    }
                    else
                    {
                        this._TagId = default(int);
                    }
                    this.SendPropertyChanged("Tag");
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

    [global::System.Data.Linq.Mapping.TableAttribute(Name = "dbo.UserTopicRatings")]
    public partial class UserTopicRating : INotifyPropertyChanging, INotifyPropertyChanged
    {

        private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);

        private System.Guid _UserId;

        private int _TopicId;

        private int _Rating;

        #region Extensibility Method Definitions
        partial void OnLoaded();
        partial void OnValidate(System.Data.Linq.ChangeAction action);
        partial void OnCreated();
        partial void OnUserIdChanging(System.Guid value);
        partial void OnUserIdChanged();
        partial void OnTopicIdChanging(int value);
        partial void OnTopicIdChanged();
        partial void OnRatingChanging(int value);
        partial void OnRatingChanged();
        #endregion

        public UserTopicRating()
        {
            OnCreated();
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_UserId", DbType = "UniqueIdentifier NOT NULL", IsPrimaryKey = true)]
        public System.Guid UserId
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

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_TopicId", DbType = "Int NOT NULL", IsPrimaryKey = true)]
        public int TopicId
        {
            get
            {
                return this._TopicId;
            }
            set
            {
                if ((this._TopicId != value))
                {
                    this.OnTopicIdChanging(value);
                    this.SendPropertyChanging();
                    this._TopicId = value;
                    this.SendPropertyChanged("TopicId");
                    this.OnTopicIdChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Rating", DbType = "Int NOT NULL")]
        public int Rating
        {
            get
            {
                return this._Rating;
            }
            set
            {
                if ((this._Rating != value))
                {
                    this.OnRatingChanging(value);
                    this.SendPropertyChanging();
                    this._Rating = value;
                    this.SendPropertyChanged("Rating");
                    this.OnRatingChanged();
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
}
