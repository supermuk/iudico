using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Serialization;
using System.Linq;

namespace FireFly.CourseEditor.Course.Manifest
{
    [XmlType("organization", Namespace = ManifestNamespaces.Imscp)]
    [Description("Element describes a particular hierarchical organization. The content of organization can be a lesson, module, course, chapter, etc")]
    [Category("Main")]
    public class OrganizationType : AbstractManifestNode, IItemContainer, ITitled, ISequencing
    {
        private static int number = 1;

        private ManifestNodeList<ItemType> m_SubItems;

        private MetadataType metadataField;
        
        private string identifierField;

        private string structureField;

        private SequencingType sequencingField;

        private bool objectivesGlobalToSystemField = true;

        private bool sharedDataGlobalToSystemField = true;

       // private SequencingPattern sequencingPatternField;

        public OrganizationType()
        {
            identifier = string.Concat("Organization", number++);
            structureField = "hierarchical";
            this.SequencingPatterns = new SequencingPatternList(this);
            SequencingManager.CreateOrganizationDefaultSequencing(this);
            
            //To protect local objectives from outer intrusion.
            this.objectivesGlobalToSystem = false;
            
        }

        //TODO: Fix FFServer and remove this stub
        [XmlElement("title")]
        public string _TitleStub
        {
            get { return Title; }
            set
            {
                // just ignore it
            }
        }

        [XmlIgnore]
        public string Title
        {
            get
            {
                return IsInDeserializationMode ? null : ((ManifestType)Parent.Parent.Parent).Title;
            }
            set
            {
                if (IsInDeserializationMode)
                {
                    // for old versions - just ignore
                }
                else
                {
                    ((ManifestType) Parent.Parent.Parent).Title = value;
                }
            }
        }

        public event Action TitleChanged
        {
            add { ((ManifestType) Parent.Parent).TitleChanged += value; }
            remove { ((ManifestType) Parent.Parent).TitleChanged -= value; }
        }

        [Description("Contains relevant information that describes the content package as a whole.")]
        [Category("Main")]
        public MetadataType metadata
        {
            get
            {
                return metadataField;
            }
            set
            {
                metadataField = value;
                Course.NotifyManifestChanged(this, ManifestChangeTypes.Changed);
            }
        }

        [XmlAttribute(DataType = "ID")]
        public string identifier
        {
            get
            {
                return identifierField;
            }
            set
            {
                identifierField = value;
                Course.NotifyManifestChanged(this, ManifestChangeTypes.Changed);
            }
        }

        [XmlAttribute]
        [DefaultValue("hierarchical")]
        public string structure
        {
            get
            {
                return structureField;
            }
            set
            {
                structureField = value;
                Course.NotifyManifestChanged(this, ManifestChangeTypes.Changed);
            }
        }

        [Category("Main")]
        [XmlElement("sequencing", Namespace = ManifestNamespaces.Imsss)]
        public SequencingType Sequencing
        {
            get
            {
                return sequencingField;
            }
            set
            {
                sequencingField = value;
                Course.NotifyManifestChanged(this, new IManifestNode[1] { value }, ManifestChangeTypes.ChildrenAdded);
            }
        }

        [Category("Main")]
        [XmlAttribute("objectivesGlobalToSystem",Namespace= ManifestNamespaces.Adlseq)]
        [DefaultValue(true)]
        public bool objectivesGlobalToSystem
        {
            get
            {
                return this.objectivesGlobalToSystemField;
            }
            set
            {
                this.objectivesGlobalToSystemField = value;
            }
        }

        [Category("Main")]
        [XmlAttribute("sharedDataGlobalToSystem", Namespace = ManifestNamespaces.Adlcp)]
        [DefaultValue(true)]
        public bool sharedDataGlobalToSystem
        {
            get
            {
                return this.sharedDataGlobalToSystemField;
            }
            set
            {
                this.sharedDataGlobalToSystemField = value;
            }
        }

        public override string ToString()
        {
            return Title;
        }

        public void RemoveChild(IManifestNode child)
        {
            switch (child.GetType().Name)
            {
                case "ItemType":
                    var item = child as ItemType;
                    if (m_SubItems.Contains(item))
                    {
                        m_SubItems.Remove(item);
                        return;
                    }
                    break;

                case "MetadataType":
                    metadata = null;
                    return;
            }
            throw new FireFlyException("Item '{0}' not found", child);
        }

        [XmlElement("item")]
        [Description("Element is a node that describes the hierarchical structure of the organization")]
        [Category("Main")]
        public ManifestNodeList<ItemType> SubItems
        {
            get
            {
                if (m_SubItems == null)
                {
                    m_SubItems = new ManifestNodeList<ItemType>(this);
                }
                return m_SubItems;
            }
            set
            {
                m_SubItems = value;
                Course.NotifyManifestChanged(this, ManifestChangeTypes.ChildrenReordered );
            }
        }

        [XmlIgnore]
        [Description("Returns boolean 'true' if organization has zero sub-items. Otherwise 'false'.")]
        public bool IsLeaf
        {
            get
            {
                return (this.SubItems.Count == 0);
            }
        }

