namespace FireFly.CourseEditor.Course.Manifest
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.IO;
    using System.Reflection;

    /// <summary>
    /// This class encapsulate all logic of working with course
    /// </summary>
    public static class Course
    {
        private static readonly Dictionary<string, int> __ContainedFileResource = new Dictionary<string, int>();

        /// <summary>
        /// Defines the name of file with answers in course package
        /// </summary>
        public const string ANSWERS_FILE_NAME = "answers.xml";

        /// <summary>
        /// Defines the name of file with answers in course package
        /// </summary>
        public const string LANGUAGES_NAMESPACE = "FireFly.CourseEditor.GUI.HtmlEditor.HighlightControl.";

        /// <summary>
        /// Defines the name of file with answers in course package
        /// </summary>
        public const string MANIFEST_FILE_NAME = "imsmanifest.xml";


        ///<summary>
        /// Namespace of <see cref="Course"/> class
        ///</summary>
        public const string NAMESPACE = "FireFly.CourseEditor.Course.";

        /// <summary>
        /// Gets current state of course
        /// </summary>
        public static CourseStates State { get; private set; }

        /// <summary>
        /// Course's manifest
        /// </summary>
        [NotNull]
        public static ManifestType Manifest
        {
            [DebuggerStepThrough]
            get { return __Manifest; }
        }

        ///<summary>
        /// Current organization
        ///</summary>
        [NotNull]
        public static OrganizationType Organization
        {
            [DebuggerStepThrough]
            get
            {
                return __Organization;
            }
        }

        /// <summary>
        /// Gets or sets answers for course
        /// </summary>
        public static Answers Answers { get; private set; }

        /// <summary>
        /// Represents full path of folder that represents the opened course
        /// </summary>
        public static string FullPath
        {
            get { return __FullPath; }
            set { __FullPath = value; }
        }
        /// <summary>
        /// Check is course saved and can be closed
        /// </summary>
        /// <returns>If course can be closed return value is true</returns>
        private static bool CanClose()
        {
            if (CourseClosing != null)
            {
                var e = new CancelEventArgs();
                CourseClosing(e);
                return !e.Cancel;
            }
            return true;
        }

        /// <summary>
        /// Open course represented as folder.
        /// </summary>
        /// <param name="courseFileName">Path to directory contains the course's files</param>
        /// <returns>If this course was opened return true</returns>
        public static bool Open([NotNull]string courseFileName)
        {
            throw new NotImplementedException();

        }

        ///<summary>
        /// Checks is specified resources included in the course package and add them otherwise
        ///</summary>
        ///<param name="namespace">Namespace of resources</param>
        ///<param name="destination">Relative path to resources in course package</param>
        ///<param name="fileNames">List of file to check</param>
        ///<exception cref="FireFlyException"></exception>
        public static void EnsureCourseContainsResources([NotNull]string @namespace, [NotNull] string destination,
            params string[] fileNames)
        {
            var filePath = MapPath(destination);
            CreateDir(filePath);

            foreach (var fileName in fileNames)
            {
                if (!__ContainedFileResource.ContainsKey(fileName))
                {
                    var s = Assembly.GetEntryAssembly().GetManifestResourceStream(@namespace + fileName);
                    Debug.Assert(s != null);
                    var bts = new byte[s.Length];
                    s.Read(bts, 0, (int)s.Length);
                    File.WriteAllBytes(Path.Combine(filePath, fileName), bts);
                    __ContainedFileResource.Add(fileName, 1);
                }
                else
                {
                    __ContainedFileResource[fileName]++;
                }
            }
        }

        ///<summary>
        /// Releases resources located by <see cref="EnsureCourseContainsResources"/> method
        ///</summary>
        ///<param name="destination">Relative path to resources in course package</param>
        ///<param name="fileNames">List of file to release</param>
        ///<exception cref="FireFlyException"></exception>
        public static void ReleaseCourseResource([NotNull]string destination, params string[] fileNames)
        {
            string filePath = MapPath(destination);

            foreach (var fn in fileNames)
            {
                int count;
                if (!__ContainedFileResource.TryGetValue(fn, out count))
                {
                    throw new FireFlyException("Resource file '{0}' was not registered", fn);
                }
                if (count == 1)
                {
                    __ContainedFileResource.Remove(fn);
                    File.Delete(Path.Combine(filePath, fn));
                }
                else
                {
                    __ContainedFileResource[fn]--;
                }
            }
            if (Directory.GetFiles(Path.GetDirectoryName(filePath), "*", SearchOption.AllDirectories).Length == 0)
            {
                Directory.Delete(filePath);
            }
        }

        /// <summary>
        /// Open zip package that contains course's files
        /// </summary>
        /// <param name="fileName">If course is opened return true</param>
        public static bool OpenZipPackage([NotNull]string fileName)
        {
            throw new NotImplementedException();

        }

        /// <summary>
        /// Save current course to zip package
        /// </summary>
        /// <param name="fileName">File name of zip package. If file not exists it will be created</param>
        public static bool SaveToZipPackage([NotNull]string fileName)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Maps relative path to absolute
        /// </summary>
        /// <param name="path">Path to map</param>
        /// <returns>Mapped absolute path</returns>
        [NotNull]
        public static string MapPath([NotNull]string path)
        {
            return Path.Combine(FullPath, path);
        }

        /// <summary>
        /// Creates directory in course package.
        /// </summary>
        /// <param name="dir">Name of new folder</param>
        public static void CreateDir([NotNull]string dir)
        {
            Directory.CreateDirectory(MapPath(dir));
        }

        /// <summary>
        /// Close opened course
        /// </summary>
        /// <returns>If course was closed return false</returns>
        public static bool Close()
        {
            throw new NotImplementedException();

        }

        /// <summary>
        /// Save all changes of current course to folder that represents is
        /// </summary>
        public static bool Save()
        {
            throw new NotImplementedException();

        }

        /// <summary>
        /// Creates new course and opens it.
        /// </summary>
        public static void CreateNew()
        {
            throw new NotImplementedException();


        }

        internal static void NotifyChanged()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Notify course about manifest was changed. 
        /// </summary>
        /// <param name="node">Node of manifest that was changed</param>
        /// <param name="aNode">Instance of additional node (see documentation for <see cref="ManifestChangedEventArgs"/>)</param>
        /// <param name="changeType">Type of changed that was made</param>
        internal static void NotifyManifestChanged([NotNull]IManifestNode node, [NotNull] IManifestNode[] aNode,
            ManifestChangeTypes changeType)
        {
            throw new NotImplementedException();

        }

        /// <summary>
        /// Notify course about manifest was changed. 
        /// </summary>
        /// <param name="node">Node of manifest that was changed</param>
        /// <param name="changeType">Type of occurred changes</param>
        internal static void NotifyManifestChanged([NotNull] IManifestNode node, ManifestChangeTypes changeType)
        {
            NotifyChanged();
            if (ManifestChanged != null && (State & CourseStates.Saving) == 0)
            {
                ManifestChanged(new ManifestChangedEventArgs(node, null, changeType));
            }
        }

        ///// <summary>
        ///// Occurs on manifest changing
        ///// </summary>
        //public static event ManifestChangingDelegate ManifestChanging;

        /// <summary>
        /// Occurs after manifest changed
        /// </summary>
        public static event Action<ManifestChangedEventArgs> ManifestChanged;

        /// <summary>
        /// Occurs when course opened
        /// </summary>
        public static event Action CourseOpened;

        /// <summary>
        /// Occurs on closing course
        /// </summary>
        public static event Action<CancelEventArgs> CourseClosing;

        /// <summary>
        /// Occurs when course was closed
        /// </summary>
        public static event Action CourseClosed;

        ///<summary>
        /// Occurs before course started to saving
        ///</summary>
        public static event Action CourseSaving;

        ///<summary>
        /// Raises when course is saved
        ///</summary>
        public static event Action CourseSaved;

        ///<summary>
        /// Raises when course change
        ///</summary>
        public static event Action CourseChanged;
               
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private static string __FullPath;
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private static ManifestType __Manifest;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private static OrganizationType __Organization;
    }
        ///<summary>
        /// Type of change
        ///</summary>
        [Flags]
        public enum ManifestChangeTypes
        {
            Changed = 1,
            ChildrenAdded = 2,
            ChildrenRemoved = 4,
            ChildrenReordered = 8
        }

        ///<summary>
        ///
        ///</summary>
        public class ManifestChangingEventArgs : CancelEventArgs
        {
            /// <summary>
            /// </summary>
            /// <param name="node">Manifest node that will be changed</param>
            public ManifestChangingEventArgs([NotNull]IManifestNode node)
            {
                ManifestNode = node;
            }

            /// <summary>
            /// Represents the node of manifest that will be changed
            /// </summary>
            public IManifestNode ManifestNode { get; set; }
        }

        ///<summary>
        /// Arguments about Manifest changed events
        ///</summary>
        public class ManifestChangedEventArgs : EventArgs
        {
            ///<summary>
            ///  Additional nodes
            ///</summary>
            public readonly IManifestNode[] Nodes;

            ///<summary>
            ///  Node that was changed
            ///</summary>
            public readonly IManifestNode ChangedNode;

            ///<summary>
            ///  Type of change
            ///</summary>
            public readonly ManifestChangeTypes ChangeType;

            ///<summary>
            ///  Creates new instance of <c>ManifestChangeEventArgs</c>
            ///</summary>
            ///<param name="changedNode"></param>
            ///<param name="nodes"></param>
            ///<param name="changeType"></param>
            public ManifestChangedEventArgs([NotNull]IManifestNode changedNode, [NotNull]IManifestNode[] nodes, ManifestChangeTypes changeType)
            {
                ChangedNode = changedNode;
                ChangeType = changeType;
                Nodes = nodes;
            }
        }

    ///<summary>
    /// Bit mask of states of <see cref="Course"/> 
    ///</summary>
    [Flags]
    public enum CourseStates
    {
        None = 0,
        Opened = 1,
        Modified = 2,
        Saving = 4,
        Opening = 8,
        UpgradeFailed = 16,
        OpenFailed = 32
    }
}