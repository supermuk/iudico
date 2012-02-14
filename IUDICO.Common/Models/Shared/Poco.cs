using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Linq;
using System.Linq;
using System.Text;
using System.Data.Linq.Mapping;

namespace IUDICO.Common.Models.Shared
{
    [global::System.Data.Linq.Mapping.TableAttribute(Name = "dbo.Chapters")]
    public partial class Chapter : INotifyPropertyChanging, INotifyPropertyChanged
    {

        private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);

        private int _Id;

        private string _Name;

        private System.DateTime _Created;

        private System.DateTime _Updated;

        private int _DisciplineRef;

        private bool _IsDeleted;

        private EntitySet<Topic> _Topics;

        private EntityRef<Discipline> _Discipline;

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
        partial void OnDisciplineRefChanging(int value);
        partial void OnDisciplineRefChanged();
        partial void OnIsDeletedChanging(bool value);
        partial void OnIsDeletedChanged();
        #endregion

        public Chapter()
        {
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

    [global::System.Data.Linq.Mapping.TableAttribute(Name = "dbo.UserTopicScores")]
    public partial class UserTopicScore : INotifyPropertyChanging, INotifyPropertyChanged
    {

        private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);

        private System.Guid _UserId;

        private int _TopicId;

        private int _Score;

        private EntityRef<Topic> _Topic;

        private EntityRef<User> _User;

        #region Extensibility Method Definitions
        partial void OnLoaded();
        partial void OnValidate(System.Data.Linq.ChangeAction action);
        partial void OnCreated();
        partial void OnUserIdChanging(System.Guid value);
        partial void OnUserIdChanged();
        partial void OnTopicIdChanging(int value);
        partial void OnTopicIdChanged();
        partial void OnScoreChanging(int value);
        partial void OnScoreChanged();
        #endregion

        public UserTopicScore()
        {
            this._Topic = default(EntityRef<Topic>);
            this._User = default(EntityRef<User>);
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
                    if (this._User.HasLoadedOrAssignedValue)
                    {
                        throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
                    }
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
                    if (this._Topic.HasLoadedOrAssignedValue)
                    {
                        throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
                    }
                    this.OnTopicIdChanging(value);
                    this.SendPropertyChanging();
                    this._TopicId = value;
                    this.SendPropertyChanged("TopicId");
                    this.OnTopicIdChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Score", DbType = "Int NOT NULL")]
        public int Score
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

        [global::System.Data.Linq.Mapping.AssociationAttribute(Name = "Topic_UserTopicScore", Storage = "_Topic", ThisKey = "TopicId", OtherKey = "Id", IsForeignKey = true)]
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
                        previousValue.UserTopicScores.Remove(this);
                    }
                    this._Topic.Entity = value;
                    if ((value != null))
                    {
                        value.UserTopicScores.Add(this);
                        this._TopicId = value.Id;
                    }
                    else
                    {
                        this._TopicId = default(int);
                    }
                    this.SendPropertyChanged("Topic");
                }
            }
        }

        [global::System.Data.Linq.Mapping.AssociationAttribute(Name = "User_UserTopicScore", Storage = "_User", ThisKey = "UserId", OtherKey = "Id", IsForeignKey = true)]
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
                        previousValue.UserTopicScores.Remove(this);
                    }
                    this._User.Entity = value;
                    if ((value != null))
                    {
                        value.UserTopicScores.Add(this);
                        this._UserId = value.Id;
                    }
                    else
                    {
                        this._UserId = default(System.Guid);
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

    [global::System.Data.Linq.Mapping.TableAttribute(Name = "dbo.Curriculums")]
    public partial class Curriculum : INotifyPropertyChanging, INotifyPropertyChanged
    {

        private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);

        private int _Id;

        private int _UserGroupRef;

        private int _DisciplineRef;

        private bool _IsDeleted;

        private bool _IsValid;

        private EntitySet<Timeline> _Timelines;

        private EntitySet<TopicAssignment> _TopicAssignments;

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
        partial void OnIsDeletedChanging(bool value);
        partial void OnIsDeletedChanged();
        partial void OnIsValidChanging(bool value);
        partial void OnIsValidChanged();
        #endregion

        public Curriculum()
        {
            this._Timelines = new EntitySet<Timeline>(new Action<Timeline>(this.attach_Timelines), new Action<Timeline>(this.detach_Timelines));
            this._TopicAssignments = new EntitySet<TopicAssignment>(new Action<TopicAssignment>(this.attach_TopicAssignments), new Action<TopicAssignment>(this.detach_TopicAssignments));
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

        [global::System.Data.Linq.Mapping.AssociationAttribute(Name = "Curriculum_Timeline", Storage = "_Timelines", ThisKey = "Id", OtherKey = "CurriculumRef")]
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

        [global::System.Data.Linq.Mapping.AssociationAttribute(Name = "Curriculum_TopicAssignment", Storage = "_TopicAssignments", ThisKey = "Id", OtherKey = "CurriculumRef")]
        public EntitySet<TopicAssignment> TopicAssignments
        {
            get
            {
                return this._TopicAssignments;
            }
            set
            {
                this._TopicAssignments.Assign(value);
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

        private void attach_Timelines(Timeline entity)
        {
            this.SendPropertyChanging();
            entity.Curriculum = this;
        }

        private void detach_Timelines(Timeline entity)
        {
            this.SendPropertyChanging();
            entity.Curriculum = null;
        }

        private void attach_TopicAssignments(TopicAssignment entity)
        {
            this.SendPropertyChanging();
            entity.Curriculum = this;
        }

        private void detach_TopicAssignments(TopicAssignment entity)
        {
            this.SendPropertyChanging();
            entity.Curriculum = null;
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

        private EntitySet<Chapter> _Chapters;

        private EntitySet<Curriculum> _Curriculums;

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
            this._Chapters = new EntitySet<Chapter>(new Action<Chapter>(this.attach_Chapters), new Action<Chapter>(this.detach_Chapters));
            this._Curriculums = new EntitySet<Curriculum>(new Action<Curriculum>(this.attach_Curriculums), new Action<Curriculum>(this.detach_Curriculums));
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
    }

    [global::System.Data.Linq.Mapping.TableAttribute(Name = "dbo.Features")]
    public partial class Feature : INotifyPropertyChanging, INotifyPropertyChanged
    {

        private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);

        private int _Id;

        private string _Name;

        private EntitySet<TopicFeature> _TopicFeatures;

        #region Extensibility Method Definitions
        partial void OnLoaded();
        partial void OnValidate(System.Data.Linq.ChangeAction action);
        partial void OnCreated();
        partial void OnIdChanging(int value);
        partial void OnIdChanged();
        partial void OnNameChanging(string value);
        partial void OnNameChanged();
        #endregion

        public Feature()
        {
            this._TopicFeatures = new EntitySet<TopicFeature>(new Action<TopicFeature>(this.attach_TopicFeatures), new Action<TopicFeature>(this.detach_TopicFeatures));
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

        [global::System.Data.Linq.Mapping.AssociationAttribute(Name = "Feature_TopicFeature", Storage = "_TopicFeatures", ThisKey = "Id", OtherKey = "FeatureId")]
        public EntitySet<TopicFeature> TopicFeatures
        {
            get
            {
                return this._TopicFeatures;
            }
            set
            {
                this._TopicFeatures.Assign(value);
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

        private void attach_TopicFeatures(TopicFeature entity)
        {
            this.SendPropertyChanging();
            entity.Feature = this;
        }

        private void detach_TopicFeatures(TopicFeature entity)
        {
            this.SendPropertyChanging();
            entity.Feature = null;
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

    [global::System.Data.Linq.Mapping.TableAttribute(Name = "dbo.Timelines")]
    public partial class Timeline : INotifyPropertyChanging, INotifyPropertyChanged
    {

        private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);

        private int _Id;

        private System.DateTime _StartDate;

        private System.DateTime _EndDate;

        private int _CurriculumRef;

        private System.Nullable<int> _ChapterRef;

        private bool _IsDeleted;

        private EntityRef<Curriculum> _Curriculum;

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
        partial void OnCurriculumRefChanging(int value);
        partial void OnCurriculumRefChanged();
        partial void OnChapterRefChanging(System.Nullable<int> value);
        partial void OnChapterRefChanged();
        partial void OnIsDeletedChanging(bool value);
        partial void OnIsDeletedChanged();
        #endregion

        public Timeline()
        {
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

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_ChapterRef", DbType = "Int")]
        public System.Nullable<int> ChapterRef
        {
            get
            {
                return this._ChapterRef;
            }
            set
            {
                if ((this._ChapterRef != value))
                {
                    this.OnChapterRefChanging(value);
                    this.SendPropertyChanging();
                    this._ChapterRef = value;
                    this.SendPropertyChanged("ChapterRef");
                    this.OnChapterRefChanged();
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

        [global::System.Data.Linq.Mapping.AssociationAttribute(Name = "Curriculum_Timeline", Storage = "_Curriculum", ThisKey = "CurriculumRef", OtherKey = "Id", IsForeignKey = true)]
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
                        previousValue.Timelines.Remove(this);
                    }
                    this._Curriculum.Entity = value;
                    if ((value != null))
                    {
                        value.Timelines.Add(this);
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
    }

    [global::System.Data.Linq.Mapping.TableAttribute(Name = "dbo.TopicAssignments")]
    public partial class TopicAssignment : INotifyPropertyChanging, INotifyPropertyChanged
    {

        private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);

        private int _Id;

        private int _TopicRef;

        private int _CurriculumRef;

        private int _MaxScore;

        private bool _IsDeleted;

        private EntityRef<Curriculum> _Curriculum;

        private EntityRef<Topic> _Topic;

        #region Extensibility Method Definitions
        partial void OnLoaded();
        partial void OnValidate(System.Data.Linq.ChangeAction action);
        partial void OnCreated();
        partial void OnIdChanging(int value);
        partial void OnIdChanged();
        partial void OnTopicRefChanging(int value);
        partial void OnTopicRefChanged();
        partial void OnCurriculumRefChanging(int value);
        partial void OnCurriculumRefChanged();
        partial void OnMaxScoreChanging(int value);
        partial void OnMaxScoreChanged();
        partial void OnIsDeletedChanging(bool value);
        partial void OnIsDeletedChanged();
        #endregion

        public TopicAssignment()
        {
            this._Curriculum = default(EntityRef<Curriculum>);
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

        [global::System.Data.Linq.Mapping.AssociationAttribute(Name = "Curriculum_TopicAssignment", Storage = "_Curriculum", ThisKey = "CurriculumRef", OtherKey = "Id", IsForeignKey = true)]
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
                        previousValue.TopicAssignments.Remove(this);
                    }
                    this._Curriculum.Entity = value;
                    if ((value != null))
                    {
                        value.TopicAssignments.Add(this);
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

        [global::System.Data.Linq.Mapping.AssociationAttribute(Name = "Topic_TopicAssignment", Storage = "_Topic", ThisKey = "TopicRef", OtherKey = "Id", IsForeignKey = true)]
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
                        previousValue.TopicAssignments.Remove(this);
                    }
                    this._Topic.Entity = value;
                    if ((value != null))
                    {
                        value.TopicAssignments.Add(this);
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

    [global::System.Data.Linq.Mapping.TableAttribute(Name = "dbo.TopicFeatures")]
    public partial class TopicFeature : INotifyPropertyChanging, INotifyPropertyChanged
    {

        private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);

        private int _TopicId;

        private int _FeatureId;

        private EntityRef<Feature> _Feature;

        private EntityRef<Topic> _Topic;

        #region Extensibility Method Definitions
        partial void OnLoaded();
        partial void OnValidate(System.Data.Linq.ChangeAction action);
        partial void OnCreated();
        partial void OnTopicIdChanging(int value);
        partial void OnTopicIdChanged();
        partial void OnFeatureIdChanging(int value);
        partial void OnFeatureIdChanged();
        #endregion

        public TopicFeature()
        {
            this._Feature = default(EntityRef<Feature>);
            this._Topic = default(EntityRef<Topic>);
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
                    if (this._Topic.HasLoadedOrAssignedValue)
                    {
                        throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
                    }
                    this.OnTopicIdChanging(value);
                    this.SendPropertyChanging();
                    this._TopicId = value;
                    this.SendPropertyChanged("TopicId");
                    this.OnTopicIdChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_FeatureId", DbType = "Int NOT NULL", IsPrimaryKey = true)]
        public int FeatureId
        {
            get
            {
                return this._FeatureId;
            }
            set
            {
                if ((this._FeatureId != value))
                {
                    if (this._Feature.HasLoadedOrAssignedValue)
                    {
                        throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
                    }
                    this.OnFeatureIdChanging(value);
                    this.SendPropertyChanging();
                    this._FeatureId = value;
                    this.SendPropertyChanged("FeatureId");
                    this.OnFeatureIdChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.AssociationAttribute(Name = "Feature_TopicFeature", Storage = "_Feature", ThisKey = "FeatureId", OtherKey = "Id", IsForeignKey = true)]
        public Feature Feature
        {
            get
            {
                return this._Feature.Entity;
            }
            set
            {
                Feature previousValue = this._Feature.Entity;
                if (((previousValue != value)
                            || (this._Feature.HasLoadedOrAssignedValue == false)))
                {
                    this.SendPropertyChanging();
                    if ((previousValue != null))
                    {
                        this._Feature.Entity = null;
                        previousValue.TopicFeatures.Remove(this);
                    }
                    this._Feature.Entity = value;
                    if ((value != null))
                    {
                        value.TopicFeatures.Add(this);
                        this._FeatureId = value.Id;
                    }
                    else
                    {
                        this._FeatureId = default(int);
                    }
                    this.SendPropertyChanged("Feature");
                }
            }
        }

        [global::System.Data.Linq.Mapping.AssociationAttribute(Name = "Topic_TopicFeature", Storage = "_Topic", ThisKey = "TopicId", OtherKey = "Id", IsForeignKey = true)]
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
                        previousValue.TopicFeatures.Remove(this);
                    }
                    this._Topic.Entity = value;
                    if ((value != null))
                    {
                        value.TopicFeatures.Add(this);
                        this._TopicId = value.Id;
                    }
                    else
                    {
                        this._TopicId = default(int);
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

    [global::System.Data.Linq.Mapping.TableAttribute(Name = "dbo.Topics")]
    public partial class Topic : INotifyPropertyChanging, INotifyPropertyChanged
    {

        private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);

        private int _Id;

        private string _Name;

        private System.DateTime _Created;

        private System.DateTime _Updated;

        private int _ChapterRef;

        private System.Nullable<int> _CourseRef;

        private int _SortOrder;

        private int _TopicTypeRef;

        private bool _IsDeleted;

        private EntitySet<UserTopicScore> _UserTopicScores;

        private EntitySet<TopicAssignment> _TopicAssignments;

        private EntitySet<TopicFeature> _TopicFeatures;

        private EntityRef<Chapter> _Chapter;

        private EntityRef<TopicType> _TopicType;

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
        partial void OnChapterRefChanging(int value);
        partial void OnChapterRefChanged();
        partial void OnCourseRefChanging(System.Nullable<int> value);
        partial void OnCourseRefChanged();
        partial void OnSortOrderChanging(int value);
        partial void OnSortOrderChanged();
        partial void OnTopicTypeRefChanging(int value);
        partial void OnTopicTypeRefChanged();
        partial void OnIsDeletedChanging(bool value);
        partial void OnIsDeletedChanged();
        #endregion

        public Topic()
        {
            this._UserTopicScores = new EntitySet<UserTopicScore>(new Action<UserTopicScore>(this.attach_UserTopicScores), new Action<UserTopicScore>(this.detach_UserTopicScores));
            this._TopicAssignments = new EntitySet<TopicAssignment>(new Action<TopicAssignment>(this.attach_TopicAssignments), new Action<TopicAssignment>(this.detach_TopicAssignments));
            this._TopicFeatures = new EntitySet<TopicFeature>(new Action<TopicFeature>(this.attach_TopicFeatures), new Action<TopicFeature>(this.detach_TopicFeatures));
            this._Chapter = default(EntityRef<Chapter>);
            this._TopicType = default(EntityRef<TopicType>);
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

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_TopicTypeRef", DbType = "Int NOT NULL")]
        public int TopicTypeRef
        {
            get
            {
                return this._TopicTypeRef;
            }
            set
            {
                if ((this._TopicTypeRef != value))
                {
                    if (this._TopicType.HasLoadedOrAssignedValue)
                    {
                        throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
                    }
                    this.OnTopicTypeRefChanging(value);
                    this.SendPropertyChanging();
                    this._TopicTypeRef = value;
                    this.SendPropertyChanged("TopicTypeRef");
                    this.OnTopicTypeRefChanged();
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

        [global::System.Data.Linq.Mapping.AssociationAttribute(Name = "Topic_UserTopicScore", Storage = "_UserTopicScores", ThisKey = "Id", OtherKey = "TopicId")]
        public EntitySet<UserTopicScore> UserTopicScores
        {
            get
            {
                return this._UserTopicScores;
            }
            set
            {
                this._UserTopicScores.Assign(value);
            }
        }

        [global::System.Data.Linq.Mapping.AssociationAttribute(Name = "Topic_TopicAssignment", Storage = "_TopicAssignments", ThisKey = "Id", OtherKey = "TopicRef")]
        public EntitySet<TopicAssignment> TopicAssignments
        {
            get
            {
                return this._TopicAssignments;
            }
            set
            {
                this._TopicAssignments.Assign(value);
            }
        }

        [global::System.Data.Linq.Mapping.AssociationAttribute(Name = "Topic_TopicFeature", Storage = "_TopicFeatures", ThisKey = "Id", OtherKey = "TopicId")]
        public EntitySet<TopicFeature> TopicFeatures
        {
            get
            {
                return this._TopicFeatures;
            }
            set
            {
                this._TopicFeatures.Assign(value);
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

        [global::System.Data.Linq.Mapping.AssociationAttribute(Name = "TopicType_Topic", Storage = "_TopicType", ThisKey = "TopicTypeRef", OtherKey = "Id", IsForeignKey = true)]
        public TopicType TopicType
        {
            get
            {
                return this._TopicType.Entity;
            }
            set
            {
                TopicType previousValue = this._TopicType.Entity;
                if (((previousValue != value)
                            || (this._TopicType.HasLoadedOrAssignedValue == false)))
                {
                    this.SendPropertyChanging();
                    if ((previousValue != null))
                    {
                        this._TopicType.Entity = null;
                        previousValue.Topics.Remove(this);
                    }
                    this._TopicType.Entity = value;
                    if ((value != null))
                    {
                        value.Topics.Add(this);
                        this._TopicTypeRef = value.Id;
                    }
                    else
                    {
                        this._TopicTypeRef = default(int);
                    }
                    this.SendPropertyChanged("TopicType");
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

        private void attach_UserTopicScores(UserTopicScore entity)
        {
            this.SendPropertyChanging();
            entity.Topic = this;
        }

        private void detach_UserTopicScores(UserTopicScore entity)
        {
            this.SendPropertyChanging();
            entity.Topic = null;
        }

        private void attach_TopicAssignments(TopicAssignment entity)
        {
            this.SendPropertyChanging();
            entity.Topic = this;
        }

        private void detach_TopicAssignments(TopicAssignment entity)
        {
            this.SendPropertyChanging();
            entity.Topic = null;
        }

        private void attach_TopicFeatures(TopicFeature entity)
        {
            this.SendPropertyChanging();
            entity.Topic = this;
        }

        private void detach_TopicFeatures(TopicFeature entity)
        {
            this.SendPropertyChanging();
            entity.Topic = null;
        }
    }

    [global::System.Data.Linq.Mapping.TableAttribute(Name = "dbo.TopicTypes")]
    public partial class TopicType : INotifyPropertyChanging, INotifyPropertyChanged
    {

        private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);

        private int _Id;

        private string _Name;

        private EntitySet<Topic> _Topics;

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

        [global::System.Data.Linq.Mapping.AssociationAttribute(Name = "TopicType_Topic", Storage = "_Topics", ThisKey = "Id", OtherKey = "TopicTypeRef")]
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
            entity.TopicType = this;
        }

        private void detach_Topics(Topic entity)
        {
            this.SendPropertyChanging();
            entity.TopicType = null;
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

        private EntitySet<UserTopicScore> _UserTopicScores;

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
            this._UserTopicScores = new EntitySet<UserTopicScore>(new Action<UserTopicScore>(this.attach_UserTopicScores), new Action<UserTopicScore>(this.detach_UserTopicScores));
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

        [global::System.Data.Linq.Mapping.AssociationAttribute(Name = "User_UserTopicScore", Storage = "_UserTopicScores", ThisKey = "Id", OtherKey = "UserId")]
        public EntitySet<UserTopicScore> UserTopicScores
        {
            get
            {
                return this._UserTopicScores;
            }
            set
            {
                this._UserTopicScores.Assign(value);
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

        private void attach_UserTopicScores(UserTopicScore entity)
        {
            this.SendPropertyChanging();
            entity.User = this;
        }

        private void detach_UserTopicScores(UserTopicScore entity)
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

    [global::System.Data.Linq.Mapping.TableAttribute(Name = "dbo.Curriculums")]
    public partial class Curriculum : INotifyPropertyChanging, INotifyPropertyChanged
    {

        private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);

        private int _Id;

        private int _UserGroupRef;

        private int _DisciplineRef;

        private bool _IsDeleted;

        private EntitySet<TopicAssignment> _TopicAssignments;

        private EntitySet<Timeline> _Timelines;

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
        partial void OnIsDeletedChanging(bool value);
        partial void OnIsDeletedChanged();
        #endregion

        public Curriculum()
        {
            this._TopicAssignments = new EntitySet<TopicAssignment>(new Action<TopicAssignment>(this.attach_TopicAssignments), new Action<TopicAssignment>(this.detach_TopicAssignments));
            this._Timelines = new EntitySet<Timeline>(new Action<Timeline>(this.attach_Timelines), new Action<Timeline>(this.detach_Timelines));
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

        [global::System.Data.Linq.Mapping.AssociationAttribute(Name = "Curriculum_TopicAssignment", Storage = "_TopicAssignments", ThisKey = "Id", OtherKey = "CurriculumRef")]
        public EntitySet<TopicAssignment> TopicAssignments
        {
            get
            {
                return this._TopicAssignments;
            }
            set
            {
                this._TopicAssignments.Assign(value);
            }
        }

        [global::System.Data.Linq.Mapping.AssociationAttribute(Name = "Curriculum_Timeline", Storage = "_Timelines", ThisKey = "Id", OtherKey = "CurriculumRef")]
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

        private void attach_TopicAssignments(TopicAssignment entity)
        {
            this.SendPropertyChanging();
            entity.Curriculum = this;
        }

        private void detach_TopicAssignments(TopicAssignment entity)
        {
            this.SendPropertyChanging();
            entity.Curriculum = null;
        }

        private void attach_Timelines(Timeline entity)
        {
            this.SendPropertyChanging();
            entity.Curriculum = this;
        }

        private void detach_Timelines(Timeline entity)
        {
            this.SendPropertyChanging();
            entity.Curriculum = null;
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

    [global::System.Data.Linq.Mapping.TableAttribute(Name = "dbo.Topics")]
    public partial class Topic : INotifyPropertyChanging, INotifyPropertyChanged
    {

        private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);

        private int _Id;

        private string _Name;

        private System.DateTime _Created;

        private System.DateTime _Updated;

        private int _ChapterRef;

        private System.Nullable<int> _CourseRef;

        private int _SortOrder;

        private int _TopicTypeRef;

        private bool _IsDeleted;

        private EntitySet<TopicAssignment> _TopicAssignments;

        private EntityRef<TopicType> _TopicType;

        private EntityRef<Chapter> _Chapter;

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
        partial void OnChapterRefChanging(int value);
        partial void OnChapterRefChanged();
        partial void OnCourseRefChanging(System.Nullable<int> value);
        partial void OnCourseRefChanged();
        partial void OnSortOrderChanging(int value);
        partial void OnSortOrderChanged();
        partial void OnTopicTypeRefChanging(int value);
        partial void OnTopicTypeRefChanged();
        partial void OnIsDeletedChanging(bool value);
        partial void OnIsDeletedChanged();
        #endregion

        public Topic()
        {
            this._TopicAssignments = new EntitySet<TopicAssignment>(new Action<TopicAssignment>(this.attach_TopicAssignments), new Action<TopicAssignment>(this.detach_TopicAssignments));
            this._TopicType = default(EntityRef<TopicType>);
            this._Chapter = default(EntityRef<Chapter>);
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

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_TopicTypeRef", DbType = "Int NOT NULL")]
        public int TopicTypeRef
        {
            get
            {
                return this._TopicTypeRef;
            }
            set
            {
                if ((this._TopicTypeRef != value))
                {
                    if (this._TopicType.HasLoadedOrAssignedValue)
                    {
                        throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
                    }
                    this.OnTopicTypeRefChanging(value);
                    this.SendPropertyChanging();
                    this._TopicTypeRef = value;
                    this.SendPropertyChanged("TopicTypeRef");
                    this.OnTopicTypeRefChanged();
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

        [global::System.Data.Linq.Mapping.AssociationAttribute(Name = "Topic_TopicAssignment", Storage = "_TopicAssignments", ThisKey = "Id", OtherKey = "TopicRef")]
        public EntitySet<TopicAssignment> TopicAssignments
        {
            get
            {
                return this._TopicAssignments;
            }
            set
            {
                this._TopicAssignments.Assign(value);
            }
        }

        [global::System.Data.Linq.Mapping.AssociationAttribute(Name = "TopicType_Topic", Storage = "_TopicType", ThisKey = "TopicTypeRef", OtherKey = "Id", IsForeignKey = true)]
        public TopicType TopicType
        {
            get
            {
                return this._TopicType.Entity;
            }
            set
            {
                TopicType previousValue = this._TopicType.Entity;
                if (((previousValue != value)
                            || (this._TopicType.HasLoadedOrAssignedValue == false)))
                {
                    this.SendPropertyChanging();
                    if ((previousValue != null))
                    {
                        this._TopicType.Entity = null;
                        previousValue.Topics.Remove(this);
                    }
                    this._TopicType.Entity = value;
                    if ((value != null))
                    {
                        value.Topics.Add(this);
                        this._TopicTypeRef = value.Id;
                    }
                    else
                    {
                        this._TopicTypeRef = default(int);
                    }
                    this.SendPropertyChanged("TopicType");
                }
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

        private void attach_TopicAssignments(TopicAssignment entity)
        {
            this.SendPropertyChanging();
            entity.Topic = this;
        }

        private void detach_TopicAssignments(TopicAssignment entity)
        {
            this.SendPropertyChanging();
            entity.Topic = null;
        }
    }

    [global::System.Data.Linq.Mapping.TableAttribute(Name = "dbo.TopicTypes")]
    public partial class TopicType : INotifyPropertyChanging, INotifyPropertyChanged
    {

        private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);

        private int _Id;

        private string _Name;

        private EntitySet<Topic> _Topics;

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

        [global::System.Data.Linq.Mapping.AssociationAttribute(Name = "TopicType_Topic", Storage = "_Topics", ThisKey = "Id", OtherKey = "TopicTypeRef")]
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
            entity.TopicType = this;
        }

        private void detach_Topics(Topic entity)
        {
            this.SendPropertyChanging();
            entity.TopicType = null;
        }
    }

    [global::System.Data.Linq.Mapping.TableAttribute(Name = "dbo.TopicAssignments")]
    public partial class TopicAssignment : INotifyPropertyChanging, INotifyPropertyChanged
    {

        private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);

        private int _Id;

        private int _TopicRef;

        private int _CurriculumRef;

        private int _MaxScore;

        private bool _IsDeleted;

        private EntityRef<Curriculum> _Curriculum;

        private EntityRef<Topic> _Topic;

        #region Extensibility Method Definitions
        partial void OnLoaded();
        partial void OnValidate(System.Data.Linq.ChangeAction action);
        partial void OnCreated();
        partial void OnIdChanging(int value);
        partial void OnIdChanged();
        partial void OnTopicRefChanging(int value);
        partial void OnTopicRefChanged();
        partial void OnCurriculumRefChanging(int value);
        partial void OnCurriculumRefChanged();
        partial void OnMaxScoreChanging(int value);
        partial void OnMaxScoreChanged();
        partial void OnIsDeletedChanging(bool value);
        partial void OnIsDeletedChanged();
        #endregion

        public TopicAssignment()
        {
            this._Curriculum = default(EntityRef<Curriculum>);
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

        [global::System.Data.Linq.Mapping.AssociationAttribute(Name = "Curriculum_TopicAssignment", Storage = "_Curriculum", ThisKey = "CurriculumRef", OtherKey = "Id", IsForeignKey = true)]
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
                        previousValue.TopicAssignments.Remove(this);
                    }
                    this._Curriculum.Entity = value;
                    if ((value != null))
                    {
                        value.TopicAssignments.Add(this);
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

        [global::System.Data.Linq.Mapping.AssociationAttribute(Name = "Topic_TopicAssignment", Storage = "_Topic", ThisKey = "TopicRef", OtherKey = "Id", IsForeignKey = true)]
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
                        previousValue.TopicAssignments.Remove(this);
                    }
                    this._Topic.Entity = value;
                    if ((value != null))
                    {
                        value.TopicAssignments.Add(this);
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

        private int _CurriculumRef;

        private System.Nullable<int> _ChapterRef;

        private bool _IsDeleted;

        private EntityRef<Curriculum> _Curriculum;

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
        partial void OnCurriculumRefChanging(int value);
        partial void OnCurriculumRefChanged();
        partial void OnChapterRefChanging(System.Nullable<int> value);
        partial void OnChapterRefChanged();
        partial void OnIsDeletedChanging(bool value);
        partial void OnIsDeletedChanged();
        #endregion

        public Timeline()
        {
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

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_ChapterRef", DbType = "Int")]
        public System.Nullable<int> ChapterRef
        {
            get
            {
                return this._ChapterRef;
            }
            set
            {
                if ((this._ChapterRef != value))
                {
                    this.OnChapterRefChanging(value);
                    this.SendPropertyChanging();
                    this._ChapterRef = value;
                    this.SendPropertyChanged("ChapterRef");
                    this.OnChapterRefChanged();
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

        [global::System.Data.Linq.Mapping.AssociationAttribute(Name = "Curriculum_Timeline", Storage = "_Curriculum", ThisKey = "CurriculumRef", OtherKey = "Id", IsForeignKey = true)]
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
                        previousValue.Timelines.Remove(this);
                    }
                    this._Curriculum.Entity = value;
                    if ((value != null))
                    {
                        value.Timelines.Add(this);
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
    }

    [global::System.Data.Linq.Mapping.TableAttribute(Name = "dbo.Chapters")]
    public partial class Chapter : INotifyPropertyChanging, INotifyPropertyChanged
    {

        private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);

        private int _Id;

        private string _Name;

        private System.DateTime _Created;

        private System.DateTime _Updated;

        private int _DisciplineRef;

        private bool _IsDeleted;

        private EntitySet<Topic> _Topics;

        private EntityRef<Discipline> _Discipline;

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
        partial void OnDisciplineRefChanging(int value);
        partial void OnDisciplineRefChanged();
        partial void OnIsDeletedChanging(bool value);
        partial void OnIsDeletedChanged();
        #endregion

        public Chapter()
        {
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