        /// <summary>
        /// Inserts grouping item, which is child of current item and contains sub items of current item.
        /// A parent of child nodes should be changed to new grouping Item.
        /// </summary>
        /// <param name="groupingItem"><see cref="ItemType"/> item, which would act as grouping item.</param>
        public void InsertGroupingItem([NotNull]ItemType groupingItem)
        {
            if (groupingItem == null)
            {
                throw new ArgumentNullException("Grouping Item can't be null!");
            }

            if (groupingItem.PageType != PageType.Chapter && groupingItem.PageType != PageType.ControlChapter)
            {
                throw new ArgumentException("Grouping Item should be a Chapter or Control Chapter!");
            }

            //1. Add all children to grouping item.
            foreach (ItemType item in this.SubItems)
            {
                groupingItem.SubItems.Add(item);
            }

            //2. Clear list of children, but not use Removing tool!!!
            this.SubItems = new ManifestNodeList<ItemType>(this);

            //3. Add grouping item to list of children.
            this.SubItems.Add(groupingItem);
        }

        /// <summary>
        /// Adds all subItems of current item to parent of current item. Removes current item.
        /// </summary>
        public void RemoveAndMerge()
        {
            if (this.Parent is IItemContainer == false)
            {
                throw new InvalidOperationException("Parent of '" + this.Title + "' should be a container of items!");
            }

            IItemContainer parent = this.Parent as IItemContainer;

            //1. Add all children to Parent
            parent.SubItems.AddRange(this.SubItems);

            //2. Clean subItems but do not remove!
            this.SubItems = new ManifestNodeList<ItemType>();

            //3. Remove item.
            parent.RemoveChild(this);
        }

        private static IEnumerable<ItemType> enumerateItemRec(IItemContainer item)
        {
            foreach (var i in item.SubItems)
            {
                yield return i;
                foreach (var ri in enumerateItemRec(i))
                {
                    yield return ri;
                } 
            }
        }

        public IEnumerable<ItemType> Items
        {
            get
            {
                foreach (var i in enumerateItemRec(this))
                {
                    yield return i;
                }
            }
        }

        [Description("Sequencing pattern collection. Defines sequencing strategy of current item or/and it's subitems or/and parent.")]
        [XmlIgnore]
        public SequencingPatternList SequencingPatterns
        {
            get;
            set;
        }

        [Description("Array of string representation of Sequencing patterns ids applied to current node.")]
        [XmlArray("sequencingPatterns")]
        [XmlArrayItem("sequencingPattern")]
        public string[] SequencingPatternsIDArray
        {
            get
            {
                var query = from pattern in this.SequencingPatterns
                            select pattern.ID;
                if (query.Count() == 0)
                {
                    return null;
                }
                return query.ToArray();
            }
            set
            {
                if (value == null)
                {
                    this.SequencingPatterns = new SequencingPatternList(this);
                    return;
                }                

                foreach (Type type in SequencingPatternList.GetAllKnownPatterns())
                {
                    ISequencingPattern pattern = type.GetConstructor(new Type[] { }).Invoke(new object[] { }) as ISequencingPattern;

                    string patternID = type.GetProperty("ID").GetValue(pattern, new object[] { }) as string;

                    if (value.Exists(curr => curr == patternID) == true)
                    {
                        if (this.SequencingPatterns == null)
                        {
                            this.SequencingPatterns = new SequencingPatternList(this);
                        }
                        this.SequencingPatterns.Add(type);
                    }
                }
            }
        }
    }

    [XmlType("organizations", Namespace = ManifestNamespaces.Imscp)]
    [Description("Contains the content structure or organization of the learning resources making up a stand-alone unit or units of instruction")]
    [Category("Main")]
    public class OrganizationsType : AbstractManifestNode, IOrganizationContainer
    {
        public OrganizationsType()
        {
            organizationField = new ManifestNodeList<OrganizationType>(this);
        }

        private ManifestNodeList<OrganizationType> organizationField;

        private string defaultField;

        [XmlElement("organization")]
        [Description("Element describes a particular hierarchical organization.The content organization can be a lesson, module, course, chapter, etc")]
        [Category("Main")]
        public ManifestNodeList<OrganizationType> Organizations
        {
            get
            {
                if (organizationField == null)
                {
                    organizationField = new ManifestNodeList<OrganizationType>(this);
                }
                return organizationField;
            }
            set
            {
                organizationField = value;
                Course.NotifyManifestChanged(this, ManifestChangeTypes.Changed);
            }
        }

        [XmlAttribute(DataType = "IDREF")]
        public string @default
        {
            get
            {
                return defaultField;
            }
            set
            {
                defaultField = value;
                Course.NotifyManifestChanged(this, ManifestChangeTypes.Changed);
            }
        }

        #region IContainer Members

        public void RemoveChild(IManifestNode child)
        {
            switch (child.GetType().Name)
            {
                case "OrganizationType":
                    {
                        var o = child as OrganizationType;
                        if (Organizations.Contains(o))
                        {
                            Organizations.Remove(o);
                            return;
                        }
                        break;
                    }
            }
            throw new FireFlyException("Item '{0}' not found", child);
        }

        #endregion
    }
}
