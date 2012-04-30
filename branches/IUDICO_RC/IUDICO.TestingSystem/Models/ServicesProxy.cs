// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ServicesProxy.cs" company="">
//   
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;

using IUDICO.Common.Models.Services;

namespace IUDICO.TestingSystem.Models
{
    /// <summary>
    /// Singleton Proxy holds reference to Lms Service.
    /// And provides easier access to other services.
    /// </summary>
    public class ServicesProxy
    {
        #region Service Properties

        public ILmsService LmsService { get; protected set; }

        public ICourseService CourseService
        {
            get
            {
                if (this.LmsService == null)
                {
                    throw new NullReferenceException("You should initialize LMSService first!");
                }
                return this.LmsService.FindService<ICourseService>();
            }
        }

        public IDisciplineService DisciplineService
        {
            get
            {
                if (this.LmsService == null)
                {
                    throw new NullReferenceException("You should initialize LMSService first!");
                }
                return this.LmsService.FindService<IDisciplineService>();
            }
        }

        public ISearchService SearchService
        {
            get
            {
                if (this.LmsService == null)
                {
                    throw new NullReferenceException("You should initialize LMSService first!");
                }
                return this.LmsService.FindService<ISearchService>();
            }
        }

        public IStatisticsService StatisticsService
        {
            get
            {
                if (this.LmsService == null)
                {
                    throw new NullReferenceException("You should initialize LMSService first!");
                }
                return this.LmsService.FindService<IStatisticsService>();
            }
        }

        public IUserService UserService
        {
            get
            {
                if (this.LmsService == null)
                {
                    throw new NullReferenceException("You should initialize LMSService first!");
                }
                return this.LmsService.FindService<IUserService>();
            }
        }

        public ITestingService TestingService
        {
            get
            {
                if (this.LmsService == null)
                {
                    throw new NullReferenceException("You should initialize LMSService first!");
                }
                return this.LmsService.FindService<ITestingService>();
            }
        }

        #endregion

        #region Singleton Implementation

        private static ServicesProxy instance;

        public static ServicesProxy Instance
        {
            get
            {
                return instance ?? (instance = new ServicesProxy());
            }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Make constructor by default private to implement singleton pattern.
        /// </summary>
        private ServicesProxy()
        {
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Initializes Lms Service property with appropriate service value
        /// which allows in further receive other services.
        /// </summary>
        /// <param name="service">ILmsService represents Lms Service.</param>
        public void Initialize(ILmsService service)
        {
            this.LmsService = service;
        }

        #endregion
    }
}